//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Reflection;

//namespace StockSharp.Xaml.Charting.ATony
//{
//    public static class ListExtensions
//    {
//        public static T[ ] ToUncheckedList< T >( this IList< T > list )
//        {
//            T[ ] objArray = list as T[ ];
//            if( objArray != null )
//            {
//                return objArray;
//            }
//            SciList< T > class744 = list as SciList< T >;
//            if( class744 != null )
//            {
//                return class744.ItemsArray;
//            }
//            if( list is PooledList< T > )
//            {
//                return ( T[ ] )typeof( PooledList< T > ).GetField( "_items", BindingFlags.Instance | BindingFlags.NonPublic ).GetValue( ( object )list );
//            }
//            FifoBuffer< T > fifoBuffer = list as FifoBuffer< T >;

//            if( fifoBuffer != null )
//            {
//                return fifoBuffer.UnWrap( 0, fifoBuffer.Count ).Prop_0;
//            }

//            return list.ToArray< T >( );
//        }

//        internal static ReadOnlyFifo< T > smethod_0< T >( this IList< T > ilist_0, bool bool_0 )
//        {
//            ReadOnlyFifo< T > class746 = ilist_0 as ReadOnlyFifo< T >;
//            if( class746 != null )
//            {
//                return class746;
//            }
//            SciList< T > class744 = ilist_0 as SciList< T >;
//            if( class744 != null )
//            {
//                return new ReadOnlyFifo< T >( class744.ItemsArray, class744.Capacity );
//            }
//            T[ ] gparam_1 = ilist_0 as T[ ];
//            if( gparam_1 != null )
//            {
//                return new ReadOnlyFifo< T >( gparam_1 );
//            }
//            if( ilist_0 is PooledList< T > )
//            {
//                return new ReadOnlyFifo< T >( ( T[ ] )typeof( PooledList< T > ).GetField( "_items", BindingFlags.Instance | BindingFlags.NonPublic ).GetValue( ( object )ilist_0 ) );
//            }
//            if( !bool_0 )
//            {
//                return ( ReadOnlyFifo< T > )null;
//            }
//            return new ReadOnlyFifo< T >( ilist_0.ToArray< T >( ) );
//        }
//    }
//}