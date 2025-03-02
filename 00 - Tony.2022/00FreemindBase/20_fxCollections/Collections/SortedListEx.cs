using System.Collections.Generic;

namespace fx.Collections
{
    /// <summary>
    ///
    /// </summary>
    public class SortedListEx< TKey, TValue > : SortedList< TKey, TValue >
    {
        /// <summary>
        ///
        /// </summary>
        public bool RemoveFirstValue( TValue value )
        {
            int index = IndexOfValue( value );
            if( index < 0 )
            {
                return false;
            }

            RemoveAt( index );
            return true;
        }
    }
}