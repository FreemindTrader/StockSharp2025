// Decompiled with JetBrains decompiler
// Type: -.RadialPanel
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class RadialPanel : Panel
{
  
  public static readonly DependencyProperty \u0023\u003DzyrnkYrk\u003D = DependencyProperty.RegisterAttached("Angle", typeof (double), typeof (RadialPanel), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(RadialPanel.\u0023\u003DzASQCMKR2osYf)));
  
  public static readonly DependencyProperty \u0023\u003DzN6gpRZCZuo6v = DependencyProperty.RegisterAttached("IsHorizontal", typeof (bool), typeof (RadialPanel), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003Dz0nBvgzfnFekE = DependencyProperty.RegisterAttached("OriginPoint", typeof (Point), typeof (RadialPanel), new PropertyMetadata((object) new Point(), new PropertyChangedCallback(RadialPanel.\u0023\u003DzASQCMKR2osYf)));

  protected override Size MeasureOverride(Size _param1)
  {
    Size availableSize = this.\u0023\u003DzY_9dol4\u003D(_param1.Width, _param1.Height);
    availableSize.Width /= 2.0;
    availableSize.Height /= 2.0;
    double val1 = 0.0;
    foreach (UIElement uiElement in this.Children.OfType<UIElement>().Where<UIElement>(RadialPanel.SomeClass34343383.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D ?? (RadialPanel.SomeClass34343383.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D = new Func<UIElement, bool>(RadialPanel.SomeClass34343383.SomeMethond0343.\u0023\u003DzAXvWjuOKFfot_ecaioqcTKc\u003D))))
    {
      uiElement.Measure(availableSize);
      val1 = Math.Max(val1, Math.Max(uiElement.DesiredSize.Width, uiElement.DesiredSize.Height));
    }
    double num = val1 * 2.0;
    return new Size(num, num);
  }

  private Size \u0023\u003DzY_9dol4\u003D(double _param1, double _param2)
  {
    _param1 = Math.Max(_param1, 0.0);
    _param2 = Math.Max(_param2, 0.0);
    return new Size(_param1, _param2);
  }

  protected override Size ArrangeOverride(Size _param1)
  {
    Point point1 = new Point(_param1.Width / 2.0, _param1.Height / 2.0);
    foreach (UIElement uiElement in this.Children.OfType<UIElement>().Where<UIElement>(RadialPanel.SomeClass34343383.\u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D ?? (RadialPanel.SomeClass34343383.\u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D = new Func<UIElement, bool>(RadialPanel.SomeClass34343383.SomeMethond0343.\u0023\u003Dz__7MkXHypl\u0024neQctv2M9Xq4\u003D))))
    {
      Size desiredSize;
      Size size1;
      if (!RadialPanel.GetIsHorizontal(uiElement))
      {
        desiredSize = uiElement.DesiredSize;
        size1 = new Size(desiredSize.Width, point1.Y);
      }
      else
      {
        double x = point1.X;
        desiredSize = uiElement.DesiredSize;
        double height = desiredSize.Height;
        size1 = new Size(x, height);
      }
      Size size2 = size1;
      Point originPoint = RadialPanel.GetOriginPoint(uiElement);
      Point point2 = new Point(point1.X, point1.Y);
      point2.X -= size2.Width * originPoint.X;
      point2.Y -= size2.Height * originPoint.Y;
      double angle = RadialPanel.GetAngle(uiElement);
      RotateTransform rotateTransform = new RotateTransform()
      {
        Angle = angle,
        CenterX = originPoint.X,
        CenterY = originPoint.Y
      };
      uiElement.RenderTransform = (Transform) rotateTransform;
      uiElement.RenderTransformOrigin = originPoint;
      uiElement.Arrange(new Rect(point2, size2));
    }
    return _param1;
  }

  public static double GetAngle(UIElement _param0)
  {
    return (double) _param0.GetValue(RadialPanel.\u0023\u003DzyrnkYrk\u003D);
  }

  public static void SetAngle(UIElement _param0, double _param1)
  {
    _param0.SetValue(RadialPanel.\u0023\u003DzyrnkYrk\u003D, (object) _param1);
  }

  public static void SetOriginPoint(UIElement _param0, Point _param1)
  {
    _param0.SetValue(RadialPanel.\u0023\u003Dz0nBvgzfnFekE, (object) _param1);
  }

  public static Point GetOriginPoint(UIElement _param0)
  {
    return (Point) _param0.GetValue(RadialPanel.\u0023\u003Dz0nBvgzfnFekE);
  }

  public static void SetIsHorizontal(UIElement _param0, bool _param1)
  {
    _param0.SetValue(RadialPanel.\u0023\u003DzN6gpRZCZuo6v, (object) _param1);
  }

  public static bool GetIsHorizontal(UIElement _param0)
  {
    return (bool) _param0.GetValue(RadialPanel.\u0023\u003DzN6gpRZCZuo6v);
  }

  private static void \u0023\u003DzASQCMKR2osYf(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is UIElement reference) || !(VisualTreeHelper.GetParent((DependencyObject) reference) is RadialPanel parent))
      return;
    parent.InvalidateArrange();
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly RadialPanel.SomeClass34343383 SomeMethond0343 = new RadialPanel.SomeClass34343383();
    public static Func<UIElement, bool> \u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D;
    public static Func<UIElement, bool> \u0023\u003DzS6LQN8LYhmIH4nSLVw\u003D\u003D;

    internal bool \u0023\u003DzAXvWjuOKFfot_ecaioqcTKc\u003D(UIElement _param1)
    {
      return _param1.\u0023\u003DzST\u0024t7rI\u003D();
    }

    internal bool \u0023\u003Dz__7MkXHypl\u0024neQctv2M9Xq4\u003D(UIElement _param1)
    {
      return _param1.\u0023\u003DzST\u0024t7rI\u003D();
    }
  }
}
