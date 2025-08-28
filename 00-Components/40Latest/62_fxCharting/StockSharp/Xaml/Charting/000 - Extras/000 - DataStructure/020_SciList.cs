using SciChart.Data.Model;
using StockSharp.Algo.Indicators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Numerics;


namespace StockSharp.Xaml.Charting;

/// <summary>
/// Defines a high-performance array-backed generic list type
/// </summary>
/// <typeparam name="T"></typeparam>
public class SciList<T> : ISciList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection 
{
    private static readonly T[] _emptyArray = new T[0];
    private const int _defaultCapacity = 128;
    private T[] _items;
    private int _size;
    private object _syncRoot;
    private int _version;

    public SciList() : this( 128 )
    {
    }

    public SciList( int capacity )
    {
        if ( capacity < 0 )
            throw new ArgumentOutOfRangeException( nameof( capacity ) );
        _items = new T[capacity];
    }

    public static SciList<T> ForArray( T[ ] arr )
    {
        return new SciList<T>() { _items = arr, _size = arr.Length };
    }

    public SciList( IEnumerable<T> collection )
    {
        if ( collection == null )
            throw new ArgumentNullException( nameof( collection ) );

        ICollection<T> objs = collection as ICollection<T>;

        if ( objs != null )
        {
            int count = objs.Count;
            _items = new T[count];
            objs.CopyTo( _items, 0 );
            _size = count;
        }
        else
        {
            _size = 0;
            _items = new T[128];
            foreach ( T obj in collection )
                Add( obj );
        }
    }

    //
    // Summary:
    //     Gets the internal ItemsArray that this list wraps for direct unchecked access
    //     NOTE: The count of the ItemsArray may differ from the count of the List. Use
    //     the List.Count when iterating
    public T[ ] ItemsArray
    {
        get
        {
            return _items;
        }
    }

    internal int Capacity
    {
        get
        {
            return _items.Length;
        }
        set
        {
            if ( value < _size )
                throw new ArgumentOutOfRangeException( nameof( value ) );
            if ( value == _items.Length )
                return;
            if ( value > 0 )
            {
                T[] objArray = new T[value];
                if ( _size > 0 )
                    Array.Copy( ( Array ) _items, 0, ( Array ) objArray, 0, _size );
                _items = objArray;
            }
            else
                _items = SciList<T>._emptyArray;
        }
    }

    bool IList.IsFixedSize
    {
        get
        {
            return false;
        }
    }

    bool IList.IsReadOnly
    {
        get
        {
            return false;
        }
    }

    bool ICollection.IsSynchronized
    {
        get
        {
            return false;
        }
    }

    object ICollection.SyncRoot
    {
        get
        {
            if ( _syncRoot == null )
                Interlocked.CompareExchange<object>( ref _syncRoot, new object(), ( object ) null );
            return _syncRoot;
        }
    }

    object IList.this[int index]
    {
        get
        {
            return ( object ) this[index];
        }
        set
        {
            try
            {
                this[index] = ( T ) value;
            }
            catch ( InvalidCastException ex )
            {
                throw new ArgumentException(
                    string.Format( "Value type {0} was of the wrong type", ( object ) value.GetType().Name ) );
            }
        }
    }

    int IList.Add( object item )
    {
        if ( item == null )
            throw new ArgumentNullException( nameof( item ) );
        try
        {
            Add( ( T ) item );
        }
        catch ( InvalidCastException ex )
        {
            throw new ArgumentException(
                string.Format( "Value type {0} was of the wrong type", ( object ) item.GetType().Name ) );
        }
        return Count - 1;
    }

    [SecuritySafeCritical]
    bool IList.Contains( object item )
    {
        if ( SciList<T>.IsCompatibleObject( item ) )
            return Contains( ( T ) item );
        return false;
    }

    void ICollection.CopyTo( Array array, int arrayIndex )
    {
        if ( array != null && array.Rank != 1 )
            throw new ArgumentException( "Array rank not supported", "array.Rank" );
        try
        {
            Array.Copy( ( Array ) _items, 0, array, arrayIndex, _size );
        }
        catch ( ArrayTypeMismatchException ex )
        {
            throw new ArgumentException( "Invalid array type" );
        }
    }

    int IList.IndexOf( object item )
    {
        if ( SciList<T>.IsCompatibleObject( item ) )
            return IndexOf( ( T ) item );
        return -1;
    }

    void IList.Insert( int index, object item )
    {
        try
        {
            Insert( index, ( T ) item );
        }
        catch ( InvalidCastException ex )
        {
            throw new ArgumentException(
                string.Format( "Value type {0} was of the wrong type", ( object ) item.GetType().Name ) );
        }
    }

