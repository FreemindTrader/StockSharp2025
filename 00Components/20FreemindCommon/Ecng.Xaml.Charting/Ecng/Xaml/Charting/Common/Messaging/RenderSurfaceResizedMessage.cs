// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Messaging.RenderSurfaceResizedMessage
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;

namespace fx.Xaml.Charting
{
    public class RenderSurfaceResizedMessage : LoggedMessageBase
    {
        public RenderSurfaceResizedMessage( object sender, Size viewportSize )
          : base( sender )
        {
            this.ViewportSize = viewportSize;
        }

        public Size ViewportSize
        {
            get; private set;
        }
    }
}
