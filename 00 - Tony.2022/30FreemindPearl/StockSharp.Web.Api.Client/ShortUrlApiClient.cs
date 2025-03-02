using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ShortUrlApiClient : BaseApiEntityClient<ShortUrl>, IShortUrlService, IBaseEntityService<ShortUrl>, IBaseService
    {
        public ShortUrlApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ShortUrlApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ShortUrl>> IShortUrlService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ShortUrl>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId );
        }

        Task<ShortUrl> IShortUrlService.GetByShortAsync(
          string url,
          CancellationToken cancellationToken)
        {
            return Get<ShortUrl>( GetCurrentMethod( "GetByShortAsync"), cancellationToken, url );
        }

        Task<ShortUrl> IShortUrlService.GetByOriginAsync(
          string origin,
          CancellationToken cancellationToken)
        {
            return Get<ShortUrl>( GetCurrentMethod( "GetByOriginAsync"), cancellationToken, origin );
        }
    }
}
