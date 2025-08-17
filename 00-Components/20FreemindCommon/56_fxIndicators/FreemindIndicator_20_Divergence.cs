using fx.Common;


using DevExpress.Mvvm;

using fx.Definitions;
using fx.Algorithm;
using fx.Bars;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using StockSharp.BusinessEntities;
using System.Threading;
using fx.Definitions.Collections;
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        protected Task AdvancedMacdDivergenceTasks( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barB4Calculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => DetectDivergenceFromZigZag( fullRecalculation, updateType, barB4Calculation, IndicatorExitToken ), IndicatorExitToken);

            tasksList.Add( first );

            return first;
        }

        protected void DetectDivergenceFromZigZag( bool fullRecalculation, DataBarUpdateType? updateType, int barB4Calculation, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            //ThreadHelper.UpdateThreadName( "DetectDivergenceFromZigZag" );

            var symbol           = Bars.Security;
            var period           = Bars.Period.Value;

            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa != null )
            {
                _hews = ( HewManager ) aa.HewManager;

                if ( _hews != null )
                {
                    var taManager          = ( PeriodXTaManager ) aa.GetPeriodXTa( period );
                    var waveImportanceCopy = _hews.GetAscendingWaveImportanceClone(period);

                    BuildMacdCrossExtremumDictionary( fullRecalculation, updateType );

                    if ( waveImportanceCopy.Count > 0 )
                    {
                        DetectMostRecentBarDivergence( symbol, period, taManager, waveImportanceCopy, token );
                        DetectLongTermR2RDivergence( symbol, period, taManager, waveImportanceCopy, token );
                        DetectShortTermLocalDivergence( symbol, period, taManager, waveImportanceCopy, token );
                        DetectLongTermR2RHiddenDivergence( symbol, period, taManager, waveImportanceCopy, token );
                        DetectShortTermHiddenDivergence( symbol, period, taManager, waveImportanceCopy, token );
                    }
                }
            }
        }

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\HiddenDivergence.png" />
        /// </summary>
        /// <param name="fullRecalculation"></param>
        /// <param name="updateType"></param>
        /// 

        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DivergenceCurrent.png" /> 
        protected void DetectMostRecentBarDivergence( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------------------
             * 
             * For the most recent databars, we need to check for both positive and negative divergence as this might be the double top or the double bottom.
             * 
             * -------------------------------------------------------------------------------------------------------------------------------------------------------
             */

            var redWaves             = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT ).ToArray();
            var redWaveCount         = redWaves.Count();

            if ( redWaveCount < 1 ) return;

            var mostRecentRed        = redWaves[ redWaveCount - 1 ];

            var macdSignificantCross = _macdSignificantCross;

            int lastMacdCross = -1;

            if ( macdSignificantCross.Count > 0 )
            {
                lastMacdCross = macdSignificantCross.Keys[ macdSignificantCross.Count - 1 ];
            }

            ref SBar bar = ref Bars.GetBarByTime(mostRecentRed.Key );

            if ( bar == SBar.EmptySBar ) return;
            

            if ( period == TimeSpan.FromHours( 2 ) )
            {

            }

            if ( lastMacdCross > bar.BarIndex )
            {
                // The last Macd cross is newer than our last red dot. When there is no cross, all calculation is not important.
                var newestWaves          = waveImportance.Where(x => x.Key >= mostRecentRed.Key  ).OrderBy(x => x.Key).ToArray();

                if ( newestWaves.Count() > 1 )
                {
                    if ( mostRecentRed.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        // Here we have a Local down Trend
                        DetectRecentDivergenceInDowntrend( symbol, period, taManager, mostRecentRed, Bars.LastBarTime.Value.ToLinuxTime(), newestWaves, token );
                        DetectHiddenDivergenceInRecentDowntrend( symbol, period, taManager, mostRecentRed, newestWaves, token );
                    }
                    else if ( mostRecentRed.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        // Here we have a Local Up Trend
                        DetectRecentDivergenceInUptrend( symbol, period, taManager, mostRecentRed, Bars.LastBarTime.Value.ToLinuxTime(), newestWaves, token );
                        DetectHiddenDivergenceInRecentUptrend( symbol, period, taManager, mostRecentRed, newestWaves, token );
                    }
                }
            }
        }

        private void DetectHiddenDivergenceInRecentDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance>[ ] innerWaves, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;



            var nonSmoothedInnerTurningPts = innerWaves.Where(x =>
                                                                    {
                                                                        if (x.Value.Signal == TASignal.WAVE_PEAK)
                                                                        {
                                                                            ref SBar bar = ref Bars.GetBarByTime(x.Key );

                                                                            if ( bar == SBar.EmptySBar ) return false;
                                                                            

                                                                            if (extremumsValueDictionary.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }

                                                                            if (macdExtremumDict.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }
                                                                        }
                                                                        return false;
                                                                    }
                                                                );



            var nonSmoothedinnerTurningPoints = nonSmoothedInnerTurningPts.ToList();

            // This need to include the first bottom. If not, then the bottom is just a spike so the macd doesn't cross over
            if ( !nonSmoothedinnerTurningPoints.Contains( previous ) )
            {
                nonSmoothedinnerTurningPoints.Insert( 0, previous );
            }

            var innerPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in nonSmoothedinnerTurningPoints )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar == SBar.EmptySBar ) return;                

                var myMacd = IndicatorResult["MACD"][(int) bar.BarIndex];

                if ( myMacd > 0 )
                {
                    innerPeaks.Add( turningPoint );
                }
            }

            var innerPeaksCount = innerPeaks.Count();
            int j               = innerPeaksCount - 1;

            while ( j > 0 )
            {
                var currentPeak    = innerPeaks[j];
                var tr             = j - 1;


                ref SBar barCurrent = ref Bars.GetBarByTime(currentPeak.Key );

                if ( barCurrent == SBar.EmptySBar ) break;
                                                               
                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];

                if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex >= 8835 && j == 1 )
                {

                }

                while ( tr >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var previousPeak = innerPeaks[tr];

                    ref SBar barPrevious = ref Bars.GetBarByTime(currentPeak.Key );
                    
                    if ( barPrevious == SBar.EmptySBar )
                    {
                        tr--;
                        continue;
                    }

                    if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex >= 8836 && j == 1 )
                    {

                    }

                    // 00 - Here we have detected Lower Top
                    if ( LocalLowerOrCloselyEqual( period, barCurrent.High, barPrevious.High ) == DivergenceBoolean.False )
                    {
                        tr--;
                        continue;
                    }


                    // 01 - Now we want to detect that the MACD is rising much stronger
                    if ( ( MacdHigherOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False ) && NoHigherMacdPeaksInBetween( ref barPrevious, ref barCurrent ) )
                    {
                        _macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                        foundDivergence = true;

                        var divergence = (barCurrent.High < barPrevious.High) ? TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW : TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP;

                        if ( ( int ) barCurrent.BarIndex > ithBar )
                        {
                            ithBar = ( int ) barCurrent.BarIndex;

                            lastSignal = divergence;
                        }

                        var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );

                        var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                        if ( aa == null )
                        {
                            return;
                        }

                        aa.AddDivergenceSignal( ref barCurrent, div );
                    }

                    tr--;
                }
                j--;
            }



            // Find divergence without major turning points
            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( _macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectHiddenDivergenceInRecentUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance>[ ] innerWaves, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = innerWaves.Where(x =>
                                                                    {
                                                                        if (x.Value.Signal == TASignal.WAVE_TROUGH)
                                                                        {
                                                                            ref SBar bar = ref Bars.GetBarByTime(x.Key );

                                                                            if ( bar == SBar.EmptySBar ) return false;

                                                                            if (macdExtremumDict.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }
                                                                        }
                                                                        return false;
                                                                    }
                                                                );


            var nonSmoothedinnerTurningPoints = nonSmoothedInnerTurningPts.ToList();

            // This need to include the first bottom. If not, then the bottom is just a spike so the macd doesn't cross over
            if ( !nonSmoothedinnerTurningPoints.Contains( previous ) )
            {
                nonSmoothedinnerTurningPoints.Insert( 0, previous );
            }

            var innerTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in nonSmoothedinnerTurningPoints )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar == SBar.EmptySBar ) break;

                var myMacd = IndicatorResult["MACD"][(int) bar.BarIndex];

                if ( myMacd < 0 )
                {
                    innerTroughs.Add( turningPoint );
                }
            }


            var innerTroughsCount = innerTroughs.Count();
            int j                 = innerTroughsCount - 1;

            while ( j > 0 )
            {
                var currentTrough = innerTroughs[j];

                var tr = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime(currentTrough.Key );

                if ( barCurrent == SBar.EmptySBar )
                {
                    break;
                }

                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];
                

                while ( tr >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var previousTrough = innerTroughs[tr];
                    ref SBar barPrevious = ref Bars.GetBarByTime( previousTrough.Key );

                    if ( barPrevious == SBar.EmptySBar )
                    {
                        tr--;
                        continue;
                    }

                    // 00 - Here we have detected higher bottom
                    if ( LocalHigherOrCloselyEqual( period, barCurrent.Low, barPrevious.Low ) == DivergenceBoolean.False )
                    {
                        tr--;
                        continue;
                    }


                    // 01 - Now we want to detect that the MACD is falling much stronger
                    if ( ( MacdLowerOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False ) && NoLowerMacdTroughsInBetween( ref barPrevious, ref barCurrent ) )
                    {
                        _macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                        foundDivergence = true;

                        var divergence = (barCurrent.Low < barPrevious.Low) ? TADivergence.HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH : TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM;

                        if ( ( int ) barCurrent.BarIndex > ithBar )
                        {
                            ithBar = ( int ) barCurrent.BarIndex;

                            lastSignal = divergence;
                        }

                        var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );

                        var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                        if ( aa == null )
                        {
                            return;
                        }

                        aa.AddDivergenceSignal( ref barCurrent, div );
                    }

                    tr--;

                }

                j--;
            }

            // Find divergence without major turning points
            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( _macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectRecentDivergenceInDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, long latestBarTime, KeyValuePair<long, WavePointImportance>[ ] waveImportance, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict         = _macdExtremumDict;
            var macdDivergence           = _macdDivergence;

            var lowerLowTroughs          = GetLowerLowTroughsBtwXY_NoLatest_OnlyCertainWaveImportance( period, waveImportance, latestBarTime, previous.Key, GetWaveImportanceToDetect( period )  );
            var troughsCount             = lowerLowTroughs.Count();
            int j                        = troughsCount - 1;


            while ( j > 0 )
            {
                var currentTrough  = lowerLowTroughs[j];
                var p              = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime(currentTrough.Key );

                if ( barCurrent == SBar.EmptySBar ) break;
                
                if ( barCurrent.IsWaveTrough() || barCurrent.IsGannTrough() )
                {
                    var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];

                    while ( p >= 0 )
                    {
                        if ( token.IsCancellationRequested )
                        {
                            token.ThrowIfCancellationRequested();
                        }

                        var previousTrough  = lowerLowTroughs[p];

                        ref SBar barPrevious = ref Bars.GetBarByTime(previousTrough.Key );

                        if ( barPrevious == SBar.EmptySBar ) break;                        

                        var barPreviousMacd = IndicatorResult["MACD"][(int)barPrevious.BarIndex];
                        var lowerLow        = LocalLowerOrCloselyEqual(period, barCurrent.Low, barPrevious.Low);

                        if ( lowerLow == DivergenceBoolean.False )
                        {
                            break;
                        }

                        var macdSignificantCross   = _macdSignificantCross;
                        var macdCount              = macdSignificantCross.Count;

                        var inBtw = new PooledList< int >( );

                        for ( int i = 0; i < macdCount; i++ )
                        {
                            var key = macdSignificantCross.Keys[ i ];

                            if ( ( key >= ( int ) barPrevious.BarIndex ) && ( key < ( int ) barCurrent.BarIndex ) )
                            {
                                inBtw.Add( key );
                            }
                        }

                        if ( inBtw.Count <= 1 )
                        {
                            bool found = false;

                            Tuple< MacdSignal, double > currentBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barCurrent.BarIndex, out currentBarInfo ) )
                            {
                                if ( currentBarInfo.Item1 == MacdSignal.LowerLow_MacdUptrend )
                                {
                                    found = true;
                                }
                            }

                            Tuple< MacdSignal, double > previousBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barPrevious.BarIndex, out previousBarInfo ) )
                            {
                                if ( previousBarInfo.Item1 == MacdSignal.LowerLow_MacdUptrend )
                                {
                                    found = true;
                                }
                            }

                            if ( found )
                            {
                                p--;
                                continue;
                            }
                            else
                            {
                                ref SBar latestBar = ref Bars.GetBarByTime( latestBarTime );

                                if ( latestBar == SBar.EmptySBar ) break;
                                
                                if ( barCurrent.BarIndex != latestBar.BarIndex )
                                {

                                }
                            }
                        }

                        // Here we have lower low in Price but higher High in MACD

                        if ( MacdHigherOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False )
                        {
                            macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                            foundDivergence = true;

                            var divergence = (lowerLow == DivergenceBoolean.CloselyEqual) ? TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM : TADivergence.POSITIVE_DIVERGENCE_LOWER_LOW;

                            if ( ( int ) barCurrent.BarIndex > ithBar )
                            {
                                ithBar = ( int ) barCurrent.BarIndex;

                                lastSignal = divergence;
                            }


                            var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.Low, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.Low, barCurrent.BarTime );

                            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                            if ( aa == null )
                            {
                                return;
                            }

                            aa.AddDivergenceSignal( ref barCurrent, div );

                            break;
                        }

                        p--;
                    }
                }
                else
                {

                }



                j--;
            }

            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }



        private void DetectRecentDivergenceInUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, long latestBarTime, KeyValuePair<long, WavePointImportance>[ ] waveImportance, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict         = _macdExtremumDict;
            var macdDivergence           = _macdDivergence;

            var higherHighTops           = GetHigherHighPeaksBtwXY_NoLatest_OnlyCertainWaveImportance( period, waveImportance, previous.Key, latestBarTime, GetWaveImportanceToDetect( period ) );
            //var higherHighTops           = GetHigherHighPeaksBtwXY_NoLatest( period, waveImportance, latestBarTime, previous.Key );
            var innerPeaksCount          = higherHighTops.Count();
            int j                        = innerPeaksCount - 1;

            while ( j > 0 )
            {
                var currentTop = higherHighTops[j];
                var p          = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime( currentTop.Key );

                if ( barCurrent == SBar.EmptySBar ) break;                

                if ( barCurrent.IsWavePeak() || barCurrent.IsGannPeak() )
                {
                    while ( p >= 0 )
                    {
                        if ( token.IsCancellationRequested )
                        {
                            token.ThrowIfCancellationRequested();
                        }

                        var previousTop = higherHighTops[p];

                        ref SBar barPrevious = ref Bars.GetBarByTime( previousTop.Key );

                        if ( barPrevious == SBar.EmptySBar ) break;
                        

                        var higherHigh  = LocalHigherOrCloselyEqual(period, barCurrent.High, barPrevious.High);

                        if ( higherHigh == DivergenceBoolean.False )
                        {
                            break;
                        }


                        ///// <image url="$(SolutionDir)\..\..\30 - CommonImages\DivergenceBug35.png" />
                        /*
                         * Here we need to if there is a significant cross up between the two points to not compare the higher high in 
                         * 
                         * 
                         */

                        var macdSignificantCross   = _macdSignificantCross;
                        var macdCount              = macdSignificantCross.Count;

                        var inBtw = new PooledList< int >( );

                        for ( int i = 0; i < macdCount; i++ )
                        {
                            var key = macdSignificantCross.Keys[ i ];

                            if ( ( key >= ( int ) barPrevious.BarIndex ) && ( key < ( int ) barCurrent.BarIndex ) )
                            {
                                inBtw.Add( key );
                            }
                        }

                        if ( inBtw.Count() <= 1 )
                        {
                            bool found = false;
                            Tuple< MacdSignal, double > currentBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barCurrent.BarIndex, out currentBarInfo ) )
                            {
                                if ( currentBarInfo.Item1 == MacdSignal.HigherHigh_MacdDowntrend )
                                {
                                    found = true;
                                }
                            }

                            Tuple< MacdSignal, double > previousBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barPrevious.BarIndex, out previousBarInfo ) )
                            {
                                if ( previousBarInfo.Item1 == MacdSignal.HigherHigh_MacdDowntrend )
                                {
                                    found = true;
                                }
                            }


                            if ( found )
                            {
                                p--;
                                continue;
                            }
                            else
                            {
                                ref SBar latestBar = ref Bars.GetBarByTime( latestBarTime );

                                if ( latestBar == SBar.EmptySBar ) break;

                                if ( barCurrent.BarIndex != latestBar.BarIndex )
                                {

                                }

                            }
                        }

                        // Here we have higher high in Price but Lower Low in MACD
                        if ( MacdLowerOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False )
                        {
                            macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                            foundDivergence = true;

                            var divergence = (higherHigh == DivergenceBoolean.CloselyEqual) ? TADivergence.NEGATIVE_DIVERGENCE_DOUBLE_TOP : TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH;

                            if ( ( int ) barCurrent.BarIndex > ithBar )
                            {
                                ithBar = ( int ) barCurrent.BarIndex;

                                lastSignal = divergence;
                            }

                            var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );

                            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                            if ( aa == null )
                            {
                                return;
                            }

                            aa.AddDivergenceSignal( ref barCurrent, div );
                        }

                        p--;
                    }
                }
                else
                {

                }


                j--;
            }

            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( macdDivergence );

                int lastKey = -1;

                foreach ( var pair in macdDivergence )
                {
                    if ( pair.Key == lastKey + 1 )
                    {

                    }

                }

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        protected void DetectShortTermHiddenDivergence( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {


            /* -------------------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  The following we are looking for normal divergence within a section of Red Dot to Red dot Zig Zag
            * 
            * -------------------------------------------------------------------------------------------------------------------------------------------------------
            */

            var redWaves = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT ).ToArray();
            var redWaveCount = redWaves.Count();





            ///// <image url="$(SolutionDir)\..\..\30 - CommonImages\HiddenDivergence2.png" scale="0.7"/>
            /* -------------------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  The following we are looking for hidden divergence within a section of Red Dot to Red dot Zig Zag
             *  
             *  In this case, we will need three points for reference, the last whole trend ( lastTrendBegin, lastTrendEnd ) and the current trend
             * 
             * -------------------------------------------------------------------------------------------------------------------------------------------------------
             */
            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( ( i + 1 ) < redWaveCount )
                {
                    var current     = redWaves[i + 1];
                    var previous    = redWaves[i];

                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }


                    // This is a local uptrend                    
                    if ( previous.Value.Signal == TASignal.WAVE_TROUGH && current.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        var innerWaves  = waveImportance.Where(x => x.Key >= previous.Key && x.Key < current.Key).OrderBy(x => x.Key).ToArray();
                        DetectHiddenDivergenceInLocalUptrend( symbol, period, taManager, previous, current, waveImportance, innerWaves, token );
                    }
                    // This is a local downtrend
                    else if ( previous.Value.Signal == TASignal.WAVE_PEAK && current.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        var innerWaves  = waveImportance.Where(x => x.Key >= previous.Key && x.Key < current.Key).OrderBy(x => x.Key).ToArray();
                        DetectHiddenDivergenceInLocalDowntrend( symbol, period, taManager, previous, current, waveImportance, innerWaves, token );
                    }
                }
            }
        }

        private void DetectLocalDivergenceInDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var barB4Calculation   = _barCountBeforeCalculation;
            var lastSignal         = TADivergence.NoDivergence;
            var ithBar             = -1;
            var foundDivergence    = false;
            var macdExtremumDict  = _macdExtremumDict;
            var macdDivergence     = _macdDivergence;

            var lowerLowTroughs    = GetLowerLowTroughsBtwXY_wLatest_OnlyCertainWaveImportance( period, waveImportance, latest, previous, GetWaveImportanceToDetect( period )  );
            var innerTroughsCount  = lowerLowTroughs.Count();
            int j                  = innerTroughsCount - 1;

            ref SBar debugBar = ref Bars.GetBarByTime( latest.Key );

            if ( debugBar == SBar.EmptySBar ) return;            

            if ( period == TimeSpan.FromDays( 1 ) && debugBar.BarIndex >= 2600 )
            {

            }

            while ( j > 0 )
            {
                var currentTrough  = lowerLowTroughs[j];
                var p              = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime(currentTrough.Key );

                if ( barCurrent == SBar.EmptySBar ) break;                
                
                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];

                if ( barCurrent.IsWaveTrough() || barCurrent.IsGannTrough() )
                {
                    while ( p >= 0 )
                    {
                        if ( token.IsCancellationRequested )
                        {
                            token.ThrowIfCancellationRequested();
                        }

                        var previousTrough  = lowerLowTroughs[p];

                        ref SBar barPrevious = ref Bars.GetBarByTime(previousTrough.Key );

                        if ( barPrevious == SBar.EmptySBar ) break;                        

                        var barPreviousMacd = IndicatorResult["MACD"][(int)barPrevious.BarIndex];
                        var lowerLow        = LocalLowerOrCloselyEqual(period, barCurrent.Low, barPrevious.Low);

                        if ( lowerLow == DivergenceBoolean.False )
                        {
                            break;
                        }

                        var macdSignificantCross   = _macdSignificantCross;
                        var macdCount              = macdSignificantCross.Count;

                        var inBtw = new PooledList< int >( );

                        for ( int i = 0; i < macdCount; i++ )
                        {
                            var key = macdSignificantCross.Keys[ i ];

                            if ( ( key >= ( int ) barPrevious.BarIndex ) && ( key < ( int ) barCurrent.BarIndex ) )
                            {
                                inBtw.Add( key );
                            }
                        }

                        if ( inBtw.Count() <= 1 )
                        {
                            bool found = false;
                            Tuple< MacdSignal, double > currentBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barCurrent.BarIndex, out currentBarInfo ) )
                            {
                                if ( currentBarInfo.Item1 == MacdSignal.LowerLow_MacdUptrend )
                                {
                                    found = true;
                                }
                            }

                            Tuple< MacdSignal, double > previousBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barPrevious.BarIndex, out previousBarInfo ) )
                            {
                                if ( previousBarInfo.Item1 == MacdSignal.LowerLow_MacdUptrend )
                                {
                                    found = true;
                                }
                            }

                            if ( found )
                            {
                                p--;
                                continue;
                            }
                            else
                            {
                                // if this is the most extreme right, which is the highest point of the current trend.

                                if ( barCurrent.BarIndex != latest.Value.BarIndex )
                                {

                                }
                            }
                        }

                        // Here we have lower low in Price but higher High in MACD

                        if ( MacdHigherOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False )
                        {
                            macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                            foundDivergence = true;

                            var divergence = (lowerLow == DivergenceBoolean.CloselyEqual) ? TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM : TADivergence.POSITIVE_DIVERGENCE_LOWER_LOW;

                            if ( ( int ) barCurrent.BarIndex > ithBar )
                            {
                                ithBar = ( int ) barCurrent.BarIndex;

                                lastSignal = divergence;
                            }

                            var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.Low, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.Low, barCurrent.BarTime );

                            var aa =( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                            if ( aa == null )
                            {
                                return;
                            }

                            aa.AddDivergenceSignal( ref barCurrent, div );

                            break;
                        }

                        p--;
                    }
                }

                j--;
            }

            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectLocalDivergenceInUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;
            var macdDivergence           = _macdDivergence;


            var higherHighTops           = GetHigherHighPeaksBtwXY_wLatest_OnlyCertainWaveImportance( period, waveImportance, latest, previous, GetWaveImportanceToDetect( period ) );
            var innerPeaksCount          = higherHighTops.Count();
            int j                        = innerPeaksCount - 1;

            while ( j > 0 )
            {
                var currentTop      = higherHighTops[j];
                var p               = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime( currentTop.Key );

                if ( barCurrent == SBar.EmptySBar ) break;                

                if ( barCurrent.IsWavePeak() || barCurrent.IsGannPeak() )
                {
                    while ( p >= 0 )
                    {
                        if ( token.IsCancellationRequested )
                        {
                            token.ThrowIfCancellationRequested();
                        }

                        var previousTop = higherHighTops[p];

                        ref SBar barPrevious = ref Bars.GetBarByTime( previousTop.Key );

                        if ( barPrevious == SBar.EmptySBar ) break;                        

                        var higherHigh  = LocalHigherOrCloselyEqual(period, barCurrent.High, barPrevious.High);

                        if ( higherHigh == DivergenceBoolean.False )
                        {
                            break;
                        }

                        if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex == 8849 )
                        {

                        }

                        var macdSignificantCross   = _macdSignificantCross;
                        var macdCount              = macdSignificantCross.Count;

                        var inBtw = new PooledList< int >( );

                        for ( int i = 0; i < macdCount; i++ )
                        {
                            var key = macdSignificantCross.Keys[ i ];

                            if ( ( key >= ( int ) barPrevious.BarIndex ) && ( key < ( int ) barCurrent.BarIndex ) )
                            {
                                inBtw.Add( key );
                            }
                        }

                        if ( inBtw.Count() <= 1 )
                        {
                            bool found = false;
                            Tuple< MacdSignal, double > currentBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barCurrent.BarIndex, out currentBarInfo ) )
                            {
                                if ( currentBarInfo.Item1 == MacdSignal.HigherHigh_MacdDowntrend )
                                {
                                    found = true;
                                }
                            }

                            Tuple< MacdSignal, double > previousBarInfo = null;

                            if ( macdExtremumDict.TryGetValue( ( int ) barPrevious.BarIndex, out previousBarInfo ) )
                            {
                                if ( previousBarInfo.Item1 == MacdSignal.HigherHigh_MacdDowntrend )
                                {
                                    found = true;
                                }
                            }

                            if ( found )
                            {
                                p--;
                                continue;
                            }
                            else
                            {
                                /// <image url="$(SolutionDir)\..\..\30 - CommonImages\MacdWork.png" /> 
                                // if this is the most extreme right, which is the highest point of the current trend.

                                if ( barCurrent.BarIndex != latest.Value.BarIndex )
                                {

                                }

                            }
                        }

                        // Here we have higher high in Price but Lower Low in MACD
                        if ( MacdLowerOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False )
                        {
                            macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                            foundDivergence = true;

                            var divergence = (higherHigh == DivergenceBoolean.CloselyEqual) ? TADivergence.NEGATIVE_DIVERGENCE_DOUBLE_TOP : TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH;

                            if ( ( int ) barCurrent.BarIndex > ithBar )
                            {
                                ithBar = ( int ) barCurrent.BarIndex;

                                lastSignal = divergence;
                            }

                            var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );
                            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                            if ( aa == null )
                            {
                                return;
                            }

                            aa.AddDivergenceSignal( ref barCurrent, div );
                        }

                        p--;
                    }
                }


                j--;
            }

            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectHiddenDivergenceInMajorDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> current, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance>[ ] innerWaves, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = innerWaves.Where(x =>
                                                                    {
                                                                        if (x.Value.Signal == TASignal.WAVE_PEAK)
                                                                        {
                                                                            ref SBar bar = ref Bars.GetBarByTime(x.Key );

                                                                            if ( bar == SBar.EmptySBar ) return false;                                                                            
                                                                           
                                                                            if (extremumsValueDictionary.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }

                                                                            if (macdExtremumDict.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }
                                                                        }
                                                                        return false;
                                                                    }
                                                                );



            var nonSmoothedinnerTurningPoints = nonSmoothedInnerTurningPts.ToList();

            // This need to include the first bottom. If not, then the bottom is just a spike so the macd doesn't cross over
            if ( !nonSmoothedinnerTurningPoints.Contains( previous ) )
            {
                nonSmoothedinnerTurningPoints.Insert( 0, previous );
            }

            var innerPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in nonSmoothedinnerTurningPoints )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar == SBar.EmptySBar ) break;

                var myMacd = IndicatorResult["MACD"][(int) bar.BarIndex];

                if ( myMacd > 0 )
                {
                    innerPeaks.Add( turningPoint );
                }
            }

            var innerPeaksCount = innerPeaks.Count();
            int j               = innerPeaksCount - 1;

            while ( j >= 0 )
            {
                var currentPeak    = innerPeaks[j];

                if ( currentPeak.Key < _lastDetectHiddenDivergenceInMajorDowntrendCheckTime )
                {
                    // Here we don't want to check those that we have check before.
                    break;
                }

                var tr             = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime( currentPeak.Key );

                if ( barCurrent == SBar.EmptySBar )                    
                {
                    break;
                }

                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];

                if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex >= 8835 && j == 1 )
                {

                }

                var highestHigh = barCurrent.High;

                while ( tr >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var previousPeak = innerPeaks[tr];

                    ref SBar barPrevious = ref Bars.GetBarByTime( previousPeak.Key );

                    if ( barPrevious == SBar.EmptySBar )                    
                    {
                        tr--;
                        continue;
                    }

                    if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex >= 8836 && j == 1 )
                    {

                    }

                    // 00 - Here we have detected Lower Top
                    if ( LocalLowerOrCloselyEqual( period, highestHigh, barPrevious.High ) == DivergenceBoolean.False )
                    {
                        tr--;
                        continue;
                    }


                    // 01 - Now we want to detect that the MACD is rising much stronger
                    if ( NoHigherMacdPeaksInBetween( ref barPrevious, ref barCurrent ) )
                    {
                        if ( MacdHigherOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False )
                        {
                            _macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                            foundDivergence = true;

                            var divergence = ( barCurrent.High < barPrevious.High ) ? TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW : TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP;

                            if ( ( int ) barCurrent.BarIndex > ithBar )
                            {
                                ithBar = ( int ) barCurrent.BarIndex;

                                lastSignal = divergence;
                            }

                            var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );
                            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                            if ( aa == null )
                            {
                                return;
                            }

                            aa.AddDivergenceSignal( ref barCurrent, div );
                        }

                        highestHigh = barPrevious.High;

                        if ( previousPeak.Value.WaveImportance > currentPeak.Value.WaveImportance )
                        {
                            // Here we are not going to search backward, as the previous low is more important turning point than me
                            break;
                        }

                    }

                    tr--;
                }
                j--;
            }

            if ( innerPeaks.Count - 1 >= 0 )
            {
                _lastDetectHiddenDivergenceInMajorDowntrendCheckTime = innerPeaks[ innerPeaks.Count - 1 ].Key;
            }

            // Find divergence without major turning points
            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( _macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectHiddenDivergenceInLocalDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> current, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance>[ ] innerWaves, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;



            var nonSmoothedInnerTurningPts = innerWaves.Where(x =>
                                                                    {
                                                                        if (x.Value.Signal == TASignal.WAVE_PEAK)
                                                                        {
                                                                            ref SBar bar = ref Bars.GetBarByTime( x.Key );

                                                                            if ( bar == SBar.EmptySBar )                                                                                
                                                                                return false;

                                                                             
                                                                            if (extremumsValueDictionary.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }

                                                                            if (macdExtremumDict.ContainsKey((int)bar.BarIndex))
                                                                            {
                                                                                return true;
                                                                            }
                                                                        }
                                                                        return false;
                                                                    }
                                                                );



            var nonSmoothedinnerTurningPoints = nonSmoothedInnerTurningPts.ToList();

            // This need to include the first bottom. If not, then the bottom is just a spike so the macd doesn't cross over
            if ( !nonSmoothedinnerTurningPoints.Contains( previous ) )
            {
                nonSmoothedinnerTurningPoints.Insert( 0, previous );
            }

            var innerPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in nonSmoothedinnerTurningPoints )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar == SBar.EmptySBar ) break;                

                var myMacd = IndicatorResult["MACD"][(int) bar.BarIndex];

                if ( myMacd > 0 )
                {
                    innerPeaks.Add( turningPoint );
                }
            }

            var innerPeaksCount = innerPeaks.Count();
            int j               = innerPeaksCount - 1;

            while ( j >= 0 )
            {
                var currentPeak    = innerPeaks[j];
                var tr             = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime(currentPeak.Key );
                if ( barCurrent == SBar.EmptySBar ) break;

                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];

                if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex >= 8835 && j == 1 )
                {

                }

                while ( tr >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var previousPeak = innerPeaks[tr];
                    ref SBar barPrevious = ref Bars.GetBarByTime( previousPeak.Key );

                    if ( barPrevious == SBar.EmptySBar )
                    {
                        tr--;
                        continue;
                    }

                    if ( period == TimeSpan.FromMinutes( 5 ) && barCurrent.BarIndex >= 8836 && j == 1 )
                    {

                    }

                    // 00 - Here we have detected Lower Top
                    if ( LocalLowerOrCloselyEqual( period, barCurrent.High, barPrevious.High ) == DivergenceBoolean.False )
                    {
                        tr--;
                        continue;
                    }


                    // 01 - Now we want to detect that the MACD is rising much stronger
                    if ( ( MacdHigherOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False ) && NoHigherMacdPeaksInBetween( ref barPrevious, ref barCurrent ) )
                    {
                        _macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                        foundDivergence = true;

                        var divergence = (barCurrent.High < barPrevious.High) ? TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW : TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP;

                        if ( ( int ) barCurrent.BarIndex > ithBar )
                        {
                            ithBar = ( int ) barCurrent.BarIndex;

                            lastSignal = divergence;
                        }

                        var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );
                        var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                        if ( aa == null )
                        {
                            return;
                        }

                        aa.AddDivergenceSignal( ref barCurrent, div );
                    }

                    tr--;
                }
                j--;
            }



            // Find divergence without major turning points
            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( _macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectHiddenDivergenceInLocalUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> current, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance>[ ] innerWaves, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict         = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = innerWaves.Where(x =>
            {
                if (x.Value.Signal == TASignal.WAVE_TROUGH)
                {
                    ref SBar bar = ref Bars.GetBarByTime( x.Key );

                    if ( bar == SBar.EmptySBar )
                        return false;                    

                    if (macdExtremumDict.ContainsKey((int)bar.BarIndex))
                    {
                        return true;
                    }
                }
                return false;
            }
                                                                );


            var nonSmoothedinnerTurningPoints = nonSmoothedInnerTurningPts.ToList();

            // This need to include the first bottom. If not, then the bottom is just a spike so the macd doesn't cross over
            if ( !nonSmoothedinnerTurningPoints.Contains( previous ) )
            {
                nonSmoothedinnerTurningPoints.Insert( 0, previous );
            }

            var innerTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in nonSmoothedinnerTurningPoints )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar == SBar.EmptySBar ) break;

                var myMacd = IndicatorResult["MACD"][(int) bar.BarIndex];

                if ( myMacd < 0 )
                {
                    innerTroughs.Add( turningPoint );
                }
            }

            ref SBar currentBar = ref Bars.GetBarByTime( current.Key ); 
            ref SBar previousBar = ref Bars.GetBarByTime( previous.Key ); 

            if ( currentBar == SBar.EmptySBar || previousBar == SBar.EmptySBar )
            {
                return;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) && currentBar.BarIndex >= 8845 )
            {

            }

            var innerTroughsCount = innerTroughs.Count();
            int j                 = innerTroughsCount - 1;

            while ( j > 0 )
            {
                var currentTrough = innerTroughs[j];

                var tr = j - 1;

                if ( period == TimeSpan.FromMinutes( 5 ) && j == 1 && currentBar.BarIndex >= 8845 )
                {

                }

                ref SBar barCurrent = ref Bars.GetBarByTime(  currentTrough.Key );

                if ( barCurrent == SBar.EmptySBar )
                {
                    j--;
                    continue;
                }

                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];

                while ( tr >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var previousTrough = innerTroughs[tr];
                    ref SBar barPrevious = ref Bars.GetBarByTime(  previousTrough.Key );

                    if ( barPrevious == SBar.EmptySBar )
                    {
                        tr--;
                        continue;
                    }

                    // 00 - Here we have detected higher bottom
                    if ( LocalHigherOrCloselyEqual( period, barCurrent.Low, barPrevious.Low ) == DivergenceBoolean.False )
                    {
                        tr--;
                        continue;
                    }

                    if ( period == TimeSpan.FromMinutes( 5 ) && j == 1 && currentBar.BarIndex >= 7440 )
                    {

                    }


                    // 01 - Now we want to detect that the MACD is falling much stronger
                    if ( ( MacdLowerOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False ) && NoLowerMacdTroughsInBetween( ref barPrevious, ref barCurrent ) )
                    {
                        _macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;

                        foundDivergence = true;

                        var divergence = (barCurrent.Low < barPrevious.Low) ? TADivergence.HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH : TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM;

                        if ( ( int ) barCurrent.BarIndex > ithBar )
                        {
                            ithBar = ( int ) barCurrent.BarIndex;

                            lastSignal = divergence;
                        }

                        var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );
                        var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                        if ( aa == null )
                        {
                            return;
                        }

                        aa.AddDivergenceSignal( ref barCurrent, div );
                    }

                    tr--;

                }

                j--;
            }

            // Find divergence without major turning points
            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( _macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        private void DetectHiddenDivergenceInMajorUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> current, BTreeDictionary<long, WavePointImportance> waveImportance, KeyValuePair<long, WavePointImportance>[ ] innerWaves, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;

            var nonSmoothedInnerTurningPts = innerWaves.Where(x =>
            {
                if (x.Value.Signal == TASignal.WAVE_TROUGH)
                {
                    ref SBar bar = ref Bars.GetBarByTime( x.Key );

                    if ( bar == SBar.EmptySBar  )
                        return false;

                    //if (extremumsValueDictionary.ContainsKey((int)bar.BarIndex))
                    //{
                    //    return true;
                    //}

                    if (macdExtremumDict.ContainsKey((int)bar.BarIndex))
                    {
                        return true;
                    }
                }
                return false;
            }
                                                                );


            var nonSmoothedinnerTurningPoints = nonSmoothedInnerTurningPts.ToList();

            // This need to include the first bottom. If not, then the bottom is just a spike so the macd doesn't cross over
            if ( !nonSmoothedinnerTurningPoints.Contains( previous ) )
            {
                nonSmoothedinnerTurningPoints.Insert( 0, previous );
            }

            var innerTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            foreach ( KeyValuePair<long, WavePointImportance> turningPoint in nonSmoothedinnerTurningPoints )
            {
                ref SBar bar = ref Bars.GetBarByTime(turningPoint.Key );

                if ( bar == SBar.EmptySBar ) break;

                var myMacd = IndicatorResult["MACD"][(int) bar.BarIndex];

                if ( myMacd < 0 )
                {
                    innerTroughs.Add( turningPoint );
                }
            }

            ref SBar currentBar = ref   
            Bars.GetBarByTime( current.Key );

            ref SBar previousBar =  ref  
            Bars.GetBarByTime( previous.Key );

            var innerTroughsCount = innerTroughs.Count();
            int j                 = innerTroughsCount - 1;

            while ( j > 0 )
            {
                var currentTrough = innerTroughs[j];

                if ( currentTrough.Key < _lastDetectHiddenDivergenceInMajorUptrendCheckTime )
                {
                    // Here we don't want to check those that we have check before.
                    break;
                }

                var tr = j - 1;

                ref SBar barCurrent = ref Bars.GetBarByTime(  currentTrough.Key );

                if ( barCurrent == SBar.EmptySBar ) break;

                var barCurrentMacd = IndicatorResult["MACD"][(int)barCurrent.BarIndex];
                
                var lowestLow      = barCurrent.Low;

                while ( tr >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var previousTrough = innerTroughs[tr];
                    ref SBar barPrevious   = ref Bars.GetBarByTime(  previousTrough.Key );

                    if ( barPrevious == SBar.EmptySBar )
                    {
                        tr--;
                        continue;
                    }

                    // 00 - Here we have detected higher bottom, if higher, we ignore them. We want to search for lower Low.
                    if ( LocalHigherOrCloselyEqual( period, lowestLow, barPrevious.Low ) == DivergenceBoolean.False )
                    {
                        tr--;
                        continue;
                    }

                    // 01 - Now we want to detect that the MACD is falling much stronger
                    if ( NoLowerMacdTroughsInBetween( ref barPrevious, ref barCurrent ) )
                    {
                        if ( ( MacdLowerOrCloselyEqual( period, ref barCurrent, ref barPrevious ) != DivergenceBoolean.False ) )
                        {
                            _macdDivergence.GetOrAddValueRef( ( int ) barCurrent.BarIndex ) = TASignal.HAS_DIVERGENCE;
                            foundDivergence = true;

                            var divergence = ( barCurrent.Low < barPrevious.Low ) ? TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH : TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM;

                            if ( ( int ) barCurrent.BarIndex > ithBar )
                            {
                                ithBar = ( int ) barCurrent.BarIndex;

                                lastSignal = divergence;
                            }

                            var div = new DivergenceEventArgs( symbol.Code, period, divergence, barPrevious.BarIndex, barPrevious.High, barPrevious.BarTime, barCurrent.BarIndex, barCurrent.High, barCurrent.BarTime );
                            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                            if ( aa == null )
                            {
                                return;
                            }

                            aa.AddDivergenceSignal( ref barCurrent, div );
                        }

                        lowestLow = barPrevious.Low;

                        if ( previousTrough.Value.WaveImportance > currentTrough.Value.WaveImportance )
                        {
                            // Here we are not going to search backward, as the previous low is more important turning point than me
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                    tr--;

                }

                j--;
            }

            if ( innerTroughs.Count - 1 >= 0 )
            {
                _lastDetectHiddenDivergenceInMajorUptrendCheckTime = innerTroughs[ innerTroughs.Count - 1 ].Key;
            }


            // Find divergence without major turning points
            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( _macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }




        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DivergenceMoreWork2.png" />
        protected void DetectLongTermR2RDivergence( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var waves                    = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT ).ToArray();

            var peakTroughCount          = waves.Count();

            var barB4Calculation         = _barCountBeforeCalculation;
            var lastSignal               = TADivergence.NoDivergence;
            var ithBar                   = -1;
            var foundDivergence          = false;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdDivergence           = _macdDivergence;

            // Find major turning points divergence
            for ( int i = peakTroughCount - 1; i >= 0; i-- )
            {
                if ( ( i - 2 ) >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var first  = waves[ i ];
                    var second = waves[ i - 1 ];
                    var third  = waves[ i - 2 ];

                    ref SBar bar1st = ref Bars.GetBarByTime(  first.Key );
                    ref SBar bar2nd = ref Bars.GetBarByTime(  second.Key );
                    ref SBar bar3rd = ref Bars.GetBarByTime(  third.Key );


                    if ( bar1st == SBar.EmptySBar ||
                         bar2nd == SBar.EmptySBar ||
                         bar3rd == SBar.EmptySBar )
                    {
                        continue;
                    }

                    if ( period == TimeSpan.FromMinutes( 5 ) && bar1st.BarIndex >= 9270 )
                    {

                    }

                    if ( first.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        if ( bar1st.BarIndex >= _lastMacdCrossDownIndex ) continue;

                        // Here we have a down trend
                        var higherHigh = HigherOrCloselyEqual(period, bar1st.High, bar3rd.High);

                        if ( ( higherHigh != DivergenceBoolean.False ) /*&& ( bar2nd.Low > bar4th.Low )*/ )
                        {
                            if ( MacdLowerOrCloselyEqual( period, ref bar1st, ref bar3rd ) != DivergenceBoolean.False )
                            {
                                macdDivergence.GetOrAddValueRef( ( int ) bar1st.BarIndex ) = TASignal.HAS_DIVERGENCE;

                                foundDivergence = true;
                                var divergence  = (higherHigh == DivergenceBoolean.CloselyEqual) ? TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_DOUBLE_TOP : TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH;

                                if ( ( int ) bar1st.BarIndex > ithBar )
                                {
                                    ithBar = ( int ) bar1st.BarIndex;

                                    lastSignal = divergence;
                                }

                                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                                if ( aa != null )
                                {
                                    var div = new DivergenceEventArgs( symbol.Code, period, divergence, bar1st.BarIndex, bar1st.High, bar1st.BarTime, bar3rd.BarIndex, bar3rd.High, bar3rd.BarTime );
                                    aa.AddDivergenceSignal( ref bar1st, div );
                                }

                            }
                        }
                    }
                    else if ( first.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        if ( bar1st.BarIndex >= _lastMacdCrossUpIndex ) continue;

                        var lowerLow = LowerOrCloselyEqual(period, bar1st.Low, bar3rd.Low);

                        if ( ( lowerLow != DivergenceBoolean.False ) /*&& ( bar2nd.High < bar4th.High ) */ )
                        {
                            //if ( bar1stMacd > bar3rdMacd )
                            if ( MacdHigherOrCloselyEqual( period, ref bar1st, ref bar3rd ) != DivergenceBoolean.False )
                            {
                                macdDivergence.GetOrAddValueRef( ( int ) bar1st.BarIndex ) = TASignal.HAS_DIVERGENCE;

                                foundDivergence = true;

                                var divergence  = (lowerLow == DivergenceBoolean.CloselyEqual) ? TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM : TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW;

                                if ( ( int ) bar1st.BarIndex > ithBar )
                                {
                                    ithBar = ( int ) bar1st.BarIndex;

                                    lastSignal = divergence;
                                }
                                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                                if ( aa != null )
                                {
                                    var div = new DivergenceEventArgs( symbol.Code, period, divergence, bar1st.BarIndex, bar1st.Low, bar1st.BarTime, bar3rd.BarIndex, bar3rd.Low, bar3rd.BarTime );
                                    aa.AddDivergenceSignal( ref bar1st, div );
                                }
                            }
                        }
                    }

                }
            }

            if ( foundDivergence )
            {
                Bars.AddSignalsToDataBar( macdDivergence );

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddDivergence( period, ithBar, lastSignal, barB4Calculation );
            }
        }

        protected void DetectShortTermLocalDivergence( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var barB4Calculation         = _barCountBeforeCalculation;
            var extremumsValueDictionary = _extremumsValueDictionary;
            var macdExtremumDict        = _macdExtremumDict;
            var macdDivergence           = _macdDivergence;


            /* -------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  The following we are looking for normal divergence within a section of Red Dot to Red dot Zig Zag
                 * 
                 * -------------------------------------------------------------------------------------------------------------------------------------------------------
                 */

            var redWaves     = waveImportance.Where( x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT && ( x.Key > _last3rdRedTime  ) ).ToArray();
            var redWaveCount = redWaves.Count();


            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( ( i + 1 ) < redWaveCount )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var latest    = redWaves[ i + 1 ];
                    var previous  = redWaves[ i ];

                    ref SBar barLatest = ref  
                    Bars.GetBarByTime(  latest.Key );

                    if ( barLatest.Index < _lastMacdCrossIndex )
                    {
                        // This is a local uptrend
                        if ( latest.Value.Signal == TASignal.WAVE_PEAK && previous.Value.Signal == TASignal.WAVE_TROUGH )
                        {
                            DetectLocalDivergenceInUptrend( symbol, period, taManager, previous, latest, waveImportance, token );
                        }
                        // This is a local downtrend
                        else if ( latest.Value.Signal == TASignal.WAVE_TROUGH && previous.Value.Signal == TASignal.WAVE_PEAK )
                        {
                            DetectLocalDivergenceInDowntrend( symbol, period, taManager, previous, latest, waveImportance, token );
                        }
                    }
                }
            }
        }

        private void DetectLongTermR2RHiddenDivergence( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var redWaves     = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT ).ToArray();

            //var redWave2 = waveImportance.Where( x =>
            //                                        {
            //                                            if ( x.Value.WaveImportance >= 89 )
            //                                            {
            //                                                ref SBar bar = default; DatabarsRepo.GetBarByTime( x.Key, ref bar );

            //                                                if ( bar.Index > _lastCandleCheckIndex )
            //                                                {
            //                                                    return true;
            //                                                }
            //                                            }

            //                                            return false;
            //                                        }
            //                                    ).ToArray( );


            var redWaveCount = redWaves.Count();

            // Find major turning points divergence
            for ( int i = redWaveCount - 1; i > 0; i-- )
            {
                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested();
                }

                var requiredWaveImportance = GetWaveImportanceByTime( period );

                var current     = redWaves[ i ];
                var previous    = redWaves[ 0 ];
                var innerWaves  = waveImportance.Where(x => x.Value.WaveImportance >= requiredWaveImportance && x.Key <= current.Key).OrderBy(x => x.Key).ToArray();

                ref SBar bar = ref  
                Bars.GetBarByTime(  current.Key );

                if ( bar.BarIndex >= _lastMacdCrossIndex ) continue;



                if ( current.Value.Signal == TASignal.WAVE_PEAK )
                {
                    DetectHiddenDivergenceInMajorDowntrend( symbol, period, taManager, previous, current, waveImportance, innerWaves, token );
                }
                else if ( current.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    DetectHiddenDivergenceInMajorUptrend( symbol, period, taManager, previous, current, waveImportance, innerWaves, token );
                }
            }
        }

        private int GetWaveImportanceByTime( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 89;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return 89;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 55;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 55;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 55;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return 34;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return 34;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return 34;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return 21;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return 21;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return 21;
            }

            return 89;
        }


        private int GetWaveImportanceToDetect( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 13;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return 13;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 13;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 8;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 8;
            }


            return 5;
        }

        public DivergenceBoolean HigherOrCloselyEqual( TimeSpan period, double bar1High, double bar3High )
        {
            if ( bar1High >= bar3High )
            {
                return DivergenceBoolean.True;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                if ( bar1High >= bar3High - 0.0005 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromMinutes( 15 ) )
            {
                if ( bar1High >= bar3High - 0.001 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                if ( bar1High >= bar3High - 0.0015 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }

            return DivergenceBoolean.False;
        }


        public DivergenceBoolean LocalHigherOrCloselyEqual( TimeSpan period, double bar1High, double bar3High )
        {
            if ( bar1High >= bar3High )
            {
                return DivergenceBoolean.True;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                if ( bar1High >= bar3High - 0.0002 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromMinutes( 15 ) )
            {
                if ( bar1High >= bar3High - 0.0004 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                if ( bar1High >= bar3High - 0.0006 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }

            return DivergenceBoolean.False;
        }


        public DivergenceBoolean LowerOrCloselyEqual( TimeSpan period, double bar1Low, double bar3Low )
        {
            if ( bar1Low <= bar3Low )
            {
                return DivergenceBoolean.True;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                if ( bar1Low <= bar3Low + 0.0005 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromMinutes( 15 ) )
            {
                if ( bar1Low <= bar3Low + 0.001 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                if ( bar1Low <= bar3Low + 0.0015 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }

            return DivergenceBoolean.False;
        }



        public DivergenceBoolean MacdHigherOrCloselyEqual( TimeSpan period, ref SBar barCurrent, ref SBar barPrevious )
        {
            var currentBarMacd = GetClosestTroughMacd( ref barCurrent );
            var previousBarMacd = GetClosestTroughMacd( ref barPrevious );

            return InternalMacdHigherOrCloselyEqual( period, currentBarMacd, previousBarMacd );
        }

        public DivergenceBoolean InternalMacdHigherOrCloselyEqual( TimeSpan period, double currentBarMacd, double previousBarMacd )
        {
            if ( currentBarMacd > previousBarMacd )
            {
                return DivergenceBoolean.True;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                if ( currentBarMacd >= previousBarMacd - 0.00002 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromMinutes( 15 ) )
            {
                if ( currentBarMacd >= previousBarMacd - 0.00004 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                if ( currentBarMacd >= previousBarMacd - 0.00006 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }

            return DivergenceBoolean.False;
        }

        public bool NoLowerMacdTroughsInBetween( ref SBar barCurrent, ref SBar barNext )
        {
            var currentBarMacd = GetClosestTroughMacd( ref barCurrent );
            var nextBarMacd    = GetClosestTroughMacd( ref barNext );

            var minimum        = Math.Min( currentBarMacd, nextBarMacd );

            var macd           = IndicatorResult [ "MACD" ];

            for ( int i = ( int ) barCurrent.BarIndex; i < ( int ) barNext.BarIndex; i++ )
            {
                if ( macd[ i ] < minimum )
                {
                    var percentage = ( ( macd [ i ] - minimum ) / macd [ i ] ) * 100;

                    if ( percentage > 2 )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool NoHigherMacdPeaksInBetween( ref SBar barCurrent, ref SBar barNext )
        {
            var currentBarMacd = GetClosestPeakMacd( ref barCurrent );
            var nextBarMacd    = GetClosestPeakMacd( ref barNext );

            var maximum        = Math.Max( currentBarMacd, nextBarMacd );

            var macd           = IndicatorResult [ "MACD" ];

            for ( int i = ( int ) barCurrent.BarIndex; i < ( int ) barNext.BarIndex; i++ )
            {
                if ( macd[ i ] > maximum )
                {
                    var percentage = ( ( macd [ i ] - maximum ) / macd [ i ] ) * 100;

                    if ( percentage > 2 )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public double GetClosestTroughMacd( ref SBar bar )
        {
            var currentBarMacd = IndicatorResult [ "MACD" ] [ ( int ) bar.BarIndex ];

            int barIndex = -1;

            Tuple< MacdSignal, double > tuple = null;

            if ( _macdExtremumDict.TryGetValue( ( int ) bar.BarIndex, out tuple ) )
            {
                if ( tuple.Item1 == MacdSignal.LowestPoint_MacdDowntrend )
                {
                    currentBarMacd = tuple.Item2;
                }
            }
            else
            {
                int count = _macdExtremumDict.Count;

                for ( int i = 0; i < count; i++ )
                {
                    if ( _macdExtremumDict.Keys[ i ] > ( int ) bar.BarIndex )
                    {
                        barIndex = i;

                        if ( barIndex < _macdExtremumDict.Count )
                        {
                            var newTuple = _macdExtremumDict.Values.ElementAt( barIndex );

                            if ( newTuple.Item1 == MacdSignal.LowestPoint_MacdDowntrend )
                            {
                                return ( newTuple.Item2 );
                            }
                        }

                        break;
                    }
                }

                //barIndex = _macdExtremumDict.FirstIndexWhereGreaterThan( ( int ) bar.BarIndex );

                //if ( barIndex < _macdExtremumDict.Count )
                //{
                //    var tuple = _macdExtremumDict.Values.ElementAt( barIndex );

                //    if ( tuple.Item1 == MacdSignal.LowestPoint_MacdDowntrend )
                //    {
                //        currentBarMacd = tuple.Item2;
                //    }                    
                //}
            }

            return currentBarMacd;
        }

        public double GetClosestPeakMacd( ref SBar bar )
        {
            var currentBarMacd = IndicatorResult [ "MACD" ] [ ( int ) bar.BarIndex ];

            int barIndex = -1;

            Tuple< MacdSignal, double > tuple = null;

            if ( _macdExtremumDict.TryGetValue( ( int ) bar.BarIndex, out tuple ) )
            {
                if ( tuple.Item1 == MacdSignal.HighestPoint_MacdUptrend )
                {
                    currentBarMacd = tuple.Item2;
                }
            }
            else
            {
                int count = _macdExtremumDict.Count;

                for ( int i = 0; i < count; i++ )
                {
                    if ( _macdExtremumDict.Keys[ i ] > ( int ) bar.BarIndex )
                    {
                        barIndex = i;

                        if ( barIndex < _macdExtremumDict.Count )
                        {
                            var newTuple = _macdExtremumDict.Values.ElementAt( barIndex );

                            if ( newTuple.Item1 == MacdSignal.HighestPoint_MacdUptrend )
                            {
                                return ( newTuple.Item2 );


                            }
                        }

                        break;
                    }
                }


            }


            return currentBarMacd;
        }

        public DivergenceBoolean MacdLowerOrCloselyEqual( TimeSpan period, ref SBar barCurrent, ref SBar barPrevious )
        {
            var currentBarMacd = GetClosestPeakMacd( ref barCurrent );
            var previousBarMacd = GetClosestPeakMacd( ref barPrevious );

            if ( currentBarMacd < previousBarMacd )
            {
                return DivergenceBoolean.True;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                if ( currentBarMacd <= previousBarMacd + 0.00002 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromMinutes( 15 ) && period <= TimeSpan.FromHours( 1 ) )
            {
                if ( currentBarMacd <= previousBarMacd + 0.00004 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                if ( currentBarMacd <= previousBarMacd + 0.00006 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }

            return DivergenceBoolean.False;
        }

        public DivergenceBoolean LocalLowerOrCloselyEqual( TimeSpan period, double currentBarLow, double previousBarLow )
        {
            if ( currentBarLow <= previousBarLow )
            {
                return DivergenceBoolean.True;
            }

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                if ( currentBarLow <= previousBarLow + 0.0002 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromMinutes( 15 ) )
            {
                if ( currentBarLow <= previousBarLow + 0.0004 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                if ( currentBarLow <= previousBarLow + 0.0006 )
                {
                    return DivergenceBoolean.CloselyEqual;
                }
            }

            return DivergenceBoolean.False;
        }

        public bool BuildMacdCrossExtremumDictionary( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return false;
            }

            var macdCrossDictionary      = _macdSignificantCross;
            var extremumsValueDictionary = _extremumsValueDictionary;

            if ( macdCrossDictionary.Count == 0 )
            {
                return false;
            }

            double extremum = 0;
            int extremumIndex = -1;

            if ( _indexOfLastMaxValuesBtMacdPoints < 1 )
            {
                _indexOfLastMaxValuesBtMacdPoints = 1;
            }

            for ( int i = _indexOfLastMaxValuesBtMacdPoints; i < macdCrossDictionary.Count; i++ )
            {
                var macdValuei_1 = macdCrossDictionary.ElementAt( i - 1 );
                var macdValuei   = macdCrossDictionary.ElementAt( i );

                extremum = FindExtremumBetweenPoints( macdValuei_1.Key, macdValuei.Key, ( macdValuei_1.Value == TASignal.HAS_BOTTOMING_SIGNAL ? true : false ), ref extremumIndex );

                if ( extremum > -1 )
                {
                    extremumsValueDictionary.GetOrAddValueRef( extremumIndex ) = new Tuple<int, double, double>( macdValuei.Key, extremum, IndicatorResult[ "MACD" ][ extremumIndex ] );


                    _indexOfLastMaxValuesBtMacdPoints = i;
                }
            }

            return true;
        }

    }
}
