using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Linq;

namespace fx.Charting.ATony
{
    internal sealed class ReadOnlyFifo< T > : IList< T >, ICollection< T >, IEnumerable< T >, IEnumerable
    {
        private readonly T[ ] gparam_0;
        private readonly int int_0;
        private readonly int int_1;

        internal ReadOnlyFifo( T[ ] gparam_1, int int_2, int int_3 )
        {
            gparam_0 = gparam_1;
            int_0 = int_2;
            int_1 = int_3;
        }

        internal ReadOnlyFifo( T[ ] gparam_1, int int_2 ) : this( gparam_1, 0, int_2 )
        {
        }

        internal ReadOnlyFifo( T[ ] gparam_1 ) : this( gparam_1, 0, gparam_1.Length )
        {
        }

        internal T[ ] Prop_0
        {
            get
            {
                return gparam_0;
            }
        }

        internal int Int32_0
        {
            get
            {
                return int_0;
            }
        }

        public int Count
        {
            get
            {
                return int_1;
            }
        }

        public int IndexOf( T item )
        {
            throw new NotImplementedException( );
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
            get
            {
                return Prop_0[ index + Int32_0 ];
            }
            set
            {
                throw new NotImplementedException( );
            }
        }

        public void Add( T item )
        {
            throw new NotSupportedException( );
        }

        public void Clear( )
        {
            throw new NotSupportedException( );
        }

        public bool Contains( T item )
        {
            throw new NotImplementedException( );
        }

        public void CopyTo( T[ ] array, int arrayIndex )
        {
            throw new NotImplementedException( );
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public bool Remove( T item )
        {
            throw new NotSupportedException( );
        }

        public IEnumerator< T > GetEnumerator( )
        {
            throw new NotImplementedException( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            throw new NotImplementedException( );
        }
    }
}

