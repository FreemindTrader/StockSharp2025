using System;
using System.Collections.Generic;
using System.Linq;
using fx.Collections;

using fx.Definitions;
using StockSharp.BusinessEntities;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        public bool GetPriceTimeInfo_Wave024X_WaveC( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long waveCtime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            if ( -1 == waveCtime )
            {
                return false;
            }

            var indexOfWaveC = bars.GetIndexByTime( waveCtime );
            var waveBTime    = FindPreviousWaveB( waveScenarioNo,period, waveCtime, currentWaveDegree );
            var wave024Xtime = FindPreviousWave024X( waveScenarioNo,period, waveCtime, currentWaveDegree );

            if ( -1 == wave024Xtime )
            {
                return false;
            }

            var wave024XBarIndex = bars.GetIndexByTime( wave024Xtime );

            int indexOfWaveB = -1;

            if ( waveBTime != -1 )
            {
                indexOfWaveB = bars.GetIndexByTime( waveBTime );
            }

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveCtime, currentWaveDegree ) )
            {
                (( DateTime Time, float Value ) wave0, ( DateTime Time, float Value ) waveC) p = default;

                if ( wave024Xtime == waveCtime )
                {
                    p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveCtime, currentWaveDegree );
                }
                else
                {
                    p = GetWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveCtime, currentWaveDegree );
                }

                if ( p != default )
                {
                    RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value > p.waveC.Value ? p.waveC.Value : p.wave0.Value,
                                                                               p.wave0.Value > p.waveC.Value ? p.wave0.Value : p.waveC.Value);

                    var length_Wave024X_WaveC = Math.Abs( p.wave0.Value - p.waveC.Value);

                    var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    HHLL hhs;
                    HHLL hhll2 = default;

                    if ( trendDirection == TrendDirection.DownTrend )
                    {
                        hhs = new HHLL( HHLLEnum.HighLow, ( uint ) ( indexOfWaveC - wave024XBarIndex ) );

                        if ( indexOfWaveB > -1 )
                        {
                            hhll2 = new HHLL( HHLLEnum.LowLow, ( uint ) ( indexOfWaveC - indexOfWaveB ) );
                        }
                    }
                    else
                    {
                        hhs = new HHLL( HHLLEnum.LowHigh, ( uint ) ( indexOfWaveC - wave024XBarIndex ) );

                        if ( indexOfWaveB > -1 )
                        {
                            hhll2 = new HHLL( HHLLEnum.HighHigh, ( uint ) ( indexOfWaveC - indexOfWaveB ) );
                        }
                    }

                    var priceTimeInfo = new WavePriceTimeInfo( trendDirection, wave024XBarIndex, indexOfWaveC, -1, trendMovedRange, null, hhs, hhll2 );

                    output = priceTimeInfo;

                    return true;
                }
            }

            return false;
        }



        public bool GetPriceTimeInfo_Wave024X_WaveA( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long waveAtime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            if ( -1 == waveAtime )
            {
                return false;
            }

            var waveABarIndex = bars.GetIndexByTime( waveAtime );

            var wave024Xtime = FindPreviousWave024X(waveScenarioNo, period, waveAtime, currentWaveDegree );

            if ( -1 == wave024Xtime )
            {
                return false;
            }

            var wave024XBarIndex = bars.GetIndexByTime( wave024Xtime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
            {
                ((DateTime Time, float Value) wave0, (DateTime Time, float Value) waveA ) p = default;

                if ( wave024Xtime == waveAtime )
                {
                    p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveAtime, currentWaveDegree );
                }
                else
                {
                    p = GetWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveAtime, currentWaveDegree );
                }

                if ( p != default )
                {
                    var length_Wave0_Wave1C = Math.Abs( p.wave0.Value - p.waveA.Value);

                    RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value > p.waveA.Value ? p.waveA.Value : p.wave0.Value,
                                                                               p.wave0.Value > p.waveA.Value ? p.wave0.Value : p.waveA.Value);

                    var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    HHLL hhs = default;
                    HHLL hhll2 = default;

                    if ( trendDirection == TrendDirection.DownTrend )
                    {
                        hhs = new HHLL( HHLLEnum.HighLow, ( uint ) ( waveABarIndex - wave024XBarIndex ) );
                    }
                    else
                    {
                        hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( waveABarIndex - wave024XBarIndex ) );
                    }
                    var priceTimeInfo = new WavePriceTimeInfo( trendDirection, wave024XBarIndex, waveABarIndex, -1, trendMovedRange, null, hhs, hhll2 );

                    output = priceTimeInfo;

                    return true;
                }
            }

            return false;
        }

        public HewPredictionTargets DoHewPredictions_UpWard( int waveScenarioNo, TimeSpan period, ref SBar bar0, ref WaveInfo currWaveInfo )
        {
            HewPredictionTargets output = null;
            var bars                    = GetDatabarsRepository( period );
            ref var redBar0Wave         = ref bar0.GetWaveFromScenario( waveScenarioNo );
            var barTime                 = bar0.LinuxTime;
            var currentWaveDegree       = currWaveInfo.WaveCycle;
            var priorWave               = GetNextWaveOfSameDegreeExcept24( waveScenarioNo,period, barTime, currWaveInfo.WaveName, currentWaveDegree );

            //![](12D390C286B505954F9512ECC396AB2F.png;;;0.03333,0.03453)

            switch ( currWaveInfo.WaveName )
            {
                case ElliottWaveEnum.WaveB:
                {
                    ((DateTime Time, float Value) wave0, (DateTime Time, float Value) waveA ) lastWaveStartEnd = GetPoints_Wave024X_WaveA( waveScenarioNo,period, barTime, currentWaveDegree );
                    var waveBContainingWave = WaveBOfWhat( waveScenarioNo,period, barTime, currentWaveDegree );

                    if ( lastWaveStartEnd != default )
                    {
                        var fibLevels           = new FibLevelsCollection( FibonacciType.ABCWaveCProjection, lastWaveStartEnd.wave0, lastWaveStartEnd.waveA, bar0.LowTimeValue );
                        output = new HewPredictionTargets( period, lastWaveStartEnd.wave0, lastWaveStartEnd.waveA, ref bar0, fibLevels, ref currWaveInfo, FibonacciType.ABCWaveCProjection );
                        output.TargetWaveName = waveBContainingWave;

                        var hews   = GetElliottWavesDictionary( period );
                        var lastWaveStart       = hews[ lastWaveStartEnd.wave0.Time.ToLinuxTime( ) ];

                        ref var hew = ref lastWaveStart.GetWaveFromScenario( waveScenarioNo );

                        var oneHigherDegree     = hew.GetFirstHighestWaveInfo( ).Value.WaveCycle + GlobalConstants.OneWaveCycle;
                        var oneDegreeHigherWave = GetPreviousWavePointInfoOfDegreeOrHigher( waveScenarioNo, period, lastWaveStartEnd.wave0.Time.ToLinuxTime( ), oneHigherDegree );

                        if ( oneDegreeHigherWave.HasValue )
                        {
                            ref SBar redBar0 = ref bars.GetBarByTime( oneDegreeHigherWave.Value.BarTime );

                            if ( redBar0 != SBar.EmptySBar )
                            {
                                var waveValue       = oneDegreeHigherWave.Value.WaveInfo;
                                var higherHew       = DoHewPredictionsHigher_Upward( waveScenarioNo,period, ref redBar0, ref waveValue );
                                var higherWaveCycle = oneDegreeHigherWave.Value.WaveInfo.WaveCycle;

                                output.AddHigherPredictionTargets( higherWaveCycle, higherHew );
                            }
                        }
                    }
                }
                break;
            }

            return output;
        }

        public FibExpRet DoHewPredictionsHigher_Upward( int waveScenarioNo, TimeSpan period, ref SBar bar0, ref WaveInfo currWaveInfo )
        {
            FibExpRet expRetResult             = null;
            FibLevelsCollection fibExpansion   = null;
            FibLevelsCollection fibRetracement = null;
            
            var barLinuxTime                   = bar0.LinuxTime;
            var currentWaveDegree              = currWaveInfo.WaveCycle;

            var priorWave = GetNextWaveOfSameDegreeExcept24(waveScenarioNo, period, barLinuxTime, currWaveInfo.WaveName, currentWaveDegree );

            switch ( currWaveInfo.WaveName )
            {
                case ElliottWaveEnum.WaveB:
                {
                    ((DateTime Time, float Value) wave0, (DateTime Time, float Value) waveA ) wave0A = default;

                    TimeSpan htf = period;

                    do
                    {
                        wave0A = GetPoints_Wave024X_WaveA( waveScenarioNo, htf, barLinuxTime, currentWaveDegree );

                        if ( wave0A != default )
                        {
                            // Apart from FibExpansion, we also want to include Fib Retracement BothExpNRet.jpg
                            /// <image url="$(SolutionDir)\Pictures\BothExpNRet.jpg"/>

                            var point024X       = wave0A.wave0;
                            var pointA          = wave0A.waveA;
                            var startPointValue = point024X.Value;
                            var endPointValue   = pointA.Value;

                            var containing      = WaveBOfWhat( waveScenarioNo,htf, bar0.LinuxTime, currentWaveDegree );

                            expRetResult = new FibExpRet( ref bar0, containing );

                            fibExpansion = new FibLevelsCollection( containing.GetProjectionTypeOfWaveB( ), wave0A.wave0, wave0A.waveA, bar0.LowTimeValue );
                            fibRetracement = new FibLevelsCollection( FibonacciType.ABCWaveBRetracement, wave0A.wave0, wave0A.waveA );

                            SetMajorResistanceFromPreviousWaves( htf, pointA, bar0.LowTimeValue, fibRetracement, fibExpansion );

                            expRetResult.AddExpansions( fibExpansion.Key, fibExpansion );
                            expRetResult.AddRetracements( fibRetracement.Key, fibRetracement );

                            break;
                        }
                        else
                        {
                            TimeSpan oneTimeSpanHigher = TimeSpan.MinValue;

                            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                            if ( aa != null )
                            {
                                htf = aa.GetOneTimeSpanHigher( htf );
                            }
                            else
                            {
                                return null;
                            }

                            if ( htf != TimeSpan.Zero )
                            {
                                var hews     = GetElliottWavesDictionary( htf );
                                var bars     = GetDatabarsRepository( htf );
                                ref SBar bar = ref bars.GetBarContainingTime( bar0.BarTime, htf );

                                if ( bar != SBar.EmptySBar )
                                {
                                    barLinuxTime = bar.LinuxTime;
                                }
                            }
                        }
                    }
                    while ( wave0A == default && htf != TimeSpan.Zero );
                }
                break;
            }

            return expRetResult;
        }



        private void SetMajorResistanceFromPreviousWaves( TimeSpan period, (DateTime startWaveTime, float startPrice) startPoint, (DateTime endWaveTime, float endPrice) endPoint, FibLevelsCollection fibRetracement, FibLevelsCollection fibExpansion )
        {
            var waveImpt         = GetWaveImportanceDictionary( period );
            var bars = GetDatabarsRepository(period);
            var currentBar       = bars.Current;

            var allPeaks   = waveImpt.Where( x =>
                                                    {
                                                        if( x.Key >= startPoint.startWaveTime.ToLinuxTime( ) && x.Key < endPoint.endWaveTime.ToLinuxTime( ) && x.Value.Signal == TASignal.WAVE_PEAK )
                                                        {
                                                            ref SBar bar = ref bars.GetBarByTime( x.Key );

                                                            if ( bar == SBar.EmptySBar )
                                                            {
                                                                return false;
                                                            }
                        
                                                            if( bar.High > currentBar.High ) return true;
                                                        }

                                                        return false;
                                                    }
                                            ).OrderBy( x => x.Key ).ToPooledList();

            foreach ( var peak in allPeaks )
            {
                ref SBar bar = ref bars.GetBarByTime( peak.Key );

                if ( bar != SBar.EmptySBar )                
                {
                    var barHigh = bar.High;

                    if ( fibRetracement.FibLevels.Count > 0 )
                    {
                        var closestRet = fibRetracement.FibLevels.OrderBy(item => Math.Abs( barHigh - item.FibLevel)).First();

                        var SRLinesRet = bar.GetUpperShadowSRInfo(period, closestRet.FibPrecentage.ToDescription(), closestRet.FibLevel);

                        if ( SRLinesRet != null )
                        {
                            fibRetracement.AddMatchedSRinfo( ref closestRet, SRLinesRet );
                        }
                    }

                    if ( fibExpansion.FibLevels.Count > 0 )
                    {
                        var closestExp = fibExpansion.FibLevels.OrderBy(item => Math.Abs( barHigh - item.FibLevel)).First();

                        var SRLinesExp = bar.GetUpperShadowSRInfo(period, closestExp.FibPrecentage.ToDescription(), closestExp.FibLevel);

                        if ( SRLinesExp != null )
                        {
                            fibExpansion.AddMatchedSRinfo( ref closestExp, SRLinesExp );
                        }
                    }
                }


            }

            var allTroughs = waveImpt.Where(x =>
            {
                if (x.Key >= startPoint.startWaveTime.ToLinuxTime() && x.Key < endPoint.endWaveTime.ToLinuxTime() && x.Value.Signal == TASignal.WAVE_TROUGH)
                {
                    ref SBar bar = ref bars.GetBarByTime( x.Key );

                    if ( bar == SBar.EmptySBar )
                    {
                        return false;
                    }
                        
                    if (bar.Low > currentBar.High) return true;
                }

                return false;
            }
                                            ).OrderBy(x => x.Key).ToList();

            foreach ( var trough in allTroughs )
            {
                ref SBar bar = ref bars.GetBarByTime( trough.Key );

                if ( bar != SBar.EmptySBar )                
                {
                    var barLow = bar.Low;

                    if ( fibRetracement.FibLevels.Count > 0 )
                    {
                        var closestRet = fibRetracement.FibLevels.OrderBy(item => Math.Abs( barLow - item.FibLevel)).First();
                        var SRLinesRet = bar.GetLowerShadowSRInfo(period, closestRet.FibPrecentage.ToDescription(), closestRet.FibLevel);

                        if ( SRLinesRet != null )
                        {
                            fibRetracement.AddMatchedSRinfo( ref closestRet, SRLinesRet );
                        }
                    }

                    if ( fibExpansion.FibLevels.Count > 0 )
                    {
                        var closestExp = fibExpansion.FibLevels.OrderBy(item => Math.Abs( barLow - item.FibLevel)).First();
                        var SRLinesExp = bar.GetLowerShadowSRInfo(period, closestExp.FibPrecentage.ToDescription(), closestExp.FibLevel);

                        if ( SRLinesExp != null )
                        {
                            fibExpansion.AddMatchedSRinfo( ref closestExp, SRLinesExp );
                        }
                    }
                }
            }
        }

        public bool GetPriceTimeInfo_Wave0_Wave1C( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave1BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            if ( -1 == wave1BarTime )
            {
                return false;
            }

            var wave1BarIndex = bars.GetIndexByTime( wave1BarTime );

            var wave0BarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave1BarTime, currentWaveDegree );

            if ( -1 == wave0BarTime )
            {
                return false;
            }

            var wave0BarIndex = bars.GetIndexByTime( wave0BarTime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave0BarTime, wave1BarTime, currentWaveDegree ) )
            {
                ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave1c) p = default;

                if ( wave0BarTime == wave1BarTime )
                {
                    p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave0BarTime, wave1BarTime, currentWaveDegree );
                }
                else
                {
                    p = GetWavePoints( waveScenarioNo, bars, period, wave0BarTime, wave1BarTime, currentWaveDegree );
                }

                if ( p != default )
                {
                    var length_Wave0_Wave1C = Math.Abs( p.wave0.Value - p.wave1c.Value );

                    RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value > p.wave1c.Value ? p.wave1c.Value : p.wave1c.Value,
                                                                               p.wave0.Value > p.wave1c.Value ? p.wave0.Value : p.Item2.Value);

                    var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    HHLL hhs = default;
                    HHLL hhll2 = default;

                    if ( trendDirection == TrendDirection.DownTrend )
                    {
                        hhs = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave1BarIndex - wave0BarIndex ) );
                    }
                    else
                    {
                        hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave1BarIndex - wave0BarIndex ) );
                    }

                    var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave0BarIndex, wave1BarIndex, -1, trendMovedRange, null, hhs, hhll2 );

                    output = priceTimeInfo;

                    return true;
                }
            }

            return false;
        }

        public bool GetPriceTimeInfo_Wave0_Wave1_Wave2_Wave3C( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave3BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            if ( -1 == wave3BarTime )
            {
                return false;
            }

            var wave3BarIndex = bars.GetIndexByTime( wave3BarTime );
            

            var wave2BarTime = FindPreviousWave2( waveScenarioNo, period, wave3BarTime, currentWaveDegree );

            if ( -1 == wave2BarTime )
            {
                return false;
            }

            if ( ! NoHigherWaveBetween( waveScenarioNo, period, wave2BarTime, wave3BarTime, currentWaveDegree ) )
            {
                return false;
            }

            var wave1BarTime = FindPreviousWave1( waveScenarioNo, period, wave2BarTime, currentWaveDegree );

            if ( -1 == wave1BarTime )
            {
                return false;
            }

            if ( !NoHigherWaveBetween( waveScenarioNo, period, wave1BarTime, wave2BarTime, currentWaveDegree ) )
            {
                return false;
            }

            var wave0BarTime = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave3BarTime, currentWaveDegree );

            if ( -1 == wave0BarTime )
            {
                return false;
            }

            var wave0BarIndex = bars.GetIndexByTime( wave0BarTime );

            if ( !NoHigherWaveBetween( waveScenarioNo, period, wave0BarTime, wave3BarTime, currentWaveDegree ) )
            {
                return false;
            }


            ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave1 ) p1 = default;            
            ((DateTime Time, float Value) wave2, (DateTime Time, float Value) wave3 ) p3 = default;

            p1 = GetWavePoints( waveScenarioNo, bars, period, wave0BarTime, wave1BarTime, currentWaveDegree );            
            p3 = GetWavePoints( waveScenarioNo, bars, period, wave2BarTime, wave3BarTime, currentWaveDegree );



            if ( ( p1 == default ) || ( p3 == default ) )
            {
                return false;
            }

            var length_Wave0_Wave1 = Math.Abs( p1.wave0.Value - p1.wave1.Value );            
            var length_Wave2_Wave3 = Math.Abs( p3.wave2.Value - p3.wave3.Value );



            //RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value > p.wave3.Value ? p.wave3.Value : p.wave0.Value,
            //                                                            p.wave0.Value > p.wave3.Value ? p.wave0.Value : p.wave3.Value );

                //var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                //HHLL hhs = default;
                //HHLL hhll2 = default;

                //if ( trendDirection == TrendDirection.DownTrend )
                //{
                //    hhs = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave3BarIndex - wave0BarIndex ) );
                //}
                //else
                //{
                //    hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave3BarIndex - wave0BarIndex ) );
                //}

                //var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave0BarIndex, wave3BarIndex, -1, trendMovedRange, null, hhs, hhll2 );

                //output = priceTimeInfo;

            //    return true;
            
            

            return false;
        }

        public bool GetPriceTimeInfo_Wave0_Wave3C( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave3BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            if ( -1 == wave3BarTime )
            {
                return false;
            }

            var wave3BarIndex = bars.GetIndexByTime( wave3BarTime );

            var wave0BarTime = FindBeginningWaveTimeOfCurrentCycle(waveScenarioNo, period, wave3BarTime, currentWaveDegree );

            if ( -1 == wave0BarTime )
            {
                return false;
            }

            var wave0BarIndex = bars.GetIndexByTime( wave0BarTime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave0BarTime, wave3BarTime, currentWaveDegree ) )
            {
                ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave3 ) p = default;

                if ( wave0BarTime == wave3BarTime )
                {
                    p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave0BarTime, wave3BarTime, currentWaveDegree );
                }
                else
                {
                    p = GetWavePoints( waveScenarioNo, bars, period, wave0BarTime, wave3BarTime, currentWaveDegree );
                }

                if ( p != default )
                {
                    var length_Wave0_Wave3 = Math.Abs( p.wave0.Value - p.wave3.Value );

                    RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value > p.wave3.Value ? p.wave3.Value : p.wave0.Value,
                                                                           p.wave0.Value > p.wave3.Value ? p.wave0.Value : p.wave3.Value );

                    var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    HHLL hhs = default;
                    HHLL hhll2 = default;

                    if ( trendDirection == TrendDirection.DownTrend )
                    {
                        hhs = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave3BarIndex - wave0BarIndex ) );
                    }
                    else
                    {
                        hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave3BarIndex - wave0BarIndex ) );
                    }

                    var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave0BarIndex, wave3BarIndex, -1, trendMovedRange, null, hhs, hhll2 );

                    output = priceTimeInfo;

                    return true;
                }
            }

            return false;
        }



        public bool GetPriceTimeInfo_Wave0_Wave1C_Wave2_HigherTF( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave2BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            var wave1BarTime = FindPreviousWave1( waveScenarioNo, period, wave2BarTime, currentWaveDegree );

            if ( -1 == wave1BarTime )
            {
                return false;
            }

            var wave1BarIndex = bars.GetIndexByTime( wave1BarTime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave1BarTime, wave2BarTime, currentWaveDegree ) )
            {
                var wave0Time = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave1BarTime, currentWaveDegree );

                if ( -1 == wave0Time )
                {
                    return false;
                }

                var wave0BarIndex = bars.GetIndexByTime( wave0Time );

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave0Time, wave1BarTime, currentWaveDegree ) )
                {
                    ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave1C ) p = default;

                    if ( wave0Time == wave1BarTime )
                    {
                        p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave0Time, wave2BarTime, currentWaveDegree );
                    }
                    else
                    {
                        p = GetWavePoints( waveScenarioNo, bars, period, wave0Time, wave1BarTime, currentWaveDegree );
                    }

                    if ( p != default )
                    {
                        var wave2BarIndex = bars.GetIndexByTime( wave2BarTime );

                        (DateTime Time, float Value) wave2 = GetCurrentPoint( bars, period, wave2BarTime, labelPos );

                        var length_Wave0_Wave1C = Math.Abs( p.wave0.Value - p.wave1C.Value);

                        RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value> p.wave1C.Value? p.wave1C.Value: p.wave0.Item2,
                                                                                   p.wave0.Value> p.wave1C.Value? p.wave0.Value: p.wave1C.Value);

                        var length_wave1C_wave2 = Math.Abs( p.wave1C.Value- wave2.Value);

                        RangeEx< double > counterTrendMovedRange = new RangeEx< double >( p.wave1C.Value> wave2.Value? wave2.Value: p.wave1C.Item2,
                                                                                          p.wave1C.Value> wave2.Value? p.wave1C.Value: wave2.Value);

                        var ratio = Math.Round( ( length_wave1C_wave2 / length_Wave0_Wave1C ) * 100, 2 );

                        var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                        HHLL hhs = default;
                        HHLL hhll2 = default;

                        if ( trendDirection == TrendDirection.DownTrend )
                        {
                            hhs = new HHLL( HHLLEnum.HighHigh, ( uint ) ( wave2BarIndex - wave0BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave2BarIndex - wave1BarIndex ) );
                        }
                        else
                        {
                            hhs = new HHLL( HHLLEnum.LowLow, ( uint ) ( wave2BarIndex - wave0BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave2BarIndex - wave1BarIndex ) );
                        }

                        var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave0BarIndex, wave1BarIndex, wave2BarIndex, trendMovedRange, counterTrendMovedRange, hhs, hhll2 );

                        output = priceTimeInfo;

                        bool validRetracement = CheckRetracementValue( ratio, GlobalConstants.Wave2RetracementLevels );

                        if ( validRetracement )
                        {
                            output = priceTimeInfo;
                            return true;
                        }
                        else
                        {
                            if ( ratio < 100 )
                            {
                                output = priceTimeInfo;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool GetPriceTimeInfo_Wave0_Wave1C_Wave2_LowerTF( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave2BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            var wave1BarTime = FindPreviousWave1( waveScenarioNo, period, wave2BarTime, currentWaveDegree );

            if ( -1 == wave1BarTime )
            {
                return false;
            }

            var wave1BarIndex = bars.GetIndexByTime( wave1BarTime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave1BarTime, wave2BarTime, currentWaveDegree ) )
            {
                var wave0Time = FindBeginningWaveTimeOfCurrentCycle( waveScenarioNo, period, wave1BarTime, currentWaveDegree );

                if ( -1 == wave0Time )
                {
                    return false;
                }

                var wave0BarIndex = bars.GetIndexByTime( wave0Time );

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave0Time, wave1BarTime, currentWaveDegree ) )
                {
                    ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave1C ) p = default;

                    if ( wave0Time == wave1BarTime )
                    {
                        p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave0Time, wave2BarTime, currentWaveDegree );
                    }
                    else
                    {
                        p = GetWavePoints( waveScenarioNo, bars, period, wave0Time, wave1BarTime, currentWaveDegree );
                    }

                    if ( p != default )
                    {
                        var wave2BarIndex = bars.GetIndexByTime( wave2BarTime );

                        (DateTime Time, float Value ) wave2 = GetCurrentPoint( bars, period, wave2BarTime, labelPos );

                        var length_Wave0_Wave1C = Math.Abs( p.wave0.Value- p.wave1C.Value);

                        RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value> p.wave1C.Value? p.wave1C.Value: p.wave0.Value,
                                                                                   p.wave0.Value> p.wave1C.Value? p.wave0.Value: p.wave1C.Value);

                        var length_wave1C_wave2 = Math.Abs( p.wave1C.Value- wave2.Value);

                        RangeEx< double > counterTrendMovedRange = new RangeEx< double >( p.wave1C.Value> wave2.Value? wave2.Value: p.wave1C.Value,
                                                                                          p.wave1C.Value> wave2.Value? p.wave1C.Value: wave2.Value);

                        var ratio = Math.Round( ( length_wave1C_wave2 / length_Wave0_Wave1C ) * 100, 2 );

                        var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                        HHLL hhs = default;
                        HHLL hhll2 = default;

                        if ( trendDirection == TrendDirection.DownTrend )
                        {
                            hhs = new HHLL( HHLLEnum.HighHigh, ( uint ) ( wave2BarIndex - wave0BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave2BarIndex - wave1BarIndex ) );
                        }
                        else
                        {
                            hhs = new HHLL( HHLLEnum.LowLow, ( uint ) ( wave2BarIndex - wave0BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave2BarIndex - wave1BarIndex ) );
                        }

                        var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave0BarIndex, wave1BarIndex, wave2BarIndex, trendMovedRange, counterTrendMovedRange, hhs, hhll2 );

                        bool validRetracement = CheckRetracementValue( ratio, GlobalConstants.Wave2RetracementLevels );

                        if ( validRetracement )
                        {
                            output = priceTimeInfo;
                            return true;
                        }
                        else
                        {
                            var smallerCycle = currentWaveDegree - GlobalConstants.OneWaveCycle;

                            if ( smallerCycle != ElliottWaveCycle.UNKNOWN )
                            {
                                if ( GetPriceTimeInfo_Wave0_Wave1C_Wave2_LowerTF( waveScenarioNo, bars, period, wave2BarTime, smallerCycle, labelPos, ref priceTimeInfo ) )
                                {
                                    output = priceTimeInfo;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckRetracementValue( double ratio, float[ ] wave2RetracementLevels )
        {
            if ( ratio > wave2RetracementLevels[ 0 ] && ( ratio < wave2RetracementLevels[ wave2RetracementLevels.Length - 1 ] ) )
            {
                return true;
            }

            return false;
        }

        public bool GetPriceTimeInfo_Wave2_Wave3C_Wave4_HigherTF( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave4BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            var wave3BarTime = FindPreviousWave3(waveScenarioNo, period, wave4BarTime, currentWaveDegree );

            if ( -1 == wave3BarTime )
            {
                return false;
            }

            var wave3BarIndex = bars.GetIndexByTime( wave3BarTime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave3BarTime, wave4BarTime, currentWaveDegree ) )
            {
                var wave2BarTime = FindPreviousWave2( waveScenarioNo, period, wave3BarTime, currentWaveDegree );

                if ( -1 == wave2BarTime )
                {
                    return false;
                }

                var wave2BarIndex = bars.GetIndexByTime( wave2BarTime );

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave2BarTime, wave3BarTime, currentWaveDegree ) )
                {
                    ((DateTime Time, float Value) wave2, (DateTime Time, float Value) wave3 ) p = default;

                    if ( wave2BarTime == wave3BarTime )
                    {
                        p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave2BarTime, wave4BarTime, currentWaveDegree );
                    }
                    else
                    {
                        p = GetWavePoints( waveScenarioNo, bars, period, wave2BarTime, wave3BarTime, currentWaveDegree );
                    }

                    if ( p != default )
                    {
                        var wave4BarIndex = bars.GetIndexByTime( wave4BarTime );
                        (DateTime Time, float Value ) wave4 = GetCurrentPoint( bars, period, wave4BarTime, labelPos );

                        var length_Wave2_Wave3C = Math.Abs( p.wave2.Value- p.wave3.Value);

                        RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave2.Value> p.wave3.Value? p.wave3.Value: p.wave2.Value,
                                                                                   p.wave2.Value> p.wave3.Value? p.wave2.Value: p.wave3.Value);

                        var length_wave3C_wave4 = Math.Abs( p.wave3.Value- wave4.Value);
                        RangeEx< double > counterTrendMovedRange = new RangeEx< double >( p.wave3.Value> wave4.Value? wave4.Value: p.wave3.Item2,
                                                                                          p.wave3.Value> wave4.Value? p.wave3.Value: wave4.Value);

                        var ratio = Math.Round( ( length_wave3C_wave4 / length_Wave2_Wave3C ) * 100, 2 );

                        var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                        HHLL hhs = default;
                        HHLL hhll2 = default;

                        if ( trendDirection == TrendDirection.DownTrend )
                        {
                            hhs = new HHLL( HHLLEnum.HighHigh, ( uint ) ( wave4BarIndex - wave2BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave4BarIndex - wave3BarIndex ) );
                        }
                        else
                        {
                            hhs = new HHLL( HHLLEnum.LowLow, ( uint ) ( wave4BarIndex - wave2BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave4BarIndex - wave3BarIndex ) );
                        }

                        var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave2BarIndex, bars.GetIndexByTime( wave3BarTime ), wave4BarIndex, trendMovedRange, counterTrendMovedRange, hhs, hhll2 );

                        bool validRetracement = CheckRetracementValue( ratio, GlobalConstants.Wave4RetracementLevels );

                        if ( validRetracement )
                        {
                            output = priceTimeInfo;
                            return true;
                        }
                        else
                        {
                            var largerCycle = currentWaveDegree + GlobalConstants.OneWaveCycle;

                            if ( largerCycle != ElliottWaveCycle.MAX )
                            {
                                if ( GetPriceTimeInfo_Wave2_Wave3C_Wave4_HigherTF( waveScenarioNo, bars, period, wave4BarTime, largerCycle, labelPos, ref priceTimeInfo ) )
                                {
                                    output = priceTimeInfo;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool GetPriceTimeInfo_Wave2_Wave3C_Wave4_LowerTF( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long wave4BarTime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            var wave3BarTime = FindPreviousWave3( waveScenarioNo, period, wave4BarTime, currentWaveDegree );

            if ( -1 == wave3BarTime )
            {
                return false;
            }

            var wave3BarIndex = bars.GetIndexByTime( wave3BarTime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, wave3BarTime, wave4BarTime, currentWaveDegree ) )
            {
                var wave2BarTime = FindPreviousWave2( waveScenarioNo, period, wave3BarTime, currentWaveDegree );

                if ( -1 == wave2BarTime )
                {
                    return false;
                }

                var wave2BarIndex = bars.GetIndexByTime( wave2BarTime );

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave2BarTime, wave3BarTime, currentWaveDegree ) )
                {
                    ((DateTime Time, float Value) wave2, (DateTime Time, float Value) wave3C) p = default;

                    if ( wave2BarTime == wave3BarTime )
                    {
                        p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave2BarTime, wave4BarTime, currentWaveDegree );
                    }
                    else
                    {
                        p = GetWavePoints( waveScenarioNo, bars, period, wave2BarTime, wave3BarTime, currentWaveDegree );
                    }

                    if ( p != default )
                    {
                        var wave4BarIndex = bars.GetIndexByTime( wave4BarTime );
                        (DateTime Time, float Value) wave4 = GetCurrentPoint( bars, period, wave4BarTime, labelPos );

                        var length_Wave2_Wave3C = Math.Abs( p.wave2.Value- p.wave3C.Value);
                        RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave2.Value> p.wave3C.Value? p.wave3C.Value: p.wave2.Item2,
                                                                                   p.wave2.Value> p.wave3C.Value? p.wave2.Value: p.wave3C.Value);

                        var length_wave3C_wave4 = Math.Abs( p.wave3C.Value- wave4.Value);

                        RangeEx< double > counterTrendMovedRange = new RangeEx< double >( p.wave3C.Value> wave4.Value? wave4.Value: p.wave3C.Item2,
                                                                                          p.wave3C.Value> wave4.Value? p.wave3C.Value: wave4.Value);

                        var ratio = Math.Round( ( length_wave3C_wave4 / length_Wave2_Wave3C ) * 100, 2 );

                        var trendDirection = ( labelPos == WaveLabelPosition.TOP ) ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                        HHLL hhs = default;
                        HHLL hhll2 = default;

                        if ( trendDirection == TrendDirection.DownTrend )
                        {
                            hhs = new HHLL( HHLLEnum.HighHigh, ( uint ) ( wave4BarIndex - wave2BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( wave4BarIndex - wave3BarIndex ) );
                        }
                        else
                        {
                            hhs = new HHLL( HHLLEnum.LowLow, ( uint ) ( wave4BarIndex - wave2BarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.HighLow, ( uint ) ( wave4BarIndex - wave3BarIndex ) );
                        }

                        var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave2BarIndex, bars.GetIndexByTime( wave3BarTime ), wave4BarIndex, trendMovedRange, counterTrendMovedRange, hhs, hhll2 );

                        bool validRetracement = CheckRetracementValue( ratio, GlobalConstants.Wave4RetracementLevels );

                        if ( validRetracement )
                        {
                            output = priceTimeInfo;
                            return true;
                        }
                        else
                        {
                            var largerCycle = currentWaveDegree + GlobalConstants.OneWaveCycle;

                            if ( largerCycle != ElliottWaveCycle.MAX )
                            {
                                if ( GetPriceTimeInfo_Wave2_Wave3C_Wave4_LowerTF( waveScenarioNo, bars, period, wave4BarTime, largerCycle, labelPos, ref priceTimeInfo ) )
                                {
                                    output = priceTimeInfo;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool GetPriceTimeInfo_Wave024X_WaveA_WaveB_HigherTF( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long waveBtime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            var waveAtime = FindPreviousWaveA( waveScenarioNo, period, waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return false;
            }

            var waveABarIndex = bars.GetIndexByTime( waveAtime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var wave024Xtime = FindPreviousWave024X( waveScenarioNo, period, waveAtime, currentWaveDegree );

                if ( -1 == wave024Xtime )
                {
                    return false;
                }

                var wave024XBarIndex = bars.GetIndexByTime( wave024Xtime );

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
                {
                    ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave1C ) p = default;

                    if ( wave024Xtime == waveAtime )
                    {
                        p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveBtime, currentWaveDegree );
                    }
                    else
                    {
                        p = GetWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveAtime, currentWaveDegree );
                    }

                    if ( p != default )
                    {
                        var waveBBarIndex = bars.GetIndexByTime( waveBtime );
                        (DateTime Time, float Value) waveB = GetCurrentPoint( bars, period, waveBtime, labelPos );

                        var length_Wave0_Wave1C = Math.Abs( p.wave0.Value- p.wave1C.Value);

                        RangeEx< double > trendMovedRange = new RangeEx< double >( p.wave0.Value> p.wave1C.Value? p.wave1C.Value: p.wave0.Item2,
                                                                                   p.wave0.Value> p.wave1C.Value? p.wave0.Value: p.wave1C.Value);

                        var length_wave1C_wave2 = Math.Abs( p.wave1C.Value- waveB.Value);

                        RangeEx< double > counterTrendMovedRange = new RangeEx< double >( p.wave1C.Value> waveB.Value? waveB.Value: p.wave1C.Item2,
                                                                                          p.wave1C.Value> waveB.Value? p.wave1C.Value: waveB.Value);

                        var ratio = Math.Round( ( length_wave1C_wave2 / length_Wave0_Wave1C ) * 100, 2 );

                        var trendDirection = labelPos == WaveLabelPosition.TOP ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                        HHLL hhs = default;
                        HHLL hhll2 = default;

                        if ( trendDirection == TrendDirection.DownTrend )
                        {
                            hhs = new HHLL( HHLLEnum.HighHigh, ( uint ) ( waveBBarIndex - wave024XBarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( waveBBarIndex - waveABarIndex ) );
                        }
                        else
                        {
                            hhs = new HHLL( HHLLEnum.LowLow, ( uint ) ( waveBBarIndex - wave024XBarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.HighLow, ( uint ) ( waveBBarIndex - waveABarIndex ) );
                        }

                        var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave024XBarIndex, waveABarIndex, waveBBarIndex, trendMovedRange, counterTrendMovedRange, hhs, hhll2 );

                        bool validRetracement = CheckRetracementValue( ratio, GlobalConstants.ABCWaveBRetracementLevels );

                        if ( validRetracement )
                        {
                            output = priceTimeInfo;
                            return true;
                        }
                        else
                        {
                            var largerCycle = currentWaveDegree + GlobalConstants.OneWaveCycle;

                            if ( largerCycle != ElliottWaveCycle.MAX )
                            {
                                if ( GetPriceTimeInfo_Wave024X_WaveA_WaveB_HigherTF( waveScenarioNo, bars, period, waveBtime, largerCycle, labelPos, ref output ) )
                                {
                                    output = priceTimeInfo;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }



        public bool GetPriceTimeInfo_Wave024X_WaveA_WaveB_LowerTF( int waveScenarioNo, fxHistoricBarsRepo bars, TimeSpan period, long waveBtime, ElliottWaveCycle currentWaveDegree, WaveLabelPosition labelPos, ref WavePriceTimeInfo output )
        {
            var waveAtime = FindPreviousWaveA( waveScenarioNo, period, waveBtime, currentWaveDegree );

            if ( -1 == waveAtime )
            {
                return false;
            }

            var waveABarIndex = bars.GetIndexByTime( waveAtime );

            if ( NoHigherWaveBetween( waveScenarioNo, period, waveAtime, waveBtime, currentWaveDegree ) )
            {
                var wave024Xtime = FindPreviousWave024X( waveScenarioNo, period, waveAtime, currentWaveDegree );

                if ( -1 == wave024Xtime )
                {
                    return false;
                }

                var wave024XBarIndex = bars.GetIndexByTime( wave024Xtime );

                if ( NoHigherWaveBetween( waveScenarioNo, period, wave024Xtime, waveAtime, currentWaveDegree ) )
                {
                    ((DateTime Time, float Value) wave0, (DateTime Time, float Value) wave1C ) p = default;

                    if ( wave024Xtime == waveAtime )
                    {
                        p = GetSpecialWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveBtime, currentWaveDegree );
                    }
                    else
                    {
                        p = GetWavePoints( waveScenarioNo, bars, period, wave024Xtime, waveAtime, currentWaveDegree );
                    }

                    if ( p != default )
                    {
                        var waveBBarIndex = bars.GetIndexByTime( waveBtime );
                        (DateTime Time, float Value) waveB = GetCurrentPoint( bars, period, waveBtime, labelPos );

                        var length_Wave0_Wave1C = Math.Abs( p.wave0.Value- p.wave1C.Value);
                        var trendMovedRange = new RangeEx< double >( p.wave0.Value> p.wave1C.Value? p.wave1C.Value: p.wave0.Value, p.wave0.Value> p.wave1C.Value? p.wave0.Value: p.wave1C.Value);

                        var length_wave1C_wave2 = Math.Abs( p.wave1C.Value- waveB.Value);
                        var counterTrendMovedRange = new RangeEx< double >( p.wave1C.Value> waveB.Value? waveB.Value: p.wave1C.Value, p.wave1C.Value> waveB.Value? p.wave1C.Value: waveB.Value);
                        var ratio = Math.Round( ( length_wave1C_wave2 / length_Wave0_Wave1C ) * 100, 2 );

                        var trendDirection = labelPos == WaveLabelPosition.TOP ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                        HHLL hhs = default;
                        HHLL hhll2 = default;

                        if ( trendDirection == TrendDirection.DownTrend )
                        {
                            hhs = new HHLL( HHLLEnum.HighHigh, ( uint ) ( waveBBarIndex - wave024XBarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.LowHigh, ( uint ) ( waveBBarIndex - waveABarIndex ) );
                        }
                        else
                        {
                            hhs = new HHLL( HHLLEnum.LowLow, ( uint ) ( waveBBarIndex - wave024XBarIndex ) );
                            hhll2 = new HHLL( HHLLEnum.HighLow, ( uint ) ( waveBBarIndex - waveABarIndex ) );
                        }

                        var priceTimeInfo = new WavePriceTimeInfo( trendDirection,  wave024XBarIndex, bars.GetIndexByTime( waveAtime ) , waveBBarIndex, trendMovedRange, counterTrendMovedRange, hhs, hhll2 );

                        bool validRetracement = CheckRetracementValue( ratio, GlobalConstants.ABCWaveBRetracementLevels );

                        if ( validRetracement )
                        {
                            output = priceTimeInfo;
                            return true;
                        }
                        else
                        {
                            var largerCycle = currentWaveDegree + GlobalConstants.OneWaveCycle;

                            if ( largerCycle != ElliottWaveCycle.MAX )
                            {
                                if ( GetPriceTimeInfo_Wave024X_WaveA_WaveB_LowerTF( waveScenarioNo, bars, period, waveBtime, largerCycle, labelPos, ref output ) )
                                {
                                    output = priceTimeInfo;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
