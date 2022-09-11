
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
            return Get<BaseEntitySet<Suspicious>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, messagesOnly, like, likeCompare );
        }

        Task ISuspiciousService.ApproveAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ApproveAsync"), cancellationToken, id );
        }

        Task ISuspiciousService.RejectAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RejectAsync"), cancellationToken, id );
        }
    }
}
