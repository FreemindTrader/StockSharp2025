// Decompiled with JetBrains decompiler
// Type: -.dje_zTYQED6UNCPRW4ZAGFE6CK5DWJPCS3SGKTB2MQGAM2NNWYBMEVZ992Z7LLS2PR94A8Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Charting;
using StockSharp.Xaml.Charting.Ultrachart;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class dje_zTYQED6UNCPRW4ZAGFE6CK5DWJPCS3SGKTB2MQGAM2NNWYBMEVZ992Z7LLS2PR94A8Q_ejd : 
  IMultiValueConverter
{
  object IMultiValueConverter.\u0023\u003DzsTgIow6roRv0_9pt7wbO_aSDLkih(
    object[] _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    if (!(_param1[0] is IChartElement element))
      return (object) null;
    switch (element)
    {
      case IChartIndicatorElement _:
      case IChartBandElement _:
      case IChartLineElement _:
      case IChartVolatilitySmileElement _:
        return (object) new ChartIndicatorElementSettingsObject(element);
      default:
        return (object) element;
    }
  }

  object[] IMultiValueConverter.\u0023\u003DzkoCt60WyxQ8Vp2m8J8PjY8sNdWh7(
    object _param1,
    Type[] _param2,
    object _param3,
    CultureInfo _param4)
  {
    throw new NotSupportedException();
  }
}
