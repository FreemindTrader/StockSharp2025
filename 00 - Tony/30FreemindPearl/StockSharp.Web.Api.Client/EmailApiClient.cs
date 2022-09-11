
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
            return Get<BaseEntitySet<Email>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, transactionId, maxErrorCount );
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
            return Post( GetCurrentMethod( "StartMassRequestAsync"), cancellationToken, transactionId, domainId, included, excluded, fromAddress, fromAlias, subject, bodyHtml, bodyPlain );
        }

        Task IEmailService.StopMassRequestAsync(
          Guid transactionId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "StopMassRequestAsync"), cancellationToken, transactionId );
        }

        Task<BaseEntitySet<(Guid transactionId, bool isCancelled, long domainId, DomainModel.Client[] clients, string fromAddress, string fromAlias, string subject, (string bodyHtml, string bodyPlain))>> IEmailService.GetMassRequests(
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<(Guid transactionId, bool isCancelled, long domainId, DomainModel.Client[] clients, string fromAddress, string fromAlias, string subject, (string bodyHtml, string bodyPlain))>>( GetCurrentMethod( "GetMassRequests"), cancellationToken);
        }
    }
}
