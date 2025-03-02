using DevExpress.Mvvm;
using Ecng.Collections;
using Ecng.Common;
using fx.Common;
using fx.Definitions;
using fx.Algorithm;
using fx.Bars;
using StockSharp.BusinessEntities;
using StockSharp.Localization;

using System;
using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using fx.Definitions.Collections;
using System.Threading;
using System.Collections.Generic;
using Ecng.Logging;


#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator, ILogSource, ILogReceiver
    {
        private void CheckForAbcWaveRotation( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> first, KeyValuePair<long, WavePointImportance> second, KeyValuePair<long, WavePointImportance> third, KeyValuePair<long, WavePointImportance> fourth, bool isMajor, CancellationToken token )
        {
            ref SBar bar1st = ref Bars.GetBarByTime( first.Key );
            ref SBar bar2nd = ref Bars.GetBarByTime( second.Key );
            ref SBar bar3rd = ref Bars.GetBarByTime( third.Key ); 
            ref SBar bar4th = ref Bars.GetBarByTime( fourth.Key ); 
              
            bool needCalculateABCTime = false;

            if ( ( second.Value.Signal == fourth.Value.Signal ) )
            {
                if ( second.Value.Signal == TASignal.WAVE_PEAK )
                {
                    if ( bar4th.High >= bar2nd.High )
                    {
                        needCalculateABCTime = true;
                    }
                }
                else if ( second.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    if ( bar4th.Low <= bar2nd.Low )
                    {
                        needCalculateABCTime = true;
                    }
                }
            }

            if ( needCalculateABCTime )
            {
                CheckForWaveRotation( period, taManager, first, fourth, isMajor, token );
            }
        }

        private KeyValuePair<long, WavePointImportance>? GetHighestExtremeInUptrend( KeyValuePair<long, WavePointImportance>[ ] waveImportance )
        {
            //var peaksCount   = waveImportance.Count();

            //int p = 0;

            //while ( p < peaksCount )
            //{
            //    i = 0;

            //    var peak = innerCheckingPeaks[ p ];
            //    SBar peakBar = default; DatabarsRepo.GetBarByTime( peak.Key );

            //    var troughsToCheck = innerTroughs.Where( x => x.Key < peak.Key ).ToList();

            //    var troughsToCheckCount = troughsToCheck.Count();

            //    if ( troughsToCheckCount > 0 )
            //    {
            //        while ( i < troughsToCheckCount )
            //        {
            //            var currentTrough = troughsToCheck[ i ];
            //            lastExtremum = currentTrough;

            //            i++;
            //        }
            //    }

            //    p++;
            //}
            return null;
        }

        private KeyValuePair<long, WavePointImportance>? GetLowestExtremeInDowntrend( KeyValuePair<long, WavePointImportance>[ ] waveImportance )
        {

            return null;
        }

        private KeyValuePair<long, WavePointImportance>[ ] GetExtremumBetweenXY( KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous, TASignal peakOrTrough )
        {
            var macdExtremumDict = _macdExtremumDict;

            var result = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous.Key && x.Key <= current.Key )
                                                        {
                                                            if ( x.Value.Signal == peakOrTrough )
                                                            {
                                                                return true;
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToArray( );

            return result;
        }

        private KeyValuePair<long, WavePointImportance>[ ] GetExtremumBetweenXY( BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous, TASignal peakOrTrough )
        {
            var newWaves = waveImportance.ToArray( );

            return GetExtremumBetweenXY( newWaves, current, previous, peakOrTrough );
        }

        private KeyValuePair<long, WavePointImportance>[ ] GetExtremumBetweenXY( BTreeDictionary<long, WavePointImportance> waveImportance, long current, long previous, TASignal peakOrTrough )
        {
            var macdExtremumDict = _macdExtremumDict;

            var result = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous && x.Key <= current )
                                                        {
                                                            if ( x.Value.Signal == peakOrTrough )
                                                            {
                                                                ref SBar bar = ref Bars.GetBarByTime(x.Key );
                                                                if( bar == SBar.EmptySBar )
                                                                    return false;

                                                                if ( macdExtremumDict.ContainsKey( bar.Index ) )
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToArray( );

            return result;
        }

        private KeyValuePair<long, WavePointImportance>[ ] GetExtremumBetweenXY( KeyValuePair<long, WavePointImportance>[ ] waveImportance, long current, long previous, TASignal peakOrTrough )
        {
            var macdExtremumDict = _macdExtremumDict;

            var result = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous && x.Key <= current )
                                                        {
                                                            if ( x.Value.Signal == peakOrTrough )
                                                            {
                                                                return true;
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToArray( );

            return result;
        }

        private KeyValuePair<long, WavePointImportance>[ ] GetExtremumAfterX( BTreeDictionary<long, WavePointImportance> waveImportance, long latest, TASignal peakOrTrough )
        {
            var macdExtremumDict = _macdExtremumDict;

            var result = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > latest )
                                                        {
                                                            if ( x.Value.Signal == peakOrTrough )
                                                            {
                                                                ref SBar bar = ref Bars.GetBarByTime(x.Key );
                                                                if( bar == SBar.EmptySBar )
                                                                    return false;

                                                                if ( macdExtremumDict.ContainsKey( bar.Index ) )
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToArray( );

            return result;
        }

        private KeyValuePair<long, WavePointImportance>? GetHighestExtremumAfterX( BTreeDictionary<long, WavePointImportance> waveImportance, long latest, TASignal peakOrTrough )
        {
            var higherPoints  = GetExtremumAfterX( waveImportance, latest, peakOrTrough );

            int count = higherPoints.Count( );

            if ( count == 0 )
            {
                return null;
            }
            else if ( count == 1 )
            {
                return higherPoints[ 0 ];
            }
            else
            {
                KeyValuePair< long, WavePointImportance >? highest = null;

                var higherPoint = Double.MinValue;

                foreach ( KeyValuePair<long, WavePointImportance> turningPoint in higherPoints )
                {
                    ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );
                    
                    if ( bar != SBar.EmptySBar )                        
                    {
                        if ( bar.High > higherPoint )
                        {
                            higherPoint = bar.High;

                            highest = turningPoint;
                        }
                    }
                }

                return highest;
            }
        }

        private KeyValuePair<long, WavePointImportance>? GetLowestExtremumAfterX( BTreeDictionary<long, WavePointImportance> waveImportance, long latest, TASignal peakOrTrough )
        {
            var lowerPoints  = GetExtremumAfterX( waveImportance, latest, peakOrTrough );

            int count = lowerPoints.Count( );

            if ( count == 0 )
            {
                return null;
            }
            else if ( count == 1 )
            {
                return lowerPoints[ 0 ];
            }
            else
            {
                KeyValuePair< long, WavePointImportance >? lowest = null;

                var lowerPoint = Double.MaxValue;

                foreach ( KeyValuePair<long, WavePointImportance> turningPoint in lowerPoints )
                {
                    ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                    if ( bar != SBar.EmptySBar )
                    {
                        if ( bar.Low < lowerPoint )
                        {
                            lowerPoint = bar.Low;

                            lowest = turningPoint;
                        }
                    }
                }

                return lowest;
            }
        }

        private KeyValuePair<long, WavePointImportance>? GetBeginningOfUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var innerTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var innerCheckingPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            GetHigherHighTroughsFromX_wPeaks( symbol, period, selectedWaves, latestWave, ref innerTroughs, ref innerCheckingPeaks );

            KeyValuePair< long, WavePointImportance >? lastExtremum = null;

            int i = 0;
            int p = 0;

            var peaksCount   = innerCheckingPeaks.Count();
            var troughsCount = innerTroughs.Count();

            while ( p < peaksCount )
            {
                i = 0;

                var peak = innerCheckingPeaks[ p ];

                ref SBar peakBar = ref Bars.GetBarByTime(peak.Key );

                if ( peakBar != SBar.EmptySBar )
                {
                    var troughsToCheck = innerTroughs.Where( x => x.Key < peak.Key ).ToList();

                    var troughsToCheckCount = troughsToCheck.Count();

                    if ( troughsToCheckCount > 0 )
                    {
                        while ( i < troughsToCheckCount )
                        {
                            var currentTrough = troughsToCheck[ i ];
                            lastExtremum = currentTrough;

                            i++;
                        }
                    }

                    p++;
                }


            }

            if ( lastExtremum.HasValue )
            {
                return lastExtremum;
            }

            //if ( peaksCount == 0 && troughsCount == 1 )
            /// <image url="$(SolutionDir)\..\..\30 - CommonImages\peaksCountOne.png"/>
            // 
            if ( troughsCount == 1 )
            {
                var trough = innerTroughs[ 0 ];

                return trough;

            }

            return null;
        }

        private KeyValuePair<long, WavePointImportance>? GetBeginningOfUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> latestWave, BTreeDictionary<long, WavePointImportance> selectedWaves )
        {
            var innerTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var innerCheckingPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            GetHigherHighTroughsFromX_wPeaks( symbol, period, selectedWaves, latestWave, ref innerTroughs, ref innerCheckingPeaks );

            KeyValuePair< long, WavePointImportance >? lastExtremum = null;

            int i = 0;
            int p = 0;

            var peaksCount   = innerCheckingPeaks.Count();
            var troughsCount = innerTroughs.Count();

            while ( p < peaksCount )
            {
                i = 0;

                var peak = innerCheckingPeaks[ p ];

                ref SBar peakBar = ref Bars.GetBarByTime(peak.Key );

                if ( peakBar != SBar.EmptySBar )
                {
                    var troughsToCheck = innerTroughs.Where( x => x.Key < peak.Key ).ToList();

                    var troughsToCheckCount = troughsToCheck.Count();

                    if ( troughsToCheckCount > 0 )
                    {
                        while ( i < troughsToCheckCount )
                        {
                            var currentTrough = troughsToCheck[ i ];
                            lastExtremum = currentTrough;

                            i++;
                        }
                    }

                    p++;
                }
            }

            if ( lastExtremum.HasValue )
            {
                return lastExtremum;
            }

            if ( troughsCount == 1 )
            {
                var trough = innerTroughs[ 0 ];

                return trough;

            }

            return null;
        }

        private KeyValuePair<long, WavePointImportance>? GetLargestWaveImportance( KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var selectedWaves = waveImportance.Where( x => x.Key > previous.Key && x.Key < latest.Key ).ToArray( );

            KeyValuePair< long, WavePointImportance > ? highest = null;
            int highestWaveImportance = 0;

            foreach ( KeyValuePair<long, WavePointImportance> wave in selectedWaves )
            {
                if ( wave.Value.WaveImportance > highestWaveImportance )
                {
                    highestWaveImportance = wave.Value.WaveImportance;

                    highest = wave;
                }
            }

            return highest;
        }

        private KeyValuePair<long, WavePointImportance>? GetTroughOfHighestWaveImportance( KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var selectedWaves = waveImportance.Where( x =>
                                                            {
                                                                if ( x.Key > previous.Key && x.Key < latest.Key )
                                                                {
                                                                    if ( x.Value.Signal == TASignal.WAVE_TROUGH )
                                                                    {
                                                                        return true;
                                                                    }
                                                                }

                                                                return false;
                                                            }
                                                    ).ToArray( );

            int highestWaveImportance = 0;

            foreach ( KeyValuePair<long, WavePointImportance> wave in selectedWaves )
            {
                if ( wave.Value.WaveImportance > highestWaveImportance )
                {
                    highestWaveImportance = wave.Value.WaveImportance;
                }
            }

            var highestTroughs = selectedWaves.Where( x => x.Value.WaveImportance == highestWaveImportance );

            double lowestTrough = double.MaxValue;
            KeyValuePair< long, WavePointImportance > ? output = null;

            foreach ( var trough in highestTroughs )
            {
                ref SBar checkingBar = ref Bars.GetBarByTime(trough.Key );

                if ( checkingBar != SBar.EmptySBar )
                {
                    if ( checkingBar.Low < lowestTrough )
                    {
                        lowestTrough = checkingBar.Low;
                        output = trough;
                    }
                }
            }



            return output;
        }


        private KeyValuePair<long, WavePointImportance>? GetPeakOfHighestWaveImportance( KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var selectedWaves = waveImportance.Where( x =>
                                                            {
                                                                if ( x.Key > previous.Key && x.Key < latest.Key )
                                                                {
                                                                    if ( x.Value.Signal == TASignal.WAVE_PEAK )
                                                                    {
                                                                        return true;
                                                                    }
                                                                }

                                                                return false;
                                                            }
                                                    ).ToArray( );

            int highestWaveImportance = 0;

            foreach ( KeyValuePair<long, WavePointImportance> wave in selectedWaves )
            {
                if ( wave.Value.WaveImportance > highestWaveImportance )
                {
                    highestWaveImportance = wave.Value.WaveImportance;
                }
            }

            var peaks = selectedWaves.Where( x => x.Value.WaveImportance == highestWaveImportance );

            double highestPeak = double.MinValue;
            KeyValuePair< long, WavePointImportance > ? output = null;

            foreach ( var peak in peaks )
            {
                ref SBar checkingBar = ref Bars.GetBarByTime(peak.Key );

                if ( checkingBar != SBar.EmptySBar )
                {
                    if ( checkingBar.High > highestPeak )
                    {
                        highestPeak = checkingBar.High;
                        output = peak;
                    }
                }
            }

            return output;
        }



        private KeyValuePair<long, WavePointImportance>? GetBeginningOfDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var innerPeaks           = new PooledList<KeyValuePair<long, WavePointImportance>>();
            var innerCheckingTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            GetLowerLowPeaksFromX_wTroughs( symbol, period, selectedWaves, latestWave, ref innerPeaks, ref innerCheckingTroughs );

            KeyValuePair< long, WavePointImportance >? lastExtremum = null;

            int i = 0;
            int t = 0;

            var troughsCount = innerCheckingTroughs.Count();
            var peaksCount = innerPeaks.Count();

            while ( t < troughsCount )
            {
                var trough = innerCheckingTroughs[ t ];

                ref SBar troughBar = ref Bars.GetBarByTime(trough.Key );

                if ( troughBar != SBar.EmptySBar )
                {
                    var peaksToCheck = innerPeaks.Where( x => x.Key < trough.Key ).ToList();

                    var peaksToCheckCount = peaksToCheck.Count();

                    i = 0;

                    if ( peaksToCheckCount > 0 )
                    {
                        while ( i < peaksToCheckCount )
                        {
                            var currentPeak = peaksToCheck[ i ];
                            lastExtremum = currentPeak;

                            i++;
                        }
                    }

                    t++;
                }
            }

            if ( lastExtremum.HasValue )
            {
                return lastExtremum;
            }

            if ( peaksCount == 1 )
            {
                var peak = innerPeaks[ 0 ];

                return peak;
            }

            return null;
        }

        private KeyValuePair<long, WavePointImportance>? GetBeginningOfDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> latestWave, BTreeDictionary<long, WavePointImportance> selectedWaves )
        {
            var innerPeaks           = new PooledList<KeyValuePair<long, WavePointImportance>>();
            var innerCheckingTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            GetLowerLowPeaksFromX_wTroughs( symbol, period, selectedWaves, latestWave, ref innerPeaks, ref innerCheckingTroughs );

            KeyValuePair< long, WavePointImportance >? lastExtremum = null;

            int i = 0;
            int t = 0;

            var troughsCount = innerCheckingTroughs.Count();
            var peaksCount = innerPeaks.Count();

            while ( t < troughsCount )
            {
                var trough = innerCheckingTroughs[ t ];

                ref SBar troughBar = ref Bars.GetBarByTime(trough.Key );

                if ( troughBar != SBar.EmptySBar )
                {
                    var peaksToCheck = innerPeaks.Where( x => x.Key < trough.Key ).ToList();

                    var peaksToCheckCount = peaksToCheck.Count();

                    i = 0;

                    if ( peaksToCheckCount > 0 )
                    {
                        while ( i < peaksToCheckCount )
                        {
                            var currentPeak = peaksToCheck[ i ];
                            lastExtremum = currentPeak;

                            i++;
                        }
                    }

                    t++;
                }
            }

            if ( lastExtremum.HasValue )
            {
                return lastExtremum;
            }

            if ( peaksCount == 1 )
            {
                var peak = innerPeaks[ 0 ];

                return peak;
            }

            return null;
        }


        private void GetLowerLowPeaksFromX_wTroughs( Security symbol, TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] selectedWaves, KeyValuePair<long, WavePointImportance> latestWave, ref PooledList<KeyValuePair<long, WavePointImportance>> innerPeaks, ref PooledList<KeyValuePair<long, WavePointImportance>> innerCheckingTroughs )
        {
            ref SBar checkingBar = ref Bars.GetBarByTime(latestWave.Key );

            if ( checkingBar == SBar.EmptySBar ) return;                

            var macdExtremumDict = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = selectedWaves.Where( x =>
            {
                if (x.Value.Signal == TASignal.WAVE_PEAK)
                {
                    return true;
                }
                return false;
            }
                                                                );

            if ( nonSmoothedInnerTurningPts.Count() == 0 ) return;

            var descTurningPoints = nonSmoothedInnerTurningPts.ToList( ).OrderByDescending( x => x.Key );

            if ( checkingBar.BarIndex < _lastMacdCrossIndex )
            {
                innerCheckingTroughs.Add( latestWave );
            }

            double highestHigh = checkingBar.High;

            long lastHighestHighTime = -1;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in descTurningPoints )
            {

                // Need more logic in here like Square_CurrentPrice_TimeElapsed_MajorUpTrend

                ref SBar turningPointBar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( turningPointBar == SBar.EmptySBar ) return;
                

                if ( lastHighestHighTime != -1 )
                {
                    var trough = selectedWaves.Where( x =>
                                                            {
                                                                if( ( x.Value.Signal == TASignal.WAVE_TROUGH ) && ( x.Key > turningPoint.Key && x.Key < lastHighestHighTime ) )
                                                                {
                                                                    return true;
                                                                }
                                                                return false;
                                                            } ).FirstOrDefault( );


                    ref SBar troughBar = ref Bars.GetBarByTime(trough.Key );

                    if ( turningPointBar != SBar.EmptySBar )
                    {
                        if ( checkingBar.Low < troughBar.Low )
                        {
                            innerCheckingTroughs.Add( trough );

                            /* ---------------------------------------------------------------------------------------------------------------------------
                              * 
                              * Tony :   After a much thoughout test, the following assignment will call the SBarList to be corrupted.
                              *          
                              *          here we have checkingBar is an alias to 6463 bar and trough bar is an alias to 6129
                              *          
                              *          This assignment 
                              *          checkingBar = troughBar;
                              *          will cause a copy of the two structs
                              *          
                              *          I believe the correct way is to do
                              *          
                              *          checkingBar = ref troughBar
                              *          
                              *          if we want to only change checkingBar to point to troughBar
                              *          
                              * ---------------------------------------------------------------------------------------------------------------------------
                              */

                            checkingBar = ref troughBar;
                        }
                        else
                        {
                            // Here we have a lower low that break out previous uptrend

                            break;
                        }
                    }
                }

                /* ---------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Tony :   This bug is so fucked up. If I have checkingBar as ref SBar instead of just SBar ( this should just be an alias )
                 *          to an internal element of SBarList
                 * 
                 *          When I reassign checkingBar to a second element, the first element will be corrupted and has the content of
                 *          the second element.
                 * ---------------------------------------------------------------------------------------------------------------------------
                 */

                //IndicatorBarsRepo.CheckBars( );

                checkingBar = ref Bars.GetLowestBarOfTheRangeInclusive( turningPointBar.BarTime, checkingBar.BarTime );

                //IndicatorBarsRepo.CheckBars( );

                if ( ( turningPointBar.High > highestHigh ) && checkingBar != SBar.EmptySBar )
                {
                    highestHigh = turningPointBar.High;
                    lastHighestHighTime = turningPoint.Key;

                    innerPeaks.Add( turningPoint );
                }
            }
        }

        private void GetLowerLowPeaksFromX_wTroughs( Security symbol, TimeSpan period, BTreeDictionary<long, WavePointImportance> selectedWaves, KeyValuePair<long, WavePointImportance> latestWave, ref PooledList<KeyValuePair<long, WavePointImportance>> innerPeaks, ref PooledList<KeyValuePair<long, WavePointImportance>> innerCheckingTroughs )
        {
            ref SBar checkingBar = ref Bars.GetBarByTime(latestWave.Key );

            if ( checkingBar == SBar.EmptySBar ) return;                

            var macdExtremumDict = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = selectedWaves.Where( x =>
                                                                        {
                                                                            if (x.Value.Signal == TASignal.WAVE_PEAK)
                                                                            {
                                                                                return true;
                                                                            }
                                                                            return false;
                                                                        }
                                                                );

            if ( nonSmoothedInnerTurningPts.Count() == 0 ) return;

            var descTurningPoints = nonSmoothedInnerTurningPts.ToList( ).OrderByDescending( x => x.Key );

            if ( checkingBar.BarIndex < _lastMacdCrossIndex )
            {
                innerCheckingTroughs.Add( latestWave );
            }

            double highestHigh = checkingBar.High;

            long lastHighestHighTime = -1;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in descTurningPoints )
            {

                // Need more logic in here like Square_CurrentPrice_TimeElapsed_MajorUpTrend
                ref SBar turningPointBar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( turningPointBar == SBar.EmptySBar ) return;
                

                if ( lastHighestHighTime != -1 )
                {
                    var trough = selectedWaves.Where( x =>
                    {
                        if( ( x.Value.Signal == TASignal.WAVE_TROUGH ) && ( x.Key > turningPoint.Key && x.Key < lastHighestHighTime ) )
                        {
                            return true;
                        }
                        return false;
                    } ).FirstOrDefault( );


                    ref SBar troughBar = ref Bars.GetBarByTime(trough.Key );

                    if ( troughBar != SBar.EmptySBar )
                    {
                        if ( checkingBar.Low < troughBar.Low )
                        {
                            innerCheckingTroughs.Add( trough );
                            checkingBar = ref troughBar;
                        }
                        else
                        {
                            // Here we have a lower low that break out previous uptrend

                            break;
                        }
                    }
                }

                checkingBar = ref Bars.GetLowestBarOfTheRangeInclusive( turningPointBar.BarTime, checkingBar.BarTime );

                if ( ( turningPointBar.High > highestHigh ) && checkingBar != SBar.EmptySBar )
                {
                    highestHigh = turningPointBar.High;
                    lastHighestHighTime = turningPoint.Key;

                    innerPeaks.Add( turningPoint );
                }
            }
        }

        private void GetHigherHighTroughsFromX_wPeaks( Security symbol, TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] selectedWaves, KeyValuePair<long, WavePointImportance> latestWave, ref PooledList<KeyValuePair<long, WavePointImportance>> innerTroughs, ref PooledList<KeyValuePair<long, WavePointImportance>> innerCheckingPeaks )
        {
            ref SBar checkingBar = ref Bars.GetBarByTime(latestWave.Key );

            if ( checkingBar == SBar.EmptySBar ) return;            

            var macdExtremumDict = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = selectedWaves.Where( x =>
            {
                if (x.Value.Signal == TASignal.WAVE_TROUGH)
                {
                    return true;
                }
                return false;
            }
                                                                );

            if ( nonSmoothedInnerTurningPts.Count() == 0 ) return;

            var descTurningPoints = nonSmoothedInnerTurningPts.ToList( ).OrderByDescending( x => x.Key );

            if ( checkingBar.BarIndex < _lastMacdCrossIndex )
            {
                innerCheckingPeaks.Add( latestWave );
            }

            double lowestLow       = checkingBar.Low;
            long lastLowestLowTime = -1;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in descTurningPoints )
            {
                ref SBar turningPointBar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( turningPointBar == SBar.EmptySBar ) return;
                
                if ( lastLowestLowTime != -1 )
                {
                    var peak = selectedWaves.Where( x =>
                                                    {
                                                        if( ( x.Value.Signal == TASignal.WAVE_PEAK ) && ( x.Key > turningPoint.Key && x.Key < lastLowestLowTime ) )
                                                        {
                                                            return true;
                                                        }
                                                        return false;
                                                    } )
                        .FirstOrDefault( );


                    ref SBar peakBar = ref Bars.GetBarByTime(peak.Key );

                    if ( peakBar != SBar.EmptySBar )
                    {
                        if ( checkingBar.High > peakBar.High )
                        {
                            innerCheckingPeaks.Add( peak );
                            checkingBar = ref peakBar;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if ( turningPointBar != SBar.EmptySBar)
                {
                    /// <image url="$(SolutionDir)\..\..\30 - CommonImages\NotTheHighest.png"></image>
                    /// 

                    /* ---------------------------------------------------------------------------------------------------------------------------------
                     * 
                     * 
                     * 
                     * --------------------------------------------------------------------------------------------------------------------------------- 
                     */
                    checkingBar = ref Bars.GetHighestBarOfTheRangeInclusive( turningPointBar.BarTime, checkingBar.BarTime );
                    
                    if ( ( turningPointBar.Low < lowestLow ) && checkingBar != SBar.EmptySBar )
                    {
                        lowestLow = turningPointBar.Low;
                        lastLowestLowTime = turningPoint.Key;

                        innerTroughs.Add( turningPoint );
                    }
                }

            }
        }

        private void GetHigherHighTroughsFromX_wPeaks( Security symbol, TimeSpan period, BTreeDictionary<long, WavePointImportance> selectedWaves, KeyValuePair<long, WavePointImportance> latestWave, ref PooledList<KeyValuePair<long, WavePointImportance>> innerTroughs, ref PooledList<KeyValuePair<long, WavePointImportance>> innerCheckingPeaks )
        {
            ref SBar checkingBar = ref Bars.GetBarByTime(latestWave.Key );

            if ( checkingBar == SBar.EmptySBar ) return;            

            var macdExtremumDict = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = selectedWaves.Where( x =>
            {
                if (x.Value.Signal == TASignal.WAVE_TROUGH)
                {
                    return true;
                }
                return false;
            }
                                                                );

            if ( nonSmoothedInnerTurningPts.Count() == 0 ) return;

            var descTurningPoints = nonSmoothedInnerTurningPts.ToList( ).OrderByDescending( x => x.Key );

            if ( checkingBar.BarIndex < _lastMacdCrossIndex )
            {
                innerCheckingPeaks.Add( latestWave );
            }

            double lowestLow       = checkingBar.Low;
            long lastLowestLowTime = -1;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in descTurningPoints )
            {
                ref SBar turningPointBar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( turningPointBar == SBar.EmptySBar ) return;
                
                if ( lastLowestLowTime != -1 )
                {
                    var peak = selectedWaves.Where( x =>
                    {
                        if( ( x.Value.Signal == TASignal.WAVE_PEAK ) && ( x.Key > turningPoint.Key && x.Key < lastLowestLowTime ) )
                        {
                            return true;
                        }
                        return false;
                    } )
                        .FirstOrDefault( );

                    ref SBar peakBar = ref Bars.GetBarByTime(peak.Key );

                    if ( peakBar != SBar.EmptySBar )
                    {
                        if ( checkingBar.High > peakBar.High )
                        {
                            innerCheckingPeaks.Add( peak );
                            checkingBar = ref peakBar;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                checkingBar = ref Bars.GetHighestBarOfTheRangeInclusive( turningPointBar.BarTime, checkingBar.BarTime );

                if ( ( turningPointBar.Low < lowestLow ) && checkingBar != SBar.EmptySBar )
                {
                    lowestLow = turningPointBar.Low;
                    lastLowestLowTime = turningPoint.Key;

                    innerTroughs.Add( turningPoint );
                }
            }
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighPeaksBtwXY_wLatest( TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous )
        {
            KeyValuePair< long, WavePointImportance >[ ] newWaves = waveImportance.ToArray( );

            return GetHigherHighPeaksBtwXY_wLatest( period, newWaves, current, previous );
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighPeaksBtwXY_wLatest( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous )
        {
            var result = GetExtremumBetweenXY( waveImportance, current, previous, TASignal.WAVE_PEAK );

            var resultList = result.ToList();

            // This need to include the latest top. If not, then the top is just a spike so the macd doesn't cross over
            if ( !resultList.Contains( current ) )
            {
                ref SBar bar = ref Bars.GetBarByTime(current.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.BarIndex < _lastMacdCrossIndex )
                    {
                        resultList.Add( current );
                    }
                }
            }

            var higherHighList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var higherPoint = Double.MinValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( LocalHigherOrCloselyEqual( period, bar.High, higherPoint ) != DivergenceBoolean.False )
                    {
                        higherPoint = bar.High;

                        higherHighList.Add( turningPoint );
                    }
                }
            }

            return higherHighList;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighPeaksBtwXY_wLatest_OnlyCertainWaveImportance( TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous, int selectedWaveImportance )
        {
            KeyValuePair< long, WavePointImportance >[ ] newWaves = waveImportance.ToArray( );

            return GetHigherHighPeaksBtwXY_wLatest_OnlyCertainWaveImportance( period, newWaves, current, previous, selectedWaveImportance );
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighPeaksBtwXY_wLatest_OnlyCertainWaveImportance( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous, int selectedWaveImportance )
        {
            var result = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous.Key && x.Key <= current.Key )
                                                        {
                                                            if ( x.Value.Signal == TASignal.WAVE_PEAK && x.Value.WaveImportance >= selectedWaveImportance )
                                                            {
                                                                return true;
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToArray( );


            var resultList = result.ToList();

            // This need to include the latest top. If not, then the top is just a spike so the macd doesn't cross over
            if ( !resultList.Contains( current ) )
            {
                ref SBar bar = ref Bars.GetBarByTime(current.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.BarIndex < _lastMacdCrossIndex )
                    {
                        resultList.Add( current );
                    }
                }
            }

            var higherHighList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var higherPoint = Double.MinValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( LocalHigherOrCloselyEqual( period, bar.High, higherPoint ) != DivergenceBoolean.False )
                    {
                        higherPoint = bar.High;

                        higherHighList.Add( turningPoint );
                    }
                }
            }

            return higherHighList;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighPeaksBtwXY_NoLatest( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, long currentTime, long previousTime )
        {
            var result = GetExtremumBetweenXY( waveImportance, currentTime, previousTime, TASignal.WAVE_PEAK );

            var resultList = result.ToList();

            var higherHighList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var higherPoint = Double.MinValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {

                    if ( LocalHigherOrCloselyEqual( period, bar.High, higherPoint ) != DivergenceBoolean.False )
                    {
                        higherPoint = bar.High;

                        higherHighList.Add( turningPoint );
                    }
                }
            }

            return higherHighList;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowTroughsBtwXY_wLatest_OnlyCertainWaveImportance( TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous, int selectedWaveImportance )
        {
            KeyValuePair< long, WavePointImportance >[ ] newWaves = waveImportance.ToArray( );

            return GetLowerLowTroughsBtwXY_wLatest_OnlyCertainWaveImportance( period, newWaves, current, previous, selectedWaveImportance );
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowTroughsBtwXY_wLatest_OnlyCertainWaveImportance( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous, int selectedWaveImportance )
        {
            var result = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous.Key && x.Key <= current.Key )
                                                        {
                                                            if ( x.Value.Signal == TASignal.WAVE_TROUGH && x.Value.WaveImportance >= selectedWaveImportance )
                                                            {
                                                                return true;
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToArray( );

            var resultList = result.ToList();

            // This need to include the latest top. If not, then the top is just a spike so the macd doesn't cross over
            if ( !resultList.Contains( current ) )
            {
                ref SBar bar = ref Bars.GetBarByTime(current.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.BarIndex < _lastMacdCrossIndex )
                    {
                        resultList.Add( current );
                    }
                }
            }

            var lowerLowList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var lowerPoint = Double.MaxValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {

                    if ( LocalLowerOrCloselyEqual( period, bar.Low, lowerPoint ) != DivergenceBoolean.False )
                    {
                        lowerPoint = bar.Low;

                        lowerLowList.Add( turningPoint );
                    }
                }
            }

            return lowerLowList;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowTroughsBtwXY_NoLatest_OnlyCertainWaveImportance( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, long previous, long current, int selectedWaveImportance )
        {
            var resultList = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous && x.Key <= current )
                                                        {
                                                            if ( x.Value.Signal == TASignal.WAVE_TROUGH && x.Value.WaveImportance >= selectedWaveImportance )
                                                            {
                                                                ref SBar bar = ref Bars.GetBarByTime(  x.Key );

                                                                if ( bar != SBar.EmptySBar )                                                                    
                                                                {
                                                                    if ( bar.BarIndex < _lastMacdCrossUpIndex )
                                                                    {
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToList( );


            var lowerLowList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var lowerPoint = Double.MaxValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {

                    if ( LocalLowerOrCloselyEqual( period, bar.Low, lowerPoint ) != DivergenceBoolean.False )
                    {
                        lowerPoint = bar.Low;

                        lowerLowList.Add( turningPoint );
                    }
                }
            }

            return lowerLowList;
        }


        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighPeaksBtwXY_NoLatest_OnlyCertainWaveImportance( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, long previous, long current, int selectedWaveImportance )
        {
            var resultList = waveImportance.Where(
                                                    x =>
                                                    {
                                                        if ( x.Key > previous && x.Key <= current )
                                                        {
                                                            if ( x.Value.Signal == TASignal.WAVE_PEAK && x.Value.WaveImportance >= selectedWaveImportance )
                                                            {
                                                                ref SBar bar = ref Bars.GetBarByTime(  x.Key );

                                                                if ( bar != SBar.EmptySBar )
                                                                {

                                                                    if ( bar.BarIndex < _lastMacdCrossIndex )
                                                                    {
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        return false;
                                                    }

                                             ).OrderBy( x => x.Key ).ToList( );


            var higherHighList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var higherPoint = Double.MinValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {

                    if ( LocalHigherOrCloselyEqual( period, bar.High, higherPoint ) != DivergenceBoolean.False )
                    {
                        higherPoint = bar.High;

                        higherHighList.Add( turningPoint );
                    }
                }
            }

            return higherHighList;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowTroughsBtwXY_wLatest( TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous )
        {
            KeyValuePair< long, WavePointImportance >[ ] newWaves = waveImportance.ToArray( );

            return GetLowerLowTroughsBtwXY_wLatest( period, newWaves, current, previous );
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowTroughsBtwXY_wLatest( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous )
        {
            var result = GetExtremumBetweenXY( waveImportance, current, previous, TASignal.WAVE_TROUGH );

            var resultList = result.ToList();

            // This need to include the latest top. If not, then the top is just a spike so the macd doesn't cross over
            if ( !resultList.Contains( current ) )
            {
                ref SBar bar = ref Bars.GetBarByTime(current.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.BarIndex < _lastMacdCrossIndex )
                    {
                        resultList.Add( current );
                    }
                }
            }

            var lowerLowList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var lowerPoint = Double.MaxValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {

                    if ( LocalLowerOrCloselyEqual( period, bar.Low, lowerPoint ) != DivergenceBoolean.False )
                    {
                        lowerPoint = bar.Low;

                        lowerLowList.Add( turningPoint );
                    }
                }
            }

            return lowerLowList;
        }


        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowTroughsBtwXY_NoLatest( TimeSpan period, KeyValuePair<long, WavePointImportance>[ ] waveImportance, long currentTime, long previousTime )
        {
            var result = GetExtremumBetweenXY( waveImportance, currentTime, previousTime, TASignal.WAVE_TROUGH );

            var resultList = result.ToList();

            var lowerLowList = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var lowerPoint = Double.MaxValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in resultList )
            {
                ref SBar bar = ref Bars.GetBarByTime( turningPoint.Key );

                if ( bar != SBar.EmptySBar )                    
                {

                    if ( LocalLowerOrCloselyEqual( period, bar.Low, lowerPoint ) != DivergenceBoolean.False )
                    {
                        lowerPoint = bar.Low;

                        lowerLowList.Add( turningPoint );
                    }
                }
            }

            return lowerLowList;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetLowerLowPeaksBtwXY_NoLatest( TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous )
        {
            var innerPeaks         = GetExtremumBetweenXY( waveImportance, current, previous, TASignal.WAVE_PEAK );

            var innerPeaksList     = innerPeaks.OrderByDescending( x => x.Key ).ToList();

            var innerLowerLowPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var higherPoint        = Double.MinValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in innerPeaksList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {

                    if ( LocalHigherOrCloselyEqual( period, bar.High, higherPoint ) != DivergenceBoolean.False )
                    {
                        higherPoint = bar.High;

                        innerLowerLowPeaks.Add( turningPoint );
                    }
                }
            }

            var newInnerLowerLowPeaks = innerLowerLowPeaks.OrderBy( x => x.Key ).ToPooledList();

            return newInnerLowerLowPeaks;
        }

        private PooledList<KeyValuePair<long, WavePointImportance>> GetHigherHighTroughsBtwXY_NoLatest( TimeSpan period, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance> current, KeyValuePair<long, WavePointImportance> previous )
        {
            var innerTroughs           = GetExtremumBetweenXY( waveImportance, current, previous, TASignal.WAVE_TROUGH );

            var innerTroughsList       = innerTroughs.OrderByDescending( x => x.Key ).ToList();

            var innerHigherHighTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var lowerPoint             = Double.MaxValue;

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in innerTroughsList )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( LocalLowerOrCloselyEqual( period, bar.Low, lowerPoint ) != DivergenceBoolean.False )
                    {
                        lowerPoint = bar.Low;

                        innerHigherHighTroughs.Add( turningPoint );
                    }
                }
            }

            var newInnerHigherHighTroughs = innerHigherHighTroughs.OrderBy( x => x.Key ).ToPooledList();

            return newInnerHigherHighTroughs;
        }

        private void CheckForWaveRotation( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> latest, KeyValuePair<long, WavePointImportance> previous, bool isMajor, CancellationToken token )
        {
            ref SBar barLatest   = ref Bars.GetBarByTime( latest.Key ); 
            ref SBar barPrevious = ref Bars.GetBarByTime( previous.Key );

            if ( barLatest == SBar.EmptySBar || barPrevious == SBar.EmptySBar )
            {
                return;
            }


            var diff = barLatest.BarIndex - barPrevious.BarIndex;

            TimeSpecialNumbersType special = TimeSpecialNumbersType.NONE;

            if ( ( special = FinancialHelper.IsSpecialTimeNumber( ( int ) diff ) ) != TimeSpecialNumbersType.NONE )
            {
                var rotation                      = WaveRotationInfo.GetWaveRotationType( previous.Value.Signal, latest.Value.Signal );

                var waveRotation                  = new WaveRotationInfo( period, ( int ) barLatest.BarIndex, rotation );
                waveRotation.BeginBarIndex = ( int ) barPrevious.BarIndex;
                waveRotation.BarDiffOrDegree = ( int ) diff;
                waveRotation.WaveTimeType = special;

                taManager.AddWaveImportantTimeInfo( ref barLatest, waveRotation );
            }

            special = TimeSpecialNumbersType.NONE;

            if ( ( special = FinancialHelper.IsNearSpecialTimeNumber( ( int ) diff ) ) != TimeSpecialNumbersType.NONE )
            {
                var rotation                      = WaveRotationInfo.GetWaveRotationType( previous.Value.Signal, latest.Value.Signal );

                var waveRotation                  = new WaveRotationInfo( period, ( int ) barLatest.BarIndex, rotation );
                waveRotation.BeginBarIndex = ( int ) barPrevious.BarIndex;
                waveRotation.BarDiffOrDegree = ( int ) diff;
                waveRotation.WaveTimeType = special;

                if ( period == TimeSpan.FromMinutes( 1 ) && diff < 10 && ( rotation == TaWaveRotation.IMPORTANT_HIGH_HIGH || rotation == TaWaveRotation.IMPORTANT_LOW_LOW ) )
                {

                }

                taManager.AddWaveImportantTimeInfo( ref barLatest, waveRotation );
            }
        }

        private void CheckForWaveRotation( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> latest, KeyValuePair<long, WavePointImportance> previous, TaWaveRotation myRotation )
        {
            ref SBar barLatest   = ref Bars.GetBarByTime( latest.Key );
            ref SBar barPrevious = ref Bars.GetBarByTime( previous.Key );

            if ( barLatest == SBar.EmptySBar || barPrevious == SBar.EmptySBar )
            {
                return;
            }            

            var diff = barLatest.BarIndex - barPrevious.BarIndex;

            TimeSpecialNumbersType special = TimeSpecialNumbersType.NONE;

            if ( ( special = FinancialHelper.IsSpecialTimeNumber( ( int ) diff ) ) != TimeSpecialNumbersType.NONE )
            {
                var waveRotation                  = new WaveRotationInfo( period, ( int ) barLatest.BarIndex, myRotation );
                waveRotation.BeginBarIndex = ( int ) barPrevious.BarIndex;
                waveRotation.BarDiffOrDegree = ( int ) diff;
                waveRotation.WaveTimeType = special;

                taManager.AddWaveImportantTimeInfo( ref barLatest, waveRotation );
            }

            special = TimeSpecialNumbersType.NONE;

            if ( ( special = FinancialHelper.IsNearSpecialTimeNumber( ( int ) diff ) ) != TimeSpecialNumbersType.NONE )
            {
                var waveRotation                  = new WaveRotationInfo( period, ( int ) barLatest.BarIndex, myRotation );
                waveRotation.BeginBarIndex = ( int ) barPrevious.BarIndex;
                waveRotation.BarDiffOrDegree = ( int ) diff;
                waveRotation.WaveTimeType = special;

                taManager.AddWaveImportantTimeInfo( ref barLatest, waveRotation );
            }
        }

        private void CheckIfPriorTrendTimeSquareWithCurrentPriceRange( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> priorTrendBegin, KeyValuePair<long, WavePointImportance> priorTrendEnd, KeyValuePair<long, WavePointImportance> currentRangeEnd, TrendDirection trend, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            ref SBar beginBar   = ref Bars.GetBarByTime( priorTrendBegin.Key );
            ref SBar priorTrendEndBar = ref Bars.GetBarByTime( priorTrendEnd.Key );
            ref SBar squaredBar = ref Bars.GetBarByTime( currentRangeEnd.Key );

            if ( beginBar == SBar.EmptySBar || priorTrendEndBar == SBar.EmptySBar || squaredBar == SBar.EmptySBar )
            {
                return;
            }

            

            var priorTrendTime   = ( int ) ( priorTrendEndBar.BarIndex - beginBar.BarIndex );
            var priorTrendCal    = ( priorTrendEndBar.BarTime - beginBar.BarTime ).Days;

            double priceRange = 0;

            if ( trend == TrendDirection.Uptrend )
            {
                priceRange = squaredBar.High - priorTrendEndBar.Low;
            }
            else if ( trend == TrendDirection.DownTrend )
            {
                priceRange = priorTrendEndBar.High - squaredBar.Low;
            }

            var currentPriceRangeDg = GannSquareOf9.GetDegree( priceRange );
            var priorTrendTimeDg    = GannSquareOf9.GetDegree( priorTrendTime );

            bool found = InternalCheckPriceTimeSquare( period, taManager, ref beginBar, ref squaredBar, priorTrendTimeDg,  currentPriceRangeDg, isMajor,  gannPriceType);

            if ( priorTrendCal > 0 && period >= TimeSpan.FromHours( 1 ) )
            {
                var priorTrendTimeCalDg = GannSquareOf9.GetDegree( priorTrendCal );

                found = InternalCheckPriceCalTimeSquare( period, taManager, ref beginBar, ref squaredBar, priorTrendTimeCalDg, currentPriceRangeDg, isMajor, gannPriceType );
            }

            if ( found )
            {
                if ( period == TimeSpan.FromMinutes( 5 ) )
                {

                }
            }
        }

        private void CheckIfPriceSquareWithTimeElapsed( TimeSpan period, PeriodXTaManager taManager, ref SBar olderBar, ref SBar newerBar, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            double closingPrice    = 0;
            double extremePrice    = 0;
            int tradingDays        = 0;
            int calenderDays       = 0;
            double closingPriceDg  = 0;
            double tradingBarsDg   = 0;
            double calenderDaysDg  = 0;
            double extremePriceDg  = 0;

            closingPrice = newerBar.Close;
            tradingDays = ( int ) ( newerBar.BarIndex - olderBar.BarIndex );
            calenderDays = ( newerBar.BarTime - olderBar.BarTime ).Days;
            extremePrice = newerBar.Low;

            closingPriceDg = GannSquareOf9.GetDegree( closingPrice );
            tradingBarsDg = GannSquareOf9.GetDegree( tradingDays );
            extremePriceDg = GannSquareOf9.GetDegree( extremePrice );

            bool found = InternalCheckPriceTimeSquare( period, taManager, ref olderBar, ref newerBar, closingPriceDg,  tradingBarsDg, isMajor,  gannPriceType);

            if ( calenderDays > 0 && period >= TimeSpan.FromHours( 1 ) )
            {
                calenderDaysDg = GannSquareOf9.GetDegree( calenderDays );

                found = InternalCheckPriceCalTimeSquare( period, taManager, ref olderBar, ref newerBar, closingPriceDg, calenderDaysDg, isMajor, gannPriceType );
            }

            if ( extremePriceDg != closingPriceDg )
            {
                found = InternalCheckPriceTimeSquare( period, taManager, ref olderBar, ref newerBar, extremePriceDg, tradingBarsDg, isMajor, gannPriceType );
            }
        }

        private void CheckIfPriorPriceRangeSquareWithCurrentTrendTime( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> priorTrendBegin, KeyValuePair<long, WavePointImportance> priorTrendEnd, KeyValuePair<long, WavePointImportance> currentRangeEnd, TrendDirection priorTrend, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            ref SBar priorTrendBeginBar   = ref Bars.GetBarByTime( priorTrendBegin.Key );
            ref SBar priorTrendEndBar = ref Bars.GetBarByTime( priorTrendEnd.Key );
            ref SBar currentRangeEndBar = ref Bars.GetBarByTime( currentRangeEnd.Key );

            if ( priorTrendBeginBar == SBar.EmptySBar || priorTrendEndBar == SBar.EmptySBar || currentRangeEndBar == SBar.EmptySBar )
            {
                return;
            }            

            int currentTrendTime   = 0;
            int currentTrendCal    = 0;

            currentTrendTime = ( int ) ( currentRangeEndBar.BarIndex - priorTrendEndBar.BarIndex );
            currentTrendCal = ( currentRangeEndBar.BarTime - priorTrendEndBar.BarTime ).Days;

            double priorTrendPrice = 0;

            if ( priorTrend == TrendDirection.DownTrend )
            {
                priorTrendPrice = priorTrendBeginBar.High - priorTrendEndBar.Low;
            }
            else if ( priorTrend == TrendDirection.Uptrend )
            {
                priorTrendPrice = priorTrendEndBar.High - priorTrendBeginBar.Low;
            }

            if ( priorTrendPrice < 0 )
            {

            }

            var priorTrendPriceDg  = GannSquareOf9.GetDegree( priorTrendPrice );
            var currentTrendTimeDg = GannSquareOf9.GetDegree( currentTrendTime );

            bool found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendBeginBar, ref currentRangeEndBar, priorTrendPriceDg,  currentTrendTimeDg, isMajor,  gannPriceType);

            if ( currentTrendCal > 0 && period >= TimeSpan.FromHours( 1 ) )
            {
                var currentTrendCalDg = GannSquareOf9.GetDegree( currentTrendCal );

                found = InternalCheckPriceCalTimeSquare( period, taManager, ref priorTrendBeginBar, ref currentRangeEndBar, priorTrendPriceDg, currentTrendCalDg, isMajor, gannPriceType );
            }

            if ( found )
            {

            }
        }

        private void CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> priorTrendEnd, KeyValuePair<long, WavePointImportance> currentTrendEnd, TrendDirection uptrend, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            ref SBar priorTrendEndBar   = ref Bars.GetBarByTime( priorTrendEnd.Key );
            ref SBar currentTrendEndBar = ref Bars.GetBarByTime( currentTrendEnd.Key );
            

            if ( priorTrendEndBar == SBar.EmptySBar || currentTrendEndBar == SBar.EmptySBar )
            {
                return;
            }
            

            var currentTrendTime        = ( int ) ( currentTrendEndBar.BarIndex - priorTrendEndBar.BarIndex );
            var currentTrendCal         = ( currentTrendEndBar.BarTime - priorTrendEndBar.BarTime ).Days;

            double priorTrendEndHighDg  = GannSquareOf9.GetDegree( priorTrendEndBar.High );
            double priorTrendEndLowDg   = GannSquareOf9.GetDegree( priorTrendEndBar.Low );
            double priorTrendEndCloseDg = GannSquareOf9.GetDegree( priorTrendEndBar.Close );
            double currentTrendTimeDg   = GannSquareOf9.GetDegree( currentTrendTime );

            if ( period == TimeSpan.FromMinutes( 5 ) && currentTrendEndBar.BarIndex == 10454 )
            {

            }

            bool found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendEndBar, ref currentTrendEndBar,  priorTrendEndCloseDg,  currentTrendTimeDg, isMajor,  gannPriceType);

            if ( priorTrendEndHighDg != priorTrendEndCloseDg )
            {
                found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendEndBar, ref currentTrendEndBar, priorTrendEndHighDg, currentTrendTimeDg, isMajor, gannPriceType );
            }

            if ( ( priorTrendEndLowDg != priorTrendEndCloseDg ) && ( priorTrendEndLowDg != priorTrendEndHighDg ) )
            {
                found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendEndBar, ref currentTrendEndBar, priorTrendEndLowDg, currentTrendTimeDg, isMajor, gannPriceType );
            }

            if ( currentTrendCal > 0 && period >= TimeSpan.FromHours( 1 ) )
            {
                var currentTrendCalDg = GannSquareOf9.GetDegree( currentTrendCal );

                found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendEndBar, ref currentTrendEndBar, priorTrendEndCloseDg, currentTrendCalDg, isMajor, gannPriceType );

                if ( priorTrendEndHighDg != priorTrendEndCloseDg )
                {
                    found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendEndBar, ref currentTrendEndBar, priorTrendEndHighDg, currentTrendCalDg, isMajor, gannPriceType );
                }

                if ( ( priorTrendEndLowDg != priorTrendEndCloseDg ) && ( priorTrendEndLowDg != priorTrendEndHighDg ) )
                {
                    found = InternalCheckPriceTimeSquare( period, taManager, ref priorTrendEndBar, ref currentTrendEndBar, priorTrendEndLowDg, currentTrendCalDg, isMajor, gannPriceType );
                }
            }

            if ( found )
            {


            }
        }

        private void CheckIfCurrentTrendPriceRangeSquareWithCurrentTrendTime( TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> currentTrendBegin, KeyValuePair<long, WavePointImportance> currentTrendEnd, TrendDirection trend, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            ref SBar currentTrendBeginBar   = ref Bars.GetBarByTime( currentTrendBegin.Key );
            ref SBar currentTrendEndBar = ref Bars.GetBarByTime( currentTrendEnd.Key );


            if ( currentTrendBeginBar == SBar.EmptySBar || currentTrendEndBar == SBar.EmptySBar )
            {
                return;
            }

            
            double currentTrendPriceRange = 0;

            if ( trend == TrendDirection.Uptrend )
            {
                currentTrendPriceRange = currentTrendEndBar.High - currentTrendBeginBar.Low;
            }
            else if ( trend == TrendDirection.DownTrend )
            {
                currentTrendPriceRange = currentTrendBeginBar.High - currentTrendEndBar.Low;
            }

            var currentTrendTime          = ( int ) ( currentTrendEndBar.BarIndex - currentTrendBeginBar.BarIndex );
            var currentTrendCal           = ( currentTrendEndBar.BarTime - currentTrendBeginBar.BarTime ).Days;

            var currentTrendTimeDg        = GannSquareOf9.GetDegree( currentTrendTime );
            var currentTrendPriceRangeDg  = GannSquareOf9.GetDegree( currentTrendPriceRange );

            bool found = InternalCheckPriceTimeSquare( period, taManager, ref currentTrendBeginBar, ref currentTrendEndBar,  currentTrendPriceRangeDg,  currentTrendTimeDg, isMajor,  gannPriceType);

            if ( currentTrendCal > 0 && period >= TimeSpan.FromHours( 1 ) )
            {
                var currentTrendCalDg = GannSquareOf9.GetDegree( currentTrendCal );

                found = InternalCheckPriceTimeSquare( period, taManager, ref currentTrendBeginBar, ref currentTrendEndBar, currentTrendPriceRangeDg, currentTrendCalDg, isMajor, gannPriceType );
            }

            if ( found )
            {

            }
        }

        private static bool InternalCheckPriceTimeSquare( TimeSpan period, PeriodXTaManager taManager, ref SBar beginBar, ref SBar mainBar, double beginBarDg, double mainBarDg, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            var squareDegree = FinancialHelper.DoTheySquareAt45Dg( beginBarDg, mainBarDg );

            bool found = false;

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) mainBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_45Dg_Multiple;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarDg;

                found = taManager.TryAddGannPriceTimeInfo( ref mainBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAt45DgNearby( period, beginBarDg, mainBarDg, isMajor );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) mainBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_45Dg_Nearby;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarDg;

                found = taManager.TryAddGannPriceTimeInfo( ref mainBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAt30Dg( beginBarDg, mainBarDg );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) mainBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_30Dg_Multiple;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarDg;

                found = taManager.TryAddGannPriceTimeInfo( ref mainBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAt30DgNearby( period, beginBarDg, mainBarDg, isMajor );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) mainBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_30Dg_Nearby;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarDg;

                found = taManager.TryAddGannPriceTimeInfo( ref mainBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAtSpecialGannSeq( beginBarDg, mainBarDg );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) mainBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_SpecialSeq;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarDg;

                found = taManager.TryAddGannPriceTimeInfo( ref mainBar, waveRotation );
            }

            return found;
        }

        private static bool InternalCheckPriceCalTimeSquare( TimeSpan period, PeriodXTaManager taManager, ref SBar beginBar, ref SBar squaredBar, double beginBarDg, double mainBarCalDg, bool isMajor, TaGannPriceTimeType gannPriceType )
        {
            var squareDegree = FinancialHelper.DoTheySquareAt45Dg( beginBarDg, mainBarCalDg );

            bool found = false;

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) squaredBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_45Dg_Calendar;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarCalDg;

                found = taManager.TryAddGannPriceTimeInfo( ref squaredBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAt45DgNearby( period, beginBarDg, mainBarCalDg, isMajor );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) squaredBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_45Dg_Nearby_Calendar;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarCalDg;

                found = taManager.TryAddGannPriceTimeInfo( ref squaredBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAt30Dg( beginBarDg, mainBarCalDg );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) squaredBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_30Dg_Calendar;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarCalDg;

                found = taManager.TryAddGannPriceTimeInfo( ref squaredBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAt30DgNearby( period, beginBarDg, mainBarCalDg, isMajor );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) squaredBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_30Dg_Nearby_Calendar;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarCalDg;

                found = taManager.TryAddGannPriceTimeInfo( ref squaredBar, waveRotation );
            }

            squareDegree = FinancialHelper.DoTheySquareAtSpecialGannSeq( beginBarDg, mainBarCalDg );

            if ( squareDegree != -1 )
            {
                var waveRotation             = new GannPriceTimeInfo( period, ( int ) squaredBar.BarIndex, gannPriceType );
                waveRotation.BeginBarIndex = ( int ) beginBar.BarIndex;
                waveRotation.BarDiffOrDegree = squareDegree;
                waveRotation.GannPriceTimeDegrees = GannDegreesType.Gann_SpecialSeq_Cal;
                waveRotation.OptionOne = beginBarDg;
                waveRotation.OptionTwo = mainBarCalDg;

                found = taManager.TryAddGannPriceTimeInfo( ref squaredBar, waveRotation );
            }


            return found;
        }


        public FreemindIndicator() : base( typeof( FreemindIndicator ).Name, false, false, true,
                                                                                            new string[ ] {
                                                                                                            "MACD",
                                                                                                            "MACDSignal",
                                                                                                            "MACD2",
                                                                                                            "MACDSignal2",

                                                                                                            "BBMean",
                                                                                                            "InnerBBUpper",
                                                                                                            "InnerBBLower",
                                                                                                            "OuterBBUpper",
                                                                                                            "OuterBBLower",
                                                                                                            "FreemindRsi",
                                                                                                            "FreemindATR",
                                                                                                            "RsiOverBought",
                                                                                                            "RsiOverSold",
                                                                                                            "MFI",
                                                                                                            "CCI",
                                                                                                             "K",
                                                                                                             "D",
                                                                                                             "SMA",
                                                                                                             "EMA5",
                                                                                                             "EMA13",
                                                                                                             "EMA144",
                                                                                                             "AroonUp",
                                                                                                             "AroonDown",
                                                                                                             "PSAR",
                                                                                                             "PivotPoint",
                                                                                                             "FastComas",
                                                                                                             "SlowComas",
                                                                                                             "MarketDirectionNo",
                                                                                                             "ZigZagS0",
                                                                                                             "ZigZagS0High",
                                                                                                             "ZigZagS0Low",
                                                                                                             "ZigZagS1",
                                                                                                             "ZigZagS1High",
                                                                                                             "ZigZagS1Low",
                                                                                                             "ZigZagS2",
                                                                                                             "ZigZagS2High",
                                                                                                             "ZigZagS2Low",
                                                                                                             "ZigZagS3",
                                                                                                             "ZigZagS3High",
                                                                                                             "ZigZagS3Low",
                                                                                                             "ZigZagS4",
                                                                                                             "ZigZagS4High",
                                                                                                             "ZigZagS4Low",
                                                                                                             "ZigZagS5",
                                                                                                             "ZigZagS5High",
                                                                                                             "ZigZagS5Low",
                                                                                                             
                                                                                                             "ZigZagNen05",
                                                                                                             "ZigZagNen05High",
                                                                                                             "ZigZagNen05Low",
                                                                                                             "ZigZagNen15",
                                                                                                             "ZigZagNen15High",
                                                                                                             "ZigZagNen15Low",
                                                                                                             "ZigZagNen30",
                                                                                                             "ZigZagNen30High",
                                                                                                             "ZigZagNen30Low",
                                                                                                             "ZigZagNen60",
                                                                                                             "ZigZagNen60High",
                                                                                                             "ZigZagNen60Low",
                                                                                                             "ZigZagNen120",
                                                                                                             "ZigZagNen120High",
                                                                                                             "ZigZagNen120Low",
                                                                                                             "ZigZagNen240",
                                                                                                             "ZigZagNen240High",
                                                                                                             "ZigZagNen240Low",
                                                                                                             "ZigZagNen480",
                                                                                                             "ZigZagNen480High",
                                                                                                             "ZigZagNen480Low",
                                                                                                             "ZigZagNen1440",
                                                                                                             "ZigZagNen1440High",
                                                                                                             "ZigZagNen1440Low",
                                                                                                             "ZigZagNen7200",
                                                                                                             "ZigZagNen7200High",
                                                                                                             "ZigZagNen7200Low",
                                                                                                        }
                                       )
        {


            _useAsyncCall = true;

            CandleSettings.Add( CandleSettingEnum.BodyLong, new CandleSetting( RangeEnum.RealBodyLength, 10, 1.0 ) );
            CandleSettings.Add( CandleSettingEnum.BodyVeryLong, new CandleSetting( RangeEnum.RealBodyLength, 10, 3.0 ) );
            CandleSettings.Add( CandleSettingEnum.BodyShort, new CandleSetting( RangeEnum.RealBodyLength, 10, 0.7 ) );
            CandleSettings.Add( CandleSettingEnum.BodyDoji, new CandleSetting( RangeEnum.RealBodyLength, 10, 0.1 ) );
            CandleSettings.Add( CandleSettingEnum.ShadowLong, new CandleSetting( RangeEnum.RealBodyLength, 0, 1.0 ) );
            CandleSettings.Add( CandleSettingEnum.ShadowVeryLong, new CandleSetting( RangeEnum.RealBodyLength, 0, 2.0 ) );
            CandleSettings.Add( CandleSettingEnum.ShadowShort, new CandleSetting( RangeEnum.Shadows, 10, 1.0 ) );
            CandleSettings.Add( CandleSettingEnum.ShadowVeryShort, new CandleSetting( RangeEnum.HighLow, 10, 0.2 ) );
            CandleSettings.Add( CandleSettingEnum.Near, new CandleSetting( RangeEnum.HighLow, 5, 0.2 ) );
            CandleSettings.Add( CandleSettingEnum.Far, new CandleSetting( RangeEnum.HighLow, 5, 0.6 ) );
            CandleSettings.Add( CandleSettingEnum.Equal, new CandleSetting( RangeEnum.HighLow, 5, 0.05 ) );

            //this.ChartSeries.Visible = false;

            //this.ChartSeries.ShouldDispalyOnChart = false;


            // TaIndicatorParameters.SetDynamic( "fxGannSwingBarCount", 1 );
            //TaIndicatorParameters.SetDynamic( "fxFastMA", 20 );
            //TaIndicatorParameters.SetDynamic( "fxSlowMA", 40 );
            //TaIndicatorParameters.SetDynamic( "fxSignal", 10 );

            //TaIndicatorParameters.SetDynamic( "ExtDepth", 55 );
            //TaIndicatorParameters.SetDynamic( "ExtBackstep", 89 );
            //TaIndicatorParameters.SetDynamic( "fxSlowMA", 40 );
            //TaIndicatorParameters.SetDynamic( "fxSignal", 10 );

            TaIndicatorParameters.IndicatorParameterUpdatedEvent += new IndicatorParameters.ParameterUpdatedValueDelegate( IndicatorDynamicParametersUpdateEvent );

            InitializeVariables();

            _lastCurrentBarUpdate = DateTime.UtcNow;
        }

        #region LogSource
        //[Browsable(false)]
        [Display(
            ResourceType = typeof( LocalizedStrings ),
            Name = LocalizedStrings.IdKey,
            Description = LocalizedStrings.IdKey,
            GroupName = LocalizedStrings.LoggingKey,
            Order = 1000 )]
        [ReadOnly( true )]
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        private string _LogSourceName;

        /// <inheritdoc />
        [ReadOnly( true )]
        
        string ILogSource.Name
        {
            get => _LogSourceName;
            set
            {
                if ( value.IsEmpty() )
                    throw new ArgumentNullException( nameof( value ) );

                _LogSourceName = value;
            }
        }

        private ILogSource _parent;

        /// <inheritdoc />
        [Browsable( false )]
        public ILogSource Parent
        {
            get => _parent;
            set
            {
                if ( value == _parent )
                    return;

                if ( value != null && _parent != null )
                    throw new ArgumentException( "", nameof( value ) );

                if ( value == this )
                    throw new ArgumentException( LocalizedStrings.CyclicDependency.Put( this ), nameof( value ) );

                _parent = value;

                if ( _parent == null )
                    ParentRemoved?.Invoke( this );
            }
        }

        /// <inheritdoc />
        public event Action<ILogSource> ParentRemoved;

        /// <inheritdoc />
        
        public virtual LogLevels LogLevel { get; set; } = LogLevels.Inherit;

        /// <inheritdoc />
        [Browsable( false )]
        public virtual DateTimeOffset CurrentTime => TimeHelper.NowWithOffset;

        /// <inheritdoc />
        [Browsable( false )]
        public bool IsRoot { get; set; }

        private Action<LogMessage> _log;

        /// <inheritdoc />
        public event Action<LogMessage> Log
        {
            add => _log += value;
            remove => _log -= value;
        }

        /// <summary>
        /// To call the event <see cref="ILogSource.Log"/>.
        /// </summary>
        /// <param name="message">A debug message.</param>
        protected virtual void RaiseLog( LogMessage message )
        {
            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );

            if ( message.Level < message.Source.LogLevel )
                return;

            _log?.Invoke( message );

            var parent = Parent as ILogReceiver;

            parent?.AddLog( message );
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        void ILogReceiver.AddLog( LogMessage message )
        {
            RaiseLog( message );
        }

        public void LogVerbose( string message, params object [ ] args )
        {
            throw new NotImplementedException();
        }

        public void LogDebug( string message, params object [ ] args )
        {
            this.LogDebug( message, args );
        }        

        public void LogInfo( string message, params object [ ] args )
        {
            this.LogInfo( message, args );
        }

        public void LogWarning( string message, params object [ ] args )
        {
            this.LogWarning( message, args );
        }

        public void LogError( string message, params object [ ] args )
        {
            this.LogError( message, args );
        }
    }
        #endregion

   
}