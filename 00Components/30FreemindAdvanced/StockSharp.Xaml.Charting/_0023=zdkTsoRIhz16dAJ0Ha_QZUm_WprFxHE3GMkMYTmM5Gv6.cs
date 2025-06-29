// Decompiled with JetBrains decompiler
// Type: #=zdkTsoRIhz16dAJ0Ha_QZUm_WprFxHE3GMkMYTmM5Gv6xPE1lfu86yUSurnkA
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Charting;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUm_WprFxHE3GMkMYTmM5Gv6xPE1lfu86yUSurnkA : 
  IValueConverter
{
  object IValueConverter.\u0023\u003DzM9yoqEmGoL\u0024Vcrr_ku1EGJc\u003D(
    object _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    switch ((ChartCandleDrawStyles) _param1)
    {
      case ChartCandleDrawStyles.LineOpen:
        return (object) \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Open;
      case ChartCandleDrawStyles.LineHigh:
        return (object) \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.High;
      case ChartCandleDrawStyles.LineLow:
        return (object) \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Low;
      default:
        return (object) \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Close;
    }
  }

  object IValueConverter.\u0023\u003Dz7t96kV0doysI1t8U28R3TqlcxXQz(
    object _param1,
    Type _param2,
    object _param3,
    CultureInfo _param4)
  {
    switch ((\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym) _param1)
    {
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Open:
        return (object) ChartCandleDrawStyles.LineOpen;
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.High:
        return (object) ChartCandleDrawStyles.LineHigh;
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Low:
        return (object) ChartCandleDrawStyles.LineLow;
      default:
        return (object) ChartCandleDrawStyles.LineClose;
    }
  }
}
