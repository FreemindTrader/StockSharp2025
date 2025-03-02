// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.TopicApiClient
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
    internal class TopicApiClient : BaseApiEntityClient<Topic>, ITopicService, IBaseEntityService<Topic>
    {
        public TopicApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public TopicApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Topic>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [24]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         domainId,
         clientId,
         groupId,
         tagId,
         tagName,
         topicType,
         isLocked,
         isPinned,
         hasRoles,
         hasGroups,
         convertBodyToHtml,
         convertBodyToHtmlWithDomainId,
         cleanText,
         truncate,
         like,
         likeCompare
            } );
        }

        Task ITopicService.AddRoleAsync(
          long groupId,
          long roleId,
          bool isRead,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "AddRoleAsync" ), cancellationToken, new object [3]
            {
         groupId,
         roleId,
         isRead
            } );
        }

        Task<bool> ITopicService.RemoveRoleAsync(
          long groupId,
          long roleId,
          bool isRead,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveRoleAsync" ), cancellationToken, new object [3]
            {
         groupId,
         roleId,
         isRead
            } );
        }
    }
}
