
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProductFeedbackApiClient : BaseApiEntityClient<ProductFeedback>, IProductFeedbackService, IBaseEntityService<ProductFeedback>, IBaseService
    {
        public ProductFeedbackApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductFeedbackApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ProductFeedback>> IProductFeedbackService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? productId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<ProductFeedback>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)productId);
        }

        Task<ProductFeedback> IProductFeedbackService.GetByProductAndClientAsync(
          long productId,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<ProductFeedback>(RestBaseApiClient.GetCurrentMethod("GetByProductAndClientAsync"), cancellationToken, (object)productId, (object)clientId);
        }
    }
}
