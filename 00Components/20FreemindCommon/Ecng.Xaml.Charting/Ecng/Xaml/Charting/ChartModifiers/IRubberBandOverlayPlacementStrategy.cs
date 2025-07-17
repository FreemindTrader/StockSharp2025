// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifiers.IRubberBandOverlayPlacementStrategy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    internal interface IRubberBandOverlayPlacementStrategy
    {
        double CalculateDraggedDistance( Point start, Point end );

        Shape CreateShape( Brush rubberBandFill, Brush rubberBandStroke, DoubleCollection rubberBandStrokeDashArray );

        Point UpdateShape( bool isXAxisOnly, Point start, Point end );

        void SetupShape( bool isXAxisOnly, Point start, Point end );
    }
}
