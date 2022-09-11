// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RequestLicenseCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Common;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class RequestLicenseCommand : BaseStudioCommand
    {
        public long BrokerId { get; }

        public string Account { get; }

        public RequestLicenseCommand( long brokerId, string account )
        {
            if ( account.IsEmpty() )
                throw new ArgumentNullException( nameof( account ) );
            BrokerId = brokerId;
            Account = account;
        }
    }
}
