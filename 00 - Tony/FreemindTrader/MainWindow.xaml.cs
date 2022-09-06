using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Ribbon;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Risk;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Storages.Csv;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Installer.IPC;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Community;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using FreemindTrader.Controls;
using FreemindTrader.Properties;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

using FreemindAITrade.View;
using FreemindAITrade;
using System.Reflection;
using DevExpress.Mvvm.UI;
using fx.Algorithm;
using fx.Common;
using fx.Bars;
using fx.Definitions;
using DevExpress.Mvvm;

namespace FreemindTrader
{
    public partial class MainWindow : DXRibbonWindow, IInstallerChannelHandler, IComponentConnector
    {
        public static RoutedCommand ConnectorSettingsCommand = new RoutedCommand();
        public static RoutedCommand ConnectDisconnectCommand = new RoutedCommand();
        public static RoutedCommand HelpCommand = new RoutedCommand();
        public static RoutedCommand AboutCommand = new RoutedCommand();
        public static RoutedCommand QuestionCommand = new RoutedCommand();
        public static RoutedCommand CommonSettingsCommand = new RoutedCommand();
        public static RoutedCommand TargetPlatformCommand = new RoutedCommand();
        public static RoutedCommand ResetSettingsCommand = new RoutedCommand();
        public static RoutedCommand SecuritiesMappingCommand = new RoutedCommand();
        public static RoutedCommand PortfoliosMappingCommand = new RoutedCommand();
        public static RoutedCommand EmulationSettingsCommand = new RoutedCommand();
        public static RoutedCommand AddWorkAreaCommand = new RoutedCommand();
        public static RoutedCommand RenameAreaCommand = new RoutedCommand();
        public static RoutedCommand BugReportCommand = new RoutedCommand();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private StudioConnector _connector;
        private readonly IEntityRegistry _entityRegistry;
        private SubscriptionControlManager _subscriptionManager;
        private LayoutManager _layoutManager;
        private EmulationSettingsEx _emulationSettings;
        private readonly InstallerChannel _installerChannel;
        private bool _closeSuccess;
        private bool _firstSecuritiesInit;
        private bool _firstPortfoliosInit;

        public static string RenameCommandTitle
        {
            get
            {
                return DockingLocalizer.GetString( DockingStringId.MenuItemRename );
            }
        }

        private static StudioUserConfig UserConfig
        {
            get
            {
                return BaseUserConfig<StudioUserConfig>.Instance;
            }
        }

        public MainWindow()
        {
            DXSplashScreen.SetState( LocalizedStrings.Initializing );
            UserConfig.SuspendChangesMonitor();
            _entityRegistry = new CsvEntityRegistry( Paths.AppDataPath );

            var tradeObjectsStorage    = _entityRegistry;
            var studioCommandService   = ( IStudioCommandService   ) new StudioCommandService();
            var securityIdStorage      = ( INativeIdStorage        ) new CsvNativeIdStorage( Paths.SecurityNativeIdDir )       { DelayAction = _entityRegistry.DelayAction };
            var securityIdMapping      = ( ISecurityMappingStorage ) new CsvSecurityMappingStorage( Paths.SecurityMappingDir ) { DelayAction = _entityRegistry.DelayAction };
            var extendedInfoStorageSvc = ( IExtendedInfoStorage    ) new CsvExtendedInfoStorage( Paths.SecurityExtendedInfo )  { DelayAction = _entityRegistry.DelayAction };

            ConfigManager.RegisterService( tradeObjectsStorage    );
            ConfigManager.RegisterService( studioCommandService   );
            ConfigManager.RegisterService( securityIdStorage      );
            ConfigManager.RegisterService( securityIdMapping      );
            ConfigManager.RegisterService( extendedInfoStorageSvc );


            AppStartSettings appStartSettings = AppStartSettings.TryLoad();
            Helper.RegisterServices( appStartSettings == null || appStartSettings.Online, GetType() );


            ConfigManager.RegisterService( "SecuritiesSelectWindowType", typeof( SecuritiesWindowEx ) );
            StockSharp.Studio.Controls.Extensions.InitServices();

            ConfigManager.RegisterService( new SnapshotRegistry( Paths.SnapshotsDir ) );
            StorageExchangeInfoProvider exchangeInfoProvider = new StorageExchangeInfoProvider( _entityRegistry, false );
            ConfigManager.RegisterService< IExchangeInfoProvider >( exchangeInfoProvider                          );
            ConfigManager.RegisterService< IMarketDataDrive      >( new LocalMarketDataDrive( "t:\\ForexData\\" ) );
            ConfigManager.RegisterService< IBackupService        >( new StockSharpBackupService()                 );
            ConfigManager.RegisterService< IStorageRegistry      >( new StorageRegistry( exchangeInfoProvider ) { DefaultDrive = ConfigManager.GetService<IMarketDataDrive>() } );

            InitializeDriveCache();
            InitializeComponent();

            Title       = Paths.AppNameWithVersion;
            LoadMainWindowSettings();
            var manager = UserConfig.LogConfig.Manager;

            manager.Listeners.Add( StatusBarPanel );
            manager.Application.AddInfoLog( Paths.AppName );
            LoggingHelper.DoWithLog( new Func<IDictionary<object, Exception>>( _entityRegistry.Init ) );

            new Action( exchangeInfoProvider.Init ).DoWithLog();

            var securities           = ( ISecurityStorage                 ) _entityRegistry.Securities;
            var positionStorage      = ( IPositionStorage                 ) _entityRegistry.PositionStorage;
            var securityProvider     = ( ISecurityProvider                ) new FilterableSecurityProvider( _entityRegistry.Securities );
            var securityMsgProvider  = ( ISecurityMessageAdapterProvider  ) new CsvSecurityMessageAdapterProvider( Path.Combine( Paths.AppDataPath, "security_adapter.csv" ) );
            var portfolioMsgProvider = ( IPortfolioMessageAdapterProvider ) new CsvPortfolioMessageAdapterProvider( Path.Combine( Paths.AppDataPath, "portfolio_adapter.csv" ) );

            ConfigManager.RegisterService< ISecurityStorage                 > ( securities           );
            ConfigManager.RegisterService< IPositionStorage                 > ( positionStorage      );
            ConfigManager.RegisterService< ISecurityProvider                > ( securityProvider     );
            ConfigManager.RegisterService< ISecurityMessageAdapterProvider  > ( securityMsgProvider  );
            ConfigManager.RegisterService< IPortfolioMessageAdapterProvider > ( portfolioMsgProvider );

            INativeIdStorage nativeIdStorage         = ServicesRegistry.NativeIdStorage;
            ISecurityMappingStorage mappingStorage   = ServicesRegistry.MappingStorage;
            IExtendedInfoStorage extendedInfoStorage = ServicesRegistry.ExtendedInfoStorage;

            LoggingHelper.DoWithLog( new Func<IDictionary<string, Exception>>( nativeIdStorage.Init ) );
            LoggingHelper.DoWithLog( new Func<IDictionary<string, Exception>>( mappingStorage.Init ) );
            LoggingHelper.DoWithLog( new Func<IDictionary<IExtendedInfoStorageItem, Exception>>( extendedInfoStorage.Init ) );

            new Action( ServicesRegistry.SecurityAdapterProvider.Init ).DoWithLog();
            new Action( ServicesRegistry.PortfolioAdapterProvider.Init ).DoWithLog();

            IRiskManager service = new RiskManager();
            UserConfig.TryLoadSettings( "RiskManager", new Action<SettingsStorage>( service.Load ) );
            ConfigManager.RegisterService( service );
            
            UserConfig.GetCommonSettings<StudioCommonSettings>().UpdateGlobalTimeZone();

            InitializeLayoutManager();
            InitializeEmulationSettings();
            InitializeCommands();
            InitializeConnector();

            StockSharp.Studio.Controls.Extensions.InitAlerts();

            ThemeManager.ApplicationThemeChanged += ( s, e ) => UserConfig.SetTheme( ApplicationThemeHelper.ApplicationThemeName );
            //_installerChannel = new InstallerChannel( Helper.ProductId, this, manager.Application );

            ConfigManager.RegisterService<ISymbolsMgr>( SymbolsMgr.Instance );
        }

