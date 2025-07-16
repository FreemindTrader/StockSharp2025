// Decompiled with JetBrains decompiler
// Type: #=z5CbAZMXp7dgzzBe$G3xhivTIORxM4pFaWkWD6T7Je4n45z8o20NaGXPXIGD_
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
public sealed class \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhivTIORxM4pFaWkWD6T7Je4n45z8o20NaGXPXIGD_ : 
  \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D
{
  private PathFigure \u0023\u003DzvdQ6jybVAi6X;
  private ArcSegment \u0023\u003DzaTvyDd\u0024bWr8t;
  private Path \u0023\u003Dz91ieiKM\u003D;
  private readonly \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D \u0023\u003DzyS9ysjWX7dyA;

  public \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhivTIORxM4pFaWkWD6T7Je4n45z8o20NaGXPXIGD_(
    IChartModifier _param1)
  {
    this.\u0023\u003DzyS9ysjWX7dyA = _param1.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>();
  }

  public double \u0023\u003DzBBczPQeswUUn(Point _param1, Point _param2)
  {
    return \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003DzexGnHaPDCcBP(ref _param1, ref _param2);
  }

  public Shape \u0023\u003Dz5G3eIQ8\u003D(Brush _param1, Brush _param2, DoubleCollection _param3)
  {
    PathGeometry pathGeometry = new PathGeometry();
    this.\u0023\u003DzvdQ6jybVAi6X = new PathFigure();
    this.\u0023\u003DzaTvyDd\u0024bWr8t = new ArcSegment();
    this.\u0023\u003DzvdQ6jybVAi6X.Segments.Add((PathSegment) this.\u0023\u003DzaTvyDd\u0024bWr8t);
    pathGeometry.Figures.Add(this.\u0023\u003DzvdQ6jybVAi6X);
    Path path = new Path();
    path.Stroke = _param1;
    path.HorizontalAlignment = HorizontalAlignment.Left;
    path.VerticalAlignment = VerticalAlignment.Top;
    path.Data = (Geometry) pathGeometry;
    this.\u0023\u003Dz91ieiKM\u003D = path;
    return (Shape) this.\u0023\u003Dz91ieiKM\u003D;
  }

  public Point \u0023\u003DzuPC9WaE\u003D(bool _param1, Point _param2, Point _param3)
  {
    \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig1 = this.\u0023\u003DzyS9ysjWX7dyA.\u0023\u003DzhGnS3f5TTzO8();
    double num1;
    double num2;
    Point point1;
    Point point2;
    Point point3;
    if (_param1)
    {
      num1 = b99xo8DgCb3haWTig1.\u0023\u003Dz8DEW4l1E337F().Height / 2.0;
      num2 = num1;
      \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig2 = b99xo8DgCb3haWTig1;
      point1 = b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(_param2);
      Point point4 = new Point(point1.X, num1 / 2.0);
      point2 = b99xo8DgCb3haWTig2.\u0023\u003DzsTReN_n58EEf(point4);
      \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig3 = b99xo8DgCb3haWTig1;
      point1 = b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(_param3);
      Point point5 = new Point(point1.X, num1 / 2.0);
      point3 = b99xo8DgCb3haWTig3.\u0023\u003DzsTReN_n58EEf(point5);
    }
    else
    {
      Point point6 = b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(_param2);
      Point point7 = b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(_param3);
      int num3 = point6.Y > point7.Y ? 1 : 0;
      num2 = num3 != 0 ? point6.Y - point7.Y : point7.Y - point6.Y;
      num1 = num3 != 0 ? point6.Y : point7.Y;
      point2 = b99xo8DgCb3haWTig1.\u0023\u003DzsTReN_n58EEf(new Point(b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(_param2).X, num1 - num2 / 2.0));
      point3 = b99xo8DgCb3haWTig1.\u0023\u003DzsTReN_n58EEf(new Point(b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(_param3).X, num1 - num2 / 2.0));
    }
    point1 = b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(point2);
    double x1 = point1.X;
    point1 = b99xo8DgCb3haWTig1.\u0023\u003Dz8miGAzg\u003D(point3);
    double x2 = point1.X;
    double num4 = Math.Abs(x1 - x2);
    this.\u0023\u003DzvdQ6jybVAi6X.StartPoint = point2;
    this.\u0023\u003DzaTvyDd\u0024bWr8t.Size = new Size(num1 - num2 / 2.0, num1 - num2 / 2.0);
    this.\u0023\u003DzaTvyDd\u0024bWr8t.Point = point3;
    this.\u0023\u003DzaTvyDd\u0024bWr8t.RotationAngle = num4;
    this.\u0023\u003DzaTvyDd\u0024bWr8t.SweepDirection = x1 > x2 ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
    this.\u0023\u003DzaTvyDd\u0024bWr8t.IsLargeArc = num4 > 180.0;
    this.\u0023\u003Dz91ieiKM\u003D.StrokeThickness = num2;
    return _param3;
  }

  public void \u0023\u003Dzz11HY86OhYRe(bool _param1, Point _param2, Point _param3)
  {
    Canvas.SetLeft((UIElement) this.\u0023\u003Dz91ieiKM\u003D, 0.0);
    Canvas.SetTop((UIElement) this.\u0023\u003Dz91ieiKM\u003D, 0.0);
  }
}