    [SecuritySafeCritical]
    void IList.Remove( object item )
    {
        if ( !SciList<T>.IsCompatibleObject( item ) )
            return;
        Remove( ( T ) item );
    }

    public int Count
    {
        get
        {
            return _size;
        }
        internal set
        {
            _size = value;
        }
    }

    bool ICollection<T>.IsReadOnly
    {
        get
        {
            return false;
        }
    }

    public bool HasValues
    {
        get 
        { 
            return _items.Length > 0; 
        }
    }

    public T this[int index]
    {
        get
        {
            if ( index >= _size )
                return default( T );
            return _items[index];
        }
        set
        {
            if ( index >= _size )
                throw new ArgumentOutOfRangeException( nameof( index ) );
            _items[index] = value;
            ++_version;
        }
    }

    public void Add( T item )
    {
        if ( _size == _items.Length )
            EnsureCapacity( _size + 1 );
        _items[_size++] = item;
        ++_version;
    }

    public void Clear()
    {
        if ( _size > 0 )
        {
            Array.Clear( ( Array ) _items, 0, _size );
            _size = 0;
        }
        ++_version;
    }

    public bool Contains( T item )
    {
        if ( ( object ) item == null )
        {
            for ( int index = 0; index < _size; ++index )
            {
                if ( ( object ) _items[index] == null )
                    return true;
            }
            return false;
        }
        EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
        for ( int index = 0; index < _size; ++index )
        {
            if ( equalityComparer.Equals( _items[index], item ) )
                return true;
        }
        return false;
    }

    public void CopyTo( T[ ] array, int arrayIndex )
    {
        Array.Copy( ( Array ) _items, 0, ( Array ) array, arrayIndex, _size );
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ( IEnumerator<T> ) new SciList<T>.Enumerator( this );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ( IEnumerator ) new SciList<T>.Enumerator( this );
    }

    public int IndexOf( T item )
    {
        return Array.IndexOf<T>( _items, item, 0, _size );
    }

