using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Data.Model;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// 
/// TimeframeSegmentPointSeries is like the line representation of the TimeframeSegment
/// 
/// TimeframeSegmentDataSeries is like the Candle of the TimeframeSegment
/// 
/// </summary>
public sealed class TimeframeSegmentPointSeries : IPointSeries
{
    private readonly Dictionary<double, CandlePriceLevel> _priceToCPL = new Dictionary<double, CandlePriceLevel>();

    private readonly TimeframeIndexedSegment[] _segments;
    private readonly DoubleRange               _yRange;
    private Values<double>                     _xValues;
    private Values<double>                     _yValues;    
    private readonly IEnumerable<double>       _allPrices;
    private readonly double                    _priceStep;
    private readonly IndexRange                _pointRange;
    private readonly IndexRange                _visibleRange;

    public TimeframeSegmentPointSeries( TimeframeDataSegment[ ] tfSegments, IndexRange pointRange, IRange visibleXRange, double priceStep )
    {
        _priceStep    = priceStep;
        _pointRange   = ( IndexRange ) pointRange.Clone();

        if ( !( visibleXRange is IndexRange visibleR ) )
        {
            visibleR = pointRange;
        }
            
        _visibleRange = visibleR;
        _segments     = new TimeframeIndexedSegment[pointRange.Max - pointRange.Min + 1];

        for ( int i = pointRange.Min; i <= pointRange.Max; ++i )
        {
            var segment = tfSegments[i];
            _segments[i - pointRange.Min] = new TimeframeIndexedSegment( segment, ( double ) i );
        }

        double min = double.MaxValue;
        double max = double.MinValue;

        foreach ( TimeframeIndexedSegment tfs in _segments )
        {
            if ( tfs.Segment.LowPrice < min )
                min = tfs.Segment.LowPrice;

            if ( tfs.Segment.HighPrice > max )
                max = tfs.Segment.HighPrice;
        }

        _yRange = new DoubleRange( min, max );

        var allPrices = new HashSet<double>();

        foreach ( (double price, CandlePriceLevel cpl) in Segments.Where( p => p.Segment.Count >= VisibleRange.Min && p.Segment.Count <= VisibleRange.Max ).SelectMany( t => t.Segment.GetCPLVFromPriceStep( priceStep ).Item1 ) )
        {
            allPrices.Add( price );
            
            if ( _priceToCPL.TryGetValue( price, out var _cpl ) )
                _priceToCPL[price] = _cpl.Join( cpl );
            else
                _priceToCPL.Add( price, cpl );
        }

        int index = 0;
        double[] allPricesArray = new double[allPrices.Count];

        foreach ( double price in allPrices )
        {
            allPricesArray[index] = price;
            ++index;
        }

        _allPrices = ( IEnumerable<double> ) new SciList<double>( allPricesArray );
    }


    /// <summary>
    /// Gets the Raw X-Values for the PointSeries.
    /// </summary>
    public Values<double> XValues
    {
        get
        {
            return _xValues ?? ( _xValues = new Values<double>( _segments.Select( p => p.X ).ToArray() ) );
        }

    }


    /// <summary>
    /// Gets the Raw Y-Values for the PointSeries.
    /// </summary>
    public Values<double> YValues
    {
        get
        {
            return _yValues ?? ( _yValues = new Values<double>( _segments.Select( p => p.Y ).ToArray() ) );
        }
    }

    public TimeframeIndexedSegment[ ] Segments => _segments;


    public IReadOnlySciList<TimeframeIndexedSegment> ReadOnlySegments
    {
        get
        {
            return ( IReadOnlySciList<TimeframeIndexedSegment> ) new ReadOnlySciList<TimeframeIndexedSegment>( _segments );
        }        
    }


    /// <summary>
    /// Gets or sets the count of elements in the PointSeries
    /// </summary>
    public int Count
    {
        get
        {
            return _segments.Length;
        }

        set
        {
            throw new NotSupportedException( "Count is read-only for TimeframeSegmentPointSeries." );
        }
    }


    public IEnumerable<double> AllPrices
    {
        get
        {
            return _allPrices;
        }
    }

    //[IndexerName("#=zMRIb09I=")]
    public IPoint this[int index]
    {
        get
        {
            return ( IPoint ) _segments[index];
        }
    }

    public double PriceStep
    {
        get
        {
            return _priceStep;
        }

    }

    public IndexRange DataRange => _pointRange;

    public IndexRange VisibleRange
    {
        get => _visibleRange;
    }
    public Type OriginalXType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Type OriginalYType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Values<int> Indexes => throw new NotImplementedException();

    public int Capacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DoubleRange XRange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <summary>
    /// Gets the min, max range in the Y-Direction.
    /// </summary>
    /// 
    /// <returns>
    /// A SciChart.Data.Model.DoubleRange defining the min, max in the Y-direction.
    /// </returns>
    public DoubleRange GetYRange()
    {
        return _yRange;
    }

    public CandlePriceLevel GetCandlePriceLevelFromPrice( double _param1 )
    {
        CandlePriceLevel candlePriceLevel;
        _priceToCPL.TryGetValue( _param1, out candlePriceLevel );
        return candlePriceLevel;
    }

    public DoubleRange GetXRange()
    {
        throw new NotImplementedException();
    }

    public void Clear( bool releaseMemory = false )
    {
        throw new NotImplementedException();
    }

    public void ApplyYCalc( ICoordinateCalculator<double> yCalc )
    {
        throw new NotImplementedException();
    }

    public void Concat( IPointSeries other, bool fifoMode, double minX )
    {
        throw new NotImplementedException();
    }


}
