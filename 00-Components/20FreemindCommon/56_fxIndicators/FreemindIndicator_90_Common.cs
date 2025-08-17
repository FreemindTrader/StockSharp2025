using fx.Common;
using fx.Bars;

using DevExpress.Mvvm;

using fx.Definitions;
using fx.Algorithm;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        //private bool GetLargestCorrectionBtwXY_Uptrend( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> current,  int inputWaveImportance, ref WavePriceTimeInfo )
        //{
        //    int currentWaveImportance = inputWaveImportance;  

        //    KeyValuePair< long, WavePointImportance > [] result = null;

        //    do
        //    {
        //        currentWaveImportance = FinancialHelper.GetWaveImportanceDegreeLower( currentWaveImportance );

        //        result = waveImportance.Where(
        //                                        x =>
        //                                        {
        //                                            if ( x.Key > previous.Key && x.Key < current.Key )
        //                                            {
        //                                                if ( x.Value.WaveImportance == currentWaveImportance )
        //                                                {
        //                                                    return true;
        //                                                }
        //                                            }

        //                                            return false;
        //                                        }

        //                                    ).OrderBy( x => x.Key ).ToArray( );

        //        
        //    } while ( result.Length == 0 && currentWaveImportance > - 1 );

        //    if ( result.Length > 0 )
        //    {
        //        if ( result.Length % 2 == 0 )
        //        {
        //            if ( result[ 0 ].Value.Signal == TASignal.WAVE_PEAK )
        //            {

        //            }
        //        }
        //        else
        //        {
        //            if ( result[ 0 ].Value.Signal == TASignal.WAVE_PEAK )
        //            {
        //                //var lowest = GetLowestExtremumAfterX( waveImportance, result[ 0 ].Key, TASignal.WAVE_TROUGH );
        //            }
        //            else if ( result[ 0 ].Value.Signal == TASignal.WAVE_TROUGH )
        //            {

        //            }




        //        }
        //    }           

        //    return null;
        //}

        //private WavePriceTimeInfo GetLargestCorrectionBtwXY_DownTrend( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> current, int inputWaveImportance )
        //{
        //    int currentWaveImportance = inputWaveImportance;

        //    KeyValuePair< long, WavePointImportance > [] result = null;

        //    do
        //    {
        //        currentWaveImportance = FinancialHelper.GetWaveImportanceDegreeLower( currentWaveImportance );

        //        result = waveImportance.Where(
        //                                        x =>
        //                                        {
        //                                            if ( x.Key > previous.Key && x.Key < current.Key )
        //                                            {
        //                                                if ( x.Value.WaveImportance == currentWaveImportance )
        //                                                {
        //                                                    return true;
        //                                                }
        //                                            }

        //                                            return false;
        //                                        }

        //                                    ).OrderBy( x => x.Key ).ToArray( );


        //    } while ( result.Length == 0 && currentWaveImportance > -1 );

        //    if ( result.Length > 0 )
        //    {
        //        if ( result.Length % 2 == 0 )
        //        {
        //            if ( result[ 0 ].Value.Signal == TASignal.WAVE_TROUGH )
        //            {

        //            }
        //        }                
        //        else
        //        {

        //        }
        //    }
        //    
        //    

        //    return null;
        //}

        public static double GetPipsForSymbol( Security symbol, double pipDifferent )
        {
            var ps = symbol.PriceStep.HasValue ? (double) symbol.PriceStep.Value : 0.0001;

            return ( Math.Round( ( pipDifferent ) / ps, 1 ) );
        }


        private PooledList<WavePriceTimeInfo> GetCorrectionsBtwXY_Uptrend( Security symbol, TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, int inputWaveImportance )
        {
            var output = new PooledList< WavePriceTimeInfo >( );

            var innerHigherHighPeaks = GetHigherHighPeaksBtwXY_wLatest( period, waveImportance, latest, previous );
            var peaksCount           = innerHigherHighPeaks.Count;

            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * Find all the correction with respect to its LOCAL TOPS. Here we find all the lowest Troughs between two PEAKS
             *              
             * --------------------------------------------------------------------------------------------------------------------------
            */
            var trendBeginTime = previous.Key;
            var lastBeginTime  = trendBeginTime;

            WavePriceTimeInfo lastPeakInfo = default;
            var j = 0;

            while ( j < peaksCount )
            {
                var currentPeak = innerHigherHighPeaks[j];
                var n = j + 1;

                if ( n < peaksCount )
                {
                    var nextPeak     = innerHigherHighPeaks[n];
                    var inBtwTroughs = GetExtremumBetweenXY( waveImportance, nextPeak, currentPeak, TASignal.WAVE_TROUGH );
                    var lowestPoint  = double.MaxValue;
                    var newestDate   = DateTime.MinValue;

                    KeyValuePair< long, WavePointImportance > ? lowestTrough = null;
                    KeyValuePair< long, WavePointImportance > ? newestTrough = null;

                    foreach ( KeyValuePair<long, WavePointImportance> trough in inBtwTroughs )
                    {
                        ref SBar bar = ref Bars.GetBarByTime( trough.Key );

                        if ( bar.Low < lowestPoint )
                        {
                            lowestPoint = bar.Low;
                            lowestTrough = trough;
                        }

                        if ( bar.BarTime > newestDate )
                        {
                            newestDate = bar.BarTime;
                            newestTrough = trough;
                        }
                    }

                    if ( newestTrough.HasValue && lowestTrough.HasValue && ( newestTrough.Value.Key != lowestTrough.Value.Key ) )
                    {
                        ref SBar newestbar = ref Bars.GetBarByTime(  newestTrough.Value.Key );

                        ref SBar lowestbar = ref Bars.GetBarByTime(  lowestTrough.Value.Key );

                        var diff = ( newestbar.Low - lowestbar.Low );

                        var pipSize = newestbar.SymbolEx.PriceStep;

                        if ( diff < ( double ) ( pipSize * 2 ) )
                        {
                            lowestTrough = newestTrough;
                        }
                    }

                    if ( lowestTrough.HasValue )
                    {
                        ref SBar trendBegin = ref Bars.GetBarByTime(  trendBeginTime );

                        ref SBar trendEnd = ref Bars.GetBarByTime(  currentPeak.Key );

                        ref SBar counterTrend = ref Bars.GetBarByTime(  lowestTrough.Value.Key );

                        var trendMovedPips = GetPipsForSymbol( symbol, trendEnd.High - trendBegin.Low );
                        var counterPips    = GetPipsForSymbol( symbol, trendEnd.High - counterTrend.Low );

                        if ( trendMovedPips > counterPips )
                        {
                            if ( lastPeakInfo.HasPriceTimeInfo( ) )
                            {
                                lastBeginTime = trendBeginTime;
                                output.Add( lastPeakInfo );
                            }
                        }
                        else
                        {
                            trendBeginTime = lastBeginTime;

                            trendBegin = default;
                            Bars.GetBarByTime( trendBeginTime );

                            trendEnd = default;
                            Bars.GetBarByTime( currentPeak.Key );

                            counterTrend = default;
                            Bars.GetBarByTime( lowestTrough.Value.Key );

                            trendMovedPips = GetPipsForSymbol( symbol, trendEnd.High - trendBegin.Low );
                            counterPips = GetPipsForSymbol( symbol, trendEnd.High - counterTrend.Low );
                        }

                        var trendMovedRange = new RangeEx< double >( trendBegin.Low, trendEnd.High );
                        var counterTrendMovedRange = new RangeEx<double>( counterTrend.Low, trendEnd.High );

                        trendBeginTime = lowestTrough.Value.Key;

                        if ( ( int ) trendBegin.BarIndex == 14241 )
                        {
                            var linuxTime = trendBegin.LinuxTime;
                        }

                        lastPeakInfo = new WavePriceTimeInfo(
                                                                TrendDirection.DownTrend,
                                                                trendBegin.Index,
                                                                trendEnd.Index,
                                                                counterTrend.Index,
                                                                trendMovedRange,
                                                                counterTrendMovedRange,
                                                                default, default
                                                            );
                    }

                    n++;
                }

                j++;
            }

            if ( lastPeakInfo.HasPriceTimeInfo( ) )
            {
                output.Add( lastPeakInfo );
            }


            return output;
        }

        private PooledList<WavePriceTimeInfo> GetCorrectionsBtwXY_DownTrend( Security symbol, TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, int inputWaveImportance )
        {
            var output = new PooledList< WavePriceTimeInfo >( );

            var innerLowerLowTroughs = GetLowerLowTroughsBtwXY_wLatest( period, waveImportance, latest, previous );
            var troughsCount         = innerLowerLowTroughs.Count;

            /* --------------------------------------------------------------------------------------------------------------------------
            * 
            * Find all the short term bounce with respect to its troughs. Here we find all the highest peaks between two troughs
            *              
            * --------------------------------------------------------------------------------------------------------------------------
           */

            var trendBeginTime = previous.Key;
            var lastBeginTime  = trendBeginTime;

            WavePriceTimeInfo lastTroughInfo = default;
            var j = 0;



            while ( j < troughsCount )
            {
                var currentTrough = innerLowerLowTroughs[j];
                var n = j + 1;

                if ( n < troughsCount )
                {
                    var nextTrough   = innerLowerLowTroughs[n];
                    var inBtwPeaks   = GetExtremumBetweenXY( waveImportance, nextTrough, currentTrough, TASignal.WAVE_PEAK );
                    var highestPoint = double.MinValue;
                    var newestDate   = DateTime.MinValue;

                    KeyValuePair< long, WavePointImportance > ? highestPeak = null;
                    KeyValuePair<long, WavePointImportance>? newestPeak     = null;

                    foreach ( KeyValuePair<long, WavePointImportance> peak in inBtwPeaks )
                    {
                        ref SBar bar = ref Bars.GetBarByTime( peak.Key );

                        if ( bar.High > highestPoint )
                        {
                            highestPoint = bar.High;
                            highestPeak = peak;
                        }

                        if ( bar.BarTime > newestDate )
                        {
                            newestDate = bar.BarTime;
                            newestPeak = peak;
                        }
                    }

                    if ( newestPeak.HasValue && highestPeak.HasValue && ( newestPeak.Value.Key != highestPeak.Value.Key ) )
                    {
                        ref SBar newestbar = ref Bars.GetBarByTime(  newestPeak.Value.Key );

                        ref SBar highestbar = ref Bars.GetBarByTime(  highestPeak.Value.Key );

                        var diff       = ( highestbar.High - newestbar.High );
                        var pipSize    = newestbar.SymbolEx.PriceStep;

                        if ( diff < ( double ) ( pipSize * 2 ) )
                        {
                            highestPeak = newestPeak;
                        }
                    }

                    if ( highestPeak.HasValue )
                    {
                        ref SBar trendBegin = ref Bars.GetBarByTime(  trendBeginTime );

                        ref SBar trendEnd = ref
                        Bars.GetBarByTime(  currentTrough.Key );

                        ref SBar counterTrend = ref
                        Bars.GetBarByTime(  highestPeak.Value.Key );

                        var trendMovedPips = GetPipsForSymbol( symbol, trendBegin.High - trendEnd.Low );
                        var counterPips    = GetPipsForSymbol( symbol, counterTrend.High - trendEnd.Low );

                        if ( trendMovedPips > counterPips )
                        {
                            if ( lastTroughInfo.HasPriceTimeInfo( ) )
                            {
                                lastBeginTime = trendBeginTime;
                                output.Add( lastTroughInfo );
                            }
                        }
                        else
                        {
                            trendBeginTime = lastBeginTime;

                            trendBegin = default;
                            Bars.GetBarByTime( trendBeginTime );

                            trendEnd = default;
                            Bars.GetBarByTime( currentTrough.Key );

                            counterTrend = default;
                            Bars.GetBarByTime( highestPeak.Value.Key );

                            trendMovedPips = GetPipsForSymbol( symbol, trendBegin.High - trendEnd.Low );
                            counterPips = GetPipsForSymbol( symbol, counterTrend.High - trendEnd.Low );
                        }

                        var trendMovedRange = new RangeEx<double>( trendEnd.Low, trendBegin.High );
                        var counterTrendMovedRange = new RangeEx<double>( trendEnd.Low, counterTrend.High );

                        trendBeginTime = highestPeak.Value.Key;

                        lastTroughInfo = new WavePriceTimeInfo(
                                                                TrendDirection.Uptrend,
                                                                trendBegin.Index,
                                                                trendEnd.Index,
                                                                counterTrend.Index,
                                                                trendMovedRange,
                                                                counterTrendMovedRange,
                                                                default,
                                                                default );

                    }

                    n++;
                }

                j++;
            }

            if ( lastTroughInfo.HasPriceTimeInfo( ) )
            {
                output.Add( lastTroughInfo );
            }

            return output;
        }

        public WaveABCInfo GetBestMatch( PooledList<WaveABCInfo> matches )
        {
            double maxScore   = double.MinValue;
            double diff100    = 0;
            WaveABCInfo bestMatch = null;

            foreach ( WaveABCInfo match in matches )
            {
                double total = 0;

                if ( match.ResponseA != default )
                {
                    total += match.ResponseA.FibLevelStrengh;
                }

                if ( match.ResponseB != default )
                {
                    total += match.ResponseB.FibLevelStrengh;
                }

                if ( match.ResponseC != default )
                {
                    total += match.ResponseC.FibLevelStrengh;
                }

                if ( total > maxScore )
                {
                    maxScore = total;
                    bestMatch = match;
                    diff100 = Math.Abs( 100 - match.CToA );
                }
                else if ( total == maxScore )
                {
                    // Here we want to find which will make Wave A and Wave C more closer

                    var curr100Diff = Math.Abs( 100 - match.CToA );

                    if ( curr100Diff < diff100 )
                    {
                        maxScore = total;
                        bestMatch = match;
                        diff100 = curr100Diff;
                    }
                }
            }

            return bestMatch;
        }






        private KeyValuePair<long, WavePointImportance>[ ] GetAllTroughsWithinRange( KeyValuePair<long, WavePointImportance>[ ] selected, ref SBar lowestBar, ref SBar highestBar )
        {
            var index = Math.Max( lowestBar.BarIndex, highestBar.BarIndex );

            var highestHigh = highestBar.High;
            var lowestLow = lowestBar.Low;

            var withinRange = selected.Where( x =>
            {
                if ( x.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    ref SBar checkingBar = ref Bars.GetBarByTime( x.Key );

                    if ( ( checkingBar.High <= highestHigh  ) && ( checkingBar.Low >= lowestLow ) && ( checkingBar.BarIndex > index  ) )
                    {
                        return true;
                    }
                }

                return false;
            } ).ToArray();

            return withinRange;
        }

        private KeyValuePair<long, WavePointImportance>[ ] GetAllPeaksWithinRange( KeyValuePair<long, WavePointImportance>[ ] selected, ref SBar lowestBar, ref SBar highestBar )
        {
            var index = Math.Max( lowestBar.BarIndex, highestBar.BarIndex );

            var highestHigh = highestBar.High;
            var lowestLow = lowestBar.Low;

            var withinRange = selected.Where( x =>
            {
                if ( x.Value.Signal == TASignal.WAVE_PEAK )
                {
                    ref SBar checkingBar = ref Bars.GetBarByTime( x.Key  );

                    if ( ( checkingBar.High <= highestHigh ) && ( checkingBar.Low >= lowestLow ) && ( checkingBar.BarIndex > index  ) )
                    {
                        return true;
                    }
                }

                return false;
            } ).ToArray();

            return withinRange;
        }

        private KeyValuePair<long, WavePointImportance>? GetLowestCorrectionWithinUpTrend( KeyValuePair<long, WavePointImportance>[ ] selected, ref SBar lowestBar, ref SBar highestBar )
        {
            var points = GetAllTroughsWithinRange( selected, ref lowestBar, ref highestBar );
            var lowest = double.MaxValue;

            KeyValuePair< long, WavePointImportance >? lowestPoint = null;

            for ( int i = 0; i < points.Length; i++ )
            {
                ref SBar checkingBar = ref Bars.GetBarByTime(points[ i ].Key );

                if ( checkingBar.Low < lowest )
                {
                    lowest = checkingBar.Low;
                    lowestPoint = points[ i ];
                }
            }

            return lowestPoint;
        }

        private KeyValuePair<long, WavePointImportance>? GetHighestCorrectionWithinDowntrend( KeyValuePair<long, WavePointImportance>[ ] selected, ref SBar lowestBar, ref SBar highestBar )
        {
            var points = GetAllPeaksWithinRange( selected, ref lowestBar, ref highestBar );
            var highest = double.MinValue;

            KeyValuePair< long, WavePointImportance >? highestPoint = null;

            for ( int i = 0; i < points.Length; i++ )
            {
                ref SBar checkingBar = ref Bars.GetBarByTime(points[ i ].Key );

                if ( checkingBar.High > highest )
                {
                    highest = checkingBar.High;
                    highestPoint = points[ i ];
                }
            }

            return highestPoint;
        }
    }
}
