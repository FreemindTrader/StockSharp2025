// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.PolarRubberBandOverlayPlacementStrategy
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.StrategyManager;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    internal class PolarRubberBandOverlayPlacementStrategy : IRubberBandOverlayPlacementStrategy
    {
        private PathFigure _pathFigure;
        private ArcSegment _arcSegment;
        private Path _path;
        private readonly IStrategyManager _strategyManager;

        public PolarRubberBandOverlayPlacementStrategy( IChartModifier modifier )
        {
            this._strategyManager = modifier.Services.GetService<IStrategyManager>();
        }

        public double CalculateDraggedDistance( Point start, Point end )
        {
            return PolarUtil.AngleDistance( ref start, ref end );
        }

        public Shape CreateShape( Brush rubberBandFill, Brush rubberBandStroke, DoubleCollection rubberBandStrokeDashArray )
        {
            PathGeometry pathGeometry = new PathGeometry();
            this._pathFigure = new PathFigure();
            this._arcSegment = new ArcSegment();
            this._pathFigure.Segments.Add( ( PathSegment ) this._arcSegment );
            pathGeometry.Figures.Add( this._pathFigure );
            Path path = new Path();
            path.Stroke = rubberBandFill;
            path.HorizontalAlignment = HorizontalAlignment.Left;
            path.VerticalAlignment = VerticalAlignment.Top;
            path.Data = ( Geometry ) pathGeometry;
            this._path = path;
            return ( Shape ) this._path;
        }

        public Point UpdateShape( bool isXAxisOnly, Point start, Point end )
        {
            ITransformationStrategy transformationStrategy1 = this._strategyManager.GetTransformationStrategy();
            double num1;
            double num2;
            Point point1;
            Point point2;
            Point point3;
            if ( isXAxisOnly )
            {
                num1 = transformationStrategy1.ViewportSize.Height / 2.0;
                num2 = num1;
                ITransformationStrategy transformationStrategy2 = transformationStrategy1;
                point1 = transformationStrategy1.Transform( start );
                Point point4 = new Point(point1.X, num1 / 2.0);
                point2 = transformationStrategy2.ReverseTransform( point4 );
                ITransformationStrategy transformationStrategy3 = transformationStrategy1;
                point1 = transformationStrategy1.Transform( end );
                Point point5 = new Point(point1.X, num1 / 2.0);
                point3 = transformationStrategy3.ReverseTransform( point5 );
            }
            else
            {
                Point point4 = transformationStrategy1.Transform(start);
                Point point5 = transformationStrategy1.Transform(end);
                bool flag = point4.Y > point5.Y;
                num2 = flag ? point4.Y - point5.Y : point5.Y - point4.Y;
                num1 = flag ? point4.Y : point5.Y;
                point2 = transformationStrategy1.ReverseTransform( new Point( transformationStrategy1.Transform( start ).X, num1 - num2 / 2.0 ) );
                point3 = transformationStrategy1.ReverseTransform( new Point( transformationStrategy1.Transform( end ).X, num1 - num2 / 2.0 ) );
            }
            point1 = transformationStrategy1.Transform( point2 );
            double x1 = point1.X;
            point1 = transformationStrategy1.Transform( point3 );
            double x2 = point1.X;
            double num3 = Math.Abs(x1 - x2);
            this._pathFigure.StartPoint = point2;
            this._arcSegment.Size = new Size( num1 - num2 / 2.0, num1 - num2 / 2.0 );
            this._arcSegment.Point = point3;
            this._arcSegment.RotationAngle = num3;
            this._arcSegment.SweepDirection = x1 > x2 ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
            this._arcSegment.IsLargeArc = num3 > 180.0;
            this._path.StrokeThickness = num2;
            return end;
        }

        public void SetupShape( bool isXAxisOnly, Point start, Point end )
        {
            Canvas.SetLeft( ( UIElement ) this._path, 0.0 );
            Canvas.SetTop( ( UIElement ) this._path, 0.0 );
        }
    }
}
