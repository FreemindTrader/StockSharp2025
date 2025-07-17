// Decompiled with JetBrains decompiler
// Type: TinyMessenger.TinyMessageBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace TinyMessenger
{
    public abstract class TinyMessageBase : ITinyMessage
    {
        private WeakReference _Sender;

        public object Sender
        {
            get
            {
                if ( this._Sender != null )
                    return this._Sender.Target;
                return ( object ) null;
            }
        }

        public TinyMessageBase( object sender )
        {
            if ( sender == null )
                throw new ArgumentNullException( nameof( sender ) );
            this._Sender = new WeakReference( sender );
        }
    }
}
