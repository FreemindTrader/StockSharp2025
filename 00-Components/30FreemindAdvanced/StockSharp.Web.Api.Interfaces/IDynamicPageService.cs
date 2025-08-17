// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IDynamicPageService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

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
      CancellationToken cancellationToken = default(CancellationToken));

    Task<(long pageId, string destUrl)> VisitAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken);

    Task<(long pageId, string destUrl)> ActionAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken);

    Task<(long pageId, string destUrl)> CancelAsync(
      long pageId,
      long domainId,
      CancellationToken cancellationToken);

    Task<object> MakeDataContextAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken = default(CancellationToken));

    [Obsolete("Use ActionAsync instead.")]
    Task<(long pageId, string destUrl)> ConfirmAsync(
      long pageId,
      long domainId,
      IDictionary<string, string> qs,
      CancellationToken cancellationToken);
}