        public async Task<InstallerMessage> GetInstallerResponseAsync( InstallerMessage request, CancellationToken token )
        {
            MainWindow mainWindow = this;
            if ( !( request is MsgClose ) )
                return null;
            mainWindow._closeSuccess = false;

            await Ecng.Xaml.XamlHelper.GuiThreadGetAsync( new Action<CancellationToken>( x => GetInstallerResponseAsync( request, token ) ), CancellationToken.None );

            if ( !mainWindow._closeSuccess )
                throw new InvalidOperationException( "application was not closed" );
            return new MsgOk();
        }

        private void MainWindow_OnLoaded( object sender, RoutedEventArgs e )
        {
            DXSplashScreen.SetState( LocalizedStrings.LoadingRibbonControls );

            foreach ( ControlType controlType in BaseAppConfig<StudioAppConfig, StudioSection>.Instance.ToolControls.GetControlTypes() )
            {
                RibbonToolControlsGroup.AddToolControl( controlType, this );
            }

            CheckIfFractalAnalysisDllExist();

            UserConfig.ResumeChangesMonitor();
            DXSplashScreen.Close();

            /* -------------------------------------------------------------------------------------------------------------------------------------------        
            *  Step 1. Send LogInCommand 
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            new LogInCommand().Process( this, false );
        }

        private void MainWindow_OnClosing( object sender, CancelEventArgs e )
        {
            if ( !UserConfig.IsReseting )
            {
                if ( !this.ShowByeByeWindow( true ) )
                {
                    e.Cancel = true;
                    return;
                }
                _entityRegistry?.DelayAction.WaitFlush( true );

                foreach ( BaseStudioControl baseStudioControl in _layoutManager.DockingControls.OfType<BaseStudioControl>() )
                {
                    if ( baseStudioControl.CanClose( CloseReason.Shutdown ) != CloseAction.Close )
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                UserConfig.SaveRiskManager( ServicesRegistry.RiskManager.Save() );
                _layoutManager.Dispose();
                StudioServicesRegistry.CommandService.Dispose();
                UserConfig.SetRibbon( Ribbon.SaveDevExpressControl() );
                UserConfig.SaveMainWindowSettings( this );
                UserConfig.Dispose();
            }
            _cts.Cancel();
            _closeSuccess = true;
        }



        



        private static void InitializeDriveCache()
        {
            StudioBaseHelper.InitializeDriveCache();
        }

        private void InitializeEmulationSettings()
        {
            _emulationSettings = new EmulationSettingsEx()
            {
                MarketDataDrive = ServicesRegistry.StorageRegistry.DefaultDrive
            };
        }

        private void InitializeConnector()
        {
            DXSplashScreen.SetState( LocalizedStrings.InitializingConnector );
            _connector = new StudioConnector( 
                                                ServicesRegistry.SecurityStorage, 
                                                ServicesRegistry.PositionStorage, 
                                                ServicesRegistry.StorageRegistry, 
                                                ServicesRegistry.SnapshotRegistry, 
                                                ServicesRegistry.NativeIdStorage, 
                                                ServicesRegistry.MappingStorage, 
                                                ServicesRegistry.ExchangeInfoProvider, 
                                                ServicesRegistry.SecurityAdapterProvider, 
                                                ServicesRegistry.PortfolioAdapterProvider 
                                            );

            _connector.Adapter.LatencyManager              = null;
            _connector.Adapter.SlippageManager             = null;

            _connector.Adapter.SupportLookupTracking       = false;
            _connector.Adapter.IsSupportTransactionLog     = false;
            _connector.Adapter.IsSupportOrderBookSort      = false;
            _connector.Adapter.Level1Extend                = false;
            _connector.Adapter.SupportPartialDownload      = false;
            _connector.Adapter.SupportBuildingFromOrderLog = false;
            _connector.Adapter.SupportOrderBookTruncate    = false;
            _connector.Adapter.SupportCandlesCompression   = false;

            _connector.UpdateEmulatorSettings( UserConfig.LoadEmulatorSettings() );
            _subscriptionManager = new SubscriptionControlManager( _connector );
            ServicesRegistry.LogManager.Sources.Add( _connector );

            _connector.Connected                          += new Action( ConnectorOnConnectionStateChanged );
            _connector.Disconnected                       += new Action( ConnectorOnConnectionStateChanged );
            _connector.ConnectionLost                     += new Action<IMessageAdapter>( ConnectorOnConnectionLost );
            _connector.ConnectionRestored                 += new Action<IMessageAdapter>( ConnectorOnConnectionRestored );
            _connector.ConnectionError                    += new Action<Exception>( ConnectorOnConnectionError );
            _connector.OrderRegisterFailReceived          += ( sub, fail ) => RaiseOrderFailedCommand( sub, fail, OrderFailTypes.Register );
            _connector.OrderCancelFailReceived            += ( sub, fail ) => RaiseOrderFailedCommand( sub, fail, OrderFailTypes.Cancel );
            _connector.OrderEditFailReceived -= ( sub, fail ) => RaiseOrderFailedCommand( sub, fail, OrderFailTypes.Edit );
            _connector.TickTradeReceived                  += ( s, t ) => RaiseEntityCommand( s, t );
            _connector.OrderLogItemReceived               += ( s, i ) => RaiseEntityCommand( s, i );
            _connector.OrderBookReceived                  += ( s, m ) => RaiseEntityCommand( s, m );
            _connector.Level1Received                     += ( s, m ) => RaiseEntityCommand( s, m );
            _connector.MarketDataSubscriptionFailed2      += new Action<Security, MarketDataMessage, SubscriptionResponseMessage>( ConnectorOnMarketDataSubscriptionFailed2 );
            _connector.MarketDataUnSubscriptionFailed2    += new Action<Security, MarketDataMessage, SubscriptionResponseMessage>( ConnectorOnMarketDataSubscriptionFailed2 );
            _connector.MassOrderCancelFailed              += ( id, error ) => error.LogError( null );
            _connector.OrderStatusFailed                  += ( id, error ) => error.LogError( null );
            _connector.CandleReceived                     += ( s, c ) =>
                                                                    {
                                                                        object[ ] targets = _subscriptionManager.Get( s );
                                                                        if ( targets.Length == 0 )
                                                                            return;
                                                                        new CandleCommand( s.CandleSeries, c ).Process( this, targets );
                                                                    };
            _connector.OrderReceived                   += new Action<Subscription, Order>( RaiseOrderCommand );
            _connector.OwnTradeReceived                += ( s, t ) => RaiseEntityCommand( s, t );
            _connector.PortfolioReceived               += new Action<Subscription, Portfolio>( RaisePortfolioCommand );
            _connector.PositionReceived                += ( s, p ) => RaiseEntityCommand( s, p );
            _connector.LookupSecuritiesResult          += ( message, securities, error ) =>
                                                                                            {
                                                                                                if ( !_firstSecuritiesInit )
                                                                                                    return;
                                                                                                _firstSecuritiesInit = false;
                                                                                                if ( error != null )
                                                                                                    return;
                                                                                                new FirstInitSecuritiesCommand( securities ).Deep().Process( this, false );
                                                                                            };
            _connector.LookupPortfoliosResult          += ( message, portfolios, error ) =>
                                                                                            {
                                                                                                if ( !_firstPortfoliosInit )
                                                                                                    return;
                                                                                                _firstPortfoliosInit = false;
                                                                                                if ( error != null )
                                                                                                    return;
                                                                                                Portfolio[ ] array = portfolios.ToArray();
                                                                                                if ( array.Length == 0 )
                                                                                                    return;
                                                                                                new FirstInitPortfoliosCommand( array ).Deep().Process( this, false );
                                                                                            };
            
            _connector.ValuesChanged += _connector_ValuesChanged;
            _connector.NewOrder += _connector_NewOrder;

            ConfigManager.RegisterService( new PortfolioDataSource ( _connector ) );
            ConfigManager.RegisterService< IConnector             >( _connector );
            ConfigManager.RegisterService< Connector              >( _connector );
            ConfigManager.RegisterService( _connector.Adapter.SecurityAdapterProvider );
            ConfigManager.RegisterService( _connector.Adapter.PortfolioAdapterProvider );
            ConfigManager.RegisterService< IMarketDataProvider    >( _connector );
            ConfigManager.RegisterService< ISubscriptionProvider  >( _connector );
            ConfigManager.RegisterService< IPortfolioProvider     >( _connector );
            ConfigManager.RegisterService< IPositionProvider      >( _connector );
            ConfigManager.RegisterService< INewsProvider          >( _connector );
            ConfigManager.RegisterService< IPriceChartDataProvider>( new PriceChartDataProvider( _connector ) );
            ConfigManager.RegisterService( ( IMessageAdapterProvider )new StudioMessageAdapterProvider( _connector.Adapter.InnerAdapters ) );

            TraderHelper.LookupAll( _connector );
            ServicesRegistry.SnapshotRegistry.Init();
        }

        private void _connector_ValuesChanged( Security security, IEnumerable<KeyValuePair<Level1Fields, object>> changedValues, DateTimeOffset serverTime, DateTimeOffset localTime )
        {
            double bidPrice = 0;
            double askPrice = 0;

            foreach ( KeyValuePair<Level1Fields, object> level1Fields in changedValues )
            {
                Level1Fields lvl1 = level1Fields.Key;

                switch ( lvl1 )
                {
                    case Level1Fields.HighPrice:
                    {

                    }
                    break;

                    case Level1Fields.LowPrice:
                    {

                    }
                    break;

                    case Level1Fields.LastTradePrice:
                    {

                    }
                    break;

                    case Level1Fields.BestBidPrice:
                    {
                        bidPrice = ( double )( decimal )level1Fields.Value;
                    }
                    break;

                    case Level1Fields.BestAskPrice:
                    {
                        askPrice = ( double )( decimal )level1Fields.Value;
                    }
                    break;
                }

            }

            if ( bidPrice > 0 && askPrice > 0 )
            {
                var quote = new QuoteMessage( security, serverTime.DateTime, bidPrice, askPrice );
                Messenger.Default.Send( quote );
            }
        }

        private void _connector_NewOrder( Order obj )
        {
            //throw new NotImplementedException( );
        }

        private void ConnectorOnMarketDataSubscriptionFailed2( Security security, MarketDataMessage origin, SubscriptionResponseMessage reply )
        {
            if ( reply.IsNotSupported() )
                UserConfig.LogConfig.Manager.Application.AddWarningLog( LocalizedStrings.SubscriptionNotSupported, origin );
            else
                reply.Error.LogError( null );
        }

        private void ClosePosition( Position position, Decimal vol )
        {
            Decimal? positionValue = position.CurrentValue;

            if ( positionValue.GetValueOrDefault() == 0 & positionValue.HasValue )
            {
                return;
            }

            Sides sides = ( position.CurrentValue.GetValueOrDefault() > 0 & position.CurrentValue.HasValue ) ? Sides.Sell : Sides.Buy;
            Security security = position.Security;

            _connector.RegisterOrder( new Order()
            {
                Security = security,
                Portfolio = position.Portfolio,
                Direction = sides,
                Volume = vol,
                Type = new OrderTypes?( OrderTypes.Market )
            } );
        }

        private void RaiseOrderFailedCommand( Subscription subscription, OrderFail fail, OrderFailTypes type )
        {
            object[ ] targets = _subscriptionManager.Get( subscription );
            if ( targets.Length == 0 )
                return;
            new OrderFailCommand( subscription, fail, type ).Process( this, targets );
        }

        private void RaiseOrderCommand( Subscription subscription, Order order )
        {
            object[ ] targets = _subscriptionManager.Get( subscription );
            if ( targets.Length == 0 )
                return;
            new OrderCommand( subscription, order ).Process( this, targets );
        }

        private void RaisePortfolioCommand( Subscription subscription, Portfolio portfolio )
        {
            object[ ] targets = RaiseEntityCommand( subscription, portfolio );
            if ( targets.Length == 0 )
                return;
            Decimal? unrealizedPnL = portfolio.UnrealizedPnL;
            Decimal? realizedPnL = portfolio.RealizedPnL;
            Decimal valueOrDefault = unrealizedPnL.GetValueOrDefault();
            Decimal? totalPnL = realizedPnL.HasValue ? new Decimal?( realizedPnL.GetValueOrDefault() + valueOrDefault ) : new Decimal?();
            Decimal? commission = portfolio.Commission;
            if ( !totalPnL.HasValue && !unrealizedPnL.HasValue && !commission.HasValue )
                return;
            new PnLChangedCommand( subscription, portfolio, portfolio.LastChangeTime, totalPnL, unrealizedPnL, commission ).Process( this, targets );
        }

        private object[ ] RaiseEntityCommand<TEntity>( Subscription subscription, TEntity entity ) where TEntity : class
        {
            object[ ] targets = _subscriptionManager.Get( subscription );
            if ( targets.Length != 0 )
                subscription.ToCommand( entity ).Process( this, targets );
            return targets;
        }

        private void InitializeLayoutManager()
        {
            DXSplashScreen.SetState( LocalizedStrings.InitializingLayoutManager );
            _layoutManager = new LayoutManager( DockingManager, DocumentHost );
            _layoutManager.Changed += () => UserConfig.SetLayout( () => GuiDispatcher.GlobalDispatcher.AddSyncAction( () => _layoutManager.Save() ) );
            _layoutManager.PredefinedControls[typeof( LogManagerPanel )] = new LogManagerPanel()
            {
                ShowStrategies = false
            };
            ServicesRegistry.LogManager.Sources.Add( _layoutManager );
            ConfigManager.RegisterService( _layoutManager );
        }

        private void LoadSettings()
        {
            StudioUserConfig userConfig = UserConfig;
            userConfig.SuspendChangesMonitor();
            userConfig.TryLoadSettings( "EmulationSettings", new Action<SettingsStorage>( _emulationSettings.Load ) );
            userConfig.TryLoadSettings( "Connector", new Action<SettingsStorage>( _connector.Load ) );

            var layoutString = Properties.Resources.DefaultLayout.LoadFromString<JsonSerializer<SettingsStorage>>();

            SettingsStorage layout = userConfig.GetValue( "Layout", layoutString );

            if ( layout != null )
            {
                ( ( Action )( () => _layoutManager.Load( layout ) ) ).DoWithLog();
            }

            userConfig.ResumeChangesMonitor();
        }

        private void LoadMainWindowSettings()
        {
            StudioUserConfig settings = UserConfig;
            ( ( Action )( 
                            () =>
                                {
                                    string ribbon = settings.GetRibbon();
                                    if ( ribbon.IsEmpty() )
                                        return;
                                    Ribbon.LoadDevExpressControl( ribbon );
                                } ) 
                        ).DoWithLog();

            ( ( Action )( () => UserConfig.LoadMainWindowSettings( this ) ) ).DoWithLog();
        }

        private bool ConfigureConnector()
        {
            bool autoConnect = UserConfig.GetAutoConnect();
            SettingsStorage connectorWindow = UserConfig.GetConnectorWindow();
            int num = _connector.Adapter.Configure( this, ref autoConnect, ref connectorWindow ) ? 1 : 0;
            UserConfig.SetConnectorWindow( connectorWindow );
            if ( num == 0 )
                return false;
            UserConfig.SetConnector( _connector.Save() );
            UserConfig.SetAutoConnect( autoConnect );
            return true;
        }

        private void ConnectorOnConnectionStateChanged()
        {
            _connector.AddInfoLog( LocalizedStrings.ConnectionStateParams, _connector.ConnectionState );
            GuiDispatcher.GlobalDispatcher.AddAction( () =>
            {
                if ( _connector.ConnectionState == ConnectionStates.Connected )
                {
                    ConnectButton.LargeGlyph = ThemedIconsExtension.GetImage( "Disconnect" );
                    ConnectButton.Content = ConnectButton.ToolTip = LocalizedStrings.Disconnect;
                }
                else
                {
                    ConnectButton.LargeGlyph = ThemedIconsExtension.GetImage( "Connect" );
                    ConnectButton.Content = ConnectButton.ToolTip = LocalizedStrings.Connect;
                }
                CommandManager.InvalidateRequerySuggested();
            } );
        }

        private void ConnectorOnConnectionRestored( IMessageAdapter adapter )
        {
            _connector.AddInfoLog( LocalizedStrings.Str2958 + " " + adapter?.ToString() );
        }

        private void ConnectorOnConnectionLost( IMessageAdapter adapter )
        {
            _connector.AddErrorLog( LocalizedStrings.Str2631Params, adapter );
        }

        private void ConnectorOnConnectionError( Exception obj )
        {
            ConnectorOnConnectionStateChanged();
            int num;
            GuiDispatcher.GlobalDispatcher.AddAction( () => num = ( int )new MessageBoxBuilder().Owner( this ).Caption( Title ).Text( LocalizedStrings.Str626 ).Button( MessageBoxButton.OK ).Warning().Show() );
        }

        private void Connect()
        {
            IInnerAdapterList innerAdapters = _connector.Adapter.InnerAdapters;

            if ( innerAdapters.IsEmpty() )
            {
                int num = ( int )new MessageBoxBuilder().Owner( this ).Text( LocalizedStrings.Str3650 ).Warning().Show();
                if ( !ConfigureConnector() )
                    return;
            }

            if ( innerAdapters.SortedAdapters.IsEmpty() )
            {
                int num = ( int )new MessageBoxBuilder().Owner( this ).Text( LocalizedStrings.Str3651 ).Warning().Show();
                if ( !ConfigureConnector() )
                    return;
            }
            _connector.Connect();
        }

        private void Disconnect()
        {
            _connector.Disconnect();
        }

        private bool IsConnectorInactive()
        {
            if ( _connector.ConnectionState != ConnectionStates.Disconnected )
                return _connector.ConnectionState == ConnectionStates.Failed;
            return true;
        }

        private void ConnectorSettingsCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = IsConnectorInactive();
        }

        private void ConnectorSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            ConfigureConnector();
        }

        private void ConnectDisconnectCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute =  _connector.ConnectionState == ConnectionStates.Connected || 
                            _connector.ConnectionState == ConnectionStates.Disconnected || 
                            _connector.ConnectionState == ConnectionStates.Failed;
        }

        private void ConnectDisconnectCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            if ( _connector.ConnectionState != ConnectionStates.Connected )
                Connect();
            else
                Disconnect();
        }

        private void HelpCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Paths.GetDocUrl( "E4B5E112-BD08-4F40-8741-7514E7E4088E.htm" ).TryOpenLink( this );
        }

        private void AboutCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new AboutWindow(), this );
        }

        private void QuestionCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.ShowQuestionWindow( null, null, null, false );
        }

        private void CommonSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.ShowCommonSettings<StudioCommonSettings>( null );
        }

        private void TargetPlatformCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.TryChangeLaunchMode();
        }

        private void ResetSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.TryResetSettings( null );
        }

        private void PortfoliosMapping_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void PortfoliosMapping_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new PortfolioMessageAdaptersWindow()
            {
                Portfolios               = StudioServicesRegistry.PortfolioDataSource,
                PortfolioAdapterProvider = ServicesRegistry.PortfolioAdapterProvider,
                AdapterProvider          = ServicesRegistry.AdapterProvider
            }, this );
        }

        private void SecuritiesMapping_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = true;
        }

        private void SecuritiesMapping_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new SecurityMessageAdaptersWindow()
            {
                SecurityProvider        = ServicesRegistry.SecurityProvider,
                SecurityAdapterProvider = ServicesRegistry.SecurityAdapterProvider,
                AdapterProvider         = ServicesRegistry.AdapterProvider
            }, this );
        }

        private void EmulationSettingsCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = IsConnectorInactive();
        }

        private void EmulationSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            MarketEmulatorSettings settings = UserConfig.LoadEmulatorSettings();
            SettingsWindow wnd = new SettingsWindow()
            {
                Settings = settings.Clone()
            };
            if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                return;
            settings.Apply( wnd.Settings );
            UserConfig.SaveEmulatorSettings( settings );
            _connector.UpdateEmulatorSettings( settings );
        }

        private object ActiveLayoutContent
        {
            get
            {
                return ( DockingManager.ActiveLayoutItem as LayoutPanel )?.Content;
            }
        }

        private void DockingManager_OnLayoutItemActivated( object sender, LayoutItemActivatedEventArgs ea )
        {
            DockItemActivated( ( ea.Item as LayoutPanel )?.Content );
        }

        private void DockingManager_OnDockItemActivated( object sender, DockItemActivatedEventArgs ea )
        {
            DockItemActivated( ( ea.Item as LayoutPanel )?.Content );
        }

        private void DockingManager_OnDockItemClosed( object sender, DockItemClosedEventArgs e )
        {
            DockItemActivated( ActiveLayoutContent );
        }

        private void DockItemActivated( object control )
        {
            if ( control == null )
            {
                ControlGallery.DataContext = null;
                RibbonComponentsGroup.IsEnabled = false;
                Ribbon.SelectedPage = RibbonCommonTab;
            }
            else
            {
                if ( control is WorkAreaControl )
                {
                    ControlGallery.DataContext = control;
                    RibbonComponentsGroup.IsEnabled = true;
                }
                else
                {
                    ControlGallery.DataContext = null;
                    RibbonComponentsGroup.IsEnabled = false;
                }
                IStudioControl control1 = control as IStudioControl;
                if ( control1 == null )
                    return;
                new ControlOpenedCommand( control1, true ).SyncProcess( this );
            }
        }

        private void AddWorkAreaCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            AddWorkArea();
        }

        private void AddWorkArea()
        {
            object state = null;
            string key = "WorkArea" + Guid.NewGuid().ToString().Replace( "-", string.Empty );
            _layoutManager.OpenDocumentWindow( key, () =>
                                                        {
                                                            var workAreaControl = new WorkAreaControl() { Key = key };
                                                            state = workAreaControl.State;
                                                            return workAreaControl;
                                                        }, true );
            new LoadLayoutCommand( Properties.Resources.DefaultAreaLayout ).SyncProcess( state );
        }

        private void CheckIsFirstRun()
        {
            StudioUserConfig userConfig = UserConfig;
            if ( !userConfig.GetIsFirstRun() )
                return;
            userConfig.SetStorageFormat();
            userConfig.SetDaysLoad();
            userConfig.SetDriveCache( ServicesRegistry.DriveCache.Save() );
            userConfig.SetIsFirstRun( false );
            AddWorkArea();
            if ( !_connector.InitStockSharpConnections( this ) )
                return;
            _firstSecuritiesInit = true;
            _firstPortfoliosInit = true;
        }

        private void BugReportCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.SendLogs( new TimeSpan?(), null, null, true );
        }

        private void RenameAreaCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            WorkAreaControl content = ( DockingManager.ActiveDockItem as ContentItem )?.Content as WorkAreaControl;

            executeRoutedEventArgs.CanExecute = content.IsTitleEditable;
        }

        private void RenameAreaCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem( e.Source as DependencyObject );
            if ( layoutItem == null )
                return;
            DockingManager.Rename( layoutItem );
        }



        private void InitializeCommands()
        {
            DXSplashScreen.SetState( LocalizedStrings.InitializingCommands );
            IStudioCommandService commandService = StudioServicesRegistry.CommandService;


            commandService.Register<LoggedInCommand>( this, true, OnLoggedInCommand, null );
            commandService.Register<LogInCommand>( this, true, OnLogInCommand, null );
            commandService.Register<OpenWindowCommand>( this, true, OnOpenWindowCommand, null );
            commandService.Register<ControlChangedCommand>( this, true, cmd => _layoutManager.MarkControlChanged( cmd.Control ), null );
            commandService.Register<PositionEditCommand>( this, true, OnPositionEditCommand, null );
            commandService.Register<ConfigureConnectorCommand>( this, true, cmd => cmd.Result = ConfigureConnector(), null );
            commandService.Register<SubscribeCommand>( this, false, ( sender, cmd ) => _subscriptionManager.Subscribe( sender, cmd.Subscription ), null );
            commandService.Register<UnSubscribeCommand>( this, false, ( sender, cmd ) => _subscriptionManager.Unsubscribe( cmd.Subscription ), null );
            commandService.Register<LookupSecuritiesCommand>( this, false, cmd => _connector.LookupSecurities( cmd.Criteria, null, MessageOfflineModes.None ), null );
            commandService.Register<RegisterOrderCommand>( this, false, cmd => _connector.RegisterOrder( cmd.Order ), null );
            commandService.Register<ReRegisterOrderCommand>( this, false, cmd => _connector.ReRegisterOrderEx( cmd.OldOrder, cmd.NewOrder ), null );
            commandService.Register<CancelOrderCommand>( this, false, cmd => _connector.CancelOrder( cmd.Order ), null );
            commandService.Register<RevertPositionCommand>( this, false, OnRevertPositionCommand, null );
            commandService.Register<ClosePositionCommand>( this, false, OnClosePositionCommand, null );
            commandService.Register<CancelAllOrdersCommand>( this, false, cmd => _connector.Orders.Where( o => o.State == OrderStates.Active ).ForEach( o => _connector.CancelOrder( o ) ), null );
            commandService.Register<OrderFailCommand>( this, false, cmd => AlertServicesRegistry.NotificationService.Notify( AlertNotifications.Popup, LocalizedStrings.XamlStr182, cmd.Entity.Error.Message, cmd.Entity.ServerTime ), null );
            commandService.Register<CreateSecurityCommand>( this, true, OnCreateSecurityCommand, null );
            commandService.Register<EditSecuritiesCommand>( this, true, OnEditSecuritiesCommand, null );
            commandService.Register<RemoveSecuritiesCommand>( this, true, OnRemoveSecuritiesCommand, null );
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  Step 1. We received LoginCommand, system proceed to Process the Login Command by connecting to remote server.
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void OnLogInCommand( LogInCommand cmd )
        {
            this.ProcessLogInCommand( _cts.Token );
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  Step 2. After successfully login to SS server, we check if we need to autoConnect the connector. 
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void OnLoggedInCommand( LoggedInCommand cmd )
        {
            this.UpdateTitle( cmd.Profile );
            SplashScreenControl.Show();
            DXSplashScreen.SetState( LocalizedStrings.LoadingSettings );

            /* ----------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 2a. Here we load all the save settings for Connector and UI Layout from previous Session.
            * 
            * -----------------------------------------------------------------------------------------------------------------------------------
            */
            LoadSettings();
            DXSplashScreen.Close();
            CheckIsFirstRun();

            if ( UserConfig.GetAutoConnect() && _connector.Adapter.InnerAdapters.Any() )
            {
                /* ----------------------------------------------------------------------------------------------------------------------------------
                * 
                *  Step 2b. Eeither we have AutoConnect setting on or we have Inner Connectors setup, we will try to call connect.
                * 
                * -----------------------------------------------------------------------------------------------------------------------------------
                */
                Connect();
            }

            if ( UserConfig.GetDoNotShowQuestionWindow() )
            {
                return;
            }

            this.ShowQuestionWindow( new TimeSpan?( TimeSpan.FromMinutes( 10.0 ) ), null, null, null );
        }

