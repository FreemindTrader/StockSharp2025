// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.TimeConverter
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
  /// <see cref="T:System.DateTimeOffset" /> or <see cref="T:System.DateTime" /> converter to a specified time zone.
  ///     </summary>
  public class TimeConverter : IValueConverter
  {
    
    private static TimeZoneInfo \u0023\u003Dz4TevudWtBTP3mcE\u00244UTgoWQ\u003D;
    
    private TimeZoneInfo \u0023\u003Dzxc7Ss38\u003D;

    /// <summary>Global time zone for whole app.</summary>
    public static TimeZoneInfo GlobalTimeZone
    {
      get
      {
        return TimeConverter.\u0023\u003Dz4TevudWtBTP3mcE\u00244UTgoWQ\u003D;
      }
      set
      {
        TimeConverter.\u0023\u003Dz4TevudWtBTP3mcE\u00244UTgoWQ\u003D = value;
      }
    }

    /// <summary>Time zone.</summary>
    public TimeZoneInfo TimeZone
    {
      get
      {
        return this.\u0023\u003Dzxc7Ss38\u003D ?? TimeConverter.GlobalTimeZone;
      }
    }

    /// <summary>
    /// <see cref="P:Ecng.Xaml.Converters.TimeConverter.TimeZone" /> is <see cref="P:System.TimeZoneInfo.Local" />.
    ///     </summary>
    public bool ConvertToLocal
    {
      get
      {
        return TimeZoneInfo.Local.Equals(this.\u0023\u003Dzxc7Ss38\u003D);
      }
      set
      {
        this.\u0023\u003Dzxc7Ss38\u003D = value ? TimeZoneInfo.Local : (TimeZoneInfo) null;
      }
    }

    /// <summary>
    /// Set <see cref="P:Ecng.Xaml.Converters.TimeConverter.TimeZone" /> by string id.
    /// </summary>
    public string TimeZoneId
    {
      get
      {
        return this.\u0023\u003Dzxc7Ss38\u003D.To<string>();
      }
      set
      {
        this.\u0023\u003Dzxc7Ss38\u003D = value.To<TimeZoneInfo>();
      }
    }

    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      TimeZoneInfo timeZone = this.TimeZone;
      if (_param1 is DateTimeOffset)
      {
        DateTimeOffset dateTimeOffset = (DateTimeOffset) _param1;
        return (object) (timeZone == null ? dateTimeOffset : TimeZoneInfo.ConvertTime(dateTimeOffset, timeZone));
      }
      if (!(_param1 is DateTime))
        return Binding.DoNothing;
      DateTime dateTime = (DateTime) _param1;
      return (object) (timeZone == null ? dateTime : TimeZoneInfo.ConvertTime(dateTime, timeZone));
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
