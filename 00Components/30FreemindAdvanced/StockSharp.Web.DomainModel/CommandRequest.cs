// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.CommandRequest
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class CommandRequest : BaseRequest
{
    public CommandRequest()
      : base(SubscriptionTypes.Command)
    {
    }

    public CommandRequestScopes Scopes { get; set; }

    public override bool AllowClient => true;

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Scopes = storage.GetValue<CommandRequestScopes>("Scopes", (CommandRequestScopes)0);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<CommandRequestScopes>("Scopes", this.Scopes);
    }
}
