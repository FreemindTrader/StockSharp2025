// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.StrategyManager.PolarTransformationStrategy
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Common.Helpers;

namespace StockSharp.Xaml.Charting.StrategyManager
{
    internal class PolarTransformationStrategy : TransformationStrategyBase
    {
        private readonly PolarCartesianTransformationHelper _helper;

        public PolarTransformationStrategy( Size viewportSize )
          : base( viewportSize )
        {
            this._helper = new PolarCartesianTransformationHelper( viewportSize.Width, viewportSize.Height );
        }

        public override Point Transform( Point point )
        {
            return this._helper.ToPolar( point.X, point.Y );
        }

        public override Point ReverseTransform( Point point )
        {
            return this._helper.ToCartesian( point.X, point.Y );
        }
    }
}
