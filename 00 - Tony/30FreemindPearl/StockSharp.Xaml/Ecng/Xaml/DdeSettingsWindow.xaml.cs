// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.DdeSettingsWindow
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Interop.Dde;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    /// <summary>DdeSettingsWindow</summary>
    public class DdeSettingsWindow : ThemedWindow, IComponentConnector
    {

        private XlsDdeClient _xlsDdeClient;
    /// <summary>
    /// </summary>
    public Action StartedAction;
        /// <summary>
        /// </summary>
        public Action StoppedAction;
        /// <summary>
        /// </summary>
        public Action FlushAction;


        /// <summary>
        /// </summary>
        public DdeSettingsWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// </summary>
        public XlsDdeClient DdeClient
        {
            get
            {
                return this._xlsDdeClient;
            }
            set
            {
                XlsDdeClient xlsDdeClient = value;
                if ( xlsDdeClient == null )
                    throw new ArgumentNullException( nameof( 2127280782 ) );
                this._xlsDdeClient = xlsDdeClient;
                this.\u0023\u003DzRqIHjawWF4dx.set_SelectedObject( ( object )value.Settings.Clone() );
                this.\u0023\u003DzB7im5JINVMmh();
            }
        }

        private void \u0023\u003DzB7im5JINVMmh()
        {
            ( ( ContentControl )this.\u0023\u003Dztby\u0024CYg\u003D).Content = this.DdeClient.IsStarted ? ( object )LocalizedStrings.Str242 : ( object )LocalizedStrings.Str2421;
            ( ( UIElement )this.\u0023\u003DzM8FSOYI\u003D).IsEnabled = !this.DdeClient.IsStarted;
        }

        private void \u0023\u003DzKzv51mN1QYyk( object _param1, RoutedEventArgs _param2 )
        {
            this.\u0023\u003DzA5TMb5J06bZb();
            if ( this.DdeClient.IsStarted )
            {
                this.StoppedAction();
                this.DdeClient.Stop();
            }
            else
            {
                this.DdeClient.Start();
                this.StartedAction();
            }
            this.\u0023\u003DzB7im5JINVMmh();
        }

        private void \u0023\u003Dz2y3ulj_BRvNN( object _param1, RoutedEventArgs _param2 )
        {
            this.\u0023\u003DzA5TMb5J06bZb();
            this.FlushAction();
        }

        private void \u0023\u003DzcEny3QYHK_gx( object _param1, RoutedEventArgs _param2 )
        {
            this.\u0023\u003DzA5TMb5J06bZb();
            ( ( Window )this ).DialogResult = new bool?( true );
        }

        private void \u0023\u003DzA5TMb5J06bZb()
        {
            this.DdeClient.Settings.Apply( ( DdeSettings )this.\u0023\u003DzRqIHjawWF4dx.get_SelectedObject() );
        }

        /// <summary>InitializeComponent</summary>
        [DebuggerNonUserCode]
        [GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        public void InitializeComponent()
        {
            if ( this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
            this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
            Application.LoadComponent( ( object )this, new Uri( nameof( 2127281017 ), UriKind.Relative ) );
        }

        [DebuggerNonUserCode]
        [EditorBrowsable( EditorBrowsableState.Never )]
        [GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
          int _param1,
          object _param2)
        {
            switch ( _param1 )
            {
                case 1:
                this.\u0023\u003DzRqIHjawWF4dx = ( PropertyGridControl )_param2;
                break;
                case 2:
                this.\u0023\u003Dztby\u0024CYg\u003D = ( SimpleButton )_param2;
                ( ( ButtonBase )this.\u0023\u003Dztby\u0024CYg\u003D).Click += new RoutedEventHandler( this.\u0023\u003DzKzv51mN1QYyk );
                break;
                case 3:
                this.\u0023\u003DzM8FSOYI\u003D = ( SimpleButton )_param2;
                ( ( ButtonBase )this.\u0023\u003DzM8FSOYI\u003D).Click += new RoutedEventHandler( this.\u0023\u003Dz2y3ulj_BRvNN );
                break;
                case 4:
                ( ( ButtonBase )_param2 ).Click += new RoutedEventHandler( this.\u0023\u003DzcEny3QYHK_gx );
                break;
                default:
                this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
                break;
            }
        }
    }
}
