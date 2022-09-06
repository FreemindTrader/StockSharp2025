using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ClientBalanceApiClient : BaseApiEntityClient<ClientBalance>, IClientBalanceService, IBaseEntityService<ClientBalance>, IBaseService
    {
        public ClientBalanceApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ClientBalanceApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ClientBalanceHistory>> IClientBalanceService.FindHistoryAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, long? balanceId, long? clientId, CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<ClientBalanceHistory>>(RestBaseApiClient.GetCurrentMethod("FindHistoryAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)balanceId, (object)clientId);
        }

        Task<ClientBalanceHistory> IClientBalanceService.UpdateHistoryAsync( ClientBalanceHistory history, CancellationToken cancellationToken)
        {
            return this.Put<ClientBalanceHistory>(RestBaseApiClient.GetCurrentMethod("UpdateHistoryAsync"), cancellationToken, (object)history);
        }

        Task IClientBalanceService.RemoveHistoryAsync( long id, CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RemoveHistoryAsync"), cancellationToken, (object)id);
        }
    }
}
