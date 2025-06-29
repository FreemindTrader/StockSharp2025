// Decompiled with JetBrains decompiler
// Type: #=zR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D : 
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<int>
{
  
  private double \u0023\u003DzeLxWL8r3I02c;
  
  private double \u0023\u003Dz8pG_zbXA5b87;

  public \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D()
  {
    this.\u0023\u003DzWzUaFxw\u003D(this.Min, this.Max);
  }

  public \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(int _param1, int _param2)
    : base(_param1, _param2)
  {
    this.\u0023\u003DzWzUaFxw\u003D(this.Min, this.Max);
  }

  private void \u0023\u003DzWzUaFxw\u003D(int _param1, int _param2)
  {
    ((INotifyPropertyChanged) this).PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dz15moWio\u003D);
    this.\u0023\u003DzeLxWL8r3I02c = (double) _param1;
    this.\u0023\u003Dz8pG_zbXA5b87 = (double) _param2;
  }

  private void \u0023\u003Dz15moWio\u003D(object _param1, PropertyChangedEventArgs _param2)
  {
    if (_param2.PropertyName != "" && _param2.PropertyName != "")
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
    return (object) new \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(this.Min, this.Max)
    {
      \u0023\u003DzeLxWL8r3I02c = this.\u0023\u003DzeLxWL8r3I02c,
      \u0023\u003Dz8pG_zbXA5b87 = this.\u0023\u003Dz8pG_zbXA5b87
    };
  }

  public override int Diff => this.Max - this.Min;

  public override bool IsZero => this.Diff == 0;

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(this.\u0023\u003DzeLxWL8r3I02c, this.\u0023\u003Dz8pG_zbXA5b87);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    int num1 = (int) _param1.\u0023\u003DzZsq6ZfbZQvsf();
    int num2 = (int) _param2.\u0023\u003DzZsq6ZfbZQvsf();
    this.\u0023\u003DzP\u0024IlreZBEpOu(num1, num2);
    if (num1 == this.Min)
      this.\u0023\u003DzeLxWL8r3I02c = _param1;
    if (num2 == this.Max)
      this.\u0023\u003Dz8pG_zbXA5b87 = _param2;
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> _param3)
  {
    this.Min = (int) Math.Max(_param1, (double) _param3.Min);
    this.Max = (int) Math.Min(_param2, (double) _param3.Max);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003DzzXTqVFg\u003D(
    double _param1,
    double _param2)
  {
    int diff = this.Diff;
    bool flag = diff == 0;
    this.\u0023\u003Dz8pG_zbXA5b87 += _param2 * (flag ? this.\u0023\u003Dz8pG_zbXA5b87 : (double) diff);
    this.\u0023\u003DzeLxWL8r3I02c -= _param1 * (flag ? this.\u0023\u003DzeLxWL8r3I02c : (double) diff);
    if (this.\u0023\u003Dz8pG_zbXA5b87 < this.\u0023\u003DzeLxWL8r3I02c)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref this.\u0023\u003DzeLxWL8r3I02c, ref this.\u0023\u003Dz8pG_zbXA5b87);
    int num1 = (int) this.\u0023\u003Dz8pG_zbXA5b87.\u0023\u003DzZsq6ZfbZQvsf();
    int num2 = (int) this.\u0023\u003DzeLxWL8r3I02c.\u0023\u003DzZsq6ZfbZQvsf();
    if (num1 < num2)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num2, ref num1);
    double zeLxWl8r3I02c = this.\u0023\u003DzeLxWL8r3I02c;
    double z8pGZbXa5b87 = this.\u0023\u003Dz8pG_zbXA5b87;
    this.Min = num2;
    this.Max = num1;
    this.\u0023\u003DzeLxWL8r3I02c = zeLxWl8r3I02c;
    this.\u0023\u003Dz8pG_zbXA5b87 = z8pGZbXa5b87;
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) new \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D(num2, num1)
    {
      \u0023\u003DzeLxWL8r3I02c = this.\u0023\u003DzeLxWL8r3I02c,
      \u0023\u003Dz8pG_zbXA5b87 = this.\u0023\u003Dz8pG_zbXA5b87
    };
  }

  public override string ToString()
  {
    return string.Format("", (object) this.Min, (object) this.Max, (object) this.\u0023\u003DzeLxWL8r3I02c, (object) this.\u0023\u003Dz8pG_zbXA5b87);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003DzJIqIiUw\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> _param1)
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
    this.\u0023\u003DzP\u0024IlreZBEpOu(num2, num1);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) this;
  }
}
