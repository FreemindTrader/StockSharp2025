// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.MessageApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class MessageApiClient :
  BaseApiEntityClient<Message>,
  IMessageService,
  IBaseEntityService<Message>
{
    public MessageApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public MessageApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
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
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Message>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[20]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) pm,
      (object) domainId,
      (object) clientId,
      (object) topicId,
      (object) parentId,
      (object) excludeRoot,
      (object) rootsOnly,
      (object) convertBodyToHtml,
      (object) convertBodyToHtmlWithDomainId,
      (object) truncate,
      (object) like,
      (object) likeCompare
        });
    }

    Task<BaseEntitySet<SystemMessage>> IMessageService.FindSystemAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      long? domainId,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<SystemMessage>>(RestBaseApiClient.GetCurrentMethod("FindSystemAsync"), cancellationToken, new object[7]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) domainId
        });
    }

    Task<string> IMessageService.TextCleanAsync(
      long? messageId,
      string text,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("TextCleanAsync"), cancellationToken, new object[2]
        {
      (object) messageId,
      (object) text
        });
    }

    Task<string> IMessageService.TextToHtmlAsync(
      long? messageId,
      long? pageId,
      string text,
      long domainId,
      int? truncate,
      bool? preventScaling,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("TextToHtmlAsync"), cancellationToken, new object[6]
        {
      (object) messageId,
      (object) pageId,
      (object) text,
      (object) domainId,
      (object) truncate,
      (object) preventScaling
        });
    }

    Task<string> IMessageService.GetHtmlAsync(
      long domainId,
      long? messageId,
      long? pageId,
      int? truncate,
      bool? preventScaling,
      CancellationToken cancellationToken)
    {
        return this.Get<string>(RestBaseApiClient.GetCurrentMethod("GetHtmlAsync"), cancellationToken, new object[5]
        {
      (object) domainId,
      (object) messageId,
      (object) pageId,
      (object) truncate,
      (object) preventScaling
        });
    }

    Task<string> IMessageService.BodyToHtmlAsync(
      string body,
      long domainId,
      int? truncate,
      bool? preventScaling,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("BodyToHtmlAsync"), cancellationToken, new object[4]
        {
      (object) body,
      (object) domainId,
      (object) truncate,
      (object) preventScaling
        });
    }

    Task<string> IMessageService.MessageToHtmlAsync(
      long messageId,
      long domainId,
      int? truncate,
      bool? preventScaling,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("MessageToHtmlAsync"), cancellationToken, new object[4]
        {
      (object) messageId,
      (object) domainId,
      (object) truncate,
      (object) preventScaling
        });
    }

    Task<object> IMessageService.GetJsonLDAsync(
      long domainId,
      long? pageId,
      long? productId,
      long? clientId,
      long? topicId,
      CancellationToken cancellationToken)
    {
        return this.Get<object>(RestBaseApiClient.GetCurrentMethod("GetJsonLDAsync"), cancellationToken, new object[5]
        {
      (object) domainId,
      (object) pageId,
      (object) productId,
      (object) clientId,
      (object) topicId
        });
    }

    Task<string> IMessageService.GetOpenGraphAsync(
      long domainId,
      long? pageId,
      long? productId,
      long? clientId,
      long? topicId,
      CancellationToken cancellationToken)
    {
        return this.Get<string>(RestBaseApiClient.GetCurrentMethod("GetOpenGraphAsync"), cancellationToken, new object[5]
        {
      (object) domainId,
      (object) pageId,
      (object) productId,
      (object) clientId,
      (object) topicId
        });
    }

    Task<Message> IMessageService.GetWithPageAsync(
      long messageId,
      int pageSize,
      CancellationToken cancellationToken)
    {
        return this.Get<Message>(RestBaseApiClient.GetCurrentMethod("GetWithPageAsync"), cancellationToken, new object[2]
        {
      (object) messageId,
      (object) pageSize
        });
    }

    Task IMessageService.WatermarkAsync(long messageId, CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("WatermarkAsync"), cancellationToken, new object[1]
        {
      (object) messageId
        });
    }
}
