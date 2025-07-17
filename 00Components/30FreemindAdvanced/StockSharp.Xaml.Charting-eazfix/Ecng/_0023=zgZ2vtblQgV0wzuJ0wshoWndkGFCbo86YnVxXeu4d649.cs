// Decompiled with JetBrains decompiler
// Type: #=zgZ2vtblQgV0wzuJ0wshoWndkGFCbo86YnVxXeu4d649rQayMtZriFpDMOQeEg6m9il48VSo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct \u0023\u003DzgZ2vtblQgV0wzuJ0wshoWndkGFCbo86YnVxXeu4d649rQayMtZriFpDMOQeEg6m9il48VSo\u003D : 
  IImageFilterFunction
{
  public double radius() => 1.5;

  public double calc_weight(double _param1)
  {
    if (_param1 < 0.5)
      return 0.75 - _param1 * _param1;
    if (_param1 >= 1.5)
      return 0.0;
    double num = _param1 - 1.5;
    return 0.5 * num * num;
  }
}
