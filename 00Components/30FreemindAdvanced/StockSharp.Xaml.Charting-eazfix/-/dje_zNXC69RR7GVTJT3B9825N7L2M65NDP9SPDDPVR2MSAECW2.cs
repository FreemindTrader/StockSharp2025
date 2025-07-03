// Decompiled with JetBrains decompiler
// Type: -.dje_zNXC69RR7GVTJT3B9825N7L2M65NDP9SPDDPVR2MSAECW2CGYHHT2Z_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class dje_zNXC69RR7GVTJT3B9825N7L2M65NDP9SPDDPVR2MSAECW2CGYHHT2Z_ejd : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) _param1;
    dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd awnnsejcnkX3MwEjd = dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd.Left;
    switch (demydmpA2K68QEjd)
    {
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top:
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom:
        awnnsejcnkX3MwEjd = dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd.Center;
        break;
    }
    return (object) awnnsejcnkX3MwEjd;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
