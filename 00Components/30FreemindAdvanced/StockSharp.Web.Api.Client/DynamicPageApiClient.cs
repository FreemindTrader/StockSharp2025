// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.DynamicPageApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Collections.Generic;
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

internal class DynamicPageApiClient :
  BaseApiEntityClient<DynamicPage>,
  IDynamicPageService,
  IBaseEntityService<DynamicPage>
{
    public DynamicPageApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public DynamicPageApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<DynamicPage>> IDynamicPageService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? parentId,
      bool? enabled,
      bool? sitemap,
      long? menuGroupId,
      int? statusCode,
      long? topicId,
      DynamicPageHandlers? handler,
      bool? isEnabled,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<DynamicPage>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[18]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) parentId,
      (object) enabled,
      (object) sitemap,
      (object) menuGroupId,
      (object) statusCode,
      (object) topicId,
      (object) handler,
      (object) isEnabled,
      (object) like,
      (object) likeCompare
        });
    }

    Task<(long pageId, string destUrl)> IDynamicPageService.VisitAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken)
    {
        return this.Post<(long, string)>(RestBaseApiClient.GetCurrentMethod("VisitAsync"), cancellationToken, new object[3]
        {
      (object) pageId,
      (object) domainId,
      (object) qs
        });
    }

    Task<(long pageId, string destUrl)> IDynamicPageService.ActionAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken)
    {
        return this.Post<(long, string)>(RestBaseApiClient.GetCurrentMethod("ActionAsync"), cancellationToken, new object[3]
        {
      (object) pageId,
      (object) domainId,
      (object) qs
        });
    }

    Task<(long pageId, string destUrl)> IDynamicPageService.CancelAsync(
      long pageId,
      long domainId,
      CancellationToken cancellationToken)
    {
        return this.Post<(long, string)>(RestBaseApiClient.GetCurrentMethod("CancelAsync"), cancellationToken, new object[2]
        {
      (object) pageId,
      (object) domainId
        });
    }

    Task<object> IDynamicPageService.MakeDataContextAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken)
    {
        return this.Post<object>(RestBaseApiClient.GetCurrentMethod("MakeDataContextAsync"), cancellationToken, new object[3]
        {
      (object) pageId,
      (object) domainId,
      (object) qs
        });
    }

    Task<(long pageId, string destUrl)> IDynamicPageService.ConfirmAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken)
    {
        return this.Post<(long, string)>(RestBaseApiClient.GetCurrentMethod("ConfirmAsync"), cancellationToken, new object[3]
        {
      (object) pageId,
      (object) domainId,
      (object) qs
        });
    }
}
