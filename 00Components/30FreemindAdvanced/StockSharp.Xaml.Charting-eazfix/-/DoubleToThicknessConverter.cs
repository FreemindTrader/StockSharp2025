// Decompiled with JetBrains decompiler
// Type: -.DoubleToThicknessConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class DoubleToThicknessConverter : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return _param1 == null ? (object) null : (object) new Thickness(((IComparable) _param1).ToDouble());
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
