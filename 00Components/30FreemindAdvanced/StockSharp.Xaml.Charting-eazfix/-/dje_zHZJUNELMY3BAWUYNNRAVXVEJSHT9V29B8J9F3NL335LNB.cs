// Decompiled with JetBrains decompiler
// Type: -.dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

#nullable disable
namespace \u002D;

internal sealed class dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd : 
  BaseMountainRenderableSeries,
  IDrawable,
  IXmlSerializable,
  \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D,
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXvZYOg6JAbLCIgQvZp9odw6FSOKg1daH3vPLNHtT2ZG4iQ\u003D\u003D,
  IRenderableSeries,
  \u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D
{
  
  public static readonly DependencyProperty \u0023\u003Dz2Rta\u0024oTnlQkx = DependencyProperty.Register(nameof (StackedGroupId), typeof (string), typeof (dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd), new PropertyMetadata((object) "DefaultStackedGroupId", new PropertyChangedCallback(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd.\u0023\u003DzyGQyqAS53k26)));
  
  public static readonly DependencyProperty \u0023\u003Dzj\u0024ZDuobq5\u0024GmruUxggpnzio\u003D = DependencyProperty.Register(nameof (IsOneHundredPercent), typeof (bool), typeof (dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd), new PropertyMetadata((object) false));

  public dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd);
  }

  [SpecialName]
  public \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKg1uYp9C5FyfAbaz1_Vr5UELTst58YqUlllvt2EY65UXJw\u003D\u003D \u0023\u003Dz9NrWMa9\u00243uT8()
  {
    return ((SciChartSurface) this.\u0023\u003DzoXzc48\u0024TAMxP())?.\u0023\u003DzZ5VDq\u0024Vzik5LlJ4Fd5jKuBg\u003D();
  }

  public string StackedGroupId
  {
    get
    {
      return (string) this.GetValue(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd.\u0023\u003Dz2Rta\u0024oTnlQkx);
    }
    set
    {
      this.SetValue(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd.\u0023\u003Dz2Rta\u0024oTnlQkx, (object) value);
    }
  }

  public bool IsOneHundredPercent
  {
    get
    {
      return (bool) this.GetValue(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd.\u0023\u003Dzj\u0024ZDuobq5\u0024GmruUxggpnzio\u003D);
    }
    set
    {
      this.SetValue(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd.\u0023\u003Dzj\u0024ZDuobq5\u0024GmruUxggpnzio\u003D, (object) value);
      this.\u0023\u003DzoXzc48\u0024TAMxP()?.InvalidateElement();
    }
  }

  public override IRange \u0023\u003DzxNQHuqrEvxH2(
    IRange _param1,
    bool _param2)
  {
    IndexRange  g8Oq2rGx6KyfAreq = _param1 != null ? this.DataSeries.GetIndicesRange(_param1) : throw new ArgumentNullException("xRange");
    return (IRange) this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzzMId\u0024f67Wftb((\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) this, g8Oq2rGx6KyfAreq);
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzNi0XCnZpx1ge(_param1);
  }

  void \u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D.\u0023\u003Dz\u0024a0fIHoQZ1sLP6Qvlz\u0024hlVI4rowBaJk\u0024GR\u0024PuibSCVV2Hj\u0024X0eFVzzfZX_qZB_h5wMbEtWBSbN\u0024XD38SCgpGvzGNi0mk(
    IRenderContext2D _param1,
    bool _param2)
  {
    IRenderPassData mz4rNexJsSmCjpOm = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D();
    double num1 = this.\u0023\u003DzNfVFwxaLW3jC(this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D());
    using (IBrush2D xrgcdFbSdWgN9GcT8 = _param1.\u0023\u003Dze8WyDhI\u003D(this.AreaBrush, this.Opacity, \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerPrimitive))
    {
      using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J1 = _param1.\u0023\u003DzL3In9ls\u003D(this.SeriesColor, this.AntiAliasing, (float) this.StrokeThickness, this.Opacity, (double[]) null, PenLineCap.Round))
      {
        \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq1;
        \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C p09swszfkFaReRy0aAtDn3C = this.\u0023\u003Dz1nUiMgOBlgNRWzTAmQ\u003D\u003D(_param2, out ftrixUnpTllY1PkTyq1);
        \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX sgkqEcPv0Ah3hMaVex = \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003Dz3GCmtrCAMr5X(_param1, mz4rNexJsSmCjpOm);
        \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzUVX2hzSJiUvkgg4NgQ6FCHrRuz_S(_param1, mz4rNexJsSmCjpOm, num1), xrgcdFbSdWgN9GcT8, (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) p09swszfkFaReRy0aAtDn3C, mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D(), false, false);
        \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J2 = rhwYsZxA33iRu6Id7J1;
        \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq2 = ftrixUnpTllY1PkTyq1;
        \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
        \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
        int num2 = this.IsDigitalLine ? 1 : 0;
        \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqQMMUpdrYizZh1\u0024TejAsPVdnlvpPU6Az\u0024I4\u003D.\u0023\u003DzftEp\u0024x4VdHnj(sgkqEcPv0Ah3hMaVex, rhwYsZxA33iRu6Id7J2, ftrixUnpTllY1PkTyq2, xkzemsMs5tGkouk5w1, xkzemsMs5tGkouk5w2, num2 != 0, false);
        \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
        if (hw1ki13vvK4WxOgoljkHyInT == null)
          return;
        \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA\u0024cZLs_od3qVmWwgIlKdnZ_XFod55ir8\u0024xo\u003D.\u0023\u003DzjBmQkSQ797ct(\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyROb9BW0uTL6\u0024tj_pT60sHZCBBCp5MfS643cl2Oc.\u0023\u003DzmuYww2BKaMZ86Wjblw\u003D\u003D(_param1, mz4rNexJsSmCjpOm, hw1ki13vvK4WxOgoljkHyInT), ftrixUnpTllY1PkTyq1, mz4rNexJsSmCjpOm.\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D(), mz4rNexJsSmCjpOm.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D());
      }
    }
  }

  private \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C \u0023\u003Dz1nUiMgOBlgNRWzTAmQ\u003D\u003D(
    bool _param1,
    out \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2)
  {
    IRenderPassData mz4rNexJsSmCjpOm = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D();
    int num1 = mz4rNexJsSmCjpOm.\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzlpVGw6E\u003D();
    int num2 = this.IsDigitalLine ? num1 * 2 - 1 : num1;
    int num3 = _param1 ? num1 * 2 - 1 : num1;
    int num4 = num2 + num3;
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C p09swszfkFaReRy0aAtDn3C1 = new \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C(num4);
    p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzwQnyySN6xaVC().\u0023\u003Dze68j\u0024gs\u003D(num4);
    p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb().\u0023\u003Dze68j\u0024gs\u003D(num4);
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C p09swszfkFaReRy0aAtDn3C2 = new \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P09swszfkFaReRy0a_AtDN3C(num2);
    p09swszfkFaReRy0aAtDn3C2.\u0023\u003DzwQnyySN6xaVC().\u0023\u003Dze68j\u0024gs\u003D(num2);
    p09swszfkFaReRy0aAtDn3C2.\u0023\u003DzPqsSI6C5MOOb().\u0023\u003Dze68j\u0024gs\u003D(num2);
    int index1 = 0;
    int index2 = num4 - 1;
    for (int index3 = 0; index3 < num1; ++index3)
    {
      \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKld48pAvULrTzJ1tmfY\u003D kld48pAvUlrTzJ1tmfY = mz4rNexJsSmCjpOm.\u0023\u003DzSKfyjpipx8dI().\u0023\u003Dz\u0024CeUvME\u003D(index3);
      Tuple<double, double> tuple = this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzeKx7SKdwYOP2((\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) this, index3, true);
      if (this.IsDigitalLine && index3 != 0)
      {
        p09swszfkFaReRy0aAtDn3C2.\u0023\u003DzwQnyySN6xaVC()[index1] = p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzwQnyySN6xaVC()[index1] = kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D();
        p09swszfkFaReRy0aAtDn3C2.\u0023\u003DzPqsSI6C5MOOb()[index1] = p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb()[index1] = p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb()[index1 - 1];
        ++index1;
      }
      p09swszfkFaReRy0aAtDn3C2.\u0023\u003DzwQnyySN6xaVC()[index1] = p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzwQnyySN6xaVC()[index1] = kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D();
      p09swszfkFaReRy0aAtDn3C2.\u0023\u003DzPqsSI6C5MOOb()[index1] = p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb()[index1] = tuple.Item1;
      ++index1;
      if (_param1 && index3 != 0)
      {
        p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzwQnyySN6xaVC()[index2] = kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D();
        p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb()[index2] = p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb()[index2 + 1];
        --index2;
      }
      p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzwQnyySN6xaVC()[index2] = kld48pAvUlrTzJ1tmfY.\u0023\u003Dz2_4KSTY\u003D();
      p09swszfkFaReRy0aAtDn3C1.\u0023\u003DzPqsSI6C5MOOb()[index2] = tuple.Item2;
      --index2;
    }
    _param2 = (\u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ) p09swszfkFaReRy0aAtDn3C2;
    return p09swszfkFaReRy0aAtDn3C1;
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dzr7PRxQcLL3EF(
    Point _param1,
    double _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3,
    bool _param4)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy1 = \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    if (this.IsVisible)
    {
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy2 = base.\u0023\u003Dzr7PRxQcLL3EF(_param1, _param2, _param3, _param4);
      zldchDrVsrVyHh6WyiGy1 = this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003Dznnv4eJBaYYey(_param1, zldchDrVsrVyHh6WyiGy2, _param2, (\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) this);
    }
    return zldchDrVsrVyHh6WyiGy1;
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3)
  {
    if (!_param2.\u0023\u003DzMeGSfVE\u003D())
    {
      int num1 = _param2.\u0023\u003DzSkvCFWUKQ7Fw();
      int num2 = _param2.\u0023\u003DzSkvCFWUKQ7Fw() + 1;
      if (num2 >= 0 && num2 < this.DataSeries.get_Count())
      {
        Tuple<double, double> tuple1 = this.\u0023\u003Dz0iTacSsYe_3qOn01Ow\u003D\u003D(num1, new Func<int, double>(this.\u0023\u003Dz1jiRuITB82XgYi4pJKvFW3zwohS6D_e6SQ\u003D\u003D));
        Tuple<double, double> tuple2 = this.\u0023\u003Dz0iTacSsYe_3qOn01Ow\u003D\u003D(num1, new Func<int, double>(this.\u0023\u003DzA_YtdYT9t_kjQZd6sJpjHJ0AAq9cdpYNMQ\u003D\u003D));
        _param2 = this.\u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(_param1, _param2, _param3, tuple2, tuple1);
      }
    }
    return _param2;
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
    if (!flag)
      flag = this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzRf_Fn6mPWZva(_param1, _param2, _param3, tuple, (\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) this);
    return flag;
  }

  private static void \u0023\u003DzyGQyqAS53k26(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NLYZEGBR7SRGPPHQJR5J8932SBPK8SBYLR2E2AEEWA_ejd sbylR2E2AeewaEjd) || sbylR2E2AeewaEjd.\u0023\u003Dz9NrWMa9\u00243uT8() == null)
      return;
    sbylR2E2AeewaEjd.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzZcYnC1z3z2MT((\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) sbylR2E2AeewaEjd, (string) _param1.OldValue, (string) _param1.NewValue);
  }

  Style IRenderableSeries.\u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV1DuKLm1V1cPvuYOQQ3vfm3CE6f2xA\u003D()
  {
    return this.Style;
  }

  void IRenderableSeries.\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEewBN21r93mO2zsz2dRPeFYWJbEJRkUuJj8DgmKkp4Eq5v4nmubs\u003D(
    Style _param1)
  {
    this.Style = _param1;
  }

  object IRenderableSeries.\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEewBN21r93mO2zsz2dRPeFYUUfzfIMBKYp9sEsF_gP\u0024Pd_00evAM\u003D()
  {
    return this.DataContext;
  }

  void IRenderableSeries.\u0023\u003DzupHrUO0UFO07vWyNRguf_0VZ\u0024qSpKkKiNQBNd6W0uxIwj8NLZZkxSgEI5esBhFlrGGFudoE\u003D(
    object _param1)
  {
    this.DataContext = _param1;
  }

  double IDrawable.\u0023\u003DzEa5ACpOap4rFIaHj5p9yfH70ARbSZe0FxQ0q\u00240QfMpnPN_04zQ\u003D\u003D()
  {
    return this.Width;
  }

  void IDrawable.\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI4gQW2p5XnENj22E0ug7VJ0RyC3hMw\u003D\u003D(
    double _param1)
  {
    this.Width = _param1;
  }

  double IDrawable.\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ZE9YOd5sMhl\u0024Z\u0024xSADAZlqXzWzlvA\u003D\u003D()
  {
    return this.Height;
  }

  void IDrawable.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntaAE_X2h6PHbsMnRBK9cYE8yLrOBvg\u003D\u003D(
    double _param1)
  {
    this.Height = _param1;
  }

  private double \u0023\u003Dz1jiRuITB82XgYi4pJKvFW3zwohS6D_e6SQ\u003D\u003D(int _param1)
  {
    return ((IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1]).ToDouble();
  }

  private double \u0023\u003DzA_YtdYT9t_kjQZd6sJpjHJ0AAq9cdpYNMQ\u003D\u003D(int _param1)
  {
    return this.\u0023\u003Dz9NrWMa9\u00243uT8().\u0023\u003DzeKx7SKdwYOP2((\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) this, _param1, false).Item1;
  }
}
