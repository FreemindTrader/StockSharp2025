// Decompiled with JetBrains decompiler
// Type: #=z59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D : 
  Range<int>
{
  public \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D()
  {
  }

  public \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D(int _param1, int _param2)
    : base(_param1, _param2)
  {
  }

  public override object Clone()
  {
    return (object) new \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D(this.Min, this.Max);
  }

  public override int Diff => this.Max - this.Min;

  public override bool IsZero => this.Diff == 0;

  public override DoubleRange AsDoubleRange()
  {
    return new DoubleRange((double) this.Min, (double) this.Max);
  }

  public override IRange<int> SetMinMax(
    double _param1,
    double _param2)
  {
    this.SetMinMaxInternal((int) _param1, (int) _param2);
    return (IRange<int>) this;
  }

  public override IRange<int> SetMinMax(
    double _param1,
    double _param2,
    IRange<int> _param3)
  {
    throw new NotImplementedException();
  }

  public override IRange<int> GrowBy(
    double _param1,
    double _param2)
  {
    int num1 = this.Max - this.Min;
    if (num1 == 0)
    {
      this.Max += (int) ((double) this.Max * _param2);
      this.Min -= (int) ((double) this.Min * _param1);
      return (IRange<int>) this;
    }
    int num2 = this.Max + (int) ((double) num1 * _param2);
    return (IRange<int>) new \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D(this.Min - (int) ((double) num1 * _param1), num2);
  }

  public override IRange<int> \u0023\u003DzJIqIiUw\u003D(
    IRange<int> _param1)
  {
    int max = this.Max;
    int min = this.Min;
    int num1 = this.Max > _param1.Max ? _param1.Max : this.Max;
    int num2 = this.Min < _param1.Min ? _param1.Min : this.Min;
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
    return (IRange<int>) this;
  }
}
