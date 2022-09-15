// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.BoolToVisibilityConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Boolean" /> to <see cref="T:System.Windows.Visibility" /> converter.
  ///     </summary>
  public class BoolToVisibilityConverter : IValueConverter
  {
    
    private Visibility \u0023\u003Dzn4HX63rMVoR5MOwhPw\u003D\u003D = Visibility.Collapsed;
    
    private Visibility \u0023\u003DzlBx1WiTI\u0024RDSV6GSXA\u003D\u003D;

    /// <summary>
    /// </summary>
    public Visibility TrueValue
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
    /// </summary>
    public Visibility FalseValue
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
      return (object) (Visibility) ((bool) _param1 == flag ? (int) this.TrueValue : (int) this.FalseValue);
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) ((Visibility) _param1 == this.TrueValue);
    }
  }
}
