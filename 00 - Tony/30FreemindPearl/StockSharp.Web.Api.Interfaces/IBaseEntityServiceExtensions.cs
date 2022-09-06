// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IBaseEntityServiceExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public static class IBaseEntityServiceExtensions
    {
        public static bool IsNotSaved<TEntity>(this TEntity entity) where TEntity : BaseEntity
        {
            return entity.CheckOnNull<TEntity>(nameof(entity)).Id == 0L;
        }

        public static bool IsSaved<TEntity>(this TEntity entity) where TEntity : BaseEntity
        {
            return !entity.IsNotSaved<TEntity>();
        }

        public static Task<TEntity> SaveAsync<TEntity>(
          this IBaseEntityService<TEntity> service,
          TEntity entity,
          CancellationToken cancellationToken = default(CancellationToken))
          where TEntity : BaseEntity
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.IsNotSaved<TEntity>())
                return service.AddAsync(entity, cancellationToken);
            return service.UpdateAsync(entity, cancellationToken);
        }

        public static Task DeleteOrRestoreAsync<TEntity>(
          this IBaseEntityService<TEntity> service,
          TEntity entity,
          CancellationToken cancellationToken = default(CancellationToken))
          where TEntity : BaseEntity
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.Deleted)
                return service.RestoreAsync(entity.Id, cancellationToken);
            return service.DeleteAsync(entity.Id, cancellationToken);
        }
    }
}
