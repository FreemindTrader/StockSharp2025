using StockSharp.Algo.Candles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public class LongDownloadTaskInfo
    {
        public LongDownloadTaskInfo( CandleSeries candleSeries, DateTimeOffset? from, DateTimeOffset? to, int priority )
        {
            CandleSeries = candleSeries;
            From         = from;
            To           = to;
            Priority     = priority;
        }

        public LongDownloadTaskInfo( bool isTick, DateTimeOffset? from, DateTimeOffset? to, int priority )
        {
            CandleSeries = null;
            IsTick       = isTick;
            From         = from;
            To           = to;
            Priority     = priority;
        }

        public int Priority { get; set; }

        public CandleSeries CandleSeries
        {
            get; set;
        }

        public bool ShouldSave { get; set; }

        public bool IsTick { get; set; }


        public DateTimeOffset? From { get; set; }

        public DateTimeOffset? To { get; set; }
    }
}