    public void Insert( int index, T item )
    {
        if ( index > _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        if ( _size == _items.Length )
            EnsureCapacity( _size + 1 );
        if ( index < _size )
            Array.Copy( ( Array ) _items, index, ( Array ) _items, index + 1, _size - index );
        _items[index] = item;
        ++_size;
        ++_version;
    }

    public bool Remove( T item )
    {
        int index = IndexOf(item);
        if ( index < 0 )
            return false;
        RemoveAt( index );
        return true;
    }

    public void RemoveAt( int index )
    {
        if ( index >= _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        --_size;
        if ( index < _size )
            Array.Copy( ( Array ) _items, index + 1, ( Array ) _items, index, _size - index );
        _items[_size] = default( T );
        ++_version;
    }

    private static bool IsCompatibleObject( object value )
    {
        if ( value is T )
            return true;
        if ( value == null )
            return ( object ) default( T ) == null;
        return false;
    }

    /// <summary>
    /// Gets the maximum in the list
    /// </summary>
    /// <returns></returns>
    public T GetMaximum()
    {
        return ArrayOperations.Maximum<T>( _items, 0, Count );
    }

    /// <summary>
    /// Gets the minimum in the list
    /// </summary>
    /// <returns></returns>
    public T GetMinimum()
    {
        return ArrayOperations.Minimum<T>( _items, 0, Count );
    }

    /// <summary>
    /// Gets the minimum and maximum in the list
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetMinMax( out T min, out T max )
    {
        min = GetMinimum();
        max = GetMaximum();
    }

    /// <summary>
    /// Adds a range of items to the list
    /// </summary>
    /// <param name="collection"></param>
    public void AddRange( IEnumerable<T> collection )
    {
        InsertRange( _size, collection );
    }

    public IUltraReadOnlyList<T> AsReadOnly()
    {
        return ( IUltraReadOnlyList<T> ) new UltraReadOnlyList<T>( this );
    }

    public void CopyTo( T[ ] array )
    {
        CopyTo( array, 0 );
    }

    public void CopyTo( int index, T[ ] array, int arrayIndex, int count )
    {
        if ( _size - index < count )
            throw new ArgumentException( "Invalid Offset or Length" );
        Array.Copy( ( Array ) _items, index, ( Array ) array, arrayIndex, count );
    }

    public bool EnsureMinSize( int minSize )
    {
        if ( Count >= minSize )
            return false;
        EnsureCapacity( minSize );
        Count = minSize;
        return true;
    }

    private void EnsureCapacity( int min )
    {
        if ( _items.Length >= min )
            return;
        int num = _items.Length == 0 ? 4 : _items.Length * 2;
        if ( num < min )
            num = min;
        Capacity = num;
    }

    public SciList<T>.Enumerator GetEnumerator()
    {
        return new SciList<T>.Enumerator( this );
    }

    public int IndexOf( T item, int index )
    {
        if ( index > _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        return Array.IndexOf<T>( _items, item, index, _size - index );
    }

    public int IndexOf( T item, int index, int count )
    {
        if ( index > _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        if ( count < 0 || index > _size - count )
            throw new ArgumentOutOfRangeException( nameof( count ) );
        return Array.IndexOf<T>( _items, item, index, count );
    }

    /// <summary>
    /// Inserts a range of items to the list
    /// </summary>
    /// <param name="index"></param>
    /// <param name="collection"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void InsertRange( int index, IEnumerable<T> collection )
    {
        if ( collection == null )
            throw new ArgumentOutOfRangeException( nameof( collection ) );

        if ( index > _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );

        Array sourceArray = collection as Array;
        
        if ( sourceArray != null )
        {
            int length = sourceArray.Length;
            EnsureCapacity( _size + length );
            Array.Copy( ( Array ) _items, index, ( Array ) _items, index + length, _size - index );
            Array.Copy( sourceArray, 0, ( Array ) _items, index, length );
            _size += length;
        }
        else
        {
            IList<T> list = collection as IList<T>;
            
            if ( list != null )
            {
                int count = list.Count;
                T[] uncheckedList = list.ToUncheckedList<T>();
                EnsureCapacity( _size + count );
                Array.Copy( ( Array ) _items, index, ( Array ) _items, index + count, _size - index );
                Array.Copy( ( Array ) uncheckedList, 0, ( Array ) _items, index, count );
                _size += count;
                ++_version;
            }
            else
            {
                using ( IEnumerator<T> enumerator = collection.GetEnumerator() )
                {
                    int length = _size - index;
                    T[] objArray = new T[length];
                    Array.Copy( ( Array ) _items, index, ( Array ) objArray, 0, length );
                    while ( enumerator.MoveNext() )
                    {
                        EnsureCapacity( _size + 1 );
                        _items[index] = enumerator.Current;
                        ++index;
                        ++_size;
                    }
                    Array.Copy( ( Array ) objArray, 0, ( Array ) _items, index, length );
                }
                ++_version;
                ++_version;
            }
        }
    }

    public int LastIndexOf( T item )
    {
        if ( _size == 0 )
            return -1;
        return LastIndexOf( item, _size - 1, _size );
    }

    public int LastIndexOf( T item, int index )
    {
        if ( index >= _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        return LastIndexOf( item, index, index + 1 );
    }

    public int LastIndexOf( T item, int index, int count )
    {
        if ( Count != 0 && index < 0 )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        if ( Count != 0 && count < 0 )
            throw new ArgumentOutOfRangeException( "Count" );
        if ( _size == 0 )
            return -1;
        if ( index >= _size )
            throw new ArgumentOutOfRangeException( nameof( index ) );
        if ( count > index + 1 )
            throw new ArgumentOutOfRangeException( "Count" );
        return Array.LastIndexOf<T>( _items, item, index, count );
    }

    /// <summary>
    /// Removes a range of items from the list
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void RemoveRange( int index, int count )
    {
        if ( index < 0 )
            throw new ArgumentOutOfRangeException( nameof( index ) );

        if ( count < 0 )
            throw new ArgumentOutOfRangeException( nameof( count ) );

        if ( _size - index < count )
            throw new ArgumentOutOfRangeException( nameof( count ) );

        if ( count <= 0 )
            return;

        _size -= count;

        if ( index < _size )
            Array.Copy( ( Array ) _items, index + count, ( Array ) _items, index, _size - index );

        Array.Clear( ( Array ) _items, _size, count );
        ++_version;
    }

    public T[ ] ToArray()
    {
        T[] objArray = new T[_size];
        Array.Copy( ( Array ) _items, 0, ( Array ) objArray, 0, _size );
        return objArray;
    }

    public void TrimExcess()
    {
        if ( _size >= ( int ) ( ( double ) _items.Length * 0.9 ) )
            return;
        Capacity = _size;
    }

    internal static IList<T> Synchronized( List<T> list )
    {
        return ( IList<T> ) new SciList<T>.SynchronizedList( list );
    }

    /// <summary>
    /// Forces the count of the list, in operations where we know the capacity in advance
    /// </summary>
    /// <param name="setLength"></param>
    public void SetCount( int setLength )
    {
        _size = setLength;
    }

    /// <summary>
    /// Returns a <see cref="List{T}"/> that contains the elements of the <see cref="SciList{T}"/>.
    /// </summary>
    /// <returns></returns>
    public IList AsList()
    {
        return new List<T>( _items );
    }

    /// <summary>
    /// Gets a value indicating whether this list has NaN values in specified range
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool ContainsNaN( int startIndex, int count )
    {
        return _items.Any( item => IsNaN( item as dynamic ) );        
    }

    private static bool IsNaN( dynamic value )
    {
        if ( value is float f ) return float.IsNaN( f );
        if ( value is double d ) return double.IsNaN( d );
        return false;
    }
    public bool IsSortedAscending( int startIndex, int count )
    {
        throw new NotImplementedException();
    }

    public bool IsEvenlySpaced( int startIndex, int count, double epsilon, out double spacing )
    {
        spacing = 0.0;

        if ( _items == null )
        {
            throw new ArgumentNullException( nameof( _items ) );
        }
        if ( startIndex < 0 || startIndex >= _items.Length )
        {
            throw new ArgumentOutOfRangeException( nameof( startIndex ), "StartIndex is out of the bounds of the list." );
        }
        if ( count < 0 )
        {
            throw new ArgumentOutOfRangeException( nameof( count ), "Count cannot be negative." );
        }
        if ( startIndex + count > _items.Length )
        {
            throw new ArgumentOutOfRangeException( nameof( count ), "The range defined by startIndex and count exceeds the list's bounds." );
        }

        // Lists with 0, 1, or 2 elements in the range are considered evenly spaced.
        if ( count <= 2 )
        {
            spacing = count <= 1 ? 0.0 : Convert.ToDouble( _items[startIndex + 1] ) - Convert.ToDouble( _items[startIndex] );
            return true;
        }

        // Calculate the common difference from the first two elements using Convert.ToDouble.
        double first = Convert.ToDouble(_items[startIndex]);
        double second = Convert.ToDouble(_items[startIndex + 1]);
        double calculatedSpacing = second - first;

        spacing = calculatedSpacing;

        // Iterate from the third element in the range.
        for ( int i = startIndex + 2; i < startIndex + count; i++ )
        {
            double current = Convert.ToDouble(_items[i]);
            double previous = Convert.ToDouble(_items[i - 1]);
            double actualDifference = current - previous;

            // Compare the actual difference with the initial spacing, considering epsilon.
            if ( Math.Abs( actualDifference - calculatedSpacing ) > epsilon )
            {
                return false;
            }
        }

        return true;
    }

    internal class SynchronizedList : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly List<T> _list;
        private readonly object _root;

        internal SynchronizedList( List<T> list )
        {
            _list = list;
            _root = ( ( ICollection ) list ).SyncRoot;
        }

        public int Count
        {
            get
            {
                lock ( _root )
                    return _list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ( ( ICollection<T> ) _list ).IsReadOnly;
            }
        }

        public T this[int index]
        {
            get
            {
                lock ( _root )
                    return _list[index];
            }
            set
            {
                lock ( _root )
                    _list[index] = value;
            }
        }

        public void Add( T item )
        {
            lock ( _root )
                _list.Add( item );
        }

        public void Clear()
        {
            lock ( _root )
                _list.Clear();
        }

        public bool Contains( T item )
        {
            lock ( _root )
                return _list.Contains( item );
        }

        public void CopyTo( T[ ] array, int arrayIndex )
        {
            lock ( _root )
                _list.CopyTo( array, arrayIndex );
        }

        public bool Remove( T item )
        {
            lock ( _root )
                return _list.Remove( item );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock ( _root )
                return ( IEnumerator ) _list.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock ( _root )
                return ( ( IEnumerable<T> ) _list ).GetEnumerator();
        }

        public int IndexOf( T item )
        {
            lock ( _root )
                return _list.IndexOf( item );
        }

        public void Insert( int index, T item )
        {
            lock ( _root )
                _list.Insert( index, item );
        }

        public void RemoveAt( int index )
        {
            lock ( _root )
                _list.RemoveAt( index );
        }
    }

    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
        private readonly SciList<T> _list;
        private readonly int _version;
        private T _current;
        private int _index;

        internal Enumerator( SciList<T> list )
        {
            _list    = list;
            _index   = 0;
            _version = list._version;
            _current = default( T );
        }

        public T Current
        {
            get
            {
                return _current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if ( _index == 0 || _index == _list._size + 1 )
                    throw new InvalidOperationException( "Enumerator Index out of range" );
                return ( object ) Current;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            SciList<T> list = _list;
            if ( _version != list._version || _index >= list._size )
                return MoveNextRare();
            _current = list._items[_index];
            ++_index;
            return true;
        }

        void IEnumerator.Reset()
        {
            if ( _version != _list._version )
                throw new InvalidOperationException( "Enumerator version is invalid" );
            _index = 0;
            _current = default( T );
        }

        private bool MoveNextRare()
        {
            if ( _version != _list._version )
                throw new InvalidOperationException( "Enumerator version is invalid" );
            _index = _list._size + 1;
            _current = default( T );
            return false;
        }
    }
}
