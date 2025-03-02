using fx.Charting;
using StockSharp.Algo.Indicators;
using StockSharp.Studio.Core.Commands;
using System;

#pragma warning disable 3001
#pragma warning disable 3003

namespace StockSharp.Studio.Core.Commands
{
    public class ChartAddElementExCommand : BaseStudioCommand
    {
        public ChartAddElementExCommand( IfxChartElement element, object source )
        {
            IfxChartElement chartElement = element;
            if( chartElement == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            Element = chartElement;
            object obj = source;
            if( obj == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            Source = obj;
        }

        public ChartAddElementExCommand( IfxChartElement element, object source, IIndicator indicator ) : this(
            element,
            source )
        {
            IIndicator indicator1 = indicator;
            if( indicator1 == null )
            {
                throw new ArgumentNullException( nameof( indicator ) );
            }

            Indicator = indicator1;
        }

        public IfxChartElement Element { get; }

        public IIndicator Indicator { get; }

        public object Source { get; }
    }
}
