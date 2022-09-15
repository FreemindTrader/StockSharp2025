// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.NullToVisibilityConvertor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see langword="null" /> (or <see cref="F:System.String.Empty" />) to <see cref="T:System.Windows.Visibility" /> converter.
  ///     </summary>
  public class NullToVisibilityConvertor : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (_param1 == null)
        return (object) Visibility.Collapsed;
      return (object) (Visibility) (_param1.To<string>().IsEmptyOrWhiteSpace() ? 2 : 0);
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
