// Decompiled with JetBrains decompiler
// Type: -.NumericAxis
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

public class NumericAxis : 
  AxisBase
{
  
  private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003DzvScByjqid0AM;
  
  private static readonly List<Type> \u0023\u003DzVGdWd1PKAs\u00242 = ((IEnumerable<Type>) new Type[10]
  {
    typeof (int),
    typeof (double),
    typeof (Decimal),
    typeof (long),
    typeof (float),
    typeof (short),
    typeof (byte),
    typeof (uint),
    typeof (ushort),
    typeof (sbyte)
  }).ToList<Type>();
  
  public static readonly DependencyProperty \u0023\u003DzUAJ6sU6NpGtu5h_eZK4qIhg\u003D = DependencyProperty.Register(nameof (ScientificNotation), typeof (\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a), typeof (NumericAxis), new PropertyMetadata((object) \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None, new PropertyChangedCallback(AxisBase.\u0023\u003DzLUQi5D4\u003D)));

  public NumericAxis()
  {
    this.DefaultStyleKey = (object) typeof (NumericAxis);
    this.DefaultLabelProvider = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B();
    this.SetCurrentValue(AxisBase.\u0023\u003Dz1bLZaITSYGdx, (object) new \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4tpH35HeyDPseaiYdk7NQiMjk());
  }

  public double MajorDelta
  {
    get
    {
      return (double) this.GetValue(AxisBase.\u0023\u003DzuTkKbr18L_Ur);
    }
    set
    {
      this.SetValue(AxisBase.\u0023\u003DzuTkKbr18L_Ur, (object) value);
    }
  }

  public double MinorDelta
  {
    get
    {
      return (double) this.GetValue(AxisBase.\u0023\u003Dzdg3vng1nptYM);
    }
    set
    {
      this.SetValue(AxisBase.\u0023\u003Dzdg3vng1nptYM, (object) value);
    }
  }

  public double? MinimalZoomConstrain
  {
    get
    {
      return (double?) this.GetValue(AxisBase.\u0023\u003DzjWJEoVoxRw8F);
    }
    set
    {
      this.SetValue(AxisBase.\u0023\u003DzjWJEoVoxRw8F, (object) value);
    }
  }

  public \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a ScientificNotation
  {
    get
    {
      return (\u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a) this.GetValue(NumericAxis.\u0023\u003DzUAJ6sU6NpGtu5h_eZK4qIhg\u003D);
    }
    set
    {
      this.SetValue(NumericAxis.\u0023\u003DzUAJ6sU6NpGtu5h_eZK4qIhg\u003D, (object) value);
    }
  }

  protected override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D()
  {
    return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D) \u0023\u003Dz03BSxVLolBnG92GmtCJpdjQ2_iFE7GeQXQiaDXkJcgDkWOKV\u0024A\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
  }

  protected override void \u0023\u003Dz2RD3F8MtvzO1()
  {
    IRange<double> visibleRange = (IRange<double>) this.VisibleRange;
    if (!this.AutoTicks)
      return;
    uint num = this.\u0023\u003Dzl02YIEvJDKYh();
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double> dll9dU6Qa5lGfoNjvig = (\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDLl9dU6QA5lGfoNJvig\u003D<double>) this.\u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D().\u0023\u003DzyE145DTzxI8R((IComparable) visibleRange.Min, (IComparable) visibleRange.Max, this.MinorsPerMajor, num);
    this.MajorDelta = dll9dU6Qa5lGfoNjvig.\u0023\u003Dzgq30Jn5PclK8();
    this.MinorDelta = dll9dU6Qa5lGfoNjvig.\u0023\u003DzZ85DqsktXJL3();
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("CalculateDelta: Min={0}, Max={1}, Major={2}, Minor={3}, MaxAutoTicks={4}", new object[5]
    {
      (object) visibleRange.Min,
      (object) visibleRange.Max,
      (object) dll9dU6Qa5lGfoNjvig.\u0023\u003Dzgq30Jn5PclK8(),
      (object) dll9dU6Qa5lGfoNjvig.\u0023\u003DzZ85DqsktXJL3(),
      (object) num
    });
  }

  public override IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    if (this.IsXAxis)
      throw new InvalidOperationException("CalculateYRange is only valid on Y-Axis types");
    double num1 = double.MinValue;
    double num2 = double.MaxValue;
    Dictionary<string, DoubleRange> dictionary = new Dictionary<string, DoubleRange>();
    int length = _param1.\u0023\u003Dz4nxjMSnapDjJ.Length;
    for (int index = 0; index < length; ++index)
    {
      IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index];
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = _param1.\u0023\u003Dz4nxjMSnapDjJ[index];
      if (uhIm4pSg8PxqhyA71 != null && ftrixUnpTllY1PkTyq != null && !(uhIm4pSg8PxqhyA71.get_YAxisId() != this.Id))
      {
        \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D ns01UjmP40FpxAl2jmQ = _param1.\u0023\u003Dzoc6wScE\u003D[index];
        IndexRange  g8Oq2rGx6KyfAreq = _param1.\u0023\u003Dz8O95DKv93zY9[index];
        DoubleRange klqcJ87Zm8UwE3WEjd1 = ns01UjmP40FpxAl2jmQ.get_DataSeriesType() != (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 1 || g8Oq2rGx6KyfAreq.Diff.CompareTo(1000) >= 0 ? ftrixUnpTllY1PkTyq.\u0023\u003DzxNQHuqrEvxH2() : ns01UjmP40FpxAl2jmQ.GetWindowedYRange(new IndexRange (g8Oq2rGx6KyfAreq.Min, g8Oq2rGx6KyfAreq.Max)).AsDoubleRange();
        string key = string.Empty;
        if (uhIm4pSg8PxqhyA71 is \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D)
        {
          \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D v57okcKlXhxnukOsbYoC = uhIm4pSg8PxqhyA71 as \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D;
          key = v57okcKlXhxnukOsbYoC.get_StackedGroupId();
          klqcJ87Zm8UwE3WEjd1 = (DoubleRange) v57okcKlXhxnukOsbYoC.\u0023\u003DzxNQHuqrEvxH2((IRange) _param1.\u0023\u003Dz8O95DKv93zY9[index], this.IsLogarithmicAxis);
          if (dictionary.ContainsKey(key))
          {
            DoubleRange klqcJ87Zm8UwE3WEjd2 = dictionary[v57okcKlXhxnukOsbYoC.get_StackedGroupId()];
            dictionary[v57okcKlXhxnukOsbYoC.get_StackedGroupId()] = (DoubleRange) klqcJ87Zm8UwE3WEjd1.\u0023\u003DzeiifnZI\u003D((IRange<double>) klqcJ87Zm8UwE3WEjd2);
          }
        }
        else
        {
          num2 = num2 < klqcJ87Zm8UwE3WEjd1.Min ? num2 : klqcJ87Zm8UwE3WEjd1.Min;
          num1 = num1 > klqcJ87Zm8UwE3WEjd1.Max ? num1 : klqcJ87Zm8UwE3WEjd1.Max;
        }
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, klqcJ87Zm8UwE3WEjd1);
      }
    }
    foreach (KeyValuePair<string, DoubleRange> keyValuePair in dictionary)
    {
      num2 = num2 < keyValuePair.Value.Min ? num2 : keyValuePair.Value.Min;
      num1 = num1 > keyValuePair.Value.Max ? num1 : keyValuePair.Value.Max;
    }
    IRange abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) num2, (IComparable) num1);
    double num3 = this.IsLogarithmicAxis ? ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this).get_LogarithmicBase() : 0.0;
    return this.GrowBy == null ? abyLt9clZggmJsWhw : abyLt9clZggmJsWhw.GrowBy(this.GrowBy.Min, this.GrowBy.Max, this.IsLogarithmicAxis, num3);
  }

  public override IRange \u0023\u003DzspbjXJnVtbB\u0024()
  {
    return (IRange) new DoubleRange(double.NaN, double.NaN);
  }

  public override IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    return (IRange) new DoubleRange(0.0, 10.0);
  }

  public override IAxis \u0023\u003DzQ8SgRgQ\u003D()
  {
    IAxis instance = (IAxis) Activator.CreateInstance(((object) this).GetType());
    if (this.VisibleRange != null)
      instance.VisibleRange = (IRange) this.VisibleRange.Clone();
    if (this.GrowBy != null)
      instance.GrowBy=((IRange<double>) this.GrowBy.Clone());
    return instance;
  }

  public override bool \u0023\u003Dz9yvpaTXy3ucx(
    IRange _param1)
  {
    return _param1 is DoubleRange;
  }

  protected override List<Type> \u0023\u003DzvwDcRtQA0c4T()
  {
    return NumericAxis.\u0023\u003DzVGdWd1PKAs\u00242;
  }

  protected override void \u0023\u003Dz_0Le6I5slA7z(
    \u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D _param1)
  {
    this.\u0023\u003DzvScByjqid0AM = base.\u0023\u003Dz0RktzzbyC\u002468();
    base.\u0023\u003Dz_0Le6I5slA7z(_param1);
  }

  public override \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003Dz0RktzzbyC\u002468()
  {
    return this.\u0023\u003DzvScByjqid0AM;
  }

  public override void \u0023\u003Dzs15X3Ar32F1\u0024(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1 = default (\u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D),
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2 = null)
  {
    this.\u0023\u003DzvScByjqid0AM = base.\u0023\u003Dz0RktzzbyC\u002468();
    \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D rfw9WrzF8qIcTicp0wQ = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D.OfType<\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D>().FirstOrDefault<\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D>();
    if (_param2 != null && rfw9WrzF8qIcTicp0wQ != null)
    {
      IRange<double> visibleRange = (IRange<double>) this.VisibleRange;
      Decimal? priceStep = rfw9WrzF8qIcTicp0wQ.PriceStep;
      if (priceStep.HasValue)
      {
        Decimal valueOrDefault = priceStep.GetValueOrDefault();
        this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2 = (double) valueOrDefault;
        this.\u0023\u003DzvScByjqid0AM.\u0023\u003Dz_WzdhI8nAiba = _param1.\u0023\u003Dz_li6Ttc\u003D.Height / ((visibleRange.Max - visibleRange.Min) / (double) valueOrDefault);
      }
    }
    base.\u0023\u003Dzs15X3Ar32F1\u0024(_param1, _param2);
  }

  public override double CurrentDatapointPixelSize
  {
    get => this.\u0023\u003DzvScByjqid0AM.\u0023\u003Dz_WzdhI8nAiba;
  }

  protected override IRange \u0023\u003DzsB7Y9t30CQ63(
    IRange _param1)
  {
    DoubleRange klqcJ87Zm8UwE3WEjd = _param1 as DoubleRange;
    return this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2 <= 0.0 || klqcJ87Zm8UwE3WEjd == null ? base.\u0023\u003DzsB7Y9t30CQ63(_param1) : (IRange) new DoubleRange(klqcJ87Zm8UwE3WEjd.Min - this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2, klqcJ87Zm8UwE3WEjd.Min + this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2);
  }

  public override IComparable GetDataValue(double _param1)
  {
    IComparable comparable = base.GetDataValue(_param1);
    if (this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2 <= 0.0 || !(comparable is double num))
      return comparable;
    return num.IsNaN() ? (IComparable) num : (IComparable) num.Round(this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2);
  }
}
