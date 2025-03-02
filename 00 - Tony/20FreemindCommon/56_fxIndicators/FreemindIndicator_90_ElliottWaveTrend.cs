
using fx.Definitions;
using fx.Algorithm;
using fx.Bars;
using System;
using fx.Collections;
using System.Linq;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private void DetectImpulsiveInTrending_Upward( int waveScenarioNo, Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves, int currentWaveImportance )
        {
            var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfUptrend.HasValue )
            {
                ref SBar upbar = ref
                Bars.GetBarByTime(  beginOfUptrend.Value.Key );

                if ( beginOfUptrend.Value.Key > oldestWave.Key )
                {
                    GetTrendingWaveType_Uptrend( waveScenarioNo, symbol, period, taManager, beginOfUptrend.Value.Key, latestWave.Key, currentWaveImportance );

                    var priorWaves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                    var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, beginOfUptrend.Value, priorWaves );

                    if ( beginOfDowntrend.HasValue )
                    {
                        ref SBar downBar = ref
                        Bars.GetBarByTime(  beginOfDowntrend.Value.Key );

                        if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                        {
                            GetTrendingWaveType_Downtrend( waveScenarioNo, symbol, period, taManager, beginOfDowntrend.Value.Key, beginOfUptrend.Value.Key, currentWaveImportance );

                            priorWaves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                            DetectImpulsiveInTrending_Upward( waveScenarioNo, symbol, period, taManager, oldestWave, beginOfDowntrend.Value, priorWaves, currentWaveImportance );
                        }
                    }
                }
            }
            else
            {

            }
        }

        private WaveType GetTrendingWaveType_Uptrend( int waveScenarioNo, Security symbol, TimeSpan period, PeriodXTaManager taManager, long begingWaveTime, long endWaveTime, int inputWaveImportance )
        {
            var inBtwWaves      = _completeWaveImportanceCopy.Where( x => x.Key >= begingWaveTime && x.Key < endWaveTime ).OrderBy( x => x.Key ).ToPooledList( );
            var inBtwGannSwings = _completeGannImportanceCopy.Where( x => x.Key >= begingWaveTime && x.Key < endWaveTime ).OrderBy( x => x.Key ).ToPooledList( );

            var begin           = _completeWaveImportanceCopy.Where( x => x.Key == begingWaveTime ).FirstOrDefault( );
            var end             = _completeWaveImportanceCopy.Where( x => x.Key == endWaveTime    ).FirstOrDefault( );

            int turningPtsCount = inBtwWaves.Count;

            PooledList<KeyValuePair<long, WavePointImportance>> selected = null;
            PooledList<WavePriceTimeInfo> corrections = null;

            if ( turningPtsCount > 2 )
            {
                selected = inBtwWaves;
                corrections = GetCorrectionsBtwXY_Uptrend( symbol, period, inBtwWaves.ToArray( ), begin, end, inputWaveImportance );
            }
            else if ( inBtwGannSwings.Count > 2 )
            {
                turningPtsCount = inBtwGannSwings.Count;
                selected = inBtwGannSwings;
                corrections = GetCorrectionsBtwXY_Uptrend( symbol, period, inBtwGannSwings.ToArray( ), begin, end, inputWaveImportance );
            }

            var impulsiveWaves = DetectTraditionalImpulsiveWaveUp( waveScenarioNo, period, begingWaveTime, begin, end, turningPtsCount, selected,  corrections );

            if ( impulsiveWaves.Count > 0 )
            {
                _periodXTaManager.AddImplusiveWaves( WaveType.Correction, impulsiveWaves );

                return WaveType.Impulsive5Waves;
            }

            return WaveType.Correction;
        }

        private WaveType GetTrendingWaveType_Downtrend( int waveScenarioNo, Security symbol, TimeSpan period, PeriodXTaManager taManager, long begingWaveTime, long endWaveTime, int inputWaveImportance )
        {
            var inBtwWaves      = _completeWaveImportanceCopy.Where( x => x.Key >= begingWaveTime && x.Key < endWaveTime ).OrderBy( x => x.Key ).ToPooledList( );
            var inBtwGannSwings = _completeGannImportanceCopy.Where( x => x.Key >= begingWaveTime && x.Key < endWaveTime ).OrderBy( x => x.Key ).ToPooledList( );

            var begin           = _completeWaveImportanceCopy.Where( x => x.Key == begingWaveTime ).FirstOrDefault( );
            var end             = _completeWaveImportanceCopy.Where( x => x.Key == endWaveTime ).FirstOrDefault( );

            int turningPtsCount = inBtwWaves.Count;

            PooledList<KeyValuePair<long, WavePointImportance>> selected = null;
            PooledList<WavePriceTimeInfo> corrections = null;

            if ( turningPtsCount > 2 )
            {
                selected = inBtwWaves;
                corrections = GetCorrectionsBtwXY_DownTrend( symbol, period, inBtwWaves.ToArray( ), begin, end, inputWaveImportance );
            }
            else if ( inBtwGannSwings.Count > 2 )
            {
                turningPtsCount = inBtwGannSwings.Count;
                selected = inBtwGannSwings;
                corrections = GetCorrectionsBtwXY_DownTrend( symbol, period, inBtwGannSwings.ToArray( ), begin, end, inputWaveImportance );
            }

            var impulsiveWaves = DetectTraditionalImpulsiveWaveUp( waveScenarioNo,period, begingWaveTime, begin, end, turningPtsCount, selected, corrections );

            if ( impulsiveWaves.Count > 0 )
            {
                _periodXTaManager.AddImplusiveWaves( WaveType.Correction, impulsiveWaves );

                return WaveType.Impulsive5Waves;
            }

            return WaveType.Correction;
        }



        private void DetectImpulsiveInTrending_Downward( int waveScenarioNo, Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves, int currentWaveImportance )
        {
            var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfDowntrend.HasValue )
            {
                if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                {
                    GetTrendingWaveType_Downtrend( waveScenarioNo, symbol, period, taManager, beginOfDowntrend.Value.Key, latestWave.Key, currentWaveImportance );

                    var priorWaves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                    var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, beginOfDowntrend.Value, priorWaves );

                    if ( beginOfUptrend.HasValue )
                    {
                        if ( beginOfUptrend.Value.Key > oldestWave.Key )
                        {
                            GetTrendingWaveType_Uptrend( waveScenarioNo, symbol, period, taManager, beginOfUptrend.Value.Key, beginOfDowntrend.Value.Key, currentWaveImportance );

                            priorWaves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                            DetectImpulsiveInTrending_Downward( waveScenarioNo, symbol, period, taManager, oldestWave, beginOfUptrend.Value, priorWaves, currentWaveImportance );
                        }
                    }
                }
            }
            else
            {

            }

        }
    }
}
