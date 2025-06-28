// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IBaseEntityServiceExtensions
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

public static class IBaseEntityServiceExtensions
{
    public static bool IsNotSaved<TEntity>(this TEntity entity) where TEntity : BaseEntity
    {
        return TypeHelper.CheckOnNull<TEntity>(entity, nameof(entity)).Id == 0L;
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
        if ((object)entity == null)
            throw new ArgumentNullException(nameof(entity));
        return entity.IsNotSaved<TEntity>() ? service.AddAsync(entity, cancellationToken) : service.UpdateAsync(entity, cancellationToken);
    }

    public static Task DeleteOrRestoreAsync<TEntity>(
      this IBaseEntityService<TEntity> service,
      TEntity entity,
      CancellationToken cancellationToken = default(CancellationToken))
      where TEntity : BaseEntity
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service));
        if ((object)entity == null)
            throw new ArgumentNullException(nameof(entity));
        return entity.Deleted ? service.RestoreAsync(entity.Id, cancellationToken) : (Task)service.DeleteAsync(entity.Id, cancellationToken);
    }
}
