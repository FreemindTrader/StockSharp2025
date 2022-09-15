// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.EnumDisplayNameConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.ComponentModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Enum" /> to display name converter.
  ///     </summary>
  public class EnumDisplayNameConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (_param1 == DependencyProperty.UnsetValue)
        return Binding.DoNothing;
      if (_param1 == null)
        return (object) string.Empty;
      if (!(_param1 is Enum))
        return Binding.DoNothing;
      return (object) _param1.GetDisplayName();
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
