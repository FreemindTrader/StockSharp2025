using System;
using System.Collections;
using System.Collections.Generic;

namespace fx.Definitions
{    
    public static class fxListReadOnlyExtensions
    {
        /// <summary>
        /// Note: Even when you ask for a ReadOnlyCollection you do receive a
        /// type that implements IReadOnlyList&lt;T&gt;, so it is possible to call
        /// the indexer (which is faster than a normal LINQ ElementAt()) yet
        /// such method is not the best performing one.
        /// </summary>
        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>( this fxList<T> collection )
        {
            return collection.AsReadOnlyList( );
        }

        /// <summary>
        /// Note: Even when you ask for a ReadOnlyCollection you do receive a
        /// type that implements IReadOnlyList&lt;T&gt;, so it is possible to call
        /// the indexer (which is faster than a normal LINQ ElementAt()) yet
        /// such method is not the best performing one.
        /// </summary>
        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>( this fxListForEach<T> collection )
        {
            return collection.AsReadOnlyList( );
        }

        // These methods return the FastList/ForEachCollection as a readonly list.
        // Note, though, that the indexer of such collections is not pretty fast (yet
        // it is faster than the LINQ ElementAt()).
        public static IReadOnlyList<T> AsReadOnlyList<T>( this fxList<T> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( "collection" );
            }

            return collection.AsImmutable( ).AsReadOnlyList( );
        }

        public static IReadOnlyList<T> AsReadOnlyList<T>( this fxListForEach<T> collection )
        {
            if ( collection == null )
            {
                throw new ArgumentNullException( "collection" );
            }

            return new _ReadOnlyList<T>( collection );
        }

        private sealed class _ReadOnlyList<T> : IReadOnlyList<T>
        {
            private readonly fxListForEach<T> _collection;

            internal _ReadOnlyList( fxListForEach<T> collection )
            {
                _collection = collection;
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

            public IEnumerator<T> GetEnumerator( )
            {
                return _collection.GetEnumerator( );
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return _collection.GetEnumerator( );
            }

            public T this[ int index ]
            {
                get { return _collection.ElementAt( index ); }
            }
        }
    }
}


