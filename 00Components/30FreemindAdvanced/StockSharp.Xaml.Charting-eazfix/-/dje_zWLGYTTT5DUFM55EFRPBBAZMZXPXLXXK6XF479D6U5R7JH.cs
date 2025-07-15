// Decompiled with JetBrains decompiler
// Type: -.dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Shapes;

#nullable disable
namespace \u002D;

internal sealed class dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd : 
  dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd
{
  
  public static readonly DependencyProperty \u0023\u003DzNRygy3vTBpTh = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzuExxmF6hE_Ve = DependencyProperty.Register(nameof (DrawVerticalLine), typeof (bool), typeof (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd), new PropertyMetadata((object) true));
  
  private bool \u0023\u003Dzlho_u4MprGUCoVpNdw\u003D\u003D;
  
  private Line \u0023\u003DzSh5iKSo\u003D;
  
  private \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D \u0023\u003DzGgdpvaCO8rGBSnoR3l3LJbo\u003D;

  public dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd);
    this.SetCurrentValue(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd.\u0023\u003DzE7h5hUE7Vu4g, (object) new \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D());
  }

  public static bool GetIncludeSeries(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.\u0023\u003DzNRygy3vTBpTh);
  }

  public static void SetIncludeSeries(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.\u0023\u003DzNRygy3vTBpTh, (object) _param1);
  }

  public bool DrawVerticalLine
  {
    get
    {
      return (bool) this.GetValue(dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.\u0023\u003DzuExxmF6hE_Ve);
    }
    set
    {
      this.SetValue(dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.\u0023\u003DzuExxmF6hE_Ve, (object) value);
    }
  }

  protected override void \u0023\u003Dz_HFvQ2jjCDBP(
    IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param1,
    ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param2)
  {
    _param1.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(new Action<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(new dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.\u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D()
    {
      \u0023\u003Dz1H5OM_lRQN1_Ej_iLQ\u003D\u003D = _param2
    }.\u0023\u003Dzk4y25HcID\u0024p8FcTs_CL_Zpw\u003D));
  }

  protected override FrameworkElement \u0023\u003DzoHJDgDlSejs6FIKEDvqYw6U\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D k3zbmiw1OaRAdtq7psDwa = _param1 as \u0023\u003DzEJoJjwSelM_K3zbmiw1OA_rAdtq\u00247psDWA\u003D\u003D;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd renderableSeries = _param1.RenderableSeries as dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd;
    return k3zbmiw1OaRAdtq7psDwa == null || renderableSeries == null || !k3zbmiw1OaRAdtq7psDwa.IsFirstSeries ? _param1.RenderableSeries.\u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D() : renderableSeries.\u0023\u003DzqGcIkFXNQBdDMR9NQxO5bUc\u003D();
  }

  protected override void \u0023\u003Dz\u0024523lOKnSPCb(
    IEnumerable<IRenderableSeries> _param1,
    IEnumerable<IRenderableSeries> _param2)
  {
    base.\u0023\u003Dz\u0024523lOKnSPCb(_param1, _param2);
    this.\u0023\u003Dzd19tXEE\u003D();
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X()
  {
    base.\u0023\u003DzleRWWIS9Sb_X();
    this.\u0023\u003Dzd19tXEE\u003D();
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
    this.\u0023\u003Dzlho_u4MprGUCoVpNdw\u003D\u003D = false;
    this.\u0023\u003Dzd19tXEE\u003D();
    base.\u0023\u003Dz_wtru8oSZoY9(_param1);
  }

  private void \u0023\u003Dzd19tXEE\u003D()
  {
    if (this.ModifierSurface == null || this.\u0023\u003DzSh5iKSo\u003D == null)
      return;
    this.ModifierSurface.Children().Remove((UIElement) this.\u0023\u003DzSh5iKSo\u003D);
  }

  protected override void \u0023\u003DzBmyEynplQR0mx3c8kg\u003D\u003D(Point _param1)
  {
    base.\u0023\u003DzBmyEynplQR0mx3c8kg\u003D\u003D(_param1);
    if (!this.\u0023\u003Dzt9d2ExuvJfVV(_param1))
      return;
    if (this.ShowAxisLabels && !this.\u0023\u003Dzlho_u4MprGUCoVpNdw\u003D\u003D)
      this.\u0023\u003DzqB2OzvmQT2Y9(_param1);
    this.\u0023\u003Dzg0TLzyH7psRX(_param1);
  }

  private void \u0023\u003Dzg0TLzyH7psRX(Point _param1)
  {
    if (!this.DrawVerticalLine || this.\u0023\u003Dzlho_u4MprGUCoVpNdw\u003D\u003D)
      return;
    bool flag = this.XAxis != null && !this.XAxis.IsHorizontalAxis;
    this.\u0023\u003Dzlho_u4MprGUCoVpNdw\u003D\u003D = this.\u0023\u003Dz7ftWcP9VIwGB(_param1, flag);
  }

  private bool \u0023\u003Dz7ftWcP9VIwGB(Point _param1, bool _param2)
  {
    this.\u0023\u003DzGgdpvaCO8rGBSnoR3l3LJbo\u003D = this.\u0023\u003DzuC1vLp\u0024GySe3Lph6qNQLDPI\u003D();
    this.\u0023\u003DzSh5iKSo\u003D = this.\u0023\u003DzGgdpvaCO8rGBSnoR3l3LJbo\u003D.\u0023\u003Dz7ftWcP9VIwGB(_param1, _param2);
    int num = this.\u0023\u003DzSh5iKSo\u003D != null ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.\u0023\u003DzSh5iKSo\u003D.Style = this.LineOverlayStyle;
    this.\u0023\u003DzSh5iKSo\u003D.IsHitTestVisible = false;
    this.ModifierSurface.Children().Add((UIElement) this.\u0023\u003DzSh5iKSo\u003D);
    return num != 0;
  }

  private \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D \u0023\u003DzuC1vLp\u0024GySe3Lph6qNQLDPI\u003D()
  {
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D jyxtBaVakvxTuthBw;
    if (this.XAxis != null && this.XAxis.get_IsPolarAxis())
    {
      if (!(this.\u0023\u003DzGgdpvaCO8rGBSnoR3l3LJbo\u003D is \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw0apI7VsQ_xj6G0khrSKZaBu8dZ57eHRCM5HUPjp bu8dZ57eHrcM5HuPjp))
        bu8dZ57eHrcM5HuPjp = new \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw0apI7VsQ_xj6G0khrSKZaBu8dZ57eHRCM5HUPjp((IChartModifier) this);
      jyxtBaVakvxTuthBw = (\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D) bu8dZ57eHrcM5HuPjp;
    }
    else
    {
      if (!(this.\u0023\u003DzGgdpvaCO8rGBSnoR3l3LJbo\u003D is \u0023\u003DzgeFvyoahWukw3bL8yZfVYtqjmhok9yjd8mH5m4kxECgD_8hOTrQuVzBMlu2MMYB2IQ\u003D\u003D quVzBmlu2MmyB2Iq))
        quVzBmlu2MmyB2Iq = new \u0023\u003DzgeFvyoahWukw3bL8yZfVYtqjmhok9yjd8mH5m4kxECgD_8hOTrQuVzBMlu2MMYB2IQ\u003D\u003D((IChartModifier) this);
      jyxtBaVakvxTuthBw = (\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D) quVzBmlu2MmyB2Iq;
    }
    return jyxtBaVakvxTuthBw;
  }

  private sealed class \u0023\u003Dz4hWzOvDOp2Sz_a2WchXH2wc\u003D
  {
    public ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003Dz1H5OM_lRQN1_Ej_iLQ\u003D\u003D;

    internal void \u0023\u003Dzk4y25HcID\u0024p8FcTs_CL_Zpw\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      if (!_param1.RenderableSeries.\u0023\u003DzVxrZQ3k9ZBGJ((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 0))
        return;
      this.\u0023\u003Dz1H5OM_lRQN1_Ej_iLQ\u003D\u003D.Add(_param1);
    }
  }
}
