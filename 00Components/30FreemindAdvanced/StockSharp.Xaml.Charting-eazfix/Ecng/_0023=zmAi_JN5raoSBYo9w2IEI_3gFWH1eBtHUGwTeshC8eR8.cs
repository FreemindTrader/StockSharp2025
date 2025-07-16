// Decompiled with JetBrains decompiler
// Type: #=zmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Runtime.CompilerServices;

#nullable disable
public class \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A : 
  \u0023\u003Dzoab8u_ljQms67cycUNvVixWA22bWp\u0024IRliT1Tbo\u003D
{
  protected static uint \u0023\u003DzmbQYIoVdQhkN;
  protected readonly int \u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D;
  protected double \u0023\u003Dz9SNCNR_cYKQr;
  protected double \u0023\u003DzRanwdSJnFyTa;
  protected DoubleRange \u0023\u003Dzb9g4_DOjBJMx;
  private double \u0023\u003DzaK\u0024949c\u003D = double.NaN;
  private double \u0023\u003DzCdBe1JGiII2y;

  public \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A(
    double _param1,
    double _param2,
    int _param3,
    uint _param4 = 10)
  {
    if (_param3 < 1)
      throw new ArgumentException("MinorsPerMajor must be greater than or equal to 2");
    this.\u0023\u003Dz9SNCNR_cYKQr = _param1;
    this.\u0023\u003DzRanwdSJnFyTa = _param2;
    this.\u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D = _param3;
    \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A.\u0023\u003DzmbQYIoVdQhkN = _param4;
  }

  [SpecialName]
  public \u0023\u003DzPauio66DvxKtWOFEEHOV9Y7gefdi\u0024o2zLQ\u003D\u003D \u0023\u003DzAnYO0vLQEDGX()
  {
    return new \u0023\u003DzPauio66DvxKtWOFEEHOV9Y7gefdi\u0024o2zLQ\u003D\u003D(this.\u0023\u003Dz9SNCNR_cYKQr, this.\u0023\u003DzRanwdSJnFyTa);
  }

  public DoubleRange \u0023\u003DzPgZsWHrUSHPaSw8veQ\u003D\u003D()
  {
    return this.\u0023\u003Dzb9g4_DOjBJMx;
  }

  public virtual void \u0023\u003Dz2RD3F8MtvzO1()
  {
    uint num1 = (uint) Math.Max((int) \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A.\u0023\u003DzmbQYIoVdQhkN - 1, 1);
    this.\u0023\u003DzaK\u0024949c\u003D = this.\u0023\u003Dz_RGjpdoFyFI_(this.\u0023\u003DzRanwdSJnFyTa - this.\u0023\u003Dz9SNCNR_cYKQr, false);
    this.\u0023\u003DzCdBe1JGiII2y = this.\u0023\u003Dz_RGjpdoFyFI_(this.\u0023\u003DzaK\u0024949c\u003D / (double) num1, true);
    double num2 = Math.Floor(this.\u0023\u003Dz9SNCNR_cYKQr / this.\u0023\u003DzCdBe1JGiII2y) * this.\u0023\u003DzCdBe1JGiII2y;
    double num3 = Math.Ceiling(this.\u0023\u003DzRanwdSJnFyTa / this.\u0023\u003DzCdBe1JGiII2y) * this.\u0023\u003DzCdBe1JGiII2y;
    this.\u0023\u003DzRanwdSJnFyTa = this.\u0023\u003DzCdBe1JGiII2y;
    this.\u0023\u003Dz9SNCNR_cYKQr = this.\u0023\u003DzCdBe1JGiII2y / (double) this.\u0023\u003Dz5BKXmnjcYbh2SvP6Xw\u003D\u003D;
    this.\u0023\u003Dzb9g4_DOjBJMx = new DoubleRange(num2, num3);
  }

  protected virtual double \u0023\u003Dz_RGjpdoFyFI_(double _param1, bool _param2)
  {
    double y = _param1 > 0.0 ? Math.Floor(Math.Log10(_param1)) : 0.0;
    double num = (_param1 / Math.Pow(10.0, y)).\u0023\u003DzZsq6ZfbZQvsf(1, MidpointRounding.AwayFromZero);
    return (!_param2 ? (num > 1.0 ? (num > 2.0 ? (num > 5.0 ? 10.0 : 5.0) : 2.0) : 1.0) : (num >= 1.5 ? (num >= 3.0 ? (num >= 7.0 ? 10.0 : 5.0) : 2.0) : 1.0)) * Math.Pow(10.0, y);
  }
}
