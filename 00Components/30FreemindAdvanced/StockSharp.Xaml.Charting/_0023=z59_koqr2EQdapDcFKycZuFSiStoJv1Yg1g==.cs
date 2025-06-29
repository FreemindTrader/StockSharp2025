// Decompiled with JetBrains decompiler
// Type: #=z59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D : 
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<int>
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

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd((double) this.Min, (double) this.Max);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzP\u0024IlreZBEpOu((int) _param1, (int) _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> _param3)
  {
    throw new NotImplementedException();
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int> \u0023\u003DzzXTqVFg\u003D(
    double _param1,
    double _param2)
  {
    int num1 = this.Max - this.Min;
    if (num1 == 0)
    {
      this.Max += (int) ((double) this.Max * _param2);
      this.Min -= (int) ((double) this.Min * _param1);
      return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) this;
    }
    int num2 = this.Max + (int) ((double) num1 * _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<int>) new \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D(this.Min - (int) ((double) num1 * _param1), num2);
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
