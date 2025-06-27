using fx.Common;


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

        private PooledList<WaveABCInfo> DetectZigZag_Upward( KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, int turningPtsCount, PooledList<KeyValuePair<long, WavePointImportance>> selected )
        {


            return null;
        }

        private PooledList<WaveABCInfo> DetectZigZag_Downward( KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, int turningPtsCount, PooledList<KeyValuePair<long, WavePointImportance>> selected )
        {


            return null;
        }


        private WaveType GetCorrectionWaveType_Upward( Security symbol, TimeSpan period, PeriodXTaManager taManager, WavePriceTimeInfo correction, int inputWaveImportance )
        {
            var inBtwWaves      = _completeWaveImportanceCopy.Where( x => x.Key >= correction.TrendEndIndex && x.Key <=  correction.CounterTrendEndIndex ).OrderBy( x => x.Key ).ToPooledList();
            var inBtwGannSwings = _completeGannImportanceCopy.Where( x => x.Key >= correction.TrendEndIndex && x.Key <= correction.CounterTrendEndIndex ).OrderBy( x => x.Key ).ToPooledList();

            var begin           = _completeWaveImportanceCopy.Where( x => x.Key == correction.TrendEndIndex ).FirstOrDefault( );
            var end             = _completeWaveImportanceCopy.Where( x => x.Key == correction.CounterTrendEndIndex ).FirstOrDefault( );

            int turningPtsCount = inBtwWaves.Count;

            PooledList< KeyValuePair< long, WavePointImportance >> selected = null;
            PooledList < WavePriceTimeInfo > corrections = null;

            if ( turningPtsCount > 2 )
            {
                selected    = inBtwWaves;
                corrections = GetCorrectionsBtwXY_Uptrend( symbol, period, inBtwWaves.ToArray( ), begin, end, inputWaveImportance );
            }
            else if ( inBtwGannSwings.Count > 2 )
            {
                turningPtsCount = inBtwGannSwings.Count;
                selected        = inBtwGannSwings;
                corrections     = GetCorrectionsBtwXY_Uptrend( symbol, period, inBtwGannSwings.ToArray( ), begin, end, inputWaveImportance );
            }

            //var impulsiveWaves = DetectTraditionalImpulsiveWaveUp( period, begin,  end, turningPtsCount, selected, corrections );

            //if ( impulsiveWaves.Count > 0 )
            //{
            //    _periodXTaManager.AddImplusiveWaves( WaveType.Correction, impulsiveWaves );
            //    return WaveType.Impulsive;
            //}

            return WaveType.Correction;
        }

        private WaveType GetCorrectionWaveType_Downward( Security symbol, TimeSpan period, PeriodXTaManager taManager, WavePriceTimeInfo correction, int inputWaveImportance )
        {
            var inBtwWaves      = _completeWaveImportanceCopy.Where( x => x.Key >= correction.TrendEndIndex && x.Key <  correction.CounterTrendEndIndex ).OrderBy( x => x.Key ).ToPooledList();
            var inBtwGannSwings = _completeGannImportanceCopy.Where( x => x.Key >= correction.TrendEndIndex && x.Key < correction.CounterTrendEndIndex ).OrderBy( x => x.Key ).ToPooledList();

            var begin           = _completeWaveImportanceCopy.Where( x => x.Key == correction.TrendEndIndex ).FirstOrDefault( );
            var end             = _completeWaveImportanceCopy.Where( x => x.Key == correction.CounterTrendEndIndex ).FirstOrDefault( );

            int turningPtsCount = inBtwWaves.Count;

            PooledList< KeyValuePair< long, WavePointImportance >> selected = null;
            PooledList < WavePriceTimeInfo > corrections                    = null;


            if ( turningPtsCount > 2 )
            {
                selected    = inBtwWaves;
                corrections = GetCorrectionsBtwXY_DownTrend( symbol, period, inBtwWaves.ToArray( ), begin, end, inputWaveImportance );
            }
            else if ( inBtwGannSwings.Count > 2 )
            {
                turningPtsCount = inBtwGannSwings.Count;
                selected        = inBtwGannSwings;
                corrections     = GetCorrectionsBtwXY_DownTrend( symbol, period, inBtwGannSwings.ToArray( ), begin, end, inputWaveImportance );
            }



            //var impulsiveWaves = DetectTraditionalImpulsiveWaveDown( period, begin, end, turningPtsCount, selected, corrections );

            //if ( impulsiveWaves.Count > 0 )
            //{
            //    _periodXTaManager.AddImplusiveWaves( WaveType.Correction, impulsiveWaves );

            //    return WaveType.Impulsive;
            //}

            return WaveType.Correction;
        }

    }
}
