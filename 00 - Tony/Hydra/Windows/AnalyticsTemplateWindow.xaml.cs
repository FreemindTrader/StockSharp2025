using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Xaml;
using StockSharp.Hydra.Properties;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Windows
{
    public partial class AnalyticsTemplateWindow : ThemedWindow, IComponentConnector
    {
        public AnalyticsTemplateWindow()
        {
            InitializeComponent();
            TemplateCtrl.SetItemsSource(
                   new AnalyticsTemplate[3]
                {
                    new AnalyticsTemplate( )
                    {
                        Title = LocalizedStrings.Str3636,
                        Body = Properties.Resources.NewAnalyticsStrategy
                    },
                    new AnalyticsTemplate( )
                    {
                        Title = LocalizedStrings.Str2839,
                        Body = Properties.Resources.DailyHighestVolumeStrategy
                    },
                    new AnalyticsTemplate( )
                    {
                        Title = LocalizedStrings.VolumeProfile,
                        Body = Properties.Resources.PriceVolumeDistributionStrategy
                    }
                },
                    t => t.Title );
            TemplateCtrl.SelectedIndex = 0;
        }

        private void TemplateCtrl_OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        { OkBtn.IsEnabled = SelectedTemplate != null; }

        public AnalyticsTemplate SelectedTemplate
        {
            get => TemplateCtrl.GetSelected<AnalyticsTemplate>();
            set => TemplateCtrl.SetSelected( value );
        }
    }
}
