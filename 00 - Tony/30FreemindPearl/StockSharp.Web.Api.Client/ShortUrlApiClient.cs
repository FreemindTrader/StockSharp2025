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
            return this.Get<BaseEntitySet<ShortUrl>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId);
        }

        Task<ShortUrl> IShortUrlService.GetByShortAsync(
          string url,
          CancellationToken cancellationToken)
        {
            return this.Get<ShortUrl>(RestBaseApiClient.GetCurrentMethod("GetByShortAsync"), cancellationToken, (object)url);
        }

        Task<ShortUrl> IShortUrlService.GetByOriginAsync(
          string origin,
          CancellationToken cancellationToken)
        {
            return this.Get<ShortUrl>(RestBaseApiClient.GetCurrentMethod("GetByOriginAsync"), cancellationToken, (object)origin);
        }
    }
}
