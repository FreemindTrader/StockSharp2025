
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ShortUrlVisitApiClient : BaseApiEntityClient<ShortUrlVisit>, IShortUrlVisitService, IBaseEntityService<ShortUrlVisit>, IBaseService
    {
        public ShortUrlVisitApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ShortUrlVisitApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ShortUrlVisit>> IShortUrlVisitService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? shortUrlId,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ShortUrlVisit>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, shortUrlId );
        }
    }
}