        private void OnOpenWindowCommand( OpenWindowCommand cmd )
        {            
            cmd.Result = cmd.IsToolWindow ? _layoutManager.OpenToolWindow( cmd.CtrlType, null, true ) : _layoutManager.OpenDocumentWindow( cmd.CtrlType, null, true );
        }

        private void OnPositionEditCommand( PositionEditCommand cmd )
        {            
            var pos = cmd.Position.Clone();

            var posEditWnd = new PositionEditWindow()
            {
                Position = pos,
                PositionStorage = ServicesRegistry.PositionStorage
            };

            if ( !Ecng.Xaml.XamlHelper.ShowModal( posEditWnd, Application.Current.GetActiveOrMainWindow() ) )
                return;

            Portfolio pf = cmd.Position as Portfolio;

            if ( pf != null )
            {
                ( ( Portfolio )pos ).CopyTo( pf );
                _connector.StorageAdapter.SaveDirect( pf.ToChangeMessage() );
            }
            else
            {
                pos.CopyTo( cmd.Position );
                _connector.StorageAdapter.SaveDirect( cmd.Position.ToChangeMessage( 0L ) );
            }
        }

        private void OnRevertPositionCommand( RevertPositionCommand cmd )
        {
            if ( cmd.Position == null && cmd.Security == null )
                return;

            Position position = cmd.Position ?? _connector.Positions.FirstOrDefault( p => p.Security == cmd.Security );
            if ( ( position != null ? ( !position.CurrentValue.HasValue ? 1 : 0 ) : 1 ) != 0 )
                return;

            ClosePosition( position, position.CurrentValue.Value.Abs() * new Decimal( 2 ) );
        }

