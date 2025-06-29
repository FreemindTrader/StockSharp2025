// Decompiled with JetBrains decompiler
// Type: #=zKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D : 
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<long>
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

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd((double) this.Min, (double) this.Max);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzP\u0024IlreZBEpOu((long) _param1, (long) _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long> _param3)
  {
    throw new NotImplementedException();
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long> \u0023\u003DzzXTqVFg\u003D(
    double _param1,
    double _param2)
  {
    long num1 = this.Max - this.Min;
    if (num1 == 0L)
    {
      this.Max += (long) ((double) this.Max * _param2);
      this.Min -= (long) ((double) this.Min * _param1);
      return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long>) this;
    }
    long num2 = this.Max + (long) (int) ((double) num1 * _param2);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long>) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D(this.Min - (long) (int) ((double) num1 * _param1), num2);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long> \u0023\u003DzJIqIiUw\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long> _param1)
  {
    long num = Math.Min(this.Max, _param1.Max);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<long>) new \u0023\u003DzKsGTwu6B0A6eMUO4QALnGBq7U0tKI4St9w\u003D\u003D(Math.Max(this.Min, _param1.Min), num);
  }
}
