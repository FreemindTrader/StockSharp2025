// Decompiled with JetBrains decompiler
// Type: -.dje_zJLZUPHSEPLZTF5VGVGDZ9JUXE8WY5BT3ZA2A8ZL7CKFWK7Z_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zJLZUPHSEPLZTF5VGVGDZ9JUXE8WY5BT3ZA2A8ZL7CKFWK7Z_ejd : IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D k3zbmiw1OaRAdtq7psDwa = _param1 as \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D;
    string str1 = k3zbmiw1OaRAdtq7psDwa.FormattedYValue;
    string str2;
    string str3;
    if (k3zbmiw1OaRAdtq7psDwa.YValue.CompareTo((object) k3zbmiw1OaRAdtq7psDwa.Y1Value) >= 0)
    {
      str2 = k3zbmiw1OaRAdtq7psDwa.FormattedYValue;
      str3 = k3zbmiw1OaRAdtq7psDwa.FormattedY1Value;
    }
    else
    {
      str2 = k3zbmiw1OaRAdtq7psDwa.FormattedY1Value;
      str3 = k3zbmiw1OaRAdtq7psDwa.FormattedYValue;
    }
    if (_param3 != null)
    {
      string upperInvariant = _param3.ToString().ToUpperInvariant();
      if (!(upperInvariant == ""))
      {
        if (!(upperInvariant == ""))
        {
          if (upperInvariant == "")
            str1 = str3;
        }
        else
          str1 = str2;
      }
      else
        str1 = k3zbmiw1OaRAdtq7psDwa.FormattedY1Value;
    }
    return (object) str1;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
