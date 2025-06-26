// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using fx.Definitions;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace fx.Bars
{
    public struct sIndicator 
    {
        private DateTime _utcTime;

        private IIndicatorValue indicatorValue;

        /// <summary>Create instance.</summary>
        /// <param name="dto">Value timestamp.</param>
        /// <param name="val">Indicator value.</param>
        public sIndicator( DateTimeOffset dto, IIndicatorValue val )
        {
            this = new sIndicator( dto.UtcDateTime, val );
        }

        public sIndicator( DateTime _param1, IIndicatorValue _param2 )
        {
            _utcTime = _param1;
            indicatorValue = _param2;
        }

        /// <summary>Value timestamp.</summary>
        public DateTime Time
        {
            get
            {
                return _utcTime;
            }

            set
            {
                _utcTime = value;
            }
        }

        /// <summary>Indicator value.</summary>
        public IIndicatorValue Value
        {
            get
            {
                return indicatorValue;
            }

            set
            {
                indicatorValue = value;
            }
        }
    }
    /// <summary>
    /// Implements a variable-size list that uses a pooled array to store the
    /// elements. A IndicatorValuesList has a capacity, which is the allocated length
    /// of the internal array. As elements are added to a IndicatorValuesList, the capacity
    /// of the IndicatorValuesList is automatically increased as required by reallocating the
    /// internal array.
    /// </summary>
    /// <remarks>
    /// This class is based on the code for <see cref="List{sIndicator}"/> but it supports <see cref="Span{sIndicator}"/>
    /// and uses <see cref="ArrayPool{sIndicator}"/> when allocating internal arrays.
    /// </remarks>
    [DebuggerDisplay( "Count = {Count}" )]
    [DebuggerTypeProxy( typeof( ICollectionDebugView<> ) )]
    [Serializable]
    public class IndicatorValuesList : IEnumerable<sIndicator>, IDisposable
    {

        #region TONY MINIMUM USE

        /// <summary>
        /// Constructs a IndicatorValuesList. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to DefaultCapacity, and then increased in multiples of two
        /// as required.
        /// </summary>
        public IndicatorValuesList() : this( ClearMode.Auto, ArrayPool<sIndicator>.Shared ) { }

        /// <summary>
        /// Constructs a IndicatorValuesList. The list is initially empty and has a capacity
        /// of zero. Upon adding the first element to the list the capacity is
        /// increased to DefaultCapacity, and then increased in multiples of two
        /// as required.
        /// </summary>
        public IndicatorValuesList( ClearMode clearMode, ArrayPool<sIndicator> customPool )
        {
            _sIndicatorValues = s_emptySBarArray;
            _sIndicatorPool = customPool ?? ArrayPool<sIndicator>.Shared;
        }

        /// <summary>
        /// Gets or sets the element at the given index.
        /// </summary>
        public ref sIndicator this[ int index ]
        {
            get
            {
                // Following trick can reduce the range check by one
                if ( ( uint ) index >= ( uint ) _size.Value )
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                return ref _sIndicatorValues[ index ];
            }
        }

        public ref sIndicator this[ uint index ]
        {
            get
            {
                // Following trick can reduce the range check by one
                if ( index >= ( uint ) _size.Value )
                {
                    ThrowHelper.ThrowArgumentOutOfRange_IndexException();
                }
                return ref _sIndicatorValues[ index ];
            }
        }

        public IndicatorValuesList( int capacity )
        {
            if ( capacity < 0 )
                ThrowHelper.ThrowArgumentOutOfRangeException( ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum );

            _sIndicatorPool = ArrayPool<sIndicator>.Shared;

            if ( capacity == 0 )
            {
                _sIndicatorValues = s_emptySBarArray;
            }
            else
            {
                _sIndicatorValues = _sIndicatorPool.Rent( capacity );
            }

            _capacity = capacity;

            Array.Clear( _sIndicatorValues, 0, _capacity );

            //for ( uint index = 0; index < _sIndicatorValues.Length; index++ )
            //{
            //    _sIndicatorValues[ index ] = new sIndicator( DateTime.MinValue, null );
            //}
        }

        /// <summary>
        /// Gets and sets the capacity of this list.  The capacity is the size of
        /// the internal array used to hold items.  When set, the internal 
        /// Memory of the list is reallocated to the given capacity.
        /// Note that the return value for this property may be larger than the property was set to.
        /// </summary>
        public int Capacity
        {
            get => _sIndicatorValues.Length;
            set
            {
                if ( value < _capacity )
                {
                    ThrowHelper.ThrowArgumentOutOfRangeException( ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity );
                }

                if ( value != _sIndicatorValues.Length )
                {
                    if ( value > 0 )
                    {
                        var newItems = _sIndicatorPool.Rent( value );

                        if ( _size.Value > 0 )
                        {
                            Array.Copy( _sIndicatorValues, newItems, _size.Value );

                            //for ( uint index = ( uint ) _size.Value + 1; index < value; index++ )
                            //{
                            //    newItems[ index ] = new sIndicator( DateTime.MinValue, null );
                            //}
                        }
                        //else
                        //{
                        //    for ( uint index = 0; index < value; index++ )
                        //    {
                        //        newItems[ index ] = new sIndicator( DateTime.MinValue, null );
                        //    }
                        //}

                        ReturnArray();
                        _sIndicatorValues = newItems;
                    }
                    else
                    {
                        ReturnArray();
                        _capacity = 0;
                    }
                }
            }
        }

        private void ReturnArray()
        {
            if ( _sIndicatorValues.Length == 0 )
                return;

            try
            {
                // Clear the elements so that the gc can reclaim the references.
                _sIndicatorPool.Return( _sIndicatorValues, true );
            }
            catch ( ArgumentException )
            {
                // oh well, the array pool didn't like our array
            }

            _sIndicatorValues = s_emptySBarArray;
        }

        /// <summary>
        /// Ensures that the capacity of this list is at least the given minimum
        /// value. If the current capacity of the list is less than min, the
        /// capacity is increased to twice the current capacity or to min,
        /// whichever is larger.
        /// </summary>
        private void EnsureCapacity( int min )
        {
            if ( _sIndicatorValues.Length < min )
            {
                int newCapacity = _sIndicatorValues.Length == 0 ? DefaultCapacity : _sIndicatorValues.Length * 2;
                // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
                // Note that this check works even when _sIndicatorValues.Length overflowed thanks to the (uint) cast
                if ( ( uint ) newCapacity > MaxArrayLength ) newCapacity = MaxArrayLength;
                if ( newCapacity < min ) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        /// <summary>
        /// Read-only property describing how many elements are in the List.
        /// </summary>
        public int Count => _size.Value;

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Add( sIndicator item )
        {
            int size = _size.Value;
            if ( ( uint ) size < ( uint ) _sIndicatorValues.Length )
            {
                _size.IncrementAndGet();
                _sIndicatorValues[ size ] = item;
            }
            else
            {
                AddWithResize( item );
            }
        }

        

        public Span<sIndicator> GetSpan( int start, int end )
        {
            Span<sIndicator> output = _sIndicatorValues;

            return output.Slice( start, end - start + 1 );
        }

        public Span<sIndicator> GetSpan( uint start, uint end )
        {
            Span<sIndicator> output = _sIndicatorValues;

            int length = (int)( end - start + 1 );

            return output.Slice( ( int ) start, length );
        }
        private readonly Sequence _size = new Sequence( 0 );

        

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref sIndicator SetIndicatorValue( DateTimeOffset dto, IIndicatorValue value )
        {
            int size = _size.Value;
            if ( ( uint ) size < ( uint ) _sIndicatorValues.Length )
            {
                _size.IncrementAndGet();

                ref var indicator = ref _sIndicatorValues[ size ];
                indicator.Time    = dto.UtcDateTime;
                indicator.Value   = value;

                return ref _sIndicatorValues[ size ];
            }
            else
            {
                return ref SetIndicatorValueWithResize( dto, value );
            }
        }

        // Non-inline from List.Add to improve its code quality as uncommon path
        [MethodImpl( MethodImplOptions.NoInlining )]
        private ref sIndicator SetIndicatorValueWithResize( DateTimeOffset dto, IIndicatorValue value )
        {
            int size = _size.Value;
            EnsureCapacity( size + 1 );
            _size.IncrementAndGet();

            ref var indicator = ref _sIndicatorValues[ size ];
            indicator.Time    = dto.UtcDateTime;
            indicator.Value   = value;

            return ref _sIndicatorValues[ size ];
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref sIndicator GetNextRefSlot()
        {
            int size = _size.Value;
            if ( ( uint ) size < ( uint ) _sIndicatorValues.Length )
            {
                _size.IncrementAndGet();
                return ref _sIndicatorValues[ size ];
            }
            else
            {
                EnsureCapacity( size + 1 );
                _size.IncrementAndGet();

                return ref _sIndicatorValues[ size ];
            }
        }


        // Non-inline from List.Add to improve its code quality as uncommon path
        [MethodImpl( MethodImplOptions.NoInlining )]
        private void AddWithResize( sIndicator item )
        {
            int size = _size.Value;
            EnsureCapacity( size + 1 );
            _size.IncrementAndGet();
            _sIndicatorValues[ size ] = item;
        }


        /// <summary>
        /// Returns an enumerator for this list with the given
        /// permission for removal of elements. If modifications made to the list 
        /// while an enumeration is in progress, the MoveNext and 
        /// GetObject methods of the enumerator will throw an exception.
        /// </summary>
        public Enumerator GetEnumerator() => new Enumerator( this );
        IEnumerator<sIndicator> IEnumerable<sIndicator>.GetEnumerator() => new Enumerator( this );
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator( this );

        private static bool ShouldClear( ClearMode mode )
        {
#if NETCOREAPP2_1
            return mode == ClearMode.Always
                || (mode == ClearMode.Auto && RuntimeHelpers.IsReferenceOrContainsReferences<sIndicator>());
#else
            return mode != ClearMode.Never;
#endif
        }

        /// <summary>
        /// Returns the internal buffers to the ArrayPool.
        /// </summary>
        public void Dispose()
        {
            ReturnArray();
            _size.SetValue( 0 );
        }

        #endregion

        // internal constant copied from Array.MaxArrayLength
        private const int MaxArrayLength                        = 0x7FEFFFFF;
        private const int DefaultCapacity                       = 200;
        

        private static readonly sIndicator[] s_emptySBarArray         = Array.Empty<sIndicator>();

        [NonSerialized]
        private ArrayPool<sIndicator> _sIndicatorPool;

        //private Type _indicatorType;
        private sIndicator[]   _sIndicatorValues; // Do not rename (binary serialization)
        private int            _capacity; // Do not rename (binary serialization)

        public struct Enumerator : IEnumerator<sIndicator>, IEnumerator
        {
            private readonly IndicatorValuesList _list;
            private int               _index;
            private sIndicator              _current;

            internal Enumerator( IndicatorValuesList list )
            {
                _list = list;
                _index = 0;
                _current = default!;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                var localList = _list;

                if ( ( ( uint ) _index < ( uint ) localList._size.Value ) )
                {
                    _current = localList._sIndicatorValues[ _index ];
                    _index++;
                    return true;
                }
                return MoveNextRare();
            }

            private bool MoveNextRare()
            {
                _index = _list._size.Value + 1;
                _current = default!;
                return false;
            }

            public sIndicator Current => _current;
#nullable enable
            object? IEnumerator.Current
            {
                get
                {
                    if ( _index == 0 || _index == _list._size.Value + 1 )
                    {
                        ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }
                    return Current;
                }
            }
#nullable disable

            void IEnumerator.Reset()
            {

                _index = 0;
                _current = default!;
            }
        }

        


        
    }






}
