// Decompiled with JetBrains decompiler
// Type: #=zFDK4fEILkMRswIjIg1$y3Bm8RZiwmK4L3EV3D9Q_3Sui7NwtBg1zT9cdY4yX
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
internal sealed class \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3Bm8RZiwmK4L3EV3D9Q_3Sui7NwtBg1zT9cdY4yX : 
  \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4tpH35HeyDPseaiYdk7NQiMjk
{
  private double \u0023\u003DzCQNtQf8p5cRzMEiHVFCbIW6ZUR\u0024dAulyiA\u003D\u003D;

  public double \u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D()
  {
    return this.\u0023\u003DzCQNtQf8p5cRzMEiHVFCbIW6ZUR\u0024dAulyiA\u003D\u003D;
  }

  public void \u0023\u003DzY2pNM8i3KOHB8USXquggYrI\u003D(double _param1)
  {
    this.\u0023\u003DzCQNtQf8p5cRzMEiHVFCbIW6ZUR\u0024dAulyiA\u003D\u003D = _param1;
  }

  protected override double[] \u0023\u003DzuAzmESJjUh9L(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    List<double> doubleList = new List<double>();
    double min = _param1.Min;
    double max = _param1.Max;
    double a = min;
    double num1 = _param2.\u0023\u003Dzgq30Jn5PclK8();
    if (!NumberUtil.IsPowerOf(a, this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D()))
      a = NumberUtil.RoundDownPower(a, this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D());
    double num2 = Math.Round(Math.Log(a, this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D()), 10);
    if (!NumberUtil.IsDivisibleBy(num2, num1))
      num2 = NumberUtil.RoundUp(num2, num1);
    double y = num2;
    double num3 = Math.Pow(this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), y);
    int num4 = 0;
    for (; num3 <= max; num3 = Math.Pow(this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), y))
    {
      if (NumberUtil.IsDivisibleBy(y, num1))
        doubleList.Add(num3);
      y = num2 + (double) ++num4 * num1;
    }
    return doubleList.ToArray();
  }

  protected override double[] \u0023\u003DzL8F08lm_ZvZT(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    double[] numArray = this.\u0023\u003DzuAzmESJjUh9L(_param1, _param2);
    return this.\u0023\u003DzL8F08lm_ZvZT(_param1, _param2, numArray);
  }

  protected override double[] \u0023\u003DzL8F08lm_ZvZT(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2,
    double[] _param3)
  {
    List<double> doubleList = new List<double>();
    double num1 = _param2.\u0023\u003DzZ85DqsktXJL3();
    double y = _param2.\u0023\u003Dzgq30Jn5PclK8();
    for (int length = _param3.Length; length >= 0; --length)
    {
      double num2 = Math.Pow(this.\u0023\u003DzHGmIgQ4g1NZaU1vzTOt9eAU\u003D(), y);
      double num3 = length < _param3.Length ? _param3[length] : _param3[length - 1] * num2;
      double num4 = num3 / num2;
      double num5 = num4 * num1;
      for (double num6 = num4 + num5; num6 < num3; num6 += num5)
        doubleList.Add(num6);
    }
    doubleList.Reverse();
    return doubleList.ToArray();
  }
}
