// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProfileVisitApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class ProfileVisitApiClient :
  BaseApiEntityClient<ProfileVisit>,
  IProfileVisitService,
  IBaseEntityService<ProfileVisit>
{
    public ProfileVisitApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ProfileVisitApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<ProfileVisit>> IProfileVisitService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? profileId,
      long? clientId,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<ProfileVisit>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[10]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) profileId,
      (object) clientId
        });
    }
}
