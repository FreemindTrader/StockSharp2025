// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.StrategyManager.CartesianTransformationStrategy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;

namespace Ecng.Xaml.Charting
{
    internal class CartesianTransformationStrategy : TransformationStrategyBase
    {
        public CartesianTransformationStrategy( Size viewportSize )
          : base( viewportSize )
        {
        }

        public override Point Transform( Point point )
        {
            return point;
        }

        public override Point ReverseTransform( Point point )
        {
            return point;
        }
    }
}
