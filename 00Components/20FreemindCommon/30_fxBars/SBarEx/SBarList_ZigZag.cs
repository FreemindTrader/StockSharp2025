using fx.Bars;
using fx.Collections;
using fx.Definitions;
using fx.TimePeriod;
using StockSharp.Algo.Candles;
using StockSharp.BusinessEntities;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace fx.Bars
{

    public partial class SBarList : IEnumerable<SBar>, IBarList, IDisposable
    {
        public PriceActionEnum PriceAction( int curr, int last )
        {
            ref var currBar = ref this[ curr ];
            ref var lastBar = ref this[ last ];

            if ( currBar.Low < lastBar.Low )
            {
                if ( currBar.High == lastBar.High )
                {
                    return PriceActionEnum.OutsideBarBrokeLow;
                }
                else if ( currBar.High > lastBar.High )
                {
                    return PriceActionEnum.OutsideBar;
                }

                return PriceActionEnum.BreakDown;
            }

            if ( currBar.High > lastBar.High )
            {
                if ( currBar.Low == lastBar.Low )
                {
                    return PriceActionEnum.OutsideBarBrokeHigh;
                }
                else if ( currBar.Low < lastBar.Low )
                {
                    return PriceActionEnum.OutsideBar;
                }

                return PriceActionEnum.BreakUp;
            }

            return PriceActionEnum.InsideBar;
        }
    }
}


