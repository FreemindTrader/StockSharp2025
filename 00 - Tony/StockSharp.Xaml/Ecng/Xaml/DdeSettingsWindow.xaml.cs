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
    public partial class DdeSettingsWindow : ThemedWindow, IComponentConnector
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
                    throw new ArgumentNullException( "xlsDdeClient == null" );

                this._xlsDdeClient = xlsDdeClient;
                DdeSettingsGrid.SelectedObject = ( ( object )value.Settings.Clone() );
                this.ToggleStartStop();
            }
        }

        private void ToggleStartStop()
        {
            StartStop.Content = this.DdeClient.IsStarted ? ( object )LocalizedStrings.Str242 : ( object )LocalizedStrings.Str2421;
            Flush.IsEnabled = !this.DdeClient.IsStarted;
        }

        private void OnStartStopClicked( object _param1, RoutedEventArgs _param2 )
        {
            this.ApplySettings();
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
            this.ToggleStartStop();
        }

        private void OnFlushButtonClicked( object _param1, RoutedEventArgs _param2 )
        {
            this.ApplySettings();
            this.FlushAction();
        }

        private void OnButtonClicked( object _param1, RoutedEventArgs _param2 )
        {
            this.ApplySettings();
            ( ( Window )this ).DialogResult = new bool?( true );
        }

        private void ApplySettings()
        {
            this.DdeClient.Settings.Apply( ( DdeSettings )this.DdeSettingsGrid.SelectedObject );
        }
    }
}
