using System;
using System.Collections;
using System.Collections.Generic;

namespace fx.Definitions
{
    public static class fxListMutableExtensions
    {
        /// <summary>
        /// Creates an IList&lt;T&gt; wrapper over this collection.
        /// Note that only mutable methods available to the result are the Add() and Clear().
        /// The indexer can only be used for gets and doesn't has a good performance either.
        /// The methods IndexOf() and Contains() haven't a good performance either.
        /// This method may be useful if you want to use this type with some LINQ methods.
        /// </summary>
        public static IList<T> AsMutableList<T>( this fxList<T> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( "collection" );
            }

            return new _fxList<T>( collection );
        }

        private sealed class _fxList<T> : IList<T>, IReadOnlyList<T>
        {
            private readonly fxList<T> _collection;

            internal _fxList( fxList<T> collection )
            {
                _collection = collection;
            }

            public int IndexOf( T item )
            {
                int index = -1;
                var comparer = EqualityComparer<T>.Default;

                checked
                {
                    foreach ( var otherItem in _collection )
                    {
                        index++;
                        if ( comparer.Equals( item, otherItem ) )
                        {
                            return index;
                        }
                    }
                }

                return -1;
            }

            public void Insert( int index, T item )
            {
                throw new NotSupportedException( );
            }

            public void RemoveAt( int index )
            {
                throw new NotSupportedException( );
            }

            public T this[ int index ]
            {
                get { return _collection.ElementAt( index ); }
                set { throw new NotSupportedException( ); }
            }

            public void Add( T item )
            {
                _collection.Add( item );
            }

            public void Clear( )
            {
                _collection.Clear( );
            }

            public bool Contains( T item )
            {
                return IndexOf( item ) != -1;
            }

            public void CopyTo( T[ ] array, int arrayIndex )
            {
                _collection.CopyTo( array, arrayIndex );
            }

            public int Count
            {
                get
                {
                    checked
                    {
                        return ( int )_collection.Count;
                    }
                }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool Remove( T item )
            {
                throw new NotSupportedException( );
            }

            public IEnumerator<T> GetEnumerator( )
            {
                return _collection.GetEnumerator( );
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return _collection.GetEnumerator( );
            }
        }

        /// <summary>
        /// Creates an IList&lt;T&gt; wrapper over this collection.
        /// Note that the collection will continue to be immutable. This method is useful
        /// if you plan to use the LINQ methods, as they actually only search for the mutable
        /// interfaces. This will not make the immutable collection mutable again.
        /// </summary>
        public static IList<T> AsMutableList<T>( this fxListForEach<T> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( "collection" );
            }

            return new _fxListForEachAsIList<T>( collection );
        }

        private sealed class _fxListForEachAsIList<T> : IList<T>, IReadOnlyList<T>
        {
            private readonly fxListForEach<T> _collection;

            internal _fxListForEachAsIList( fxListForEach<T> collection )
            {
                _collection = collection;
            }

            public int IndexOf( T item )
            {
                int index = -1;
                var comparer = EqualityComparer<T>.Default;

                checked
                {
                    foreach ( var otherItem in _collection )
                    {
                        index++;
                        if ( comparer.Equals( item, otherItem ) )
                        {
                            return index;
                        }
                    }
                }

                return -1;
            }

            public void Insert( int index, T item )
            {
                throw new NotSupportedException( );
            }

            public void RemoveAt( int index )
            {
                throw new NotSupportedException( );
            }

            public T this[ int index ]
            {
                get { return _collection.ElementAt( index ); }
                set { throw new NotSupportedException( ); }
            }

            public void Add( T item )
            {
                throw new InvalidOperationException( );
            }

            public void Clear( )
            {
                throw new InvalidOperationException( );
            }

            public bool Contains( T item )
            {
                return IndexOf( item ) != -1;
            }

            public void CopyTo( T[ ] array, int arrayIndex )
            {
                _collection.CopyTo( array, arrayIndex );
            }

            public int Count
            {
                get
                {
                    checked
                    {
                        return ( int )_collection.Count;
                    }
                }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove( T item )
            {
                throw new NotSupportedException( );
            }

            public IEnumerator<T> GetEnumerator( )
            {
                return _collection.GetEnumerator( );
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return _collection.GetEnumerator( );
            }
        }
    }
}

