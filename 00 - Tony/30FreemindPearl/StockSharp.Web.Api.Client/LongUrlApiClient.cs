
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class LongUrlApiClient : BaseApiEntityClient<LongUrl>, ILongUrlService, IBaseEntityService<LongUrl>, IBaseService
    {
        public LongUrlApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public LongUrlApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<LongUrl>> ILongUrlService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<LongUrl>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc );
        }
    }
}
