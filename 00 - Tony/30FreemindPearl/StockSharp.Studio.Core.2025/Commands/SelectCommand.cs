// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.SelectCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public abstract class SelectCommand : BaseStudioCommand
    {
        protected SelectCommand( object instance, bool canEdit )
        {
            this.Instance = instance;
            this.CanEdit = canEdit;           
        }

        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public virtual Type InstanceType
        {
            get
            {
                Type type = this.Instance?.GetType();
                if ( ( object ) type != null )
                    return type;
                return typeof( object );
            }
        }

        public object Instance { get; }

        public bool CanEdit { get; }
    }
}
