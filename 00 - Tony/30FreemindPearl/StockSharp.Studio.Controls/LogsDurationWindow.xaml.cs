
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using SmartFormat;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class LogsDurationWindow : ThemedWindow, IComponentConnector
    {
        
        public TimeSpan Duration
        {
            get
            {
                return ( TimeSpan )this.ComboBoxEdit.EditValue;
            }
        }

        public LogsDurationWindow()
        {
            this.InitializeComponent();
            this.ComboBoxEdit.DisplayMember = "Item1";
            this.ComboBoxEdit.ValueMember = "Item2";
            CultureInfo ci = CultureInfo.GetCultureInfo( LocalizedStrings.ActiveLanguage );
            this.ComboBoxEdit.ItemsSource = ( object )new Tuple<string, TimeSpan>[5]
            {
        Tuple.Create<string, TimeSpan>(LocalizedStrings.AllPeriod, TimeSpan.MaxValue),
        Tuple.Create<string, TimeSpan>(daysstr(1), TimeSpan.FromDays(1.0)),
        Tuple.Create<string, TimeSpan>(daysstr(3), TimeSpan.FromDays(3.0)),
        Tuple.Create<string, TimeSpan>(daysstr(7), TimeSpan.FromDays(7.0)),
        Tuple.Create<string, TimeSpan>(daysstr(30), TimeSpan.FromDays(30.0))
            };
            this.ComboBoxEdit.SelectedIndex = 0;

            string daysstr( int days )
            {
                return Smart.Format( ( IFormatProvider )ci, LocalizedStrings.DaysParamSmartPlural, ( object )days );
            }
        }        
    }
}
