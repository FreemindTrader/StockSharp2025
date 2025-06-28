// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyTrade
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyTrade : BaseEntity, IUserIdEntity, ISystemIdEntity
{
    public StrategyOrder Order { get; set; }

    public Decimal Price { get; set; }

    public Decimal Volume { get; set; }

    public string UserId { get; set; }

    public string SystemId { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Order = storage.GetValue<StrategyOrder>("Order", (StrategyOrder)null);
        this.Price = storage.GetValue<Decimal>("Price", 0M);
        this.Volume = storage.GetValue<Decimal>("Volume", 0M);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.SystemId = storage.GetValue<string>("SystemId", (string)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<StrategyOrder>("Order", this.Order).Set<Decimal>("Price", this.Price).Set<Decimal>("Volume", this.Volume).Set<string>("UserId", this.UserId).Set<string>("SystemId", this.SystemId);
    }
}
