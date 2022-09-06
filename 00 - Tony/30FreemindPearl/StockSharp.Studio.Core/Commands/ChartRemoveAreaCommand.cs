// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ChartRemoveAreaCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Charting;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ChartRemoveAreaCommand : BaseStudioCommand
    {
        public IChartArea Area { get; }

        public ChartRemoveAreaCommand( IChartArea area )
        {
            IChartArea chartArea = area;
            if ( chartArea == null )
                throw new ArgumentNullException( nameof( area ) );
            this.Area = chartArea;
        }
    }
}
