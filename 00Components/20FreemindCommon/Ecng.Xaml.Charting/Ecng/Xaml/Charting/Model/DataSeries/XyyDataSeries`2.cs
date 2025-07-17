// Ecng.Xaml.Charting.Model.DataSeries.XyyDataSeries<TX,TY>
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

public class XyyDataSeries<TX, TY> : DataSeries<TX, TY>, IXyyDataSeries<TX, TY>, IDataSeries<TX, TY>, IDataSeries, ISuspendable, IXyyDataSeries where TX : IComparable where TY : IComparable
{
    private ISeriesColumn<TY> _y1Column = new SeriesColumn<TY>();

    private readonly DataSeriesAppendBuffer<ValueTuple<TX, TY, TY>> _appendBuffer;

    public override IRange YRange
    {
        get
        {
            TY min = default(TY);
            TY max = default(TY);
            TY min2 = default(TY);
            TY max2 = default(TY);
            ArrayOperations.MinMax<TY>( ( IEnumerable<TY> ) _yColumn, out min, out max );
            ArrayOperations.MinMax<TY>( ( IEnumerable<TY> ) _y1Column, out min2, out max2 );
            min = DataSeries<TX, TY>.YMath.Min( min, min2 );
            max = DataSeries<TX, TY>.YMath.Max( max, max2 );
            return RangeFactory.NewRange( ( IComparable ) ( object ) min, ( IComparable ) ( object ) max );
        }
    }

    public override DataSeriesType DataSeriesType => DataSeriesType.Xyy;

    IList IXyyDataSeries.Y1Values
    {
        get
        {
            return _y1Column;
        }
    }

    public IList<TY> Y1Values => _y1Column;

    public override bool HasValues
    {
        get
        {
            if ( _xColumn.HasValues && _yColumn.HasValues )
            {
                return _y1Column.HasValues;
            }
            return false;
        }
    }

    public XyyDataSeries()
    {
        _appendBuffer = new DataSeriesAppendBuffer<ValueTuple<TX, TY, TY>>( FlushAppendBuffer );
    }

    public override void RemoveAt( int index )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            TY val = base.YValues[index];
            TY val2 = Y1Values[index];
            TX val3 = base.XValues[index];
            base.XValues.RemoveAt( index );
            base.YValues.RemoveAt( index );
            Y1Values.RemoveAt( index );
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
            _y1Column.RemoveRange( startIndex, count );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
        }
    }

    public override IDataSeries<TX, TY> Clone()
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            XyyDataSeries<TX, TY> xyyDataSeries = new XyyDataSeries<TX, TY>();
            xyyDataSeries.FifoCapacity = base.FifoCapacity;
            xyyDataSeries.AcceptsUnsortedData = base.AcceptsUnsortedData;
            xyyDataSeries.Append( base.XValues, base.YValues, Y1Values );
            return xyyDataSeries;
        }
    }

    public override TY GetYMinAt( int index, TY existingYMin )
    {
        TY val = Y1Values[index];
        TY val2 = base.YValues[index];
        if ( !DataSeries<TX, TY>.YMath.IsNaN( val2 ) && !DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return DataSeries<TX, TY>.YMath.Min( DataSeries<TX, TY>.YMath.Min( val2, val ), existingYMin );
        }
        return existingYMin;
    }

    public override TY GetYMaxAt( int index, TY existingYMax )
    {
        TY val = Y1Values[index];
        TY val2 = base.YValues[index];
        if ( !DataSeries<TX, TY>.YMath.IsNaN( val2 ) && !DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return DataSeries<TX, TY>.YMath.Max( DataSeries<TX, TY>.YMath.Max( val2, val ), existingYMax );
        }
        return existingYMax;
    }

    public override IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
    {
        lock ( base.SyncRoot )
        {
            IPointSeries yPoints = factory.GetPointResampler<TX, TY>().Execute(resamplingMode, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _yColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries y1Points = factory.GetPointResampler<TX, TY>().Execute(resamplingMode, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _y1Column, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            return new XyyPointSeries( yPoints, y1Points );
        }
    }

    public override void OnBeginRenderPass()
    {
        base.OnBeginRenderPass();
        _appendBuffer.Flush();
    }

    public override HitTestInfo ToHitTestInfo( int index )
    {
        lock ( base.SyncRoot )
        {
            HitTestInfo result = base.ToHitTestInfo(index);
            if ( !result.IsEmpty() )
            {
                TY val = Y1Values[index];
                result.Y1Value = ( IComparable ) ( object ) val;
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
                _yColumn = new FifoSeriesColumn<TY>( value );
                _y1Column = new FifoSeriesColumn<TY>( value );
            }
            else
            {
                _xColumn = new SeriesColumn<TX>();
                _yColumn = new SeriesColumn<TY>();
                _y1Column = new SeriesColumn<TY>();
            }
            ( ( ICollection<TX> ) _xColumn ).Clear();
            ( ( ICollection<TY> ) _yColumn ).Clear();
            ( ( ICollection<TY> ) _y1Column ).Clear();
            Maybe.Do<DataSeriesAppendBuffer<ValueTuple<TX, TY, TY>>>( _appendBuffer, ( Action<DataSeriesAppendBuffer<ValueTuple<TX, TY, TY>>> ) delegate ( DataSeriesAppendBuffer<ValueTuple<TX, TY, TY>> b )
            {
                b.Clear();
            } );
        }
    }

    public override void Append( TX x, params TY[ ] yValues )
    {
        if ( yValues.Length != 2 )
        {
            ThrowWhenAppendInvalid( 2 );
        }
        Append( x, yValues[ 0 ], yValues[ 1 ] );
    }

    public override void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
    {
        if ( yValues.Length != 2 )
        {
            ThrowWhenAppendInvalid( 2 );
        }
        Append( x, yValues[ 0 ], yValues[ 1 ] );
    }

    public void Append( TX x, TY y0, TY y1 )
    {
        _appendBuffer.Append( ValueTuple.Create<TX, TY, TY>( x, y0, y1 ) );
        OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
    }

    public void Append( IEnumerable<TX> x, IEnumerable<TY> y0, IEnumerable<TY> y1 )
    {
        if ( !EnumerableExtensions.IsEmpty<TX>( x ) )
        {
            IEnumerator<TX> enumerator = x.GetEnumerator();
            IEnumerator<TY> enumerator2 = y0.GetEnumerator();
            IEnumerator<TY> enumerator3 = y1.GetEnumerator();
            lock ( _appendBuffer.SyncRoot )
            {
                while ( enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() )
                {
                    _appendBuffer.Append( ValueTuple.Create<TX, TY, TY>( enumerator.Current, enumerator2.Current, enumerator3.Current ) );
                }
            }
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }
    }

    public void Update( TX x, TY y0, TY y1 )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            int num = ((IList)_xColumn).FindIndex(base.IsSorted, (IComparable)(object)x, SearchMode.Exact);
            if ( num != -1 )
            {
                ( ( IList<TY> ) _yColumn )[ num ] = y0;
                ( ( IList<TY> ) _y1Column )[ num ] = y1;
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            }
        }
    }

    public void Insert( int index, TX x, TY y0, TY y1 )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            base.XValues.Insert( index, x );
            base.YValues.Insert( index, y0 );
            Y1Values.Insert( index, y1 );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnInsertXValue( _xColumn, index, x, base.AcceptsUnsortedData );
        }
    }

    public void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y0, IEnumerable<TY> y1 )
    {
        TY val = ArrayOperations.Minimum<TY>(y0);
        TY val2 = ArrayOperations.Maximum<TY>(y0);
        TY val3 = ArrayOperations.Minimum<TY>(y1);
        TY val4 = ArrayOperations.Maximum<TY>(y1);
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            int count = ((ISeriesColumn)_xColumn).Count;
            _xColumn.InsertRange( startIndex, x );
            int count2 = ((ISeriesColumn)_xColumn).Count;
            _yColumn.InsertRange( startIndex, y0 );
            _y1Column.InsertRange( startIndex, y1 );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnInsertXValues( _xColumn, startIndex, count2 - count, x, base.AcceptsUnsortedData );
        }
    }

    private void FlushAppendBuffer( IList<ValueTuple<TX, TY, TY>> bufferedValues )
    {
        lock ( base.SyncRoot )
        {
            IEnumerable<TX> enumerable = Enumerable.Select<ValueTuple<TX, TY, TY>, TX>((IEnumerable<ValueTuple<TX, TY, TY>>)bufferedValues, (Func<ValueTuple<TX, TY, TY>, TX>)((ValueTuple<TX, TY, TY> b) => b.Item1));
            int count = ((ISeriesColumn)_xColumn).Count;
            _xColumn.AddRange( enumerable );
            _yColumn.AddRange( Enumerable.Select<ValueTuple<TX, TY, TY>, TY>( ( IEnumerable<ValueTuple<TX, TY, TY>> ) bufferedValues, ( Func<ValueTuple<TX, TY, TY>, TY> ) ( ( ValueTuple<TX, TY, TY> b ) => b.Item2 ) ) );
            _y1Column.AddRange( Enumerable.Select<ValueTuple<TX, TY, TY>, TY>( ( IEnumerable<ValueTuple<TX, TY, TY>> ) bufferedValues, ( Func<ValueTuple<TX, TY, TY>, TY> ) ( ( ValueTuple<TX, TY, TY> b ) => b.Item3 ) ) );
            base.DataDistributionCalculator.OnAppendXValues( _xColumn, count, enumerable, base.AcceptsUnsortedData );
        }
    }
}
