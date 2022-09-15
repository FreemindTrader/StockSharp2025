// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.IndexerConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// Get value by index from the specified <see cref="T:System.Collections.IList" /> converter.
  /// </summary>
  public class IndexerConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      IList list = _param1 as IList;
      if (list == null)
        return (object) DependencyProperty.UnsetValue;
      if (!(_param3 is int))
        return (object) DependencyProperty.UnsetValue;
      int index = (int) _param3;
      if (index >= list.Count)
        return (object) DependencyProperty.UnsetValue;
      return list[index];
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
