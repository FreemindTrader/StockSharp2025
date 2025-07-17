// Ecng.Xaml.Charting.Model.DataSeries.BoxPlotDataSeries<TX,TY>
using System;
using System.Collections.Generic;
using Ecng.Xaml.Charting;
public class BoxPlotDataSeries<TX, TY> : DataSeries<TX, TY> where TX : IComparable where TY : IComparable
{
    private ISeriesColumn<TY> _minimumColumn = new SeriesColumn<TY>();

    private ISeriesColumn<TY> _lowerQuartileColumn = new SeriesColumn<TY>();

    private ISeriesColumn<TY> _upperQuartileColumn = new SeriesColumn<TY>();

    private ISeriesColumn<TY> _maximumColumn = new SeriesColumn<TY>();

    public override bool HasValues
    {
        get
        {
            if ( _xColumn.HasValues && _minimumColumn.HasValues && _lowerQuartileColumn.HasValues && _yColumn.HasValues && _upperQuartileColumn.HasValues )
            {
                return _maximumColumn.HasValues;
            }
            return false;
        }
    }

    public override DataSeriesType DataSeriesType => DataSeriesType.Box;

    public IList<TY> MedianValues => base.YValues;

    public IList<TY> MinimumValues => _minimumColumn;

    public IList<TY> MaximumValues => _maximumColumn;

    public IList<TY> UpperQuartileValues => _upperQuartileColumn;

    public IList<TY> LowerQuartileValues => _lowerQuartileColumn;

    protected override void ClearColumns()
    {
        if ( base.FifoCapacity.HasValue )
        {
            int value = base.FifoCapacity.Value;
            _xColumn = new FifoSeriesColumn<TX>( value );
            _yColumn = new FifoSeriesColumn<TY>( value );
            _minimumColumn = new FifoSeriesColumn<TY>( value );
            _lowerQuartileColumn = new FifoSeriesColumn<TY>( value );
            _upperQuartileColumn = new FifoSeriesColumn<TY>( value );
            _maximumColumn = new FifoSeriesColumn<TY>( value );
        }
        else
        {
            _xColumn = new SeriesColumn<TX>();
            _yColumn = new SeriesColumn<TY>();
            _minimumColumn = new SeriesColumn<TY>();
            _lowerQuartileColumn = new SeriesColumn<TY>();
            _upperQuartileColumn = new SeriesColumn<TY>();
            _maximumColumn = new SeriesColumn<TY>();
        }
        ( ( ICollection<TX> ) _xColumn ).Clear();
        ( ( ICollection<TY> ) _yColumn ).Clear();
        ( ( ICollection<TY> ) _minimumColumn ).Clear();
        ( ( ICollection<TY> ) _lowerQuartileColumn ).Clear();
        ( ( ICollection<TY> ) _upperQuartileColumn ).Clear();
        ( ( ICollection<TY> ) _maximumColumn ).Clear();
    }

    public override void RemoveAt( int index )
    {
        lock ( base.SyncRoot )
        {
            TY val = base.YValues[index];
            TX val2 = base.XValues[index];
            base.XValues.RemoveAt( index );
            base.YValues.RemoveAt( index );
            ( ( IList<TY> ) _maximumColumn ).RemoveAt( index );
            ( ( IList<TY> ) _minimumColumn ).RemoveAt( index );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
        }
    }

    public override void RemoveRange( int startIndex, int count )
    {
        _xColumn.RemoveRange( startIndex, count );
        _yColumn.RemoveRange( startIndex, count );
        _maximumColumn.RemoveRange( startIndex, count );
        _upperQuartileColumn.RemoveRange( startIndex, count );
        _yColumn.RemoveRange( startIndex, count );
        _lowerQuartileColumn.RemoveRange( startIndex, count );
        _minimumColumn.RemoveRange( startIndex, count );
        OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        base.DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
    }

    public override IDataSeries<TX, TY> Clone()
    {
        lock ( base.SyncRoot )
        {
            BoxPlotDataSeries<TX, TY> boxPlotDataSeries = new BoxPlotDataSeries<TX, TY>();
            boxPlotDataSeries.FifoCapacity = base.FifoCapacity;
            boxPlotDataSeries.AcceptsUnsortedData = base.AcceptsUnsortedData;
            boxPlotDataSeries.Append( base.XValues, base.YValues, _minimumColumn, _lowerQuartileColumn, _upperQuartileColumn, _maximumColumn );
            return boxPlotDataSeries;
        }
    }

