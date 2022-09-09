using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using DevExpress.Xpf.Ribbon;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Data;
using Ecng.Interop;
using Ecng.Security;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Database;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.DataProvider.SqlServer;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.Configuration;
using StockSharp.Hydra.Controls;
using StockSharp.Hydra.Core;
using StockSharp.Hydra.Core.Server;
using StockSharp.Hydra.Panes;
using StockSharp.Hydra.Windows;
using StockSharp.Installer.IPC;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Server.Core;
using StockSharp.Studio.Community;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;


#pragma warning disable CS0169
#pragma warning disable CS0414

namespace StockSharp.Hydra
{
    public partial class MainWindow : DXRibbonWindow, IComponentConnector
    {
        public static readonly RoutedCommand TargetPlatformCommand     = new RoutedCommand();
        public static readonly RoutedCommand HelpCommand               = new RoutedCommand();
        public static readonly RoutedCommand AboutCommand              = new RoutedCommand();
        public static readonly RoutedCommand QuestionCommand           = new RoutedCommand();
        public static readonly RoutedCommand OpenPaneCommand           = new RoutedCommand();
        public static readonly RoutedCommand ImportPaneCommand         = new RoutedCommand();
        public static readonly RoutedCommand BoardsCommand             = new RoutedCommand();
        public static readonly RoutedCommand AnalyticsCommand          = new RoutedCommand();
        public static readonly RoutedCommand MemoryStatisticsCommand   = new RoutedCommand();
        public static readonly RoutedCommand StartStopCommand          = new RoutedCommand();
        public static readonly RoutedCommand ResetSettingsCommand      = new RoutedCommand();
        public static readonly RoutedCommand EraseDataCommand          = new RoutedCommand();
        public static readonly RoutedCommand CommonSettingsCommand     = new RoutedCommand();
        public static readonly RoutedCommand ProxySettingsCommand      = new RoutedCommand();
        public static readonly RoutedCommand SynchronizeCommand        = new RoutedCommand();
        public static readonly RoutedCommand ServerSettingsCommand     = new RoutedCommand();
        public static readonly RoutedCommand EditUsersCommand          = new RoutedCommand();
        public static readonly RoutedCommand SubscriptionsCommand      = new RoutedCommand();
        public static readonly RoutedCommand SecurityMappingCommand    = new RoutedCommand();
        public static readonly RoutedCommand SimulatorSettingsCommand  = new RoutedCommand();
        public static readonly RoutedCommand BugReportCommand          = new RoutedCommand();
        public static readonly DependencyProperty IsStartedProperty    = DependencyProperty.Register( nameof( IsStarted ), typeof( bool ), typeof( MainWindow ) );
        public static readonly RoutedCommand TaskEnabledChangedCommand = new RoutedCommand();
        public static readonly RoutedCommand RemoveTaskCommand         = new RoutedCommand();
        public static readonly RoutedCommand AddSourcesCommand         = new RoutedCommand();
        public static readonly RoutedCommand AddToolsCommand           = new RoutedCommand();

        private readonly CancellationTokenSource      _cts             = new CancellationTokenSource();
        private readonly List<IHydraTask>             _tasksToStart    = new List<IHydraTask>();
        private readonly SynchronizedList<IHydraTask> _startedTasks    = new SynchronizedList<IHydraTask>();
        private readonly LogManager                   _logManager;
        private readonly HydraEmailLogListener        _emailListener;
        private LayoutManager                         _layoutManager;
        private static HydraCommonSettings            _settings;
        private bool                                  _closeAppOnStop;
        private bool                                  _isStopping;
        private System.Timers.Timer                   _timer;
        private System.Timers.Timer                   _killTimer;
        private readonly WpfScheduler<IHydraTask>     _scheduler;
        private DispatcherTimer                       _updateStatusTimer;
        private TrayIcon                              _trayIcon;
        private bool                                  _uiLoaded;
        private readonly PermissionCredentialsStorage _credentialsStorage;
        private readonly InstallerChannel             _installerChannel;
        private bool                                  _closeSuccess;
        private bool                                  _ignoreProcessing;


        private static StudioUserConfig UserConfig
        {
            get
            {
                return BaseUserConfig<StudioUserConfig>.Instance;
            }
        }

        internal static HydraCommonSettings Settings
        {
            get
            {
                return _settings ?? ( _settings = UserConfig.GetSettings() );
            }
        }

        public WindowState CurrentWindowState { get; set; }

        public bool IsStarted
        {
            get
            {
                return ( bool )GetValue( IsStartedProperty );
            }
            set
            {
                SetValue( IsStartedProperty, value );
            }
        }

        public Core.Hydra Hydra { get; }

        public MainWindow()
        {
            if ( !Paths.StartIsRunning() )
            {
                int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.ApplicationIsAlreadyRunning ).Warning().Show();
                Environment.Exit( 1 );
            }
            
            _logManager = UserConfig.LogConfig.Manager;
            
            DXSplashScreen.SetState( LocalizedStrings.Initializing );
            
            UserConfig.SuspendChangesMonitor();
            
            var appStartSettings = StockSharp.Configuration.AppStartSettings.TryLoad();
            Helper.RegisterServices( appStartSettings == null || appStartSettings.Online, GetType() );
            ConfigManager.RegisterService( "SecuritiesSelectWindowType", typeof( Windows.SecuritiesWindowEx ) );
            ConfigManager.RegisterService<IStudioCommandService>( new StudioCommandService() );
            
            InitializeComponent();
            
            Studio.Controls.Extensions.InitServices();
            
