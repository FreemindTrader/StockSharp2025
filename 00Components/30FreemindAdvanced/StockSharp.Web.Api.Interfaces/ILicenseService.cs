// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ILicenseService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface ILicenseService : IBaseEntityService<License>
{
    Task<BaseEntitySet<License>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? clientId = null,
      long? featureId = null,
      string platformId = null,
      string hardwareId = null,
      DateTime? expirationDateMin = null,
      DateTime? expirationDateMax = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task SendLicenseByEmailAsync(long licenseId, CancellationToken cancellationToken = default(CancellationToken));

    Task<LicenseFeatureEx> AddFeatureAsync(
      LicenseFeatureEx feature,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> RemoveFeatureAsync(long featureId, CancellationToken cancellationToken = default(CancellationToken));
}
