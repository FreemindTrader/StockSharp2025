// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.LinesClippingDecoratorFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
namespace Ecng.Xaml.Charting
{
    internal class LinesClippingDecoratorFactory : IPathContextFactory
    {
        private readonly IPathContextFactory _factory;
        private readonly Size _viewportSize;

        public LinesClippingDecoratorFactory( IPathContextFactory factory, Size viewportSize )
        {
            this._factory = factory;
            this._viewportSize = viewportSize;
        }

        public IPathDrawingContext Begin( IPathColor color, double startX, double startY )
        {
            return new LinesClippingDecorator( this._factory, this._viewportSize ).Begin( color, startX, startY );
        }
    }
}
