using System;
using System.Collections;
using System.Collections.Generic;

namespace fx.Collections
{
    public static class FragmentableQueue
    {
        public delegate int GetBlockSizeDelegate( Type itemType );
        private static GetBlockSizeDelegate _getDefaultMinimumBlockSize = ( itemType ) => 32;

        public static GetBlockSizeDelegate GetDefaultMinimumBlockSize
        {
            get
            {
                return _getDefaultMinimumBlockSize;
            }
            set
            {
                if( value == null )
                {
                    _getDefaultMinimumBlockSize = ( itemType ) => 32;
                }
                else
                {
                    _getDefaultMinimumBlockSize = value;
                }
            }
        }

        private static GetBlockSizeDelegate _getDefaultMaximumBlockSize = ( itemType ) => 1000000;

        public static GetBlockSizeDelegate GetDefaultMaximumBlockSize
        {
            get
            {
                return _getDefaultMaximumBlockSize;
            }
            set
            {
                if( value == null )
                {
                    _getDefaultMaximumBlockSize = ( itemType ) => 1000000;
                }
                else
                {
                    _getDefaultMaximumBlockSize = value;
                }
            }
        }
    }

    public sealed class FragmentableQueue< T >
    {
        private static Func< int > _getDefaultMinimumBlockSize = ( ) => FragmentableQueue.GetDefaultMinimumBlockSize( typeof( T ) );

        public static Func< int > GetDefaultMinimumBlockSize
        {
            get
            {
                return _getDefaultMinimumBlockSize;
            }
            set
            {
                if( value == null )
                {
                    _getDefaultMinimumBlockSize = ( ) => FragmentableQueue.GetDefaultMinimumBlockSize( typeof( T ) );
                }
                else
                {
                    _getDefaultMinimumBlockSize = value;
                }
            }
        }

        private static Func< int > _getDefaultMaximumBlockSize = ( ) => FragmentableQueue.GetDefaultMaximumBlockSize( typeof( T ) );

        public static Func< int > GetDefaultMaximumBlockSize
        {
            get
            {
                return _getDefaultMaximumBlockSize;
            }
            set
            {
                if( value == null )
                {
                    _getDefaultMaximumBlockSize = ( ) => FragmentableQueue.GetDefaultMaximumBlockSize( typeof( T ) );
                }
                else
                {
                    _getDefaultMaximumBlockSize = value;
                }
            }
        }

        internal sealed class DatabarNode
        {
            internal DatabarNode( long size )
            {
                _array = new T[size];
            }

            internal readonly T[] _array;
            internal DatabarNode? _nextNode;
        }

        private DatabarNode? _firstNode;
        private DatabarNode? _lastNode;
        private T[]? _firstArray;
        private T[]? _lastArray;
        private long _count;
        private readonly Func< int > _getMinimumBlockSize;
        private readonly Func< int > _getMaximumBlockSize;
        private int _positionInFirstNode;
        private int _positionInLastNode;

        public FragmentableQueue( ) : this( null !, null ! )
        {
        }

        public FragmentableQueue( Func< int > getMinimumBlockSize, Func< int > getMaximumBlockSize )
        {
            if( getMinimumBlockSize == null )
            {
                getMinimumBlockSize = ( ) => _getDefaultMinimumBlockSize( );
            }

            if( getMaximumBlockSize == null )
            {
                getMaximumBlockSize = ( ) => _getDefaultMaximumBlockSize( );
            }

            _getMinimumBlockSize = getMinimumBlockSize;
            _getMaximumBlockSize = getMaximumBlockSize;
        }

        public long Count
        {
            get
            {
                return _count;
            }
        }

        public void Clear( )
        {
            _firstNode = null;
            _lastNode = null;
            _firstArray = null;
            _lastArray = null;
            _count = 0;
            _positionInFirstNode = 0;
            _positionInLastNode = 0;
        }

        public void Enqueue( T item )
        {
            if( _lastArray == null || _positionInLastNode >= _lastArray.Length )
            {
                int minimumBlockSize = _getMinimumBlockSize( );
                if( minimumBlockSize < 1 )
                {
                    throw new InvalidOperationException( "The GetMinimumBlockSize delegate gave an invalid result." );
                }

                long blockSize = _count;
                if( blockSize < minimumBlockSize )
                {
                    blockSize = minimumBlockSize;
                }
                else
                {
                    int maximumBlockSize = _getMaximumBlockSize( );
                    if( maximumBlockSize > minimumBlockSize && blockSize > maximumBlockSize )
                    {
                        blockSize = maximumBlockSize;
                    }
                }

                var node = new DatabarNode( blockSize );
                var array = node._array;

                if( _lastNode != null )
                {
                    _lastNode._nextNode = node;
                }
                else
                {
                    _firstNode = node;
                    _firstArray = array;
                }

                _lastNode = node;
                _lastArray = array;
                _positionInLastNode = 0;
            }

            _lastArray[ _positionInLastNode ] = item;
            _positionInLastNode++;
            _count++;
        }

        public T Dequeue( )
        {
            bool succeeded;
            T result = Dequeue( out succeeded );
            if( !succeeded )
            {
                throw new InvalidOperationException( "The queue is empty." );
            }

            return result;
        }

        public T Dequeue( out bool succeeded )
        {
            if( _count == 0 )
            {
                succeeded = false;
                return default( T ) !;
            }

            T result = _firstArray![ _positionInFirstNode ] ;
            _firstArray[ _positionInFirstNode ] = default( T ) !;

            int nextPosition = _positionInFirstNode + 1;
            if( nextPosition < _firstArray.Length )
            {
                _positionInFirstNode = nextPosition;
            }
            else
            {
                _positionInFirstNode = 0;

                _firstNode = _firstNode!._nextNode;
                if( _firstNode != null )
                {
                    _firstArray = _firstNode._array;
                }
                else
                {
                    _firstArray = null;
                    _lastArray = null;
                    _lastNode = null;

                    _positionInLastNode = 0;
                }
            }

            _count--;
            succeeded = true;
            return result;
        }

        /// <summary>
        /// Gets an enumerator to Dequeue items from this queue.
        /// </summary>
        public Enumerator GetEnumerator( )
        {
            return new Enumerator( this );
        }

        public struct Enumerator :
			IEnumerator< T >
        {
            private FragmentableQueue< T > _queue;

            public Enumerator( FragmentableQueue< T > queue )
            {
                _queue = queue;
                _current = default( T )!;
            }

            public void Dispose( )
            {
                _queue = null!;
            }

            private T _current;

            public T Current
            {
                get
                {
                    return _current;
                }
            }

            public bool MoveNext( )
            {
                bool succeeded;
                _current = _queue.Dequeue( out succeeded );
                return succeeded;
            }

            void IEnumerator.Reset( )
            {
                throw new NotSupportedException( );
            }

            object IEnumerator.Current
            {
                get
                {
                    return _current!;
                }
            }
        }
    }
}
