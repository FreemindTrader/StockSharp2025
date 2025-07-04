// Decompiled with JetBrains decompiler
// Type: #=z2zU2vmiOzxGfUBTaur1A_kZtb7YQc$kEo5r$cPBp2g91_YdMug==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
internal abstract class \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_kZtb7YQc\u0024kEo5r\u0024cPBp2g91_YdMug\u003D\u003D : 
  \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<IComparable>
{
  public override IComparable[] \u0023\u003Dzctqa9kMCtfQQ(
    IAxisParams _param1)
  {
    return this.\u0023\u003Dzctqa9kMCtfQQ(_param1.VisibleRange, (\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<TimeSpan>) new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(_param1.MinorDelta.\u0023\u003Dzto51K8pl8UAh(), _param1.MajorDelta.\u0023\u003Dzto51K8pl8UAh()));
  }

  public override IComparable[] \u0023\u003Dz65PoZl8ZJBOc(
    IAxisParams _param1)
  {
    return this.\u0023\u003Dz65PoZl8ZJBOc(_param1.VisibleRange, (\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<TimeSpan>) new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(_param1.MinorDelta.\u0023\u003Dzto51K8pl8UAh(), _param1.MajorDelta.\u0023\u003Dzto51K8pl8UAh()));
  }

  protected override double[] \u0023\u003DzzRdFGRW8MXMa(IComparable[] _param1)
  {
    return ((IEnumerable<IComparable>) _param1).Select<IComparable, double>(new Func<IComparable, double>(this.\u0023\u003Dz3spHJE8\u003D)).ToArray<double>();
  }

  protected abstract double \u0023\u003Dz3spHJE8\u003D(IComparable _param1);

  private IComparable[] \u0023\u003Dzctqa9kMCtfQQ(
    IRange _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<TimeSpan> _param2)
  {
    List<IComparable> comparableList = new List<IComparable>();
    if (this.\u0023\u003Dz9qPl\u0024k2gJZsD(_param1, _param2.\u0023\u003Dzgq30Jn5PclK8()))
    {
      IComparable min = _param1.Min;
      IComparable max = _param1.Max;
      for (IComparable comparable = this.RoundUp(min, _param2.\u0023\u003Dzgq30Jn5PclK8()); comparable.CompareTo((object) max) <= 0 && this.\u0023\u003Dzl5VrLhRrr5CB(comparable, _param2.\u0023\u003Dzgq30Jn5PclK8()); comparable = this.\u0023\u003Dz8ly0q7w\u003D(comparable, _param2.\u0023\u003Dzgq30Jn5PclK8()))
      {
        if (!comparableList.Contains(comparable))
          comparableList.Add(comparable);
      }
    }
    return comparableList.ToArray();
  }

  private bool \u0023\u003Dz9qPl\u0024k2gJZsD(
    IRange _param1,
    TimeSpan _param2)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "tickRange");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param2, "tickDelta");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D(_param1.Min, "tickRange.Min").\u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D(_param1.Max, "tickRange.Max");
    return !_param2.\u0023\u003DzhilcjqQ\u003D() && !_param1.IsZero && _param1.Min.IsDefined() && _param1.Max.IsDefined();
  }

  protected abstract IComparable RoundUp(IComparable _param1, TimeSpan _param2);

  protected abstract bool \u0023\u003Dzl5VrLhRrr5CB(IComparable _param1, TimeSpan _param2);

  protected abstract IComparable \u0023\u003Dz8ly0q7w\u003D(IComparable _param1, TimeSpan _param2);

  protected abstract bool IsDivisibleBy(IComparable _param1, TimeSpan _param2);

  private IComparable[] \u0023\u003Dz65PoZl8ZJBOc(
    IRange _param1,
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<TimeSpan> _param2)
  {
    List<IComparable> comparableList = new List<IComparable>();
    if (this.\u0023\u003Dz9qPl\u0024k2gJZsD(_param1, _param2.\u0023\u003DzZ85DqsktXJL3()))
    {
      IComparable min = _param1.Min;
      IComparable max = _param1.Max;
      IComparable comparable = min;
      if (!this.IsDivisibleBy(comparable, _param2.\u0023\u003DzZ85DqsktXJL3()))
        comparable = this.RoundUp(comparable, _param2.\u0023\u003DzZ85DqsktXJL3());
      for (; comparable.CompareTo((object) max) < 0 && this.\u0023\u003Dzl5VrLhRrr5CB(comparable, _param2.\u0023\u003DzZ85DqsktXJL3()); comparable = this.\u0023\u003Dz8ly0q7w\u003D(comparable, _param2.\u0023\u003DzZ85DqsktXJL3()))
      {
        if (!this.IsDivisibleBy(comparable, _param2.\u0023\u003Dzgq30Jn5PclK8()) && comparable.CompareTo((object) max) != 0 && comparable.CompareTo((object) min) != 0)
          comparableList.Add(comparable);
      }
    }
    return comparableList.ToArray();
  }
}
