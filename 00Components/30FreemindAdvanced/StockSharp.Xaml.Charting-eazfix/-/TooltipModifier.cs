// Decompiled with JetBrains decompiler
// Type: -.TooltipModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace SciChart.Charting;

internal sealed class TooltipModifier : 
  TooltipModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DzNRygy3vTBpTh = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (TooltipModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzqLvPVa7rf46kZxQtAw\u003D\u003D = DependencyProperty.Register(nameof (TooltipLabelDataContextSelector), typeof (Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, object>), typeof (TooltipModifier), new PropertyMetadata((PropertyChangedCallback) null));
  
  private TemplatableControl \u0023\u003DzKBqDaxX8NlWc;

  public TooltipModifier()
  {
    this.DefaultStyleKey = (object) typeof (TooltipModifier);
  }

  public static bool GetIncludeSeries(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(TooltipModifier.\u0023\u003DzNRygy3vTBpTh);
  }

  public static void SetIncludeSeries(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(TooltipModifier.\u0023\u003DzNRygy3vTBpTh, (object) _param1);
  }

  public Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, object> TooltipLabelDataContextSelector
  {
    get
    {
      return (Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, object>) this.GetValue(TooltipModifier.\u0023\u003DzqLvPVa7rf46kZxQtAw\u003D\u003D);
    }
    set
    {
      this.SetValue(TooltipModifier.\u0023\u003DzqLvPVa7rf46kZxQtAw\u003D\u003D, (object) value);
    }
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X() => this.\u0023\u003Dz8oag0whBqVB\u0024();

  private void \u0023\u003Dz8oag0whBqVB\u0024()
  {
    if (this.\u0023\u003DzKBqDaxX8NlWc == null || this.ModifierSurface == null)
      return;
    if (this.ModifierSurface.Children().Contains((UIElement) this.\u0023\u003DzKBqDaxX8NlWc))
      this.ModifierSurface.Children().Remove((UIElement) this.\u0023\u003DzKBqDaxX8NlWc);
    this.\u0023\u003DzeAqKwx8\u003D = new Point(double.NaN, double.NaN);
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("TooltipModifier Master MouseMove: {0}, {1}", new object[2]
    {
      (object) _param1.X,
      (object) _param1.Y
    });
    object obj = this.\u0023\u003DzUnM3UXVEt72A(_param1, out Point _);
    if (obj != null && this.\u0023\u003DzKBqDaxX8NlWc != null)
    {
      this.\u0023\u003DzKBqDaxX8NlWc.DataContext = obj;
      this.\u0023\u003Dz880XZzrLPPf8(_param1);
    }
    else
      this.\u0023\u003Dz8oag0whBqVB\u0024();
  }

  protected override void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1)
  {
  }

  private object \u0023\u003DzUnM3UXVEt72A(Point _param1, out Point _param2)
  {
    _param2 = new Point();
    object obj = (object) null;
    foreach (\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D vdj8C0KctI6r27Gg in this.\u0023\u003DzzhlDItrRFv\u0024\u0024(_param1))
    {
      if (vdj8C0KctI6r27Gg.IsHit && vdj8C0KctI6r27Gg.RenderableSeries.\u0023\u003DzVxrZQ3k9ZBGJ((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 2))
      {
        _param2 = vdj8C0KctI6r27Gg.XyCoordinate;
        obj = this.TooltipLabelDataContextSelector != null ? this.TooltipLabelDataContextSelector(vdj8C0KctI6r27Gg) : (object) vdj8C0KctI6r27Gg;
        break;
      }
    }
    return obj;
  }

  private void \u0023\u003Dz880XZzrLPPf8(Point _param1)
  {
    if (this.\u0023\u003DzKBqDaxX8NlWc == null)
      return;
    this.\u0023\u003DzHi3KLf0npKDN((FrameworkElement) this.\u0023\u003DzKBqDaxX8NlWc, _param1);
    if (this.ModifierSurface.Children().Contains((UIElement) this.\u0023\u003DzKBqDaxX8NlWc))
      return;
    this.ModifierSurface.Children().Add((UIElement) this.\u0023\u003DzKBqDaxX8NlWc);
  }

  private void \u0023\u003DzHi3KLf0npKDN(FrameworkElement _param1, Point _param2)
  {
    Rect rect1 = new Rect(0.0, 0.0, this.ModifierSurface.ActualWidth, this.ModifierSurface.ActualHeight);
    double length1 = _param2.X + 6.0;
    double length2 = _param2.Y + 6.0;
    Rect rect2 = new Rect(length1, length2, _param1.ActualWidth, _param1.ActualHeight);
    if (rect1.Right < rect2.Right)
      length1 = _param2.X - rect2.Width - 6.0;
    if (rect1.Bottom < rect2.Bottom)
    {
      double num1 = rect2.Bottom - rect1.Bottom;
      double num2 = length2 - num1;
      length2 = num2 < 0.0 ? 0.0 : num2;
    }
    Canvas.SetLeft((UIElement) _param1, length1);
    Canvas.SetTop((UIElement) _param1, length2);
  }

  protected override void \u0023\u003DzY0Ucom6W\u0024E0ZkvcKcA\u003D\u003D()
  {
    this.\u0023\u003DzKBqDaxX8NlWc = this.\u0023\u003DzBv1vB\u0024LEKSF4(this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, (object) this);
  }

  protected override void \u0023\u003Dz9otdS\u0024TJZ4U8t8zXqw\u003D\u003D()
  {
  }
}
