using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class LicenseFeatureApiClient : BaseApiEntityClient<LicenseFeature>, ILicenseFeatureService, IBaseEntityService<LicenseFeature>, IBaseService
    {
        public LicenseFeatureApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public LicenseFeatureApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<LicenseFeature>> ILicenseFeatureService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? roleId,
          bool? ownOnly,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<LicenseFeature>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, roleId, ownOnly );
        }
    }
}
