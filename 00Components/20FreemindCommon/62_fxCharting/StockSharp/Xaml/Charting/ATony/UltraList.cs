//using SciChart.Data.Numerics.GenericMath;
//using System;
//using System.Collections;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Security;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace fx.Charting.ATony
//{
//    public interface IUltraList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
//    {
//        T GetMaximum( );

//        T GetMinimum( );

//        void AddRange( IEnumerable<T> items );

//        void InsertRange( int index, IEnumerable<T> items );

//        void RemoveRange( int index, int count );

//        T[ ] ItemsArray { get; }

//        void SetCount( int setLength );
//    }

//    public interface IUltraReadOnlyList<T> : IUltraList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
//    {
//    }

//    internal class UltraList<T> : IUltraList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
//    {
//        private static readonly T[ ] _emptyArray = new T[ 0 ];
//        private const int _defaultCapacity = 128;
//        private T[ ] _items;
//        private int _size;
//        private object _syncRoot;
//        private int _version;

//        public UltraList( )
//          : this( 128 )
//        {
//        }

//        public UltraList( int capacity )
//        {
//            if ( capacity < 0 )
//            {
//                throw new ArgumentOutOfRangeException( nameof( capacity ) );
//            }

//            this._items = new T[ capacity ];
//        }

//        public static UltraList<T> ForArray( T[ ] arr )
//        {
//            return new UltraList<T>( ) { _items = arr, _size = arr.Length };
//        }

//        public UltraList( IEnumerable<T> collection )
//        {
//            if ( collection == null )
//            {
//                throw new ArgumentNullException( nameof( collection ) );
//            }

//            ICollection<T> objs = collection as ICollection<T>;
//            if ( objs != null )
//            {
//                int count = objs.Count;
//                this._items = new T[ count ];
//                objs.CopyTo( this._items, 0 );
//                this._size = count;
//            }
//            else
//            {
//                this._size = 0;
//                this._items = new T[ 128 ];
//                foreach ( T obj in collection )
//                {
//                    this.Add( obj );
//                }
//            }
//        }

//        public T[ ] ItemsArray
//        {
//            get
//            {
//                return this._items;
//            }
//        }

//        internal int Capacity
//        {
//            get
//            {
//                return this._items.Length;
//            }
//            set
//            {
//                if ( value < this._size )
//                {
//                    throw new ArgumentOutOfRangeException( nameof( value ) );
//                }

//                if ( value == this._items.Length )
//                {
//                    return;
//                }

//                if ( value > 0 )
//                {
//                    T[ ] objArray = new T[ value ];
//                    if ( this._size > 0 )
//                    {
//                        Array.Copy( ( Array )this._items, 0, ( Array )objArray, 0, this._size );
//                    }

//                    this._items = objArray;
//                }
//                else
//                {
//                    this._items = UltraList<T>._emptyArray;
//                }
//            }
//        }

//        bool IList.IsFixedSize
//        {
//            get
//            {
//                return false;
//            }
//        }

//        bool IList.IsReadOnly
//        {
//            get
//            {
//                return false;
//            }
//        }

//        bool ICollection.IsSynchronized
//        {
//            get
//            {
//                return false;
//            }
//        }

//        object ICollection.SyncRoot
//        {
//            get
//            {
//                if ( this._syncRoot == null )
//                {
//                    Interlocked.CompareExchange<object>( ref this._syncRoot, new object( ), ( object )null );
//                }

//                return this._syncRoot;
//            }
//        }

//        object IList.this[ int index ]
//        {
//            get
//            {
//                return ( object )this[ index ];
//            }
//            set
//            {
//                try
//                {
//                    this[ index ] = ( T )value;
//                }
//                catch ( InvalidCastException ex )
//                {
//                    throw new ArgumentException( string.Format( "Value type {0} was of the wrong type", ( object )value.GetType( ).Name ) );
//                }
//            }
//        }

//        int IList.Add( object item )
//        {
//            if ( item == null )
//            {
//                throw new ArgumentNullException( nameof( item ) );
//            }

//            try
//            {
//                this.Add( ( T )item );
//            }
//            catch ( InvalidCastException ex )
//            {
//                throw new ArgumentException( string.Format( "Value type {0} was of the wrong type", ( object )item.GetType( ).Name ) );
//            }
//            return this.Count - 1;
//        }

//        [SecuritySafeCritical]
//        bool IList.Contains( object item )
//        {
//            if ( UltraList<T>.IsCompatibleObject( item ) )
//            {
//                return this.Contains( ( T )item );
//            }

//            return false;
//        }

//        void ICollection.CopyTo( Array array, int arrayIndex )
//        {
//            if ( array != null && array.Rank != 1 )
//            {
//                throw new ArgumentException( "Array rank not supported", "array.Rank" );
//            }

