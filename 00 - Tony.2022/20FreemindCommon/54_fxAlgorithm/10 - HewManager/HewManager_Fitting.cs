using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using System.Linq;
using fx.Definitions.UndoRedo;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using fx.Common;
using fx.Collections;
using fx.Bars;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        private long _verifyBeginTime = -1;
        private long _verifyEndTime = -1;
        private ElliottWaveEnum _verifyWave = ElliottWaveEnum.NONE;
        private TimeSpan _verifyPeriod;
        private TrendDirection _verifyTrend = TrendDirection.NoTrend;

        public void SmartAddWave_ABC_to_45(
                                                int waveScenarioNo,
                                                long bartimeWave4,
                                                TimeSpan period,
                                                ElliottWaveCycle waveCycle )
        {
            ref SBar Wave4bar = ref _bars.GetBarByTime( bartimeWave4 );

            if ( Wave4bar != SBar.EmptySBar )                
            {
                ref var hew4 = ref Wave4bar.GetWaveFromScenario( waveScenarioNo );

                if ( hew4.HasElliottWave )
                {
                    var wave4Label = hew4.GetWaveLabelPosition();

                    var wave4Cycle = hew4.GetFirstHighestWaveInfo( ).Value.WaveCycle;

                    var trend = TrendDirection.NoTrend;

                    if ( wave4Label == WaveLabelPosition.TOP )
                    {
                        trend = TrendDirection.DownTrend;
                    }
                    else if ( wave4Label == WaveLabelPosition.BOTTOM )
                    {
                        trend = TrendDirection.Uptrend;
                    }

                    if ( trend != TrendDirection.NoTrend )
                    {                        
                        SBar breakingBar = default; ;

                        if ( _bars.GetBarThatBreakWaveFourAndExtremumIndex( ref Wave4bar, trend, out SBar Wave5bar, ref breakingBar ) )
                        {
                            if ( Wave5bar != default )
                            {
                                // If we already have Wave A, Wave B label in the software, we shouldn't bother to be smart
                                if ( HasWave( waveScenarioNo, bartimeWave4, Wave5bar.LinuxTime, ElliottWaveEnum.WaveA, waveCycle ) || HasWave( waveScenarioNo, bartimeWave4, Wave5bar.LinuxTime, ElliottWaveEnum.WaveB, waveCycle ) )
                                {
                                    return;
                                }

                                if ( ( Wave5bar.BarIndex - Wave4bar.BarIndex ) >= 2 )
                                {
                                    if ( NoHigherWaveBetween( waveScenarioNo, Wave4bar.LinuxTime, breakingBar.LinuxTime, wave4Cycle ) )
                                    {
                                        var inBetweenZigZag = FindImportantWavePointsBetween( period, bartimeWave4, Wave5bar.LinuxTime );

                                        int zzCount = inBetweenZigZag.Count;

                                        if ( zzCount > 0 )
                                        {
                                            var wav5BTime = FindWave5BLinuxTime( inBetweenZigZag, trend );

                                            if ( wav5BTime != -1 )
                                            {
                                                var inBetweenWave4_Wave5B = FindImportantWavePointsBetween( period, bartimeWave4, wav5BTime );

                                                if ( inBetweenWave4_Wave5B.Count > 0 )
                                                {
                                                    long wave5Atime = GetWaveAOfWave5( trend, bartimeWave4, wav5BTime, Wave5bar.LinuxTime, inBetweenWave4_Wave5B );

                                                    if ( wave5Atime != -1 )
                                                    {                                                                                                                                                                        
                                                        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, wave5Atime, wave4Cycle, ( ( wave4Label == WaveLabelPosition.TOP ) ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ), ElliottWaveEnum.WaveA );
                                                        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, wav5BTime, wave4Cycle, ( ( wave4Label == WaveLabelPosition.TOP ) ? WaveLabelPosition.TOP : WaveLabelPosition.BOTTOM ), ElliottWaveEnum.WaveB );
                                                        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, Wave5bar.LinuxTime, wave4Cycle, ( ( wave4Label == WaveLabelPosition.TOP ) ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP ), ElliottWaveEnum.Wave5C );

                                                        WaveInfo? higherWave = GetContainingWaveOfThisWave5( waveScenarioNo, Wave5bar.LinuxTime, wave4Cycle );

                                                        if ( higherWave != null )
                                                        {
                                                            if ( higherWave.Value.WaveName == ElliottWaveEnum.WaveC )
                                                            {
                                                                InternalAddWaveC( waveScenarioNo, period, Wave5bar.LinuxTime, higherWave.Value.WaveCycle, ref Wave5bar );
                                                            }
                                                            else
                                                            {
                                                                DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, Wave5bar.LinuxTime, higherWave.Value.WaveCycle, higherWave.Value.LabelPosition, higherWave.Value.WaveName );
                                                            }
                                                        }
                                                        
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SmartAddWaveXtoManagerAndBar( waveScenarioNo, period, Wave5bar.LinuxTime, wave4Cycle, ElliottWaveEnum.Wave5, ref Wave5bar );
                                        }
                                    }
                                    else
                                    {
                                        // That means we have higher wave degree in between current wave 4 and our calculated Wave 5. So in a way, our current wave of higher degree should be the end of Wave 5.

                                        var dbHewNext = GetNextWaveStructure( waveScenarioNo, bartimeWave4 );

                                        ref var hew = ref dbHewNext.GetWaveFromScenario( waveScenarioNo );

                                        var allWaves = hew.GetAllWaves( );

                                        var inBetweenZigZag = FindImportantWavePointsBetween( period, bartimeWave4, dbHewNext.StartDate );

                                        ElliottWaveEnum desiredWaveName = ElliottWaveEnum.NONE;


                                        ref SBar Wave5bar2 = ref _bars.GetBarByTime( dbHewNext.StartDate );

                                        if ( Wave5bar2 != SBar.EmptySBar )                                            
                                        {
                                            if ( inBetweenZigZag.Count >= 2 )
                                            {
                                                desiredWaveName = ElliottWaveEnum.Wave5C;
                                            }
                                            else
                                            {
                                                desiredWaveName = ElliottWaveEnum.Wave5;
                                            }

                                            foreach ( var oneWave in allWaves )
                                            {
                                                if ( ( oneWave.WaveName == ElliottWaveEnum.WaveA || oneWave.WaveName == ElliottWaveEnum.WaveC ) && ( ( oneWave.WaveCycle - wave4Cycle ) == GlobalConstants.OneWaveCycle ) )
                                                {
                                                    DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, Wave5bar2.LinuxTime, wave4Cycle, oneWave.LabelPosition, desiredWaveName );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private long FindWave5BLinuxTime( PooledList<WavePointImportance> inBetweenZigZag, TrendDirection trend )
        {
            long wave5BTime = -1;

            double extremum = 0;

            if ( trend == TrendDirection.Uptrend )
            {
                extremum = double.MaxValue;

                foreach ( WavePointImportance wavePointImportance in inBetweenZigZag )
                {
                    if ( wavePointImportance.Signal == TASignal.WAVE_TROUGH || wavePointImportance.Signal == TASignal.GANN_TROUGH )
                    {
                        ref SBar extremumBar = ref _bars.GetBarByTime( wavePointImportance.LinuxTime );

                        if ( extremumBar != SBar.EmptySBar )                            
                        {
                            if ( extremumBar.Low < extremum )
                            {
                                extremum = extremumBar.Low;
                                wave5BTime = extremumBar.LinuxTime;
                            }
                        }
                    }
                }
            }
            else if ( trend == TrendDirection.DownTrend )
            {
                extremum = double.MinValue;

                foreach ( WavePointImportance wavePointImportance in inBetweenZigZag )
                {
                    if ( wavePointImportance.Signal == TASignal.WAVE_PEAK || wavePointImportance.Signal == TASignal.GANN_PEAK )
                    {
                        ref SBar extremumBar = ref _bars.GetBarByTime( wavePointImportance.LinuxTime );

                        if ( extremumBar != SBar.EmptySBar )
                        {
                            if ( extremumBar.High > extremum )
                            {
                                extremum = extremumBar.High;
                                wave5BTime = extremumBar.LinuxTime;
                            }
                        }
                    }
                }
            }

            return wave5BTime;
        }

        private long GetWaveAOfWave5( TrendDirection trend,
                                      long bartimeWave4,
                                      long bartimeWaveB,
                                      long bartimeWaveC,
                                      PooledList<WavePointImportance> inBetweenWave4_Wave5B )
        {
            long bartimeWaveA = -1;
            double extremum = 0;

            if ( trend == TrendDirection.Uptrend )
            {
                extremum = double.MinValue;

                foreach ( WavePointImportance waveA in inBetweenWave4_Wave5B )
                {
                    ref SBar bar = ref _bars.GetBarByTime( waveA.LinuxTime );

                    if ( bar != SBar.EmptySBar )
                    {
                        if ( bar.High > extremum )
                        {
                            extremum = bar.High;
                            bartimeWaveA = waveA.LinuxTime;
                        }
                    }
                }
            }
            else if ( trend == TrendDirection.DownTrend )
            {
                extremum = double.MaxValue;

                foreach ( WavePointImportance waveA in inBetweenWave4_Wave5B )
                {
                    ref SBar bar = ref _bars.GetBarByTime( waveA.LinuxTime );

                    if ( bar != SBar.EmptySBar )
                    {
                        if ( bar.Low < extremum )
                        {
                            extremum = bar.Low;
                            bartimeWaveA = waveA.LinuxTime;
                        }
                    }
                }
            }

            float totalMissedPips = VerifyHarmonicRatioForWaveABC( trend, bartimeWave4, bartimeWaveA, bartimeWaveB, bartimeWaveC );

            if ( totalMissedPips > 0.0010 )
            {
                //Messenger.Default.Send( new WorkDoneMessage( "Save Waves to Database" ) );
            }

            return bartimeWaveA;
        }

        private float VerifyHarmonicRatioForWaveABC( TrendDirection trend,
                                                     long baseBarTime,
                                                     long barTimeWaveA,
                                                     long barTimeWaveB,
                                                     long barTimeWaveC )
        {

            ref SBar barBase = ref _bars.GetBarByTime( baseBarTime );

            if ( barBase == SBar.EmptySBar ) return 0;

            ref SBar barWaveA = ref _bars.GetBarByTime( barTimeWaveA );

            if ( barWaveA == SBar.EmptySBar ) return 0;

            ref SBar barWaveB = ref _bars.GetBarByTime( barTimeWaveB );

            if ( barWaveB == SBar.EmptySBar ) return 0;

            ref SBar barWaveC = ref _bars.GetBarByTime( barTimeWaveC );

            if ( barWaveC == SBar.EmptySBar ) return 0;
            
            float basePoint = 0f;
            float waveA = 0f;
            float waveB = 0f;
            float waveC = 0f;

            float length0A = -1;
            float lengthBC = -1;

            if ( trend == TrendDirection.Uptrend )
            {
                basePoint = ( float ) barBase.Low;
                waveA = ( float ) barWaveA.High;
                waveB = ( float ) barWaveB.Low;
                waveC = ( float ) barWaveC.High;

                length0A = waveA - basePoint;
                lengthBC = waveC - waveB;

            }
            else if ( trend == TrendDirection.DownTrend )
            {
                basePoint = ( float ) barBase.High;
                waveA = ( float ) barWaveA.Low;
                waveB = ( float ) barWaveB.High;
                waveC = ( float ) barWaveC.Low;

                length0A = basePoint - waveA;
                lengthBC = waveB - waveC;
            }

            return Math.Abs( length0A - lengthBC );
        }

        public void SmartAdd_12345_InWaveA( int waveScenarioNo, long bartimeWaveA )
        {
            ref SBar barWaveA = ref _bars.GetBarByTime( bartimeWaveA );
            
            if ( barWaveA != SBar.EmptySBar )            
            {
                ref var hewA = ref barWaveA.GetWaveFromScenario( waveScenarioNo );

                var cycleWaveA = hewA.GetFirstHighestWaveInfo( ).Value.WaveCycle;
                long beginWaveTime = FindBeginningWaveOfCurrentABCX( waveScenarioNo, bartimeWaveA, cycleWaveA );

                if ( beginWaveTime > -1 )
                {
                    if ( NoHigherWaveBetween( waveScenarioNo, beginWaveTime, bartimeWaveA, cycleWaveA ) )
                    {
                        var inBetweenZigZag = FindImportantWavePointsBetween( _currentActiveTimeFrame, beginWaveTime, bartimeWaveA );
                        var inBetweenGannSw = FindGannSwingBetween( _currentActiveTimeFrame, beginWaveTime, bartimeWaveA );

                        if ( inBetweenZigZag.Count >= 4 || inBetweenGannSw.Count >= 4 )
                        {
                            inBetweenZigZag.Sort();

                            inBetweenGannSw.Sort();


                            ref SBar previousBar = ref _bars.GetBarByTime( beginWaveTime );

                            if ( previousBar != SBar.EmptySBar )
                            {
                                if ( _bars.IsUpTrend( previousBar.LinuxTime, bartimeWaveA ) )
                                {
                                    SmartFillWavesToZigZag( beginWaveTime, bartimeWaveA, inBetweenZigZag, inBetweenGannSw, TrendDirection.Uptrend );
                                    VerifyWave12345_LowerTimeFrame( TrendDirection.Uptrend, ElliottWaveEnum.WaveA, beginWaveTime, bartimeWaveA );
                                }
                                else if ( _bars.IsDownTrend( previousBar.LinuxTime, bartimeWaveA ) )
                                {
                                    SmartFillWavesToZigZag( beginWaveTime, bartimeWaveA, inBetweenZigZag, inBetweenGannSw, TrendDirection.DownTrend );
                                    VerifyWave12345_LowerTimeFrame( TrendDirection.DownTrend, ElliottWaveEnum.WaveA, beginWaveTime, bartimeWaveA );
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SmartAdd_12345_InWaveC( int waveScenarioNo, long bartimeWaveC )
        {
            ref SBar barWaveC = ref _bars.GetBarByTime( bartimeWaveC );

            if ( barWaveC != SBar.EmptySBar )                
            {
                ref var hewC = ref barWaveC.GetWaveFromScenario( waveScenarioNo );

                var cycleWaveC = hewC.GetFirstHighestWaveInfo( ).Value.WaveCycle;

                var previousWave = GetPreviousWaveStructureOfDegree( waveScenarioNo, bartimeWaveC, cycleWaveC );

                if ( previousWave != null )
                {
                    if ( NoHigherWaveBetween( waveScenarioNo, previousWave.StartDate, bartimeWaveC, cycleWaveC ) )
                    {
                        var inBetweenZigZag = FindImportantWavePointsBetween( _currentActiveTimeFrame, previousWave.StartDate, bartimeWaveC );

                        var inBetweenGannSw = FindGannSwingBetween( _currentActiveTimeFrame, previousWave.StartDate, bartimeWaveC );

                        int zzCount = inBetweenZigZag.Count;

                        if ( inBetweenZigZag.Count >= 4 || inBetweenGannSw.Count >= 4 )
                        {
                            inBetweenZigZag.Sort();

                            inBetweenGannSw.Sort();


                            ref SBar previousBar = ref _bars.GetBarByTime( previousWave.StartDate );

                            if ( previousBar != SBar.EmptySBar )
                            {
                                if ( _bars.IsUpTrend( previousBar.LinuxTime, bartimeWaveC ) )
                                {
                                    SmartFillWavesToZigZag( previousWave.StartDate, bartimeWaveC, inBetweenZigZag, inBetweenGannSw, TrendDirection.Uptrend );
                                }
                                else if ( _bars.IsDownTrend( previousBar.LinuxTime, bartimeWaveC ) )
                                {
                                    SmartFillWavesToZigZag( previousWave.StartDate, bartimeWaveC, inBetweenZigZag, inBetweenGannSw, TrendDirection.DownTrend );
                                }
                            }


                        }
                    }
                }
            }
        }

        public void VerifyWave12345_LowerTimeFrame( TrendDirection trendDirection,
                                                    ElliottWaveEnum waveName,
                                                    long beginningBarTime,
                                                    long endBarTime )
        {



            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                _verifyPeriod = aa.GetOneTimeSpanLower( _currentActiveTimeFrame );
            }
            else
            {
                return;
            }

            _verifyTrend = trendDirection;

            ref SBar selectedBar = ref _bars.GetBarByTime( beginningBarTime );
            
            if ( ( selectedBar != SBar.EmptySBar ) && _verifyPeriod >= TimeSpan.FromMinutes( 1 ) )
            {
                var bars = GetDatabarsRepository( _verifyPeriod );
                _verifyBeginTime = beginningBarTime;
                _verifyEndTime = endBarTime;
                _verifyWave = waveName;

                int index = bars.GetIndexByTime( beginningBarTime );

                if ( index != -1 )
                {
                    StartPerformingWaveFindingTask( _verifyPeriod, _verifyBeginTime, _verifyEndTime, _verifyWave );
                }
                else
                {
                    var endTime = bars[ 0 ].BarTime - _verifyPeriod;

                    var beginTime = beginningBarTime.FromLinuxTime( );

                    beginTime = beginTime.AddMinutes( -_verifyPeriod.TotalMinutes * 100 );

                    bars.Indicators.IndicatorManagerFullCalculationDoneEvent += Indicators_IndicatorFullCalculationDoneEvent;

                    bars.DownloadDatabarsFromXtoY( beginTime, endTime );
                }
            }
        }

        private long FindWave2OfA( long bartimePreviousWave,
                                   long bartimeWaveA,
                                   PooledList<WavePointImportance> inBetweenZigZag,
                                   TrendDirection trendDirection )
        {
            long wave2RawBarTime = -1;

            double extremum = 0;

            if ( trendDirection == TrendDirection.Uptrend )
            {
                extremum = double.MaxValue;

                foreach ( WavePointImportance wavePointImportance in inBetweenZigZag )
                {
                    ref SBar barWave2 = ref _bars.GetBarByTime( wavePointImportance.LinuxTime );

                    if ( barWave2 != SBar.EmptySBar )
                    {
                        var waveExtremum = barWave2.IsWavePeak( ) ? barWave2.High : barWave2.Low;

                        if ( waveExtremum < extremum )
                        {
                            extremum = waveExtremum;
                            wave2RawBarTime = barWave2.LinuxTime;
                        }
                    }
                }
            }
            else if ( trendDirection == TrendDirection.DownTrend )
            {
                extremum = double.MinValue;

                foreach ( WavePointImportance wavePointImportance in inBetweenZigZag )
                {
                    ref SBar barWave2 = ref _bars.GetBarByTime( wavePointImportance.LinuxTime );

                    if ( barWave2 != SBar.EmptySBar )
                    {
                        var waveExtremum = barWave2.IsWavePeak( ) ? barWave2.High : barWave2.Low;

                        if ( waveExtremum > extremum )
                        {
                            extremum = waveExtremum;
                            wave2RawBarTime = barWave2.LinuxTime;
                        }
                    }
                }
            }

            return ( wave2RawBarTime );
        }

        private long FindWave1OfA( long bartimePreviousWave,
                                   long wave2RawBarTime,
                                   PooledList<WavePointImportance> inBetweenZigZag,
                                   TrendDirection trendDirection )
        {
            var wave1Points = inBetweenZigZag.FindAll( x => x.LinuxTime < wave2RawBarTime );

            long wave1RawBarTime = -1;

            double extremum = 0;

            if ( trendDirection == TrendDirection.Uptrend )
            {
                extremum = double.MinValue;

                foreach ( WavePointImportance wavePointImportance in inBetweenZigZag )
                {
                    ref SBar barWave1 = ref _bars.GetBarByTime( wavePointImportance.LinuxTime );

                    if ( barWave1 != SBar.EmptySBar )                        
                    {
                        var waveExtremum = barWave1.IsWavePeak( ) ? barWave1.High : barWave1.Low;

                        if ( waveExtremum > extremum )
                        {
                            extremum = waveExtremum;
                            wave1RawBarTime = barWave1.LinuxTime;
                        }
                    }
                }
            }
            else if ( trendDirection == TrendDirection.DownTrend )
            {
                extremum = double.MaxValue;

                foreach ( WavePointImportance wavePointImportance in inBetweenZigZag )
                {
                    ref SBar barWave1 = ref _bars.GetBarByTime( wavePointImportance.LinuxTime );

                    if ( barWave1 != SBar.EmptySBar )
                    {
                        var waveExtremum = barWave1.IsWavePeak( ) ? barWave1.High : barWave1.Low;

                        if ( waveExtremum < extremum )
                        {
                            extremum = waveExtremum;
                            wave1RawBarTime = barWave1.LinuxTime;
                        }
                    }
                }
            }

            return ( wave1RawBarTime );
        }

        private void SmartFillWavesToZigZag( long bartimePreviousWave,
                                             long bartimeWaveA,
                                             PooledList<WavePointImportance> inBetweenZigZag,
                                             PooledList<WavePointImportance> inBetweenGannSwing,
                                             TrendDirection trendDirection )
        {
            long wave2RawBarTime = FindWave2OfA( bartimePreviousWave, bartimeWaveA, inBetweenZigZag, trendDirection );

            long wave1RawBarTime = FindWave1OfA( bartimePreviousWave, wave2RawBarTime, inBetweenZigZag, trendDirection );
        }

        public bool HasWave( int waveScenarioNo, long beginPointTime, long endPointTime, ElliottWaveEnum elliottWaveName, ElliottWaveCycle waveCycle )
        {
            if ( _hews.Count > 0 )
            {
                var previousWaves = _hews.GetElementsRightOf( endPointTime );

                foreach ( var previousWave in previousWaves )
                {
                    if ( previousWave.Key >= beginPointTime )
                    {
                        ref var hew = ref previousWave.Value.GetWaveFromScenario( waveScenarioNo );

                        var allWaves = hew.GetAllWaves( );

                        foreach ( var wave in allWaves )
                        {
                            if ( wave.WaveName == elliottWaveName && wave.WaveCycle == waveCycle )
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void SmartAdd_ABC_ToWave( int waveScenarioNo, TimeSpan period,
                                         long beginPointTime,
                                         long endPointTime,
                                         ElliottWaveCycle waveCycle )
        {
            if ( beginPointTime == -1 || endPointTime == -1 )
            {
                return;
            }

            if ( HasWave( waveScenarioNo, beginPointTime, endPointTime, ElliottWaveEnum.WaveA, waveCycle ) || HasWave( waveScenarioNo, beginPointTime, endPointTime, ElliottWaveEnum.WaveB, waveCycle ) )
            {
                return;
            }

            var inBetweenZigZag = FindImportantWavePointsBetween( _currentActiveTimeFrame, beginPointTime, endPointTime );

            var inBetweenGannSwing = FindGannSwingBetween( _currentActiveTimeFrame, beginPointTime, endPointTime );

            //int nenZigZagCount = inBetweenZigZag.Count;

            //int gannSwingCount = inBetweenGannSwing.Count;

            long bartimeWaveBegin = beginPointTime;
            long bartimeWaveC = endPointTime;

            TrendDirection trend = TrendDirection.NoTrend;

            if ( _bars.IsUpTrend( beginPointTime, endPointTime ) )
            {
                trend = TrendDirection.Uptrend;
            }
            else if ( _bars.IsDownTrend( beginPointTime, endPointTime ) )
            {
                trend = TrendDirection.DownTrend;
            }

            var highestWaveImptPairs = FindHighestWaveImportancePairs( inBetweenZigZag );

            float lessDiff = float.MaxValue;
            ((SBar bar, WavePointImportance) waveA, (SBar bar, WavePointImportance) waveB ) lessDiffPair = default;


            if ( highestWaveImptPairs.Count > 0 )
            {
                foreach ( ((SBar bar, WavePointImportance) begin, (SBar bar, WavePointImportance) end) wavePt in highestWaveImptPairs )
                {
                    var waveAB_BC_diff = VerifyHarmonicRatioForWaveABC( trend, beginPointTime, wavePt.begin.bar.LinuxTime, wavePt.end.bar.LinuxTime, bartimeWaveC );

                    if ( waveAB_BC_diff < lessDiff )
                    {
                        lessDiff = waveAB_BC_diff;
                        lessDiffPair = wavePt;
                    }
                }

                if ( lessDiffPair != default )
                {
                    var WaveA = lessDiffPair.waveA;
                    var WaveB = lessDiffPair.waveB;

                    DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, WaveA.bar.LinuxTime, waveCycle, trend == TrendDirection.Uptrend ? WaveLabelPosition.TOP : WaveLabelPosition.BOTTOM, ElliottWaveEnum.WaveA );
                    DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, WaveB.bar.LinuxTime, waveCycle, trend == TrendDirection.Uptrend ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP, ElliottWaveEnum.WaveB );
                }
            }



            //PooledList< Tuple< WavePointImportance, WavePointImportance > > potentialAB = FindRetracementPoints( trend, inBetweenZigZag );

            //

            //Tuple< WavePointImportance, WavePointImportance > leastDeviatedWavePoint = null;
            //Tuple< WavePointImportance, WavePointImportance > leastDeviatedWavePoint2 = null;

            //bool didCalculate = false;

            //if( potentialAB.Count > 0 )
            //{
            //    foreach( Tuple< WavePointImportance, WavePointImportance > wavePoint in potentialAB )
            //    {
            //        var missedPips = VerifyHarmonicRatioForWaveABC( trend, beginPointTime, wavePoint.Item1.EpochTime, wavePoint.Item2.EpochTime, bartimeWaveC );

            //        if( missedPips < totalMissedPips )
            //        {
            //            totalMissedPips = missedPips;
            //            leastDeviatedWavePoint = wavePoint;

            //            didCalculate = true;
            //        }
            //    }
            //}

            //PooledList< Tuple< WavePointImportance, WavePointImportance > > potentialAB2 = FindRetracementPoints( trend, inBetweenGannSwing );

            //if( potentialAB2.Count > 0 )
            //{
            //    foreach( Tuple< WavePointImportance, WavePointImportance > wavePoint in potentialAB2 )
            //    {
            //        var missedPips = VerifyHarmonicRatioForWaveABC( trend, beginPointTime, wavePoint.Item1.EpochTime, wavePoint.Item2.EpochTime, bartimeWaveC );

            //        if( missedPips < totalMissedPips )
            //        {
            //            totalMissedPips2 = missedPips;
            //            leastDeviatedWavePoint2 = wavePoint;

            //            didCalculate = true;
            //        }
            //    }
            //}

            //if( didCalculate )
            //{
            //    if( totalMissedPips < totalMissedPips2 )
            //    {
            //        var WaveA = leastDeviatedWavePoint.Item1;
            //        var WaveB = leastDeviatedWavePoint.Item2;

            //        var barA = _bars.GetBarByTime( WaveA.EpochTime );
            //        var barB = _bars.GetBarByTime( WaveB.EpochTime );

            //        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, WaveA.EpochTime, waveCycle, trend == TrendDirection.Uptrend ? WaveLabelPosition.TOP : WaveLabelPosition.BOTTOM, ElliottWaveEnum.WaveA, barA );
            //        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, WaveB.EpochTime, waveCycle, trend == TrendDirection.Uptrend ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP, ElliottWaveEnum.WaveB, barB );
            //    }
            //    else
            //    {
            //        var WaveA = leastDeviatedWavePoint2.Item1;
            //        var WaveB = leastDeviatedWavePoint2.Item2;

            //        var barA = _bars.GetBarByTime( WaveA.EpochTime );
            //        var barB = _bars.GetBarByTime( WaveB.EpochTime );

            //        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, WaveA.EpochTime, waveCycle, trend == TrendDirection.Uptrend ? WaveLabelPosition.TOP : WaveLabelPosition.BOTTOM, ElliottWaveEnum.WaveA, barA );
            //        DumpAddWaveXtoManagerAndBar( waveScenarioNo, period, WaveB.EpochTime, waveCycle, trend == TrendDirection.Uptrend ? WaveLabelPosition.BOTTOM : WaveLabelPosition.TOP, ElliottWaveEnum.WaveB, barB );
            //    }
            //}

            //if ( nenZigZagCount >= 2 )
            //{

            //}

            //if ( gannSwingCount > 5 )
            //{

            //}

            //long bartimeWaveC = MainChart.SelectedCandleBarTime;

            //int barIndexWaveC = _bars.GetIndexByTime( bartimeWaveC );

            //if ( barIndexWaveC > -1 )
            //{
            //    var barWaveC = _bars.GetBarByIndex( barIndexWaveC );

            //    var labelWaveC = barWaveC.GetElliottWave().LabelPosition;

            //    var cycleWaveC = barWaveC.GetElliottWave().GetFirstHighestWaveDegree( ).Value.WaveCycle;

            //    var previousWave = GetPreviousWaveStructureOfDegree( bartimeWaveC, cycleWaveC );

            //    if ( previousWave != null )
            //    {
            //        if ( NoHigherWaveBetween( previousWave.StartDate, bartimeWaveC, cycleWaveC ) )
            //        {
            //            

            //            if ( zzCount > 0 )
            //            {
            //                inBetweenZigZag.Sort( );

            //                SBar previousBar = _bars.GetBarByTime( previousWave.StartDate );

            //                
            //            }
            //        }
            //    }

            //}
        }

        public PooledList<((SBar, WavePointImportance), (SBar, WavePointImportance))> FindHighestWaveImportancePairs( PooledList<WavePointImportance> wavePt )
        {
            PooledList< ((SBar, WavePointImportance), (SBar, WavePointImportance)) > output = new PooledList<((SBar, WavePointImportance), (SBar, WavePointImportance))>();

            int highestWaveImpt = -1;

            for ( int i = 0; i < wavePt.Count; i++ )
            {
                if ( wavePt[ i ].WaveImportance > highestWaveImpt )
                {
                    highestWaveImpt = wavePt[ i ].WaveImportance;
                }
            }

            SBar beginBar = default;
            WavePointImportance beginWaveImpt = null;

            SBar endBar = default;
            WavePointImportance endWaveImpt = null;            

            for ( int i = 0; i < wavePt.Count; i++ )
            {
                if ( wavePt[ i ].WaveImportance == highestWaveImpt )
                {
                    ref SBar currentBar = ref _bars.GetBarByTime( wavePt[ i ].LinuxTime );

                    if ( currentBar != SBar.EmptySBar )
                    {
                        if ( beginBar == default )
                        {
                            beginBar = currentBar;
                            beginWaveImpt = wavePt[ i ];

                            continue;
                        }

                        if ( beginBar != default && endBar == default )
                        {
                            endBar = currentBar;
                            endWaveImpt = wavePt[ i ];
                        }

                        if ( beginBar != default && endBar != default )
                        {
                            output.Add( ((beginBar, beginWaveImpt), (endBar, endWaveImpt)) );

                            beginBar = default;
                            endBar = default;
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<Tuple<WavePointImportance, WavePointImportance>> FindRetracementPoints( TrendDirection trend, PooledList<WavePointImportance> wavePointList )
        {
            var output = new PooledList< Tuple< WavePointImportance, WavePointImportance > >( );

            if ( trend == TrendDirection.Uptrend )
            {
                output = UpTrendRetracmentPoints( trend, wavePointList );
            }
            else if ( trend == TrendDirection.DownTrend )
            {
                output = DownTrendFindRetracementPoints( trend, wavePointList );
            }

            return output;
        }

        public PooledList<Tuple<WavePointImportance, WavePointImportance>> UpTrendRetracmentPoints2( TrendDirection trend,
                                                                                                   PooledList<WavePointImportance> wavePointList )
        {
            var output = new PooledList< Tuple< WavePointImportance, WavePointImportance > >( );

            return output;
        }

        public PooledList<Tuple<WavePointImportance, WavePointImportance>> UpTrendRetracmentPoints( TrendDirection trend,
                                                                                                   PooledList<WavePointImportance> wavePointList )
        {
            int highestWaveIndex = -1;

            var output = new PooledList< Tuple< WavePointImportance, WavePointImportance > >( );
            
            var currentSignal = TASignal.NONE;
            long currentWaveTime = -1;

            double highest = double.MaxValue;

            for ( int i = 0; i < wavePointList.Count; i++ )
            {
                currentSignal = wavePointList[ i ].Signal;
                currentWaveTime = wavePointList[ i ].LinuxTime;

                ref SBar currentBar = ref _bars.GetBarByTime( currentWaveTime );

                if ( currentBar != SBar.EmptySBar )
                {
                    if ( i == 0 && ( currentSignal == TASignal.WAVE_PEAK || currentSignal == TASignal.GANN_PEAK ) )
                    {
                        highest = currentBar.High;
                        highestWaveIndex = i;
                    }
                    else
                    {
                        if ( currentSignal == TASignal.WAVE_PEAK || currentSignal == TASignal.GANN_PEAK )
                        {
                            if ( currentBar.High > highest )
                            {
                                int lowestIndex = FindLowestPointsBetween( highestWaveIndex, i, wavePointList );

                                if ( lowestIndex > -1 )
                                {
                                    currentWaveTime = wavePointList[ lowestIndex ].LinuxTime;

                                    ref SBar lowestBar = ref _bars.GetBarByTime( currentWaveTime );

                                    if ( lowestBar != SBar.EmptySBar )                                    
                                    {
                                        output.Add( new Tuple<WavePointImportance, WavePointImportance>( wavePointList[ highestWaveIndex ], wavePointList[ lowestIndex ] ) );

                                        highest = lowestBar.High;
                                        highestWaveIndex = lowestIndex + 1;

                                        i = lowestIndex;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return output;
        }

        public PooledList<Tuple<WavePointImportance, WavePointImportance>> DownTrendFindRetracementPoints( TrendDirection trend,
                                                                                                          PooledList<WavePointImportance> wavePointList )
        {
            int lowestWaveIndex = -1;

            var output = new PooledList< Tuple< WavePointImportance, WavePointImportance > >( );
            
            var currentSignal = TASignal.NONE;
            long currentWaveTime = -1;

            double lowest = double.MaxValue;

            for ( int i = 0; i < wavePointList.Count; i++ )
            {
                currentSignal       = wavePointList[ i ].Signal;
                currentWaveTime     = wavePointList[ i ].LinuxTime;
                ref SBar currentBar = ref _bars.GetBarByTime( currentWaveTime );

                if ( currentBar != SBar.EmptySBar )                    
                {
                    if ( i == 0 && ( currentSignal == TASignal.WAVE_TROUGH || currentSignal == TASignal.GANN_TROUGH ) )
                    {
                        lowest          = currentBar.Low;
                        lowestWaveIndex = i;
                    }
                    else
                    {
                        if ( currentSignal == TASignal.WAVE_TROUGH || currentSignal == TASignal.GANN_TROUGH )
                        {
                            if ( currentBar.Low < lowest )
                            {
                                int highestIndex = FindHighestPointsBetween( lowestWaveIndex, i, wavePointList );

                                if ( highestIndex > -1 )
                                {
                                    currentWaveTime = wavePointList[ highestIndex ].LinuxTime;

                                    ref SBar highestBar = ref _bars.GetBarByTime( currentWaveTime );

                                    if ( highestBar != SBar.EmptySBar )
                                    {
                                        output.Add( new Tuple<WavePointImportance, WavePointImportance>( wavePointList[ lowestWaveIndex ], wavePointList[ highestIndex ] ) );

                                        lowest          = highestBar.Low;
                                        lowestWaveIndex = highestIndex + 1;

                                        i               = highestIndex;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return output;
        }

        private int FindHighestPointsBetween( int lowerbound,
                                              int upperbound,
                                              PooledList<WavePointImportance> wavePointList )
        {
            if ( lowerbound < 0 || lowerbound >= wavePointList.Count )
            {
                return -1;
            }

            if ( upperbound < 0 || upperbound >= wavePointList.Count )
            {
                return -1;
            }

            var currentSignal = wavePointList[ lowerbound ].Signal;
            var currentWaveTime = wavePointList[ lowerbound ].LinuxTime;
            
            int highestIndex = -1;

            ref SBar firstBar = ref _bars.GetBarByTime( currentWaveTime );

            if ( firstBar != SBar.EmptySBar )
            {
                double highest = firstBar.High;

                for ( int i = lowerbound + 1; i <= upperbound; i++ )
                {
                    currentSignal = wavePointList[ i ].Signal;
                    currentWaveTime = wavePointList[ i ].LinuxTime;

                    ref SBar nextBar = ref _bars.GetBarByTime( currentWaveTime );

                    if ( nextBar != SBar.EmptySBar )
                    {
                        if ( currentSignal == TASignal.WAVE_PEAK || currentSignal == TASignal.GANN_PEAK )
                        {
                            if ( nextBar.High > highest )
                            {
                                highest = nextBar.High;
                                highestIndex = i;
                            }
                        }
                    }
                }
            }

            return highestIndex;
        }

        private int FindLowestPointsBetween( int lowerbound,
                                              int upperbound,
                                              PooledList<WavePointImportance> wavePointList )
        {
            if ( lowerbound < 0 || lowerbound >= wavePointList.Count )
            {
                return -1;
            }

            if ( upperbound < 0 || upperbound >= wavePointList.Count )
            {
                return -1;
            }

            var currentSignal = wavePointList[ lowerbound ].Signal;
            var currentWaveTime = wavePointList[ lowerbound ].LinuxTime;
            
            int lowestIndex = -1;

            ref SBar currentBar = ref _bars.GetBarByTime( currentWaveTime );

            if ( currentBar != SBar.EmptySBar )
            {
                double lowest = currentBar.Low;
            
                for ( int i = lowerbound + 1; i <= upperbound; i++ )
                {
                    currentSignal = wavePointList[ i ].Signal;
                    currentWaveTime = wavePointList[ i ].LinuxTime;

                    ref SBar mybar = ref _bars.GetBarByTime( currentWaveTime );

                    if ( mybar != SBar.EmptySBar )
                    {
                        if ( currentSignal == TASignal.WAVE_TROUGH || currentSignal == TASignal.GANN_TROUGH )
                        {
                            if ( mybar.Low < lowest )
                            {
                                lowest = mybar.Low;
                                lowestIndex = i;
                            }
                        }
                    }
                }
            }

            return lowestIndex;
        }

        //private void LowerTFRepo_HistoricBarUpdateEvent( fxHistoricBarsRepo provider,
        //                                                 DataBarUpdateType updateType,
        //                                                 uint updatedBarsCount )
        //{
        //    if ( updateType == DataBarUpdateType.Initial )
        //    {
        //        provider.HistoricBarUpdateEvent -= LowerTFRepo_HistoricBarUpdateEvent;

        //        if ( _verifyBeginTime > -1 && _verifyEndTime > -1 && _verifyWave != ElliottWaveEnum.NONE )
        //        {
        //            var currentTimeFrame = provider.Period;

        //            StartPerformingWaveFindingTask( currentTimeFrame.Value, _verifyBeginTime, _verifyEndTime, _verifyWave );
        //        }
        //    }
        //}

        private void Indicators_IndicatorFullCalculationDoneEvent( IIndicatorManager session,
                                                                   IMyIndicator indicator )
        {
            if ( _verifyBeginTime > -1 && _verifyEndTime > -1 && _verifyWave != ElliottWaveEnum.NONE )
            {
                StartPerformingWaveFindingTask( session.Period, _verifyBeginTime, _verifyEndTime, _verifyWave );
            }
        }

        public void AdjustBeingAndEndTime( TimeSpan period )
        {
            var bars = GetDatabarsRepository( period );            

            ref SBar bar = ref _bars.GetBarByTime( _verifyBeginTime );

            if ( bar == SBar.EmptySBar ) return;                

            TimeSpan oneTimeSpanHigher = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                oneTimeSpanHigher = aa.GetOneTimeSpanHigher( _verifyPeriod );
            }
            else
            {
                return;
            }

            if ( _verifyTrend == TrendDirection.Uptrend )
            {
                ref SBar beginExtremeBar = ref bars.GetLowestBarOfTheRange( bar.BarTime, oneTimeSpanHigher );

                if ( beginExtremeBar != SBar.EmptySBar )
                {
                    _verifyBeginTime = beginExtremeBar.LinuxTime;
                }

                ref SBar bar2 = ref _bars.GetBarByTime( _verifyEndTime );

                if ( bar2 != SBar.EmptySBar )
                {
                    ref SBar endExtremumBar = ref bars.GetHighestBarOfTheRange( bar2.BarTime, oneTimeSpanHigher );

                    if ( endExtremumBar != SBar.EmptySBar )
                    {
                        _verifyEndTime = endExtremumBar.LinuxTime;
                    }
                }
            }
            else if ( _verifyTrend == TrendDirection.DownTrend )
            {
                ref SBar beginExtremeBar = ref bars.GetHighestBarOfTheRange( bar.BarTime, oneTimeSpanHigher );

                if ( beginExtremeBar != SBar.EmptySBar )
                {
                    _verifyBeginTime = beginExtremeBar.LinuxTime;
                }

                ref SBar bar2 = ref _bars.GetBarByTime( _verifyEndTime );

                if ( bar2 != SBar.EmptySBar )
                {
                    ref SBar endExtremumBar = ref bars.GetLowestBarOfTheRange( bar2.BarTime, oneTimeSpanHigher );

                    if ( endExtremumBar != SBar.EmptySBar )
                    {
                        _verifyEndTime = endExtremumBar.LinuxTime;
                    }
                }
            }
        }

        public void StartPerformingWaveFindingTask( TimeSpan period,
                                                    long beginTime,
                                                    long endTime,
                                                    ElliottWaveEnum waveName )
        {
            Task detectTask = new Task(
                                            ( ) =>
                                            {
                                                PerformingWaveFindingTask( period );
                                            }, GeneralHelper.GlobalExitToken( ) );

            detectTask.Start();
        }

        private void PerformingWaveFindingTask( TimeSpan period )
        {
            //ThreadHelper.UpdateThreadName( "PerformingWaveFindingTask" );

            AdjustBeingAndEndTime( period );

            var inBetweenZigZag = FindImportantWavePointsBetween( period, _verifyBeginTime, _verifyEndTime );

            var inBetweenGannSw = FindGannSwingBetween( period, _verifyBeginTime, _verifyEndTime );

            TrendDirection trend = TrendDirection.NoTrend;

            if ( _bars.IsUpTrend( _verifyBeginTime, _verifyEndTime ) )
            {
                trend = TrendDirection.Uptrend;
            }
            else if ( _bars.IsDownTrend( _verifyBeginTime, _verifyEndTime ) )
            {
                trend = TrendDirection.DownTrend;
            }

            AnalyseMajorTurningPoints( period, _verifyBeginTime, _verifyEndTime, trend, 89, inBetweenZigZag );

            //var largest = FindLargestRetracement( period, beginTime, endTime, trend, inBetweenZigZag, inBetweenGannSw );
        }

        private Tuple<WavePointImportance, WavePointImportance> FindLargestRetracement( TimeSpan period,
                                                                                          long beginPointTime,
                                                                                          long endPointTime,
                                                                                          TrendDirection trend,
                                                                                          PooledList<WavePointImportance> inBetweenZigZag,
                                                                                          PooledList<WavePointImportance> inBetweenGannSw )
        {
            var potentialRetracement = FindRetracementPoints( trend, inBetweenZigZag );
            var potentialRetracement2 = FindRetracementPoints( trend, inBetweenGannSw );

            long beginTrendTime = -1;
            long endTrendTime = -1;
            long endCounterTrendTime = -1;

            var pointSize = _bars.Security.PriceStep.Value;

            var output = new PooledList< WavePriceTimeInfo >( );

            if ( potentialRetracement.Count > 0 )
            {
                foreach ( Tuple<WavePointImportance, WavePointImportance> wavePoint in potentialRetracement )
                {
                    if ( beginTrendTime == -1 )
                    {
                        beginTrendTime = beginPointTime;
                    }
                    else
                    {
                        beginTrendTime = endCounterTrendTime;
                    }

                    endTrendTime = wavePoint.Item1.LinuxTime;

                    endCounterTrendTime = wavePoint.Item2.LinuxTime;

                    WavePriceTimeInfo wavePT = default;

                    if ( FindRetracementInfo( trend, period, beginTrendTime, endTrendTime, endCounterTrendTime, ( double ) pointSize, ref wavePT ) )
                    {
                        output.Add( wavePT );
                    }
                }

                AddRetracementValuesToBars( period, beginPointTime, endPointTime, 0, output );
            }

            return null;
        }

        private bool FindRetracementInfo( TrendDirection trend,
                                                            TimeSpan period,
                                                           long beginTrendTime,
                                                           long endTrendTime,
                                                           long endCounterTrendTime,
                                                           double pointsize, ref WavePriceTimeInfo output )
        {
            if ( pointsize == 0 )
            {
                pointsize = 1;
            }

            ref SBar beginTrendBar = ref _bars.GetBarByTime( beginTrendTime );

            if ( beginTrendBar == SBar.EmptySBar ) return false;

            ref SBar endTrendBar = ref _bars.GetBarByTime( endTrendTime );

            if ( endTrendBar == SBar.EmptySBar ) return false;

            ref SBar counterTrendBar = ref _bars.GetBarByTime( endCounterTrendTime );

            if ( counterTrendBar == SBar.EmptySBar ) return false;
            
            double trendMovedPips = 0;
            double counterTrendPips = 0;
            RangeEx< double > trendMovedRange = null;
            RangeEx< double > counterTrendMovedRange = null;

            if ( trend == TrendDirection.Uptrend )
            {
                trendMovedRange        = new RangeEx<double>( beginTrendBar.Low, endTrendBar.High );
                trendMovedPips         = ( endTrendBar.High - beginTrendBar.Low ) / pointsize;

                counterTrendMovedRange = new RangeEx<double>( counterTrendBar.Low, endTrendBar.High );
                counterTrendPips       = ( endTrendBar.High - counterTrendBar.Low ) / pointsize;
            }
            else if ( trend == TrendDirection.DownTrend )
            {
                trendMovedRange        = new RangeEx<double>( endTrendBar.Low, beginTrendBar.High );
                trendMovedPips         = ( beginTrendBar.High - endTrendBar.Low ) / pointsize;

                counterTrendMovedRange = new RangeEx<double>( endTrendBar.Low, counterTrendBar.High );
                counterTrendPips       = ( counterTrendBar.High - endTrendBar.Low ) / pointsize;
            }
            else
            {
                return false;
            }

            output = new WavePriceTimeInfo( trend, beginTrendBar.Index, endTrendBar.Index, counterTrendBar.Index, trendMovedRange, counterTrendMovedRange, default, default );

            return true;
        }

        private void AddRetracementValuesToBars(    TimeSpan period,
                                                    long beginPointTime,
                                                    long endPointTime,
                                                    int waveImportance,
                                                    PooledList<WavePriceTimeInfo> retracementInfos )
        {
            var undo = GetSelectedUndoRedoArea( period );
            var bars = GetDatabarsRepository( period );

            var trendMovedPips = retracementInfos.OrderByDescending( x => x.TrendMovedPips );
            var counterMovedPips = retracementInfos.OrderByDescending( x => x.CounterTrendMovedPips );

            using ( undo.Start( "AddRetracementValuesToBars" ) )
            {
                int ranking = 0;

                foreach ( WavePriceTimeInfo retracementInfo in trendMovedPips )
                {
                    ranking++;

                    ref SBar trendBeginBar = ref bars.GetBarByTime( retracementInfo.TrendBeginIndex );

                    if ( trendBeginBar == SBar.EmptySBar ) return;

                    ref SBar trendEndBar = ref bars.GetBarByTime( retracementInfo.TrendEndIndex );

                    if ( trendEndBar == SBar.EmptySBar ) return;

                    ref SBar counterBar = ref bars.GetBarByTime( retracementInfo.CounterTrendEndIndex );

                    if ( counterBar == SBar.EmptySBar ) return;

                    trendEndBar.MainPriceTimeInfo = retracementInfo;
                }

                ranking = 0;

                foreach ( WavePriceTimeInfo retracementInfo in counterMovedPips )
                {
                    ranking++;

                    ref SBar trendBeginBar = ref bars.GetBarByTime( retracementInfo.TrendBeginIndex );

                    if ( trendBeginBar == SBar.EmptySBar ) return;

                    ref SBar trendEndBar = ref bars.GetBarByTime( retracementInfo.TrendEndIndex );

                    if ( trendEndBar == SBar.EmptySBar ) return;

                    ref SBar counterBar = ref bars.GetBarByTime( retracementInfo.CounterTrendEndIndex );

                    if ( counterBar == SBar.EmptySBar ) return;

                    counterBar.MainPriceTimeInfo = retracementInfo;                    
                }

                undo.Commit();
            }
        }

        private WavePriceTimeInfo GetLargestTrendyMove( PooledList<WavePriceTimeInfo> retracementInfos )
        {
            double trendMovedPips = 0;
            WavePriceTimeInfo output = default;

            foreach ( WavePriceTimeInfo retracementInfo in retracementInfos )
            {
                if ( retracementInfo.TrendMovedPips > trendMovedPips )
                {
                    trendMovedPips = retracementInfo.TrendMovedPips;

                    output = retracementInfo;
                }
            }

            return output;
        }

        private WavePriceTimeInfo GetLargestRetracement( PooledList<WavePriceTimeInfo> retracementInfos )
        {
            double counterTrendPips = 0;
            WavePriceTimeInfo output = default;

            foreach ( WavePriceTimeInfo retracementInfo in retracementInfos )
            {
                if ( retracementInfo.CounterTrendMovedPips > counterTrendPips )
                {
                    counterTrendPips = retracementInfo.CounterTrendMovedPips;

                    output = retracementInfo;
                }
            }

            return output;
        }

        private void AnalyseMajorTurningPoints( TimeSpan period,
                                                long beginPointTime,
                                                long endPointTime,
                                                TrendDirection trend,
                                                int waveImportance,
                                                PooledList<WavePointImportance> inBetweenGannSw )
        {
            var majorPts = inBetweenGannSw.Where( x => ( x.WaveImportance == waveImportance && x.LinuxTime >= beginPointTime && x.LinuxTime <= endPointTime ) || ( x.LinuxTime == endPointTime ) ).ToPooledList();

            var potentialRetracement = FindRetracementPoints( trend, majorPts );

            double pointSize = ( double )_bars.Security.PriceStep.Value;

            var output = new PooledList< WavePriceTimeInfo >( );

            long beginTrendTime = -1;
            long endTrendTime = -1;
            long endCounterTrendTime = -1;

            if ( potentialRetracement.Count > 0 )
            {
                foreach ( Tuple<WavePointImportance, WavePointImportance> wavePoint in potentialRetracement )
                {
                    if ( beginTrendTime == -1 )
                    {
                        beginTrendTime = beginPointTime;
                    }
                    else
                    {
                        beginTrendTime = endCounterTrendTime;
                    }

                    endTrendTime = wavePoint.Item1.LinuxTime;

                    endCounterTrendTime = wavePoint.Item2.LinuxTime;

                    WavePriceTimeInfo wavePT = default;

                    if ( FindRetracementInfo( trend, period, beginTrendTime, endTrendTime, endCounterTrendTime, pointSize, ref wavePT ) )
                    {
                        output.Add( wavePT );
                    }
                }

                AddRetracementValuesToBars( period, beginPointTime, endPointTime, waveImportance, output );

                beginTrendTime = -1;

                var lower = GetOneWaveImportanceLower( waveImportance );

                if ( lower > 0 )
                {
                    foreach ( Tuple<WavePointImportance, WavePointImportance> wavePoint in potentialRetracement )
                    {
                        if ( beginTrendTime == -1 )
                        {
                            beginTrendTime = beginPointTime;
                        }
                        else
                        {
                            beginTrendTime = endCounterTrendTime;
                        }

                        endTrendTime = wavePoint.Item1.LinuxTime;
                        endCounterTrendTime = wavePoint.Item2.LinuxTime;

                        AnalyseMajorTurningPoints( period, beginTrendTime, endTrendTime, trend, lower, inBetweenGannSw );
                    }

                    AnalyseMajorTurningPoints( period, endCounterTrendTime, endPointTime, trend, lower, inBetweenGannSw );
                }
            }
            else if ( majorPts.Count > 0 )
            {
                var lower = GetOneWaveImportanceLower( waveImportance );

                if ( lower > 0 )
                {
                    AnalyseMajorTurningPoints( period, beginPointTime, endPointTime, trend, lower, inBetweenGannSw );
                }
            }
        }
    }
}

