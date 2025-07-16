// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.AxisMarkerAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal class AxisMarkerAnnotation : AnchorPointAnnotation
{
  public static readonly DependencyProperty FormattedValueProperty = DependencyProperty.Register(nameof (FormattedValue), typeof (string), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
  public static readonly DependencyProperty MarkerPointWidthProperty = DependencyProperty.Register(nameof (MarkerPointWidth), typeof (double), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) 8.0));
  public static readonly DependencyProperty LabelTemplateProperty = DependencyProperty.Register(nameof (LabelTemplate), typeof (DataTemplate), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
  public static readonly DependencyProperty PointerTemplateProperty = DependencyProperty.Register(nameof (PointerTemplate), typeof (DataTemplate), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
  protected internal static readonly DependencyProperty AxisInfoProperty = DependencyProperty.Register(nameof (AxisInfo), typeof (\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));

  public AxisMarkerAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (AxisMarkerAnnotation);
    this.AnnotationCanvas = AnnotationCanvas.YAxis;
  }

  public string FormattedValue
  {
    get => (string) this.GetValue(AxisMarkerAnnotation.FormattedValueProperty);
    set => this.SetValue(AxisMarkerAnnotation.FormattedValueProperty, (object) value);
  }

  public double MarkerPointWidth
  {
    get => (double) this.GetValue(AxisMarkerAnnotation.MarkerPointWidthProperty);
    set => this.SetValue(AxisMarkerAnnotation.MarkerPointWidthProperty, (object) value);
  }

  public DataTemplate LabelTemplate
  {
    get => (DataTemplate) this.GetValue(AxisMarkerAnnotation.LabelTemplateProperty);
    set => this.SetValue(AxisMarkerAnnotation.LabelTemplateProperty, (object) value);
  }

  public DataTemplate PointerTemplate
  {
    get => (DataTemplate) this.GetValue(AxisMarkerAnnotation.PointerTemplateProperty);
    set => this.SetValue(AxisMarkerAnnotation.PointerTemplateProperty, (object) value);
  }

  public IAxis Axis
  {
    get
    {
      return this.AnnotationCanvas != AnnotationCanvas.YAxis ? this.XAxis : this.YAxis;
    }
  }

  public \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D AxisInfo
  {
    get
    {
      return (\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D) this.GetValue(AxisMarkerAnnotation.AxisInfoProperty);
    }
    private set => this.SetValue(AxisMarkerAnnotation.AxisInfoProperty, (object) value);
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.AnnotationRoot = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<FrameworkElement>("PART_AxisMarkerAnnotationRoot");
  }

  protected override void OnAxisAlignmentChanged(
    IAxis axis,
    AxisAlignment oldAlignment)
  {
    base.OnAxisAlignmentChanged(axis, oldAlignment);
    Cursor selectedCursor = this.GetSelectedCursor();
    this.SetCurrentValue(FrameworkElement.CursorProperty, (object) selectedCursor);
  }

  public override void Update(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordinateCalculator,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordinateCalculator)
  {
    base.Update(xCoordinateCalculator, yCoordinateCalculator);
    if (!(this.Axis is AxisBase axis))
      return;
    this.AxisInfo = axis.\u0023\u003DzjuB\u0024Pa8\u003D((IComparable) this.GetValue(this.GetBaseProperty()));
  }

  private DependencyProperty GetBaseProperty()
  {
    return this.AnnotationCanvas != AnnotationCanvas.XAxis ? AnnotationBase.Y1Property : AnnotationBase.X1Property;
  }

  protected override Cursor GetSelectedCursor()
  {
    return this.Axis == null || !this.Axis.IsHorizontalAxis ? Cursors.SizeNS : Cursors.SizeWE;
  }

  protected override double ToCoordinate(
    IComparable dataValue,
    double canvasMeasurement,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc,
    XyDirection direction)
  {
    return base.ToCoordinate(dataValue, canvasMeasurement, coordCalc, direction) - coordCalc.\u0023\u003DzV1bNkSgej_yk();
  }

  public override bool IsPointWithinBounds(Point point)
  {
    Grid annotationRoot = this.AnnotationRoot as Grid;
    point = this.ModifierSurface.TranslatePoint(point, (IHitTestable) this);
    Point point1 = point;
    return annotationRoot.\u0023\u003DzbOxVzAyGdX66(point1);
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new AxisMarkerAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new AxisMarkerAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  private static void PlaceMarker(
    IAxis axis,
    AxisMarkerAnnotation axisMarker,
    AnnotationCoordinates coordinates)
  {
    coordinates = axisMarker.GetAnchorAnnotationCoordinates(coordinates);
    Point point = new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE);
    AxisMarkerAnnotation.ClearAxisMarkerPlacement((FrameworkElement) axisMarker);
    if (axis == null)
      return;
    if (axis.IsHorizontalAxis)
    {
      DependencyProperty dependencyProperty = axis.get_AxisAlignment() == AxisAlignment.Bottom ? AxisCanvas.\u0023\u003DzZpWLYz8\u003D : AxisCanvas.\u0023\u003DzasJeVgQ\u003D;
      axisMarker.SetValue(dependencyProperty, (object) 0.0);
      AxisCanvas.SetLeft((UIElement) axisMarker, point.X);
    }
    else
    {
      DependencyProperty dependencyProperty = axis.get_AxisAlignment() == AxisAlignment.Right ? AxisCanvas.\u0023\u003DztX3bWaM\u003D : AxisCanvas.\u0023\u003DzQLHMxl4\u003D;
      axisMarker.SetValue(dependencyProperty, (object) 0.0);
      AxisCanvas.SetTop((UIElement) axisMarker, point.Y);
    }
  }

  private static void ClearAxisMarkerPlacement(FrameworkElement axisLabel)
  {
    axisLabel.SetValue(AxisCanvas.\u0023\u003DztX3bWaM\u003D, (object) double.NaN);
    axisLabel.SetValue(AxisCanvas.\u0023\u003DzQLHMxl4\u003D, (object) double.NaN);
    axisLabel.SetValue(AxisCanvas.\u0023\u003DzHEgPKfijwe68, (object) double.NaN);
    axisLabel.SetValue(AxisCanvas.\u0023\u003DzZpWLYz8\u003D, (object) double.NaN);
    axisLabel.SetValue(AxisCanvas.\u0023\u003DzasJeVgQ\u003D, (object) double.NaN);
  }

  private static double CalculateNewPosition(
    AxisMarkerAnnotation annotation,
    double currentPosition,
    double offset,
    double canvasSize)
  {
    double coord = currentPosition + offset;
    if (!annotation.IsCoordinateValid(coord, canvasSize))
    {
      if (coord < 0.0)
        coord = 0.0;
      if (coord > canvasSize)
        coord = canvasSize - 1.0;
    }
    return coord;
  }

  internal sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(AxisMarkerAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<AxisMarkerAnnotation>(_param1)
  {
    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      return AxisMarkerAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D.\u0023\u003Dz2j7EMwL\u0024YzHt(_param1, this.\u0023\u003Dz_iIh83yfe01U());
    }

    private static Point[] \u0023\u003Dz2j7EMwL\u0024YzHt(
      AnnotationCoordinates _param0,
      AxisMarkerAnnotation _param1)
    {
      _param0 = _param1.GetAnchorAnnotationCoordinates(_param0);
      _param1.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      IAxis axis = _param1.Axis;
      if (axis == null)
        return Array.Empty<Point>();
      IChartModifierSurface modifierSurface = _param1.ModifierSurface;
      if (axis.IsHorizontalAxis)
      {
        double width = _param1.DesiredSize.Width;
        double zS2K6sVvd5Iy = _param0.\u0023\u003DzS2_K6sVvd5IY;
        return new Point[4]
        {
          axis.TranslatePoint(new Point(zS2K6sVvd5Iy, 0.0), (IHitTestable) modifierSurface),
          axis.TranslatePoint(new Point(zS2K6sVvd5Iy, axis.ActualHeight), (IHitTestable) modifierSurface),
          axis.TranslatePoint(new Point(zS2K6sVvd5Iy + width, axis.ActualHeight), (IHitTestable) modifierSurface),
          axis.TranslatePoint(new Point(zS2K6sVvd5Iy + width, 0.0), (IHitTestable) modifierSurface)
        };
      }
      double height = _param1.DesiredSize.Height;
      double z2J4l3QuGwZhe = _param0.\u0023\u003Dz2J4l3QUGwZHE;
      return new Point[4]
      {
        axis.TranslatePoint(new Point(0.0, z2J4l3QuGwZhe), (IHitTestable) modifierSurface),
        axis.TranslatePoint(new Point(axis.ActualWidth, z2J4l3QuGwZhe), (IHitTestable) modifierSurface),
        axis.TranslatePoint(new Point(axis.ActualWidth, z2J4l3QuGwZhe + height), (IHitTestable) modifierSurface),
        axis.TranslatePoint(new Point(0.0, z2J4l3QuGwZhe + height), (IHitTestable) modifierSurface)
      };
    }

    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
      AxisMarkerAnnotation.PlaceMarker(this.\u0023\u003Dz_iIh83yfe01U().Axis, this.\u0023\u003Dz_iIh83yfe01U(), _param1);
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      AnnotationCoordinates _param1,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param2)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis == null)
        return false;
      bool flag;
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.IsHorizontalAxis)
      {
        double zS2K6sVvd5Iy = _param1.\u0023\u003DzS2_K6sVvd5IY;
        flag = zS2K6sVvd5Iy >= 0.0 && zS2K6sVvd5Iy <= _param2.ActualWidth;
      }
      else
      {
        double z2J4l3QuGwZhe = _param1.\u0023\u003Dz2J4l3QUGwZHE;
        flag = z2J4l3QuGwZhe >= 0.0 && z2J4l3QuGwZhe <= _param2.ActualHeight;
      }
      return flag;
    }

    protected override void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      AnnotationCoordinates _param1,
      ref double _param2,
      ref double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis == null)
        return;
      IComparable[] comparableArray = !this.\u0023\u003Dz_iIh83yfe01U().Axis.IsHorizontalAxis ? this.\u0023\u003DzvQ1aszE\u003D(0.0, AxisMarkerAnnotation.CalculateNewPosition(this.\u0023\u003Dz_iIh83yfe01U(), _param1.\u0023\u003Dz2J4l3QUGwZHE, _param3, _param4.ActualHeight)) : this.\u0023\u003DzvQ1aszE\u003D(AxisMarkerAnnotation.CalculateNewPosition(this.\u0023\u003Dz_iIh83yfe01U(), _param1.\u0023\u003DzS2_K6sVvd5IY, _param2, _param4.ActualWidth), 0.0);
      DependencyProperty baseProperty = this.\u0023\u003Dz_iIh83yfe01U().GetBaseProperty();
      IComparable comparable = this.\u0023\u003Dz_iIh83yfe01U().Axis.\u0023\u003DzFrVmckt\u0024NpG6() ? comparableArray[0] : comparableArray[1];
      this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(baseProperty, (object) comparable);
    }
  }

  internal sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<AxisMarkerAnnotation>
  {
    private readonly double \u0023\u003DzYklHyyiv14LN;

    public \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(AxisMarkerAnnotation _param1)
      : base(_param1)
    {
      this.\u0023\u003DzYklHyyiv14LN = \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8DEW4l1E337F());
    }

    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis == null)
        return;
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.\u0023\u003DzFrVmckt\u0024NpG6())
      {
        AxisMarkerAnnotation.ClearAxisMarkerPlacement((FrameworkElement) this.\u0023\u003Dz_iIh83yfe01U());
        AxisCanvas.SetCenterLeft((UIElement) this.\u0023\u003Dz_iIh83yfe01U(), _param1.\u0023\u003DzS2_K6sVvd5IY);
        AxisCanvas.SetBottom((UIElement) this.\u0023\u003Dz_iIh83yfe01U(), 0.0);
      }
      else
      {
        _param1.\u0023\u003DzS2_K6sVvd5IY = _param1.\u0023\u003Dz2J4l3QUGwZHE;
        AxisMarkerAnnotation.PlaceMarker(this.\u0023\u003Dz_iIh83yfe01U().Axis, this.\u0023\u003Dz_iIh83yfe01U(), _param1);
      }
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      AnnotationCoordinates _param1,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param2)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis == null)
        return false;
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.\u0023\u003DzFrVmckt\u0024NpG6())
        return true;
      bool flag;
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.IsHorizontalAxis)
      {
        double z2J4l3QuGwZhe = _param1.\u0023\u003Dz2J4l3QUGwZHE;
        flag = z2J4l3QuGwZhe >= 0.0 && z2J4l3QuGwZhe <= _param2.ActualWidth;
      }
      else
      {
        double z2J4l3QuGwZhe = _param1.\u0023\u003Dz2J4l3QUGwZHE;
        flag = z2J4l3QuGwZhe >= 0.0 && z2J4l3QuGwZhe <= _param2.ActualHeight;
      }
      return flag;
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis == null)
        return Array.Empty<Point>();
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.\u0023\u003DzFrVmckt\u0024NpG6())
      {
        AnnotationCoordinates nnpojF4sCpkA8pp0g = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
        return new Point[1]
        {
          new Point(nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE)
        };
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY = _param1.\u0023\u003Dz2J4l3QUGwZHE;
      _param1 = this.\u0023\u003Dz_iIh83yfe01U().GetAnchorAnnotationCoordinates(_param1);
      return AxisMarkerAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D.\u0023\u003Dz2j7EMwL\u0024YzHt(_param1, this.\u0023\u003Dz_iIh83yfe01U());
    }

    private static Point[] \u0023\u003Dz2j7EMwL\u0024YzHt(
      AnnotationCoordinates _param0,
      AxisMarkerAnnotation _param1)
    {
      _param1.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      IAxis axis = _param1.Axis;
      IChartModifierSurface modifierSurface = _param1.ModifierSurface;
      if (axis.IsHorizontalAxis)
      {
        double num1 = _param0.\u0023\u003DzS2_K6sVvd5IY + _param1.DesiredSize.Width / 2.0;
        double num2 = axis.get_AxisAlignment() == AxisAlignment.Top ? axis.ActualHeight : 0.0;
        return new Point[1]
        {
          axis.TranslatePoint(new Point(num1, num2), (IHitTestable) modifierSurface)
        };
      }
      double num3 = axis.get_AxisAlignment() == AxisAlignment.Left ? axis.ActualWidth : 0.0;
      double num4 = _param0.\u0023\u003Dz2J4l3QUGwZHE + _param1.DesiredSize.Height / 2.0;
      return new Point[1]
      {
        axis.TranslatePoint(new Point(num3, num4), (IHitTestable) modifierSurface)
      };
    }

    protected override Tuple<Point, Point> \u0023\u003DzQDA5x2uuH9m3(
      AnnotationCoordinates _param1,
      double _param2,
      double _param3)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.\u0023\u003DzFrVmckt\u0024NpG6())
        return base.\u0023\u003DzQDA5x2uuH9m3(_param1, _param2, _param3);
      Point point = new Point(_param2, _param3);
      return new Tuple<Point, Point>(point, point);
    }

    protected override AnnotationCoordinates \u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(
      AnnotationCoordinates _param1)
    {
      if (this.\u0023\u003Dz_iIh83yfe01U().Axis.\u0023\u003DzFrVmckt\u0024NpG6())
        _param1.\u0023\u003Dz2J4l3QUGwZHE = _param1.\u0023\u003DzWp13vlQiZCJc = this.\u0023\u003DzYklHyyiv14LN;
      return base.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
    }

    public override void \u0023\u003DzuPL3ELSPZybJ(
      AnnotationCoordinates _param1,
      double _param2,
      double _param3,
      \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk _param4)
    {
      IAxis axis = this.\u0023\u003Dz_iIh83yfe01U().Axis;
      if (axis.\u0023\u003DzFrVmckt\u0024NpG6())
      {
        double x = this.\u0023\u003DzQDA5x2uuH9m3(_param1, _param2, _param3).Item1.X;
        IComparable[] comparableArray = this.\u0023\u003DzvQ1aszE\u003D(AxisMarkerAnnotation.CalculateNewPosition(this.\u0023\u003Dz_iIh83yfe01U(), _param1.\u0023\u003DzS2_K6sVvd5IY, x, 360.0), 0.0);
        this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray[0]);
      }
      else
      {
        double coord = axis.IsHorizontalAxis ? AxisMarkerAnnotation.CalculateNewPosition(this.\u0023\u003Dz_iIh83yfe01U(), _param1.\u0023\u003Dz2J4l3QUGwZHE, _param2, _param4.ActualWidth) : AxisMarkerAnnotation.CalculateNewPosition(this.\u0023\u003Dz_iIh83yfe01U(), _param1.\u0023\u003Dz2J4l3QUGwZHE, _param3, _param4.ActualHeight);
        IComparable comparable = this.\u0023\u003Dz_iIh83yfe01U().FromCoordinate(coord, axis);
        this.\u0023\u003Dz_iIh83yfe01U().SetCurrentValue(AnnotationBase.Y1Property, (object) comparable);
      }
    }
  }
}
