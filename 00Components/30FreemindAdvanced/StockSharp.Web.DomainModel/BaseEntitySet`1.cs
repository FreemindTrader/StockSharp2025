// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntitySet`1
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class BaseEntitySet<TEntity> : IBaseEntitySet, IPersistable
{
    public long Count { get; set; }

    public TEntity[] Items { get; set; }

    Array IBaseEntitySet.Items => (Array)this.Items;

    bool IBaseEntitySet.Has(Type entityType, long entityId)
    {
        if (entityType != typeof(TEntity))
            return false;

        TEntity[] items = this.Items;
        if (items == null)
            return false;
        return ((IEnumerable<TEntity>)items).Any<TEntity>((Func<TEntity, bool>)(e =>
        {
            BaseEntity baseEntity = e as BaseEntity;
            if (baseEntity == null)
                return false;

            return baseEntity.Id == entityId;
        }));
    }

    public virtual void Load(SettingsStorage storage)
    {
        this.Count = storage.GetValue<long>("Count", 0L);
        this.Items = storage.GetValue<TEntity[]>("Items", (TEntity[])null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<long>("Count", this.Count).Set<TEntity[]>("Items", this.Items);
    }
}
