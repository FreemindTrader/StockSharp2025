// Decompiled with JetBrains decompiler
// Type: -.FastErrorBarsRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class FastErrorBarsRenderableSeries : 
  BaseRenderableSeries
{
  
  private int \u0023\u003Dz3XkQnBHZj9ZA;
  
  public static readonly DependencyProperty \u0023\u003DzVvc2lVdKTrj8 = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (FastErrorBarsRenderableSeries), new PropertyMetadata((object) 0.2, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));

  public FastErrorBarsRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastErrorBarsRenderableSeries);
  }

  public virtual double DataPointWidth
  {
    get
    {
      return (double) this.GetValue(FastErrorBarsRenderableSeries.\u0023\u003DzVvc2lVdKTrj8);
    }
    set
    {
      this.SetValue(FastErrorBarsRenderableSeries.\u0023\u003DzVvc2lVdKTrj8, (object) value);
    }
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    return base.\u0023\u003DzWcglUt8A7ABL() && this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    this.\u0023\u003Dzz7UraMUVt1cf<\u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt>("ErrorDataSeries");
    this.\u0023\u003Dz3XkQnBHZj9ZA = this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI(), this.DataPointWidth);
    bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
    \u0023\u003DzJLUdDOVbHWuhCASQiXx2GONS5yjJOEFg518v349a03h5 joeFg518v349a03h5 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI() as \u0023\u003DzJLUdDOVbHWuhCASQiXx2GONS5yjJOEFg518v349a03h5;
    int num1 = joeFg518v349a03h5.\u0023\u003DzlpVGw6E\u003D();
    int num2 = this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), (IPointSeries) joeFg518v349a03h5, this.DataPointWidth);
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null, PenLineCap.Round))
    {
      \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
      for (int index = 0; index < num1; ++index)
      {
        \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt> iwzGyvO4YaqDkpiI = joeFg518v349a03h5[index] as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt>;
        double num3 = iwzGyvO4YaqDkpiI.X;
        double d1 = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb().\u0023\u003Dz\u0024ktIt4bbVFKI();
        double d2 = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb().\u0023\u003Dzasn0Azw0UwTS();
        if (!double.IsNaN(d1) && !double.IsNaN(d2))
        {
          float num4 = (float) _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(num3);
          float num5 = (float) _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d1);
          float num6 = (float) _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d2);
          int num7 = (int) ((double) num2 * 0.5);
          float num8 = num4 - (float) num7;
          float num9 = num4 + (float) num7;
          iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(num4, num5, flag), this.\u0023\u003Dzop6vn0GowyiR(num4, num6, flag), rhwYsZxA33iRu6Id7J);
          iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(num8, num5, flag), this.\u0023\u003Dzop6vn0GowyiR(num9, num5, flag), rhwYsZxA33iRu6Id7J);
          iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(num8, num6, flag), this.\u0023\u003Dzop6vn0GowyiR(num9, num6, flag), rhwYsZxA33iRu6Id7J);
        }
      }
    }
  }

  protected override HitTestInfo \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    HitTestInfo zldchDrVsrVyHh6WyiGy1 = base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, this.\u0023\u003Dz1runmyhnjbZYf6YRbnCukUGsf9D0YvUs2A\u003D\u003D(_param2), false);
    HitTestInfo zldchDrVsrVyHh6WyiGy2 = this.\u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(_param1, zldchDrVsrVyHh6WyiGy1, _param2);
    double num1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? Math.Abs(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS().Y - _param1.Y) : Math.Abs(zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxZfJER0dbHuS().X - _param1.X);
    if (!zldchDrVsrVyHh6WyiGy2.\u0023\u003DzxIOIxNIOU4djmPFSiA\u003D\u003D())
    {
      bool flag1 = num1 < this.\u0023\u003DzcaynwI5AMDdY(zldchDrVsrVyHh6WyiGy2) / this.DataPointWidth / 2.0;
      ref HitTestInfo local = ref zldchDrVsrVyHh6WyiGy2;
      bool flag2;
      zldchDrVsrVyHh6WyiGy2.\u0023\u003DzkNMVgQ88lfxP(flag2 = flag1);
      int num2 = flag2 ? 1 : 0;
      local.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(num2 != 0);
    }
    return zldchDrVsrVyHh6WyiGy2;
  }

  protected override double \u0023\u003DzcaynwI5AMDdY(
    HitTestInfo _param1)
  {
    return (double) this.\u0023\u003Dz3XkQnBHZj9ZA;
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    HitTestInfo _param1)
  {
    return _param1.\u0023\u003DzCH7BygPgTyIy().ToDouble();
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    HitTestInfo _param1)
  {
    return _param1.\u0023\u003Dz1D\u00248\u0024t39Cb2c().ToDouble();
  }
}
