// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.Extensions.ApiLogListener
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Logging;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client.Extensions
{
    public class ApiLogListener : LogListener
    {
        private readonly IErrorService _errorSvc;
        private readonly CancellationToken _token;
        private readonly long _clientId;

        public ApiLogListener( IErrorService errorSvc, long clientId, CancellationToken token )
        {
            
            IErrorService errorService = errorSvc;
            if ( errorService == null )
                throw new ArgumentNullException( nameof( errorSvc ) );
            this._errorSvc = errorService;
            this._clientId = clientId;
            this._token = token;
            Filters.Add( LoggingHelper.OnlyError );
        }

        protected override void OnWriteMessage( LogMessage message )
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
            errorSvc.AddAsync( entity, token ).ContinueWith( ( Action<Task<Error>> ) ( t => exception = t.Exception ) );
        }
    }
}
