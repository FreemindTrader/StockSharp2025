// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VectorPOD`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class VectorPOD<dataType> : IDataContainer<dataType>
    {
        private dataType[] internalArray = new dataType[0];
        protected int currentSize;
        private static dataType zeroed_object;

        public int AllocatedSize
        {
            get
            {
                return this.internalArray.Length;
            }
        }

        public VectorPOD()
        {
        }

        public VectorPOD( int cap )
          : this( cap, 0 )
        {
        }

        public VectorPOD( int capacity, int extraTail )
        {
            this.Allocate( capacity, extraTail );
        }

        public virtual void Remove( int indexToRemove )
        {
            if ( indexToRemove >= this.Length )
                throw new Exception( "requested remove past end of array" );
            for ( int index = indexToRemove ; index < this.Length - 1 ; ++index )
                this.internalArray[ index ] = this.internalArray[ index + 1 ];
            --this.currentSize;
        }

        public virtual void RemoveLast()
        {
            if ( this.currentSize == 0 )
                return;
            --this.currentSize;
        }

        public VectorPOD( VectorPOD<dataType> vectorToCopy )
        {
            this.currentSize = vectorToCopy.currentSize;
            this.internalArray = ( dataType[ ] ) vectorToCopy.internalArray.Clone();
        }

        public void CopyFrom( VectorPOD<dataType> vetorToCopy )
        {
            this.Allocate( vetorToCopy.currentSize );
            if ( vetorToCopy.currentSize == 0 )
                return;
            vetorToCopy.internalArray.CopyTo( ( System.Array ) this.internalArray, 0 );
        }

        public void Capacity( int newCapacity )
        {
            this.Capacity( newCapacity, 0 );
        }

        public void Capacity( int newCapacity, int extraTail )
        {
            this.currentSize = 0;
            if ( newCapacity <= this.AllocatedSize )
                return;
            this.internalArray = ( dataType[ ] ) null;
            int length = newCapacity + extraTail;
            if ( length == 0 )
                return;
            this.internalArray = new dataType[ length ];
        }

        public int Capacity()
        {
            return this.AllocatedSize;
        }

        public void Allocate( int size )
        {
            this.Allocate( size, 0 );
        }

        public void Allocate( int size, int extraTail )
        {
            this.Capacity( size, extraTail );
            this.currentSize = size;
        }

        public void Resize( int newSize )
        {
            if ( newSize <= this.currentSize || newSize <= this.AllocatedSize )
                return;
            dataType[] dataTypeArray = new dataType[newSize];
            if ( this.internalArray != null )
            {
                for ( int index = 0 ; index < this.internalArray.Length ; ++index )
                    dataTypeArray[ index ] = this.internalArray[ index ];
            }
            this.internalArray = dataTypeArray;
        }

        public void zero()
        {
            int length = this.internalArray.Length;
            for ( int index = 0 ; index < length ; ++index )
                this.internalArray[ index ] = VectorPOD<dataType>.zeroed_object;
        }

        public virtual void add( dataType v )
        {
            if ( this.internalArray == null || this.internalArray.Length < this.currentSize + 1 )
                this.Resize( this.currentSize + this.currentSize / 2 + 16 );
            this.internalArray[ this.currentSize++ ] = v;
        }

        public void push_back( dataType v )
        {
            this.internalArray[ this.currentSize++ ] = v;
        }

        public void insert_at( int pos, dataType val )
        {
            if ( pos >= this.currentSize )
            {
                this.internalArray[ this.currentSize ] = val;
            }
            else
            {
                for ( int index = 0 ; index < this.currentSize - pos ; ++index )
                    this.internalArray[ index + pos + 1 ] = this.internalArray[ index + pos ];
                this.internalArray[ pos ] = val;
            }
            ++this.currentSize;
        }

        public void inc_size( int size )
        {
            this.currentSize += size;
        }

        public int size()
        {
            return this.currentSize;
        }

        public dataType this[ int i ]
        {
            get
            {
                return this.internalArray[ i ];
            }
        }

        public dataType[ ] Array
        {
            get
            {
                return this.internalArray;
            }
        }

        public dataType at( int i )
        {
            return this.internalArray[ i ];
        }

        public dataType value_at( int i )
        {
            return this.internalArray[ i ];
        }

        public dataType[ ] data()
        {
            return this.internalArray;
        }

        public void remove_all()
        {
            this.currentSize = 0;
        }

        public void clear()
        {
            this.currentSize = 0;
        }

        public void cut_at( int num )
        {
            if ( num >= this.currentSize )
                return;
            this.currentSize = num;
        }

        public int Length
        {
            get
            {
                return this.currentSize;
            }
        }
    }
}
