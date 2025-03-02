using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Xpf.Bars;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Interop;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.Algo.Export;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Strategies.Reporting;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Studio.IPC;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.IndicatorPainters;
using StockSharp.Xaml.Code;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    public static class Extensions
    {
        public static void RegisterUIServices()
        {
            ConfigManager.RegisterService<IDispatcher>( GuiDispatcher.GlobalDispatcher );
            ConfigManager.RegisterService<IIndicatorProvider>( new IndicatorProvider() );
            ConfigManager.RegisterService<IChartIndicatorPainterProvider>( new ChartIndicatorPainterProvider() );
            ConfigManager.RegisterService<IExcelWorkerProvider>( new DevExpExcelWorkerProvider() );
            ConfigManager.RegisterService<IOAuthProvider>( new OAuthProvider() );            
            ConfigManager.RegisterService<IReportGeneratorProvider>( new ReportGeneratorProvider(  new IReportGenerator [4]
                                                                                                                            {
                                                                                                                                new ExcelReportGenerator(ServicesRegistry.ExcelProvider),
                                                                                                                                new XmlReportGenerator(),
                                                                                                                                new CsvReportGenerator(),
                                                                                                                                 new JsonReportGenerator()
                                                                                                                            } ) );
            ConfigManager.RegisterService<IIndicatorColorProvider>( new IndicatorColorProvider() );
            StudioHelper.RegisterCompilerCache();
            IChartExtensions.IndicatorProvider.Init();
            IChartExtensions.IndicatorPainterProvider.Init();
            ConfigManager.RegisterService<ILastDirSelector>( new LastDirSelector( StudioUserConfig.Instance ) );
            PropertyGridEx.CustomEditors.Add( typeof( ITelegramChannelEditor ), typeof( ClientSocialComboBoxEditor ) );
        }

        public static BarButtonItem CreateToolControl<T>( this object sender ) where T : IStudioControl
        {
            return new ControlType( typeof( T ) ).CreateToolControl( sender, ( Action<Type> ) null );
        }

        public static BarButtonItem CreateToolControl( this ControlType controlType, object sender, Action<Type> customHandler )
        {
            if ( controlType == null )
                throw new ArgumentNullException( nameof( controlType ) );
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = controlType.Icon;
            BarButtonItem barButtonItem2 = barButtonItem1;
            barButtonItem2.SetBinding( BarItem.ContentProperty, ( BindingBase ) new Binding()
            {
                Source =  controlType,
                Path = new PropertyPath( "Name", Array.Empty<object>() )
            } );
            barButtonItem2.SetBinding( FrameworkContentElement.ToolTipProperty, ( BindingBase ) new Binding()
            {
                Source =  controlType,
                Path = new PropertyPath( "Description", Array.Empty<object>() )
            } );
            barButtonItem2.SetBinding( BarItem.LargeGlyphProperty, ( BindingBase ) new ThemedIconBinding( ( DrawingImage ) controlType.Icon ) );
            Type type = controlType.Type;
            bool isToolWindow = controlType.IsToolWindow;
            if ( customHandler == null )
                barButtonItem2.ItemClick += ( ItemClickEventHandler ) ( ( s, e ) => new OpenWindowCommand( type, isToolWindow ).Process( sender, false ) );
            else
                barButtonItem2.ItemClick += ( ItemClickEventHandler ) ( ( s, e ) => customHandler( type ) );
            return barButtonItem2;
        }

        private static async Task<Client> TryLogin( this Window owner, CancellationToken token )
        {            
            Client myClient = null;
            try
            {
                if ( owner == null )
                    throw new ArgumentNullException( nameof( owner ) );
                ICredentialsProvider credProvider = ConfigurationServicesRegistry.CredentialsProvider;
                ServerCredentials credentials = (ServerCredentials) null;
                bool autoLogon = true;
                bool flag1 = (bool) LoggingHelper.DoWithLog<bool>( (() => !credProvider.TryLoad(out credentials)));
                
                Client result2;
                while ( true )
                {
                    if ( flag1 )
                    {
                        ValueTuple<ServerCredentials, bool> awaitResult = await CredentialsWindow.TryShow( ( DependencyObject ) owner, credentials );
                        ServerCredentials serverCredentials = awaitResult.Item1;
                        bool isAutoLogon = awaitResult.Item2;
                        
                        if ( serverCredentials != null )
                        {
                            credentials = serverCredentials;
                            autoLogon = isAutoLogon;
                            credProvider.Save( credentials, autoLogon );
                        }
                    }
                    try
                    {
                        myClient = await WebApiServicesRegistry.GetService<IClientService>().GetCurrentAsync(new bool?(), (string) null, token);

                        string accessToken = StockSharp.Web.Common.ClientExtensions.GetAccessToken( myClient );
                         
                        if ( !StringHelper.IsEmpty( accessToken ) )
                        {
                            credentials.Token = ( StringHelper.Secure( accessToken ) );
                            credentials.Password = null;
                            LoggingHelper.DoWithLog( ( Action ) ( () => credProvider.Save( credentials, autoLogon ) ) );
                        }


                        return myClient;
                    }
                    catch ( Exception ex )
                    {
                        int num2 = (int) new MessageBoxBuilder().Text(ex.Message).Error().Owner(owner).Show();
                        LoggingHelper.LogError( ex, ( string ) null );
                        flag1 = true;
                    }
                }                                
            }
            catch ( Exception ex )
            {
                
            }

            return myClient;
        }

        public static ValueTask SubscribeNews( this IWebSocketService socket, Window owner, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            if ( socket == null )
                throw new ArgumentNullException( nameof( socket ) );
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            StockSharp.BusinessEntities.Subscription subscription = new StockSharp.BusinessEntities.Subscription(StockSharp.Messages.DataType.News, (SecurityMessage) null);
            return socket.SubscribeAsync<NewsRequest, NewsResponse>( new NewsRequest(), ( Func<NewsResponse, CancellationToken, ValueTask> ) ( ( r, t ) =>
            {
                string text = r.Text;
                new EntityCommand<News>( subscription, new News()
                {
                    Headline = text,
                    ExpiryDate = new DateTimeOffset?( DateTimeOffset.UtcNow.AddDays( 1.0 ) ),
                    Story = text,
                    Id = ( string ) Converter.To<string>(  r.EntityId )
                } ).Process(  owner, false );
                return new ValueTask();
            } ), cancellationToken );
        }

        public static void ProcessLogInCommand( this Window owner, CancellationToken token )
        {
            if ( WebApiServicesRegistry.Offline )
                new LoggedInCommand( WebApiHelper.Profile ).Process(  owner, false );
            else
                owner.TryLogin( token ).ContinueWith( ( Action<Task<Client>> ) ( t =>
                {
                    if ( t.IsFaulted )
                    {
                        LoggingHelper.LogError( ( Exception ) t.Exception, ( string ) null );
                    }
                    else
                    {
                        Client result = t.Result;
                        if ( result == null )
                        {
                            owner.Close();
                        }
                        else
                        {
                            WebApiHelper.Profile = result;
                            new LoggedInCommand( result ).Process(  owner, false );
                        }
                    }
                } ), token );
        }

        private static StudioUserConfig Config
        {
            get
            {
                return StudioUserConfig.Instance;
            }
        }

        public static bool TryResetSettings( this Window owner, Action postAction = null )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            if ( new MessageBoxBuilder().Text( StringHelper.Put( LocalizedStrings.SettingsWillBeResetContinue, new object [1]
            {
        (object) Paths.AppName
            } ) ).Warning().Owner( owner ).YesNo().Show() != MessageBoxResult.Yes )
                return false;
            if ( postAction != null )
                postAction();
            AlertServicesRegistry.TryNotificationService?.Dispose();
            AlertServicesRegistry.TryProcessingService?.Dispose();
            Extensions.Config.ResetSettings();
            owner.Restart();
            return true;
        }

        public static bool InitStockSharpConnections( this Connector connector, Window owner )
        {
            return false;
        }

        public static void TryAddEntity<TEntity>(
          this IList<StockSharp.BusinessEntities.Position> entity,
          EntityCommand<TEntity> cmd )
          where TEntity : StockSharp.BusinessEntities.Position
        {
            if ( entity == null )
                throw new ArgumentNullException( nameof( entity ) );
            if ( cmd == null )
                throw new ArgumentNullException( nameof( cmd ) );
            if (  cmd.Entity == null )
                return;
            CollectionHelper.TryAdd<StockSharp.BusinessEntities.Position>(  entity,  cmd.Entity );
        }

        public static void AddEntity<TEntity>( this IList<TEntity> entity, EntityCommand<TEntity> cmd ) where TEntity : class
        {
            if ( entity == null )
                throw new ArgumentNullException( nameof( entity ) );
            if ( cmd == null )
                throw new ArgumentNullException( nameof( cmd ) );
            if (  cmd.Entity == null )
                return;
            entity.Add( cmd.Entity );
        }

        public static void TryAddEntities<TEntity>(
          this IList<TEntity> entities,
          EntityCommand<TEntity> cmd )
          where TEntity : class
        {
            if ( entities == null )
                throw new ArgumentNullException( nameof( entities ) );
            if ( cmd == null )
                throw new ArgumentNullException( nameof( cmd ) );
            if (  cmd.Entity == null )
                return;
            CollectionHelper.TryAdd<TEntity>(  entities, cmd.Entity );
        }

        public static bool CanCloseOrRevert( this StockSharp.BusinessEntities.Position position )
        {
            if ( position == null || position is Portfolio )
                return false;
            Decimal? currentValue = position.CurrentValue;
            Decimal num = new Decimal();
            return !( currentValue.GetValueOrDefault() == num & currentValue.HasValue );
        }

        public static void ShowCommonSettings<TSettings>(
          this Window owner,
          Action<TSettings> applyChanges = null )
          where TSettings : StudioCommonSettings, new()
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            StudioUserConfig instance = StudioUserConfig.Instance;
            TSettings commonSettings = instance.GetCommonSettings<TSettings>();
            commonSettings.IsDark = ThemeExtensions.IsCurrDark();
            if ( !commonSettings.ShowSettingsWindow<TSettings>( ( DependencyObject ) owner, ( string ) null ) )
                return;
            instance.SetCommonSettings<TSettings>( commonSettings );
            if ( applyChanges != null )
                applyChanges( commonSettings );
            if ( ThemeExtensions.IsCurrDark() != commonSettings.IsDark )
                ThemeExtensions.Invert();
            commonSettings.UpdateGlobalTimeZone();
            commonSettings.UpdateErrorsDialogs();
        }

        public static void UpdateGlobalTimeZone( this StudioCommonSettings settings )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            TimeConverter.GlobalTimeZone = settings.TimeZone;
        }

        public static void UpdateErrorsDialogs( this StudioCommonSettings settings )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            ( ( BaseApplication ) Application.Current ).ShowExceptions = settings.ErrorsDialogs;
        }

        public static async Task InitAlerts( CancellationToken cancellationToken )
        {
            try
            {
                int valueOrDefault1 = ConfigManager.TryGet<int?>("alertsMaxProcess", new int?()).GetValueOrDefault(10);
                int valueOrDefault2 = ConfigManager.TryGet<int?>("alertsMaxNotify", new int?()).GetValueOrDefault(3);
                ConfigManager.RegisterService<IAlertProcessingService>( ( IAlertProcessingService ) new AlertProcessingService( valueOrDefault1 ) );
                string appDataPath = Paths.AppDataPath;
                WebApiAlertNotificationService notificationService1 = new WebApiAlertNotificationService();
                AlertNotificationService notificationService2 = new AlertNotificationService(valueOrDefault2, appDataPath, (IAlertNotificationService) notificationService1);
                ConfigManager.RegisterService<IAlertNotificationService>( ( IAlertNotificationService ) notificationService2 );
                ConfigManager.RegisterService<IDesktopPopupService>( ( IDesktopPopupService ) notificationService2 );
                ServicesRegistry.LogManager.Sources.Add( AlertServicesRegistry.ProcessingService );
                ServicesRegistry.LogManager.Sources.Add( AlertServicesRegistry.NotificationService );
                IAlertProcessingService alertService = AlertServicesRegistry.ProcessingService;
                SettingsStorage settings = StudioUserConfig.Instance.GetAlertService();
                if ( settings != null )
                    Do.Invariant( ( Action ) ( () => ( ( IPersistable ) alertService ).Load( settings ) ) );
                alertService.Registered += new Action<AlertSchema>( AlertsChanged );
                alertService.UnRegistered += new Action<AlertSchema>( AlertsChanged );
                if ( WebApiServicesRegistry.Offline )
                    return;
                await WebApiServicesRegistry.GetService<IClientSocialService>().RefreshAsync( cancellationToken );

                void AlertsChanged( AlertSchema schema )
                {
                    StudioUserConfig.Instance.SetAlertService( PersistableHelper.Save( ( IPersistable ) alertService ) );
                }
            }
            catch ( Exception ex )
            {
                LoggingHelper.LogError( ex, ( string ) null );
            }
        }

        public static bool TryConnect( this Connector connector, Window owner )
        {
            if ( connector == null )
                throw new ArgumentNullException( nameof( connector ) );
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            IInnerAdapterList innerAdapters = connector.Adapter.InnerAdapters;
            if ( CollectionHelper.IsEmpty<IMessageAdapter>(  innerAdapters ) )
            {
                int num = (int) new MessageBoxBuilder().Owner(owner).Text(LocalizedStrings.AtLeastOneConnectionMustBe).Warning().Show();
                if ( !connector.ConfigureConnector( owner ) )
                    return false;
            }
            if ( CollectionHelper.IsEmpty<IMessageAdapter>(  innerAdapters.SortedAdapters ) )
            {
                int num = (int) new MessageBoxBuilder().Owner(owner).Text(LocalizedStrings.AtLeastOneConnectionActive).Warning().Show();
                if ( !connector.ConfigureConnector( owner ) )
                    return false;
            }
            connector.Connect();
            return true;
        }

        public static bool ConfigureConnector( this Connector connector, Window owner )
        {
            if ( connector == null )
                throw new ArgumentNullException( nameof( connector ) );
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            StudioUserConfig instance = StudioUserConfig.Instance;
            bool autoConnect = instance.GetAutoConnect();
            SettingsStorage connectorWindow = instance.GetConnectorWindow();
            int num = connector.Adapter.Configure(owner, ref autoConnect, ref connectorWindow) ? 1 : 0;
            instance.SetConnectorWindow( connectorWindow );
            if ( num == 0 )
                return false;
            instance.SetConnector( PersistableHelper.Save( ( IPersistable ) connector ) );
            instance.SetAutoConnect( autoConnect );
            return true;
        }

        public static StudioChannel CreateChannel( this LogManager logManager, Func<StudioMessage, CancellationToken, ValueTask<StudioMessage>> handler, DependencyObject owner )
        {
            if ( logManager == null )
                throw new ArgumentNullException( nameof( logManager ) );
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            return new StudioChannel( WebApiHelper.ProductId, Paths.CreateSerializer<SettingsStorage>( true ), handler, ( ILogSource ) logManager.Application, new Func<bool>( receiver ) );

            bool receiver()
            {
                return ( ( DispatcherObject ) owner ).GuiSync<bool>( ( Func<bool> ) ( () =>
                {
                    if ( new MessageBoxBuilder().Owner( owner ).Text( LocalizedStrings.InstallerNotStarted ).Warning().YesNo().Show() != MessageBoxResult.Yes )
                        return false;
                    string installedPath = Paths.TryGetInstalledPath(16L);
                    if ( StringHelper.IsEmpty( installedPath ) )
                    {
                        int num = (int) new MessageBoxBuilder().Owner(owner).Text(StringHelper.Put(LocalizedStrings.AppNotFound, new object[1]
            {
              (object) LocalizedStrings.Installer
            })).Warning().Show();
                        return false;
                    }
                    return Process.Start( new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = "StockSharp.Installer.UI.exe",
                        WorkingDirectory = installedPath
                    } ) != null;
                } ) );
            }
        }

        public static bool HideBars { get; set; }

        public static void TryHideBar( this IBarManagerControl ctrl )
        {
            if ( ctrl == null )
                throw new ArgumentNullException( nameof( ctrl ) );
            if ( !Extensions.HideBars )
                return;
            ctrl.Bar.Visibility = Visibility.Collapsed;
        }

        public static void InvertThemeAndSave()
        {
            ThemeExtensions.Invert();
            StudioUserConfig instance = StudioUserConfig.Instance;
            StudioCommonSettings commonSettings = instance.GetCommonSettings<StudioCommonSettings>();
            commonSettings.IsDark = ThemeExtensions.IsCurrDark();
            instance.SetCommonSettings<StudioCommonSettings>( commonSettings );
        }
    }
}
