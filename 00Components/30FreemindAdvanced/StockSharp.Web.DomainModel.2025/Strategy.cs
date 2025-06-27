// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.Strategy
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class Strategy : BaseEntity, IClientEntity, INameEntity, IUserIdEntity, IStateEntity<SubscriptionStates?>, IInstrumentEntity, ILastTimeEntity
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Type = ( StrategyType ) storage.GetValue<StrategyType>( "Type", null );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.Backtest = ( StrategyBacktestOptions ) storage.GetValue<StrategyBacktestOptions>( "Backtest", null );
            this.Optimization = ( StrategyOptimizationOptions ) storage.GetValue<StrategyOptimizationOptions>( "Optimization", null );
            this.Genetic = ( StrategyGenetic ) storage.GetValue<StrategyGenetic>( "Genetic", null );
            this.Live = ( StrategyLiveOptions ) storage.GetValue<StrategyLiveOptions>( "Live", null );
            this.State = ( SubscriptionStates? ) storage.GetValue<SubscriptionStates?>( "State", new SubscriptionStates?() );
            this.PnL = ( StrategyPnL ) storage.GetValue<StrategyPnL>( "PnL", null );
            this.Account = ( StrategyAccount ) storage.GetValue<StrategyAccount>( "Account", null );
            this.Security = ( InstrumentInfo ) storage.GetValue<InstrumentInfo>( "Security", null );
            this.App = ( App ) storage.GetValue<App>( "App", null );
            this.BacktestFrom = ( DateTime? ) storage.GetValue<DateTime?>( "BacktestFrom", new DateTime?() );
            this.BacktestTo = ( DateTime? ) storage.GetValue<DateTime?>( "BacktestTo", new DateTime?() );
            this.BacktestReport = ( File ) storage.GetValue<File>( "BacktestReport", null );
            this.BacktestReportType = ( StrategyReportTypes? ) storage.GetValue<StrategyReportTypes?>( "BacktestReportType", new StrategyReportTypes?() );
            this.EmulatorSettings = ( string ) storage.GetValue<string>( "EmulatorSettings", null );
            this.StrategySettings = ( string ) storage.GetValue<string>( "StrategySettings", null );
            this.LastCommandTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastCommandTime", new DateTime?() );
            this.LastStatusTime = ( DateTime? ) storage.GetValue<DateTime?>( "LastStatusTime", new DateTime?() );
            this.Params = ( BaseEntitySet<StrategyParam> ) storage.GetValue<BaseEntitySet<StrategyParam>>( "Params", null );
            this.Orders = ( BaseEntitySet<StrategyOrder> ) storage.GetValue<BaseEntitySet<StrategyOrder>>( "Orders", null );
            this.Positions = ( BaseEntitySet<StrategyPositionChange> ) storage.GetValue<BaseEntitySet<StrategyPositionChange>>( "Positions", null );
            this.Equities = ( BaseEntitySet<StrategyEquityChange> ) storage.GetValue<BaseEntitySet<StrategyEquityChange>>( "Equities", null );
            this.Trades = ( BaseEntitySet<StrategyTrade> ) storage.GetValue<BaseEntitySet<StrategyTrade>>( "Trades", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<StrategyType>( "Type", this.Type ).Set<Client>( "Client", this.Client ).Set<string>( "Name", this.Name ).Set<string>( "UserId", this.UserId ).Set<StrategyBacktestOptions>( "Backtest", this.Backtest ).Set<StrategyOptimizationOptions>( "Optimization", this.Optimization ).Set<StrategyGenetic>( "Genetic", this.Genetic ).Set<StrategyLiveOptions>( "Live", this.Live ).Set<SubscriptionStates?>( "State", this.State ).Set<StrategyPnL>( "PnL", this.PnL ).Set<StrategyAccount>( "Account", this.Account ).Set<InstrumentInfo>( "Security", this.Security ).Set<App>( "App", this.App ).Set<DateTime?>( "BacktestFrom", this.BacktestFrom ).Set<DateTime?>( "BacktestTo", this.BacktestTo ).Set<File>( "BacktestReport", this.BacktestReport ).Set<StrategyReportTypes?>( "BacktestReportType", this.BacktestReportType ).Set<string>( "EmulatorSettings", this.EmulatorSettings ).Set<string>( "StrategySettings", this.StrategySettings ).Set<DateTime?>( "LastCommandTime", this.LastCommandTime ).Set<DateTime?>( "LastStatusTime", this.LastStatusTime ).Set<BaseEntitySet<StrategyParam>>( "Params", this.Params ).Set<BaseEntitySet<StrategyOrder>>( "Orders", this.Orders ).Set<BaseEntitySet<StrategyPositionChange>>( "Positions", this.Positions ).Set<BaseEntitySet<StrategyEquityChange>>( "Equities", this.Equities ).Set<BaseEntitySet<StrategyTrade>>( "Trades", this.Trades );
        }
    }
}
