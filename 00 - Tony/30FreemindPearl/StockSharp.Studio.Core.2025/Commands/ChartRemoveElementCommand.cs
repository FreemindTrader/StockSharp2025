// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ChartRemoveElementCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.Charting;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ChartRemoveElementCommand : BaseStudioCommand
    {
        public ChartRemoveElementCommand( IChartElement element, object source )
        {
            object obj = source;
            if ( obj == null )
                throw new ArgumentNullException( nameof( source ) );
            this.Source = obj;
            IChartElement chartElement = element;
            if ( chartElement == null )
                throw new ArgumentNullException( nameof( element ) );
            this.Element = chartElement;
           
        }

        public object Source { get; }

        public IChartElement Element { get; }
    }
}
