// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartTimeFrameNameConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Localization;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class ChartTimeFrameNameConverter : IValueConverter
{
  object IValueConverter.Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
  {
    if (!(value is TimeSpan timeSpan))
      return Binding.DoNothing;
    if (timeSpan == TimeSpan.Zero)
      return (object) LocalizedStrings.Period;
    if (timeSpan.TotalDays >= 1.0)
      return (object) (((int) timeSpan.TotalDays).ToString() + LocalizedStrings.DaysLetter);
    if (timeSpan.TotalHours >= 1.0)
      return (object) (((int) timeSpan.TotalHours).ToString() + LocalizedStrings.HoursLetter);
    return timeSpan.TotalMinutes >= 1.0 ? (object) (((int) timeSpan.TotalMinutes).ToString() + LocalizedStrings.MinutesLetter) : (object) timeSpan.ToString();
  }

  object IValueConverter.ConvertBack(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
