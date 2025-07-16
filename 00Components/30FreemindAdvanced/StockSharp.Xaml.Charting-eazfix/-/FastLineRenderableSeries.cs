// Decompiled with JetBrains decompiler
// Type: -.FastLineRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class FastLineRenderableSeries : 
  BaseRenderableSeries
{
  
  public static readonly DependencyProperty \u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D = DependencyProperty.Register(nameof (IsDigitalLine), typeof (bool), typeof (FastLineRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzTLzzX5iDcGuj = DependencyProperty.Register(nameof (StrokeDashArray), typeof (double[]), typeof (FastLineRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzGQjlrvOq17qko2MPPw\u003D\u003D = DependencyProperty.Register(nameof (OhlcDrawMode), typeof (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym), typeof (FastLineRenderableSeries), new PropertyMetadata((object) \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Close, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));

  public FastLineRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (FastLineRenderableSeries);
  }

  public bool IsDigitalLine
  {
    get
    {
      return (bool) this.GetValue(FastLineRenderableSeries.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D);
    }
    set
    {
      this.SetValue(FastLineRenderableSeries.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D, (object) value);
    }
  }

  [TypeConverter(typeof (\u0023\u003DzdJvCkWEpdQt1kv1Y55xjvd3\u0024HvNYL\u0024mKr9gng\u0024AK3Fkh))]
  public double[] StrokeDashArray
  {
    get
    {
      return (double[]) this.GetValue(FastLineRenderableSeries.\u0023\u003DzTLzzX5iDcGuj);
    }
    set
    {
      this.SetValue(FastLineRenderableSeries.\u0023\u003DzTLzzX5iDcGuj, (object) value);
    }
  }

  public \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym OhlcDrawMode
  {
    get
    {
      return (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym) this.GetValue(FastLineRenderableSeries.\u0023\u003DzGQjlrvOq17qko2MPPw\u003D\u003D);
    }
    set
    {
      this.SetValue(FastLineRenderableSeries.\u0023\u003DzGQjlrvOq17qko2MPPw\u003D\u003D, (object) value);
    }
  }

  [SpecialName]
  public override object \u0023\u003DzQavr9eonlwL7DeqLQA\u003D\u003D()
  {
    return (object) this.OhlcDrawMode;
  }

  protected override void \u0023\u003Dzd3otebQ_ivQa()
  {
  }

  protected override bool \u0023\u003DzWcglUt8A7ABL()
  {
    if (!base.\u0023\u003DzWcglUt8A7ABL())
      return false;
    return this.SeriesColor.A != (byte) 0 && this.StrokeThickness > 0 || this.PointMarker != null;
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz47Cmf38KMhH_(
    int _param1)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = base.\u0023\u003Dz47Cmf38KMhH_(_param1);
    if (zldchDrVsrVyHh6WyiGy.\u0023\u003DzRkghOq8y7ncj() != (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 1 && zldchDrVsrVyHh6WyiGy.\u0023\u003DzRkghOq8y7ncj() != (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 4)
      return zldchDrVsrVyHh6WyiGy;
    zldchDrVsrVyHh6WyiGy.\u0023\u003DzQ9xCEGz0Gl\u0024q((\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 0);
    switch (this.OhlcDrawMode)
    {
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Open:
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzBswzhzuQHrrX(zldchDrVsrVyHh6WyiGy.\u0023\u003DzlVz0JivzQhAY());
        break;
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.High:
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzBswzhzuQHrrX(zldchDrVsrVyHh6WyiGy.\u0023\u003Dzk8BrWRwbV\u0024Y\u0024());
        break;
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Low:
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzBswzhzuQHrrX(zldchDrVsrVyHh6WyiGy.\u0023\u003Dz89dSIjCLFKC0());
        break;
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VET677_jS5lgxbrMDtSh7WF5TJSM8_PDGQYU_nqym.Close:
        zldchDrVsrVyHh6WyiGy.\u0023\u003DzBswzhzuQHrrX(zldchDrVsrVyHh6WyiGy.\u0023\u003DzrRG8qdg_pzoL());
        break;
    }
    return zldchDrVsrVyHh6WyiGy;
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    FastLineRenderableSeries.\u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D qeYqJf714K2eMyR9I = new FastLineRenderableSeries.\u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D();
    qeYqJf714K2eMyR9I._variableSome3535 = this;
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    int num = this.PaletteProvider != null ? 1 : 0;
    qeYqJf714K2eMyR9I.\u0023\u003DzxS8dq0E\u003D = this.SeriesColor;
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX sgkqEcPv0Ah3hMaVex = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dz3GCmtrCAMr5X(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    if (num != 0)
    {
      qeYqJf714K2eMyR9I.\u0023\u003Dz\u0024sDnaZw\u003D = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, this.StrokeDashArray);
      try
      {
        Func<double, double, \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J> func = new Func<double, double, \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J>(qeYqJf714K2eMyR9I.\u0023\u003Dz9WeCEzhdx4LBOIEZGg\u003D\u003D);
        \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex, (Func<double, double, IPathColor>) func, ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, this.DrawNaNAs == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines);
      }
      finally
      {
        if (qeYqJf714K2eMyR9I.\u0023\u003Dz\u0024sDnaZw\u003D != null)
          qeYqJf714K2eMyR9I.\u0023\u003Dz\u0024sDnaZw\u003D.Dispose();
      }
    }
    else
    {
      using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, this.StrokeDashArray, PenLineCap.Round))
        \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex, rhwYsZxA33iRu6Id7J, ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), this.IsDigitalLine, this.DrawNaNAs == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines);
    }
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
    if (hw1ki13vvK4WxOgoljkHyInT == null)
      return;
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzmuYww2BKaMZ86Wjblw\u003D\u003D(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D(), hw1ki13vvK4WxOgoljkHyInT), ftrixUnpTllY1PkTyq, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D());
  }

  private sealed class \u0023\u003Dz_e22hQeYq_jf714K2eMYR9I\u003D
  {
    public FastLineRenderableSeries _variableSome3535;
    public Color \u0023\u003DzxS8dq0E\u003D;
    public \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D \u0023\u003Dz\u0024sDnaZw\u003D;

    public \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dz9WeCEzhdx4LBOIEZGg\u003D\u003D(
      double _param1,
      double _param2)
    {
      return this.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this._variableSome3535.PaletteProvider.\u0023\u003DzP50Orng\u003D((IRenderableSeries) this._variableSome3535, _param1, _param2) ?? this.\u0023\u003DzxS8dq0E\u003D);
    }
  }
}
