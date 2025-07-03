// Decompiled with JetBrains decompiler
// Type: -.dje_zYEYVHRUCVES8WP44Q8DAVLAAKX7YAG5YVNW3E68RZUZY4G2Z625J4_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal sealed class dje_zYEYVHRUCVES8WP44Q8DAVLAAKX7YAG5YVNW3E68RZUZY4G2Z625J4_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    pattern_1 = new Color();
    switch (_param1)
    {
      case SolidColorBrush solidColorBrush:
        pattern_1 = solidColorBrush.Color;
        break;
    }
    return (object) new SolidColorBrush((double) pattern_1.R * 0.299 + (double) pattern_1.G * 0.587 + (double) pattern_1.B * 0.114 > 128.0 ? Colors.Black : Colors.White);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
