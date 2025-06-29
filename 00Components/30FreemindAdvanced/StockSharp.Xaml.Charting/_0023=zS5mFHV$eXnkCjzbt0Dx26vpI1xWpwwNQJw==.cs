// Decompiled with JetBrains decompiler
// Type: #=zS5mFHV$eXnkCjzbt0Dx26vpI1xWpwwNQJw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D : 
  dje_zXJSU6RS8TCQ6ZVU6XJ5NL7C4LYU27ZM5SDS8FXR9_ejd<DateTime>
{
  
  private static readonly string \u0023\u003Dz_wxyT_Q\u003D = XXX.SSS(-539442001);

  public \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D()
  {
    this.Min = DateTime.MaxValue;
    this.Max = DateTime.MaxValue;
  }

  public \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(
    DateTime _param1,
    DateTime _param2)
    : base(_param1, _param2)
  {
  }

  public override string ToString()
  {
    string format = XXX.SSS(-539442000);
    Type type = this.GetType();
    DateTime dateTime = this.Min;
    string str1 = dateTime.ToString(\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D.\u0023\u003Dz_wxyT_Q\u003D);
    dateTime = this.Max;
    string str2 = dateTime.ToString(\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D.\u0023\u003Dz_wxyT_Q\u003D);
    return string.Format(format, (object) type, (object) str1, (object) str2);
  }

  public override object Clone()
  {
    return (object) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(this.Min, this.Max);
  }

  public override DateTime Diff
  {
    get => !(this.Min > this.Max) ? new DateTime((this.Max - this.Min).Ticks) : DateTime.MinValue;
  }

  public override bool IsZero => (this.Max - this.Min).Ticks <= DateTime.MinValue.Ticks;

  public override dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd \u0023\u003DzfODy_Nxn8OGy()
  {
    DateTime dateTime = this.Min;
    double ticks1 = (double) dateTime.Ticks;
    dateTime = this.Max;
    double ticks2 = (double) dateTime.Ticks;
    return new dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd(ticks1, ticks2);
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzP\u0024IlreZBEpOu(new DateTime(\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D((long) _param1.\u0023\u003DzZsq6ZfbZQvsf(), DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks)), new DateTime(\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D((long) _param2.\u0023\u003DzZsq6ZfbZQvsf(), DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks)));
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime> \u0023\u003Dz8b8KOJANG3C3(
    double _param1,
    double _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime> _param3)
  {
    DateTime dateTime1 = new DateTime((long) _param1.\u0023\u003DzZsq6ZfbZQvsf());
    DateTime dateTime2 = new DateTime((long) _param2.\u0023\u003DzZsq6ZfbZQvsf());
    if (_param3 != null)
    {
      dateTime1 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzTOKoqZw\u003D<DateTime>(dateTime1, _param3.Min);
      dateTime2 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzRHWvkgM\u003D<DateTime>(dateTime2, _param3.Max);
    }
    this.Min = dateTime1;
    this.Max = dateTime2;
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime> \u0023\u003DzzXTqVFg\u003D(
    double _param1,
    double _param2)
  {
    if (!this.Min.\u0023\u003DzutrFxOU\u003D() || !this.Max.\u0023\u003DzutrFxOU\u003D())
      return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime>) this;
    long ticks1 = (this.Max - this.Min).Ticks;
    bool isZero = this.IsZero;
    DateTime dateTime = this.Max;
    double ticks2 = (double) dateTime.Ticks;
    double num1 = _param2;
    long num2;
    if (!isZero)
    {
      num2 = ticks1;
    }
    else
    {
      dateTime = this.Max;
      num2 = dateTime.Ticks;
    }
    double num3 = (double) num2;
    double num4 = num1 * num3;
    long ticks3 = (long) (ticks2 + num4);
    dateTime = this.Min;
    double ticks4 = (double) dateTime.Ticks;
    double num5 = _param1;
    long num6;
    if (!isZero)
    {
      num6 = ticks1;
    }
    else
    {
      dateTime = this.Min;
      num6 = dateTime.Ticks;
    }
    double num7 = (double) num6;
    double num8 = num5 * num7;
    long ticks5 = (long) (ticks4 - num8);
    if (ticks3 > DateTime.MaxValue.Ticks || ticks5 < DateTime.MinValue.Ticks)
      return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime>) this;
    if (ticks3 < ticks5)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref ticks3, ref ticks5);
    this.Max = new DateTime(ticks3);
    this.Min = new DateTime(ticks5);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime>) this;
  }

  public override \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime> \u0023\u003DzJIqIiUw\u003D(
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime> _param1)
  {
    DateTime max = this.Max;
    DateTime min = this.Min;
    DateTime dateTime1 = this.Max > _param1.Max ? _param1.Max : this.Max;
    DateTime dateTime2 = this.Min < _param1.Min ? _param1.Min : this.Min;
    if (dateTime2 > _param1.Max)
      dateTime2 = _param1.Min;
    if (dateTime1 < min)
      dateTime1 = _param1.Max;
    if (dateTime2 > dateTime1)
    {
      dateTime2 = _param1.Min;
      dateTime1 = _param1.Max;
    }
    this.\u0023\u003DzP\u0024IlreZBEpOu(dateTime2, dateTime1);
    return (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<DateTime>) this;
  }
}
