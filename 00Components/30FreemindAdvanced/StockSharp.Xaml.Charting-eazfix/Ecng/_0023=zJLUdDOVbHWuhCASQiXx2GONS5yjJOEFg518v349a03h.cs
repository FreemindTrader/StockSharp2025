// Decompiled with JetBrains decompiler
// Type: #=zJLUdDOVbHWuhCASQiXx2GONS5yjJOEFg518v349a03h5
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003DzJLUdDOVbHWuhCASQiXx2GONS5yjJOEFg518v349a03h5 : 
  \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D<\u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt>
{
  private readonly IPointSeries \u0023\u003Dz63SQYF34lutc;
  private readonly IPointSeries \u0023\u003DzkctAaveAJY_W;

  public \u0023\u003DzJLUdDOVbHWuhCASQiXx2GONS5yjJOEFg518v349a03h5(
    IPointSeries _param1,
    IPointSeries _param2,
    IPointSeries _param3)
    : base(_param1)
  {
    this.\u0023\u003Dz63SQYF34lutc = _param2;
    this.\u0023\u003DzkctAaveAJY_W = _param3;
  }

  [SpecialName]
  public override int \u0023\u003DzlpVGw6E\u003D()
  {
    return this.\u0023\u003Dz_\u0024BXHQKXpGkf().\u0023\u003DzlpVGw6E\u003D();
  }

  [IndexerName("#=zMRIb09I=")]
  public override \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D this[int _param1]
  {
    get
    {
      return (\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D) new \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt>(this.\u0023\u003Dz_\u0024BXHQKXpGkf().\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dz2_4KSTY\u003D(), new \u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt(this.\u0023\u003Dz_\u0024BXHQKXpGkf().\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D(), this.\u0023\u003Dz63SQYF34lutc.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D(), this.\u0023\u003DzkctAaveAJY_W.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D()));
    }
  }

  public override DoubleRange \u0023\u003DzxNQHuqrEvxH2()
  {
    int num1 = this.\u0023\u003DzlpVGw6E\u003D();
    double num2 = double.MaxValue;
    double num3 = double.MinValue;
    for (int index = 0; index < num1; ++index)
    {
      double d1 = this.\u0023\u003Dz63SQYF34lutc.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      double d2 = this.\u0023\u003DzkctAaveAJY_W.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      if (!double.IsNaN(d1) && !double.IsNaN(d2))
      {
        num2 = num2 < d2 ? num2 : d2;
        num3 = num3 > d1 ? num3 : d1;
        num1 = this.\u0023\u003Dz63SQYF34lutc.\u0023\u003DzlpVGw6E\u003D();
      }
    }
    return new DoubleRange(num2, num3);
  }
}
