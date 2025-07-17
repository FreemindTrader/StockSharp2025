// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.SegmentDataSeries.TimeframeSegmentWrapper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public class TimeframeSegmentWrapper : IPoint
    {
        public TimeframeDataSegment Segment
        {
            get;
        }

        public double X
        {
            get;
        }

        public double Y
        {
            get
            {
                return this.Segment.Y;
            }
        }

        public TimeframeSegmentWrapper( TimeframeDataSegment segment, double index )
        {
            this.Segment = segment;
            this.X = index;
        }
    }
}
