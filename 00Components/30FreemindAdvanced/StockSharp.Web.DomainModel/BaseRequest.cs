// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseRequest
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public abstract class BaseRequest(SubscriptionTypes type) : IPersistable
{
    public bool IsSubscribe { get; set; }

    public long Id { get; set; }

    public SubscriptionTypes Type { get; set; } = type;

    public virtual bool AllowClient => false;

    public override string ToString() => $"Type={this.Type}, Id={this.Id}";

    public virtual void Load(SettingsStorage storage)
    {
        this.IsSubscribe = storage.GetValue<bool>("IsSubscribe", false);
        this.Id = storage.GetValue<long>("Id", 0L);
        this.Type = storage.GetValue<SubscriptionTypes>("Type", SubscriptionTypes.StrategyUpdate);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<bool>("IsSubscribe", this.IsSubscribe).Set<long>("Id", this.Id).Set<SubscriptionTypes>("Type", this.Type);
    }
}
