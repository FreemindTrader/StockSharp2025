// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyAccount
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyAccount : BaseEntity, INameEntity
{
    public Client Client { get; set; }

    public string Name { get; set; }

    public StrategyConnection Connection { get; set; }

    public BaseEntitySet<StrategyOrder> Orders { get; set; }

    public BaseEntitySet<StrategyTrade> Trades { get; set; }

    public BaseEntitySet<StrategyPositionChange> Positions { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.Connection = storage.GetValue<StrategyConnection>("Connection", (StrategyConnection)null);
        this.Orders = storage.GetValue<BaseEntitySet<StrategyOrder>>("Orders", (BaseEntitySet<StrategyOrder>)null);
        this.Trades = storage.GetValue<BaseEntitySet<StrategyTrade>>("Trades", (BaseEntitySet<StrategyTrade>)null);
        this.Positions = storage.GetValue<BaseEntitySet<StrategyPositionChange>>("Positions", (BaseEntitySet<StrategyPositionChange>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Client>("Client", this.Client).Set<string>("Name", this.Name).Set<StrategyConnection>("Connection", this.Connection).Set<BaseEntitySet<StrategyOrder>>("Orders", this.Orders).Set<BaseEntitySet<StrategyTrade>>("Trades", this.Trades).Set<BaseEntitySet<StrategyPositionChange>>("Positions", this.Positions);
    }
}
