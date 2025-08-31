//using SciChart.Charting.Numerics.CoordinateCalculators;
//using SciChart.Data.Model;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StockSharp.Xaml.Charting.ATony
//{
//    public class TimeframeSegmentWrapper : IPoint
//    {
//        public TimeframeDataSegment Segment { get; }

//        public double X { get; }

//        public double Y
//        {
//            get
//            {
//                return this.Segment.Y;
//            }
//        }

//        public TimeframeSegmentWrapper( TimeframeDataSegment segment, double index )
//        {
//            this.Segment = segment;
//            this.X = index;
//        }
//    }


//    internal class TimeframeSegmentPointSeries : IPointSeries
//    {
//        private readonly PooledDictionary<double, long> _volumeByPrice = new PooledDictionary<double, long>( );
//        private readonly TimeframeSegmentWrapper[ ] _segments;
//        private readonly DoubleRange _yRange;
//        private ISciList<double> _xValues;
//        private Values< double > _myX;


//        Values<double> IPointSeries.XValues
//        {
//            get
//            {
//                var segmentArray = this._segments.Select( s => s.X ).ToArray( );

//                _myX = new Values<double>( segmentArray );

//                return ( _myX );
//            }
//        }

//        public DoubleRange GetYRange( )
//        {
//            return this._yRange;
//        }

//        Values<double> IPointSeries.YValues => throw new NotImplementedException( );

//        public Values<int> Indexes => throw new NotImplementedException( );

//        public ISciList<double> XValues
//        {
//            get
//            {
//                return this._xValues ?? ( this._xValues = ( ISciList<double> )new SciList<double>( ( ( IEnumerable<TimeframeSegmentWrapper> )this._segments ).Select( ( Func<TimeframeSegmentWrapper, double> )( s => s.X ) ) ) );
//            }
//        }

//        public ISciList<double> YValues
//        {
//            get
//            {
//                return ( ISciList<double> )null;
//            }
//        }

//        public TimeframeSegmentWrapper[ ] Segments
//        {
//            get
//            {
//                return this._segments;
//            }
//        }

//        //protected ISciReadOnlyList<TimeframeSegmentWrapper> SegmentsReadOnly
//        //{
//        //    get
//        //    {
//        //        return ( ISciReadOnlyList<TimeframeSegmentWrapper> )new ReadOnlySciList<TimeframeSegmentWrapper>( this._segments );
//        //    }
//        //}

//       

//        public IEnumerable<double> AllPrices { get; }

//        public IPoint this[ int index ]
//        {
//            get
//            {
//                return ( IPoint )this._segments[ index ];
//            }
//        }

//        public double PriceStep { get; }

//        public IndexRange DataRange { get; }

//        public IndexRange VisibleRange { get; }

//       

//        int IPointSeries.Count 
//        {
//            get
//            {
//                return this._segments.Length;
//            }

//            set => throw new NotImplementedException( ); 
//        }
//        public int Capacity { get => throw new NotImplementedException( ); set => throw new NotImplementedException( ); }
//        public DoubleRange XRange { get => throw new NotImplementedException( ); set => throw new NotImplementedException( ); }

//        public TimeframeSegmentPointSeries( TimeframeDataSegment[ ] segments, IndexRange dataRange, IRange visibleRange, double priceStep )
//        {
//            this.PriceStep    = priceStep;
//            this.DataRange    = ( IndexRange )dataRange.Clone( );
//            this.VisibleRange = visibleRange as IndexRange ?? dataRange;
//            this._segments    = new TimeframeSegmentWrapper[ dataRange.Max - dataRange.Min + 1 ];

//            for ( int min = dataRange.Min; min <= dataRange.Max; ++min )
//            {
//                TimeframeDataSegment segment = segments[ min ];
//                this._segments[ min - dataRange.Min ] = new TimeframeSegmentWrapper( segment, ( double )min );
//            }
//            double min1 = double.MaxValue;
//            double max = double.MinValue;
//            foreach ( TimeframeSegmentWrapper segment in this._segments )
//            {
//                if ( segment.Segment.MinPrice < min1 )
//                {
//                    min1 = segment.Segment.MinPrice;
//                }

//                if ( segment.Segment.MaxPrice > max )
//                {
//                    max = segment.Segment.MaxPrice;
//                }
//            }
//            this._yRange = new DoubleRange( min1, max );
//            PooledSet<double> source = new PooledSet<double>( );
//            foreach ( TimeframeDataSegment.PriceLevel priceLevel in ( ( IEnumerable<TimeframeSegmentWrapper> )this.Segments ).Where<TimeframeSegmentWrapper>( ( Func<TimeframeSegmentWrapper, bool> )( seg =>
//            {
//                if ( seg.Segment.Index >= this.VisibleRange.Min )
//                {
//                    return seg.Segment.Index <= this.VisibleRange.Max;
//                }

//                return false;
//            } ) ).SelectMany<TimeframeSegmentWrapper, TimeframeDataSegment.PriceLevel>( ( Func<TimeframeSegmentWrapper, IEnumerable<TimeframeDataSegment.PriceLevel>> )( seg => seg.Segment.Values.Where<TimeframeDataSegment.PriceLevel>( ( Func<TimeframeDataSegment.PriceLevel, bool> )( pv => pv != null ) ) ) ) )
//            {
//                source.Add( priceLevel.Price );
//                long num;
//                this._volumeByPrice.TryGetValue( priceLevel.Price, out num );
//                this._volumeByPrice[ priceLevel.Price ] = priceLevel.Value + num;
//            }
//            this.AllPrices = ( IEnumerable<double> )source.ToArray<double>( );
//        }

//        

//        public long GetVolumeByPrice( double normalizedPrice )
//        {
//            long num;
//            this._volumeByPrice.TryGetValue( normalizedPrice, out num );
//            return num;
//        }

//        public void Clear( )
//        {
//            throw new NotImplementedException( );
//        }

//        public void ApplyYCalc( ICoordinateCalculator<double> yCalc )
//        {
//            throw new NotImplementedException( );
//        }

//        public void Concat( IPointSeries other, bool fifoMode, double minX )
//        {
//            throw new NotImplementedException( );
//        }
//    }
//}

