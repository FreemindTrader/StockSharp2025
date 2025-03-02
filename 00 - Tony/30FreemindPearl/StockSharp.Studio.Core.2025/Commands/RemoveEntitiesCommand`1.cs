// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RemoveEntitiesCommand`1
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Core.Commands
{
    public class RemoveEntitiesCommand<TEntity> : BaseStudioCommand
    {
        public RemoveEntitiesCommand( IEnumerable<TEntity> entities )
        {
            IEnumerable<TEntity> entities1 = entities;
            if ( entities1 == null )
                throw new ArgumentNullException( nameof( entities ) );
            this.Entities = entities1;           
        }

        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public IEnumerable<TEntity> Entities { get; }
    }
}
