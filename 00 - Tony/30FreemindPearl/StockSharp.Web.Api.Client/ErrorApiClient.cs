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
            return Get<BaseEntitySet<Error>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, paymentId, like, likeCompare );
        }

        Task IErrorService.MinimizePriorityAsync(
          long id,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "MinimizePriorityAsync"), cancellationToken, id );
        }
    }
}
