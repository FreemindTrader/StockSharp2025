// Decompiled with JetBrains decompiler
// Type: #=zAtYWtSRxk8WC$EcJQ7b1L7dHXBxHQWas0w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D : 
  Range<TimeSpan>
{
  
  private static readonly string \u0023\u003Dz_wxyT_Q\u003D = "hh\\:mm\\:ss";

  public \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D()
  {
    this.Min = TimeSpan.MaxValue;
    this.Max = TimeSpan.MaxValue;
  }

  public \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D(
    TimeSpan _param1,
    TimeSpan _param2)
    : base(_param1, _param2)
  {
  }

  public override string ToString()
  {
    Type type = this.GetType();
    TimeSpan timeSpan = this.Min;
    string str1 = timeSpan.ToString(\u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D.\u0023\u003Dz_wxyT_Q\u003D);
    timeSpan = this.Max;
    string str2 = timeSpan.ToString(\u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D.\u0023\u003Dz_wxyT_Q\u003D);
    return $"{type} {{Min={str1}, Max={str2}}}";
  }

  public override object Clone()
  {
    return (object) new \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D(this.Min, this.Max);
  }

  public override TimeSpan Diff => new TimeSpan((this.Max - this.Min).Ticks);

  public override bool IsZero => (this.Max - this.Min).Ticks <= TimeSpan.MinValue.Ticks;

  public override DoubleRange AsDoubleRange()
  {
    TimeSpan timeSpan = this.Min;
    double ticks1 = (double) timeSpan.Ticks;
    timeSpan = this.Max;
    double ticks2 = (double) timeSpan.Ticks;
    return new DoubleRange(ticks1, ticks2);
  }

  public override IRange<TimeSpan> SetMinMax(
    double _param1,
    double _param2)
  {
    this.SetMinMaxInternal(new TimeSpan(NumberUtil.Constrain((long) _param1.\u0023\u003DzZsq6ZfbZQvsf(), TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks)), new TimeSpan(NumberUtil.Constrain((long) _param2.\u0023\u003DzZsq6ZfbZQvsf(), TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks)));
    return (IRange<TimeSpan>) this;
  }

  public override IRange<TimeSpan> SetMinMax(
    double _param1,
    double _param2,
    IRange<TimeSpan> _param3)
  {
    TimeSpan timeSpan1 = new TimeSpan((long) _param1.\u0023\u003DzZsq6ZfbZQvsf());
    TimeSpan timeSpan2 = new TimeSpan((long) _param2.\u0023\u003DzZsq6ZfbZQvsf());
    if (_param3 != null)
    {
      timeSpan1 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.Max<TimeSpan>(timeSpan1, _param3.Min);
      timeSpan2 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.Min<TimeSpan>(timeSpan2, _param3.Max);
    }
    this.Min = timeSpan1;
    this.Max = timeSpan2;
    return (IRange<TimeSpan>) this;
  }

  public override IRange<TimeSpan> GrowBy(
    double _param1,
    double _param2)
  {
    if (!this.Min.IsDefined() || !this.Max.IsDefined())
      return (IRange<TimeSpan>) this;
    long ticks1 = (this.Max - this.Min).Ticks;
    if (ticks1 == 0L)
    {
      TimeSpan timeSpan = this.Max;
      double ticks2 = (double) timeSpan.Ticks;
      timeSpan = this.Max;
      double num1 = (double) timeSpan.Ticks * _param2;
      this.Max = new TimeSpan((long) (ticks2 + num1));
      timeSpan = this.Min;
      double ticks3 = (double) timeSpan.Ticks;
      timeSpan = this.Min;
      double num2 = (double) timeSpan.Ticks * _param1;
      this.Min = new TimeSpan((long) (ticks3 - num2));
      return (IRange<TimeSpan>) this;
    }
    long ticks4 = (long) ((double) this.Max.Ticks + (double) ticks1 * _param2);
    long ticks5 = (long) ((double) this.Min.Ticks - (double) ticks1 * _param1);
    if (ticks4 > TimeSpan.MaxValue.Ticks || ticks5 < TimeSpan.MinValue.Ticks)
      return (IRange<TimeSpan>) this;
    this.Max = new TimeSpan(ticks4);
    this.Min = new TimeSpan(ticks5);
    return (IRange<TimeSpan>) this;
  }

  public override IRange<TimeSpan> \u0023\u003DzJIqIiUw\u003D(
    IRange<TimeSpan> _param1)
  {
    TimeSpan max = this.Max;
    TimeSpan min = this.Min;
    TimeSpan timeSpan1 = this.Max > _param1.Max ? _param1.Max : this.Max;
    TimeSpan timeSpan2 = this.Min < _param1.Min ? _param1.Min : this.Min;
    if (timeSpan2 > _param1.Max)
      timeSpan2 = _param1.Min;
    if (timeSpan1 < min)
      timeSpan1 = _param1.Max;
    if (timeSpan2 > timeSpan1)
    {
      timeSpan2 = _param1.Min;
      timeSpan1 = _param1.Max;
    }
    this.SetMinMaxInternal(timeSpan2, timeSpan1);
    return (IRange<TimeSpan>) this;
  }
}
