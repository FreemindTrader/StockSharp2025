// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.ComboBoxEditValueConverter`1
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:DevExpress.Xpf.Editors.ComboBoxEdit" /> value converter.
  ///     </summary>
  /// <typeparam name="T">Convertible type.</typeparam>
  public sealed class ComboBoxEditValueConverter<T> : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) ((IEnumerable<T>) _param1).Select<T, object>(ComboBoxEditValueConverter<T>.SomeShit.\u0023\u003DzdzagqQ4DBMmb9r9ufw\u003D\u003D ?? (ComboBoxEditValueConverter<T>.SomeShit.\u0023\u003DzdzagqQ4DBMmb9r9ufw\u003D\u003D = new Func<T, object>(ComboBoxEditValueConverter<T>.SomeShit.ShitMethod02.\u0023\u003DzzWuoUYoJ1eESZYiuaB\u0024\u00248tDApP4uhv8lFg\u003D\u003D))).ToList<object>();
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (_param1 == null)
        return (object) Array.Empty<T>();
      return (object) ((IEnumerable<object>) _param1).Select<object, T>(ComboBoxEditValueConverter<T>.SomeShit.ShitMethod01 ?? (ComboBoxEditValueConverter<T>.SomeShit.ShitMethod01 = new Func<object, T>(ComboBoxEditValueConverter<T>.SomeShit.ShitMethod02.\u0023\u003Dzay5BWEQe5cSNEnRAt8Pe4XMP8_6WXVTFez4Vmas\u003D))).ToArray<T>();
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly ComboBoxEditValueConverter<\u0023\u003DznSahTwA\u003D>.SomeShit ShitMethod02 = new ComboBoxEditValueConverter<\u0023\u003DznSahTwA\u003D>.SomeShit();
      public static Func<\u0023\u003DznSahTwA\u003D, object> \u0023\u003DzdzagqQ4DBMmb9r9ufw\u003D\u003D;
      public static Func<object, \u0023\u003DznSahTwA\u003D> ShitMethod01;

      internal object \u0023\u003DzzWuoUYoJ1eESZYiuaB\u0024\u00248tDApP4uhv8lFg\u003D\u003D(
        \u0023\u003DznSahTwA\u003D _param1)
      {
        return (object) _param1;
      }

      internal \u0023\u003DznSahTwA\u003D \u0023\u003Dzay5BWEQe5cSNEnRAt8Pe4XMP8_6WXVTFez4Vmas\u003D(
        object _param1)
      {
        return (\u0023\u003DznSahTwA\u003D) _param1;
      }
    }
  }
}
