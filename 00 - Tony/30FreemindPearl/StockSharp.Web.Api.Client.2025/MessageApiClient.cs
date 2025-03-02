// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.MessageApiClient
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
    internal class MessageApiClient : BaseApiEntityClient<Message>, IMessageService, IBaseEntityService<Message>
    {
        public MessageApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public MessageApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<Message>> IMessageService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          bool? pm,
          long? domainId,
          long? clientId,
          long? topicId,
          long? parentId,
          bool? excludeRoot,
          bool? rootsOnly,
          bool? convertBodyToHtml,
          long? convertBodyToHtmlWithDomainId,
          int? truncate,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Message>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [20]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         pm,
         domainId,
         clientId,
         topicId,
         parentId,
         excludeRoot,
         rootsOnly,
         convertBodyToHtml,
         convertBodyToHtmlWithDomainId,
         truncate,
         like,
         likeCompare
            } );
        }

        Task<BaseEntitySet<SystemMessage>> IMessageService.FindSystemAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          long? domainId,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<SystemMessage>>( RestBaseApiClient.GetCurrentMethod( "FindSystemAsync" ), cancellationToken, new object [7]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         domainId
            } );
        }

        Task<string> IMessageService.TextCleanAsync(
          long? messageId,
          string text,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "TextCleanAsync" ), cancellationToken, new object [2]
            {
         messageId,
         text
            } );
        }

        Task<string> IMessageService.TextToHtmlAsync(
          long? messageId,
          long? pageId,
          string text,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "TextToHtmlAsync" ), cancellationToken, new object [6]
            {
         messageId,
         pageId,
         text,
         domainId,
         truncate,
         preventScaling
            } );
        }

        Task<string> IMessageService.GetHtmlAsync(
          long domainId,
          long? messageId,
          long? pageId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken )
        {
            return this.Get<string>( RestBaseApiClient.GetCurrentMethod( "GetHtmlAsync" ), cancellationToken, new object [5]
            {
         domainId,
         messageId,
         pageId,
         truncate,
         preventScaling
            } );
        }

        Task<string> IMessageService.BodyToHtmlAsync(
          string body,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "BodyToHtmlAsync" ), cancellationToken, new object [4]
            {
         body,
         domainId,
         truncate,
         preventScaling
            } );
        }

        Task<string> IMessageService.MessageToHtmlAsync(
          long messageId,
          long domainId,
          int? truncate,
          bool? preventScaling,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "MessageToHtmlAsync" ), cancellationToken, new object [4]
            {
         messageId,
         domainId,
         truncate,
         preventScaling
            } );
        }

        Task<object> IMessageService.GetJsonLDAsync(
          long domainId,
          long? pageId,
          long? productId,
          long? clientId,
          long? topicId,
          CancellationToken cancellationToken )
        {
            return this.Get<object>( RestBaseApiClient.GetCurrentMethod( "GetJsonLDAsync" ), cancellationToken, new object [5]
            {
         domainId,
         pageId,
         productId,
         clientId,
         topicId
            } );
        }

        Task<string> IMessageService.GetOpenGraphAsync(
          long domainId,
          long? pageId,
          long? productId,
          long? clientId,
          long? topicId,
          CancellationToken cancellationToken )
        {
            return this.Get<string>( RestBaseApiClient.GetCurrentMethod( "GetOpenGraphAsync" ), cancellationToken, new object [5]
            {
         domainId,
         pageId,
         productId,
         clientId,
         topicId
            } );
        }

        Task<Message> IMessageService.GetWithPageAsync(
          long messageId,
          int pageSize,
          CancellationToken cancellationToken )
        {
            return this.Get<Message>( RestBaseApiClient.GetCurrentMethod( "GetWithPageAsync" ), cancellationToken, new object [2]
            {
         messageId,
         pageSize
            } );
        }

        Task IMessageService.WatermarkAsync(
          long messageId,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "WatermarkAsync" ), cancellationToken, new object [1]
            {
         messageId
            } );
        }
    }
}
