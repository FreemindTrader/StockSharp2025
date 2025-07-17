// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.FifoSeriesColumn`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecng.Xaml.Charting
{
    internal class FifoSeriesColumn<T> : BaseSeriesColumn<T>
    {
        private readonly T[] _doubleBuffer;
        private readonly int _fifoSize;

        public FifoSeriesColumn( int size )
        {
            this._fifoSize = size;
            this._innerList = ( IUltraList<T> ) new FifoBuffer<T>( size );
            this._doubleBuffer = new T[ size ];
        }

        public override UncheckedList<T> ToUncheckedList( int baseIndex, int count )
        {
            int num = baseIndex > this.Count ? this.Count : baseIndex;
            int count1 = Math.Min(this.Count - num, count);
            if ( this._innerList.Count != this._fifoSize )
                return new UncheckedList<T>( this._innerList.ItemsArray, num, count1 );
            ( ( FifoBuffer<T> ) this._innerList ).CopyTo( num, this._doubleBuffer, 0, count1 );
            return new UncheckedList<T>( this._doubleBuffer, 0, count1 );
        }

        public T[ ] ToArray()
        {
            if ( this._innerList.Count != this._fifoSize )
                return this._innerList.ToArray<T>();
            this._innerList.CopyTo( this._doubleBuffer, 0 );
            return this._doubleBuffer;
        }

        public IList<T> ToUnorderedUncheckedList()
        {
            return ( IList<T> ) this._innerList.ItemsArray;
        }
    }
}
