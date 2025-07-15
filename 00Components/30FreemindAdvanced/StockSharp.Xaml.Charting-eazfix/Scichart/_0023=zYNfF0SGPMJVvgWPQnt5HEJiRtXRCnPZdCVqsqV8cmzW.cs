// Decompiled with JetBrains decompiler
// Type: #=zYNfF0SGPMJVvgWPQnt5HEJiRtXRCnPZdCVqsqV8cmzW4e6QdAywEXAh86y0Qs6E_vMDCpvPvum5n
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzYNfF0SGPMJVvgWPQnt5HEJiRtXRCnPZdCVqsqV8cmzW4e6QdAywEXAh86y0Qs6E_vMDCpvPvum5n : 
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D
{
  private readonly double dgr;
  private readonly double dgs;
  private double \u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D;
  private readonly double \u0023\u003DzwJz9\u0024v\u0024HJzZW6awGLQ\u003D\u003D;
  private readonly double \u0023\u003DzUziyuwJyQ831Mi1yVQ\u003D\u003D;

  public \u0023\u003DzYNfF0SGPMJVvgWPQnt5HEJiRtXRCnPZdCVqsqV8cmzW4e6QdAywEXAh86y0Qs6E_vMDCpvPvum5n(
    double _param1,
    double _param2,
    double _param3,
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd _param4,
    bool _param5)
    : this(_param1, _param2, _param3, _param4 == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection, _param4 == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection, _param5)
  {
  }

  public \u0023\u003DzYNfF0SGPMJVvgWPQnt5HEJiRtXRCnPZdCVqsqV8cmzW4e6QdAywEXAh86y0Qs6E_vMDCpvPvum5n(
    double _param1,
    double _param2,
    double _param3,
    bool _param4,
    bool _param5,
    bool _param6)
  {
    this.\u0023\u003DzeuXgfasUDyUfGmCF\u0024EtXjOjpTjP2(_param4);
    this.\u0023\u003Dz83sA3hbUFtmcF0NtN3F3NVYGbngE(_param5);
    this.\u0023\u003DziaxW5h6Fhau4h9lgdx67D0k\u003D(_param6);
    this.dgr = _param2;
    this.dgs = _param3;
    this.\u0023\u003DzUziyuwJyQ831Mi1yVQ\u003D\u003D = _param1 - 1.0;
    this.\u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D = this.\u0023\u003DzUziyuwJyQ831Mi1yVQ\u003D\u003D / (this.dgs - this.dgr);
    this.\u0023\u003DzwJz9\u0024v\u0024HJzZW6awGLQ\u003D\u003D = 1.0 / this.\u0023\u003DzUziyuwJyQ831Mi1yVQ\u003D\u003D;
  }

  public sealed override double \u0023\u003DzhL6gsJw\u003D(DateTime _param1)
  {
    return this.\u0023\u003DzhL6gsJw\u003D((double) _param1.Ticks);
  }

  public sealed override double \u0023\u003DzhL6gsJw\u003D(double _param1)
  {
    return this.\u0023\u003DzUziyuwJyQ831Mi1yVQ\u003D\u003D - (this.dgs - _param1) * this.\u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D + this.\u0023\u003DzV1bNkSgej_yk();
  }

  public sealed override double GetDataValue(double _param1)
  {
    return (this.dgs - this.dgr) * (_param1 - this.\u0023\u003DzV1bNkSgej_yk()) * this.\u0023\u003DzwJz9\u0024v\u0024HJzZW6awGLQ\u003D\u003D + this.dgr;
  }

  internal double \u0023\u003Dz2iMPijbZ\u00245q1()
  {
    return this.\u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D;
  }

  internal void \u0023\u003DzXcUa1KEkAu53(double _param1)
  {
    this.\u0023\u003DzTs14_FMipJuw5PQcuw\u003D\u003D = _param1;
  }
}
