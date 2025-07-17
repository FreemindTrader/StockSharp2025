// Decompiled with JetBrains decompiler
// Type: TinyMessenger.TinyMessengerSubscriptionException
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace TinyMessenger
{
    public class TinyMessengerSubscriptionException : Exception
    {
        private const string ERROR_TEXT = "Unable to add subscription for {0} : {1}";

        public TinyMessengerSubscriptionException( Type messageType, string reason )
          : base( string.Format( "Unable to add subscription for {0} : {1}", ( object ) messageType, ( object ) reason ) )
        {
        }

        public TinyMessengerSubscriptionException( Type messageType, string reason, Exception innerException )
          : base( string.Format( "Unable to add subscription for {0} : {1}", ( object ) messageType, ( object ) reason ), innerException )
        {
        }
    }
}
