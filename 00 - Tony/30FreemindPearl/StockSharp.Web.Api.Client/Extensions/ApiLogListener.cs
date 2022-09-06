
using StockSharp.Logging;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;

namespace StockSharp.Web.Api.Client.Extensions
{
    public class ApiLogListener : LogListener
    {
        private readonly IErrorService _errorSvc;
        private readonly CancellationToken _token;

        public ApiLogListener(IErrorService errorSvc, CancellationToken token)
        {
            IErrorService errorService = errorSvc;
            if (errorService == null)
                throw new ArgumentNullException(nameof(errorSvc));
            _errorSvc = errorService;
            _token = token;
            Filters.Add(LoggingHelper.OnlyError);
        }

        protected override void OnWriteMessage(LogMessage message)
        {
            IErrorService errorSvc = _errorSvc;
            Error entity = new Error();
            entity.Url = message.Source.Name;
            entity.Text = message.Message;
            CancellationToken token = _token;
            errorSvc.AddAsync(entity, token);
        }
    }
}
