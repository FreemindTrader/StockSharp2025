// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CandleSeriesCheckBox
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using StockSharp.Messages;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls
{
    internal sealed class CandleSeriesCheckBox : CheckBox
    {
        public static readonly DependencyProperty SeriesProperty = DependencyProperty.Register(nameof (Series), typeof (DataType), typeof (CandleSeriesCheckBox));

        public DataType Series
        {
            get
            {
                return ( DataType ) this.GetValue( CandleSeriesCheckBox.SeriesProperty );
            }
            set
            {
                this.SetValue( CandleSeriesCheckBox.SeriesProperty,  value );
            }
        }
    }
}
