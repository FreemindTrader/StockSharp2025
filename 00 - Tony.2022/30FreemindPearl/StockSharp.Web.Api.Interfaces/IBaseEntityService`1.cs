// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IBaseEntityService`1
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IBaseEntityService<TEntity> : IBaseService where TEntity : BaseEntity
    {
        Task<TEntity[]> GetMultiAsync(long[] ids, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> GetAsync(long id, CancellationToken cancellationToken = default(CancellationToken));

        Task<BaseEntitySet<TEntity>> GetRangeAsync(
          long skip = 0,
          long? count = 20,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity[]> AddBatchAsync(TEntity[] entities, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default(CancellationToken));

        Task RestoreAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
