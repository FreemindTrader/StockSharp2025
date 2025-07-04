// Decompiled with JetBrains decompiler
// Type: #=z3RRntx4pzkd854dIVpLK6S_EKy$AtkpA9s$N3N0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003Dz3RRntx4pzkd854dIVpLK6S_EKy\u0024AtkpA9s\u0024N3N0\u003D : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzhqaqE72KwYhN;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzBzxDw0vK0H2y;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzWnRrpa5ameJV0z9YzfgOUTE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dzx0rsvwquW0jl8rk1q4d2AbI\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dzyn6MOqk7gf4U1Xps1g\u003D\u003D;

  public \u0023\u003Dz3RRntx4pzkd854dIVpLK6S_EKy\u0024AtkpA9s\u0024N3N0\u003D(
    IRenderableSeries _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.MinimumValue = Convert.ToDouble((object) _param2.Minimum);
    this.MaximumValue = Convert.ToDouble((object) _param2.Maximum);
    this.MedianValue = Convert.ToDouble((object) _param2.\u0023\u003Dztppz3AJQqFM5JvzsZw\u003D\u003D());
    this.LowerQuartileValue = Convert.ToDouble((object) _param2.\u0023\u003Dzfb6u1svFQnJip22g8mPdcNo\u003D());
    this.UpperQuartileValue = Convert.ToDouble((object) _param2.\u0023\u003DzXV41m4nLk9xqErg0fYHc8lw\u003D());
  }

  public double MinimumValue
  {
    get => this.\u0023\u003DzhqaqE72KwYhN;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzhqaqE72KwYhN, value, nameof (MinimumValue)))
        return;
      this.\u0023\u003Dz15moWio\u003D("FormattedMinimumValue");
    }
  }

  public string FormattedMinimumValue
  {
    get => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.MinimumValue);
  }

  public double MaximumValue
  {
    get => this.\u0023\u003DzBzxDw0vK0H2y;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzBzxDw0vK0H2y, value, nameof (MaximumValue)))
        return;
      this.\u0023\u003Dz15moWio\u003D("FormattedMaximumValue");
    }
  }

  public string FormattedMaximumValue
  {
    get => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.MaximumValue);
  }

  public double MedianValue
  {
    get => this.\u0023\u003Dzyn6MOqk7gf4U1Xps1g\u003D\u003D;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003Dzyn6MOqk7gf4U1Xps1g\u003D\u003D, value, nameof (MedianValue)))
        return;
      this.\u0023\u003Dz15moWio\u003D("FormattedMedianValue");
    }
  }

  public string FormattedMedianValue
  {
    get => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.MedianValue);
  }

  public double LowerQuartileValue
  {
    get => this.\u0023\u003DzWnRrpa5ameJV0z9YzfgOUTE\u003D;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzWnRrpa5ameJV0z9YzfgOUTE\u003D, value, nameof (LowerQuartileValue)))
        return;
      this.\u0023\u003Dz15moWio\u003D("FormattedLowerQuartileValue");
    }
  }

  public string FormattedLowerQuartileValue
  {
    get => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.LowerQuartileValue);
  }

  public double UpperQuartileValue
  {
    get => this.\u0023\u003Dzx0rsvwquW0jl8rk1q4d2AbI\u003D;
    set
    {
      if (!this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003Dzx0rsvwquW0jl8rk1q4d2AbI\u003D, value, nameof (UpperQuartileValue)))
        return;
      this.\u0023\u003Dz15moWio\u003D("FormattedUpperQuartileValue");
    }
  }

  public string FormattedUpperQuartileValue
  {
    get => this.\u0023\u003DzwMhD0eRlLP6L((IComparable) this.UpperQuartileValue);
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    \u0023\u003Dz3RRntx4pzkd854dIVpLK6S_EKy\u0024AtkpA9s\u0024N3N0\u003D lk6SEkyAtkpA9sN3N0 = (\u0023\u003Dz3RRntx4pzkd854dIVpLK6S_EKy\u0024AtkpA9s\u0024N3N0\u003D) _param1;
    this.MinimumValue = lk6SEkyAtkpA9sN3N0.MinimumValue;
    this.MaximumValue = lk6SEkyAtkpA9sN3N0.MaximumValue;
    this.LowerQuartileValue = lk6SEkyAtkpA9sN3N0.LowerQuartileValue;
    this.UpperQuartileValue = lk6SEkyAtkpA9sN3N0.UpperQuartileValue;
    this.MedianValue = lk6SEkyAtkpA9sN3N0.MedianValue;
  }
}
