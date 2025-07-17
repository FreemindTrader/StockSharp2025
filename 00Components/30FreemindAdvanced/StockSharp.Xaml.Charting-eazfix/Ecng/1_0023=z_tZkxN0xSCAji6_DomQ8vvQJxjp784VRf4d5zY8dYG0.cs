// Decompiled with JetBrains decompiler
// Type: #=z_tZkxN0xSCAji6_DomQ8vvQJxjp784VRf4d5zY8dYG0Lnu1O0AIymGPrDkDOp7jd4BdoJfg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vvQJxjp784VRf4d5zY8dYG0Lnu1O0AIymGPrDkDOp7jd4BdoJfg\u003D : 
  IImageFilterFunction
{
  public double radius() => 1.0;

  public double calc_weight(double _param1)
  {
    return 0.54 + 0.46 * Math.Cos(Math.PI * _param1);
  }
}
