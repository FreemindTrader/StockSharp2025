using fx.Charting;
using System;

#pragma warning disable 3001
#pragma warning disable 3003


namespace StockSharp.Studio.Core.Commands
{
    public class ChartResetElementExCommand : BaseStudioCommand
    {
        public ChartResetElementExCommand( IfxChartElement element, object tag = null )
        {
            var chartElement = element;
            if( chartElement == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            Element = chartElement;
            Tag = tag;
        }

        public IfxChartElement Element { get; }

        public object Tag { get; }
    }
}
