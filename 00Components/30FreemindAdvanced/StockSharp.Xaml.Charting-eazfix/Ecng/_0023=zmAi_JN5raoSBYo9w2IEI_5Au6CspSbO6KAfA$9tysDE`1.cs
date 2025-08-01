// Decompiled with JetBrains decompiler
// Type: #=zmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA$9tysDE2RAfvXg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Runtime.CompilerServices;

#nullable disable
public abstract class \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D<TY> : 
  \u0023\u003DzJLUdDOVbHWuhCASQiXx2GOKt\u0024mK6FSeHKsHYvxDmsdABmRtdKA\u003D\u003D<TY>,
  IPointSeries
  where TY : IComparable
{
  private readonly IPointSeries \u0023\u003DzA2V_HFN716My;

  protected \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D(
    IPointSeries _param1)
  {
    this.\u0023\u003DzA2V_HFN716My = _param1;
  }

  [SpecialName]
  public IPointSeries \u0023\u003Dz_\u0024BXHQKXpGkf()
  {
    return this.\u0023\u003DzA2V_HFN716My;
  }

  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzwQnyySN6xaVC()
  {
    return this.\u0023\u003DzA2V_HFN716My.\u0023\u003DzwQnyySN6xaVC();
  }

  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<double> \u0023\u003DzPqsSI6C5MOOb()
  {
    return this.\u0023\u003DzA2V_HFN716My.\u0023\u003DzPqsSI6C5MOOb();
  }

  [SpecialName]
  public abstract int \u0023\u003DzlpVGw6E\u003D();

  [IndexerName("#=zMRIb09I=")]
  public abstract IPoint this[int _param1] { get; }

  public abstract DoubleRange \u0023\u003DzxNQHuqrEvxH2();
}
