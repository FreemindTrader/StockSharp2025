//using SciChart.Data.Numerics.GenericMath;
//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;

//namespace StockSharp.Xaml.Charting.ATony
//{
//    internal class FifoBuffer< T > : AbstractList< T >
//    {
//        private const int int_1 = 0;
//        private readonly int int_2;
//        private readonly T[ ] gparam_1;

//        public FifoBuffer( int int_4 ) : base( int_4 )
//        {
//            this.StartIndex = 0;
//            this.int_2 = int_4;
//            this.gparam_1 = new T[ int_4 ];
//        }

//        internal int Int32_0
//        {
//            get
//            {
//                return this.int_2;
//            }
//        }

//        internal int StartIndex
//        {
//            get;
//            set;
//        }

//        public override void Add( T item )
//        {
//            this.ItemsArray[ this.method_2( ) ] = item;
//            this.Count = Math.Min( this.Count + 1, this.int_2 );
//        }

//        [Obsolete( "NOTE SC-3551: Legacy code. To maintain compatibility with SciChart v4.0 FIFO before unwrap implemented. This will be removed in future releases of SciChart as the FIFO Unwrap is done in-place" )]
//        public ReadOnlyFifo< T > UnWrap( int int_4, int int_5 )
//        {
//            int num1 = int_4 > this.Count ? this.Count : int_4;
//            int num2 = Math.Min( this.Count - num1, int_5 );
//            if( this.Count != this.Int32_0 )
//            {
//                return new ReadOnlyFifo< T >( this.ItemsArray, num1, num2 );
//            }
//            this.method_6( num1, this.gparam_1, 0, num2 );
//            return new ReadOnlyFifo< T >( this.gparam_1, 0, num2 );
//        }

//        public override void CopyTo( Array array, int index )
//        {
//            throw new NotImplementedException( );
//        }

//        public override T GetMaximum( )
//        {
//            int count = this.Count;
//            int startIndex = this.method_4( 0 );
//            int num = this.method_4( count - 1 );
//            if( startIndex < num )
//            {
//                return ArrayOperations.Maximum< T >( this.ItemsArray, startIndex, count );
//            }
//            return ArrayOperations.Maximum< T >( new T[ 2 ] { ArrayOperations.Maximum< T >( this.ItemsArray, startIndex, this.Int32_0 ), ArrayOperations.Maximum< T >( this.ItemsArray, 0, num + 1 ) } );
//        }

//        public override T GetMinimum( )
//        {
//            int count = this.Count;
//            int startIndex = this.method_4( 0 );
//            int num = this.method_4( count - 1 );
//            if( startIndex < num )
//            {
//                return ArrayOperations.Minimum< T >( this.ItemsArray, startIndex, count );
//            }
//            return ArrayOperations.Minimum< T >( new T[ 2 ] { ArrayOperations.Minimum< T >( this.ItemsArray, startIndex, this.Int32_0 ), ArrayOperations.Minimum< T >( this.ItemsArray, 0, num + 1 ) } );
//        }

//        public override void GetMinMax( out T min, out T max )
//        {
//            int count = this.Count;
//            int startIndex = this.method_4( 0 );
//            int num = this.method_4( count - 1 );
//            if( startIndex < num )
//            {
//                ArrayOperations.MinMax< T >( this.ItemsArray, startIndex, count, out min, out max );
//            }
//            else
//            {
//                T[ ] array = new T[ 4 ];
//                ArrayOperations.MinMax< T >( this.ItemsArray, startIndex, this.Int32_0, out array[ 0 ], out array[ 1 ] );
//                ArrayOperations.MinMax< T >( this.ItemsArray, 0, num + 1, out array[ 2 ], out array[ 3 ] );
//                ArrayOperations.MinMax< T >( array, out min, out max );
//            }
//        }

//        public override bool ContainsNaN( int startIndex, int count )
//        {
//            int startIndex1 = this.method_4( startIndex );
//            int num = this.method_4( startIndex + count - 1 );
//            if( startIndex1 < num )
//            {
//                return ArrayOperations.ContainsNaN< T >( this.ItemsArray, startIndex1, count );
//            }
//            if( !ArrayOperations.ContainsNaN< T >( this.ItemsArray, startIndex1, this.Int32_0 - startIndex1 ) )
//            {
//                return ArrayOperations.ContainsNaN< T >( this.ItemsArray, 0, num + 1 );
//            }
//            return true;
//        }

