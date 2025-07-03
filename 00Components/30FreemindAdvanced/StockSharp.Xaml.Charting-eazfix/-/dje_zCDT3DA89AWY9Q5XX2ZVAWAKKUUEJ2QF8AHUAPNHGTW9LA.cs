// Decompiled with JetBrains decompiler
// Type: -.dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal sealed class dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd : Panel
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzAJq6AwIY2FAx = DependencyProperty.Register(nameof (StretchToSize), typeof (bool), typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzSmUgMtsFJvHJ)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzQOUi7HHVdeMg = DependencyProperty.Register(nameof (IsReversedOrder), typeof (bool), typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzSmUgMtsFJvHJ)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzhcwQeN4Ni3tYrLZddCANtlE\u003D = DependencyProperty.Register(nameof (MinimimalItemSize), typeof (double), typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd), new PropertyMetadata((object) 0.0));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzxCZVrddkVrh_8diOJA\u003D\u003D = DependencyProperty.RegisterAttached("ShouldCopyThicknessToParent", typeof (bool), typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd), new PropertyMetadata((object) false));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz_FG2Iog\u003D = DependencyProperty.RegisterAttached("Thickness", typeof (double), typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dzt8OwNSS67lRk)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Size \u0023\u003Dzk9BUboiLgD9a;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private UIElement \u0023\u003Dz8RF1NqwMBMye;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003Dz5Q_\u0024YkuhIHbd;

  public dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd()
  {
    this.Loaded += new RoutedEventHandler(this.\u0023\u003Dzf9AYwNNDizJWbyAPQQ\u003D\u003D);
    this.Unloaded += new RoutedEventHandler(this.\u0023\u003Dzn0eOhoub0qbFxJRA9g\u003D\u003D);
  }

  private void \u0023\u003Dzk23ZGLO4yuc7()
  {
    if (this.IsItemsHost)
      this.\u0023\u003Dz8RF1NqwMBMye = (UIElement) this.\u0023\u003DzTXWydaSPeI\u0024J<ItemsControl>();
    else
      this.\u0023\u003Dz8RF1NqwMBMye = (UIElement) this;
  }

  public bool StretchToSize
  {
    get
    {
      return (bool) this.GetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzAJq6AwIY2FAx);
    }
    set
    {
      this.SetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzAJq6AwIY2FAx, (object) value);
    }
  }

  public bool IsReversedOrder
  {
    get
    {
      return (bool) this.GetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzQOUi7HHVdeMg);
    }
    set
    {
      this.SetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzQOUi7HHVdeMg, (object) value);
    }
  }

  public double MinimimalItemSize
  {
    get
    {
      return (double) this.GetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzhcwQeN4Ni3tYrLZddCANtlE\u003D);
    }
    set
    {
      this.SetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzhcwQeN4Ni3tYrLZddCANtlE\u003D, (object) value);
    }
  }

  protected override Size MeasureOverride(Size _param1)
  {
    bool flag = !this.StretchToSize;
    this.\u0023\u003Dzk9BUboiLgD9a = base.MeasureOverride(_param1);
    UIElement[] array = this.\u0023\u003DzL8atuyQ\u003D().ToArray<UIElement>();
    double num = ((IEnumerable<UIElement>) array).Select<UIElement, double>(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D ?? (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D = new Func<UIElement, double>(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.GetThickness))).Sum();
    Size size = this.\u0023\u003DznnXn2lRSshJn(_param1.Width, _param1.Height, num);
    foreach (UIElement uiElement in array)
    {
      double thickness = dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.GetThickness(uiElement);
      size.Width += 2.0 * thickness;
      size.Height += 2.0 * thickness;
      if (flag)
        uiElement.Measure(this.\u0023\u003Dzk9BUboiLgD9a);
    }
    if (size.Width.\u0023\u003Dz_Bj0HmLWq3hY() && size.Height.\u0023\u003Dz_Bj0HmLWq3hY())
    {
      this.\u0023\u003Dzk9BUboiLgD9a.Width = Math.Max(size.Width, this.\u0023\u003Dzk9BUboiLgD9a.Width);
      this.\u0023\u003Dzk9BUboiLgD9a.Height = Math.Max(size.Height, this.\u0023\u003Dzk9BUboiLgD9a.Height);
    }
    if (!flag)
    {
      foreach (UIElement uiElement in array)
        uiElement.Measure(this.\u0023\u003Dzk9BUboiLgD9a);
    }
    return this.\u0023\u003Dzk9BUboiLgD9a;
  }

  private IEnumerable<UIElement> \u0023\u003DzL8atuyQ\u003D()
  {
    IEnumerable<UIElement> source = this.Children.OfType<UIElement>().Where<UIElement>(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D ?? (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D = new Func<UIElement, bool>(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz2zySBoQIuvaRfw_ykBJUBYo\u003D)));
    return !this.IsReversedOrder ? source : source.Reverse<UIElement>();
  }

  private Size \u0023\u003DznnXn2lRSshJn(double _param1, double _param2, double _param3)
  {
    if (!this.StretchToSize)
      _param1 = _param2 = Math.Min(_param1, _param2);
    _param1 -= _param3 * 2.0;
    _param2 -= _param3 * 2.0;
    if (!this.StretchToSize)
    {
      _param1 = Math.Max(_param1, 50.0);
      _param2 = Math.Max(_param2, 50.0);
    }
    return new Size(_param1, _param2);
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    UIElement[] array = this.\u0023\u003DzL8atuyQ\u003D().ToArray<UIElement>();
    double num = ((IEnumerable<UIElement>) array).Select<UIElement, double>(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D ?? (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D = new Func<UIElement, double>(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.GetThickness))).Sum();
    Size size = this.\u0023\u003DznnXn2lRSshJn(_param1.Width, _param1.Height, num);
    Point point = new Point(_param1.Width / 2.0, _param1.Height / 2.0);
    foreach (UIElement uiElement in array)
    {
      double thickness = dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.GetThickness(uiElement);
      size.Width += 2.0 * thickness;
      size.Height += 2.0 * thickness;
      uiElement.Arrange(new Rect(new Point(point.X - size.Width / 2.0, point.Y - size.Height / 2.0), size));
    }
    this.\u0023\u003DzLuKOYbwzbNgn(num);
    return _param1;
  }

  private void \u0023\u003DzLuKOYbwzbNgn(double _param1)
  {
    if (!this.\u0023\u003Dz5Q_\u0024YkuhIHbd)
      this.\u0023\u003Dzk23ZGLO4yuc7();
    if (this.\u0023\u003Dz8RF1NqwMBMye == null)
      return;
    dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.SetThickness(this.\u0023\u003Dz8RF1NqwMBMye, _param1);
  }

  public static void SetThickness(UIElement _param0, double _param1)
  {
    _param0.SetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz_FG2Iog\u003D, (object) _param1);
  }

  public static double GetThickness(UIElement _param0)
  {
    return (double) _param0.GetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz_FG2Iog\u003D);
  }

  public static void SetShouldCopyThicknessToParent(UIElement _param0, bool _param1)
  {
    _param0.SetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzxCZVrddkVrh_8diOJA\u003D\u003D, (object) _param1);
  }

  public static bool GetShouldCopyThicknessToParent(UIElement _param0)
  {
    return (bool) _param0.GetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzxCZVrddkVrh_8diOJA\u003D\u003D);
  }

  private static void \u0023\u003DzSmUgMtsFJvHJ(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd ahuapnhgtW9LabzEjd))
      return;
    ahuapnhgtW9LabzEjd.InvalidateMeasure();
  }

  private static void \u0023\u003Dzt8OwNSS67lRk(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is UIElement reference))
      return;
    if (VisualTreeHelper.GetParent((DependencyObject) reference) is dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd parent)
      parent.InvalidateMeasure();
    dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003DzTqJvQ4TZxFYJ(reference, (double) _param1.NewValue);
  }

  private static void \u0023\u003DzTqJvQ4TZxFYJ(UIElement _param0, double _param1)
  {
    if (!dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.GetShouldCopyThicknessToParent(_param0) || !(VisualTreeHelper.GetParent((DependencyObject) _param0) is UIElement parent))
      return;
    parent.SetValue(dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz_FG2Iog\u003D, (object) _param1);
  }

  private void \u0023\u003Dzf9AYwNNDizJWbyAPQQ\u003D\u003D(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003Dzk23ZGLO4yuc7();
    this.\u0023\u003Dz5Q_\u0024YkuhIHbd = true;
  }

  private void \u0023\u003Dzn0eOhoub0qbFxJRA9g\u003D\u003D(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003Dz8RF1NqwMBMye = (UIElement) null;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUEJ2QF8AHUAPNHGTW9LABZ_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<UIElement, bool> \u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D;

    internal bool \u0023\u003Dz2zySBoQIuvaRfw_ykBJUBYo\u003D(UIElement _param1)
    {
      return _param1.\u0023\u003DzST\u0024t7rI\u003D();
    }
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static Func<UIElement, double> \u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D;
  }
}
