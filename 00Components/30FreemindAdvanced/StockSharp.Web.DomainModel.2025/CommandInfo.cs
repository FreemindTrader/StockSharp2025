// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.CommandInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System.Collections.Generic;

namespace StockSharp.Web.DomainModel
{
    public class CommandInfo : IPersistable
    {
        public CommandTypes Command { get; set; }

        public CommandScopes Scope { get; set; }

        public IDictionary<string, string> Args { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Command = ( CommandTypes ) storage.GetValue<CommandTypes>( "Command", 0 );
            this.Scope = ( CommandScopes ) storage.GetValue<CommandScopes>( "Scope", 0 );
            this.Args = ( IDictionary<string, string> ) storage.GetValue<IDictionary<string, string>>( "Args", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<CommandTypes>( "Command", this.Command ).Set<CommandScopes>( "Scope", this.Scope ).Set<IDictionary<string, string>>( "Args", this.Args );
        }
    }
}
