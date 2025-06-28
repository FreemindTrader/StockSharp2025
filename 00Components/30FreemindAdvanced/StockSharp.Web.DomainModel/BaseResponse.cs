// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseResponse
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public abstract class BaseResponse(SubscriptionTypes type) : IPersistable
{
    public SubscriptionTypes Type { get; } = type;

    public long RequestId { get; set; }

    public virtual bool AllowEveryone => false;

    public override string ToString() => $"Type={this.Type}, RequestId={this.RequestId}";

    public virtual void Load(SettingsStorage storage)
    {
        this.RequestId = storage.GetValue<long>("RequestId", 0L);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<long>("RequestId", this.RequestId);
    }
}
