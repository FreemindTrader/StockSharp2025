// Decompiled with JetBrains decompiler
// Type: -.dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
namespace \u002D;

internal sealed class dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd : 
  dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd,
  IDrawable,
  IAxisParams,
  ISuspendable,
  IInvalidatableElement,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe6IW1TrHf1OjeIxI4VnnySGI,
  IAxis,
  IHitTestable
{
  
  public static readonly DependencyProperty \u0023\u003Dzaf3Lae48WNlm = DependencyProperty.Register(nameof (BarTimeFrame), typeof (double), typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd), new PropertyMetadata((object) -1.0));
  
  public static readonly DependencyProperty \u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D = DependencyProperty.Register(nameof (SubDayTextFormatting), typeof (string), typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  private static readonly List<Type> \u0023\u003DzVGdWd1PKAs\u00242 = ((IEnumerable<Type>) new Type[1]
  {
    typeof (DateTime)
  }).ToList<Type>();
  
  private \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003DzvScByjqid0AM;
  
  private double _dataPointWidth = double.NaN;

  public dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd);
    this.DefaultLabelProvider = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) new \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u0oVUOjcWZm6tIn4bpm9YMvj_jwo7f3RMYA\u003D();
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1bLZaITSYGdx, (object) new \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4tpH35HeyDPseaiYdk7NQiMjk());
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfolHRDLbOj27, (object) new \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEnKJO\u0024vNxvMVTEURYjOQFVGJMB3rXA\u003D\u003D());
  }

  public string SubDayTextFormatting
  {
    get
    {
      return (string) this.GetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D, (object) value);
    }
  }

  public double BarTimeFrame
  {
    get
    {
      return (double) this.GetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003Dzaf3Lae48WNlm);
    }
    set
    {
      this.SetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003Dzaf3Lae48WNlm, (object) value);
    }
  }

  public int? MinimalZoomConstrain
  {
    get
    {
      return (int?) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzjWJEoVoxRw8F);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzjWJEoVoxRw8F, (object) value);
    }
  }

  public override double CurrentDatapointPixelSize => this._dataPointWidth;

  public override bool IsCategoryAxis => true;

  public override IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    throw new InvalidOperationException("CalculateYRange is only valid on Y-Axis types");
  }

  public override IRange \u0023\u003DzFwoMKP9juTnt()
  {
    if (!this.IsXAxis)
      throw new InvalidOperationException("CategoryDateTimeAxis is only valid as an X-Axis");
    IRange abyLt9clZggmJsWhw1 = this.VisibleRange == null || !this.VisibleRange.IsDefined ? this.\u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D() : this.VisibleRange;
    IRange abyLt9clZggmJsWhw2 = this.\u0023\u003Dzd6x7lH_dQH0I();
    if (abyLt9clZggmJsWhw2 != null)
    {
      abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw2.GrowBy(this.GrowBy.Min, this.GrowBy.Max);
      if (this.VisibleRangeLimit != null)
        abyLt9clZggmJsWhw1.\u0023\u003DzJIqIiUw\u003D(this.VisibleRangeLimit, this.VisibleRangeLimitMode);
    }
    return abyLt9clZggmJsWhw1;
  }

  protected override IRange \u0023\u003Dzd6x7lH_dQH0I()
  {
    return this.ParentSurface == null || this.ParentSurface.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>() ? (IRange) null : (IRange) this.\u0023\u003DzrBtJ_MpEE_B5();
  }

  private IndexRange  \u0023\u003DzrBtJ_MpEE_B5()
  {
    IndexRange  g8Oq2rGx6KyfAreq = (IndexRange ) null;
    IRenderableSeries s1JolYrWoYpqmQ6ug = this.ParentSurface.get_RenderableSeries().FirstOrDefault<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzcHRWPhoLpVN6adCW3mQzIrw\u003D));
    if (s1JolYrWoYpqmQ6ug != null)
      g8Oq2rGx6KyfAreq = new IndexRange (0, s1JolYrWoYpqmQ6ug.get_DataSeries().\u0023\u003DzwQnyySN6xaVC().Count - 1);
    return g8Oq2rGx6KyfAreq;
  }

  public override IAxis \u0023\u003DzQ8SgRgQ\u003D()
  {
    dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd nu9622VfydaypdeqEjd = new dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd();
    if (this.VisibleRange != null)
      nu9622VfydaypdeqEjd.VisibleRange = (IRange) this.VisibleRange.Clone();
    if (this.GrowBy != null)
      nu9622VfydaypdeqEjd.GrowBy = (IRange<double>) this.GrowBy.Clone();
    nu9622VfydaypdeqEjd.BarTimeFrame = -1.0;
    return (IAxis) nu9622VfydaypdeqEjd;
  }

  protected override void \u0023\u003Dz2RD3F8MtvzO1()
  {
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D fyGp0VhHiJ5hoI4Ca = this.\u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D();
    uint num1 = this.\u0023\u003Dzl02YIEvJDKYh();
    IComparable min = this.VisibleRange.Min;
    IComparable max = this.VisibleRange.Max;
    int minorsPerMajor = this.MinorsPerMajor;
    int num2 = (int) num1;
    \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqGwqd9HRLNiqw6g\u003D\u003D mqGwqd9HrlNiqw6g = fyGp0VhHiJ5hoI4Ca.\u0023\u003DzyE145DTzxI8R(min, max, minorsPerMajor, (uint) num2);
    this.MajorDelta = mqGwqd9HrlNiqw6g.\u0023\u003Dzgq30Jn5PclK8();
    this.MinorDelta = mqGwqd9HrlNiqw6g.\u0023\u003DzZ85DqsktXJL3();
  }

  protected override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D()
  {
    return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D) \u0023\u003Dz03BSxVLolBnG92GmtCJpdjQ2_iFE7GeQXQiaDXkJcgDkWOKV\u0024A\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
  }

  public override double \u0023\u003DzhL6gsJw\u003D(IComparable _param1)
  {
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w = this.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    if (xkzemsMs5tGkouk5w == null)
      return double.NaN;
    if (xkzemsMs5tGkouk5w is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0 && _param1 is DateTime)
      _param1 = (IComparable) q9i0MXI7Qb9c1V6c0.\u0023\u003DzFk6sufr\u0024co4e((DateTime) _param1);
    return this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D.\u0023\u003DzhL6gsJw\u003D(_param1.ToDouble());
  }

  public override IComparable GetDataValue(double _param1)
  {
    if (this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D == null)
      return (IComparable) int.MaxValue;
    double num = this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D.GetDataValue(_param1);
    return (IComparable) (this.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0 ? q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc((int) num) : num.\u0023\u003Dzxuo5aY4wjkaI());
  }

  protected override IComparable \u0023\u003Dz3ZiX3E6vqtLl(IComparable _param1)
  {
    return (IComparable) _param1.\u0023\u003Dzxuo5aY4wjkaI();
  }

  public override bool \u0023\u003Dz9yvpaTXy3ucx(
    IRange _param1)
  {
    return _param1 is IndexRange ;
  }

  public override IRange \u0023\u003DzspbjXJnVtbB\u0024()
  {
    return (IRange) new IndexRange (0, int.MaxValue);
  }

  public override IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    return (IRange) new IndexRange (0, 10);
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
    if (_param2 != null)
    {
      this.\u0023\u003DzFDFY\u0024WdH2REe(_param2);
    }
    else
    {
      this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzJqKipxOjoBSS = false;
      this.\u0023\u003DzvScByjqid0AM.\u0023\u003DznUzlqIN9ReXL = (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) null;
    }
    base.\u0023\u003Dzs15X3Ar32F1\u0024(_param1, _param2);
  }

  public override void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1, TimeSpan _param2)
  {
    if (!(this.VisibleRange is IndexRange  visibleRange))
      return;
    IndexRange  g8Oq2rGx6KyfAreq = new IndexRange (visibleRange.Min + _param1, visibleRange.Max + _param1);
    this.\u0023\u003DzuPwHeHOc6hD2hGW59w\u003D\u003D((IRange) g8Oq2rGx6KyfAreq);
    this.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D((IRange) g8Oq2rGx6KyfAreq, _param2);
  }

  private void \u0023\u003DzFDFY\u0024WdH2REe(
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param1)
  {
    this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzJqKipxOjoBSS = true;
    this.\u0023\u003DzvScByjqid0AM.\u0023\u003DznUzlqIN9ReXL = _param1;
    IRange visibleRange = this.VisibleRange;
    int val1 = visibleRange != null ? Math.Max((int) visibleRange.Min, 0) : 0;
    int num = visibleRange != null ? Math.Max(val1, (int) visibleRange.Max) : 0;
    this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzToxB29CkMiO6 = new IndexRange (val1, num);
    this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzormciIUBnCr2 = this.\u0023\u003DzbY7N\u0024Xk2WSr8();
    this._dataPointWidth = dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003Dz6Vq26Hfm2pXj((IndexRange ) visibleRange, this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzdTxNrgQ\u003D);
    this.\u0023\u003DzvScByjqid0AM.\u0023\u003Dz_WzdhI8nAiba = this._dataPointWidth;
    this.\u0023\u003DzvScByjqid0AM.\u0023\u003DzdTxNrgQ\u003D -= this._dataPointWidth;
    this.\u0023\u003Dz15moWio\u003D("CurrentDatapointPixelSize");
  }

  internal double \u0023\u003DzbY7N\u0024Xk2WSr8()
  {
    IList z9jZg9RufbqZ = this.\u0023\u003DzvScByjqid0AM.\u0023\u003Dz9jZG_9RUfbqZ;
    double num = (double) TimeSpan.FromSeconds(this.BarTimeFrame).Ticks;
    if (this.BarTimeFrame <= 0.0)
    {
      double ticks = (double) TimeSpan.FromSeconds(60.0).Ticks;
      if (z9jZg9RufbqZ != null && z9jZg9RufbqZ.Count >= 2)
      {
        int index = z9jZg9RufbqZ.Count - 1;
        num = (((DateTime) z9jZg9RufbqZ[index]).ToDouble() - ((DateTime) z9jZg9RufbqZ[0]).ToDouble()) / (double) index;
      }
      num = num > 0.0 ? num : ticks;
    }
    return num;
  }

  internal static double \u0023\u003Dz6Vq26Hfm2pXj(
    IndexRange  _param0,
    double _param1)
  {
    if (_param0 == null)
      return 1.0;
    int num = _param0.Max - _param0.Min + 1;
    return _param1 / (double) num;
  }

  public \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D \u0023\u003DzFL7WRclCPBWI(
    IndexRange  _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "visibleRange");
    \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D dx26vpI1xWpwwNqJw = (\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D) null;
    if (this.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0)
      dx26vpI1xWpwwNqJw = new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(_param1.Min).\u0023\u003Dzxuo5aY4wjkaI(), q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(_param1.Max).\u0023\u003Dzxuo5aY4wjkaI());
    return dx26vpI1xWpwwNqJw;
  }

  protected override List<Type> \u0023\u003DzvwDcRtQA0c4T()
  {
    return dje_zP5SLCZMPLKRDSVWETEPWLMZPT4N45VSYZ76M5M7C6J68NU9622VFYDAYPDEQ_ejd.\u0023\u003DzVGdWd1PKAs\u00242;
  }

  HorizontalAlignment IAxis.\u0023\u003Dz5VLaAZX2bctAcuSoajSAXtBWUytTwKmwegWB430RRP_iyVUjrw\u003D\u003D()
  {
    return this.HorizontalAlignment;
  }

  void IAxis.\u0023\u003DzTbSy5Tg7CNKewHb2FguXqzKcL0mg\u0024lar5H2OZ3W_18PGuoI1WA\u003D\u003D(
    HorizontalAlignment _param1)
  {
    this.HorizontalAlignment = _param1;
  }

  VerticalAlignment IAxis.\u0023\u003DzSseiGdgwJmJ1pkmz7CEFfx8mbOWyc1wXvn8wBzjwACKu6EY0OQ\u003D\u003D()
  {
    return this.VerticalAlignment;
  }

  void IAxis.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntTNDUky6SmSb6\u0024FDWAQO1Y0HmfujBQ\u003D\u003D(
    VerticalAlignment _param1)
  {
    this.VerticalAlignment = _param1;
  }

  Visibility IAxis.\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRYwo3gBY9dA\u0024Mbe\u0024dG0As1jePnhZWw\u003D\u003D()
  {
    return this.Visibility;
  }

  void IAxis.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntSyV0Rj0ibC6aIMhpwJ2VCPFsZZaBw\u003D\u003D(
    Visibility _param1)
  {
    this.Visibility = _param1;
  }

  double IHitTestable.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double IHitTestable.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }

  private bool \u0023\u003DzcHRWPhoLpVN6adCW3mQzIrw\u003D(
    IRenderableSeries _param1)
  {
    if (!(_param1.get_XAxisId() == this.Id))
      return false;
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = _param1.get_DataSeries();
    return dataSeries != null && dataSeries.get_HasValues();
  }
}
