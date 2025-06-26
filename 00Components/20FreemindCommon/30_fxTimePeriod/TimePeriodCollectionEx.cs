
using fx.Base;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace fx.TimePeriod
{
    public sealed class TimePeriodCollectionEx : IEnumerable<TimeBlockEx>, IDisposable
    {        
        private TimeBlockEx[ ] _timeBlockExArray;
        
        private TimeBlockNode _firstNode;
        private TimeBlockNode _actualNode;

        
        private readonly Sequence  _blockCount = new Sequence( 0 );
        
        private readonly Func<int> _getMaximumBlockSize;
        private readonly Sequence  _blockCountInActualArray = new Sequence( 0 );
        
        
        public TimePeriodCollectionEx( int capacity )
        {            
            CreateStorage( capacity );            

            _getMaximumBlockSize = () => _getDefaultMaximumBlockSize(); ;
        }

        
        
        


        // OK. We may not want to put tests in our delegates for specific types, yet
        // we may want to configure specific types with different block sizes.
        // So, we can do it by using the delegates in here. These affect only
        // the actual SBarEx, not the others.
        private static Func<int> _getDefaultFirstBlockSize = ( ) => TimeBlockStaticFunctions.GetDefaultFirstBlockSize( typeof( TimeBlockEx ) );
        public static Func<int> GetDefaultFirstBlockSize
        {
            get
            {
                return _getDefaultFirstBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultFirstBlockSize = () => TimeBlockStaticFunctions.GetDefaultFirstBlockSize( typeof( TimeBlockEx ) );
                else
                    _getDefaultFirstBlockSize = value;
            }
        }

        private static Func<int> _getDefaultMaximumBlockSize = ( ) => TimeBlockStaticFunctions.GetDefaultMaximumBlockSize( typeof( TimeBlockEx ) );
        public static Func<int> GetDefaultMaximumBlockSize
        {
            get
            {
                return _getDefaultMaximumBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultMaximumBlockSize = () => TimeBlockStaticFunctions.GetDefaultMaximumBlockSize( typeof( TimeBlockEx ) );
                else
                    _getDefaultMaximumBlockSize = value;
            }
        }

        internal sealed class TimeBlockNode
        {
            internal TimeBlockNode( int size )
            {
                _timeBlockExArray = ArrayPool<TimeBlockEx>.Shared.Rent( size );
            }            
            
            internal readonly TimeBlockEx[] _timeBlockExArray;
            internal TimeBlockNode _nextNode;
        }
        
        public void Dispose()
        {
            ReturnArray();
            _blockCount.SetValue( 0 );
        }

        private void ReturnArray()
        {
            var node = _firstNode;

            while ( node != null )
            {
                var array    = node._timeBlockExArray;
                var nextNode = node._nextNode;

                try
                {
                    // Clear the elements so that the gc can reclaim the references.
                    ArrayPool<TimeBlockEx>.Shared.Return( array );
                }
                catch ( ArgumentException )
                {
                    // oh well, the array pool didn't like our array
                }

                node = nextNode;
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void CreateStorage( int capacity )
        {
            _firstNode = new TimeBlockNode( capacity );
            _actualNode = _firstNode;
            _timeBlockExArray = _firstNode._timeBlockExArray;
        }

        

        public long LongCount
        {
            get
            {
                return _blockCount.Value;
            }
        }

        public int Count
        {
            get
            {
                return _blockCount.Value;
            }
        }

        public void Clear()
        {
            var size = _firstNode._timeBlockExArray.Length;

            ReturnArray();

            _firstNode = new TimeBlockNode( size );
            _timeBlockExArray = _firstNode._timeBlockExArray;
            _actualNode = _firstNode;

            _blockCount.SetValueVolatile( 0 );
            _blockCountInActualArray.SetValueVolatile( 0 );
        }

        public void Add( DateTime begin, DateTime end )
        {
            if ( _blockCountInActualArray.Value == _timeBlockExArray.Length )
            {
                int count = _blockCount.Value;
                int maximumSize = _getMaximumBlockSize( );

                if ( maximumSize < 1 )
                    throw new InvalidOperationException( "The GetMaximumBlockSize delegate returned an invalid value." );

                if ( count > maximumSize )
                    count = maximumSize;

                var newNode           = new TimeBlockNode( count );
                _actualNode._nextNode = newNode;
                _actualNode = newNode;
                _timeBlockExArray = newNode._timeBlockExArray;
                
                _blockCountInActualArray.SetValueVolatile( 0 );
            }

            ref TimeBlockEx tb = ref _timeBlockExArray[ _blockCountInActualArray.Value ];

            tb.Start = begin;
            tb.End = end;

            _blockCount.IncrementAndGet();
            _blockCountInActualArray.IncrementAndGet();
        }

        // ----------------------------------------------------------------------
        public int IntersectionPeriods( DateTime test )
        {
            int index = -1;

            long min = 0;
            long max = Count - 1;

            while ( min <= max )
            {
                long mid     = ( min + max ) / 2;

                ref TimeBlockEx block = ref this[ mid ];
                
                if ( block.HasInside( test ) )
                {
                    return (int) mid;
                }
                else if ( test < block.Start )
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }            

            return index;
        } 



        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref TimeBlockEx GetNextRefSlot()
        {
            if ( _blockCountInActualArray.Value == _timeBlockExArray.Length )
            {
                int count = _blockCount.Value;
                int maximumSize = _getMaximumBlockSize( );

                if ( maximumSize < 1 )
                    throw new InvalidOperationException( "The GetMaximumBlockSize delegate returned an invalid value." );

                if ( count > maximumSize )
                    count = maximumSize;

                var newNode           = new TimeBlockNode( count );
                _actualNode._nextNode = newNode;
                _actualNode = newNode;
                _timeBlockExArray = newNode._timeBlockExArray;

                _blockCountInActualArray.SetValueVolatile( 0 );
            }


            _blockCount.IncrementAndGet();
            _blockCountInActualArray.IncrementAndGet();

            return ref _timeBlockExArray[ _blockCountInActualArray.Value ];
        }




        
        IEnumerator<TimeBlockEx> IEnumerable<TimeBlockEx>.GetEnumerator()
        {
            return new TimeBlockForEach.Enumerator( _firstNode, _timeBlockExArray, _blockCountInActualArray.Value );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TimeBlockForEach.Enumerator( _firstNode, _timeBlockExArray, _blockCountInActualArray.Value );
        }

        public TimeBlockForEach.Enumerator GetEnumerator()
        {
            return new TimeBlockForEach.Enumerator( _firstNode, _timeBlockExArray, _blockCountInActualArray.Value );
        }
        public TimeBlockForEach AsImmutable()
        {
            return new TimeBlockForEach( _firstNode, _timeBlockExArray, _blockCountInActualArray.Value, _blockCount.Value );
        }
        public TimeBlockEx[ ] ToArray()
        {
            return AsImmutable().ToArray();
        }
        public void CopyTo( TimeBlockEx[ ] array, long arrayIndex )
        {
            AsImmutable().CopyTo( array, arrayIndex );
        }




        public ref TimeBlockEx this[ long index ]
        {
            get
            {
                if ( index < 0 && index > _blockCount.Value )
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }

                var node = _firstNode;

                while ( node != null )
                {
                    var array = node._timeBlockExArray;
                    if ( index < array.Length )
                        return ref array[ index ];

                    index -= array.Length;
                    node = node._nextNode;
                }


                return ref TimeBlockEx.EmptyBlock;
            }

        }
    }


    // We use delegates to get the default values, and they are really "global".
    // That is, instead of being per generic type, they can be configured for
    // all the data-types at once.
    public static class TimeBlockStaticFunctions
    {
        public delegate int GetBlockSizeDelegate( Type itemType );

        private static GetBlockSizeDelegate _getDefaultFirstBlockSize = ( itemType ) => 32;

        public static GetBlockSizeDelegate GetDefaultFirstBlockSize
        {
            get
            {
                return _getDefaultFirstBlockSize;
            }
            set
            {
                if ( value == null )
                    _getDefaultFirstBlockSize = ( itemType ) => 32;
                else
                    _getDefaultFirstBlockSize = value;
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
                if ( value == null )
                    _getDefaultMaximumBlockSize = ( itemType ) => 1000000;
                else
                    _getDefaultMaximumBlockSize = value;
            }
        }

        
    }

    public sealed class TimeBlockForEach
    {
        private readonly TimePeriodCollectionEx.TimeBlockNode _firstNode;
        private readonly TimeBlockEx[ ] _lastArray;
        private readonly int _countInLastArray;
        private readonly long _barCount;

        internal TimeBlockForEach( TimePeriodCollectionEx.TimeBlockNode firstNode, TimeBlockEx[ ] lastArray, int countInLastArray, long count )
        {
            _firstNode = firstNode;
            _lastArray = lastArray;
            _countInLastArray = countInLastArray;
            _barCount = count;
        }

        public long Count
        {
            get
            {
                return _barCount;
            }
        }

        // This method is here for completeness, but it is slower for the
        // latest items as many blocks may need to be navigated.
        // Yet this ElementAt is much faster than the LINQ one, as here
        // we only need to navigate blocks, not element by element.
        public TimeBlockEx ElementAt( long index )
        {
            Debug.Assert( index >= 0 && index < _barCount );

            var node = _firstNode;
            while ( true )
            {
                var array = node._timeBlockExArray;
                if ( index < array.Length )
                    return array[ index ];

                index -= array.Length;
                node = node._nextNode;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator( _firstNode, _lastArray, _countInLastArray );
        }

        public struct Enumerator : IEnumerator<TimeBlockEx>
        {
            private TimeBlockEx[ ] _array;
            private TimePeriodCollectionEx.TimeBlockNode _node;
            private long _positionInArray;
            private long _countInArray;
            private TimeBlockEx[ ] _lastArray;
            private int _countInLastArray;


            internal Enumerator( TimePeriodCollectionEx.TimeBlockNode firstNode, TimeBlockEx[ ] lastArray, int countInLastArray )
            {
                _node = firstNode;
                _array = _node._timeBlockExArray;
                _positionInArray = -1;

                _lastArray = lastArray;

                if ( _array == lastArray )
                    _countInArray = countInLastArray;
                else
                    _countInArray = _array.Length;

                _countInLastArray = countInLastArray;
            }

            public TimeBlockEx Current
            {
                get
                {
                    return _array[ _positionInArray ];
                }
            }

            public void Dispose()
            {
                _array = null;
                _node = null;
                _lastArray = null;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                if ( _array == null )
                    return false;

                _positionInArray++;
                if ( _positionInArray >= _countInArray )
                {
                    _node = _node._nextNode;

                    if ( _node == null )
                    {
                        _array = null;
                        return false;
                    }

                    _array = _node._timeBlockExArray;
                    _positionInArray = 0;

                    if ( _array == _lastArray )
                        _countInArray = _countInLastArray;
                    else
                        _countInArray = _array.Length;
                }

                return true;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }

        public TimeBlockEx[ ] ToArray()
        {
            var result = new TimeBlockEx[ _barCount ];

            CopyTo( result, 0 );

            return result;
        }
        public void CopyTo( TimeBlockEx[ ] array, long arrayIndex )
        {
            Debug.Assert( array != null );

            Debug.Assert( arrayIndex >= 0 && arrayIndex <= ( array.Length - _barCount ) );

            var node = _firstNode;
            while ( true )
            {
                var nodeArray = node._timeBlockExArray;

                if ( nodeArray == _lastArray )
                {
                    Array.Copy( nodeArray, 0, array, arrayIndex, _countInLastArray );
                    return;
                }

                nodeArray.CopyTo( array, arrayIndex );
                arrayIndex += nodeArray.Length;

                node = node._nextNode;
            }
        }
    }
}



