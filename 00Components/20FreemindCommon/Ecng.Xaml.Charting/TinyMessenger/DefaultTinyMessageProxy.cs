// Decompiled with JetBrains decompiler
// Type: TinyMessenger.DefaultTinyMessageProxy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace TinyMessenger
{
    public sealed class DefaultTinyMessageProxy : ITinyMessageProxy
    {
        private static readonly DefaultTinyMessageProxy _Instance = new DefaultTinyMessageProxy();

        public static DefaultTinyMessageProxy Instance
        {
            get
            {
                return DefaultTinyMessageProxy._Instance;
            }
        }

        private DefaultTinyMessageProxy()
        {
        }

        public void Deliver( ITinyMessage message, ITinyMessageSubscription subscription )
        {
            subscription.Deliver( message );
        }
    }
}
