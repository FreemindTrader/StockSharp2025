using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class ProfileVisitApiClient : BaseApiEntityClient<ProfileVisit>, IProfileVisitService, IBaseEntityService<ProfileVisit>, IBaseService
    {
        public ProfileVisitApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProfileVisitApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ProfileVisit>> IProfileVisitService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<ProfileVisit>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId);
        }
    }
}
