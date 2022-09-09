using fx.Algorithm;
using fx.Definitions;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class TradeStationViewModel : BaseLogReceiver, IMutltiTimeFrameSessionDataRepo
    {
        public void StartWaveAnalysis( IEnumerable<Security> selectedSymbols )
        {

        }

        public void ShowMoreWaves()
        {
            _selectedViewModel.ChartVM.ShowMoreWaves();

            _selectedViewModel.Refresh();
        }

        public void ShowLessWaves()
        {
            _selectedViewModel.ChartVM.ShowLessWaves();

            _selectedViewModel.Refresh();
        }

        public void AddAltWaves()
        {
            var viewModel = CreateAltViewModel( _selectedViewModel.ResponsibleTF, _selectedViewModel.ResponsibleTF.ToReadable() );

            if ( _selectedViewModel.ResponsibleTF < TimeSpan.FromDays( 1 ) )
            {
                _intradayViewModels.Add( viewModel );
            }

            _allVisibleViewModels.Add( viewModel );

            ChildViews.Add( viewModel );
        }

        public void MarkPrimary()
        {
            using ( _selectedUndoRedoArea.Start( "MarkPrimary" ) )
            {
                IChartTabViewModel anotherVM = GetViewModelFromWaveScenarioNo( _selectedViewModel.ResponsibleTF, 1 );

                if ( anotherVM != null )
                {
                    _selectedViewModel.ChartVM.RemoveAllEWaves();
                    anotherVM.ChartVM.RemoveAllEWaves();

                    _selectedViewModel.MarkPrimary();

                    _selectedViewModel.Refresh();

                    anotherVM.Refresh();
                }

                _selectedUndoRedoArea.Commit();


            }
        }
        public IChartTabViewModel GetViewModelFromWaveScenarioNo( TimeSpan period, int waveScenarioNumber )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                switch ( waveScenarioNumber )
                {
                    case 1:
                        return _01MinVm;

                    default:
                        {
                            int index = _01MinAltVms.FindIndex( x => x.WaveScenarioNumber == waveScenarioNumber );

                            if ( index > -1 )
                            {
                                return _01MinAltVms[index];
                            }
                        }
                        break;
                }

            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                switch ( waveScenarioNumber )
                {
                    case 1:
                        return _01hrsVm;

                    default:
                        {
                            int index = _01hrsAltVms.FindIndex( x => x.WaveScenarioNumber == waveScenarioNumber );

                            if ( index > -1 )
                            {
                                return _01hrsAltVms[index];
                            }
                        }
                        break;
                }
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                switch ( waveScenarioNumber )
                {
                    case 1:
                        return _dailyVm;

                    default:
                        {
                            int index = _dailyAltVms.FindIndex( x => x.WaveScenarioNumber == waveScenarioNumber );

                            if ( index > -1 )
                            {
                                return _dailyAltVms[index];
                            }
                        }
                        break;
                }
            }

            return null;
        }

        public void CheckEnabledViewOptions()
        {
            if ( ShowCandlePattern )
            {
                _selectedViewModel.ChartVM.ShowCandlePattern( true );
            }

            if ( ShowTradingTime )
            {
                _selectedViewModel.ChartVM.ShowTradingTime( true );
            }

            if ( ShowFreemindIndicators )
            {
                _selectedViewModel.ChartVM.ShowFreemindIndicators( true );
            }

            if ( YAxisAutoRange )
            {
                _selectedViewModel.ChartVM.YAxisAutoRange( true );
            }

            if ( ShowElliottWave )
            {
                _selectedViewModel.ChartVM.ShowElliottWave( true );
            }

            if ( ShowMonoWave )
            {
                _selectedViewModel.ChartVM.ShowMonoWave( true );
            }

            if ( ShowHewDetection )
            {
                _selectedViewModel.ChartVM.ShowHewDetection( true );
            }

            if ( ShowDivergence )
            {
                _selectedViewModel.ChartVM.ShowDivergence( true );
            }

            if ( ShowSmallTradingEvent )
            {
                _selectedViewModel.ChartVM.ShowSmallTradingEvent( true );
            }

            if ( ShowGannPriceTime )
            {
                _selectedViewModel.ChartVM.ShowGannPriceTime( true );
            }
        }

        public void BranchWaves()
        {
            using ( _selectedUndoRedoArea.Start( "BranchWaves" ) )
            {
                _selectedViewModel.BranchWaves();

                _selectedViewModel.Refresh();

                _selectedUndoRedoArea.Commit();
            }
        }
    }
}
