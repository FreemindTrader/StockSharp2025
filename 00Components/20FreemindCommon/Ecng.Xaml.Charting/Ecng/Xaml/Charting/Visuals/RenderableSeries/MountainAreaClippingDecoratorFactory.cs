// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.MountainAreaClippingDecoratorFactory
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
namespace fx.Xaml.Charting
{
    internal class MountainAreaClippingDecoratorFactory : IPathContextFactory
    {
        private readonly MountainAreaPathContextFactory _mountainAreaFactory;
        private readonly Size _viewportSize;

        public MountainAreaClippingDecoratorFactory( MountainAreaPathContextFactory mountainAreaFactory, Size viewportSize )
        {
            this._mountainAreaFactory = mountainAreaFactory;
            this._viewportSize = viewportSize;
        }

        public IPathDrawingContext Begin( IPathColor color, double startX, double startY )
        {
            return new PolygonClippingDecorator( ( IPathContextFactory ) this._mountainAreaFactory, this._viewportSize ).Begin( color, startX, startY );
        }
    }
}
