//using DevExpress.Xpf.Core;
//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.Configuration;
//using Ecng.Interop;
//using Ecng.Localization;
//using Ecng.Reflection;
//using Ecng.Serialization;
//using Ecng.Xaml;
//using Ecng.Xaml.DevExp;
//using StockSharp.Community;
//using StockSharp.Localization;
//using StockSharp.Logging;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Windows;
//using System.Windows.Input;
//using System.Windows.Threading;

//#pragma warning disable 618

//namespace StockSharp.Xaml
//{
//    public abstract class BaseApplication : Application
//    {
//        private bool _isInitialized;
//        private bool _bShowException;

//        public static readonly string AppDataPath;
//        public static readonly string PlatformConfigurationFile;
//        public static readonly string ProxyConfigurationFile;
//        public static readonly string SecurityNativeIdDir;
//        public static readonly string SecurityMappingDir;
//        public static readonly string SecurityExtendedInfo;
//        public static readonly string StorageDir;

//        private static ProxySettings _proxySettings;
//        private static readonly ICommand _helpCommand;
//        private static Mutex _isRunningMutex;
//        private string _appIcon;
//        private bool _checkTargetPlatform;

//        protected BaseApplication( )
//        {
//            ShutdownMode                                      = ShutdownMode.OnMainWindowClose;
//            DispatcherUnhandledException                     += new DispatcherUnhandledExceptionEventHandler( BaseApplication_DispatcherUnhandledException );
//            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler( OnThreadException );
//            AppDomain.CurrentDomain.UnhandledException       += new UnhandledExceptionEventHandler( CurrentDomain_UnhandledException );
//            GuiDispatcher.GlobalDispatcher.Error             += new Action<Exception>( OnDispatcherError );
//            ShowExceptions                                    = true;
//            MessageBoxBuilder.DefaultHandler                  = new DevExpMessageBoxHandler( );
//        }

//        static BaseApplication( )
//        {
//            NameValueCollection appSettings = ConfigManager.AppSettings;

//            string str1                     = appSettings.Get( "settingsPath" );
//            AppDataPath                     = str1.IsEmpty( ) ? Path.Combine( ServerCredentialsExtensions.StockSharpFolder, Title ) : str1.ToFullPathIfNeed( );
//            PlatformConfigurationFile       = Path.Combine( AppDataPath, "platform_config.xml" );
//            ProxyConfigurationFile          = Path.Combine( AppDataPath, "proxy_config.xml" );
//            SecurityNativeIdDir             = Path.Combine( AppDataPath, "NativeId" );
//            SecurityMappingDir              = Path.Combine( AppDataPath, "Symbol mapping" );
//            SecurityExtendedInfo            = Path.Combine( AppDataPath, "Extended info" );
//            StorageDir                      = Path.Combine( AppDataPath, "Storage" );
//            _helpCommand                    = GetHelpCommand( );
//            string disableUI                = appSettings.Get( "disableUIIssue" );

//            if ( ( disableUI != null ? ( disableUI.To<bool>( ) ? 1 : 0 ) : 0 ) == 0 )
//            {
//                return;
//            }

//            DXGridDataController.DisableThreadingProblemsDetection = true;
//        }

//        public bool ShowExceptions
//        {
//            get
//            {
//                return _bShowException;
//            }
//            set
//            {
//                _bShowException = value;
//            }
//        }

//        protected internal virtual IEnumerable<TargetPlatformFeature> ExtendedFeatures
//        {
//            get
//            {
//                return Enumerable.Empty<TargetPlatformFeature>( );
//            }
//        }

//        private void HandleException( Exception exception, bool isWpf )
//        {
//            try
//            {
//                exception.LogError( null );

//                if ( !isWpf )
//                {
//                    return;
//                }

//                if ( !_isInitialized && MainWindow != null )
//                {
//                    _isInitialized = !MainWindow.GetValue<Window, VoidType, bool>( "IsSourceWindowNull", null );
//                }

