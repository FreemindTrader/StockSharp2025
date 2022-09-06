// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ILicenseService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ILicenseService : IBaseEntityService<License>, IBaseService
    {
        Task<BaseEntitySet<License>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          long? featureId = null,
          string platformId = null,
          string hardwareId = null,
          DateTime? expirationDateMin = null,
          DateTime? expirationDateMax = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<License> RenewLicenseAsync(long licenseId, CancellationToken cancellationToken = default(CancellationToken));

        Task SendLicenseByEmailAsync(long licenseId, CancellationToken cancellationToken = default(CancellationToken));

        Task<LicenseFeatureEx> AddFeatureAsync(
          LicenseFeatureEx feature,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveFeatureAsync(long featureId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
