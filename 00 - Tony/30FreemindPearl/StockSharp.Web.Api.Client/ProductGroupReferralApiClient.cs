
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProductGroupReferralApiClient : BaseApiEntityClient<ProductGroupReferral>, IProductGroupReferralService, IBaseEntityService<ProductGroupReferral>, IBaseService
    {
        public ProductGroupReferralApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductGroupReferralApiClient(
          HttpMessageInvoker http,
          string login,
          SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ProductGroupReferral>> IProductGroupReferralService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? groupId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<ProductGroupReferral>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)groupId);
        }
    }
}
