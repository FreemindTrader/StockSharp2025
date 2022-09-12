using Ecng.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for ChatPanel.xaml
    /// </summary>
    public partial class ChatPanel : UserControl, IDisposable
    {
        internal WebBrowser _webBrowser = new WebBrowser();

        static ChatPanel( )
        {
            XamlHelper.SetBrowserEmulationVersion( BrowserEmulationVersion.Version11 );
        }

        public ChatPanel()
        {
            InitializeComponent();

            _webBrowser.HideScriptErrors( true );
        }

        public void Dispose( )
        {
            _webBrowser.Dispose();
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {
            if ( this.IsDesignMode(  ) )
            {
                return;
            }

            _webBrowser.Dispatcher.GuiAsync( () => this._webBrowser.Navigate( "http://chat.stocksharp.com/" ) );
        }

        private void Browser_Navigated( object sender, NavigationEventArgs e )
        {

        }
    }
}
