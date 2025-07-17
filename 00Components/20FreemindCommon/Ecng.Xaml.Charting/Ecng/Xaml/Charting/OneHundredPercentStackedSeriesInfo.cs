// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.OneHundredPercentStackedSeriesInfo
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting
{
    public class OneHundredPercentStackedSeriesInfo : SeriesInfo
    {
        private double _percentage;

        public OneHundredPercentStackedSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.Percentage = hitTestInfo.Persentage;
        }

        public double Percentage
        {
            get
            {
                return this._percentage;
            }
            set
            {
                this.SetField<double>( ref this._percentage, value, nameof( Percentage ) );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            this.Percentage = ( ( OneHundredPercentStackedSeriesInfo ) other ).Percentage;
        }
    }
}
