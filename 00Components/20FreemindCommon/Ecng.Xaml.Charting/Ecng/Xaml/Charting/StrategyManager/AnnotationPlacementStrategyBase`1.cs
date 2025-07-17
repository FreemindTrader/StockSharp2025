// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.StrategyManager.AnnotationPlacementStrategyBase`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
namespace Ecng.Xaml.Charting
{
    internal abstract class AnnotationPlacementStrategyBase<T> : IAnnotationPlacementStrategy where T : AnnotationBase
    {
        private readonly T _annotation;

        protected AnnotationPlacementStrategyBase( T annotation )
        {
            this._annotation = annotation;
        }

        public T Annotation
        {
            get
            {
                return this._annotation;
            }
        }

        public abstract void PlaceAnnotation( AnnotationCoordinates coordinates );

        public abstract Point[ ] GetBasePoints( AnnotationCoordinates coordinates );

        public abstract void SetBasePoint( Point newPoint, int index );

        public abstract bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas annotationCanvas );

        public abstract void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas );
    }
}
