// Decompiled with JetBrains decompiler
// Type: -.dje_zLDCP3GC55N3ZB534UKL3RYD8CJMU76XSLND83RQ2PUZCL3Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zLDCP3GC55N3ZB534UKL3RYD8CJMU76XSLND83RQ2PUZCL3Q_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    Thickness thickness = (Thickness) _param1;
    switch ((string) _param3)
    {
      case "Top":
        return (object) thickness.Top;
      case "Bottom":
        return (object) thickness.Bottom;
      case "Left":
        return (object) thickness.Left;
      case "Right":
        return (object) thickness.Right;
      default:
        return (object) ((thickness.Left + thickness.Right + thickness.Top + thickness.Bottom) / 4.0);
    }
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
