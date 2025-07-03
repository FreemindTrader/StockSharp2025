// Decompiled with JetBrains decompiler
// Type: -.DoubleRange
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
namespace \u002D;

internal sealed class DoubleRange : 
  Range<double>
{
  public DoubleRange()
  {
  }

  public DoubleRange(double _param1, double _param2)
    : base(_param1, _param2)
  {
  }

  public static DoubleRange UndefinedRange
  {
    return new DoubleRange(double.NaN, double.NaN);
  }

  public override object Clone()
  {
    return (object) new DoubleRange(this.Min, this.Max);
  }

  public override double Diff => this.Max - this.Min;

  public override bool IsZero => Math.Abs(this.Diff) <= double.Epsilon;

  public override DoubleRange AsDoubleRange()
  {
    return this;
  }

  public override IRange<double> SetMinMax(
    double _param1,
    double _param2)
  {
    this.SetMinMaxInternal(_param1, _param2);
    return (IRange<double>) this;
  }

  public override IRange<double> SetMinMax(
    double _param1,
    double _param2,
    IRange<double> _param3)
  {
    this.Min = Math.Max(_param1, _param3.Min);
    this.Max = Math.Min(_param2, _param3.Max);
    return (IRange<double>) this;
  }

  public override IRange<double> GrowBy(
    double _param1,
    double _param2)
  {
    double diff = this.Diff;
    double num1 = this.Min - _param1 * (this.IsZero ? this.Min : diff);
    double num2 = this.Max + _param2 * (this.IsZero ? this.Max : diff);
    if (num1 > num2)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num1, ref num2);
    if (Math.Abs(num1 - num2) <= double.Epsilon && Math.Abs(num1) <= double.Epsilon)
    {
      num1 = -1.0;
      num2 = 1.0;
    }
    this.Min = num1;
    this.Max = num2;
    return (IRange<double>) this;
  }

  public override IRange<double> \u0023\u003DzJIqIiUw\u003D(
    IRange<double> _param1)
  {
    double max = this.Max;
    double min = this.Min;
    double num1 = Math.Min(this.Max, _param1.Max);
    double num2 = Math.Max(this.Min, _param1.Min);
    if (num2 > _param1.Max)
      num2 = _param1.Min;
    if (num1 < min)
      num1 = _param1.Max;
    if (num2 > num1)
    {
      num2 = _param1.Min;
      num1 = _param1.Max;
    }
    this.SetMinMaxInternal(num2, num1);
    return (IRange<double>) this;
  }
}
