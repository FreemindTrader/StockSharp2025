// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyOrder
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyOrder :
  BaseEntity,
  INameEntity,
  IStateEntity<OrderStates>,
  IUserIdEntity,
  IInstrumentEntity,
  IStrategyEntity,
  ISystemIdEntity
{
    public Strategy Strategy { get; set; }

    public InstrumentInfo Security { get; set; }

    public StrategyAccount Account { get; set; }

    public Sides Side { get; set; }

    public OrderTypes Type { get; set; }

    public OrderStates State { get; set; }

    public TimeInForce? Tif { get; set; }

    public DateTime? Till { get; set; }

    public OrderPositionEffects? PositionEffect { get; set; }

    public bool? PostOnly { get; set; }

    public Decimal Price { get; set; }

    public Decimal Balance { get; set; }

    public Decimal Volume { get; set; }

    public Decimal? VisibleVolume { get; set; }

    public string Name { get; set; }

    public string UserId { get; set; }

    public string SystemId { get; set; }

    public string ErrorMessage { get; set; }

    public DateTime? CancelledTime { get; set; }

    public DateTime? MatchedTime { get; set; }

    public BaseEntitySet<StrategyTrade> Trades { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Strategy = storage.GetValue<Strategy>("Strategy", (Strategy)null);
        this.Security = storage.GetValue<InstrumentInfo>("Security", (InstrumentInfo)null);
        this.Account = storage.GetValue<StrategyAccount>("Account", (StrategyAccount)null);
        this.Side = storage.GetValue<Sides>("Side", (Sides)0);
        this.Type = storage.GetValue<OrderTypes>("Type", (OrderTypes)0);
        this.State = storage.GetValue<OrderStates>("State", (OrderStates)0);
        this.Tif = storage.GetValue<TimeInForce?>("Tif", new TimeInForce?());
        this.Till = storage.GetValue<DateTime?>("Till", new DateTime?());
        this.PositionEffect = storage.GetValue<OrderPositionEffects?>("PositionEffect", new OrderPositionEffects?());
        this.PostOnly = storage.GetValue<bool?>("PostOnly", new bool?());
        this.Price = storage.GetValue<Decimal>("Price", 0M);
        this.Balance = storage.GetValue<Decimal>("Balance", 0M);
        this.Volume = storage.GetValue<Decimal>("Volume", 0M);
        this.VisibleVolume = storage.GetValue<Decimal?>("VisibleVolume", new Decimal?());
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.SystemId = storage.GetValue<string>("SystemId", (string)null);
        this.ErrorMessage = storage.GetValue<string>("ErrorMessage", (string)null);
        this.CancelledTime = storage.GetValue<DateTime?>("CancelledTime", new DateTime?());
        this.MatchedTime = storage.GetValue<DateTime?>("MatchedTime", new DateTime?());
        this.Trades = storage.GetValue<BaseEntitySet<StrategyTrade>>("Trades", (BaseEntitySet<StrategyTrade>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<Strategy>("Strategy", this.Strategy).Set<InstrumentInfo>("Security", this.Security).Set<StrategyAccount>("Account", this.Account).Set<Sides>("Side", this.Side).Set<OrderTypes>("Type", this.Type).Set<OrderStates>("State", this.State).Set<TimeInForce?>("Tif", this.Tif).Set<DateTime?>("Till", this.Till).Set<OrderPositionEffects?>("PositionEffect", this.PositionEffect).Set<bool?>("PostOnly", this.PostOnly).Set<Decimal>("Price", this.Price).Set<Decimal>("Balance", this.Balance).Set<Decimal>("Volume", this.Volume).Set<Decimal?>("VisibleVolume", this.VisibleVolume).Set<string>("Name", this.Name).Set<string>("UserId", this.UserId).Set<string>("SystemId", this.SystemId).Set<string>("ErrorMessage", this.ErrorMessage).Set<DateTime?>("CancelledTime", this.CancelledTime).Set<DateTime?>("MatchedTime", this.MatchedTime).Set<BaseEntitySet<StrategyTrade>>("Trades", this.Trades);
    }
}
