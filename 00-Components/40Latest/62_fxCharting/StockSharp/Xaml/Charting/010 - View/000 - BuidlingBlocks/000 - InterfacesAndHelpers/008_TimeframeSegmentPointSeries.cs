using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Data.Model;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockSharp.Xaml.Charting;
public sealed class TimeframeSegmentPointSeries : IPointSeries
{
    private readonly TimeframeSegmentWrapper[] _segments;
    private readonly DoubleRange _yRange;
    private Values<double> _xValues;
    private Values<double> _yValues;
    private readonly Dictionary<double, CandlePriceLevel> _doubleToCandlePriceLevel;
    private readonly IEnumerable<double> _allPrices;
    private readonly double _priceStep;
    private readonly IndexRange _pointRange;
    private readonly IndexRange _visibleRange;

    public TimeframeSegmentPointSeries(TimeframeDataSegment[] tfSegments, IndexRange pointRange, IRange visibleXRange, double priceStep)
    {        
        _priceStep = priceStep;
        _pointRange = (IndexRange)pointRange.Clone();
        
        if ( !( visibleXRange is IndexRange visibleR ) )
            visibleR = pointRange;
        
        _visibleRange = visibleR;
        _segments = new TimeframeSegmentWrapper[pointRange.Max - pointRange.Min + 1];
        
        for ( int min = pointRange.Min; min <= pointRange.Max; ++min )
        {
            var segment = tfSegments[min];
            _segments[min - pointRange.Min] = new TimeframeSegmentWrapper(segment, (double)min);
        }
        double myMinValue = double.MaxValue;
        double myMaxValue = double.MinValue;

        foreach ( TimeframeSegmentWrapper wrapper in _segments )
        {
            if ( wrapper.Segment.MinPrice < myMinValue )
                myMinValue = wrapper.Segment.MinPrice;

            if ( wrapper.Segment.MaxPrice > myMaxValue )
                myMaxValue = wrapper.Segment.MaxPrice;
        }

        _yRange = new DoubleRange(myMinValue, myMaxValue);
        
        var newPriceSet = new HashSet<double>();

        foreach ( (double key, CandlePriceLevel level) in Segments.Where(p => p.Segment.Count >= VisibleRange.Min && p.Segment.Count <= VisibleRange.Max).SelectMany(t => t.Segment.GetPriceLevelsAndVolume(priceStep).Item1) )
        {
            newPriceSet.Add(key);
            CandlePriceLevel candlePriceLevel2;
            if ( _doubleToCandlePriceLevel.TryGetValue(key, out candlePriceLevel2) )
                _doubleToCandlePriceLevel[key] = ( candlePriceLevel2 ).Join(level);
            else
                _doubleToCandlePriceLevel.Add(key, level);
        }
        
        int index = 0;
        double[] newPriceArray = new double[newPriceSet.Count];
        
        foreach ( double item in newPriceSet )
        {
            newPriceArray[index] = item;
            ++index;
        }

        _allPrices = (IEnumerable<double>)new SciList<double>(newPriceArray);
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
            return _yValues ?? ( _yValues = new Values<double>(_segments.Select(p => p.Y).ToArray()) );
        }        
    }

    public TimeframeSegmentWrapper[] Segments => _segments;


    protected IUltraReadOnlyList<TimeframeSegmentWrapper> Method0098()
    {
        return (IUltraReadOnlyList<TimeframeSegmentWrapper>)new UltraReadOnlyList<TimeframeSegmentWrapper>(_segments);
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
            throw new NotSupportedException("Count is read-only for TimeframeSegmentPointSeries.");
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
            return (IPoint)_segments[index];
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

    public CandlePriceLevel GetCandlePriceLevelFromPrice(double _param1)
    {
        CandlePriceLevel candlePriceLevel;
        _doubleToCandlePriceLevel.TryGetValue(_param1, out candlePriceLevel);
        return candlePriceLevel;
    }
       
    public DoubleRange GetXRange()
    {
        throw new NotImplementedException();
    }

    public void Clear(bool releaseMemory = false)
    {
        throw new NotImplementedException();
    }

    public void ApplyYCalc(ICoordinateCalculator<double> yCalc)
    {
        throw new NotImplementedException();
    }

    public void Concat(IPointSeries other, bool fifoMode, double minX)
    {
        throw new NotImplementedException();
    }


}