        private void OnClosePositionCommand( ClosePositionCommand cmd )
        {
            if ( cmd.Position == null && cmd.Security == null )
                return;

            Position position = cmd.Position ?? _connector.Positions.FirstOrDefault( p => p.Security == cmd.Security );
            if ( ( position != null ? ( !position.CurrentValue.HasValue ? 1 : 0 ) : 1 ) != 0 )
                return;

            ClosePosition( position, position.CurrentValue.Value.Abs() );
        }

        private void OnCreateSecurityCommand( CreateSecurityCommand cmd )
        {
            ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
            ISecurityWindow securityWindow;
            if ( cmd.SecurityType == typeof( Security ) )
            {
                securityWindow = new SecurityCreateWindow()
                {
                    SecurityStorage = securityStorage,
                    Security = new Security()
                };
            }
            else if ( cmd.SecurityType == typeof( IndexSecurity ) )
            {
                securityWindow = new IndexSecurityWindow()
                {
                    SecurityStorage = securityStorage
                };
            }
            else if ( cmd.SecurityType == typeof( ContinuousSecurity ) )
            {
                securityWindow = new ContinuousSecurityWindow()
                {
                    SecurityStorage = securityStorage,
                    ExchangeInfoProvider = ServicesRegistry.ExchangeInfoProvider
                };
            }
            else
            {
                throw new InvalidOperationException( LocalizedStrings.Str2140Params.Put( cmd.SecurityType ) );
            }

            if ( !Ecng.Xaml.XamlHelper.ShowModal( ( Window )securityWindow, Application.Current.GetActiveOrMainWindow() ) )
                return;

            _connector.SendOutMessage( securityWindow.Security.ToMessage( new SecurityId?(), 0L, false ) );

            cmd.Security = securityWindow.Security;
        }

