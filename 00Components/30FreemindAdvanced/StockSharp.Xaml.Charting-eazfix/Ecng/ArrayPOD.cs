using System;
using System.Runtime.CompilerServices;

namespace MatterHackers.Agg
{
    internal class ArrayPOD<T> : IDataContainer<T>
    {
        private T[] m_array;
        private int m_size;

        public ArrayPOD()
          : this( 64 )
        {
        }

        public ArrayPOD( int size )
        {
            this.m_array = new T[ size ];
            this.m_size = size;
        }

        public void RemoveLast()
        {
            throw new NotImplementedException();
        }

        public ArrayPOD( ArrayPOD<T> v )
        {
            this.m_array = ( T[ ] ) v.m_array.Clone();
            this.m_size = v.m_size;
        }

        public void Resize( int size )
        {
            if ( size == this.m_size )
                return;
            this.m_array = new T[ size ];
        }

        public int Size()
        {
            return this.m_size;
        }
        
        public T this[ int index ]
        {
            get
            {
                return this.m_array[ index ];
            }
            set
            {
                this.m_array[ index ] = value;
            }
        }

        public T[ ] Array
        {
            get
            {
                return this.m_array;
            }
        }
    }
}
