// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.LicenseApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class LicenseApiClient :
  BaseApiEntityClient<License>,
  ILicenseService,
  IBaseEntityService<License>
{
    public LicenseApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public LicenseApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<License>> ILicenseService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? featureId,
      string platformId,
      string hardwareId,
      DateTime? expirationDateMin,
      DateTime? expirationDateMax,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<License>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[16 /*0x10*/]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId,
      (object) featureId,
      (object) platformId,
      (object) hardwareId,
      (object) expirationDateMin,
      (object) expirationDateMax,
      (object) like,
      (object) likeCompare
        });
    }

    Task ILicenseService.SendLicenseByEmailAsync(long licenseId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("SendLicenseByEmailAsync"), cancellationToken, new object[1]
        {
      (object) licenseId
        });
    }

    Task<LicenseFeatureEx> ILicenseService.AddFeatureAsync(
      LicenseFeatureEx feature,
      CancellationToken cancellationToken)
    {
        return this.Post<LicenseFeatureEx>(RestBaseApiClient.GetCurrentMethod("AddFeatureAsync"), cancellationToken, new object[1]
        {
      (object) feature
        });
    }

    Task<bool> ILicenseService.RemoveFeatureAsync(long featureId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveFeatureAsync"), cancellationToken, new object[1]
        {
      (object) featureId
        });
    }
}
