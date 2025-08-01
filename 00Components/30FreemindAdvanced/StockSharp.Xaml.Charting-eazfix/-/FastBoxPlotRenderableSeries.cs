// Decompiled with JetBrains decompiler
// Type: -.FastBoxPlotRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class FastBoxPlotRenderableSeries : 
  BaseRenderableSeries
{
  
  public static readonly DependencyProperty \u0023\u003DzVvc2lVdKTrj8 = DependencyProperty.Register(nameof (DataPointWidth), typeof (double), typeof (FastBoxPlotRenderableSeries), new PropertyMetadata((object) 0.2, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzoiPF1o\u0024bG_HJ = DependencyProperty.Register(nameof (BodyBrush), typeof (Brush), typeof (FastBoxPlotRenderableSeries), new PropertyMetadata((object) new SolidColorBrush(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0)), new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  private int \u0023\u003DzmZ8\u0024YArgrhlI;

  public FastBoxPlotRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastBoxPlotRenderableSeries);
  }

  public virtual double DataPointWidth
  {
    get
    {
      return (double) this.GetValue(FastBoxPlotRenderableSeries.\u0023\u003DzVvc2lVdKTrj8);
    }
    set
    {
      this.SetValue(FastBoxPlotRenderableSeries.\u0023\u003DzVvc2lVdKTrj8, (object) value);
    }
  }

  public Brush BodyBrush
  {
    get
    {
      return (Brush) this.GetValue(FastBoxPlotRenderableSeries.\u0023\u003DzoiPF1o\u0024bG_HJ);
    }
    set
    {
      this.SetValue(FastBoxPlotRenderableSeries.\u0023\u003DzoiPF1o\u0024bG_HJ, (object) value);
    }
  }

  protected override HitTestInfo \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    HitTestInfo zldchDrVsrVyHh6WyiGy1 = base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, _param2, false);
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

  public override SeriesInfo \u0023\u003DzZZbJdAS6fDJ\u0024(
    HitTestInfo _param1)
  {
    return (SeriesInfo) new \u0023\u003Dz3RRntx4pzkd854dIVpLK6S_EKy\u0024AtkpA9s\u0024N3N0\u003D((IRenderableSeries) this, _param1);
  }

  protected override double \u0023\u003DzcaynwI5AMDdY(
    HitTestInfo _param1)
  {
    return (double) this.\u0023\u003DzmZ8\u0024YArgrhlI;
  }

  protected override double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    HitTestInfo _param1)
  {
    return _param1.Minimum.ToDouble();
  }

  protected override double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    HitTestInfo _param1)
  {
    return _param1.Maximum.ToDouble();
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.BodyBrush != null;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    bool flag = _param2.\u0023\u003DzDoU1CJhSUWFV();
    double num1 = 0.0;
    this.\u0023\u003Dzz7UraMUVt1cf<\u0023\u003Dzs5PVP76t9JCEogjKMLIN9W\u0024EMs7d6n8LMrq66EFixVuy>("BoxDataSeries");
    \u0023\u003DzKsGTwu6B0A6eMUO4QALnGMy6VZRl6ViWfvaAKHBRvgtX rl6ViWfvaAkhbRvgtX = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI() as \u0023\u003DzKsGTwu6B0A6eMUO4QALnGMy6VZRl6ViWfvaAKHBRvgtX;
    int num2 = rl6ViWfvaAkhbRvgtX.\u0023\u003DzlpVGw6E\u003D();
    this.\u0023\u003DzmZ8\u0024YArgrhlI = this.\u0023\u003Dz6BuO4fnhj6SX(_param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), (IPointSeries) rl6ViWfvaAkhbRvgtX, this.DataPointWidth);
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null, PenLineCap.Round))
    {
      using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) (this.StrokeThickness + 1), this.Opacity, (double[]) null, PenLineCap.Round))
      {
        using (IBrush2D xrgcdFbSdWgN9GcT8 = _param1.\u0023\u003Dze8WyDhI\u003D(this.BodyBrush, this.Opacity, \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerPrimitive))
        {
          \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ iluL6N4L8CsqVgQq = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzYtr1U3NGZ0n8(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
          for (int index = 0; index < num2; ++index)
          {
            \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003Dzs5PVP76t9JCEogjKMLIN9W\u0024EMs7d6n8LMrq66EFixVuy> iwzGyvO4YaqDkpiI = rl6ViWfvaAkhbRvgtX[index] as \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<\u0023\u003Dzs5PVP76t9JCEogjKMLIN9W\u0024EMs7d6n8LMrq66EFixVuy>;
            double num3 = iwzGyvO4YaqDkpiI.X;
            \u0023\u003Dzs5PVP76t9JCEogjKMLIN9W\u0024EMs7d6n8LMrq66EFixVuy ems7d6n8Lmrq66EfixVuy = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
            double d1 = ems7d6n8Lmrq66EfixVuy.\u0023\u003DzKrTvxa8MJ66h();
            ems7d6n8Lmrq66EfixVuy = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
            double d2 = ems7d6n8Lmrq66EfixVuy.\u0023\u003Dzfb6u1svFQnJip22g8mPdcNo\u003D();
            ems7d6n8Lmrq66EfixVuy = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
            double d3 = ems7d6n8Lmrq66EfixVuy.Y;
            ems7d6n8Lmrq66EfixVuy = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
            double d4 = ems7d6n8Lmrq66EfixVuy.\u0023\u003DzXV41m4nLk9xqErg0fYHc8lw\u003D();
            ems7d6n8Lmrq66EfixVuy = iwzGyvO4YaqDkpiI.\u0023\u003DzPqsSI6C5MOOb();
            double d5 = ems7d6n8Lmrq66EfixVuy.\u0023\u003Dzg1M\u0024G_5sXlam();
            if (!double.IsNaN(d1) && !double.IsNaN(d2) && !double.IsNaN(d3) && !double.IsNaN(d4) && !double.IsNaN(d5))
            {
              double num4 = _param2.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(num3);
              double num5 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d5);
              double num6 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d4);
              double num7 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d3);
              double num8 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d2);
              double num9 = _param2.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(d1);
              double num10 = (double) this.\u0023\u003DzmZ8\u0024YArgrhlI * 0.5;
              double num11 = num4 - num10;
              double num12 = num4 + num10;
              iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num11, num5), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num12, num5), flag), rhwYsZxA33iRu6Id7J1);
              iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num4, num5), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num4, num6), flag), rhwYsZxA33iRu6Id7J1);
              iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num4, num8), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num4, num9), flag), rhwYsZxA33iRu6Id7J1);
              iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num11, num9), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num12, num9), flag), rhwYsZxA33iRu6Id7J1);
              iluL6N4L8CsqVgQq.\u0023\u003DzkpjYNfwbvIK8(this.\u0023\u003Dzop6vn0GowyiR(new Point(num11, num8), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num12, num6), flag), xrgcdFbSdWgN9GcT8, rhwYsZxA33iRu6Id7J1, num1);
              if (this.StrokeThickness > 0)
                iluL6N4L8CsqVgQq.\u0023\u003Dzk8_eoWQ\u003D(this.\u0023\u003Dzop6vn0GowyiR(new Point(num11 + 1.0, num7), flag), this.\u0023\u003Dzop6vn0GowyiR(new Point(num12, num7), flag), rhwYsZxA33iRu6Id7J2);
            }
          }
        }
      }
    }
  }
}
