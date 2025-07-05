using Ecng.Collections;
using StockSharp.Localization;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;

namespace fx.Charting
{
    [TypeConverter( typeof( ExpandableObjectConverter ) )]
    
    public class TradesUI : TransactionUI< TradesUI >
    {
        protected override bool OnDraw( ChartDrawData data )
        {
            PooledList< ChartDrawData.sTrade > source = data.GetTrade( this );

            if( source != null && !source.IsEmpty( ) )
            {
                return ( ( IDrawableChartElement )this ).StartDrawing( source.Cast< ChartDrawData.IDrawValue >( ).ToEx( source.Count ) );
            }
            return false;
        }
    }
}
