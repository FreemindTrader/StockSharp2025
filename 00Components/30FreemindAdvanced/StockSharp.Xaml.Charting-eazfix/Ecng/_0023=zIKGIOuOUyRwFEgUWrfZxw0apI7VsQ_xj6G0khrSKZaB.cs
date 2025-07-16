// Decompiled with JetBrains decompiler
// Type: #=zIKGIOuOUyRwFEgUWrfZxw0apI7VsQ_xj6G0khrSKZaBu8dZ57eHRCM5HUPjp
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Shapes;

#nullable disable
internal sealed class \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw0apI7VsQ_xj6G0khrSKZaBu8dZ57eHRCM5HUPjp : 
  \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI84SlzGeHM63aaGG8vJyxtBAVakvxTUthBw\u003D
{
  private readonly IChartModifier \u0023\u003Dz0mppXFo\u003D;

  public \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw0apI7VsQ_xj6G0khrSKZaBu8dZ57eHRCM5HUPjp(
    IChartModifier _param1)
  {
    this.\u0023\u003Dz0mppXFo\u003D = _param1;
  }

  public Line \u0023\u003Dz7ftWcP9VIwGB(Point _param1, bool _param2)
  {
    Line line = (Line) null;
    if (_param1.Y.IsDefined() && _param1.X.IsDefined())
    {
      line = new Line();
      \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig = this.\u0023\u003Dz0mppXFo\u003D.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
      Point point1 = b99xo8DgCb3haWTig.\u0023\u003Dz8miGAzg\u003D(_param1);
      Point point2 = b99xo8DgCb3haWTig.\u0023\u003DzsTReN_n58EEf(new Point(point1.X, 0.0));
      Point point3 = b99xo8DgCb3haWTig.\u0023\u003DzsTReN_n58EEf(new Point(point1.X, b99xo8DgCb3haWTig.\u0023\u003Dz8DEW4l1E337F().Height));
      line.X1 = point2.X;
      line.X2 = point3.X;
      line.Y1 = point2.Y;
      line.Y2 = point3.Y;
    }
    return line;
  }
}
