using Ecng.Collections;
using StockSharp.Localization;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows.Media;
using System;

namespace StockSharp.Xaml.Charting
{
    
    public class OrdersUI : TransactionUI< OrdersUI >
    {
        public OrdersUI( )
        {
            SellColor = Colors.Transparent;
            BuyColor  = Colors.Transparent;
        }

        protected override bool OnDraw( ChartDrawData data )
        {
            throw new NotImplementedException();
            //PooledList< ChartDrawData.sTrade > source = data.GetOrder( this );
            //if( source != null && !source.IsEmpty( ) )
            //{
            //    return ( ( IChartElementUiDomain )this ).StartDrawing( source.Cast< ChartDrawData.IDrawValue >( ).ToEx( source.Count ) );
            //}
            //return false;
        }
    }
}
