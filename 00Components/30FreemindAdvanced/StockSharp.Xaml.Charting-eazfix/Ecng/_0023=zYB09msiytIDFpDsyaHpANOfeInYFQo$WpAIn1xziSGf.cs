// Decompiled with JetBrains decompiler
// Type: #=zYB09msiytIDFpDsyaHpANOfeInYFQo$WpAIn1xziSGfNI1uko0FFh3E=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public sealed class \u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D
{
  private static readonly IList<TimeSpan> \u0023\u003Dz3yhjwpbyTKgX = (IList<TimeSpan>) new TimeSpan[11]
  {
    TimeSpan.FromDays(1.0),
    TimeSpan.FromDays(2.0),
    TimeSpan.FromDays(7.0),
    TimeSpan.FromDays(14.0),
    TimeSpanExtensions.FromMonths(1),
    TimeSpanExtensions.FromMonths(3),
    TimeSpanExtensions.FromMonths(6),
    TimeSpanExtensions.FromYears(1),
    TimeSpanExtensions.FromYears(2),
    TimeSpanExtensions.FromYears(5),
    TimeSpanExtensions.FromYears(10)
  };

  public static TickCoordinates \u0023\u003DzU4j4bt2YhYuc(
    IndexRange  _param0,
    \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D _param1,
    ICategoryCoordinateCalculator _param2,
    int _param3,
    int _param4)
  {
    \u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D.\u0023\u003DzR\u0024h5gkzF2FxdwMnQnA\u003D\u003D h5gkzF2FxdwMnQnA = new \u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D.\u0023\u003DzR\u0024h5gkzF2FxdwMnQnA\u003D\u003D();
    h5gkzF2FxdwMnQnA.\u0023\u003DzkDUgQT\u0024Ndmp3 = _param4;
    h5gkzF2FxdwMnQnA.\u0023\u003DzK1JYTZ7MRwYt = _param2;
    TimeSpan timeSpan1 = _param1.Max - _param1.Min;
    h5gkzF2FxdwMnQnA.\u0023\u003DzFYPaJVU_4gr3 = new TimeSpan(timeSpan1.Ticks);
    TimeSpan timeSpan2 = \u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D.\u0023\u003Dz3yhjwpbyTKgX.FirstOrDefault<TimeSpan>(new Func<TimeSpan, bool>(h5gkzF2FxdwMnQnA.\u0023\u003DzB\u0024jkKCKwnbgrDLw6QA\u003D\u003D));
    if (timeSpan2.Equals(TimeSpan.Zero))
    {
      \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A btHuGwTeshC8eR8A = new \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A(0.0, (double) timeSpan1.Ticks / (double) TimeSpanExtensions.FromYears(1).Ticks, _param3, (uint) h5gkzF2FxdwMnQnA.\u0023\u003DzkDUgQT\u0024Ndmp3);
      btHuGwTeshC8eR8A.\u0023\u003Dz2RD3F8MtvzO1();
      \u0023\u003DzPauio66DvxKtWOFEEHOV9Y7gefdi\u0024o2zLQ\u003D\u003D wofeehoV9Y7gefdiO2zLq = btHuGwTeshC8eR8A.\u0023\u003DzAnYO0vLQEDGX();
      \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D karIzshiFbv2m5Lhw = new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(TimeSpanExtensions.FromYears((int) wofeehoV9Y7gefdiO2zLq.\u0023\u003DzZ85DqsktXJL3()), TimeSpanExtensions.FromYears((int) wofeehoV9Y7gefdiO2zLq.\u0023\u003Dzgq30Jn5PclK8()));
    }
    int index = \u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D.\u0023\u003Dz3yhjwpbyTKgX.IndexOf(timeSpan2) - 2;
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D karIzshiFbv2m5Lhw1;
    if (index >= 0)
    {
      karIzshiFbv2m5Lhw1 = new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(\u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D.\u0023\u003Dz3yhjwpbyTKgX[index], timeSpan2);
    }
    else
    {
      \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A btHuGwTeshC8eR8A = new \u0023\u003DzmAi_JN5raoSBYo9w2IEI_3gFWH1eBtHUGwTeshC8eR8A(0.0, (double) timeSpan1.Ticks, _param3, (uint) h5gkzF2FxdwMnQnA.\u0023\u003DzkDUgQT\u0024Ndmp3);
      btHuGwTeshC8eR8A.\u0023\u003Dz2RD3F8MtvzO1();
      \u0023\u003DzPauio66DvxKtWOFEEHOV9Y7gefdi\u0024o2zLQ\u003D\u003D wofeehoV9Y7gefdiO2zLq = btHuGwTeshC8eR8A.\u0023\u003DzAnYO0vLQEDGX();
      karIzshiFbv2m5Lhw1 = new \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D(TimeSpan.FromTicks((long) wofeehoV9Y7gefdiO2zLq.\u0023\u003DzZ85DqsktXJL3()), TimeSpan.FromTicks((long) wofeehoV9Y7gefdiO2zLq.\u0023\u003Dzgq30Jn5PclK8()));
    }
    double[] source1 = new \u0023\u003DzJhc8WdlQgSkcniY\u0024669aniHe9rfoFyUgrbTADSj0lBiy().GetMajorTicks((IAxisParams) new \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4toVZhpii2xTJMHSIVk\u0024BPGa8()
    {
      VisibleRange = (IRange) _param1,
      MajorDelta = (IComparable) karIzshiFbv2m5Lhw1.\u0023\u003Dzgq30Jn5PclK8(),
      MinorDelta = (IComparable) karIzshiFbv2m5Lhw1.\u0023\u003DzZ85DqsktXJL3()
    }).\u0023\u003DzvczFDIa7rqI9<IComparable>();
    int[] array1 = ((IEnumerable<double>) source1).Select<double, int>(new Func<double, int>(h5gkzF2FxdwMnQnA.\u0023\u003DzlriQv7fIybq6O\u0024QXyQ\u003D\u003D)).ToArray<int>();
    float[] array2 = ((IEnumerable<double>) source1).Select<double, float>(new Func<double, float>(h5gkzF2FxdwMnQnA.\u0023\u003DzbgKwWFXmHUKxbeGw\u0024g\u003D\u003D)).ToArray<float>();
    int[] source2 = \u0023\u003DzYB09msiytIDFpDsyaHpANOfeInYFQo\u0024WpAIn1xziSGfNI1uko0FFh3E\u003D.\u0023\u003DzvFWfv01C7Te8(_param0, array1, _param3);
    double[] array3 = ((IEnumerable<int>) source2).Select<int, double>(new Func<int, double>(h5gkzF2FxdwMnQnA.\u0023\u003DzE5OH4cF6TNfGbmYLEg\u003D\u003D)).ToArray<double>();
    float[] array4 = ((IEnumerable<int>) source2).Select<int, float>(new Func<int, float>(h5gkzF2FxdwMnQnA.\u0023\u003Dz7BGDergfw\u0024eT9PNdMw\u003D\u003D)).ToArray<float>();
    return new TickCoordinates(array3, source1, array4, array2);
  }

  private static int[] \u0023\u003DzvFWfv01C7Te8(
    IndexRange  _param0,
    int[] _param1,
    int _param2)
  {
    List<int> intList = new List<int>();
    int num1 = 0;
    int num2 = 0;
    int index1 = 1;
    while (index1 < _param1.Length)
    {
      num1 += _param1[index1] - _param1[index1 - 1];
      ++index1;
      ++num2;
    }
    int num3 = Math.Max((int) Math.Ceiling((double) num1 / (double) (num2 * _param2)), 1);
    int num4 = num3 - _param0.Min % num3;
    for (int index2 = _param0.Min + num4; index2 <= _param0.Max; index2 += num3)
      intList.Add(index2);
    return intList.ToArray();
  }

  private sealed class \u0023\u003DzR\u0024h5gkzF2FxdwMnQnA\u003D\u003D
  {
    public int \u0023\u003DzkDUgQT\u0024Ndmp3;
    public TimeSpan \u0023\u003DzFYPaJVU_4gr3;
    public ICategoryCoordinateCalculator \u0023\u003DzK1JYTZ7MRwYt;

    public bool \u0023\u003DzB\u0024jkKCKwnbgrDLw6QA\u003D\u003D(TimeSpan _param1)
    {
      return new TimeSpan(_param1.Ticks * (long) this.\u0023\u003DzkDUgQT\u0024Ndmp3) > this.\u0023\u003DzFYPaJVU_4gr3;
    }

    public int \u0023\u003DzlriQv7fIybq6O\u0024QXyQ\u003D\u003D(double _param1)
    {
      return this.\u0023\u003DzK1JYTZ7MRwYt.\u0023\u003DzFk6sufr\u0024co4e(_param1.\u0023\u003Dzxuo5aY4wjkaI());
    }

    public float \u0023\u003DzbgKwWFXmHUKxbeGw\u0024g\u003D\u003D(double _param1)
    {
      return (float) this.\u0023\u003DzK1JYTZ7MRwYt.\u0023\u003DzhL6gsJw\u003D((double) this.\u0023\u003DzK1JYTZ7MRwYt.\u0023\u003DzFk6sufr\u0024co4e(_param1.\u0023\u003Dzxuo5aY4wjkaI()));
    }

    public double \u0023\u003DzE5OH4cF6TNfGbmYLEg\u003D\u003D(int _param1)
    {
      return this.\u0023\u003DzK1JYTZ7MRwYt.\u0023\u003DzWZQlXHuDrnKc(_param1).ToDouble();
    }

    public float \u0023\u003Dz7BGDergfw\u0024eT9PNdMw\u003D\u003D(int _param1)
    {
      return (float) this.\u0023\u003DzK1JYTZ7MRwYt.\u0023\u003DzhL6gsJw\u003D((double) _param1);
    }
  }
}
