// Decompiled with JetBrains decompiler
// Type: #=zIKGIOuOUyRwFEgUWrfZxw_VCdMZ55IvngQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw_VCdMZ55IvngQ\u003D\u003D : 
  Range<float>
{
  public \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw_VCdMZ55IvngQ\u003D\u003D()
  {
  }

  public \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw_VCdMZ55IvngQ\u003D\u003D(float _param1, float _param2)
    : base(_param1, _param2)
  {
  }

  public override object Clone()
  {
    return (object) new \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw_VCdMZ55IvngQ\u003D\u003D(this.Min, this.Max);
  }

  public override float Diff => this.Max - this.Min;

  public override bool IsZero => (double) Math.Abs(this.Max - this.Min) < double.Epsilon;

  public override DoubleRange AsDoubleRange()
  {
    return new DoubleRange((double) this.Min, (double) this.Max);
  }

  public override IRange<float> SetMinMax(
    double _param1,
    double _param2)
  {
    this.SetMinMaxInternal((float) _param1, (float) _param2);
    return (IRange<float>) this;
  }

  public override IRange<float> SetMinMax(
    double _param1,
    double _param2,
    IRange<float> _param3)
  {
    this.Min = Math.Max((float) _param1, _param3.Min);
    this.Max = Math.Min((float) _param2, _param3.Max);
    return (IRange<float>) this;
  }

  public override IRange<float> GrowBy(
    double _param1,
    double _param2)
  {
    float diff = this.Diff;
    if ((double) Math.Abs(diff) < double.Epsilon)
    {
      this.Max += this.Max * (float) _param2;
      this.Min -= this.Min * (float) _param1;
      return (IRange<float>) this;
    }
    this.Max += diff * (float) _param2;
    this.Min -= diff * (float) _param1;
    return (IRange<float>) this;
  }

  public override IRange<float> \u0023\u003DzJIqIiUw\u003D(
    IRange<float> _param1)
  {
    this.Max = Math.Min(this.Max, _param1.Max);
    this.Min = Math.Max(this.Min, _param1.Min);
    return (IRange<float>) this;
  }
}
