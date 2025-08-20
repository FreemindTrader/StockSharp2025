//using SciChart.Data.Numerics.GenericMath;
//using System;
//using System.Collections;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;

//namespace StockSharp.Xaml.Charting.ATony
//{
//    internal sealed class SciList< T > : AbstractList< T >
//    {
//        private static readonly T[ ] gparam_1 = new T[ 0 ];
//        private const int int_1 = 0;
//        private int int_2;

//        public SciList( ) : this( 128 )
//        {
//        }

//        public SciList( int capacity ) : base( capacity )
//        {
//        }

//        public SciList( IEnumerable< T > collection ) : this( )
//        {
//            if( collection == null )
//            {
//                throw new ArgumentNullException( "collection" );
//            }

//            ICollection< T > objs = collection as ICollection< T >;
//            
//            if( objs != null )
//            {
//                int count = objs.Count;
//                ItemsArray = new T[ count ];
//                objs.CopyTo( ItemsArray, 0 );
//                Capacity = count;
//            }
//            else
//            {
//                Capacity = 0;
//                ItemsArray = new T[ 0 ];
//                foreach( T obj in collection )
//                {
//                    Add( obj );
//                }
//            }
//        }

//        internal int Capacity
//        {
//            get
//            {
//                return ItemsArray.Length;
//            }
//            set
//            {
//                if( value < Capacity )
//                {
//                    throw new ArgumentOutOfRangeException( nameof( value ) );
//                }

//                if( value == ItemsArray.Length )
//                {
//                    return;
//                }

//                if( value > 0 )
//                {
//                    T[ ] objArray = new T[ value ];
//                    if( Capacity > 0 )
//                    {
//                        Array.Copy( ( Array )ItemsArray, 0, ( Array )objArray, 0, Capacity );
//                    }
//                    ItemsArray = objArray;
//                }
//                else
//                {
//                    ItemsArray = SciList< T >.gparam_1;
//                }
//            }
//        }

//        public override IEnumerator< T > GetEnumerator( )
//        {
//            return ( IEnumerator< T > )new SciList< T >.Struct66( this );
//        }

//        public override void CopyTo( Array array, int arrayIndex )
//        {
//            if( array != null && array.Rank != 1 )
//            {
//                throw new ArgumentException( "Array rank not supported", "array.Rank" );
//            }
//            try
//            {
//                Array.Copy( ( Array )ItemsArray, 0, array, arrayIndex, Capacity );
//            }
//            catch( ArrayTypeMismatchException ex )
//            {
//                throw new ArgumentException( "Invalid array type" );
//            }
//        }

//        protected override T vmethod_0( int int_3 )
//        {
//            if( int_3 < Capacity )
//            {
//                return ItemsArray[ int_3 ];
//            }
//            return default( T );
//        }

//        protected override void vmethod_1( int int_3, T gparam_2 )
//        {
//            if( int_3 >= Capacity )
//            {
//                throw new ArgumentOutOfRangeException( "index" );
//            }
//            ItemsArray[ int_3 ] = gparam_2;
//            ++int_2;
//        }

//        public override void Add( T item )
//        {
//            if( Capacity == ItemsArray.Length )
//            {
//                EnsureCapacity( Capacity + 1 );
//            }
//            ItemsArray[ Capacity++ ] = item;
//            ++int_2;
//        }

//        public override void Clear( )
//        {
//            if( Capacity > 0 )
//            {
//                Array.Clear( ( Array )ItemsArray, 0, Capacity );
//                Capacity = 0;
//            }
//            ++int_2;
//        }

//        public override void CopyTo( T[ ] array, int arrayIndex )
//        {
//            Array.Copy( ( Array )ItemsArray, 0, ( Array )array, arrayIndex, Capacity );
//        }

//        public override int IndexOf( T item )
//        {
//            return Array.IndexOf< T >( ItemsArray, item, 0, Capacity );
//        }

//        public override void Insert( int index, T item )
//        {
//            if( index > Capacity )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }
//            if( Capacity == ItemsArray.Length )
//            {
//                EnsureCapacity( Capacity + 1 );
//            }
//            if( index < Capacity )
//            {
//                Array.Copy( ( Array )ItemsArray, index, ( Array )ItemsArray, index + 1, Capacity - index );
//            }
//            ItemsArray[ index ] = item;
//            ++Capacity;
//            ++int_2;
//        }

//        public override T GetMaximum( )
//        {
//            return ArrayOperations.Maximum< T >( ItemsArray, 0, Capacity );
//        }

//        public override T GetMinimum( )
//        {
//            return ArrayOperations.Minimum< T >( ItemsArray, 0, Capacity );
//        }

//        public override void GetMinMax( out T min, out T max )
//        {
//            ArrayOperations.MinMax< T >( ItemsArray, 0, Capacity, out min, out max );
//        }

//        public override bool ContainsNaN( int startIndex, int count )
//        {
//            return ArrayOperations.ContainsNaN< T >( ItemsArray, startIndex, count );
//        }

//        public override bool IsSortedAscending( int startIndex, int count )
//        {
//            return ArrayOperations.IsSortedAscending< T >( ItemsArray, startIndex, count );
//        }

//        public override bool IsEvenlySpaced(
//          int startIndex,
//          int count,
//          double epsilon,
//          out double spacing )
//        {
//            return ArrayOperations.IsEvenlySpaced< T >( ItemsArray, startIndex, count, epsilon, out spacing );
//        }

