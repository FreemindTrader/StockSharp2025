// Decompiled with JetBrains decompiler
// Type: #=zSseiGdgwJmJ1pkmz7CEFfwuk7mZpVT219h_NwMrKXT5sYdAawRHD8Yx_rVjVyiLHpg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
internal sealed class \u0023\u003DzSseiGdgwJmJ1pkmz7CEFfwuk7mZpVT219h_NwMrKXT5sYdAawRHD8Yx_rVjVyiLHpg\u003D\u003D : 
  \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D
{
  private readonly IChartModifier \u0023\u003Dz0mppXFo\u003D;
  private Shape \u0023\u003DzmySr2zE\u003D;

  public \u0023\u003DzSseiGdgwJmJ1pkmz7CEFfwuk7mZpVT219h_NwMrKXT5sYdAawRHD8Yx_rVjVyiLHpg\u003D\u003D(
    IChartModifier _param1)
  {
    this.\u0023\u003Dz0mppXFo\u003D = _param1;
  }

  public double \u0023\u003DzBBczPQeswUUn(Point _param1, Point _param2)
  {
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(_param1, _param2);
  }

  public Shape \u0023\u003Dz5G3eIQ8\u003D(Brush _param1, Brush _param2, DoubleCollection _param3)
  {
    Rectangle rectangle = new Rectangle();
    rectangle.Fill = _param1;
    rectangle.Stroke = _param2;
    rectangle.StrokeDashArray = _param3;
    this.\u0023\u003DzmySr2zE\u003D = (Shape) rectangle;
    return this.\u0023\u003DzmySr2zE\u003D;
  }

  public Point \u0023\u003DzuPC9WaE\u003D(bool _param1, Point _param2, Point _param3)
  {
    if (_param1)
    {
      if (this.\u0023\u003Dz0mppXFo\u003D.XAxis.IsHorizontalAxis)
      {
        _param2.Y = 0.0;
        _param3.Y = this.\u0023\u003Dz0mppXFo\u003D.ModifierSurface.ActualHeight;
      }
      else
      {
        _param2.X = 0.0;
        _param3.X = this.\u0023\u003Dz0mppXFo\u003D.ModifierSurface.ActualWidth;
      }
    }
    _param3 = new Rect(0.0, 0.0, this.\u0023\u003Dz0mppXFo\u003D.ModifierSurface.ActualWidth, this.\u0023\u003Dz0mppXFo\u003D.ModifierSurface.ActualHeight).\u0023\u003Dz2xcanLolC38g(_param3);
    Rect rect = new Rect(_param2, _param3);
    Canvas.SetLeft((UIElement) this.\u0023\u003DzmySr2zE\u003D, rect.X);
    Canvas.SetTop((UIElement) this.\u0023\u003DzmySr2zE\u003D, rect.Y);
    this.\u0023\u003DzmySr2zE\u003D.Width = rect.Width;
    this.\u0023\u003DzmySr2zE\u003D.Height = rect.Height;
    return _param3;
  }

  public void \u0023\u003Dzz11HY86OhYRe(bool _param1, Point _param2, Point _param3)
  {
    this.\u0023\u003DzuPC9WaE\u003D(_param1, _param2, _param3);
  }
}
