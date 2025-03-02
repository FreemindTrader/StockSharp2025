// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IWebSocketService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Logging;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IWebSocketService : ILogSource, IDisposable, IAsyncDisposable
    {
        ValueTask ConnectAsync( CancellationToken cancellationToken );

        ValueTask SubscribeAsync<TRequest, TResponse>(
          TRequest request,
          Func<TResponse, CancellationToken, ValueTask> handleResponse,
          CancellationToken cancellationToken )
          where TRequest : BaseRequest
          where TResponse : BaseResponse;

        ValueTask UnSubscribeAsync( long id, CancellationToken cancellationToken );
    }
}
