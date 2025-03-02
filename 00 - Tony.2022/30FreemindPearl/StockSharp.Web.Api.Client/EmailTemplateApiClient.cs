using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using Ecng.Common;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class EmailTemplateApiClient : BaseApiEntityClient<EmailTemplate>, IEmailTemplateService, IBaseEntityService<EmailTemplate>, IBaseService
    {
        public EmailTemplateApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public EmailTemplateApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<EmailTemplate>> IEmailTemplateService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? productId,
          long? productGroupId,
          long? topicId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<EmailTemplate>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, productId, productGroupId, topicId );
        }

        Task IEmailTemplateService.TestAsync(
          string templateName,
          CurrencyTypes currency,
          long? productId,
          long? groupId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "TestAsync"), cancellationToken, templateName, currency, productId, groupId );
        }
    }
}
