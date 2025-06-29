// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.LineAnnotationBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal abstract class LineAnnotationBase : AnnotationBase
{
  public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539343026), typeof (DoubleCollection), typeof (LineAnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434641), typeof (double), typeof (LineAnnotationBase), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(LineAnnotationBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzbxsQzTgivoh40ZJSBEuA2TI\u003D)));
  public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539434289), typeof (Brush), typeof (LineAnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));

  public Brush Stroke
  {
    get => (Brush) this.GetValue(LineAnnotationBase.StrokeProperty);
    set => this.SetValue(LineAnnotationBase.StrokeProperty, (object) value);
  }

  public double StrokeThickness
  {
    get => (double) this.GetValue(LineAnnotationBase.StrokeThicknessProperty);
    set => this.SetValue(LineAnnotationBase.StrokeThicknessProperty, (object) value);
  }

  public DoubleCollection StrokeDashArray
  {
    get => (DoubleCollection) this.GetValue(LineAnnotationBase.StrokeDashArrayProperty);
    set => this.SetValue(LineAnnotationBase.StrokeDashArrayProperty, (object) value);
  }

  protected override void GetPropertiesFromIndex(
    int index,
    out DependencyProperty x,
    out DependencyProperty y)
  {
    if (index == 0)
    {
      x = AnnotationBase.X1Property;
      y = AnnotationBase.Y1Property;
    }
    else
    {
      x = AnnotationBase.X2Property;
      y = AnnotationBase.Y2Property;
    }
  }

  protected override Cursor GetSelectedCursor() => Cursors.SizeAll;

  protected void MeasureRefresh()
  {
    ((DispatcherObject) this).Dispatcher.\u0023\u003DznvGFbrlLtrNN((Action) (() =>
    {
      FrameworkElement annotationRoot = this.AnnotationRoot;
      if (annotationRoot != null)
        annotationRoot.\u0023\u003DzI0WdlDcUgrX_();
      this.Refresh();
    }));
  }

  public override bool IsPointWithinBounds(Point point)
  {
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk canvas = this.GetCanvas(this.AnnotationCanvas);
    if (this.XAxis == null || this.YAxis == null)
      return false;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D coordinates = this.GetCoordinates(canvas, xCalc, yCalc);
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(point, new Point(coordinates.\u0023\u003DzS2_K6sVvd5IY, coordinates.\u0023\u003Dz2J4l3QUGwZHE), new Point(coordinates.\u0023\u003Dz6aJoeqoqAzym, coordinates.\u0023\u003DzWp13vlQiZCJc), true) < 7.07;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly LineAnnotationBase.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new LineAnnotationBase.\u0023\u003Dz7qOdpi4\u003D();

    internal void \u0023\u003DzbxsQzTgivoh40ZJSBEuA2TI\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((LineAnnotationBase) _param1).MeasureRefresh();
    }
  }
}
