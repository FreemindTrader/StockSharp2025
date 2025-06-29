// Decompiled with JetBrains decompiler
// Type: #=z6XUxHRwFxnCP5tzqYbRSvpYDuG0YJcoE3XfW_V4=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Algo.Indicators;
using StockSharp.Xaml.Charting;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003Dz6XUxHRwFxnCP5tzqYbRSvpYDuG0YJcoE3XfW_V4\u003D : 
  IMultiValueConverter
{
  public object Convert(object[] _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) Tuple.Create<ChartArea, IndicatorType>((ChartArea) _param1[0], (IndicatorType) _param1[1]);
  }

  public object[] ConvertBack(object _param1, Type[] _param2, object _param3, CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