        private void OnEditSecuritiesCommand( EditSecuritiesCommand cmd )
        {
            ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
            Security[ ] array = cmd.Securities.Where( s => !s.IsBasket() ).ToArray();
            if ( array.Length != 0 )
            {
                if ( !Ecng.Xaml.XamlHelper.ShowModal( new SecurityCreateWindow() { SecurityStorage = securityStorage, Securities = array }, Application.Current.GetActiveOrMainWindow() ) )
                    return;
                foreach ( Security security in array )
                    _connector.SendOutMessage( security.ToMessage( new SecurityId?(), 0L, false ) );
            }

            foreach ( Security security in cmd.Securities.Where( s => s.IsIndex() ) )
            {
                if ( !Ecng.Xaml.XamlHelper.ShowModal( new IndexSecurityWindow() { SecurityStorage = securityStorage, Security = security }, Application.Current.GetActiveOrMainWindow() ) )
                    return;
                _connector.SendOutMessage( security.ToMessage( new SecurityId?(), 0L, false ) );
            }

            foreach ( Security security in cmd.Securities.Where( s => s.IsContinuous() ) )
            {
                if ( !Ecng.Xaml.XamlHelper.ShowModal( new ContinuousSecurityWindow() { SecurityStorage = securityStorage, Security = security }, Application.Current.GetActiveOrMainWindow() ) )
                    break;
                _connector.SendOutMessage( security.ToMessage( new SecurityId?(), 0L, false ) );
            }
        }

