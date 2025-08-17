using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using fx.Common;

using fx.Algorithm;
using fx.Bars;
using fx.TALib;
using fx.Definitions;
using System.Data;
using fx.Database;


#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        protected Task TonyZigZagTask( bool fullRecalculation, DataBarUpdateType? updateType, int extDepth, int extBackStep, NeoSwingVariables neoV, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => TonyZigZag( fullRecalculation, updateType, extDepth, extBackStep, neoV        ), IndicatorExitToken);

            tasksList.Add( first );

            return first;
        }

        public void TonyZigZag( bool fullRecalculation, DataBarUpdateType? updateType, int zigZagMinSeperation, int extremumMinSeperation, NeoSwingVariables neoV )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            var barB4Calculation  = _barCountBeforeCalculation;

            if ( barB4Calculation - 1 < zigZagMinSeperation ) return;

            if ( updateType == DataBarUpdateType.Initial || updateType == DataBarUpdateType.Reloaded )
            {
                DoInitialZigZagAnalysis( 0 );
            }
            else
            {
                
            }
        }

        public void DoInitialZigZagAnalysis( int selectedIndex )
        {
            var count = Bars.TotalBarCount;

            if( count < 30 )
                return;

            var bars = Bars.MainDataBars;

            var zz = new ZigZag( bars );
            var lastHigh = Bars[ 0 ].High;
            var lasLow   = Bars[ 0 ].Low;

            int startIndex    = selectedIndex - 300 ;
            int endIndex      = selectedIndex;
            int indexCount    = endIndex - startIndex + 1;

            var atr           = new double [ indexCount ];

            var outBeginIdx   = 0;
            var outNBElement  = 0;

            Core.RetCode code = Core.Atr(Bars, startIndex, endIndex, atr, out outBeginIdx, out outNBElement, ATRtimePeriod );

            var avgRange = atr[ outNBElement - 1 ];

            zz.SetAvgLength( avgRange );
            zz.Initialize( 0 );

            for ( int i = selectedIndex + 1; i < count; i++ )
            {
                var priceAction = bars.PriceAction( i, i - 1 );

                switch ( priceAction )
                {
                    case PriceActionEnum.BreakDown:
                    {
                        if ( bars[i].IsRising )
                        {
                            zz.BreakDownAndRetrace( i, bars[ i ].Low, bars[ i ].High );
                        }
                        else
                        {
                            zz.BreakDownAction( i, bars[ i ].Low );
                        }
                    }
                    break;

                    case PriceActionEnum.BreakUp:
                    {
                        zz.BreakUpAction( i, bars[i].High );
                    }
                    break;

                    case PriceActionEnum.InsideBar:
                    {
                        zz.InsideBarAction( i );
                    }
                    break;

                    case PriceActionEnum.OutsideBar:
                    {
                        if ( bars[ i ].IsRising )
                        {
                            zz.OutsideBarRising( i );
                        }
                        else
                        {
                            zz.OutsideBarFalling( i );
                        }
                    }
                    break;

                    case PriceActionEnum.OutsideBarBrokeHigh:
                    {
                        if ( bars[ i ].IsRising )
                        {
                            zz.BreakUpAction( i, bars[ i ].High );
                        }
                        else
                        {
                            zz.BreakDownAction( i, bars[ i ].Low );
                        }
                    }
                    break;

                    case PriceActionEnum.OutsideBarBrokeLow:
                    {
                        
                    }    
                    break;
                }
            }
        }
    }
}
