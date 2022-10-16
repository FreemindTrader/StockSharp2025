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

    public class NullBarMessage
    {
        public DateTime QuoteTime { get; set; }
        public double Open { get; }
        public double Close { get; }

        public double High { get; }

        public double Low { get; }

        public double Volume { get; }

        public string Security { get; }

        public NullBarMessage( DateTime quoteTime, double open, double close, double high, double low, double volume, string security )
        {
            QuoteTime = quoteTime;
            Open = open;
            Close = close;
            High = high;
            Low = low;
            Volume = volume;
            Security = security;
        }
    }
}
