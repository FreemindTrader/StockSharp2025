// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.LineArrowAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal class LineArrowAnnotation : LineAnnotationBase
{
  public static readonly DependencyProperty HeadLengthProperty = DependencyProperty.Register(XXX.SSS(-539440830), typeof (double), typeof (LineArrowAnnotation), new PropertyMetadata((object) 4.0, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
  public static readonly DependencyProperty HeadWidthProperty = DependencyProperty.Register(XXX.SSS(-539440782), typeof (double), typeof (LineArrowAnnotation), new PropertyMetadata((object) 8.0, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
  private Line _line;
  private Line _ghostLine;
  private Polygon _arrowHead;

  public LineArrowAnnotation() => this.DefaultStyleKey = (object) typeof (LineArrowAnnotation);

  public double HeadLength
  {
    get => (double) this.GetValue(LineArrowAnnotation.HeadLengthProperty);
    set => this.SetValue(LineArrowAnnotation.HeadLengthProperty, (object) value);
  }

  public double HeadWidth
  {
    get => (double) this.GetValue(LineArrowAnnotation.HeadWidthProperty);
    set => this.SetValue(LineArrowAnnotation.HeadWidthProperty, (object) value);
  }

  public override void OnApplyTemplate()
  {
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>(XXX.SSS(-539342889));
    this._line = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Line>(XXX.SSS(-539342541));
    this._ghostLine = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Line>(XXX.SSS(-539343005));
    this._arrowHead = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Polygon>(XXX.SSS(-539342928));
  }

  public override bool IsPointWithinBounds(Point point)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    if (this.XAxis == null || this.YAxis == null)
      return false;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates = this.GetCoordinates(canvas, xCalc, yCalc);
    Point pt1 = new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE);
    Point pt2 = new Point(coordinates.\u0023\u003Dz6aJoeqoqAzym, coordinates.\u0023\u003DzWp13vlQiZCJc);
    Point[] headPoints = this.GetHeadPoints(pt1, pt2, this.HeadLength, this.HeadWidth);
    return base.IsPointWithinBounds(point) || \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003Dz_0KJG0n18I8E(point, headPoints[0], headPoints[1], pt2);
  }

  internal Point[] GetHeadPoints(Point pt1, Point pt2, double headLength, double headWidth)
  {
    double num1 = Math.Atan2(pt1.Y - pt2.Y, pt1.X - pt2.X);
    double num2 = Math.Sin(num1);
    double num3 = Math.Cos(num1);
    Point point1 = new Point(pt2.X + (this.HeadWidth * num3 - this.HeadLength * num2), pt2.Y + (this.HeadWidth * num2 + this.HeadLength * num3));
    Point point2 = new Point(pt2.X + (this.HeadWidth * num3 + this.HeadLength * num2), pt2.Y + (this.HeadWidth * num2 - this.HeadLength * num3));
    return new Point[3]{ pt2, point1, point2 };
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new LineArrowAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new LineArrowAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  private static void PlaceLineArrowAnnotation(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    LineArrowAnnotation annotation)
  {
    annotation._line.X1 = coordinates.\u0023\u003DzS2_K6sVvd5IY;
    annotation._line.X2 = coordinates.\u0023\u003Dz6aJoeqoqAzym;
    annotation._line.Y1 = coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    annotation._line.Y2 = coordinates.\u0023\u003DzWp13vlQiZCJc;
    annotation._ghostLine.X1 = coordinates.\u0023\u003DzS2_K6sVvd5IY;
    annotation._ghostLine.X2 = coordinates.\u0023\u003Dz6aJoeqoqAzym;
    annotation._ghostLine.Y1 = coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    annotation._ghostLine.Y2 = coordinates.\u0023\u003DzWp13vlQiZCJc;
    Point[] headPoints = annotation.GetHeadPoints(new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE), new Point(coordinates.\u0023\u003Dz6aJoeqoqAzym, coordinates.\u0023\u003DzWp13vlQiZCJc), annotation.HeadLength, annotation.HeadWidth);
    PointCollection pointCollection = new PointCollection()
    {
      headPoints[0],
      headPoints[1],
      headPoints[2]
    };
    annotation._arrowHead.Points = pointCollection;
  }

  private static Point[] CalculateBasePoints(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates)
  {
    return new Point[2]
    {
      new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE),
      new Point(coordinates.\u0023\u003Dz6aJoeqoqAzym, coordinates.\u0023\u003DzWp13vlQiZCJc)
    };
  }

  internal Line Line => this._line;

  internal Line GhostLine => this._ghostLine;

  internal Polygon ArrowHead => this._arrowHead;

  internal sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(LineArrowAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<LineArrowAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      LineArrowAnnotation.PlaceLineArrowAnnotation(_param1, this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return LineArrowAnnotation.CalculateBasePoints(_param1);
    }
  }

  internal sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(LineArrowAnnotation _param1) : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<LineArrowAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      LineArrowAnnotation.PlaceLineArrowAnnotation(this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1), this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return LineArrowAnnotation.CalculateBasePoints(this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1));
    }
  }
}
