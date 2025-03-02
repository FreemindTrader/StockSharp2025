using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public class LicenseApiClient : BaseApiEntityClient<License>, ILicenseService, IBaseEntityService<License>, IBaseService
    {
        public LicenseApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public LicenseApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<License>> ILicenseService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? featureId,
          string platformId,
          string hardwareId,
          DateTime? expirationDateMin,
          DateTime? expirationDateMax,
          string like,
          LikeCompares? likeCompare,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<License>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, featureId, platformId, hardwareId, expirationDateMin, expirationDateMax, like, likeCompare );
        }

        Task<License> ILicenseService.RenewLicenseAsync(
          long licenseId,
          CancellationToken cancellationToken)
        {
            return Post<License>( GetCurrentMethod( "RenewLicenseAsync"), cancellationToken, licenseId );
        }

        Task ILicenseService.SendLicenseByEmailAsync(
          long licenseId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "SendLicenseByEmailAsync"), cancellationToken, licenseId );
        }

        Task<LicenseFeatureEx> ILicenseService.AddFeatureAsync(
          LicenseFeatureEx feature,
          CancellationToken cancellationToken)
        {
            return Post<LicenseFeatureEx>( GetCurrentMethod( "AddFeatureAsync"), cancellationToken, feature );
        }

        Task<bool> ILicenseService.RemoveFeatureAsync(
          long featureId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveFeatureAsync"), cancellationToken, featureId );
        }
    }
}
