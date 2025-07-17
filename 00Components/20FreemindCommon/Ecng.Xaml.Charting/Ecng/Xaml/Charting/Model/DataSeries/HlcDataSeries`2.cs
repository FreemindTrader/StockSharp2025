// Ecng.Xaml.Charting.Model.DataSeries.HlcDataSeries<TX,TY>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ecng.Xaml.Charting;
using Ecng.Xaml.Charting.Common;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics;
using Ecng.Xaml.Charting.Numerics.PointResamplers;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

public sealed class HlcDataSeries<TX, TY> : DataSeries<TX, TY>, IHlcDataSeries<TX, TY>, IDataSeries<TX, TY>, IDataSeries, ISuspendable, IHlcDataSeries where TX : IComparable where TY : IComparable
{
    private ISeriesColumn<TY> _highColumn = new SeriesColumn<TY>();

    private ISeriesColumn<TY> _lowColumn = new SeriesColumn<TY>();

    private readonly DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY>> _appendBuffer;

    public override IRange YRange
    {
        get
        {
            TY val = ArrayOperations.Maximum<TY>((IEnumerable<TY>)_highColumn);
            TY val2 = ArrayOperations.Minimum<TY>((IEnumerable<TY>)_lowColumn);
            return RangeFactory.NewRange( ( IComparable ) ( object ) val2, ( IComparable ) ( object ) val );
        }
    }

    public override bool HasValues
    {
        get
        {
            if ( _xColumn.HasValues && _yColumn.HasValues && _highColumn.HasValues )
            {
                return _lowColumn.HasValues;
            }
            return false;
        }
    }

    public override DataSeriesType DataSeriesType => DataSeriesType.Hlc;

    IList IHlcDataSeries.HighValues
    {
        get
        {
            return _highColumn;
        }
    }

    public IList<TY> HighValues => _highColumn;

    IList IHlcDataSeries.LowValues
    {
        get
        {
            return _lowColumn;
        }
    }

    public IList<TY> LowValues => _lowColumn;

    public HlcDataSeries()
    {
        _appendBuffer = new DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY>>( FlushAppendBuffer );
    }

    public override HitTestInfo ToHitTestInfo( int index )
    {
        lock ( base.SyncRoot )
        {
            HitTestInfo result = base.ToHitTestInfo(index);
            if ( !result.IsEmpty() )
            {
                result.ErrorHigh = ( IComparable ) ( object ) ( ( IList<TY> ) _highColumn )[ index ];
                result.ErrorLow = ( IComparable ) ( object ) ( ( IList<TY> ) _lowColumn )[ index ];
            }
            return result;
        }
    }

    protected override void ClearColumns()
    {
        lock ( base.SyncRoot )
        {
            if ( base.FifoCapacity.HasValue )
            {
                int value = base.FifoCapacity.Value;
                _xColumn = new FifoSeriesColumn<TX>( value );
                _lowColumn = new FifoSeriesColumn<TY>( value );
                _highColumn = new FifoSeriesColumn<TY>( value );
                _yColumn = new FifoSeriesColumn<TY>( value );
            }
            else
            {
                _xColumn = new SeriesColumn<TX>();
                _lowColumn = new SeriesColumn<TY>();
                _highColumn = new SeriesColumn<TY>();
                _yColumn = new SeriesColumn<TY>();
            }
            ( ( ICollection<TX> ) _xColumn ).Clear();
            ( ( ICollection<TY> ) _lowColumn ).Clear();
            ( ( ICollection<TY> ) _highColumn ).Clear();
            ( ( ICollection<TY> ) _yColumn ).Clear();
            Maybe.Do<DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY>>>( _appendBuffer, ( Action<DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY>>> ) delegate ( DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY>> b )
            {
                b.Clear();
            } );
        }
    }

    public override void RemoveAt( int index )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            base.XValues.RemoveAt( index );
            base.YValues.RemoveAt( index );
            ( ( IList<TY> ) _highColumn ).RemoveAt( index );
            ( ( IList<TY> ) _lowColumn ).RemoveAt( index );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
        }
    }

    public override void RemoveRange( int startIndex, int count )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            _xColumn.RemoveRange( startIndex, count );
            _yColumn.RemoveRange( startIndex, count );
            _lowColumn.RemoveRange( startIndex, count );
            _highColumn.RemoveRange( startIndex, count );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
        }
    }

    public override IDataSeries<TX, TY> Clone()
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            HlcDataSeries<TX, TY> hlcDataSeries = new HlcDataSeries<TX, TY>();
            hlcDataSeries.FifoCapacity = base.FifoCapacity;
            hlcDataSeries.AcceptsUnsortedData = base.AcceptsUnsortedData;
            hlcDataSeries.Append( base.XValues, base.YValues, _highColumn, _lowColumn );
            return hlcDataSeries;
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
            IPointSeries yErrorHighPoints = pointResampler.Execute(resamplingMode3, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _highColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries yErrorLowPoints = pointResampler.Execute(resamplingMode4, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _lowColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            return new HlcPointSeries( yPoints, yErrorHighPoints, yErrorLowPoints );
        }
    }

    public override void OnBeginRenderPass()
    {
        base.OnBeginRenderPass();
        _appendBuffer.Flush();
    }

    public override void Append( TX x, params TY[ ] yValues )
    {
        if ( yValues.Length != 3 )
        {
            ThrowWhenAppendInvalid( 3 );
        }
        Append( x, yValues[ 0 ], yValues[ 1 ], yValues[ 2 ] );
    }

    public override void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
    {
        if ( yValues.Length != 3 )
        {
            ThrowWhenAppendInvalid( 3 );
        }
        Append( x, yValues[ 0 ], yValues[ 1 ], yValues[ 2 ] );
    }

    public void Append( TX x, TY y, TY high, TY low )
    {
        _appendBuffer.Append( ValueTuple.Create<TX, TY, TY, TY>( x, high, low, y ) );
        OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
    }

    public void Append( IEnumerable<TX> x, IEnumerable<TY> y, IEnumerable<TY> high, IEnumerable<TY> low )
    {
        if ( !EnumerableExtensions.IsEmpty<TX>( x ) )
        {
            IEnumerator<TX> enumerator = x.GetEnumerator();
            IEnumerator<TY> enumerator2 = high.GetEnumerator();
            IEnumerator<TY> enumerator3 = low.GetEnumerator();
            IEnumerator<TY> enumerator4 = y.GetEnumerator();
            lock ( _appendBuffer.SyncRoot )
            {
                while ( enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() && enumerator4.MoveNext() )
                {
                    _appendBuffer.Append( ValueTuple.Create<TX, TY, TY, TY>( enumerator.Current, enumerator2.Current, enumerator3.Current, enumerator4.Current ) );
                }
            }
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }
    }

    public void Update( TX x, TY y, TY high, TY low )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            int num = ((IList)_xColumn).FindIndex(base.IsSorted, (IComparable)(object)x, SearchMode.Exact);
            if ( num != -1 )
            {
                ( ( IList<TY> ) _yColumn )[ num ] = y;
                ( ( IList<TY> ) _highColumn )[ num ] = high;
                ( ( IList<TY> ) _lowColumn )[ num ] = low;
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            }
        }
    }

    public void Insert( int index, TX x, TY y, TY high, TY low )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            base.XValues.Insert( index, x );
            _yColumn.Insert( index, y );
            _highColumn.Insert( index, high );
            _lowColumn.Insert( index, low );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnInsertXValue( _xColumn, index, x, base.AcceptsUnsortedData );
        }
    }

    public void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y, IEnumerable<TY> high, IEnumerable<TY> low )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            int count = ((ISeriesColumn)_xColumn).Count;
            _xColumn.InsertRange( startIndex, x );
            int count2 = ((ISeriesColumn)_xColumn).Count;
            _yColumn.InsertRange( startIndex, y );
            _highColumn.InsertRange( startIndex, high );
            _lowColumn.InsertRange( startIndex, low );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnInsertXValues( _xColumn, startIndex, count2 - count, x, base.AcceptsUnsortedData );
        }
    }

    public override TY GetYMaxAt( int index, TY existingYMax )
    {
        TY val = ((IList<TY>)_highColumn)[index];
        if ( DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return existingYMax;
        }
        return DataSeries<TX, TY>.YMath.Max( existingYMax, val );
    }

    public override TY GetYMinAt( int index, TY existingYMin )
    {
        TY val = ((IList<TY>)_lowColumn)[index];
        if ( DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return existingYMin;
        }
        return DataSeries<TX, TY>.YMath.Min( existingYMin, val );
    }

    private void FlushAppendBuffer( IList<ValueTuple<TX, TY, TY, TY>> bufferedValues )
    {
        lock ( base.SyncRoot )
        {
            IEnumerable<TX> enumerable = Enumerable.Select<ValueTuple<TX, TY, TY, TY>, TX>((IEnumerable<ValueTuple<TX, TY, TY, TY>>)bufferedValues, (Func<ValueTuple<TX, TY, TY, TY>, TX>)((ValueTuple<TX, TY, TY, TY> b) => b.Item1));
            int count = ((ISeriesColumn)_xColumn).Count;
            _xColumn.AddRange( enumerable );
            _highColumn.AddRange( Enumerable.Select<ValueTuple<TX, TY, TY, TY>, TY>( ( IEnumerable<ValueTuple<TX, TY, TY, TY>> ) bufferedValues, ( Func<ValueTuple<TX, TY, TY, TY>, TY> ) ( ( ValueTuple<TX, TY, TY, TY> b ) => b.Item2 ) ) );
            _lowColumn.AddRange( Enumerable.Select<ValueTuple<TX, TY, TY, TY>, TY>( ( IEnumerable<ValueTuple<TX, TY, TY, TY>> ) bufferedValues, ( Func<ValueTuple<TX, TY, TY, TY>, TY> ) ( ( ValueTuple<TX, TY, TY, TY> b ) => b.Item3 ) ) );
            _yColumn.AddRange( Enumerable.Select<ValueTuple<TX, TY, TY, TY>, TY>( ( IEnumerable<ValueTuple<TX, TY, TY, TY>> ) bufferedValues, ( Func<ValueTuple<TX, TY, TY, TY>, TY> ) ( ( ValueTuple<TX, TY, TY, TY> b ) => b.Item4 ) ) );
            base.DataDistributionCalculator.OnAppendXValues( _xColumn, count, enumerable, base.AcceptsUnsortedData );
        }
    }
}