//            try
//            {
//                Array.Copy( ( Array )this._items, 0, array, arrayIndex, this._size );
//            }
//            catch ( ArrayTypeMismatchException ex )
//            {
//                throw new ArgumentException( "Invalid array type" );
//            }
//        }

//        int IList.IndexOf( object item )
//        {
//            if ( UltraList<T>.IsCompatibleObject( item ) )
//            {
//                return this.IndexOf( ( T )item );
//            }

//            return -1;
//        }

//        void IList.Insert( int index, object item )
//        {
//            try
//            {
//                this.Insert( index, ( T )item );
//            }
//            catch ( InvalidCastException ex )
//            {
//                throw new ArgumentException( string.Format( "Value type {0} was of the wrong type", ( object )item.GetType( ).Name ) );
//            }
//        }

//        [SecuritySafeCritical]
//        void IList.Remove( object item )
//        {
//            if ( !UltraList<T>.IsCompatibleObject( item ) )
//            {
//                return;
//            }

//            this.Remove( ( T )item );
//        }

//        public int Count
//        {
//            get
//            {
//                return this._size;
//            }
//            internal set
//            {
//                this._size = value;
//            }
//        }

//        bool ICollection<T>.IsReadOnly
//        {
//            get
//            {
//                return false;
//            }
//        }

//        public T this[ int index ]
//        {
//            get
//            {
//                if ( index >= this._size )
//                {
//                    return default( T );
//                }

//                return this._items[ index ];
//            }
//            set
//            {
//                if ( index >= this._size )
//                {
//                    throw new ArgumentOutOfRangeException( nameof( index ) );
//                }

//                this._items[ index ] = value;
//                ++this._version;
//            }
//        }

//        public void Add( T item )
//        {
//            if ( this._size == this._items.Length )
//            {
//                this.EnsureCapacity( this._size + 1 );
//            }

//            this._items[ this._size++ ] = item;
//            ++this._version;
//        }

//        public void Clear( )
//        {
//            if ( this._size > 0 )
//            {
//                Array.Clear( ( Array )this._items, 0, this._size );
//                this._size = 0;
//            }
//            ++this._version;
//        }

//        public bool Contains( T item )
//        {
//            if ( ( object )item == null )
//            {
//                for ( int index = 0; index < this._size; ++index )
//                {
//                    if ( ( object )this._items[ index ] == null )
//                    {
//                        return true;
//                    }
//                }
//                return false;
//            }
//            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
//            for ( int index = 0; index < this._size; ++index )
//            {
//                if ( equalityComparer.Equals( this._items[ index ], item ) )
//                {
//                    return true;
//                }
//            }
//            return false;
//        }

//        public void CopyTo( T[ ] array, int arrayIndex )
//        {
//            Array.Copy( ( Array )this._items, 0, ( Array )array, arrayIndex, this._size );
//        }

//        IEnumerator<T> IEnumerable<T>.GetEnumerator( )
//        {
//            return ( IEnumerator<T> )new UltraList<T>.Enumerator( this );
//        }

//        IEnumerator IEnumerable.GetEnumerator( )
//        {
//            return ( IEnumerator )new UltraList<T>.Enumerator( this );
//        }

//        public int IndexOf( T item )
//        {
//            return Array.IndexOf<T>( this._items, item, 0, this._size );
//        }

//        public void Insert( int index, T item )
//        {
//            if ( index > this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            if ( this._size == this._items.Length )
//            {
//                this.EnsureCapacity( this._size + 1 );
//            }

//            if ( index < this._size )
//            {
//                Array.Copy( ( Array )this._items, index, ( Array )this._items, index + 1, this._size - index );
//            }

//            this._items[ index ] = item;
//            ++this._size;
//            ++this._version;
//        }

//        public bool Remove( T item )
//        {
//            int index = this.IndexOf( item );
//            if ( index < 0 )
//            {
//                return false;
//            }

//            this.RemoveAt( index );
//            return true;
//        }

//        public void RemoveAt( int index )
//        {
//            if ( index >= this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            --this._size;
//            if ( index < this._size )
//            {
//                Array.Copy( ( Array )this._items, index + 1, ( Array )this._items, index, this._size - index );
//            }

//            this._items[ this._size ] = default( T );
//            ++this._version;
//        }

//        private static bool IsCompatibleObject( object value )
//        {
//            if ( value is T )
//            {
//                return true;
//            }

//            if ( value == null )
//            {
//                return ( object )default( T ) == null;
//            }

//            return false;
//        }

//        public T GetMaximum( )
//        {
//            return ArrayOperations.Maximum<T>( this._items, 0, this.Count );
//        }

//        public T GetMinimum( )
//        {
//            return ArrayOperations.Minimum<T>( this._items, 0, this.Count );
//        }

