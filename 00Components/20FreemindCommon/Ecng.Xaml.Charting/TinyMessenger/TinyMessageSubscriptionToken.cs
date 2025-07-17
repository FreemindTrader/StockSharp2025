// Decompiled with JetBrains decompiler
// Type: TinyMessenger.TinyMessageSubscriptionToken
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;

namespace TinyMessenger
{
    public sealed class TinyMessageSubscriptionToken : IDisposable
    {
        private WeakReference _Hub;
        private Type _MessageType;

        public TinyMessageSubscriptionToken( ITinyMessengerHub hub, Type messageType )
        {
            if ( hub == null )
                throw new ArgumentNullException( nameof( hub ) );
            if ( !typeof( ITinyMessage ).IsAssignableFrom( messageType ) )
                throw new ArgumentOutOfRangeException( nameof( messageType ) );
            _Hub = new WeakReference( ( object ) hub );
            _MessageType = messageType;
        }

        public void Dispose()
        {
            if ( _Hub.IsAlive )
            {
                ITinyMessengerHub target = _Hub.Target as ITinyMessengerHub;
                if ( target != null )
                    typeof( ITinyMessengerHub ).GetMethod( "Unsubscribe", new Type[ 1 ]
                    {
            typeof (TinyMessageSubscriptionToken)
                    } ).MakeGenericMethod( _MessageType ).Invoke( ( object ) target, new object[ 1 ]
                    {
            (object) this
                    } );
            }
            GC.SuppressFinalize( ( object ) this );
        }
    }
}
