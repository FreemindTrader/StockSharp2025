using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Disruptor;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Algorithm;
using fx.Bars;
using fx.Charting;
using fx.Collections;
using fx.Definitions;
using fx.Definitions.UndoRedo;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#pragma warning disable CS0618

namespace FreemindAITrade.ViewModels
{
    public partial class TradeStationViewModel : BaseLogReceiver, IMutltiTimeFrameSessionDataRepo, IPersistable, IValueEventHandler<CandleStruct>
    {
        ChartTabViewModelFactory _factory = new ChartTabViewModelFactory();
        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        public ISplashScreenService SplashScreenService { get { return this.GetService<ISplashScreenService>(); } }

        private UndoRedoArea _selectedUndoRedoArea = null;
        private IChartTabViewModel _selectedViewModel = null;
        private ElliottWaveCycle _defaultWaveCycle = ElliottWaveCycle.Micro;
        private PooledList<IChartTabViewModel> _allVisibleViewModels = new PooledList<IChartTabViewModel>();
        private PooledList<IChartTabViewModel> _nonVisibleViewModels = new PooledList<IChartTabViewModel>();

        private PooledList<IChartTabViewModel> _intradayViewModels = new PooledList<IChartTabViewModel>();

        private IChartTabViewModel _lastTabViewModel = null;


        private ObservableCollection<object> _childViews = new ObservableCollection<object>();
        private IDocument _selectSymbolDoc = null;
        private IEnumerable<Security> _selectedSecurities;
        private bool _isBarIntegrityCheck = false;
        private bool _showAllTimeFrameCharts = false;

        private Portfolio _selectedPortfolio;
        private Security _security;
        private SymbolSelectViewModel _symbolSelectViewModel;

        private IChartTabViewModel _01secVm = null;
        private IChartTabViewModel _01MinVm = null;
        private IChartTabViewModel _04MinVm = null;
        private IChartTabViewModel _05MinVm = null;
        private IChartTabViewModel _15MinVm = null;
        private IChartTabViewModel _30MinVm = null;
        private IChartTabViewModel _01hrsVm = null;
        private IChartTabViewModel _02hrsVm = null;
        private IChartTabViewModel _03hrsVm = null;
        private IChartTabViewModel _04hrsVm = null;
        private IChartTabViewModel _06hrsVm = null;
        private IChartTabViewModel _08hrsVm = null;
        private IChartTabViewModel _dailyVm = null;
        private IChartTabViewModel _weeklyVm = null;
        private IChartTabViewModel _monthlyVm = null;

        private CancellationTokenSource _cancelToken = new CancellationTokenSource();

        public CancellationTokenSource TradeStationExitTokenSource
        {
            get
            {
                return _cancelToken;
            }

            set
            {
                _cancelToken = value;
            }
        }

        public CancellationToken TradeStationExitToken
        {
            get
            {
                return _cancelToken.Token;
            }
        }




        List<IChartTabViewModel> _01MinAltVms = new List<IChartTabViewModel>();
        List<IChartTabViewModel> _01hrsAltVms = new List<IChartTabViewModel>();
        List<IChartTabViewModel> _dailyAltVms = new List<IChartTabViewModel>();


        private Stopwatch _stopWatch = new Stopwatch();
        private readonly ObservableCollection<TimeSpan> _timeFrames;

        private Connector _liveTradingConnector;

        private bool _symbolSelected = false;

        private fxHistoricEmulationConnector _backTestingConnector;

        private CandleManager _candleManager;

        public int _activatedCount = 0;

        public sealed class CandleDrawStyleImage
        {
            private readonly ImageSource _ImgSource;
            private readonly string _name;
            private readonly ChartCandleDrawStyles _candleDrawStyle;

            public CandleDrawStyleImage( ImageSource imgSrc, string name, ChartCandleDrawStyles style )
            {
                if ( name.IsEmpty() )
                    throw new ArgumentNullException( nameof( name ) );
                ImageSource imageSource = imgSrc;
                if ( imageSource == null )
                    throw new ArgumentNullException( nameof( imgSrc ) );
                _ImgSource = imageSource;
                _name = name;
                _candleDrawStyle = style;
            }

