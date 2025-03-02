// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogsDurationWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using SmartFormat;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StockSharp.Studio.Controls
{
    public partial class LogsDurationWindow : ThemedWindow
    {
        public TimeSpan Duration
        {
            get
            {
                return ( TimeSpan ) this.ComboBoxEdit.EditValue;
            }
        }

        public LogsDurationWindow()
        {
            this.InitializeComponent();
            CultureInfo ci = LocalizedStrings.CurrentCulture;
            Tuple<string, TimeSpan>[] tupleArray = new Tuple<string, TimeSpan>[5]
                                                          {
                                                            Tuple.Create<string, TimeSpan>(LocalizedStrings.AllPeriod, TimeSpan.MaxValue),
                                                            Tuple.Create<string, TimeSpan>(daysstr(1), TimeSpan.FromDays(1.0)),
                                                            Tuple.Create<string, TimeSpan>(daysstr(3), TimeSpan.FromDays(3.0)),
                                                            Tuple.Create<string, TimeSpan>(daysstr(7), TimeSpan.FromDays(7.0)),
                                                            Tuple.Create<string, TimeSpan>(daysstr(30), TimeSpan.FromDays(30.0))
                                                          };

            Tuple<string, TimeSpan> tuple = ((IEnumerable<Tuple<string, TimeSpan>>) tupleArray).First<Tuple<string, TimeSpan>>();
            this.ComboBoxEdit.DisplayMember = "Item1";
            this.ComboBoxEdit.ValueMember = "Item2";
            this.ComboBoxEdit.ItemsSource = ( object ) tupleArray;
            this.ComboBoxEdit.SelectedItem = ( object ) tuple;

            string daysstr( int days )
            {
                return Smart.Format( ( IFormatProvider ) ci, LocalizedStrings.DaysParamSmartPlural, new object [1]
                {
           days
                } );
            }
        }
    }
}
