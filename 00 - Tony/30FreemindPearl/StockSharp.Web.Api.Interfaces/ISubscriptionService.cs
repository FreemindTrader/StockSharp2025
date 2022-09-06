// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ISubscriptionService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ISubscriptionService : IBaseEntityService<Subscription>, IBaseService
    {
        Task<BaseEntitySet<Subscription>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? unionOrJoin = null,
          long? clientId = null,
          long? topicId = null,
          bool? includeTags = null,
          TopicTypes? topicType = null,
          long? tagId = null,
          long? author = null,
          long? domainId = null,
          bool? checkCanSend = false,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
