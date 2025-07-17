using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StockSharp.Xaml.Charting.Common;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Numerics;
using StockSharp.Xaml.Charting.Numerics.PointResamplers;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public class OhlcDataSeries<TX, TY> : StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>, IOhlcDataSeries<TX, TY>, IDataSeries<TX, TY>, IDataSeries, ISuspendable, IOhlcDataSeries where TX : IComparable where TY : IComparable
    {
        private ISeriesColumn<TY> _openColumn = (ISeriesColumn<TY>) new SeriesColumn<TY>();
        private ISeriesColumn<TY> _highColumn = (ISeriesColumn<TY>) new SeriesColumn<TY>();
        private ISeriesColumn<TY> _lowColumn = (ISeriesColumn<TY>) new SeriesColumn<TY>();
        private ISeriesColumn<TY> _closeColumn;
        private readonly DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY, TY>> _appendBuffer;

        public OhlcDataSeries()
        {
            _closeColumn = _yColumn;
            _appendBuffer = new DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY, TY>>( new Action<IList<ValueTuple<TX, TY, TY, TY, TY>>>( FlushAppendBuffer ) );
        }

        public override IRange YRange
        {
            get
            {
                return RangeFactory.NewRange( ( IComparable ) ArrayOperations.Minimum<TY>( ( IEnumerable<TY> ) _lowColumn ), ( IComparable ) ArrayOperations.Maximum<TY>( ( IEnumerable<TY> ) _highColumn ) );
            }
        }

        public override bool HasValues
        {
            get
            {
                if ( _xColumn.HasValues && _openColumn.HasValues && ( _highColumn.HasValues && _lowColumn.HasValues ) )
                    return _closeColumn.HasValues;
                return false;
            }
        }

        public override DataSeriesType DataSeriesType
        {
            get
            {
                return DataSeriesType.Ohlc;
            }
        }

        public override HitTestInfo ToHitTestInfo( int index )
        {
            lock ( SyncRoot )
            {
                HitTestInfo hitTestInfo = base.ToHitTestInfo(index);
                if ( !hitTestInfo.IsEmpty() )
                {
                    hitTestInfo.OpenValue = ( IComparable ) OpenValues[ index ];
                    hitTestInfo.HighValue = ( IComparable ) HighValues[ index ];
                    hitTestInfo.LowValue = ( IComparable ) LowValues[ index ];
                    hitTestInfo.CloseValue = ( IComparable ) CloseValues[ index ];
                }
                return hitTestInfo;
            }
        }

        protected override void ClearColumns()
        {
            lock ( SyncRoot )
            {
                if ( FifoCapacity.HasValue )
                {
                    int size = FifoCapacity.Value;
                    _xColumn = ( ISeriesColumn<TX> ) new FifoSeriesColumn<TX>( size );
                    _openColumn = ( ISeriesColumn<TY> ) new FifoSeriesColumn<TY>( size );
                    _highColumn = ( ISeriesColumn<TY> ) new FifoSeriesColumn<TY>( size );
                    _lowColumn = ( ISeriesColumn<TY> ) new FifoSeriesColumn<TY>( size );
                    _yColumn = ( ISeriesColumn<TY> ) new FifoSeriesColumn<TY>( size );
                    _closeColumn = _yColumn;
                }
                else
                {
                    _xColumn = ( ISeriesColumn<TX> ) new SeriesColumn<TX>();
                    _openColumn = ( ISeriesColumn<TY> ) new SeriesColumn<TY>();
                    _highColumn = ( ISeriesColumn<TY> ) new SeriesColumn<TY>();
                    _lowColumn = ( ISeriesColumn<TY> ) new SeriesColumn<TY>();
                    _yColumn = ( ISeriesColumn<TY> ) new SeriesColumn<TY>();
                    _closeColumn = _yColumn;
                }
              ( ( ICollection<TX> ) _xColumn ).Clear();
                ( ( ICollection<TY> ) _openColumn ).Clear();
                ( ( ICollection<TY> ) _highColumn ).Clear();
                ( ( ICollection<TY> ) _lowColumn ).Clear();
                ( ( ICollection<TY> ) _closeColumn ).Clear();
                _appendBuffer.Do<DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY, TY>>>( ( Action<DataSeriesAppendBuffer<ValueTuple<TX, TY, TY, TY, TY>>> ) ( b => b.Clear() ) );
            }
        }

        public override void RemoveAt( int index )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                XValues.RemoveAt( index );
                YValues.RemoveAt( index );
                OpenValues.RemoveAt( index );
                HighValues.RemoveAt( index );
                LowValues.RemoveAt( index );
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
                _openColumn.RemoveRange( startIndex, count );
                _highColumn.RemoveRange( startIndex, count );
                _lowColumn.RemoveRange( startIndex, count );
                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.UpdateDataDistributionFlagsWhenRemovedXValues();
            }
        }

        public override IDataSeries<TX, TY> Clone()
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                OhlcDataSeries<TX, TY> ohlcDataSeries = new OhlcDataSeries<TX, TY>();
                ohlcDataSeries.FifoCapacity = FifoCapacity;
                ohlcDataSeries.AcceptsUnsortedData = AcceptsUnsortedData;
                ohlcDataSeries.Append( ( IEnumerable<TX> ) XValues, ( IEnumerable<TY> ) OpenValues, ( IEnumerable<TY> ) HighValues, ( IEnumerable<TY> ) LowValues, ( IEnumerable<TY> ) CloseValues );
                return ( IDataSeries<TX, TY> ) ohlcDataSeries;
            }
        }

        public override IPointSeries ToPointSeries( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isCategoryAxis, bool? dataIsDisplayedAs2D, IRange visibleXRange, IPointResamplerFactory factory, object pointSeriesArg = null )
        {
            OhlcLineDrawMode? nullable = pointSeriesArg as OhlcLineDrawMode?;
            if ( !nullable.HasValue )
            {
                lock ( SyncRoot )
                {
                    ResamplingMode resamplingMode1 = resamplingMode == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Mid;
                    ResamplingMode resamplingMode2 = resamplingMode == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Max;
                    ResamplingMode resamplingMode3 = resamplingMode == ResamplingMode.None ? ResamplingMode.None : ResamplingMode.Min;
                    IPointResampler pointResampler = factory.GetPointResampler<TX, TY>();
                    return ( IPointSeries ) new OhlcPointSeries( pointResampler.Execute( resamplingMode1, pointRange, viewportWidth, IsFifo, isCategoryAxis, ( IList ) _xColumn, ( IList ) _openColumn, new bool?( DataDistributionCalculator.DataIsSortedAscending ), new bool?( DataDistributionCalculator.DataIsEvenlySpaced ), dataIsDisplayedAs2D, visibleXRange ), pointResampler.Execute( resamplingMode2, pointRange, viewportWidth, IsFifo, isCategoryAxis, ( IList ) _xColumn, ( IList ) _highColumn, new bool?( DataDistributionCalculator.DataIsSortedAscending ), new bool?( DataDistributionCalculator.DataIsEvenlySpaced ), dataIsDisplayedAs2D, visibleXRange ), pointResampler.Execute( resamplingMode3, pointRange, viewportWidth, IsFifo, isCategoryAxis, ( IList ) _xColumn, ( IList ) _lowColumn, new bool?( DataDistributionCalculator.DataIsSortedAscending ), new bool?( DataDistributionCalculator.DataIsEvenlySpaced ), dataIsDisplayedAs2D, visibleXRange ), pointResampler.Execute( resamplingMode1, pointRange, viewportWidth, IsFifo, isCategoryAxis, ( IList ) _xColumn, ( IList ) _closeColumn, new bool?( DataDistributionCalculator.DataIsSortedAscending ), new bool?( DataDistributionCalculator.DataIsEvenlySpaced ), dataIsDisplayedAs2D, visibleXRange ) );
                }
            }
            else
            {
                ISeriesColumn<TY> seriesColumn = nullable.Value == OhlcLineDrawMode.Open ? _openColumn : (nullable.Value == OhlcLineDrawMode.High ? _highColumn : (nullable.Value == OhlcLineDrawMode.Low ? _lowColumn : _closeColumn));
                lock ( SyncRoot )
                    return factory.GetPointResampler<TX, TY>().Execute( resamplingMode, pointRange, viewportWidth, IsFifo, isCategoryAxis, ( IList ) _xColumn, ( IList ) seriesColumn, new bool?( DataDistributionCalculator.DataIsSortedAscending ), new bool?( DataDistributionCalculator.DataIsEvenlySpaced ), dataIsDisplayedAs2D, visibleXRange );
            }
        }

        public override void OnBeginRenderPass()
        {
            base.OnBeginRenderPass();
            _appendBuffer.Flush();
        }

        IList IOhlcDataSeries.OpenValues
        {
            get
            {
                return ( IList ) _openColumn;
            }
        }

        IList IOhlcDataSeries.HighValues
        {
            get
            {
                return ( IList ) _highColumn;
            }
        }

        IList IOhlcDataSeries.LowValues
        {
            get
            {
                return ( IList ) _lowColumn;
            }
        }

        IList IOhlcDataSeries.CloseValues
        {
            get
            {
                return ( IList ) _closeColumn;
            }
        }

        public IList<TY> OpenValues
        {
            get
            {
                return ( IList<TY> ) _openColumn;
            }
        }

        public IList<TY> HighValues
        {
            get
            {
                return ( IList<TY> ) _highColumn;
            }
        }

        public IList<TY> LowValues
        {
            get
            {
                return ( IList<TY> ) _lowColumn;
            }
        }

        public IList<TY> CloseValues
        {
            get
            {
                return ( IList<TY> ) _closeColumn;
            }
        }

        public override void Append( TX x, params TY[ ] yValues )
        {
            if ( yValues.Length != 4 )
                ThrowWhenAppendInvalid( 4 );
            Append( x, yValues[ 0 ], yValues[ 1 ], yValues[ 2 ], yValues[ 3 ] );
        }

        public override void Append( IEnumerable<TX> x, params IEnumerable<TY>[ ] yValues )
        {
            if ( yValues.Length != 4 )
                ThrowWhenAppendInvalid( 4 );
            Append( x, yValues[ 0 ], yValues[ 1 ], yValues[ 2 ], yValues[ 3 ] );
        }

        public void Append( TX x, TY open, TY high, TY low, TY close )
        {
            _appendBuffer.Append( ValueTuple.Create<TX, TY, TY, TY, TY>( x, open, high, low, close ) );
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }

        public void Append( IEnumerable<TX> x, IEnumerable<TY> open, IEnumerable<TY> high, IEnumerable<TY> low, IEnumerable<TY> close )
        {
            if ( x.IsEmpty<TX>() )
                return;
            IEnumerator<TX> enumerator1 = x.GetEnumerator();
            IEnumerator<TY> enumerator2 = open.GetEnumerator();
            IEnumerator<TY> enumerator3 = high.GetEnumerator();
            IEnumerator<TY> enumerator4 = low.GetEnumerator();
            IEnumerator<TY> enumerator5 = close.GetEnumerator();
            lock ( _appendBuffer.SyncRoot )
            {
                while ( enumerator1.MoveNext() )
                {
                    if ( enumerator2.MoveNext() )
                    {
                        if ( enumerator3.MoveNext() )
                        {
                            if ( enumerator4.MoveNext() )
                            {
                                if ( enumerator5.MoveNext() )
                                    _appendBuffer.Append( ValueTuple.Create<TX, TY, TY, TY, TY>( enumerator1.Current, enumerator2.Current, enumerator3.Current, enumerator4.Current, enumerator5.Current ) );
                                else
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
        }

        public void Update( TX x, TY open, TY high, TY low, TY close )
        {
            lock ( base.SyncRoot )
            {
                _appendBuffer.Flush();

                int num = ((IList)_xColumn).FindIndex(base.IsSorted, (IComparable)(object)x, SearchMode.Exact);

                if ( num != -1 )
                {
                    ( ( IList<TY> ) _openColumn )[ num ] = open;
                    ( ( IList<TY> ) _highColumn )[ num ] = high;
                    ( ( IList<TY> ) _lowColumn )[ num ] = low;
                    ( ( IList<TY> ) _closeColumn )[ num ] = close;

                    OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                }
            }
        }

        public void Insert( int index, TX x, TY open, TY high, TY low, TY close )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();

                XValues.Insert( index, x );
                OpenValues.Insert( index, open );
                HighValues.Insert( index, high );
                LowValues.Insert( index, low );
                CloseValues.Insert( index, close );

                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.OnInsertXValue( _xColumn, index, x, AcceptsUnsortedData );
            }
        }

        public void InsertRange( int startIndex, IEnumerable<TX> x, IEnumerable<TY> open, IEnumerable<TY> high, IEnumerable<TY> low, IEnumerable<TY> close )
        {
            lock ( SyncRoot )
            {
                _appendBuffer.Flush();
                int count1 = ((ISeriesColumn) _xColumn).Count;
                _xColumn.InsertRange( startIndex, x );
                int count2 = ((ISeriesColumn) _xColumn).Count;

                _openColumn.InsertRange( startIndex, open );
                _highColumn.InsertRange( startIndex, high );
                _lowColumn.InsertRange( startIndex, low );
                _closeColumn.InsertRange( startIndex, close );

                OnDataSeriesChanged( DataSeriesUpdate.DataChanged );
                DataDistributionCalculator.OnInsertXValues( _xColumn, startIndex, count2 - count1, x, AcceptsUnsortedData );
            }
        }

        public override TY GetYMaxAt( int index, TY existingYMax )
        {
            TY highValue = HighValues[index];
            if ( StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( highValue ) )
                return existingYMax;
            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Max( existingYMax, highValue );
        }

        public override TY GetYMinAt( int index, TY existingYMin )
        {
            TY lowValue = LowValues[index];
            if ( StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.IsNaN( lowValue ) )
                return existingYMin;
            return StockSharp.Xaml.Charting.Model.DataSeries.DataSeries<TX, TY>.YMath.Min( existingYMin, lowValue );
        }

        private void FlushAppendBuffer( IList<ValueTuple<TX, TY, TY, TY, TY>> bufferedValues )
        {
            lock ( SyncRoot )
            {
                IEnumerable<TX> xes = bufferedValues.Select<ValueTuple<TX, TY, TY, TY, TY>, TX>((Func<ValueTuple<TX, TY, TY, TY, TY>, TX>) (b => b.Item1));
                int count = ((ISeriesColumn) _xColumn).Count;

                _xColumn.AddRange( xes );
                _openColumn.AddRange( bufferedValues.Select<ValueTuple<TX, TY, TY, TY, TY>, TY>( ( Func<ValueTuple<TX, TY, TY, TY, TY>, TY> ) ( b => b.Item2 ) ) );
                _highColumn.AddRange( bufferedValues.Select<ValueTuple<TX, TY, TY, TY, TY>, TY>( ( Func<ValueTuple<TX, TY, TY, TY, TY>, TY> ) ( b => b.Item3 ) ) );
                _lowColumn.AddRange( bufferedValues.Select<ValueTuple<TX, TY, TY, TY, TY>, TY>( ( Func<ValueTuple<TX, TY, TY, TY, TY>, TY> ) ( b => b.Item4 ) ) );
                _closeColumn.AddRange( bufferedValues.Select<ValueTuple<TX, TY, TY, TY, TY>, TY>( ( Func<ValueTuple<TX, TY, TY, TY, TY>, TY> ) ( b => b.Item5 ) ) );
                DataDistributionCalculator.OnAppendXValues( _xColumn, count, xes, AcceptsUnsortedData );
            }
        }
    }
}
