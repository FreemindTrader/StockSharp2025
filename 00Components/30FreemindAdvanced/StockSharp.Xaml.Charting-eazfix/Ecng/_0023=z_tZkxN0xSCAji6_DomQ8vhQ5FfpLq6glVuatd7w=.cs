// Decompiled with JetBrains decompiler
// Type: #=z_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows.Threading;

#nullable disable
public sealed class \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D : 
  \u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D
{
  private Dispatcher \u0023\u003DzoOQOCHaSepYx;
  private static bool \u0023\u003DzpRppQMPLh4Ch;

  public \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D(Dispatcher _param1)
  {
    if (\u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D.\u0023\u003DzpRppQMPLh4Ch)
      return;
    this.\u0023\u003DzoOQOCHaSepYx = _param1;
  }

  public void \u0023\u003DzbMTrUAQ\u003D(Action _param1, DispatcherPriority _param2)
  {
    if (this.\u0023\u003DzoOQOCHaSepYx == null)
      _param1();
    else
      this.\u0023\u003DzoOQOCHaSepYx.BeginInvoke((Delegate) _param1, _param2, Array.Empty<object>());
  }

  public void \u0023\u003Dz40vIrjqAtFMX(Action _param1, DispatcherPriority _param2)
  {
    if (this.\u0023\u003DzoOQOCHaSepYx == null || this.\u0023\u003DzoOQOCHaSepYx.CheckAccess())
      _param1();
    else
      this.\u0023\u003DzbMTrUAQ\u003D(_param1, _param2);
  }

  public static void \u0023\u003Dzu9ylO34prDMx()
  {
    \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D.\u0023\u003DzpRppQMPLh4Ch = true;
  }

  public static bool \u0023\u003DzMNVV9_LtRFVB()
  {
    return \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D.\u0023\u003DzpRppQMPLh4Ch;
  }
}
