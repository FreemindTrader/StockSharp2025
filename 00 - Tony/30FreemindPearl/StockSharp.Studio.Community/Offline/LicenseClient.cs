
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal class LicenseClient : BaseOfflineClient<License>, ILicenseService, IBaseEntityService<License>, IBaseService
    {
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
            return Task.FromResult<BaseEntitySet<License>>(((IEnumerable<License>)Array.Empty<License>()).ToEntitySet<License>(0));
        }

        Task<License> ILicenseService.RenewLicenseAsync(
          long licenseId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task ILicenseService.SendLicenseByEmailAsync(
          long licenseId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<LicenseFeatureEx> ILicenseService.AddFeatureAsync(
          LicenseFeatureEx feature,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<bool> ILicenseService.RemoveFeatureAsync(
          long featureId,
          CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}
