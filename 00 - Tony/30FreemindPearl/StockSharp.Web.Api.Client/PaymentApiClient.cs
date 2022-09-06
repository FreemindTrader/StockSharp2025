using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net.Currencies;

namespace StockSharp.Web.Api.Client
{
    public class PaymentApiClient : BaseApiEntityClient<Payment>, IPaymentService, IBaseEntityService<Payment>, IBaseService, ICurrencyConverter
    {
        public PaymentApiClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        public PaymentApiClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        Task<BaseEntitySet<Payment>> IPaymentService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          long? clientId,
          PaymentStates? state,
          long? productId,
          long? orderId,
          long? accountId,
          long? gatewayId,
          CancellationToken cancellationToken)
        {
            return this.Get<BaseEntitySet<Payment>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, (object)skip, (object)count, (object)deleted, (object)orderBy, (object)orderByDesc, (object)clientId, (object)state, (object)productId, (object)orderId, (object)accountId, (object)gatewayId);
        }

        Task<(Payment payment, DynamicPage page)> IPaymentService.GetByTokenAsync(
          string paymentToken,
          CancellationToken cancellationToken)
        {
            return this.Get<ValueTuple<Payment, DynamicPage>>(RestBaseApiClient.GetCurrentMethod("GetByTokenAsync"), cancellationToken, (object)paymentToken);
        }

        Task<Payment> IPaymentService.PayFromBalanceAsync(
          string pid,
          CancellationToken cancellationToken)
        {
            return this.Post<Payment>(RestBaseApiClient.GetCurrentMethod("PayFromBalanceAsync"), cancellationToken, (object)pid);
        }

        Task IPaymentService.ApplyActionsAsync(
          long paymentId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ApplyActionsAsync"), cancellationToken, (object)paymentId);
        }

        Task<string> IPaymentService.ApproveAsync(
          long paymentId,
          string returnUrl,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("ApproveAsync"), cancellationToken, (object)paymentId, (object)returnUrl);
        }

        Task<string> IPaymentService.CancelAsync(
          long paymentId,
          string returnUrl,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("CancelAsync"), cancellationToken, (object)paymentId, (object)returnUrl);
        }

        Task IPaymentService.AddDocumentAsync(
          long paymentId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("AddDocumentAsync"), cancellationToken, (object)paymentId, (object)fileId);
        }

        Task<bool> IPaymentService.RemoveDocumentAsync(
          long paymentId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveDocumentAsync"), cancellationToken, (object)paymentId, (object)fileId);
        }

        Task<(string payUrl, string payBody)> IPaymentService.PayAsync(
          string url,
          long domainId,
          long gatewayId,
          CurrencyTypes currency,
          CancellationToken cancellationToken)
        {
            return this.Post<ValueTuple<string, string>>(RestBaseApiClient.GetCurrentMethod("PayAsync"), cancellationToken, (object)url, (object)domainId, (object)gatewayId, (object)currency);
        }

        Task<string> IPaymentService.EncryptAsync(
          string text,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("EncryptAsync"), cancellationToken, (object)text);
        }

        Task<string> IPaymentService.DecryptAsync(
          string text,
          CancellationToken cancellationToken)
        {
            return this.Post<string>(RestBaseApiClient.GetCurrentMethod("DecryptAsync"), cancellationToken, (object)text);
        }

        Task IPaymentService.ProcessResponseAsync(
          long gatewayId,
          string url,
          byte[] content,
          IDictionary<string, string> responseArgs,
          CancellationToken cancellationToken)
        {
            return this.Post(RestBaseApiClient.GetCurrentMethod("ProcessResponseAsync"), cancellationToken, (object)gatewayId, (object)url, (object)content, (object)responseArgs);
        }

        Task<Decimal> ICurrencyConverter.GetRateAsync(
          CurrencyTypes from,
          CurrencyTypes to,
          DateTime date,
          CancellationToken cancellationToken)
        {
            return this.Get<Decimal>(RestBaseApiClient.GetCurrentMethod("GetRateAsync"), cancellationToken, (object)from, (object)to, (object)date);
        }
    }
}