            _logManager.Listeners.Add( StatusBarPanel );
            _logManager.Listeners.Add( _emailListener = new HydraEmailLogListener( _logManager ) );
            
            Title = Paths.AppNameWithVersion;
            _logManager.Application.AddInfoLog( Title );
            
            LoadMainWindowSettings();
            Settings.UpdateGlobalTimeZone();
            InitializeLayoutManager();
            Instance = this;

            ( ( Action )( () =>
                                {
                                    //DatabaseProviderRegistry.AddProvider<LinqToDB.Data.DataConnection>( () => new LinqToDB.Data.DataConnection( LinqToDB.ProviderName.SqlServer2012, @"Server=.\;Database=SqlServer.2012;Trusted_Connection=True;Enlist=False;" ));
                                    //DatabaseProviderRegistry.AddProvider( () => new SQLiteDataProvider( "SQLite.MS" ) );
                                    DatabaseConnectionCache service = new DatabaseConnectionCache();
                                    ConfigManager.RegisterService( service );

                                    var storage = UserConfig.GetValue<SettingsStorage>( "DatabaseConnectionCache", null );
                                    if ( storage == null )
                                        return;
                                    service.Load( storage );
                                } 
                         ) ).DoWithLog();

            ConfigManager.RegisterService<IBackupService>( new StockSharpBackupService() );
            _credentialsStorage = new PermissionCredentialsStorage( Path.Combine( Paths.AppDataPath, "server_credentials.json" ) );
            var credentialsAuthorization = new PermissionCredentialsAuthorization( _credentialsStorage );
            ConfigManager.RegisterService( _credentialsStorage );

            var authProvider = new AuthorizationProvider();
            authProvider.Add( AuthorizationModes.Anonymous, new AnonymousAuthorization() );
            authProvider.Add( AuthorizationModes.Windows,   new WindowsAuthorization() );
            authProvider.Add( AuthorizationModes.Community, new CommunityAuthorization( Helper.ProductId, Paths.InstalledVersion ) );
            authProvider.Add( AuthorizationModes.Custom,    credentialsAuthorization );

            var serverSettings = UserConfig.GetServerSettings();
            var emulateSetting = UserConfig.LoadEmulatorSettings();

            Hydra = new Core.Hydra( serverSettings, emulateSetting, authProvider, sessionId => _credentialsStorage.TryGetByLogin( sessionId ) ?? _credentialsStorage.TryGetByLogin( "*" ) );

            Hydra.Server.StateChanged   += new Action( HydraServerStateChanged );
            Hydra.Server.ProcessCommand += new Action<CommandTypes>( HydraServerProcessCommand );

            OnUpdateUi( null, null );
            InitializeCommands();
            _logManager.Sources.Add( Hydra );

            ThemeManager.ApplicationThemeChanged += ( s, e ) => UserConfig.SetTheme( ApplicationThemeHelper.ApplicationThemeName );
            
            _scheduler = new WpfScheduler<IHydraTask>( Tasks );
            _scheduler.Starting += tasks =>
                                            {
                                                if ( _isStopping )
                                                    return;
                                                
                                                if ( IsStarted )
                                                {
                                                    AppendStartTask( tasks );
                                                }                                                    
                                                else
                                                {
                                                    StartTasks( tasks );
                                                }                                                    
                                            };

            _scheduler.Stopping += tasks =>
                                            {
                                                if ( _isStopping )
                                                    return;

                                                foreach ( IHydraTask task in tasks )
                                                {
                                                    task.Stop();
                                                }
                                                    
                                            };

            //_installerChannel = new InstallerChannel( Helper.ProductId, this, _logManager.Application );
        }

        public Task<InstallerMessage> GetInstallerResponseAsync( InstallerMessage request, CancellationToken token )
        {
            throw new NotImplementedException();
        }

        //public async Task<InstallerMessage> GetInstallerResponseAsync( InstallerMessage request, CancellationToken token )
        //{
        //    MainWindow mainWindow = this;
        //    if ( !( request is MsgClose ) )
        //        return ( InstallerMessage )null;
        //    mainWindow._closeSuccess = false;
        //    // ISSUE: reference to a compiler-generated method
        //    await Ecng.Xaml.XamlHelper.GuiThreadGetAsync( new Action<CancellationToken>( mainWindow.\u003CGetInstallerResponseAsync\u003Eb__54_0 ), CancellationToken.None );
        //    if ( !mainWindow._closeSuccess )
        //        throw new InvalidOperationException( "application was not closed" );
        //    return ( InstallerMessage )new MsgOk();
        //}

        internal void LoadCredentials()
        {
            _credentialsStorage.LoadCredentials();
        }

        private void InitializeLayoutManager()
        {
            DXSplashScreen.SetState( LocalizedStrings.InitializingLayoutManager );
            _layoutManager = new LayoutManager( DockingManager, DocumentHost );
            _layoutManager.Changed += () => UserConfig.SetLayout( () => GuiDispatcher.GlobalDispatcher.AddSyncAction( () => _layoutManager.Save() ) );
            _logManager.Sources.Add( _layoutManager );
            ConfigManager.RegisterService( _layoutManager );
        }

