// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.CommandInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.Collections.Generic;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class CommandInfo : IPersistable
{
    public CommandTypes Command { get; set; }

    public CommandScopes Scope { get; set; }

    public IDictionary<string, string> Args { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.Command = storage.GetValue<CommandTypes>("Command", (CommandTypes)0);
        this.Scope = storage.GetValue<CommandScopes>("Scope", (CommandScopes)0);
        this.Args = storage.GetValue<IDictionary<string, string>>("Args", (IDictionary<string, string>)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<CommandTypes>("Command", this.Command).Set<CommandScopes>("Scope", this.Scope).Set<IDictionary<string, string>>("Args", this.Args);
    }
}
