// Decompiled with JetBrains decompiler
// Type: -.PolarPanel
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
namespace StockSharp.Charting;

public sealed class PolarPanel : Panel
{
  
  public static readonly DependencyProperty \u0023\u003DzAJq6AwIY2FAx = DependencyProperty.Register(nameof (StretchToSize), typeof (bool), typeof (PolarPanel), new PropertyMetadata((object) false, new PropertyChangedCallback(PolarPanel.\u0023\u003DzSmUgMtsFJvHJ)));
  
  public static readonly DependencyProperty \u0023\u003DzQOUi7HHVdeMg = DependencyProperty.Register(nameof (IsReversedOrder), typeof (bool), typeof (PolarPanel), new PropertyMetadata((object) false, new PropertyChangedCallback(PolarPanel.\u0023\u003DzSmUgMtsFJvHJ)));
  
  public static readonly DependencyProperty \u0023\u003DzhcwQeN4Ni3tYrLZddCANtlE\u003D = DependencyProperty.Register(nameof (MinimimalItemSize), typeof (double), typeof (PolarPanel), new PropertyMetadata((object) 0.0));
  
  public static readonly DependencyProperty \u0023\u003DzxCZVrddkVrh_8diOJA\u003D\u003D = DependencyProperty.RegisterAttached("ShouldCopyThicknessToParent", typeof (bool), typeof (PolarPanel), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003Dz_FG2Iog\u003D = DependencyProperty.RegisterAttached("Thickness", typeof (double), typeof (PolarPanel), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(PolarPanel.\u0023\u003Dzt8OwNSS67lRk)));
  
  private Size \u0023\u003Dzk9BUboiLgD9a;
  
  private UIElement \u0023\u003Dz8RF1NqwMBMye;
  
  private bool \u0023\u003Dz5Q_\u0024YkuhIHbd;

  public PolarPanel()
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
      return (bool) this.GetValue(PolarPanel.\u0023\u003DzAJq6AwIY2FAx);
    }
    set
    {
      this.SetValue(PolarPanel.\u0023\u003DzAJq6AwIY2FAx, (object) value);
    }
  }

  public bool IsReversedOrder
  {
    get
    {
      return (bool) this.GetValue(PolarPanel.\u0023\u003DzQOUi7HHVdeMg);
    }
    set
    {
      this.SetValue(PolarPanel.\u0023\u003DzQOUi7HHVdeMg, (object) value);
    }
  }

  public double MinimimalItemSize
  {
    get
    {
      return (double) this.GetValue(PolarPanel.\u0023\u003DzhcwQeN4Ni3tYrLZddCANtlE\u003D);
    }
    set
    {
      this.SetValue(PolarPanel.\u0023\u003DzhcwQeN4Ni3tYrLZddCANtlE\u003D, (object) value);
    }
  }

  protected override Size MeasureOverride(Size _param1)
  {
    bool flag = !this.StretchToSize;
    this.\u0023\u003Dzk9BUboiLgD9a = base.MeasureOverride(_param1);
    UIElement[] array = this.\u0023\u003DzL8atuyQ\u003D().ToArray<UIElement>();
    double num = ((IEnumerable<UIElement>) array).Select<UIElement, double>(PolarPanel.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D ?? (PolarPanel.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D = new Func<UIElement, double>(PolarPanel.GetThickness))).Sum();
    Size size = this.\u0023\u003DznnXn2lRSshJn(_param1.Width, _param1.Height, num);
    foreach (UIElement uiElement in array)
    {
      double thickness = PolarPanel.GetThickness(uiElement);
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
    IEnumerable<UIElement> source = this.Children.OfType<UIElement>().Where<UIElement>(PolarPanel.SomeClass34343383.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D ?? (PolarPanel.SomeClass34343383.\u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D = new Func<UIElement, bool>(PolarPanel.SomeClass34343383.SomeMethond0343.\u0023\u003Dz2zySBoQIuvaRfw_ykBJUBYo\u003D)));
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
    double num = ((IEnumerable<UIElement>) array).Select<UIElement, double>(PolarPanel.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D ?? (PolarPanel.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D = new Func<UIElement, double>(PolarPanel.GetThickness))).Sum();
    Size size = this.\u0023\u003DznnXn2lRSshJn(_param1.Width, _param1.Height, num);
    Point point = new Point(_param1.Width / 2.0, _param1.Height / 2.0);
    foreach (UIElement uiElement in array)
    {
      double thickness = PolarPanel.GetThickness(uiElement);
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
    PolarPanel.SetThickness(this.\u0023\u003Dz8RF1NqwMBMye, _param1);
  }

  public static void SetThickness(UIElement _param0, double _param1)
  {
    _param0.SetValue(PolarPanel.\u0023\u003Dz_FG2Iog\u003D, (object) _param1);
  }

  public static double GetThickness(UIElement _param0)
  {
    return (double) _param0.GetValue(PolarPanel.\u0023\u003Dz_FG2Iog\u003D);
  }

  public static void SetShouldCopyThicknessToParent(UIElement _param0, bool _param1)
  {
    _param0.SetValue(PolarPanel.\u0023\u003DzxCZVrddkVrh_8diOJA\u003D\u003D, (object) _param1);
  }

  public static bool GetShouldCopyThicknessToParent(UIElement _param0)
  {
    return (bool) _param0.GetValue(PolarPanel.\u0023\u003DzxCZVrddkVrh_8diOJA\u003D\u003D);
  }

  private static void \u0023\u003DzSmUgMtsFJvHJ(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is PolarPanel ahuapnhgtW9LabzEjd))
      return;
    ahuapnhgtW9LabzEjd.InvalidateMeasure();
  }

  private static void \u0023\u003Dzt8OwNSS67lRk(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is UIElement reference))
      return;
    if (VisualTreeHelper.GetParent((DependencyObject) reference) is PolarPanel parent)
      parent.InvalidateMeasure();
    PolarPanel.\u0023\u003DzTqJvQ4TZxFYJ(reference, (double) _param1.NewValue);
  }

  private static void \u0023\u003DzTqJvQ4TZxFYJ(UIElement _param0, double _param1)
  {
    if (!PolarPanel.GetShouldCopyThicknessToParent(_param0) || !(VisualTreeHelper.GetParent((DependencyObject) _param0) is UIElement parent))
      return;
    parent.SetValue(PolarPanel.\u0023\u003Dz_FG2Iog\u003D, (object) _param1);
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
  private sealed class SomeClass34343383
  {
    public static readonly PolarPanel.SomeClass34343383 SomeMethond0343 = new PolarPanel.SomeClass34343383();
    public static Func<UIElement, bool> \u0023\u003Dzfa4InO8BG_qfj7v\u0024gA\u003D\u003D;

    public bool \u0023\u003Dz2zySBoQIuvaRfw_ykBJUBYo\u003D(UIElement _param1)
    {
      return _param1.\u0023\u003DzST\u0024t7rI\u003D();
    }
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static Func<UIElement, double> \u0023\u003DzuCLgS8K74VjOD1F9QA\u003D\u003D;
  }
}
