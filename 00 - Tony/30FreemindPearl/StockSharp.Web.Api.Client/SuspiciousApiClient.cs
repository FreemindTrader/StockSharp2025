
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class SuspiciousApiClient : BaseApiEntityClient<Suspicious>, ISuspiciousService, IBaseEntityService<Suspicious>, IBaseService
    {
        public SuspiciousApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public SuspiciousApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Suspicious>> ISuspiciousService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          bool? messagesOnly,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Suspicious>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)messagesOnly, (object)like, (object)likeCompare);
        }

        Task ISuspiciousService.ApproveAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ApproveAsync"), cancellationToken, (object)id);
        }

        Task ISuspiciousService.RejectAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RejectAsync"), cancellationToken, (object)id);
        }
    }
}