        private void InitializeCommands()
        {
            DXSplashScreen.SetState( LocalizedStrings.InitializingCommands );
            IStudioCommandService commandService = StudioServicesRegistry.CommandService;
            commandService.Register<ControlChangedCommand>( this, true, cmd => _layoutManager.MarkControlChanged( cmd.Control ), null );
            commandService.Register<LoggedInCommand>( this, true, cmd =>
            {
                this.UpdateTitle( cmd.Profile );
                SplashScreenControl.Show();
                DXSplashScreen.SetState( LocalizedStrings.LoadingSettings );
                Task.Factory.StartNew   ( 
                                            () =>
                                            {
                                                UserConfig.SetIsFirstRun( false );
                                                Hydra.Initialize();
                                                GuiDispatcher.GlobalDispatcher.AddSyncAction( () => DXSplashScreen.SetState( LocalizedStrings.Str2942.Put( LocalizedStrings.Securities ) ) );
                                                GuiDispatcher.GlobalDispatcher.AddAction( () =>
                                                {
                                                    if ( UserConfig.GetDoNotShowQuestionWindow() )
                                                        return;
                                                    this.ShowQuestionWindow( new TimeSpan?( TimeSpan.FromMinutes( 10.0 ) ), null, null, null );
                                                } );
                                            } 
                                        )
                                        .ContinueWith( res =>
                                        {
                                                DXSplashScreen.Close();
                                                if ( res.IsFaulted && res.Exception != null )
                                                {
                                                    ( res.Exception.InnerException ?? res.Exception ).LogError( null );

                                                    if ( new MessageBoxBuilder().Caption( LocalizedStrings.Str2943 ).Text( LocalizedStrings.DatabaseCorruptedResetConfig ).Error().Owner( this ).YesNo().Show() == MessageBoxResult.Yes )
                                                    {
                                                        this.ShowByeByeWindow( true );
                                                        UserConfig.ResetSettings();
                                                        this.Restart();
                                                    }
                                                    else
                                                    {
                                                        Close();
                                                    }                                                        
                                                }
                                                else
                                                {
                                                    Tasks.AddRange( TaskManager.Tasks );
                                                    LoadCredentials();
                                                    InitializeGuiEnvironment();
                                                    ApplySettings( Settings );
                                                    ApplyServerSettings( false );
                                                    _uiLoaded = true;
                                                    CommandManager.InvalidateRequerySuggested();
                                                    
                                                    _timer = new System.Timers.Timer( TimeSpan.FromSeconds( 30.0 ).TotalMilliseconds )
                                                    {
                                                        AutoReset = true
                                                    };

                                                    _timer.Elapsed += ( s, args ) =>
                                                                                {
                                                                                    TimeSpan time = DateTime.Now.TimeOfDay;
                                                                                    this.GuiAsync( () =>
                                                                                                        {
                                                                                                             TimeSpan? stopTime = Settings.StopTime;
                                                                                                             if ( !stopTime.HasValue || !( time >= stopTime.Value ) || !( time < stopTime.Value.Add( TimeSpan.FromMinutes( 5.0 ) ) ) )
                                                                                                                 return;
                                                                                                             AutoStopAndKill();
                                                                                                        } 
                                                                                                  );
                                                                                };


                                                    _timer.Start();

                                                    if ( Tasks.Count == 0 )
                                                    {
                                                        List<Type> source = new List<Type>();
                                                        SourcesWindow wnd1 = new SourcesWindow() { AvailableTasks = AvailableSources };
                                                        if ( Ecng.Xaml.XamlHelper.ShowModal( wnd1, this ) )
                                                            source.AddRange( wnd1.SelectedTasks );
                                                        ToolsWindow wnd2 = new ToolsWindow() { AvailableTasks = AvailableTools };
                                                        if ( Ecng.Xaml.XamlHelper.ShowModal( wnd2, this ) )
                                                            source.AddRange( wnd2.SelectedTasks );
                                                        if ( source.Any() )
                                                            AddTasks( source );
                                                    }

                                                    _scheduler.Start();
                                                }
                                        }, TaskScheduler.FromCurrentSynchronizationContext() );
            }, null );
            commandService.Register<LogInCommand>( this, true, cmd => this.ProcessLogInCommand( _cts.Token ), null );
            commandService.Register<OpenWindowCommand>( this, true, cmd =>
            {
                if ( cmd.CtrlType != typeof( LogManagerPanel ) )
                    return;
                DockingManager.DockController.Activate( LogToolWindow );
            }, null );
        }

        private Type[ ] AvailableSources
        {
            get
            {
                return TaskManager.AvailableTasks.Where( t => !t.IsCategoryOf( MessageAdapterCategories.Tool ) ).ToArray();
            }
        }

        private Type[ ] AvailableTools
        {
            get
            {
                return TaskManager.AvailableTasks.Where( t => t.IsCategoryOf( MessageAdapterCategories.Tool ) ).ToArray();
            }
        }

        public static MainWindow Instance { get; private set; }

