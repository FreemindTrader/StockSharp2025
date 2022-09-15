// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.EnumBooleanConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// <see cref="T:System.Enum" /> to <see cref="T:System.Boolean" /> converter.
  ///     </summary>
  public class EnumBooleanConverter : IValueConverter
  {
    
    private bool \u0023\u003DzssrBIztuUuqTKHZox\u0024lF3Yg\u003D;

    /// <summary>Default value when unchecked.</summary>
    public bool DefaultValueWhenUnchecked
    {
      get
      {
        return this.\u0023\u003DzssrBIztuUuqTKHZox\u0024lF3Yg\u003D;
      }
      set
      {
        this.\u0023\u003DzssrBIztuUuqTKHZox\u0024lF3Yg\u003D = value;
      }
    }

    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      string str = _param3 as string;
      if (str == null)
        return (object) DependencyProperty.UnsetValue;
      if (!Enum.IsDefined(_param1.GetType(), _param1))
        return (object) DependencyProperty.UnsetValue;
      return (object) str.To(_param1.GetType()).Equals(_param1);
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      string str = _param3 as string;
      if (str == null)
        return (object) DependencyProperty.UnsetValue;
      if (this.DefaultValueWhenUnchecked && !(bool) _param1)
        return _param2.GetValues().ElementAt<object>(0);
      return str.To(_param2);
    }
  }
}
