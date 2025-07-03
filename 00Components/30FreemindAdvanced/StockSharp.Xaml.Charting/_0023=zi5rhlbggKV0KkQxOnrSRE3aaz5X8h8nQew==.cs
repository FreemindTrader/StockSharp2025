// Decompiled with JetBrains decompiler
// Type: #=zi5rhlbggKV0KkQxOnrSRE3aaz5X8h8nQew==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003Dzi5rhlbggKV0KkQxOnrSRE3aaz5X8h8nQew\u003D\u003D : 
  Range<Decimal>
{
  public \u0023\u003Dzi5rhlbggKV0KkQxOnrSRE3aaz5X8h8nQew\u003D\u003D()
  {
  }

  public \u0023\u003Dzi5rhlbggKV0KkQxOnrSRE3aaz5X8h8nQew\u003D\u003D(
    Decimal _param1,
    Decimal _param2)
    : base(_param1, _param2)
  {
  }

  public override Decimal Diff => this.Max - this.Min;

  public override bool IsZero => this.Diff == 0M;

  public override object Clone()
  {
    return (object) new \u0023\u003Dzi5rhlbggKV0KkQxOnrSRE3aaz5X8h8nQew\u003D\u003D(this.Min, this.Max);
  }

  public override DoubleRange AsDoubleRange()
  {
    return new DoubleRange((double) this.Min, (double) this.Max);
  }

  public override IRange<Decimal> SetMinMax(
    double _param1,
    double _param2)
  {
    this.SetMinMaxInternal((Decimal) _param1, (Decimal) _param2);
    return (IRange<Decimal>) this;
  }

  public override IRange<Decimal> SetMinMax(
    double _param1,
    double _param2,
    IRange<Decimal> _param3)
  {
    throw new NotImplementedException();
  }

  public override IRange<Decimal> GrowBy(
    double _param1,
    double _param2)
  {
    Decimal diff = this.Diff;
    if (diff == 0.0M)
    {
      this.Max = this.Max + this.Max * (Decimal) _param2;
      this.Min = this.Min - this.Min * (Decimal) _param1;
      return (IRange<Decimal>) this;
    }
    this.Max = this.Max + diff * (Decimal) _param2;
    this.Min = this.Min - diff * (Decimal) _param1;
    return (IRange<Decimal>) this;
  }

  public override IRange<Decimal> \u0023\u003DzJIqIiUw\u003D(
    IRange<Decimal> _param1)
  {
    this.Max = Math.Min(this.Max, _param1.Max);
    this.Min = Math.Max(this.Min, _param1.Min);
    return (IRange<Decimal>) this;
  }
}
