// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.PolarPlaceRolloverLineStrategy
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.StrategyManager;

namespace StockSharp.Xaml.Charting.ChartModifiers
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
