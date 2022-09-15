// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.NullToBoolConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see langword="null" /> (or <see cref="F:System.String.Empty" />) to <see cref="T:System.Boolean" /> converter.
  ///     </summary>
  public class NullToBoolConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (_param1 == null)
        return (object) false;
      return (object) !_param1.To<string>().IsEmptyOrWhiteSpace();
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return Binding.DoNothing;
    }
  }
}
