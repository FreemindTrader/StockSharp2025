// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.BandSeriesInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public class BandSeriesInfo : SeriesInfo
    {
        private IComparable _y1Value;
        private Point _xy1Coordinate;
        private bool _isFirstSeries;

        public override object SeriesInfoKey
        {
            get
            {
                return ( object ) Tuple.Create<IRenderableSeries, bool>( this.RenderableSeries, this.IsFirstSeries );
            }
        }

        public BandSeriesInfo( IRenderableSeries rSeries, HitTestInfo hitTestInfo )
          : base( rSeries, hitTestInfo )
        {
            this.Y1Value = hitTestInfo.Y1Value;
            this.Xy1Coordinate = hitTestInfo.Y1HitTestPoint;
        }

        public bool IsFirstSeries
        {
            get
            {
                return this._isFirstSeries;
            }
            set
            {
                this._isFirstSeries = value;
            }
        }

        public IComparable Y1Value
        {
            get
            {
                return this._y1Value;
            }
            set
            {
                if ( !this.SetField<IComparable>( ref this._y1Value, value, nameof( Y1Value ) ) )
                    return;
                this.OnPropertyChanged( "FormattedY1Value" );
            }
        }

        public string FormattedY1Value
        {
            get
            {
                return this.GetYCursorFormattedValue( this.Y1Value );
            }
        }

        public Point Xy1Coordinate
        {
            get
            {
                return this._xy1Coordinate;
            }
            set
            {
                this.SetField<Point>( ref this._xy1Coordinate, value, nameof( Xy1Coordinate ) );
            }
        }

        public override void CopyFrom( SeriesInfo other )
        {
            base.CopyFrom( other );
            BandSeriesInfo bandSeriesInfo = (BandSeriesInfo) other;
            this.Y1Value = bandSeriesInfo.Y1Value;
            this.Xy1Coordinate = bandSeriesInfo.Xy1Coordinate;
            this.IsFirstSeries = bandSeriesInfo.IsFirstSeries;
        }
    }
}
