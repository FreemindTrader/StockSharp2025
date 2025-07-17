// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.StrategyManager.IAnnotationPlacementStrategy
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Visuals.Annotations;

namespace StockSharp.Xaml.Charting.StrategyManager
{
    public interface IAnnotationPlacementStrategy
    {
        void PlaceAnnotation( AnnotationCoordinates coordinates );

        Point[ ] GetBasePoints( AnnotationCoordinates coordinates );

        void SetBasePoint( Point newPoint, int index );

        bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas annotationCanvas );

        void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas );
    }
}
