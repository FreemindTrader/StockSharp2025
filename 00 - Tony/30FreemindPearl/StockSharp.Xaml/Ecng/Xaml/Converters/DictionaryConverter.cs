// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.DictionaryConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using Ecng.Reflection;
using StockSharp.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// Get value by key from the specified <see cref="T:System.Collections.IDictionary" /> converter.
  /// </summary>
  public class DictionaryConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      IDictionary dictionary = (IDictionary) _param1;
      Type type = dictionary != null ? ReflectionHelper.GetGenericType(dictionary.GetType(), typeof (IDictionary<,>)) : (Type) null;
      if (type == (Type) null)
        return (object) DependencyProperty.UnsetValue;
      object key;
      try
      {
        key = _param3.To(type.GetGenericArguments()[0]);
      }
      catch (Exception ex)
      {
        ex.LogError((string) null);
        return (object) DependencyProperty.UnsetValue;
      }
      if (dictionary.Contains(key))
        return dictionary[key];
      return (object) DependencyProperty.UnsetValue;
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
