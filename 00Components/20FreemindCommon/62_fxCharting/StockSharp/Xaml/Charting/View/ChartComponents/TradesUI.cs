using Ecng.Collections;
using StockSharp.Localization;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;

namespace StockSharp.Xaml.Charting
{
    [TypeConverter( typeof( ExpandableObjectConverter ) )]
    
    public class TradesUI : TransactionUI< TradesUI >
    {
        protected override bool OnDraw( ChartDrawDataEx data )
        {
            PooledList< ChartDrawDataEx.sTrade > source = data.GetTrade( this );

            if( source != null && !source.IsEmpty( ) )
            {
                return ( ( IDrawableChartElement )this ).StartDrawing( source.Cast< ChartDrawDataEx.IDrawValue >( ).ToEx( source.Count ) );
            }
            return false;
        }
    }
}
