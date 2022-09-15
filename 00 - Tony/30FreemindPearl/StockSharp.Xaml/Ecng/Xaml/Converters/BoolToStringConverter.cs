// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.BoolToStringConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Boolean" /> to <see cref="T:System.String" /> converter.
  ///     </summary>
  public class BoolToStringConverter : IValueConverter
  {
    
    private string \u0023\u003DzlBx1WiTI\u0024RDSV6GSXA\u003D\u003D;
    
    private string \u0023\u003Dzn4HX63rMVoR5MOwhPw\u003D\u003D;

    /// <summary>
    /// <see langword="true" /> value.
    ///     </summary>
    public string TrueValue
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
    public string FalseValue
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
      bool? nullable = _param1 as bool?;
      if (!(nullable.GetValueOrDefault() & nullable.HasValue))
        return (object) this.FalseValue;
      return (object) this.TrueValue;
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) ((string) _param1 == this.TrueValue);
    }
  }
}
