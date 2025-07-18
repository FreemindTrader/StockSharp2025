// Decompiled with JetBrains decompiler
// Type: #=z5tgJOvSsgsmn_0Qv_7eNQyLB5gOqhWUoP6MuFJyCu$rQqXo4LTiJvVgyCa_6Bm39_Yn25sk=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections;

#nullable disable
public sealed class \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQyLB5gOqhWUoP6MuFJyCu\u0024rQqXo4LTiJvVgyCa_6Bm39_Yn25sk\u003D : 
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D,
  \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>,
  ICategoryCoordinateCalculator
{
  private bool \u0023\u003DzOU8wceErNMhUttw6c6n4ESA\u003D;
  private IList \u0023\u003DzGQUQhOv49SbNC0pu6Ad4lKU\u003D;
  private readonly double \u0023\u003DzUJCePcfzDIqu;
  private readonly IndexRange  \u0023\u003DzbcBiY3v5phYc;
  private readonly double \u0023\u003Dz0AQCdKVYriX\u0024cCousg\u003D\u003D;
  private readonly double \u0023\u003Dz6_fch3qptd6X;

  public \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQyLB5gOqhWUoP6MuFJyCu\u0024rQqXo4LTiJvVgyCa_6Bm39_Yn25sk\u003D(
    double _param1,
    double _param2,
    double _param3,
    IPointSeries _param4,
    IndexRange  _param5,
    bool _param6,
    IList _param7,
    bool _param8)
  {
    this.\u0023\u003DzGur9DsivJBMn(_param7);
    this.\u0023\u003Dz4OmLXA2fvE0iTkvNxQ\u003D\u003D(_param8);
    this.\u0023\u003DzeuXgfasUDyUfGmCF\u0024EtXjOjpTjP2(true);
    this.\u0023\u003Dz83sA3hbUFtmcF0NtN3F3NVYGbngE(_param6);
    this.\u0023\u003DzvDM5VNsRq\u0024n4O3LH\u0024gcgFYuzm5ek(true);
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) _param1, "barTimeFrame").\u0023\u003Dzc5Mtj4NgZLWC((IComparable) 0.0);
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param4, "categoryPointSeries");
    this.\u0023\u003DzUJCePcfzDIqu = _param1;
    this.\u0023\u003DzbcBiY3v5phYc = _param5;
    this.\u0023\u003Dz0AQCdKVYriX\u0024cCousg\u003D\u003D = _param2;
    this.\u0023\u003Dz6_fch3qptd6X = _param3;
  }

  public bool \u0023\u003DzpnH1hhKZYi_ClVj9VA\u003D\u003D()
  {
    return this.\u0023\u003DzOU8wceErNMhUttw6c6n4ESA\u003D;
  }

  private void \u0023\u003Dz4OmLXA2fvE0iTkvNxQ\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzOU8wceErNMhUttw6c6n4ESA\u003D = _param1;
  }

  private IList \u0023\u003DzpUn2Qsz7CiQS() => this.\u0023\u003DzGQUQhOv49SbNC0pu6Ad4lKU\u003D;

  private void \u0023\u003DzGur9DsivJBMn(IList _param1)
  {
    this.\u0023\u003DzGQUQhOv49SbNC0pu6Ad4lKU\u003D = _param1;
  }

  public sealed override double \u0023\u003DzhL6gsJw\u003D(DateTime _param1)
  {
    return this.\u0023\u003DzhL6gsJw\u003D((double) _param1.Ticks);
  }

  public sealed override double \u0023\u003DzhL6gsJw\u003D(double _param1)
  {
    return this.\u0023\u003Dz6_fch3qptd6X * (_param1.\u0023\u003DzZsq6ZfbZQvsf() - (double) this.\u0023\u003DzbcBiY3v5phYc.Min) / (double) (this.\u0023\u003DzbcBiY3v5phYc.Max - this.\u0023\u003DzbcBiY3v5phYc.Min) + this.\u0023\u003DzV1bNkSgej_yk();
  }

  public sealed override double GetDataValue(double _param1)
  {
    return ((double) (this.\u0023\u003DzbcBiY3v5phYc.Max - this.\u0023\u003DzbcBiY3v5phYc.Min) * (_param1 - this.\u0023\u003DzV1bNkSgej_yk()) / this.\u0023\u003Dz6_fch3qptd6X + (double) this.\u0023\u003DzbcBiY3v5phYc.Min).\u0023\u003DzZsq6ZfbZQvsf();
  }

  public override DoubleRange \u0023\u003Dznj_TkFQ\u003D(
    double _param1,
    DoubleRange _param2)
  {
    int num = (int) (-_param1 / this.\u0023\u003Dz0AQCdKVYriX\u0024cCousg\u003D\u003D);
    return new DoubleRange(_param2.Min + (double) num, _param2.Max + (double) num);
  }

  public DateTime \u0023\u003DzWZQlXHuDrnKc(int _param1)
  {
    if (_param1 == int.MinValue)
      return DateTime.MinValue;
    int count = this.\u0023\u003DzpUn2Qsz7CiQS().Count;
    IComparable comparable = (IComparable) DateTime.MinValue;
    if (!this.\u0023\u003DzpUn2Qsz7CiQS().\u0023\u003DzfFZjWlZiH0nd())
      comparable = _param1 >= 0 ? (_param1 < count ? this.\u0023\u003Dzu4GWOma99kiX(_param1) : (IComparable) ((double) (_param1 - count + 1) * this.\u0023\u003DzUJCePcfzDIqu + (double) this.\u0023\u003Dzu4GWOma99kiX(count - 1).\u0023\u003Dzxuo5aY4wjkaI().Ticks)) : (IComparable) ((double) _param1 * this.\u0023\u003DzUJCePcfzDIqu + (double) this.\u0023\u003Dzu4GWOma99kiX(0).\u0023\u003Dzxuo5aY4wjkaI().Ticks);
    return comparable.\u0023\u003Dzxuo5aY4wjkaI();
  }

  public int \u0023\u003DzFk6sufr\u0024co4e(IComparable _param1)
  {
    return this.\u0023\u003DzFk6sufr\u0024co4e(_param1.\u0023\u003Dzxuo5aY4wjkaI());
  }

  public int \u0023\u003DzFk6sufr\u0024co4e(DateTime _param1)
  {
    return this.\u0023\u003DzFk6sufr\u0024co4e(_param1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1);
  }

  public int \u0023\u003DzFk6sufr\u0024co4e(
    DateTime _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2)
  {
    int count = this.\u0023\u003DzpUn2Qsz7CiQS().Count;
    int num = this.\u0023\u003Dz23D407H2FgVA((IComparable) _param1, _param2);
    if (!this.\u0023\u003DzpUn2Qsz7CiQS().\u0023\u003DzfFZjWlZiH0nd())
    {
      DateTime dateTime1 = ((IComparable) this.\u0023\u003DzpUn2Qsz7CiQS()[count - 1]).\u0023\u003Dzxuo5aY4wjkaI();
      DateTime dateTime2 = ((IComparable) this.\u0023\u003DzpUn2Qsz7CiQS()[0]).\u0023\u003Dzxuo5aY4wjkaI();
      if (_param1 > dateTime1)
        num = (int) ((double) (_param1.Ticks - dateTime1.Ticks) / this.\u0023\u003DzUJCePcfzDIqu + (double) count - 1.0);
      else if (_param1 < dateTime2)
        num = (int) ((double) (_param1.Ticks - dateTime2.Ticks) / this.\u0023\u003DzUJCePcfzDIqu);
    }
    return num;
  }

  private IComparable \u0023\u003Dzu4GWOma99kiX(int _param1)
  {
    _param1 = NumberUtil.Constrain(_param1, 0, this.\u0023\u003DzpUn2Qsz7CiQS().Count - 1);
    return (IComparable) this.\u0023\u003DzpUn2Qsz7CiQS()[_param1];
  }

  private int \u0023\u003Dz23D407H2FgVA(
    IComparable _param1,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param2)
  {
    if (!this.\u0023\u003DzpUn2Qsz7CiQS().\u0023\u003DzfFZjWlZiH0nd())
      _param1 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzsQE9ClE\u003D(_param1.ToDouble(), this.\u0023\u003DzpUn2Qsz7CiQS()[0].GetType());
    return this.\u0023\u003DzpUn2Qsz7CiQS().\u0023\u003DzFH1yjjY\u003D(this.\u0023\u003DzpnH1hhKZYi_ClVj9VA\u003D\u003D(), _param1, _param2);
  }
}
