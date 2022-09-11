using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProductVersionMapApiClient : BaseApiEntityClient<ProductVersionMap>, IProductVersionMapService, IBaseEntityService<ProductVersionMap>, IBaseService
    {
        public ProductVersionMapApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductVersionMapApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ProductVersionMap>> IProductVersionMapService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? productId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ProductVersionMap>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, productId );
        }
    }
}
