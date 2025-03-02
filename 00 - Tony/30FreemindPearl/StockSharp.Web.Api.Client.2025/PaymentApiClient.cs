// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.PaymentApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using Ecng.Net.Currencies;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class PaymentApiClient : BaseApiEntityClient<Payment>, IPaymentService, IBaseEntityService<Payment>, ICurrencyConverter
    {
        public PaymentApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public PaymentApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Payment>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [21]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         fromId,
         toId,
         state,
         productId,
         orderId,
         accountId,
         gatewayId,
         domainId,
         currency,
         isTest,
         isRecurrent,
         like,
         likeCompare
            } );
        }

        Task IPaymentService.ApplyActionsAsync(
          long paymentId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ApplyActionsAsync" ), cancellationToken, new object [1]
            {
         paymentId
            } );
        }

        Task<string> IPaymentService.ApproveAsync(
          long paymentId,
          string returnUrl,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "ApproveAsync" ), cancellationToken, new object [2]
            {
         paymentId,
         returnUrl
            } );
        }

        Task<string> IPaymentService.CancelAsync(
          long paymentId,
          string returnUrl,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "CancelAsync" ), cancellationToken, new object [2]
            {
         paymentId,
         returnUrl
            } );
        }

        
        Task<(string payUrl, string payBody)> IPaymentService.PayAsync(
          string url,
          long domainId,
          long gatewayId,
          CurrencyTypes currency,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<string, string>>( RestBaseApiClient.GetCurrentMethod( "PayAsync" ), cancellationToken, new object [4]
            {
         url,
         domainId,
         gatewayId,
         currency
            } );
        }

        Task<string> IPaymentService.EncryptAsync(
          string text,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "EncryptAsync" ), cancellationToken, new object [1]
            {
         text
            } );
        }

        Task<string> IPaymentService.DecryptAsync(
          string text,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "DecryptAsync" ), cancellationToken, new object [1]
            {
         text
            } );
        }

        Task<object> IPaymentService.ResponseAsync(
          string gatewayCode,
          IDictionary<string, string> queryArgs,
          byte [ ] content,
          IDictionary<string, string> formArgs,
          CancellationToken cancellationToken )
        {
            return this.Post<object>( RestBaseApiClient.GetCurrentMethod( "ResponseAsync" ), cancellationToken, new object [4]
            {
         gatewayCode,
         queryArgs,
         content,
         formArgs
            } );
        }

        Task<Payment> IPaymentService.MakeRecurrentAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post<Payment>( RestBaseApiClient.GetCurrentMethod( "MakeRecurrentAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }

        Task IPaymentService.MakeRecurrentsAsync( CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "MakeRecurrentsAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task IPaymentService.RefundRejectAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "RefundRejectAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }

        Task IPaymentService.RefundApproveAsync(
          long orderId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "RefundApproveAsync" ), cancellationToken, new object [1]
            {
         orderId
            } );
        }

        Task<Decimal> ICurrencyConverter.GetRateAsync( CurrencyTypes from, CurrencyTypes to, DateTime date, CancellationToken cancellationToken )
        {
            return this.Get<Decimal>( RestBaseApiClient.GetCurrentMethod( "GetRateAsync" ), cancellationToken, new object [3]
            {
         from,
         to,
         date
            } );
        }
    }
}
