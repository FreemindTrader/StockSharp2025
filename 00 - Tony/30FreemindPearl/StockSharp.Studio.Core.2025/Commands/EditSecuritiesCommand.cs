// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.EditSecuritiesCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Core.Commands
{
    public class EditSecuritiesCommand : BaseStudioCommand
    {
        public EditSecuritiesCommand( Security security )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            this.Securities = ( IEnumerable<Security> ) new Security [1]
            {
        security
            };
        }

        public EditSecuritiesCommand( IEnumerable<Security> securities )
        {
            IEnumerable<Security> securities1 = securities;
            if ( securities1 == null )
                throw new ArgumentNullException( nameof( securities ) );
            this.Securities = securities1;
        }

        public IEnumerable<Security> Securities { get; }
    }
}
