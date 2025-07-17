// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries.TimeframeSegmentPointSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries
{
    internal class TimeframeSegmentPointSeries : IPointSeries
    {
        private readonly Dictionary<double, long> _volumeByPrice = new Dictionary<double, long>();
        private readonly TimeframeSegmentWrapper[] _segments;
        private readonly DoubleRange _yRange;
        private IUltraList<double> _xValues;

        public IUltraList<double> XValues
        {
            get
            {
                return this._xValues ?? ( this._xValues = ( IUltraList<double> ) new UltraList<double>( ( ( IEnumerable<TimeframeSegmentWrapper> ) this._segments ).Select<TimeframeSegmentWrapper, double>( ( Func<TimeframeSegmentWrapper, double> ) ( s => s.X ) ) ) );
            }
        }

        public IUltraList<double> YValues
        {
            get
            {
                return ( IUltraList<double> ) null;
            }
        }

        public TimeframeSegmentWrapper[ ] Segments
        {
            get
            {
                return this._segments;
            }
        }

        protected IUltraReadOnlyList<TimeframeSegmentWrapper> SegmentsReadOnly
        {
            get
            {
                return ( IUltraReadOnlyList<TimeframeSegmentWrapper> ) new UltraReadOnlyList<TimeframeSegmentWrapper>( this._segments );
            }
        }

        public int Count
        {
            get
            {
                return this._segments.Length;
            }
        }

        public IEnumerable<double> AllPrices
        {
            get;
        }

        public IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) this._segments[ index ];
            }
        }

        public double PriceStep
        {
            get;
        }

        public IndexRange DataRange
        {
            get;
        }

        public IndexRange VisibleRange
        {
            get;
        }

        public TimeframeSegmentPointSeries( TimeframeDataSegment[ ] segments, IndexRange dataRange, IRange visibleRange, double priceStep )
        {
            this.PriceStep = priceStep;
            this.DataRange = ( IndexRange ) dataRange.Clone();
            this.VisibleRange = visibleRange as IndexRange ?? dataRange;
            this._segments = new TimeframeSegmentWrapper[ dataRange.Max - dataRange.Min + 1 ];
            for ( int min = dataRange.Min ; min <= dataRange.Max ; ++min )
            {
                TimeframeDataSegment segment = segments[min];
                this._segments[ min - dataRange.Min ] = new TimeframeSegmentWrapper( segment, ( double ) min );
            }
            double min1 = double.MaxValue;
            double max = double.MinValue;
            foreach ( TimeframeSegmentWrapper segment in this._segments )
            {
                if ( segment.Segment.MinPrice < min1 )
                    min1 = segment.Segment.MinPrice;
                if ( segment.Segment.MaxPrice > max )
                    max = segment.Segment.MaxPrice;
            }
            this._yRange = new DoubleRange( min1, max );
            HashSet<double> source = new HashSet<double>();
            foreach ( TimeframeDataSegment.PriceLevel priceLevel in ( ( IEnumerable<TimeframeSegmentWrapper> ) this.Segments ).Where<TimeframeSegmentWrapper>( ( Func<TimeframeSegmentWrapper, bool> ) ( seg =>
            {
                if ( seg.Segment.Index >= this.VisibleRange.Min )
                    return seg.Segment.Index <= this.VisibleRange.Max;
                return false;
            } ) ).SelectMany<TimeframeSegmentWrapper, TimeframeDataSegment.PriceLevel>( ( Func<TimeframeSegmentWrapper, IEnumerable<TimeframeDataSegment.PriceLevel>> ) ( seg => seg.Segment.Values.Where<TimeframeDataSegment.PriceLevel>( ( Func<TimeframeDataSegment.PriceLevel, bool> ) ( pv => pv != null ) ) ) ) )
            {
                source.Add( priceLevel.Price );
                long num;
                this._volumeByPrice.TryGetValue( priceLevel.Price, out num );
                this._volumeByPrice[ priceLevel.Price ] = priceLevel.Value + num;
            }
            this.AllPrices = ( IEnumerable<double> ) source.ToArray<double>();
        }

        public DoubleRange GetYRange()
        {
            return this._yRange;
        }

        public long GetVolumeByPrice( double normalizedPrice )
        {
            long num;
            this._volumeByPrice.TryGetValue( normalizedPrice, out num );
            return num;
        }
    }
}
