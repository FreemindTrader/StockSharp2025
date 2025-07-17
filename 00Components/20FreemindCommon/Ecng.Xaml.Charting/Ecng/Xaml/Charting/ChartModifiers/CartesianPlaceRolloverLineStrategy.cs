// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.CartesianPlaceRolloverLineStrategy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Shapes;
namespace Ecng.Xaml.Charting
{
    internal class CartesianPlaceRolloverLineStrategy : IPlaceRolloverLineStrategy
    {
        private readonly IChartModifier _modifier;

        public CartesianPlaceRolloverLineStrategy( IChartModifier modifier )
        {
            this._modifier = modifier;
        }

        public Line ShowVerticalLine( Point hitPoint, bool isVerticalChart )
        {
            Line line = (Line) null;
            if ( hitPoint.Y.IsDefined() & isVerticalChart || hitPoint.X.IsDefined() && !isVerticalChart )
            {
                line = new Line();
                if ( isVerticalChart )
                {
                    line.X1 = 0.0;
                    line.X2 = this._modifier.ModifierSurface.ActualWidth;
                    line.Y1 = hitPoint.Y;
                    line.Y2 = hitPoint.Y;
                }
                else
                {
                    line.X1 = hitPoint.X;
                    line.X2 = hitPoint.X;
                    line.Y1 = 0.0;
                    line.Y2 = this._modifier.ModifierSurface.ActualHeight;
                }
            }
            return line;
        }
    }
}