//        public void AddRange( IEnumerable<T> collection )
//        {
//            this.InsertRange( this._size, collection );
//        }

//        public IUltraReadOnlyList<T> AsReadOnly( )
//        {
//            return ( IUltraReadOnlyList<T> )new UltraReadOnlyList<T>( this );
//        }

//        public void CopyTo( T[ ] array )
//        {
//            this.CopyTo( array, 0 );
//        }

//        public void CopyTo( int index, T[ ] array, int arrayIndex, int count )
//        {
//            if ( this._size - index < count )
//            {
//                throw new ArgumentException( "Invalid Offset or Length" );
//            }

//            Array.Copy( ( Array )this._items, index, ( Array )array, arrayIndex, count );
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
//            if ( this._items.Length >= min )
//            {
//                return;
//            }

//            int num = this._items.Length == 0 ? 4 : this._items.Length * 2;
//            if ( num < min )
//            {
//                num = min;
//            }

//            this.Capacity = num;
//        }

//        public UltraList<T>.Enumerator GetEnumerator( )
//        {
//            return new UltraList<T>.Enumerator( this );
//        }

//        public int IndexOf( T item, int index )
//        {
//            if ( index > this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            return Array.IndexOf<T>( this._items, item, index, this._size - index );
//        }

//        public int IndexOf( T item, int index, int count )
//        {
//            if ( index > this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            if ( count < 0 || index > this._size - count )
//            {
//                throw new ArgumentOutOfRangeException( nameof( count ) );
//            }

//            return Array.IndexOf<T>( this._items, item, index, count );
//        }

//        public void InsertRange( int index, IEnumerable<T> collection )
//        {
//            if ( collection == null )
//            {
//                throw new ArgumentOutOfRangeException( nameof( collection ) );
//            }

//            if ( index > this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            Array sourceArray = collection as Array;
//            if ( sourceArray != null )
//            {
//                int length = sourceArray.Length;
//                this.EnsureCapacity( this._size + length );
//                Array.Copy( ( Array )this._items, index, ( Array )this._items, index + length, this._size - index );
//                Array.Copy( sourceArray, 0, ( Array )this._items, index, length );
//                this._size += length;
//            }
//            else
//            {
//                IList<T> list = collection as IList<T>;
//                if ( list != null )
//                {
//                    int count = list.Count;
//                    T[ ] uncheckedList = list.ToUncheckedList<T>( );
//                    this.EnsureCapacity( this._size + count );
//                    Array.Copy( ( Array )this._items, index, ( Array )this._items, index + count, this._size - index );
//                    Array.Copy( ( Array )uncheckedList, 0, ( Array )this._items, index, count );
//                    this._size += count;
//                    ++this._version;
//                }
//                else
//                {
//                    using ( IEnumerator<T> enumerator = collection.GetEnumerator( ) )
//                    {
//                        int length = this._size - index;
//                        T[ ] objArray = new T[ length ];
//                        Array.Copy( ( Array )this._items, index, ( Array )objArray, 0, length );
//                        while ( enumerator.MoveNext( ) )
//                        {
//                            this.EnsureCapacity( this._size + 1 );
//                            this._items[ index ] = enumerator.Current;
//                            ++index;
//                            ++this._size;
//                        }
//                        Array.Copy( ( Array )objArray, 0, ( Array )this._items, index, length );
//                    }
//                    ++this._version;
//                    ++this._version;
//                }
//            }
//        }

//        public int LastIndexOf( T item )
//        {
//            if ( this._size == 0 )
//            {
//                return -1;
//            }

//            return this.LastIndexOf( item, this._size - 1, this._size );
//        }

//        public int LastIndexOf( T item, int index )
//        {
//            if ( index >= this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            return this.LastIndexOf( item, index, index + 1 );
//        }

//        public int LastIndexOf( T item, int index, int count )
//        {
//            if ( this.Count != 0 && index < 0 )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            if ( this.Count != 0 && count < 0 )
//            {
//                throw new ArgumentOutOfRangeException( "Count" );
//            }

//            if ( this._size == 0 )
//            {
//                return -1;
//            }

//            if ( index >= this._size )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            if ( count > index + 1 )
//            {
//                throw new ArgumentOutOfRangeException( "Count" );
//            }

//            return Array.LastIndexOf<T>( this._items, item, index, count );
//        }

//        public void RemoveRange( int index, int count )
//        {
//            if ( index < 0 )
//            {
//                throw new ArgumentOutOfRangeException( nameof( index ) );
//            }

//            if ( count < 0 )
//            {
//                throw new ArgumentOutOfRangeException( nameof( count ) );
//            }

//            if ( this._size - index < count )
//            {
//                throw new ArgumentOutOfRangeException( nameof( count ) );
//            }

//            if ( count <= 0 )
//            {
//                return;
//            }

