// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CandleSeriesCheckBox
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System.Windows;
using System.Windows.Controls;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Studio.Controls;

internal sealed class CandleSeriesCheckBox : CheckBox
{
    public static readonly DependencyProperty SeriesProperty = DependencyProperty.Register(nameof(Series), typeof(DataType), typeof(CandleSeriesCheckBox));

    public DataType Series
    {
        get => (DataType)this.GetValue(CandleSeriesCheckBox.SeriesProperty);
        set => this.SetValue(CandleSeriesCheckBox.SeriesProperty, (object)value);
    }
}
