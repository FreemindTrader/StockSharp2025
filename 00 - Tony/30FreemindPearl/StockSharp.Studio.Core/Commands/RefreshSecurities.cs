// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RefreshSecurities
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo.Storages;
using StockSharp.Messages;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class RefreshSecurities : BaseStudioCommand
    {
        public IMarketDataDrive Drive { get; }

        public SecurityLookupMessage Criteria { get; }

        public Func<bool> IsCancelled { get; }

        public Action<int> ProgressChanged { get; }

        public Action<int> WhenFinished { get; }

        public RefreshSecurities(
          IMarketDataDrive drive,
          SecurityLookupMessage criteria,
          Func<bool> isCancelled,
          Action<int> progressChanged,
          Action<int> whenFinished )
        {
            IMarketDataDrive marketDataDrive = drive;
            if ( marketDataDrive == null )
                throw new ArgumentNullException( nameof( drive ) );
            this.Drive = marketDataDrive;
            SecurityLookupMessage securityLookupMessage = criteria;
            if ( securityLookupMessage == null )
                throw new ArgumentNullException( nameof( criteria ) );
            this.Criteria = securityLookupMessage;
            Func<bool> func = isCancelled;
            if ( func == null )
                throw new ArgumentNullException( nameof( isCancelled ) );
            this.IsCancelled = func;
            Action<int> action1 = progressChanged;
            if ( action1 == null )
                throw new ArgumentNullException( nameof( progressChanged ) );
            this.ProgressChanged = action1;
            Action<int> action2 = whenFinished;
            if ( action2 == null )
                throw new ArgumentNullException( nameof( whenFinished ) );
            this.WhenFinished = action2;
        }
    }
}
