// Decompiled with JetBrains decompiler
// Type: #=zR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
public sealed class IndexRange  : 
  Range<int>
{
  
  private double \u0023\u003DzeLxWL8r3I02c;
  
  private double \u0023\u003Dz8pG_zbXA5b87;

  public IndexRange ()
  {
    this.Init(this.Min, this.Max);
  }

  public IndexRange (int _param1, int _param2)
    : base(_param1, _param2)
  {
    this.Init(this.Min, this.Max);
  }

  private void Init(int _param1, int _param2)
  {
    ((INotifyPropertyChanged) this).PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
    this.\u0023\u003DzeLxWL8r3I02c = (double) _param1;
    this.\u0023\u003Dz8pG_zbXA5b87 = (double) _param2;
  }

  private void OnPropertyChanged(object _param1, PropertyChangedEventArgs _param2)
  {
    if (_param2.PropertyName != "Min" && _param2.PropertyName != "Max")
      return;
    if ((int) this.\u0023\u003DzeLxWL8r3I02c.\u0023\u003DzZsq6ZfbZQvsf() != this.Min)
      this.\u0023\u003DzeLxWL8r3I02c = (double) this.Min;
    if ((int) this.\u0023\u003Dz8pG_zbXA5b87.\u0023\u003DzZsq6ZfbZQvsf() == this.Max)
      return;
    this.\u0023\u003Dz8pG_zbXA5b87 = (double) this.Max;
  }

  public override bool IsDefined => base.IsDefined && this.Min <= this.Max;

  public override object Clone()
  {
    return (object) new IndexRange (this.Min, this.Max)
    {
      \u0023\u003DzeLxWL8r3I02c = this.\u0023\u003DzeLxWL8r3I02c,
      \u0023\u003Dz8pG_zbXA5b87 = this.\u0023\u003Dz8pG_zbXA5b87
    };
  }

  public override int Diff => this.Max - this.Min;

  public override bool IsZero => this.Diff == 0;

  public override DoubleRange AsDoubleRange()
  {
    return new DoubleRange(this.\u0023\u003DzeLxWL8r3I02c, this.\u0023\u003Dz8pG_zbXA5b87);
  }

  public override IRange<int> SetMinMax(
    double _param1,
    double _param2)
  {
    int num1 = (int) _param1.\u0023\u003DzZsq6ZfbZQvsf();
    int num2 = (int) _param2.\u0023\u003DzZsq6ZfbZQvsf();
    this.SetMinMaxInternal(num1, num2);
    if (num1 == this.Min)
      this.\u0023\u003DzeLxWL8r3I02c = _param1;
    if (num2 == this.Max)
      this.\u0023\u003Dz8pG_zbXA5b87 = _param2;
    return (IRange<int>) this;
  }

  public override IRange<int> SetMinMax(
    double _param1,
    double _param2,
    IRange<int> _param3)
  {
    this.Min = (int) Math.Max(_param1, (double) _param3.Min);
    this.Max = (int) Math.Min(_param2, (double) _param3.Max);
    return (IRange<int>) this;
  }

  public override IRange<int> GrowBy(
    double _param1,
    double _param2)
  {
    int diff = this.Diff;
    bool flag = diff == 0;
    this.\u0023\u003Dz8pG_zbXA5b87 += _param2 * (flag ? this.\u0023\u003Dz8pG_zbXA5b87 : (double) diff);
    this.\u0023\u003DzeLxWL8r3I02c -= _param1 * (flag ? this.\u0023\u003DzeLxWL8r3I02c : (double) diff);
    if (this.\u0023\u003Dz8pG_zbXA5b87 < this.\u0023\u003DzeLxWL8r3I02c)
      NumberUtil.Swap(ref this.\u0023\u003DzeLxWL8r3I02c, ref this.\u0023\u003Dz8pG_zbXA5b87);
    int num1 = (int) this.\u0023\u003Dz8pG_zbXA5b87.\u0023\u003DzZsq6ZfbZQvsf();
    int num2 = (int) this.\u0023\u003DzeLxWL8r3I02c.\u0023\u003DzZsq6ZfbZQvsf();
    if (num1 < num2)
      NumberUtil.Swap(ref num2, ref num1);
    double zeLxWl8r3I02c = this.\u0023\u003DzeLxWL8r3I02c;
    double z8pGZbXa5b87 = this.\u0023\u003Dz8pG_zbXA5b87;
    this.Min = num2;
    this.Max = num1;
    this.\u0023\u003DzeLxWL8r3I02c = zeLxWl8r3I02c;
    this.\u0023\u003Dz8pG_zbXA5b87 = z8pGZbXa5b87;
    return (IRange<int>) new IndexRange (num2, num1)
    {
      \u0023\u003DzeLxWL8r3I02c = this.\u0023\u003DzeLxWL8r3I02c,
      \u0023\u003Dz8pG_zbXA5b87 = this.\u0023\u003Dz8pG_zbXA5b87
    };
  }

  public override string ToString()
  {
    return $"min={this.Min}, max={this.Max}, dmin={this.\u0023\u003DzeLxWL8r3I02c:F3}, dmax={this.\u0023\u003Dz8pG_zbXA5b87:F3}";
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