//        public override bool IsSortedAscending( int startIndex, int count )
//        {
//            int startIndex1 = this.method_4( startIndex );
//            int num = this.method_4( startIndex + count - 1 );
//            if( startIndex1 < num )
//            {
//                return ArrayOperations.IsSortedAscending< T >( this.ItemsArray, startIndex1, count );
//            }
//            if( ArrayOperations.IsSortedAscending< T >( new T[ 2 ] { this.ItemsArray[ this.Int32_0 - 1 ], this.ItemsArray[ 0 ] } ) && ArrayOperations.IsSortedAscending< T >( this.ItemsArray, startIndex1, this.Int32_0 - startIndex1 ) )
//            {
//                return ArrayOperations.IsSortedAscending< T >( this.ItemsArray, 0, num + 1 );
//            }
//            return false;
//        }

//        public override bool IsEvenlySpaced(
//          int startIndex,
//          int count,
//          double epsilon,
//          out double spacing )
//        {
//            int startIndex1 = this.method_4( startIndex );
//            int num1 = this.method_4( startIndex + count - 1 );
//            if( startIndex1 < num1 )
//            {
//                return ArrayOperations.IsEvenlySpaced< T >( this.ItemsArray, startIndex1, count, epsilon, out spacing );
//            }
//            double spacing1 = 0.0;
//            double spacing2 = 0.0;
//            double spacing3;
//            int num2;
//            if( ArrayOperations.IsEvenlySpaced< T >( this.ItemsArray, startIndex1, this.Int32_0 - startIndex1, epsilon, out spacing3 ) && ArrayOperations.IsEvenlySpaced< T >( this.ItemsArray, 0, num1 + 1, epsilon, out spacing1 ) )
//            {
//                if( ArrayOperations.IsEvenlySpaced< T >( new T[ 2 ] { this.ItemsArray[ this.Int32_0 - 1 ], this.ItemsArray[ 0 ] }, epsilon, out spacing2 ) && Math.Abs( spacing3 - spacing1 ) < epsilon )
//                {
//                    num2 = Math.Abs( spacing1 - spacing2 ) < epsilon ? 1 : 0;
//                    goto label_6;
//                }
//            }
//            num2 = 0;
//            label_6:
//bool flag = num2 != 0;
//            spacing = flag ? spacing3 : Math.Max( Math.Max( spacing3, spacing1 ), spacing2 );
//            return flag;
//        }

//        public override void AddRange( IEnumerable< T > items )
//        {
//            int int_4 = this.method_2( );
//            T[ ] itemsArray = this.ItemsArray;
//            int int_6 = this.int_2 - int_4;
//            Array array_0 = items as Array;
//            if( array_0 != null )
//            {
//                this.method_1( int_4, array_0, array_0.Length, int_6, itemsArray );
//            }
//            else
//            {
//                IList< T > list = items as IList< T >;
//                if( list != null )
//                {
//                    T[ ] uncheckedList = list.ToUncheckedList< T >( );
//                    this.method_1( int_4, ( Array )uncheckedList, list.Count, int_6, itemsArray );
//                }
//                else
//                {
//                    T[ ] array = items.ToArray< T >( );
//                    this.method_1( int_4, ( Array )array, array.Length, int_6, itemsArray );
//                }
//            }
//        }

//        public override void InsertRange( int index, IEnumerable< T > items )
//        {
//            throw new NotSupportedException( "Insert is not a supported operation on a Fifo Buffer" );
//        }

//        protected override void vmethod_2( int int_4, int int_5 )
//        {
//            int destinationIndex = this.method_4( int_4 );
//            int sourceIndex = this.method_4( int_4 + int_5 );
//            int num = this.Count - int_5;
//            if( sourceIndex <= destinationIndex )
//            {
//                this.StartIndex -= sourceIndex;
//                Array.Copy( ( Array )this.ItemsArray, sourceIndex, ( Array )this.ItemsArray, 0, num );
//            }
//            else
//            {
//                Array.Copy( ( Array )this.ItemsArray, sourceIndex, ( Array )this.ItemsArray, destinationIndex, this.Count - sourceIndex );
//            }
//            Array.Clear( ( Array )this.ItemsArray, num, int_5 );
//            this.Count = num;
//        }