            public ImageSource Image
            {
                get
                {
                    return _ImgSource;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public ChartCandleDrawStyles CandleDrawStyle()
            {
                return _candleDrawStyle;
            }
        }

        public sealed class WaveCycleImage
        {
            private readonly ImageSource _ImgSource;
            private readonly string _name;
            private ElliottWaveCycle _waveCycle;

            public WaveCycleImage( ImageSource imgSrc, string name, ElliottWaveCycle style )
            {
                if ( name.IsEmpty() )
                    throw new ArgumentNullException( nameof( name ) );
                ImageSource imageSource = imgSrc;
                if ( imageSource == null )
                    throw new ArgumentNullException( nameof( imgSrc ) );

                _ImgSource = imageSource;
                _name = name;
                _waveCycle = style;
            }

            public ImageSource Image
            {
                get
                {
                    return _ImgSource;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public ElliottWaveCycle WaveCycle
            {
                get
                {
                    return _waveCycle;
                }

                set
                {
                    _waveCycle = value;
                }

            }
        }


        public sealed class WaveImptImage
        {
            private readonly ImageSource _ImgSource;
            private readonly string _name;
            private readonly int _waveImpt;

            public WaveImptImage( ImageSource imgSrc, string name, int waveImpt )
            {
                if ( name.IsEmpty() )
                    throw new ArgumentNullException( nameof( name ) );
                ImageSource imageSource = imgSrc;
                if ( imageSource == null )
                    throw new ArgumentNullException( nameof( imgSrc ) );

                _ImgSource = imageSource;
                _name = name;
                _waveImpt = waveImpt;
            }

            public ImageSource Image
            {
                get
                {
                    return _ImgSource;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public int WaveImportance
            {
                get
                {
                    return _waveImpt;
                }
            }
        }

        static TradeStationViewModel()
        {
            RegisterCandleStyle( "candlesbars", LocalizedStrings.Bars, ChartCandleDrawStyles.Ohlc );
            RegisterCandleStyle( "candlestick", LocalizedStrings.CandleStick, ChartCandleDrawStyles.CandleStick );
            RegisterCandleStyle( "candlesline", LocalizedStrings.LineOpen, ChartCandleDrawStyles.LineOpen );
            RegisterCandleStyle( "candlesline", LocalizedStrings.LineClose, ChartCandleDrawStyles.LineClose );
            RegisterCandleStyle( "candlesline", LocalizedStrings.LineHigh, ChartCandleDrawStyles.LineHigh );
            RegisterCandleStyle( "candlesline", LocalizedStrings.LineLow, ChartCandleDrawStyles.LineLow );
            RegisterCandleStyle( "candleslinearea", LocalizedStrings.Area, ChartCandleDrawStyles.Area );
            RegisterCandleStyle( "ganttchart", LocalizedStrings.BoxChart, ChartCandleDrawStyles.BoxVolume );
            RegisterCandleStyle( "ganttchart", LocalizedStrings.ClusterProfile, ChartCandleDrawStyles.ClusterProfile );
            RegisterCandleStyle( "candlesxo", LocalizedStrings.PnFCandle, ChartCandleDrawStyles.PnF );

            RegisterWaveCycle( "GrandSupercycle", "GrandSupercycle", ElliottWaveCycle.GrandSupercycle );
            RegisterWaveCycle( "Supercycle", "Supercycle", ElliottWaveCycle.Supercycle );
            RegisterWaveCycle( "Cycle", "Cycle", ElliottWaveCycle.Cycle );
            RegisterWaveCycle( "Primary", "Primary", ElliottWaveCycle.Primary );
            RegisterWaveCycle( "Intermediate", "Intermediate", ElliottWaveCycle.Intermediate );
            RegisterWaveCycle( "SubIntermediate", "SubIntermediate", ElliottWaveCycle.SubIntermediate );
            RegisterWaveCycle( "Minor", "Minor", ElliottWaveCycle.Minor );
            RegisterWaveCycle( "SubMinor", "SubMinor", ElliottWaveCycle.SubMinor );
            RegisterWaveCycle( "Minute", "Minute", ElliottWaveCycle.Minute );
            RegisterWaveCycle( "SubMinute", "SubMinute", ElliottWaveCycle.SubMinute );
            RegisterWaveCycle( "Minuette", "Minuette", ElliottWaveCycle.Minuette );
            RegisterWaveCycle( "Subminuette", "Subminuette", ElliottWaveCycle.Subminuette );
            RegisterWaveCycle( "Micro", "Micro", ElliottWaveCycle.Micro );
            RegisterWaveCycle( "Submicro", "Submicro", ElliottWaveCycle.Submicro );
            RegisterWaveCycle( "Miniscule", "Miniscule", ElliottWaveCycle.Miniscule );

            RegisterWaveImpt( "144", "Monthly", GlobalConstants.MONTHLYIMPT );
            RegisterWaveImpt( "144", "Weekly", GlobalConstants.WEEKLYIMPT );
            RegisterWaveImpt( "144", "Daily", GlobalConstants.DAILYIMPT );
            RegisterWaveImpt( "55", "8 Hours", GlobalConstants.HRS08IMPT );
            RegisterWaveImpt( "21", "4 Hours", GlobalConstants.HRS04IMPT );
            RegisterWaveImpt( "8", "2 Hours", GlobalConstants.HRS02IMPT );
            RegisterWaveImpt( "34", "1 Hour", GlobalConstants.HRS01IMPT );
            RegisterWaveImpt( "13", "30 Minute", GlobalConstants.MINS30IMPT );
            RegisterWaveImpt( "89", "15 Minute", GlobalConstants.MINS15IMPT );
            RegisterWaveImpt( "5", "5 Minute", GlobalConstants.MINS05IMPT );
            RegisterWaveImpt( "0", "NA", 10000 );
        }

        /// <summary>Register candle style.</summary>
        /// <param name="image">Image.</param>
        /// <param name="name">Name.</param>
        /// <param name="style">Style of candles rendering.</param>
        public static void RegisterCandleStyle( string image, string name, ChartCandleDrawStyles style )
        {
            CandleDrawStyleImages.Add( new CandleDrawStyleImage( ThemedIconsExtension.GetImage( image ), name, style ) );
        }

        public static void RegisterWaveCycle( string image, string name, ElliottWaveCycle cycle )
        {
            var path = string.Format( "Images/{0}.png", image );

            var source = new BitmapImage( DevExpress.Utils.AssemblyHelper.GetResourceUri( typeof( TradeStationViewModel ).Assembly, path ) );

            WaveCycleImages.Add( new WaveCycleImage( source, name, cycle ) );
        }

        public static void RegisterWaveImpt( string image, string name, int impt )
        {
            var path = string.Format( "Images/{0}.png", image );

            var source = new BitmapImage( DevExpress.Utils.AssemblyHelper.GetResourceUri( typeof( TradeStationViewModel ).Assembly, path ) );

            WaveImptImages.Add( new WaveImptImage( source, name, impt ) );
        }

        public static readonly PooledList<CandleDrawStyleImage> CandleDrawStyleImages = new PooledList<CandleDrawStyleImage>();
        public static readonly PooledList<WaveCycleImage> WaveCycleImages = new PooledList<WaveCycleImage>();
        public static readonly PooledList<WaveImptImage> WaveImptImages = new PooledList<WaveImptImage>();


        private static StudioUserConfig UserConfig
        {
            get
            {
                return BaseUserConfig<StudioUserConfig>.Instance;
            }
        }



        public TradeStationViewModel()
        {
            Messenger.Default.Register<PivotsPointUpdateMessage>( this, x => OnPivotPointsChange( x ) );
            Messenger.Default.Register<LocateBarMessage>( this, x => OnLocateBarMessage( x ) );
            Messenger.Default.Register<BackResearchUpdateMessage>( this, x => OnSelectedBarChanged( x ) );

            _timeFrames = new ObservableCollection<TimeSpan>( new TimeSpan[ ]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromMinutes(1.0),
                TimeSpan.FromMinutes(4.0),
                TimeSpan.FromMinutes(5.0),
                TimeSpan.FromMinutes(15.0),
                TimeSpan.FromMinutes(30.0),
                TimeSpan.FromHours(1.0),
                TimeSpan.FromHours(2.0),
                TimeSpan.FromHours(4.0),
                TimeSpan.FromHours(6.0),
                TimeSpan.FromHours(8.0),
                TimeSpan.FromDays(1.0),
                TimeSpan.FromDays(7.0),
                TimeSpan.FromDays(30)
            } );

            ChartExViewModel.RefreshEvent += TonyOnRefreshEvent;

            //;            
        }

        public ObservableCollection<TimeSpan> SupportTimeFrames
        {
            get
            {
                return _timeFrames;
            }
        }



        private void OnSelectedBarChanged( BackResearchUpdateMessage msg )
        {
            var barTime = msg.SelectedBar.BarTime;
            var period = msg.SelectedBar.BarPeriod;

            DateTime periodBarTime = barTime;


            if ( _01secVm != null && _01secVm.FreemindIndicator != null )
            {
                _01secVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _01MinVm != null && _01MinVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromMinutes( 1 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 1 ) );
                }

                _01MinVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _04MinVm != null && _04MinVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromMinutes( 4 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 4 ) );
                }

                _04MinVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _05MinVm != null && _05MinVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromMinutes( 5 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 5 ) );
                }

                _05MinVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _15MinVm != null && _15MinVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromMinutes( 15 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 15 ) );
                }

