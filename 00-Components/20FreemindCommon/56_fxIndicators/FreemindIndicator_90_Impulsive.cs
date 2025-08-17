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
using System.Collections.Generic;

#pragma warning disable 414
#pragma warning disable 219

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private PooledList<WavePriceTimeInfo> FindNonOverlappingRegion( TimeSpan period, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, int turningPtsCount, PooledList<KeyValuePair<long, WavePointImportance>> selected, PooledList<WavePriceTimeInfo> corrections )
        {
            var output = new PooledList<WavePriceTimeInfo>( );

            int i = 0;

            int count = corrections.Count;

            if ( count > 1 )
            {
                do
                {
                    var trendRange   = corrections[ i ].TrendMoveRange;
                    var counterRange = corrections[ i + 1 ].CounterTrendMovedRange;
                    var correct = corrections[ i + 1 ];

                    if ( trendRange.Overlaps( counterRange ) )
                    {
                        correct.NonOverlapped = false;
                    }
                    else
                    {
                        correct.NonOverlapped = true;

                        output.Add( correct );
                    }

                    i++;
                }
                while ( i + 1 < count );
            }

            return output;
        }

        private PooledList<ImpulsiveWave> DetectTraditionalImpulsiveWaveUp( int waveScenarioNo, TimeSpan period, long begingWaveTime, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, int turningPtsCount, PooledList<KeyValuePair<long, WavePointImportance>> selected, PooledList<WavePriceTimeInfo> corrections )
        {
            if ( period == TimeSpan.FromMinutes( 15 ) )
            {

            }


            PooledList< ImpulsiveWave > output = new PooledList< ImpulsiveWave >( );

            int i = 0;

            if ( selected != null )
            {
                KeyValuePair< long, WavePointImportance >? wave2 = null;
                KeyValuePair< long, WavePointImportance >? wave4 = null;

                PooledList<FibLevelInfo> wave2Retracement              = null;
                PooledList<FibLevelInfo> wave3Projection               = null;

                PooledList<FibLevelInfo> wave3CProjection              = null;
                PooledList<FibLevelInfo> wave4Retracement              = null;
                PooledList<FibLevelInfo> wave5Projection               = null;

                WaveSRLineResponse wave2_Response                = default;
                WaveSRLineResponse wave3A_Response               = default;
                WaveSRLineResponse wave3B_Response               = default;
                WaveSRLineResponse wave3C_Response               = default;
                WaveSRLineResponse wave4_Response                = default;
                WaveSRLineResponse wave5C_Response               = default;

                bool foundWave1    = false;
                bool foundWave2    = false;
                bool foundWave3    = false;
                bool foundWave3a   = false;
                bool foundWave3b   = false;
                bool foundWave4    = false;
                bool foundWave5    = false;
                bool hasDeep3b     = false;

                long wave0        = -1;
                long wave1        = -1;

                SBar bar0 = default;
                SBar bar1 = default;
                SBar bar2 = default;
                SBar bar3c = default;
                SBar bar3a = default;
                SBar bar3b = default;
                SBar bar4 = default;
                SBar bar5 = default;

                double len_0_1    = 0;
                double len_1_2    = 0;
                double len_2_3    = 0;
                double len_2_3a   = 0;
                double len_3a_3b  = 0;
                double len_3b_3c  = 0;
                double len_0_3    = 0;
                double len_3_4    = 0;
                double len_4_5    = 0;

                double wave2Ret   = 0;
                double wave4Ret   = 0;
                double wave3cExp  = 0;
                double wave5Exp   = 0;

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                * 
                *  Step 0. We w
                * 
                * ------------------------------------------------------------------------------------------------------------------------------------------- 
                */
                /// <image url="$(SolutionDir)\Pictures\NonOverlappedRegions.jpg" />
                var potentialWave3 = FindNonOverlappingRegion( period, begin, end, turningPtsCount, selected, corrections );

                if ( potentialWave3.Count > 0 )
                {
                    switch ( potentialWave3.Count )
                    {
                        case 1:
                        {

                        }
                        break;

                        case 2:
                        {
                            ref SBar redBar0 = ref Bars.GetBarByTime(  begingWaveTime );

                            ref var redBar0Wave = ref redBar0.GetWaveFromScenario( waveScenarioNo );

                            if ( redBar0Wave.HasElliottWave )
                            {
                                HewPredictionTargets predictionTargets = null;

                                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
                                if ( aa == null )
                                    return null;

                                var hewManager = (HewManager) aa.HewManager;

                                var redBar0WaveInfo = hewManager.GetWaveOfHigestDegree( waveScenarioNo, period, begingWaveTime );

                                if ( redBar0WaveInfo.HasValue )
                                {
                                    var value = redBar0WaveInfo.Value;
                                    predictionTargets = hewManager.DoHewPredictions_UpWard( waveScenarioNo, period, ref redBar0, ref value );

                                    if ( predictionTargets != null )
                                    {
                                        predictionTargets.Analyze( );
                                    }
                                }                                
                            }

                            var potWaveA = potentialWave3[ 0 ];
                            var potWaveC = potentialWave3[ 1 ];

                            if ( potWaveA.CounterTrendEndIndex == potWaveC.TrendBeginIndex )
                            {
                                // These two segments are connected together. We might have found Wave A B C of Wave 3.
                                // In the potWaveA, we need to see if we have both Wave 1, 2 in there.
                                // And in the potWaveC, we need to see if we have Wave 4 in there ?
                                var C2ARatio = potWaveA.TrendMovedPips / potWaveC.TrendMovedPips * 100;

                                var inBtwWaves = _completeWaveImportanceCopy.Where( x => x.Key > potWaveA.TrendBeginIndex && x.Key < potWaveA.TrendEndIndex ).OrderBy( x => x.Key ).ToList( );

                                if ( inBtwWaves.Count > 0 )
                                {

                                }
                            }
                            else
                            {
                                //![](531C39E1C811D1AF2AED69215DF7536A.png;;;0.02147,0.01754)
                                ((SBar, WavePointImportance) begin, (SBar, WavePointImportance) end ) midHighImpt = FindHighestWavePointsBtw( period, potWaveA.CounterTrendEndIndex, potWaveC.TrendBeginIndex );


                            }
                        }
                        break;

                        default:
                        {

                        }
                        break;
                    }






                }

                if ( i < turningPtsCount )
                {
                    wave0 = selected[ i ].Key;
                }

                if ( ( i + 1 ) < turningPtsCount )
                {
                    wave1 = selected[ i + 1 ].Key;

                    Bars.GetBarByTime( wave0 );
                    Bars.GetBarByTime( wave1 );

                    len_0_1 = bar1.High - bar0.Low;

                    foundWave1 = true;
                }

                wave2 = GetLowestCorrectionWithinUpTrend( selected.ToArray( ), ref bar0, ref bar1 );

                if ( wave2.HasValue )
                {
                    Bars.GetBarByTime( wave2.Value.Key );

                    var wave2RetracementObj = new FibLevelsCollection( FibonacciType.Wave2Retracement, bar0.LowTimeValue, bar1.HighTimeValue );

                    wave2Retracement = wave2RetracementObj.FibLevels;

                    harmonicEWave.GetClosestSRLine( FibonacciType.Wave2Retracement, ref bar2, wave2Retracement, TrendDirection.DownTrend, out wave2_Response );

                    len_1_2 = bar1.High - bar2.Low;

                    if ( len_0_1 > 0 )
                    {
                        wave2Ret = ( len_1_2 / len_0_1 ) * 100;
                    }

                    foundWave2 = true;
                }
                else
                {
                    foundWave2 = false;
                }

                if ( foundWave2 )
                {
                    var afterWave2 = selected.Where( x =>
                    {
                        if ( x.Key > wave2.Value.Key && x.Key < end.Key && x.Value.Signal == TASignal.WAVE_PEAK  )
                        {
                            return true;
                        }

                        return false;
                    } );

                    if ( foundWave1 && foundWave2 )
                    {
                        var wave3ProjectionObj = new FibLevelsCollection( FibonacciType.Wave3Projection, bar0.LowTimeValue, bar1.HighTimeValue, bar2.LowTimeValue );

                        wave3Projection = wave3ProjectionObj.FibLevels;
                    }



                    foreach ( var nextWave in afterWave2 )
                    {
                        Bars.GetBarByTime( nextWave.Key );

                        if ( wave3Projection.Count > 0 )
                        {
                            harmonicEWave.GetClosestSRLine( FibonacciType.Wave3Projection, ref bar3c, wave3Projection, TrendDirection.Uptrend, out wave3C_Response );
                        }

                        len_2_3 = bar3c.High - bar2.Low;

                        if ( len_0_1 > 0 )
                        {
                            if ( ( len_2_3 / len_0_1 ) * 100 >= 176.4 )
                            {
                                foundWave3 = true;

                                if ( foundWave1 && foundWave2 && foundWave3 )
                                {
                                    foundWave3a = foundWave3b = DetectABCOfWave3_UpTrend( ref bar0, ref bar1, ref bar2, out bar3a, out wave3A_Response, out bar3b, out wave3B_Response, bar3c, out wave3C_Response, out hasDeep3b );
                                }

                                len_0_3 = bar3c.High - bar0.Low;

                                var afterWave3 = selected.Where(
                                                                    x =>
                                                                    {
                                                                        if ( x.Key > nextWave.Key && x.Value.Signal == TASignal.WAVE_TROUGH )
                                                                        {
                                                                            return true;
                                                                        }

                                                                        return false;
                                                                    }
                                                               );

                                if ( afterWave3.Count( ) > 0 )
                                {
                                    var wave4RetracementObj = new FibLevelsCollection( FibonacciType.Wave4Retracement, bar0.LowTimeValue, bar3c.HighTimeValue );
                                    wave4Retracement = wave4RetracementObj.FibLevels;

                                    wave4 = GetLowestCorrectionWithinUpTrend( afterWave3.ToArray( ), ref bar2, ref bar3c );

                                    if ( wave4.HasValue )
                                    {
                                        Bars.GetBarByTime( wave4.Value.Key );
                                        harmonicEWave.GetClosestSRLine( FibonacciType.Wave4Retracement, ref bar4, wave4Retracement, TrendDirection.DownTrend, out wave4_Response );

                                        len_3_4 = bar3c.High - bar4.Low;

                                        if ( len_0_3 > 0 )
                                        {
                                            wave4Ret = len_3_4 / len_0_3 * 100;

                                            var altSum = ( wave4Ret + wave2Ret );

                                            foundWave4 = ( altSum >= 80 ) && ( altSum <= 100 );
                                        }

                                        if ( foundWave4 )
                                        {
                                            Bars.GetBarByTime( end.Key );

                                            var wave5ProjectionObj = new FibLevelsCollection( FibonacciType.Wave5Projection, bar0.LowTimeValue, bar3c.HighTimeValue, bar4.LowTimeValue );
                                            wave5Projection = wave5ProjectionObj.FibLevels;
                                            harmonicEWave.GetClosestSRLine( FibonacciType.Wave5Projection, ref bar5, wave5Projection, TrendDirection.Uptrend, out wave5C_Response );

                                            len_4_5 = bar5.High - bar4.Low;

                                            if ( len_0_3 > 0 )
                                            {
                                                wave5Exp = len_4_5 / len_0_3 * 100;

                                                foundWave5 = ( ( wave5Exp >= 41.40 ) && ( wave5Exp <= 105.60 ) );
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foundWave3 = false;
                                }
                            }
                            else
                            {
                                foundWave3 = false;
                            }
                        }

                        if ( foundWave1 && foundWave2 && foundWave3 && foundWave4 && foundWave5 )
                        {


                            ImpulsiveWave wave = null;

                            if ( foundWave3a && foundWave3b )
                            {
                                wave = new ImpulsiveWave( TrendDirection.Uptrend, ref bar0, ref bar1, ref bar2, ref bar3a, ref bar3b, ref bar3c, ref bar4, ref bar5 );
                            }
                            else
                            {
                                wave = new ImpulsiveWave( TrendDirection.Uptrend, ref bar0, ref bar1, ref bar2, ref bar3c, ref bar4, ref bar5 );
                            }


                            output.Add( wave );
                        }
                    }
                }
            }

            return output;
        }

        private ((SBar, WavePointImportance), (SBar, WavePointImportance)) FindHighestWavePointsBtw( TimeSpan period, long beginTime, long endTime )
        {
            ((SBar, WavePointImportance) begin, (SBar, WavePointImportance) end ) output = default;

            var waveImpt = _hews.GetAscendingWaveImportanceClone( period );

            var wavePt = waveImpt.Where( x => x.Key > beginTime && x.Key < endTime ).OrderBy( x => x.Key ).ToList( );

            int highestWaveImpt = -1;

            for ( int i = 0; i < wavePt.Count; i++ )
            {
                if ( wavePt[ i ].Value.WaveImportance > highestWaveImpt )
                {
                    highestWaveImpt = wavePt[ i ].Value.WaveImportance;
                }
            }

            SBar beginBar = default;
            WavePointImportance beginWaveImpt = null;

            SBar endBar = default;
            WavePointImportance endWaveImpt = null;
            

            for ( int i = 0; i < wavePt.Count; i++ )
            {
                if ( wavePt[ i ].Value.WaveImportance == highestWaveImpt )
                {
                    ref SBar currentBar = ref Bars.GetBarByTime( wavePt[ i ].Key );

                    if ( beginBar == default )
                    {
                        beginBar = currentBar;
                        beginWaveImpt = wavePt[ i ].Value;

                        continue;
                    }

                    if ( beginBar != default && endBar == default )
                    {
                        endBar = currentBar;
                        endWaveImpt = wavePt[ i ].Value;
                    }

                    if ( beginBar != default && endBar != default )
                    {
                        output = ((beginBar, beginWaveImpt), (endBar, endWaveImpt));

                        beginBar = default;
                        endBar = default;
                    }
                }
            }

            return output;
        }

        public PooledList<((SBar, WavePointImportance), (SBar, WavePointImportance))> FindHighestWaveImportancePairs( PooledList<KeyValuePair<long, WavePointImportance>> wavePt )
        {
            PooledList< ((SBar, WavePointImportance), (SBar, WavePointImportance)) > output = new PooledList<((SBar, WavePointImportance), (SBar, WavePointImportance))>();



            return output;
        }

        private PooledList<ImpulsiveWave> DetectTraditionalImpulsiveWaveDown( TimeSpan period, long begingWaveTime, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, int turningPtsCount, PooledList<KeyValuePair<long, WavePointImportance>> selected, ref PooledList<WavePriceTimeInfo> corrections )
        {
            ref SBar redBar0 = ref
            Bars.GetBarByTime(  begingWaveTime );

            var wave3 = FindNonOverlappingRegion( period, begin, end, turningPtsCount, selected, corrections );

            if ( wave3.Count > 0 )
            {

            }

            PooledList< ImpulsiveWave > output = new PooledList< ImpulsiveWave >( );

            int i = 0;

            if ( selected != null )
            {
                KeyValuePair< long, WavePointImportance >? wave2 = null;
                KeyValuePair< long, WavePointImportance >? wave4 = null;

                PooledList<FibLevelInfo> wave2Retracement  = null;
                PooledList<FibLevelInfo> wave3Projection   = null;
                PooledList<FibLevelInfo> wave3BRetracement = null;
                PooledList<FibLevelInfo> wave3CProjection  = null;
                PooledList<FibLevelInfo> wave4Retracement  = null;
                PooledList<FibLevelInfo> wave5Projection   = null;

                WaveSRLineResponse wave2_Response    = default;
                WaveSRLineResponse wave3A_Response   = default;
                WaveSRLineResponse wave3B_Response   = default;
                WaveSRLineResponse wave3C_Response   = default;
                WaveSRLineResponse wave4_Response    = default;
                WaveSRLineResponse wave5C_Response   = default;


                bool foundWave1    = false;
                bool foundWave2    = false;
                bool foundWave3    = false;
                bool foundWave3a   = false;
                bool foundWave3b   = false;
                bool foundWave4    = false;
                bool foundWave5    = false;
                bool hasDeep3b     = false;

                long wave0         = -1;
                long wave1         = -1;

                SBar bar0 = default;
                SBar bar1 = default;
                SBar bar2 = default;
                SBar bar3a = default;
                SBar bar3b = default;
                SBar bar3c = default;

                SBar bar4 = default;
                SBar bar5 = default;

                double len_0_1     = 0;
                double len_1_2     = 0;
                double len_2_3     = 0;
                double len_2_3a    = 0;
                double len_3a_3b   = 0;
                double len_3b_3c   = 0;
                double len_0_3     = 0;
                double len_3_4     = 0;
                double len_4_5     = 0;

                double wave2Ret    = 0;
                double wave4Ret    = 0;
                double wave3cExp   = 0;
                double wave5Exp    = 0;



                if ( i < turningPtsCount )
                {
                    wave0 = selected[ i ].Key;
                }

                if ( ( i + 1 ) < turningPtsCount )
                {
                    wave1 = selected[ i + 1 ].Key;

                    Bars.GetBarByTime( wave0 );
                    Bars.GetBarByTime( wave1 );

                    len_0_1 = bar0.High - bar1.Low;

                    foundWave1 = true;
                }

                wave2 = GetHighestCorrectionWithinDowntrend( selected.ToArray( ), ref bar1, ref bar0 );

                if ( wave2.HasValue )
                {
                    Bars.GetBarByTime( wave2.Value.Key );

                    var wave2RetracementObj = new FibLevelsCollection( FibonacciType.Wave2Retracement, bar0.HighTimeValue, bar1.LowTimeValue );

                    wave2Retracement = wave2RetracementObj.FibLevels;

                    harmonicEWave.GetClosestSRLine( FibonacciType.Wave2Retracement, ref bar2, wave2Retracement, TrendDirection.Uptrend, out wave2_Response );

                    len_1_2 = bar2.High - bar1.Low;

                    if ( len_0_1 > 0 )
                    {
                        wave2Ret = ( len_1_2 / len_0_1 ) * 100;
                    }

                    foundWave2 = true;
                }
                else
                {
                    foundWave2 = false;
                }

                if ( foundWave2 )
                {
                    var afterWave2 = selected.Where( x =>
                    {
                        if ( x.Key > wave2.Value.Key && x.Value.Signal == TASignal.WAVE_TROUGH  )
                        {
                            return true;
                        }

                        return false;
                    } );

                    if ( foundWave1 && foundWave2 )
                    {
                        var wave3ProjectionObj = new FibLevelsCollection( FibonacciType.Wave3Projection, bar0.HighTimeValue, bar1.LowTimeValue, bar2.HighTimeValue );

                        wave3Projection = wave3ProjectionObj.FibLevels;
                    }



                    foreach ( var nextWave in afterWave2 )
                    {
                        Bars.GetBarByTime( nextWave.Key );

                        if ( wave3Projection.Count > 0 )
                        {
                            harmonicEWave.GetClosestSRLine( FibonacciType.Wave3Projection, ref bar3c, wave3Projection, TrendDirection.DownTrend, out wave3C_Response );
                        }

                        len_2_3 = bar2.High - bar3c.Low;

                        if ( len_0_1 > 0 )
                        {
                            if ( ( len_2_3 / len_0_1 ) * 100 >= 176.4 )
                            {
                                foundWave3 = true;

                                if ( foundWave1 && foundWave2 && foundWave3 )
                                {
                                    foundWave3a = foundWave3b = DetectABCOfWave3_DownTrend( ref bar0, ref bar1, ref bar2, out bar3a, out wave3A_Response, out bar3b, out wave3B_Response, ref bar3c, out wave3C_Response, out hasDeep3b );
                                }

                                len_0_3 = bar0.High - bar3c.Low;

                                var afterWave3 = selected.Where(
                                                                    x =>
                                                                    {
                                                                        if ( x.Key > nextWave.Key && x.Value.Signal == TASignal.WAVE_PEAK )
                                                                        {
                                                                            return true;
                                                                        }

                                                                        return false;
                                                                    }
                                                               );

                                if ( afterWave3.Count( ) > 0 )
                                {
                                    var wave4RetracementObj = new FibLevelsCollection( FibonacciType.Wave4Retracement, bar0.HighTimeValue, bar3c.LowTimeValue );
                                    wave4Retracement = wave4RetracementObj.FibLevels;

                                    wave4 = GetHighestCorrectionWithinDowntrend( afterWave3.ToArray( ), ref bar3c, ref bar2 );

                                    if ( wave4.HasValue )
                                    {
                                        Bars.GetBarByTime( wave4.Value.Key );
                                        harmonicEWave.GetClosestSRLine( FibonacciType.Wave4Retracement, ref bar4, wave4Retracement, TrendDirection.Uptrend, out wave4_Response );

                                        len_3_4 = bar4.High - bar3c.Low;

                                        if ( len_0_3 > 0 )
                                        {
                                            wave4Ret = len_3_4 / len_0_3 * 100;

                                            var altSum = ( wave4Ret + wave2Ret );

                                            foundWave4 = ( altSum >= 80 ) && ( altSum <= 100 );
                                        }

                                        if ( foundWave4 )
                                        {
                                            Bars.GetBarByTime( end.Key );

                                            var wave5ProjectionObj = new FibLevelsCollection( FibonacciType.Wave5Projection, bar0.HighTimeValue, bar3c.LowTimeValue, bar4.HighTimeValue );
                                            wave5Projection = wave5ProjectionObj.FibLevels;
                                            harmonicEWave.GetClosestSRLine( FibonacciType.Wave5Projection, ref bar5, wave5Projection, TrendDirection.DownTrend, out wave5C_Response );

                                            len_4_5 = bar4.High - bar5.Low;

                                            if ( len_0_3 > 0 )
                                            {
                                                wave5Exp = len_4_5 / len_0_3 * 100;

                                                foundWave5 = ( ( wave5Exp >= 41.40 ) && ( wave5Exp <= 105.60 ) );
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foundWave3 = false;
                                }
                            }
                            else
                            {
                                foundWave3 = false;
                            }
                        }

                        if ( foundWave1 && foundWave2 && foundWave3 && foundWave4 && foundWave5 )
                        {
                            var bar2Time = bar2.LinuxTime;
                            var bar3Time =  bar3c.LinuxTime;
                            var inBtwWave2_3 = _completeWaveImportanceCopy.Where( x => x.Key > bar2Time && x.Key <  bar3Time ).OrderBy( x => x.Key ).ToList();

                            if ( inBtwWave2_3.Count > 0 )
                            {
                                int j = 0;

                                while ( j < inBtwWave2_3.Count )
                                {
                                    var trough1 = inBtwWave2_3[ j ];

                                    if ( trough1.Value.Signal == TASignal.WAVE_TROUGH )
                                    {
                                        Bars.GetBarByTime( trough1.Key );

                                        harmonicEWave.GetClosestSRLine( FibonacciType.Wave3Projection, ref bar3a, wave3Projection, TrendDirection.DownTrend, out wave3A_Response );

                                        len_2_3a = bar2.High - bar3a.Low;

                                        if ( ( j + 1 ) < inBtwWave2_3.Count )
                                        {
                                            var peak                 = inBtwWave2_3[ j + 1 ];

                                            Bars.GetBarByTime( peak.Key );

                                            var wave3bRetracementObj = new FibLevelsCollection( FibonacciType.ABCWaveBRetracement, bar2.HighTimeValue, bar3a.LowTimeValue );
                                            wave3BRetracement = wave3bRetracementObj.FibLevels;

                                            harmonicEWave.GetClosestSRLine( FibonacciType.ABCWaveBRetracement, ref bar3b, wave3BRetracement, TrendDirection.Uptrend, out wave3B_Response );

                                            var wave3CExpansionObj   = new FibLevelsCollection( FibonacciType.WaveCProjection, bar2.HighTimeValue, bar3a.LowTimeValue, bar3b.HighTimeValue );
                                            wave3CProjection = wave3CExpansionObj.FibLevels;
                                            harmonicEWave.GetClosestSRLine( FibonacciType.WaveCProjection, ref bar3c, wave3CProjection, TrendDirection.DownTrend, out wave3C_Response );

                                            len_3a_3b = bar3b.High - bar3a.Low;

                                            len_3b_3c = bar3b.High - bar3c.Low;

                                            wave3cExp = len_3b_3c / len_2_3a * 100;

                                            if ( wave3cExp > 76 )
                                            {
                                                foundWave3a = true;
                                                foundWave3b = true;
                                            }
                                        }
                                    }

                                    j++;
                                }
                            }



                            ImpulsiveWave wave = null;

                            if ( foundWave3a && foundWave3b )
                            {
                                wave = new ImpulsiveWave( TrendDirection.Uptrend, ref bar0, ref bar1, ref bar2, ref bar3a, ref bar3b, ref bar3c, ref bar4, ref bar5 );
                            }
                            else
                            {
                                wave = new ImpulsiveWave( TrendDirection.Uptrend, ref bar0, ref bar1, ref bar2, ref bar3c, ref bar4, ref bar5 );
                            }


                            output.Add( wave );
                        }
                    }
                }
            }

            return output;
        }

        private bool DetectABCOfWave3_UpTrend( ref SBar bar0, ref SBar bar1, ref SBar bar2, out SBar bar3a, out WaveSRLineResponse wave3A_Response, out SBar bar3b, out WaveSRLineResponse wave3B_Response, SBar bar3c, out WaveSRLineResponse wave3C_Response, out bool hasDeep3b )
        {
            bar3a = default;
            bar3b = default;
            hasDeep3b = false;
            wave3A_Response = default;
            wave3B_Response = default;
            wave3C_Response = default;

            bool foundWave3a        = false;
            bool foundWave3b        = false;

            double wave3cExp        = 0;
            double wave3bRet        = 0;
            double wave3cTo3a       = 0;

            PooledList< WaveABCInfo > matches = new PooledList< WaveABCInfo >( );

            var wave3ProjectionObj  = new FibLevelsCollection( FibonacciType.Wave3Projection, bar0.LowTimeValue, bar1.HighTimeValue, bar2.LowTimeValue );

            PooledList<FibLevelInfo> wave3Projection   = null;
            PooledList<FibLevelInfo> wave3BRetracement = null;
            PooledList<FibLevelInfo> wave3CProjection  = null;


            double len_2_3a   = 0;
            double len_3a_3b  = 0;
            double len_3b_3c  = 0;


            wave3Projection = wave3ProjectionObj.FibLevels;

            var bar2Time = bar2.LinuxTime;

            var inBtwWave2_3 = _completeWaveImportanceCopy.Where( x => x.Key > bar2Time && x.Key <  bar3c.LinuxTime ).OrderBy( x => x.Key ).ToList();

            if ( inBtwWave2_3.Count > 0 )
            {
                int j = 0;

                while ( j < inBtwWave2_3.Count )
                {
                    var peak = inBtwWave2_3[ j ];

                    if ( peak.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        Bars.GetBarByTime( peak.Key );

                        harmonicEWave.GetClosestSRLine( FibonacciType.Wave3Projection, ref bar3a, wave3Projection, TrendDirection.Uptrend, out wave3A_Response );

                        len_2_3a = bar3a.High - bar2.Low;

                        if ( ( j + 1 ) < inBtwWave2_3.Count )
                        {
                            var trough               = inBtwWave2_3[ j + 1 ];

                            Bars.GetBarByTime( trough.Key );

                            var wave3bRetracementObj = new FibLevelsCollection( FibonacciType.ABCWaveBRetracement, bar2.LowTimeValue, bar3a.HighTimeValue );
                            wave3BRetracement = wave3bRetracementObj.FibLevels;

                            harmonicEWave.GetClosestSRLine( FibonacciType.ABCWaveBRetracement, ref bar3b, wave3BRetracement, TrendDirection.DownTrend, out wave3B_Response );

                            var wave3CExpansionObj   = new FibLevelsCollection( FibonacciType.ABCWaveCProjection, bar2.LowTimeValue, bar3a.HighTimeValue, bar3b.LowTimeValue );
                            wave3CProjection = wave3CExpansionObj.FibLevels;
                            harmonicEWave.GetClosestSRLine( FibonacciType.ABCWaveCProjection, ref bar3c, wave3CProjection, TrendDirection.Uptrend, out wave3C_Response );

                            len_3a_3b = bar3a.High - bar3b.Low;
                            len_3b_3c = bar3c.High - bar3b.Low;

                            wave3bRet = len_3a_3b / len_2_3a * 100;
                            wave3cExp = len_3b_3c / len_2_3a * 100;

                            wave3cTo3a = len_3b_3c / len_2_3a * 100;

                            if ( wave3bRet >= 66 )
                            {
                                hasDeep3b = true;
                            }

                            if ( wave3cExp > 66 && wave3A_Response != default && wave3C_Response != default )
                            {
                                foundWave3a = true;
                                foundWave3b = true;

                                matches.Add( new WaveABCInfo(
                                                            TrendDirection.Uptrend,
                                                            ref bar2,
                                                            ref bar3a,
                                                            ref bar3b,
                                                            ref bar3c,
                                                            wave3A_Response,
                                                            wave3B_Response,
                                                            wave3C_Response,
                                                            hasDeep3b,
                                                            wave3cTo3a
                                                        ) );
                            }
                        }
                    }

                    j++;
                }
            }

            WaveABCInfo bestMatch = null;

            if ( matches.Count > 1 )
            {
                bestMatch = GetBestMatch( matches );
            }
            else if ( matches.Count == 1 )
            {
                bestMatch = matches[ 0 ];
            }

            if ( bestMatch != null )
            {
                bar3a = bestMatch.BarA;
                bar3b = bestMatch.BarB;
                wave3A_Response = bestMatch.ResponseA;
                wave3B_Response = bestMatch.ResponseB;
                hasDeep3b = bestMatch.DeepWaveB;
            }

            return ( foundWave3a );
        }

        private bool DetectABCOfWave3_DownTrend( ref SBar bar0, ref SBar bar1, ref SBar bar2, out SBar bar3a, out WaveSRLineResponse wave3A_Response, out SBar bar3b, out WaveSRLineResponse wave3B_Response, ref SBar bar3c, out WaveSRLineResponse wave3C_Response, out bool hasDeep3b )
        {
            bar3a = default;
            bar3b = default;
            hasDeep3b = false;
            wave3A_Response = default;
            wave3B_Response = default;
            wave3C_Response = default;

            bool foundWave3a  = false;
            bool foundWave3b  = false;

            double wave3cExp  = 0;
            double wave3bRet  = 0;
            double wave3cTo3a = 0;

            PooledList< WaveABCInfo > matches = new PooledList< WaveABCInfo >( );

            var wave3ProjectionObj  = new FibLevelsCollection( FibonacciType.Wave3Projection, bar0.HighTimeValue, bar1.LowTimeValue, bar2.HighTimeValue );

            PooledList<FibLevelInfo> wave3Projection   = null;
            PooledList<FibLevelInfo> wave3BRetracement = null;
            PooledList<FibLevelInfo> wave3CProj        = null;

            double len_2_3a   = 0;
            double len_3a_3b  = 0;
            double len_3b_3c  = 0;


            wave3Projection = wave3ProjectionObj.FibLevels;

            var bar2Time = bar2.LinuxTime;
            var bar3cTime =  bar3c.LinuxTime;

            var inBtwWave2_3 = _completeWaveImportanceCopy.Where( x => x.Key > bar2Time && x.Key < bar3cTime ).OrderBy( x => x.Key ).ToList();

            if ( inBtwWave2_3.Count > 0 )
            {
                int j = 0;

                while ( j < inBtwWave2_3.Count )
                {
                    var trough = inBtwWave2_3[ j ];

                    if ( trough.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        Bars.GetBarByTime( trough.Key );

                        harmonicEWave.GetClosestSRLine( FibonacciType.Wave3Projection, ref bar3a, wave3Projection, TrendDirection.DownTrend, out wave3A_Response );

                        len_2_3a = bar2.High - bar3a.Low;

                        if ( ( j + 1 ) < inBtwWave2_3.Count )
                        {
                            var peak          = inBtwWave2_3[ j + 1 ];

                            Bars.GetBarByTime( peak.Key );

                            var wave3bRetObj  = new FibLevelsCollection( FibonacciType.ABCWaveBRetracement, bar2.HighTimeValue, bar3a.LowTimeValue );
                            wave3BRetracement = wave3bRetObj.FibLevels;

                            harmonicEWave.GetClosestSRLine( FibonacciType.ABCWaveBRetracement, ref bar3b, wave3BRetracement, TrendDirection.Uptrend, out wave3B_Response );

                            var wave3CExp     = new FibLevelsCollection( FibonacciType.ABCWaveCProjection, bar2.HighTimeValue, bar3a.LowTimeValue, bar3b.HighTimeValue );
                            wave3CProj = wave3CExp.FibLevels;

                            harmonicEWave.GetClosestSRLine( FibonacciType.ABCWaveCProjection, ref bar3c, wave3CProj, TrendDirection.DownTrend, out wave3C_Response );

                            len_3a_3b = bar3b.High - bar3a.Low;
                            len_3b_3c = bar3b.High - bar3c.Low;

                            wave3bRet = len_3a_3b / len_2_3a * 100;
                            wave3cExp = len_3b_3c / len_2_3a * 100;
                            wave3cTo3a = len_3b_3c / len_2_3a * 100;


                            if ( wave3bRet > 66 )
                            {
                                hasDeep3b = true;
                            }

                            if ( wave3cExp > 66 && wave3A_Response != default && wave3C_Response != default )
                            {
                                foundWave3a = true;
                                foundWave3b = true;

                                matches.Add( new WaveABCInfo(
                                                            TrendDirection.DownTrend,
                                                            ref bar2,
                                                            ref bar3a,
                                                            ref bar3b,
                                                            ref bar3c,
                                                            wave3A_Response,
                                                            wave3B_Response,
                                                            wave3C_Response,
                                                            hasDeep3b,
                                                            wave3cTo3a
                                                        ) );
                            }


                        }
                    }

                    j++;
                }
            }

            WaveABCInfo bestMatch = null;

            if ( matches.Count > 1 )
            {
                bestMatch = GetBestMatch( matches );
            }
            else if ( matches.Count == 1 )
            {
                bestMatch = matches[ 0 ];
            }

            if ( bestMatch != null )
            {
                bar3a = bestMatch.BarA;
                bar3b = bestMatch.BarB;
                wave3A_Response = bestMatch.ResponseA;
                wave3B_Response = bestMatch.ResponseB;
                hasDeep3b = bestMatch.DeepWaveB;
            }

            return ( foundWave3a );
        }

    }
}
