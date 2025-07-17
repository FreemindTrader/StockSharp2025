// Decompiled with JetBrains decompiler
// Type: TinyMessenger.ITinyMessageSubscription
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace TinyMessenger
{
    public interface ITinyMessageSubscription
    {
        TinyMessageSubscriptionToken SubscriptionToken
        {
            get;
        }

        bool ShouldAttemptDelivery( ITinyMessage message );

        void Deliver( ITinyMessage message );
    }
}
