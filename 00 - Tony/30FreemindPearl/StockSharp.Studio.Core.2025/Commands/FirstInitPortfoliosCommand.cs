// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.FirstInitPortfoliosCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Collections;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Core.Commands
{
    public class FirstInitPortfoliosCommand : BaseStudioCommand
    {
        public FirstInitPortfoliosCommand( IEnumerable<Portfolio> portfolios )
        {
            IEnumerable<Portfolio> portfolios1 = portfolios;
            if ( portfolios1 == null )
                throw new ArgumentNullException( nameof( portfolios ) );
            this.Portfolios = portfolios1;
            if ( CollectionHelper.IsEmpty<Portfolio>(  this.Portfolios ) )
                throw new ArgumentException( nameof( portfolios ) );
        }

        public IEnumerable<Portfolio> Portfolios { get; }
    }
}
