// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ProductOrderApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class ProductOrderApiClient : BaseApiEntityClient<ProductOrder>, IProductOrderService, IBaseEntityService<ProductOrder>
    {
        public ProductOrderApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ProductOrderApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ProductOrder>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [23]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         clientId,
         productId,
         contentType,
         repeatAmountLow,
         repeatAmountHigh,
         endLow,
         endHigh,
         flags,
         isActiveOnly,
         referralId,
         isRepeatOnly,
         priceType,
         currency,
         like,
         likeCompare
            } );
        }

        Task<ProductOrder> IProductOrderService.RenewAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post<ProductOrder>( RestBaseApiClient.GetCurrentMethod( "RenewAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }

        Task IProductOrderService.StopAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "StopAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }

        Task IProductOrderService.TrialRejectAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "TrialRejectAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }

        Task IProductOrderService.TrialApproveAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "TrialApproveAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }
    }
}
