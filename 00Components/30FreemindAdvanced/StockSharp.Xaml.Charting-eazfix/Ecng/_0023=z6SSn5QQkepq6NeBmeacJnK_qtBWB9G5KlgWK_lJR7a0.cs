// Decompiled with JetBrains decompiler
// Type: #=z6SSn5QQkepq6NeBmeacJnK_qtBWB9G5KlgWK_lJR7a0x
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003Dz6SSn5QQkepq6NeBmeacJnK_qtBWB9G5KlgWK_lJR7a0x : 
  \u0023\u003DzmAi_JN5raoSBYo9w2IEI_5Au6CspSbO6KAfA\u00249tysDE2RAfvXg\u003D\u003D<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>
{
  private readonly IPointSeries \u0023\u003DzA2V_HFN716My;
  private readonly IPointSeries \u0023\u003DzAmGK7ptW3CxB;

  public \u0023\u003Dz6SSn5QQkepq6NeBmeacJnK_qtBWB9G5KlgWK_lJR7a0x(
    IPointSeries _param1,
    IPointSeries _param2)
    : base(_param1)
  {
    this.\u0023\u003DzA2V_HFN716My = _param1;
    this.\u0023\u003DzAmGK7ptW3CxB = _param2;
  }

  [SpecialName]
  public override int \u0023\u003DzlpVGw6E\u003D()
  {
    return this.\u0023\u003DzA2V_HFN716My.\u0023\u003DzlpVGw6E\u003D();
  }

  public new IPointSeries \u0023\u003Dz_\u0024BXHQKXpGkf()
  {
    return this.\u0023\u003DzA2V_HFN716My;
  }

  public IPointSeries \u0023\u003DzkXILACWX1Jlw()
  {
    return this.\u0023\u003DzAmGK7ptW3CxB;
  }

  [IndexerName("#=zMRIb09I=")]
  public override \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D this[int _param1]
  {
    get
    {
      return (\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D) new \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6>(this.\u0023\u003DzA2V_HFN716My.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dz2_4KSTY\u003D(), new \u0023\u003DzY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6(this.\u0023\u003DzA2V_HFN716My != null ? this.\u0023\u003DzA2V_HFN716My.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D() : 0.0, this.\u0023\u003DzAmGK7ptW3CxB.\u0023\u003Dz\u0024CeUvME\u003D(_param1).\u0023\u003Dzu7q98_E\u003D()));
    }
  }

  public override DoubleRange \u0023\u003DzxNQHuqrEvxH2()
  {
    int num1 = this.\u0023\u003DzlpVGw6E\u003D();
    double num2 = double.MaxValue;
    double num3 = double.MinValue;
    for (int index = 0; index < num1; ++index)
    {
      double d = this.\u0023\u003DzA2V_HFN716My.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      if (!double.IsNaN(d))
      {
        num2 = num2 < d ? num2 : d;
        num3 = num3 > d ? num3 : d;
      }
    }
    return new DoubleRange(num2, num3);
  }
}
