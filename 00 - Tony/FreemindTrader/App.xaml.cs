using DevExpress.Xpf.Core;
using Ecng.Configuration;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Xaml;
using System;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;

namespace FreemindTrader
{
    public partial class App : BaseApplication
    {
        static App()
        {
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
