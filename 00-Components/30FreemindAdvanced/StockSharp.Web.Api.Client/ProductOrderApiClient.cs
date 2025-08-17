// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductOrderApiClient
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

internal class ProductOrderApiClient :
  BaseApiEntityClient<ProductOrder>,
  IProductOrderService,
  IBaseEntityService<ProductOrder>
{
    public ProductOrderApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ProductOrderApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<ProductOrder>> IProductOrderService.FindAsync(
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
      ProductContentTypes2? contentType,
      Decimal? repeatAmountLow,
      Decimal? repeatAmountHigh,
      DateTime? endLow,
      DateTime? endHigh,
      ProductOrderFlags? flags,
      bool? isActiveOnly,
      long? referralId,
      bool? isRepeatOnly,
      ProductPriceTypes? priceType,
      CurrencyTypes? currency,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<ProductOrder>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[23]
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
      (object) contentType,
      (object) repeatAmountLow,
      (object) repeatAmountHigh,
      (object) endLow,
      (object) endHigh,
      (object) flags,
      (object) isActiveOnly,
      (object) referralId,
      (object) isRepeatOnly,
      (object) priceType,
      (object) currency,
      (object) like,
      (object) likeCompare
        });
    }

    Task<ProductOrder> IProductOrderService.RenewAsync(
      long orderId,
      CancellationToken cancellationToken)
    {
        return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("RenewAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }

    Task IProductOrderService.StopAsync(long orderId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("StopAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }

    Task IProductOrderService.TrialRejectAsync(long orderId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("TrialRejectAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }

    Task IProductOrderService.TrialApproveAsync(long orderId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("TrialApproveAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }
}