        private void OnRemoveSecuritiesCommand( RemoveSecuritiesCommand cmd )
        {
            Security[ ] array = cmd.Securities.ToArray();

            if ( new MessageBoxBuilder().Owner( this ).Warning().Text( LocalizedStrings.DeleteNSecurities.Put( array.Length ) ).OkCancel().Show() != MessageBoxResult.OK )
                return;

            foreach ( Security security in array )
            {
                _connector.SendOutMessage( new SecurityRemoveMessage()
                {
                    SecurityId = security.ToSecurityId( null, true, false )
                } );
                _entityRegistry.Securities.Remove( security );
            }

            new SecuritiesRemovedCommand( array ).Deep().Process( this, true );
        }

        private void FreemindResult_ItemClick( object sender, ItemClickEventArgs e )
        {
            AddFreemindResultWindow();
        }

        private void AddFreemindResultWindow()
        {
            object state = null;

            string key = "FreemindResult" + Guid.NewGuid().ToString().Replace( "-", string.Empty );

            _layoutManager.OpenDocumentWindow( key, () =>
            {
                var ta = new TechnicalAnalysisView()
                {
                    Key = key
                };
                state = ta.State;
                return ta;
            }, true
                                             );
        }

        private Dictionary< Guid, TradeStationView > _tradeStationView = new Dictionary<Guid, TradeStationView>();
        private Type _fxMainTradeStationType = null;

