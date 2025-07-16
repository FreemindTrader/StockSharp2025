// Decompiled with JetBrains decompiler
// Type: -.LegendPlacementToGridPositionConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class LegendPlacementToGridPositionConverter : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    LegendPlacement aqkdhqsfywuD3K2Ejd = (LegendPlacement) _param1;
    string str1 = "ROW";
    if (!(_param3 is string str2))
      str2 = str1;
    bool flag = str2.ToUpperInvariant().Contains(str1);
    int num;
    switch (aqkdhqsfywuD3K2Ejd)
    {
      case LegendPlacement.TopLeft:
        num = flag ? 1 : 0;
        break;
      case LegendPlacement.TopRight:
        num = flag ? 1 : 4;
        break;
      case LegendPlacement.BottomLeft:
        num = flag ? 5 : 0;
        break;
      case LegendPlacement.BottomRight:
        num = flag ? 5 : 4;
        break;
      case LegendPlacement.Top:
        num = flag ? 1 : 2;
        break;
      case LegendPlacement.Bottom:
        num = flag ? 5 : 2;
        break;
      case LegendPlacement.Left:
        num = flag ? 3 : 0;
        break;
      case LegendPlacement.Right:
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
