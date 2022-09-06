
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class EmailApiClient : BaseApiEntityClient<Email>, IEmailService, IBaseEntityService<Email>, IBaseService
    {
        public EmailApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public EmailApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Email>> IEmailService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          Guid? transactionId,
          int? maxErrorCount,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Email>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)transactionId, (object)maxErrorCount);
        }

        Task IEmailService.StartMassRequestAsync(
          Guid transactionId,
          long domainId,
          long[] included,
          long[] excluded,
          string fromAddress,
          string fromAlias,
          string subject,
          string bodyHtml,
          string bodyPlain,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("StartMassRequestAsync"), cancellationToken, (object)transactionId, (object)domainId, (object)included, (object)excluded, (object)fromAddress, (object)fromAlias, (object)subject, (object)bodyHtml, (object)bodyPlain);
        }

        Task IEmailService.StopMassRequestAsync(
          Guid transactionId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("StopMassRequestAsync"), cancellationToken, (object)transactionId);
        }

        Task<BaseEntitySet<(Guid transactionId, bool isCancelled, long domainId, StockSharp.Web.DomainModel.Client[] clients, string fromAddress, string fromAlias, string subject, (string bodyHtml, string bodyPlain))>> IEmailService.GetMassRequests(
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<(Guid transactionId, bool isCancelled, long domainId, StockSharp.Web.DomainModel.Client[] clients, string fromAddress, string fromAlias, string subject, (string bodyHtml, string bodyPlain))>>(RestBaseApiClient.GetCurrentMethod("GetMassRequests"), cancellationToken);
        }
    }
}
