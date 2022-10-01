using DevExpress.Mvvm;
using fx.Algorithm;
using fx.Bars;
using fx.Collections;
using fx.Definitions;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {


        public PooledList<double> GetSelectedLinesForSoundAlert()
        {
            return ( _chartVM.GetSelectedLinesForSoundAlert() );
        }






        public override void AddWaveOneToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar == SBar.EmptySBar ) return;


            var finalWaveCycle = waveCycle;

            if ( /*_chartVM.IsSpecialBar ||*/ _hews.IsSpecialBar( _waveScenarioNumber, selectedBarTime ) || bar.IsSpecialBar )
            {
                bool isOutsideBar = _bars.IsOutsideBar( selectedBarTime );

                _hews?.AddIntelliWave( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar, true, _bars.IsOutsideBar( selectedBarTime ) );
            }

            else
            {
                var allWave = _hews.GetAllWavesDescending( _waveScenarioNumber, selectedBarTime );

                if ( allWave != null && allWave.Count > 0 )
                {
                    AddToExistingWave( waveScenarioNo, selectedBarTime, waveCycle, elliottWave );
                }
                else
                {
                    var outputWaveCycle = _hews?.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar );

                    if ( outputWaveCycle.HasValue )
                    {
                        finalWaveCycle = outputWaveCycle.Value;
                    }
                }

                long beginningTime = _hews.FindBeginningWaveTimeOfCurrentCycle( _waveScenarioNumber, selectedBarTime, finalWaveCycle );

                _hews.SmartABC_ToWave += ( waveScenarioNo, ResponsibleTF, beginningTime, selectedBarTime, finalWaveCycle );
            }

        }

        public override void AddWaveTwoToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
            var desiredCycle = waveCycle;

            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar == SBar.EmptySBar ) return;






            if ( _chartVM.IsSpecialBar || _hews.IsSpecialBar( _waveScenarioNumber, selectedBarTime ) || bar.IsSpecialBar )
            {
                bool isOutsideBar = _bars.IsOutsideBar( selectedBarTime );

                _hews.AddIntelliWave( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar, true, _bars.IsOutsideBar( selectedBarTime ) );
            }

            else
            {
                var allWave = _hews.GetAllWavesDescending( _waveScenarioNumber, selectedBarTime );

                if ( allWave != null && allWave.Count > 0 )
                {
                    desiredCycle = AddToExistingWave( waveScenarioNo, selectedBarTime, waveCycle, elliottWave );
                }
                else
                {
                    desiredCycle = _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar );
                }
            }


            Refresh();
        }

        public override void AddWaveToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar == SBar.EmptySBar ) return;



            if ( _chartVM.IsSpecialBar || _hews.IsSpecialBar( _waveScenarioNumber, selectedBarTime ) || bar.IsSpecialBar )
            {
                bool isOutsideBar = _bars.IsOutsideBar( selectedBarTime );

                _hews?.AddIntelliWave( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar, true, _bars.IsOutsideBar( selectedBarTime ) );
            }

            else
            {
                var allWave = _hews.GetAllWavesDescending( _waveScenarioNumber, selectedBarTime );

                if ( allWave != null && allWave.Count > 0 )
                {
                    AddToExistingWave( waveScenarioNo, selectedBarTime, waveCycle, elliottWave );
                }
                else
                {
                    _hews?.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar );
                }
            }


            Refresh();
        }

        public override void AddWaveFourToChart( int waveScenarioNo, ElliottWaveCycle waveCycle )
        {
            AddWaveToChart( waveScenarioNo, waveCycle, ElliottWaveEnum.Wave4 );

            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            _hews?.SmartAddWave_ABC_to_45( waveScenarioNo, selectedBarTime, ResponsibleTF, waveCycle );
        }





        public override void AddWaveBToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar == SBar.EmptySBar ) return;


            if ( _chartVM.IsSpecialBar || _hews.IsSpecialBar( _waveScenarioNumber, selectedBarTime ) || bar.IsSpecialBar )
            {
                _hews?.AddIntelliWave( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar, true, _bars.IsOutsideBar( selectedBarTime ) );
            }
            else
            {
                var allWave = _hews.GetAllWavesDescending( _waveScenarioNumber, selectedBarTime );

                if ( allWave != null && allWave.Count > 0 )
                {
                    AddToExistingWave( waveScenarioNo, selectedBarTime, waveCycle, elliottWave );
                }
                else
                {
                    var desiredCycle = waveCycle;

                    var dbHewPrev = _hews.GetPreviousWaveStructure( waveScenarioNo, selectedBarTime );

                    if ( dbHewPrev != null ) // This means we are dealing with the ABC of a larger timeframe
                    {
                        ref var hew = ref dbHewPrev.GetWaveFromScenario( waveScenarioNo );
                        var previousWaves = hew.GetAllWaves();

                        foreach ( var previousWave in previousWaves )
                        {
                            if ( previousWave.WaveName == ElliottWaveEnum.WaveA )
                            {
                                desiredCycle = previousWave.WaveCycle;
                            }
                        }
                    }

                    _hews?.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, desiredCycle, elliottWave, ref bar );
                }
            }

            Refresh();

        }

        public override void CopyBarTimeToClipboard()
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            if ( selectedBarTime > -1 )
            {
                Clipboard.SetDataObject( selectedBarTime.ToString() );
            }
        }



        public override void AddWaveCToChart( int waveScenarioNo, ElliottWaveCycle waveCycle, ElliottWaveEnum elliottWave )
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar == SBar.EmptySBar ) return;


            if ( _chartVM.IsSpecialBar || _hews.IsSpecialBar( _waveScenarioNumber, selectedBarTime ) || bar.IsSpecialBar )
            {
                _hews?.AddIntelliWave( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, elliottWave, ref bar, true, _bars.IsOutsideBar( selectedBarTime ) );
            }
            else
            {
                var allWave = _hews.GetAllWavesDescending( _waveScenarioNumber, selectedBarTime );

                if ( allWave != null && allWave.Count > 0 )
                {
                    AddToExistingWave( waveScenarioNo, selectedBarTime, waveCycle, elliottWave );
                }
                else
                {
                    var WaveCCycle = waveCycle; //- 5; // We are adding Wave C of a lower degree.

                    bool found = false;

                    long preivousBTime = -1;

                    /*  
                     *  -----------------------------------------------------------------------------------------------------------------------------
                     *      
                     *      We are looking backward to see if if we can find any Wave B of Larger degree                             
                     *  -----------------------------------------------------------------------------------------------------------------------------
                    */
                    while ( WaveCCycle != ElliottWaveCycle.MAX && found == false )
                    {
                        preivousBTime = _hews.FindPreviousWaveB( _waveScenarioNumber, selectedBarTime, WaveCCycle );

                        if ( preivousBTime == -1 )
                        {
                            WaveCCycle += GlobalConstants.OneWaveCycle;
                        }
                        else
                        {
                            if ( _hews.NoEqualOrHigherWaveBetween( waveScenarioNo, preivousBTime, selectedBarTime, WaveCCycle ) )
                            {
                                found = true;
                            }
                            else
                            {
                                WaveCCycle += GlobalConstants.OneWaveCycle;
                            }
                        }
                    }

                    if ( !found )
                    {
                        WaveCCycle = waveCycle - GlobalConstants.OneWaveCycle;

                        while ( WaveCCycle != ElliottWaveCycle.UNKNOWN && found == false )
                        {
                            preivousBTime = _hews.FindPreviousWaveB( _waveScenarioNumber, selectedBarTime, WaveCCycle );

                            if ( preivousBTime == -1 )
                            {
                                WaveCCycle -= GlobalConstants.OneWaveCycle;
                            }
                            else
                            {
                                if ( _hews.NoEqualOrHigherWaveBetween( waveScenarioNo, preivousBTime, selectedBarTime, WaveCCycle ) )
                                {
                                    found = true;
                                }
                                else
                                {
                                    WaveCCycle -= GlobalConstants.OneWaveCycle;
                                }
                            }
                        }
                    }



                    if ( found )
                    {
                        ProcessAddingWaveC( waveScenarioNo, selectedBarTime, WaveCCycle );
                    }
                    else
                    {
                        /*  
                         *  -----------------------------------------------------------------------------------------------------------------------------
                         *      If we end up getting here, we could be 
                         *      1) We are initially defining the wave, so there is no previous wave.
                         *      2) We have labeled an afterward wave, so there is also no previous wave.
                         *  -----------------------------------------------------------------------------------------------------------------------------
                        */

                        _hews.SmartAddWaveXtoManagerAndBar( waveScenarioNo, ResponsibleTF, selectedBarTime, waveCycle, ElliottWaveEnum.WaveC, ref bar );
                    }
                }
            }


            Refresh();

        }


        public override void CycleUpSelectedBar( int waveScenarioNo )
        {
            if ( null == _hews )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

                if ( aa != null )
                {
                    _hews = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }
            }

            if ( _chartVM.HasMultipleBarsHighlighted )
            {
                var barLinuxTime = _chartVM.HighlightedBarLinuxTime;

                if ( barLinuxTime.Count > 0 )
                {
                    foreach ( var barTime in barLinuxTime )
                    {
                        if ( barTime > -1 )
                        {
                            ref SBar bar = ref _bars.GetBarByTime( barTime );

                            if ( bar != SBar.EmptySBar )
                            {
                                _hews.CycleUpSelectedBar( waveScenarioNo, ResponsibleTF, bar.LinuxTime, ref bar );
                            }
                        }
                    }
                }
            }
            else
            {
                long selectedBarTime = _chartVM.SelectedCandleBarTime;

                if ( selectedBarTime > -1 )
                {
                    ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                    if ( bar != SBar.EmptySBar )
                        _hews.CycleUpSelectedBar( waveScenarioNo, ResponsibleTF, selectedBarTime, ref bar );
                }
            }

            Refresh();
        }

        public override void CycleDownSelectedBar( int waveScenarioNo )
        {
            if ( null == _hews )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

                if ( aa != null )
                {
                    _hews = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }
            }

            if ( _chartVM.HasMultipleBarsHighlighted )
            {
                var barTimes = _chartVM.HighlightedBarLinuxTime;

                if ( barTimes.Count > 0 )
                {
                    foreach ( var barTime in barTimes )
                    {
                        if ( barTime > -1 )
                        {
                            ref SBar bar = ref _bars.GetBarByTime( barTime );

                            if ( bar != SBar.EmptySBar )
                                _hews.CycleDownSelectedBar( waveScenarioNo, ResponsibleTF, bar.LinuxTime, ref bar );
                        }
                    }
                }
            }
            else
            {
                long selectedBarTime = _chartVM.SelectedCandleBarTime;

                if ( selectedBarTime > -1 )
                {
                    ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                    if ( bar != SBar.EmptySBar )
                        _hews.CycleDownSelectedBar( waveScenarioNo, ResponsibleTF, selectedBarTime, ref bar );
                }
            }

            Refresh();
        }


        public override void BranchWaves()
        {
            CheckHew();

            if ( _chartVM.HasMultipleBarsHighlighted )
            {
                var barTimes = _chartVM.HighlightedBarLinuxTime;

                if ( barTimes.Count == 1 )
                {
                    foreach ( var barTime in barTimes )
                    {
                        if ( barTime > -1 )
                        {
                            ref SBar bar = ref _bars.GetBarByTime( barTime );

                            if ( bar != SBar.EmptySBar )
                            {
                                _hews?.BranchWaves( _waveScenarioNumber, bar.LinuxTime, bar.BarPeriod );
                            }
                        }
                    }
                }
            }
            else
            {
                long selectedBarTime = _chartVM.SelectedCandleBarTime;

                if ( selectedBarTime > -1 )
                {
                    ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                    if ( bar != SBar.EmptySBar )
                    {
                        _hews?.BranchWaves( _waveScenarioNumber, bar.LinuxTime, bar.BarPeriod );
                    }
                }
            }

            Refresh();
        }


        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        private void CheckHew()
        {
            if ( null == _hews )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

                if ( aa != null )
                {
                    _hews = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }
            }
        }


        public override void RemoveWavesFromManagerAndBar( int waveScenarioNo, TimeSpan period )
        {
            CheckHew();

            if ( _chartVM.HasMultipleBarsHighlighted )
            {
                var barTimes = _chartVM.HighlightedBarLinuxTime;

                foreach ( var barTime in barTimes )
                {
                    if ( barTime > -1 )
                    {
                        ref SBar bar = ref _bars.GetBarByTime( barTime );

                        if ( bar != SBar.EmptySBar )
                        {
                            _hews?.DeleteWavesFromManagerAndBar( waveScenarioNo, bar.LinuxTime, ref bar, period );
                        }
                    }
                }
            }
            else
            {
                long selectedBarTime = _chartVM.SelectedCandleBarTime;

                if ( selectedBarTime > -1 )
                {
                    ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                    if ( bar != SBar.EmptySBar )
                    {
                        _hews?.DeleteWavesFromManagerAndBar( waveScenarioNo, selectedBarTime, ref bar, period );
                    }
                }
            }

            Refresh();
        }


        public override void AnalyzeWaveTarget()
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            if ( selectedBarTime > -1 )
            {
                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _bars.Security );

                WaveTargetsTsoCollection waveTargets = aa.WaveTargetBindingList;
                waveTargets.Clear();

                _hews.AnalyzeWaveTarget( _waveScenarioNumber, ResponsibleTF, _bars, selectedBarTime );
            }
        }

        public override void FindAndLoadDatabarsHigherTimeFrame( int waveScenarioNo )
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            ref SBar selectedBar = ref _bars.GetBarByTime( selectedBarTime );

            if ( selectedBar == SBar.EmptySBar ) return;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

            TimeSpan higherTimeSpan = TimeSpan.MinValue;

            if ( aa != null )
            {
                higherTimeSpan = aa.GetOneTimeSpanHigher( ResponsibleTF );
            }
            else
            {
                return;
            }


            if ( selectedBar != SBar.EmptySBar && higherTimeSpan <= TimeSpan.FromDays( 31 ) )
            {
                var higherTFRepo = SymbolsMgr.Instance.GetDatabarRepo( SelectedSecurity, higherTimeSpan );

                ref SBar higherBar = ref higherTFRepo.GetBarContainingTime( selectedBar.BarTime, higherTimeSpan );

                if ( higherBar != SBar.EmptySBar )
                {
                    Messenger.Default.Send( new LocateBarMessage( higherBar.BarIndex, higherBar.LinuxTime, higherTimeSpan, true ) );
                }
            }
        }

        public override void FindAndLoadDatabarsLowerTimeFrame( int waveScenarioNo )
        {
            long selectedBarTime = _chartVM.SelectedCandleBarTime;

            if ( selectedBarTime == -1 )
                return;

            fxHistoricBarsRepo barsLTF = null;

            ref SBar selectedBar = ref _bars.GetBarByTime( selectedBarTime );

            if ( selectedBar == SBar.EmptySBar ) return;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

            TimeSpan lowerTimespan = TimeSpan.MinValue;

            if ( aa != null )
            {
                lowerTimespan = aa.GetOneTimeSpanLower( ResponsibleTF );
            }
            else
            {
                return;
            }

            var endTime = DateTime.MinValue;



            if ( selectedBar != SBar.EmptySBar && lowerTimespan >= TimeSpan.FromTicks( 1 ) )
            {
                barsLTF = SymbolsMgr.Instance.GetDatabarRepo( SelectedSecurity, lowerTimespan );

                var lowerRange = selectedBarTime.FromLinuxTime();

                var upperRange = lowerRange + ResponsibleTF;

                if ( selectedBar.HasElliottWave )
                {
                    ref var hew = ref selectedBar.GetWaveFromScenario( waveScenarioNo );
                    var highestWave = hew.GetFirstHighestWaveInfo();

                    if ( highestWave.HasValue )
                    {
                        var highest = highestWave.Value;
                        ref SBar lowerTFBar = ref barsLTF.GetBarWithWaveInRange( ref highest, lowerRange, upperRange );

                        if ( lowerTFBar != SBar.EmptySBar )
                        {
                            Messenger.Default.Send( new LocateBarMessage( lowerTFBar.BarIndex, lowerTFBar.LinuxTime, lowerTimespan, true ) );

                            return;

                        }
                    }
                }


                if ( selectedBar.IsExtremum() )
                {
                    int waveImportance = -1;

                    if ( _hews != null )
                    {
                        waveImportance = _hews.GetWaveImportance( selectedBar.BarPeriod, selectedBar.LinuxTime );
                    }

                    var extremumType = selectedBar.GetExtremumType();

                    if ( waveImportance > -1 && extremumType != TASignal.NONE )
                    {
                        ref SBar lowerTFBar = ref barsLTF.GetHighestWaveImportanceInTheRange( SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity ).HewManager, lowerTimespan, waveImportance, lowerRange, upperRange, extremumType );

                        if ( lowerTFBar != SBar.EmptySBar )
                        {
                            Messenger.Default.Send( new LocateBarMessage( lowerTFBar.BarIndex, lowerTFBar.LinuxTime, lowerTimespan, true ) );

                            return;

                        }
                    }
                }


                if ( barsLTF.TotalBarCount > 0 )
                {
                    endTime = barsLTF[0].BarTime - lowerTimespan;
                }
            }


            var beginTime = selectedBar.BarTime;

            beginTime = beginTime.AddMinutes( -lowerTimespan.TotalMinutes * 100 );

            if ( beginTime < endTime )
            {
                //var fxDialog = new fxDatabarDownloader( );

                //fxDialog.Symbol = Session.Info.fxSymbol.SymbolString;

                //fxDialog.Period = lowerTimespan;

                //fxDialog.StartDate = beginTime;

                //fxDialog.StartPosition = FormStartPosition.CenterScreen;

                //if ( fxDialog.ShowDialog( ) == DialogResult.OK )
                //{
                //    var myplashScreenManager = SplashScreenManager.Default;

                //    if ( myplashScreenManager != null && myplashScreenManager.IsSplashFormVisible )
                //    {
                //        SplashScreenManager.CloseForm( );
                //    }

                //    SplashScreenManager.ShowForm( typeof( DatabarDownloadSplashScreen ) );

                //    string message = string.Format( "Download {2} databars from {0} to {1} ", beginTime, endTime, lowerTimespan );

                //    Messenger.Default.Send( new ClientRequestMessage( message ) );

                //    barsLTF.DownloadDatabarsFromXtoY( beginTime, endTime );
                //}
            }
        }


  



        public override void LockWavesInDB()
        {
            if ( _chartVM.HasMultipleBarsHighlighted )
            {
                var barTimes = _chartVM.HighlightedBarLinuxTime;

                var lockedTime = new PooledList<long>();

                if ( barTimes.Count > 0 )
                {
                    foreach ( var barTime in barTimes )
                    {
                        if ( barTime > -1 )
                        {
                            ref SBar bar = ref _bars.GetBarByTime( barTime );

                            if ( bar != SBar.EmptySBar )
                                lockedTime.Add( bar.LinuxTime );
                        }
                    }

                    _hews.LockAndSaveWavesInDB( ResponsibleTF, lockedTime );
                }
            }
            else
            {
                long selectedBarTime = _chartVM.SelectedCandleBarTime;

                if ( selectedBarTime > -1 )
                {
                    ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

                    if ( bar != SBar.EmptySBar )
                        _hews.LockAndSaveWaveInDB( ResponsibleTF, bar.LinuxTime );
                }
            }
        }

        public override void MarkPrimary()
        {
            CheckHew();

            _hews?.MarkWaveAsPrimary( _waveScenarioNumber, ResponsibleTF );

            Refresh();
        }

    }


}