//                if ( ShowExceptions )
//                {
//                    MessageBoxBuilder messageBoxBuilder = new MessageBoxBuilder( ).Error( exception );
//                    if ( _isInitialized )
//                    {
//                        try
//                        {
//                            messageBoxBuilder.Owner( Current.GetActiveOrMainWindow( ) );
//                        }
//                        catch
//                        {
//                            messageBoxBuilder.Owner( MainWindow );
//                        }
//                    }

//                    messageBoxBuilder.Show( );
//                }
//                if ( _isInitialized )
//                {
//                    return;
//                }

//                ExitApplication( );
//            }
//            catch ( Exception e )
//            {
//                try
//                {
//                    e.LogError( null );
//                }
//                catch ( Exception ex2 )
//                {
//                    Trace.WriteLine( e );
//                    Trace.WriteLine( ex2 );
//                }
//                ExitApplication( );
//            }
//        }

//        private void OnThreadException( object sender, ThreadExceptionEventArgs e )
//        {
//            HandleException( e.Exception, false );
//        }

//        private void BaseApplication_DispatcherUnhandledException(
//          object sender,
//          DispatcherUnhandledExceptionEventArgs e )
//        {
//            if ( !( e.Exception is COMException exception ) || exception.ErrorCode != -2147221040 )
//            {
//                HandleException( e.Exception, true );
//            }

//            e.Handled = true;
//        }

//        private void CurrentDomain_UnhandledException( object sender, UnhandledExceptionEventArgs e )
//        {
//            HandleException( ( Exception )e.ExceptionObject, false );
//        }

//        private static string Title => TypeHelper.ApplicationName.Replace( "S#.", string.Empty );

//        public static ProxySettings ProxySettings
//        {
//            get
//            {
//                return _proxySettings;
//            }
//        }

//        private static void smethod_1( ProxySettings proxySettings_1 )
//        {
//            _proxySettings = proxySettings_1;
//        }

//        public static ICommand HelpCommand
//        {
//            get
//            {
//                return _helpCommand;
//            }
//        }

//        private static ICommand GetHelpCommand( )
//        {
//            return new DelegateCommand( dpo =>
//                                                  {
//                                                      string url = Doc.GetUrl( ( UIElement )dpo );
//                                                      if ( url == null )
//                                                      {
//                                                          return;
//                                                      }

//                                                      HelpButton.CreateUrl( url ).TryOpenLink( ( DependencyObject )dpo );
//                                                  },

//                                                    o =>
//                                                    {
//                                                        if ( o is UIElement element )
//                                                        {
//                                                            return Doc.GetUrl( element ) != null;
//                                                        }

//                                                        return false;
//                                                    } );
//        }

//        public static void StartIsRunning( )
//        {
//            if ( ThreadingHelper.TryGetUniqueMutex( AppDataPath.GetHashCode( ).To<string>( ), out _isRunningMutex ) )
//            {
//                return;
//            }

//            int num = ( int )new MessageBoxBuilder( ).Text( LocalizedStrings.Str2945 ).Warning( ).Show( );
//            Environment.Exit( 1 );
//        }

//        public static void StopIsRunning( )
//        {
//            _isRunningMutex?.ReleaseMutex( );
//        }

//        public string AppIcon
//        {
//            get
//            {
//                return _appIcon;
//            }
//            set
//            {
//                _appIcon = value;
//            }
//        }

//        protected bool CheckTargetPlatform
//        {
//            get
//            {
//                return _checkTargetPlatform;
//            }
//            set
//            {
//                _checkTargetPlatform = value;
//            }
//        }

//        protected override void OnStartup( StartupEventArgs eventArgs )
//        {
//            Resources.MergedDictionaries.Add( new ResourceDictionary( )
//            {
//                Source = new Uri( "pack://application:,,,/StockSharp.Xaml;component/Themes/BaseApplicationResources.xaml" )
//            } );

//            GuiDispatcher.InitGlobalDispatcher( );

