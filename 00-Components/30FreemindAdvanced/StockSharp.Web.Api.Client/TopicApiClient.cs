// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.TopicApiClient
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

internal class TopicApiClient : BaseApiEntityClient<Topic>, ITopicService, IBaseEntityService<Topic>
{
    public TopicApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public TopicApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<Topic>> ITopicService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? domainId,
      long? clientId,
      long? groupId,
      long? tagId,
      string tagName,
      TopicTypes? topicType,
      bool? isLocked,
      bool? isPinned,
      bool? hasRoles,
      bool? hasGroups,
      bool? convertBodyToHtml,
      long? convertBodyToHtmlWithDomainId,
      bool? cleanText,
      int? truncate,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Topic>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[24]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) domainId,
      (object) clientId,
      (object) groupId,
      (object) tagId,
      (object) tagName,
      (object) topicType,
      (object) isLocked,
      (object) isPinned,
      (object) hasRoles,
      (object) hasGroups,
      (object) convertBodyToHtml,
      (object) convertBodyToHtmlWithDomainId,
      (object) cleanText,
      (object) truncate,
      (object) like,
      (object) likeCompare
        });
    }

    Task ITopicService.AddRoleAsync(
      long groupId,
      long roleId,
      bool isRead,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("AddRoleAsync"), cancellationToken, new object[3]
        {
      (object) groupId,
      (object) roleId,
      (object) isRead
        });
    }

    Task<bool> ITopicService.RemoveRoleAsync(
      long groupId,
      long roleId,
      bool isRead,
      CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveRoleAsync"), cancellationToken, new object[3]
        {
      (object) groupId,
      (object) roleId,
      (object) isRead
        });
    }
}
