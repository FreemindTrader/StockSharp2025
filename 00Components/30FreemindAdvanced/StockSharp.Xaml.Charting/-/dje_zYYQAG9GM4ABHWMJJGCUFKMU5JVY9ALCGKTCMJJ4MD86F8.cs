// Decompiled with JetBrains decompiler
// Type: -.dje_zYYQAG9GM4ABHWMJJGCUFKMU5JVY9ALCGKTCMJJ4MD86F8U2DG7MFLA88262A_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zYYQAG9GM4ABHWMJJGCUFKMU5JVY9ALCGKTCMJJ4MD86F8U2DG7MFLA88262A_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    double num = (double) _param1;
    string s = (string) _param3 ?? \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430936);
    double result;
    if (double.TryParse(s, NumberStyles.Any, (IFormatProvider) _param4, out result))
      return (object) (num * result);
    return double.TryParse(s, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? (object) (num * result) : (object) null;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
