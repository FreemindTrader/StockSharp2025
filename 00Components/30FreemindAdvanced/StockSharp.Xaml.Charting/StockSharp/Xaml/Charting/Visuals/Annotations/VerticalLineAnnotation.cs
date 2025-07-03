// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.VerticalLineAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using StockSharp.Charting;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

[System.Windows.Markup.ContentProperty("AnnotationLabels")]
[TemplatePart(Name = "PART_LineAnnotationRoot", Type = typeof (Grid))]
internal class VerticalLineAnnotation : LineAnnotationWithLabelsBase
{
  public new static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register("", typeof (VerticalAlignment), typeof (VerticalLineAnnotation), new PropertyMetadata((object) VerticalAlignment.Stretch, new PropertyChangedCallback(VerticalLineAnnotation.OnVerticalAlignmentChanged)));
  public static readonly DependencyProperty LabelsOrientationProperty = DependencyProperty.Register("", typeof (Orientation), typeof (VerticalLineAnnotation), new PropertyMetadata((object) Orientation.Vertical, new PropertyChangedCallback(VerticalLineAnnotation.OnLabelsOrientationChanged)));

  public VerticalLineAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (VerticalLineAnnotation);
  }

  public Orientation LabelsOrientation
  {
    get => (Orientation) this.GetValue(VerticalLineAnnotation.LabelsOrientationProperty);
    set => this.SetValue(VerticalLineAnnotation.LabelsOrientationProperty, (object) value);
  }

  public new VerticalAlignment VerticalAlignment
  {
    get => (VerticalAlignment) this.GetValue(VerticalLineAnnotation.VerticalAlignmentProperty);
    set => this.SetValue(VerticalLineAnnotation.VerticalAlignmentProperty, (object) value);
  }

  private void ApplyOrientation(
    dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd label)
  {
    if (this.LabelsOrientation == Orientation.Horizontal)
    {
      label.RotationAngle = 0.0;
    }
    else
    {
      LabelPlacement labelPlacement = label.\u0023\u003DzLtJnA4CR3Exc().GetLabelPlacement(label);
      switch (labelPlacement)
      {
        case LabelPlacement.Bottom:
        case LabelPlacement.Top:
        case LabelPlacement.Axis:
          label.RotationAngle = 0.0;
          break;
        default:
          if (labelPlacement.\u0023\u003DzsJ2ryUw\u003D())
          {
            label.RotationAngle = 90.0;
            break;
          }
          if (!labelPlacement.\u0023\u003DzY_J1ez4\u003D() && labelPlacement != LabelPlacement.Center)
            break;
          label.RotationAngle = -90.0;
          break;
      }
    }
  }

  protected override void Attach(
    dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd label)
  {
    base.Attach(label);
    if (this.IsHidden)
      return;
    this.ApplyOrientation(label);
  }

  public override IAxis GetUsedAxis()
  {
    IAxis usedAxis = (IAxis) null;
    if (this.XAxis != null)
      usedAxis = this.XAxis.IsHorizontalAxis ? this.XAxis : this.YAxis;
    else if (this.YAxis != null)
      usedAxis = this.YAxis.IsHorizontalAxis ? this.YAxis : this.XAxis;
    return usedAxis;
  }

  protected override void ApplyPlacement(
    dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd label,
    LabelPlacement placement)
  {
    bool flag1 = placement.\u0023\u003DzEiJTKKs\u003D();
    bool flag2 = placement.\u0023\u003DzHnHjVLw\u003D();
    bool flag3 = placement.\u0023\u003DzY_J1ez4\u003D();
    if (placement.\u0023\u003DzsJ2ryUw\u003D() | flag3)
    {
      label.SetValue(Grid.RowProperty, (object) 1);
      label.SetValue(Grid.ColumnProperty, (object) (flag3 ? 0 : 2));
      label.VerticalAlignment = VerticalAlignment.Center;
      label.HorizontalAlignment = flag3 ? HorizontalAlignment.Right : HorizontalAlignment.Left;
      if (flag2)
        label.VerticalAlignment = VerticalAlignment.Bottom;
      if (!flag1)
        return;
      label.VerticalAlignment = VerticalAlignment.Top;
    }
    else
    {
      label.SetValue(Grid.RowProperty, (object) (flag2 ? 2 : (!flag1 ? 1 : 0)));
      label.SetValue(Grid.ColumnProperty, (object) 1);
      label.HorizontalAlignment = HorizontalAlignment.Center;
      label.VerticalAlignment = VerticalAlignment.Center;
    }
  }

  internal override LabelPlacement ResolveAutoPlacement()
  {
    LabelPlacement labelPlacement = LabelPlacement.Axis;
    if (this.VerticalAlignment == VerticalAlignment.Top)
      labelPlacement = LabelPlacement.Bottom;
    if (this.VerticalAlignment == VerticalAlignment.Center)
      labelPlacement = LabelPlacement.Right;
    if (this.VerticalAlignment == VerticalAlignment.Stretch)
      labelPlacement = LabelPlacement.Axis;
    return labelPlacement;
  }

  protected override Cursor GetSelectedCursor() => Cursors.SizeWE;

  protected override (double fixedHOffset, double fixedVOffset) MoveAnnotationTo(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    double horizOffset,
    double vertOffset)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    double coord = coordinates.\u0023\u003DzS2_K6sVvd5IY + horizOffset;
    if (!this.IsCoordinateValid(coord, canvas.ActualWidth))
    {
      if (coord < 0.0)
        horizOffset -= coord - 1.0;
      if (coord > canvas.ActualWidth)
        horizOffset -= coord - (canvas.ActualWidth - 1.0);
      coord = coordinates.\u0023\u003DzS2_K6sVvd5IY + horizOffset;
    }
    base.SetBasePoint(new Point()
    {
      X = coord,
      Y = coordinates.\u0023\u003Dz2J4l3QUGwZHE
    }, 0, this.XAxis, this.YAxis);
    return (horizOffset, vertOffset);
  }

  protected override void SetBasePoint(
    Point newPoint,
    int index,
    IAxis xAxis,
    IAxis yAxis)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    IComparable[] comparableArray = this.FromCoordinates(0.0, newPoint.Y);
    IComparable comparable1 = comparableArray[0];
    IComparable comparable2 = comparableArray[1];
    DependencyProperty x;
    DependencyProperty y;
    this.GetPropertiesFromIndex(index, out x, out y);
    bool flag = !this.XAxis.IsHorizontalAxis;
    if (!this.IsCoordinateValid(newPoint.Y, canvas.ActualHeight))
      return;
    if (flag)
      this.SetCurrentValue(x, (object) comparable1);
    else
      this.SetCurrentValue(y, (object) comparable2);
  }

  protected override void GetPropertiesFromIndex(
    int index,
    out DependencyProperty x,
    out DependencyProperty y)
  {
    x = AnnotationBase.X1Property;
    y = AnnotationBase.Y1Property;
    switch (this.VerticalAlignment)
    {
      case VerticalAlignment.Top:
        y = AnnotationBase.Y2Property;
        break;
      case VerticalAlignment.Center:
        y = index == 0 ? AnnotationBase.Y1Property : AnnotationBase.Y2Property;
        break;
      case VerticalAlignment.Bottom:
        y = AnnotationBase.Y1Property;
        break;
    }
  }

  private static void OnVerticalAlignmentChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    if (!(d is VerticalLineAnnotation verticalLineAnnotation))
      return;
    verticalLineAnnotation.Refresh();
  }

  private static void OnLabelsOrientationChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    if (!(d is VerticalLineAnnotation verticalLineAnnotation))
      return;
    foreach (dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd annotationLabel in (Collection<dje_zN5RZ9FJPADKV6EEGGFEMB6VJK4MEZ85NW8C52EKQS76HUN2_ejd>) verticalLineAnnotation.AnnotationLabels)
      verticalLineAnnotation.ApplyOrientation(annotationLabel);
    verticalLineAnnotation.MeasureRefresh();
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new VerticalLineAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new VerticalLineAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  internal new sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(
    VerticalLineAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<VerticalLineAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.\u0023\u003Dz_iIh83yfe01U().GetCanvas(this.\u0023\u003Dz_iIh83yfe01U().AnnotationCanvas);
      double num1 = 0.0;
      double zS2K6sVvd5Iy = _param1.\u0023\u003DzS2_K6sVvd5IY;
      if (!zS2K6sVvd5Iy.\u0023\u003Dz_Bj0HmLWq3hY() || canvas == null)
        return;
      double val1 = canvas.ActualHeight;
      switch (this.\u0023\u003Dz_iIh83yfe01U().VerticalAlignment)
      {
        case VerticalAlignment.Top:
          val1 = _param1.\u0023\u003Dz2J4l3QUGwZHE.IsFiniteNumber() ? _param1.\u0023\u003Dz2J4l3QUGwZHE : _param1.\u0023\u003DzWp13vlQiZCJc;
          break;
        case VerticalAlignment.Center:
          double num2 = Math.Min(_param1.\u0023\u003Dz2J4l3QUGwZHE, _param1.\u0023\u003DzWp13vlQiZCJc);
          double num3 = Math.Max(_param1.\u0023\u003Dz2J4l3QUGwZHE, _param1.\u0023\u003DzWp13vlQiZCJc);
          num1 = num2;
          double num4 = num2;
          val1 = num3 - num4;
          break;
        case VerticalAlignment.Bottom:
          num1 = _param1.\u0023\u003Dz2J4l3QUGwZHE.IsFiniteNumber() ? _param1.\u0023\u003Dz2J4l3QUGwZHE : _param1.\u0023\u003DzWp13vlQiZCJc;
          val1 -= num1;
          break;
      }
      this.\u0023\u003DzNUoYFVRHgzxB(zS2K6sVvd5Iy, num1, Math.Max(val1, 0.0), _param1.\u0023\u003DzN0WZcqs\u003D);
    }

    private void \u0023\u003DzNUoYFVRHgzxB(
      double _param1,
      double _param2,
      double _param3,
      double _param4)
    {
      Grid annotationRoot = this.\u0023\u003Dz_iIh83yfe01U().AnnotationRoot as Grid;
      this.\u0023\u003Dz_iIh83yfe01U().Height = _param3;
      double num1 = annotationRoot.ColumnDefinitions[0].ActualWidth + (double) (int) (annotationRoot.ColumnDefinitions[1].ActualWidth / 2.0);
      double num2 = _param1 - (num1.\u0023\u003Dz_Bj0HmLWq3hY() ? num1 : 0.0);
      this.\u0023\u003Dz_iIh83yfe01U().SetValue(Canvas.LeftProperty, (object) num2);
      this.\u0023\u003Dz_iIh83yfe01U().SetValue(Canvas.TopProperty, (object) _param2);
      this.\u0023\u003Dz_iIh83yfe01U().TryPlaceAxisLabels(new Point(_param1 - _param4, _param2));
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      Point[] pointArray = (Point[]) null;
      switch (this.\u0023\u003Dz_iIh83yfe01U().VerticalAlignment)
      {
        case VerticalAlignment.Top:
          pointArray = new Point[1]
          {
            new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003DzWp13vlQiZCJc)
          };
          break;
        case VerticalAlignment.Center:
          pointArray = new Point[2]
          {
            new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE),
            new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003DzWp13vlQiZCJc)
          };
          break;
        case VerticalAlignment.Bottom:
          pointArray = new Point[1]
          {
            new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE)
          };
          break;
      }
      return pointArray;
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param2)
    {
      bool flag = false;
      if (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.ActualWidth)
      {
        flag = true;
      }
      else
      {
        switch (this.\u0023\u003Dz_iIh83yfe01U().VerticalAlignment)
        {
          case VerticalAlignment.Top:
            flag = _param1.\u0023\u003DzWp13vlQiZCJc < 0.0;
            break;
          case VerticalAlignment.Center:
            flag = _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 && _param1.\u0023\u003DzWp13vlQiZCJc < 0.0 || _param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.ActualHeight && _param1.\u0023\u003DzWp13vlQiZCJc > _param2.ActualHeight;
            break;
          case VerticalAlignment.Bottom:
            flag = _param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.ActualHeight;
            break;
        }
      }
      return !flag;
    }
  }

  internal new sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<VerticalLineAnnotation>
  {
    public \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(VerticalLineAnnotation _param1)
      : base(_param1)
    {
      throw new InvalidOperationException(string.Format("", (object) ((object) _param1).GetType().Name));
    }
  }
}
