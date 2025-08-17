// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.WebSockets.WebSocketApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Serialization;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client.WebSockets;

public class WebSocketApiClient :
  BaseLogReceiver,
  IWebSocketService,
  ILogSource,
  IDisposable,
  IAsyncDisposable
{
    private readonly Channel<BaseRequest> _requests;
    private readonly CachedSynchronizedDictionary<long, (BaseRequest request, Type responseType, Func<BaseResponse, CancellationToken, ValueTask> handler)> _subscriptions = new CachedSynchronizedDictionary<long, (BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>)>();
    private readonly Uri _socketAddress;
    private readonly string _schema;
    private readonly SecureString _schemaValue;
    private static readonly TimeSpan _pingInterval = TimeSpan.FromSeconds(30.0);
    private static readonly TimeSpan _connectInterval = TimeSpan.FromSeconds(10.0);

    private WebSocketApiClient(Uri baseAddress, string schema, string value)
    {
        this._socketAddress = new Uri(baseAddress ?? throw new ArgumentNullException(nameof(baseAddress)), "ws");
        this._requests = Channel.CreateUnbounded<BaseRequest>();
        this._schema = schema;
        this._schemaValue = StringHelper.Secure(value);
    }

    public WebSocketApiClient(Uri baseAddress, SecureString token)
      : this(baseAddress, "Bearer", StringHelper.UnSecure(token))
    {
    }

    public WebSocketApiClient(Uri baseAddress, string login, SecureString password)
      : this(baseAddress, "Basic", StringHelper.Base64(StringHelper.UTF8($"{login}:{StringHelper.UnSecure(password)}")))
    {
    }

    public async ValueTask DisposeAsync()
    {
        WebSocketApiClient webSocketApiClient = this;
        ((Disposable)webSocketApiClient).Dispose();
        CancellationTokenSource cts = new CancellationTokenSource(WebSocketApiClient._connectInterval);
        try
        {
            await AsyncHelper.WhenAll(((IEnumerable<long>)webSocketApiClient._subscriptions.CachedKeys).Select<long, ValueTask>((Func<long, ValueTask>)(subId => this.UnSubscribeAsync(subId, cts.Token))));
            await webSocketApiClient._requests.Writer.WriteAsync((BaseRequest)new WebSocketApiClient.DisposeRequest(), cts.Token);
        }
        catch (Exception ex)
        {
            LoggingHelper.AddErrorLog((ILogReceiver)webSocketApiClient, ex);
        }
    }

    public int MaxConnect { get; set; } = 5;

    public int MaxReconnect { get; set; } = 10;

    public ValueTask ConnectAsync(CancellationToken cancellationToken)
    {
        return this.ConnectAsyncImpl(this.MaxConnect, cancellationToken);
    }

    private async ValueTask ConnectAsyncImpl(int attemptsLeft, CancellationToken cancellationToken)
    {
        WebSocketApiClient webSocketApiClient = this;
        ClientWebSocket socket = new ClientWebSocket();
        socket.Options.SetRequestHeader("Authorization", AuthSchemas.FormatAuth(webSocketApiClient._schema, webSocketApiClient._schemaValue));
        try
        {
            LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, "Connecting...", Array.Empty<object>());
            await socket.ConnectAsync(webSocketApiClient._socketAddress, cancellationToken);
            LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, "Connected.", Array.Empty<object>());
            KeyValuePair<long, (BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>)>[] keyValuePairArray = webSocketApiClient._subscriptions.CachedPairs;
            for (int index = 0; index < keyValuePairArray.Length; ++index)
            {
                BaseRequest baseRequest = keyValuePairArray[index].Value.Item1;
                LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, Converter.To<string>((object)baseRequest), Array.Empty<object>());
                await webSocketApiClient._requests.Writer.WriteAsync(baseRequest, cancellationToken);
            }
            keyValuePairArray = (KeyValuePair<long, (BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>)>[])null;
        }
        catch (Exception ex)
        {
            LoggingHelper.AddErrorLog((ILogReceiver)webSocketApiClient, ex);
            if (--attemptsLeft > 0)
            {
                await AsyncHelper.Delay(WebSocketApiClient._connectInterval, cancellationToken);
                await webSocketApiClient.ConnectAsyncImpl(attemptsLeft, cancellationToken);
                return;
            }
        }
        var childToken = AsyncHelper.CreateChildToken(cancellationToken, new TimeSpan?());
        CancellationTokenSource cts = childToken.Item1;
        CancellationToken stopToken = childToken.Item2;
        await Task.Run(() => this.RunResponseLoop(socket, cts, cancellationToken, stopToken));
        await Task.Run(() => this.RunPingLoop(stopToken));
        await Task.Run(() => this.RunRequestLoop(socket, cts, stopToken));
    }

    private async Task RunResponseLoop(ClientWebSocket socket, CancellationTokenSource cts, CancellationToken reconnectToken, CancellationToken cancellationToken)
    {
        WebSocketApiClient webSocketApiClient = this;
        if (socket == null)
            throw new ArgumentNullException(nameof(socket));
        if (cts == null)
            throw new ArgumentNullException(nameof(cts));
        byte[] buf = new byte[1048576 /*0x100000*/];
        MemoryStream responseBody = new MemoryStream();
        int currError = 0;
        LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, "resp loop started.", Array.Empty<object>());
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                WebSocketReceiveResult async = await socket.ReceiveAsync(new ArraySegment<byte>(buf), cancellationToken);
                if (async.CloseStatus.HasValue)
                {
                    this.AddWarningLog("CloseStatus = {0}.", async.CloseStatus);
                    break;
                }

                if (async.Count == 0)
                {
                    LoggingHelper.AddWarningLog((ILogReceiver)webSocketApiClient, "Received 0 bytes.", Array.Empty<object>());
                    break;
                }
                responseBody.Write(buf, 0, async.Count);
                if (async.EndOfMessage)
                {
                    if (responseBody.Length != 0L)
                    {
                        ArraySegment<byte> actualBuffer = IOHelper.GetActualBuffer(responseBody);
                        try
                        {
                            responseBody.Position = 0L;
                            WebSocketApiClient.ResponseImpl responseImpl = JsonHelper.DeserializeObject<WebSocketApiClient.ResponseImpl>(StringHelper.UTF8(actualBuffer));
                            (BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>) t;

                            if (!_subscriptions.TryGetValue(responseImpl.RequestId, out t))
                            {
                                this.AddWarningLog("Unk request id {0}.", responseImpl.RequestId);
                                continue;
                            }

                            BaseResponse baseResponse = (BaseResponse)await t.Item2.CreateJsonSerializer().DeserializeAsync((Stream)responseBody, cancellationToken);
                            if (baseResponse is PongResponse)
                                LoggingHelper.AddVerboseLog((ILogReceiver)webSocketApiClient, "Pong", Array.Empty<object>());
                            else
                                LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, Converter.To<string>((object)baseResponse), Array.Empty<object>());
                            try
                            {
                                await t.Item3(baseResponse, cancellationToken);
                            }
                            catch (Exception ex)
                            {
                                LoggingHelper.AddErrorLog((ILogReceiver)webSocketApiClient, ex);
                            }
                            t = new ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>>();
                        }
                        finally
                        {
                            responseBody.SetLength(0L);
                        }
                        currError = 0;
                    }
                    else
                        continue;
                }
                else
                    continue;
            }
            catch (Exception ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    ++currError;
                    LoggingHelper.AddErrorLog((ILogReceiver)webSocketApiClient, ex);
                    if (currError >= 10 || socket.State == WebSocketState.Aborted)
                    {
                        if (currError >= 10)
                            LoggingHelper.AddErrorLog((ILogReceiver)webSocketApiClient, "!!! Stopping. Errors={0}/{1} State={2} !!!", new object[3]
                            {
                (object) currError,
                (object) 10,
                (object) socket.State
                            });
                        cts.Cancel();
                        socket.Dispose();
                        if (webSocketApiClient.MaxReconnect > 0)
                        {
                            await webSocketApiClient.ConnectAsyncImpl(webSocketApiClient.MaxReconnect, reconnectToken);
                            break;
                        }
                        break;
                    }
                }
                else
                    break;
            }
        }
        LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, "resp loop finished.", Array.Empty<object>());
        buf = (byte[])null;
        responseBody = (MemoryStream)null;
    }

    private async Task RunRequestLoop(
      ClientWebSocket socket,
      CancellationTokenSource cts,
      CancellationToken cancellationToken)
    {
        WebSocketApiClient webSocketApiClient = this;
        if (socket == null)
            throw new ArgumentNullException(nameof(socket));
        LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, "req loop started.", Array.Empty<object>());
        MemoryStream requestBody = new MemoryStream();
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                BaseRequest baseRequest = await webSocketApiClient._requests.Reader.ReadAsync(cancellationToken);
                if (baseRequest is WebSocketApiClient.DisposeRequest)
                {
                    webSocketApiClient._requests.Writer.Complete((Exception)null);
                    cts.Cancel();
                    socket.Dispose();
                    break;
                }
                requestBody.Position = 0L;
                await baseRequest.GetType().CreateJsonSerializer().SerializeAsync((object)baseRequest, (Stream)requestBody, cancellationToken);
                ArraySegment<byte> actualBuffer = IOHelper.GetActualBuffer(requestBody);
                LoggingHelper.AddVerboseLog((ILogReceiver)webSocketApiClient, StringHelper.UTF8(actualBuffer), Array.Empty<object>());
                await socket.SendAsync(actualBuffer, WebSocketMessageType.Text, true, cancellationToken);
            }
            catch (Exception ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                    LoggingHelper.AddErrorLog((ILogReceiver)webSocketApiClient, ex);
                else
                    break;
            }
        }
        LoggingHelper.AddDebugLog((ILogReceiver)webSocketApiClient, "req loop finished.", Array.Empty<object>());
        requestBody = (MemoryStream)null;
    }

    private async Task RunPingLoop(CancellationToken cancellationToken)
    {

        this.AddDebugLog("ping loop started.", Array.Empty<object>());
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await AsyncHelper.Delay(WebSocketApiClient._pingInterval, cancellationToken);

                Func<PongResponse, CancellationToken, ValueTask> handleResponse = (r, t) => default(ValueTask);

                await SubscribeAsync<PingRequest, PongResponse>(new PingRequest(), handleResponse, cancellationToken);
            }
            catch (Exception ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                    this.AddErrorLog(ex);
                else
                    break;
            }
        }
        this.AddDebugLog("ping loop finished.", Array.Empty<object>());
    }

    public ValueTask SubscribeAsync<TRequest, TResponse>(
      TRequest request,
      Func<TResponse, CancellationToken, ValueTask> handleResponse,
      CancellationToken cancellationToken)
      where TRequest : BaseRequest
      where TResponse : BaseResponse
    {
        if ((object)request == null)
            throw new ArgumentNullException(nameof(request));
        if (handleResponse == null)
            throw new ArgumentNullException(nameof(handleResponse));
        if ((object)request is PingRequest)
            LoggingHelper.AddVerboseLog((ILogReceiver)this, "Ping", Array.Empty<object>());
        else
            LoggingHelper.AddDebugLog((ILogReceiver)this, Converter.To<string>((object)request), Array.Empty<object>());
        if (!((object)request is PingRequest) && request.Id == 0L)
            request.Id = (long)(((SynchronizedDictionary<long, (BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>)>)this._subscriptions).Count + 1);
        request.IsSubscribe = true;
        if (!((object)request is PingRequest))
            ((SynchronizedDictionary<long, (BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>)>)this._subscriptions).Add(request.Id, ((BaseRequest)PersistableHelper.Clone<TRequest>(request), typeof(TResponse), new Func<BaseResponse, CancellationToken, ValueTask>(Handle)));
        return this._requests.Writer.WriteAsync((BaseRequest)request, cancellationToken);

        ValueTask Handle(BaseResponse obj, CancellationToken t) => handleResponse((TResponse)obj, t);
    }

    public ValueTask UnSubscribeAsync(long id, CancellationToken cancellationToken)
    {
        ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>> valueTuple;
        if (!CollectionHelper.TryGetAndRemove<long, ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>>>(this._subscriptions, id, out valueTuple))
        {
            return default(ValueTask);
        }
        return this._requests.Writer.WriteAsync(new WebSocketApiClient.UnsubscribeRequest
        {
            Id = id
        }, cancellationToken);
    }

    private class UnsubscribeRequest : BaseRequest
    {
        public UnsubscribeRequest()
          : base(~SubscriptionTypes.StrategyUpdate)
        {
        }
    }

    private class DisposeRequest : BaseRequest
    {
        public DisposeRequest()
          : base(~SubscriptionTypes.Client)
        {
        }
    }

    private class ResponseImpl : BaseResponse
    {
        public ResponseImpl()
          : base(~SubscriptionTypes.StrategyUpdate)
        {
        }
    }
}
