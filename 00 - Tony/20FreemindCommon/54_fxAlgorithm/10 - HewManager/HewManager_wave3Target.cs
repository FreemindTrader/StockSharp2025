using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using Ecng.Common;
using fx.TimePeriod;
using fx.Collections;
using fx.Bars;
using StockSharp.Logging;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        
        public void ShrinkWave3Target()
        {
            switch( _wave3ProjectionType )
            {
                case FibonacciType.Wave3All:
                {
                    _wave3ProjectionType = FibonacciType.Wave3SuperExtended;
                }
                break;

                case FibonacciType.Wave3SuperExtended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3Extended;
                }
                    break;

                case FibonacciType.Wave3Extended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                break;

                case FibonacciType.Wave3ClassicProjection:
                {
                    _wave3ProjectionType = FibonacciType.CompactWave3;
                }
                break;

                case FibonacciType.CompactWave3:
                {
                    _wave3ProjectionType = FibonacciType.CompactWave3;
                }
                    break;

                default:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                    break;

            }            
        }

        public void ExtendedWave3Target()
        {
            switch ( _wave3ProjectionType )
            {
                case FibonacciType.Wave3SuperExtended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3All;
                }
                break;

                case FibonacciType.Wave3Extended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3SuperExtended;
                }
                break;

                case FibonacciType.Wave3ClassicProjection:
                {
                    _wave3ProjectionType = FibonacciType.Wave3Extended;
                }
                break;

                case FibonacciType.CompactWave3:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                break;                

                default:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waveScenarioNo"></param>
        /// <param name="wave2CTime"></param>
        /// <param name="currentWaveDegree"></param>
        /// <param name="wave3All"></param>
        /// <returns></returns>
        private FibonacciType AnalyseWave3A3B3C45( int waveScenarioNo, long wave2CTime, ElliottWaveCycle currentWaveDegree, FibLevelsCollection wave3All )
        {
            //![](4940A3181E5ADC0F695E0C104B459425.png;;;0.03291,0.03345)
            ((DateTime, float PeakTrough) wave0, (DateTime, float PeakTrough) wave1C) pts = GetPoints_Wave0_Wave1C_ForWave2( waveScenarioNo, _currentActiveTimeFrame, wave2CTime, currentWaveDegree );

            bool uptrend = pts.wave1C.PeakTrough > pts.wave0.PeakTrough ? false : true;

            var waveImportance = GetWaveImportanceDictionary( _currentActiveTimeFrame );

            var nextWaveImportance = waveImportance.GetElementsRightOf( wave2CTime );

            (SBar bar, WavePointImportance impt) highest = FindLatestHighestWaveImportance( nextWaveImportance );

            if ( highest != default )
            {
                if ( wave3All.GetClosestFibLevel( highest.bar.ClosePrice, out var closetLine ) )
                {
                    if ( closetLine.FibPrecentage > FibPercentage.Fib_323_6 )
                    {
                        return FibonacciType.Wave3SuperExtended;
                    }
                    else if ( closetLine.FibPrecentage > FibPercentage.Fib_241_4 )
                    {
                        return FibonacciType.Wave3Extended;
                    }
                }
            }

            return FibonacciType.Wave3ClassicProjection;
        }
    }
}
