// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.StackedMountainsWrapper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace fx.Xaml.Charting
{
    internal class StackedMountainsWrapper : StackedSeriesWrapperBase<IStackedMountainRenderableSeries>, IStackedMountainsWrapper, IStackedSeriesWrapperBase<IStackedMountainRenderableSeries>
    {
        public override void DrawStackedSeries( IRenderContext2D renderContext )
        {
            if ( ++this.Counter != this.SeriesCollection.Count<IStackedMountainRenderableSeries>( ( Func<IStackedMountainRenderableSeries, bool> ) ( x => x.IsVisible ) ) )
                return;
            this.Counter = 0;
            List<IStackedMountainRenderableSeries> list = this.SeriesCollection.Where<IStackedMountainRenderableSeries>((Func<IStackedMountainRenderableSeries, bool>) (x => x.IsVisible)).ToList<IStackedMountainRenderableSeries>();
            if ( !list.Any<IStackedMountainRenderableSeries>() )
                return;
            bool isPreviousSeriesDigital = false;
            for ( int index = list.Count - 1 ; index >= 0 ; --index )
            {
                if ( index > 0 )
                    isPreviousSeriesDigital = list[ index - 1 ].IsDigitalLine;
                list[ index ].DrawMountain( renderContext, isPreviousSeriesDigital );
            }
        }

        public bool IsHitTest( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, Tuple<IComparable, IComparable> hitDataValue, IStackedMountainRenderableSeries series )
        {
            if ( !double.IsNaN( nearestHitResult.YValue.ToDouble() ) )
            {
                int dataSeriesIndex = nearestHitResult.DataSeriesIndex;
                Point point = new Point(hitDataValue.Item1.ToDouble(), hitDataValue.Item2.ToDouble());
                Tuple<double, double> tuple1 = this.AccumulateYValueAtX(series, dataSeriesIndex, false);
                Tuple<double, double> tuple2 = this.AccumulateYValueAtX(series, dataSeriesIndex + 1, false);
                double x1 = ((IComparable) series.DataSeries.XValues[dataSeriesIndex]).ToDouble();
                double x2 = ((IComparable) series.DataSeries.XValues[dataSeriesIndex + 1]).ToDouble();
                PointUtil.Line firstLine1 = new PointUtil.Line(new Point(x1, tuple1.Item1), new Point(x2, tuple2.Item1));
                PointUtil.Line firstLine2 = new PointUtil.Line(new Point(x1, tuple1.Item2), new Point(x2, tuple2.Item2));
                PointUtil.Line secondLine = new PointUtil.Line(new Point(point.X, Math.Min(tuple1.Item2, tuple2.Item2)), new Point(point.X, Math.Max(tuple1.Item1, tuple2.Item1)));
                Point intersectionPoint1;
                PointUtil.LineIntersection2D( firstLine1, secondLine, out intersectionPoint1 );
                Point intersectionPoint2;
                PointUtil.LineIntersection2D( firstLine2, secondLine, out intersectionPoint2 );
                double y1 = intersectionPoint1.Y;
                double y2 = intersectionPoint2.Y;
                if ( y1 < y2 )
                    NumberUtil.Swap( ref y1, ref y2 );
                nearestHitResult.IsHit = point.Y > y2 && point.Y < y1;
            }
            return nearestHitResult.IsHit;
        }
    }
}
