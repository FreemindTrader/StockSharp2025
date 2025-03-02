// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.BaseSubscriptionCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public abstract class BaseSubscriptionCommand : BaseStudioCommand
    {
        protected BaseSubscriptionCommand( Subscription subscription )
        {
            Subscription subscription1 = subscription;
            if ( subscription1 == null )
                throw new ArgumentNullException( nameof( subscription ) );
            this.Subscription = subscription1;            
        }

        public Subscription Subscription { get; }

        public override bool IsPersistent
        {
            get
            {
                return true;
            }
        }
    }
}