        private void MainWindowLoaded( object sender, RoutedEventArgs e )
        {
            UserConfig.ResumeChangesMonitor();
            DXSplashScreen.Close();
            new LogInCommand().Process( this, false );
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            if ( IsStarted )
            {
                if ( new MessageBoxBuilder().Text( LocalizedStrings.Str2944 ).Warning().Owner( this ).YesNo().Show() == MessageBoxResult.Yes )
                {
                    StopAllTasks();
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            if ( !UserConfig.IsReseting )
            {
                if ( !this.ShowByeByeWindow( true ) )
                {
                    e.Cancel = true;
                    return;
                }
                ServicesRegistry.EntityRegistry?.DelayAction.WaitFlush( true );
                if ( ConfigManager.IsServiceRegistered<IStorageRegistry>() )
                {
                    UserConfig.SetLayout( 
                                              () =>
                                              {
                                                  SettingsStorage s = new SettingsStorage();
                                                  GuiDispatcher.GlobalDispatcher.AddSyncAction( () => _layoutManager.Save( s, true ) );
                                                  return s;
                                              } 
                                        );
                    _layoutManager.Dispose();
                    StudioServicesRegistry.CommandService.Dispose();
                    UserConfig.SetRibbon( Ribbon.SaveDevExpressControl() );
                    UserConfig.SaveMainWindowSettings( this );
                    UserConfig.Dispose();
                    ServicesRegistry.DriveCache?.Dispose();
                }
            }
            _scheduler.Stop();
            _cts.Cancel();
            _timer?.Close();
            _trayIcon?.Close();
            Hydra.Dispose();
            Paths.StopIsRunning();
            _closeSuccess = true;
            base.OnClosing( e );
        }

        private void InitializeGuiEnvironment()
        {
            StudioUserConfig settings = UserConfig;
            settings.SuspendChangesMonitor();
            ( ( Action )( () =>
            {
                SettingsStorage storage = settings.GetValue<SettingsStorage>( "Layout", null );
                if ( storage == null )
                    return;
                _layoutManager.Load( storage );
            } ) ).DoWithLog();
            DatabaseConnectionCache dbCache = DatabaseHelper.Cache;
            dbCache.ConnectionCreated += c => UserConfig.SetDelayValue( "DatabaseConnectionCache", () => dbCache.Save() );
            dbCache.ConnectionDeleted += c => UserConfig.SetDelayValue( "DatabaseConnectionCache", () => dbCache.Save() );
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = ( TimeSpan.FromSeconds( 1.0 ) );
            _updateStatusTimer = dispatcherTimer;
            _updateStatusTimer.Tick += ( new EventHandler( OnUpdateUi ) );
            _updateStatusTimer.Start();
            _trayIcon = new TrayIcon();
            _trayIcon.StartStop += () => ExecutedStartStopCommand( null, null );
            _trayIcon.Logs += () => new OpenWindowCommand( typeof( LogManagerPanel ).GUID.ToString(), typeof( LogManagerPanel ), true ).SyncProcess( this );
            _trayIcon.Show( this );
            settings.ResumeChangesMonitor();
        }

        private void LoadMainWindowSettings()
        {
            StudioUserConfig settings = UserConfig;
            ( ( Action )( () =>
            {
                string ribbon = settings.GetRibbon();
                if ( ribbon.IsEmpty() )
                    return;
                Ribbon.LoadDevExpressControl( ribbon );
            } ) ).DoWithLog();
            ( ( Action )( () => UserConfig.LoadMainWindowSettings( this ) ) ).DoWithLog();
        }

        private void ApplySettings( HydraCommonSettings settings )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            _killTimer?.Stop();
            if ( !settings.AutoStart )
                return;
            Start( true );
        }

        private bool ServerOnStartDownloading()
        {
            return this.GuiSync( () =>
            {
                if ( IsStarted )
                    return true;
                if ( _isStopping )
                    return false;
                return StartTasks( Tasks.Where( s => s.IsEnabled ).ToArray() );
            } );
        }

        private void ServerOnStopDownloading()
        {
            this.GuiAsync( () =>
           {
               if ( !IsStarted )
                   return;
               StopAllTasks();
           } );
        }

        private void ServerOnRestarting()
        {
            this.GuiAsync( new Action( this.Restart ) );
        }

        private void AutoStopAndKill()
        {
            if ( IsStarted )
            {
                _killTimer = new System.Timers.Timer( TimeSpan.FromMinutes( 10.0 ).TotalMilliseconds );
                _killTimer.Elapsed += ( arg1, arg2 ) => Process.GetCurrentProcess().Kill();
                _killTimer.Start();
                _closeAppOnStop = true;
                StopAllTasks();
            }
            else
                Close();
        }

        private void ExecutedStartStopCommand( object sender, ExecutedRoutedEventArgs e )
        {
            if ( IsStarted )
                StopAllTasks();
            else
                Start( false );
        }

        private void CanExecuteStartStopCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            if ( IsStarted )
                e.CanExecute = !_isStopping && _tasksToStart.All( t =>
                {
                    if ( t.State != TaskStates.Started )
                        return t.State == TaskStates.Stopped;
                    return true;
                } );
            else
                e.CanExecute = _uiLoaded;
        }

