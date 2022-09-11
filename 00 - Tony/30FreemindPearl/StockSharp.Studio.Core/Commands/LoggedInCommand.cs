// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.LoggedInCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Web.DomainModel;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class LoggedInCommand : BaseStudioCommand
    {
        public LoggedInCommand( Client profile )
        {
            Client client = profile;
            if ( client == null )
                throw new ArgumentNullException( nameof( profile ) );
            Profile = client;
        }

        public Client Profile { get; }
    }
}
