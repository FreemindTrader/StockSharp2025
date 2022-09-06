using Ecng.Common;
using Ecng.Net.Currencies;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IPaymentService : IBaseEntityService<Payment>, IBaseService, ICurrencyConverter
    {
        Task<BaseEntitySet<Payment>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? clientId = null,
          PaymentStates? state = null,
          long? productId = null,
          long? orderId = null,
          long? accountId = null,
          long? gatewayId = null,
          CancellationToken cancellationToken = default(CancellationToken));


        Task<(Payment payment, DynamicPage page)> GetByTokenAsync(string paymentToken, CancellationToken cancellationToken = default(CancellationToken));

        Task<Payment> PayFromBalanceAsync(string pid, CancellationToken cancellationToken = default(CancellationToken));

        Task ApplyActionsAsync(long paymentId, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> ApproveAsync(long paymentId, string returnUrl, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> CancelAsync(long paymentId, string returnUrl, CancellationToken cancellationToken = default(CancellationToken));

        Task AddDocumentAsync(long paymentId, long fileId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveDocumentAsync(long paymentId, long fileId, CancellationToken cancellationToken = default(CancellationToken));


        Task<(string payUrl, string payBody)> PayAsync(string url, long domainId, long gatewayId, CurrencyTypes currency, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> EncryptAsync(string text, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> DecryptAsync(string text, CancellationToken cancellationToken = default(CancellationToken));

        Task ProcessResponseAsync(
          long gatewayId,
          string url,
          byte[] content,
          IDictionary<string, string> responseArgs,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}


