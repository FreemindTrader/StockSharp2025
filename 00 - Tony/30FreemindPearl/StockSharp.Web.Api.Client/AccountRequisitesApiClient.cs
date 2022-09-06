
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class AccountRequisitesApiClient : BaseApiEntityClient<AccountRequisites>, IAccountRequisitesService, IBaseEntityService<AccountRequisites>, IBaseService
    {
        public AccountRequisitesApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public AccountRequisitesApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<AccountRequisites>> IAccountRequisitesService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<AccountRequisites>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId);
        }
    }
}
