// Decompiled with JetBrains decompiler
// Type: -.dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal abstract class dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd : 
  dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal static readonly DependencyProperty \u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D = DependencyProperty.RegisterAttached(XXX.SSS(-539429214), typeof (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd), typeof (dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private List<FrameworkElement> \u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D = new List<FrameworkElement>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private List<FrameworkElement> \u0023\u003Dzd2PtoXT5k7_W = new List<FrameworkElement>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzWlihjOZFzDLV;

  protected dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd()
  {
    this.\u0023\u003DzIxFmGbTNpwO0 = new DelayActionHelper()
    {
      Interval = this.HoverDelay
    };
  }

  internal static Control \u0023\u003DzZGWrXySsGWItNMGOow\u003D\u003D(DependencyObject _param0)
  {
    return (Control) _param0.GetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D);
  }

  internal static void \u0023\u003Dz4xEz6kzuJnZA5QZEVA\u003D\u003D(
    DependencyObject _param0,
    Control _param1)
  {
    _param0.SetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D, (object) _param1);
  }

  public double HoverDelay
  {
    get
    {
      return (double) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzcSEHQMyzEgRH);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCFGW3UZZ44XSUJJQGVNND2_ejd.\u0023\u003DzcSEHQMyzEgRH, (object) value);
    }
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003DzleRWWIS9Sb_X();
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    base.\u0023\u003DzCM2UQyuakisf();
    base.OnDetached();
    this.\u0023\u003DzCTuuQt0\u003D(false);
  }

  private void \u0023\u003DzCTuuQt0\u003D(bool _param1)
  {
    if (this.ModifierSurface != null)
    {
      foreach (FrameworkElement frameworkElement in this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D)
      {
        this.\u0023\u003DzpWd3bhexNgJne_G3pk5QQoE\u003D(frameworkElement);
        if (_param1)
          this.\u0023\u003DzsTXk6VTlNzAx(frameworkElement);
      }
    }
    this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D.Clear();
  }

  protected virtual void \u0023\u003DzpWd3bhexNgJne_G3pk5QQoE\u003D(FrameworkElement _param1)
  {
    this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) _param1);
    if (this.\u0023\u003Dz1lxFA46cCdxI())
      return;
    _param1.MouseMove -= new MouseEventHandler(this.\u0023\u003Dzh9KvuS_jLUYL93sI\u0024BP6nio\u003D);
    _param1.MouseLeave -= new MouseEventHandler(this.\u0023\u003DzJNHp\u0024bXt4L5qmQ5ozPuXdks\u003D);
    _param1.MouseLeftButtonDown -= new MouseButtonEventHandler(this.\u0023\u003DzJNHp\u0024bXt4L5qmQ5ozPuXdks\u003D);
  }

  private void \u0023\u003DzsTXk6VTlNzAx(FrameworkElement _param1)
  {
    FrameworkElement frameworkElement = (FrameworkElement) _param1.GetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D);
    if (frameworkElement == null)
      return;
    this.\u0023\u003Dzd2PtoXT5k7_W.Remove(frameworkElement);
    if (!this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) frameworkElement))
      return;
    this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) frameworkElement);
  }

  protected override bool \u0023\u003DzD5SquRN7M_9c(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    bool flag = !_param1.\u0023\u003DzMeGSfVE\u003D() && _param1.\u0023\u003DzxIOIxNIOU4djmPFSiA\u003D\u003D() && _param1.\u0023\u003DzoDOpfzdgHalT() && _param1.\u0023\u003DzxZfJER0dbHuS().X.\u0023\u003DzutrFxOU\u003D() && _param1.\u0023\u003DzxZfJER0dbHuS().Y.\u0023\u003DzutrFxOU\u003D();
    if (_param1.\u0023\u003DzRkghOq8y7ncj() == (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSW1J0DiEBQ7p1fR0bYE\u003D) 2)
      flag &= _param1.\u0023\u003DzsTZhf2NJangnoun2zQ\u003D\u003D().Y.\u0023\u003DzutrFxOU\u003D();
    return flag;
  }

  protected override void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1)
  {
    this.\u0023\u003Dz_wtru8oSZoY9(_param1);
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
    if (!this.IsAttached || !this.IsEnabled || this.ParentSurface == null)
      return;
    if (this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseHover)
      this.\u0023\u003DzpPviEVAcoiYQ();
    this.\u0023\u003DzCTuuQt0\u003D(false);
    ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> renderableSeries = this.ParentSurface.get_RenderableSeries();
    ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> source = new ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>();
    if (renderableSeries != null)
    {
      dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D uuovZyvmyeAqC2gog8 = new dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D();
      this.\u0023\u003Dz_HFvQ2jjCDBP(this.\u0023\u003DzzhlDItrRFv\u0024\u0024(_param1), source);
      uuovZyvmyeAqC2gog8.\u0023\u003Dz4bf8Oyc\u003D = this.XAxis;
      if (uuovZyvmyeAqC2gog8.\u0023\u003Dz4bf8Oyc\u003D != null)
      {
        foreach (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D vdj8C0KctI6r27Gg in (IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) source.OrderBy<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, double>(uuovZyvmyeAqC2gog8.\u0023\u003Dzon\u0024_RZacJIPJ ?? (uuovZyvmyeAqC2gog8.\u0023\u003Dzon\u0024_RZacJIPJ = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, double>(uuovZyvmyeAqC2gog8.\u0023\u003Dz7Vd1ie04V9seGu9XAXPfhNY\u003D))))
        {
          FrameworkElement frameworkElement = this.\u0023\u003DzoHJDgDlSejs6FIKEDvqYw6U\u003D(vdj8C0KctI6r27Gg);
          if (this.\u0023\u003Dz\u00242kgBXq\u00240zMRcZoHT\u0024crAZw\u003D(frameworkElement, vdj8C0KctI6r27Gg))
          {
            this.\u0023\u003DzFVmSB_1\u0024FHARkzhGiA\u003D\u003D(frameworkElement, vdj8C0KctI6r27Gg);
            this.\u0023\u003DzBmyEynplQR0mx3c8kg\u003D\u003D(vdj8C0KctI6r27Gg.XyCoordinate);
          }
        }
      }
      this.\u0023\u003Dz0by717G8vA4kLgq5Pw\u003D\u003D(_param1);
    }
    this.SeriesData.\u0023\u003DzGPrsWyT8SibF((IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) source);
  }

  protected virtual void \u0023\u003Dz_HFvQ2jjCDBP(
    IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param1,
    ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param2)
  {
    _param1.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(new Action<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(((Collection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) _param2).Add));
  }

  protected override IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzzhlDItrRFv\u0024\u0024(
    Point _param1)
  {
    return this.\u0023\u003DzzhlDItrRFv\u0024\u0024(new Func<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D>(new dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D()
    {
      \u0023\u003Dz_hWqBbI\u003D = _param1,
      \u0023\u003DzRRvwDu67s9Rm = this
    }.\u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D)).\u0023\u003DzcxxcgCCxILAWoUgLzQ\u003D\u003D();
  }

  protected abstract FrameworkElement \u0023\u003DzoHJDgDlSejs6FIKEDvqYw6U\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1);

  private bool \u0023\u003Dz\u00242kgBXq\u00240zMRcZoHT\u0024crAZw\u003D(
    FrameworkElement _param1,
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param2)
  {
    bool flag = _param1 != null;
    if (flag)
    {
      flag = this.\u0023\u003Dz\u00242kgBXq\u00240zMRcZoHT\u0024crAZw\u003D(_param1, _param2.XyCoordinate);
      if (flag)
        this.\u0023\u003Dz5r90DrevxrwN_pOUhQP\u0024AaU\u003D(_param1);
    }
    return flag;
  }

  private bool \u0023\u003Dz\u00242kgBXq\u00240zMRcZoHT\u0024crAZw\u003D(
    FrameworkElement _param1,
    Point _param2)
  {
    int num = this.ModifierSurface.IsPointWithinBounds(_param2) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.\u0023\u003Dz87N63HuX5eNCqzCUw3CPmb4\u003D(_param1, _param2);
    return num != 0;
  }

  private void \u0023\u003Dz87N63HuX5eNCqzCUw3CPmb4\u003D(FrameworkElement _param1, Point _param2)
  {
    double left = Canvas.GetLeft((UIElement) _param1);
    double top = Canvas.GetTop((UIElement) _param1);
    _param1.\u0023\u003DzI0WdlDcUgrX_();
    double length1 = _param2.X - _param1.DesiredSize.Width / 2.0;
    double length2 = _param2.Y - _param1.DesiredSize.Height / 2.0;
    if (left.Equals(length1) && top.Equals(length2))
      return;
    this.\u0023\u003DzWlihjOZFzDLV = true;
    Canvas.SetLeft((UIElement) _param1, length1);
    Canvas.SetTop((UIElement) _param1, length2);
  }

  private void \u0023\u003Dz5r90DrevxrwN_pOUhQP\u0024AaU\u003D(FrameworkElement _param1)
  {
    if (!this.\u0023\u003Dz1lxFA46cCdxI())
    {
      _param1.MouseMove += new MouseEventHandler(this.\u0023\u003Dzh9KvuS_jLUYL93sI\u0024BP6nio\u003D);
      _param1.MouseLeave += new MouseEventHandler(this.\u0023\u003DzJNHp\u0024bXt4L5qmQ5ozPuXdks\u003D);
      _param1.MouseLeftButtonDown += new MouseButtonEventHandler(this.\u0023\u003DzJNHp\u0024bXt4L5qmQ5ozPuXdks\u003D);
    }
    this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) _param1);
    this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D.Add(_param1);
  }

  private void \u0023\u003DzFVmSB_1\u0024FHARkzhGiA\u003D\u003D(
    FrameworkElement _param1,
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param2)
  {
    if (!(_param1.GetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D) is FrameworkElement frameworkElement))
    {
      frameworkElement = (FrameworkElement) this.\u0023\u003DzBv1vB\u0024LEKSF4(this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, (object) _param2);
      _param1.SetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D, (object) frameworkElement);
    }
    frameworkElement.DataContext = (object) _param2;
  }

  protected virtual void \u0023\u003DzBmyEynplQR0mx3c8kg\u003D\u003D(Point _param1)
  {
    if (!this.\u0023\u003Dzt9d2ExuvJfVV(_param1))
      return;
    this.\u0023\u003DzrdOQnD0oNXZ9bphGEA\u003D\u003D(_param1);
  }

  protected void \u0023\u003DzrdOQnD0oNXZ9bphGEA\u003D\u003D(Point _param1)
  {
    if (!this.ShowAxisLabels || this.\u0023\u003DzLgNKpMJ5V7kN())
      return;
    this.\u0023\u003DzQrcA6OyHGRkq();
  }

  private void \u0023\u003Dz0by717G8vA4kLgq5Pw\u003D\u003D(Point _param1)
  {
    bool flag = !this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D.\u0023\u003DzCCMM80zDpO6N<FrameworkElement>();
    if ((!flag ? 1 : (!this.\u0023\u003Dz1lxFA46cCdxI() ? 1 : 0)) == 0)
    {
      if (this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseHover)
        this.\u0023\u003DzIxFmGbTNpwO0.Start(new Action(this.\u0023\u003DzYB52HqOat\u0024eiFNWFGCgZV6d4qran));
      else
        this.\u0023\u003DzK\u0024stNbfZ5RYn(this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D);
    }
    else if (this.ShowTooltipOn != dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseOver || !flag)
      this.\u0023\u003DzpPviEVAcoiYQ();
    this.\u0023\u003DzBmyEynplQR0mx3c8kg\u003D\u003D(_param1);
  }

  private void \u0023\u003DzpPviEVAcoiYQ()
  {
    if (this.ModifierSurface == null)
      return;
    foreach (FrameworkElement frameworkElement in this.\u0023\u003Dzd2PtoXT5k7_W)
    {
      if (this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) frameworkElement))
        this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Remove((UIElement) frameworkElement);
    }
    this.\u0023\u003Dzd2PtoXT5k7_W.Clear();
    foreach (FrameworkElement frameworkElement in this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D)
      this.\u0023\u003DzsTXk6VTlNzAx(frameworkElement);
  }

  private void \u0023\u003DzJNHp\u0024bXt4L5qmQ5ozPuXdks\u003D(
    object _param1,
    MouseEventArgs _param2)
  {
    this.\u0023\u003DzpPviEVAcoiYQ();
    this.\u0023\u003DzWlihjOZFzDLV = true;
  }

  private void \u0023\u003Dzh9KvuS_jLUYL93sI\u0024BP6nio\u003D(
    object _param1,
    MouseEventArgs _param2)
  {
    List<FrameworkElement> frameworkElementList = this.\u0023\u003DzlX5r9Aa8WgmNUx8_CPM_dSE\u003D(_param2.GetPosition((IInputElement) (this.ModifierSurface as UIElement)));
    if (!this.\u0023\u003DzWlihjOZFzDLV)
      return;
    this.\u0023\u003DzK\u0024stNbfZ5RYn(frameworkElementList);
    this.\u0023\u003DzWlihjOZFzDLV = false;
  }

  private List<FrameworkElement> \u0023\u003DzlX5r9Aa8WgmNUx8_CPM_dSE\u003D(Point _param1)
  {
    List<FrameworkElement> frameworkElementList = new List<FrameworkElement>();
    foreach (FrameworkElement frameworkElement in this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D)
    {
      if (this.\u0023\u003DzJ9n0Twkemi0Ero8oB32YtoE\u003D(frameworkElement).Contains(_param1))
        frameworkElementList.Add(frameworkElement);
    }
    return frameworkElementList;
  }

  private Rect \u0023\u003DzJ9n0Twkemi0Ero8oB32YtoE\u003D(FrameworkElement _param1)
  {
    double num = (double) _param1.GetValue(Canvas.TopProperty);
    return new Rect((double) _param1.GetValue(Canvas.LeftProperty), num, _param1.ActualWidth, _param1.ActualHeight);
  }

  private void \u0023\u003DzK\u0024stNbfZ5RYn(List<FrameworkElement> _param1)
  {
    this.\u0023\u003DzpPviEVAcoiYQ();
    if (_param1.\u0023\u003DzMeGSfVE\u003D<FrameworkElement>())
      return;
    foreach (FrameworkElement frameworkElement in _param1)
      this.\u0023\u003DzG0IVNFBi26Bw(frameworkElement);
    foreach (FrameworkElement frameworkElement in this.\u0023\u003Dzd2PtoXT5k7_W)
    {
      if (!this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Contains((UIElement) frameworkElement))
      {
        (frameworkElement.Parent as Panel).\u0023\u003DziYdJ\u00246cCiBha((object) frameworkElement);
        this.ModifierSurface.\u0023\u003DzBDSV99pPo8hY().Add((UIElement) frameworkElement);
      }
    }
  }

  private void \u0023\u003DzG0IVNFBi26Bw(FrameworkElement _param1)
  {
    FrameworkElement frameworkElement = (FrameworkElement) _param1.GetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D);
    if (frameworkElement == null)
      return;
    double left = Canvas.GetLeft((UIElement) _param1);
    double top = Canvas.GetTop((UIElement) _param1);
    Rect rect1 = this.\u0023\u003DzJ9n0Twkemi0Ero8oB32YtoE\u003D(_param1);
    Rect rect2 = new Rect(0.0, 0.0, this.ModifierSurface.\u0023\u003Dzu2ObQ3hMALTN(), this.ModifierSurface.\u0023\u003Dz2kO1mtG\u0024bEUM());
    this.\u0023\u003Dzd2PtoXT5k7_W.Add(this.\u0023\u003Dz7hf3biWYyl4a(new Point(left, top), frameworkElement, rect1, rect2));
  }

  private FrameworkElement \u0023\u003Dz7hf3biWYyl4a(
    Point _param1,
    FrameworkElement _param2,
    Rect _param3,
    Rect _param4)
  {
    dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D kvt89B1lUeA7EdfukJs = new dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D();
    FrameworkElement element = _param2;
    kvt89B1lUeA7EdfukJs.\u0023\u003DzHxqHejRJ3zna = this.\u0023\u003Dz\u0024Im\u00248\u0024bqRJGl(element, _param1, _param3, _param4);
    FrameworkElement frameworkElement1 = this.\u0023\u003Dzd2PtoXT5k7_W.FirstOrDefault<FrameworkElement>(new Func<FrameworkElement, bool>(kvt89B1lUeA7EdfukJs.\u0023\u003DzL4Rz\u0024NK6CSO_4VDHiA\u003D\u003D));
    if (frameworkElement1 != null)
    {
      FrameworkElement frameworkElement2 = (FrameworkElement) this.\u0023\u003DzVKLXohYyQKx\u0024(frameworkElement1, _param2);
      element = this.\u0023\u003Dz7hf3biWYyl4a(_param1, frameworkElement2, _param3, _param4);
    }
    else
    {
      Canvas.SetLeft((UIElement) element, kvt89B1lUeA7EdfukJs.\u0023\u003DzHxqHejRJ3zna.X);
      Canvas.SetTop((UIElement) element, kvt89B1lUeA7EdfukJs.\u0023\u003DzHxqHejRJ3zna.Y);
    }
    return element;
  }

  private Rect \u0023\u003Dz\u0024Im\u00248\u0024bqRJGl(
    FrameworkElement _param1,
    Point _param2,
    Rect _param3,
    Rect _param4)
  {
    _param1.\u0023\u003DzI0WdlDcUgrX_();
    double num1;
    double num2;
    if (this.ShowTooltipOn == dje_zW5MLMFCCFHVWSRGZD77RWP95DJXJ342SYK8Q3LCSNM5MWZXH4Q2GW_ejd.MouseOver)
    {
      num1 = 3.0;
      num2 = 0.0;
    }
    else
    {
      num1 = 0.0;
      num2 = _param3.Height / 2.0 + _param1.ActualHeight / 2.0;
    }
    double num3 = _param3.Right + num1;
    double num4 = !(_param1 is Panel panel) || panel.Children.Count <= 1 ? _param3.Bottom - num2 + num1 : _param2.Y - _param1.ActualHeight / 2.0;
    Rect rect = new Rect(num3, num4, _param1.ActualWidth, _param1.ActualHeight);
    if (_param4.Right < rect.Right)
      num3 = _param3.Left - _param1.ActualWidth - num1;
    if (_param4.Bottom < rect.Bottom)
    {
      double num5 = rect.Bottom - _param4.Bottom;
      num4 = num4 - num5 - num1;
    }
    if (_param4.Top > rect.Top)
    {
      double num6 = _param4.Top - rect.Top;
      num4 = num4 + num6 + num1;
    }
    rect.X = num3;
    rect.Y = num4;
    return rect;
  }

  private StackPanel \u0023\u003DzVKLXohYyQKx\u0024(
    FrameworkElement _param1,
    FrameworkElement _param2)
  {
    StackPanel stackPanel1 = _param1 as StackPanel;
    StackPanel stackPanel2 = _param2 as StackPanel;
    StackPanel stackPanel3;
    if (stackPanel1 != null)
    {
      stackPanel1.\u0023\u003DzH0osWQkV_Y8_((object) _param2, -1);
      stackPanel3 = stackPanel1;
    }
    else if (stackPanel2 != null)
    {
      stackPanel2.\u0023\u003DzH0osWQkV_Y8_((object) _param1, 0);
      stackPanel3 = stackPanel2;
    }
    else
    {
      stackPanel3 = new StackPanel();
      stackPanel3.\u0023\u003DzH0osWQkV_Y8_((object) _param1, -1);
      stackPanel3.\u0023\u003DzH0osWQkV_Y8_((object) _param2, -1);
    }
    this.\u0023\u003Dzd2PtoXT5k7_W.Remove(_param1);
    this.\u0023\u003Dzd2PtoXT5k7_W.Remove(_param2);
    return stackPanel3;
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X()
  {
    this.\u0023\u003DzCTuuQt0\u003D(false);
    this.\u0023\u003DzpPviEVAcoiYQ();
    this.\u0023\u003DzaHyDUm8F3XtsCygXSA\u003D\u003D();
    this.\u0023\u003DzIxFmGbTNpwO0.Stop();
  }

  protected override void \u0023\u003DzY0Ucom6W\u0024E0ZkvcKcA\u003D\u003D()
  {
    if (this.ParentSurface == null || this.ParentSurface.get_RenderableSeries() == null)
      return;
    this.ParentSurface.get_RenderableSeries().Where<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D ?? (dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D = new Func<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D, bool>(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzm45_WtFfYT4tvUJMHg7mOs2IT228))).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>(new Action<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D>(this.\u0023\u003Dzw9s7TrZ4QMkFRbgXlOyM52Lqlxec));
  }

  protected override void \u0023\u003Dz9otdS\u0024TJZ4U8t8zXqw\u003D\u003D()
  {
    this.\u0023\u003DzQrcA6OyHGRkq();
  }

  protected override void \u0023\u003Dz\u0024523lOKnSPCb(
    IEnumerable<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> _param1,
    IEnumerable<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> _param2)
  {
    base.\u0023\u003Dz\u0024523lOKnSPCb(_param1, _param2);
    this.\u0023\u003DzCTuuQt0\u003D(true);
  }

  private void \u0023\u003DzYB52HqOat\u0024eiFNWFGCgZV6d4qran()
  {
    this.\u0023\u003DzK\u0024stNbfZ5RYn(this.\u0023\u003Dzy9FxkpTkktj_\u0024CSRiA\u003D\u003D);
  }

  private void \u0023\u003Dzw9s7TrZ4QMkFRbgXlOyM52Lqlxec(
    \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
  {
    _param1.\u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D().SetValue(dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dzj5rVXg3E650t0FU9dw\u003D\u003D, (object) this.\u0023\u003DzBv1vB\u0024LEKSF4(this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, _param1.\u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D().DataContext));
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D, bool> \u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D;

    internal bool \u0023\u003Dzm45_WtFfYT4tvUJMHg7mOs2IT228(
      \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
    {
      return _param1.\u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D() != null && _param1.\u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D().DataContext != null;
    }
  }

  private sealed class \u0023\u003DzE1LcgUuovZyvmyeAqC2gog8\u003D
  {
    public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003Dz4bf8Oyc\u003D;
    public Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, double> \u0023\u003Dzon\u0024_RZacJIPJ;

    internal double \u0023\u003Dz7Vd1ie04V9seGu9XAXPfhNY\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return !this.\u0023\u003Dz4bf8Oyc\u003D.IsHorizontalAxis ? _param1.XyCoordinate.X : _param1.XyCoordinate.Y;
    }
  }

  private sealed class \u0023\u003DzE1noKVt89B1lUeA7EDfukJs\u003D
  {
    public Rect \u0023\u003DzHxqHejRJ3zna;

    internal bool \u0023\u003DzL4Rz\u0024NK6CSO_4VDHiA\u003D\u003D(FrameworkElement _param1)
    {
      return new Rect(Canvas.GetLeft((UIElement) _param1), Canvas.GetTop((UIElement) _param1), _param1.ActualWidth, _param1.ActualHeight).IntersectsWith(this.\u0023\u003DzHxqHejRJ3zna);
    }
  }

  private sealed class \u0023\u003DzWDeT\u0024CyLvtppRwsYCyoelU8\u003D
  {
    public Point \u0023\u003Dz_hWqBbI\u003D;
    public dje_zZF2ZJZG8SE3C2A3Q4WP7G7C5HK3C8HEAQSAKCA7W3GNCVWR3A27YZ_ejd \u0023\u003DzRRvwDu67s9Rm;

    internal \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D(
      \u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D _param1)
    {
      return _param1.\u0023\u003DznVLFa68vHPHy(this.\u0023\u003Dz_hWqBbI\u003D, this.\u0023\u003DzRRvwDu67s9Rm.UseInterpolation);
    }
  }
}
