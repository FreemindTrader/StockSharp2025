using fx.Common;
using fx.Bars;

using DevExpress.Mvvm;

using fx.Definitions;
using fx.Algorithm;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using fx.TALib;
using StockSharp.BusinessEntities;
using fx.Database;
using System.Data.Common;
using fx.Definitions.Collections;
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private BTreeDictionary< long, WavePointImportance > _completeWaveImportanceCopy = null;
        private BTreeDictionary< long, WavePointImportance > _completeGannImportanceCopy = null;
        private BTreeDictionary< long, DbBaZi > _baZiMap = new BTreeDictionary< long, DbBaZi >( );

        private PooledList< KeyValuePair< long, WavePointImportance > > _monoWaveImportance = null;

        private static readonly object _lock = new object( );
        private bool _isBuildingWaveImportance = false;

        private bool AdvancedBuildWaveImportance( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return false;
            }

            var period = Bars.Period.Value;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
            if ( aa == null )
                return false;

            if ( aa.WaveImportanceCalculationAddDone( ) )
            {
                if ( _isBuildingWaveImportance == false )
                {
                    bool lockTaken = false;
                    try
                    {
                        Monitor.TryEnter( _lock, 100, ref lockTaken );

                        if ( lockTaken )
                        {
                            _isBuildingWaveImportance = true;

                            var result1 = BuildRedCirclesGannImportanceAcrossTimeFrame( fullRecalculation, updateType );
                            var result2 = BuildRedStarsWaveImportanceAcrossTimeFrame( fullRecalculation, updateType );

                            _isBuildingWaveImportance = false;

                            return ( result1 & result2 );
                        }
                    }
                    finally
                    {
                        if ( lockTaken )
                        {
                            Monitor.Exit( _lock );
                        }
                    }
                }

            }

            return false;
        }

        private bool BuildRedStarsWaveImportanceAcrossTimeFrame( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            var symbol           = Bars.Security.Code;

            if ( _hews != null )
            {
                SyncRedStarsWaveImportanceDownTimeframe( TimeSpan.FromDays( 1 ) );
            }

            return false;
        }

        private bool BuildRedCirclesGannImportanceAcrossTimeFrame( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            var symbol = Bars.Security.Code;

            if ( _hews != null )
            {
                SyncRedCirclesGannImportanceDownTimeframe( TimeSpan.FromDays( 1 ) );
            }

            return false;
        }


        public void SyncRedCirclesGannImportanceDownTimeframe( TimeSpan currentTF )
        {
            fxHistoricBarsRepo barsLTF = null;

            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneTimeSpanLower( currentTF );
            }
            else
            {
                return;
            }

            if ( lowerTimeSpan < TimeSpan.FromMinutes( 1 ) )
            {
                return;
            }


            var curWaveImpt = _hews.GetGannSwingDictionary(currentTF);
            var curBars     = GetDatabarRepo(currentTF);

            if ( curBars == null )
                return;

            var curRedCircles = curWaveImpt.Where(x => x.Value.WaveImportance >= 5 ).ToArray();

            barsLTF = GetDatabarRepo( lowerTimeSpan );

            if ( barsLTF == null || barsLTF.DoneLoading == false )
                return;
            var lowerWaveImpt = _hews.GetGannSwingDictionary(lowerTimeSpan);


            for ( int i = curRedCircles.Length - 1; i >= 0; --i )
            {
                var waveImp   = curRedCircles[i];
                var ImpValue  = curRedCircles[i].Value.WaveImportance;
                var highestTF = curRedCircles[i].Value.HighestTimeframe;
                ref SBar bar  = ref curBars.GetBarByTime( waveImp.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.BarTime < barsLTF.FirstBarTime )
                    {
                        break;
                    }

                    var extremumType = bar.GetGannType();
                    var beginTime    = bar.BarTime;
                    var endTime      = beginTime + currentTF;


                    if ( ImpValue > -1 && extremumType != TASignal.NONE )
                    {
                        var index = (int)barsLTF.GetHighestGannImportanceInTheRange(lowerTimeSpan, ImpValue, beginTime, endTime, extremumType);

                        if ( index > -1 )
                        {
                            var lowerBarLinuxTime = barsLTF.GetTimeAtIndex(index).Value.ToLinuxTime();

                            WavePointImportance corrLowerTFWaveImportance = null;

                            if ( lowerWaveImpt.TryGetValue( lowerBarLinuxTime, out corrLowerTFWaveImportance ) )
                            {
                                if ( highestTF > corrLowerTFWaveImportance.HighestTimeframe )
                                {
                                    corrLowerTFWaveImportance.HighestTimeframe = highestTF;
                                }
                            }
                        }
                    }
                }
            }

            SyncRedCirclesGannImportanceDownTimeframe( lowerTimeSpan );
        }

        public void SyncRedStarsWaveImportanceDownTimeframe( TimeSpan currentTF )
        {
            fxHistoricBarsRepo barsLTF = null;

            if ( currentTF == TimeSpan.FromDays( 30 ) )
            {

            }

            TimeSpan lowerTimeSpan = TimeSpan.MinValue;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa != null )
            {
                lowerTimeSpan = aa.GetOneTimeSpanLower( currentTF );
            }
            else
            {
                return;
            }

            if ( lowerTimeSpan < TimeSpan.FromMinutes( 1 ) )
            {
                return;
            }


            var curWaveImpt   = _hews.GetAscendingWaveImportanceClone( currentTF );
            var curBars       = GetDatabarRepo( currentTF );

            if ( curBars == null )
                return;

            var curRedDots    = curWaveImpt.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT ).ToArray();

            barsLTF = GetDatabarRepo( lowerTimeSpan );

            if ( barsLTF == null || barsLTF.DoneLoading == false )
                return;
            var lowerWaveImpt = _hews.GetAscendingWaveImportanceClone( lowerTimeSpan );


            for ( int i = curRedDots.Length - 1; i >= 0; --i )
            {
                var waveImp   = curRedDots[ i ];
                var ImpValue  = curRedDots[ i ].Value.WaveImportance;
                var highestTF = curRedDots[ i ].Value.HighestTimeframe;
                ref SBar bar = ref curBars.GetBarByTime( waveImp.Key );

                if ( bar != SBar.EmptySBar )
                {
                    if ( bar.BarTime < barsLTF.FirstBarTime )
                    {
                        break;
                    }

                    var extremumType = bar.GetWaveType( );

                    var beginTime    = bar.BarTime;

                    var endTime = beginTime + currentTF;

                    if ( beginTime.DayOfWeek == DayOfWeek.Sunday )
                    {
                        beginTime = beginTime.AddDays( -2 );
                    }



                    if ( endTime.DayOfWeek == DayOfWeek.Sunday )
                    {
                        endTime = endTime.AddDays( 1 );
                    }


                    if ( ImpValue > -1 && extremumType != TASignal.NONE )
                    {
                        ref SBar hWIbar = ref barsLTF.GetHighestWaveImportanceInTheRange( aa.HewManager, lowerTimeSpan, ImpValue, beginTime, endTime, extremumType );

                        if ( hWIbar != SBar.EmptySBar )
                        {
                            var lowerBarLinuxTime = hWIbar.LinuxTime;

                            WavePointImportance corrLowerTFWaveImportance = null ;

                            if ( lowerWaveImpt.TryGetValue( lowerBarLinuxTime, out corrLowerTFWaveImportance ) )
                            {
                                if ( highestTF > corrLowerTFWaveImportance.HighestTimeframe )
                                {
                                    corrLowerTFWaveImportance.HighestTimeframe = highestTF;
                                }
                            }
                        }
                    }
                }
            }

            SyncRedStarsWaveImportanceDownTimeframe( lowerTimeSpan );
        }

        private void OnIdentifyElliottWave( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan inputPeriod, int barsCountBeforeCalculation )
        {
            //ThreadHelper.UpdateThreadName( "OnIdentifyElliottWave" );

            AdvancedBuildWaveImportance( fullRecalculation, updateType );

            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            var barB4Calculation = _barCountBeforeCalculation;

            var symbol = Bars.Security;
            var period = Bars.Period.Value;

            if ( _hews == null )
            {
                return;
            }

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );
            if ( aa == null )
                return;

            var taManager = ( PeriodXTaManager )aa.GetPeriodXTa( period );

            _completeWaveImportanceCopy = _hews.GetAscendingWaveImportanceClone( period );
            _completeGannImportanceCopy = _hews.GetGannSwingDictionaryClone( period );

            if ( inputPeriod == TimeSpan.FromHours( 1 ) )
            {
                if ( _completeWaveImportanceCopy.Count > 0 )
                {
                    DetectBaZiOfRedDots( symbol, period, taManager, _completeWaveImportanceCopy );

                    if ( _baZiMap.Count > 0 )
                    {
                        //SaveBaZiToDB( );
                    }
                }
            }

            if ( inputPeriod == TimeSpan.FromMinutes( 1 ) || inputPeriod == TimeSpan.FromHours( 1 ) || inputPeriod == TimeSpan.FromDays( 1 ) )
            {
                //_hews.BuildElliottWaves( period );

                // AnalyzeHarmonicElliottWaves( symbol, period, taManager );

                if ( _completeWaveImportanceCopy.Count > 0 )
                {
                    aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
                    if ( aa == null )
                        return;

                    _monoWaveManager = aa.CreateOrGetMonowaveManager( inputPeriod );

                    if ( updateType == DataBarUpdateType.Initial )
                    {
                        _monoWaveManager.Initialize( Bars, _completeWaveImportanceCopy );
                        _monoWaveManager.BuildMonoWaves( taManager );
                        _monoWaveManager.NewAnalyzeMonoWaves( symbol, period, taManager, _completeWaveImportanceCopy );
                    }
                    else if ( updateType == DataBarUpdateType.HistoryUpdate )
                    {

                    }
                }
            }           
        }

        protected void FindOutWhereAreWeInWavesCycle( int waveScenarioNo, Security symbol, TimeSpan period )
        {
            PooledList<WavePointInfo> highestWaves =  _hews.GetWavesOfHighestDegree( waveScenarioNo, period );

            if ( highestWaves.Count > 0 )
            {
                _hews.AnalyseWaveTargetsFromHere( highestWaves );
            }

            // 
            // Firstly I need to find out the larget cycle and see where we are.

            //ElliottWaveCycle topDown = ElliottWaveCycle.GrandSupercycle;

            //do
            //{
            //    PooledList< WavePointInfo > allWaves = _hews.GetWavesOfDegree(period, topDown, DatabarsRepo );

            //    _hews.WhereAreWeInWavesCycle( allWaves, DatabarsRepo, period, waveImportance );

            //    topDown -= GlobalConstants.OneWaveCycle;
            //}
            //while ( topDown != ElliottWaveCycle.UNKNOWN );

        }

        private void SaveBaZiToDB( )
        {
            bool didUpdate = false;

            using ( var context = new ForexDatabars( ) )
            {
                if ( _baZiMap.Count > 0 )
                {
                    didUpdate = true;

                    foreach ( var baziPair in _baZiMap )
                    {
                        var barLinuxTime = baziPair.Key;
                        var baziInfo = baziPair.Value;
                        long id = baziInfo.Id;

                        if ( id > 0 )
                        {
                            var found = from b in context.BAZI where b.Id == id select b;

                            if ( found.Any( ) )
                            {
                                continue;
                            }
                            else
                            {
                                var first            = context.BAZI.Create( );
                                first.UtcDate = baziInfo.UtcDate;
                                first.LocalDate = baziInfo.LocalDate;
                                first.Symbol = baziInfo.Symbol;
                                first.WaveImportance = baziInfo.WaveImportance;
                                first.Period = baziInfo.Period;
                                first.YearlyBaZi = baziInfo.YearlyBaZi;
                                first.MonthlyBaZi = baziInfo.MonthlyBaZi;
                                first.DailyBaZi = baziInfo.DailyBaZi;
                                first.HourlyBaZi = baziInfo.HourlyBaZi;
                                first.BelongsTo = baziInfo.BelongsTo;
                                first.BaZiEval = baziInfo.BaZiEval;
                                first.Gold = baziInfo.Gold;
                                first.Wood = baziInfo.Wood;
                                first.Water = baziInfo.Water;
                                first.Fire = baziInfo.Fire;
                                first.Earth = baziInfo.Earth;
                                first.Same = baziInfo.Same;
                                first.Difference = baziInfo.Difference;
                                first.TopBottom = baziInfo.TopBottom;

                                context.BAZI.Add( first );
                            }
                        }
                        else
                        {
                            var found = from b in context.BAZI where b.UtcDate == baziInfo.UtcDate && b.Symbol == baziInfo.Symbol select b;

                            if ( found.Any( ) )
                            {
                                continue;
                            }
                            else
                            {
                                var first            = context.BAZI.Create( );
                                first.UtcDate = baziInfo.UtcDate;
                                first.LocalDate = baziInfo.LocalDate;
                                first.Symbol = baziInfo.Symbol;
                                first.WaveImportance = baziInfo.WaveImportance;
                                first.Period = baziInfo.Period;
                                first.YearlyBaZi = baziInfo.YearlyBaZi;
                                first.MonthlyBaZi = baziInfo.MonthlyBaZi;
                                first.DailyBaZi = baziInfo.DailyBaZi;
                                first.HourlyBaZi = baziInfo.HourlyBaZi;
                                first.BelongsTo = baziInfo.BelongsTo;
                                first.BaZiEval = baziInfo.BaZiEval;
                                first.Gold = baziInfo.Gold;
                                first.Wood = baziInfo.Wood;
                                first.Water = baziInfo.Water;
                                first.Fire = baziInfo.Fire;
                                first.Earth = baziInfo.Earth;
                                first.Same = baziInfo.Same;
                                first.Difference = baziInfo.Difference;
                                first.TopBottom = baziInfo.TopBottom;

                                context.BAZI.Add( first );
                            }
                        }
                    }
                }

                if ( didUpdate )
                {
                    try
                    {
                        context.SaveChanges( );
                    }
                    catch ( DbException ex )
                    {
                        //this.LogError( ex );
                    }

                    Messenger.Default.Send( new WorkDoneMessage( "Save Waves to Database" ) );
                }
            }
        }


        protected void DetectBaZiOfRedDots( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var barB4Calculation = _barCountBeforeCalculation;

            foreach ( KeyValuePair<long, WavePointImportance> wave in waveImportance )
            {
                ref SBar bar = ref
                Bars.GetBarByTime(  wave.Key );

                string bazi = "";
                string baziY = "";
                string baziM = "";
                string baziD = "";
                string baziH = "";
                string bazieval = "";
                double gold = 0;
                double wood = 0;
                double water = 0;
                double fire = 0;
                double earth = 0;
                double same = 0;
                double diff = 0;
                string belongsTo = "";
                bool topBottom = false;

                if ( wave.Value.Signal == TASignal.WAVE_PEAK )
                {
                    var barTime    = bar.LocalTime;

                    var calender   = new ChineseCalendar( barTime );

                    baziY = calender.GanZhiYearSimple;
                    baziM = calender.GanZhiMonthSimple;
                    baziD = calender.GanZhiDaySimple;
                    bazi = calender.GanZhiDateSimple;

                    var baziAlgo   = new BaziAlgorithm( );

                    baziH = baziAlgo.ComputeTimeGan( bazi, barTime.Hour );

                    var full       = baziY + baziM + baziD + baziH;

                    bazieval = baziAlgo.EvalBazi( full );

                    belongsTo = baziAlgo.BelongsTo.ToString( );
                    gold = baziAlgo.GetStrength( 0 );
                    wood = baziAlgo.GetStrength( 1 );
                    water = baziAlgo.GetStrength( 2 );
                    fire = baziAlgo.GetStrength( 3 );
                    earth = baziAlgo.GetStrength( 4 );
                    same = baziAlgo.TongLei;
                    diff = baziAlgo.YiLei;
                    topBottom = true;

                    bar.BaZiString = bazieval;

                }
                else if ( wave.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    var barTime  = bar.LocalTime;

                    var calender = new ChineseCalendar( barTime );

                    baziY = calender.GanZhiYearSimple;
                    baziM = calender.GanZhiMonthSimple;
                    baziD = calender.GanZhiDaySimple;
                    bazi = calender.GanZhiDateSimple;

                    var baziAlgo = new BaziAlgorithm( );

                    baziH = baziAlgo.ComputeTimeGan( bazi, barTime.Hour );

                    var full     = baziY + baziM + baziD + baziH;

                    bazieval = baziAlgo.EvalBazi( full );

                    belongsTo = baziAlgo.BelongsTo.ToString( );
                    gold = baziAlgo.GetStrength( 0 );
                    wood = baziAlgo.GetStrength( 1 );
                    water = baziAlgo.GetStrength( 2 );
                    fire = baziAlgo.GetStrength( 3 );
                    earth = baziAlgo.GetStrength( 4 );
                    same = baziAlgo.TongLei;
                    diff = baziAlgo.YiLei;

                    bar.BaZiString = bazieval;
                }

                if ( !_baZiMap.ContainsKey( bar.LinuxTime ) )
                {
                    var dbbazi = new DbBaZi( bar.BarTime, bar.LocalTime, symbol.Code, period.ToShortForm(), topBottom, wave.Value.WaveImportance, baziY, baziM, baziD, baziH, belongsTo, bazieval, gold, wood, water, fire, earth, same, diff );

                    _baZiMap.Add( bar.LinuxTime, dbbazi );
                }
            }
        }

        protected void FindOutWhereAreWeInWavesCycle( int waveScenarioNo, Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            // 
            // Firstly I need to find out the larget cycle and see where we are.

            ElliottWaveCycle topDown = ElliottWaveCycle.GrandSupercycle;

            do
            {
                PooledList< WavePointInfo > allWaves = _hews.GetWavesOfDegree(waveScenarioNo, period, topDown, Bars );

                _hews.WhereAreWeInWavesCycle( waveScenarioNo, allWaves, Bars, period, waveImportance );

                topDown -= GlobalConstants.OneWaveCycle;
            }
            while ( topDown != ElliottWaveCycle.UNKNOWN );

        }

        //protected void AnalyzeMonoWaves( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        //{
        //    var waves = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT ).ToArray();

        //    var peakTroughCounts = waves.Count();

        //    if ( peakTroughCounts >= 3 )
        //    {
        //        var nM1    = waves[ peakTroughCounts - 3 ];
        //        var n      = waves[ peakTroughCounts - 2 ];
        //        var nP1    = waves[ peakTroughCounts - 1 ];

        //        var nP1Bar = DatabarsRepo.GetBarByTime( nP1.Key );
        //        var nBar   = DatabarsRepo.GetBarByTime( n.Key );
        //        var nM1Bar = DatabarsRepo.GetBarByTime( nM1.Key );
        //        
        //        RuleOfObservationAndRetracement( symbol, period, taManager, waves, nP1, n, nM1 );                                
        //    }
        //}

        //



        protected void AnalyzeHarmonicElliottWaves( Security symbol, TimeSpan period, PeriodXTaManager taManager )
        {
            var barB4Calculation = _barCountBeforeCalculation;

            var hourlyWaveImpt = _hews.GetAscendingWaveImportanceClone( TimeSpan.FromHours( 1 )  );

            var waves = hourlyWaveImpt.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT ).ToArray();

            var peakTroughCounts = waves.Count();

            if ( peakTroughCounts >= 3 )
            {
                var oldestWave = waves[ 0 ];

                var n_2    = waves[ peakTroughCounts - 3 ];
                var n_1    = waves[ peakTroughCounts - 2 ];
                var n      = waves[ peakTroughCounts - 1 ];

                var barRepo1hr  = SymbolsMgr.Instance.GetDatabarRepo( _indicatorSecurity, TimeSpan.FromHours( 1 ) );

                ref SBar nbar = ref barRepo1hr.GetBarByTime( n.Key );
                ref SBar n_2bar = ref barRepo1hr.GetBarByTime( n_2.Key );


                if ( n.Value.Signal == TASignal.WAVE_PEAK && n_2.Value.Signal == TASignal.WAVE_PEAK )
                {
                    if ( nbar.High >= n_2bar.High )
                    {
                        DetectImpulsiveUpTrend_1min( symbol, period, taManager, n, n_1 );
                    }
                    else
                    {
                        DetectImpulsiveDownTrend_1min( symbol, period, taManager, n, n_1 );
                    }
                }
                else if ( n.Value.Signal == TASignal.WAVE_TROUGH && n_2.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    if ( nbar.Low <= n_2bar.Low )
                    {
                        DetectImpulsiveDownTrend_1min( symbol, period, taManager, n, n_1 );
                    }
                    else
                    {
                        DetectImpulsiveUpTrend_1min( symbol, period, taManager, n, n_1 );
                    }
                }
            }
        }

        private void DetectImpulsiveUpTrend_1min( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> n, KeyValuePair<long, WavePointImportance> n_1 )
        {
            var waveImpt1Min  = _hews.GetAscendingWaveImportanceClone( TimeSpan.FromMinutes( 1 )  );
            ref SBar beginBar = ref Bars.GetLowestBarOfTheRange( n_1.Key.FromLinuxTime(), TimeSpan.FromHours( 1 ) );
            ref SBar endBar   = ref Bars.GetHighestBarOfTheRange( n.Key.FromLinuxTime(), TimeSpan.FromHours( 1 ) ) ;

            if ( beginBar != SBar.EmptySBar && endBar != SBar.EmptySBar )
            {
                var beginTime = beginBar.LinuxTime;
                var endTime = endBar.LinuxTime;

                var inBtwWaves = waveImpt1Min.Where(x => x.Key >= beginTime && x.Key <= endTime ).ToList();
                int count      = inBtwWaves.Count;

                //PooledList<KeyValuePair<long, WavePointImportance>> selected = null;
                //MonoWavesGroup corrections = null;

                //if ( count > 2 )
                //{
                //    var begin   = inBtwWaves.Find( x => x.Key == beginBar.LinuxTime );
                //    var end     = inBtwWaves.Find( x => x.Key == endBar.LinuxTime   );

                //    selected    = inBtwWaves;
                //    corrections = Get1MinMonoWavesGroup_Uptrend( symbol, TimeSpan.FromMinutes( 1 ), inBtwWaves, begin, beginBar, end, endBar, GlobalConstants.DAILYIMPT );

                //    //var potentialWave3 = FindNonOverlappingRegion( TimeSpan.FromMinutes( 1 ), begin, end, count, selected, corrections );
                //}                
            }

        }

        private void DetectImpulsiveDownTrend_1min( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> n, KeyValuePair<long, WavePointImportance> n_1 )
        {
            var waveImpt1Min  = _hews.GetAscendingWaveImportanceClone( TimeSpan.FromMinutes( 1 )  );
            ref SBar beginBar = ref Bars.GetLowestBarOfTheRange( n_1.Key.FromLinuxTime(), TimeSpan.FromHours( 1 ) );
            ref SBar endBar   = ref Bars.GetHighestBarOfTheRange( n.Key.FromLinuxTime(), TimeSpan.FromHours( 1 ) ) ;

            if ( beginBar != SBar.EmptySBar && endBar != SBar.EmptySBar )
            {
                var beginTime = beginBar.LinuxTime;
                var endTime = endBar.LinuxTime;

                var inBtwWaves = waveImpt1Min.Where(x => x.Key >= beginTime && x.Key <= endTime ).ToList();
                int count      = inBtwWaves.Count;

                List<KeyValuePair<long, WavePointImportance>> selected = null;
                //MonoWavesGroup corrections = null;

                if ( count > 2 )
                {
                    var begin   = inBtwWaves.Find( x => x.Key == beginTime );
                    var end     = inBtwWaves.Find( x => x.Key == endTime   );

                    selected = inBtwWaves;
                    //corrections = Get1MinMonoWavesGroup_Uptrend( symbol, TimeSpan.FromMinutes( 1 ), inBtwWaves, begin, beginBar, end, endBar, GlobalConstants.DAILYIMPT );

                    //var potentialWave3 = FindNonOverlappingRegion( TimeSpan.FromMinutes( 1 ), begin, end, count, selected, corrections );
                }
            }

        }

        //public MonoWavesGroup Get1MinMonoWavesGroup_Uptrend( Security symbol, TimeSpan period, PooledList< KeyValuePair<long, WavePointImportance>> waveImportance, KeyValuePair<long, WavePointImportance> beginPt, ref SBar beginBar, KeyValuePair<long, WavePointImportance> endPt, ref SBar endBar, int inputWaveImportance )
        //{
        //    MonoWavesGroup output = new MonoWavesGroup( beginPt, beginBar, endPt, endBar, TrendDirection.Uptrend );

        //    var firstRedDot = waveImportance.Where( x => x.Key > beginPt.Key && x.Value.WaveImportance == GlobalConstants.DAILYIMPT ).Take( 1 );

        //    return output;
        //}

        private void DetectImpulsiveInCorrection_Upward( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves, int currentWaveImportance )
        {
            var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfUptrend.HasValue )
            {
                ref SBar upbar = ref
                Bars.GetBarByTime(  beginOfUptrend.Value.Key );

                if ( beginOfUptrend.Value.Key > oldestWave.Key )
                {
                    var upCorrections = GetCorrectionsBtwXY_Uptrend( symbol, period, selectedWaves, beginOfUptrend.Value, latestWave, currentWaveImportance );

                    if ( upCorrections.Count > 0 )
                    {
                        foreach ( var correction in upCorrections )
                        {
                            if ( correction.HasPriceTimeInfo( ) )
                            {
                                WaveType correctionWaveType = GetCorrectionWaveType_Downward( symbol, period, taManager, correction, currentWaveImportance );

                                if ( correctionWaveType != WaveType.UNKNOWN )
                                {

                                }
                            }
                        }
                    }

                    var priorWaves = selectedWaves.Where(x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key  ).ToArray();

                    var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, beginOfUptrend.Value, priorWaves );

                    if ( beginOfDowntrend.HasValue )
                    {
                        ref SBar downBar = ref
                        Bars.GetBarByTime(  beginOfDowntrend.Value.Key );

                        if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                        {
                            var downCorrections = GetCorrectionsBtwXY_DownTrend( symbol, period, priorWaves,beginOfDowntrend.Value, beginOfUptrend.Value, currentWaveImportance );

                            if ( downCorrections.Count > 0 )
                            {
                                foreach ( var correction in downCorrections )
                                {
                                    if ( correction.HasPriceTimeInfo( ) )
                                    {
                                        WaveType correctionWaveType = GetCorrectionWaveType_Upward( symbol, period, taManager, correction, currentWaveImportance );

                                        if ( correctionWaveType != WaveType.UNKNOWN )
                                        {

                                        }
                                    }
                                }
                            }

                            priorWaves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                            DetectImpulsiveInCorrection_Upward( symbol, period, taManager, oldestWave, beginOfDowntrend.Value, priorWaves, currentWaveImportance );
                        }
                    }
                }
            }
            else
            {

            }
        }



        private void DetectImpulsiveInCorrection_Downward( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves, int currentWaveImportance )
        {
            var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfDowntrend.HasValue )
            {
                ref SBar downBar = ref
                Bars.GetBarByTime(  beginOfDowntrend.Value.Key );

                if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                {
                    var downCorrections = GetCorrectionsBtwXY_DownTrend( symbol, period, selectedWaves, beginOfDowntrend.Value, latestWave, currentWaveImportance );

                    if ( downCorrections.Count > 0 )
                    {
                        foreach ( var correction in downCorrections )
                        {
                            WaveType correctionWaveType = GetCorrectionWaveType_Upward( symbol, period, taManager, correction, currentWaveImportance );

                            if ( correctionWaveType != WaveType.UNKNOWN )
                            {

                            }
                        }
                    }

                    var priorWaves = selectedWaves.Where(x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key  ).ToArray();

                    var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, beginOfDowntrend.Value, priorWaves );

                    if ( beginOfUptrend.HasValue )
                    {
                        ref SBar upBar = ref
                        Bars.GetBarByTime(  beginOfUptrend.Value.Key  );

                        if ( beginOfUptrend.Value.Key > oldestWave.Key )
                        {
                            var upCorrections = GetCorrectionsBtwXY_Uptrend( symbol, period, priorWaves, beginOfUptrend.Value, beginOfDowntrend.Value, currentWaveImportance );

                            if ( upCorrections.Count > 0 )
                            {
                                foreach ( var correction in upCorrections )
                                {
                                    WaveType correctionWaveType = GetCorrectionWaveType_Downward( symbol, period, taManager, correction, currentWaveImportance );

                                    if ( correctionWaveType != WaveType.UNKNOWN )
                                    {

                                    }
                                }
                            }

                            priorWaves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                            DetectImpulsiveInCorrection_Downward( symbol, period, taManager, oldestWave, beginOfUptrend.Value, priorWaves, currentWaveImportance );
                        }
                    }
                }
            }
            else
            {

            }

        }


        //public HewFibTargets 
    }
}