//            this._size -= count;
//            if ( index < this._size )
//            {
//                Array.Copy( ( Array )this._items, index + count, ( Array )this._items, index, this._size - index );
//            }

//            Array.Clear( ( Array )this._items, this._size, count );
//            ++this._version;
//        }

//        public T[ ] ToArray( )
//        {
//            T[ ] objArray = new T[ this._size ];
//            Array.Copy( ( Array )this._items, 0, ( Array )objArray, 0, this._size );
//            return objArray;
//        }

//        public void TrimExcess( )
//        {
//            if ( this._size >= ( int )( ( double )this._items.Length * 0.9 ) )
//            {
//                return;
//            }

//            this.Capacity = this._size;
//        }

//        internal static IList<T> Synchronized( PooledList<T> list )
//        {
//            return ( IList<T> )new UltraList<T>.SynchronizedList( list );
//        }

//        public void SetCount( int setLength )
//        {
//            this._size = setLength;
//        }

//        internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
//        {
//            private readonly PooledList<T> _list;
//            private readonly object _root;

//            internal SynchronizedList( PooledList<T> list )
//            {
//                this._list = list;
//                this._root = ( ( ICollection )list ).SyncRoot;
//            }

//            public int Count
//            {
//                get
//                {
//                    lock ( this._root )
//                    {
//                        return this._list.Count;
//                    }
//                }
//            }

//            public bool IsReadOnly
//            {
//                get
//                {
//                    return ( ( ICollection<T> )this._list ).IsReadOnly;
//                }
//            }

//            public T this[ int index ]
//            {
//                get
//                {
//                    lock ( this._root )
//                    {
//                        return this._list[ index ];
//                    }
//                }
//                set
//                {
//                    lock ( this._root )
//                    {
//                        this._list[ index ] = value;
//                    }
//                }
//            }

//            public void Add( T item )
//            {
//                lock ( this._root )
//                {
//                    this._list.Add( item );
//                }
//            }

//            public void Clear( )
//            {
//                lock ( this._root )
//                {
//                    this._list.Clear( );
//                }
//            }

//            public bool Contains( T item )
//            {
//                lock ( this._root )
//                {
//                    return this._list.Contains( item );
//                }
//            }

//            public void CopyTo( T[ ] array, int arrayIndex )
//            {
//                lock ( this._root )
//                {
//                    this._list.CopyTo( array, arrayIndex );
//                }
//            }

//            public bool Remove( T item )
//            {
//                lock ( this._root )
//                {
//                    return this._list.Remove( item );
//                }
//            }

//            IEnumerator IEnumerable.GetEnumerator( )
//            {
//                lock ( this._root )
//                {
//                    return ( IEnumerator )this._list.GetEnumerator( );
//                }
//            }

//            IEnumerator<T> IEnumerable<T>.GetEnumerator( )
//            {
//                lock ( this._root )
//                {
//                    return ( ( IEnumerable<T> )this._list ).GetEnumerator( );
//                }
//            }

//            public int IndexOf( T item )
//            {
//                lock ( this._root )
//                {
//                    return this._list.IndexOf( item );
//                }
//            }

//            public void Insert( int index, T item )
//            {
//                lock ( this._root )
//                {
//                    this._list.Insert( index, item );
//                }
//            }

//            public void RemoveAt( int index )
//            {
//                lock ( this._root )
//                {
//                    this._list.RemoveAt( index );
//                }
//            }
//        }

//        public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
//        {
//            private readonly UltraList<T> _list;
//            private readonly int _version;
//            private T _current;
//            private int _index;

//            internal Enumerator( UltraList<T> list )
//            {
//                this._list = list;
//                this._index = 0;
//                this._version = list._version;
//                this._current = default( T );
//            }

//            public T Current
//            {
//                get
//                {
//                    return this._current;
//                }
//            }

//            object IEnumerator.Current
//            {
//                get
//                {
//                    if ( this._index == 0 || this._index == this._list._size + 1 )
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
//                UltraList<T> list = this._list;
//                if ( this._version != list._version || this._index >= list._size )
//                {
//                    return this.MoveNextRare( );
//                }

//                this._current = list._items[ this._index ];
//                ++this._index;
//                return true;
//            }

//            void IEnumerator.Reset( )
//            {
//                if ( this._version != this._list._version )
//                {
//                    throw new InvalidOperationException( "Enumerator version is invalid" );
//                }

//                this._index = 0;
//                this._current = default( T );
//            }

//            private bool MoveNextRare( )
//            {
//                if ( this._version != this._list._version )
//                {
//                    throw new InvalidOperationException( "Enumerator version is invalid" );
//                }

//                this._index = this._list._size + 1;
//                this._current = default( T );
//                return false;
//            }
//        }
//    }
//}

