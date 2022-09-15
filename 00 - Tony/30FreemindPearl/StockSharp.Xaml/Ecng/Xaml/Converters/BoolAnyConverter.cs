// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.BoolAnyConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// Set of <see cref="T:System.Boolean" /> (any) check on <see cref="P:Ecng.Xaml.Converters.BoolAnyConverter.Value" /> equality converter.
  /// </summary>
  public class BoolAnyConverter : IMultiValueConverter
  {
    
    private bool _value = true;

    /// <summary>Value.</summary>
    public bool Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    object IMultiValueConverter.Convert(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) ((IEnumerable<object>) _param1).Any<object>(new Func<object, bool>(this.IsBoolEqual));
    }

    object[] IMultiValueConverter.ConvertBack(
      object _param1,
      Type[] _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }

    private bool IsBoolEqual(object _param1)
    {
      if (_param1 is bool)
        return (bool) _param1 == this.Value;
      return false;
    }
  }
}
