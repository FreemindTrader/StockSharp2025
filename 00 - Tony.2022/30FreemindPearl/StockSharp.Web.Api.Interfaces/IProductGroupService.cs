// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IProductGroupService
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IProductGroupService : IBaseEntityService<ProductGroup>, IBaseService
    {
        Task<BaseEntitySet<ProductGroup>> FindAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          long? productId = null,
          long? childId = null,
          long? parentId = null,
          long? managerId = null,
          long? emailTemplateId = null,
          bool? expandInner = null,
          string like = null,
          LikeCompares? likeCompare = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task AddChildAsync(long parentId, long childId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveChildAsync(
          long parentId,
          long childId,
          CancellationToken cancellationToken = default(CancellationToken));

        Task AddManagerAsync(long groupId, long clientId, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> RemoveManagerAsync(
          long groupId,
          long clientId,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
