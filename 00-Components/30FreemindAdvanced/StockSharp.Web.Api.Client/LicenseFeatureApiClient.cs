// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.LicenseFeatureApiClient
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

internal class LicenseFeatureApiClient :
  BaseApiEntityClient<LicenseFeature>,
  ILicenseFeatureService,
  IBaseEntityService<LicenseFeature>
{
    public LicenseFeatureApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public LicenseFeatureApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<LicenseFeature>> ILicenseFeatureService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? roleId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<LicenseFeature>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[11]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) roleId,
      (object) like,
      (object) likeCompare
        });
    }

    Task ILicenseFeatureService.AddRoleAsync(
      long featureId,
      long roleId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, new object[2]
        {
      (object) featureId,
      (object) roleId
        });
    }

    Task<bool> ILicenseFeatureService.RemoveRoleAsync(
      long featureId,
      long roleId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, new object[2]
        {
      (object) featureId,
      (object) roleId
        });
    }
}
