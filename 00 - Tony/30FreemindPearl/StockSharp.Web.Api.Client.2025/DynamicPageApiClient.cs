// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.DynamicPageApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
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
    internal class DynamicPageApiClient : BaseApiEntityClient<DynamicPage>, IDynamicPageService, IBaseEntityService<DynamicPage>
    {
        public DynamicPageApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public DynamicPageApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<DynamicPage>> IDynamicPageService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? parentId,
          bool? enabled,
          bool? sitemap,
          long? menuGroupId,
          int? statusCode,
          long? topicId,
          DynamicPageHandlers? handler,
          bool? isEnabled,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<DynamicPage>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [18]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         parentId,
         enabled,
         sitemap,
         menuGroupId,
         statusCode,
         topicId,
         handler,
         isEnabled,
         like,
         likeCompare
            } );
        }

        
        Task<(long pageId, string destUrl)> IDynamicPageService.VisitAsync( long pageId, long domainId, IDictionary<string, string> qs, CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<long, string>>( RestBaseApiClient.GetCurrentMethod( "VisitAsync" ), cancellationToken, new object [3]
            {
         pageId,
         domainId,
         qs
            } );
        }

        
        Task<(long pageId, string destUrl )> IDynamicPageService.ActionAsync(
          long pageId,
          long domainId,
          IDictionary<string, string> qs,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<long, string>>( RestBaseApiClient.GetCurrentMethod( "ActionAsync" ), cancellationToken, new object [3]
            {
         pageId,
         domainId,
         qs
            } );
        }
        
        Task<(long pageId, string destUrl)> IDynamicPageService.CancelAsync(
          long pageId,
          long domainId,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<long, string>>( RestBaseApiClient.GetCurrentMethod( "CancelAsync" ), cancellationToken, new object [2]
            {
         pageId,
         domainId
            } );
        }

        Task<object> IDynamicPageService.MakeDataContextAsync( long pageId, long domainId, IDictionary<string, string> qs, CancellationToken cancellationToken )
        {
            return this.Post<object>( RestBaseApiClient.GetCurrentMethod( "MakeDataContextAsync" ), cancellationToken, new object [3]
            {
         pageId,
         domainId,
         qs
            } );
        }

        
        Task<( long pageId, string destUrl)> IDynamicPageService.ConfirmAsync( long pageId, long domainId, IDictionary<string, string> qs, CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<long, string>>( RestBaseApiClient.GetCurrentMethod( "ConfirmAsync" ), cancellationToken, new object [3]
            {
         pageId,
         domainId,
         qs
            } );
        }
    }
}
