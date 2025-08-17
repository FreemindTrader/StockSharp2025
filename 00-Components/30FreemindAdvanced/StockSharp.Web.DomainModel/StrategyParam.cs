// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyParam
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyParam : BaseEntity, INameEntity, IStrategyEntity, IUserIdEntity
{
    public string Name { get; set; }

    public string UserId { get; set; }

    public Strategy Strategy { get; set; }

    public string Value { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public string Step { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.Strategy = storage.GetValue<Strategy>("Strategy", (Strategy)null);
        this.Value = storage.GetValue<string>("Value", (string)null);
        this.From = storage.GetValue<string>("From", (string)null);
        this.To = storage.GetValue<string>("To", (string)null);
        this.Step = storage.GetValue<string>("Step", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("Name", this.Name).Set<string>("UserId", this.UserId).Set<Strategy>("Strategy", this.Strategy).Set<string>("Value", this.Value).Set<string>("From", this.From).Set<string>("To", this.To).Set<string>("Step", this.Step);
    }
}
