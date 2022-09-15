// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.BoolToDoubleConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Boolean" /> to <see cref="T:System.Double" /> converter.
  ///     </summary>
  public class BoolToDoubleConverter : IValueConverter
  {
    
    private double \u0023\u003DzlBx1WiTI\u0024RDSV6GSXA\u003D\u003D = 0.5;
    
    private double \u0023\u003Dzn4HX63rMVoR5MOwhPw\u003D\u003D = 1.0;

    /// <summary>
    /// <see langword="true" /> value.
    ///     </summary>
    public double TrueValue
    {
      get
      {
        return this.\u0023\u003DzlBx1WiTI\u0024RDSV6GSXA\u003D\u003D;
      }
      set
      {
        this.\u0023\u003DzlBx1WiTI\u0024RDSV6GSXA\u003D\u003D = value;
      }
    }

    /// <summary>
    /// <see langword="false" /> value.
    ///     </summary>
    public double FalseValue
    {
      get
      {
        return this.\u0023\u003Dzn4HX63rMVoR5MOwhPw\u003D\u003D;
      }
      set
      {
        this.\u0023\u003Dzn4HX63rMVoR5MOwhPw\u003D\u003D = value;
      }
    }

    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      bool flag = _param3 == null || _param3.To<bool>();
      return (object) ((bool) _param1 == flag ? this.TrueValue : this.FalseValue);
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
