// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.BoxAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

public class BoxAnnotation : AnnotationBase
{
  public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof (CornerRadius), typeof (CornerRadius), typeof (BoxAnnotation), new PropertyMetadata((object) new CornerRadius()));

  public BoxAnnotation() => this.DefaultStyleKey = (object) typeof (BoxAnnotation);

  public CornerRadius CornerRadius
  {
    get => (CornerRadius) this.GetValue(BoxAnnotation.CornerRadiusProperty);
    set => this.SetValue(BoxAnnotation.CornerRadiusProperty, (object) value);
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Border>("PART_BoxAnnotationRoot");
  }

  protected override Cursor GetSelectedCursor() => Cursors.SizeAll;

  public override bool IsPointWithinBounds(Point point)
  {
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    if (this.XAxis == null || this.YAxis == null)
      return false;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis.GetCurrentCoordinateCalculator();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis.GetCurrentCoordinateCalculator();
    AnnotationCoordinates coordinates = this.GetCoordinates(canvas, xCalc, yCalc);
    return new Rect(new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE), new Point(coordinates.\u0023\u003Dz6aJoeqoqAzym, coordinates.\u0023\u003DzWp13vlQiZCJc)).Contains(point);
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new BoxAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new BoxAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  private static void PlaceBoxAnnotation(
    BoxAnnotation annotation,
    AnnotationCoordinates coordinates)
  {
    double zS2K6sVvd5Iy = coordinates.\u0023\u003DzS2_K6sVvd5IY;
    double z6aJoeqoqAzym = coordinates.\u0023\u003Dz6aJoeqoqAzym;
    double z2J4l3QuGwZhe = coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    double zWp13vlQiZcJc = coordinates.\u0023\u003DzWp13vlQiZCJc;
    if (z6aJoeqoqAzym < zS2K6sVvd5Iy)
      NumberUtil.Swap(ref zS2K6sVvd5Iy, ref z6aJoeqoqAzym);
    if (zWp13vlQiZcJc < z2J4l3QuGwZhe)
      NumberUtil.Swap(ref z2J4l3QuGwZhe, ref zWp13vlQiZcJc);
    annotation.Width = z6aJoeqoqAzym - zS2K6sVvd5Iy + 1.0;
    annotation.Height = zWp13vlQiZcJc - z2J4l3QuGwZhe + 1.0;
    Canvas.SetLeft((UIElement) annotation, zS2K6sVvd5Iy);
    Canvas.SetTop((UIElement) annotation, z2J4l3QuGwZhe);
  }

  public sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(BoxAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<BoxAnnotation>(_param1)
  {
    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      return new Point[4]
      {
        new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE),
        new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003Dz2J4l3QUGwZHE),
        new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc),
        new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003DzWp13vlQiZCJc)
      };
    }

    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
      BoxAnnotation.PlaceBoxAnnotation(this.\u0023\u003Dz_iIh83yfe01U(), _param1);
    }
  }

  public sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(BoxAnnotation _param1) : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<BoxAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
      _param1 = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
      BoxAnnotation.PlaceBoxAnnotation(this.\u0023\u003Dz_iIh83yfe01U(), _param1);
    }

    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
    {
      if (_param2 == 1)
        _param2 = 2;
      base.\u0023\u003DzzNonn\u0024lG8ddm(_param1, _param2);
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      AnnotationCoordinates nnpojF4sCpkA8pp0g = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
      return new Point[2]
      {
        new Point(nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE),
        new Point(nnpojF4sCpkA8pp0g.\u0023\u003Dz6aJoeqoqAzym, nnpojF4sCpkA8pp0g.\u0023\u003DzWp13vlQiZCJc)
      };
    }
  }
}
