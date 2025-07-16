// Decompiled with JetBrains decompiler
// Type: #=zm$__dHBBbeN8TiOszDZ4toVZhpii2xTJMHSIVk$BPGa8
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4toVZhpii2xTJMHSIVk\u0024BPGa8 : 
  IAxisParams
{
  private IRange \u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;
  private IRange<double> \u0023\u003DztXZBASMM9qIoqbN93Q\u003D\u003D;
  private IComparable \u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D;
  private IComparable \u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D;

  public IRange VisibleRange
  {
    get => this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D;
    set => this.\u0023\u003DzoWUjTMpdJ7oG_NEfGcn_NZw\u003D = value;
  }

  public IRange<double> GrowBy
  {
    get => this.\u0023\u003DztXZBASMM9qIoqbN93Q\u003D\u003D;
    set => this.\u0023\u003DztXZBASMM9qIoqbN93Q\u003D\u003D = value;
  }

  public IComparable MinorDelta
  {
    get => this.\u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D;
    set => this.\u0023\u003Dze_N82X05Vu1NIUMEb0ySPgs\u003D = value;
  }

  public IComparable MajorDelta
  {
    get => this.\u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D;
    set => this.\u0023\u003DzcJHzh_mr6f8CMKRKsZVWY\u0024w\u003D = value;
  }

  public IRange \u0023\u003DzFwoMKP9juTnt()
  {
    throw new NotImplementedException();
  }
}
