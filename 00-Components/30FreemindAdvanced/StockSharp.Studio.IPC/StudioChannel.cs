// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.StudioChannel
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F4CDD47D-561A-463F-994A-61FC038C2B5F
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;

#nullable enable
namespace StockSharp.Studio.IPC;

/// <summary>Message channel between S# applications.</summary>
public class StudioChannel : BaseLogReceiver
{
    private static readonly TimeSpan _connectTimeout = TimeSpan.FromSeconds(5.0);
    private static long _lastTransactionId;
    private readonly long _channelId;
    private readonly
#nullable disable
    Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> _handler;
    private readonly Func<bool> _tryStartReceiver;
    private readonly SynchronizedDictionary<long, TaskCompletionSource<StudioMessage>> _clientResponseTasks = new SynchronizedDictionary<long, TaskCompletionSource<StudioMessage>>();
    private readonly CachedSynchronizedList<StudioChannel.PendingTaskInfo> _pendingTasks = new CachedSynchronizedList<StudioChannel.PendingTaskInfo>();
    private NamedPipeServerStream _server;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly ISerializer<SettingsStorage> _serializer;

    public StudioChannel(
      long channelId,
      ISerializer<SettingsStorage> serializer,
      Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> handler,
      ILogSource parent,
      Func<bool> tryStartReceiver)
    {
        this._channelId = channelId;
        this._handler = handler ?? throw new ArgumentNullException(nameof(handler));
        this._tryStartReceiver = tryStartReceiver ?? throw new ArgumentNullException(nameof(tryStartReceiver));
        ((BaseLogSource)this).Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        this._serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
    }

    /// <inheritdoc />
    protected override void DisposeManaged()
    {
        this.WaitPendingTasks(TimeSpan.FromSeconds(5.0));
        this._cts?.Cancel();
        this.DisposeManaged();
    }

