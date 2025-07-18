// Decompiled with JetBrains decompiler
// Type: #=zm$__dHBBbeN8TiOszDZ4tpH35HeyDPseaiYdk7NQiMjk
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public class NumericTickProvider : 
  \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<double>
{
  public override double[] \u0023\u003Dz65PoZl8ZJBOc(
    IAxisParams _param1)
  {
    return this.\u0023\u003Dz65PoZl8ZJBOc((IRange<double>) _param1.VisibleRange.AsDoubleRange(), (\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double>) new \u0023\u003DzPauio66DvxKtWOFEEHOV9Y7gefdi\u0024o2zLQ\u003D\u003D(_param1.MinorDelta.ToDouble(), _param1.MajorDelta.ToDouble()));
  }

  public override double[] GetMajorTicks(
    IAxisParams _param1)
  {
    return this.GetMajorTicks((IRange<double>) _param1.VisibleRange.AsDoubleRange(), (\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double>) new \u0023\u003DzPauio66DvxKtWOFEEHOV9Y7gefdi\u0024o2zLQ\u003D\u003D(_param1.MinorDelta.ToDouble(), _param1.MajorDelta.ToDouble()));
  }

  public double[] GetMajorTicks(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    return !this.\u0023\u003DziQ5R1esym2Y\u0024(_param1, _param2) ? Array.Empty<double>() : this.\u0023\u003DzuAzmESJjUh9L(_param1, _param2);
  }

  protected bool \u0023\u003DziQ5R1esym2Y\u0024(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "tickRange");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param2, "tickDelta");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) _param1.Min, "tickRange.Min").\u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D((IComparable) _param1.Max, "tickRange.Max");
    return _param2.\u0023\u003DzZ85DqsktXJL3().\u0023\u003Dz_Bj0HmLWq3hY() && _param2.\u0023\u003DzZ85DqsktXJL3().CompareTo(1E-13) >= 0 && _param1.Min.\u0023\u003Dz_Bj0HmLWq3hY() && _param1.Max.\u0023\u003Dz_Bj0HmLWq3hY();
  }

  public double[] \u0023\u003Dz65PoZl8ZJBOc(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    return !this.\u0023\u003DziQ5R1esym2Y\u0024(_param1, _param2) ? Array.Empty<double>() : this.\u0023\u003DzL8F08lm_ZvZT(_param1, _param2);
  }

  public double[] \u0023\u003Dz65PoZl8ZJBOc(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2,
    double[] _param3)
  {
    return !this.\u0023\u003DziQ5R1esym2Y\u0024(_param1, _param2) ? Array.Empty<double>() : this.\u0023\u003DzL8F08lm_ZvZT(_param1, _param2, _param3);
  }

  protected virtual double[] \u0023\u003DzuAzmESJjUh9L(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    return this.GetTicksWithinRange(_param1, _param2.\u0023\u003Dzgq30Jn5PclK8(), _param2.\u0023\u003Dzgq30Jn5PclK8());
  }

  private double[] GetTicksWithinRange(
    IRange<double> _param1,
    double _param2,
    double _param3)
  {
    List<double> doubleList = new List<double>();
    double min = _param1.Min;
    double max = _param1.Max;
    double num1 = min;
    bool flag = _param2.CompareTo(_param3) == 0;
    if (!NumberUtil.IsDivisibleBy(num1, _param2))
      num1 = NumberUtil.RoundUp(num1, _param2);
    double num2 = num1;
    int num3 = 0;
    for (; num1 <= max; num1 = num2 + (double) ++num3 * _param2)
    {
      if (!(NumberUtil.IsDivisibleBy(num1, _param3) ^ flag))
        doubleList.Add(num1);
    }
    return doubleList.ToArray();
  }

  protected virtual double[] \u0023\u003DzL8F08lm_ZvZT(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2,
    double[] _param3)
  {
    return this.\u0023\u003DzL8F08lm_ZvZT(_param1, _param2);
  }

  protected virtual double[] \u0023\u003DzL8F08lm_ZvZT(
    IRange<double> _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> _param2)
  {
    return this.GetTicksWithinRange(_param1, _param2.\u0023\u003DzZ85DqsktXJL3(), _param2.\u0023\u003Dzgq30Jn5PclK8());
  }
}
