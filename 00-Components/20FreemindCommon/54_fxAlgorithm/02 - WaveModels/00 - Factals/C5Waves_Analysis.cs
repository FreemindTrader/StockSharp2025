using fx.Database;
using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Common;
using fx.Collections;

#pragma warning disable 414

namespace fx.Algorithm
{
    public partial class C5Waves : I5Waves
    {
        public void StartAnalysis( ref HewLong hew, ElliottWaveEnum highestWaveName, ElliottWaveCycle highestWaveDegree )
        {
            var waveInfos = hew.GetAllWaves( );

            foreach ( var waveInfo in waveInfos )
            {
                AnalyzeWaveInfo( waveInfo );
            }
        }

        private void AnalyzeWaveInfo( WaveInfo waveInfo )
        {
            var waveDegree = waveInfo.WaveCycle;
            var waveName   = waveInfo.WaveName;

            var waveBegin  = _hews.FindBeginOfCurrentTrend( _k.WaveScenarioNo, _k.Period, _k.RawBeginTime, waveDegree );

            if ( waveBegin == -1 )
                return;

            switch ( waveName )
            {
                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                {
                    ProcessWave1( waveInfo );
                }
                break;

                case ElliottWaveEnum.Wave2:
                {

                }
                break;

                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                {
                    ProcessWave3( waveInfo );
                   
                }
                break;

                case ElliottWaveEnum.Wave4:
                {
                    ProcessWave4( waveInfo );
                }
                break;

                case ElliottWaveEnum.WaveA:
                {

                }
                break;

                case ElliottWaveEnum.WaveB:
                {

                }
                break;

                case ElliottWaveEnum.WaveC:
                {

                }
                break;

            }



            /*
            if ( _wave0Time > -1 )
            {
                Bar0 = _bars.GetBarByTime( _wave0Time );
            }

            if ( _wave1Time > -1 )
            {
                Bar1 = _bars.GetBarByTime( _wave1Time );
            }

            if ( _wave2Time > -1 )
            {
                Bar2 = _bars.GetBarByTime( _wave2Time );
            }

            if ( _wave3ATime > -1 )
            {
                Bar3A = _bars.GetBarByTime( _wave3ATime );
            }

            if ( _wave3BTime > -1 )
            {
                Bar3B = _bars.GetBarByTime( _wave3BTime );
            }

            if ( _wave3CTime > -1 )
            {
                Bar3C = _bars.GetBarByTime( _wave3CTime );
            }

            if ( _wave4Time > -1 )
            {
                Bar4 = _bars.GetBarByTime( _wave4Time );
            }

            var symbolex = SymbolHelper.ToSymbolEx( _bars.Security, period );

            if ( _wave1Time > -1 )
            {
                _wave2Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _wave1Time, ElliottWaveEnum.Wave1, waveDegree );

                if ( _wave2Ret != null && Bar2 != SBar.EmptySBar )
                {
                    if ( _wave2Ret.GetClosestFibLevel( Bar2.PeakTroughValue, out wave2Retracement ) )
                    {
                        Wave2Ret = wave2Retracement.FibPrecentage;
                    }
                }
            }

            if ( _wave2Time > -1 )
            {
                _wave3Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _wave2Time, ElliottWaveEnum.Wave2, waveDegree );

                if ( _wave3Exp != null )
                {
                    if ( Bar3A != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3A.PeakTroughValue, out wave3AtoWave1 ) )
                        {
                            Wave3aProj = wave3AtoWave1.FibPrecentage;
                        }
                    }

                    if ( Bar3B != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out wave3btowave1 ) )
                        {
                            Wave3btoWave1 = wave3btowave1.FibPrecentage;
                        }
                    }

                    if ( Bar3C != SBar.EmptySBar )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3C.PeakTroughValue, out wave3CtoWave1 ) )
                        {
                            Wave3cProj = wave3CtoWave1.FibPrecentage;
                        }
                    }
                }
            }

            if ( _wave3ATime > -1 )
            {
                _wave3BRet = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _wave3ATime, ElliottWaveEnum.WaveA, waveDegree );

                if ( _wave3BRet != null && Bar3B != SBar.EmptySBar )
                {
                    if ( _wave3BRet.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3BRetValue ) )
                    {
                        Wave3bRet = wave3BRetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar3B.PeakTroughValue, out FibLevelInfo wave3btoWave1Value ) )
                        {
                            Wave3btoWave1 = wave3btoWave1Value.FibPrecentage;
                        }
                    }
                }
            }


            if ( _wave3CTime > -1 )
            {
                _wave4Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _wave3CTime, ElliottWaveEnum.Wave3, waveDegree );

                if ( _wave4Ret != null && Bar4 != SBar.EmptySBar )
                {
                    if ( _wave4Ret.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4RetValue ) )
                    {
                        Wave4Ret = wave4RetValue.FibPrecentage;
                    }

                    if ( _wave3Exp != null )
                    {
                        if ( _wave3Exp.GetClosestFibLevel( Bar4.PeakTroughValue, out FibLevelInfo wave4toWave1Value ) )
                        {
                            Wave4toWave1 = wave4toWave1Value.FibPrecentage;
                        }
                    }
                }
            }

            if ( _wave4Time > -1 )
            {
                _wave5Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, _wave4Time, ElliottWaveEnum.Wave4, waveDegree );
            }


            if ( wave2Retracement != default && wave3AtoWave1 != default && wave3btowave1 != default && wave3CtoWave1 != default )
            {
                Wave3Type = GetWave3Type( ref wave2Retracement, ref wave3AtoWave1, ref wave3btowave1, ref wave3CtoWave1 );
            }


            if ( wave3CtoWave1 != default && Wave3Type == Wave3Type.UNKNOWN )
            {
                Wave3Type = GetWave3TypeByWave3CTarget( ref wave3CtoWave1 );
            }

            if ( wave3AtoWave1 != default && Wave3Type == Wave3Type.UNKNOWN )
            {
                Wave3Type = GetWave3TypeByWave3ATarget( ref wave3CtoWave1 );
            }


            PredictTargetsBasedOnWave3Model( waveScenarioNo, period, selectedBarTime, waveName, waveDegree, Wave3Type );
            */
        }

        

