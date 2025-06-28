// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IPaymentService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net.Currencies;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IPaymentService : IBaseEntityService<Payment>, ICurrencyConverter
{
    Task<BaseEntitySet<Payment>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? fromId = null,
      long? toId = null,
      PaymentStates? state = null,
      long? productId = null,
      long? orderId = null,
      long? accountId = null,
      long? gatewayId = null,
      long? domainId = null,
      CurrencyTypes? currency = null,
      bool? isTest = null,
      bool? isRecurrent = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task ApplyActionsAsync(long paymentId, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> ApproveAsync(long paymentId, string returnUrl, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> CancelAsync(long paymentId, string returnUrl, CancellationToken cancellationToken = default(CancellationToken));

    Task<(string payUrl, string payBody)> PayAsync(
      string url,
      long domainId,
      long gatewayId,
      CurrencyTypes currency,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> EncryptAsync(string text, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> DecryptAsync(string text, CancellationToken cancellationToken = default(CancellationToken));

    Task<object> ResponseAsync(
      string gatewayCode,
      IDictionary<string, string> queryArgs,
      byte[] content,
      IDictionary<string, string> formArgs,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<Payment> MakeRecurrentAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

    Task MakeRecurrentsAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task RefundRejectAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));

    Task RefundApproveAsync(long orderId, CancellationToken cancellationToken = default(CancellationToken));
}
