
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class FavoriteApiClient : BaseApiEntityClient<Favorite>, IFavoriteService, IBaseEntityService<Favorite>, IBaseService
    {
        public FavoriteApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public FavoriteApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Favorite>> IFavoriteService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? topicId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<Favorite>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, topicId );
        }
    }
}
