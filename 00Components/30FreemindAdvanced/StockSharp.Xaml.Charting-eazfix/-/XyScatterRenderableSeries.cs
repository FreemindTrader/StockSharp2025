// Decompiled with JetBrains decompiler
// Type: -.XyScatterRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public sealed class XyScatterRenderableSeries : 
  BaseRenderableSeries
{
  
  public static readonly DependencyProperty \u0023\u003DzLAfsTxKYUsTns9NHKhkyIU0\u003D = DependencyProperty.Register(nameof (DoClusterResampling), typeof (bool), typeof (BaseRenderableSeries), new PropertyMetadata((object) false));

  public XyScatterRenderableSeries()
  {
    this.DefaultStyleKey = (object) typeof (XyScatterRenderableSeries);
    this.SetCurrentValue(BaseRenderableSeries.\u0023\u003DzZgWT7YttYHbwyP3zHCVW0zI\u003D, (object) \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.None);
  }

  public bool DoClusterResampling
  {
    get
    {
      return (bool) this.GetValue(XyScatterRenderableSeries.\u0023\u003DzLAfsTxKYUsTns9NHKhkyIU0\u003D);
    }
    set
    {
      this.SetValue(XyScatterRenderableSeries.\u0023\u003DzLAfsTxKYUsTns9NHKhkyIU0\u003D, (object) value);
    }
  }

  [SpecialName]
  public override bool \u0023\u003DztPaciKMZWysZOtqEskMFjk8\u003D() => true;

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    XyScatterRenderableSeries.SomeClass34343 zPKCmcad6Nxc5A8A = new XyScatterRenderableSeries.SomeClass34343();
    zPKCmcad6Nxc5A8A._variableSome3535 = this;
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    IRenderPassData mz4rNexJsSmCjpOm = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D();
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
    if (hw1ki13vvK4WxOgoljkHyInT == null)
      return;
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D paletteProvider = this.PaletteProvider;
    zPKCmcad6Nxc5A8A.\u0023\u003DzhFYkfW57ne2I = this.SeriesColor;
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX sgkqEcPv0Ah3hMaVex = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzmuYww2BKaMZ86Wjblw\u003D\u003D(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D(), hw1ki13vvK4WxOgoljkHyInT);
    if (paletteProvider != null)
    {
      zPKCmcad6Nxc5A8A.\u0023\u003Dz\u0024sDnaZw\u003D = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity);
      try
      {
        Func<double, double, \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J> func = new Func<double, double, \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J>(zPKCmcad6Nxc5A8A.\u0023\u003Dz9WeCEzhdx4LBOIEZGg\u003D\u003D);
        \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(sgkqEcPv0Ah3hMaVex, (Func<double, double, IPathColor>) func, ftrixUnpTllY1PkTyq, mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D());
      }
      finally
      {
        if (zPKCmcad6Nxc5A8A.\u0023\u003Dz\u0024sDnaZw\u003D != null)
          zPKCmcad6Nxc5A8A.\u0023\u003Dz\u0024sDnaZw\u003D.Dispose();
      }
    }
    else
      \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(sgkqEcPv0Ah3hMaVex, ftrixUnpTllY1PkTyq, mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D());
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    return base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, this.\u0023\u003Dz1runmyhnjbZYf6YRbnCukUGsf9D0YvUs2A\u003D\u003D(_param2), false);
  }

  private sealed class SomeClass34343
  {
    public XyScatterRenderableSeries _variableSome3535;
    public Color \u0023\u003DzhFYkfW57ne2I;
    public \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D \u0023\u003Dz\u0024sDnaZw\u003D;

    public \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dz9WeCEzhdx4LBOIEZGg\u003D\u003D(
      double _param1,
      double _param2)
    {
      return this.\u0023\u003Dz\u0024sDnaZw\u003D.\u0023\u003Dzc8S9rSE\u003D(this._variableSome3535.PaletteProvider.\u0023\u003DzP50Orng\u003D((IRenderableSeries) this._variableSome3535, _param1, _param2) ?? this.\u0023\u003DzhFYkfW57ne2I);
    }
  }
}
