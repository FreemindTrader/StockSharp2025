using Ecng.Common;
using fx.Definitions;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Bars
{
    public enum PriceActionEnum
    {
        BreakDown           = 0,
        BreakUp             = 1,
        InsideBar           = 2,
        OutsideBar          = 3,
        OutsideBarBrokeHigh = 4,
        OutsideBarBrokeLow  = 5,
    }
}
