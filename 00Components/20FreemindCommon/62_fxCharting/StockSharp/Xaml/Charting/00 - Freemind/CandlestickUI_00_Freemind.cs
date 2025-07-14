using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;
using fx.Definitions;

namespace StockSharp.Xaml.Charting
{
    public partial class ChartCandleElement : ChartComponent<ChartCandleElement>, ICloneable, INotifyPropertyChanging, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, IChartElement
    {
        [Display( Description = "CandlePattern", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowCandlePattern
        {
            get
            {
                return _showCandlePattern;
            }
            set
            {
                _showCandlePattern = value;
                RaisePropertyChanged( nameof( ShowCandlePattern ) );
            }
        }

        [Display( Description = "IndicatorResult", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowIndicatorResult
        {
            get
            {
                return _showIndicatorResult;
            }
            set
            {
                _showIndicatorResult = value;
                RaisePropertyChanged( nameof( ShowIndicatorResult ) );
            }
        }

        [Display( Description = "ShowExtremes", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowExtremes
        {
            get
            {
                return _showExtremes;
            }
            set
            {
                _showExtremes = value;
                RaisePropertyChanged( nameof( ShowExtremes ) );
            }
        }

        [Display( Description = "MacdExtremes", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowMacdExtremes
        {
            get
            {
                return _showMacdExtremes;
            }
            set
            {
                _showMacdExtremes = value;
                RaisePropertyChanged( nameof( ShowMacdExtremes ) );
            }
        }


        [Display( Description = "TradingTime", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowTradingTime
        {
            get
            {
                return _showTradingTime;
            }
            set
            {
                _showTradingTime = value;
                RaisePropertyChanged( nameof( ShowTradingTime ) );
            }
        }

        [Display( Description = "WaveScenarioNo", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public int WaveScenarioNo
        {
            get
            {
                return _waveScenarioNo;
            }
            set
            {
                _waveScenarioNo = value;
                RaisePropertyChanged( nameof( WaveScenarioNo ) );
            }
        }

        private int _waveImportance;

        [Display( Description = "WaveImportance", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public int WaveImportance
        {
            get
            {
                return _waveImportance;
            }
            set
            {
                _waveImportance = value;
                RaisePropertyChanged( nameof( WaveImportance ) );
            }
        }

        private ElliottWaveCycle  _waveCycle;

        [Display( Description = "WaveImportance", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public ElliottWaveCycle WaveCycle
        {
            get
            {
                return _waveCycle;
            }
            set
            {
                _waveCycle = value;
                RaisePropertyChanged( nameof( WaveCycle ) );
            }
        }

        [Display( Description = "ElliottWaveZigZag", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowElliottWave
        {
            get
            {
                return _showElliottWave;
            }
            set
            {
                _showElliottWave = value;
                RaisePropertyChanged( nameof( ShowElliottWave ) );
            }
        }

        [Display(Description = "ElliottWaveZigZag", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof(LocalizedStrings))]
        public bool IsSimulation
        {
            get
            {
                return _isSimulation;
            }
            set
            {
                _isSimulation = value;
                RaisePropertyChanged(nameof(IsSimulation));
            }
        }

        [Display( Description = "MonoWaveGroup", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]        
        public bool ShowMonoWave
        {
            get
            {
                return _showMonotWave;
            }
            set
            {
                _showMonotWave = value;
                RaisePropertyChanged( nameof( ShowMonoWave ) );
            }
        }

        private bool _showHewDetection = false;
        public bool ShowHewDetection
        {
            get
            {
                return _showHewDetection;
            }
            set
            {
                _showHewDetection = value;
                RaisePropertyChanged( nameof( ShowHewDetection ) );
            }
        }


        [Display( Description = "ShowDivergence", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowDivergence
        {
            get
            {
                return _showDivergence;
            }
            set
            {
                _showDivergence = value;
                RaisePropertyChanged( nameof( ShowDivergence ) );
            }
        }



        [Display( Description = "PriceTimeSignal", GroupName = "StyleString", Name = "TechnicalAnalysis", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowPriceTimeSignal
        {
            get
            {
                return _showPriceTimeSignal;
            }
            set
            {
                _showPriceTimeSignal = value;
                RaisePropertyChanged( nameof( ShowPriceTimeSignal ) );
            }
        }

        public int SignalMargin
        {
            get
            {
                return _signalMargin;
            }
            set
            {
                _signalMargin = value;
                RaisePropertyChanged( nameof( SignalMargin ) );
            }
        }

        
        public bool HighQualityWaveText
        {
            get
            {
                return _highQualityWaveText;
            }
            set
            {
                _highQualityWaveText = value;
                RaisePropertyChanged( nameof( HighQualityWaveText ) );
            }
        }

        public long SelectedCandleBarTime
        {
            get
            {
                return _viewModel.SelectedCandleBarTime;
            }

            set
            {
                _viewModel.SelectedCandleBarTime = value;
            }
        }

        public void LockFibLevelsObject( )
        {
            _viewModel.LockFibLevelsObject( );
        }


        public void RemoveAllEWaves()
        {
            _viewModel.RemoveAllEWaves();
        }


        public void DeleteAllLockFibLevels( )
        {
            _viewModel.DeleteAllLockFibLevels( );
        }

        public void ResetUI( )
        {
            _viewModel.ResetUI( );
        }
    }
}
