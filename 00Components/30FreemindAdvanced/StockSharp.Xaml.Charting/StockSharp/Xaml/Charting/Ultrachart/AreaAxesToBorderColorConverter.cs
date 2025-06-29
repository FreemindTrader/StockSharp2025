// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Ultrachart.AreaAxesToBorderColorConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.Ultrachart;

/// <summary>
/// Show red border around legend element if one or both axes are undefined.
/// </summary>
public class AreaAxesToBorderColorConverter : IMultiValueConverter
{
  object IMultiValueConverter.Convert(
    object[] values,
    Type targetType,
    object parameter,
    CultureInfo culture)
  {
    return values.Length == 2 && values[0] != null && values[1] != null ? (object) null : (object) new SolidColorBrush(Colors.Red.ToTransparent((byte) 50));
  }

  object[] IMultiValueConverter.ConvertBack(
    object value,
    Type[] targetTypes,
    object parameter,
    CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