        public Wave3Type GetWave3Type( ref FibLevelInfo wave1Retracement, ref FibLevelInfo wave3AtoWave1, ref FibLevelInfo wave3btowave1, ref FibLevelInfo wave3CtoWave1 )
        {
            if ( wave1Retracement == default || wave3AtoWave1 == default || wave3btowave1 == default || wave3CtoWave1 == default )
            {
                return Wave3Type.UNKNOWN;
            }

            switch ( wave3AtoWave1.FibPrecentage )
            {
                case FibPercentage p when ( p >= FibPercentage.Fib_100 && p <= FibPercentage.Fib_114_6 ):
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_176_4 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_200 )
                    {
                        return Wave3Type.Classic;
                    }
                }
                break;

                case FibPercentage.Fib_123_6:
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_176_4 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_200 )
                    {
                        return Wave3Type.Classic;
                    }
                    else if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_214_6 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_261_8 )
                    {
                        return Wave3Type.Extended;
                    }
                }
                break;

                case FibPercentage p when ( p > FibPercentage.Fib_123_6 && p < FibPercentage.Fib_176_4 ):
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_214_6 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_261_8 )
                    {
                        return Wave3Type.Extended;
                    }
                }
                break;

                case FibPercentage p when ( p >= FibPercentage.Fib_176_4 ):
                {
                    if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_314_6 )
                    {
                        return Wave3Type.SuperExtended;
                    }
                }
                break;
            }

            return Wave3Type.UNKNOWN;
        }

        public Wave3Type GetWave3TypeByWave3CTarget( ref FibLevelInfo wave3CtoWave1 )
        {
            if ( wave3CtoWave1 == default )
            {
                return Wave3Type.UNKNOWN;
            }

            if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_176_4 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_200 )
            {
                return Wave3Type.Classic;
            }
            else if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_214_6 && wave3CtoWave1.FibPrecentage < FibPercentage.Fib_261_8 )
            {
                return Wave3Type.Extended;
            }
            else if ( wave3CtoWave1.FibPrecentage >= FibPercentage.Fib_314_6 )
            {
                return Wave3Type.SuperExtended;
            }

            return Wave3Type.UNKNOWN;
        }

        public Wave3Type GetWave3TypeByWave3ATarget( ref FibLevelInfo wave3AtoWave1 )
        {
            if ( wave3AtoWave1 == default )
            {
                return Wave3Type.UNKNOWN;
            }

            switch ( wave3AtoWave1.FibPrecentage )
            {
                case FibPercentage p when ( p >= FibPercentage.Fib_100 && p < FibPercentage.Fib_114_6 ):
                {
                    return Wave3Type.Classic;
                }

                case FibPercentage.Fib_123_6:
                {
                    return Wave3Type.Classic;
                }

                case FibPercentage p when ( p > FibPercentage.Fib_123_6 && p < FibPercentage.Fib_176_4 ):
                {
                    return Wave3Type.Extended;
                }

                case FibPercentage p when ( p >= FibPercentage.Fib_176_4 ):
                {
                    return Wave3Type.SuperExtended;
                }
            }

            return Wave3Type.UNKNOWN;
        }

        private void PredictTargetsBasedOnWave3Model( int waveScenarioNo, TimeSpan period, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree, Wave3Type wave3Type )
        {
            if ( wave3Type == Wave3Type.Classic )
            {

            }
            else if ( wave3Type == Wave3Type.Extended )
            {
                switch ( waveName )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {
                        // Should not be here, as if we only have wave 1 and nothing else, we can't do any prediction
                        throw new NotImplementedException();
                    }

                    case ElliottWaveEnum.Wave2:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {
                        PredictWave4ForExtendedWave3( period, selectedBarTime, waveDegree );
                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        PredictWave5ForExtendedWave3( period, selectedBarTime, waveDegree );
                    }
                    break;

                    case ElliottWaveEnum.WaveA:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveB:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveC:
                    {

                    }
                    break;

                }
            }
            else if ( wave3Type == Wave3Type.SuperExtended )
            {
                switch ( waveName )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {
                        // Should not be here, as if we only have wave 1 and nothing else, we can't do any prediction
                        throw new NotImplementedException();
                    }

                    case ElliottWaveEnum.Wave2:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {
                        PredictWave4ForSuperExtendedWave3( waveScenarioNo, period, selectedBarTime, waveDegree );
                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        PredictWave5ForSuperExtendedWave3( period, selectedBarTime, waveDegree );
                    }
                    break;

                    case ElliottWaveEnum.WaveA:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveB:
                    {

                    }
                    break;

                    case ElliottWaveEnum.WaveC:
                    {

                    }
                    break;

                }
            }
        }

        private void PredictWave4ForExtendedWave3( TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {

        }

        private void PredictWave4ForSuperExtendedWave3( int waveScenarioNo, TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {
            if ( _wave4Ret == null )
                return;

            // ![](36FB7D21B48FCA036EA0851CF62D4A17.png;;;0.03383,0.02484)
            // To maintain the bullishness, wave < 4 > should not break wave {IV}            

            var superExtendedWave4Targets       = new PooledList< FibLevelInfo >( );
            double maxWave4FibLevel             = 0;
            var pips                            = (double) _bars.Security.PriceStep.Value;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Since it is Super Extended Wave 3 model, Wave 4 should be as follows.
             *  
             *  Wave 4:         176.4% - 200% - 223.6% of Wave 1.
             *                  23.6%  - 41.4% - 44.1% of Wave 3    
             * ------------------------------------------------------------------------------------------------------------------------------------------- */
            var wave4_RET_236_441   = _wave4Ret.GetAllFibLevelsLE( FibPercentage.Fib_44_1, 5 * pips );
            var wave1_EXP_1764_2236 = _wave3Exp.GetFibLevelsBtw( FibPercentage.Fib_176_4, FibPercentage.Fib_223_6 );

            if ( wave4_RET_236_441 != null )
            {
                if ( _wave4Ret.IsUptrend )
                {
                    // The Whole Trend from beginning is Uptrend. Wave4 is downtrend, so we have to find the lowest
                    maxWave4FibLevel = wave4_RET_236_441.Min( t => t.FibLevel );

                }
                else
                {
                    // The Whole Trend from beginning is Downtrend. Wave4 is uptrend, so we have to find the highest
                    maxWave4FibLevel = wave4_RET_236_441.Max( t => t.FibLevel );
                }

                superExtendedWave4Targets.AddRange( wave4_RET_236_441 );

                superExtendedWave4Targets = superExtendedWave4Targets.OrderBy( x => x.FibLevel ).ToPooledList();
            }

            long wave4smaller = _hews.FindPreviousWave4( waveScenarioNo,period, selectedBarTime, waveDegree - GlobalConstants.OneWaveCycle );

            if ( wave4smaller > -1 )
            {
                ref SBar barSmaller4 = ref _bars.GetBarByTime( wave4smaller );

                if ( barSmaller4 == SBar.EmptySBar ) return;

                int breakIndex = -1;

                if ( barSmaller4.IsWaveTrough() )
                {
                    breakIndex = _bars.GetIndexBreakDown( barSmaller4.BarIndex, _bars.TotalBarCount, barSmaller4.Low );
                }
                else
                {
                    breakIndex = _bars.GetIndexBreakUp( barSmaller4.BarIndex, _bars.TotalBarCount, barSmaller4.High );
                }

                if ( breakIndex > -1 )
                {
                    PooledList< FibLevelInfo > wave4Broken = null;

                    // Now that Wave 4 has been broken, we are going to look at the Fib levels below Wave 4.
                    if ( barSmaller4 != SBar.EmptySBar )
                    {
                        if ( barSmaller4.IsTrough() )
                        {
                            wave4Broken = _wave4Ret.GetAllFibLevelsBelow( barSmaller4.PeakTroughValue, out double discard );
                        }
                        else
                        {
                            wave4Broken = _wave4Ret.GetAllFibLevelsAbove( barSmaller4.PeakTroughValue, out double discard );
                        }

                        foreach ( FibLevelInfo fibLevelInfo in wave4Broken )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => x.Equals( fibLevelInfo ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.LikelyScore *= 2;
                            }
                        }
                    }

                    // ![](00F903AC9BCCD58282FC55DDDCF8C3A7.png;;;0.02630,0.02218)
                    foreach ( FibLevelInfo fibLevelInfo in _wave4Ret.TonyFirstLevels.FibLevels )
                    {
                        var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                        if ( index > -1 )
                        {
                            var selectedLevel = superExtendedWave4Targets[ index ];

                            selectedLevel.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( !_wave4Ret.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }

                    foreach ( FibLevelInfo fibLevelInfo in _wave4Ret.TonySecondLevels.FibLevels )
                    {
                        var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                        if ( index > -1 )
                        {
                            var selectedLevel = superExtendedWave4Targets[ index ];

                            selectedLevel.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( !_wave4Ret.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }

                    foreach ( FibLevelInfo fibLevelInfo in _wave4Ret.TonyRetracementLevels.FibLevels )
                    {
                        var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                        if ( index > -1 )
                        {
                            var selectedLevel = superExtendedWave4Targets[ index ];

                            selectedLevel.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( !_wave4Ret.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                {
                                    superExtendedWave4Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }
                }
                else
                {
                    PooledList< FibLevelInfo > notBreakWave4 = null;

                    // To maintain the bullishness, wave < 4 > should not break wave {IV}
                    var nonBreakWave4Ret = _hews.GetRetracementTargets(waveScenarioNo, _bars.Security, period, wave4smaller, selectedBarTime, waveDegree );

                    if ( nonBreakWave4Ret != null )
                    {
                        foreach ( FibLevelInfo fibLevelInfo in nonBreakWave4Ret.RegularRetraceProjectionLevels.FibLevels )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.UpdateAll( fibLevelInfo );
                            }
                            else
                            {
                                superExtendedWave4Targets.Add( fibLevelInfo );
                            }
                        }
                    }



                    if ( barSmaller4 != SBar.EmptySBar )
                    {
                        if ( barSmaller4.IsTrough() )
                        {
                            notBreakWave4 = _wave4Ret.GetAllFibLevelsAbove( barSmaller4.PeakTroughValue, out double discard );
                        }
                        else
                        {
                            notBreakWave4 = _wave4Ret.GetAllFibLevelsBelow( barSmaller4.PeakTroughValue, out double discard );
                        }

                        foreach ( FibLevelInfo fibLevelInfo in notBreakWave4 )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => x.Equals( fibLevelInfo ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.LikelyScore *= 2;
                            }
                        }
                    }

                    var pt_4b_5 = _hews.GetPoints_Wave4B_Wave5( waveScenarioNo, wave4smaller, waveDegree - GlobalConstants.OneWaveCycle, selectedBarTime );

                    if ( pt_4b_5 != default )
                    {
                        var fib_4b_5 = _hews.GetRetracementTargets( _bars.Security, period, pt_4b_5 );

                        foreach ( FibLevelInfo fibLevelInfo in fib_4b_5.RegularRetraceProjectionLevels.FibLevels )
                        {
                            var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                            if ( index > -1 )
                            {
                                var selectedLevel = superExtendedWave4Targets[ index ];

                                selectedLevel.UpdateAll( fibLevelInfo );
                            }
                            else
                            {
                                if ( !_wave4Ret.IsUptrend )
                                {
                                    if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                    {
                                        superExtendedWave4Targets.Add( fibLevelInfo );
                                    }
                                }
                                else
                                {
                                    if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                    {
                                        superExtendedWave4Targets.Add( fibLevelInfo );
                                    }
                                }
                            }
                        }

                        var time4B = pt_4b_5.Item1.Item1.ToLinuxTime( );

                        var pt_4_5 = _hews.GetPoints_LowerDegreeWave4_Wave5(waveScenarioNo, time4B, waveDegree - GlobalConstants.OneWaveCycle * 2, selectedBarTime );

                        if ( pt_4_5 != default )
                        {
                            var pt_4b_5Target = _hews.GetTonyProjectionTarget( _bars.Security, period, pt_4_5 );

                            foreach ( FibLevelInfo fibLevelInfo in pt_4b_5Target.TonyFirstLevels.FibLevels )
                            {
                                var index = superExtendedWave4Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( pips * 5 ) );

                                if ( index > -1 )
                                {
                                    var selectedLevel = superExtendedWave4Targets[ index ];

                                    selectedLevel.UpdateAll( fibLevelInfo );
                                }
                                else
                                {
                                    if ( !_wave4Ret.IsUptrend )
                                    {
                                        if ( fibLevelInfo.FibLevel < maxWave4FibLevel )
                                        {
                                            superExtendedWave4Targets.Add( fibLevelInfo );
                                        }
                                    }
                                    else
                                    {
                                        if ( fibLevelInfo.FibLevel > maxWave4FibLevel )
                                        {
                                            superExtendedWave4Targets.Add( fibLevelInfo );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for ( int i = 0; i < superExtendedWave4Targets.Count; i++ )
                {
                    if ( Bar3C.IsWavePeak() )
                    {
                        if ( _bars.GetIndexBreakDown( Bar3C.BarIndex, _bars.TotalBarCount, superExtendedWave4Targets[ i ].FibLevel ) > -1 )
                        {
                            var fibInfo = superExtendedWave4Targets[ i ];
                            fibInfo.IsBroken = true;
                            superExtendedWave4Targets[ i ] = fibInfo;

                        }
                    }
                    else
                    {
                        if ( _bars.GetIndexBreakUp( Bar3C.BarIndex, _bars.TotalBarCount, superExtendedWave4Targets[ i ].FibLevel ) > -1 )
                        {
                            var fibInfo = superExtendedWave4Targets[ i ];
                            fibInfo.IsBroken = true;
                            superExtendedWave4Targets[ i ] = fibInfo;
                        }
                    }
                }

                foreach ( FibLevelInfo fibLevelInfo in superExtendedWave4Targets )
                {

                }

                _predictedTargets = superExtendedWave4Targets.OrderByDescending( x => x.OverlappedCount ).ToPooledList();
            }
        }

        private void PredictWave5ForExtendedWave3( TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {
            if ( _wave5Exp == null )
                return;

            PooledList< FibLevelInfo > wave5Targets = null;

            var diff = _bars.Security.PriceStep.Value;

            double highestLowestFib = 0;

            if ( _wave5Exp != null && Bar3C != SBar.EmptySBar )
            {
                if ( Bar3C.IsTrough() )
                {
                    wave5Targets = _wave5Exp.GetAllFibLevelsBelow( Bar3C.PeakTroughValue, out highestLowestFib );
                }
                else
                {
                    wave5Targets = _wave5Exp.GetAllFibLevelsAbove( Bar3C.PeakTroughValue, out highestLowestFib );
                }



                if ( _wave5Exp.HasTonyExtensions )
                {
                    foreach ( FibLevelInfo fibLevelInfo in _wave5Exp.TonyFirstLevels.FibLevels )
                    {
                        var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                        if ( index > -1 )
                        {
                            var wave5CalcTarget = wave5Targets[ index ];

                            wave5CalcTarget.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( _wave5Exp.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < highestLowestFib && fibLevelInfo.FibLevel >= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > highestLowestFib && fibLevelInfo.FibLevel <= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }
                }

                if ( _wave5Exp.HasTonyExtensions2 )
                {
                    foreach ( FibLevelInfo fibLevelInfo in _wave5Exp.TonySecondLevels.FibLevels )
                    {
                        var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                        if ( index > -1 )
                        {
                            var wave5CalcTarget = wave5Targets[ index ];

                            wave5CalcTarget.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( _wave5Exp.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < highestLowestFib && fibLevelInfo.FibLevel >= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > highestLowestFib && fibLevelInfo.FibLevel <= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                        }
                    }
                }
            }

            if ( _wave3Exp != null )
            {
                var proj3a = Wave3aProj;
                var proj3c = Wave3cProj;
                var ret3b = Wave3btoWave1;

                var target = _wave3Exp.GetFibLevelsBtw( FibPercentage.Fib_276_4, FibPercentage.Fib_323_6 );

                foreach ( FibLevelInfo fibLevelInfo in target )
                {
                    var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                    if ( index > -1 )
                    {
                        var wave5CalcTarget = wave5Targets[ index ];

                        wave5CalcTarget.UpdateAll( fibLevelInfo );
                    }
                    else
                    {
                        if ( _wave5Exp.IsUptrend )
                        {
                            if ( fibLevelInfo.FibLevel < highestLowestFib )
                            {
                                wave5Targets.Add( fibLevelInfo );
                            }
                        }
                        else
                        {
                            if ( fibLevelInfo.FibLevel > highestLowestFib )
                            {
                                wave5Targets.Add( fibLevelInfo );
                            }
                        }
                    }
                }

                if ( _wave3Exp.HasTonyExtensions )
                {
                    foreach ( FibLevelInfo fibLevelInfo in _wave3Exp.TonyFirstLevels.FibLevels )
                    {
                        var index = wave5Targets.FindIndex( x => Math.Abs( x.FibLevel - fibLevelInfo.FibLevel ) < (double)( diff * 4 ) );

                        if ( index > -1 )
                        {
                            var wave5CalcTarget = wave5Targets[ index ];

                            wave5CalcTarget.UpdateAll( fibLevelInfo );
                        }
                        else
                        {
                            if ( _wave5Exp.IsUptrend )
                            {
                                if ( fibLevelInfo.FibLevel < highestLowestFib && fibLevelInfo.FibLevel >= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );
                                }
                            }
                            else
                            {
                                if ( fibLevelInfo.FibLevel > highestLowestFib && fibLevelInfo.FibLevel <= Bar3C.PeakTroughValue )
                                {
                                    wave5Targets.Add( fibLevelInfo );

                                }
                            }
                        }
                    }
                }

                _predictedTargets = wave5Targets;
            }
        }



        private void PredictWave5ForSuperExtendedWave3( TimeSpan period, long selectedBarTime, ElliottWaveCycle waveDegree )
        {

        }

    }
}
