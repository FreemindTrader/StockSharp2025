// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.FifoBuffer`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    internal class FifoBuffer<T> : IUltraList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private int _startIndex = -1;
        private T[] _innerList;
        private readonly int _size;
        private int _usedSize;

        public int Size
        {
            get
            {
                return this._size;
            }
        }

        public int Count
        {
            get
            {
                return this._usedSize;
            }
            internal set
            {
                this._usedSize = value;
            }
        }

        public FifoBuffer( int size )
        {
            this._size = size;
            this._innerList = new T[ size ];
        }

        public T Add( T item )
        {
            this._innerList[ this.NextIndex() ] = item;
            this._usedSize = Math.Min( this._usedSize + 1, this._size );
            return item;
        }

        public T GetMaximum()
        {
            return ArrayOperations.Maximum<T>( this._innerList, 0, this._usedSize );
        }

        public T GetMinimum()
        {
            return ArrayOperations.Minimum<T>( this._innerList, 0, this._usedSize );
        }

        public void AddRange( IEnumerable<T> items )
        {
            int start = this.NextIndex();
            T[] innerList = this._innerList;
            int rem = this.Size - start;
            Array array1 = items as Array;
            if ( array1 != null )
            {
                this.ArrayAddRange( start, array1, array1.Length, rem, innerList );
            }
            else
            {
                IList<T> list = items as IList<T>;
                if ( list != null )
                {
                    T[] uncheckedList = list.ToUncheckedList<T>();
                    this.ArrayAddRange( start, ( Array ) uncheckedList, list.Count, rem, innerList );
                }
                else
                {
                    T[] array2 = items.ToArray<T>();
                    this.ArrayAddRange( start, ( Array ) array2, array2.Length, rem, innerList );
                }
            }
        }

        public void InsertRange( int index, IEnumerable<T> items )
        {
            throw new NotSupportedException( "Insert is not a supported operation on a Fifo Buffer" );
        }

        public void RemoveRange( int index, int count )
        {
            throw new NotSupportedException( "Remove is not a supported operation on a Fifo Buffer" );
        }

        private void ArrayAddRange( int start, Array array, int srcCount, int rem, T[ ] innerArray )
        {
            if ( srcCount > this.Size )
            {
                Array.Copy( array, srcCount - this.Size, ( Array ) this._innerList, 0, this.Size );
                this._startIndex = -1;
                this._usedSize = this.Size;
            }
            else if ( srcCount < rem )
            {
                Array.Copy( array, 0, ( Array ) innerArray, start, srcCount );
                this._usedSize = Math.Min( this._usedSize + srcCount, this._size );
                this._startIndex = start + srcCount - 1;
            }
            else
            {
                int length1 = rem;
                int length2 = srcCount - rem;
                Array.Copy( array, 0, ( Array ) this._innerList, start, length1 );
                Array.Copy( array, rem, ( Array ) this._innerList, 0, length2 );
                this._usedSize = Math.Min( this._usedSize + srcCount, this._size );
                this._startIndex = length2 - 1;
            }
        }

        public int IndexOf( T item )
        {
            int index = Array.IndexOf<T>(this._innerList, item, 0, this._size);
            if ( index == -1 )
                return -1;
            return this.ReverseResolveIndex( index );
        }

        public void Insert( int index, T item )
        {
            throw new NotSupportedException( "Insert is not a supported operation on a Fifo Buffer" );
        }

        public void RemoveAt( int index )
        {
            throw new NotSupportedException( "Remove is not a supported operation on a Fifo Buffer" );
        }

        T IList<T>.this[ int index ]
        {
            get
            {
                return this.GetItemAt( index );
            }
            set
            {
                this.SetItemAt( index, value );
            }
        }

        public T this[ int index ]
        {
            get
            {
                return this.GetItemAt( index );
            }
        }

        private int NextIndex()
        {
            if ( this._startIndex < 0 && this._usedSize != this.Size )
                return this._usedSize;
            this._startIndex = ( this._startIndex + 1 ) % this.Size;
            if ( this._startIndex > this._usedSize )
                this._startIndex = this._usedSize;
            return this._startIndex;
        }

        internal int ReverseResolveIndex( int index )
        {
            if ( this._startIndex < 0 )
                return index;
            return ( index - this._startIndex + this._usedSize - 1 ) % this._usedSize;
        }

        internal int ResolveIndex( int index )
        {
            if ( this._startIndex < 0 )
                return index;
            return ( this._startIndex + 1 + index ) % this._usedSize;
        }

        private void ValidateIndex( int index )
        {
            if ( index < 0 || index >= this._usedSize )
                throw new IndexOutOfRangeException();
        }

        void ICollection<T>.Add( T item )
        {
            this.Add( item );
        }

        public void Clear()
        {
            this._innerList = new T[ this._size ];
            this._startIndex = 0;
            this._usedSize = 0;
        }

        public bool Contains( T item )
        {
            return Array.IndexOf<T>( this._innerList, item, 0, this._size ) != -1;
        }

        public void CopyTo( T[ ] array, int arrayIndex )
        {
            int sourceIndex = this._startIndex + 1;
            int length1 = array.Length - sourceIndex;
            Array.Copy( ( Array ) this._innerList, sourceIndex, ( Array ) array, arrayIndex, length1 );
            int length2 = array.Length - length1;
            if ( length2 <= 0 )
                return;
            Array.Copy( ( Array ) this._innerList, 0, ( Array ) array, length1 + arrayIndex, length2 );
        }

        internal void CopyTo( int sourceIndex, T[ ] array, int destinationIndex, int count )
        {
            int sourceIndex1 = this._startIndex + 1 + sourceIndex;
            if ( sourceIndex1 < array.Length )
            {
                int length1 = Math.Min(array.Length - sourceIndex1, count);
                Array.Copy( ( Array ) this._innerList, sourceIndex1, ( Array ) array, destinationIndex, length1 );
                int length2 = count - length1;
                if ( length2 <= 0 )
                    return;
                Array.Copy( ( Array ) this._innerList, 0, ( Array ) array, destinationIndex + length1, length2 );
            }
            else
                Array.Copy( ( Array ) this._innerList, sourceIndex1 - array.Length, ( Array ) array, destinationIndex, count );
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove( T item )
        {
            int destinationIndex = Array.IndexOf<T>(this._innerList, item, 0, this._size);
            if ( destinationIndex < 0 )
                return false;
            --this._usedSize;
            if ( destinationIndex < this.Size - 1 )
                Array.Copy( ( Array ) this._innerList, destinationIndex + 1, ( Array ) this._innerList, destinationIndex, this.Size - destinationIndex - 1 );
            this._innerList[ this._usedSize ] = default( T );
            if ( destinationIndex <= this._startIndex )
                --this._startIndex;
            return true;
        }

        public T GetItemAt( int index )
        {
            this.ValidateIndex( index );
            return this._innerList[ this.ResolveIndex( index ) ];
        }

        public void SetItemAt( int index, T value )
        {
            this.ValidateIndex( index );
            this._innerList[ this.ResolveIndex( index ) ] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if ( this._usedSize != 0 )
            {
                int i;
                for ( i = this._startIndex + 1 ; i < this._usedSize ; ++i )
                    yield return this._innerList[ i ];
                for ( i = 0 ; i <= this._startIndex ; ++i )
                    yield return this._innerList[ i ];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( IEnumerator ) this.GetEnumerator();
        }

        internal int StartIndex
        {
            get
            {
                return this._startIndex;
            }
            set
            {
                this._startIndex = value;
            }
        }

        public T[ ] ItemsArray
        {
            get
            {
                return this._innerList;
            }
        }

        public void SetCount( int setLength )
        {
            throw new InvalidOperationException( "SetCount is not valid on Circular-Buffer list types" );
        }
    }
}
