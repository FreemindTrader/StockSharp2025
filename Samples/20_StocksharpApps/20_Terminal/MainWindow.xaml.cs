// Decompiled with JetBrains decompiler
// Type: StockSharp.Terminal.MainWindow
// Assembly: Terminal, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 33913FF8-0D5D-4EE9-A5BB-58AEFF5B15A5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\Terminal.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.XtraSpellChecker.Parser;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Compilation;
using Ecng.Compilation.Roslyn;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Risk;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Storages.Csv;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Fix;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Studio.IPC;
using StockSharp.Studio.WebApi;
using StockSharp.Terminal.Controls;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Position = StockSharp.BusinessEntities.Position;

namespace StockSharp.Terminal
{
    public partial class MainWindow : ThemedWindow
    {
        public static readonly RoutedCommand ConnectorSettingsCommand = new RoutedCommand();
        public static readonly RoutedCommand ConnectDisconnectCommand = new RoutedCommand();
        public static readonly RoutedCommand HelpCommand = new RoutedCommand();
        public static readonly RoutedCommand AboutCommand = new RoutedCommand();
        public static readonly RoutedCommand QuestionCommand = new RoutedCommand();
        public static readonly RoutedCommand CommonSettingsCommand = new RoutedCommand();
        public static readonly RoutedCommand ResetSettingsCommand = new RoutedCommand();
        public static readonly RoutedCommand SecuritiesMappingCommand = new RoutedCommand();
        public static readonly RoutedCommand PortfoliosMappingCommand = new RoutedCommand();
        public static readonly RoutedCommand EmulationSettingsCommand = new RoutedCommand();
        public static readonly RoutedCommand RenameAreaCommand = new RoutedCommand();
        public static readonly RoutedCommand BugReportCommand = new RoutedCommand();
        public static readonly RoutedCommand ThemeCommand = new RoutedCommand();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly SynchronizedDictionary<Order, Decimal> _prevBalance = new SynchronizedDictionary<Order, Decimal>();
        private StudioConnector _connector;
        private SubscriptionControlManager _subscriptionManager;
        private LayoutManager _layoutManager;
        private readonly StudioChannel _ipcChannel;
        private bool _closeSuccess;
        private bool _firstSecuritiesInit;
        private bool _firstPortfoliosInit;
        private readonly StudioNotificationSettings _settings;
        private bool _uiLoaded;
        

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
                return StudioUserConfig.Instance;
            }
        }

        public MainWindow()
        {
            DXSplashScreen.SetState( LocalizedStrings.Initializing );
            MainWindow.UserConfig.SuspendChangesMonitor();
            //GetType().RegisterWebApiServices( new bool?() );

            ConfigManager.RegisterService<ICompiler>( new CSharpCompiler() );
            StockSharp.Studio.Controls.Extensions.RegisterUIServices();
            ConfigManager.RegisterService<IStudioCommandService>( new StudioCommandService() );
            MainWindow.FillComponentControls();
            InitializeComponent();
            LoadMainWindowSettings();
            LogManager manager = MainWindow.UserConfig.LogConfig.Manager;

            manager.Listeners.Add( StatusBarPanel );
            manager.Application.AddInfoLog( Paths.AppName );


            _settings = MainWindow.UserConfig.GetCommonSettings<StudioNotificationSettings>();
            _settings.UpdateGlobalTimeZone();

            InitializeLayoutManager();
            InitializeCommands();

            ConfigManager.RegisterService<IEntityRegistry>( new CsvEntityRegistry( Paths.AppDataPath ) );
            ThemeManager.ApplicationThemeChanged += ( ThemeChangedRoutedEventHandler ) ( ( s, e ) => MainWindow.UserConfig.SetTheme( ApplicationThemeHelper.ApplicationThemeName ) );
            _ipcChannel = manager.CreateChannel( new Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>>( GetIpcResponseAsync ), ( DependencyObject ) this );
        }

        private static void FillComponentControls()
        {
            addComponent<SecuritiesPanel>();
            addComponent<Level1Panel>();
            addComponent<BuySellGridPanel>();
            addComponent<ScalpingMarketDepthControl>();
            addComponent<InteractiveChart>();
            addComponent<EquityCurveChartPanel>();
            addComponent<MyTradesPanel>();
            addComponent<OrdersPanel>();
            addComponent<PositionsPanel>();
            addComponent<TradesPanel>();
            addComponent<PositionChartPanel>();
            addComponent<PropertiesPanel>();
            addComponent<OptionPositionChartPanel>();
            addComponent<OptionDeskPanel>();
            addComponent<OptionVolatilitySmilePanel>();
            addComponent<OrderLogPanel>();

            void addComponent<T>() where T : IStudioControl
            {
                ControlType.AddComponent<T>();
            }
        }

        public async ValueTask<StudioMessage> GetIpcResponseAsync( StudioMessage request, CancellationToken token )
        {
            MainWindow mainWindow = this;
            if ( !( request is MsgClose ) )
                return ( StudioMessage ) null;
            mainWindow._closeSuccess = false;
            //// ISSUE: reference to a compiler-generated method
            //await Ecng.Xaml.XamlHelper.GuiThreadGetAsync( new Action<CancellationToken>( mainWindow.\u003CGetIpcResponseAsync\u003Eb__29_0 ), CancellationToken.None );
            //if ( !mainWindow._closeSuccess )
            //    throw new InvalidOperationException( "application was not closed" );
            return ( StudioMessage ) new MsgOk();
        }

        private void MainWindow_OnLoaded( object sender, RoutedEventArgs e )
        {
            DXSplashScreen.SetState( ( object ) LocalizedStrings.LoadingRibbonControls );
            addTool<LogManagerPanel>();
            addTool<PortfoliosPanel>();
            addTool<ExchangeEditorPanel>();
            addTool<MarketDataPanel>();
            addTool<StockSharp.Studio.Controls.NewsPanel>();
            MainWindow.UserConfig.ResumeChangesMonitor();
            DXSplashScreen.Close();
            _settings.UpdateErrorsDialogs();
            new LogInCommand().Process( ( object ) this, false );

            void addTool<T>() where T : IStudioControl
            {
                ViewMenu.Items.Add( ( IBarItem ) ( ( object ) this ).CreateToolControl<T>() );
            }
        }

        private void MainWindow_OnClosing( object sender, CancelEventArgs e )
        {
            if ( !MainWindow.UserConfig.IsReseting )
            {
                if ( !this.ShowByeByeWindow( true ) )
                {
                    e.Cancel = true;
                    return;
                }
                ServicesRegistry.EntityRegistry.DelayAction.WaitFlush( true );
                foreach ( BaseStudioControl baseStudioControl in _layoutManager.DockingControls.OfType<BaseStudioControl>() )
                {
                    if ( baseStudioControl.CanClose( CloseReason.Shutdown ) != CloseAction.Close )
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                IRiskManager tryRiskManager = ServicesRegistry.TryRiskManager;
                if ( tryRiskManager != null )
                    MainWindow.UserConfig.SaveRiskManager( PersistableHelper.Save( ( IPersistable ) tryRiskManager ) );
                ( ( Disposable ) _layoutManager ).Dispose();
                StudioServicesRegistry.CommandService.Dispose();
                MainWindow.UserConfig.SaveMainWindowSettings( this.ToStorage() );
                MainWindow.UserConfig.Dispose();
            }
            _cts.Cancel();
            _closeSuccess = true;
        }

        private void InitializeCommands()
        {
            DXSplashScreen.SetState( ( object ) LocalizedStrings.InitializingCommands );
            IStudioCommandService commandService = StudioServicesRegistry.CommandService;

            commandService.Register<LoggedInCommand>( this, true, OnLoggedInCommand, null );
            commandService.Register<LogInCommand>( this, true, OnLogInCommand, null );
            commandService.Register<OpenWindowCommand>( this, true, OnOpenWindowCommand, null );
            commandService.Register<PositionEditCommand>( this, true, OnPositionEditCommand, null );
            commandService.Register<ControlChangedCommand>( this, true, cmd => _layoutManager.MarkControlChanged( cmd.Control ), null );
            commandService.Register<ConfigureConnectorCommand>( this, true, cmd => cmd.Result = _connector.ConfigureConnector( this ), null );
            commandService.Register<SubscribeCommand>( this, false, ( sender, cmd ) => _subscriptionManager.Subscribe( sender, cmd.Subscription ), null );
            commandService.Register<UnSubscribeCommand>( this, false, ( sender, cmd ) => _subscriptionManager.Unsubscribe( cmd.Subscription ), null );
            commandService.Register<LookupSecuritiesCommand>( this, false, cmd => _connector.LookupSecurities( cmd.Criteria, null, MessageOfflineModes.None ), null );
            commandService.Register<RegisterOrderCommand>( this, false, cmd => _connector.RegisterOrder( cmd.Order ), null );
            commandService.Register<ReRegisterOrderCommand>( this, false, cmd => _connector.ReRegisterOrderEx( cmd.OldOrder, cmd.NewOrder ), null );
            commandService.Register<CancelOrderCommand>( this, false, cmd => _connector.CancelOrder( cmd.Order ), null );
            commandService.Register<RevertPositionCommand>( this, false, OnRevertPositionCommand, null );
            commandService.Register<ClosePositionCommand>( this, false, OnClosePositionCommand, null );
            commandService.Register<CancelAllOrdersCommand>( this, false, cmd => _connector.Orders.Where( o => o.State == OrderStates.Active ).ForEach( o => _connector.CancelOrder( o ) ), null );
            commandService.Register<CreateSecurityCommand>( this, true, OnCreateSecurityCommand, null );
            commandService.Register<EditSecuritiesCommand>( this, true, OnEditSecuritiesCommand, null );
            commandService.Register<RemoveEntitiesCommand<Security>>( this, true, OnRemoveEntitiesCommand, null );
        }

        private void OnRemoveEntitiesCommand( object arg1, RemoveEntitiesCommand<Security> cmd )
        {

            Security[] array = cmd.Entities.ToArray<Security>();

            if ( new MessageBoxBuilder().Owner( this ).Warning().Text( StringHelper.Put( LocalizedStrings.DeleteNSecurities, array.Length ) ).OkCancel().Show() != MessageBoxResult.OK )
                return;

            foreach ( Security security in array )
            {
                _connector.SendOutMessage
                (  
                    new SecurityRemoveMessage()
                    {
                        SecurityId = security.ToSecurityId( ( SecurityIdGenerator ) null, true, false )
                    } 
                );

                ServicesRegistry.EntityRegistry.Securities.Remove( security );
            }

            new EntitiesRemovedCommand<Security>( ( IEnumerable<Security> ) array ).Deep<EntitiesRemovedCommand<Security>>().Process( ( object ) this, true );        
        }

        private void OnLoggedInCommand( LoggedInCommand cmd )
        {
            SplashScreenControl.Show();
            CancellationToken token = _cts.Token;
            WebApiHelper.InitServices( OnSocketConnected, token );

            ConfigManager.RegisterService<SnapshotRegistry>( new SnapshotRegistry( Paths.SnapshotsDir ) );
            ConfigManager.RegisterService<Type>( "SecuritiesSelectWindowType", typeof( SecuritiesWindowEx ) );

            StudioHelper.InitializeStorage();

            DXSplashScreen.SetState( LocalizedStrings.LoadingSettings );

            LoggingHelper.DoWithLog( ServicesRegistry.EntityRegistry.Init );
            LoggingHelper.DoWithLog( ServicesRegistry.ExchangeInfoProvider.Init );

            ConfigManager.RegisterService<ISecurityStorage>( ServicesRegistry.EntityRegistry.Securities );
            ConfigManager.RegisterService<IPositionStorage>( ServicesRegistry.EntityRegistry.PositionStorage );

            ServicesRegistry.EntityRegistry.RegisterProviders();

            StudioHelper.InitServices();
            StudioHelper.InitProviders();

            var service = new RiskManager();
            MainWindow.UserConfig.TryLoadSettings( "RiskManager", new Action<SettingsStorage>( service.Load ) );
            ConfigManager.RegisterService<IRiskManager>( service );
            InitializeConnector();
            StockSharp.Studio.Controls.Extensions.InitAlerts( token );
            LoadSettings();
            DXSplashScreen.Close();

            CheckIsFirstRun();

            if ( MainWindow.UserConfig.GetAutoConnect() && _connector.Adapter.InnerAdapters.Any<IMessageAdapter>() )
                Connect();

            _ipcChannel.RunServerAsync( token );
            _uiLoaded = true;
            CommandManager.InvalidateRequerySuggested();
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

        // commandService.Register<OpenWindowCommand>( ( object ) this, true, ( Action<OpenWindowCommand> ) ( cmd => cmd.Result = cmd.IsToolWindow ? _layoutManager.OpenToolWindow( cmd.CtrlType ) : _layoutManager.OpenDocumentWindow( cmd.CtrlType ) ), ( Func<OpenWindowCommand, bool> ) null );
        private void OnOpenWindowCommand( OpenWindowCommand cmd )
        {
            cmd.Result = cmd.IsToolWindow ? _layoutManager.OpenToolWindow( cmd.CtrlType ) : _layoutManager.OpenDocumentWindow( cmd.CtrlType );
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
                ( ( Portfolio ) pos ).CopyTo( pf );
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

            var position = cmd.Position ?? _connector.Positions.FirstOrDefault( p => p.Security == cmd.Security );

            if ( position != null && position.CurrentValue == 0 )
                return;

            ClosePosition( position, MathHelper.Abs( position.CurrentValue.Value ) * 2 );
        }




        private void OnClosePositionCommand( ClosePositionCommand cmd )
        {
            if ( cmd.Position == null && cmd.Security == null )
                return;

            Position position = cmd.Position ?? _connector.Positions.FirstOrDefault( p => p.Security == cmd.Security );

            if ( position != null && position.CurrentValue == 0 )
                return;


            ClosePosition( position, MathHelper.Abs( position.CurrentValue.Value ) );
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
                throw new InvalidOperationException( LocalizedStrings.UnknownType.Put( cmd.SecurityType ) );
            }

            if ( !Ecng.Xaml.XamlHelper.ShowModal( ( Window ) securityWindow, Application.Current.GetActiveOrMainWindow() ) )
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





        private void InitializeConnector()
        {
            DXSplashScreen.SetState( ( object ) LocalizedStrings.InitializingConnector );            
            
            _connector = new StudioConnector( 
                                                ServicesRegistry.SecurityStorage, 
                                                ServicesRegistry.PositionStorage, 
                                                ServicesRegistry.StorageRegistry, 
                                                ServicesRegistry.SnapshotRegistry, 
                                                ServicesRegistry.NativeIdStorage, 
                                                ServicesRegistry.MappingStorage, 
                                                ServicesRegistry.ExchangeInfoProvider, 
                                                ServicesRegistry.SecurityAdapterProvider, 
                                                ServicesRegistry.PortfolioAdapterProvider, 
                                                StorageModes.Incremental | StorageModes.Snapshot 
                                             );
            _connector.UpdateEmulatorSettings( MainWindow.UserConfig.LoadEmulatorSettings() );
            _subscriptionManager = new SubscriptionControlManager( ( ISubscriptionProvider ) _connector );
            ServicesRegistry.LogManager.Sources.Add( _connector );

            _connector.Connected                           += ConnectorOnConnectionStateChanged;
            _connector.Disconnected                        += ConnectorOnConnectionStateChanged;
            _connector.ConnectionLost                      += ConnectorOnConnectionLost;
            _connector.ConnectionRestored                  += ConnectorOnConnectionRestored;
            _connector.ConnectionError                     += ConnectorOnConnectionError;
            _connector.OrderRegisterFailReceived           += ( sub, fail ) => RaiseOrderFailedCommand( sub, fail, OrderFailTypes.Register ) ;
            _connector.OrderCancelFailReceived             += ( sub, fail ) => RaiseOrderFailedCommand( sub, fail, OrderFailTypes.Cancel ) ;
            _connector.OrderEditFailReceived               -= ( sub, fail ) => RaiseOrderFailedCommand( sub, fail, OrderFailTypes.Edit ) ;
            _connector.TickTradeReceived                   += ( s, t ) => RaiseEntityCommand<ITickTradeMessage>( s, t ) ;
            _connector.OrderLogReceived                    += ( s, i ) => RaiseEntityCommand<IOrderLogMessage>( s, i ) ;
            _connector.OrderBookReceived                   += ( s, m ) => RaiseEntityCommand<IOrderBookMessage>( s, m ) ;
            _connector.Level1Received                      += ( s, m ) => RaiseEntityCommand<Level1ChangeMessage>( s, m ) ;
            _connector.NewsReceived                        += ( s, n ) => RaiseEntityCommand<News>( s, n ) ;
            _connector.SubscriptionFailed                  += ConnectorOnSubscriptionFailed ;
            _connector.MassOrderCancelFailed               += ( id, error ) => LoggingHelper.LogError( error,  null );
            _connector.CandleReceived                      += ( s, c ) =>
                                                                        {
                                                                            var targets = _subscriptionManager.Get(s);
                                                                            if ( targets.Length == 0 )
                                                                                return;
                                                                            new CandleCommand( s.CandleSeries, c ).Process( ( object ) this, targets );
                                                                        };
            _connector.OrderReceived                       +=  RaiseOrderCommand;
            _connector.OwnTradeReceived                    +=  ( s, t ) => RaiseEntityCommand<MyTrade>( s, t );
            _connector.PortfolioReceived                   += RaisePortfolioCommand;
            _connector.PositionReceived                    += ( s, p ) => RaiseEntityCommand<Position>( s, p );
            _connector.LookupSecuritiesResult              += ( message, securities, error ) =>
                                                                                            {
                                                                                                if ( !_firstSecuritiesInit )
                                                                                                    return;
                                                                                                _firstSecuritiesInit = false;
                                                                                                if ( error != null )
                                                                                                    return;
                                                                                                new FirstInitSecuritiesCommand( securities ).Deep<FirstInitSecuritiesCommand>().Process( ( object ) this, false );
                                                                                            };
            _connector.LookupPortfoliosResult              +=  ( message, portfolios, error ) =>
                                                                {
                                                                    if ( !_firstPortfoliosInit )
                                                                        return;
                                                                    _firstPortfoliosInit = false;
                                                                    if ( error != null )
                                                                        return;
                                                                    Portfolio[] array = portfolios.ToArray<Portfolio>();
                                                                    if ( array.Length == 0 )
                                                                        return;
                                                                    new FirstInitPortfoliosCommand( ( IEnumerable<Portfolio> ) array ).Deep<FirstInitPortfoliosCommand>().Process( ( object ) this, false );
                                                                };
            ConfigManager.RegisterService( new PortfolioDataSource( _connector ) );
            ConfigManager.RegisterService<IConnector>( _connector );
            ConfigManager.RegisterService<Connector>( _connector );
            ConfigManager.RegisterService<ISecurityMessageAdapterProvider>( _connector.Adapter.SecurityAdapterProvider );
            ConfigManager.RegisterService<IPortfolioMessageAdapterProvider>( _connector.Adapter.PortfolioAdapterProvider );
            ConfigManager.RegisterService<IMarketDataProvider>(  _connector );
            ConfigManager.RegisterService<ISubscriptionProvider>( _connector );
            ConfigManager.RegisterService<IPortfolioProvider>( _connector );
            ConfigManager.RegisterService<IPositionProvider>( _connector );
            ConfigManager.RegisterService<INewsProvider>( _connector );
            ConfigManager.RegisterService<IPriceChartDataProvider>( new PriceChartDataProvider( _connector ) );
            ConfigManager.RegisterService<IMessageAdapterProvider>( new StudioMessageAdapterProvider( _connector.Adapter.InnerAdapters, typeof( FixMessageAdapter ) ) );
            TraderHelper.LookupAll( _connector );
            ServicesRegistry.SnapshotRegistry.Init();
        }

        private void ConnectorOnSubscriptionFailed( Subscription subscription, Exception error, bool isRegister )
        {
            if ( error == SubscriptionResponseMessage.NotSupported )
                LoggingHelper.AddWarningLog( MainWindow.UserConfig.LogConfig.Manager.Application, LocalizedStrings.SubscriptionNotSupported, subscription );
            else
                LoggingHelper.LogError( error,null );
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

        private void RaiseOrderFailedCommand( Subscription subscription, OrderFail fail, OrderFailTypes failType )
        {
            var targets = _subscriptionManager.Get(subscription);
            
            if ( targets.Length != 0 )
                new OrderFailCommand( subscription, fail, failType ).Process( ( object ) this, targets );
            
            if ( subscription.SubscriptionMessage.IsLookup() || failType == OrderFailTypes.Cancel )
                return;
            
            _settings.OrderError.TrySendNotification( _settings, LogLevels.Error, LocalizedStrings.OrderFail, fail.Error.Message, new DateTimeOffset?( fail.ServerTime ) );
        }

        private void RaiseOrderCommand( Subscription subscription, Order order )
        {
            var targets = _subscriptionManager.Get(subscription);
            if ( targets.Length != 0 )
                new OrderCommand( subscription, order ).Process( ( object ) this, targets );

            Decimal? newBalance = _prevBalance.TryGetValue2<Order, Decimal>( order);
            
            if ( newBalance.GetValueOrDefault() == order.Balance & newBalance.HasValue )
                return;

            _prevBalance[order] = order.Balance;

            if ( subscription.SubscriptionMessage.IsLookup() )
                return;

            if ( order.IsMatched() )
                _settings.OrderMatched.TrySendNotification( _settings, LogLevels.Info , LocalizedStrings.OrderMatched2, StringHelper.Put( LocalizedStrings.OrderMatched, order, new DateTimeOffset?( order.ServerTime ) ));
            else
                _settings.OrderChanged.TrySendNotification( _settings, LogLevels.Info, LocalizedStrings.OrderChange, StringHelper.Put( LocalizedStrings.OrderChanged, order, new DateTimeOffset?( order.ServerTime )));
        }

        private void RaisePortfolioCommand( Subscription subscription, Portfolio portfolio )
        {
            object[] targets = RaiseEntityCommand<Portfolio>(subscription, portfolio);
            if ( targets.Length == 0 )
                return;
            Decimal? unrealizedPnL = portfolio.UnrealizedPnL;
            Decimal? realizedPnL = portfolio.RealizedPnL;
            Decimal? commission = portfolio.Commission;
            if ( !realizedPnL.HasValue && !unrealizedPnL.HasValue && !commission.HasValue )
                return;
            new PnLChangedCommand( subscription, portfolio, portfolio.LocalTime, realizedPnL, unrealizedPnL, commission ).Process( ( object ) this, targets );
        }

        private object [ ] RaiseEntityCommand<TEntity>( Subscription subscription, TEntity entity ) where TEntity : class
        {
            object[] targets = _subscriptionManager.Get(subscription);
            if ( targets.Length != 0 )
                subscription.ToCommand<TEntity>( entity ).Process( ( object ) this, targets );
            return targets;
        }

        private void InitializeLayoutManager()
        {
            DXSplashScreen.SetState( ( object ) LocalizedStrings.InitializingLayoutManager );
            _layoutManager = new LayoutManager( DockingManager, DocumentHost );
            _layoutManager.Changed += ( Action ) ( () => MainWindow.UserConfig.SetLayout( ( Func<SettingsStorage> ) ( () => GuiDispatcher.GlobalDispatcher.AddSyncAction<SettingsStorage>( ( Func<SettingsStorage> ) ( () => PersistableHelper.Save( ( IPersistable ) _layoutManager ) ) ) ) ) );
            
            ServicesRegistry.LogManager.Sources.Add( _layoutManager );
            ConfigManager.RegisterService<LayoutManager>( _layoutManager );
        }

        private void LoadSettings()
        {
            StudioUserConfig userConfig = MainWindow.UserConfig;
            userConfig.SuspendChangesMonitor();
            userConfig.TryLoadSettings( "Connector",  _connector.Load );
            
            SettingsStorage layout = userConfig.GetValue<SettingsStorage>("Layout", (SettingsStorage) PersistableHelper.LoadFromString<SettingsStorage>( Paths.CreateSerializer<SettingsStorage>(true), StockSharp.Terminal.Properties.Resources.DefaultLayout));
            if ( layout != null )
                LoggingHelper.DoWithLog( ( Action ) ( () => ( ( BaseLogSource ) _layoutManager ).Load( layout ) ) );
            userConfig.ResumeChangesMonitor();
        }

        private void LoadMainWindowSettings()
        {
            LoggingHelper.DoWithLog( ( Action ) ( () =>
            {
                SettingsStorage storage = MainWindow.UserConfig.LoadMainWindowSettings();
                if ( storage == null )
                    return;
                storage.FromStorage( ( Window ) this );
            } ) );
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
            LoggingHelper.AddInfoLog( ( ILogReceiver ) _connector, LocalizedStrings.ConnectionRestored + " " + adapter?.ToString(), Array.Empty<object>() );
            _settings.ConnectionRestored.TrySendNotification( _settings, ( LogLevels ) 3, LocalizedStrings.ConnectionRestored, ( string ) null, new DateTimeOffset?() );
        }

        private void ConnectorOnConnectionLost( IMessageAdapter adapter )
        {
            LoggingHelper.AddErrorLog( ( ILogReceiver ) _connector, LocalizedStrings.ConnectionLostStateIs, new object [1]
            {
         adapter
            } );
            _settings.ConnectionLost.TrySendNotification( _settings, ( LogLevels ) 4, LocalizedStrings.ConnectionLost, ( string ) null, new DateTimeOffset?() );
        }

        private void ConnectorOnConnectionError( Exception obj )
        {
            ConnectorOnConnectionStateChanged();
            int num;
            GuiDispatcher.GlobalDispatcher.AddAction( ( Action ) ( () => num = ( int ) new MessageBoxBuilder().Owner( ( Window ) this ).Caption( Title ).Text( LocalizedStrings.SomeConnectionFailed ).Button( MessageBoxButton.OK ).Warning().Show() ) );
        }

        private void Connect()
        {
            _connector.TryConnect( ( Window ) this );
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
            e.CanExecute = _uiLoaded && IsConnectorInactive();
        }

        private void ConnectorSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            _connector.ConfigureConnector( ( Window ) this );
        }

        private void ConnectDisconnectCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            if ( !_uiLoaded )
                e.CanExecute = false;
            else
                e.CanExecute = _connector.ConnectionState == ConnectionStates.Connected ||
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
            Paths.GetDocUrl( "topics/terminal.html" ).TryOpenLink( ( DependencyObject ) this );
        }

        private void AboutCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new AboutWindow(), ( Window ) this );
        }

        private void QuestionCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            //OpenChat();
        }

        private void CommonSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.ShowCommonSettings<StudioCommonSettings>( null );
        }

        private void ResetSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.TryResetSettings( null );
        }

        private void PortfoliosMapping_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _uiLoaded;
        }

        private void PortfoliosMapping_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new PortfolioMessageAdaptersWindow()
            {
                Portfolios = StudioServicesRegistry.PortfolioDataSource,
                PortfolioAdapterProvider = ServicesRegistry.PortfolioAdapterProvider,
                AdapterProvider = ServicesRegistry.AdapterProvider
            }, ( Window ) this );
        }

        private void SecuritiesMapping_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _uiLoaded;
        }

        private void SecuritiesMapping_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new SecurityMessageAdaptersWindow()
            {
                SecurityProvider = ServicesRegistry.SecurityProvider,
                SecurityAdapterProvider = ServicesRegistry.SecurityAdapterProvider,
                AdapterProvider = ServicesRegistry.AdapterProvider
            }, ( Window ) this );
        }

        private void EmulationSettingsCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _uiLoaded && IsConnectorInactive();
        }

        private void EmulationSettingsCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            MarketEmulatorSettings settings = MainWindow.UserConfig.LoadEmulatorSettings();
            if ( !settings.ShowSettingsWindow<MarketEmulatorSettings>( ( DependencyObject ) Owner, "topics/designer/connections_settings/simulator.html" ) )
                return;
            MainWindow.UserConfig.SaveEmulatorSettings( settings );
            _connector.UpdateEmulatorSettings( settings );
        }

        private object ActiveLayoutContent
        {
            get
            {
                return ( DockingManager.ActiveLayoutItem as LayoutPanel )?.Content;
            }
        }

        private void DockingManager_OnLayoutItemActivated(
          object sender,
          LayoutItemActivatedEventArgs ea )
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
            IStudioControl control1 = control as IStudioControl;
            if ( control1 == null )
                return;
            new ControlOpenedCommand( control1 ).SyncProcess( ( object ) this );
        }

        private void AddWorkArea_ItemClick( object sender, ItemClickEventArgs e )
        {
            AddWorkArea();
        }

        private void AddWorkArea()
        {
            string key = "WorkArea" + Guid.NewGuid().GetFileNameWithoutExtension( );
            var valueTuple = _layoutManager.OpenDocumentWindow<WorkAreaControl>( key,  () =>
                                                                                       {
                                                                                          return new WorkAreaControl() { Key = key };
                                                                                       }
                                                                                );
            IStudioControl studioControl = valueTuple.Item1;
            if ( !valueTuple.Item2 )
                return;

            new LoadLayoutCommand( Properties.Resources.DefaultAreaLayout ).SyncProcess( ( ( IControlsGalleryControl ) studioControl ).State );
        }

        private void CheckIsFirstRun()
        {
            if ( !MainWindow.UserConfig.EnsureFirstTime() )
                return;
            AddWorkArea();
            if ( !_connector.InitStockSharpConnections( ( Window ) this ) )
                return;
            _firstSecuritiesInit = true;
            _firstPortfoliosInit = true;
        }

        private void BugReportCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.SendLogs();
        }

        private void ThemeCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            StockSharp.Studio.Controls.Extensions.InvertThemeAndSave();
        }

        private void RenameAreaCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            WorkAreaControl content = (DockingManager.ActiveDockItem as ContentItem)?.Content as WorkAreaControl;
            int num = content == null ? 0 : (content.IsTitleEditable ? 1 : 0);
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private void RenameAreaCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(e.Source as DependencyObject);
            if ( layoutItem == null )
                return;
            DockingManager.Rename( layoutItem );
        }

        private ValueTask OnSocketConnected(
          IWebSocketService socket,
          CancellationToken cancellationToken )
        {
            return socket.SubscribeNews( ( Window ) this, cancellationToken );
        }

        //[DebuggerNonUserCode]
        //[GeneratedCode( "PresentationBuildTasks", "9.0.0.0" )]
        //public void InitializeComponent()
        //{
        //    if ( _contentLoaded )
        //        return;
        //    _contentLoaded = true;
        //    Application.LoadComponent( ( object ) this, new Uri( "/Terminal;V5.0.0;component/mainwindow.xaml", UriKind.Relative ) );
        //}

        //[DebuggerNonUserCode]
        //[GeneratedCode( "PresentationBuildTasks", "9.0.0.0" )]
        //[EditorBrowsable( EditorBrowsableState.Never )]
        //void IComponentConnector.Connect( int connectionId, object target )
        //{
        //    switch ( connectionId )
        //    {
        //        case 1:
        //        ( ( Window ) target ).Closing += new CancelEventHandler( MainWindow_OnClosing );
        //        ( ( FrameworkElement ) target ).Loaded += new RoutedEventHandler( MainWindow_OnLoaded );
        //        break;
        //        case 2:
        //        ( ( CommandBinding ) target ).CanExecute += new CanExecuteRoutedEventHandler( ConnectorSettingsCommand_OnCanExecute );
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( ConnectorSettingsCommand_OnExecuted );
        //        break;
        //        case 3:
        //        ( ( CommandBinding ) target ).CanExecute += new CanExecuteRoutedEventHandler( ConnectDisconnectCommand_OnCanExecute );
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( ConnectDisconnectCommand_OnExecuted );
        //        break;
        //        case 4:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( HelpCommand_OnExecuted );
        //        break;
        //        case 5:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( AboutCommand_OnExecuted );
        //        break;
        //        case 6:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( QuestionCommand_OnExecuted );
        //        break;
        //        case 7:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( CommonSettingsCommand_OnExecuted );
        //        break;
        //        case 8:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( ResetSettingsCommand_OnExecuted );
        //        break;
        //        case 9:
        //        ( ( CommandBinding ) target ).CanExecute += new CanExecuteRoutedEventHandler( SecuritiesMapping_OnCanExecute );
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( SecuritiesMapping_OnExecuted );
        //        break;
        //        case 10:
        //        ( ( CommandBinding ) target ).CanExecute += new CanExecuteRoutedEventHandler( PortfoliosMapping_OnCanExecute );
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( PortfoliosMapping_OnExecuted );
        //        break;
        //        case 11:
        //        ( ( CommandBinding ) target ).CanExecute += new CanExecuteRoutedEventHandler( EmulationSettingsCommand_OnCanExecute );
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( EmulationSettingsCommand_OnExecuted );
        //        break;
        //        case 12:
        //        ( ( CommandBinding ) target ).CanExecute += new CanExecuteRoutedEventHandler( RenameAreaCommand_OnCanExecute );
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( RenameAreaCommand_OnExecuted );
        //        break;
        //        case 13:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( BugReportCommand_OnExecuted );
        //        break;
        //        case 14:
        //        ( ( CommandBinding ) target ).Executed += new ExecutedRoutedEventHandler( ThemeCommand_OnExecuted );
        //        break;
        //        case 15:
        //        ConnectButton = ( BarButtonItem ) target;
        //        break;
        //        case 16:
        //        ViewMenu = ( BarSubItem ) target;
        //        break;
        //        case 17:
        //        ( ( BarItem ) target ).ItemClick += new ItemClickEventHandler( AddWorkArea_ItemClick );
        //        break;
        //        case 18:
        //        DockingManager = ( DockLayoutManager ) target;
        //        DockingManager.LayoutItemActivated += new LayoutItemActivatedEventHandler( DockingManager_OnLayoutItemActivated );
        //        DockingManager.DockItemActivated += new DockItemActivatedEventHandler( DockingManager_OnDockItemActivated );
        //        DockingManager.DockItemClosed += new DockItemClosedEventHandler( DockingManager_OnDockItemClosed );
        //        break;
        //        case 19:
        //        bRename = ( BarButtonItem ) target;
        //        break;
        //        case 20:
        //        Root = ( LayoutGroup ) target;
        //        break;
        //        case 21:
        //        DocumentHost = ( DocumentGroup ) target;
        //        break;
        //        case 22:
        //        StatusBarPanel = ( StatusBarPanel ) target;
        //        break;
        //        default:
        //        _contentLoaded = true;
        //        break;
        //    }
        //}
    }
}
