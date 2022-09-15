// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.DrawingColorToBrushConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Drawing.Color" /> to <see cref="T:System.Windows.Media.SolidColorBrush" /> converter.
  ///     </summary>
  public class DrawingColorToBrushConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) new SolidColorBrush(((System.Drawing.Color) _param1).ToWpf());
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
