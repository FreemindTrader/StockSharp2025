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
using fx.TALib;
using fx.Definitions; 
using System.Data;


#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        protected Task BasicGannZigZagTask( bool fullRecalculation, DataBarUpdateType? updateType, int numberOfBarsForReversal, GannSwingVariables gann, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicGannZigZagTask" );            

            Task first = new Task(() => CalculateGannSwing( fullRecalculation, updateType, numberOfBarsForReversal, gann ), IndicatorExitToken);

            tasksList.Add( first );            

            return first;
        }

        protected int GannSwingLookBack()
        {
            return 0;
        }

        protected void CalculateGannSwing( bool fullRecalculation, DataBarUpdateType? updateType, int numberOfBarsForReversal, GannSwingVariables g )
	    {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            if ( ( updateType == DataBarUpdateType.Initial ) || ( updateType == DataBarUpdateType.HistoryUpdate ) || ( updateType == DataBarUpdateType.Reloaded ) || updateType == DataBarUpdateType.NewPeriod )
            {
                var barB4Calculation = _barCountBeforeCalculation;

	            int nthHigherHighOrLowerLowForTrendChange = numberOfBarsForReversal;

                TASignal old;

                //bool endCyklDirection = true;
                //bool endSearchPattern = false;

                // Variables for Gann Swing
                /// <image url="$(SolutionDir)\..\..\30 - CommonImages\ReferenceBar.jpg" />
                // Since the inside bar is ignored, the analyst must look at the preceding bar to determine if the trend line should
                // be moved up or down. This bar is known as the last active bar or the reference bar.
                string zigZag           = "ZigZagS" + numberOfBarsForReversal.ToString( "D1" );
                string zigZagHigh       = zigZag + "High";
                string zigZagLow        = zigZag + "Low";

                double referenceBarLow  = 0;
                double referenceBarHigh = 0;
                int outsideBarCount     = 0;                // counter external bars
                int lowerLowBarCount    = 0;
                int higerHighBarCount   = 0;
                
                int repoStartingIndex   = 0;
                int resultSetLength     = IndicatorResult[ zigZag ].Count;
                int startIndex          = 0;
                int endIndex            = 0;
                int indexCount          = 0;
                int lookback            = 0;

                var gannSwingMinorTrend = new ThreadSafeDictionary< long, TASignal>( );
                

                g._trendDirection = TrendDirection.NoTrend;
                g._timeOfLastCheckedBar   = long.MinValue;

                if ( fullRecalculation == false )
                {
                    lookback          = GannSwingLookBack( );

                    repoStartingIndex = Math.Max( 0, resultSetLength - lookback - 2 );
                }

                if ( fullRecalculation )
                {
                    startIndex        = 0;
                    g.ResetVariablesSnapshot();

                    RemoveAllGannPeakAndTroughsFromBars( );
                }
                else
                {
                    startIndex = Math.Max( 0, g._lastGannSwingExtremumIndex );
                    g.RestoreVariablesSnapshot();                    
                }

                //endIndex                = barB4Calculation - repoStartingIndex - 1;
                //indexCount              = endIndex - startIndex + 1;

                endIndex                = barB4Calculation;
                indexCount              = endIndex - startIndex;

                var ZigZagsBoth         = new double[ indexCount ];
                var ZigZagsHigh         = new double[ indexCount ];
                var ZigZagsLow          = new double[ indexCount ];

                

                for ( int i = startIndex; i < endIndex; i++ )
                {
                    if ( Bars.Period == TimeSpan.FromMinutes( 5 ) && i == 8062 )
                    {

                    }

                    if ( g._lastLow == 0 )
                    {
                        g._lastLow             = Bars[ i ].Low;
                        g._lastHigh            = Bars[i].High;
                        g._lastHigherHighIndex = i;
                        g._lastLowerLowIndex   = i;
                    }

                    if ( g._timeOfLastCheckedBar != Bars[ i ].LinuxTime )
                    {
                        g._timeOfLastCheckedBar = Bars[ i ].LinuxTime;

                        // --------------------------------------------------------------------------------------------------------
                        //
                        // when the referenceBarLow and referenceBarHigh are both zero, that will mean we just start the whole
                        // processing.
                        //
                        // --------------------------------------------------------------------------------------------------------
                        if ( referenceBarLow == 0 && referenceBarHigh == 0 )
                        {
                            // From the book.
                            // The Outside Bar
                            // The order of occurrence of the high and low on an outside move day is critical and should be noted.
                            //  If the minor trend line is moving up 
                            //      First move of the outside move day is to the high, the minor trend line moves up to the high, then down to the low. 
                            //      The intermediate and main trend charts will read this move a 1-bar up.
                            //If the minor trend line is moving up
                            //      First move of the outside move day is to the low then up to the high, the minor trend line moves down to the low then to the high. 
                            //      This action moves the intermediate and main trend indicator line up while the down move is ignored.
                            //If the minor trend line is moving down.
                            //      The first move of the outside move day is to the high, the minor trend line moves to the high, then to the low. 
                            //      The intermediate and main trend indicator charts treat this action as a one-day move up.
                            //If the minor trend line is moving down
                            //      The first move is to the low, the minor trend line moves to the low, then to the high. 
                            //      The intermediate and main trend indicator charts follow the move down to the low.
                            //
                            // This can be realized in the real time. But when it comes down to history, a "loose interpretation" starts. 
                            // So, swings built in the real time can differ from those built on the same period of time, but after it has become history.
                            //
                            // To avoid any ambiguousness, the trend will continue the trend that had prevailed before the external bar came.
                            //
                            // However, since the trend must change on the external bar, according to the description, let us consider for the intermediate, main trend and for trends of higher orders 
                            // the external bar as a bar, from which the count of bars starts to detect a possible further trend. 
                            //
                            if ( g._lastLow > Bars[ i ].Low && g._lastHigh < Bars[i].High )               // current bar is an outside bar
                            {                                
                                g._lastLow    = Bars[ i ].Low;
                                g._lastHigh   = Bars[i].High;
                                referenceBarLow  = Bars[ i ].Low;
                                referenceBarHigh = Bars[i].High;
                                outsideBarCount++;

                                if ( g._trendDirection == TrendDirection.Uptrend )
                                {

                                    // This external bar is the bar from which the count of bars starts to detect a possible further trend
                                    lowerLowBarCount        = outsideBarCount;
                                    g._lastHigherHighIndex    = i;                                    
                                }
                                else if ( g._trendDirection   == TrendDirection.DownTrend )
                                {
                                    higerHighBarCount       = outsideBarCount;
                                    g._lastLowerLowIndex      = i;                                    
                                }
                                else
                                {
                                    lowerLowBarCount++;
                                    higerHighBarCount++;
                                }
                            }
                            else if ( g._lastLow <= Bars[ i ].Low && g._lastHigh < Bars[ i ].High )             // current bar is rising                                
                            {
                                referenceBarLow             = 0;
                                referenceBarHigh            = Bars[i].High;
                                lowerLowBarCount            = 0;
                                outsideBarCount             = 0;

                                if ( g._trendDirection != TrendDirection.Uptrend )
                                {
                                    // This higher high bar count is important because we need this to determine if the n bar counts has been archieved.
                                    // minor trend          = 1 bar  breaking higher high from a low price
                                    // Intermediate trend   = 2 bars breaking higher high from a low price
                                    // Main trend           = 3 bars breaking higher high from a low price 
                                    higerHighBarCount++;
                                }
                                else
                                {
                                    // We have another HIGHER HIGH in an uptrend. Let's update the last HIGHER HIGH and its index.
                                    g._lastLow                = Bars[ i ].Low;
                                    g._lastHigh               = Bars[i].High;
                                    referenceBarLow              = 0;
                                    referenceBarHigh             = 0;
                                    g._lastHigherHighIndex    = i;                                    
                                }
                            }
                            else if ( g._lastLow > Bars[ i ].Low && g._lastHigh >= Bars[ i ].High )             // current bar is decending
                                
                            {
                                referenceBarLow             = Bars[ i ].Low;
                                referenceBarHigh            = 0;
                                higerHighBarCount           = 0;
                                outsideBarCount             = 0;

                                if ( g._trendDirection != TrendDirection.DownTrend )
                                {
                                    // This lower low bar count is important because we need this to determine if the n bar counts has been archieved.
                                    lowerLowBarCount++;
                                }
                                else
                                {
                                    // We have another LOWER LOW during the downtrend. Let's update the last low and its index.
                                    g._lastLow                = Bars[ i ].Low;
                                    g._lastHigh               = Bars[i].High;
                                    referenceBarLow         = 0;
                                    referenceBarHigh        = 0;
                                    g._lastLowerLowIndex      = i;                                    
                                }
                            }
                        }
                        // --------------------------------------------------------------------------------------------------------
                        //
                        // when both referenceBarLow and referenceBarHigh have value > 0, that means the reference bar is an
                        // Outside Bar
                        //
                        // --------------------------------------------------------------------------------------------------------
                        else if ( referenceBarLow > 0 && referenceBarHigh > 0 )                         // Previous bar is an Outside bar
                        {
                            if ( referenceBarLow > Bars[ i ].Low && referenceBarHigh < Bars[i].High ) // Current bar is also an outside bar
                            {
                                g._lastLow                  = Bars[ i ].Low;
                                g._lastHigh                 = Bars[i].High;
                                referenceBarLow           = Bars[ i ].Low;
                                referenceBarHigh          = Bars[i].High;
                                outsideBarCount++;

                                if ( g._trendDirection == TrendDirection.Uptrend )
                                {
                                    lowerLowBarCount      = outsideBarCount;
                                    g._lastHigherHighIndex  = i;                                    
                                }
                                else if ( g._trendDirection == TrendDirection.DownTrend )
                                {
                                    higerHighBarCount     = outsideBarCount;
                                    g._lastLowerLowIndex    = i;                                    
                                }
                                else
                                {
                                    lowerLowBarCount++;
                                    higerHighBarCount++;
                                }
                            }
                            else if ( referenceBarLow <= Bars[ i ].Low && referenceBarHigh < Bars[i].High )                               
                            {
                                referenceBarLow           = 0;
                                referenceBarHigh          = Bars[i].High;
                                lowerLowBarCount          = 0;
                                outsideBarCount           = 0;

                                if ( g._trendDirection != TrendDirection.Uptrend ) 
                                {
                                    // Increase the HIGHER HIGH count so the n-bar reversal can be tracked.
                                    higerHighBarCount++;    
                                }
                                else
                                {
                                    // We have another HIGHER HIGH in an uptrend. Let's update the last HIGHER HIGH and its index.
                                    g._lastLow              = Bars[ i ].Low;
                                    g._lastHigh             = Bars[i].High;
                                    referenceBarLow       = 0;
                                    referenceBarHigh      = 0;
                                    g._lastHigherHighIndex  = i;                                    
                                }
                            }
                            else if ( referenceBarLow > Bars[ i ].Low && referenceBarHigh >= Bars[i].High )           // The current bar is making a LOWER LOW                           
                            {
                                referenceBarLow           = Bars[ i ].Low;
                                referenceBarHigh          = 0;
                                higerHighBarCount         = 0;
                                outsideBarCount           = 0;

                                if ( g._trendDirection != TrendDirection.DownTrend )
                                {
                                    // Increase the lower Low count so the n-bar reversal can be tracked.
                                    lowerLowBarCount++;
                                }
                                else
                                {
                                    // We have another LOWER LOW during the downtrend. Let's update the last low and its index.
                                    g._lastLow              = Bars[ i ].Low;
                                    g._lastHigh             = Bars[i].High;
                                    referenceBarLow       = 0;
                                    referenceBarHigh      = 0;
                                    g._lastLowerLowIndex    = i;                                    
                                }
                            }
                        }
                        // --------------------------------------------------------------------------------------------------------
                        //
                        // when only referenceBarLow > 0, that mean the current trend is an DOWN TREND                         
                        //
                        // --------------------------------------------------------------------------------------------------------
                        else if ( referenceBarLow > 0 )
                        {
                            if ( referenceBarLow > Bars[ i ].Low && g._lastHigh < Bars[i].High ) // outside bar
                            {
                                g._lastLow                 = Bars[ i ].Low;
                                g._lastHigh                = Bars[i].High;
                                referenceBarLow          = Bars[ i ].Low;
                                referenceBarHigh         = Bars[i].High;
                                outsideBarCount++;

                                if ( g._trendDirection == TrendDirection.Uptrend )
                                {
                                    lowerLowBarCount     = outsideBarCount;
                                    g._lastHigherHighIndex = i;                                    
                                }
                                else if ( g._trendDirection == TrendDirection.DownTrend )
                                {
                                    higerHighBarCount    = outsideBarCount;
                                    g._lastLowerLowIndex   = i;                                    
                                }
                                else
                                {
                                    lowerLowBarCount++;
                                    higerHighBarCount++;
                                }
                            }
                            else if ( referenceBarLow <= Bars[ i ].Low && g._lastHigh < Bars[i].High )              // The current bar is making a HIGHER HIGH                            
                            {
                                referenceBarLow          = 0;
                                referenceBarHigh         = Bars[i].High;
                                lowerLowBarCount         = 0;
                                outsideBarCount          = 0;

                                if ( g._trendDirection != TrendDirection.Uptrend )
                                {
                                    // If not at uptrend, we need to keep track of the higher high count to see if reversal has taken place.
                                    higerHighBarCount++;
                                }
                                else
                                {
                                    // We have another HIGHER HIGH in an uptrend. Let's update the last HIGHER HIGH and its index.
                                    g._lastLow             = Bars[ i ].Low;
                                    g._lastHigh            = Bars[i].High;
                                    referenceBarLow      = 0;
                                    referenceBarHigh     = 0;
                                    g._lastHigherHighIndex = i;                                    
                                }
                            }
                            else if ( referenceBarLow > Bars[ i ].Low && g._lastHigh >= Bars[i].High )              // The current bar is making a LOWER LOW                              
                            {
                                referenceBarLow          = Bars[ i ].Low;
                                referenceBarHigh         = 0;
                                higerHighBarCount        = 0;
                                outsideBarCount          = 0;

                                if ( g._trendDirection != TrendDirection.DownTrend )
                                {
                                    // If not at downtrend, we need to keep track of the LOWER LOW count to see if reversal has taken place.
                                    lowerLowBarCount++;
                                }
                                else
                                {
                                    // We have another LOWER LOW during the downtrend. Let's update the last low and its index.
                                    g._lastLow             = Bars[ i ].Low;
                                    g._lastHigh            = Bars[i].High;
                                    referenceBarLow      = 0;
                                    referenceBarHigh     = 0;
                                    g._lastLowerLowIndex   = i;                                    
                                }
                            }
                        }
                        // --------------------------------------------------------------------------------------------------------
                        //
                        // when only referenceBarHigh > 0, that mean the current trend is an UP TREND                         
                        //
                        // --------------------------------------------------------------------------------------------------------
                        else if ( referenceBarHigh > 0 )
                        {
                            if ( g._lastLow > Bars[ i ].Low && referenceBarHigh < Bars[i].High ) // outside bar
                            {
                                g._lastLow                 = Bars[ i ].Low;
                                g._lastHigh                = Bars[i].High;
                                referenceBarLow          = Bars[ i ].Low;
                                referenceBarHigh         = Bars[i].High;
                                outsideBarCount++;

                                if ( g._trendDirection == TrendDirection.Uptrend )
                                {                                    
                                    lowerLowBarCount     = outsideBarCount;
                                    g._lastHigherHighIndex = i;                                    
                                }
                                else if ( g._trendDirection == TrendDirection.DownTrend )
                                {
                                    higerHighBarCount    = outsideBarCount;
                                    g._lastLowerLowIndex   = i;                                    
                                }
                                else
                                {
                                    lowerLowBarCount++;
                                    higerHighBarCount++;
                                }
                            }
                            else if ( g._lastLow <= Bars[ i ].Low && referenceBarHigh < Bars[i].High )              // The current bar is making a HIGHER HIGH                                                        
                            {
                                referenceBarLow          = 0;
                                referenceBarHigh         = Bars[i].High;
                                lowerLowBarCount         = 0;
                                outsideBarCount          = 0;

                                if ( g._trendDirection != TrendDirection.Uptrend )
                                {
                                    // If not at uptrend, we need to keep track of the higher high count to see if reversal has taken place.
                                    higerHighBarCount++;
                                }
                                else
                                {
                                    // We have another HIGHER HIGH in an uptrend. Let's update the last HIGHER HIGH and its index.
                                    g._lastLow             = Bars[ i ].Low;
                                    g._lastHigh            = Bars[i].High;
                                    referenceBarLow      = 0;
                                    referenceBarHigh     = 0;
                                    g._lastHigherHighIndex = i;                                    
                                }
                            }
                            else if ( g._lastLow > Bars[ i ].Low && referenceBarHigh >= Bars[i].High )              // The current bar is making a LOWER LOW                                                                            
                            {
                                referenceBarLow          = Bars[ i ].Low;
                                referenceBarHigh         = 0;
                                higerHighBarCount        = 0;
                                outsideBarCount          = 0;

                                if ( g._trendDirection != TrendDirection.DownTrend )
                                {
                                    // If not at downtrend, we need to keep track of the LOWER LOW count to see if reversal has taken place.
                                    lowerLowBarCount++;
                                }
                                else
                                {
                                    // We have another LOWER LOW during the downtrend. Let's update the last low and its index.
                                    g._lastLow             = Bars[ i ].Low;
                                    g._lastHigh            = Bars[i].High;
                                    referenceBarLow      = 0;
                                    referenceBarHigh     = 0;
                                    g._lastLowerLowIndex   = i;                                    
                                }
                            }
                        }

                        // --------------------------------------------------------------------------------------------------------
                        //
                        //  This is the second part of the logic.
                        //      Here we start to detect if there is enough bar to change the existing trend.
                        //      1) First check when there is no pre-existing trend.
                        //      2) Then check if there is enough LOWER LOW to change an existing UP TREND
                        //      3) Then check if there is enough HIGHER HIGH to change an existing DOWN TREND
                        // --------------------------------------------------------------------------------------------------------
                        // Determine the direction of the trend.
                        if ( g._trendDirection == TrendDirection.NoTrend )
                        {
                            if ( g._lastLow < referenceBarLow && g._lastHigh > referenceBarHigh ) // inside bar
                            {
                                g._lastLow               = Bars[ i ].Low;
                                g._lastHigh              = Bars[i].High;
                                g._lastHigherHighIndex   = i;
                                g._lastLowerLowIndex     = i;
                                lowerLowBarCount       = 0;
                                higerHighBarCount      = 0;
                                outsideBarCount        = 0;
                            }

                            if (    higerHighBarCount > lowerLowBarCount && 
                                    higerHighBarCount > outsideBarCount &&
                                    higerHighBarCount > nthHigherHighOrLowerLowForTrendChange )
                            {
                                g._lastLow               = Bars[ i ].Low;
                                g._lastHigh              = Bars[i].High;
                                referenceBarLow        = 0;
                                referenceBarHigh       = 0;
                                g._trendDirection        = TrendDirection.Uptrend;
                                higerHighBarCount      = 0;
                                lowerLowBarCount       = 0;
                                outsideBarCount        = 0;

                                gannSwingMinorTrend.TryGetValueAndAddOrReplace( Bars[ g._lastLowerLowIndex ].LinuxTime, TASignal.GANN_TROUGH, out old );                                

                                if ( g._lastLowerLowIndex >= startIndex )
                                {
                                    int index2            = g._lastLowerLowIndex - startIndex;
                                    var lowValue          = Bars[ g._lastLowerLowIndex ].Low;

                                    ZigZagsBoth[ index2 ] = lowValue;
                                    ZigZagsLow[ index2 ]  = lowValue;
                                    ZigZagsHigh[ index2 ] = 0;    
                                }
                                else
                                {
                                    this.LogError( "Gann Swing array Error _indexOfLastLowerLowBar < startIndex, _indexOfLastLowerLowBar = " + g._lastLowerLowIndex + ", startIndex = " + startIndex );

                                    CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                                }
                                
                                g._lastHigherHighIndex = i;                                
                                g._lastGannSwingExtremumIndex = g._lastLowerLowIndex;

                                if ( g._lastGannSwingExtremumIndex > -1 )
                                {
                                    g.StoreVariablesSnapshot( Bars[ g._lastGannSwingExtremumIndex ].Low, Bars[ g._lastGannSwingExtremumIndex ].High );
                                }

                                
                            }
                            else if (   lowerLowBarCount > higerHighBarCount && 
                                        lowerLowBarCount > outsideBarCount &&
                                        lowerLowBarCount > nthHigherHighOrLowerLowForTrendChange )
                            {
                                g._lastLow                                              = Bars[ i ].Low;
                                g._lastHigh                                             = Bars[i].High;
                                referenceBarLow                                       = 0;
                                referenceBarHigh                                      = 0;
                                g._trendDirection                                       = TrendDirection.DownTrend;
                                lowerLowBarCount                                      = 0;
                                higerHighBarCount                                     = 0;
                                outsideBarCount                                       = 0;
                                gannSwingMinorTrend.TryGetValueAndAddOrReplace( Bars[ g._lastHigherHighIndex ].LinuxTime, TASignal.GANN_PEAK, out old );
                                

                                if ( g._lastHigherHighIndex >= startIndex )
                                {
                                    var index = g._lastHigherHighIndex - startIndex;
                                    var newValue = Bars[ g._lastHigherHighIndex ].High;

                                    ZigZagsBoth[ index ] = newValue;
                                    ZigZagsHigh[ index ] = newValue;
                                    ZigZagsLow[ index ] = 0;                                   
                                }
                                else
                                {
                                    this.LogError( "Gann Swing array Error _indexOfLastHigherHighBar < startIndex, _indexOfLastHigherHighBar = " + g._lastHigherHighIndex + ", startIndex = " + startIndex );

                                    CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                                }
                                
                                g._lastLowerLowIndex                               = i;                                
                                g._lastGannSwingExtremumIndex                              = g._lastHigherHighIndex;

                                if ( g._lastGannSwingExtremumIndex > -1 )
                                {
                                    g.StoreVariablesSnapshot( Bars[ g._lastGannSwingExtremumIndex ].Low, Bars[ g._lastGannSwingExtremumIndex ].High );
                                }

                                
                            }
                        }
                        else
                        {
                            if ( referenceBarLow == 0 && referenceBarHigh == 0 )
                            {
                                lowerLowBarCount  = 0;
                                higerHighBarCount = 0;
                                outsideBarCount   = 0;
                            }

                            // --------------------------------------------------------------------------------------------------------
                            //
                            // Current trend is UPTREND. We want to find out if we have enough lower low to qualify the reversal
                            //
                            // --------------------------------------------------------------------------------------------------------
                            if ( g._trendDirection == TrendDirection.Uptrend )
                            {
                                if (    lowerLowBarCount > higerHighBarCount && 
                                        lowerLowBarCount > outsideBarCount &&
                                        lowerLowBarCount > nthHigherHighOrLowerLowForTrendChange ) // Determine the point of changing trends.
                                {
                                    // remember the importance of the trend of the previous bar _trendDirection
                                    
                                    g._trendDirection  = TrendDirection.DownTrend;
                                    lowerLowBarCount = 0;

                                    gannSwingMinorTrend.TryGetValueAndAddOrReplace( Bars[ g._lastHigherHighIndex ].LinuxTime, TASignal.GANN_PEAK, out old );
                                    

                                    if ( g._lastHigherHighIndex >= startIndex )
                                    {
                                        var index = g._lastHigherHighIndex - startIndex;
                                        var newValue = Bars[ g._lastHigherHighIndex ].High;

                                        ZigZagsBoth[ index ] = newValue;
                                        ZigZagsHigh[ index ] = newValue;
                                        ZigZagsLow[ index ]  = 0;    
                                    }
                                    else
                                    {
                                        this.LogError( "Gann Swing array Error _indexOfLastHigherHighBar < startIndex, _indexOfLastHigherHighBar = " + g._lastHigherHighIndex + ", startIndex = " + startIndex );

                                        CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                                    }
                                    
                                    g._lastGannSwingExtremumIndex = g._lastHigherHighIndex;
                                    g._lastLowerLowIndex = i;
                                    g._lastLow           = Bars[ i ].Low;
                                    g._lastHigh          = Bars[i].High;
                                    referenceBarLow    = 0;
                                    referenceBarHigh   = 0;

                                    for ( int n = 0; outsideBarCount < nthHigherHighOrLowerLowForTrendChange; n++ )
                                    {
                                        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\2OutsideBars4.jpg" />
                                        
                                        if ( g._lastLow < Bars[ i - n - 1 ].Low && g._lastHigh > Bars[ i - n - 1 ].High )
                                        {
                                            outsideBarCount++;
                                            higerHighBarCount++;

                                            g._lastLow         = Bars[ i - n - 1 ].Low;
                                            g._lastHigh        = Bars[ i - n - 1 ].High;
                                            referenceBarHigh = Bars[i].High;
                                        }
                                        else break;
                                    }

                                    g._lastLow  = Bars[ i ].Low;
                                    g._lastHigh = Bars[i].High;


                                    if ( g._lastGannSwingExtremumIndex > -1 )
                                    {
                                        g.StoreVariablesSnapshot( Bars[ g._lastGannSwingExtremumIndex ].Low, Bars[ g._lastGannSwingExtremumIndex ].High );
                                    }

                                    
                                }
                            }

                            // --------------------------------------------------------------------------------------------------------
                            //
                            // Current trend is down trend. Let's find if we have fulfill the n-bar reversal by having enough
                            // Higher high
                            //
                            // --------------------------------------------------------------------------------------------------------
                            if ( g._trendDirection == TrendDirection.DownTrend )
                            {
                                if ( higerHighBarCount > lowerLowBarCount && 
                                     higerHighBarCount > outsideBarCount &&
                                     higerHighBarCount > nthHigherHighOrLowerLowForTrendChange ) // Determine the point of changing trends.
                                {
                                    // remember the importance of the trend of the previous bar gann._trendDirection                                    
                                    g._trendDirection   = TrendDirection.Uptrend;
                                    higerHighBarCount = 0;

                                    gannSwingMinorTrend.TryGetValueAndAddOrReplace( Bars[ g._lastLowerLowIndex ].LinuxTime, TASignal.GANN_TROUGH, out old );
                                    

                                    if ( g._lastLowerLowIndex >= startIndex )
                                    {
                                        var index = g._lastLowerLowIndex - startIndex;
                                        var newValue = Bars[ g._lastLowerLowIndex ].Low;

                                        ZigZagsBoth[ index ] = newValue;
                                        ZigZagsLow[ index ]  = newValue;
                                        ZigZagsHigh[ index ] = 0;    
                                    }
                                    else
                                    {
                                        this.LogError( "Gann Swing array Error _indexOfLastLowerLowBar < startIndex, _indexOfLastLowerLowBar = " + g._lastLowerLowIndex + ", startIndex = " + startIndex );

                                        CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                                    }
                                    
                                    g._lastGannSwingExtremumIndex   = g._lastLowerLowIndex;                                    
                                    g._lastHigherHighIndex = i;                                                                        
                                    g._lastLow             = Bars[ i ].Low;
                                    g._lastHigh            = Bars[i].High;
                                    referenceBarLow      = 0;
                                    referenceBarHigh     = 0;

                                    for ( int n = 0; outsideBarCount < nthHigherHighOrLowerLowForTrendChange; n++ )
                                    {
                                        if ( g._lastLow < Bars[ i - n - 1 ].Low && g._lastHigh > Bars[ i - n - 1 ].High )
                                        {
                                            outsideBarCount++;
                                            lowerLowBarCount++;

                                            g._lastLow        = Bars[ i - n - 1 ].Low;
                                            g._lastHigh       = Bars[ i - n - 1 ].High;
                                            
                                            referenceBarLow = Bars[ i ].Low;
                                        }
                                        else break;
                                    }

                                    g._lastLow  = Bars[ i ].Low;
                                    g._lastHigh = Bars[i].High;

                                    if ( g._lastGannSwingExtremumIndex > -1  )
                                    {
                                        g.StoreVariablesSnapshot( Bars[ g._lastGannSwingExtremumIndex ].Low, Bars[ g._lastGannSwingExtremumIndex ].High );
                                    }

                                    
                                }
                            }
                        }
                    }

                    // --------------------------------------------------------------------------------------------------------
                    //
                    // For the last Ray, it has two scenario.
                    //  1) If it is downtrend, the ray will join the last Higher High to the current lowest point
                    //
                    //  2) If it is uptrend, the ray will join the last lower low to the current highest point
                    // --------------------------------------------------------------------------------------------------------
                    if ( i == ( endIndex - 1) )
                    {
                        if ( g._lastHigh < Bars[i].High && g._trendDirection == TrendDirection.Uptrend )						// The trend in the current bar rising
                        {
                            g._lastHigherHighIndex                             = i;

                            gannSwingMinorTrend.TryGetValueAndAddOrReplace( Bars[ g._lastHigherHighIndex ].LinuxTime, TASignal.GANN_PEAK, out old );                            

                            if ( g._lastHigherHighIndex >= startIndex )
                            {
                                ZigZagsBoth[ g._lastHigherHighIndex - startIndex ] = Bars[ g._lastHigherHighIndex ].High;
                                ZigZagsHigh[ g._lastHigherHighIndex - startIndex ] = Bars[ g._lastHigherHighIndex ].High;
                                ZigZagsLow[ g._lastHigherHighIndex - startIndex ]  = 0;    
                            }
                            else
                            {
                                this.LogError( "Gann Swing array Error _indexOfLastHigherHighBar < startIndex, _indexOfLastHigherHighBar = " + g._lastHigherHighIndex + ", startIndex = " + startIndex );

                                CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                            }

                            
                            g._lastGannSwingExtremumIndex                              = g._lastHigherHighIndex;
                        }
                        else if ( g._lastLow > Bars[ i ].Low && g._trendDirection == TrendDirection.DownTrend )						// The trend in the current bar downward
                        {
                            g._lastLowerLowIndex                             = i;
                            gannSwingMinorTrend.TryGetValueAndAddOrReplace( Bars[ g._lastLowerLowIndex ].LinuxTime, TASignal.GANN_TROUGH, out old );
                            

                            if ( g._lastLowerLowIndex >= startIndex )
                            {
                                ZigZagsBoth[ g._lastLowerLowIndex - startIndex ] = Bars[ g._lastLowerLowIndex ].Low;
                                ZigZagsLow[ g._lastLowerLowIndex - startIndex ]  = Bars[ g._lastLowerLowIndex ].Low;
                                ZigZagsHigh[ g._lastLowerLowIndex - startIndex ] = 0;    
                            }
                            else
                            {
                                this.LogError( "Gann Swing array Error _indexOfLastLowerLowBar < startIndex, _indexOfLastLowerLowBar = " + g._lastLowerLowIndex + ", startIndex = " + startIndex );

                                CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                            }
                            
                            g._lastGannSwingExtremumIndex                            = g._lastLowerLowIndex;
                            
                        }
                        //===================================================================================================

                        // Zero bar. Calculation of the most current ray of Gann Swing Zig Zag                  
                        if ( g._lastLowerLowIndex > 1 )
                        {
                            if ( g._trendDirection == TrendDirection.Uptrend )
                            {
                                // 1) Everytime there is a new bar and a higher high, we need to clear out the last ray                                
                                for ( int n = g._lastLowerLowIndex + 1; n < endIndex; n++ )
                                {
                                    if ( n >= startIndex )
                                    {
                                        if ( n - startIndex > 0 )
                                        {
                                            ZigZagsHigh[ n - startIndex ] = 0.0;
                                            ZigZagsBoth[ n - startIndex ] = 0.0;    
                                        }
                                    }                                    
                                }

                                // 2) And then store the new higher high                                
                                if ( g._lastHigherHighIndex >= startIndex )
                                {
                                    ZigZagsBoth[ g._lastHigherHighIndex - startIndex ] = Bars[ g._lastHigherHighIndex ].High;
                                    ZigZagsHigh[ g._lastHigherHighIndex - startIndex ] = Bars[ g._lastHigherHighIndex ].High;
                                    ZigZagsLow[ g._lastHigherHighIndex - startIndex ]  = 0;    
                                }
                                else
                                {
                                    this.LogError( "Gann Swing array Error _indexOfLastHigherHighBar < startIndex, _indexOfLastHigherHighBar = " + g._lastHigherHighIndex + ", startIndex = " + startIndex );

                                    CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );

                                }
                                
                            }
                        }                            

                        if ( g._lastHigherHighIndex > 1 )
                        {
                            if ( g._trendDirection == TrendDirection.DownTrend )
                            {
                                // 1) Everytime there is a new bar and a lower low, we need to clear out the last ray     
                                for ( int n = g._lastHigherHighIndex + 1; n < endIndex; n++ )
                                {
                                    if ( n - startIndex > 0 )
                                    {
                                        ZigZagsLow[ n - startIndex ] = 0.0;
                                        ZigZagsBoth[ n - startIndex ]    = 0.0;    
                                    }
                                }

                                // 2) And then store the new LOWER LOW                                
                                if ( g._lastLowerLowIndex >= startIndex )
                                {
                                    ZigZagsBoth[ g._lastLowerLowIndex - startIndex ] = Bars[ g._lastLowerLowIndex ].Low;
                                    ZigZagsLow[ g._lastLowerLowIndex - startIndex ]  = Bars[ g._lastLowerLowIndex ].Low;
                                    ZigZagsHigh[ g._lastLowerLowIndex - startIndex ] = 0;    
                                }
                                else
                                {
                                    this.LogError( "Gann Swing array Error _indexOfLastLowerLowBar < startIndex, _indexOfLastLowerLowBar = " + g._lastLowerLowIndex + ", startIndex = " + startIndex );

                                    CalculateGannSwing( true, DataBarUpdateType.Initial, numberOfBarsForReversal, g );
                                }
                            }
                        }

                        //if ( _timeOfLastCheckedBar < DatabarsRepo.TimeArray[ 1 ] ) i = 2;

                        //====================================================================================================
                    }                 
                }

                
                IndicatorResult.AddSetValues( zigZag,     startIndex, indexCount, true, ZigZagsBoth );
                IndicatorResult.AddSetValues( zigZagHigh, startIndex, indexCount, true, ZigZagsHigh );
                IndicatorResult.AddSetValues( zigZagLow,  startIndex, indexCount, true, ZigZagsLow );

                var ZigZagsCount         = IndicatorResult[ zigZag ].Count;
                var ZigZagSHighCount     = IndicatorResult[ zigZagHigh ].Count;
                var ZigZagSLowCount      = IndicatorResult[ zigZagLow ].Count;

                if ( ( ZigZagsCount != barB4Calculation ) || ( ZigZagSHighCount != barB4Calculation ) || ( ZigZagSLowCount != barB4Calculation ) )
                {
                    //throw new InvalidProgramException( );
                }

                if ( nthHigherHighOrLowerLowForTrendChange == 1 )
                {
                    Bars.AddSignalsToDataBar( gannSwingMinorTrend );
                }


                if ( null == _hews )
                {
                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security );

                    if( aa == null )
                        return;

                    _hews = ( HewManager ) aa.HewManager;
                }

                _hews.BuildGannSwingImportance(Bars, Bars.Period.Value, nthHigherHighOrLowerLowForTrendChange, gannSwingMinorTrend );

                

                //if ( DatabarsRepo.Period.Value == TimeSpan.FromMinutes( 5 ) )
                //{
                //    nenZigZag( fullRecalculation, updateType );
                //}

            }
	    }

    }
}