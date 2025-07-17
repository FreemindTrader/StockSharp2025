// Decompiled with JetBrains decompiler
// Type: #=z_tZkxN0xSCAji6_DomQ8vvQJxjp784VRf4d5zY8dYG0C1dGQIqLmKK6CuUo$qGQO1iAIZOs=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vvQJxjp784VRf4d5zY8dYG0C1dGQIqLmKK6CuUo\u0024qGQO1iAIZOs\u003D : 
  IImageFilterFunction
{
  public double radius() => 1.0;

  public double calc_weight(double _param1)
  {
    return (2.0 * _param1 - 3.0) * _param1 * _param1 + 1.0;
  }
}
