// Decompiled with JetBrains decompiler
// Type: -.BooleanToVisibilityConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class BooleanToVisibilityConverter : IValueConverter
{
  
  private Visibility \u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D;

  public BooleanToVisibilityConverter()
  {
    this.\u0023\u003DzgUA4lBZwhI1n(Visibility.Collapsed);
  }

  
  public Visibility FalseVisibilityValue
  {
    get => this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D;
    set => this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D = value;
  }

  public Visibility \u0023\u003DzYTPqj7MFUvQf() => this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D;

  public void \u0023\u003DzgUA4lBZwhI1n(Visibility _param1)
  {
    this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D = _param1;
  }

  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    int num = string.Equals(_param3 as string, "INVERSE", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
    Visibility visibility1 = num != 0 ? this.\u0023\u003DzYTPqj7MFUvQf() : Visibility.Visible;
    Visibility visibility2 = num != 0 ? Visibility.Visible : this.\u0023\u003DzYTPqj7MFUvQf();
    return (object) (Visibility) ((bool) _param1 ? (int) visibility1 : (int) visibility2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) true;
  }
}