//        public override void AddRange( IEnumerable< T > collection )
//        {
//            InsertRange( Capacity, collection );
//        }

//        public bool EnsureMinSize( int minSize )
//        {
//            if ( Count >= minSize )
//            {
//                return false;
//            }

//            EnsureCapacity( minSize );
//            Count = minSize;
//            return true;
//        }

//        private void EnsureCapacity( int min )
//        {
//            if( ItemsArray.Length >= min )
//            {
//                return;
//            }
//            int num = ItemsArray.Length == 0 ? 4 : ItemsArray.Length * 2;
//            if( num < min )
//            {
//                num = min;
//            }
//            Capacity = num;
//        }

//        public override void InsertRange( int index, IEnumerable< T > collection )
//        {
//            if( collection == null )
//            {
//                return;
//            }
//            if( index > Capacity )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }
//            T[ ] objArray1 = collection as T[ ];
//            if( objArray1 != null )
//            {
//                int length = objArray1.Length;
//                EnsureCapacity( Capacity + length );
//                Array.Copy( ( Array )ItemsArray, index, ( Array )ItemsArray, index + length, Capacity - index );
//                Array.Copy( ( Array )objArray1, 0, ( Array )ItemsArray, index, length );
//                Capacity += length;
//                ++int_2;
//            }
//            else
//            {
//                IList< T > list = collection as IList< T >;
//                if( list != null )
//                {
//                    int count = list.Count;
//                    T[ ] uncheckedList = list.ToUncheckedList< T >( );
//                    EnsureCapacity( Capacity + count );
//                    Array.Copy( ( Array )ItemsArray, index, ( Array )ItemsArray, index + count, Capacity - index );
//                    T[ ] itemsArray = ItemsArray;
//                    int destinationIndex = index;
//                    int length = count;
//                    Array.Copy( ( Array )uncheckedList, 0, ( Array )itemsArray, destinationIndex, length );
//                    Capacity += count;
//                    ++int_2;
//                }
//                else
//                {
//                    using( IEnumerator< T > enumerator = collection.GetEnumerator( ) )
//                    {
//                        int length = Capacity - index;
//                        T[ ] objArray2 = new T[ length ];
//                        Array.Copy( ( Array )ItemsArray, index, ( Array )objArray2, 0, length );
//                        while( enumerator.MoveNext( ) )
//                        {
//                            EnsureCapacity( Capacity + 1 );
//                            ItemsArray[ index ] = enumerator.Current;
//                            ++index;
//                            ++Capacity;
//                        }
//                        Array.Copy( ( Array )objArray2, 0, ( Array )ItemsArray, index, length );
//                        ++int_2;
//                    }
//                }
//            }
//        }

//        protected override void vmethod_2( int int_3, int int_4 )
//        {
//            Capacity -= int_4;
//            if( int_3 < Capacity )
//            {
//                Array.Copy( ( Array )ItemsArray, int_3 + int_4, ( Array )ItemsArray, int_3, Capacity - int_3 );
//            }
//            Array.Clear( ( Array )ItemsArray, Capacity, int_4 );
//            ++int_2;
//        }

//        public T[ ] method_1( )
//        {
//            T[ ] objArray = new T[ Capacity ];
//            Array.Copy( ( Array )ItemsArray, 0, ( Array )objArray, 0, Capacity );
//            return objArray;
//        }

//        public override void SetCount( int setLength )
//        {
//            EnsureCapacity( setLength );
//            Capacity = setLength;
//        }

//        private struct Struct66 : IEnumerator< T >, IDisposable, IEnumerator
//        {
//            private readonly SciList< T > class744_0;
//            private readonly int int_0;
//            private T gparam_0;
//            private int int_1;

//            internal Struct66( SciList< T > class744_1 )
//            {
//                class744_0 = class744_1;
//                int_1 = 0;
//                int_0 = class744_1.int_2;
//                gparam_0 = default( T );
//            }

//            public T Current
//            {
//                get
//                {
//                    return gparam_0;
//                }
//            }

//            object IEnumerator.Current
//            {
//                get
//                {
//                    if( int_1 == 0 || int_1 == class744_0.Capacity + 1 )
//                    {
//                        throw new InvalidOperationException( "Enumerator Index out of range" );
//                    }
//                    return ( object )Current;
//                }
//            }

//            public void Dispose( )
//            {
//            }

//            public bool MoveNext( )
//            {
//                SciList< T > class7440 = class744_0;
//                if( int_0 != class7440.int_2 || int_1 >= class7440.Capacity )
//                {
//                    return method_0( );
//                }
//                gparam_0 = class7440.ItemsArray[ int_1 ];
//                ++int_1;
//                return true;
//            }

//            void IEnumerator.Reset( )
//            {
//                if( int_0 != class744_0.int_2 )
//                {
//                    throw new InvalidOperationException( "Enumerator version is invalid" );
//                }
//                int_1 = 0;
//                gparam_0 = default( T );
//            }

//            private bool method_0( )
//            {
//                if( int_0 != class744_0.int_2 )
//                {
//                    throw new InvalidOperationException( "Enumerator version is invalid" );
//                }
//                int_1 = class744_0.Capacity + 1;
//                gparam_0 = default( T );
//                return false;
//            }
//        }
//    }
//}