//        private void method_1( int int_4, Array array_0, int int_5, int int_6, T[ ] gparam_2 )
//        {
//            if( int_5 > this.int_2 )
//            {
//                Array.Copy( array_0, int_5 - this.int_2, ( Array )this.ItemsArray, 0, this.int_2 );
//                this.StartIndex = 0;
//                this.Count = this.int_2;
//            }
//            else if( int_5 < int_6 )
//            {
//                Array.Copy( array_0, 0, ( Array )gparam_2, int_4, int_5 );
//                this.Count = Math.Min( this.Count + int_5, this.int_2 );
//                this.StartIndex = int_4 + int_5;
//            }
//            else
//            {
//                int length1 = int_6;
//                int length2 = int_5 - int_6;
//                Array.Copy( array_0, 0, ( Array )this.ItemsArray, int_4, length1 );
//                Array.Copy( array_0, int_6, ( Array )this.ItemsArray, 0, length2 );
//                this.Count = Math.Min( this.Count + int_5, this.int_2 );
//                this.StartIndex = length2;
//            }
//        }

//        public override int IndexOf( T item )
//        {
//            int int_4 = Array.IndexOf< T >( this.ItemsArray, item, 0, this.int_2 );
//            if( int_4 == -1 )
//            {
//                return -1;
//            }
//            return this.method_3( int_4 );
//        }

//        public override void Insert( int index, T item )
//        {
//            throw new NotSupportedException( "Insert is not a supported operation on a Fifo Buffer" );
//        }

//        private int method_2( )
//        {
//            if( this.StartIndex <= 0 && this.Count != this.int_2 )
//            {
//                return this.Count;
//            }
//            int startIndex = this.StartIndex;
//            this.StartIndex = ( this.StartIndex + 1 ) % this.int_2;
//            return startIndex;
//        }

//        internal int method_3( int int_4 )
//        {
//            return ( int_4 - this.StartIndex + this.Count ) % this.Count;
//        }

//        internal int method_4( int int_4 )
//        {
//            return ( this.StartIndex + int_4 ) % this.Count;
//        }

//        private void method_5( int int_4 )
//        {
//            if( int_4 < 0 || int_4 >= this.Count )
//            {
//                throw new IndexOutOfRangeException( );
//            }
//        }

//        public override void Clear( )
//        {
//            Array.Clear( ( Array )this.ItemsArray, 0, this.int_2 );
//            this.StartIndex = 0;
//            this.Count = 0;
//        }

//        public override void CopyTo( T[ ] array, int arrayIndex )
//        {
//            int startIndex = this.StartIndex;
//            int length1 = array.Length - startIndex;
//            Array.Copy( ( Array )this.ItemsArray, startIndex, ( Array )array, arrayIndex, length1 );
//            int length2 = array.Length - length1;
//            if( length2 <= 0 )
//            {
//                return;
//            }
//            Array.Copy( ( Array )this.ItemsArray, 0, ( Array )array, length1 + arrayIndex, length2 );
//        }

//        internal void method_6( int int_4, T[ ] gparam_2, int int_5, int int_6 )
//        {
//            int sourceIndex = this.StartIndex + int_4;
//            if( sourceIndex < gparam_2.Length )
//            {
//                int length1 = Math.Min( gparam_2.Length - sourceIndex, int_6 );
//                Array.Copy( ( Array )this.ItemsArray, sourceIndex, ( Array )gparam_2, int_5, length1 );
//                int length2 = int_6 - length1;
//                if( length2 <= 0 )
//                {
//                    return;
//                }
//                Array.Copy( ( Array )this.ItemsArray, 0, ( Array )gparam_2, int_5 + length1, length2 );
//            }
//            else
//            {
//                Array.Copy( ( Array )this.ItemsArray, sourceIndex - gparam_2.Length, ( Array )gparam_2, int_5, int_6 );
//            }
//        }

//        protected override T vmethod_0( int int_4 )
//        {
//            this.method_5( int_4 );
//            return this.ItemsArray[ this.method_4( int_4 ) ];
//        }

//        protected override void vmethod_1( int int_4, T gparam_2 )
//        {
//            this.method_5( int_4 );
//            this.ItemsArray[ this.method_4( int_4 ) ] = gparam_2;
//        }

//        public override IEnumerator< T > GetEnumerator( )
//        {
//            FifoBuffer< T > class743 = this;
//            // ISSUE: explicit non-virtual call
//            if( ( class743.Count ) != 0 )
//            {
//                int index;
//                // ISSUE: explicit non-virtual call
//                for( index = class743.StartIndex; index < ( class743.Count ); ++index )
//                {
//                    // ISSUE: explicit non-virtual call
//                    yield return ( class743.ItemsArray )[ index ];
//                }
//                for( index = 0; index < class743.StartIndex; ++index )
//                {
//                    // ISSUE: explicit non-virtual call
//                    yield return ( class743.ItemsArray )[ index ];
//                }
//            }
//        }

//        public override void SetCount( int setLength )
//        {
//            throw new InvalidOperationException( "SetCount is not valid on Circular-Buffer list types" );
//        }
//    }
//}
