// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.FileSizeConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
  /// <summary>File size to human readable text converter.</summary>
  public class FileSizeConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      long? nullable1 = _param1 as long?;
      if (!nullable1.HasValue)
      {
        int? nullable2 = _param1 as int?;
        nullable1 = nullable2.HasValue ? new long?((long) nullable2.GetValueOrDefault()) : new long?();
      }
      if (!nullable1.HasValue)
        return Binding.DoNothing;
      return (object) nullable1.Value.ToHumanReadableFileSize();
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
