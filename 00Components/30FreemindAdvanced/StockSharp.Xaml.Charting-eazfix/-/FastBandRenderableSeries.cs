// Decompiled with JetBrains decompiler
// Type: -.FastBandRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class FastBandRenderableSeries : 
  BaseRenderableSeries
{
  
  public static readonly DependencyProperty \u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D = DependencyProperty.Register(nameof (IsDigitalLine), typeof (bool), typeof (FastBandRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzV4cOZ_3w4VF7 = DependencyProperty.Register(nameof (Series1Color), typeof (Color), typeof (FastBandRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzqXdfa4iudfGA = DependencyProperty.Register(nameof (BandUpColor), typeof (Color), typeof (FastBandRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzJJDbrFb7cXV\u0024 = DependencyProperty.Register(nameof (BandDownColor), typeof (Color), typeof (FastBandRenderableSeries), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dz6dTf9uHwK3CQ2m0now\u003D\u003D = DependencyProperty.Register(nameof (Series0StrokeDashArray), typeof (double[]), typeof (FastBandRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzFtj5s4w63Vxye7u2Bw\u003D\u003D = DependencyProperty.Register(nameof (Series1StrokeDashArray), typeof (double[]), typeof (FastBandRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  private FrameworkElement \u0023\u003DzYbhxxX0hEwb\u0024mbXc7A6rNBo\u003D;

  public FastBandRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastBandRenderableSeries);
  }

  public bool IsDigitalLine
  {
    get
    {
      return (bool) this.GetValue(FastBandRenderableSeries.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D);
    }
    set
    {
      this.SetValue(FastBandRenderableSeries.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D, (object) value);
    }
  }

  [TypeConverter(typeof (\u0023\u003DzdJvCkWEpdQt1kv1Y55xjvd3\u0024HvNYL\u0024mKr9gng\u0024AK3Fkh))]
  public double[] Series0StrokeDashArray
  {
    get
    {
      return (double[]) this.GetValue(FastBandRenderableSeries.\u0023\u003Dz6dTf9uHwK3CQ2m0now\u003D\u003D);
    }
    set
    {
      this.SetValue(FastBandRenderableSeries.\u0023\u003Dz6dTf9uHwK3CQ2m0now\u003D\u003D, (object) value);
    }
  }

  [TypeConverter(typeof (\u0023\u003DzdJvCkWEpdQt1kv1Y55xjvd3\u0024HvNYL\u0024mKr9gng\u0024AK3Fkh))]
  public double[] Series1StrokeDashArray
  {
    get
    {
      return (double[]) this.GetValue(FastBandRenderableSeries.\u0023\u003DzFtj5s4w63Vxye7u2Bw\u003D\u003D);
    }
    set
    {
      this.SetValue(FastBandRenderableSeries.\u0023\u003DzFtj5s4w63Vxye7u2Bw\u003D\u003D, (object) value);
    }
  }

  public Color Series1Color
  {
    get
    {
      return (Color) this.GetValue(FastBandRenderableSeries.\u0023\u003DzV4cOZ_3w4VF7);
    }
    set
    {
      this.SetValue(FastBandRenderableSeries.\u0023\u003DzV4cOZ_3w4VF7, (object) value);
    }
  }

  public Color BandDownColor
  {
    get
    {
      return (Color) this.GetValue(FastBandRenderableSeries.\u0023\u003DzJJDbrFb7cXV\u0024);
    }
    set
    {
      this.SetValue(FastBandRenderableSeries.\u0023\u003DzJJDbrFb7cXV\u0024, (object) value);
    }
  }

  public Color BandUpColor
  {
    get
    {
      return (Color) this.GetValue(FastBandRenderableSeries.\u0023\u003DzqXdfa4iudfGA);
    }
    set
    {
      this.SetValue(FastBandRenderableSeries.\u0023\u003DzqXdfa4iudfGA, (object) value);
    }
  }

  public FrameworkElement \u0023\u003DzqGcIkFXNQBdDMR9NQxO5bUc\u003D()
  {
    return this.\u0023\u003DzYbhxxX0hEwb\u0024mbXc7A6rNBo\u003D;
  }

  private void \u0023\u003DzKku6BxUK5qV2HDtZFsAnu9o\u003D(FrameworkElement _param1)
  {
    this.\u0023\u003DzYbhxxX0hEwb\u0024mbXc7A6rNBo\u003D = _param1;
  }

  protected override void \u0023\u003Dz2KqJz\u00247bE2vLRKrYBA\u003D\u003D()
  {
    base.\u0023\u003Dz2KqJz\u00247bE2vLRKrYBA\u003D\u003D();
    this.\u0023\u003DzKku6BxUK5qV2HDtZFsAnu9o\u003D((FrameworkElement) PointMarker.\u0023\u003DzBv1vB\u0024LEKSF4(this.RolloverMarkerTemplate, (object) this));
  }

  protected override HitTestInfo \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    HitTestInfo zldchDrVsrVyHh6WyiGy = base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, this.\u0023\u003Dz1runmyhnjbZYf6YRbnCukUGsf9D0YvUs2A\u003D\u003D(_param2), _param3);
    zldchDrVsrVyHh6WyiGy.\u0023\u003DzQ9xCEGz0Gl\u0024q((DataSeriesType) 2);
    if (_param3 && !zldchDrVsrVyHh6WyiGy.\u0023\u003Dzmh1LiTa467ce())
    {
      Tuple<IComparable, IComparable> tuple = this.\u0023\u003Dzs0Y0\u0024lrpmkkQ(_param1);
      int index1 = this.DataSeries.\u0023\u003DzwQnyySN6xaVC().\u0023\u003DzFH1yjjY\u003D(this.DataSeries.get_IsSorted(), tuple.Item1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2);
      int index2 = this.DataSeries.\u0023\u003DzwQnyySN6xaVC().\u0023\u003DzFH1yjjY\u003D(this.DataSeries.get_IsSorted(), tuple.Item1, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
      double num1 = ((IComparable) this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[index1]).ToDouble();
      double num2 = ((IComparable) this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[index2]).ToDouble();
      Point point1 = new Point(num1, ((IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[index1]).ToDouble());
      Point point2 = new Point(num2, ((IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[index2]).ToDouble());
      int num3 = double.IsNaN(point1.Y) ? 1 : (double.IsNaN(point2.Y) ? 1 : 0);
      Point point3 = new Point(num1, ((IComparable) ((\u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH) this.DataSeries).\u0023\u003DzxBmSWTMopfir()[index1]).ToDouble());
      Point point4 = new Point(num2, ((IComparable) ((\u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH) this.DataSeries).\u0023\u003DzxBmSWTMopfir()[index2]).ToDouble());
      int num4 = double.IsNaN(point3.Y) ? (true ? 1 : 0) : (double.IsNaN(point4.Y) ? 1 : 0);
      if ((num3 | num4) != 0)
        return zldchDrVsrVyHh6WyiGy;
      Point point5 = new Point(tuple.Item1.ToDouble(), tuple.Item2.ToDouble());
      Point point6;
      if (\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_Q3WCiJm2fzt(new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D(point1, point2), new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D(point3, point4), out point6))
        zldchDrVsrVyHh6WyiGy.\u0023\u003Dzn3o1RS9wuET8(\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_0KJG0n18I8E(point5, point1, point3, point6) || \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_0KJG0n18I8E(point5, point2, point4, point6));
      else
        zldchDrVsrVyHh6WyiGy.\u0023\u003Dzn3o1RS9wuET8(\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_0KJG0n18I8E(point5, point1, point2, point3) || \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_0KJG0n18I8E(point5, point3, point4, point2));
    }
    return zldchDrVsrVyHh6WyiGy;
  }

  protected override HitTestInfo \u0023\u003Dzr7PRxQcLL3EF(
    Point _param1,
    double _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3,
    bool _param4)
  {
    HitTestInfo zldchDrVsrVyHh6WyiGy = base.\u0023\u003Dzr7PRxQcLL3EF(_param1, _param2, _param3, _param4);
    if (!zldchDrVsrVyHh6WyiGy.\u0023\u003DzMeGSfVE\u003D())
    {
      bool flag = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV();
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
      double num1 = flag ? zldchDrVsrVyHh6WyiGy.\u0023\u003DzxZfJER0dbHuS().Y : zldchDrVsrVyHh6WyiGy.\u0023\u003DzxZfJER0dbHuS().X;
      double num2 = zldchDrVsrVyHh6WyiGy.\u0023\u003DzpV2MuX1Y\u0024EoN().ToDouble();
      double num3 = xkzemsMs5tGkouk5w.\u0023\u003DzhL6gsJw\u003D(num2);
      Point point = this.\u0023\u003Dzop6vn0GowyiR(new Point(num1, num3), flag);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dz8RUGHczgdGF57F9Tiw\u003D\u003D(point);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dzn3o1RS9wuET8(zldchDrVsrVyHh6WyiGy.\u0023\u003Dzmh1LiTa467ce() || \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(point, _param1) < _param2);
    }
    return zldchDrVsrVyHh6WyiGy;
  }

  protected override HitTestInfo \u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(
    Point _param1,
    HitTestInfo _param2,
    double _param3)
  {
    HitTestInfo zldchDrVsrVyHh6WyiGy1 = new HitTestInfo();
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dz2Iv\u0024sxQuGDBR(_param2.\u0023\u003DztryT5H42SVj8());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzBswzhzuQHrrX(_param2.\u0023\u003DzpV2MuX1Y\u0024EoN());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dzo2ftAfxjqC04(_param2.\u0023\u003DzsTZhf2NJangnoun2zQ\u003D\u003D());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzV4wgjRUOXtRf(_param2.\u0023\u003DzSkvCFWUKQ7Fw());
    HitTestInfo zldchDrVsrVyHh6WyiGy2 = zldchDrVsrVyHh6WyiGy1;
    Tuple<double, double> tuple1 = this.\u0023\u003Dz0iTacSsYe_3qOn01Ow\u003D\u003D(_param2.\u0023\u003DzSkvCFWUKQ7Fw(), new Func<int, double>(this.\u0023\u003Dz0D9nQXfw9PS9XdbmK6BuM0s6nQRIlp3hKg\u003D\u003D));
    HitTestInfo zldchDrVsrVyHh6WyiGy3 = this.\u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(_param1, _param2, _param3, tuple1, (Tuple<double, double>) null);
    Tuple<double, double> tuple2 = this.\u0023\u003Dz0iTacSsYe_3qOn01Ow\u003D\u003D(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzSkvCFWUKQ7Fw(), new Func<int, double>(this.\u0023\u003Dz_Zd\u0024WqC1Iyyk5s9eL6trPhOVda_6sRI59g\u003D\u003D));
    HitTestInfo zldchDrVsrVyHh6WyiGy4 = this.\u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(_param1, zldchDrVsrVyHh6WyiGy2, _param3, tuple2, (Tuple<double, double>) null);
    zldchDrVsrVyHh6WyiGy3.\u0023\u003Dz3JT1kQLA9WwW(zldchDrVsrVyHh6WyiGy4.\u0023\u003Dzd9IAScWutAfJ());
    zldchDrVsrVyHh6WyiGy3.\u0023\u003Dz8RUGHczgdGF57F9Tiw\u003D\u003D(zldchDrVsrVyHh6WyiGy4.\u0023\u003DzxZfJER0dbHuS());
    zldchDrVsrVyHh6WyiGy3.\u0023\u003Dzn3o1RS9wuET8(zldchDrVsrVyHh6WyiGy3.\u0023\u003Dzmh1LiTa467ce() || zldchDrVsrVyHh6WyiGy4.\u0023\u003Dzmh1LiTa467ce());
    return zldchDrVsrVyHh6WyiGy3;
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.Series1Color.A != (byte) 0 && this.StrokeThickness > 0 || this.BandDownColor.A != (byte) 0 || this.BandUpColor.A != (byte) 0 || this.PointMarker != null;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    this.\u0023\u003Dzz7UraMUVt1cf<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>("XyyDataSeries");
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null, PenLineCap.Round))
    {
      using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = _param1.\u0023\u003DzL3In9ls\u003D(this.Series1Color, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null, PenLineCap.Round))
        this.\u0023\u003Dz22YRQ7A35A6I(rhwYsZxA33iRu6Id7J1, rhwYsZxA33iRu6Id7J2, _param1, _param2);
    }
  }

  private void \u0023\u003Dz22YRQ7A35A6I(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param2,
    IRenderContext2D _param3,
    IRenderPassData _param4)
  {
    using (IBrush2D xrgcdFbSdWgN9GcT8_1 = _param3.\u0023\u003Dze8WyDhI\u003D(this.BandUpColor, this.Opacity, new bool?()))
    {
      using (IBrush2D xrgcdFbSdWgN9GcT8_2 = _param3.\u0023\u003Dze8WyDhI\u003D(this.BandDownColor, this.Opacity, new bool?()))
      {
        \u0023\u003Dz59_koqr2EQdapDcFKycZuMFujzBx_Vn_sKSeFk9GdLpI bxVnSKseFk9GdLpI = (\u0023\u003Dz59_koqr2EQdapDcFKycZuMFujzBx_Vn_sKSeFk9GdLpI) this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
        int num1 = bxVnSKseFk9GdLpI.Count();
        \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param3, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
        foreach (FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D z71mAcyQ in (IEnumerable<FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D>) this.\u0023\u003DzKNuqvbEQzJvP6cmJXg\u003D\u003D(_param3, _param4, bxVnSKseFk9GdLpI.\u0023\u003Dz_\u0024BXHQKXpGkf(), xrgcdFbSdWgN9GcT8_1, bxVnSKseFk9GdLpI.\u0023\u003DzPL7HPIragYCv(), xrgcdFbSdWgN9GcT8_2))
        {
          Point[] array = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzUIfyPrQYBsuR((IEnumerable<Point>) z71mAcyQ.\u0023\u003Dz9I1m\u0024aaHezaI(), _param3.\u0023\u003Dz8DEW4l1E337F(), 0, 0).ToArray<Point>();
          iluL6N4L8CsqVgQq.\u0023\u003Dz_I15ZX7u91\u0024T(z71mAcyQ.\u0023\u003DzRsgTSqM\u003D(), array);
        }
        using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param3.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, this.Series0StrokeDashArray, PenLineCap.Round))
          \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dz3GCmtrCAMr5X(_param3, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D()), rhwYsZxA33iRu6Id7J, bxVnSKseFk9GdLpI.\u0023\u003Dz_\u0024BXHQKXpGkf(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, this.DrawNaNAs == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines);
        using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param3.\u0023\u003DzL3In9ls\u003D(this.Series1Color, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, this.Series1StrokeDashArray, PenLineCap.Round))
          \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dz3GCmtrCAMr5X(_param3, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D()), rhwYsZxA33iRu6Id7J, bxVnSKseFk9GdLpI.\u0023\u003DzPL7HPIragYCv(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, this.DrawNaNAs == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines);
        \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
        if (hw1ki13vvK4WxOgoljkHyInT == null)
          return;
        int num2 = hw1ki13vvK4WxOgoljkHyInT.Stroke == this.SeriesColor ? 1 : 0;
        bool flag = hw1ki13vvK4WxOgoljkHyInT.Fill == this.SeriesColor;
        \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = num2 != 0 ? _param1 : (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) null;
        \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = num2 != 0 ? _param2 : (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) null;
        IBrush2D xrgcdFbSdWgN9GcT8_3 = flag ? xrgcdFbSdWgN9GcT8_1 : (IBrush2D) null;
        IBrush2D xrgcdFbSdWgN9GcT8_4 = flag ? xrgcdFbSdWgN9GcT8_2 : (IBrush2D) null;
        for (int index = 0; index < num1; ++index)
        {
          \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD> iwzGyvO4YaqDkpiI = bxVnSKseFk9GdLpI[index] as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD>;
          double num3 = iwzGyvO4YaqDkpiI.\u0023\u003Dz2_4KSTY\u003D();
          \u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD dyaB43Ay2Ep7KjoD = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
          double num4 = dyaB43Ay2Ep7KjoD.\u0023\u003Dz1iB_fGLmDWyy();
          dyaB43Ay2Ep7KjoD = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
          double num5 = dyaB43Ay2Ep7KjoD.\u0023\u003DzZB\u0024O5xT4bzKv();
          if (num4 == num4 && num5 == num5)
          {
            float num6 = (float) _param4.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(num3);
            float num7 = (float) _param4.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num4);
            float num8 = (float) _param4.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num5);
            iluL6N4L8CsqVgQq.\u0023\u003DzNq_YOflx6uAn(hw1ki13vvK4WxOgoljkHyInT, new Point((double) num6, (double) num7), xrgcdFbSdWgN9GcT8_3, rhwYsZxA33iRu6Id7J1);
            iluL6N4L8CsqVgQq.\u0023\u003DzNq_YOflx6uAn(hw1ki13vvK4WxOgoljkHyInT, new Point((double) num6, (double) num8), xrgcdFbSdWgN9GcT8_4, rhwYsZxA33iRu6Id7J2);
          }
        }
      }
    }
  }

  protected virtual IList<FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D> \u0023\u003DzKNuqvbEQzJvP6cmJXg\u003D\u003D(
    IRenderContext2D _param1,
    IRenderPassData _param2,
    IPointSeries _param3,
    IBrush2D _param4,
    IPointSeries _param5,
    IBrush2D _param6)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param3, "yPointSeries");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param5, "y1PointSeries");
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzsmufvA2pkwpA(_param3.Count(), "yPointSeries", _param5.Count(), "y1PointSeries");
    List<FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D> z71mAcyQList = new List<FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D>();
    int num1 = _param3.Count();
    List<Point> pointList = new List<Point>(32 /*0x20*/);
    int num2 = 0;
    int num3 = FastBandRenderableSeries.\u0023\u003DzaQ9tpxhMknmu(_param3, _param5, num2, num1);
    for (int index = num3 + 1; index < num1; ++index)
    {
      double num4 = _param3.\u0023\u003Dz\u0024CeUvME\u003D(index - 1).\u0023\u003Dz2_4KSTY\u003D();
      double num5 = _param3.\u0023\u003Dz\u0024CeUvME\u003D(index - 1).\u0023\u003Dzu7q98_E\u003D();
      double num6 = _param5.\u0023\u003Dz\u0024CeUvME\u003D(index - 1).\u0023\u003Dzu7q98_E\u003D();
      double num7 = _param3.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dz2_4KSTY\u003D();
      double num8 = _param3.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      double num9 = _param5.\u0023\u003Dz\u0024CeUvME\u003D(index).\u0023\u003Dzu7q98_E\u003D();
      if ((num8.IsNaN() ? 1 : (num9.IsNaN() ? 1 : 0)) != 0)
      {
        this.\u0023\u003DzVyzQAuU\u003D(num3, index, _param3, _param2, pointList);
        this.\u0023\u003Dz3fxFWrVp1Ljj(num3, index, _param5, _param2, pointList);
        z71mAcyQList.Add(new FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D(pointList.ToArray(), num5 > num6 ? _param4 : _param6));
        int num10 = index + 1;
        index = FastBandRenderableSeries.\u0023\u003DzaQ9tpxhMknmu(_param3, _param5, num10, num1);
        if (index < num1)
        {
          num3 = index;
          pointList = new List<Point>(32 /*0x20*/);
        }
        else
          break;
      }
      else
      {
        Point point1;
        if (\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_Q3WCiJm2fzt(new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D(num4, num5, num7, num8), new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D(num4, num6, num7, num9), out point1))
        {
          \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
          \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
          bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
          float num11 = (float) xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(num7);
          float num12 = (float) xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(num4);
          Point point2 = new Point(xkzemsMs5tGkouk5w1.\u0023\u003DzcNWwm_gWa4NJdtQNJ1Cl\u0024zStdK0t() ? (double) num12 + ((double) num11 - (double) num12) * (point1.X - num4) : xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(point1.X), xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(point1.Y));
          this.\u0023\u003DzVyzQAuU\u003D(num3, index, _param3, _param2, pointList);
          if (this.IsDigitalLine)
          {
            pointList.Add(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num11, xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num5)), flag));
            pointList.Add(this.\u0023\u003Dzop6vn0GowyiR(new Point((double) num11, xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num6)), flag));
          }
          else
            pointList.Add(this.\u0023\u003Dzop6vn0GowyiR(point2, flag));
          this.\u0023\u003Dz3fxFWrVp1Ljj(num3, index, _param5, _param2, pointList);
          z71mAcyQList.Add(new FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D(pointList.ToArray(), num5 > num6 ? _param4 : _param6));
          num3 = index;
          pointList = new List<Point>(32 /*0x20*/);
          if (!this.IsDigitalLine)
            pointList.Add(this.\u0023\u003Dzop6vn0GowyiR(point2, flag));
        }
      }
    }
    if (num1 > 0)
    {
      \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D kld48pAvUlrTzJ1tmfY1 = _param3.\u0023\u003Dz\u0024CeUvME\u003D(num1 - 1);
      \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D kld48pAvUlrTzJ1tmfY2 = _param5.\u0023\u003Dz\u0024CeUvME\u003D(num1 - 1);
      if (!kld48pAvUlrTzJ1tmfY1.\u0023\u003Dzu7q98_E\u003D().IsNaN() && !kld48pAvUlrTzJ1tmfY2.\u0023\u003Dzu7q98_E\u003D().IsNaN())
      {
        this.\u0023\u003DzVyzQAuU\u003D(num3, num1, _param3, _param2, pointList);
        this.\u0023\u003Dz3fxFWrVp1Ljj(num3, num1, _param5, _param2, pointList);
        IBrush2D xrgcdFbSdWgN9GcT8 = kld48pAvUlrTzJ1tmfY1.\u0023\u003Dzu7q98_E\u003D() > kld48pAvUlrTzJ1tmfY2.\u0023\u003Dzu7q98_E\u003D() ? _param4 : _param6;
        z71mAcyQList.Add(new FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D(pointList.ToArray(), xrgcdFbSdWgN9GcT8));
      }
    }
    return (IList<FastBandRenderableSeries.\u0023\u003Dz71mAcyQ\u003D>) z71mAcyQList;
  }

  private void \u0023\u003DzVyzQAuU\u003D(
    int _param1,
    int _param2,
    IPointSeries _param3,
    IRenderPassData _param4,
    List<Point> _param5)
  {
    for (int index = _param1; index < _param2; ++index)
      this.\u0023\u003DzpcjqGH38NbsS(_param3.\u0023\u003Dz\u0024CeUvME\u003D(index), _param5, false, _param4);
  }

  private void \u0023\u003Dz3fxFWrVp1Ljj(
    int _param1,
    int _param2,
    IPointSeries _param3,
    IRenderPassData _param4,
    List<Point> _param5)
  {
    for (int index = _param2 - 1; index >= _param1; --index)
      this.\u0023\u003DzpcjqGH38NbsS(_param3.\u0023\u003Dz\u0024CeUvME\u003D(index), _param5, true, _param4);
    _param5.Add(_param5[0]);
  }

  private void \u0023\u003DzpcjqGH38NbsS(
    \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D _param1,
    List<Point> _param2,
    bool _param3,
    IRenderPassData _param4)
  {
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = _param4.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = _param4.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    bool flag1 = _param4.\u0023\u003DzDoU1CJhSUWFV();
    double num1 = _param1.\u0023\u003Dzu7q98_E\u003D();
    float num2 = (float) xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(num1);
    double num3 = _param1.\u0023\u003Dz2_4KSTY\u003D();
    Point point1 = this.\u0023\u003Dzop6vn0GowyiR(new Point(xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(num3), (double) num2), flag1);
    if (this.IsDigitalLine && _param2.Count > 0)
    {
      Point point2 = _param2[_param2.Count - 1];
      bool flag2 = _param3 ^ flag1;
      _param2.Add(flag2 ? new Point(point2.X, point1.Y) : new Point(point1.X, point2.Y));
    }
    _param2.Add(point1);
  }

  private static int \u0023\u003DzaQ9tpxhMknmu(
    IPointSeries _param0,
    IPointSeries _param1,
    int _param2,
    int _param3)
  {
    while (_param2 < _param3 && (double.IsNaN(_param0.\u0023\u003Dz\u0024CeUvME\u003D(_param2).\u0023\u003Dzu7q98_E\u003D()) || double.IsNaN(_param1.\u0023\u003Dz\u0024CeUvME\u003D(_param2).\u0023\u003Dzu7q98_E\u003D())))
      ++_param2;
    return _param2;
  }

  protected override void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1,
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param2)
  {
    switch (_param2)
    {
      case null:
      case \u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH _:
        base.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(_param1, _param2);
        break;
      default:
        throw new InvalidOperationException($"{((object) this).GetType().Name} expects a DataSeries of type {typeof (\u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH)}. Please ensure the correct data has been bound to the Renderable Series");
    }
  }

  private double \u0023\u003Dz0D9nQXfw9PS9XdbmK6BuM0s6nQRIlp3hKg\u003D\u003D(int _param1)
  {
    return ((IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1]).ToDouble();
  }

  private double \u0023\u003Dz_Zd\u0024WqC1Iyyk5s9eL6trPhOVda_6sRI59g\u003D\u003D(int _param1)
  {
    return ((IComparable) ((\u0023\u003DzlvwXE9mBO1uItIXfGGLJcJ38syr\u0024xe9jQYRhESYENuoH) this.DataSeries).\u0023\u003DzxBmSWTMopfir()[_param1]).ToDouble();
  }

  protected struct \u0023\u003Dz71mAcyQ\u003D
  {
    
    private readonly Point[] \u0023\u003DzYw05nwk\u003D;
    
    private readonly IBrush2D \u0023\u003DzA2wbnrb_JRtA;

    public \u0023\u003Dz71mAcyQ\u003D(
      Point[] _param1,
      IBrush2D _param2)
      : this()
    {
      this.\u0023\u003DzYw05nwk\u003D = _param1;
      this.\u0023\u003DzA2wbnrb_JRtA = _param2;
    }

    public IBrush2D \u0023\u003DzRsgTSqM\u003D()
    {
      return this.\u0023\u003DzA2wbnrb_JRtA;
    }

    public Point[] \u0023\u003Dz9I1m\u0024aaHezaI() => this.\u0023\u003DzYw05nwk\u003D;
  }
}
