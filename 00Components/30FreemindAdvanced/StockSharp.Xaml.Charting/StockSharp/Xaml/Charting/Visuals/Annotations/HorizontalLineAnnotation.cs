// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.HorizontalLineAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Charting;
using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

[System.Windows.Markup.ContentProperty("AnnotationLabels")]
[TemplatePart(Name = "PART_LineAnnotationRoot", Type = typeof (Grid))]
internal class HorizontalLineAnnotation : LineAnnotationWithLabelsBase
{
  public new static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register("", typeof (HorizontalAlignment), typeof (HorizontalLineAnnotation), new PropertyMetadata((object) HorizontalAlignment.Left, new PropertyChangedCallback(HorizontalLineAnnotation.OnHorizontalAlignmentChanged)));
  public static readonly DependencyProperty YDragStepProperty = DependencyProperty.Register("", typeof (double), typeof (HorizontalLineAnnotation), new PropertyMetadata((object) 0.0));

  public HorizontalLineAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (HorizontalLineAnnotation);
  }

  public new HorizontalAlignment HorizontalAlignment
  {
    get
    {
      return (HorizontalAlignment) this.GetValue(HorizontalLineAnnotation.HorizontalAlignmentProperty);
    }
    set => this.SetValue(HorizontalLineAnnotation.HorizontalAlignmentProperty, (object) value);
  }

  public double YDragStep
  {
    get => (double) this.GetValue(HorizontalLineAnnotation.YDragStepProperty);
    set => this.SetValue(HorizontalLineAnnotation.YDragStepProperty, (object) value);
  }

  public override IAxis GetUsedAxis()
  {
    IAxis usedAxis = (IAxis) null;
    if (this.XAxis != null)
      usedAxis = this.XAxis.IsHorizontalAxis ? this.YAxis : this.XAxis;
    else if (this.YAxis != null)
      usedAxis = this.YAxis.IsHorizontalAxis ? this.XAxis : this.YAxis;
    return usedAxis;
  }

  internal override LabelPlacement ResolveAutoPlacement()
  {
    LabelPlacement labelPlacement = LabelPlacement.Top;
    if (this.HorizontalAlignment == HorizontalAlignment.Right)
      labelPlacement = LabelPlacement.TopRight;
    if (this.HorizontalAlignment == HorizontalAlignment.Left)
      labelPlacement = LabelPlacement.TopLeft;
    if (this.HorizontalAlignment == HorizontalAlignment.Center)
      labelPlacement = LabelPlacement.Top;
    if (this.HorizontalAlignment == HorizontalAlignment.Stretch)
      labelPlacement = LabelPlacement.Axis;
    return labelPlacement;
  }

  protected override (double fixedHOffset, double fixedVOffset) MoveAnnotationTo(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    double horizOffset,
    double vertOffset)
  {
    IAxis usedAxis = this.GetUsedAxis();
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    double coord = coordinates.\u0023\u003Dz2J4l3QUGwZHE + vertOffset;
    if (!this.IsCoordinateValid(coord, canvas.ActualHeight))
    {
      if (coord < 0.0)
        vertOffset -= coord - 1.0;
      if (coord > canvas.ActualHeight)
        vertOffset -= coord - (canvas.ActualHeight - 1.0);
      coord = coordinates.\u0023\u003Dz2J4l3QUGwZHE + vertOffset;
    }
    if (this.YDragStep > 0.0 && !usedAxis.IsHorizontalAxis && !usedAxis.\u0023\u003DzFrVmckt\u0024NpG6())
    {
      IComparable comparable1 = this.FromCoordinate(coordinates.\u0023\u003Dz2J4l3QUGwZHE, usedAxis);
      IComparable comparable2 = this.FromCoordinate(coord, usedAxis);
      int num1 = (int) (Math.Abs(comparable1.ToDouble() - comparable2.ToDouble()) / this.YDragStep);
      int num2 = !usedAxis.get_FlipCoordinates() ? -Math.Sign(vertOffset) : Math.Sign(vertOffset);
      coord = this.ToCoordinate((IComparable) (comparable1.ToDouble() + (double) (num2 * num1) * this.YDragStep), usedAxis);
      vertOffset = coord - coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    }
    if (this.IsCoordinateValid(coord, canvas.ActualHeight))
      base.SetBasePoint(new Point()
      {
        X = coordinates.\u0023\u003DzS2_K6sVvd5IY,
        Y = coord
      }, 0, this.XAxis, this.YAxis);
    return (horizOffset, vertOffset);
  }

  protected override void GetPropertiesFromIndex(
    int index,
    out DependencyProperty X,
    out DependencyProperty Y)
  {
    X = AnnotationBase.X1Property;
    Y = AnnotationBase.Y1Property;
    switch (this.HorizontalAlignment)
    {
      case HorizontalAlignment.Left:
        X = AnnotationBase.X2Property;
        break;
      case HorizontalAlignment.Center:
        X = index == 0 ? AnnotationBase.X1Property : AnnotationBase.X2Property;
        break;
      case HorizontalAlignment.Right:
        X = AnnotationBase.X1Property;
        break;
    }
  }

  protected override void SetBasePoint(
    Point newPoint,
    int index,
    IAxis xAxis,
    IAxis yAxis)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    IComparable[] comparableArray = this.FromCoordinates(newPoint);
    IComparable comparable1 = comparableArray[0];
    IComparable comparable2 = comparableArray[1];
    DependencyProperty x;
    DependencyProperty y;
    this.GetPropertiesFromIndex(index, out x, out y);
    bool flag = !this.XAxis.IsHorizontalAxis;
    if (!this.IsCoordinateValid(newPoint.X, canvas.ActualWidth))
      return;
    if (flag)
      this.SetCurrentValue(y, (object) comparable2);
    else
      this.SetCurrentValue(x, (object) comparable1);
  }

  private static void OnHorizontalAlignmentChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    if (!(d is HorizontalLineAnnotation horizontalLineAnnotation))
      return;
    horizontalLineAnnotation.Refresh();
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new HorizontalLineAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new HorizontalLineAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  internal new sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(
    HorizontalLineAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<HorizontalLineAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.\u0023\u003Dz_iIh83yfe01U().GetCanvas(this.\u0023\u003Dz_iIh83yfe01U().AnnotationCanvas);
      double num1 = 0.0;
      double z2J4l3QuGwZhe = _param1.\u0023\u003Dz2J4l3QUGwZHE;
      if (!z2J4l3QuGwZhe.\u0023\u003Dz_Bj0HmLWq3hY() || canvas == null)
        return;
      double val1 = canvas.ActualWidth;
      switch (this.\u0023\u003Dz_iIh83yfe01U().HorizontalAlignment)
      {
        case HorizontalAlignment.Left:
          val1 = _param1.\u0023\u003DzS2_K6sVvd5IY.IsDefined() ? _param1.\u0023\u003DzS2_K6sVvd5IY : _param1.\u0023\u003Dz6aJoeqoqAzym;
          break;
        case HorizontalAlignment.Center:
          double num2 = Math.Min(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz6aJoeqoqAzym);
          double num3 = Math.Max(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz6aJoeqoqAzym);
          num1 = num2;
          double num4 = num2;
          val1 = num3 - num4;
          break;
        case HorizontalAlignment.Right:
          num1 = _param1.\u0023\u003DzS2_K6sVvd5IY.IsDefined() ? _param1.\u0023\u003DzS2_K6sVvd5IY : _param1.\u0023\u003Dz6aJoeqoqAzym;
          val1 -= num1;
          break;
      }
      this.\u0023\u003DzNUoYFVRHgzxB(num1, z2J4l3QuGwZhe, Math.Max(val1, 0.0), _param1.\u0023\u003DzjAUs6E8\u003D);
    }

    private void \u0023\u003DzNUoYFVRHgzxB(
      double _param1,
      double _param2,
      double _param3,
      double _param4)
    {
      Grid annotationRoot = this.\u0023\u003Dz_iIh83yfe01U().AnnotationRoot as Grid;
      this.\u0023\u003Dz_iIh83yfe01U().Width = _param3;
      double num1 = annotationRoot.RowDefinitions[0].ActualHeight + annotationRoot.RowDefinitions[1].ActualHeight / 2.0;
      double num2 = _param2 - (num1.\u0023\u003Dz_Bj0HmLWq3hY() ? num1 : 0.0);
      this.\u0023\u003Dz_iIh83yfe01U().SetValue(Canvas.LeftProperty, (object) _param1);
      this.\u0023\u003Dz_iIh83yfe01U().SetValue(Canvas.TopProperty, (object) num2);
      this.\u0023\u003Dz_iIh83yfe01U().TryPlaceAxisLabels(new Point(_param1, _param2 - _param4));
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      Point[] pointArray = (Point[]) null;
      switch (this.\u0023\u003Dz_iIh83yfe01U().HorizontalAlignment)
      {
        case HorizontalAlignment.Left:
          pointArray = new Point[1]
          {
            new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003Dz2J4l3QUGwZHE)
          };
          break;
        case HorizontalAlignment.Center:
          pointArray = new Point[2]
          {
            new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE),
            new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003Dz2J4l3QUGwZHE)
          };
          break;
        case HorizontalAlignment.Right:
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
      if (_param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 || _param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.ActualHeight)
      {
        flag = true;
      }
      else
      {
        switch (this.\u0023\u003Dz_iIh83yfe01U().HorizontalAlignment)
        {
          case HorizontalAlignment.Left:
            flag = _param1.\u0023\u003Dz6aJoeqoqAzym < 0.0;
            break;
          case HorizontalAlignment.Center:
            flag = _param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 && _param1.\u0023\u003Dz6aJoeqoqAzym < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.ActualWidth && _param1.\u0023\u003Dz6aJoeqoqAzym > _param2.ActualWidth;
            break;
          case HorizontalAlignment.Right:
            flag = _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.ActualWidth;
            break;
        }
      }
      return !flag;
    }
  }

  internal new sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<HorizontalLineAnnotation>
  {
    public \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(HorizontalLineAnnotation _param1)
      : base(_param1)
    {
      throw new InvalidOperationException(string.Format("", (object) ((object) _param1).GetType().Name));
    }
  }
}
