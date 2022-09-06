// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.FirstInitPortfoliosCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

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
            if ( this.Portfolios.IsEmpty<Portfolio>() )
                throw new ArgumentException( nameof( portfolios ) );
        }

        public IEnumerable<Portfolio> Portfolios { get; }
    }
}
