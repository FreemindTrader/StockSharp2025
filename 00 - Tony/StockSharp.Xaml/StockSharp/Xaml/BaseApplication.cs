using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Grid.TreeList;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Localization;
using Ecng.Reflection;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using TheArtOfDev.HtmlRenderer.WPF.Adapters;

namespace StockSharp.Xaml
{
    /// <summary>The base class for WPF applications.</summary>
    public abstract class BaseApplication : System.Windows.Application
    {
        //private sealed class HelpCommandLambaClass
        //{
        //    public static readonly BaseApplication.HelpCommandLambaClass _i033 = new BaseApplication.HelpCommandLambaClass();
        //    public static Action<object> _A033;
        //    public static Predicate<object> _P033;
        //    public static Func<string, bool> _F033;

        //    internal void F011( object dpo )
        //    {
        //        string url = Doc.GetUrl( ( UIElement )dpo );
        //        if ( url == null )
        //            return;
        //        Paths.GetDocUrl( url ).TryOpenLink( ( DependencyObject )dpo );
        //    }

        //    internal bool F012( object o )
        //    {
        //        UIElement element = o as UIElement;
        //        if ( element != null )
        //            return Doc.GetUrl( element ) != null;
        //        return false;
        //    }

        //    internal bool F013( string _param1 )
        //    {
        //        return StringHelper.ContainsIgnoreCase( _param1, nameof( 2127269627 ) );
        //    }
        //}

        public static ICommand HelpCommand
        {
            get
            {
                return BaseApplication._helpCommand;
            }
        }

        /// <summary>Proxy-server settings.</summary>
        public static ProxySettings ProxySettings
        {
            get
            {
                return BaseApplication._proxySettings;
            }

            set
            {
                _proxySettings = value;
            }
        }


        /// <summary>The application icon.</summary>
        public string AppIcon
        {
            get
            {
                return this._appIcon;
            }
            set
            {
                this._appIcon = value;
            }
        }

        /// <summary>Whether to check the platform at start.</summary>
        protected bool CheckTargetPlatform
        {
            get
            {
                return this._checkTargetPlatform;
            }
            set
            {
                this._checkTargetPlatform = value;
            }
        }



        private static ICommand GetHelpCommand()
        {
            return new DelegateCommand( dpo =>
            {
                string url = Doc.GetUrl( ( UIElement )dpo );
                if ( url == null )
                {
                    return;
                }

                Paths.GetDocUrl( url ).TryOpenLink( ( DependencyObject )dpo );
            },

                                                    o =>
                                                    {
                                                        if ( o is UIElement element )
                                                        {
                                                            return Doc.GetUrl( element ) != null;
                                                        }

                                                        return false;
                                                    } );
        }
        private static readonly ICommand _helpCommand = BaseApplication.GetHelpCommand();

        private bool _isInitialized;

        private bool _showExceptions;

        private static ProxySettings _proxySettings;

        private string _appIcon;

        private bool _checkTargetPlatform;

        internal sealed class HtmlRenderRContextMenu : RContextMenu
        {

            private readonly PopupMenu _popupMenu;

            public HtmlRenderRContextMenu()
            {
                PopupMenu popupMenu = new PopupMenu();
                popupMenu.GlyphSize = GlyphSize.Custom;
                popupMenu.CustomGlyphSize = Size.Empty;
                this._popupMenu = popupMenu;
            }

            public override int ItemsCount
            {
                get
                {
                    return ( ( Collection<IBarItem> )this._popupMenu.Items ).Count;
                }
            }

            public override void AddDivider()
            {
                ( ( Collection<IBarItem> )this._popupMenu.Items ).Add( ( IBarItem )new BarItemSeparator() );
            }

            public override void AddItem( string _param1, bool _param2, EventHandler evtHandler )
            {
                var btn = new BarButtonItem();
                btn.Content = _param1;
                btn.IsEnabled = _param2;
                btn.ItemClick += new ItemClickEventHandler( evtHandler );

                _popupMenu.Items.Add( btn );
            }

