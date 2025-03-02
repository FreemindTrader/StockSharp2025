// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.WebSockets.WebSocketApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Serialization;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client.WebSockets
{
    public class WebSocketApiClient : BaseLogReceiver, IWebSocketService, ILogSource, IDisposable, IAsyncDisposable
    {
        private class UnsubscribeRequest : BaseRequest
        {
            public UnsubscribeRequest()
              : base( ~SubscriptionTypes.StrategyUpdate )
            {
            }
        }

        private class DisposeRequest : BaseRequest
        {
            public DisposeRequest() : base( ~SubscriptionTypes.Client )
            {
            }
        }

        private class ResponseImpl : BaseResponse
        {
            public ResponseImpl()
              : base( ~SubscriptionTypes.StrategyUpdate )
            {
            }
        }


        private static readonly TimeSpan _pingInterval = TimeSpan.FromSeconds(30.0);
        private static readonly TimeSpan _connectInterval = TimeSpan.FromSeconds(10.0);
        private readonly Channel<BaseRequest> _requests;

        private readonly CachedSynchronizedDictionary<long, (BaseRequest request, Type responseType, Func<BaseResponse, CancellationToken, ValueTask> handler)> _subscriptions;
        private readonly Uri _socketAddress;
        private readonly string _schema;
        private readonly SecureString _schemaValue;

        private WebSocketApiClient( Uri baseAddress, string schema, string value )
        {
            Uri baseUri = baseAddress;
            if ( ( object ) baseUri == null )
                throw new ArgumentNullException( nameof( baseAddress ) );
            _socketAddress = new Uri( baseUri, "ws" );
            _requests = Channel.CreateUnbounded<BaseRequest>();
            _schema = schema;
            _schemaValue = StringHelper.Secure( value );
        }

        public WebSocketApiClient( Uri baseAddress, SecureString token )
          : this( baseAddress, "Bearer", StringHelper.UnSecure( token ) )
        {
        }

        public WebSocketApiClient( Uri baseAddress, string login, SecureString password )
          : this( baseAddress, "Basic", StringHelper.Base64( StringHelper.UTF8( login + ":" + StringHelper.UnSecure( password ) ) ) )
        {
        }

        public ValueTask UnSubscribeAsync( long id, CancellationToken cancellationToken )
        {
            ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>> valueTuple;
            if ( !CollectionHelper.TryGetAndRemove<long, ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>>>( this._subscriptions, id, out valueTuple ) )
            {
                return default( ValueTask );
            }
            return this._requests.Writer.WriteAsync( new WebSocketApiClient.UnsubscribeRequest
            {
                Id = id
            }, cancellationToken );
        }


        public async ValueTask DisposeAsync()
        {
            this.Dispose();
            var cts = new CancellationTokenSource( _connectInterval );

            await AsyncHelper.WhenAll( ( ( IEnumerable<long> ) this._subscriptions.CachedKeys ).Select( subId => this.UnSubscribeAsync( subId, cts.Token ) ) );

            await this._requests.Writer.WriteAsync( ( BaseRequest ) new WebSocketApiClient.DisposeRequest(), cts.Token );
        }

        public int MaxConnect { get; set; }

        public int MaxReconnect { get; set; }

        public ValueTask ConnectAsync( CancellationToken cancellationToken )
        {
            return this.ConnectAsyncImpl( this.MaxConnect, cancellationToken );
        }

        private async ValueTask ConnectAsyncImpl( int attemptsLeft, CancellationToken cancellationToken )
        {
            try
            {
                ClientWebSocket socket = new ClientWebSocket();
                socket.Options.SetRequestHeader( "Authorization", AuthSchemas.FormatAuth( _schema, _schemaValue ) );

                try
                {
                    LoggingHelper.AddDebugLog( this, "Connecting...", Array.Empty<object>() );
                    await socket.ConnectAsync( _socketAddress, cancellationToken );

                    LoggingHelper.AddDebugLog( this, "Connected.", Array.Empty<object>() );


                    for ( int index = 0; index < _subscriptions.CachedPairs.Length; ++index )
                    {
                        BaseRequest baseRequest = _subscriptions.CachedPairs[index].Value.Item1;
                        LoggingHelper.AddDebugLog( this, baseRequest.To<string>(), Array.Empty<object>() );
                        await _requests.Writer.WriteAsync( baseRequest, cancellationToken );
                    }
                }
                catch ( Exception ex )
                {
                    LoggingHelper.AddErrorLog( this, ex );
                    if ( --attemptsLeft > 0 )
                    {
                        await AsyncHelper.Delay( WebSocketApiClient._connectInterval, cancellationToken );

                        await this.ConnectAsyncImpl( attemptsLeft, cancellationToken );
                    }
                }
                var childToken = AsyncHelper.CreateChildToken(cancellationToken, new TimeSpan?());
                CancellationTokenSource cts = childToken.Item1;
                CancellationToken stopToken = childToken.Item2;
                await Task.Run( () => this.RunResponseLoop( socket, cts, cancellationToken, stopToken ) );
                await Task.Run( () => this.RunPingLoop( stopToken ) );
                await Task.Run( () => this.RunRequestLoop( socket, cts, stopToken ) );
            }
            catch ( Exception ex )
            {

            }
        }

        private async Task RunResponseLoop( ClientWebSocket socket, CancellationTokenSource cts, CancellationToken reconnectToken, CancellationToken cancellationToken )
        {
            int awaitNum = -2;
            byte[] buf;
            MemoryStream responseBody;
            try
            {
                if ( socket == null )
                    throw new ArgumentNullException( nameof( socket ) );
                if ( cts == null )
                    throw new ArgumentNullException( nameof( cts ) );
                buf = new byte [1048576];
                responseBody = new MemoryStream();
                int currError = 0;
                this.AddDebugLog( "resp loop started.", Array.Empty<object>() );

                while ( !cancellationToken.IsCancellationRequested )
                {
                    try
                    {
                        var result1 = await socket.ReceiveAsync(new ArraySegment<byte>(buf), cancellationToken);

                        if ( result1.CloseStatus.HasValue )
                        {
                            this.AddWarningLog( "CloseStatus = {0}.", result1.CloseStatus );
                            break;
                        }

                        if ( result1.Count == 0 )
                        {
                            this.AddWarningLog( "Received 0 bytes.", Array.Empty<object>() );
                            break;
                        }

                        awaitNum = 0;
                        responseBody.Write( buf, 0, result1.Count );
                        if ( result1.EndOfMessage )
                        {
                            if ( responseBody.Length != 0L )
                            {
                                ArraySegment<byte> actualBuffer = IOHelper.GetActualBuffer(responseBody);
                                try
                                {
                                    responseBody.Position = 0L;
                                    var responseImpl = JsonHelper.DeserializeObject<WebSocketApiClient.ResponseImpl>(StringHelper.UTF8(actualBuffer));
                                    ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>> t;

                                    if ( !_subscriptions.TryGetValue( responseImpl.RequestId, out t ) )
                                    {
                                        this.AddWarningLog( "Unk request id {0}.", responseImpl.RequestId );
                                        continue;
                                    }
                                    var result2  = await t.Item2.CreateJsonSerializer().DeserializeAsync((Stream) responseBody, cancellationToken);

                                    if ( result2 is PongResponse )
                                        this.AddVerboseLog( "Pong", Array.Empty<object>() );
                                    else
                                        this.AddDebugLog( result2.To<string>(), Array.Empty<object>() );

                                    awaitNum = 1;
                                    try
                                    {
                                        BaseResponse response = (BaseResponse) result2;
                                        await t.Item3( response, cancellationToken );
                                    }
                                    catch ( Exception ex )
                                    {
                                        this.AddErrorLog( ex );
                                    }

                                    awaitNum = 2;
                                    t = new ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>>();
                                }
                                finally
                                {
                                    if ( awaitNum < 0 )
                                        responseBody.SetLength( 0L );
                                }
                                currError = 0;
                            }
                            else
                                continue;
                        }
                        else
                            continue;
                    }
                    catch ( Exception ex )
                    {
                        if ( !cancellationToken.IsCancellationRequested )
                        {
                            ++currError;
                            this.AddErrorLog( ex );

                            if ( currError >= 10 || socket.State == WebSocketState.Aborted )
                            {
                                if ( currError >= 10 )
                                    this.AddErrorLog( "!!! Stopping. Errors={0}/{1} State={2} !!!", currError, 10, socket.State );
                                cts.Cancel();
                                socket.Dispose();

                                if ( this.MaxReconnect > 0 )
                                {
                                    await this.ConnectAsyncImpl( this.MaxReconnect, reconnectToken );

                                    break;
                                }
                                break;
                            }
                        }
                        else
                            break;
                    }
                }
                this.AddDebugLog( "resp loop finished.", Array.Empty<object>() );
            }
            catch ( Exception ex )
            {

            }

        }

        private async Task RunRequestLoop( ClientWebSocket socket, CancellationTokenSource cts, CancellationToken cancellationToken )
        {

            MemoryStream requestBody;
            try
            {
                if ( socket == null )
                    throw new ArgumentNullException( nameof( socket ) );

                this.AddDebugLog( "req loop started.", Array.Empty<object>() );
                requestBody = new MemoryStream();

                while ( !cancellationToken.IsCancellationRequested )
                {
                    try
                    {
                        var waitResult = await _requests.Reader.ReadAsync(cancellationToken);

                        BaseRequest result = (BaseRequest) waitResult;

                        if ( result is WebSocketApiClient.DisposeRequest )
                        {
                            _requests.Writer.Complete( null );
                            cts.Cancel();
                            socket.Dispose();
                            break;
                        }

                        requestBody.Position = 0L;
                        await result.GetType().CreateJsonSerializer().SerializeAsync( result, ( Stream ) requestBody, cancellationToken );

                        var actualBuffer = IOHelper.GetActualBuffer(requestBody);
                        this.AddVerboseLog( StringHelper.UTF8( actualBuffer ), Array.Empty<object>() );

                        await socket.SendAsync( actualBuffer, WebSocketMessageType.Text, true, cancellationToken );
                    }
                    catch ( Exception ex )
                    {
                        if ( !cancellationToken.IsCancellationRequested )
                            this.AddErrorLog( ex );
                        else
                            break;
                    }
                }
                this.AddDebugLog( "req loop finished.", Array.Empty<object>() );
            }
            catch ( Exception ex )
            {

            }

        }

        private async Task RunPingLoop( CancellationToken cancellationToken )
        {

            this.AddDebugLog( "ping loop started.", Array.Empty<object>() );
            while ( !cancellationToken.IsCancellationRequested )
            {
                try
                {
                    await AsyncHelper.Delay( WebSocketApiClient._pingInterval, cancellationToken );

                    Func<PongResponse, CancellationToken, ValueTask> handleResponse = ( r, t ) =>  default(ValueTask);

                    await SubscribeAsync<PingRequest, PongResponse>( new PingRequest(), handleResponse, cancellationToken );
                }
                catch ( Exception ex )
                {
                    if ( !cancellationToken.IsCancellationRequested )
                        this.AddErrorLog( ex );
                    else
                        break;
                }
            }
            this.AddDebugLog( "ping loop finished.", Array.Empty<object>() );
        }

        public ValueTask SubscribeAsync<TRequest, TResponse>( TRequest request, Func<TResponse, CancellationToken, ValueTask> handleResponse, CancellationToken cts )
          where TRequest : BaseRequest
          where TResponse : BaseResponse
        {
            if ( request == null )
                throw new ArgumentNullException( nameof( request ) );

            if ( handleResponse == null )
                throw new ArgumentNullException( nameof( handleResponse ) );

            if ( request is PingRequest )
                this.AddVerboseLog( "Ping", Array.Empty<object>() );
            else
                this.AddDebugLog( request.To<string>(), Array.Empty<object>() );

            if ( !( request is PingRequest ) && request.Id == 0L )
                request.Id = this._subscriptions.Count + 1;

            request.IsSubscribe = true;

            if ( !( request is PingRequest ) )
                this._subscriptions.Add( request.Id, new ValueTuple<BaseRequest, Type, Func<BaseResponse, CancellationToken, ValueTask>>( ( BaseRequest ) ( object ) PersistableHelper.Clone<TRequest>( request ), typeof( TResponse ), new Func<BaseResponse, CancellationToken, ValueTask>( Handle ) ) );

            ValueTask Handle( BaseResponse obj, CancellationToken t )
            {
                return handleResponse( ( TResponse ) obj, t );
            }

            if ( !( request is PingRequest ) )
                this._subscriptions.Add( request.Id, ( request.Clone<TRequest>(  ), typeof( TResponse ), new Func<BaseResponse, CancellationToken, ValueTask>( Handle ) ) );

            return this._requests.Writer.WriteAsync( ( BaseRequest ) request, cts );
        }
    }
}


