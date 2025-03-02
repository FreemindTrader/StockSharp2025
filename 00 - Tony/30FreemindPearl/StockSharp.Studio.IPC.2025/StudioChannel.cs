// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.StudioChannel
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65FF0FD2-B114-4B6B-959A-42B33214A877
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.IPC.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.IPC
{
    /// <summary>Message channel between S# applications.</summary>
    public class StudioChannel : BaseLogReceiver
    {
        private static readonly TimeSpan _connectTimeout = TimeSpan.FromSeconds(5.0);
        private static long _lastTransactionId;
        private readonly long _channelId;
        private readonly Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> _handler;
        private readonly Func<bool> _tryStartReceiver;
        private readonly SynchronizedDictionary<long, TaskCompletionSource<StudioMessage>> _clientResponseTasks;
        private readonly CachedSynchronizedList<StudioChannel.PendingTaskInfo> _pendingTasks;
        private NamedPipeServerStream _server;
        private readonly CancellationTokenSource _cts;
        private readonly ISerializer<SettingsStorage> _serializer;

        public StudioChannel( long channelId, ISerializer<SettingsStorage> serializer, Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> handler, ILogSource parent, Func<bool> tryStartReceiver )
        {

            this._channelId = channelId;
            Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> func1 = handler;
            if ( func1 == null )
                throw new ArgumentNullException( nameof( handler ) );
            this._handler = func1;
            Func<bool> func2 = tryStartReceiver;
            if ( func2 == null )
                throw new ArgumentNullException( nameof( tryStartReceiver ) );
            this._tryStartReceiver = func2;
            ILogSource ilogSource = parent;
            
            if ( ilogSource == null )
                throw new ArgumentNullException( nameof( parent ) );


            this.Parent = ilogSource;
            ISerializer<SettingsStorage> iserializer = serializer;
            if ( iserializer == null )
                throw new ArgumentNullException( nameof( serializer ) );
            this._serializer = iserializer;
        }

        /// <inheritdoc />
        protected override void DisposeManaged()
        {
            this.WaitPendingTasks( TimeSpan.FromSeconds( 5.0 ) );
            this._cts?.Cancel();

            this.DisposeManaged();
        }

        /// <summary>
        /// </summary>
        public Task RunServerAsync( CancellationToken token )
        {
            int shortSize = 2;
            byte[] fixedBuf = new byte[(int) ushort.MaxValue + shortSize];
            string pipeName = StudioChannel.ProductToPipeName(this._channelId);
            LoggingHelper.AddDebugLog( ( ILogReceiver ) this, "starting pipe server '" + pipeName + "'", Array.Empty<object>() );
            this._server = new NamedPipeServerStream( pipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous );
            Task.Run( ( Func<Task> ) ( async () =>
            {
                try
                {
                    while ( !token.IsCancellationRequested )
                    {
                        await this._server.WaitForConnectionAsync( token );
                        LoggingHelper.AddDebugLog( ( ILogReceiver ) this, "accepted incoming connection", Array.Empty<object>() );
                        ushort? len = new ushort?();
                        int total = 0;
                    label_3:
                        int num1 = 0;
                        do
                        {
                            int num2 = await this._server.ReadAsync(new Memory<byte>(fixedBuf, total, fixedBuf.Length - total), token);
                            if ( num2 != 0 )
                            {
                                total += num2;
                                if ( !len.HasValue )
                                {
                                    if ( total >= 2 )
                                        len = new ushort?( ( ushort ) Converter.To<ushort>( ( object ) fixedBuf ) );
                                    else
                                        continue;
                                }
                                num1 = total - shortSize;
                                if ( ( int ) len.Value == num1 )
                                    goto label_11;
                            }
                            else
                                goto label_11;
                        }
                        while ( ( int ) len.Value <= num1 );
                        goto label_3;
                    label_11:
                        this._server.Disconnect();
                        this.HandleIncomingMessageAsync( ( Stream ) new MemoryStream( fixedBuf, shortSize, total ), token );
                        len = new ushort?();
                    }
                }
                catch ( Exception ex )
                {
                    if ( token.IsCancellationRequested )
                        return;
                    LoggingHelper.AddErrorLog( ( ILogReceiver ) this, "pipe server error: {0}", new object [1]
              {
             ex
                } );
                }
                finally
                {
                    this._server?.Dispose();
                }
            } ), token );
            return Task.CompletedTask;
        }

        private async Task HandleIncomingMessageAsync( Stream stream, CancellationToken token )
        {
            StudioChannel studioChannel = this;
            StudioChannel.PendingTaskInfo pendingTask;

            int awaitCount = -2;

            try
            {
                pendingTask = studioChannel.AddPendingTask( "handle incoming msg" );
                try
                {
                    await Task.Yield();
                    
                    StudioMessage message = (StudioMessage) PersistableHelper.LoadEntire<StudioMessage>((SettingsStorage) ISerializerExtensions.Deserialize<SettingsStorage>( studioChannel._serializer, stream));
                    pendingTask.AppendInfo( message.ToString() );
                    LoggingHelper.AddDebugLog( ( ILogReceiver ) studioChannel, "got msg ({0} bytes): {1}", stream.Length, message.ToString() );
                    
                    if ( message.IsResponse )
                    {
                        TaskCompletionSource<StudioMessage> completionSource;
                        
                        if ( CollectionHelper.TryGetAndRemove<long, TaskCompletionSource<StudioMessage>>( studioChannel._clientResponseTasks, message.TransactionId,  out completionSource ) )
                            completionSource.TrySetResult( message );
                        else
                            LoggingHelper.AddDebugLog( ( ILogReceiver ) studioChannel, "unable to find corresponding request {0}", message.TransactionId );
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
                            var awr = await studioChannel._handler(message, token);
                            
                            message1 = awr ?? ( StudioMessage ) notHandled;
                            notHandled = null;

                            awaitCount = 1;
                        }
                        catch ( Exception ex )
                        {
                            message1 = ( StudioMessage ) new MsgError()
                            {
                                ErrorMessage = ex.Message,
                                IsCancellation = ( ex is OperationCanceledException )
                            };
                        }
                        StudioChannel.PendingTaskInfo pendingTaskInfo = pendingTask;
                        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
                        interpolatedStringHandler.AppendLiteral( "sending response " );
                        interpolatedStringHandler.AppendFormatted<StudioMessage>( message1 );
                        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                        pendingTaskInfo.AppendInfo( stringAndClear );
                        message1.IsResponse = true;
                        message1.TransactionId = message.TransactionId;
                        await studioChannel.SendAsync(message1, message.From, true, token);
                        
                    }
                    message = ( StudioMessage ) null;
                }
                catch ( Exception ex )
                {
                    LoggingHelper.AddErrorLog( ( ILogReceiver ) studioChannel, "error handling incoming message: {0}", new object [1]
                    {
             ex
                    } );
                }
                finally
                {
                    if ( awaitCount < 0 && pendingTask != null )
                        ( ( IDisposable ) pendingTask ).Dispose();
                }
            }
            catch ( Exception ex )
            {
                
            }          
        }

        /// <summary>Send message and get response asynchonously.</summary>
        /// <param name="message"></param>
        /// <param name="toProductId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<StudioMessage> GetResponseAsync( StudioMessage message, long toProductId, CancellationToken token )
        {
            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );
            message.IsResponse = false;
            message.TransactionId = Interlocked.Increment( ref StudioChannel._lastTransactionId );
            return this.SendAsync( message, toProductId, true, token );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toProductId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task SendAsync( StudioMessage message, long toProductId, CancellationToken token )
        {
            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );
            message.IsResponse = false;
            message.TransactionId = Interlocked.Increment( ref StudioChannel._lastTransactionId );
            return ( Task ) this.SendAsync( message, toProductId, false, token );
        }

        private async Task<StudioMessage> SendAsync( StudioMessage message, long toProductId, bool waitForResponse, CancellationToken token )
        {
            // ISSUE: explicit reference operation
            // ISSUE: reference to a compiler-generated field
            int num1 = -2;
            StudioChannel studioChannel1 = this;
            StudioChannel.PendingTaskInfo pendingTask;
            StudioMessage result = null;
            try
            {
                if ( message == null )
                    throw new ArgumentNullException( nameof( message ) );
                if ( toProductId == 0L )
                    throw new ArgumentNullException( nameof( toProductId ) );

                StudioChannel studioChannel2 = studioChannel1;

                var shandle = new DefaultInterpolatedStringHandler(4, 3);
                shandle.AppendFormatted( nameof( SendAsync ) );
                shandle.AppendLiteral( "(" );
                shandle.AppendFormatted<StudioMessage>( message );
                shandle.AppendLiteral( ", " );
                shandle.AppendFormatted<long>( toProductId );
                shandle.AppendLiteral( ")" );
                string stringAndClear = shandle.ToStringAndClear();
                pendingTask = studioChannel2.AddPendingTask( stringAndClear );
                try
                {
                    try
                    {
                        TaskCompletionSource<StudioMessage> tcs = null;
                        if ( waitForResponse )
                        {
                            tcs = ( TaskCompletionSource<StudioMessage> ) AsyncHelper.CreateTaskCompletionSource<StudioMessage>( true );
                            studioChannel1._clientResponseTasks[ message.TransactionId ] = tcs;

                            new CancellationTokenSource( TimeSpan.FromMinutes( 1.0 ) ).Token.Register( ( Action ) ( () => tcs.TrySetCanceled() ), false );
                        }
                        NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", StudioChannel.ProductToPipeName(toProductId), PipeDirection.Out, PipeOptions.Asynchronous);
                        try
                        {
                            bool canStart = true;
                            TaskAwaiter taskAwaiter1;
                            Exception inner = null;
                            TaskAwaiter awaiter1;
                            while ( true )
                            {
                                int num2 = 0;
                                do
                                {
                                    try
                                    {
                                        await pipeClient.ConnectAsync((int) StudioChannel._connectTimeout.TotalMilliseconds, token);

                                        if ( !pipeClient.IsConnected )
                                            throw new StudioChannel.ConnectionException( "pipe is not connected", ( Exception ) null );
                                        pendingTask.AppendInfo( "connected" );
                                        message.From = studioChannel1._channelId;
                                        MemoryStream data = new MemoryStream();

                                        await studioChannel1._serializer.SerializeAsync(PersistableHelper.SaveEntire((IPersistable) message, false), (Stream) data, token);
                                        
                                        LoggingHelper.AddDebugLog( ( ILogReceiver ) studioChannel1, "sending msg to {0}: {1}", toProductId, message.ToString() );
                                        ArraySegment<byte> buffer = IOHelper.GetActualBuffer(data);
                                        
                                        await pipeClient.WriteAsync((ReadOnlyMemory<byte>) ((byte[]) Converter.To<byte[]>( checked ((ushort) buffer.Count))), token);                                        
                                        await pipeClient.WriteAsync( ( ReadOnlyMemory<byte> ) buffer, token );                                        
                                        await pipeClient.FlushAsync(token);
                                        
                                        pendingTask.AppendInfo( "sent" );
                                        pipeClient.Dispose();
                                        StudioMessage studioMessage;
                                        if ( waitForResponse )
                                        {
                                            var result2 = await tcs.Task.WaitAsync(token);
                                            studioMessage = result2;
                                        }
                                        else
                                            studioMessage = ( StudioMessage ) null;
                                        result = studioMessage;

                                    }
                                    catch ( Exception ex )
                                    {
                                        inner = ex;
                                        num2 = 1;
                                    }
                                }
                                while ( num2 != 1 );
                                
                                if ( !token.IsCancellationRequested && !( inner is TimeoutException ) )
                                {
                                    if ( canStart && toProductId == 16L && studioChannel1._tryStartReceiver() )
                                    {
                                        awaiter1 = AsyncHelper.Delay( TimeSpan.FromSeconds( 10.0 ), token ).GetAwaiter();
                                        if ( awaiter1.IsCompleted )
                                        {
                                            awaiter1.GetResult();
                                            canStart = false;
                                        }                                        
                                    }
                                    else
                                    {
                                        throw new StudioChannel.ConnectionException( "pipe connect error", inner );
                                    }                                        
                                }
                                else
                                    break;
                            }
                            result = ( StudioMessage ) null;                            
                            
                        }
                        finally
                        {
                            if ( num1 < 0 && pipeClient != null )
                                pipeClient.Dispose();
                        }
                    }
                    catch ( OperationCanceledException ex )
                    {
                        studioChannel1.LogWarning( "{0} canceled", new object [1]
                        {
               nameof (SendAsync)
                        } );
                        throw;
                    }
                    catch ( Exception ex )
                    {
                        studioChannel1.LogError( "{0} error: {1}", new object [2]
                        {
               nameof (SendAsync),
               ex
                        } );
                        throw;
                    }
                    finally
                    {
                        if ( num1 < 0 && waitForResponse )
                            studioChannel1._clientResponseTasks.Remove( message.TransactionId );
                    }
                }
                finally
                {
                    if ( num1 < 0 && pendingTask != null )
                        ( ( IDisposable ) pendingTask ).Dispose();
                }
            }
            catch ( Exception ex )
            {
                
            }

            return result;
        }

        private StudioChannel.PendingTaskInfo AddPendingTask( string info )
        {
            return new StudioChannel.PendingTaskInfo( ( ISynchronizedCollection<StudioChannel.PendingTaskInfo> ) this._pendingTasks, info );
        }

        private void WaitPendingTasks( TimeSpan timeout )
        {
            StudioChannel.PendingTaskInfo[] array1 = ((IEnumerable<StudioChannel.PendingTaskInfo>) this._pendingTasks.Cache).ToArray<StudioChannel.PendingTaskInfo>();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeout);
            try
            {
                Task.WaitAll( ( ( IEnumerable<StudioChannel.PendingTaskInfo> ) array1 ).Select<StudioChannel.PendingTaskInfo, Task>( ( Func<StudioChannel.PendingTaskInfo, Task> ) ( t => ( Task ) t.Tcs.Task ) ).ToArray<Task>(), cancellationTokenSource.Token );
            }
            catch ( Exception ex )
            {
                this.LogWarning( "{0}: {1}", new object [2]
                {
           nameof (WaitPendingTasks),
           ex
                } );
            }
            StudioChannel.PendingTaskInfo[] array2 = ((IEnumerable<StudioChannel.PendingTaskInfo>) array1).Where<StudioChannel.PendingTaskInfo>((Func<StudioChannel.PendingTaskInfo, bool>) (t => !t.Tcs.Task.IsCompleted)).ToArray<StudioChannel.PendingTaskInfo>();
            if ( !( ( IEnumerable<StudioChannel.PendingTaskInfo> ) array2 ).Any<StudioChannel.PendingTaskInfo>() )
                return;
            this.LogWarning( "following tasks were not completed: {0}", new object [1]
            {
         StringHelper.JoinCommaSpace(((IEnumerable<StudioChannel.PendingTaskInfo>) array2).Select<StudioChannel.PendingTaskInfo, string>((Func<StudioChannel.PendingTaskInfo, string>) (t => t.Info)))
            } );
        }

        private static string ProductToPipeName( long channelId )
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
            interpolatedStringHandler.AppendLiteral( "stocksharp_installer_ipc_pipe_" );
            interpolatedStringHandler.AppendFormatted<long>( channelId );
            return interpolatedStringHandler.ToStringAndClear();
        }

        private class PendingTaskInfo : Disposable
        {
            private readonly ISynchronizedCollection<StudioChannel.PendingTaskInfo> _coll;

            public PendingTaskInfo( ISynchronizedCollection<StudioChannel.PendingTaskInfo> coll, string info = null )
            {
                
                ISynchronizedCollection<StudioChannel.PendingTaskInfo> isynchronizedCollection = coll;
                if ( isynchronizedCollection == null )
                    throw new ArgumentNullException( nameof( coll ) );
                this._coll = isynchronizedCollection;
                this.Tcs = new TaskCompletionSource<object>();
                this.Info = info;
                ( ( ICollection<StudioChannel.PendingTaskInfo> ) this._coll ).Add( this );
            }

            protected virtual void DisposeManaged()
            {
                this.Tcs.TrySetResult( ( object ) null );
                ( ( ICollection<StudioChannel.PendingTaskInfo> ) this._coll ).Remove( this );
                base.DisposeManaged();
            }

            public void AppendInfo( string msg )
            {
                this.Info = this.Info + "\n" + msg;
            }

            public TaskCompletionSource<object> Tcs { get; }

            public string Info { get; set; }
        }

        /// <summary>Connection cannot be established.</summary>
        public class ConnectionException : Exception
        {
            /// <summary>Create instance.</summary>
            /// <param name="message"></param>
            /// <param name="inner"></param>
            public ConnectionException( string message = "", Exception inner = null )
              : base( message, inner )
            {
            }
        }
    }
}
