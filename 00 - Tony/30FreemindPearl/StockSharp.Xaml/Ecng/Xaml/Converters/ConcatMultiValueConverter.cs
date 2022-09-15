// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.ConcatMultiValueConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>Concat values into single string converter.</summary>
  public class ConcatMultiValueConverter : IMultiValueConverter
  {
    object IMultiValueConverter.Convert(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) ((IEnumerable<object>) _param1).Select<object, string>(ConcatMultiValueConverter.SomeShit.\u0023\u003DzdzagqQ4DBMmb9r9ufw\u003D\u003D ?? (ConcatMultiValueConverter.SomeShit.\u0023\u003DzdzagqQ4DBMmb9r9ufw\u003D\u003D = new Func<object, string>(ConcatMultiValueConverter.SomeShit.ShitMethod02.\u0023\u003DzVX0prdjnQvfOESgN52B2y9EZWlyV5MKVwnGN1gU\u003D))).Join(nameof(2127277864));
    }

    object[] IMultiValueConverter.ConvertBack(
      object _param1,
      Type[] _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly ConcatMultiValueConverter.SomeShit ShitMethod02 = new ConcatMultiValueConverter.SomeShit();
      public static Func<object, string> \u0023\u003DzdzagqQ4DBMmb9r9ufw\u003D\u003D;

      internal string \u0023\u003DzVX0prdjnQvfOESgN52B2y9EZWlyV5MKVwnGN1gU\u003D(object _param1)
      {
        return _param1.To<string>();
      }
    }
  }
}
