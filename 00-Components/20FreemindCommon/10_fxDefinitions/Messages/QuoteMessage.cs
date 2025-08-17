using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class QuoteMessage
    {
        public DateTime QuoteTime { get; set; }
        public double Ask { get; }
        public double Bid { get; }
        public Security Security { get; }

        public QuoteMessage( Security sec, DateTime offerTime, double bid, double ask )
        {
            QuoteTime = offerTime;
            Security = sec;
            Ask = ask;
            Bid = bid;
        }
    }
}
