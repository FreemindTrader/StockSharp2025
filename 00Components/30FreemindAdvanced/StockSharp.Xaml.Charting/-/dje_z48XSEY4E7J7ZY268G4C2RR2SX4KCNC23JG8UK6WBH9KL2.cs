// Decompiled with JetBrains decompiler
// Type: -.dje_z48XSEY4E7J7ZY268G4C2RR2SX4KCNC23JG8UK6WBH9KL2UA_ejd
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

internal sealed class dje_z48XSEY4E7J7ZY268G4C2RR2SX4KCNC23JG8UK6WBH9KL2UA_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    int num = string.Equals(_param3 as string, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430952), StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
    Visibility visibility1 = num != 0 ? Visibility.Visible : Visibility.Collapsed;
    Visibility visibility2 = num != 0 ? Visibility.Collapsed : Visibility.Visible;
    return (object) (Visibility) (_param1 == null ? (int) visibility1 : (int) visibility2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
