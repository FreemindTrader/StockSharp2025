// Decompiled with JetBrains decompiler
// Type: -.FastMountainRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal sealed class FastMountainRenderableSeries : 
  BaseMountainRenderableSeries
{
  public FastMountainRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastMountainRenderableSeries);
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.AreaBrush != null;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    FastMountainRenderableSeries.SomeClass1234 _someMemebers1234 = new FastMountainRenderableSeries.SomeClass1234();
    _someMemebers1234.\u0023\u003DzRRvwDu67s9Rm = this;
    double num1 = this.\u0023\u003DzNfVFwxaLW3jC(_param2);
    float num2 = (float) this.\u0023\u003DzySDi0_ve2vLaE3cXlA\u003D\u003D();
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    _someMemebers1234.\u0023\u003DzxS8dq0E\u003D = this.SeriesColor;
    _someMemebers1234.\u0023\u003DzWQetKrw\u003D = _param1.\u0023\u003Dze8WyDhI\u003D(this.AreaBrush, this.Opacity, \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerPrimitive);
    try
    {
      using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null, PenLineCap.Round))
      {
        \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX sgkqEcPv0Ah3hMaVex1 = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dz3GCmtrCAMr5X(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
        \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX sgkqEcPv0Ah3hMaVex2 = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dztos6AjFhGj6qHx8r6Ds0ao0\u003D(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D(), num2, num1);
        if (this.PaletteProvider != null)
        {
          _someMemebers1234.\u0023\u003Dz\u0024sDnaZw\u003D = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity);
          try
          {
            Func<double, double, \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J> func1 = new Func<double, double, \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J>(_someMemebers1234.\u0023\u003Dz9WeCEzhdx4LBOIEZGg\u003D\u003D);
            Func<double, double, IBrush2D> func2 = new Func<double, double, IBrush2D>(_someMemebers1234.\u0023\u003DzFAoPfkrtOCx4PY1ldA\u003D\u003D);
            \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex2, (Func<double, double, IPathColor>) func2, ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, false);
            \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex1, (Func<double, double, IPathColor>) func1, ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, this.DrawNaNAs == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines);
          }
          finally
          {
            if (_someMemebers1234.\u0023\u003Dz\u0024sDnaZw\u003D != null)
              _someMemebers1234.\u0023\u003Dz\u0024sDnaZw\u003D.Dispose();
          }
        }
        else
        {
          \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex2, _someMemebers1234.\u0023\u003DzWQetKrw\u003D, ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, false);
          \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex1, rhwYsZxA33iRu6Id7J, ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, false);
        }
        \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
        if (hw1ki13vvK4WxOgoljkHyInT == null)
          return;
        \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzmuYww2BKaMZ86Wjblw\u003D\u003D(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D(), hw1ki13vvK4WxOgoljkHyInT), ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D());
      }
    }
    finally
    {
      if (_someMemebers1234.\u0023\u003DzWQetKrw\u003D != null)
        _someMemebers1234.\u0023\u003DzWQetKrw\u003D.Dispose();
    }
  }

  protected override bool \u0023\u003DzRf_Fn6mPWZva(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3,
    Point _param4,
    Point _param5)
  {
    bool flag = base.\u0023\u003DzRf_Fn6mPWZva(_param1, _param2, _param3, _param4, _param5);
    Tuple<IComparable, IComparable> tuple = this.\u0023\u003Dzs0Y0\u0024lrpmkkQ(_param1);
    if (!flag && tuple.Item1.ToDouble() >= _param4.X && tuple.Item1.ToDouble() <= _param5.X && !double.IsNaN(_param2.\u0023\u003Dzd9IAScWutAfJ().ToDouble()))
    {
      Point point;
      \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzlI3LM2ikHAvB(new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D(_param4, _param5), new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzRKlLwRo\u003D(new Point(tuple.Item1.ToDouble(), this.ZeroLineY), new Point(tuple.Item1.ToDouble(), Math.Max(_param4.Y, _param5.Y))), out point);
      flag = tuple.Item2.ToDouble().CompareTo(this.ZeroLineY) >= 0 && tuple.Item2.ToDouble().CompareTo(point.Y) <= 0;
    }
    return flag;
  }

  private sealed class SomeClass1234
  {
    public FastMountainRenderableSeries \u0023\u003DzRRvwDu67s9Rm;
    public Color \u0023\u003DzxS8dq0E\u003D;
    public IBrush2D \u0023\u003DzWQetKrw\u003D;
    public \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D \u0023\u003Dz\u0024sDnaZw\u003D;

    internal \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dz9WeCEzhdx4LBOIEZGg\u003D\u003D(
      double _param1,
      double _param2)
    {
      return this.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this.\u0023\u003DzRRvwDu67s9Rm.PaletteProvider.\u0023\u003DzP50Orng\u003D((\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) this.\u0023\u003DzRRvwDu67s9Rm, _param1, _param2) ?? this.\u0023\u003DzxS8dq0E\u003D);
    }

    internal IBrush2D \u0023\u003DzFAoPfkrtOCx4PY1ldA\u003D\u003D(
      double _param1,
      double _param2)
    {
      Color? nullable = this.\u0023\u003DzRRvwDu67s9Rm.PaletteProvider.\u0023\u003DzP50Orng\u003D((\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) this.\u0023\u003DzRRvwDu67s9Rm, _param1, _param2);
      return !nullable.HasValue ? this.\u0023\u003DzWQetKrw\u003D : this.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003DzNryPIU0\u003D(nullable.Value);
    }
  }
}
