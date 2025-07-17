// StockSharp.Xaml.Charting.Model.DataSeries.XyzDataSeries<TX,TY,TZ>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.Common;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Numerics.GenericMath;
using StockSharp.Xaml.Charting.Numerics.PointResamplers;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

public class XyzDataSeries<TX, TY, TZ> : DataSeries<TX, TY>, IXyzDataSeries<TX, TY, TZ>, IDataSeries<TX, TY>, IDataSeries, ISuspendable, IXyzDataSeries where TX : IComparable where TY : IComparable where TZ : IComparable
{
    public static readonly IMath<TZ> ZMath = GenericMathFactory.New<TZ>();

    private readonly DataSeriesAppendBuffer<ValTuple<TX, TY, TZ>> _appendBuffer;

    protected ISeriesColumn<TZ> _zColumn = new SeriesColumn<TZ>();

    private TZ _zMin;

    private TZ _zMax;

    public override DataSeriesType DataSeriesType => DataSeriesType.Xyz;

    IList IXyzDataSeries.ZValues
    {
        get
        {
            return _zColumn;
        }
    }

    public IList<TZ> ZValues => _zColumn;

    public override bool HasValues
    {
        get
        {
            if ( _xColumn.HasValues && _yColumn.HasValues )
            {
                return _zColumn.HasValues;
            }
            return false;
        }
    }

    public XyzDataSeries()
    {
        _appendBuffer = new DataSeriesAppendBuffer<ValTuple<TX, TY, TZ>>( FlushAppendBuffer );
    }

