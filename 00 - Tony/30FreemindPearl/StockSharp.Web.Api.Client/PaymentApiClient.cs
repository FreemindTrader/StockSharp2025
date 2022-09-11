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
            return Get<BaseEntitySet<Payment>>( GetCurrentMethod( "FindAsync"), cancellationToken, skip, count, deleted, orderBy, orderByDesc, clientId, state, productId, orderId, accountId, gatewayId );
        }

        Task<(Payment payment, DynamicPage page)> IPaymentService.GetByTokenAsync(
          string paymentToken,
          CancellationToken cancellationToken)
        {
            return Get<ValueTuple<Payment, DynamicPage>>( GetCurrentMethod( "GetByTokenAsync"), cancellationToken, paymentToken );
        }

        Task<Payment> IPaymentService.PayFromBalanceAsync(
          string pid,
          CancellationToken cancellationToken)
        {
            return Post<Payment>( GetCurrentMethod( "PayFromBalanceAsync"), cancellationToken, pid );
        }

        Task IPaymentService.ApplyActionsAsync(
          long paymentId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ApplyActionsAsync"), cancellationToken, paymentId );
        }

        Task<string> IPaymentService.ApproveAsync(
          long paymentId,
          string returnUrl,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "ApproveAsync"), cancellationToken, paymentId, returnUrl );
        }

        Task<string> IPaymentService.CancelAsync(
          long paymentId,
          string returnUrl,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "CancelAsync"), cancellationToken, paymentId, returnUrl );
        }

        Task IPaymentService.AddDocumentAsync(
          long paymentId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "AddDocumentAsync"), cancellationToken, paymentId, fileId );
        }

        Task<bool> IPaymentService.RemoveDocumentAsync(
          long paymentId,
          long fileId,
          CancellationToken cancellationToken)
        {
            return Post<bool>( GetCurrentMethod( "RemoveDocumentAsync"), cancellationToken, paymentId, fileId );
        }

        Task<(string payUrl, string payBody)> IPaymentService.PayAsync(
          string url,
          long domainId,
          long gatewayId,
          CurrencyTypes currency,
          CancellationToken cancellationToken)
        {
            return Post<ValueTuple<string, string>>( GetCurrentMethod( "PayAsync"), cancellationToken, url, domainId, gatewayId, currency );
        }

        Task<string> IPaymentService.EncryptAsync(
          string text,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "EncryptAsync"), cancellationToken, text );
        }

        Task<string> IPaymentService.DecryptAsync(
          string text,
          CancellationToken cancellationToken)
        {
            return Post<string>( GetCurrentMethod( "DecryptAsync"), cancellationToken, text );
        }

        Task IPaymentService.ProcessResponseAsync(
          long gatewayId,
          string url,
          byte[] content,
          IDictionary<string, string> responseArgs,
          CancellationToken cancellationToken)
        {
            return Post( GetCurrentMethod( "ProcessResponseAsync"), cancellationToken, gatewayId, url, content, responseArgs );
        }

        Task<Decimal> ICurrencyConverter.GetRateAsync(
          CurrencyTypes from,
          CurrencyTypes to,
          DateTime date,
          CancellationToken cancellationToken)
        {
            return Get<Decimal>( GetCurrentMethod( "GetRateAsync"), cancellationToken, from, to, date );
        }
    }
}
