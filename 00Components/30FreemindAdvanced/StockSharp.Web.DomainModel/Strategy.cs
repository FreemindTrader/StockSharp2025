// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Strategy
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;
using StockSharp.Messages;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class Strategy :
  BaseEntity,
  IClientEntity,
  INameEntity,
  IUserIdEntity,
  IStateEntity<SubscriptionStates?>,
  IInstrumentEntity,
  ILastTimeEntity
{
    public StrategyType Type { get; set; }

    public Client Client { get; set; }

    public string Name { get; set; }

    public string UserId { get; set; }

    public StrategyBacktestOptions Backtest { get; set; }

    public StrategyOptimizationOptions Optimization { get; set; }

    public StrategyGenetic Genetic { get; set; }

    public StrategyLiveOptions Live { get; set; }

    public SubscriptionStates? State { get; set; }

    public StrategyPnL PnL { get; set; }

    public StrategyAccount Account { get; set; }

    public InstrumentInfo Security { get; set; }

    public App App { get; set; }

    public DateTime? BacktestFrom { get; set; }

    public DateTime? BacktestTo { get; set; }

    public File BacktestReport { get; set; }

    public StrategyReportTypes? BacktestReportType { get; set; }

    public string EmulatorSettings { get; set; }

    public string StrategySettings { get; set; }

    public DateTime? LastCommandTime { get; set; }

    public DateTime? LastStatusTime { get; set; }

    public BaseEntitySet<StrategyParam> Params { get; set; }

    public BaseEntitySet<StrategyOrder> Orders { get; set; }

    public BaseEntitySet<StrategyPositionChange> Positions { get; set; }

    public BaseEntitySet<StrategyEquityChange> Equities { get; set; }

    public BaseEntitySet<StrategyTrade> Trades { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Type = storage.GetValue<StrategyType>("Type", (StrategyType)null);
        this.Client = storage.GetValue<Client>("Client", (Client)null);
        this.Name = storage.GetValue<string>("Name", (string)null);
        this.UserId = storage.GetValue<string>("UserId", (string)null);
        this.Backtest = storage.GetValue<StrategyBacktestOptions>("Backtest", (StrategyBacktestOptions)null);
        this.Optimization = storage.GetValue<StrategyOptimizationOptions>("Optimization", (StrategyOptimizationOptions)null);
        this.Genetic = storage.GetValue<StrategyGenetic>("Genetic", (StrategyGenetic)null);
        this.Live = storage.GetValue<StrategyLiveOptions>("Live", (StrategyLiveOptions)null);
        this.State = storage.GetValue<SubscriptionStates?>("State", new SubscriptionStates?());
        this.PnL = storage.GetValue<StrategyPnL>("PnL", (StrategyPnL)null);
        this.Account = storage.GetValue<StrategyAccount>("Account", (StrategyAccount)null);
        this.Security = storage.GetValue<InstrumentInfo>("Security", (InstrumentInfo)null);
        this.App = storage.GetValue<App>("App", (App)null);
        this.BacktestFrom = storage.GetValue<DateTime?>("BacktestFrom", new DateTime?());
        this.BacktestTo = storage.GetValue<DateTime?>("BacktestTo", new DateTime?());
        this.BacktestReport = storage.GetValue<File>("BacktestReport", (File)null);
        this.BacktestReportType = storage.GetValue<StrategyReportTypes?>("BacktestReportType", new StrategyReportTypes?());
        this.EmulatorSettings = storage.GetValue<string>("EmulatorSettings", (string)null);
        this.StrategySettings = storage.GetValue<string>("StrategySettings", (string)null);
        this.LastCommandTime = storage.GetValue<DateTime?>("LastCommandTime", new DateTime?());
        this.LastStatusTime = storage.GetValue<DateTime?>("LastStatusTime", new DateTime?());
        this.Params = storage.GetValue<BaseEntitySet<StrategyParam>>("Params", (BaseEntitySet<StrategyParam>)null);
        this.Orders = storage.GetValue<BaseEntitySet<StrategyOrder>>("Orders", (BaseEntitySet<StrategyOrder>)null);
        this.Positions = storage.GetValue<BaseEntitySet<StrategyPositionChange>>("Positions", (BaseEntitySet<StrategyPositionChange>)null);
        this.Equities = storage.GetValue<BaseEntitySet<StrategyEquityChange>>("Equities", (BaseEntitySet<StrategyEquityChange>)null);
        this.Trades = storage.GetValue<BaseEntitySet<StrategyTrade>>("Trades", (BaseEntitySet<StrategyTrade>)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<StrategyType>("Type", this.Type).Set<Client>("Client", this.Client).Set<string>("Name", this.Name).Set<string>("UserId", this.UserId).Set<StrategyBacktestOptions>("Backtest", this.Backtest).Set<StrategyOptimizationOptions>("Optimization", this.Optimization).Set<StrategyGenetic>("Genetic", this.Genetic).Set<StrategyLiveOptions>("Live", this.Live).Set<SubscriptionStates?>("State", this.State).Set<StrategyPnL>("PnL", this.PnL).Set<StrategyAccount>("Account", this.Account).Set<InstrumentInfo>("Security", this.Security).Set<App>("App", this.App).Set<DateTime?>("BacktestFrom", this.BacktestFrom).Set<DateTime?>("BacktestTo", this.BacktestTo).Set<File>("BacktestReport", this.BacktestReport).Set<StrategyReportTypes?>("BacktestReportType", this.BacktestReportType).Set<string>("EmulatorSettings", this.EmulatorSettings).Set<string>("StrategySettings", this.StrategySettings).Set<DateTime?>("LastCommandTime", this.LastCommandTime).Set<DateTime?>("LastStatusTime", this.LastStatusTime).Set<BaseEntitySet<StrategyParam>>("Params", this.Params).Set<BaseEntitySet<StrategyOrder>>("Orders", this.Orders).Set<BaseEntitySet<StrategyPositionChange>>("Positions", this.Positions).Set<BaseEntitySet<StrategyEquityChange>>("Equities", this.Equities).Set<BaseEntitySet<StrategyTrade>>("Trades", this.Trades);
    }
}