        private void TradeStation_ItemClick( object sender, ItemClickEventArgs e )
        {
            var tradeStationView = _fxMainTradeStationType.CreateInstance<TradeStationView>();

            tradeStationView.ViewGuid = Guid.NewGuid();
            tradeStationView.Closing += _fxMainTradeStation_Closing;
            tradeStationView.Closed  += _fxMainTradeStation_Closed;
            _connector.Connected     += tradeStationView.ViewModel.OnAdapterConnected;

            _tradeStationView.Add( tradeStationView.ViewGuid, tradeStationView );

            /*
             * Tony: Fix an error that Type is not found in the DocumentMangerServices to display a view
             * By default, the Default property contains a ViewLocator object capable of locating Views within the entry assembly. 
             * If you want the ViewLocator to locate Views within two or more assemblies, create a new ViewLocator object via a constructor that 
             * takes a list of assemblies as a parameter. Then, assign the created object to the Default property.
             * 
             * */

            var myViewLocator = new FreemindViewLocator(
                                                            new Assembly[ ]
                                                            {
                                                                _fxMainTradeStationType.Assembly,
                                                                typeof( MainWindow ).Assembly
                                                            } ); ;

            ViewLocator.Default = myViewLocator;

            if ( ScreenHelper.HasSecondMonitor() )
            {
                var secondary = ScreenHelper.GetSecondaryScreen();

                tradeStationView.Left   = secondary.Bounds.Left;
                tradeStationView.Top    = secondary.Bounds.Top;
                tradeStationView.Width  = secondary.Bounds.Width;
                tradeStationView.Height = secondary.Bounds.Height;
            }
            else
            {
                var primary = ScreenHelper.GetPrimaryScreen();

                tradeStationView.Left   = primary.Bounds.Left;
                tradeStationView.Top    = primary.Bounds.Top;
            }

            tradeStationView.ViewModel.Connector = _connector;
            tradeStationView.Show();
            tradeStationView.Activate();
        }

