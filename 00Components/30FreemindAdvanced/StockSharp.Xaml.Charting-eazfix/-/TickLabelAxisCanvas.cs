// Decompiled with JetBrains decompiler
// Type: -.TickLabelAxisCanvas
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
namespace SciChart.Charting;

internal class TickLabelAxisCanvas : 
  AxisCanvas
{
  
  private readonly DoubleMath \u0023\u003Dzxyhw1x0WHB0D = new DoubleMath();
  
  private readonly List<DefaultTickLabel> \u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D = new List<DefaultTickLabel>();
  
  public static readonly DependencyProperty \u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D = DependencyProperty.Register(nameof (IsLabelCullingEnabled), typeof (bool), typeof (TickLabelAxisCanvas), new PropertyMetadata((object) true));

  public bool IsLabelCullingEnabled
  {
    get
    {
      return (bool) this.GetValue(TickLabelAxisCanvas.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D);
    }
    set
    {
      this.SetValue(TickLabelAxisCanvas.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D, (object) value);
    }
  }

  protected override Size MeasureOverride(Size _param1)
  {
    return !this.SizeWidthToContent ? this.\u0023\u003Dz1HThMHk4tb8z() : this.\u0023\u003Dzs8eGzgkBBuUS();
  }

  private Size \u0023\u003Dzs8eGzgkBBuUS()
  {
    Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
    double num1 = 0.0;
    foreach (UIElement child in this.Children)
    {
      child.Measure(availableSize);
      Size desiredSize = child.DesiredSize;
      double width1 = desiredSize.Width;
      double num2 = this.\u0023\u003Dzxyhw1x0WHB0D.Max(AxisCanvas.GetLeft(child) + width1, AxisCanvas.GetCenterLeft(child) + width1 / 2.0);
      if (this.\u0023\u003Dzxyhw1x0WHB0D.IsNaN(num2))
      {
        double right = AxisCanvas.GetRight(child);
        desiredSize = child.DesiredSize;
        double width2 = desiredSize.Width;
        num2 = right + width2;
      }
      num1 = this.\u0023\u003Dzxyhw1x0WHB0D.Max(num2, num1);
    }
    return new Size(num1, 0.0);
  }

  private Size \u0023\u003Dz1HThMHk4tb8z()
  {
    Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
    double num1 = 0.0;
    foreach (UIElement child in this.Children)
    {
      child.Measure(availableSize);
      Size desiredSize = child.DesiredSize;
      double height1 = desiredSize.Height;
      double num2 = this.\u0023\u003Dzxyhw1x0WHB0D.Max(AxisCanvas.GetTop(child) + height1, AxisCanvas.GetCenterTop(child) + height1 / 2.0);
      if (this.\u0023\u003Dzxyhw1x0WHB0D.IsNaN(num2))
      {
        double bottom = AxisCanvas.GetBottom(child);
        desiredSize = child.DesiredSize;
        double height2 = desiredSize.Height;
        num2 = bottom + height2;
      }
      num1 = this.\u0023\u003Dzxyhw1x0WHB0D.Max(num2, num1);
    }
    return new Size(0.0, num1);
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    this.\u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D.Clear();
    bool flag1 = false;
    bool labelCullingEnabled = this.IsLabelCullingEnabled;
    foreach (IGrouping<int, DefaultTickLabel> source in (IEnumerable<IGrouping<int, DefaultTickLabel>>) this.Children.OfType<DefaultTickLabel>().GroupBy<DefaultTickLabel, int>(TickLabelAxisCanvas.SomeClass34343383.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D ?? (TickLabelAxisCanvas.SomeClass34343383.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D = new Func<DefaultTickLabel, int>(TickLabelAxisCanvas.SomeClass34343383.SomeMethond0343.\u0023\u003DzBxO6u\u0024HlVKKWbdOt3RMd_j8\u003D))).OrderByDescending<IGrouping<int, DefaultTickLabel>, int>(TickLabelAxisCanvas.SomeClass34343383.\u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D ?? (TickLabelAxisCanvas.SomeClass34343383.\u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D = new Func<IGrouping<int, DefaultTickLabel>, int>(TickLabelAxisCanvas.SomeClass34343383.SomeMethond0343.\u0023\u003Dzug\u0024q6sWpYdaQHtVbrSZZtJw\u003D))))
    {
      if (!flag1)
      {
        foreach (DefaultTickLabel y9QajdhH6H6U9EEjd in (IEnumerable<DefaultTickLabel>) source)
        {
          TickLabelAxisCanvas.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D zeaY3Uu1m4CyxerxRw = new TickLabelAxisCanvas.\u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D();
          zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1Nrh7CPGtaPVGj6s\u0024g\u003D\u003D = this.\u0023\u003Dzzigj\u0024danccVcsgdXBQ\u003D\u003D(_param1, (UIElement) y9QajdhH6H6U9EEjd);
          y9QajdhH6H6U9EEjd.\u0023\u003DzT3xod7VLFT46Za\u0024kcJyM7Jw\u003D(zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1Nrh7CPGtaPVGj6s\u0024g\u003D\u003D);
          if (labelCullingEnabled)
          {
            flag1 = this.\u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D.Any<DefaultTickLabel>(new Func<DefaultTickLabel, bool>(zeaY3Uu1m4CyxerxRw.\u0023\u003Dz9Wax66ks0XlfNwfslg\u003D\u003D));
            if (flag1)
              break;
          }
          TickLabelAxisCanvas.\u0023\u003Dz8fHvNrgO0UsN(y9QajdhH6H6U9EEjd);
          this.\u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D.Add(y9QajdhH6H6U9EEjd);
          y9QajdhH6H6U9EEjd.Arrange(zeaY3Uu1m4CyxerxRw.\u0023\u003Dz1Nrh7CPGtaPVGj6s\u0024g\u003D\u003D);
        }
      }
      if (flag1)
      {
        bool flag2 = this.\u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D.Count == 1 && this.\u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D[0].\u0023\u003DzcWxBLkct8Sd5yemMlw\u003D\u003D() == source.Key;
        source.Skip<DefaultTickLabel>(flag2 ? 1 : 0).\u0023\u003Dz30RSSSygABj_<DefaultTickLabel>(new Action<DefaultTickLabel>(this.\u0023\u003DzTOyx6CMtEQYGanOVx2vhbhA\u003D));
      }
    }
    return _param1;
  }

  private static void \u0023\u003Dz8fHvNrgO0UsN(
    DefaultTickLabel _param0)
  {
    _param0.Opacity = 1.0;
  }

  private static void \u0023\u003DzwV4XeywmVQIY(
    DefaultTickLabel _param0)
  {
    _param0.Opacity = 0.0;
  }

  private void \u0023\u003DzTOyx6CMtEQYGanOVx2vhbhA\u003D(
    DefaultTickLabel _param1)
  {
    TickLabelAxisCanvas.\u0023\u003DzwV4XeywmVQIY(_param1);
    this.\u0023\u003DzYkVi0tC6ZJOZw\u00246TxQ\u003D\u003D.Remove(_param1);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly TickLabelAxisCanvas.SomeClass34343383 SomeMethond0343 = new TickLabelAxisCanvas.SomeClass34343383();
    public static Func<DefaultTickLabel, int> \u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D;
    public static Func<IGrouping<int, DefaultTickLabel>, int> \u0023\u003DzDGaDWsF\u00243rnprEPEXA\u003D\u003D;

    internal int \u0023\u003DzBxO6u\u0024HlVKKWbdOt3RMd_j8\u003D(
      DefaultTickLabel _param1)
    {
      return _param1.\u0023\u003DzcWxBLkct8Sd5yemMlw\u003D\u003D();
    }

    internal int \u0023\u003Dzug\u0024q6sWpYdaQHtVbrSZZtJw\u003D(
      IGrouping<int, DefaultTickLabel> _param1)
    {
      return _param1.Key;
    }
  }

  private sealed class \u0023\u003Dzea\u0024y3Uu1m4CYxerxRw\u003D\u003D
  {
    public Rect \u0023\u003Dz1Nrh7CPGtaPVGj6s\u0024g\u003D\u003D;

    internal bool \u0023\u003Dz9Wax66ks0XlfNwfslg\u003D\u003D(
      DefaultTickLabel _param1)
    {
      return _param1.\u0023\u003DzLqLOI4\u0024kbsVKm2XaOGI9Rw8\u003D().IntersectsWith(this.\u0023\u003Dz1Nrh7CPGtaPVGj6s\u0024g\u003D\u003D);
    }
  }
}
