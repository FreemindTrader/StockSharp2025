using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using Ecng.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class SplashScreenControl : UserControl
    {
        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(nameof (AppName), typeof (string), typeof (SplashScreenControl), new PropertyMetadata((object) TypeHelper.ApplicationName ));
        public static readonly DependencyProperty AppIconProperty = DependencyProperty.Register(nameof (AppIcon), typeof (string), typeof (SplashScreenControl));
        

        public SplashScreenControl( )
        {
            this.InitializeComponent();
            this.Footer_Text.Text = "Copyright © " + ( object ) DateTime.Now.Year;
            this.AppIcon = ( ( BaseApplication ) Application.Current ).AppIcon;
        }

        public string AppName
        {
            get
            {
                return ( string ) this.GetValue( SplashScreenControl.AppNameProperty );
            }
            set
            {
                this.SetValue( SplashScreenControl.AppNameProperty, ( object ) value );
            }
        }

        public string AppIcon
        {
            get
            {
                return ( string ) this.GetValue( SplashScreenControl.AppIconProperty );
            }
            private set
            {
                this.SetValue( SplashScreenControl.AppIconProperty, ( object ) value );
            }
        }

        public static void Show( )
        {
            DXSplashScreen.Show( o => {
                                            Window wnd = new Window();
                                            wnd.ShowActivated         = false;
                                            wnd.WindowStyle           = WindowStyle.None;
                                            wnd.ResizeMode            = ResizeMode.NoResize;
                                            wnd.AllowsTransparency    = true;
                                            wnd.ShowInTaskbar         = false;
                                            wnd.Topmost               = true;
                                            wnd.SizeToContent         = SizeToContent.WidthAndHeight;
                                            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                            WindowFadeAnimationBehavior.SetEnableAnimation( wnd, true );
                                            wnd.Topmost               = false;
                                            return wnd;
                                     }, 
                                     
                              x =>  {
                                         var splash = new SplashScreenControl();
                                         splash.DataContext = new SplashScreenViewModel();
                                         return ( object ) splash;
                                    },  
                                     null, 
                                     null );
        }              
    }
}