// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.StrategyManager.TransformationStrategyBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;

namespace Ecng.Xaml.Charting.StrategyManager
{
    internal abstract class TransformationStrategyBase : ITransformationStrategy
    {
        public Size ViewportSize
        {
            get; private set;
        }

        protected TransformationStrategyBase( Size viewportSize )
        {
            this.ViewportSize = viewportSize;
        }

        public abstract Point Transform( Point point );

        public abstract Point ReverseTransform( Point point );
    }
}
