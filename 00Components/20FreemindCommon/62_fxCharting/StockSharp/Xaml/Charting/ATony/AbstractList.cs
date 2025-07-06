using SciChart.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Linq;

namespace StockSharp.Xaml.Charting.ATony
{
    internal abstract class AbstractList< T > : IList< T >, ICollection< T >, IEnumerable< T >, ISciList< T >, IEnumerable, IList, ICollection
    {
        private readonly object object_0 = new object( );

        protected AbstractList( int int_1 )
        {
            if( int_1 < 0 )
            {
                throw new ArgumentOutOfRangeException( "capacity" );
            }
            ItemsArray = new T[ int_1 ];
            Count = 0;
        }

        public T[ ] ItemsArray
        {
            get;
            internal set;
        }

        public int Count
        {
            get;
            internal set;
        }

        public bool HasValues
        {
            get
            {
                return ( uint )Count > 0U;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return object_0;
            }
        }

        public abstract void Add( T item );

        public int Add( object value )
        {
            Add( ( T )value );
            return Count - 1;
        }

        public abstract void Insert( int index, T item );

        public void Insert( int index, object value )
        {
            Insert( index, ( T )value );
        }

        public bool Contains( T item )
        {
            return IndexOf( item ) != -1;
        }

        public bool Contains( object value )
        {
            if( value is T )
            {
                return Contains( ( T )value );
            }
            return false;
        }

        public abstract void Clear( );

        public abstract int IndexOf( T item );

        public int IndexOf( object value )
        {
            if( value is T )
            {
                return IndexOf( ( T )value );
            }
            return -1;
        }

        public bool Remove( T item )
        {
            int index = IndexOf( item );
            if( index == -1 )
            {
                return false;
            }
            RemoveAt( index );
            return true;
        }

        public void Remove( object value )
        {
            if( !( value is T ) )
            {
                return;
            }
            Remove( ( T )value );
        }

        public void RemoveAt( int index )
        {
            if( index < 0 && index >= Count )
            {
                throw new ArgumentOutOfRangeException( nameof( index ) );
            }
            vmethod_2( index, 1 );
        }

        object IList.this[ int index ]
        {
            get
            {
                return vmethod_0( index );
            }
            set
            {
                vmethod_1( index, ( T )value );
            }
        }

        public T this[ int index ]
        {
            get
            {
                return vmethod_0( index );
            }
            set
            {
                vmethod_1( index, value );
            }
        }

        protected abstract T vmethod_0( int int_1 );

        protected abstract void vmethod_1( int int_1, T gparam_1 );

        public abstract void CopyTo( T[ ] array, int arrayIndex );

        public abstract void CopyTo( Array array, int index );

        public abstract T GetMaximum( );

        public abstract T GetMinimum( );

        public abstract void GetMinMax( out T min, out T max );

        public abstract bool ContainsNaN( int startIndex, int count );

        public abstract bool IsSortedAscending( int startIndex, int count );

        public abstract bool IsEvenlySpaced(
          int startIndex,
          int count,
          double epsilon,
          out double spacing );

        public abstract void AddRange( IEnumerable< T > items );

        public abstract void InsertRange( int index, IEnumerable< T > items );

        public void RemoveRange( int index, int count )
        {
            if( index < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( index ) );
            }
            if( count < 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( count ) );
            }
            if( Count - index < count )
            {
                throw new ArgumentOutOfRangeException( nameof( count ) );
            }
            if( count <= 0 )
            {
                return;
            }
            vmethod_2( index, count );
        }

        protected abstract void vmethod_2( int int_1, int int_2 );

        public abstract void SetCount( int setLength );

        public abstract IEnumerator< T > GetEnumerator( );

        public IList AsList( )
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }
    }
}
