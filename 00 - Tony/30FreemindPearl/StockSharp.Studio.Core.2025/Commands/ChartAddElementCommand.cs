// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ChartAddElementCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ChartAddElementCommand : BaseStudioCommand
    {
        public ChartAddElementCommand( IChartElement element, object source )
        {
            IChartElement chartElement = element;
            if ( chartElement == null )
                throw new ArgumentNullException( nameof( element ) );
            this.Element = chartElement;
            object obj = source;
            if ( obj == null )
                throw new ArgumentNullException( nameof( source ) );
            this.Source = obj;
            
        }

        public ChartAddElementCommand( IChartElement element, object source, IIndicator indicator )
          : this( element, source )
        {
            IIndicator indicator1 = indicator;
            if ( indicator1 == null )
                throw new ArgumentNullException( nameof( indicator ) );
            this.Indicator = indicator1;
        }

        public IChartElement Element { get; }

        public object Source { get; }

        public IIndicator Indicator { get; }
    }
}
