using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public struct AskBidBar
    {
        public DateTime BarTime;
        public double Ask;
        public double Bid;

        public AskBidBar( DateTime time, double ask, double bid )
        {            
            BarTime = DateTime.SpecifyKind( time, DateTimeKind.Utc );
            Ask = ask;
            Bid = bid;
        }

        public decimal Price => (decimal) ( Ask + Bid ) / 2;
    }
}
