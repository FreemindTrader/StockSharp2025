// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.BoolToBrushConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Boolean" /> to <see cref="T:System.Windows.Media.Brush" /> converter.
  ///     </summary>
  public class BoolToBrushConverter : IValueConverter
  {
    
    private Brush \u0023\u003DzlBx1WiTI\u0024RDSV6GSXA\u003D\u003D = (Brush) new SolidColorBrush(Colors.Transparent);
    
    private Brush \u0023\u003Dzn4HX63rMVoR5MOwhPw\u003D\u003D = (Brush) new SolidColorBrush(Colors.Transparent);

    /// <summary>
    /// <see langword="true" /> value.
    ///     </summary>
    public Brush TrueValue
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
    public Brush FalseValue
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
      if ((bool) _param1 != flag)
        return (object) this.FalseValue;
      return (object) this.TrueValue;
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
