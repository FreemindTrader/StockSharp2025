// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntityIdResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public abstract class BaseEntityIdResponse(SubscriptionTypes type) : BaseResponse(type)
{
    public long? EntityId { get; set; }

    public long? ClientId { get; set; }

    public bool Removed { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.EntityId = storage.GetValue<long?>("EntityId", new long?());
        this.ClientId = storage.GetValue<long?>("ClientId", new long?());
        this.Removed = storage.GetValue<bool>("Removed", false);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<long?>("EntityId", this.EntityId).Set<long?>("ClientId", this.ClientId).Set<bool>("Removed", this.Removed);
    }
}
