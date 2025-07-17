// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.HlcSeriesInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public class HlcSeriesInfo : SeriesInfo
    {
        private double _highValue;
        private double _lowValue;
        private double _closeValue;

        public HlcSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.HighValue = Convert.ToDouble( ( object ) hitTestInfo.ErrorHigh );
            this.LowValue = Convert.ToDouble( ( object ) hitTestInfo.ErrorLow );
            this.CloseValue = Convert.ToDouble( ( object ) hitTestInfo.YValue );
        }

        public double HighValue
        {
            get
            {
                return this._highValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._highValue, value, nameof( HighValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedHighValue" );
            }
        }

        public string FormattedHighValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.HighValue );
            }
        }

        public double LowValue
        {
            get
            {
                return this._lowValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._lowValue, value, nameof( LowValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedLowValue" );
            }
        }

        public string FormattedLowValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.LowValue );
            }
        }

        public double CloseValue
        {
            get
            {
                return this._closeValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._closeValue, value, nameof( CloseValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedCloseValue" );
            }
        }

        public string FormattedCloseValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.CloseValue );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            HlcSeriesInfo hlcSeriesInfo = (HlcSeriesInfo) other;
            this.HighValue = hlcSeriesInfo.HighValue;
            this.LowValue = hlcSeriesInfo.LowValue;
            this.CloseValue = hlcSeriesInfo.CloseValue;
        }
    }
}
