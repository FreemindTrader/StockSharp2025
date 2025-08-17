// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.DraftApiClient
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

internal class DraftApiClient : BaseApiEntityClient<Draft>, IDraftService, IBaseEntityService<Draft>
{
    public DraftApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public DraftApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<Draft>> IDraftService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Draft>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[11]
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
      (object) like,
      (object) likeCompare
        });
    }

    Task<Draft> IDraftService.TryGetByPageIdAsync(string pageId, CancellationToken cancellationToken)
    {
        return this.Get<Draft>(RestBaseApiClient.GetCurrentMethod("TryGetByPageIdAsync"), cancellationToken, new object[1]
        {
      (object) pageId
        });
    }

    Task<bool> IDraftService.RemoveByPageAsync(string pageId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveByPageAsync"), cancellationToken, new object[1]
        {
      (object) pageId
        });
    }

    Task<bool> IDraftService.RemoveFileAsync(long fileId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveFileAsync"), cancellationToken, new object[1]
        {
      (object) fileId
        });
    }
}
