// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ChartResetElementCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.Charting;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ChartResetElementCommand : BaseStudioCommand
    {
        public ChartResetElementCommand( IChartElement element, object tag = null )
        {
            IChartElement chartElement = element;
            if ( chartElement == null )
                throw new ArgumentNullException( nameof( element ) );
            this.Element = chartElement;
            this.Tag = tag;
           
        }

        public IChartElement Element { get; }

        public object Tag { get; }
    }
}
