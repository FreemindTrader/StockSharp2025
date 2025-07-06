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
//                this.ItemsArray = new T[ count ];
//                objs.CopyTo( this.ItemsArray, 0 );
//                this.Capacity = count;
//            }
//            else
//            {
//                this.Capacity = 0;
//                this.ItemsArray = new T[ 0 ];
//                foreach( T obj in collection )
//                {
//                    this.Add( obj );
//                }
//            }
//        }

//        internal int Capacity
//        {
//            get
//            {
//                return this.ItemsArray.Length;
//            }
//            set
//            {
//                if( value < this.Capacity )
//                {
//                    throw new ArgumentOutOfRangeException( nameof( value ) );
//                }

//                if( value == this.ItemsArray.Length )
//                {
//                    return;
//                }

//                if( value > 0 )
//                {
//                    T[ ] objArray = new T[ value ];
//                    if( this.Capacity > 0 )
//                    {
//                        Array.Copy( ( Array )this.ItemsArray, 0, ( Array )objArray, 0, this.Capacity );
//                    }
//                    this.ItemsArray = objArray;
//                }
//                else
//                {
//                    this.ItemsArray = SciList< T >.gparam_1;
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
//                Array.Copy( ( Array )this.ItemsArray, 0, array, arrayIndex, this.Capacity );
//            }
//            catch( ArrayTypeMismatchException ex )
//            {
//                throw new ArgumentException( "Invalid array type" );
//            }
//        }

//        protected override T vmethod_0( int int_3 )
//        {
//            if( int_3 < this.Capacity )
//            {
//                return this.ItemsArray[ int_3 ];
//            }
//            return default( T );
//        }

//        protected override void vmethod_1( int int_3, T gparam_2 )
//        {
//            if( int_3 >= this.Capacity )
//            {
//                throw new ArgumentOutOfRangeException( "index" );
//            }
//            this.ItemsArray[ int_3 ] = gparam_2;
//            ++this.int_2;
//        }

//        public override void Add( T item )
//        {
//            if( this.Capacity == this.ItemsArray.Length )
//            {
//                this.EnsureCapacity( this.Capacity + 1 );
//            }
//            this.ItemsArray[ this.Capacity++ ] = item;
//            ++this.int_2;
//        }

//        public override void Clear( )
//        {
//            if( this.Capacity > 0 )
//            {
//                Array.Clear( ( Array )this.ItemsArray, 0, this.Capacity );
//                this.Capacity = 0;
//            }
//            ++this.int_2;
//        }

//        public override void CopyTo( T[ ] array, int arrayIndex )
//        {
//            Array.Copy( ( Array )this.ItemsArray, 0, ( Array )array, arrayIndex, this.Capacity );
//        }

//        public override int IndexOf( T item )
//        {
//            return Array.IndexOf< T >( this.ItemsArray, item, 0, this.Capacity );
//        }

//        public override void Insert( int index, T item )
//        {
//            if( index > this.Capacity )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }
//            if( this.Capacity == this.ItemsArray.Length )
//            {
//                this.EnsureCapacity( this.Capacity + 1 );
//            }
//            if( index < this.Capacity )
//            {
//                Array.Copy( ( Array )this.ItemsArray, index, ( Array )this.ItemsArray, index + 1, this.Capacity - index );
//            }
//            this.ItemsArray[ index ] = item;
//            ++this.Capacity;
//            ++this.int_2;
//        }

//        public override T GetMaximum( )
//        {
//            return ArrayOperations.Maximum< T >( this.ItemsArray, 0, this.Capacity );
//        }

//        public override T GetMinimum( )
//        {
//            return ArrayOperations.Minimum< T >( this.ItemsArray, 0, this.Capacity );
//        }

//        public override void GetMinMax( out T min, out T max )
//        {
//            ArrayOperations.MinMax< T >( this.ItemsArray, 0, this.Capacity, out min, out max );
//        }

//        public override bool ContainsNaN( int startIndex, int count )
//        {
//            return ArrayOperations.ContainsNaN< T >( this.ItemsArray, startIndex, count );
//        }

//        public override bool IsSortedAscending( int startIndex, int count )
//        {
//            return ArrayOperations.IsSortedAscending< T >( this.ItemsArray, startIndex, count );
//        }

//        public override bool IsEvenlySpaced(
//          int startIndex,
//          int count,
//          double epsilon,
//          out double spacing )
//        {
//            return ArrayOperations.IsEvenlySpaced< T >( this.ItemsArray, startIndex, count, epsilon, out spacing );
//        }

//        public override void AddRange( IEnumerable< T > collection )
//        {
//            this.InsertRange( this.Capacity, collection );
//        }

//        public bool EnsureMinSize( int minSize )
//        {
//            if ( this.Count >= minSize )
//            {
//                return false;
//            }

//            this.EnsureCapacity( minSize );
//            this.Count = minSize;
//            return true;
//        }

//        private void EnsureCapacity( int min )
//        {
//            if( this.ItemsArray.Length >= min )
//            {
//                return;
//            }
//            int num = this.ItemsArray.Length == 0 ? 4 : this.ItemsArray.Length * 2;
//            if( num < min )
//            {
//                num = min;
//            }
//            this.Capacity = num;
//        }

