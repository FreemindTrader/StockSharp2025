// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductGroupApiClient
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

internal class ProductGroupApiClient :
  BaseApiEntityClient<ProductGroup>,
  IProductGroupService,
  IBaseEntityService<ProductGroup>
{
    public ProductGroupApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ProductGroupApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<ProductGroup>> IProductGroupService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? productId,
      long? childId,
      long? parentId,
      long? managerId,
      long? emailTemplateId,
      bool? expandInner,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<ProductGroup>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[16 /*0x10*/]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) productId,
      (object) childId,
      (object) parentId,
      (object) managerId,
      (object) emailTemplateId,
      (object) expandInner,
      (object) like,
      (object) likeCompare
        });
    }

    Task IProductGroupService.AddChildAsync(
      long parentId,
      long childId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddChildAsync"), cancellationToken, new object[2]
        {
      (object) parentId,
      (object) childId
        });
    }

    Task<bool> IProductGroupService.RemoveChildAsync(
      long parentId,
      long childId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveChildAsync"), cancellationToken, new object[2]
        {
      (object) parentId,
      (object) childId
        });
    }

    Task IProductGroupService.AddManagerAsync(
      long groupId,
      long clientId,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddManagerAsync"), cancellationToken, new object[2]
        {
      (object) groupId,
      (object) clientId
        });
    }

    Task<bool> IProductGroupService.RemoveManagerAsync(
      long groupId,
      long clientId,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveManagerAsync"), cancellationToken, new object[2]
        {
      (object) groupId,
      (object) clientId
        });
    }
}