                _15MinVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _30MinVm != null && _30MinVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromMinutes( 30 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 30 ) );
                }

                _30MinVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _01hrsVm != null && _01hrsVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromMinutes( 60 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 60 ) );
                }

                _01hrsVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _02hrsVm != null && _02hrsVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromHours( 2 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 2 ) );
                }

                _02hrsVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _03hrsVm != null && _03hrsVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromHours( 3 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 3 ) );
                }

                _03hrsVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _04hrsVm != null && _04hrsVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromHours( 4 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 4 ) );
                }

                _04hrsVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _06hrsVm != null && _06hrsVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromHours( 6 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 6 ) );
                }

                _06hrsVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _08hrsVm != null && _08hrsVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromHours( 8 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromMinutes( 8 ) );
                }

                _08hrsVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _dailyVm != null && _dailyVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromDays( 1 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromDays( 1 ) );
                }

                _dailyVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _weeklyVm != null && _weeklyVm.FreemindIndicator != null )
            {
                if ( period < TimeSpan.FromDays( 7 ) )
                {
                    periodBarTime = barTime.RoundDown( TimeSpan.FromDays( 1 ) );
                }

                _weeklyVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }

            if ( _monthlyVm != null && _monthlyVm.FreemindIndicator != null )
            {
                _monthlyVm.FreemindIndicator.GetTAInfoAt( periodBarTime );
            }
        }

        private void OnLocateBarMessage( LocateBarMessage message )
        {
            if ( message.SwitchPeriod )
            {
                IChartTabViewModel switchedVM = GetViewModelByTimeSpan( message.ResponsibleTimeFrame );

                if ( switchedVM != null )
                {
                    switchedVM.NeedToCenterOnBar = true;

                    switchedVM.SelectedCandleBarTime = message.LinuxTime;

                    DateTime selectedBarTime = message.LinuxTime.FromLinuxTime();

                    if ( message.BarIndex > -1 )
                    {
                        switchedVM.CenterViewOnThisBarTime = message.LinuxTime;

                        switchedVM.CenterViewOnTime( selectedBarTime );
                    }
                    else
                    {
                        var iBarRepo = switchedVM.DatabarRepo;

                        int index = iBarRepo.GetIndexByTime( message.LinuxTime );

                        switchedVM.CenterViewOnTime( selectedBarTime );
                    }

                    switchedVM.IsActive = true;
                }
            }
            else
            {
                if ( _selectedViewModel.ResponsibleTF == message.ResponsibleTimeFrame )
                {
                    _selectedViewModel.SelectedCandleBarTime = message.LinuxTime;

                    DateTime selectedBarTime = message.LinuxTime.FromLinuxTime();

                    if ( message.BarIndex > -1 )
                    {
                        _selectedViewModel.CenterViewOnThisBarTime = message.BarIndex;

                        _selectedViewModel.CenterViewOnIndexNow( selectedBarTime );
                    }
                }

            }
        }

        private IChartTabViewModel GetViewModelByTimeSpan( TimeSpan period )
        {
            foreach ( IChartTabViewModel vm in _allVisibleViewModels )
            {
                if ( vm.ResponsibleTF == period )
                {
                    return vm;
                }
            }

            return null;
        }


        public void OnAdapterConnected()
        {

        }



        public Connector Connector
        {
            get
            {
                return _liveTradingConnector;
            }

            set
            {
                _liveTradingConnector = value;
            }
        }

        protected IDocumentManagerService DocumentManagerService
        {
            get
            {
                return this.GetService<IDocumentManagerService>();
            }
        }


        protected IDialogService DialogService
        {
            get
            {
                return this.GetService<IDialogService>();
            }
        }

        public static TradeStationViewModel Create()
        {
            return ViewModelSource.Create( () => new TradeStationViewModel() );
        }

        private void myExecuteMethod()
        {
            //Debug.Print( "Registration complete" );
        }



        /// <summary>
        /// This is only here so I remember how to write WindowedDocumentUIService
        /// </summary>
        public void ShowWindow()
        {
            _symbolSelectViewModel = SymbolSelectViewModel.Create();
            _symbolSelectViewModel.SetParentViewModel( this );

            _selectSymbolDoc = DocumentManagerService.FindDocument( _symbolSelectViewModel );

            if ( _selectSymbolDoc == null )
            {
                _selectSymbolDoc = DocumentManagerService.CreateDocument( "SymbolSelectView", _symbolSelectViewModel );

                _selectSymbolDoc.Id = DocumentManagerService.Documents.Count();
            }
            _selectSymbolDoc.Show();
        }


        public void ReloadDatabars()
        {
            _selectedViewModel.ReloadCandles();
        }



        public void GotoDatabarDialog()
        {
            var gotobarViewModel = GotoDatabarViewModel.Create();

            IDialogService service = this.GetService<IDialogService>( "GotoBarService" );
            MessageResult result = service.ShowDialog(
                                                        dialogButtons: MessageButton.OKCancel,
                                                        title: "Goto Databar Index",
                                                        viewModel: gotobarViewModel
            );

            if ( result == MessageResult.OK )
            {
                var gotoBar = gotobarViewModel.DatabarIndex;

                var barX = _selectedViewModel.DatabarRepo.GetBarByIndex( gotoBar );

                long selectedBarTime = -1;

                if ( barX != SBar.EmptySBar )
                {
                    selectedBarTime = barX.BarTime.ToLinuxTime();
                }

                Messenger.Default.Send( new LocateBarMessage( gotoBar, selectedBarTime, _selectedViewModel.ResponsibleTF, false ) );
            }
        }

        public bool ShowPortfolioDialog( PortfolioDataSource portfolioDataSource )
        {
            var service = this.GetService<IDialogService>( "SelectPortfolioService" );


            // Tony: https://www.devexpress.com/Support/Center/Question/Details/T315317/how-to-pass-param-to-dialog-in-wpf-mvvm
            // The dialog that's shown has its own DataContext bind to SymbolSelectViewModel and I am creating a ViewModel here.
            // So there are two view model and the second view model is empty without 


            var portfolioVM = PortfolioPickerWindowViewModel.Create();

            portfolioVM.PortfolioDataSource = portfolioDataSource;

            UICommand registerCommand = new UICommand()
            {
                Caption = "Okay",
                IsCancel = false,
                IsDefault = true,
                Command = new DevExpress.Mvvm.DelegateCommand<CancelEventArgs>( x => myExecuteMethod(), x => portfolioVM.SelectedPortfolio != null ),
            };

            UICommand cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false,
            };

            UICommand result = service.ShowDialog(
                                                        dialogCommands: new PooledList<UICommand>() { registerCommand, cancelCommand },
                                                        title: "Select Portfolio",
                                                        documentType: "PortfolioPickerView",
                                                        viewModel: portfolioVM,
                                                        parameter: null,
                                                        parentViewModel: this
                                                      );

            if ( result == cancelCommand || result == null )
            {
                return false;
            }

            UserConfig.SetDelayValue( "PortfolioPickerView", () => GuiDispatcher.GlobalDispatcher.AddSyncAction( () => portfolioVM.Save() ) );

            _selectedPortfolio = portfolioVM.SelectedPortfolio;

            return true;
        }




        //private void DialogObject_QueryView( object sender, DevExpress.Utils.MVVM.Services.QueryViewEventArgs e )
        //{
        //    throw new NotImplementedException( );
        //}

        public void ShowDialog()
        {
            // Tony: https://www.devexpress.com/Support/Center/Question/Details/T315317/how-to-pass-param-to-dialog-in-wpf-mvvm
            // The dialog that's shown has its own DataContext bind to SymbolSelectViewModel and I am creating a ViewModel here.
            // So there are two view model and the second view model is empty without 


            _symbolSelectViewModel = SymbolSelectViewModel.Create();

            _symbolSelectViewModel.SecurityProvider = ServicesRegistry.SecurityProvider;

            UICommand registerCommand = new UICommand()
            {
                Caption = "Okay",
                IsCancel = false,
                IsDefault = true,
                Command = new DevExpress.Mvvm.DelegateCommand<CancelEventArgs>( x => myExecuteMethod(), x => _symbolSelectViewModel.IsEnable ),
            };

            UICommand cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false,
            };

            IDialogService service = this.GetService<IDialogService>( "SelectSymbolService" );

            UICommand result = service.ShowDialog(
                                                            dialogCommands: new PooledList<UICommand>() { registerCommand, cancelCommand },
                                                            title: "Select Symbol",
                                                            viewModel: _symbolSelectViewModel
                                                        );

            if ( result == cancelCommand || result == null )
            {
                return;
            }

            var newSetting = _symbolSelectViewModel.Save();

            UserConfig.SetDelayValue( "SymbolSelectView", () => GuiDispatcher.GlobalDispatcher.AddSyncAction( () => _symbolSelectViewModel.Save() ) );

            _selectedSecurities = _symbolSelectViewModel.Securities.LookupAll();

            _selectedPortfolio = _symbolSelectViewModel.portfolioVM.SelectedPortfolio;

            _isBarIntegrityCheck = _symbolSelectViewModel.IsBarIntegrityCheck;

            _showAllTimeFrameCharts = _symbolSelectViewModel.ShowAllTimeFrameCharts;

            if ( _selectedPortfolio == null )
            {
                bool setupOkay = false;
                var PortfolioSource = ConfigManager.GetService<PortfolioDataSource>();

                setupOkay = ShowPortfolioDialog( PortfolioSource );

                if ( !setupOkay )
                {
                    return;
                }
            }

            _security = _selectedSecurities.FirstOrDefault();

            OrderSettings.Security = _security;

            var selectedTF = _symbolSelectViewModel.GetSelectedTimeFrames();

            Messenger.Default.Send( new SelectSecurityMessage( _security, selectedTF ) );

            SplashScreenService.ShowSplashScreen();
            SplashScreenService.SetSplashScreenState( "Initializing View Models..." );
            SplashScreenService.SetSplashScreenProgress( 1, 100 );

            _symbolSelected = true;


            if ( _symbolSelectViewModel.TradingMode == TradingMode.LIVETRADING )
            {
                StartRingBuffer();
                _liveTradingConnector.CandleSeriesProcessing += RingBufferCandleSeriesProcessing;

                CreateLiveTrading();
            }
            else if ( _symbolSelectViewModel.TradingMode == TradingMode.BACKTESTING )
            {
                if ( _selectedPortfolio != null )
                {
                    var storageRegistry = ServicesRegistry.StorageRegistry;

                    _backTestingConnector = new fxHistoricEmulationConnector( new[ ] { _security }, new[ ] { _selectedPortfolio } )
                    {
                        HistoryMessageAdapter =
                                                {
                                                    StorageRegistry = storageRegistry,
                                                    StorageFormat   = StorageFormats.Binary,
                                                    StartDate       = _symbolSelectViewModel.StartDate,
                                                    StopDate        = _symbolSelectViewModel.EndDate
                                                },
                        LogLevel = LogLevels.Info
                    };

                    _candleManager = new CandleManager( _backTestingConnector );

                    ServicesRegistry.LogManager.Sources.Add( _backTestingConnector );
                }

                CreateBackTesting( _symbolSelectViewModel.StartDate, _symbolSelectViewModel.EndDate );
            }
        }



        private void CreateBackTesting( DateTime startDate, DateTime endDate )
        {
            if ( _symbolSelectViewModel.Sec01 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromSeconds( 1 ), "1 Sec", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Min01 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromMinutes( 1 ), "1 Min", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Min04 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromMinutes( 4 ), "4 Min", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Min05 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromMinutes( 5 ), "5 Min", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Min15 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromMinutes( 15 ), "15 Min", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Min30 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromMinutes( 30 ), "30 Min", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Hrs01 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromHours( 1 ), "1 Hour", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Hrs02 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromHours( 2 ), "2 Hour", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Hrs03 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromHours( 3 ), "3 Hour", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Hrs04 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromHours( 4 ), "4 Hour", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Hrs06 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromHours( 6 ), "6 Hour", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Hrs08 )
            {
                InitializeBackTestingViewModel( TimeSpan.FromHours( 8 ), "8 Hour", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Daily )
            {
                InitializeBackTestingViewModel( TimeSpan.FromDays( 1 ), "Daily", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Weekly )
            {
                InitializeBackTestingViewModel( TimeSpan.FromDays( 7 ), "Weekly", startDate, endDate );
            }

            if ( _symbolSelectViewModel.Monthly )
            {
                InitializeBackTestingViewModel( TimeSpan.FromDays( 30 ), "Monthly", startDate, endDate );
            }
        }

        private void InitializeBackTestingViewModel( TimeSpan period, string periodString, DateTime startDate, DateTime endDate )
        {
            string msg = "Initializing " + periodString + " backtesting...";

            SplashScreenService.SetSplashScreenState( msg );

            var testVM = CreateBackTesterViewModel( period, periodString, startDate, endDate );
            testVM.TabActivated += OnTabActivated;
            _lastTabViewModel = testVM;
            testVM.DoneDownloadBarsEvent += OnDoneDownloadBarsEvent;

            if ( period < TimeSpan.FromDays( 1 ) )
            {
                _intradayViewModels.Add( testVM );
            }

            _allVisibleViewModels.Add( testVM );

            ChildViews.Add( testVM );
        }

        private void CreateLiveTrading()
        {
            if ( _symbolSelectViewModel.Sec01 )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromSeconds( 1 ), "1 Sec" );
            }

            if ( _symbolSelectViewModel.Min01 )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromMinutes( 1 ), "1 Min", _symbolSelectViewModel.LoadAll01Min );
            }

            if ( _symbolSelectViewModel.Min04 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromMinutes( 4 ), "4 Min" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromMinutes( 4 ), "4 Min" );
                }

            }

            if ( _symbolSelectViewModel.Min05 )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromMinutes( 5 ), "5 Min" );
            }

            if ( _symbolSelectViewModel.Min15 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromMinutes( 15 ), "15 Min" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromMinutes( 15 ), "15 Min" );
                }
            }

            if ( _symbolSelectViewModel.Min30 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromMinutes( 30 ), "30 Min" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromMinutes( 30 ), "30 Min" );
                }
            }

            if ( _symbolSelectViewModel.Hrs01 )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromHours( 1 ), "1 Hour", _symbolSelectViewModel.LoadAllHourly );
            }

            if ( _symbolSelectViewModel.Hrs02 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromHours( 2 ), "2 Hour" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromHours( 2 ), "2 Hour" );
                }
            }

            if ( _symbolSelectViewModel.Hrs03 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromHours( 3 ), "3 Hour" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromHours( 3 ), "3 Hour" );
                }
            }

            if ( _symbolSelectViewModel.Hrs04 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromHours( 4 ), "4 Hour" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromHours( 4 ), "4 Hour" );
                }

            }

            if ( _symbolSelectViewModel.Hrs06 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromHours( 6 ), "6 Hour" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromHours( 6 ), "6 Hour" );
                }
            }

            if ( _symbolSelectViewModel.Hrs08 )
            {
                if ( _showAllTimeFrameCharts )
                {
                    InitializeVisibleLiveTradingViewModel( TimeSpan.FromHours( 8 ), "8 Hour" );
                }
                else
                {
                    InitializeNONVisualLiveTradingViewModel( TimeSpan.FromHours( 8 ), "8 Hour" );
                }

            }

            if ( _symbolSelectViewModel.Daily )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromDays( 1 ), "Daily", _symbolSelectViewModel.LoadAllHourly );
            }

            if ( _symbolSelectViewModel.Weekly )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromDays( 7 ), "Weekly" );
            }

            if ( _symbolSelectViewModel.Monthly )
            {
                InitializeVisibleLiveTradingViewModel( TimeSpan.FromDays( 30 ), "Monthly" );
            }
        }


        private void InitializeVisibleLiveTradingViewModel( TimeSpan period, string periodString, bool loadAll = false )
        {
            string msg = "Initializing " + periodString + " Live Trading UI...";

            SplashScreenService.SetSplashScreenState( msg );

            _stopWatch.Start();

            var viewModel = CreateViewModel( period, periodString, loadAll );
            viewModel.TabActivated += OnTabActivated;
            viewModel.CandlesLoadedEvent += OnCandlesLoaded;
            _lastTabViewModel = viewModel;
            viewModel.DoneDownloadBarsEvent += OnDoneDownloadBarsEvent;

            if ( period < TimeSpan.FromDays( 1 ) )
            {
                _intradayViewModels.Add( viewModel );
            }

            _allVisibleViewModels.Add( viewModel );

            ChildViews.Add( viewModel );
        }

        private void InitializeNONVisualLiveTradingViewModel( TimeSpan period, string periodString )
        {
            string msg = "Initializing " + periodString + " Live Trading Data Only...";

            SplashScreenService.SetSplashScreenState( msg );

            _stopWatch.Start();

            var viewModel = CreateNonVisualViewModel( period, periodString );

            viewModel.CandlesLoadedEvent += OnCandlesLoaded;

            viewModel.DoneDownloadBarsEvent += OnDoneDownloadBarsEvent;

            if ( period < TimeSpan.FromDays( 1 ) )
            {
                _intradayViewModels.Add( viewModel );
            }

            _nonVisibleViewModels.Add( viewModel );

            // Here I need to add code to start the loading of databar 
            // Need to take care of Step9

            Task first = new Task( () => viewModel.Step3_LoadCandlesFromLocalStorage_NonVisual(), TradeStationExitToken );

            first.Start();
        }

        private IChartTabViewModel CreateViewModel( TimeSpan period, string periodString, bool loadAll )
        {
            IChartTabViewModel output = null;
            if ( period == TimeSpan.FromDays( 30 ) )
            {
                output = _monthlyVm = _factory.Create( this, periodString, "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromTicks( 25920000000000L ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                output = _weeklyVm = _factory.Create( this, periodString, "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromTicks( 6048000000000L ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                output = _dailyVm = _factory.Create( this, "Daily", "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromDays( 1.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                output = _08hrsVm = _factory.Create( this, "8 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 8.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                output = _06hrsVm = _factory.Create( this, "6 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 6.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                output = _04hrsVm = _factory.Create( this, "4 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 4.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                output = _03hrsVm = _factory.Create( this, "3 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 3.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                output = _02hrsVm = _factory.Create( this, "2 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 2.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                output = _01hrsVm = _factory.Create( this, "1 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 1.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                output = _30MinVm = _factory.Create( this, "30 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 30.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                output = _15MinVm = _factory.Create( this, "15 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 15.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                output = _05MinVm = _factory.Create( this, "5 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 5.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                output = _04MinVm = _factory.Create( this, "4 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 4.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                output = _01MinVm = _factory.Create( this, "1 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 1.0 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }
            else if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                output = _01secVm = _factory.Create( this, "1 Sec", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromSeconds( 1 ), _security, Connector, _isBarIntegrityCheck, loadAll, _cancelToken );
            }



            ( ( ISupportParentViewModel )output ).ParentViewModel = this;

            return output;
        }

        private IChartTabViewModel CreateNonVisualViewModel( TimeSpan period, string periodString )
        {
            IChartTabViewModel output = null;
            if ( period == TimeSpan.FromDays( 30 ) )
            {
                output = _monthlyVm = _factory.CreateNonVisual( this, periodString, "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromTicks( 25920000000000L ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                output = _weeklyVm = _factory.CreateNonVisual( this, periodString, "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromTicks( 6048000000000L ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                output = _dailyVm = _factory.CreateNonVisual( this, "Daily", "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromDays( 1.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                output = _08hrsVm = _factory.CreateNonVisual( this, "8 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 8.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                output = _06hrsVm = _factory.CreateNonVisual( this, "6 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 6.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                output = _04hrsVm = _factory.CreateNonVisual( this, "4 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 4.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                output = _03hrsVm = _factory.CreateNonVisual( this, "3 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 3.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                output = _02hrsVm = _factory.CreateNonVisual( this, "2 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 2.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                output = _01hrsVm = _factory.CreateNonVisual( this, "1 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 1.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                output = _30MinVm = _factory.CreateNonVisual( this, "30 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 30.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                output = _15MinVm = _factory.CreateNonVisual( this, "15 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 15.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                output = _05MinVm = _factory.CreateNonVisual( this, "5 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 5.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                output = _04MinVm = _factory.CreateNonVisual( this, "4 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 4.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                output = _01MinVm = _factory.CreateNonVisual( this, "1 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 1.0 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }
            else if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                output = _01secVm = _factory.CreateNonVisual( this, "1 Sec", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromSeconds( 1 ), _security, Connector, _isBarIntegrityCheck, _cancelToken );
            }

            ( ( ISupportParentViewModel )output ).ParentViewModel = this;

            return output;
        }





        private IChartTabViewModel CreateAltViewModel( TimeSpan period, string periodString )
        {
            IChartTabViewModel output = null;

            if ( period == TimeSpan.FromDays( 1 ) )
            {
                var count = _dailyAltVms.Count + 1; // Main Count + whatever number of alt count

                var name = "Daily Alt - " + count;

                output = _factory.CreateAlt( this, name, "SvgImages/Business Objects/BO_State.svg", TimeSpan.FromDays( 1.0 ), _security, Connector, _isBarIntegrityCheck, count + 1, _cancelToken );

                _dailyAltVms.Add( output );
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                var count = _01hrsAltVms.Count + 1; // Main Count + whatever number of alt count

                var name = "1 hr Alt - " + count;

                output = _factory.CreateAlt( this, name, "SvgImages/Business Objects/BO_State.svg", TimeSpan.FromHours( 1.0 ), _security, Connector, _isBarIntegrityCheck, count + 1, _cancelToken );

                _01hrsAltVms.Add( output );
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                var count = _01MinAltVms.Count + 1; // Main Count + whatever number of alt count

                var name = "1 Min Alt - " + count;

                output = _factory.CreateAlt( this, name, "SvgImages/Business Objects/BO_State.svg", TimeSpan.FromMinutes( 1.0 ), _security, Connector, _isBarIntegrityCheck, count + 1, _cancelToken );

                _01MinAltVms.Add( output );
            }
            else
            {
                return null;
            }


            ( ( ISupportParentViewModel )output ).ParentViewModel = this;

            return output;
        }

        private IChartTabViewModel CreateBackTesterViewModel( TimeSpan period, string periodString, DateTime startDate, DateTime endDate )
        {
            IChartTabViewModel output = null;
            if ( period == TimeSpan.FromDays( 30 ) )
            {
                output = _monthlyVm = _factory.Create( this, periodString, "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromTicks( 25920000000000L ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                output = _weeklyVm = _factory.Create( this, periodString, "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromTicks( 6048000000000L ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                output = _dailyVm = _factory.Create( this, "Daily", "SvgImages/Outlook Inspired/NotStarted.svg", TimeSpan.FromDays( 1.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                output = _08hrsVm = _factory.Create( this, "8 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 8.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                output = _06hrsVm = _factory.Create( this, "6 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 6.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                output = _04hrsVm = _factory.Create( this, "4 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 4.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                output = _03hrsVm = _factory.Create( this, "3 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 3.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                output = _02hrsVm = _factory.Create( this, "2 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 2.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                output = _01hrsVm = _factory.Create( this, "1 hr", "SvgImages/Outlook Inspired/Deferred.svg", TimeSpan.FromHours( 1.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                output = _30MinVm = _factory.Create( this, "30 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 30.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                output = _15MinVm = _factory.Create( this, "15 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 15.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                output = _05MinVm = _factory.Create( this, "5 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 5.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                output = _04MinVm = _factory.Create( this, "4 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 4.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                output = _01MinVm = _factory.Create( this, "1 Min", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromMinutes( 1.0 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }
            else if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                output = _01secVm = _factory.Create( this, "1 Sec", "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromSeconds( 1 ), _security, _backTestingConnector, _candleManager, _selectedPortfolio, startDate, endDate, _cancelToken );
            }

            ( ( ISupportParentViewModel )output ).ParentViewModel = this;

            return output;
        }

        private void OnDoneDownloadBarsEvent( IChartTabViewModel vm )
        {
            if ( vm == _lastTabViewModel )
            {
                DispatcherService.BeginInvoke
                (
                    () =>
                        {
                            SplashScreenService.SetSplashScreenState( "Done Initialization." );
                            Thread.Sleep( TimeSpan.FromSeconds( 1 ) );
                            SplashScreenService.HideSplashScreen();

                            if ( _01MinVm != null && _lastTabViewModel != _01MinVm )
                            {
                                _01MinVm.IsActive = true;

                                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                                HewManager elliottWaveManager = null;

                                if ( aa != null )
                                {
                                    elliottWaveManager = ( HewManager )aa.HewManager;
                                }
                                else
                                {
                                    return;
                                }

                                elliottWaveManager.SwitchTimeFrame( this, _01MinVm.ResponsibleTF );

                                _selectedUndoRedoArea = elliottWaveManager.GetSelectedUndoRedoArea( _01MinVm.ResponsibleTF );
                            }

                            /* -------------------------------------------------------------------------------------------------------------------------------------------
                             * 
                             *  Now that all the tabs are done loading, we can load previous Settings for TradeStation
                             * 
                             * ------------------------------------------------------------------------------------------------------------------------------------------- */
                            StudioUserConfig userConfig = UserConfig;

                            userConfig.SuspendChangesMonitor();
                            BaseUserConfig<StudioUserConfig>.Instance.TryLoadSettings( "TradeStationChartsSettings", new Action<SettingsStorage>( Load ) );
                            userConfig.ResumeChangesMonitor();
                        }
                );
            }

            if ( vm == _01MinVm && _lastTabViewModel != _01MinVm )
            {
                if ( _01MinVm.IsActive )
                {
                    DispatcherService.BeginInvoke
                                                (
                                                    () =>
                                                    {
                                                        /* -------------------------------------------------------------------------------------------------------------------------------------------
                                                         * 
                                                         *  Tony: Don't know why the following will cost the system to crash and exit.
                                                         * 
                                                         * ------------------------------------------------------------------------------------------------------------------------------------------- */

                                                        _selectedViewModel = _01MinVm;
                                                        ShowElliottWave = true;
                                                        //ShowSmallTradingEvent = true;
                                                        ShowDivergence = true;
                                                    }
                                                );

                }
            }


        }


        public void ResetVariables()
        {
            foreach ( IChartTabViewModel vm in _allVisibleViewModels )
            {
                vm.StopTimerThread();
                vm.UnRegisterCommandsAndEvents();
            }
            _activatedCount = 0;
        }

        private void OnCandlesLoaded( IChartTabViewModel vm )
        {
            _stopWatch.Stop();

            string msg = string.Format( "Candles Loaded .... Time Take = {0} ms", _stopWatch.Elapsed.TotalMilliseconds );

            DispatcherService.BeginInvoke( () =>
            {
                if ( vm != null )
                {
                    SplashScreenService.SetSplashScreenState( msg );
                }

                SplashScreenService.SetSplashScreenProgress( 50, 100 );
            } );
        }

        private void OnTabActivated( IChartTabViewModel vm )
        {
            _stopWatch.Stop();

            string msg = string.Format( "UI Loaded .... Time Take = {0} ms.\n", _stopWatch.Elapsed.TotalMilliseconds );

            if ( _allVisibleViewModels.Count == 1 )
            {
                vm.IsActive = true;

                SplashScreenService.SetSplashScreenProgress( 100, 100 );

                DispatcherService.BeginInvoke( () => { SplashScreenService.SetSplashScreenState( msg ); } );

                _stopWatch.Start();
            }


            if ( _activatedCount < _allVisibleViewModels.Count - 1 )
            {
                _activatedCount++;

                var nextVM = _allVisibleViewModels[_activatedCount];

                double result = ( _activatedCount / ( double )_allVisibleViewModels.Count ) * 100.0;

                DispatcherService.BeginInvoke( () =>
                {
                    if ( nextVM != null )
                    {
                        msg += "Initializing " + nextVM.ResponsibleTF.ToReadable() + " Live Trading UI...";

                        _stopWatch.Start();

                        SplashScreenService.SetSplashScreenState( msg );
                    }

                    SplashScreenService.SetSplashScreenProgress( result, 100 );
                } );

                if ( nextVM != null )
                {
                    nextVM.IsActive = true;

                    if ( nextVM == _lastTabViewModel )
                    {
                        DispatcherService.BeginInvoke( () => { SplashScreenService.SetSplashScreenState( msg ); } );
                    }
                }
            }
            else
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                HewManager elliottWaveManager = null;

                if ( aa != null )
                {
                    elliottWaveManager = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }

                elliottWaveManager.SwitchTimeFrame( this, vm.ResponsibleTF );

                _selectedUndoRedoArea = elliottWaveManager.GetSelectedUndoRedoArea( vm.ResponsibleTF );
            }
        }

        //public IChartTabViewModel GetNextViewModel( IChartTabViewModel vm )
        //{
        //    if ( vm == _01secVm )
        //    {
        //        return _01MinVm;
        //    }
        //    else if ( vm == _01MinVm )
        //    {
        //        return _04MinVm;
        //    }
        //    else if ( vm == _04MinVm )
        //    {
        //        return _05MinVm;
        //    }
        //    else if ( vm == _05MinVm )
        //    {
        //        return _15MinVm;
        //    }
        //    else if ( vm == _15MinVm )
        //    {
        //        return _30MinVm;
        //    }
        //    else if ( vm == _30MinVm )
        //    {
        //        return _01hrsVm;
        //    }
        //    else if ( vm == _01hrsVm )
        //    {
        //        return _02hrsVm;
        //    }
        //    else if ( vm == _02hrsVm )
        //    {
        //        return _03hrsVm;
        //    }
        //    else if ( vm == _03hrsVm )
        //    {
        //        return _04hrsVm;
        //    }
        //    else if ( vm == _04hrsVm )
        //    {
        //        return _06hrsVm;
        //    }
        //    else if ( vm == _06hrsVm )
        //    {
        //        return _08hrsVm;
        //    }
        //    else if ( vm == _08hrsVm )
        //    {
        //        return _dailyVm;
        //    }
        //    else if ( vm == _dailyVm )
        //    {
        //        return _weeklyVm;
        //    }
        //    else if ( vm == _weeklyVm )
        //    {
        //        return _monthlyVm;
        //    }

        //    return null;
        //}


        public void CloseDocument()
        {
            _selectSymbolDoc.Close();
        }

        public ObservableCollection<object> ChildViews
        {
            get
            {
                return _childViews;
            }
            set
            {
                _childViews = value;
            }
        }

        public string Symbol
        {
            get
            {
                return _security.Code;
            }
        }

        public Security Security
        {
            get
            {
                return _security;
            }
        }

        public void CheckAndShowFibonacci()
        {
            _selectedViewModel.ChartVM.CheckAndShowFibonacci();

            _selectedViewModel.Refresh();

            _selectedUndoRedoArea.Commit();
        }

        public void AddWaveOne()
        {
            using ( _selectedUndoRedoArea.Start( "Add Wave1" ) )
            {
                _selectedViewModel.AddWaveOneToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.Wave1 );
                CheckAndShowFibonacci();
            }
        }

        public void AddWaveTwo()
        {
            using ( _selectedUndoRedoArea.Start( "Add Wave2" ) )
            {
                _selectedViewModel.AddWaveTwoToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.Wave2 );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveThree()
        {
            using ( _selectedUndoRedoArea.Start( "Add Wave3" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.Wave3 );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveFour()
        {
            using ( _selectedUndoRedoArea.Start( "Add Wave4" ) )
            {
                _selectedViewModel.AddWaveFourToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveFive()
        {
            using ( _selectedUndoRedoArea.Start( "Add Wave5" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.Wave5 );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveA()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveA" ) )
            {
                _selectedViewModel.AddWaveAToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveA );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveB()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveB" ) )
            {
                _selectedViewModel.AddWaveBToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveB );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveC()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveC" ) )
            {
                _selectedViewModel.AddWaveCToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveC );

                CheckAndShowFibonacci();
            }
        }

        public void AddTriangleWaveA()
        {
            using ( _selectedUndoRedoArea.Start( "Add Triangle Wave A" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveTA );

                CheckAndShowFibonacci();
            }
        }

        public void AddTriangleWaveB()
        {
            using ( _selectedUndoRedoArea.Start( "Add Triangle Wave B" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveTB );

                CheckAndShowFibonacci();
            }
        }

        public void AddTriangleWaveC()
        {
            using ( _selectedUndoRedoArea.Start( "Add Triangle Wave C" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveTC );

                CheckAndShowFibonacci();
            }
        }

        public void AddTriangleWaveD()
        {
            using ( _selectedUndoRedoArea.Start( "Add Triangle Wave D" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveTD );

                CheckAndShowFibonacci();
            }
        }

        public void AddTriangleWaveE()
        {
            using ( _selectedUndoRedoArea.Start( "Add Triangle Wave E" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveTE );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveW()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveW" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveW );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveX()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveX" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveX );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveY()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveY" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveY );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveZ()
        {
            using ( _selectedUndoRedoArea.Start( "Add WaveZ" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveZ );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveEFA()
        {
            using ( _selectedUndoRedoArea.Start( "Add Expanded Flat A" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveEFA );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveEFB()
        {
            using ( _selectedUndoRedoArea.Start( "Add Expanded Flat B" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveEFB );

                CheckAndShowFibonacci();
            }
        }

        public void AddWaveEFC()
        {
            using ( _selectedUndoRedoArea.Start( "Add Expanded Flat C" ) )
            {
                _selectedViewModel.AddWaveToChart( _selectedViewModel.WaveScenarioNumber, _defaultWaveCycle, ElliottWaveEnum.WaveEFC );

                CheckAndShowFibonacci();
            }
        }

        public void CycleWaveUp()
        {
            using ( _selectedUndoRedoArea.Start( "CycleUp Waves" ) )
            {
                _selectedViewModel.CycleUpSelectedBar( _selectedViewModel.WaveScenarioNumber );

                _selectedViewModel.Refresh();

                _selectedUndoRedoArea.Commit();
            }
        }

        public void CycleWaveDown()
        {
            using ( _selectedUndoRedoArea.Start( "CycleWaveDown Waves" ) )
            {
                _selectedViewModel.CycleDownSelectedBar( _selectedViewModel.WaveScenarioNumber );

                _selectedViewModel.Refresh();

                _selectedUndoRedoArea.Commit();
            }
        }

        public void DeleteWaves()
        {
            using ( _selectedUndoRedoArea.Start( "Delete Wave" ) )
            {
                _selectedViewModel.RemoveWavesFromManagerAndBar( _selectedViewModel.WaveScenarioNumber, _selectedViewModel.ResponsibleTF );

                _selectedViewModel.Refresh();

                _selectedUndoRedoArea.Commit();
            }
        }

        public void LockFibLevels()
        {
            _selectedViewModel.ChartVM.LockFibLevelsObject();
        }

        

        public void DeleteAllLockFibLevels()
        {
            _selectedViewModel.ChartVM.DeleteAllLockFibLevels();
        }
       

        public void SyncWavesUp()
        {
            using ( _selectedUndoRedoArea.Start( "Sync Waves Up" ) )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                HewManager elliottWaveManager = null;

                if ( aa != null )
                {
                    elliottWaveManager = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }

                elliottWaveManager.SyncWavesUpHigherTimeFrame( _selectedViewModel.WaveScenarioNumber, _selectedViewModel.ResponsibleTF );

                _selectedUndoRedoArea.Commit();
            }
        }



        public void SyncWavesDown()
        {
            using ( _selectedUndoRedoArea.Start( "Sync Waves Down" ) )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                HewManager elliottWaveManager = null;

                if ( aa != null )
                {
                    elliottWaveManager = ( HewManager )aa.HewManager;
                }
                else
                {
                    return;
                }

                elliottWaveManager.SyncWavesDownSmallerTimeFrame( _selectedViewModel.WaveScenarioNumber, _selectedViewModel.ResponsibleTF, true );

                _selectedUndoRedoArea.Commit();
            }
        }

        public void FindBarOnLowerTF()
        {
            //using ( _selectedUndoRedoArea.Start( "Find and Load Wave Lower TF" ) )
            //{
            //    _selectedViewModel.FindAndLoadDatabarsLowerTimeFrame( );

            //    _selectedUndoRedoArea.Commit( );
            //}
        }

        public void SaveWavesToDB()
        {
            Task first = new Task( () => TaskSaveElliottWavesToDB(), TradeStationExitToken );

            first.Start();
        }

        public void LockWavesInDB()
        {
            Task first = new Task( () => TaskLockWavesInDB(), TradeStationExitToken );

            first.Start();
        }

        public void AnalysisWave()
        {
            Task first = new Task( () => TaskAnalysisWave(), TradeStationExitToken );

            first.Start();
        }

        private void TaskAnalysisWave()
        {
            var symbol = _security;

            _selectedViewModel.AnalysisWave();
        }

        public void fxRedo()
        {
            Redo( _selectedViewModel.ResponsibleTF );

            _selectedViewModel.Refresh();
        }

        public void Redo( TimeSpan period )
        {
            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            HewManager elliottWaveManager = null;

            if ( aa != null )
            {
                elliottWaveManager = ( HewManager )aa.HewManager;
            }
            else
            {
                return;
            }

            var selectedUndoRedo = elliottWaveManager.GetSelectedUndoRedoArea( period );

            if ( selectedUndoRedo != null )
            {
                selectedUndoRedo.Redo();
            }
        }

        public void fxUndo()
        {
            Undo( _selectedViewModel.ResponsibleTF );

            _selectedViewModel.Refresh();
        }

        public void Undo( TimeSpan period )
        {
            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            HewManager elliottWaveManager = null;

            if ( aa != null )
            {
                elliottWaveManager = ( HewManager )aa.HewManager;
            }
            else
            {
                return;
            }

            var selectedUndoRedo = elliottWaveManager.GetSelectedUndoRedoArea( period );

            if ( selectedUndoRedo != null )
            {
                selectedUndoRedo.Undo();
            }
        }



        private void TaskSaveElliottWavesToDB()
        {
            var symbol = _security;

            ////ThreadHelper.UpdateThreadName( "TaskSaveElliottWavesToDB" );

            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            HewManager elliottWaveManager = null;

            if ( aa != null )
            {
                elliottWaveManager = ( HewManager )aa.HewManager;
            }
            else
            {
                return;
            }

            using ( _selectedUndoRedoArea.Start( "Save Waves" ) )
            {
                elliottWaveManager.SaveElliottWaveToDatabase( TimeSpan.FromDays( 30 ) );
                this.AddInfoLog( "Saving Monthly Waves......Done" );

                elliottWaveManager.SaveElliottWaveToDatabase( TimeSpan.FromDays( 7 ) );
                this.AddInfoLog( "Saving Weekly Waves......Done" );

                elliottWaveManager.SaveElliottWaveToDatabase( TimeSpan.FromDays( 1 ) );
                this.AddInfoLog( "Saving Daily Waves......Done" );

                elliottWaveManager.SaveElliottWaveToDatabase( TimeSpan.FromHours( 1 ) );
                this.AddInfoLog( "Saving 1-Hr Waves......Done" );

                elliottWaveManager.SaveElliottWaveToDatabase( TimeSpan.FromMinutes( 1 ) );
                this.AddInfoLog( "Saving 1-Min Waves......Done" );
            }
        }


        /// <summary>
        /// Tony: Here I am going to start writing code to detect Wave Pattern.
        /// </summary>
        public void WaveAnalysis()
        {

        }


        private void TaskLockWavesInDB()
        {
            var symbol = _security;

            //ThreadHelper.UpdateThreadName( "TaskLockWavesInDB" );

            _selectedViewModel.LockWavesInDB();
        }


    }

    //public class KeyConverter : IEventArgsConverter
    //{
    //    #region IEventArgsConverter Members
    //    
    //    object IEventArgsConverter.Convert( object sender, object args )
    //    {            
    //        Key pressedKey = ( ( KeyEventArgs )args ).Key;
    //        return pressedKey;
    //    }
    //    #endregion
    //}
}
