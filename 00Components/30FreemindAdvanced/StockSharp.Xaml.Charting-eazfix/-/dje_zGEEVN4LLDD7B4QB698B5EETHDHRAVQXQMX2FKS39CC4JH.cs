// Decompiled with JetBrains decompiler
// Type: -.dje_zGEEVN4LLDD7B4QB698B5EETHDHRAVQXQMX2FKS39CC4JHQ4FEHWLELR2Y64AQZB2ZMVREEG3YUEHPNZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class dje_zGEEVN4LLDD7B4QB698B5EETHDHRAVQXQMX2FKS39CC4JHQ4FEHWLELR2Y64AQZB2ZMVREEG3YUEHPNZ_ejd : 
  IMultiValueConverter
{
  object IMultiValueConverter.\u0023\u003DzsTgIow6roRv0_9pt7wbO_aSDLkih(
    object[] _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    return (object) (Visibility) (!(_param1[1] is SciChartSurface) || !(_param1[0] as bool?).GetValueOrDefault() ? 2 : 0);
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
