// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.LoggedInCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

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
            this.Profile = client;           
        }

        public Client Profile { get; }
    }
}
