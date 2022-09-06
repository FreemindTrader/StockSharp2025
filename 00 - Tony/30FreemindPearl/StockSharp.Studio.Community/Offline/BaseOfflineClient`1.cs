
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.Community.Offline
{
    internal abstract class BaseOfflineClient<TEntity> : IBaseEntityService<TEntity>, IBaseService where TEntity : BaseEntity
    {
        ContextContainer IBaseService.ContextContainer
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        Task<TEntity> IBaseEntityService<TEntity>.AddAsync( TEntity entity, CancellationToken cancellationToken)
        {
            return Task.FromResult<TEntity>(entity);
        }

        Task<TEntity[]> IBaseEntityService<TEntity>.AddBatchAsync( TEntity[] entities, CancellationToken cancellationToken)
        {
            return Task.FromResult<TEntity[]>(entities);
        }

        Task<bool> IBaseEntityService<TEntity>.DeleteAsync( long id, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<TEntity> IBaseEntityService<TEntity>.GetAsync( long id, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<TEntity[]> IBaseEntityService<TEntity>.GetMultiAsync( long[] ids, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<BaseEntitySet<TEntity>> IBaseEntityService<TEntity>.GetRangeAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task IBaseEntityService<TEntity>.RestoreAsync( long id, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        Task<TEntity> IBaseEntityService<TEntity>.UpdateAsync( TEntity entity, CancellationToken cancellationToken)
        {
            return Task.FromResult<TEntity>(entity);
        }
    }
}
