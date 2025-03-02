
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProductBugReportApiClient : BaseApiEntityClient<ProductBugReport>, IProductBugReportService, IBaseEntityService<ProductBugReport>, IBaseService
    {
        public ProductBugReportApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductBugReportApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ProductBugReport>> IProductBugReportService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? productId,
          long? errorId,
          bool? aggregated,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ProductBugReport>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, productId, errorId, aggregated );
        }

        Task<ProductBugReport> IProductBugReportService.TryProposeAsync(
          ProductBugReport entity,
          CancellationToken cancellationToken)
        {
            return Post<ProductBugReport>( GetCurrentMethod( "TryProposeAsync"), cancellationToken, entity );
        }
    }
}
