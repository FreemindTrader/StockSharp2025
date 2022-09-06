using fx.Collections;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class WavePredictionMessage
    {
        public Security Symbol { get; set; }

        public long BarTime { get; set; }

        public WavePredictionMessage( Security symbol )
        {
            this.Symbol = symbol;
            BarTime = 0;
        }

        public WavePredictionMessage( Security symbol, long barTime )
        {
            this.Symbol = symbol;
            BarTime = barTime;
        }
    }
}
