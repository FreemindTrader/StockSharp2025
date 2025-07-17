// Decompiled with JetBrains decompiler
// Type: TinyMessenger.ITinyMessengerHub
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace TinyMessenger
{
    public interface ITinyMessengerHub
    {
        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, bool useStrongReferences ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, bool useStrongReferences, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, bool useStrongReferences ) where TMessage : class, ITinyMessage;

        TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, bool useStrongReferences, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage;

        void Unsubscribe<TMessage>( TinyMessageSubscriptionToken subscriptionToken ) where TMessage : class, ITinyMessage;

        void Publish<TMessage>( TMessage message ) where TMessage : class, ITinyMessage;

        void PublishAsync<TMessage>( TMessage message ) where TMessage : class, ITinyMessage;

        void PublishAsync<TMessage>( TMessage message, AsyncCallback callback ) where TMessage : class, ITinyMessage;
    }
}