            public override void RemoveLastDivider()
            {
                if ( !( this._popupMenu.Items[this._popupMenu.Items.Count - 1] is BarItemSeparator ) )
                    return;

                _popupMenu.Items.RemoveAt( this._popupMenu.Items.Count - 1 );
            }

            public override void Show( RControl _param1, RPoint _param2 )
            {
                this._popupMenu.PlacementRectangle = new Rect( new Point( _param2.X, _param2.Y ), Size.Empty );
                this._popupMenu.IsOpen = true;
            }

            public override void Dispose()
            {
                _popupMenu.IsOpen = false;
                _popupMenu.PlacementTarget = null;
                _popupMenu.Items.Clear();
            }
        }

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Xaml.BaseApplication" />.
        /// </summary>
        protected BaseApplication()
        {
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler( this.OnThreadException );
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler( this.OnCurrentDomainUnhandledException );
            GuiDispatcher.GlobalDispatcher.Error += new Action<Exception>( this.OnGlobalDispatcherError );
            this.ShowExceptions = true;
            MessageBoxBuilder.DefaultHandler = ( IMessageBoxHandler )new DevExpMessageBoxHandler();
            var instance = WpfAdapter.Instance;
            instance.CreateContextMenuHandler += ( new Func<RContextMenu>( this.OnCreateContextMenuHandler) );
            instance.SaveToFileHandler += ( new Action<RImage, string, string, RControl>( this.OnSaveToFileHandler) );
        }

        private void OnThreadException( object _param1, ThreadExceptionEventArgs _param2 )
        {
            this.HandleException( _param2.Exception, false );
        }

        private void OnCurrentDomainUnhandledException( object _param1, UnhandledExceptionEventArgs _param2 )
        {
            this.HandleException( ( Exception )_param2.ExceptionObject, false );
        }

        private void OnGlobalDispatcherError( Exception _param1 )
        {
            this.HandleException( _param1, true );
        }


        private void OnDispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            COMException exception = e.Exception as COMException;
            if ( exception == null || exception.ErrorCode != -2147221040 )
                this.HandleException( e.Exception, true );
            e.Handled = ( true );
        }

        private void HandleException( Exception ex, bool showHandleEx )
        {
            try
            {
                LoggingHelper.LogError( ex, ( string )null );
                if ( !showHandleEx )
                    return;

                if ( !this._isInitialized && this.MainWindow != null )
                {
                    this._isInitialized = ReflectionHelper.GetValue<Window, VoidType, bool>( this.MainWindow, "IsSourceWindowNull", null ) == false;
                }


                if ( this.ShowExceptions )
                {
                    MessageBoxBuilder messageBoxBuilder = new MessageBoxBuilder().Error( ex );
                    if ( this._isInitialized )
                    {
                        try
                        {
                            messageBoxBuilder.Owner( System.Windows.Application.Current.GetActiveOrMainWindow() );
                        }
                        catch
                        {
                            messageBoxBuilder.Owner( this.MainWindow );
                        }
                    }
                    int num = ( int )messageBoxBuilder.Show();
                }
                if ( this._isInitialized )
                    return;
                BaseApplication.ExitProgram();
            }
            catch ( Exception ex1 )
            {
                try
                {
                    LoggingHelper.LogError( ex1, ( string )null );
                }
                catch ( Exception ex2 )
                {
                    Trace.WriteLine( ( object )ex1 );
                    Trace.WriteLine( ( object )ex2 );
                }
                BaseApplication.ExitProgram();
            }
        }

        private static void ExitProgram()
        {
            Environment.Exit( -1 );
        }

        private void OnSaveToFileHandler( RImage _param1, string _param2, string _param3, RControl _param4 )
        {
            var dlg = new DXSaveFileDialog();
            dlg.Filter = "Images|*.png;*.bmp;*.jpg;*.tif;*.gif;*.wmp;";
            dlg.FileName = _param2;
            dlg.DefaultExt = ( _param3 );
            ( ( Microsoft.Win32.CommonDialog )dlg ).ShowDialog().GetValueOrDefault();
        }

        private RContextMenu OnCreateContextMenuHandler()
        {
            return ( RContextMenu )new BaseApplication.HtmlRenderRContextMenu();
        }