//        public override void InsertRange( int index, IEnumerable< T > collection )
//        {
//            if( collection == null )
//            {
//                return;
//            }
//            if( index > this.Capacity )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }
//            T[ ] objArray1 = collection as T[ ];
//            if( objArray1 != null )
//            {
//                int length = objArray1.Length;
//                this.EnsureCapacity( this.Capacity + length );
//                Array.Copy( ( Array )this.ItemsArray, index, ( Array )this.ItemsArray, index + length, this.Capacity - index );
//                Array.Copy( ( Array )objArray1, 0, ( Array )this.ItemsArray, index, length );
//                this.Capacity += length;
//                ++this.int_2;
//            }
//            else
//            {
//                IList< T > list = collection as IList< T >;
//                if( list != null )
//                {
//                    int count = list.Count;
//                    T[ ] uncheckedList = list.ToUncheckedList< T >( );
//                    this.EnsureCapacity( this.Capacity + count );
//                    Array.Copy( ( Array )this.ItemsArray, index, ( Array )this.ItemsArray, index + count, this.Capacity - index );
//                    T[ ] itemsArray = this.ItemsArray;
//                    int destinationIndex = index;
//                    int length = count;
//                    Array.Copy( ( Array )uncheckedList, 0, ( Array )itemsArray, destinationIndex, length );
//                    this.Capacity += count;
//                    ++this.int_2;
//                }
//                else
//                {
//                    using( IEnumerator< T > enumerator = collection.GetEnumerator( ) )
//                    {
//                        int length = this.Capacity - index;
//                        T[ ] objArray2 = new T[ length ];
//                        Array.Copy( ( Array )this.ItemsArray, index, ( Array )objArray2, 0, length );
//                        while( enumerator.MoveNext( ) )
//                        {
//                            this.EnsureCapacity( this.Capacity + 1 );
//                            this.ItemsArray[ index ] = enumerator.Current;
//                            ++index;
//                            ++this.Capacity;
//                        }
//                        Array.Copy( ( Array )objArray2, 0, ( Array )this.ItemsArray, index, length );
//                        ++this.int_2;
//                    }
//                }
//            }
//        }

//        protected override void vmethod_2( int int_3, int int_4 )
//        {
//            this.Capacity -= int_4;
//            if( int_3 < this.Capacity )
//            {
//                Array.Copy( ( Array )this.ItemsArray, int_3 + int_4, ( Array )this.ItemsArray, int_3, this.Capacity - int_3 );
//            }
//            Array.Clear( ( Array )this.ItemsArray, this.Capacity, int_4 );
//            ++this.int_2;
//        }

//        public T[ ] method_1( )
//        {
//            T[ ] objArray = new T[ this.Capacity ];
//            Array.Copy( ( Array )this.ItemsArray, 0, ( Array )objArray, 0, this.Capacity );
//            return objArray;
//        }

//        public override void SetCount( int setLength )
//        {
//            this.EnsureCapacity( setLength );
//            this.Capacity = setLength;
//        }

//        private struct Struct66 : IEnumerator< T >, IDisposable, IEnumerator
//        {
//            private readonly SciList< T > class744_0;
//            private readonly int int_0;
//            private T gparam_0;
//            private int int_1;

//            internal Struct66( SciList< T > class744_1 )
//            {
//                this.class744_0 = class744_1;
//                this.int_1 = 0;
//                this.int_0 = class744_1.int_2;
//                this.gparam_0 = default( T );
//            }

//            public T Current
//            {
//                get
//                {
//                    return this.gparam_0;
//                }
//            }

//            object IEnumerator.Current
//            {
//                get
//                {
//                    if( this.int_1 == 0 || this.int_1 == this.class744_0.Capacity + 1 )
//                    {
//                        throw new InvalidOperationException( "Enumerator Index out of range" );
//                    }
//                    return ( object )this.Current;
//                }
//            }

//            public void Dispose( )
//            {
//            }

//            public bool MoveNext( )
//            {
//                SciList< T > class7440 = this.class744_0;
//                if( this.int_0 != class7440.int_2 || this.int_1 >= class7440.Capacity )
//                {
//                    return this.method_0( );
//                }
//                this.gparam_0 = class7440.ItemsArray[ this.int_1 ];
//                ++this.int_1;
//                return true;
//            }

//            void IEnumerator.Reset( )
//            {
//                if( this.int_0 != this.class744_0.int_2 )
//                {
//                    throw new InvalidOperationException( "Enumerator version is invalid" );
//                }
//                this.int_1 = 0;
//                this.gparam_0 = default( T );
//            }

//            private bool method_0( )
//            {
//                if( this.int_0 != this.class744_0.int_2 )
//                {
//                    throw new InvalidOperationException( "Enumerator version is invalid" );
//                }
//                this.int_1 = this.class744_0.Capacity + 1;
//                this.gparam_0 = default( T );
//                return false;
//            }
//        }
//    }
//}
