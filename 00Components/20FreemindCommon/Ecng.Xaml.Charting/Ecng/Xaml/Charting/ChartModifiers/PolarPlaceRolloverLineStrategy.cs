// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PolarPlaceRolloverLineStrategy
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Shapes;
namespace fx.Xaml.Charting
{
    internal class PolarPlaceRolloverLineStrategy : IPlaceRolloverLineStrategy
    {
        private readonly IChartModifier _modifier;

        public PolarPlaceRolloverLineStrategy( IChartModifier modifier )
        {
            this._modifier = modifier;
        }

        public Line ShowVerticalLine( Point hitPoint, bool isVerticalChart )
        {
            Line line = (Line) null;
            if ( hitPoint.Y.IsDefined() && hitPoint.X.IsDefined() )
            {
                line = new Line();
                ITransformationStrategy transformationStrategy = this._modifier.Services.GetService<IStrategyManager>().GetTransformationStrategy();
                Point point1 = transformationStrategy.Transform(hitPoint);
                Point point2 = transformationStrategy.ReverseTransform(new Point(point1.X, 0.0));
                Point point3 = transformationStrategy.ReverseTransform(new Point(point1.X, transformationStrategy.ViewportSize.Height));
                line.X1 = point2.X;
                line.X2 = point3.X;
                line.Y1 = point2.Y;
                line.Y2 = point3.Y;
            }
            return line;
        }
    }
}
