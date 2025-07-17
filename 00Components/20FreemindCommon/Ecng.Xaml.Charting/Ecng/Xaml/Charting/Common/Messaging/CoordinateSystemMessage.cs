// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Messaging.CoordinateSystemMessage
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Common.Messaging
{
    public class CoordinateSystemMessage : LoggedMessageBase
    {
        public CoordinateSystemMessage( object sender, CoordinateSystem coordinateSystem )
          : base( sender )
        {
            this.CoordinateSystem = coordinateSystem;
        }

        public CoordinateSystem CoordinateSystem
        {
            get; private set;
        }
    }
}
