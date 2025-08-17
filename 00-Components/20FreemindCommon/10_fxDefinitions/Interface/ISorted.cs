using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    /// <summary>
    /// Represents a generic interface of an ordered collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface ISortedCollection<T> : ICollection<T>
    {
        /// <summary>
        /// Gets the comparer used to order items in the collection.
        /// </summary>
        IComparer<T> Comparer
        {
            get;
        }

        /// <summary>
        /// Gets indication of whether the collection allows duplicate values.
        /// </summary>
        bool AllowDuplicates
        {
            get;
        }

        IEnumerable<T> GetElementsLeftOf( T value );
        IEnumerable<T> GetElementsRightOf( T value );

        /// <summary>
        /// Get all items equal to or greater than the specified value, starting with the lowest index and moving forwards.
        /// </summary>
        IEnumerable<T> WhereGreaterOrEqual( T value );



        /// <summary>
        /// Get all items less than or equal to the specified value, starting with the highest index and moving backwards.
        /// </summary>
        IEnumerable<T> WhereLessOrEqualBackwards( T value );

        /// <summary>
        /// Gets the index of the first item greater than the specified value.
        /// /// </summary>
        int FirstIndexWhereGreaterThan( T value );

        /// <summary>
        /// Gets the index of the last item less than the specified key.
        /// </summary>
        int LastIndexWhereLessThan( T value );

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        T At( int index );

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        void RemoveAt( int index );

        /// <summary>
        /// Get all items starting at the index, and moving forward.
        /// </summary>
        IEnumerable<T> ForwardFromIndex( int index );

        /// <summary>
        /// Get all items starting at the index, and moving backward.
        /// </summary>
        IEnumerable<T> BackwardFromIndex( int index );
    }

    /// <summary>
    /// Represents a generic interface of ordered key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface ISortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IEnumerable<KeyValuePair<TKey, TValue>> GetElementsLeftOf( TKey value );
        IEnumerable<KeyValuePair<TKey, TValue>> GetElementsRightOf( TKey value );

        TValue GetLeftElement( TKey value );
        TValue GetRightElement( TKey value );
        /// <summary>
        /// Get all items having a key equal to or greater than the specified key, starting with the lowest index and moving forwards.
        /// </summary>
        IEnumerable<KeyValuePair<TKey, TValue>> WhereGreaterOrEqual( TKey key );

        /// <summary>
        /// Get all items less than or equal to the specified value, starting with the highest index and moving backwards.
        /// </summary>
        IEnumerable<KeyValuePair<TKey, TValue>> WhereLessOrEqualBackwards( TKey keyUpperBound );

        /// <summary>
        /// Gets the sorted collection of keys.
        /// </summary>
        new ISortedCollection<TKey> Keys
        {
            get;
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        KeyValuePair<TKey, TValue> At( int index );

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        void RemoveAt( int index );

        /// <summary>
        /// Sets the value at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void SetValueAt( int index, TValue value );

        /// <summary>
        /// Get all items starting at the index, and moving forward.
        /// </summary>
        IEnumerable<KeyValuePair<TKey, TValue>> ForwardFromIndex( int index );

        /// <summary>
        /// Get all items starting at the index, and moving backward.
        /// </summary>
        IEnumerable<KeyValuePair<TKey, TValue>> BackwardFromIndex( int index );
    }
}
