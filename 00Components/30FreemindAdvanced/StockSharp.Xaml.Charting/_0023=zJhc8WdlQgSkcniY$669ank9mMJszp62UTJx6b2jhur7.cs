// Decompiled with JetBrains decompiler
// Type: #=zJhc8WdlQgSkcniY$669ank9mMJszp62UTJx6b2jhur7L
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L : 
  dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd
{
  
  public static readonly DependencyProperty \u0023\u003DzigzndanwPIFY = DependencyProperty.Register("", typeof (Style), typeof (\u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L), new PropertyMetadata(new PropertyChangedCallback(\u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L.\u0023\u003DzBLNrrTpkTSKCvEflkQ\u003D\u003D)));
  
  private EventHandler<EventArgs> \u0023\u003DzDpzMqzE\u003D;
  
  private bool \u0023\u003Dz5h4MErOTnNi_;

  public \u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L()
  {
    this.SetCurrentValue(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd.\u0023\u003DzG8iU0LHRhO7j, (object) true);
    this.SetCurrentValue(ChartModifierBase.\u0023\u003DzdfZ5r82v29C_, (object) dje_zKVLMQAQ8PVF9ES7S7RS764GN625CPCY4KFZRJDNDGVHGXXQ_ejd.MouseLeftButton);
  }

  public void \u0023\u003DzhBqSd5Scc0Hy(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzDpzMqzE\u003D;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzDpzMqzE\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public void \u0023\u003Dz_bNSX12Vpev3(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzDpzMqzE\u003D;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzDpzMqzE\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public Style SelectedSeriesStyle
  {
    get
    {
      return (Style) this.GetValue(\u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L.\u0023\u003DzigzndanwPIFY);
    }
    set
    {
      this.SetValue(\u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L.\u0023\u003DzigzndanwPIFY, (object) value);
    }
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003DzL8n9VSdSfzYa();
  }

  private void \u0023\u003DzL8n9VSdSfzYa()
  {
    if (this.ParentSurface == null)
      return;
    this.ParentSurface.get_SelectedRenderableSeries().\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003DzFEW\u0024G5HKuzKO));
  }

  protected override void \u0023\u003Dz\u0024523lOKnSPCb(
    IEnumerable<IRenderableSeries> _param1,
    IEnumerable<IRenderableSeries> _param2)
  {
    base.\u0023\u003Dz\u0024523lOKnSPCb(_param1, _param2);
    if (_param2 == null)
      return;
    this.\u0023\u003DzL8n9VSdSfzYa();
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X()
  {
  }

  public override void \u0023\u003DzY1JcdEJm3Ryc(
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt _param1)
  {
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    this.\u0023\u003Dz5h4MErOTnNi_ = _param1.\u0023\u003DzgMFxvpJd_50n() == \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D.Ctrl;
    this.\u0023\u003DzebZge1miA2O0(_param1);
  }

  protected override void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1)
  {
    this.\u0023\u003Dz_wtru8oSZoY9(_param1);
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
    bool flag = !this.ParentSurface.get_SelectedRenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>();
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D[] array = this.\u0023\u003DzzhlDItrRFv\u0024\u0024(_param1).ToArray<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>();
    if (\u0023\u003DzsIIzg9COgILMyUKVNisy8sT1ePq3.\u0023\u003DzDCv6G5Q\u003D<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(array))
    {
      IRenderableSeries renderableSeries = ((IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) array).First<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>().RenderableSeries;
      if (this.\u0023\u003Dz5h4MErOTnNi_)
        this.\u0023\u003Dz\u0024AkGiLXUHbZU(renderableSeries);
      else
        this.\u0023\u003Dzl6QWjU23t55A(renderableSeries);
      this.\u0023\u003DzAxm3XXgpqYph();
    }
    else
    {
      if (!flag)
        return;
      this.\u0023\u003DzS__l_ifl50tN();
      this.\u0023\u003DzAxm3XXgpqYph();
    }
  }

  protected virtual void \u0023\u003Dzl6QWjU23t55A(
    IRenderableSeries _param1)
  {
    int num = !_param1.get_IsSelected() ? 1 : (this.ParentSurface.get_SelectedRenderableSeries().Count > 1 ? 1 : 0);
    this.\u0023\u003DzS__l_ifl50tN();
    if (num == 0)
      return;
    this.\u0023\u003Dz\u0024AkGiLXUHbZU(_param1);
  }

  protected virtual void \u0023\u003Dz\u0024AkGiLXUHbZU(
    IRenderableSeries _param1)
  {
    _param1.set_IsSelected(!_param1.get_IsSelected());
    if (!_param1.get_IsSelected())
      return;
    this.\u0023\u003DzFEW\u0024G5HKuzKO(_param1);
  }

  protected virtual void \u0023\u003DzS__l_ifl50tN()
  {
    if (this.ParentSurface.get_SelectedRenderableSeries().Count == 0)
      return;
    for (int index = this.ParentSurface.get_SelectedRenderableSeries().Count - 1; index >= 0; --index)
      this.ParentSurface.get_SelectedRenderableSeries()[index].set_IsSelected(false);
  }

  protected virtual void \u0023\u003DzFEW\u0024G5HKuzKO(
    IRenderableSeries _param1)
  {
    if (this.SelectedSeriesStyle == null)
      return;
    bool flag = this.SelectedSeriesStyle.TargetType.IsAssignableFrom(_param1.GetType());
    if (!(_param1.get_SelectedSeriesStyle() == null & flag))
      return;
    _param1.set_SelectedSeriesStyle(this.SelectedSeriesStyle);
  }

  private void \u0023\u003DzAxm3XXgpqYph()
  {
    EventHandler<EventArgs> zDpzMqzE = this.\u0023\u003DzDpzMqzE\u003D;
    if (zDpzMqzE == null)
      return;
    zDpzMqzE((object) this, EventArgs.Empty);
  }

  private static void \u0023\u003DzBLNrrTpkTSKCvEflkQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ank9mMJszp62UTJx6b2jhur7L) _param0).\u0023\u003DzL8n9VSdSfzYa();
  }
}
