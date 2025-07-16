// Decompiled with JetBrains decompiler
// Type: #=zupHrUO0UFO07vWyNRguf_6ysMlGddjtXaKmYmfm7BXCbJVgPatVdTk6mYJRm2u1jf0ONCavXZ04$u6e9gQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003DzupHrUO0UFO07vWyNRguf_6ysMlGddjtXaKmYmfm7BXCbJVgPatVdTk6mYJRm2u1jf0ONCavXZ04\u0024u6e9gQ\u003D\u003D : 
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D,
  \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>,
  \u0023\u003Dz03BSxVLolBnG92GmtCJpdiLKTtCzjuQ0xoe6ulFF1YojE7MNYqd4tFZ\u0024k2xs1tMz\u0024adc_4QoZXJGrlg9Jw\u003D\u003D
{
  private readonly double \u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D;
  private readonly double \u0023\u003Dz7He4cRQXQdd\u0024;
  private readonly double \u0023\u003Dzsddx1wVhEV09;
  private readonly double \u0023\u003Dzj3OllUE2noch;
  private readonly double \u0023\u003DzwJz9\u0024v\u0024HJzZW6awGLQ\u003D\u003D;
  private readonly double \u0023\u003Dz6_fch3qptd6X;
  private readonly double \u0023\u003DzMXPRhxbl7IIq;

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_6ysMlGddjtXaKmYmfm7BXCbJVgPatVdTk6mYJRm2u1jf0ONCavXZ04\u0024u6e9gQ\u003D\u003D(
    double _param1,
    double _param2,
    double _param3,
    XyDirection _param4,
    bool _param5)
    : this(_param1, _param2, _param3, 10.0, _param4 == XyDirection.XDirection, true, _param5)
  {
  }

  public \u0023\u003DzupHrUO0UFO07vWyNRguf_6ysMlGddjtXaKmYmfm7BXCbJVgPatVdTk6mYJRm2u1jf0ONCavXZ04\u0024u6e9gQ\u003D\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    bool _param5,
    bool _param6,
    bool _param7)
  {
    this.\u0023\u003DzeuXgfasUDyUfGmCF\u0024EtXjOjpTjP2(_param5);
    this.\u0023\u003Dz83sA3hbUFtmcF0NtN3F3NVYGbngE(_param6);
    this.\u0023\u003DziaxW5h6Fhau4h9lgdx67D0k\u003D(_param7);
    this.\u0023\u003Dz8\u0024GxD\u0024g_M8S4JyO6aGfp2x9x4OjeFZJQ9v\u0024BibU\u003D(true);
    this.\u0023\u003DzMXPRhxbl7IIq = _param4;
    this.\u0023\u003Dz6_fch3qptd6X = _param1;
    this.\u0023\u003Dzj3OllUE2noch = _param2 < 0.0 ? -1.0 : 1.0;
    this.\u0023\u003Dz7He4cRQXQdd\u0024 = Math.Log(this.\u0023\u003Dzj3OllUE2noch * _param3, this.\u0023\u003DzMXPRhxbl7IIq);
    this.\u0023\u003Dzsddx1wVhEV09 = Math.Log(this.\u0023\u003Dzj3OllUE2noch * _param2, this.\u0023\u003DzMXPRhxbl7IIq);
    this.\u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D = (_param1 - 1.0) / (this.\u0023\u003Dz7He4cRQXQdd\u0024 - this.\u0023\u003Dzsddx1wVhEV09);
    this.\u0023\u003DzwJz9\u0024v\u0024HJzZW6awGLQ\u003D\u003D = 1.0 / (this.\u0023\u003Dz6_fch3qptd6X - 1.0);
  }

  [SpecialName]
  public double \u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D() => this.\u0023\u003DzMXPRhxbl7IIq;

  public sealed override double \u0023\u003DzhL6gsJw\u003D(DateTime _param1)
  {
    return this.\u0023\u003DzhL6gsJw\u003D((double) _param1.Ticks);
  }

  public sealed override double \u0023\u003DzhL6gsJw\u003D(double _param1)
  {
    double num = (this.\u0023\u003Dz7He4cRQXQdd\u0024 - Math.Log(this.\u0023\u003Dzj3OllUE2noch * _param1, this.\u0023\u003DzMXPRhxbl7IIq)) * this.\u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D;
    return \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D.\u0023\u003Dzq1wgKfc\u003D(this.\u0023\u003DzO78SU8SL\u0024EgcBFJxM_M4bKPQHMQQ() ^ this.\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D(), num, this.\u0023\u003Dz6_fch3qptd6X) + this.\u0023\u003DzV1bNkSgej_yk();
  }

  public sealed override double GetDataValue(double _param1)
  {
    _param1 = \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D.\u0023\u003Dzq1wgKfc\u003D(this.\u0023\u003DzO78SU8SL\u0024EgcBFJxM_M4bKPQHMQQ() == this.\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D(), _param1 - this.\u0023\u003DzV1bNkSgej_yk(), this.\u0023\u003Dz6_fch3qptd6X);
    return Math.Pow(this.\u0023\u003DzMXPRhxbl7IIq, (this.\u0023\u003Dz7He4cRQXQdd\u0024 - this.\u0023\u003Dzsddx1wVhEV09) * _param1 * this.\u0023\u003DzwJz9\u0024v\u0024HJzZW6awGLQ\u003D\u003D + this.\u0023\u003Dzsddx1wVhEV09) * this.\u0023\u003Dzj3OllUE2noch;
  }

  public override DoubleRange \u0023\u003Dznj_TkFQ\u003D(
    double _param1,
    double _param2,
    IRange _param3)
  {
    return ((IRange) _param3.Clone()).GrowBy(_param1, _param2, true, this.\u0023\u003DzMXPRhxbl7IIq).AsDoubleRange();
  }

  public override DoubleRange \u0023\u003Dznj_TkFQ\u003D(
    double _param1,
    DoubleRange _param2)
  {
    double a = this.GetDataValue(0.0);
    double num = Math.Log(this.GetDataValue(_param1), this.\u0023\u003DzMXPRhxbl7IIq) - Math.Log(a, this.\u0023\u003DzMXPRhxbl7IIq);
    if (this.\u0023\u003DzO78SU8SL\u0024EgcBFJxM_M4bKPQHMQQ())
      num = -num;
    return new DoubleRange(Math.Pow(this.\u0023\u003DzMXPRhxbl7IIq, this.\u0023\u003Dzsddx1wVhEV09 + num), Math.Pow(this.\u0023\u003DzMXPRhxbl7IIq, this.\u0023\u003Dz7He4cRQXQdd\u0024 + num));
  }
}
