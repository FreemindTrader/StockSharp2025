// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.TimeframeSegmentSeriesInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public class TimeframeSegmentSeriesInfo : SeriesInfo
    {
        private long _volume;

        public TimeframeSegmentSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.Volume = hitTestInfo.Volume;
        }

        public long Volume
        {
            get
            {
                return this._volume;
            }
            set
            {
                this.SetField<long>( ref this._volume, value, nameof( Volume ) );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            this.Volume = ( ( TimeframeSegmentSeriesInfo ) other ).Volume;
        }
    }
}
