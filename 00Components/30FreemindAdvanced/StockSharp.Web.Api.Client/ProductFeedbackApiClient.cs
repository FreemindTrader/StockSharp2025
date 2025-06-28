// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductFeedbackApiClient
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

internal class ProductFeedbackApiClient :
  BaseApiEntityClient<ProductFeedback>,
  IProductFeedbackService,
  IBaseEntityService<ProductFeedback>
{
    public ProductFeedbackApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ProductFeedbackApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<ProductFeedback>> IProductFeedbackService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? productId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<ProductFeedback>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[12]
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
      (object) productId,
      (object) like,
      (object) likeCompare
        });
    }

    Task<ProductFeedback> IProductFeedbackService.GetByProductAndClientAsync(
      long productId,
      long? clientId,
      CancellationToken cancellationToken)
    {
        return this.Get<ProductFeedback>(RestBaseApiClient.GetCurrentMethod("GetByProductAndClientAsync"), cancellationToken, new object[2]
        {
      (object) productId,
      (object) clientId
        });
    }
}