    public override IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
    {
        lock ( base.SyncRoot )
        {
            ResamplingMode resamplingMode2 = (resamplingMode != 0) ? ResamplingMode.Mid : ResamplingMode.None;
            ResamplingMode resamplingMode3 = (resamplingMode != 0) ? ResamplingMode.Max : ResamplingMode.None;
            ResamplingMode resamplingMode4 = (resamplingMode != 0) ? ResamplingMode.Min : ResamplingMode.None;
            IPointResampler pointResampler = factory.GetPointResampler<TX, TY>();
            IPointSeries yPoints = pointResampler.Execute(resamplingMode2, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _yColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries upperPoints = pointResampler.Execute(resamplingMode2, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _upperQuartileColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries lowerPoints = pointResampler.Execute(resamplingMode2, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _lowerQuartileColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries maxPoints = pointResampler.Execute(resamplingMode3, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _maximumColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries minPoints = pointResampler.Execute(resamplingMode4, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _minimumColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            return new BoxPointSeries( yPoints, minPoints, lowerPoints, upperPoints, maxPoints );
        }
    }

    public override HitTestInfo ToHitTestInfo( int index )
    {
        lock ( base.SyncRoot )
        {
            HitTestInfo result = base.ToHitTestInfo(index);
            if ( !result.IsEmpty() )
            {
                result.Minimum = ( IComparable ) ( object ) ( ( IList<TY> ) _minimumColumn )[ index ];
                result.Maximum = ( IComparable ) ( object ) ( ( IList<TY> ) _maximumColumn )[ index ];
                result.Median = ( IComparable ) ( object ) ( ( IList<TY> ) _yColumn )[ index ];
                result.LowerQuartile = ( IComparable ) ( object ) ( ( IList<TY> ) _lowerQuartileColumn )[ index ];
                result.UpperQuartile = ( IComparable ) ( object ) ( ( IList<TY> ) _upperQuartileColumn )[ index ];
            }
            return result;
        }
    }

    public override void Append( TX x, params TY[ ] yValues )
    {
        if ( yValues.Length != 5 )
        {
            ThrowWhenAppendInvalid( 5 );
        }
        Append( x, yValues[ 0 ], yValues[ 1 ], yValues[ 2 ], yValues[ 3 ], yValues[ 4 ] );
    }

    public override void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
    {
        if ( yValues.Length != 5 )
        {
            ThrowWhenAppendInvalid( 5 );
        }
        Append( x, yValues[ 0 ], yValues[ 1 ], yValues[ 2 ], yValues[ 3 ], yValues[ 4 ] );
    }

    private void Append( TX x, TY median, TY minimum, TY lowerQuartile, TY upperQuartile, TY maximum )
    {
        lock ( base.SyncRoot )
        {
            _xColumn.Add( x );
            _yColumn.Add( median );
            _minimumColumn.Add( minimum );
            _lowerQuartileColumn.Add( lowerQuartile );
            _upperQuartileColumn.Add( upperQuartile );
            _maximumColumn.Add( maximum );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnAppendXValue( _xColumn, x, base.AcceptsUnsortedData );
        }
    }

    public void Append( IEnumerable<TX> x, IEnumerable<TY> median, IEnumerable<TY> minimum, IEnumerable<TY> lowerQuartile, IEnumerable<TY> upperQuartile, IEnumerable<TY> maximum )
    {
        if ( !EnumerableExtensions.IsEmpty<TX>( x ) )
        {
            lock ( base.SyncRoot )
            {
                int count = ((ISeriesColumn)_xColumn).Count;
                _xColumn.AddRange( x );
                _yColumn.AddRange( median );
                _minimumColumn.AddRange( minimum );
                _lowerQuartileColumn.AddRange( lowerQuartile );
                _upperQuartileColumn.AddRange( upperQuartile );
                _maximumColumn.AddRange( maximum );
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                base.DataDistributionCalculator.OnAppendXValues( _xColumn, count, x, base.AcceptsUnsortedData );
            }
        }
    }

    public override TY GetYMaxAt( int index, TY existingYMax )
    {
        TY val = ((IList<TY>)_maximumColumn)[index];
        if ( DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return existingYMax;
        }
        return DataSeries<TX, TY>.YMath.Max( existingYMax, val );
    }

    public override TY GetYMinAt( int index, TY existingYMin )
    {
        TY val = ((IList<TY>)_minimumColumn)[index];
        if ( DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return existingYMin;
        }
        return DataSeries<TX, TY>.YMath.Min( existingYMin, val );
    }
}
