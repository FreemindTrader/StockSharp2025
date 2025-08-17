using DevExpress.Mvvm;
using Ecng.Logging;
using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Database;
using fx.Definitions;
using fx.TimePeriod;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    //![](9B4595CED784909AC1195C12F1754846.png;;;0.03159,0.02605)
    public partial class WavePredictionModel : BaseLogReceiver, IWavePredictionModel
    {
        private PooledList<FibLevelInfo> _largerTargets = null;
        private PooledList<FibLevelInfo> _unfoldingTargets = null;
        private fxHistoricBarsRepo _bars;
        private HewManager _hews;
        private WaveModelKey _k;

        public WavePredictionModel( WaveModelKey key, fxHistoricBarsRepo bars, HewManager hewManager )
        {
            _k = key;
            _bars = bars;
            _hews = hewManager;
        }

        public void BuildWaveModel()
        {
            var hews = _hews.GetElliottWavesDictionary( _k.Period );

            DbElliottWave selectedWave = null;

            if ( hews.ContainsKey( _k.RawBeginTime ) )
            {
                selectedWave = hews[_k.RawBeginTime];
            }

            ref var hew = ref selectedWave.GetWaveFromScenario( _k.WaveScenarioNo );

            var highestWave = hew.GetFirstHighestWaveInfo();

            if ( highestWave.HasValue )
            {
                var waveName = highestWave.Value.WaveName;
                var waveDegree = highestWave.Value.WaveCycle;

                switch ( waveName )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave2:
                    {

                    }
                    break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                    {
                        AnalyseWave4RetracementTarget( _k, ref hew, waveName, waveDegree );

                    }
                    break;

                    case ElliottWaveEnum.Wave4:
                    {
                        //AnalyseWave5ProjectionTarget( waveScenarioNo, period, bars, selectedBarTime, waveDegree );
                    }
                    break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                    {
                        //AnalyseWave1OrWaveATarget( waveScenarioNo, period, bars, selectedBarTime, waveDegree, ref eWave );
                    }
                    break;

                    case ElliottWaveEnum.WaveTA:

                    break;
                    case ElliottWaveEnum.WaveTB:

                    break;
                    case ElliottWaveEnum.WaveTC:

                    break;
                    case ElliottWaveEnum.WaveTD:

                    break;
                    case ElliottWaveEnum.WaveTE:

                    break;
                    case ElliottWaveEnum.WaveEFA:

                    break;
                    case ElliottWaveEnum.WaveEFB:

                    break;
                    case ElliottWaveEnum.WaveEFC:

                    break;
                    case ElliottWaveEnum.WaveA:
                    {
                        AnalyseWaveBRetracementTarget( _k, ref hew, waveName, waveDegree );
                    }
                    break;

                    case ElliottWaveEnum.WaveB:

                    break;
                    case ElliottWaveEnum.WaveC:

                    break;
                    case ElliottWaveEnum.WaveX:

                    break;
                    case ElliottWaveEnum.WaveY:

                    break;
                    case ElliottWaveEnum.WaveZ:

                    break;
                    case ElliottWaveEnum.WaveW:

                    break;
                }
            }

        }

        private void AnalyseWave4RetracementTarget( WaveModelKey k, ref HewLong hew, ElliottWaveEnum highestWaveName, ElliottWaveCycle highestWaveDegree )
        {
            var wave3PreditWave4 = new C5Waves( _bars, _hews, k );

            wave3PreditWave4.StartAnalysis( ref hew, highestWaveName, highestWaveDegree );
            //AnalyseWithPivotPoints( wave3PreditWave4 );



        }

        private void AnalyseWaveBRetracementTarget( WaveModelKey k, ref HewLong hew, ElliottWaveEnum highestWaveName, ElliottWaveCycle highestWaveDegree )
        {
            // ![](C8E272AC030B3D488D26185106CBF001.png)
            long preceding5WavesStartTime = -1;

            var precedingWave = _hews.GetPreviousWaveStructureOfDegree( k.WaveScenarioNo, k.RawBeginTime, k.WaveCycle );

            if ( precedingWave != null )
            {
                ref var prevHew = ref precedingWave.GetWaveFromScenario( _k.WaveScenarioNo );

                var highestWave = prevHew.GetFirstHighestWaveInfo();

                if ( highestWave.HasValue )
                {
                    var waveName = highestWave.Value.WaveName;
                    var waveDegree = highestWave.Value.WaveCycle;

                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave2:
                        {
                            // So we just finished Wave 3A, currently in Wave 3B

                            preceding5WavesStartTime = precedingWave.StartDate;
                        }
                        break;

                        case ElliottWaveEnum.Wave4:
                        {
                            // So we just finished Wave 5A, currently in Wave 5B
                            preceding5WavesStartTime = precedingWave.StartDate;
                        }
                        break;


                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }


                var prevK       = new WaveModelKey( k.Security, k.Period, k.WaveScenarioNo, preceding5WavesStartTime, k.RawBeginTime, k.WaveCycle );
                var prev5Wave   = new C5Waves( _bars, _hews, prevK );

                prev5Wave.ProcessFinishedWaves( );

                var bar         = _bars.GetBarByTime( k.RawBeginTime );

                var retracement = _hews.GetHewFibTargets( k.WaveScenarioNo, bar.SymbolEx, k.RawBeginTime, ElliottWaveEnum.WaveA, k.WaveCycle );

                var waveABC     = new WaveABC( _bars, _hews, k );

                waveABC.StartAnalysis( ref hew, highestWaveName, highestWaveDegree, prev5Wave, retracement );
                AnalyseWithPivotPoints( waveABC );



            }
        }

        private void AnalyseWithPivotPoints( IFactal factal )
        {
            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _bars.Security );
            if ( aa == null )
                return;

            var dailyPP = ( IPivotPointIndicator )aa.DailyPivotPoint;
            var weeklyPP = ( IPivotPointIndicator )aa.WeeklyPivotPoint;
            var monthlyPP = ( IPivotPointIndicator )aa.MonthlyPivotPoint;

            var wave4Targets = factal.PredictedTargets;

            if ( wave4Targets == null )
            {
                return;
            }

            PivotPointsInfo monthlyPivotPoint = null;

            if ( monthlyPP == null )
            {
                this.LogError( "Monthly Pivot Point is missing, Calculating Target will be miinaccuratessing" );
            }
            else
            {
                monthlyPivotPoint = monthlyPP.GetPivotPointsAt( _k.RawBeginTime.FromLinuxTime(), out TimeBlockEx temp3 );

                if ( monthlyPivotPoint == null )
                {
                    this.LogError( "Monthly Pivot Point is missing, Calculating Target will be miinaccuratessing" );
                }
                else
                {
                    foreach ( var pp in monthlyPivotPoint.AllPivotPoints )
                    {
                        var index = wave4Targets.FindIndex( x => Math.Abs( x.FibLevel - pp.Value ) < _bars.PipsAllowance() );

                        if ( index > -1 )
                        {
                            var wave4CalcTarget = wave4Targets[index];

                            wave4CalcTarget.DynamicSRLinesRating += 30;
                        }
                    }
                }
            }

            PivotPointsInfo weeklyPivotPoint = null;

            if ( weeklyPP == null )
            {
                this.LogError( "Weekly Pivot Point is missing, Calculating Target will be inaccurate" );
            }
            else
            {
                weeklyPivotPoint = weeklyPP.GetPivotPointsAt( _k.RawBeginTime.FromLinuxTime(), out TimeBlockEx temp2 );

                if ( weeklyPivotPoint == null )
                {
                    this.LogError( "Weekly Pivot Point is missing, Calculating Target will be inaccurate" );
                }
                else
                {
                    foreach ( var pp in weeklyPivotPoint.AllPivotPoints )
                    {
                        var index = wave4Targets.FindIndex( x => Math.Abs( x.FibLevel - pp.Value ) < _bars.PipsAllowance() );

                        if ( index > -1 )
                        {
                            var wave4CalcTarget = wave4Targets[index];

                            wave4CalcTarget.DynamicSRLinesRating += 7;
                        }
                    }
                }
            }


            var today = DateTime.UtcNow;

            if ( dailyPP == null )
            {
                this.LogError( "Daily Pivot Point is missing, Calculating Target will be inaccurate" );
            }
            else
            {
                var dailyPivotPoint = dailyPP.GetPivotPointsAt( today, out TimeBlockEx temp1 );

                if ( dailyPivotPoint == null )
                {
                    this.LogError( "Daily Pivot Point is missing, Calculating Target will be inaccurate" );
                }
                else
                {
                    foreach ( var pp in dailyPivotPoint.AllPivotPoints )
                    {
                        var index = wave4Targets.FindIndex( x => Math.Abs( x.FibLevel - pp.Value ) < _bars.PipsAllowance() );

                        if ( index > -1 )
                        {
                            var wave4CalcTarget = wave4Targets[index];

                            wave4CalcTarget.DynamicSRLinesRating += 1;
                        }
                    }
                }
            }

            var waveTargets = aa.WaveTargetBindingList;

            waveTargets.Clear();


            foreach ( var target in wave4Targets )
            {
                waveTargets.Add( target );
            }

            Messenger.Default.Send( new WavePredictionMessage( _bars.Security, _k.RawBeginTime ) );
        }


        public ElliottWaveCycle MyWaveCycle { get; set; }



        public PooledList<FibLevelInfo> LargerTargets
        {
            get { return _largerTargets; }
            set
            {
                _largerTargets = value;
            }
        }

        public PooledList<FibLevelInfo> UnfoldingTargets
        {
            get { return _unfoldingTargets; }
            set
            {
                _unfoldingTargets = value;
            }
        }

        public PooledList<FibLevelInfo> PredictedTargets
        {
            get { return _unfoldingTargets; }
            set
            {
                _unfoldingTargets = value;
            }
        }

        public TrendDirection Trend
        {
            get
            {
                if ( Bar0 != SBar.EmptySBar )
                {
                    if ( Bar0.IsTrough() )
                    {
                        return TrendDirection.Uptrend;
                    }
                    else if ( Bar0.IsPeak() )
                    {
                        return TrendDirection.DownTrend;
                    }

                }

                return TrendDirection.NoTrend;
            }
        }

        public void GetFibExtremum( out FibLevelInfo output )
        {
            output = default;

            if ( Trend == TrendDirection.Uptrend )
            {
                double max = double.MinValue;
                foreach ( var level in _largerTargets )
                {
                    if ( level.FibLevel > max )
                    {
                        max = level.FibLevel;
                        output = level;
                    }
                }
            }
            else if ( Trend == TrendDirection.DownTrend )
            {
                double min = double.MaxValue;
                foreach ( var level in _largerTargets )
                {
                    if ( level.FibLevel < min )
                    {
                        min = level.FibLevel;
                        output = level;
                    }
                }
            }
        }

        public Wave3Type Wave3Type { get; set; }

        public FibPercentage Wave2Ret { get; set; }
        public FibPercentage Wave3aProj { get; set; }
        public FibPercentage Wave3cProj { get; set; }

        public FibPercentage Wave2toWave1 { get; set; }

        public FibPercentage Wave3bRet { get; set; }
        public FibPercentage Wave3btoWave1 { get; set; }

        public FibPercentage Wave4Ret { get; set; }
        public FibPercentage Wave4toWave1 { get; set; }

        public FibPercentage Wave5Proj { get; set; }
        public FibPercentage Wave5toWave1 { get; set; }

        public SBar Bar0;
        public SBar Bar1;
        public SBar Bar2;
        public SBar Bar3A;
        public SBar Bar3B;
        public SBar Bar3C;
        public SBar Bar4;

        public SBar Bar5A;
        public SBar Bar5B;
        public SBar Bar5C;




        //private void PredictTargetsBasedOnWave3Model( int waveScenarioNo, TimeSpan period, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree, Wave3Type wave3Type )
        //{
        //    if ( wave3Type == Wave3Type.Classic )
        //    {

        //    }
        //    else if ( wave3Type == Wave3Type.Extended )
        //    {
        //        switch ( waveName )
        //        {
        //            case ElliottWaveEnum.Wave1:
        //            case ElliottWaveEnum.Wave1C:
        //            {
        //                // Should not be here, as if we only have wave 1 and nothing else, we can't do any prediction
        //                throw new NotImplementedException( );
        //            }

        //            case ElliottWaveEnum.Wave2:
        //            {

        //            }
        //            break;

        //            case ElliottWaveEnum.Wave3:
        //            case ElliottWaveEnum.Wave3C:
        //            {
        //                PredictWave4ForExtendedWave3( period, selectedBarTime, waveDegree );
        //            }
        //            break;

        //            case ElliottWaveEnum.Wave4:
        //            {
        //                PredictWave5ForExtendedWave3( period, selectedBarTime, waveDegree );
        //            }
        //            break;

        //            case ElliottWaveEnum.WaveA:
        //            {

        //            }
        //            break;

        //            case ElliottWaveEnum.WaveB:
        //            {

        //            }
        //            break;

        //            case ElliottWaveEnum.WaveC:
        //            {

        //            }
        //            break;

        //        }
        //    }
        //    else if ( wave3Type == Wave3Type.SuperExtended )
        //    {
        //        switch ( waveName )
        //        {
        //            case ElliottWaveEnum.Wave1:
        //            case ElliottWaveEnum.Wave1C:
        //            {
        //                // Should not be here, as if we only have wave 1 and nothing else, we can't do any prediction
        //                throw new NotImplementedException( );
        //            }

        //            case ElliottWaveEnum.Wave2:
        //            {

        //            }
        //            break;

        //            case ElliottWaveEnum.Wave3:
        //            case ElliottWaveEnum.Wave3C:
        //            {
        //                PredictWave4ForSuperExtendedWave3( waveScenarioNo, period, selectedBarTime, waveDegree );
        //            }
        //            break;

        //            case ElliottWaveEnum.Wave4:
        //            {
        //                PredictWave5ForSuperExtendedWave3( period, selectedBarTime, waveDegree );
        //            }
        //            break;

        //            case ElliottWaveEnum.WaveA:
        //            {

        //            }
        //            break;

        //            case ElliottWaveEnum.WaveB:
        //            {

        //            }
        //            break;

        //            case ElliottWaveEnum.WaveC:
        //            {

        //            }
        //            break;

        //        }
        //    }
        //}

        //





        //public void AnalyseCurrentCorrection( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
        //{
        //    _correctiveWaveB = new CorrectivePredictionModel( _bars, _hews );

        //    _correctiveWaveB.AnalysePastImpulsiveMove( waveScenarioNo, period, bars, selectedBarTime, waveName, waveDegree - GlobalConstants.OneWaveCycle );

        //    if ( _correctiveWaveB.LargerTargets != null && _correctiveWaveB.LargerTargets.Count > 0 )
        //    {
        //        var diff           = ( double ) _bars.Security.PriceStep.Value;

        //        _unfoldingTargets  = _correctiveWaveB.LargerTargets;

        //        foreach ( FibLevelInfo largerTarget in _largerTargets )
        //        {
        //            var index = _unfoldingTargets.FindIndex( x => x.WithinCluster( largerTarget, _bars.PipsAllowance()  ) );

        //            if ( index > -1 )
        //            {
        //                var selectedLevel = _unfoldingTargets[ index ];

        //                selectedLevel.UpdateAll( largerTarget );                        
        //            }
        //            else
        //            {
        //                _unfoldingTargets.Add( largerTarget );
        //            }
        //        }

        //        _predictedTargets = _correctiveWaveB.LargerTargets.OrderByDescending( x => x.OverlappedCount ).ToPooledList( );
        //    }
        //}

        //public void AnalyseWave5Movement( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle waveDegree )
        //{
        //    // Inside Wave 5, we should have 2 impulsive move, One is Wave A
        //    //![](9457DE1D7689BDA6FD7192C2F4762876.png;;;0.03086,0.03270)
        //    _impulsiveWaveC = new WavePredictionModel( _bars, _hews );

        //    _largerTargets = _impulsiveWaveC.Analyse5A5B5C( waveScenarioNo, period, bars, selectedBarTime, waveName, waveDegree );

        //}

        //public PooledList<FibLevelInfo> Analyse5A5B5C( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveEnum waveName, ElliottWaveCycle wave5Degree )
        //{
        //    PooledList< FibLevelInfo > output = new PooledList< FibLevelInfo >( );

        //    var smWavesOf5 = _hews.GetAllWavesAfter(waveScenarioNo, period, selectedBarTime );


        //    foreach ( var smallerWaves in smWavesOf5 )
        //    {
        //        ref var hew = ref smallerWaves.GetWaveFromScenario( waveScenarioNo );

        //        var waveInfo = hew.GetHewPointInfoAtCycle( wave5Degree );

        //        if ( waveInfo.HasValue )
        //        {
        //            if ( waveInfo.Value.WaveName == ElliottWaveEnum.WaveA  )
        //            {
        //                Bar5A = _bars.GetBarByTime( smallerWaves.StartDate );
        //            }

        //            if ( waveInfo.Value.WaveName == ElliottWaveEnum.WaveB )
        //            {
        //                Bar5B = _bars.GetBarByTime( smallerWaves.StartDate );
        //            }
        //        }
        //    }

        //    if ( Bar5A != SBar.EmptySBar && Bar5B != SBar.EmptySBar )
        //    {
        //        var symbolex     = SymbolHelper.ToSymbolEx( _bars.Security, period );

        //        var pips         = (double) _bars.Security.PriceStep.Value;

        //        var waveCExp     = _hews.GetHewFibTargets( waveScenarioNo, symbolex, Bar5B.LinuxTime, ElliottWaveEnum.WaveB, wave5Degree );

        //        var wave5C_ABC = waveCExp.TonyGetFibLevelsBtw( FibPercentage.Fib_76_4, FibPercentage.Fib_223_6, 5 * pips );

        //        // If we have wave Bar5A and Bar5B, then we are in the process of Bar5C

        //        var wave5CExp_02 = Analyse5CInternal( _bars, waveScenarioNo, period, Bar5B.LinuxTime, ElliottWaveEnum.WaveB, wave5Degree );

        //        if ( wave5CExp_02.Count > 0 )
        //        {
        //            output.AddRange( wave5CExp_02 );

        //            var diff = ( double ) _bars.Security.PriceStep.Value;

        //            foreach ( FibLevelInfo lvl in wave5C_ABC )
        //            {
        //                var index = output.FindIndex( x => x.WithinCluster( lvl, _bars.PipsAllowance()  ) );

        //                if ( index > -1 )
        //                {
        //                    var selectedLevel = output[ index ];

        //                    selectedLevel.UpdateAll( lvl );
        //                }
        //                else
        //                {
        //                    output.Add( lvl );
        //                }
        //            }
        //        }
        //    }

        //    output = output.OrderByDescending( x => x.OverlappedCount ).ThenByDescending( x => x.LikelyScore ).ToPooledList();

        //    _largerTargets = output;

        //    return output;
        //}

        //private PooledList<FibLevelInfo> Analyse5CInternal( fxHistoricBarsRepo bars, int waveScenarioNo, TimeSpan period, long bar5BTime, ElliottWaveEnum waveB, ElliottWaveCycle wave5Degree )
        //{
        //    PooledList< FibLevelInfo > output = new PooledList< FibLevelInfo >( );

        //    var cSubDegree  = wave5Degree - GlobalConstants.OneWaveCycle;

        //    var smWavesOf5C  = _hews.GetAllWavesOfDegreeAfter( waveScenarioNo, period, bar5BTime, ElliottWaveEnum.WaveB, cSubDegree );

        //    bool hasWave4 = false;

        //    foreach ( var smWaves in smWavesOf5C )
        //    {
        //        var waveInfo = smWaves.GetWaveFromScenario( waveScenarioNo ).GetFirstHighestWaveInfo( );

        //        if ( waveInfo.HasValue )
        //        {
        //            var waveName = waveInfo.Value.WaveName;
        //            var wave4Time = smWaves.StartDate;

        //            if ( waveName == ElliottWaveEnum.Wave4 )
        //            {
        //                var wave5 = new WavePredictionModel( bars, _hews );

        //                wave5.AnalysePastImpulsiveMove( waveScenarioNo, period, bars, wave4Time, ElliottWaveEnum.Wave4, cSubDegree );
        //                wave5.AnalyseCurrentCorrection( waveScenarioNo, period, bars, wave4Time, ElliottWaveEnum.Wave3C, cSubDegree );

        //                if ( wave5.LargerTargets != null  )
        //                {
        //                    output.AddRange( wave5.LargerTargets );
        //                }

        //                hasWave4 = true;

        //                var smallerWavesOf5C  = _hews.GetAllWavesOfDegreeAfter( waveScenarioNo, period, wave4Time, ElliottWaveEnum.Wave4, cSubDegree );

        //                if ( smallerWavesOf5C.Count > 0 )
        //                {
        //                    /* ------------------------------------------------------------------------------------------------
        //                     * 
        //                     * The lower the degree, the higher the accuracy, we need to upgrade its importance.
        //                     * 
        //                     * ------------------------------------------------------------------------------------------------ 
        //                     */
        //                    var evenMorePreciseTarget = Analyse5A5B5C( waveScenarioNo, period, bars, wave4Time, ElliottWaveEnum.Wave4, cSubDegree );

        //                    if ( evenMorePreciseTarget.Count > 0 )
        //                    {
        //                        var diff = ( double ) _bars.Security.PriceStep.Value;

        //                        foreach ( FibLevelInfo largerTarget in evenMorePreciseTarget )
        //                        {
        //                            var index = output.FindIndex( x => x.WithinCluster( largerTarget, _bars.PipsAllowance()  ) );

        //                            if ( index > -1 )
        //                            {
        //                                var selectedLevel = output[ index ];

        //                                selectedLevel.UpdateAll( largerTarget );
        //                            }
        //                            else
        //                            {
        //                                output.Add( largerTarget );
        //                            }
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    output = output.OrderByDescending( x => x.OverlappedCount ).ToPooledList();
        //                    _largerTargets = output;
        //                    return output;
        //                }
        //            }

        //        }
        //    }

        //    if ( output.Count > 0 )
        //    {
        //        output = output.OrderByDescending( x => x.OverlappedCount ).ToPooledList();

        //        _largerTargets = output;

        //    }

        //    return output;
        //}


    }

}
