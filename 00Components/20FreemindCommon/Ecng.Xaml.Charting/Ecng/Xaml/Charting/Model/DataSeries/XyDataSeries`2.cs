// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.XyDataSeries`2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ecng.Xaml.Charting.Common;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Numerics;
using Ecng.Xaml.Charting.Numerics.PointResamplers;
using Ecng.Xaml.Charting.Visuals;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    public class XyDataSeries<TX, TY> : Ecng.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>, IXyDataSeries<TX, TY>, IDataSeries<TX, TY>, IDataSeries, ISuspendable, IXyDataSeries where TX : IComparable where TY : IComparable
    {
        private readonly DataSeriesAppendBuffer<ValueTuple<TX, TY>> _appendBuffer;

        public XyDataSeries()
        {
            _appendBuffer = new DataSeriesAppendBuffer<ValueTuple<TX, TY>>( new Action<IList<ValueTuple<TX, TY>>>( FlushAppendBuffer ) );
        }

        public override DataSeriesType DataSeriesType
        {
            get
            {
                return DataSeriesType.Xy;
            }
        }

        public override bool HasValues
        {
            get
            {
                if ( _xColumn.HasValues )
                    return _yColumn.HasValues;
                return false;
            }
        }

        public override IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
        {
            lock ( SyncRoot )
                return factory.GetPointResampler<TX, TY>().Execute( resamplingMode, pointRange, viewportWidth, IsFifo, isCategoryAxis, ( IList ) _xColumn, ( IList ) _yColumn, new bool?( DataDistributionCalculator.DataIsSortedAscending ), new bool?( DataDistributionCalculator.DataIsEvenlySpaced ), dataIsDisplayedAs2D, visibleXRange );
        }

        public override void OnBeginRenderPass()
        {
            base.OnBeginRenderPass();
            _appendBuffer.Flush();
        }

        protected override void ClearColumns()
        {
            lock ( SyncRoot )
            {
                if ( FifoCapacity.HasValue )
                {
                    int size = FifoCapacity.Value;
                    _xColumn = ( ISeriesColumn<TX> ) new FifoSeriesColumn<TX>( size );
                    _yColumn = ( ISeriesColumn<TY> ) new FifoSeriesColumn<TY>( size );
                }
                else
                {
                    _xColumn = ( ISeriesColumn<TX> ) new SeriesColumn<TX>();
                    _yColumn = ( ISeriesColumn<TY> ) new SeriesColumn<TY>();
                }
              ( ( ICollection<TX> ) _xColumn ).Clear();
                ( ( ICollection<TY> ) _yColumn ).Clear();
                _appendBuffer.Do<DataSeriesAppendBuffer<ValueTuple<TX, TY>>>( ( Action<DataSeriesAppendBuffer<ValueTuple<TX, TY>>> ) ( b => b.Clear() ) );
            }
        }

        public override void RemoveAt( int index )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                TY yvalue = YValues[index];
                TX xvalue = XValues[index];
                XValues.RemoveAt( index );
                YValues.RemoveAt( index );
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
            }
        }

        public override void RemoveRange( int startIndex, int count )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                _xColumn.RemoveRange( startIndex, count );
                _yColumn.RemoveRange( startIndex, count );
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
            }
        }

        public override IDataSeries<TX, TY> Clone()
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                XyDataSeries<TX, TY> xyDataSeries = new XyDataSeries<TX, TY>();
                xyDataSeries.FifoCapacity = FifoCapacity;
                xyDataSeries.AcceptsUnsortedData = AcceptsUnsortedData;
                xyDataSeries.Append( ( IEnumerable<TX> ) XValues, ( IEnumerable<TY> ) YValues );
                return ( IDataSeries<TX, TY> ) xyDataSeries;
            }
        }

        public override void Append( TX x, params TY[ ] yValues )
        {
            if ( yValues.Length != 1 )
                ThrowWhenAppendInvalid( 1 );
            Append( x, yValues[ 0 ] );
        }

        public override void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
        {
            if ( yValues.Length != 1 )
                ThrowWhenAppendInvalid( 1 );
            Append( x, yValues[ 0 ] );
        }

        public virtual void Append( TX x, TY y )
        {
            _appendBuffer.Append( ValueTuple.Create<TX, TY>( x, y ) );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }

        public virtual void Append( IEnumerable<TX> x, IEnumerable<TY> y )
        {
            if ( x.IsEmpty<TX>() )
                return;
            IEnumerator<TX> enumerator1 = x.GetEnumerator();
            IEnumerator<TY> enumerator2 = y.GetEnumerator();
            lock ( _appendBuffer.SyncRoot )
            {
                while ( enumerator1.MoveNext() )
                {
                    if ( enumerator2.MoveNext() )
                        _appendBuffer.Append( ValueTuple.Create<TX, TY>( enumerator1.Current, enumerator2.Current ) );
                    else
                        break;
                }
            }
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }

        public virtual void Update( TX x, TY y )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                int index = ( (IList)_xColumn ).FindIndex(IsSorted, (IComparable) x, SearchMode.Exact);
                if ( index == -1 )
                    return;

                ( ( IList ) _yColumn )[ index ] = y;
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
            }
        }

        public virtual void Insert( int index, TX x, TY y )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                XValues.Insert( index, x );
                YValues.Insert( index, y );
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.OnInsertXValue( _xColumn, index, x, AcceptsUnsortedData );
            }
        }

        public virtual void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> y )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                int count1 = ((ISeriesColumn) _xColumn).Count;
                _xColumn.InsertRange( startIndex, x );
                int count2 = ((ISeriesColumn) _xColumn).Count;
                _yColumn.InsertRange( startIndex, y );
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.OnInsertXValues( _xColumn, startIndex, count2 - count1, x, AcceptsUnsortedData );
            }
        }

        public override TY GetYMinAt( int index, TY existingYMin )
        {
            TY yvalue = YValues[index];
            if ( !Ecng.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( yvalue ) )
                return Ecng.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Min( existingYMin, yvalue );
            return existingYMin;
        }

        public override TY GetYMaxAt( int index, TY existingYMax )
        {
            TY yvalue = YValues[index];
            if ( !Ecng.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( yvalue ) )
                return Ecng.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Max( existingYMax, yvalue );
            return existingYMax;
        }

        private void FlushAppendBuffer( IList<ValueTuple<TX, TY>> bufferedValues )
        {
            lock ( SyncRoot )
            {
                IEnumerable<TX> xes = bufferedValues.Select<ValueTuple<TX, TY>, TX>((Func<ValueTuple<TX, TY>, TX>) (b => b.Item1));
                int count = ((ISeriesColumn) _xColumn).Count;
                _xColumn.AddRange( xes );
                _yColumn.AddRange( bufferedValues.Select<ValueTuple<TX, TY>, TY>( ( Func<ValueTuple<TX, TY>, TY> ) ( b => b.Item2 ) ) );
                DataDistributionCalculator.OnAppendXValues( _xColumn, count, xes, AcceptsUnsortedData );
            }
        }
    }
}
