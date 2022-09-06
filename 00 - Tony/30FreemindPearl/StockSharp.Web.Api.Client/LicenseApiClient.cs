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
            return this.Get<BaseEntitySet<License>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)featureId, (object)platformId, (object)hardwareId, (object)expirationDateMin, (object)expirationDateMax, (object)like, (object)likeCompare);
        }

        Task<License> ILicenseService.RenewLicenseAsync(
          long licenseId,
          CancellationToken cancellationToken)
        {
            return this.Post<License>(RestBaseApiClient.GetCurrentMethod("RenewLicenseAsync"), cancellationToken, (object)licenseId);
        }

        Task ILicenseService.SendLicenseByEmailAsync(
          long licenseId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("SendLicenseByEmailAsync"), cancellationToken, (object)licenseId);
        }

        Task<LicenseFeatureEx> ILicenseService.AddFeatureAsync(
          LicenseFeatureEx feature,
          CancellationToken cancellationToken)
        {
            return this.Post<LicenseFeatureEx>(RestBaseApiClient.GetCurrentMethod("AddFeatureAsync"), cancellationToken, (object)feature);
        }

        Task<bool> ILicenseService.RemoveFeatureAsync(
          long featureId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveFeatureAsync"), cancellationToken, (object)featureId);
        }
    }
}
