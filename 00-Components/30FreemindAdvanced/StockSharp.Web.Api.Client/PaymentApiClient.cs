// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.PaymentApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using Ecng.Net.Currencies;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class PaymentApiClient :
  BaseApiEntityClient<Payment>,
  IPaymentService,
  IBaseEntityService<Payment>,
  ICurrencyConverter
{
    public PaymentApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public PaymentApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<Payment>> IPaymentService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? fromId,
      long? toId,
      PaymentStates? state,
      long? productId,
      long? orderId,
      long? accountId,
      long? gatewayId,
      long? domainId,
      CurrencyTypes? currency,
      bool? isTest,
      bool? isRecurrent,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Payment>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[21]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) fromId,
      (object) toId,
      (object) state,
      (object) productId,
      (object) orderId,
      (object) accountId,
      (object) gatewayId,
      (object) domainId,
      (object) currency,
      (object) isTest,
      (object) isRecurrent,
      (object) like,
      (object) likeCompare
        });
    }

    Task IPaymentService.ApplyActionsAsync(long paymentId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("ApplyActionsAsync"), cancellationToken, new object[1]
        {
      (object) paymentId
        });
    }

    Task<string> IPaymentService.ApproveAsync(
      long paymentId,
      string returnUrl,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("ApproveAsync"), cancellationToken, new object[2]
        {
      (object) paymentId,
      (object) returnUrl
        });
    }

    Task<string> IPaymentService.CancelAsync(
      long paymentId,
      string returnUrl,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("CancelAsync"), cancellationToken, new object[2]
        {
      (object) paymentId,
      (object) returnUrl
        });
    }

    Task<(string payUrl, string payBody)> IPaymentService.PayAsync(
      string url,
      long domainId,
      long gatewayId,
      CurrencyTypes currency,
      CancellationToken cancellationToken)
    {
        return this.Post<(string, string)>(RestBaseApiClient.GetCurrentMethod("PayAsync"), cancellationToken, new object[4]
        {
      (object) url,
      (object) domainId,
      (object) gatewayId,
      (object) currency
        });
    }

    Task<string> IPaymentService.EncryptAsync(string text, CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("EncryptAsync"), cancellationToken, new object[1]
        {
      (object) text
        });
    }

    Task<string> IPaymentService.DecryptAsync(string text, CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("DecryptAsync"), cancellationToken, new object[1]
        {
      (object) text
        });
    }

    Task<object> IPaymentService.ResponseAsync(
      string gatewayCode,
      IDictionary<string, string> queryArgs,
      byte[] content,
      IDictionary<string, string> formArgs,
      CancellationToken cancellationToken)
    {
        return this.Post<object>(RestBaseApiClient.GetCurrentMethod("ResponseAsync"), cancellationToken, new object[4]
        {
      (object) gatewayCode,
      (object) queryArgs,
      (object) content,
      (object) formArgs
        });
    }

    Task<Payment> IPaymentService.MakeRecurrentAsync(
      long orderId,
      CancellationToken cancellationToken)
    {
        return this.Post<Payment>(RestBaseApiClient.GetCurrentMethod("MakeRecurrentAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }

    Task IPaymentService.MakeRecurrentsAsync(CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("MakeRecurrentsAsync"), cancellationToken, Array.Empty<object>());
    }

    Task IPaymentService.RefundRejectAsync(long orderId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RefundRejectAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }

    Task IPaymentService.RefundApproveAsync(long orderId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("RefundApproveAsync"), cancellationToken, new object[1]
        {
      (object) orderId
        });
    }

    Task<Decimal> ICurrencyConverter.GetRateAsync(
      CurrencyTypes from,
      CurrencyTypes to,
      DateTime date,
      CancellationToken cancellationToken)
    {
        return this.Get<Decimal>(RestBaseApiClient.GetCurrentMethod("GetRateAsync"), cancellationToken, new object[3]
        {
      (object) from,
      (object) to,
      (object) date
        });
    }
}
