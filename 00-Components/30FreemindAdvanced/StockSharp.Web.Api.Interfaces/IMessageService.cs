// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IMessageService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IMessageService : IBaseEntityService<Message>
{
    Task<BaseEntitySet<Message>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      bool? pm = null,
      long? domainId = null,
      long? clientId = null,
      long? topicId = null,
      long? parentId = null,
      bool? excludeRoot = null,
      bool? rootsOnly = null,
      bool? convertBodyToHtml = null,
      long? convertBodyToHtmlWithDomainId = null,
      int? truncate = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    [Obsolete]
    Task<BaseEntitySet<SystemMessage>> FindSystemAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      long? domainId = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> TextCleanAsync(long? messageId, string text, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> TextToHtmlAsync(
      long? messageId,
      long? pageId,
      string text,
      long domainId,
      int? truncate = null,
      bool? preventScaling = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> GetHtmlAsync(
      long domainId,
      long? messageId = null,
      long? pageId = null,
      int? truncate = null,
      bool? preventScaling = null,
      CancellationToken cancellationToken = default(CancellationToken));

    [Obsolete("Use TextToHtmlAsync method.")]
    Task<string> BodyToHtmlAsync(
      string body,
      long domainId,
      int? truncate = null,
      bool? preventScaling = null,
      CancellationToken cancellationToken = default(CancellationToken));

    [Obsolete("Use TextToHtmlAsync method.")]
    Task<string> MessageToHtmlAsync(
      long messageId,
      long domainId,
      int? truncate = null,
      bool? preventScaling = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<object> GetJsonLDAsync(
      long domainId,
      long? pageId = null,
      long? productId = null,
      long? clientId = null,
      long? topicId = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> GetOpenGraphAsync(
      long domainId,
      long? pageId = null,
      long? productId = null,
      long? clientId = null,
      long? topicId = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<Message> GetWithPageAsync(long messageId, int pageSize, CancellationToken cancellationToken = default(CancellationToken));

    Task WatermarkAsync(long messageId, CancellationToken cancellationToken = default(CancellationToken));
}
