// Decompiled with JetBrains decompiler
// Type: #=zKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D : 
  Range<long>
{
  public \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D()
  {
  }

  public \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D(long _param1, long _param2)
    : base(_param1, _param2)
  {
  }

  public override object Clone()
  {
    return (object) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D(this.Min, this.Max);
  }

  public override long Diff => this.Max - this.Min;

  public override bool IsZero => this.Diff == 0L;

  public override DoubleRange AsDoubleRange()
  {
    return new DoubleRange((double) this.Min, (double) this.Max);
  }

  public override IRange<long> SetMinMax(
    double _param1,
    double _param2)
  {
    this.SetMinMaxInternal((long) _param1, (long) _param2);
    return (IRange<long>) this;
  }

  public override IRange<long> SetMinMax(
    double _param1,
    double _param2,
    IRange<long> _param3)
  {
    throw new NotImplementedException();
  }

  public override IRange<long> GrowBy(
    double _param1,
    double _param2)
  {
    long num1 = this.Max - this.Min;
    if (num1 == 0L)
    {
      this.Max += (long) ((double) this.Max * _param2);
      this.Min -= (long) ((double) this.Min * _param1);
      return (IRange<long>) this;
    }
    long num2 = this.Max + (long) (int) ((double) num1 * _param2);
    return (IRange<long>) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D(this.Min - (long) (int) ((double) num1 * _param1), num2);
  }

  public override IRange<long> \u0023\u003DzJIqIiUw\u003D(
    IRange<long> _param1)
  {
    long num = Math.Min(this.Max, _param1.Max);
    return (IRange<long>) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D(Math.Max(this.Min, _param1.Min), num);
  }
}
