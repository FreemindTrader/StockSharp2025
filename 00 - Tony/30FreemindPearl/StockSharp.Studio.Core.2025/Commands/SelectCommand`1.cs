// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.SelectCommand`1
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public class SelectCommand<T> : SelectCommand
    {
        public SelectCommand( T instance, bool canEdit )
          : base( ( object ) instance, canEdit )
        {
        }

        public override Type InstanceType
        {
            get
            {
                return typeof( T );
            }
        }

        public T TypedInstance
        {
            get
            {
                return ( T ) this.Instance;
            }
        }
    }
}
