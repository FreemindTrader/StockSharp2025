
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
    public class ProductOrderApiClient : BaseApiEntityClient<ProductOrder>, IProductOrderService, IBaseEntityService<ProductOrder>, IBaseService
    {
        public ProductOrderApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public ProductOrderApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<ProductOrder>> IProductOrderService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          long? productId,
          ProductContentTypes2? contentType,
          Decimal? repeatAmountLow,
          Decimal? repeatAmountHigh,
          DateTime? endLow,
          DateTime? endHigh,
          ProductOrderFlags? flags,
          bool? isActiveOnly,
          bool? isReferralOnly,
          bool? isRepeatOnly,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<ProductOrder>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, productId, contentType, repeatAmountLow, repeatAmountHigh, endLow, endHigh, flags, isActiveOnly, isReferralOnly, isRepeatOnly );
        }

        Task<Payment> IProductOrderService.MakeRecurrentAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post<Payment>( GetCurrentMethod( "MakeRecurrentAsync"), cancellationToken, orderId );
        }

        Task IProductOrderService.MakeRecurrentsAsync(
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "MakeRecurrentsAsync"), cancellationToken, Array.Empty<object>());
        }

        Task<ProductOrder> IProductOrderService.RenewAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post<ProductOrder>( GetCurrentMethod( "RenewAsync"), cancellationToken, orderId );
        }

        Task<ProductOrder> IProductOrderService.StopAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post<ProductOrder>( GetCurrentMethod( "StopAsync"), cancellationToken, orderId );
        }

        Task IProductOrderService.TrialRejectAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "TrialRejectAsync"), cancellationToken, orderId );
        }

        Task IProductOrderService.TrialApproveAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "TrialApproveAsync"), cancellationToken, orderId );
        }

        Task IProductOrderService.RefundRejectAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RefundRejectAsync"), cancellationToken, orderId );
        }

        Task IProductOrderService.RefundApproveAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "RefundApproveAsync"), cancellationToken, orderId );
        }
    }
}
