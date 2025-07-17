// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Extensions.EnumerableExtensions
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics.GenericMath;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Common.Extensions
{
    public static class EnumerableExtensions
    {
        private static bool _isLokingSameColors;
        private static Point _prevPoint;
        private static Color? _prevPointColor;

        internal static UIElement SingleOrDefault( this UIElementCollection collection, Predicate<UIElement> predicate )
        {
            foreach ( object obj in collection )
            {
                if ( predicate( ( UIElement ) obj ) )
                {
                    return ( UIElement ) obj;
                }
            }
            return ( UIElement ) null;
        }

        internal static DoubleRange GetRange( this IList<double> list )
        {
            double min;
            double max;
            ArrayOperations.MinMax<double>( list is FifoSeriesColumn<double> ? ( IEnumerable<double> ) ( ( FifoSeriesColumn<double> ) list ).ToUnorderedUncheckedList() : ( IEnumerable<double> ) list.ToUncheckedList<double>(), out min, out max );
            return new DoubleRange( min, max );
        }

        internal static double[ ] ToDoubleArray<T>( this IList<T> list )
        {
            return list.ToUncheckedList<T>() as double[ ];
        }

        internal static UncheckedList<T> ToUncheckedList<T>( this IList<T> list, IndexRange indexRange, bool allowCopy )
        {
            int count = indexRange.Max - indexRange.Min + 1;
            UncheckedList<T> uncheckedList = list as UncheckedList<T>;
            if ( uncheckedList != null )
            {
                return new UncheckedList<T>( uncheckedList.Array, indexRange.Min + uncheckedList.BaseIndex, count );
            }

            BaseSeriesColumn<T> baseSeriesColumn = list as BaseSeriesColumn<T>;
            if ( baseSeriesColumn != null )
            {
                return baseSeriesColumn.ToUncheckedList( indexRange.Min, count );
            }

            T[] array = list as T[];
            if ( array != null )
            {
                return new UncheckedList<T>( array, indexRange.Min, count );
            }

            if ( list is List<T> )
            {
                return new UncheckedList<T>( ( T[ ] ) typeof( List<T> ).GetField( "_items", BindingFlags.Instance | BindingFlags.NonPublic ).GetValue( ( object ) list ), indexRange.Min, count );
            }

            if ( allowCopy )
            {
                return new UncheckedList<T>( list.ToArray<T>(), indexRange.Min, count );
            }

            return ( UncheckedList<T> ) null;
        }

        internal static UncheckedList<T> ToUncheckedList<T>( this IList<T> list, bool allowCopy )
        {
            UncheckedList<T> uncheckedList = list as UncheckedList<T>;
            if ( uncheckedList != null )
            {
                return uncheckedList;
            }

            BaseSeriesColumn<T> baseSeriesColumn = list as BaseSeriesColumn<T>;
            if ( baseSeriesColumn != null )
            {
                return baseSeriesColumn.ToUncheckedList( 0, baseSeriesColumn.Count );
            }

            T[] array = list as T[];
            if ( array != null )
            {
                return new UncheckedList<T>( array );
            }

            if ( list is List<T> )
            {
                return new UncheckedList<T>( ( T[ ] ) typeof( List<T> ).GetField( "_items", BindingFlags.Instance | BindingFlags.NonPublic ).GetValue( ( object ) list ) );
            }

            if ( allowCopy )
            {
                return new UncheckedList<T>( list.ToArray<T>() );
            }

            return ( UncheckedList<T> ) null;
        }

        internal static T[ ] ToUncheckedList<T>( this IList<T> list )
        {
            SeriesColumn<T> seriesColumn = list as SeriesColumn<T>;
            if ( seriesColumn != null )
            {
                return seriesColumn.UncheckedArray();
            }

            T[] objArray = list as T[];
            if ( objArray != null )
            {
                return objArray;
            }

            UltraList<T> ultraList = list as UltraList<T>;
            if ( ultraList != null )
            {
                return ultraList.ItemsArray;
            }

            if ( list is List<T> )
            {
                return ( T[ ] ) typeof( List<T> ).GetField( "_items", BindingFlags.Instance | BindingFlags.NonPublic ).GetValue( ( object ) list );
            }

            FifoSeriesColumn<T> fifoSeriesColumn = list as FifoSeriesColumn<T>;
            if ( fifoSeriesColumn != null )
            {
                return fifoSeriesColumn.ToArray();
            }

            return list.ToArray<T>();
        }

        internal static bool IsNullOrEmptyList( this IList enumerable )
        {
            if ( enumerable != null )
            {
                return enumerable.Count == 0;
            }

            return true;
        }

        internal static bool IsNullOrEmpty<T>( this IEnumerable<T> enumerable )
        {
            if ( enumerable != null )
            {
                return !enumerable.Any<T>();
            }

            return true;
        }

        internal static bool IsEmpty<T>( this IEnumerable<T> enumerable )
        {
            return !enumerable.Any<T>();
        }

        internal static void ForEachDo<T>( this IEnumerable enumerable, Action<T> operation )
        {
            if ( enumerable == null )
            {
                return;
            }

            EnumerableExtensions.ForEachDo<T>( ( IEnumerable<T> ) enumerable.OfType<IRenderableSeries>(), operation );
        }

        internal static void ForEachDo<T>( this IEnumerable<T> enumerable, Action<T> operation )
        {
            if ( enumerable == null )
            {
                return;
            }

            Guard.NotNull( ( object ) operation, nameof( operation ) );
            foreach ( T obj in enumerable )
            {
                operation( obj );
            }
        }

        internal static void RemoveWhere<T>( this IList<T> collection, Predicate<T> predicate )
        {
            for ( int index = 0 ; index < collection.Count ; ++index )
            {
                if ( predicate( collection[ index ] ) )
                {
                    collection.RemoveAt( index-- );
                }
            }
        }

        internal static void AddIfNotContains<T>( this IList<T> collection, T item )
        {
            if ( collection.Contains( item ) )
            {
                return;
            }

            collection.Add( item );
        }

        public static int FindIndex<T>( this IList<T> list, bool isSorted, IComparable value, SearchMode searchMode ) where T : IComparable
        {
            return ( ( IList ) list ).FindIndex( isSorted, value, searchMode );
        }

        public static int FindIndex( this IList list, bool isSorted, IComparable value, SearchMode searchMode )
        {
            if ( list == null )
            {
                throw new ArgumentNullException( nameof( list ) );
            }

            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            Comparer<IComparable> comparer = Comparer<IComparable>.Default;
            if ( isSorted )
            {
                return list.FindIndexInSortedData( value, ( IComparer<IComparable> ) comparer, searchMode );
            }

            if ( searchMode == SearchMode.Exact )
            {
                return list.IndexOf( ( object ) value );
            }

            throw new NotImplementedException( string.Format( "Unsorted data occurs in the collection. The only allowed SearchMode is {0} when FindIndex() is called on an unsorted collection, but {1} was passed in.", ( object ) SearchMode.Exact, ( object ) searchMode ) );
        }

        private static int FindIndexInSortedData( this IList list, IComparable value, IComparer<IComparable> comparer, SearchMode searchMode )
        {
            if ( list.Count == 0 )
            {
                return -1;
            }

            if ( comparer.Compare( value, ( IComparable ) list[ 0 ] ) < 0 )
            {
                return searchMode != SearchMode.Exact ? 0 : -1;
            }

            if ( comparer.Compare( value, ( IComparable ) list[ 0 ] ) == 0 )
            {
                return 0;
            }

            if ( comparer.Compare( value, ( IComparable ) list[ list.Count - 1 ] ) == 0 )
            {
                return list.Count - 1;
            }

            if ( comparer.Compare( value, ( IComparable ) list[ list.Count - 1 ] ) > 0 )
            {
                if ( searchMode != SearchMode.Exact )
                {
                    return list.Count - 1;
                }

                return -1;
            }
            int lower = 0;
            int upper = list.Count - 1;
            while ( lower <= upper )
            {
                int index = (lower + upper) / 2;
                int num = comparer.Compare(value, (IComparable) list[index]);
                if ( num == 0 )
                {
                    return index;
                }

                if ( num < 0 )
                {
                    upper = index - 1;
                }
                else
                {
                    lower = index + 1;
                }
            }
            if ( searchMode == SearchMode.Exact )
            {
                return -1;
            }

            if ( searchMode == SearchMode.Nearest )
            {
                return EnumerableExtensions.GetNearestMiddleIndex( list, lower, upper, value );
            }

            int num1 = (lower + upper) / 2;
            if ( searchMode != SearchMode.RoundDown )
            {
                return num1 + 1;
            }

            return num1;
        }

        internal static int FindIndexInSortedData<TX>( this TX[ ] array, int arrayLength, TX value, SearchMode searchMode, IMath<TX> math ) where TX : IComparable
        {
            if ( arrayLength == 0 )
            {
                return -1;
            }

            if ( value.CompareTo( ( object ) array[ 0 ] ) < 0 )
            {
                return searchMode != SearchMode.Exact ? 0 : -1;
            }

            if ( value.CompareTo( ( object ) array[ 0 ] ) == 0 )
            {
                return 0;
            }

            if ( value.CompareTo( ( object ) array[ arrayLength - 1 ] ) == 0 )
            {
                return arrayLength - 1;
            }

            if ( value.CompareTo( ( object ) array[ arrayLength - 1 ] ) > 0 )
            {
                if ( searchMode != SearchMode.Exact )
                {
                    return arrayLength - 1;
                }

                return -1;
            }
            int lower = 0;
            int upper = arrayLength - 1;
            while ( lower <= upper )
            {
                int index = (lower + upper) / 2;
                int num = value.CompareTo((object) array[index]);
                if ( num == 0 )
                {
                    return index;
                }

                if ( num < 0 )
                {
                    upper = index - 1;
                }
                else
                {
                    lower = index + 1;
                }
            }
            if ( searchMode == SearchMode.Exact )
            {
                return -1;
            }

            if ( searchMode == SearchMode.Nearest )
            {
                return EnumerableExtensions.GetNearestMiddleIndex<TX>( array, arrayLength, lower, upper, value, math );
            }

            int num1 = (lower + upper) / 2;
            if ( searchMode != SearchMode.RoundDown )
            {
                return num1 + 1;
            }

            return num1;
        }

        private static int GetNearestMiddleIndex( IList list, int lower, int upper, IComparable value )
        {
            if ( lower > upper )
            {
                int num = upper;
                upper = lower;
                lower = num;
            }
            upper = NumberUtil.Constrain( upper, 0, list.Count - 1 );
            lower = NumberUtil.Constrain( lower, 0, list.Count - 1 );
            double num1 = ((IComparable) list[lower]).ToDouble();
            double num2 = ((IComparable) list[upper]).ToDouble();
            double num3 = value.ToDouble();
            if ( num3 - num1 < num2 - num3 )
            {
                return lower;
            }

            return upper;
        }

        private static int GetNearestMiddleIndex<TX>( TX[ ] array, int arrayLength, int lower, int upper, TX value, IMath<TX> math ) where TX : IComparable
        {
            if ( lower > upper )
            {
                int num = upper;
                upper = lower;
                lower = num;
            }
            upper = NumberUtil.Constrain( upper, 0, arrayLength - 1 );
            lower = NumberUtil.Constrain( lower, 0, arrayLength - 1 );
            TX b = array[lower];
            TX a = array[upper];
            TX x = value;
            if ( math.Subtract( x, b ).CompareTo( ( object ) math.Subtract( a, x ) ) < 0 )
            {
                return lower;
            }

            return upper;
        }

        internal static void AddRange<T>( this ObservableCollection<T> collection, IEnumerable<T> values )
        {
            foreach ( T obj in values )
            {
                collection.Add( obj );
            }
        }

        internal static IEnumerable<IEnumerable<Tuple<Point, Point>>> SplitMultilineByGaps( this IEnumerable<Tuple<Point, Point>> points )
        {
            IEnumerator<Tuple<Point, Point>> iterator = points.GetEnumerator();
            if ( iterator.MoveNext() )
            {
                while ( EnumerableExtensions.EnumerateUntilNonGap( iterator ) )
                {
                    yield return EnumerableExtensions.EnumerateUntilGap( iterator );
                    if ( !iterator.MoveNext() )
                    {
                        break;
                    }
                }
            }
        }

        private static bool EnumerateUntilNonGap( IEnumerator<Tuple<Point, Point>> iterator )
        {
            if ( !double.IsNaN( iterator.Current.Item1.X ) && !double.IsNaN( iterator.Current.Item1.Y ) )
            {
                return true;
            }

            while ( iterator.MoveNext() )
            {
                if ( !double.IsNaN( iterator.Current.Item1.X ) && !double.IsNaN( iterator.Current.Item1.Y ) )
                {
                    return true;
                }
            }
            return false;
        }

        private static IEnumerable<Tuple<Point, Point>> EnumerateUntilGap( IEnumerator<Tuple<Point, Point>> iterator )
        {
            if ( !double.IsNaN( iterator.Current.Item1.X ) && !double.IsNaN( iterator.Current.Item1.Y ) )
            {
                yield return iterator.Current;
                while ( iterator.MoveNext() && ( !double.IsNaN( iterator.Current.Item1.X ) && !double.IsNaN( iterator.Current.Item1.Y ) ) )
                {
                    yield return iterator.Current;
                }
            }
        }

        internal static IEnumerable<SeriesInfo> SplitToSinglePointInfo( this IEnumerable<SeriesInfo> infos )
        {
            IEnumerator<SeriesInfo> enumerator = infos.GetEnumerator();
            while ( enumerator.MoveNext() )
            {
                BandSeriesInfo current = enumerator.Current as BandSeriesInfo;
                if ( current != null )
                {
                    HitTestInfo hitTestInfo = new HitTestInfo()
                    {
                        YValue = current.Y1Value,
                        Y1Value = current.YValue,
                        HitTestPoint = current.Xy1Coordinate,
                        XValue = current.XValue,
                        DataSeriesIndex = current.DataSeriesIndex,
                        DataSeriesType = current.DataSeriesType,
                        IsHit = current.IsHit
                    };
                    yield return ( SeriesInfo ) new BandSeriesInfo( current.RenderableSeries, hitTestInfo )
                    {
                        IsFirstSeries = true
                    };
                }
                yield return enumerator.Current;
            }
        }

        public static T? MaxOrNullable<T>( this IEnumerable<T> that ) where T : struct, IComparable
        {
            if ( !that.Any<T>() )
            {
                return new T?();
            }

            return new T?( that.Max<T>() );
        }

        public static T? MinOrNullable<T>( this IEnumerable<T> that ) where T : struct, IComparable
        {
            if ( !that.Any<T>() )
            {
                return new T?();
            }

            return new T?( that.Min<T>() );
        }
    }
}
