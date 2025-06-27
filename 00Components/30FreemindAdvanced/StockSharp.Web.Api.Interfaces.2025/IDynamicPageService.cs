// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IDynamicPageService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IDynamicPageService : IBaseEntityService<DynamicPage>
    {
        Task<BaseEntitySet<DynamicPage>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          long? parentId = null,
          bool? enabled = null,
          bool? sitemap = null,
          long? menuGroupId = null,
          int? statusCode = null,
          long? topicId = null,
          DynamicPageHandlers? handler = null,
          bool? isEnabled = null,
          string like = null,
          ComparisonOperator? likeCompare = null,
          CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(long pageId, string destUrl)> VisitAsync(
          long pageId,
          long domainId,
          IDictionary<string, string> qs,
          CancellationToken cancellationToken );

        Task<(long pageId, string destUrl)> ActionAsync( long pageId, long domainId, IDictionary<string, string> qs, CancellationToken cancellationToken );


        Task<(long pageId, string destUrl)> CancelAsync( long pageId, long domainId, CancellationToken cancellationToken );

        Task<object> MakeDataContextAsync(
          long pageId,
          long domainId,
          IDictionary<string, string> qs,
          CancellationToken cancellationToken = default( CancellationToken ) );

        [Obsolete( "Use ActionAsync instead." )]

        Task<(long pageId, string destUrl)> ConfirmAsync( long pageId, long domainId, IDictionary<string, string> qs, CancellationToken cancellationToken );
    }
}
