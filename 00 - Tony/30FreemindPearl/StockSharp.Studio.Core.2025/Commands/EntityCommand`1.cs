// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.EntityCommand`1
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class EntityCommand<TEntity> : BaseStudioCommand where TEntity : class
    {
        public EntityCommand( Subscription subscription, TEntity entity )
        {
            Subscription subscription1 = subscription;
            if ( subscription1 == null )
                throw new ArgumentNullException( nameof( subscription ) );
            this.Subscription = subscription1;
            TEntity entity1 = entity;
            if ( ( object ) entity1 == null )
                throw new ArgumentNullException( nameof( entity ) );
            this.Entity = entity1;
           
        }

        public Subscription Subscription { get; }

        public TEntity Entity { get; }
    }
}
