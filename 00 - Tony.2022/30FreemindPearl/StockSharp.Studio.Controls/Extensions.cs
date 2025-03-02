// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Extensions
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Compilation;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Interop;
using Ecng.Roslyn;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using Ecng.Xaml.Excel;
using Nito.AsyncEx;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Community;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Code;
using System;
using System.Collections.Generic;
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
        private const string _referencesKey = "References";
        private const string _themeKey = "Theme";
        private const string _mainWindowKey = "MainWindow";

        public static void InitServices()
        {
            ConfigManager.RegisterService<IDispatcher>( GuiDispatcher.GlobalDispatcher );
            ConfigManager.RegisterService<IIndicatorProvider>( new ChartIndicatorProvider() );
            ConfigManager.RegisterService<ICompilerService>( new RoslynCompilerService() );
            ConfigManager.RegisterService<IExcelWorkerProvider>( new DevExpExcelWorkerProvider() );
        }

        public static void AddToolControl(
          this RibbonPageGroup page,
          ControlType controlType,
          object sender )
        {
            if ( page == null )
                throw new ArgumentNullException( nameof( page ) );
            if ( controlType == null )
                throw new ArgumentNullException( nameof( controlType ) );
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            Type type = controlType.Type;
            string id = type.GUID.ToString();
            bool isToolWindow = controlType.IsToolWindow;
            StudioCommandAttribute studioCommand = controlType.Type.GetAttribute<StudioCommandAttribute>( true );
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Content = controlType.Name;
            barButtonItem1.ToolTip = controlType.Description;
            barButtonItem1.Glyph = controlType.Icon;
            BarButtonItem barButtonItem2 = barButtonItem1;
            barButtonItem2.SetBinding( BarItem.LargeGlyphProperty, new ThemedIconBinding( ( DrawingImage )controlType.Icon ) );
            if ( studioCommand == null )
                barButtonItem2.ItemClick += ( s, e ) => new OpenWindowCommand( id, type, isToolWindow ).Process( sender, false );
            else
                barButtonItem2.ItemClick += ( s, e ) => studioCommand.CommandType.CreateInstance<IStudioCommand>().Process( sender, false );
            page.Items.Add( barButtonItem2 );
        }

        public static IEnumerable<ControlType> GetControlTypes(
          this IEnumerable<Type> types )
        {
            return types.Select( type => new ControlType( type ) ).ToArray();
        }

        public static void UpdateTitle( this Window window, Client profile )
        {
            if ( window == null )
                throw new ArgumentNullException( nameof( window ) );
            if ( profile == null )
                throw new ArgumentNullException( nameof( profile ) );
            window.Title = window.Title + " [" + profile.DisplayName + "]";
        }

        public static bool ShowByeByeWindow( this Window owner, bool feedback = true )
        {
            return true;
        }

        public static void SendFeedback( this Window owner )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            IProductFeedbackService svc = CommunityServicesRegistry.GetService<IProductFeedbackService>();
            if ( AsyncContext.Run( () => svc.GetByProductAndClientAsync( Helper.ProductId, new long?(), new CancellationToken() ) ) != null )
                return;
            DateTime? nextTimeFeedback = Config.GetNextTimeFeedback();
            DateTime now = DateTime.Now;
            if ( ( nextTimeFeedback.HasValue ? ( nextTimeFeedback.GetValueOrDefault() > now ? 1 : 0 ) : 0 ) != 0 )
                return;
            RatingWindow wnd = new RatingWindow();
            if ( !XamlHelper.ShowModal( wnd, owner ) )
            {
                Config.SetNextTimeFeedback( DateTime.Now.AddDays( 7.0 ) );
            }
            else
            {
                int rating = wnd.RatingValue;
                string comment = wnd.Comment;
                AsyncContext.Run( () =>
                   {
                       IProductFeedbackService productFeedbackService = svc;
                       ProductFeedback entity = new ProductFeedback();
                       entity.Product = new Product()
                       {
                           Id = Helper.ProductId
                       };
                       entity.Rate = rating;
                       entity.Message = new Web.DomainModel.Message() { Body = comment };
                       CancellationToken cancellationToken = new CancellationToken();
                       return productFeedbackService.AddAsync( entity, cancellationToken );
                   } );
                int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.ThankYouForFeedback ).Owner( owner ).Show();
            }
        }

        internal static string GetSystemInfo()
        {
            return string.Format( "{0}: {1}\r\n{2}: {3}", LocalizedStrings.XamlStr47, Environment.Is64BitProcess, LocalizedStrings.OSVersion, Environment.OSVersion );
        }

        public static void ShowQuestionWindow(
          this Window owner,
          string attachPath = null,
          string caption = null,
          string text = null,
          bool isBugReport = false )
        {
            //QuestionWindow wnd = new QuestionWindow() { AttachPath = attachPath, MessagePrompt = isBugReport ? LocalizedStrings.DescribeTheBugInDetails : LocalizedStrings.DescribeTheQuestionInDetails, TextInfo = Extensions.GetSystemInfo(), Caption = "[" + Assembly.GetEntryAssembly().GetName().Name + "] " + caption, Text = text };
            //IFileService fileSvc = CommunityServicesRegistry.GetService<IFileService>();
            //List<StockSharp.Web.DomainModel.File> attachments = new List<StockSharp.Web.DomainModel.File>();
            //wnd.FileProcessing += ( Func<string, byte[ ], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> )( async ( name, body, progress, t ) =>
            //   {
            //       IFileService service = fileSvc;
            //       StockSharp.Web.DomainModel.File file1 = new StockSharp.Web.DomainModel.File();
            //       file1.Name = name;
            //       Stream body1 = body.To<Stream>();
            //       Action<long> progress1 = progress;
            //       CancellationToken cancellationToken1 = t;
            //       Compressions? compression = new Compressions?();
            //       CancellationToken cancellationToken2 = cancellationToken1;

            //       // Tony Fix
            //       //StockSharp.Web.DomainModel.File file2 = await service.UploadFullAsync( file1, body1, 102400, progress1, compression, cancellationToken2 );
            //       attachments.Add( file2 );
            //       return file2;
            //   } );
            //if ( !XamlHelper.ShowModal( wnd, owner ) )
            //    return;
            //ProductBugReport productBugReport = new ProductBugReport();
            //Product product = new Product();
            //product.Id = StockSharp.Studio.Community.Helper.ProductId;
            //productBugReport.Product = product;
            //productBugReport.Version = Paths.InstalledVersion;
            //productBugReport.SystemInfo = wnd.TextInfo;
            //productBugReport.Message = new StockSharp.Web.DomainModel.Message()
            //{
            //    Topic = new Topic() { Title = wnd.Caption },
            //    Body = wnd.Text,
            //    Attachments = attachments.ToEntitySet<StockSharp.Web.DomainModel.File>( 0 )
            //};
            //ProductBugReport report = productBugReport;
            //ILogReceiver application = LogManager.Instance?.Application;
            //application.AddDebugLog( "sending {0} '{1}', {2} attachments", isBugReport ? ( object )"bug report" : ( object )"question", ( object )wnd.Caption, ( object )attachments.Count );
            //IProductBugReportService reportSvc = CommunityServicesRegistry.GetService<IProductBugReportService>();
            //report = AsyncContext.Run<ProductBugReport>( ( Func<Task<ProductBugReport>> )( () => reportSvc.AddAsync( report, new CancellationToken() ) ) );
            //application.AddDebugLog( string.Format( "report: (bug={0}, message={1})", ( object )report.Id, ( object )report.Message.Id ) );
            //int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.ThankYouForQuestion ).Owner( owner ).Show();
        }

        public static void ShowQuestionWindow(
          this Window owner,
          TimeSpan? delay,
          string attachPath = null,
          string caption = null,
          string text = null )
        {
            if ( delay.HasValue )
                ( ( Action )( () => owner.GuiAsync( () =>
                         {
                             int num = ( int )new MessageBoxBuilder().Owner( owner ).Caption( owner.Title ).Text( LocalizedStrings.IfYouHaveAnyQuestions ).YesNo().Show();
                             Config.SetDoNotShowQuestionWindow( true );
                             if ( num != 6 )
                                 return;
                             owner.ShowQuestionWindow( attachPath, caption, text, false );
                         } ) ) ).Timer().Interval( delay.Value, TimeSpan.Zero );
            else
                owner.ShowQuestionWindow( attachPath, caption, text, false );
        }

        public static string PrepareLogsFile( TimeSpan duration, Action<Exception> errorHandler )
        {
            if ( errorHandler == null )
                throw new ArgumentNullException( nameof( errorHandler ) );
            string path2 = string.Format( "logs_{0:yyyyMMdd}", DateTime.Today );
            string logsDir = Config.LogConfig.LogsDir;
            string str1 = Path.Combine( Paths.AppDataPath, path2 );
            string str2 = str1 + ".zip";
            str1.SafeDeleteDir();
            System.IO.File.Delete( str2 );
            try
            {
                Directory.CreateDirectory( str1 );
                DateTime dateTime = duration == TimeSpan.MaxValue ? DateTime.MinValue : DateTime.Today - duration;
                foreach ( string directory in Directory.GetDirectories( logsDir ) )
                {
                    try
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo( directory );
                        if ( !( directoryInfo.LastWriteTime < dateTime ) )
                        {
                            string str3 = Path.Combine( str1, directoryInfo.Name );
                            Directory.CreateDirectory( str3 );
                            IOHelper.CopyDirectory( directoryInfo.FullName, str3 );
                        }
                    }
                    catch ( Exception ex )
                    {
                        errorHandler( ex );
                    }
                }
                ZipFile.CreateFromDirectory( str1, str2 );
            }
            finally
            {
                try
                {
                    str1.SafeDeleteDir();
                }
                catch
                {
                }
            }
            return str2;
        }

        public static void SendLogs(
          this Window owner,
          TimeSpan? duration = null,
          string caption = null,
          string text = null,
          bool isBugReport = false )
        {
            if ( !duration.HasValue )
            {
                LogsDurationWindow wnd = new LogsDurationWindow();
                if ( !XamlHelper.ShowModal( wnd, owner ) )
                    return;
                duration = new TimeSpan?( wnd.Duration );
            }
            if ( caption == null )
                caption = LocalizedStrings.BugReport;
            try
            {
                string str = PrepareLogsFile( duration.Value, ex => ex.LogError( "Prepare logs file error: {0}" ) );
                LogManager instance = LogManager.Instance;
                if ( instance != null )
                    instance.Application.AddDebugLog( "created logs zip: {0}", str );
                try
                {
                    owner.ShowQuestionWindow( str, caption, text, isBugReport );
                }
                finally
                {
                    try
                    {
                        System.IO.File.Delete( str );
                    }
                    catch ( Exception ex )
                    {
                        ex.LogError( null );
                    }
                }
            }
            catch ( Exception ex )
            {
                ex.LogError( "Prepare logs file error: {0}" );
                int num = ( int )new MessageBoxBuilder().Owner( owner ).Error().Text( LocalizedStrings.CanNotCreateLogsFile ).Button( MessageBoxButton.OK ).Show();
            }
        }

        public static Client TryLogin( this Window owner, CancellationToken token )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            ICredentialsProvider credProvider = ServicesRegistry.CredentialsProvider;
            ServerCredentials credentials = null;
            bool flag = ( ( Func<bool> )( () => !credProvider.TryLoad( out credentials ) ) ).DoWithLog();
            Client client;
            while ( true )
            {
                if ( flag )
                {
                    CredentialsWindow credentialsWindow = new CredentialsWindow() { IsLoggedIn = false }.FillWindow( credentials );
                    if ( XamlHelper.ShowModal( credentialsWindow, owner ) )
                    {
                        credentials = credentialsWindow.GetCredentials();
                        credProvider.Save( credentials );
                    }
                    else
                        break;
                }
                try
                {
                    IClientService clientSvc = CommunityServicesRegistry.GetService<IClientService>();
                    client = AsyncContext.Run( () => clientSvc.GetCurrentAsync( new bool?(), token ) );
                    goto label_10;
                }
                catch ( Exception ex )
                {
                    int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str3647 + ex.Message ).Error().Owner( owner ).Show();
                    ex.LogError( null );
                    flag = true;
                }
            }
            owner.Close();
            return null;
        label_10:
            if ( !client.AuthToken.IsEmpty() )
            {
                credentials.Token = client.AuthToken.Secure();
                credentials.Password = null;
                ( ( Action )( () => credProvider.Save( credentials ) ) ).DoWithLog();
            }
            return client;
        }

        public static Client Profile { get; private set; }

        public static void ProcessLogInCommand( this Window owner, CancellationToken token )
        {
            Client profile = owner.TryLogin( token );
            if ( profile == null )
            {
                owner.Close();
            }
            else
            {
                Profile = profile;
                new LoggedInCommand( profile ).Process( owner, false );
                Algo.Subscription mySubscription = new Algo.Subscription( DataType.News, ( SecurityMessage )null );
                Helper.InitServices( m =>
                   {
                       if ( !( m.Source == "StockSharp" ) )
                           return;
                       Algo.Subscription subscription = mySubscription;
                       News entity = new News();
                       entity.Headline = m.Topic.Title;
                       DateTime? expiryDate = m.ExpiryDate;
                       entity.ExpiryDate = expiryDate.HasValue ? new DateTimeOffset?( ( DateTimeOffset )expiryDate.GetValueOrDefault() ) : new DateTimeOffset?();
                       entity.Story = m.Body;
                       entity.Id = m.Id.To<string>();
                       new EntityCommand<News>( subscription, entity ).Process( owner, false );
                   }, () => { }, token );
            }
        }

        public static bool TryChangeLaunchMode( this Window owner )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            if ( new MessageBoxBuilder().Text( LocalizedStrings.Str2952Params.Put( Paths.AppName ) ).Owner( owner ).Info().YesNo().Show() != MessageBoxResult.Yes )
                return false;
            string activeLanguage = LocalizedStrings.ActiveLanguage;
            bool flag = !CommunityServicesRegistry.Offline;
            AppStartSettingsWindow wnd = new AppStartSettingsWindow();
            if ( !XamlHelper.ShowModal( wnd, owner ) || wnd.SelectedLanguage == activeLanguage && wnd.Online == flag )
                return false;
            LocalizedStrings.ActiveLanguage = activeLanguage;
            owner.Restart();
            return true;
        }

        private static StudioUserConfig Config
        {
            get
            {
                return BaseUserConfig<StudioUserConfig>.Instance;
            }
        }

        public static bool TryResetSettings( this Window owner, Action postAction = null )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            if ( new MessageBoxBuilder().Text( LocalizedStrings.Str2954Params.Put( Paths.AppName ) ).Warning().Owner( owner ).YesNo().Show() != MessageBoxResult.Yes )
                return false;
            if ( postAction != null )
                postAction();
            AlertServicesRegistry.TryNotificationService?.Dispose();
            Config.ResetSettings();
            owner.Restart();
            return true;
        }

        public static void Restart( this Window owner )
        {
            if ( owner == null )
                throw new ArgumentNullException( nameof( owner ) );
            owner.Close();
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        public static bool InitStockSharpConnections( this Connector connector, Window owner )
        {
            if ( connector == null )
                throw new ArgumentNullException( nameof( connector ) );
            ServerCredentials credentials;
            if ( new MessageBoxBuilder().Info().Text( LocalizedStrings.SetupStockSharpConnectionFirstTime ).Owner( owner ).YesNo().Show() != MessageBoxResult.Yes || !ServicesRegistry.CredentialsProvider.TryLoad( out credentials ) )
                return false;
            connector.Adapter.InnerAdapters.AddRange( ServicesRegistry.AdapterProvider.CreateStockSharpAdapters( connector.TransactionIdGenerator, credentials.Email, credentials.Password ) );
            Config.SetConnector( connector.Save() );
            Config.SetAutoConnect( true );
            return true;
        }

        public static void TryAddEntities<TEntity>(
          this IList<Position> entities,
          EntityCommand<TEntity> cmd )
          where TEntity : Position
        {
            if ( entities == null )
                throw new ArgumentNullException( nameof( entities ) );
            if ( cmd == null )
                throw new ArgumentNullException( nameof( cmd ) );
            if ( cmd.Entity != null )
            {
                entities.TryAdd( cmd.Entity );
            }
            else
            {
                if ( cmd.Entities == null )
                    return;
                foreach ( TEntity entity in cmd.Entities )
                    entities.TryAdd( entity );
            }
        }

        public static void AddEntities<TEntity>(
          this IList<TEntity> entities,
          EntityCommand<TEntity> cmd )
          where TEntity : class
        {
            if ( entities == null )
                throw new ArgumentNullException( nameof( entities ) );
            if ( cmd == null )
                throw new ArgumentNullException( nameof( cmd ) );
            if ( cmd.Entity != null )
            {
                entities.Add( cmd.Entity );
            }
            else
            {
                if ( cmd.Entities == null )
                    return;
                entities.AddRange( cmd.Entities );
            }
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
            if ( cmd.Entity != null )
            {
                entities.TryAdd( cmd.Entity );
            }
            else
            {
                if ( cmd.Entities == null )
                    return;
                foreach ( TEntity entity in cmd.Entities )
                    entities.TryAdd( entity );
            }
        }

        public static bool CanCloseOrRevert( this Position position )
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
            StudioUserConfig instance = BaseUserConfig<StudioUserConfig>.Instance;
            TSettings commonSettings = instance.GetCommonSettings<TSettings>();
            SettingsWindow wnd = new SettingsWindow() { Settings = commonSettings.Clone() };
            ( ( TSettings )wnd.Settings ).IsDark = ThemeExtensions.IsCurrDark();
            if ( !XamlHelper.ShowModal( wnd, owner ) )
                return;
            commonSettings.Apply( wnd.Settings );
            instance.SetCommonSettings( commonSettings );
            if ( applyChanges != null )
                applyChanges( commonSettings );
            if ( ThemeExtensions.IsCurrDark() != commonSettings.IsDark )
                ThemeExtensions.Invert();
            commonSettings.UpdateGlobalTimeZone();
        }

        public static void UpdateGlobalTimeZone( this StudioCommonSettings settings )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            TimeConverter.GlobalTimeZone = settings.TimeZone;
        }

        public static IEnumerable<CodeReference> GetReferences(
          this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "References", Array.Empty<CodeReference>() );
        }

        public static void SetReferences(
          this IPersistableService service,
          IEnumerable<CodeReference> references )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            if ( references == null )
                throw new ArgumentNullException( nameof( references ) );
            service.SetValue( "References", references.ToArray() );
        }

        public static string GetTheme( this IPersistableService service )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            return service.GetValue( "Theme", ThemeExtensions.DefaultTheme );
        }

        public static void SetTheme( this IPersistableService service, string theme )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            service.SetValue( "Theme", theme );
        }

        public static void LoadMainWindowSettings( this IPersistableService service, Window window )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            SettingsStorage storage = service.GetValue<SettingsStorage>( "MainWindow", null );
            if ( storage == null )
                return;
            window.LoadWindowSettings( storage );
        }

        public static void SaveMainWindowSettings( this IPersistableService service, Window window )
        {
            if ( service == null )
                throw new ArgumentNullException( nameof( service ) );
            SettingsStorage storage = new SettingsStorage();
            window.SaveWindowSettings( storage );
            service.SetValue( "MainWindow", storage );
        }

        public static void InitAlerts()
        {
            //( ( Action )( () =>
            //   {
            //       ConfigManager.RegisterService<IAlertProcessingService>( ( IAlertProcessingService )new AlertProcessingService( 1024 ) );
            //       ConfigManager.RegisterService<IAlertNotificationService>( ( IAlertNotificationService )new AlertNotificationService( 1024, Paths.AppDataPath, null ) );
            //       IAlertProcessingService alertService = AlertServicesRegistry.ProcessingService;
            //       SettingsStorage settings = BaseUserConfig<StudioUserConfig>.Instance.GetAlertService();
            //       if ( settings != null )
            //           Do.Invariant( ( Action )( () => alertService.Load( settings ) ) );
            //       alertService.Registered += new Action<AlertSchema>( AlertsChanged );
            //       alertService.UnRegistered += new Action<AlertSchema>( AlertsChanged );
            //       ServicesRegistry.Connector.NewMessage += new Action<StockSharp.Messages.Message>( alertService.Process );

            //       void AlertsChanged( AlertSchema schema )
            //       {
            //           BaseUserConfig<StudioUserConfig>.Instance.SetAlertService( alertService.Save() );
            //       }
            //   } ) ).DoWithLog();
        }
    }
}
