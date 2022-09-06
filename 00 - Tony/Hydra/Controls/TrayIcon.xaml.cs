using DevExpress.Xpf.Bars;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace StockSharp.Hydra.Controls
{
    public partial class TrayIcon : System.Windows.Controls.UserControl, IComponentConnector
    {
        private NotifyIcon _trayIcon;
        private PopupMenu _trayMenu;
        private MainWindow _window;        

        public TrayIcon()
        {
            InitializeComponent();
        }

        public void Show( MainWindow window )
        {
            _window = window ?? throw new ArgumentNullException( nameof( window ) );
            _window.IsVisibleChanged += new DependencyPropertyChangedEventHandler( WindowOnIsVisibleChanged );

            using ( Stream stream = System.Windows.Application.GetResourceStream( new Uri( "pack://application:,,," + ( ( BaseApplication )System.Windows.Application.Current ).AppIcon ) ).Stream )
            {
                _trayIcon = new NotifyIcon() { Icon = new Icon( stream ), Text = Paths.AppName };
            }

            _trayMenu = ( PopupMenu )Resources["TrayMenu"];
            _trayIcon.Click += ( s1, e1 ) =>
            {
                if ( ( ( MouseEventArgs )e1 ).Button == MouseButtons.Left )
                {
                    ShowHideMainWindow( null, null );
                }
                else
                {
                    System.Drawing.Point position = System.Windows.Forms.Cursor.Position;
                    _trayMenu.PlacementRectangle = new Rect( position.X, position.Y, 0.0, 0.0 );
                    _trayMenu.IsOpen = true;
                    _window.Activate();
                }
            };
            _trayIcon.Visible = true;
        }

        public void UpdateState( bool isStarted )
        {
            BarButtonItem barButtonItem1 = ( BarButtonItem )_trayMenu.Items[2];
            BarButtonItem barButtonItem2 = ( BarButtonItem )_trayMenu.Items[3];
            string str = isStarted ? LocalizedStrings.Str242 : LocalizedStrings.Str2421;
            barButtonItem1.Content = str;
        }

        public void ShowErrorMessage( Exception error )
        {
            if ( error == null )
                throw new ArgumentNullException( nameof( error ) );
            _trayIcon.BalloonTipTitle = error.Message;
            _trayIcon.BalloonTipText = error.ToString();
            _trayIcon.BalloonTipIcon = ToolTipIcon.Error;
        }

        public void Close()
        {
            _window.IsVisibleChanged -= WindowOnIsVisibleChanged;
            _trayIcon.Visible = false;
        }

        private void WindowOnIsVisibleChanged( object sender, DependencyPropertyChangedEventArgs args )
        {
            ( ( BarItem )_trayMenu.Items[0] ).Content = _window.IsVisible ? LocalizedStrings.Str1479 : ( object )LocalizedStrings.Str2933;
        }

        private void ShowHideMainWindow( object sender, RoutedEventArgs e )
        {
            _trayMenu.IsOpen = false;
            if ( _window.IsVisible )
            {
                _window.Hide();
            }
            else
            {
                _window.Show();
                _window.WindowState = _window.CurrentWindowState;
                _window.Activate();
            }
        }

        private void MenuExitClick( object sender, RoutedEventArgs e )
        {
            _window.Close();
        }

        public event Action StartStop;

        public event Action Logs;

        private void StartStopClick( object sender, RoutedEventArgs e )
        {
            Action startStop = StartStop;
            if ( startStop == null )
                return;
            startStop();
        }

        private void LogsClick( object sender, RoutedEventArgs e )
        {
            Action logs = Logs;
            if ( logs == null )
                return;
            logs();
        }        
    }
}
