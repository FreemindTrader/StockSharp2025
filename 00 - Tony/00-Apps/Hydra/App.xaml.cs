// Type: StockSharp.Hydra.App
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using DevExpress.Xpf.Core;
using Ecng.Configuration;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Xaml;
using StockSharp.Xaml.CodeEditor;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace StockSharp.Hydra
{
    public partial class App : CodeEditorBaseApplication
    {
        static App()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win10Light.Name;
        }

        public App()
        {
            AppIcon = ConfigManager.TryGet( "appIcon", "/Properties/stocksharp_data.ico" );
            CheckTargetPlatform = true;
        }

        protected override void OnStartup( StartupEventArgs e )
        {
            SplashScreenControl.Show();
            base.OnStartup( e );
        }        
    }
}
