// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.TopicGroupApiClient
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

internal class TopicGroupApiClient :
  BaseApiEntityClient<TopicGroup>,
  ITopicGroupService,
  IBaseEntityService<TopicGroup>
{
    public TopicGroupApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public TopicGroupApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<TopicGroup>> ITopicGroupService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? roleId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<TopicGroup>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[11]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) roleId,
      (object) like,
      (object) likeCompare
        });
    }

    Task ITopicGroupService.AddRoleAsync(
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

    Task<bool> ITopicGroupService.RemoveRoleAsync(
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