        private void Start( bool auto = false )
        {
            IHydraTask[ ] array = Tasks.Where( l => l.IsEnabled ).ToArray();
            if ( array.Length == 0 )
            {
                int num1 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str2946Params.Put( auto ? LocalizedStrings.Str2947 + Environment.NewLine : ( object )string.Empty ) ).Warning().Owner( this ).Show();
            }
            else
            {
                string str1 = array.GroupBy( s => s.Drive.Path, s => s.Name ).Where( path => path.Count() > 1 ).Aggregate( string.Empty, ( current, g ) => current + LocalizedStrings.Str2948Params.Put( g.JoinCommaSpace(), g.Key ) );
                if ( !str1.IsEmpty() && new MessageBoxBuilder().Text( str1 + Environment.NewLine + LocalizedStrings.Str2949 ).Warning().Owner( this ).YesNo().Show() != MessageBoxResult.Yes )
                    return;
                string str2 = array.Where( t =>
                {
                    HydraTaskSecurity allSecurity = t.GetAllSecurity();
                    if ( allSecurity == null )
                        return false;
                    return t.SupportedDataTypes.Intersect( allSecurity.GetDataTypes() ).Any( dt => !t.IsAllDownloadingSupported( dt ) );
                } ).Select( t => t.Name ).JoinCommaSpace();
                if ( !str2.IsEmpty() )
                {
                    if ( new MessageBoxBuilder().Text( LocalizedStrings.TasksAllInstruments.Put( str2 ) ).Warning().Owner( this ).YesNo().Show() != MessageBoxResult.Yes )
                        return;
                }
                if ( StartTasks( array ) )
                    return;
                int num2 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str2951 ).Warning().Owner( this ).Show();
            }
        }

        protected override void OnStateChanged( EventArgs e )
        {
            base.OnStateChanged( e );
            if ( ServicesRegistry.EntityRegistry == null || !Settings.MinimizeToTray )
                return;
            if ( WindowState == WindowState.Minimized )
                Hide();
            else
                CurrentWindowState = WindowState;
        }

        private void OnUpdateUi( object sender, EventArgs e )
        {
            Core.Hydra hydra = Hydra;
            StatusBarPanel.Status = string.Format( "T={0}     D={1}     OL={2}     L1={3}     C={4}     N={5}     TS={6}", hydra.LoadedTrades, hydra.LoadedDepths, hydra.LoadedOrderLog, hydra.LoadedLevel1, hydra.LoadedCandles, hydra.LoadedNews, hydra.LoadedTransactions );
        }

        private void UpdateStartedUI()
        {
            _trayIcon.UpdateState( IsStarted );
            if ( IsStarted )
            {
                StartStopBigBtn.Content = StartStopBigBtn.ToolTip = LocalizedStrings.Str242;
                StartStopBigBtn.LargeGlyph = ThemedIconsExtension.GetImage( "Pause" );
            }
            else
            {
                StartStopBigBtn.Content = StartStopBigBtn.ToolTip = LocalizedStrings.Str2421;
                StartStopBigBtn.LargeGlyph = ThemedIconsExtension.GetImage( "Start" );
            }
        }

        private void ExecutedHelpCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Paths.GetDocUrl( "a720a275-440a-44ce-86e2-bcec2e0bc55f.htm" ).TryOpenLink( this );
        }

        private void ExecutedAboutCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new AboutWindow(), this );
        }

        private void QuestionCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.ShowQuestionWindow( null, null, null, false );
        }

        private void ExecutedTargetPlatformCommand( object sender, ExecutedRoutedEventArgs e )
        {
            this.TryChangeLaunchMode();
        }

        private void ExecutedExit( object sender, ExecutedRoutedEventArgs e )
        {
            Close();
        }

        public static IPane CreatePane(
          string type,
          DataType dataType,
          BusinessEntities.Security security,
          IMarketDataDrive drive,
          StorageFormats format,
          DateTime? from,
          DateTime? to )
        {
            DataPane dataPane;
            switch ( type )
            {
                case "Candles":
                    CandlesPane candlesPane = new CandlesPane();
                    if ( dataType != null )
                        candlesPane.CandleSeries = dataType.ToCandleSeries( null );
                    dataPane = candlesPane;
                    break;
                case "Depths":
                    dataPane = new DepthPane();
                    break;
                case "Indicator":
                    dataPane = new IndicatorPane();
                    break;
                case "Level1":
                    dataPane = new Level1Pane();
                    break;
                case "News":
                    dataPane = new NewsPane();
                    break;
                case "OptionDesk":
                    dataPane = new OptionDeskPane();
                    break;
                case "OrderLog":
                    dataPane = new OrderLogPane();
                    break;
                case "PositionChanges":
                    dataPane = new PositionChangePane();
                    break;
                case "Ticks":
                    dataPane = new TradesPane();
                    break;
                case "Transactions":
                    dataPane = new TransactionsPane();
                    break;
                default:
                    return null;
            }
            dataPane.SelectedSecurity = security;
            dataPane.Drive = drive;
            dataPane.StorageFormat = format;
            if ( from.HasValue )
                dataPane.From = from;
            if ( to.HasValue )
                dataPane.To = to;
            return dataPane;
        }

        private void ExecutedOpenPaneCommand( object sender, ExecutedRoutedEventArgs e )
        {
            string type = e.Parameter.ToString();
            IPane pane = CreatePane( type, null, null, ServicesRegistry.DriveCache.Drives.FirstOrDefault(), StorageFormats.Binary, new DateTime?(), new DateTime?() );
            if ( pane == null )
            {
                if ( !( type == "Task" ) )
                {
                    if ( type == "Securities" )
                        _layoutManager.OpenDocumentWithKey( typeof( AllSecuritiesPane ) );
                }
                else
                {
                    IHydraTask selectedTask = NavigationBar.SelectedTask;
                    if ( selectedTask != null )
                        pane = EnsureTaskPane( selectedTask );
                }
                if ( pane == null )
                    return;
            }
            ShowPane( pane );
        }

        private void ExecutedImportPaneCommand( object sender, ExecutedRoutedEventArgs e )
        {
            string str = e.Parameter.ToString();
            DataType dataType;
            switch ( str )
            {
                case "Board":
                    dataType = DataType.Board;
                    break;
                case "Candles":
                    dataType = DataType.TimeFrame( TimeSpan.FromMinutes( 1.0 ) );
                    break;
                case "Depths":
                    dataType = DataType.MarketDepth;
                    break;
                case "Level1":
                    dataType = DataType.Level1;
                    break;
                case "News":
                    dataType = DataType.News;
                    break;
                case "OrderLog":
                    dataType = DataType.OrderLog;
                    break;
                case "PositionChanges":
                    dataType = DataType.PositionChanges;
                    break;
                case "Securities":
                    dataType = DataType.Securities;
                    break;
                case "Ticks":
                    dataType = DataType.Ticks;
                    break;
                case "Transactions":
                    dataType = DataType.Transactions;
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( e ), str, LocalizedStrings.Str1655 );
            }
            _layoutManager.OpenDocumentWindow( new ImportPane()
            {
                DataType = dataType
            }, true );
        }

        private void CanExecuteResetSettingsCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !IsStarted;
        }

        private void ExecutedResetSettingsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            this.TryResetSettings( new Action( TaskManager.Reset ) );
        }

        private void ExecutedEraseDataCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new EraseDataWindow()
            {
                StorageRegistry = ServicesRegistry.StorageRegistry,
                EntityRegistry = ServicesRegistry.EntityRegistry
            }, this );
        }

        private void CanExecuteEraseDataCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _uiLoaded && !IsStarted;
        }

        private void ExecutedCommonSettingsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            this.ShowCommonSettings( new Action<HydraCommonSettings>( ApplySettings ) );
        }

        private void ExecutedProxySettingsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            BaseApplication.EditProxySettings( this );
        }

        private void ExecutedSynchronizeCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Ecng.Xaml.XamlHelper.ShowModal( new SynchronizeWindow(), this );
        }

        private void CanExecuteSynchronizeCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !IsStarted && _uiLoaded;
        }

        private void ExecutedBoardsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _layoutManager.OpenDocumentWithKey( typeof( ExchangeBoardPane ) );
        }

        private void ExecutedAnalyticsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            AnalyticsTemplateWindow wnd = new AnalyticsTemplateWindow();
            if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                return;
            AnalyticsPane analyticsPane = new AnalyticsPane();
            analyticsPane.Init( wnd.SelectedTemplate );
            ShowPane( analyticsPane );
        }

        public void ShowPane( IPane pane )
        {
            if ( pane == null )
                throw new ArgumentNullException( nameof( pane ) );
            _layoutManager.OpenDocumentWindow( pane, true );
        }

        private void ExecutedMemoryStatisticsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            MemoryStatistics.AddOrRemove();
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
            RibbonAnalyticsGroup.DataContext = control as AnalyticsPane;
            RibbonAnalyticsGroup.IsEnabled = RibbonAnalyticsGroup.DataContext != null;
            if ( RibbonAnalyticsGroup.IsEnabled )
                RibbonAnalyticsTab.IsSelected = true;
            else if ( ( RibbonAnalyticsTab.IsSelected || RibbonImportTab.IsSelected ) && ( !( control is AnalyticsResultPane ) && !( control is ImportPane ) ) )
                RibbonCommonTab.IsSelected = true;
            IStudioControl control1 = control as IStudioControl;
            if ( control1 == null )
                return;
            new ControlOpenedCommand( control1, true ).SyncProcess( this );
        }

        private void BugReportCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            this.SendLogs( new TimeSpan?(), null, null, true );
        }

        private void CanUILoadedCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _uiLoaded;
        }

        private void ServerUIEnable( bool value )
        {
            this.GuiAsync( () =>
      {
          ServerSwitchButton( () => StartStopServerBtn.EditValue = value );
          SubscriptionsButton.IsEnabled = value;
      } );
        }

        private bool CanStartServer()
        {
            string validate = Core.Hydra.Validate;
            if ( validate.IsEmpty() )
                return true;
            Hydra.Server.AddErrorLog( validate );
            int num = ( int )new MessageBoxBuilder().Text( validate ).Error().Owner( this ).Show();
            return false;
        }

        private void ApplyServerSettings( bool needSave )
        {
            if ( Hydra.ApplyServerSettings( needSave, new Func<bool>( CanStartServer ) ) )
                BaseUserConfig<StudioUserConfig>.Instance.SetServerSettings( Hydra.ServerSettings );
            CredentialsButton.IsEnabled = ( uint )Hydra.ServerSettings.Authorization > 0U;
            ServerSwitchButton( () => StartStopSimulatorBtn.EditValue = Hydra.ServerSettings.SimulatorEnabled );
        }

        private void ServerSwitchButton( Action action )
        {
            _ignoreProcessing = true;
            try
            {
                action();
            }
            finally
            {
                _ignoreProcessing = false;
            }
        }

        private void HydraServerStateChanged()
        {
            switch ( Hydra.Server.State )
            {
                case ChannelStates.Stopped:
                    ServerUIEnable( false );
                    break;
                case ChannelStates.Started:
                    ServerUIEnable( true );
                    break;
            }
        }

        private void HydraServerProcessCommand( CommandTypes command )
        {
            switch ( command )
            {
                case CommandTypes.Start:
                    ServerOnStartDownloading();
                    break;
                case CommandTypes.Stop:
                    ServerOnStopDownloading();
                    break;
                case CommandTypes.Restart:
                    ServerOnRestarting();
                    break;
            }
        }

        private void StartStopServerBtn_EditValueChanged( object sender, RoutedEventArgs e )
        {
            if ( _ignoreProcessing )
                return;
            HydraServerSettings serverSettings = Hydra.ServerSettings;
            if ( serverSettings.IsFixServer )
            {
                Hydra.Server.Close();
                serverSettings.IsFixServer = false;
            }
            else if ( CanStartServer() )
            {
                Hydra.Server.Open();
                serverSettings.IsFixServer = true;
            }
            else
            {
                ServerSwitchButton( () => StartStopServerBtn.EditValue = false );
                return;
            }
            UserConfig.SetServerSettings( serverSettings );
        }

        private void ExecutedServerSettingsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            HydraServerSettings serverSettings = Hydra.ServerSettings;
            SettingsWindow wnd = new SettingsWindow() { Settings = serverSettings.Clone() };
            Doc.SetUrl( wnd, "7d845e99-6bde-437e-b7f4-059be0438894.htm" );
            if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                return;
            serverSettings.Apply( wnd.Settings );
            ApplyServerSettings( true );
        }

        private void ExecutedEditUsersCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _layoutManager.OpenDocumentWithKey( typeof( UsersPane ) );
        }

        private void ExecutedSubscriptionsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _layoutManager.OpenDocumentWithKey( typeof( SubscriptionsPane ) );
        }

        private void ExecutedSecurityMappingCommand( object sender, ExecutedRoutedEventArgs e )
        {
            _layoutManager.OpenDocumentWithKey( typeof( SecurityMappingPane ) );
        }

        private void StartStopSimulatorBtn_EditValueChanged( object sender, RoutedEventArgs e )
        {
            if ( _ignoreProcessing )
                return;
            HydraServerSettings serverSettings = Hydra.ServerSettings;
            serverSettings.SimulatorEnabled = ( bool )StartStopSimulatorBtn.EditValue;
            UserConfig.SetServerSettings( serverSettings );
        }

        private void ExecutedSimulatorSettingsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            MarketEmulatorSettings emulatorSettings = Hydra.EmulatorSettings.Clone();
            if ( !Ecng.Xaml.XamlHelper.ShowModal( new SettingsWindow() { Settings = emulatorSettings }, this ) )
                return;
            Hydra.EmulatorSettings.Apply( emulatorSettings );
            UserConfig.SaveEmulatorSettings( emulatorSettings );
        }

        private void CanExecuteSimulatorSettingsCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            int num;
            if ( _uiLoaded )
            {
                HydraServerSettings serverSettings = Hydra.ServerSettings;
                num = serverSettings != null ? ( serverSettings.SimulatorEnabled ? 1 : 0 ) : 0;
            }
            else
                num = 0;
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private static HydraTaskManager TaskManager
        {
            get
            {
                return HydraTaskManager.Instance;
            }
        }

        public IList<IHydraTask> Tasks
        {
            get
            {
                return NavigationBar.Tasks;
            }
        }

        public IEnumerable<IHydraTask> Sources
        {
            get
            {
                return Tasks.Where( t => !t.IsCategoryOf( MessageAdapterCategories.Tool ) );
            }
        }

        private void ExecutedRemoveTaskCommand( object sender, ExecutedRoutedEventArgs e )
        {
            IHydraTask task = ( IHydraTask )e.Parameter;
            BusyIndicator.BusyContent = LocalizedStrings.Str2902Params.Put( task.Name );
            BusyIndicator.IsBusy = true;
            Task.Factory.StartNew( () => TaskManager.Delete( task ) ).ContinueWithExceptionHandling( this, res =>
            {
                if ( res )
                {
                    _layoutManager.Remove( c =>
              {
                  TaskPane taskPane = c as TaskPane;
                  if ( taskPane != null )
                      return taskPane.Task == task;
                  return false;
              } );
                    Tasks.Remove( task );
                }
                BusyIndicator.IsBusy = false;
            } );
        }

        private void CanExecuteRemoveTaskCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            if ( IsStarted )
            {
                IHydraTask parameter = ( IHydraTask )e.Parameter;
                e.CanExecute = !parameter.IsEnabled;
            }
            else
                e.CanExecute = true;
        }

        private void ExecutedAddSourcesCommand( object sender, ExecutedRoutedEventArgs e )
        {
            SourcesWindow wnd = new SourcesWindow() { AvailableTasks = AvailableSources };
            if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                return;
            AddTasks( wnd.SelectedTasks );
        }

        private void ExecutedAddToolsCommand( object sender, ExecutedRoutedEventArgs e )
        {
            ToolsWindow wnd = new ToolsWindow() { AvailableTasks = AvailableTools };
            if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, this ) )
                return;
            AddTasks( wnd.SelectedTasks );
        }

        private void AddTasks( IEnumerable<Type> taskTypes )
        {
            if ( taskTypes == null )
                throw new ArgumentNullException( nameof( taskTypes ) );
            Type[ ] arr = taskTypes.ToArray();
            if ( arr.Length == 0 )
                throw new ArgumentNullException( nameof( taskTypes ) );
            BusyIndicator.IsBusy = true;
            BusyIndicator.BusyContent = LocalizedStrings.Str2904Params.Put( arr[0].GetTaskDisplayName() );
            Task.Factory.StartNew( () => TaskManager.Create( arr ) ).ContinueWithExceptionHandling( this, ( res, tasks ) =>
            {
                BusyIndicator.IsBusy = false;
                if ( !res )
                    return;
                tasks = tasks.ToArray();
                Tasks.AddRange( tasks );
                IHydraTask hydraTask = tasks.FirstOrDefault();
                if ( hydraTask != null )
                {
                    NavigationBar.SelectedTask = hydraTask;
                    foreach ( IHydraTask task in tasks )
                    {
                        TaskPane taskPane = EnsureTaskPane( task );
                        if ( taskPane != null )
                            ShowPane( taskPane );
                    }
                }
                foreach ( IHydraTask task in tasks )
                {
                    if ( task.GetType().IsObsolete() )
                    {
                        int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.SourceObsolete ).Error().Owner( this ).Show();
                        task.IsEnabled = false;
                        task.SaveSettings();
                    }
                    else if ( new MessageBoxBuilder().Text( LocalizedStrings.EnableTaskNow.Put( task.Name ) ).Question().Owner( this ).YesNo().Show() == MessageBoxResult.Yes )
                    {
                        task.IsEnabled = true;
                        task.SaveSettings();
                    }
                }
            } );
        }

        private TaskPane EnsureTaskPane( IHydraTask task )
        {
            return ( TaskPane )_layoutManager.OpenDocumentWindow( new TaskPane() { Task = task }, true );
        }

        private void ExecutedTaskEnabledChangedCommand( object sender, ExecutedRoutedEventArgs e )
        {
            IHydraTask parameter = ( IHydraTask )e.Parameter;
            parameter.IsEnabled = !parameter.IsEnabled;
            if ( parameter.IsEnabled )
            {
                if ( parameter.GetType().IsObsolete() )
                {
                    int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.SourceObsolete ).Error().Owner( this ).Show();
                    parameter.IsEnabled = false;
                    parameter.SaveSettings();
                }
                else
                    parameter.SaveSettings();
            }
            else
                parameter.SaveSettings();
        }

        private void CanExecuteTaskEnabledChangedCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = !IsStarted;
        }

        private bool AppendStartTask( IEnumerable<IHydraTask> enabledTasks )
        {
            IHydraTask[ ] array = enabledTasks.Where( task =>
            {
                task.AddInfoLog( LocalizedStrings.Str2906 );
                if ( !task.Securities.IsEmpty() )
                    return true;
                task.AddInfoLog( LocalizedStrings.Str2907 );
                return false;
            } ).ToArray();
            if ( array.IsEmpty() )
                return false;
            _tasksToStart.AddRange( array );
            foreach ( IHydraTask hydraTask in array )
            {
                hydraTask.Started += new Action<IHydraTask>( OnStartedTask );
                hydraTask.Stopped += new Action<IHydraTask>( OnStoppedTask );
                hydraTask.Start();
            }
            return true;
        }

        private bool StartTasks( IEnumerable<IHydraTask> enabledTasks )
        {
            if ( enabledTasks == null )
                throw new ArgumentNullException( nameof( enabledTasks ) );
            _tasksToStart.Clear();
            if ( !AppendStartTask( enabledTasks ) )
                return false;
            if ( _emailListener != null )
            {
                _emailListener.ErrorCount = new Decimal();
                _emailListener.ErrorEmailSent = false;
            }
            IsStarted = true;
            UpdateStartedUI();
            return true;
        }

        private void StopAllTasks()
        {
            _isStopping = true;
            _tasksToStart.Where( t => t.State == TaskStates.Started ).ForEach( d => d.Stop() );
        }

        private void OnStartedTask( IHydraTask task )
        {
            _startedTasks.Add( task );
            GuiDispatcher.GlobalDispatcher.AddAction( new Action( CommandManager.InvalidateRequerySuggested ) );
        }

        private void OnStoppedTask( IHydraTask task )
        {
            task.Started -= new Action<IHydraTask>( OnStartedTask );
            task.Stopped -= new Action<IHydraTask>( OnStoppedTask );
            _startedTasks.Remove( task );
            if ( !_startedTasks.IsEmpty() )
                return;
            OnStoppedSources();
        }

        private void OnStoppedSources()
        {
            ServicesRegistry.EntityRegistry.WaitSecuritiesFlush();
            GuiDispatcher.GlobalDispatcher.AddAction( () =>
            {
                IsStarted = false;
                _isStopping = false;
                UpdateStartedUI();
                CommandManager.InvalidateRequerySuggested();
                if ( !_closeAppOnStop )
                    return;
                Close();
            } );
        }

        private void NavigationBar_OnTaskSelected()
        {
            IHydraTask task = NavigationBar.SelectedTask;
            _layoutManager.Activate( c =>
            {
                TaskPane taskPane = c as TaskPane;
                if ( taskPane != null )
                    return taskPane.DataContext == task;
                return false;
            } );
        }

        private void NavigationBar_OnDoubleClick()
        {
            OpenPaneCommand.Execute( "Task", null );
        }

        

        private class HydraEmailLogListener : EmailLogListener
        {
            private readonly LogManager _logManager;
            public bool ErrorEmailSent;
            public Decimal ErrorCount;

            public HydraEmailLogListener( LogManager logManager )
            {
                LogManager logManager1 = logManager;
                if ( logManager1 == null )
                    throw new ArgumentNullException( nameof( logManager ) );
                _logManager = logManager1;
                From = "no-reply@stocksharp.com";
            }

            protected override string GetSubject( LogMessage message )
            {
                return message.Source.Name;
            }

            protected override void OnWriteMessage( LogMessage message )
            {
                if ( ErrorEmailSent || message.Level != LogLevels.Error )
                    return;
                ++ErrorCount;
                HydraCommonSettings settings = Settings;
                int emailErrorCount = settings.EmailErrorCount;
                if ( emailErrorCount <= 0 || ErrorCount <= emailErrorCount )
                    return;
                To = settings.EmailErrorAddress;
                if ( To.IsEmpty() )
                    return;
                ErrorEmailSent = true;
                base.OnWriteMessage( new LogMessage( _logManager.Application, TimeHelper.NowWithOffset, LogLevels.Error, LocalizedStrings.Str2940Params.Put( emailErrorCount ), Array.Empty<object>() ) );
            }
        }
    }
}
