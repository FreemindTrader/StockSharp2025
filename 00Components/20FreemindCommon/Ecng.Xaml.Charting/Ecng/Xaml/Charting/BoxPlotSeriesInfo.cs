// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.BoxPlotSeriesInfo
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting
{
    public class BoxPlotSeriesInfo : SeriesInfo
    {
        private double _minimumValue;
        private double _maximumValue;
        private double _lowerQuartileValue;
        private double _upperQuartileValue;
        private double _medianValue;

        public BoxPlotSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.MinimumValue = Convert.ToDouble( ( object ) hitTestInfo.Minimum );
            this.MaximumValue = Convert.ToDouble( ( object ) hitTestInfo.Maximum );
            this.MedianValue = Convert.ToDouble( ( object ) hitTestInfo.Median );
            this.LowerQuartileValue = Convert.ToDouble( ( object ) hitTestInfo.LowerQuartile );
            this.UpperQuartileValue = Convert.ToDouble( ( object ) hitTestInfo.UpperQuartile );
        }

        public double MinimumValue
        {
            get
            {
                return this._minimumValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._minimumValue, value, nameof( MinimumValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedMinimumValue" );
            }
        }

        public string FormattedMinimumValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.MinimumValue );
            }
        }

        public double MaximumValue
        {
            get
            {
                return this._maximumValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._maximumValue, value, nameof( MaximumValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedMaximumValue" );
            }
        }

        public string FormattedMaximumValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.MaximumValue );
            }
        }

        public double MedianValue
        {
            get
            {
                return this._medianValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._medianValue, value, nameof( MedianValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedMedianValue" );
            }
        }

        public string FormattedMedianValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.MedianValue );
            }
        }

        public double LowerQuartileValue
        {
            get
            {
                return this._lowerQuartileValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._lowerQuartileValue, value, nameof( LowerQuartileValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedLowerQuartileValue" );
            }
        }

        public string FormattedLowerQuartileValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.LowerQuartileValue );
            }
        }

        public double UpperQuartileValue
        {
            get
            {
                return this._upperQuartileValue;
            }
            set
            {
                if ( !this.SetField<double>( ref this._upperQuartileValue, value, nameof( UpperQuartileValue ) ) )
                    return;
                this.OnPropertyChanged( "FormattedUpperQuartileValue" );
            }
        }

        public string FormattedUpperQuartileValue
        {
            get
            {
                return this.GetYCursorFormattedValue( ( IComparable ) this.UpperQuartileValue );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            BoxPlotSeriesInfo boxPlotSeriesInfo = (BoxPlotSeriesInfo) other;
            this.MinimumValue = boxPlotSeriesInfo.MinimumValue;
            this.MaximumValue = boxPlotSeriesInfo.MaximumValue;
            this.LowerQuartileValue = boxPlotSeriesInfo.LowerQuartileValue;
            this.UpperQuartileValue = boxPlotSeriesInfo.UpperQuartileValue;
            this.MedianValue = boxPlotSeriesInfo.MedianValue;
        }
    }
}
