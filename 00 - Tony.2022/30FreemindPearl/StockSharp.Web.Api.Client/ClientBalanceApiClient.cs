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
            return Get<BaseEntitySet<ClientBalanceHistory>>( GetCurrentMethod( "FindHistoryAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, balanceId, clientId );
        }

        Task<ClientBalanceHistory> IClientBalanceService.UpdateHistoryAsync( ClientBalanceHistory history, CancellationToken cancellationToken)
        {
            return Put<ClientBalanceHistory>( GetCurrentMethod( "UpdateHistoryAsync"), cancellationToken, history );
        }

        Task IClientBalanceService.RemoveHistoryAsync( long id, CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RemoveHistoryAsync"), cancellationToken, id );
        }
    }
}
