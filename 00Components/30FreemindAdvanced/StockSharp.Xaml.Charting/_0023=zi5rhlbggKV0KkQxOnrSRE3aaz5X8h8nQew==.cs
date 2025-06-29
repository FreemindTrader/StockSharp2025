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
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<Decimal>
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

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd((double) this.Min, (double) this.Max);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzP\u0024IlreZBEpOu((Decimal) _param1, (Decimal) _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal> _param3)
  {
    throw new NotImplementedException();
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal> \u0023\u003DzzXTqVFg\u003D(
    double _param1,
    double _param2)
  {
    Decimal diff = this.Diff;
    if (diff == 0.0M)
    {
      this.Max = this.Max + this.Max * (Decimal) _param2;
      this.Min = this.Min - this.Min * (Decimal) _param1;
      return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal>) this;
    }
    this.Max = this.Max + diff * (Decimal) _param2;
    this.Min = this.Min - diff * (Decimal) _param1;
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal> \u0023\u003DzJIqIiUw\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal> _param1)
  {
    this.Max = Math.Min(this.Max, _param1.Max);
    this.Min = Math.Max(this.Min, _param1.Min);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<Decimal>) this;
  }
}
