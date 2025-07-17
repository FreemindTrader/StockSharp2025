// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ZoomExtentsMessage
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting
{
    public class ZoomExtentsMessage : LoggedMessageBase
    {
        public bool ZoomYOnly
        {
            get; private set;
        }

        public ZoomExtentsMessage( object sender )
          : base( sender )
        {
        }

        public ZoomExtentsMessage( object sender, bool zoomYOnly )
          : this( sender )
        {
            this.ZoomYOnly = zoomYOnly;
        }
    }
}
