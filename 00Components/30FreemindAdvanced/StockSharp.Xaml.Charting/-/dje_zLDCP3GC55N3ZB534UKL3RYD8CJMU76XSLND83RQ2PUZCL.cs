// Decompiled with JetBrains decompiler
// Type: -.dje_zLDCP3GC55N3ZB534UKL3RYD8CJMU76XSLND83RQ2PUZCL3Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zLDCP3GC55N3ZB534UKL3RYD8CJMU76XSLND83RQ2PUZCL3Q_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    Thickness thickness = (Thickness) _param1;
    string str = (string) _param3;
    if (str == "")
      return (object) thickness.Top;
    if (str == "")
      return (object) thickness.Bottom;
    if (str == "")
      return (object) thickness.Left;
    return str == "" ? (object) thickness.Right : (object) ((thickness.Left + thickness.Right + thickness.Top + thickness.Bottom) / 4.0);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
