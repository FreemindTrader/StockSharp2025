// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.BaseSubscriptionCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public abstract class BaseSubscriptionCommand : BaseStudioCommand
    {
        public Subscription Subscription { get; }

        public override bool IsPersistent
        {
            get
            {
                return true;
            }
        }

        protected BaseSubscriptionCommand( Subscription subscription )
        {
            Subscription subscription1 = subscription;
            if ( subscription1 == null )
                throw new ArgumentNullException( nameof( subscription ) );
            Subscription = subscription1;
        }
    }
}
