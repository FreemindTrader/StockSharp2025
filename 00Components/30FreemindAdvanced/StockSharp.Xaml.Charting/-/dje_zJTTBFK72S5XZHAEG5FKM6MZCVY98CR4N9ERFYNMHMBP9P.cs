// Decompiled with JetBrains decompiler
// Type: -.dje_zJTTBFK72S5XZHAEG5FKM6MZCVY98CR4N9ERFYNMHMBP9PPQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zJTTBFK72S5XZHAEG5FKM6MZCVY98CR4N9ERFYNMHMBP9PPQ_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    double num1 = (double) _param1;
    int num2 = string.Equals(_param3 as string, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539441330), StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
    double num3 = num1 / 2.0;
    return (object) (num2 != 0 ? new Thickness(0.0, -num3, 0.0, -num3) : new Thickness(-num3, 0.0, -num3, 0.0));
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
