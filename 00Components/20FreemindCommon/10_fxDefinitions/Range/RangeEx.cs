using fx.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace fx.Definitions
{
    /// <summary>
    /// Represents a range of items.
    /// </summary>
    /// <typeparam name="T">The range type.</typeparam>
    public class RangeEx<T> : IComparable<RangeEx<T>>, IComparable<T>, IComparable where T : IComparable<T>
    {

        #region Declarations
        private readonly T _lowerBound;
        private readonly T _upperBound;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the range.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range.</param>
        /// <param name="upperBound">The upper bound of the range.</param>
        public RangeEx( T lowerBound, T upperBound )
        {
            Assert.NotNull( "lowerBound", lowerBound );
            Assert.NotNull( "upperBound", upperBound );
            Assert.IsTrue( "lowerBound", lowerBound.CompareTo( upperBound ) <= 0 );

            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The start of the range.
        /// </summary>
        public T LowerBound
        {
            get
            {
                return _lowerBound;
            }
        }

        /// <summary>
        /// The upper bound of the range.
        /// </summary>
        public T UpperBound
        {
            get
            {
                return _upperBound;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Indicates if the range contains <code>value</code>.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>true if the range contains <code>value</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        public bool Contains( T value )
        {
            Assert.NotNull( "value", value );

            return ( ( LowerBound.CompareTo( value ) <= 0 ) && ( UpperBound.CompareTo( value ) >= 0 ) );
        }

        /// <summary>
        /// Indicates if the range contains <code>value</code>.
        /// </summary>
        /// <param name="value">A range to test.</param>
        /// <returns>true if the entire range in <code>value</code> is within this range.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        public bool Contains( RangeEx<T> value )
        {
            Assert.NotNull( "value", value );

            int i1 = LowerBound.CompareTo(value.LowerBound);
            int i2 = UpperBound.CompareTo(value.UpperBound);
            return ( ( LowerBound.CompareTo( value.LowerBound ) <= 0 ) && ( UpperBound.CompareTo( value.UpperBound ) >= 0 ) );
        }

        /// <summary>
        /// Indicates if the range is contained by <code>value</code>.
        /// </summary>
        /// <param name="value">A range to test.</param>
        /// <returns>true if the entire range is within <code>value</code>.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        public bool IsContainedBy( RangeEx<T> value )
        {
            Assert.NotNull( "value", value );

            return value.Contains( this );
        }

        /// <summary>
        /// Indicates if the range overlaps <code>value</code>.
        /// </summary>
        /// <param name="value">A range to test.</param>
        /// <returns>true if any of the range in <code>value</code> is within this range.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        public bool Overlaps( RangeEx<T> value )
        {
            Assert.NotNull( "value", value );

            return ( Contains( value.LowerBound ) || Contains( value.UpperBound ) || value.Contains( LowerBound ) || value.Contains( UpperBound ) );
        }

        /// <summary>
        /// Returns the range that represents the intersection of this range and <code>value</code>.
        /// </summary>
        /// <param name="value">The range to intersect with.</param>
        /// <returns>A range that contains the values that are common in both ranges, or null if there is no intersection.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        /// <exception cref="ArgumentException"><code>value</code> does not overlap the range.</exception>
        public RangeEx<T> Intersect( RangeEx<T> value )
        {
            Assert.NotNull( "value", value );
            Assert.IsTrue( "value", Overlaps( value ) );    // Intersect makes no sense unless there is an overlap

            T start;
            if ( LowerBound.CompareTo( value.LowerBound ) > 0 )
            {
                start = LowerBound;
            }
            else
            {
                start = value.LowerBound;
            }

            if ( UpperBound.CompareTo( value.UpperBound ) < 0 )
            {
                return new RangeEx<T>( start, UpperBound );
            }
            else
            {
                return new RangeEx<T>( start, value.UpperBound );
            }
        }

        /// <summary>
        /// Returns the range that represents the union of this range and <code>value</code>.
        /// </summary>
        /// <param name="value">The range to union with.</param>
        /// <returns>A range that contains both ranges, or null if there is no union.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        /// <exception cref="ArgumentException"><code>value</code> is not contiguous with the range.</exception>
        public RangeEx<T> Union( RangeEx<T> value )
        {
            Assert.NotNull( "value", value );
            Assert.IsTrue( "value", IsContiguousWith( value ) );    // Union makes no sense unless there is a contiguous border

            // If either one is a subset of the other, then is it the union
            if ( Contains( value ) )
            {
                return this;
            }
            else if ( value.Contains( this ) )
            {
                return value;
            }
            else
            {
                T start;
                if ( LowerBound.CompareTo( value.LowerBound ) < 0 )
                {
                    start = LowerBound;
                }
                else
                {
                    start = value.LowerBound;
                }

                if ( UpperBound.CompareTo( value.UpperBound ) > 0 )
                {
                    return new RangeEx<T>( start, UpperBound );
                }
                else
                {
                    return new RangeEx<T>( start, value.UpperBound );
                }
            }
        }

        /// <summary>
        /// Returns a range which contains the current range, minus <code>value</code>.
        /// </summary>
        /// <param name="value">The value to complement the range by.</param>
        /// <returns>The complemented range.</returns>
        /// <exception cref="ArgumentNullException"><code>value</code> is null.</exception>
        /// <exception cref="ArgumentException">
        /// <code>value</code> is contained by this range, complementing would lead to a split range.
        /// </exception>
        public RangeEx<T> Complement( RangeEx<T> value )
        {
            Assert.NotNull( "value", value );
            Assert.IsFalse( "value", Contains( value ) );

            if ( Overlaps( value ) )
            {
                T start;

                // If value's start and end straddle our start, move our start up to be values end.
                if ( ( LowerBound.CompareTo( value.LowerBound ) > 0 ) && ( LowerBound.CompareTo( value.UpperBound ) < 0 ) )
                {
                    start = value.UpperBound;
                }
                else
                {
                    start = LowerBound;
                }

                // If value's start and end straddle our end, move our end back down to be values start.
                if ( ( UpperBound.CompareTo( value.LowerBound ) > 0 ) && ( UpperBound.CompareTo( value.UpperBound ) < 0 ) )
                {
                    return new RangeEx<T>( start, value.LowerBound );
                }
                else
                {
                    return new RangeEx<T>( start, UpperBound );
                }
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Splits the range into two.
        /// </summary>
        /// <param name="position">The position to split the range at.</param>
        /// <returns>The split ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>position</code> is null.</exception>
        /// <exception cref="ArgumentException"><code>position</code> is not contained within the range.</exception>
        public IEnumerable<RangeEx<T>> Split( T position )
        {
            Assert.NotNull( "position", position );
            Assert.IsTrue( "position", Contains( position ) );

            if ( ( LowerBound.CompareTo( position ) == 0 ) || ( UpperBound.CompareTo( position ) == 0 ) )
            {
                // The position is at a boundary, so a split does not happen
                yield return this;
            }
            else
            {
                yield return Range.Create( LowerBound, position );
                yield return Range.Create( position, UpperBound );
            }
        }
        /// <summary>
        /// Iterates the range.
        /// </summary>
        /// <param name="incrementor">A function which takes a value, and returns the next value.</param>
        /// <returns>The items in the range.</returns>
        public IEnumerable<T> Iterate( Func<T, T> incrementor )
        {
            yield return LowerBound;
            T item = incrementor(LowerBound);
            while ( UpperBound.CompareTo( item ) >= 0 )
            {
                yield return item;
                item = incrementor( item );
            }
        }

        /// <summary>
        /// Iterates the range in reverse.
        /// </summary>
        /// <param name="decrementor">A function which takes a value, and returns the previous value.</param>
        /// <returns>The items in the range.</returns>
        public IEnumerable<T> ReverseIterate( Func<T, T> decrementor )
        {
            yield return UpperBound;
            T item = decrementor(UpperBound);
            while ( CompareTo( item ) <= 0 )
            {
                yield return item;
                item = decrementor( item );
            }
        }

        /// <summary>
        /// Indicates if this range is contiguous with <code>range</code>.
        /// </summary>
        /// <param name="range">The range to check.</param>
        /// <returns>true if the two ranges are contiguous, false otherwise.</returns>
        
        public bool IsContiguousWith( RangeEx<T> range )
        {
            if ( Overlaps( range ) || range.Overlaps( this ) || range.Contains( this ) || Contains( range ) )
            {
                return true;
            }

            // Once we remove overlapping and containing, only touching if available
            return ( ( UpperBound.Equals( range.LowerBound ) ) || ( LowerBound.Equals( range.UpperBound ) ) );
        }
        #endregion

        #region Overrides
        /// <summary>
        /// See <see cref="System.Object.ToString"/>.
        /// </summary>
        public override string ToString( )
        {
            return "{" + LowerBound + "->" + UpperBound + "}";
        }

        /// <summary>
        /// See <see cref="System.Object.Equals"/>.
        /// </summary>
        public override bool Equals( object obj )
        {
            if ( obj is RangeEx<T> )
            {
                RangeEx<T> other = (RangeEx<T>)obj;
                return ( ( CompareTo( other ) == 0 ) && ( UpperBound.CompareTo( other.UpperBound ) == 0 ) );
            }

            return false;
        }

        /// <summary>
        /// See <see cref="System.Object.GetHashCode"/>.
        /// </summary>
        public override int GetHashCode( )
        {
            return LowerBound.GetHashCode( );
        }
        #endregion

        #region Operators
        /// <summary>
        /// Overrides the equals operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the two ranges are equal, false otherwise.</returns>
        public static bool operator ==( RangeEx<T> left, RangeEx<T> right )
        {
            // If both are null, or both are same instance, return true.
            if ( ReferenceEquals( left, right ) )
            {
                return true;
            }

            // If one is null, but not both, return false.
            if ( ( ( object ) left == null ) || ( ( object ) right == null ) )
            {
                return false;
            }

            return ( left.CompareTo( right ) == 0 );
        }

        /// <summary>
        /// Overrides the not equals operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the two ranges are equal, false otherwise.</returns>
        /// <summary>
        /// Overrides the equals operator.
        /// </summary>
        /// <returns>true if the two ranges are equal, false otherwise.</returns>
        public static bool operator !=( RangeEx<T> left, RangeEx<T> right )
        {
            return !( left == right );
        }

        /// <summary>
        /// Overrides the greater than operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is greater than <code>right</code>, false otherwise.</returns>
        public static bool operator >( RangeEx<T> left, RangeEx<T> right )
        {
            return ( left.CompareTo( right ) > 0 );
        }

        /// <summary>
        /// Overrides the less than operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is less than <code>right</code>, false otherwise.</returns>
        public static bool operator <( RangeEx<T> left, RangeEx<T> right )
        {
            return ( left.CompareTo( right ) < 0 );
        }

        /// <summary>
        /// Overrides the greater than operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is greater than <code>right</code>, false otherwise.</returns>
        public static bool operator >( RangeEx<T> left, T right )
        {
            return ( left.CompareTo( right ) > 0 );
        }

        /// <summary>
        /// Overrides the less than operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is less than <code>right</code>, false otherwise.</returns>
        public static bool operator <( RangeEx<T> left, T right )
        {
            return ( left.CompareTo( right ) < 0 );
        }

        /// <summary>
        /// Overrides the greater than or equal operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is greater than or equal to <code>right</code>, false otherwise.</returns>
        public static bool operator >=( RangeEx<T> left, RangeEx<T> right )
        {
            return ( left.CompareTo( right ) >= 0 );
        }

        /// <summary>
        /// Overrides the less than or equal to operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is less than or equal to <code>right</code>, false otherwise.</returns>
        public static bool operator <=( RangeEx<T> left, RangeEx<T> right )
        {
            return ( left.CompareTo( right ) <= 0 );
        }

        /// <summary>
        /// Overrides the greater than or equals operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is greater than or equal to <code>right</code>, false otherwise.</returns>
        public static bool operator >=( RangeEx<T> left, T right )
        {
            return ( left.CompareTo( right ) >= 0 );
        }

        /// <summary>
        /// Overrides the less than or equals operator.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>true if the <code>left</code> is less than or equal to <code>right</code>, false otherwise.</returns>
        public static bool operator <=( RangeEx<T> left, T right )
        {
            return ( left.CompareTo( right ) <= 0 );
        }

        /// <summary>
        /// The complement operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The complement of <code>left</code> and <code>right</code>.</returns>
        public static RangeEx<T> operator ^( RangeEx<T> left, RangeEx<T> right )
        {
            return left.Complement( right );
        }

        /// <summary>
        /// The union operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The union of <code>left</code> and <code>right</code>.</returns>
        public static RangeEx<T> operator |( RangeEx<T> left, RangeEx<T> right )
        {
            return left.Union( right );
        }

        /// <summary>
        /// The intersection operator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The intersection of <code>left</code> and <code>right</code>.</returns>
        public static RangeEx<T> operator &( RangeEx<T> left, RangeEx<T> right )
        {
            return left.Intersect( right );
        }
        #endregion

        #region IComparable<Range<T>> Members
        /// <summary>
        /// See <see cref="IComparable{T}.CompareTo"/>.
        /// </summary>
        public int CompareTo( RangeEx<T> other )
        {
            return LowerBound.CompareTo( other.LowerBound );
        }
        #endregion

        #region IComparable<T> Members
        /// <summary>
        /// See <see cref="IComparable{T}.CompareTo"/>.
        /// </summary>
        public int CompareTo( T other )
        {
            return LowerBound.CompareTo( other );
        }
        #endregion

        #region IComparable Members
        /// <summary>
        /// See <see cref="IComparable.CompareTo"/>.
        /// </summary>
        public int CompareTo( object obj )
        {
            if ( obj is RangeEx<T> )
            {
                RangeEx<T> other = (RangeEx<T>)obj;
                return CompareTo( other );
            }
            else if ( obj is T )
            {
                T other = (T)obj;
                return CompareTo( other );
            }

            throw new InvalidOperationException( string.Format( "Cannot compare to {0}", obj ) );
        }
        #endregion

    }

    /// <summary>
    /// Represents a range of items, with an associated value.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public sealed class Range<TKey, TValue> : RangeEx<TKey> where TKey : IComparable<TKey>
    {

        #region Declarations
        private readonly TValue _value;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the range.
        /// </summary>
        /// <param name="lowerBound">The lower bound of the range.</param>
        /// <param name="upperBound">The upper bound of the range.</param>
        /// <param name="value">The value.</param>
        internal Range( TKey lowerBound, TKey upperBound, TValue value )
            : base( lowerBound, upperBound )
        {
            _value = value;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The value for the range.
        /// </summary>
        public TValue Value
        {
            get
            {
                return _value;
            }
        }
        #endregion

    }

    /// <summary>
    /// Represents a range of array items, with an associated value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public sealed class RangeArray<T> : RangeEx<int>
    {

        #region Declarations
        private readonly T[] _values;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the range.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="values">The values.</param>
        internal RangeArray( int startIndex, T [ ] values )
            : base( startIndex, startIndex + values.Length - 1 )
        {
            _values = values;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The values for the range.
        /// </summary>
        public T [ ] Values
        {
            get
            {
                return _values;
            }
        }
        #endregion

    }

    /// <summary>
    /// Contains a set of range methods.
    /// </summary>
    public static class Range
    {

        #region Creation
        /// <summary>
        /// Creates a range array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="startIndex">The start index for the array.</param>
        /// <param name="values">The array values.</param>
        /// <returns>The range array.</returns>
        /// <exception cref="ArgumentNullException"><code>values</code> is null.</exception>
        /// <exception cref="ArgumentException">
        /// <code>values</code> is empty, or <code>startIndex</code> is less than zero.
        /// </exception>
        public static RangeArray<T> Create<T>( int startIndex, T [ ] values )
        {
            Assert.IsTrue( "startIndex", startIndex >= 0 );
            Assert.NotNull( "values", values );
            Assert.NotEmpty( "values", values );

            return new RangeArray<T>( startIndex, values );
        }

        /// <summary>
        /// Creates a range array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="range">The range for the array.</param>
        /// <param name="values">The array values.</param>
        /// <returns>The range array.</returns>
        /// <exception cref="ArgumentNullException"><code>range</code> or <code>values</code> is null.</exception>
        /// <exception cref="ArgumentException">
        /// <code>values</code> is empty, or the range does not match the values.Length.
        /// </exception>
        public static RangeArray<T> Create<T>( RangeEx<int> range, T [ ] values )
        {
            Assert.NotNull( "range", range );
            Assert.NotNull( "values", values );
            Assert.NotEmpty( "values", values );
            Assert.IsTrue( "range", ( values.Length == ( range.UpperBound - range.LowerBound ) ) );

            return new RangeArray<T>( range.LowerBound, values );
        }

        /// <summary>
        /// Creates an inclusive range.
        /// </summary>
        /// <typeparam name="T">The type of range.</typeparam>
        /// <param name="from">The from value.</param>
        /// <param name="to">The to value.</param>
        /// <returns>The range.</returns>
        /// <exception cref="ArgumentNullException"><code>from</code> or <code>to</code> is null.</exception>
        public static RangeEx<T> Create<T>( T from, T to ) where T : IComparable<T>
        {
            Assert.NotNull( "from", from );
            Assert.NotNull( "to", to );

            return new RangeEx<T>( from, to );
        }

        /// <summary>
        /// Creates an inclusive range.
        /// </summary>
        /// <typeparam name="TKey">The type of range.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="from">The from value.</param>
        /// <param name="to">The to value.</param>
        /// <param name="value">The value.</param>
        /// <returns>The range.</returns>
        /// <exception cref="ArgumentNullException"><code>from</code> or <code>to</code> is null.</exception>
        public static Range<TKey, TValue> Create<TKey, TValue>( TKey from, TKey to, TValue value ) where TKey : IComparable<TKey>
        {
            Assert.NotNull( "from", from );
            Assert.NotNull( "to", to );

            return new Range<TKey, TValue>( from, to, value );
        }
        #endregion

        #region Enumerables
        /// <summary>
        /// Makes a Range{TKey, TValue} enumerator covariant with Range{TKey}
        /// </summary>
        /// <typeparam name="TKey">The range type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="ranges">The ranges to make covariant.</param>
        
        internal static IEnumerable<RangeEx<TKey>> MakeCovariant<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges ) where TKey : IComparable<TKey>
        {
            Assert.NotNull( "ranges", ranges );

            foreach ( Range<TKey, TValue> item in ranges )
            {
                yield return item;
            }
        }

        /// <summary>
        /// Makes a RangeArray{T} enumerator covariant with Range{int}
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="ranges">The ranges to make covariant.</param>
        
        private static IEnumerable<RangeEx<int>> MakeCovariant<T>( this IEnumerable<RangeArray<T>> ranges )
        {
            Assert.NotNull( "ranges", ranges );

            foreach ( RangeArray<T> item in ranges )
            {
                yield return item;
            }
        }

        /// <summary>
        /// Indicates if the ranges contain the range.
        /// </summary>
        /// <typeparam name="T">The range type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="from">The start to look for.</param>
        /// <param name="to">The end to look for.</param>
        /// <returns>true if <code>ranges</code> contain the range, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>from</code> is null, or <code>to</code> is null.</exception>
        /// <exception cref="ArgumentException"><code>from</code> is greater than <code>to</code>.</exception>
        public static bool Contains<T>( this IEnumerable<RangeEx<T>> ranges, T from, T to ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );
            Assert.NotNull( "from", from );
            Assert.NotNull( "to", to );
            Assert.IsTrue( "from", from.CompareTo( to ) <= 0 );

            return ranges.Contains( Create( from, to ) );
        }

        /// <summary>
        /// Indicates if the ranges contain the range.
        /// </summary>
        /// <typeparam name="TKey">The range key type.</typeparam>
        /// <typeparam name="TValue">THe range value type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="from">The start to look for.</param>
        /// <param name="to">The end to look for.</param>
        /// <returns>true if <code>ranges</code> contain the range, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>from</code> is null, or <code>to</code> is null.</exception>
        /// <exception cref="ArgumentException"><code>from</code> is greater than <code>to</code>.</exception>
        public static bool Contains<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges, TKey from, TKey to ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Contains( from, to );
        }

        /// <summary>
        /// Indicates if the ranges contain the range.
        /// </summary>
        /// <typeparam name="T">The range array type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="from">The start to look for.</param>
        /// <param name="to">The end to look for.</param>
        /// <returns>true if <code>ranges</code> contain the range, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>from</code> is null, or <code>to</code> is null.</exception>
        /// <exception cref="ArgumentException"><code>from</code> is greater than <code>to</code>.</exception>
        public static bool Contains<T>( this IEnumerable<RangeArray<T>> ranges, int from, int to )
        {
            return ranges.MakeCovariant( ).Contains( from, to );
        }

        /// <summary>
        /// Indicates if the ranges contain the range.
        /// </summary>
        /// <typeparam name="T">The range type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="range">The item to look for.</param>
        /// <returns>true if <code>ranges</code> contain <code>range</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>range</code> is null.</exception>
        public static bool Contains<T>( this IEnumerable<RangeEx<T>> ranges, RangeEx<T> range ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );
            Assert.NotNull( "item", range );

            foreach ( RangeEx<T> item in ranges.Overlapped( range ).Coalesce( ) )
            {
                if ( item.Contains( range ) )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Indicates if the ranges contain the range.
        /// </summary>
        /// <typeparam name="TKey">The range key type.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="range">The item to look for.</param>
        /// <returns>true if <code>ranges</code> contain <code>range</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>range</code> is null.</exception>
        public static bool Contains<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges, RangeEx<TKey> range ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Contains( range );
        }

        /// <summary>
        /// Indicates if the range arrays contain the range.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="range">The item to look for.</param>
        /// <returns>true if <code>ranges</code> contain <code>range</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>range</code> is null.</exception>
        public static bool Contains<T>( this IEnumerable<RangeArray<T>> ranges, RangeEx<int> range )
        {
            return ranges.MakeCovariant( ).Contains( range );
        }

        /// <summary>
        /// Indicates if the ranges contain the item.
        /// </summary>
        /// <typeparam name="T">The range type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="item">The item to look for.</param>
        /// <returns>true if <code>ranges</code> contain <code>item</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>item</code> is null.</exception>
        public static bool Contains<T>( this IEnumerable<RangeEx<T>> ranges, T item ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );
            Assert.NotNull( "item", item );

            foreach ( RangeEx<T> range in ranges )
            {
                if ( range.Contains( item ) )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Indicates if the ranges contain the item.
        /// </summary>
        /// <typeparam name="TKey">The range key type.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="item">The item to look for.</param>
        /// <returns>true if <code>ranges</code> contain <code>item</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>item</code> is null.</exception>
        public static bool Contains<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges, TKey item ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Contains( item );
        }

        /// <summary>
        /// Indicates if the range arrays contain the item.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The ranges to look in.</param>
        /// <param name="item">The item to look for.</param>
        /// <returns>true if <code>ranges</code> contain <code>item</code>, false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>item</code> is null.</exception>
        public static bool Contains<T>( this IEnumerable<RangeArray<T>> ranges, int item )
        {
            return ranges.MakeCovariant( ).Contains( item );
        }

        /// <summary>
        /// Sorts the ranges.
        /// </summary>
        /// <typeparam name="T">The type of range.</typeparam>
        /// <param name="ranges">The sorted ranges.</param>
        /// <returns>The sorted ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> is null.</exception>
        public static IEnumerable<RangeEx<T>> Sort<T>( this IEnumerable<RangeEx<T>> ranges ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );

            PooledList<RangeEx<T>> list = new PooledList<RangeEx<T>>(ranges);
            list.Sort( );
            return list;
        }

        /// <summary>
        /// Sorts the ranges.
        /// </summary>
        /// <typeparam name="TKey">The range key type.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The sorted ranges.</param>
        /// <returns>The sorted ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> is null.</exception>
        public static IEnumerable<RangeEx<TKey>> Sort<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Sort( );
        }

        /// <summary>
        /// Sorts the range arrays.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The sorted ranges.</param>
        /// <returns>The sorted ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> is null.</exception>
        public static IEnumerable<RangeEx<int>> Sort<T>( this IEnumerable<RangeArray<T>> ranges )
        {
            return ranges.MakeCovariant( ).Sort( );
        }

        /// <summary>
        /// Coaleses the ranges.
        /// </summary>
        /// <typeparam name="T">The range key type.</typeparam>
        /// <param name="ranges">The ranges to coalesce.</param>
        /// <returns>The coalesced ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> is null.</exception>
        public static IEnumerable<RangeEx<T>> Coalesce<T>( this IEnumerable<RangeEx<T>> ranges ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );

            RangeEx<T> previous = null;
            foreach ( RangeEx<T> item in ranges.Sort( ) )
            {
                if ( previous == null )
                {
                    previous = item;
                }
                else
                {
                    // Possible coalescing
                    if ( previous.IsContiguousWith( item ) )
                    {
                        // Intersect the ranges
                        previous = previous | item;
                    }
                    else
                    {
                        yield return previous;
                        previous = item;
                    }
                }
            }

            if ( previous != null )
            {
                yield return previous;
            }
        }

        /// <summary>
        /// Coaleses the ranges.
        /// </summary>
        /// <typeparam name="TKey">The range key type.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The ranges to coalesce.</param>
        /// <returns>The coalesced ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> is null.</exception>
        public static IEnumerable<RangeEx<TKey>> Coalesce<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Coalesce( );
        }

        /// <summary>
        /// Coaleses the range arrays.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The ranges to coalesce.</param>
        /// <returns>The coalesced ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> is null.</exception>
        public static IEnumerable<RangeEx<int>> Coalesce<T>( this IEnumerable<RangeArray<T>> ranges )
        {
            return ranges.MakeCovariant( ).Coalesce( );
        }

        /// <summary>
        /// Fetches the ranges which are overlapped by this range.
        /// </summary>
        /// <typeparam name="T">The type of range.</typeparam>
        /// <param name="ranges">The ranges.</param>
        /// <param name="range">The range to test for overlappping.</param>
        /// <returns>The overlapped ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>range</code> is null.</exception>
        public static IEnumerable<RangeEx<T>> Overlapped<T>( this IEnumerable<RangeEx<T>> ranges, RangeEx<T> range ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );
            Assert.NotNull( "range", range );

            foreach ( RangeEx<T> item in ranges )
            {
                if ( item.Overlaps( range ) )
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Fetches the ranges which are overlapped by this range.
        /// </summary>
        /// <typeparam name="TKey">The type of range.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The ranges.</param>
        /// <param name="range">The range to test for overlappping.</param>
        /// <returns>The overlapped ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>range</code> is null.</exception>
        public static IEnumerable<RangeEx<TKey>> Overlapped<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges, RangeEx<TKey> range ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Overlapped( range );
        }

        /// <summary>
        /// Fetches the range arrays which are overlapped by this range.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The ranges.</param>
        /// <param name="range">The range to test for overlappping.</param>
        /// <returns>The overlapped ranges.</returns>
        /// <exception cref="ArgumentNullException"><code>ranges</code> or <code>range</code> is null.</exception>
        public static IEnumerable<RangeEx<int>> Overlapped<T>( this IEnumerable<RangeArray<T>> ranges, RangeEx<int> range )
        {
            return ranges.MakeCovariant( ).Overlapped( range );
        }

        /// <summary>
        /// Searches a set of ranges, and returns the matching items.
        /// </summary>
        /// <typeparam name="T">The type of range.</typeparam>
        /// <param name="ranges">The ranges to search.</param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The matching ranges.</returns>
        public static IEnumerable<RangeEx<T>> Find<T>( this IEnumerable<RangeEx<T>> ranges, Func<RangeEx<T>, bool> predicate ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );
            Assert.NotNull( "predicate", predicate );

            foreach ( RangeEx<T> range in ranges )
            {
                if ( predicate( range ) )
                {
                    yield return range;
                }
            }
        }

        /// <summary>
        /// Searches a set of ranges, and returns the matching items.
        /// </summary>
        /// <typeparam name="TKey">The type of range.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The ranges to search.</param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The matching ranges.</returns>
        public static IEnumerable<RangeEx<TKey>> Find<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges, Func<RangeEx<TKey>, bool> predicate ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).Find( predicate );
        }

        /// <summary>
        /// Searches a set of range arrays, and returns the matching items.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The ranges to search.</param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The matching ranges.</returns>
        public static IEnumerable<RangeEx<int>> Find<T>( this IEnumerable<RangeArray<T>> ranges, Func<RangeEx<int>, bool> predicate )
        {
            return ranges.MakeCovariant( ).Find( predicate );
        }

        /// <summary>
        /// Searches a range, and returns the first matching item.
        /// </summary>
        /// <typeparam name="T">The type of range.</typeparam>
        /// <param name="ranges">The ranges to search.</param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The first matching range, or null if no ranges match the predicate.</returns>
        public static RangeEx<T> FindFirst<T>( this IEnumerable<RangeEx<T>> ranges, Func<RangeEx<T>, bool> predicate ) where T : IComparable<T>
        {
            Assert.NotNull( "ranges", ranges );
            Assert.NotNull( "predicate", predicate );

            foreach ( RangeEx<T> range in ranges.Find( predicate ) )
            {
                return range;
            }

            return null;
        }

        /// <summary>
        /// Searches a set of ranges, and returns the first matching items.
        /// </summary>
        /// <typeparam name="TKey">The type of range.</typeparam>
        /// <typeparam name="TValue">The range value type.</typeparam>
        /// <param name="ranges">The ranges to search.</param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The first matching range, or null.</returns>
        public static Range<TKey, TValue> FindFirst<TKey, TValue>( this IEnumerable<Range<TKey, TValue>> ranges, Func<RangeEx<TKey>, bool> predicate ) where TKey : IComparable<TKey>
        {
            return ranges.MakeCovariant( ).FindFirst( predicate ) as Range<TKey, TValue>;
        }

        /// <summary>
        /// Searches a set of range arrays, and returns the first matching items.
        /// </summary>
        /// <typeparam name="T">The range value type.</typeparam>
        /// <param name="ranges">The ranges to search.</param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The first matching range, or null.</returns>
        public static RangeArray<T> FindFirst<T>( this IEnumerable<RangeArray<T>> ranges, Func<RangeEx<int>, bool> predicate )
        {
            return ranges.MakeCovariant( ).FindFirst( predicate ) as RangeArray<T>;
        }
        #endregion

    }
}