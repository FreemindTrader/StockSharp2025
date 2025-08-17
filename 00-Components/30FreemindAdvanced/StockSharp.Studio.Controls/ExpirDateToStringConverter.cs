// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ExpirDateToStringConverter
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Studio.Controls;

internal sealed class ExpirDateToStringConverter : IValueConverter
{
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo _)
    {
        if (value is DateTime dateTime)
            return (object)dateTime.ToString("D");
        return value == null ? (object)null : (object)value.ToString();
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
