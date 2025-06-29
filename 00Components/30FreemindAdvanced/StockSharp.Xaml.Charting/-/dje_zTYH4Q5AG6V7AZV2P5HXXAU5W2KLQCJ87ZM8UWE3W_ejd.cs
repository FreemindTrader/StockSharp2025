// Decompiled with JetBrains decompiler
// Type: -.dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd : 
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<double>
{
  public dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd()
  {
  }

  public dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(double _param1, double _param2)
    : base(_param1, _param2)
  {
  }

  public static dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzUNx30p_smDNA()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(double.NaN, double.NaN);
  }

  public override object Clone()
  {
    return (object) new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(this.Min, this.Max);
  }

  public override double Diff => this.Max - this.Min;

  public override bool IsZero => Math.Abs(this.Diff) <= double.Epsilon;

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    return this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzP\u0024IlreZBEpOu(_param1, _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> _param3)
  {
    this.Min = Math.Max(_param1, _param3.Min);
    this.Max = Math.Min(_param2, _param3.Max);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> \u0023\u003DzzXTqVFg\u003D(
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
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> \u0023\u003DzJIqIiUw\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double> _param1)
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
    this.\u0023\u003DzP\u0024IlreZBEpOu(num2, num1);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) this;
  }
}
