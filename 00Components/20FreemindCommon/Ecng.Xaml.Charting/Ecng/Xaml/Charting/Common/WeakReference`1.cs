// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.WeakReference`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting.Common
{
    internal class WeakReference<T> where T : class
    {
        private readonly WeakReference inner;

        internal WeakReference( T target )
          : this( target, false )
        {
        }

        internal WeakReference( T target, bool trackResurrection )
        {
            if ( ( object ) target == null )
                throw new ArgumentNullException( nameof( target ) );
            this.inner = new WeakReference( ( object ) target, trackResurrection );
        }

        internal T Target
        {
            get
            {
                return ( T ) this.inner.Target;
            }
            set
            {
                this.inner.Target = ( object ) value;
            }
        }

        internal bool IsAlive
        {
            get
            {
                return this.inner.IsAlive;
            }
        }
    }
}