        private void _fxMainTradeStation_Closing( object sender, EventArgs e )
        {
            GeneralHelper.CancelAllTasks();
        }

        private void _fxMainTradeStation_Closed( object sender, EventArgs e )
        {            
            if ( sender is TradeStationView )
            {
                var view = ( TradeStationView )sender;

                var security = view.ViewModel.Security;

                view.ViewModel.ResetVariables();

                view.ViewModel.SaveSettings();

                _tradeStationView.Remove( view.ViewGuid );

                if ( security != null )
                {
                    SymbolsMgr.Instance.RemoveSymbolsData( security.Code );
                }

                if ( _tradeStationView.Count == 0 )
                {
                    _connector.Disconnect();
                }

                GeneralHelper.CreateNewGlobalExitSource( );
            }            
        }

        private void CheckIfFractalAnalysisDllExist()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var aiTradePath = currentDirectory + "\\fx.AITrade.dll";

            if ( File.Exists( aiTradePath ) )
            {
                List<Type> typeList = new List<Type>();

                try
                {
                    Assembly assembly = Assembly.LoadFrom( aiTradePath );

                    typeList.AddRange( assembly.GetTypes().ToArray() );
                }
                catch ( Exception ex )
                {
                    ex.LogError( null );
                }

                foreach ( Type type in typeList )
                {
                    if ( type.BaseType == typeof( DXRibbonWindow ) )
                    {
                        _fxMainTradeStationType = type;                        
                        
                        TradeStation.IsVisible = true;
                        FreemindResult.IsVisible = true;

                        return;
                    }
                }
            }
        }

    }
}