//            if ( !ProxyConfigurationFile.IsEmpty( ) && File.Exists( ProxyConfigurationFile ) )
//            {
//                SettingsStorage storage = new XmlSerializer<SettingsStorage>( ).Deserialize( ProxyConfigurationFile );
//                smethod_1( new ProxySettings( ) );
//                ProxySettings.Load( storage );
//                ProxySettings.ApplyProxySettings( );
//            }
//            else
//            {
//                smethod_1( ProxySettings.GetProxySettings( ) );
//            }

//            if ( CheckTargetPlatform )
//            {
//                if ( !( ( IEnumerable<string> )eventArgs.Args ).Any( s => s == Assembly.GetEntryAssembly( ).Location ) && Environment.Is64BitOperatingSystem )
//                {
//                    if ( !Directory.Exists( AppDataPath ) )
//                    {
//                        Directory.CreateDirectory( AppDataPath );
//                    }

//                    TargetPlatformWindow targetPlatformWindow = new TargetPlatformWindow( );
//                    if ( !targetPlatformWindow.AutoStart )
//                    {
//                        bool? result = targetPlatformWindow.ShowDialog( );

//                        if ( !( result.GetValueOrDefault( ) & result.HasValue ) )
//                        {
//                            MainWindow = null;
//                            ExitApplication( );
//                            base.OnStartup( eventArgs );
//                            return;
//                        }
//                    }

//                    switch ( targetPlatformWindow.SelectedPlatform )
//                    {
//                        case Platforms.x86:
//                            StartX86( );
//                            Process.GetCurrentProcess( ).Kill( );
//                            break;

//                        case Platforms.x64:
//                            MainWindow = null;
//                            SetCulture( );
//                            break;
//                    }
//                }
//                else
//                {
//                    string s = ( ( IEnumerable<string> )eventArgs.Args ).FirstOrDefault( str => str.ContainsIgnoreCase( "lang=" ) );
//                    LocalizedStrings.ActiveLanguage = s != null ? s.Remove( "lang=", true ).To<Languages>( ) : LocalizedStrings.ActiveLanguage;
//                    SetCulture( );
//                    return;
//                }
//            }
            
//            base.OnStartup( eventArgs );
//        }

//        private static void ExitApplication( )
//        {
//            Environment.Exit( -1 );
//        }

//        public static void EditProxySettings( Window owner )
//        {
//            ProxyEditorWindow wnd = new ProxyEditorWindow( )
//            {
//                ProxySettings = ProxySettings.Clone( ) ?? new ProxySettings( )
//            };
//            if ( !Ecng.Xaml.XamlHelper.ShowModal( wnd, owner ) )
//            {
//                return;
//            }

//            ProxySettings.Load( wnd.ProxySettings.Save( ) );
//            ProxySettings.ApplyProxySettings( );
//            if ( ProxyConfigurationFile.IsEmpty( ) )
//            {
//                return;
//            }

//            new XmlSerializer<SettingsStorage>( ).Serialize( ProxySettings.Save( ), ProxyConfigurationFile );
//        }

//        private void SetCulture( )
//        {
//            ShutdownMode = ShutdownMode.OnMainWindowClose;
//            CultureInfo cultureInfo = CultureInfo.GetCultureInfo( LocalizedStrings.ActiveLanguage == Languages.English ? "en-US" : "ru-RU" );
//            Thread.CurrentThread.CurrentCulture = cultureInfo;
//            Thread.CurrentThread.CurrentUICulture = cultureInfo;
//        }

//        private static void StartX86( )
//        {
//            var launcher = Path.Combine( AppDataPath, "{0}.x86.exe".Put( Title ) );

//            StockSharp.Xaml.Properties.Resources.Launcher_x86.Save( launcher );

//            Process.Start( launcher, "\"{0}\" {1}{2}".Put( Assembly.GetEntryAssembly( ).Location, "lang=", LocalizedStrings.ActiveLanguage ) );
//        }

//        private void OnDispatcherError( Exception e )
//        {
//            HandleException( e, true );
//        }             
//    }
//}
