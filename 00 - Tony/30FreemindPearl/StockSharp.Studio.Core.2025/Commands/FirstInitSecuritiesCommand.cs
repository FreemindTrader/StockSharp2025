// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.FirstInitSecuritiesCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Collections;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Core.Commands
{
    public class FirstInitSecuritiesCommand : BaseStudioCommand
    {
        public FirstInitSecuritiesCommand( IEnumerable<Security> securities )
        {
            IEnumerable<Security> securities1 = securities;
            if ( securities1 == null )
                throw new ArgumentNullException( nameof( securities ) );
            this.Securities = securities1;
            if ( CollectionHelper.IsEmpty<Security>(  this.Securities ) )
                throw new ArgumentException( nameof( securities ) );
        }

        public IEnumerable<Security> Securities { get; }
    }
}
