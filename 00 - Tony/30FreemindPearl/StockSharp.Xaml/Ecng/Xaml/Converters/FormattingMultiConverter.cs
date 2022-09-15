// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.FormattingMultiConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>String formatting converter.</summary>
  public class FormattingMultiConverter : IMultiValueConverter
  {
    /// <summary>A composite format string.</summary>
    public string FormatString;

    object IMultiValueConverter.Convert(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      if (_param3 != null && !(_param3 is string) || ((IEnumerable<object>) _param1).Any<object>(FormattingMultiConverter.SomeShit.ShitMethod01 ?? (FormattingMultiConverter.SomeShit.ShitMethod01 = new Func<object, bool>(FormattingMultiConverter.SomeShit.ShitMethod02.\u0023\u003DzIaT12rHsPmn9aKIofytBGIpQIwmX\u0024QyczByrlGY\u003D))))
        return Binding.DoNothing;
      string str = _param3 as string;
      if (str.IsEmpty())
        str = this.FormatString;
      return (object) string.Format((IFormatProvider) _param4, str, _param1);
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
      public static readonly FormattingMultiConverter.SomeShit ShitMethod02 = new FormattingMultiConverter.SomeShit();
      public static Func<object, bool> ShitMethod01;

      internal bool \u0023\u003DzIaT12rHsPmn9aKIofytBGIpQIwmX\u0024QyczByrlGY\u003D(object _param1)
      {
        return _param1 == DependencyProperty.UnsetValue;
      }
    }
  }
}
