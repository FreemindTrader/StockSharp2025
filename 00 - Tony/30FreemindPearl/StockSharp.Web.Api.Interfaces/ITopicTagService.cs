// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ITopicTagService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ITopicTagService : IBaseEntityService<TopicTag>, IBaseService
    {
        Task<BaseEntitySet<TopicTag>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? topicId = null,
          TopicTypes? topicType = null,
          long? domainId = null,
          bool? my = null,
          bool? aggregated = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<TopicTag> GetByNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
