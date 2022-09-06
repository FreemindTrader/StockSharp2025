using fx.Charting;
using StockSharp.Studio.Core.Commands;
using System;

#pragma warning disable 3001
#pragma warning disable 3003


namespace StockSharp.Studio.Core.Commands
{
    public class ChartRemoveElementExCommand : BaseStudioCommand
    {
        public ChartRemoveElementExCommand( IfxChartElement element, object source )
        {
            var chartElement = element;
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

        public IfxChartElement Element { get; }

        public object Source { get; }
    }
}
