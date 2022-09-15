// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.CountryIdToFlagImageSourceConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>Country id to country flag converter.</summary>
  public class CountryIdToFlagImageSourceConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      CountryCodes? nullable = \u0023\u003DzRic4pLDuovRISsMG9xeTkP\u00244c7DX.\u0023\u003Dz8VPx6yYpb0g\u0024(_param1);
      if (nullable.HasValue)
        return (object) nullable.Value.\u0023\u003DzhwhXotIRAeiI();
      return (object) null;
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
