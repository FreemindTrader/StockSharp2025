// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.BaseSeriesColumn`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    internal abstract class BaseSeriesColumn<T> : ISeriesColumn<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, ISeriesColumn
    {
        protected IUltraList<T> _innerList = (IUltraList<T>) new UltraList<T>(128);

        public virtual UncheckedList<T> ToUncheckedList( int baseIndex, int count )
        {
            int baseIndex1 = baseIndex > this.Count ? this.Count : baseIndex;
            int count1 = Math.Min(this.Count - baseIndex1, count);
            return new UncheckedList<T>( this._innerList.ItemsArray, baseIndex1, count1 );
        }

        public T[ ] UncheckedArray()
        {
            return this._innerList.ItemsArray;
        }

        public void Add( T item )
        {
            this._innerList.Add( item );
        }

        public int Add( object value )
        {
            return ( ( IList ) this._innerList ).Add( value );
        }

        public bool Contains( object value )
        {
            return ( ( IList ) this._innerList ).Contains( value );
        }

        public void Clear()
        {
            this._innerList.Clear();
        }

        public int IndexOf( object value )
        {
            if ( this.Count == 0 )
            {
                return -1;
            }

            return this.IndexOf( ( T ) Convert.ChangeType( value, typeof( T ), ( IFormatProvider ) null ) );
        }

        public void Insert( int index, object value )
        {
            this._innerList.Insert( index, ( T ) value );
        }

        public void Remove( object value )
        {
            this._innerList.Remove( ( T ) value );
        }

        public bool Contains( T item )
        {
            return this._innerList.Contains( item );
        }

        public void CopyTo( T[ ] array, int arrayIndex )
        {
            this._innerList.CopyTo( array, arrayIndex );
        }

        bool ICollection<T>.Remove( T item )
        {
            return this._innerList.Remove( item );
        }

        public T GetMinimum()
        {
            return this._innerList.GetMinimum();
        }

        public T GetMaximum()
        {
            return this._innerList.GetMaximum();
        }

        public void AddRange( IEnumerable<T> items )
        {
            this._innerList.AddRange( items );
        }

        public void InsertRange( int startIndex, IEnumerable<T> values )
        {
            this._innerList.InsertRange( startIndex, values );
        }

        public void RemoveRange( int startIndex, int count )
        {
            this._innerList.RemoveRange( startIndex, count );
        }

        public void Remove( T item )
        {
            this._innerList.Remove( item );
        }

        public int IndexOf( T item )
        {
            return this._innerList.IndexOf( item );
        }

        public void Insert( int index, T item )
        {
            this._innerList.Insert( index, item );
        }

        public void RemoveAt( int index )
        {
            this._innerList.RemoveAt( index );
        }

        object IList.this[ int index ]
        {
            get
            {
                return ( object ) this._innerList[ index ];
            }
            set
            {
                this._innerList[ index ] = ( T ) value;
            }
        }

        public T this[ int index ]
        {
            get
            {
                return this._innerList[ index ];
            }
            set
            {
                this._innerList[ index ] = value;
            }
        }

        public void CopyTo( Array array, int index )
        {
            ( ( ICollection ) this._innerList ).CopyTo( array, index );
        }

        public int Count
        {
            get
            {
                return this._innerList.Count;
            }
        }

        public object SyncRoot
        {
            get
            {
                return ( object ) this._innerList;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool HasValues
        {
            get
            {
                return ( uint ) this._innerList.Count > 0U;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( IEnumerator ) this.GetEnumerator();
        }
    }
}
