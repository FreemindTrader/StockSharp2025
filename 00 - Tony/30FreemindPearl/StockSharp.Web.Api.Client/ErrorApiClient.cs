using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ErrorApiClient : BaseApiEntityClient<Error>, IErrorService, IBaseEntityService<Error>, IBaseService
    {
        public ErrorApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ErrorApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Error>> IErrorService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? paymentId,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Error>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)paymentId, (object)like, (object)likeCompare);
        }

        Task IErrorService.MinimizePriorityAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("MinimizePriorityAsync"), cancellationToken, (object)id);
        }
    }
}
