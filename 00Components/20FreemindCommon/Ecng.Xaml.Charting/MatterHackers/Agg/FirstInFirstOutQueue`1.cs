// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.FirstInFirstOutQueue`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class FirstInFirstOutQueue<T>
    {
        private T[] itemArray;
        private int size;
        private int head;
        private int shiftFactor;
        private int mask;

        public int Count
        {
            get
            {
                return this.size;
            }
        }

        public FirstInFirstOutQueue( int shiftFactor )
        {
            this.shiftFactor = shiftFactor;
            this.mask = ( 1 << shiftFactor ) - 1;
            this.itemArray = new T[ 1 << shiftFactor ];
            this.head = 0;
            this.size = 0;
        }

        public T First
        {
            get
            {
                return this.itemArray[ this.head & this.mask ];
            }
        }

        public void Enqueue( T itemToQueue )
        {
            if ( this.size == this.itemArray.Length )
            {
                int num = this.head & this.mask;
                ++this.shiftFactor;
                this.mask = ( 1 << this.shiftFactor ) - 1;
                T[] objArray = new T[1 << this.shiftFactor];
                Array.Copy( ( Array ) this.itemArray, num, ( Array ) objArray, 0, this.size - num );
                Array.Copy( ( Array ) this.itemArray, 0, ( Array ) objArray, this.size - num, num );
                this.itemArray = objArray;
                this.head = 0;
            }
            this.itemArray[ this.head + this.size++ & this.mask ] = itemToQueue;
        }

        public T Dequeue()
        {
            T obj = this.itemArray[this.head & this.mask];
            if ( this.size > 0 )
            {
                ++this.head;
                --this.size;
            }
            return obj;
        }
    }
}
