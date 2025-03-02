
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class DomainApiClient : BaseApiEntityClient<Domain>, IDomainService, IBaseEntityService<Domain>, IBaseService
    {
        public DomainApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public DomainApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Domain>> IDomainService.FindAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<Domain>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc );
        }
    }
}
