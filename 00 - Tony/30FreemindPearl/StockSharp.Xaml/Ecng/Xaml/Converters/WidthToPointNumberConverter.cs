// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.WidthToPointNumberConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// </summary>
  public class WidthToPointNumberConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      int num;
      if (_param1 is double)
      {
        double d = (double) _param1;
        if (!double.IsNaN(d))
        {
          num = (int) (d / 500.0 + 1.0) * 100;
          goto label_4;
        }
      }
      num = 0;
label_4:
      return (object) num;
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }
}
