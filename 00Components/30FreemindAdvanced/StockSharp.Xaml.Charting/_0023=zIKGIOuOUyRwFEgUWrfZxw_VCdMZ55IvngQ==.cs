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
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<float>
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

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd((double) this.Min, (double) this.Max);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzP\u0024IlreZBEpOu((float) _param1, (float) _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float> _param3)
  {
    this.Min = Math.Max((float) _param1, _param3.Min);
    this.Max = Math.Min((float) _param2, _param3.Max);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float> \u0023\u003DzzXTqVFg\u003D(
    double _param1,
    double _param2)
  {
    float diff = this.Diff;
    if ((double) Math.Abs(diff) < double.Epsilon)
    {
      this.Max += this.Max * (float) _param2;
      this.Min -= this.Min * (float) _param1;
      return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float>) this;
    }
    this.Max += diff * (float) _param2;
    this.Min -= diff * (float) _param1;
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float> \u0023\u003DzJIqIiUw\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float> _param1)
  {
    this.Max = Math.Min(this.Max, _param1.Max);
    this.Min = Math.Max(this.Min, _param1.Min);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<float>) this;
  }
}
