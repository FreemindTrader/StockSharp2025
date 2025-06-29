// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.LineAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal class LineAnnotation : LineAnnotationBase
{
  private Line _line;
  private Line _ghostLine;

  public LineAnnotation() => this.DefaultStyleKey = (object) typeof (LineAnnotation);

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>("");
    this._line = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Line>("");
    this._ghostLine = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Line>("");
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new LineAnnotation.\u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new LineAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  private static void PlaceLineAnnotation(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates,
    LineAnnotation annotation)
  {
    annotation._line.X1 = coordinates.\u0023\u003DzS2_K6sVvd5IY;
    annotation._line.X2 = coordinates.\u0023\u003Dz6aJoeqoqAzym;
    annotation._line.Y1 = coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    annotation._line.Y2 = coordinates.\u0023\u003DzWp13vlQiZCJc;
    annotation._ghostLine.X1 = coordinates.\u0023\u003DzS2_K6sVvd5IY;
    annotation._ghostLine.X2 = coordinates.\u0023\u003Dz6aJoeqoqAzym;
    annotation._ghostLine.Y1 = coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    annotation._ghostLine.Y2 = coordinates.\u0023\u003DzWp13vlQiZCJc;
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

  internal sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(LineAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<LineAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      LineAnnotation.PlaceLineAnnotation(_param1, this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return LineAnnotation.CalculateBasePoints(_param1);
    }
  }

  internal sealed class \u0023\u003Dzzgx9mA6OUPz1eU6E9w\u003D\u003D(LineAnnotation _param1) : 
    AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<LineAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      LineAnnotation.PlaceLineAnnotation(this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1), this.\u0023\u003Dz_iIh83yfe01U());
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D _param1)
    {
      return LineAnnotation.CalculateBasePoints(this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1));
    }
  }
}
