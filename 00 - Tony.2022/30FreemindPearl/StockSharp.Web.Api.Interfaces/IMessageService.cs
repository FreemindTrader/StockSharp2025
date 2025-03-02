// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IMessageService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IMessageService : IBaseEntityService<Message>, IBaseService
    {
        Task<BaseEntitySet<Message>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? pm = null,
          long? domainId = null,
          long? clientId = null,
          long? topicId = null,
          long? parentId = null,
          bool? excludeRoot = null,
          bool? rootsOnly = null,
          bool? convertBodyToHtml = null,
          int? truncate = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<SystemMessage>> FindSystemAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? domainId = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<string> BodyCleanAsync(string body, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> MessageCleanAsync(long messageId, CancellationToken cancellationToken = default(CancellationToken));

        Task<string> BodyToHtmlAsync(
          string body,
          long domainId,
          int? truncate = null,
          bool? preventScaling = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<string> MessageToHtmlAsync(
          long messageId,
          long domainId,
          int? truncate = null,
          bool? preventScaling = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<string> PageToHtmlAsync(
          long pageId,
          long domainId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<object> GetJsonLDAsync(
          long domainId,
          long? pageId = null,
          long? productId = null,
          long? clientId = null,
          long? topicId = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<Message> GetWithPageAsync(
          long messageId,
          int pageSize,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
