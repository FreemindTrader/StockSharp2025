// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.Extensions.ApiLogListener
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Logging;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client.Extensions;

public class ApiLogListener : LogListener
{
    private readonly IErrorService _errorSvc;
    private readonly CancellationToken _token;
    private readonly long _clientId;

    public ApiLogListener(IErrorService errorSvc, long clientId, CancellationToken token)
    {
        this._errorSvc = errorSvc ?? throw new ArgumentNullException(nameof(errorSvc));
        this._clientId = clientId;
        this._token = token;
        this.Filters.Add(LoggingHelper.OnlyError);
    }

    protected override void OnWriteMessage(LogMessage message)
    {
        IErrorService errorSvc = this._errorSvc;
        Error entity = new Error();
        StockSharp.Web.DomainModel.Client client = new StockSharp.Web.DomainModel.Client();
        client.Id = this._clientId;
        entity.Client = client;
        entity.Url = message.Source.Name;
        entity.Text = message.Message;
        CancellationToken token = this._token;
        AggregateException exception;
        errorSvc.AddAsync(entity, token).ContinueWith((Action<Task<Error>>)(t => exception = t.Exception));
    }
}