        /// <summary>
        /// To show errors on the screen or just to pass them to <see cref="T:StockSharp.Logging.LogManager" />. By default, errors are displayed.
        /// </summary>
        public bool ShowExceptions
        {
            get
            {
                return this._showExceptions;
            }
            set
            {
                this._showExceptions = value;
            }
        }




        static BaseApplication()
        {
            bool? disableUIIssueString = ( bool? )ConfigManager.TryGet<bool?>( "disableUIIssue", new bool?() );

            if ( disableUIIssueString.GetValueOrDefault() == true & disableUIIssueString.HasValue )
            {
                DXGridDataController.DisableThreadingProblemsDetection = true;
                TreeListDataController.DisableThreadingProblemsDetection = true;
            }

            bool? disableExpect100 = ( bool? )ConfigManager.TryGet<bool?>( "disableExpect100Continue", new bool?() );

            if ( !( disableExpect100.GetValueOrDefault() == true & disableExpect100.HasValue ) )
                return;

            ServicePointManager.Expect100Continue = false;
        }









        private static void SetProxySettings( ProxySettings _param0 )
        {
            BaseApplication._proxySettings = _param0;
        }

        /// <summary>Help command.</summary>
        /// <summary>Processing the application start.</summary>
        /// <param name="e">Argument.</param>
        protected override void OnStartup( StartupEventArgs e )
        {
            this.Resources.MergedDictionaries.Add( new ResourceDictionary()
            {
                Source = new Uri( "pack://application:,,,/StockSharp.Xaml;component/Themes/BaseApplicationResources.xaml" )
            } );

            GuiDispatcher.InitGlobalDispatcher();
            BaseApplication.SetProxySettings( ProxySettings.GetProxySettings() );

            if ( this.CheckTargetPlatform )
            {
                var executablePath = Assembly.GetEntryAssembly().Location;

                if ( e.Args.Any( a => a == executablePath ) || !Environment.Is64BitOperatingSystem )
                {
                    string language = e.Args.FirstOrDefault( s => StringHelper.ContainsIgnoreCase( s, "lang=" ) );

                    LocalizedStrings.ActiveLanguage = ( language != null ? StringHelper.Remove( language, "lang=", true ) : LocalizedStrings.ActiveLanguage );

                    this.SetCulture();

                    return;
                }
                Directory.CreateDirectory( ( string )Paths.AppDataPath );
                AppStartSettingsWindow startSettingsWindow = new AppStartSettingsWindow();
                if ( !startSettingsWindow.AutoStart )
                {
                    bool? nullable = startSettingsWindow.ShowDialog();
                    if ( !( nullable.GetValueOrDefault() & nullable.HasValue ) )
                    {
                        this.MainWindow = ( Window )null;
                        BaseApplication.ExitProgram();
                        goto label_7;
                    }
                }
                this.MainWindow = ( Window )null;
                this.SetCulture();
            }
            label_7:
            base.OnStartup( e );
        }

        private void SetCulture()
        {
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var cultureInfo = CultureInfo.GetCultureInfo( LocalizedStrings.ActiveLanguage == "us" ? "en-US" : "ru-RU" );

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }        

        /// <summary>To edit the proxy settings.</summary>
        /// <param name="owner">Parent window.</param>
        public static void EditProxySettings( Window owner )
        {
            SettingsWindow settingsWindow1 = new SettingsWindow();
            ProxySettings proxySettings = BaseApplication.ProxySettings;
            settingsWindow1.Settings = ( IPersistable )( ( proxySettings != null ? PersistableHelper.Clone<ProxySettings>( proxySettings ) : null ) ?? new ProxySettings() );
            SettingsWindow settingsWindow2 = settingsWindow1;
            if ( !XamlHelper.ShowModal( ( Window )settingsWindow2, owner ) )
                return;
            PersistableHelper.Apply<IPersistable>( BaseApplication.ProxySettings, settingsWindow2.Settings );
            BaseApplication.ProxySettings.ApplyProxySettings( true );
        }





    }
}
