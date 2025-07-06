using DevExpress.Mvvm;
using Ecng.Xaml;
using fx.Algorithm;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using DevExpress.Xpf.Bars;
using fx.Common;
using Ecng.ComponentModel;

namespace StockSharp.Xaml.Charting
{
    public class TradingEventSmallViewModel : DevExpress.Mvvm.ViewModelBase
    {
        private string _symbol;

        private PeriodXTaManager _manager;


        // Instead of using AverageDailyRange, in the XAML, the right way to use is TaManager.AverageDailyRange
        public PeriodXTaManager TaManager
        {
            get { return _manager; }
            set
            {
                SetValue( ref _manager, value );
            }
        }

        bool _exceedDailyRange;
        public Brush WarningColor
        {
            get
            {
                if ( _exceedDailyRange )
                {
                    return new SolidColorBrush( Colors.Yellow );
                }
                else
                {
                    return new SolidColorBrush( Colors.Transparent );
                }
            }
        }
        public bool ExceedDailyRange
        {
            get
            {
                return _exceedDailyRange;
            }
            set
            {
                _exceedDailyRange = value;
                RaisePropertyChanged( nameof( WarningColor ) );
            }
        }

        

        public TradingEventSmallViewModel( )
        {
            Messenger.Default.Register<SelectSecurityMessage>( this, x => OnSelectSecurityMessage( x ) );
            Messenger.Default.Register<WavePredictionMessage>( this, x => OnWavePredictionChange( x ) );
        }

        private void OnSelectSecurityMessage( SelectSecurityMessage x )
        {
            if ( string.IsNullOrEmpty( _symbol ) )
            {
                _symbol = x.Symbol.Code;

                var aa = (AdvancedAnalysisManager)SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

                if ( aa != null )
                {
                    aa.InitializeTechnicalAnalysis( x.SelectedTF );
                    aa.InitializeBacktestingTA( _symbol, x.SelectedTF );
                    TaManager = ( PeriodXTaManager ) aa.GetPeriodXTa( TimeSpan.FromDays( 1 ) );

                    TaManager.PropertyChanged += TaManager_PropertyChanged;
                }

                if ( aa == null )
                    return;

                RaisePropertyChanged( nameof( TradingEventsItemSource ) );
            }
        }

        private void OnWavePredictionChange( WavePredictionMessage x )
        {
            _symbol = x.Symbol.Code;

            var aa = (AdvancedAnalysisManager)SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

            if ( aa == null )
                return;

            RaisePropertyChanged( nameof( WaveTargetItemSource ) );
        }

        

        private void TaManager_PropertyChanged( object sender, System.ComponentModel.PropertyChangedEventArgs e )
        {
            switch ( e.PropertyName )
            {
                case "TodayRangeToAvgPercentage":
                {
                    if ( TaManager.TodayRangeToAvgPercentage == 100 )
                    {
                        ExceedDailyRange = true;
                    }
                    else 
                    {
                        ExceedDailyRange = false;
                    }
                }
                break;
            }
        }

        public ObservableCollectionEx<FxTradingEvents> TradingEventsItemSource
        {
            get
            {
                if ( _symbol != null )
                {
                    ObservableCollectionEx< FxTradingEvents > tradingEventList = null;

                    var aa = (AdvancedAnalysisManager)SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

                    if ( aa != null )
                    {
                        tradingEventList = aa.TradingEventsItemSource;
                        TaManager = ( PeriodXTaManager ) aa.GetPeriodXTa( TimeSpan.FromDays( 1 ) );

                        TaManager.PropertyChanged += TaManager_PropertyChanged;
                    }
                    
                    return tradingEventList;
                }

                return null;
            }
        }

        public ObservableCollectionEx<FibLevelInfo> WaveTargetItemSource
        {
            get
            {
                if ( _symbol != null )
                {
                    ObservableCollectionEx< FibLevelInfo > waveTargets = null;

                    var aa = (AdvancedAnalysisManager)SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

                    if ( aa != null )
                    {
                        waveTargets = aa.WaveTargetItemSource;
                    }

                    return waveTargets;
                }

                return null;
            }
        }

        public void StartItemSource( string symbol )
        {
            _symbol = symbol;

            RaisePropertyChanged( nameof( TradingEventsItemSource ) );
        }

        
        public void OnCheckChanged( ItemClickEventArgs args )
        {
            ItemClickEventArgs myevent = args;

            //if ( args.Source is BarCheckItem )
            //{
            //    var checkItem = ( BarCheckItem )args.Source;

            //    if ( checkItem.IsChecked.Value == false )
            //    {
            //        return;
            //    }
            //}

            //_tradingMode = ( TradingMode ) Enum.Parse( typeof( fx.Definitions.TradingMode ), ( string ) myevent.Item.Tag );
        }

        //public PooledList< > 
    }

    public class CandlesData : PooledList<CandleImage>
    {
        static PooledList<CandleImage> dataSource = null;
        public static PooledList<CandleImage> DataSource
        {
            get
            {
                if ( dataSource != null )
                    return dataSource;
                //XmlSerializer s = new XmlSerializer( typeof( CountriesData ) );
                //Assembly assembly = typeof( CountriesData ).Assembly;
                //dataSource = ( PooledList<CandleImage> )s.Deserialize( assembly.GetManifestResourceStream( DemoHelper.GetPath( "GridDemo.Data.", assembly ) + "Countries.xml" ) );
                return dataSource;
            }
        }
    }

    public class CandleImage
    {
        public string ActualNWindName { get { return NWindName ?? Name; } }
        public string ActualName { get { return Name ?? NWindName; } }
        public string Name { get; set; }
        public string NWindName { get; set; }
        public byte[ ] Flag { get; set; }
    }
}


