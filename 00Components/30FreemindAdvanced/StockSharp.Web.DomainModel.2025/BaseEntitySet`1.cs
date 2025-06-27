// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntitySet`1
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.DomainModel
{
    public class BaseEntitySet<TEntity> : IBaseEntitySet, IPersistable
    {
        public long Count { get; set; }

        public TEntity [ ] Items { get; set; }

        Array IBaseEntitySet.Items
        {
            get
            {
                return ( Array ) this.Items;
            }
        }

        bool IBaseEntitySet.Has( Type entityType, long entityId )
        {
            if ( !( entityType == typeof( TEntity ) ) )
                return false;
            TEntity[] items = this.Items;
            if ( items == null )
                return false;
            return ( ( IEnumerable<TEntity> ) items ).Any<TEntity>( ( Func<TEntity, bool> ) ( e =>
            {
                BaseEntity baseEntity =  e as BaseEntity;
                if ( baseEntity == null )
                    return false;
                
                return  baseEntity.Id  == entityId;
            } ) );
        }

        public virtual void Load( SettingsStorage storage )
        {
            this.Count = ( long ) storage.GetValue<long>( "Count", 0L );
            this.Items = ( TEntity [ ] ) storage.GetValue<TEntity [ ]>( "Items", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<long>( "Count", this.Count ).Set<TEntity [ ]>( "Items", this.Items );
        }
    }
}
