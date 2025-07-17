// Decompiled with JetBrains decompiler
// Type: #=zRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM5ViTC2hY7NnM6xjAN4UiLjRWShZ5Y=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM5ViTC2hY7NnM6xjAN4UiLjRWShZ5Y\u003D : 
  IImageFilterFunction
{
  public double radius() => 1.0;

  public double calc_weight(double _param1)
  {
    return 0.5 + 0.5 * Math.Cos(Math.PI * _param1);
  }
}
