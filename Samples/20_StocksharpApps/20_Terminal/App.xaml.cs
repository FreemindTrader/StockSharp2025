using DevExpress.Xpf.Core;
using Ecng.Configuration;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace StockSharp.Terminal
{
    public partial class App : BaseApplication
    {        
        static App()
        {
            ApplicationThemeHelper.ApplicationThemeName = StudioUserConfig.Instance.GetTheme( "VS2019Dark" );
        }

        public App()
        {
            this.AppIcon = ConfigManager.TryGet<string>( "appIcon", "/Properties/stocksharp_studio.ico" );
            this.CheckTargetPlatform = true;
        }

        protected override void OnStartup( StartupEventArgs e )
        {
            SplashScreenControl.Show();
            base.OnStartup( e );
        }

        
    }
}
