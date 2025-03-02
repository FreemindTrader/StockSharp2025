using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class PayGatewayApiClient : BaseApiEntityClient<PayGateway>, IPayGatewayService, IBaseEntityService<PayGateway>, IBaseService
    {
        public PayGatewayApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public PayGatewayApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<PayGateway>> IPayGatewayService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? domainId,
          string url,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<PayGateway>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, domainId, url );
        }
    }
}
