// Decompiled with JetBrains decompiler
// Type: -.dje_zS7YY9QYBN58NRQDVW3CHXMQTLMN3BLC47SWJ4H2SA7WZC3Q2YX9NCCQJD54Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zS7YY9QYBN58NRQDVW3CHXMQTLMN3BLC47SWJ4H2SA7WZC3Q2YX9NCCQJD54Q_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) _param1;
    dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd r852JmG36S4EnausEjd = dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd.Center;
    switch (demydmpA2K68QEjd)
    {
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top:
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom:
        r852JmG36S4EnausEjd = dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd.Top;
        break;
    }
    return (object) r852JmG36S4EnausEjd;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
