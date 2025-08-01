// Decompiled with JetBrains decompiler
// Type: #=zPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
public struct \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D(
  double _param1,
  double _param2) : IPoint
{
  
  private readonly double \u0023\u003DzMANe_Ig\u003D = _param1;
  
  private readonly double \u0023\u003Dz0HHYg1s\u003D = _param2;

  public override string ToString()
  {
    return $"X={this.\u0023\u003DzMANe_Ig\u003D};Y={this.\u0023\u003Dz0HHYg1s\u003D}";
  }

  [SpecialName]
  public double X => this.\u0023\u003DzMANe_Ig\u003D;

  [SpecialName]
  public double Y => this.\u0023\u003Dz0HHYg1s\u003D;
}
