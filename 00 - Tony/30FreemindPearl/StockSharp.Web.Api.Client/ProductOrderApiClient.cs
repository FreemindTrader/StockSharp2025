
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
            return this.Get<BaseEntitySet<ProductOrder>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)productId, (object)contentType, (object)repeatAmountLow, (object)repeatAmountHigh, (object)endLow, (object)endHigh, (object)flags, (object)isActiveOnly, (object)isReferralOnly, (object)isRepeatOnly);
        }

        Task<Payment> IProductOrderService.MakeRecurrentAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post<Payment>(RestBaseApiClient.GetCurrentMethod("MakeRecurrentAsync"), cancellationToken, (object)orderId);
        }

        Task IProductOrderService.MakeRecurrentsAsync(
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("MakeRecurrentsAsync"), cancellationToken, Array.Empty<object>());
        }

        Task<ProductOrder> IProductOrderService.RenewAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("RenewAsync"), cancellationToken, (object)orderId);
        }

        Task<ProductOrder> IProductOrderService.StopAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post<ProductOrder>(RestBaseApiClient.GetCurrentMethod("StopAsync"), cancellationToken, (object)orderId);
        }

        Task IProductOrderService.TrialRejectAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("TrialRejectAsync"), cancellationToken, (object)orderId);
        }

        Task IProductOrderService.TrialApproveAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("TrialApproveAsync"), cancellationToken, (object)orderId);
        }

        Task IProductOrderService.RefundRejectAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RefundRejectAsync"), cancellationToken, (object)orderId);
        }

        Task IProductOrderService.RefundApproveAsync(
          long orderId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("RefundApproveAsync"), cancellationToken, (object)orderId);
        }
    }
}