    public override void RemoveAt( int index )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            base.XValues.RemoveAt( index );
            base.YValues.RemoveAt( index );
            ZValues.RemoveAt( index );
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
            _zColumn.RemoveRange( startIndex, count );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
        }
    }

    public override IDataSeries<TX, TY> Clone()
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            XyzDataSeries<TX, TY, TZ> xyzDataSeries = new XyzDataSeries<TX, TY, TZ>();
            xyzDataSeries.FifoCapacity = base.FifoCapacity;
            xyzDataSeries.AcceptsUnsortedData = base.AcceptsUnsortedData;
            xyzDataSeries.Append( base.XValues, base.YValues, ZValues );
            return xyzDataSeries;
        }
    }

    public override TY GetYMinAt( int index, TY existingYMin )
    {
        TY val = base.YValues[index];
        if ( !DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return DataSeries<TX, TY>.YMath.Min( existingYMin, val );
        }
        return existingYMin;
    }

    public override TY GetYMaxAt( int index, TY existingYMax )
    {
        TY val = base.YValues[index];
        if ( !DataSeries<TX, TY>.YMath.IsNaN( val ) )
        {
            return DataSeries<TX, TY>.YMath.Max( existingYMax, val );
        }
        return existingYMax;
    }

    public override IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
    {
        lock ( base.SyncRoot )
        {
            IPointSeries yPoints = factory.GetPointResampler<TX, TY>().Execute(resamplingMode, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _yColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            IPointSeries zPoints = factory.GetPointResampler<TX, TZ>().Execute(resamplingMode, pointRange, viewportWidth, base.IsFifo, isCategoryAxis, _xColumn, _zColumn, base.DataDistributionCalculator.DataIsSortedAscending, base.DataDistributionCalculator.DataIsEvenlySpaced, dataIsDisplayedAs2D, visibleXRange);
            return new XyzPointSeries( yPoints, zPoints );
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
                TZ val = ZValues[index];
                result.ZValue = ( IComparable ) ( object ) val;
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
                _zColumn = new FifoSeriesColumn<TZ>( value );
            }
            else
            {
                _xColumn = new SeriesColumn<TX>();
                _yColumn = new SeriesColumn<TY>();
                _zColumn = new SeriesColumn<TZ>();
            }
            ( ( ICollection<TX> ) _xColumn ).Clear();
            ( ( ICollection<TY> ) _yColumn ).Clear();
            ( ( ICollection<TZ> ) _zColumn ).Clear();
            Maybe.Do<DataSeriesAppendBuffer<ValTuple<TX, TY, TZ>>>( _appendBuffer, ( Action<DataSeriesAppendBuffer<ValTuple<TX, TY, TZ>>> ) delegate ( DataSeriesAppendBuffer<ValTuple<TX, TY, TZ>> b )
            {
                b.Clear();
            } );
        }
    }

    public override void Append( TX x, params TY[ ] yValues )
    {
        throw new InvalidOperationException( $"Append(TX x, params TY[] yValues) in type {GetType().Name} must receive X, Y and Z values. Please use the Append(x,y,z) overload" );
    }

    public override void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
    {
        throw new InvalidOperationException( $"Append(TX x, params TY[] yValues) in type {GetType().Name} must receive X, Y and Z values. Please use the Append(x,y,z) overload" );
    }

    public void Append( TX x, TY y, TZ z )
    {
        _appendBuffer.Append( ValTuple.Create<TX, TY, TZ>( x, y, z ) );
        OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
    }

    public void Append( IEnumerable<TX> x, IEnumerable<TY> y, IEnumerable<TZ> z )
    {
        if ( !EnumerableExtensions.IsEmpty<TX>( x ) )
        {
            IEnumerator<TX> enumerator = x.GetEnumerator();
            IEnumerator<TY> enumerator2 = y.GetEnumerator();
            IEnumerator<TZ> enumerator3 = z.GetEnumerator();
            lock ( _appendBuffer.SyncRoot )
            {
                while ( enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() )
                {
                    _appendBuffer.Append( ValTuple.Create<TX, TY, TZ>( enumerator.Current, enumerator2.Current, enumerator3.Current ) );
                }
            }
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }
    }

    public void Update( TX x, TY y, TZ z )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            int num = ((IList)_xColumn).FindIndex(base.IsSorted, (IComparable)(object)x, SearchMode.Exact);
            if ( num != -1 )
            {
                ( ( IList<TY> ) _yColumn )[ num ] = y;
                ( ( IList<TZ> ) _zColumn )[ num ] = z;
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            }
        }
    }

    public void Insert( int index, TX x, TY y, TZ z )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            base.XValues.Insert( index, x );
            base.YValues.Insert( index, y );
            ZValues.Insert( index, z );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnInsertXValue( _xColumn, index, x, base.AcceptsUnsortedData );
        }
    }

    public void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y, IEnumerable<TZ> z )
    {
        lock ( base.SyncRoot )
        {
            _appendBuffer.Flush();
            int count = ((ISeriesColumn)_xColumn).Count;
            _xColumn.InsertRange( startIndex, x );
            int count2 = ((ISeriesColumn)_xColumn).Count;
            _yColumn.InsertRange( startIndex, y );
            _zColumn.InsertRange( startIndex, z );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            base.DataDistributionCalculator.OnInsertXValues( _xColumn, startIndex, count2 - count, x, base.AcceptsUnsortedData );
        }
    }

    private void FlushAppendBuffer( IList<ValTuple<TX, TY, TZ>> bufferedValues )
    {
        lock ( base.SyncRoot )
        {
            IEnumerable<TX> enumerable = Enumerable.Select<ValTuple<TX, TY, TZ>, TX>((IEnumerable<ValTuple<TX, TY, TZ>>)bufferedValues, (Func<ValTuple<TX, TY, TZ>, TX>)((ValTuple<TX, TY, TZ> b) => b.Item1));
            int count = ((ISeriesColumn)_xColumn).Count;
            _xColumn.AddRange( enumerable );
            _yColumn.AddRange( Enumerable.Select<ValTuple<TX, TY, TZ>, TY>( ( IEnumerable<ValTuple<TX, TY, TZ>> ) bufferedValues, ( Func<ValTuple<TX, TY, TZ>, TY> ) ( ( ValTuple<TX, TY, TZ> b ) => b.Item2 ) ) );
            _zColumn.AddRange( Enumerable.Select<ValTuple<TX, TY, TZ>, TZ>( ( IEnumerable<ValTuple<TX, TY, TZ>> ) bufferedValues, ( Func<ValTuple<TX, TY, TZ>, TZ> ) ( ( ValTuple<TX, TY, TZ> b ) => b.Item3 ) ) );
            base.DataDistributionCalculator.OnAppendXValues( _xColumn, count, enumerable, base.AcceptsUnsortedData );
        }
    }
}
