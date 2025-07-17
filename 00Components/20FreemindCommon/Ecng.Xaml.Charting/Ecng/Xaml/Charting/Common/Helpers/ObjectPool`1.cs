// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Helpers.ObjectPool`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Common.Helpers
{
    internal class ObjectPool<T> : IDisposable where T : new()
    {
        private readonly object _locker;
        private readonly Queue<T> _queue;
        private int _count;

        public ObjectPool()
        {
            this._locker = new object();
            this._queue = new Queue<T>();
        }

        public ObjectPool( int initAmount, Func<T, T> actionOnCreation )
          : this()
        {
            for ( int index = 0 ; index < initAmount ; ++index )
            {
                T obj = actionOnCreation(Activator.CreateInstance<T>());
                ++this._count;
                this.Put( obj );
            }
        }

        public int Count
        {
            get
            {
                return this._count;
            }
        }

        public int AvailableCount
        {
            get
            {
                return this._queue.Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this._queue.Count == 0;
            }
        }

        public T Get()
        {
            return this.Get( ( Func<T, T> ) ( T => T ) );
        }

        public T Get( Func<T, T> actionOnCreation )
        {
            return this.Get( ( Func<T> ) ( () => actionOnCreation( Activator.CreateInstance<T>() ) ) );
        }

        public T Get( Func<T> actionOnCreation )
        {
            lock ( this._locker )
            {
                if ( this._queue.Count > 0 )
                    return this._queue.Dequeue();
                ++this._count;
                return actionOnCreation();
            }
        }

        public void Put( T item )
        {
            lock ( this._locker )
                this._queue.Enqueue( item );
        }

        public void Dispose()
        {
            lock ( this._locker )
            {
                this._count = 0;
                while ( this._queue.Count > 0 )
                    this._queue.Dequeue();
            }
        }
    }
}
