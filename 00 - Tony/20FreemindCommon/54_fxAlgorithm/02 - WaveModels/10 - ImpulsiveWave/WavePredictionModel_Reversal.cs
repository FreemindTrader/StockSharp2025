using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public partial class WavePredictionModel : IWavePredictionModel
    {
        public void AnalyseLastABC( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle wave5Degree, ref HewLong wave5CHew )
        {
            /*
            long largerWave4Begin         = -1;
            long wave3B                   = -1;
            long wave1                    = -1;
            long wave2                    = -1;
            long wave3A                   = -1;
            long wave3C                   = -1;
            long wave4                    = -1;
            long wave5A                   = -1;
            long wave5B                   = -1;
            long wave5C                   = -1;

            FibLevelInfo wave2Retracement = default;
            FibLevelInfo wave3AtoWave1    = default;
            FibLevelInfo wave3btowave1    = default;
            FibLevelInfo wave3CtoWave1    = default;

            var potentialLevels = new PooledList< FibLevelInfo >( );

            var waveInfos       = wave5CHew.GetAllWaves( );

            var symbolex        = SymbolHelper.ToSymbolEx( _bars.Security, period );

            wave5C              = selectedBarTime;

            foreach ( var waveInfo in waveInfos )
            {
                var wavDegree = waveInfo.WaveCycle;

                wave4 = _hews.FindPreviousWave4( waveScenarioNo, period, selectedBarTime, wavDegree );

                if ( wave4 > -1 )
                {
                    if ( _hews.NoHigherWaveBetween( waveScenarioNo, wave4, wave5C, wavDegree ) )
                    {
                        wave5B = _hews.FindPreviousWaveBBtw( waveScenarioNo, period, wave4, wave5C, wavDegree );
                        wave5A = _hews.FindPreviousWaveABtw( waveScenarioNo, period, wave4, wave5C, wavDegree );
                    }
                }
            }


            largerWave4Begin = _hews.FindPreviousWave4( waveScenarioNo, period, selectedBarTime, wave5Degree );

            if ( largerWave4Begin == -1 )
                return;

            PooledList< FibLevelInfo > output = new PooledList< FibLevelInfo >( );

            var smWavesOf5 = _hews.GetAllWavesAfter(waveScenarioNo, period, largerWave4Begin );


            foreach ( var smallerWaves in smWavesOf5 )
            {
                ref var hew = ref smallerWaves.GetWaveFromScenario( waveScenarioNo );

                var waveInfo = hew.GetHewPointInfoAtCycle( wave5Degree );

                if ( waveInfo.HasValue )
                {
                    if ( waveInfo.Value.WaveName == ElliottWaveEnum.WaveA )
                    {
                        wave5A = smallerWaves.StartDate;
                        Bar5A = _bars.GetBarByTime( smallerWaves.StartDate );
                    }

                    if ( waveInfo.Value.WaveName == ElliottWaveEnum.WaveB )
                    {
                        wave5B = smallerWaves.StartDate;
                        Bar5B = _bars.GetBarByTime( smallerWaves.StartDate );
                    }
                }
            }

            if ( waveName != ElliottWaveEnum.Wave5 && waveName != ElliottWaveEnum.Wave5C )
                return;

            wave5C = selectedBarTime;

            if ( wave5C > -1 )
            {
                Bar5C = _bars.GetBarByTime( wave5C );
            }

            if ( wave5B > -1 )
            {
                // We have wave 5B to wave 5C, this should be an impulsive wave.

                var smWave5C = new WavePredictionModel( bars, _hews );
            }


            var smDegree = wave5Degree - GlobalConstants.OneWaveCycle;

            wave4 = _hews.FindPreviousWave4( waveScenarioNo, period, selectedBarTime, smDegree );

            if ( wave4 != -1 && _hews.NoHigherWaveBetween( waveScenarioNo, wave4, wave5C, smDegree ) )
            {
                wave5B = _hews.FindPreviousWaveBBtw( waveScenarioNo, period, wave4, wave5C, smDegree );
                wave5A = _hews.FindPreviousWaveABtw( waveScenarioNo, period, wave4, wave5C, smDegree );
            }

            wave3C = _hews.FindPreviousWave3( waveScenarioNo, period, selectedBarTime, smDegree );

            if ( wave3C != -1 && _hews.NoHigherWaveBetween( waveScenarioNo, wave3C, wave5C, smDegree ) )
            {
                wave3B = _hews.FindPreviousWaveBBtw( waveScenarioNo, period, largerWave4Begin, wave3C, smDegree );
                wave3A = _hews.FindPreviousWaveABtw( waveScenarioNo, period, largerWave4Begin, wave3C, smDegree );

                wave2 = _hews.FindPreviousWave2Btw( waveScenarioNo, period, largerWave4Begin, wave3C, smDegree );
                wave1 = _hews.FindPreviousWave1Btw( waveScenarioNo, period, largerWave4Begin, wave3C, smDegree );
            }

            if ( largerWave4Begin > -1 )
            {
                Bar0 = _bars.GetBarByTime( largerWave4Begin );
            }

            if ( wave1 > -1 )
            {
                Bar1 = _bars.GetBarByTime( wave1 );
            }

            if ( wave2 > -1 )
            {
                Bar2 = _bars.GetBarByTime( wave2 );
            }

            if ( wave3A > -1 )
            {
                Bar3A = _bars.GetBarByTime( wave3A );
            }

            if ( wave3B > -1 )
            {
                Bar3B = _bars.GetBarByTime( wave3B );
            }

            if ( wave3C > -1 )
            {
                Bar3C = _bars.GetBarByTime( wave3C );
            }

            if ( wave4 > -1 )
            {
                Bar4 = _bars.GetBarByTime( wave4 );
            }

            if ( wave5A > -1 )
            {
                Bar5A = _bars.GetBarByTime( wave5A );
            }

            if ( wave5B > -1 )
            {
                Bar5B = _bars.GetBarByTime( wave5B );
            }

            if ( wave5C > -1 )
            {
                Bar5C = _bars.GetBarByTime( wave5C );
            }

            if ( wave1 > -1 )
            {
                _wave2Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave1, ElliottWaveEnum.Wave1, wave5Degree );

                if ( _wave2Ret != null && Bar2 != SBar.EmptySBar )
                {
                    if ( _wave2Ret.GetClosestFibLevel( Bar2.PeakTroughValue, out wave2Retracement ) )
                    {
                        Wave2Ret = wave2Retracement.FibPrecentage;
                    }
                }
            }

            if ( wave2 > -1 )
            {
                _wave3Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave2, ElliottWaveEnum.Wave2, wave5Degree );

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

            if ( wave3A > -1 )
            {
                _wave3BRet = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave3A, ElliottWaveEnum.WaveA, wave5Degree );

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


            if ( wave3C > -1 )
            {
                _wave4Ret = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave3C, ElliottWaveEnum.Wave3, wave5Degree );

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

            if ( wave4 > -1 )
            {
                _wave5Exp = _hews.GetHewFibTargets( waveScenarioNo, symbolex, wave4, ElliottWaveEnum.Wave4, wave5Degree );

                if ( _wave5Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5CExpValue ) )
                {
                    Wave5Proj = wave5CExpValue.FibPrecentage;
                }

                if ( _wave3Exp != null )
                {
                    if ( _wave3Exp.GetClosestFibLevel( Bar5C.PeakTroughValue, out FibLevelInfo wave5toWave1Value ) )
                    {
                        Wave5toWave1 = wave5toWave1Value.FibPrecentage;
                    }
                }
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



            */

        }

    }
}