    /// <summary>
    /// </summary>
    public Task RunServerAsync(CancellationToken token)
    {
        int shortSize = 2;
        byte[] fixedBuf = new byte[(int)ushort.MaxValue + shortSize];
        string pipeName = StudioChannel.ProductToPipeName(this._channelId);
        this.LogDebug($"starting pipe server '{pipeName}'", Array.Empty<object>());
        this._server = new NamedPipeServerStream(pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
        Task.Run((Func<Task>)(async () =>
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await this._server.WaitForConnectionAsync(token);
                    this.LogDebug("accepted incoming connection", Array.Empty<object>());
                    ushort? len = new ushort?();
                    int total = 0;
                label_3:
                    int num1 = 0;
                    do
                    {
                        int num2 = await this._server.ReadAsync(new Memory<byte>(fixedBuf, total, fixedBuf.Length - total), token);
                        if (num2 != 0)
                        {
                            total += num2;
                            if (!len.HasValue)
                            {
                                if (total >= 2)
                                    len = new ushort?(Converter.To<ushort>((object)fixedBuf));
                                else
                                    continue;
                            }
                            num1 = total - shortSize;
                            if ((int)len.Value == num1)
                                goto label_11;
                        }
                        else
                            goto label_11;
                    }
                    while ((int)len.Value <= num1);
                    goto label_3;
                label_11:
                    this._server.Disconnect();
                    await this.HandleIncomingMessageAsync((Stream)new MemoryStream(fixedBuf, shortSize, total), token);
                    len = new ushort?();
                }
            }
            catch (Exception ex)
            {
                if (token.IsCancellationRequested)
                    return;
                this.LogError("pipe server error: {0}", new object[1]
            {
          (object) ex
            });
            }
            finally
            {
                this._server?.Dispose();
            }
        }), token);
        return Task.CompletedTask;
    }

    private async Task HandleIncomingMessageAsync(Stream stream, CancellationToken token)
    {
        StudioChannel studioChannel = this;
        StudioChannel.PendingTaskInfo pendingTask = studioChannel.AddPendingTask("handle incoming msg");
        try
        {
            await Task.Yield();
            StudioMessage message = PersistableHelper.LoadEntire<StudioMessage>(ISerializerExtensions.Deserialize<SettingsStorage>(studioChannel._serializer, stream));
            pendingTask.AppendInfo(message.ToString());
            studioChannel.LogDebug("got msg ({0} bytes): {1}", new object[2]
            {
        (object) stream.Length,
        (object) message.ToString()
            });
            if (message.IsResponse)
            {
                TaskCompletionSource<StudioMessage> completionSource;
                if (CollectionHelper.TryGetAndRemove<long, TaskCompletionSource<StudioMessage>>((IDictionary<long, TaskCompletionSource<StudioMessage>>)studioChannel._clientResponseTasks, message.TransactionId, out completionSource))
                    completionSource.TrySetResult(message);
                else
                    studioChannel.LogDebug("unable to find corresponding request {0}", new object[1]
                    {
            (object) message.TransactionId
                    });
            }
            else
            {
                StudioMessage message1;
                try
                {
                    MsgError notHandled = new MsgError()
                    {
                        ErrorMessage = "message was not handled by the server"
                    };
                    message1 = await studioChannel._handler(message, token) ?? (StudioMessage)notHandled;
                    notHandled = (MsgError)null;
                }
                catch (Exception ex)
                {
                    message1 = (StudioMessage)new MsgError()
                    {
                        ErrorMessage = ex.Message,
                        IsCancellation = (ex is OperationCanceledException)
                    };
                }
                pendingTask.AppendInfo($"sending response {message1}");
                message1.IsResponse = true;
                message1.TransactionId = message.TransactionId;
                StudioMessage studioMessage = await studioChannel.SendAsync(message1, message.From, true, token);
            }
            message = (StudioMessage)null;
            pendingTask = (StudioChannel.PendingTaskInfo)null;
        }
        catch (Exception ex)
        {
            studioChannel.LogError("error handling incoming message: {0}", new object[1]
            {
        (object) ex
            });
            pendingTask = (StudioChannel.PendingTaskInfo)null;
        }
        finally
        {
            ((IDisposable)pendingTask)?.Dispose();
        }
    }

    /// <summary>Send message and get response asynchonously.</summary>
    /// <param name="message"></param>
    /// <param name="toProductId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<StudioMessage> GetResponseAsync(
      StudioMessage message,
      long toProductId,
      CancellationToken token)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));
        message.IsResponse = false;
        message.TransactionId = Interlocked.Increment(ref StudioChannel._lastTransactionId);
        return this.SendAsync(message, toProductId, true, token);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="toProductId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task SendAsync(StudioMessage message, long toProductId, CancellationToken token)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));
        message.IsResponse = false;
        message.TransactionId = Interlocked.Increment(ref StudioChannel._lastTransactionId);
        return (Task)this.SendAsync(message, toProductId, false, token);
    }

    private async Task<StudioMessage> SendAsync(StudioMessage message, long toProductId, bool waitForResponse, CancellationToken token)
    {
        StudioChannel studioChannel1 = this;
        if (message == null)
            throw new ArgumentNullException(nameof(message));
        if (toProductId == 0L)
            throw new ArgumentNullException(nameof(toProductId));
        StudioChannel studioChannel2 = studioChannel1;
        string info = $"{nameof(SendAsync)}({message}, {toProductId})";
        using (StudioChannel.PendingTaskInfo pendingTask = studioChannel2.AddPendingTask(info))
        {
            try
            {
                TaskCompletionSource<StudioMessage> tcs = (TaskCompletionSource<StudioMessage>)null;
                if (waitForResponse)
                {
                    tcs = AsyncHelper.CreateTaskCompletionSource<StudioMessage>(true);
                    studioChannel1._clientResponseTasks[message.TransactionId] = tcs;
                    new CancellationTokenSource(TimeSpan.FromMinutes(1.0)).Token.Register((Action)(() => tcs.TrySetCanceled()), false);
                }
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", StudioChannel.ProductToPipeName(toProductId), PipeDirection.Out, PipeOptions.Asynchronous))
                {
                    bool canStart = true;
                    Exception inner;
                    while (true)
                    {
                        int num;
                        do
                        {
                            try
                            {
                                await pipeClient.ConnectAsync((int)StudioChannel._connectTimeout.TotalMilliseconds, token);
                                goto label_20;
                            }
                            catch (Exception ex)
                            {
                                num = 1;
                                inner = ex;
                            }
                        }
                        while (num != 1);
                        
                        if (!token.IsCancellationRequested && !(inner is TimeoutException))
                        {
                            if (canStart && toProductId == 16L /*0x10*/ && studioChannel1._tryStartReceiver())
                            {
                                await AsyncHelper.Delay(TimeSpan.FromSeconds(10.0), token);
                                canStart = false;
                            }
                            else
                                goto label_19;
                        }
                        else
                            break;
                    }
                    return (StudioMessage)null;
                label_19:
                    throw new StudioChannel.ConnectionException("pipe connect error", inner);
                label_20:
                    if (!pipeClient.IsConnected)
                        throw new StudioChannel.ConnectionException("pipe is not connected");
                    pendingTask.AppendInfo("connected");
                    message.From = studioChannel1._channelId;
                    MemoryStream data = new MemoryStream();
                    await studioChannel1._serializer.SerializeAsync(PersistableHelper.SaveEntire((IPersistable)message, false), (Stream)data, token);
                    studioChannel1.LogDebug("sending msg to {0}: {1}", new object[2]
                    {
            (object) toProductId,
            (object) message.ToString()
                    });
                    ArraySegment<byte> buffer = IOHelper.GetActualBuffer(data);
                    ValueTask valueTask = pipeClient.WriteAsync((ReadOnlyMemory<byte>)Converter.To<byte[]>((object)checked((ushort)buffer.Count)), token);
                    await valueTask;
                    valueTask = pipeClient.WriteAsync((ReadOnlyMemory<byte>)buffer, token);
                    await valueTask;
                    await pipeClient.FlushAsync(token);
                    pendingTask.AppendInfo("sent");
                    pipeClient.Dispose();
                    StudioMessage studioMessage;
                    if (waitForResponse)
                        studioMessage = await tcs.Task.WaitAsync(token);
                    else
                        studioMessage = (StudioMessage)null;
                    return studioMessage;
                }
            }
            catch (OperationCanceledException ex)
            {
                studioChannel1.LogWarning("{0} canceled", new object[1]
                {
          (object) nameof (SendAsync)
                });
                throw;
            }
            catch (Exception ex)
            {
                studioChannel1.LogError("{0} error: {1}", new object[2]
                {
          (object) nameof (SendAsync),
          (object) ex
                });
                throw;
            }
            finally
            {
                if (waitForResponse)
                    studioChannel1._clientResponseTasks.Remove(message.TransactionId);
            }
        }
    }

    private StudioChannel.PendingTaskInfo AddPendingTask(string info)
    {
        return new StudioChannel.PendingTaskInfo((ISynchronizedCollection<StudioChannel.PendingTaskInfo>)this._pendingTasks, info);
    }

    private void WaitPendingTasks(TimeSpan timeout)
    {
        StudioChannel.PendingTaskInfo[] array1 = ((IEnumerable<StudioChannel.PendingTaskInfo>)this._pendingTasks.Cache).ToArray<StudioChannel.PendingTaskInfo>();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeout);
        try
        {
            Task.WaitAll(((IEnumerable<StudioChannel.PendingTaskInfo>)array1).Select<StudioChannel.PendingTaskInfo, Task>((Func<StudioChannel.PendingTaskInfo, Task>)(t => (Task)t.Tcs.Task)).ToArray<Task>(), cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
            this.LogWarning("{0}: {1}", new object[2]
            {
        (object) nameof (WaitPendingTasks),
        (object) ex
            });
        }
        StudioChannel.PendingTaskInfo[] array2 = ((IEnumerable<StudioChannel.PendingTaskInfo>)array1).Where<StudioChannel.PendingTaskInfo>((Func<StudioChannel.PendingTaskInfo, bool>)(t => !t.Tcs.Task.IsCompleted)).ToArray<StudioChannel.PendingTaskInfo>();
        if (!((IEnumerable<StudioChannel.PendingTaskInfo>)array2).Any<StudioChannel.PendingTaskInfo>())
            return;
        this.LogWarning("following tasks were not completed: {0}", new object[1]
        {
      (object) StringHelper.JoinCommaSpace(((IEnumerable<StudioChannel.PendingTaskInfo>) array2).Select<StudioChannel.PendingTaskInfo, string>((Func<StudioChannel.PendingTaskInfo, string>) (t => t.Info)))
        });
    }

    private static string ProductToPipeName(long channelId)
    {
        return $"stocksharp_installer_ipc_pipe_{channelId}";
    }

    private class PendingTaskInfo : Disposable
    {
        private readonly ISynchronizedCollection<StudioChannel.PendingTaskInfo> _coll;

        public PendingTaskInfo(
          ISynchronizedCollection<StudioChannel.PendingTaskInfo> coll,
          string info = null)
        {
            this._coll = coll ?? throw new ArgumentNullException(nameof(coll));
            this.Tcs = new TaskCompletionSource<object>();
            this.Info = info;
            ((ICollection<StudioChannel.PendingTaskInfo>)this._coll).Add(this);
        }

        protected override void DisposeManaged()
        {
            this.Tcs.TrySetResult((object)null);
            ((ICollection<StudioChannel.PendingTaskInfo>)this._coll).Remove(this);
            base.DisposeManaged();
        }

        public void AppendInfo(string msg) => this.Info = $"{this.Info}\n{msg}";

        public TaskCompletionSource<object> Tcs { get; }

        public string Info { get; set; }
    }

    /// <summary>Connection cannot be established.</summary>
    /// <summary>Create instance.</summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public class ConnectionException(string message = "", Exception inner = null) : Exception(message, inner)
    {
    }
}
