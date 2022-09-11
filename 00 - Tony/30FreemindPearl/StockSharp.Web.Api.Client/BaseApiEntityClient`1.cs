
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    public abstract class BaseApiEntityClient<TEntity> : BaseApiClient, IBaseEntityService<TEntity>, IBaseService where TEntity : BaseEntity
    {
        protected BaseApiEntityClient(HttpMessageInvoker http, SecureString token)
          : base(http, token)
        {
        }

        protected BaseApiEntityClient(HttpMessageInvoker http, string login, SecureString password)
          : base(http, login, password)
        {
        }

        public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return PostAsync<TEntity>(GetCurrentMethod(nameof(AddAsync)), cancellationToken, entity );
        }

        public virtual Task<TEntity[]> AddBatchAsync(
          TEntity[] entities,
          CancellationToken cancellationToken)
        {
            return PostAsync<TEntity[]>(GetCurrentMethod(nameof(AddBatchAsync)), cancellationToken, entities );
        }

        public virtual Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
        {
            return DeleteAsync<bool>(GetCurrentMethod(nameof(DeleteAsync)), cancellationToken, id );
        }

        public virtual Task<TEntity> GetAsync(long id, CancellationToken cancellationToken)
        {
            return GetAsync<TEntity>(GetCurrentMethod(nameof(GetAsync)), cancellationToken, id );
        }

        public virtual Task<TEntity[]> GetMultiAsync(
          long[] ids,
          CancellationToken cancellationToken)
        {
            return GetAsync<TEntity[]>(GetCurrentMethod(nameof(GetMultiAsync)), cancellationToken, ids );
        }

        public virtual Task<BaseEntitySet<TEntity>> GetRangeAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          CancellationToken cancellationToken)
        {
            return Get<BaseEntitySet<TEntity>>(GetCurrentMethod(nameof(GetRangeAsync)), cancellationToken, skip, count, deleted, orderBy, orderByDesc );
        }

        public virtual Task<TEntity> UpdateAsync(
          TEntity entity,
          CancellationToken cancellationToken)
        {
            return PutAsync<TEntity>(GetCurrentMethod(nameof(UpdateAsync)), cancellationToken, entity );
        }

        public virtual Task RestoreAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostAsync<TEntity>( GetCurrentMethod( nameof( RestoreAsync ) ), cancellationToken, id );
        }
    }
}
