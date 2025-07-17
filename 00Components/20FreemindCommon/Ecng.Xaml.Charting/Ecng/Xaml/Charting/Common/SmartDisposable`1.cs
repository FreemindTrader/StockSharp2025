// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.SmartDisposable`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    internal class SmartDisposable<T> : IDisposable where T : IDisposable
    {
        private readonly T _inner;

        public SmartDisposable( T inner )
        {
            this._inner = inner;
        }

        ~SmartDisposable()
        {
            this.Dispose( false );
        }

        internal T Inner
        {
            get
            {
                return this._inner;
            }
        }

        public void Dispose()
        {
            this.Dispose( true );
            GC.SuppressFinalize( ( object ) this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( ( object ) this._inner == null )
                return;
            this._inner.Dispose();
        }

        public static explicit operator T( SmartDisposable<T> b )
        {
            return b._inner;
        }
    }
}
