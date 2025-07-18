// Decompiled with JetBrains decompiler
// Type: -.DateTimeAxis
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class DateTimeAxis : 
  TimeSpanAxisBase
{
  
  private static readonly List<Type> \u0023\u003DzVGdWd1PKAs\u00242 = ((IEnumerable<Type>) new Type[1]
  {
    typeof (DateTime)
  }).ToList<Type>();
  
  public static readonly DependencyProperty SubDayTextFormattingProperty = DependencyProperty.Register(nameof (SubDayTextFormatting), typeof (string), typeof (DateTimeAxis), new PropertyMetadata((object) null, new PropertyChangedCallback(AxisBase.\u0023\u003DzLUQi5D4\u003D)));

  public DateTimeAxis()
  {
    this.DefaultStyleKey = (object) typeof (DateTimeAxis);
    this.DefaultLabelProvider = (ILabelProvider) new fxTradeChartAxisLabelProvider();
    this.SetCurrentValue(AxisBase.\u0023\u003Dz1bLZaITSYGdx, (object) new \u0023\u003DzJhc8WdlQgSkcniY\u0024669aniHe9rfoFyUgrbTADSj0lBiy());
  }

  public string SubDayTextFormatting
  {
    get
    {
      return (string) this.GetValue(DateTimeAxis.SubDayTextFormattingProperty);
    }
    set
    {
      this.SetValue(DateTimeAxis.SubDayTextFormattingProperty, (object) value);
    }
  }

  public override IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    if (this.IsXAxis)
      throw new InvalidOperationException("CalculateYRange is only valid on Y-Axis types");
    double val1_1 = DateTime.MinValue.ToDouble();
    double val1_2 = DateTime.MaxValue.ToDouble();
    int length = _param1.\u0023\u003Dz4nxjMSnapDjJ.Length;
    for (int index = 0; index < length; ++index)
    {
      IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index];
      IPointSeries ftrixUnpTllY1PkTyq = _param1.\u0023\u003Dz4nxjMSnapDjJ[index];
      if (uhIm4pSg8PxqhyA71 != null && ftrixUnpTllY1PkTyq != null && !(uhIm4pSg8PxqhyA71.get_YAxisId() != this.Id))
      {
        DoubleRange klqcJ87Zm8UwE3WEjd = ftrixUnpTllY1PkTyq.\u0023\u003DzxNQHuqrEvxH2();
        val1_2 = val1_2 < klqcJ87Zm8UwE3WEjd.Min ? val1_2 : klqcJ87Zm8UwE3WEjd.Min;
        val1_1 = val1_1 > klqcJ87Zm8UwE3WEjd.Max ? val1_1 : klqcJ87Zm8UwE3WEjd.Max;
      }
    }
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(new DateTime(Math.Min((long) val1_2, DateTime.MaxValue.Ticks)), new DateTime(Math.Max((long) val1_1, DateTime.MinValue.Ticks))).GrowBy(this.GrowBy.Min, this.GrowBy.Max);
  }

  public override IRange \u0023\u003DzFwoMKP9juTnt()
  {
    IRange abyLt9clZggmJsWhw = base.\u0023\u003DzFwoMKP9juTnt();
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(abyLt9clZggmJsWhw.Min.\u0023\u003Dzxuo5aY4wjkaI(), abyLt9clZggmJsWhw.Max.\u0023\u003Dzxuo5aY4wjkaI());
  }

  protected override IRange \u0023\u003DzsB7Y9t30CQ63(
    IRange _param1)
  {
    long ticks = TimeSpan.FromDays(1.0).Ticks;
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) (_param1.Min.ToDouble() - (double) ticks), (IComparable) (_param1.Max.ToDouble() + (double) ticks));
  }

  public override void \u0023\u003DzQ4klw1orSVl\u0024(Type _param1)
  {
    if (!DateTimeAxis.\u0023\u003DzVGdWd1PKAs\u00242.Contains(_param1))
      throw new InvalidOperationException($"DateTimeAxis does not support the type {_param1}. Supported types include {string.Join(", ", DateTimeAxis.\u0023\u003DzVGdWd1PKAs\u00242.Select<Type, string>(DateTimeAxis.SomeClass34343383.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D ?? (DateTimeAxis.SomeClass34343383.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D = new Func<Type, string>(DateTimeAxis.SomeClass34343383.SomeMethond0343.\u0023\u003Dzxhk169PwYjC0CfRyZh2qvME\u003D))).ToArray<string>())}");
  }

  public override IRange \u0023\u003DzspbjXJnVtbB\u0024()
  {
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D();
  }

  public override IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    DateTime date = DateTime.UtcNow.Date;
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(date.AddDays(-1.0), date.AddDays(1.0));
  }

  protected override IRange \u0023\u003DzJMGFyjEoHSQY(
    IComparable _param1,
    IComparable _param2)
  {
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(_param1.\u0023\u003Dzxuo5aY4wjkaI(), _param2.\u0023\u003Dzxuo5aY4wjkaI());
  }

  protected override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D()
  {
    return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D) \u0023\u003DzpTBWTwmpvpgHkLhFsQhfVp7kErA5SKirgiuHeuzdyo1JF6GbLQ\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
  }

  protected override IComparable \u0023\u003Dz3ZiX3E6vqtLl(IComparable _param1)
  {
    return (IComparable) _param1.\u0023\u003Dzxuo5aY4wjkaI();
  }

  public override bool \u0023\u003Dz9yvpaTXy3ucx(
    IRange _param1)
  {
    return _param1 is \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D;
  }

  protected override List<Type> \u0023\u003DzvwDcRtQA0c4T()
  {
    return DateTimeAxis.\u0023\u003DzVGdWd1PKAs\u00242;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly DateTimeAxis.SomeClass34343383 SomeMethond0343 = new DateTimeAxis.SomeClass34343383();
    public static Func<Type, string> \u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D;

    public string \u0023\u003Dzxhk169PwYjC0CfRyZh2qvME\u003D(Type _param1) => _param1.Name;
  }
}
