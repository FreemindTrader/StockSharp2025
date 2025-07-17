// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ISeriesDrawingHelper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
namespace fx.Xaml.Charting
{
    public interface ISeriesDrawingHelper
    {
        void DrawBox( Point pt1, Point pt2, IBrush2D pointFill, IPen2D pointPen, double gradientRotationAngle );

        void DrawLine( Point pt1, Point pt2, IPen2D pointPen );

        void FillPolygon( IBrush2D fillBrush, Point[ ] points );

        void DrawPoint( IPointMarker pt, Point point, IBrush2D brush, IPen2D pen );

        void DrawPoints( IPointMarker pt, IEnumerable<Point> points );

        void DrawQuad( IPen2D pen, Point pt1, Point pt2 );
    }
}
