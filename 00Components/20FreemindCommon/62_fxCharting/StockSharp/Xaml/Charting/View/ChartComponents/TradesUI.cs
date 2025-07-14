using Ecng.Collections;
using StockSharp.Localization;
using System.Collections.Generic; 
using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System;

namespace StockSharp.Xaml.Charting
{
    [TypeConverter( typeof( ExpandableObjectConverter ) )]
    
    public class TradesUI : TransactionUI< TradesUI >
    {
        protected override bool OnDraw( ChartDrawData data )
        {
            throw new NotImplementedException();

            //PooledList< ChartDrawData.sTrade > source = data.Ge

            //if( source != null && !source.IsEmpty( ) )
            //{
            //    return ( ( IDrawableChartElement )this ).StartDrawing( source.Cast< ChartDrawData.IDrawValue >( ).ToEx( source.Count ) );
            //}
            //return false;
        }
    }
}
