// Decompiled with JetBrains decompiler
// Type: -.dje_zWZNJY4V9YE28DF2VMU22KD7WGATKPZNGSSJNGKYK8LKVUBGCDCCUWKDQJUSA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zWZNJY4V9YE28DF2VMU22KD7WGATKPZNGSSJNGKYK8LKVUBGCDCCUWKDQJUSA_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd aqkdhqsfywuD3K2Ejd = (dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd) _param1;
    string str1 = \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430263);
    if (!(_param3 is string str2))
      str2 = str1;
    bool flag = str2.ToUpperInvariant().Contains(str1);
    int num;
    switch (aqkdhqsfywuD3K2Ejd)
    {
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.TopLeft:
        num = flag ? 1 : 0;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.TopRight:
        num = flag ? 1 : 4;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.BottomLeft:
        num = flag ? 5 : 0;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.BottomRight:
        num = flag ? 5 : 4;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.Top:
        num = flag ? 1 : 2;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.Bottom:
        num = flag ? 5 : 2;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.Left:
        num = flag ? 3 : 0;
        break;
      case dje_z5SKW2EFDH6HACNTXE6B7JUCG8CMX3VV2AQKDHQSFYWUD3K2_ejd.Right:
        num = flag ? 3 : 4;
        break;
      default:
        num = flag ? 3 : 2;
        break;
    }
    return (object) num;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
