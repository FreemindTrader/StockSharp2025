using DevExpress.Xpf.Core;
using Ecng.Configuration;
using StockSharp.Xaml;
using System;
using System.Windows;
using System.Windows.Threading;

namespace FreemindTrader
{
    public partial class App : BaseApplication
    {
        static App()
        {
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win10Light.Name;
        }

        public App()
        {
            AppIcon = ConfigManager.TryGet( "appIcon", "/Properties/FreemindTrader.ico" );
            CheckTargetPlatform = true;
        }

        protected override void OnStartup( StartupEventArgs e )
        {
            SplashScreenControl.Show();
            base.OnStartup( e );
        }

        private void ApplicationDispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show( MainWindow, e.Exception.ToString() );
            e.Handled = true;
        }

    }
}
