// Decompiled with JetBrains decompiler
// Type: #=zNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows;

#nullable disable
internal static class \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D
{
  public static double \u0023\u003Dz62MsOEK3dnlV(Size _param0)
  {
    return \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param0.Width, _param0.Height);
  }

  public static double \u0023\u003Dz62MsOEK3dnlV(double _param0, double _param1)
  {
    return Math.Min(_param0, _param1) / 2.0;
  }

  public static double \u0023\u003DzexGnHaPDCcBP(ref Point _param0, ref Point _param1)
  {
    return Math.Abs(_param0.X - _param1.X);
  }

  public static Size \u0023\u003Dzzv9duDNs\u0024tjr9662Jw\u003D\u003D(Size _param0)
  {
    return new Size(360.0, \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param0));
  }
}
