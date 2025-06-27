// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyOrder
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using StockSharp.Messages;
using System;

namespace StockSharp.Web.DomainModel
{
    public class StrategyOrder : BaseEntity, INameEntity, IStateEntity<OrderStates>, IUserIdEntity, IInstrumentEntity, IStrategyEntity, ISystemIdEntity
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

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Strategy = ( Strategy ) storage.GetValue<Strategy>( "Strategy", null );
            this.Security = ( InstrumentInfo ) storage.GetValue<InstrumentInfo>( "Security", null );
            this.Account = ( StrategyAccount ) storage.GetValue<StrategyAccount>( "Account", null );
            this.Side = ( Sides ) storage.GetValue<Sides>( "Side", 0 );
            this.Type = ( OrderTypes ) storage.GetValue<OrderTypes>( "Type", 0 );
            this.State = ( OrderStates ) storage.GetValue<OrderStates>( "State", 0 );
            this.Tif = ( TimeInForce? ) storage.GetValue<TimeInForce?>( "Tif", new TimeInForce?() );
            this.Till = ( DateTime? ) storage.GetValue<DateTime?>( "Till", new DateTime?() );
            this.PositionEffect = ( OrderPositionEffects? ) storage.GetValue<OrderPositionEffects?>( "PositionEffect", new OrderPositionEffects?() );
            this.PostOnly = ( bool? ) storage.GetValue<bool?>( "PostOnly", new bool?() );
            this.Price = ( Decimal ) storage.GetValue<Decimal>( "Price", Decimal.Zero );
            this.Balance = ( Decimal ) storage.GetValue<Decimal>( "Balance", Decimal.Zero );
            this.Volume = ( Decimal ) storage.GetValue<Decimal>( "Volume", Decimal.Zero );
            this.VisibleVolume = ( Decimal? ) storage.GetValue<Decimal?>( "VisibleVolume", new Decimal?() );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.SystemId = ( string ) storage.GetValue<string>( "SystemId", null );
            this.ErrorMessage = ( string ) storage.GetValue<string>( "ErrorMessage", null );
            this.CancelledTime = ( DateTime? ) storage.GetValue<DateTime?>( "CancelledTime", new DateTime?() );
            this.MatchedTime = ( DateTime? ) storage.GetValue<DateTime?>( "MatchedTime", new DateTime?() );
            this.Trades = ( BaseEntitySet<StrategyTrade> ) storage.GetValue<BaseEntitySet<StrategyTrade>>( "Trades", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Strategy>( "Strategy", this.Strategy ).Set<InstrumentInfo>( "Security", this.Security ).Set<StrategyAccount>( "Account", this.Account ).Set<Sides>( "Side", this.Side ).Set<OrderTypes>( "Type", this.Type ).Set<OrderStates>( "State", this.State ).Set<TimeInForce?>( "Tif", this.Tif ).Set<DateTime?>( "Till", this.Till ).Set<OrderPositionEffects?>( "PositionEffect", this.PositionEffect ).Set<bool?>( "PostOnly", this.PostOnly ).Set<Decimal>( "Price", this.Price ).Set<Decimal>( "Balance", this.Balance ).Set<Decimal>( "Volume", this.Volume ).Set<Decimal?>( "VisibleVolume", this.VisibleVolume ).Set<string>( "Name", this.Name ).Set<string>( "UserId", this.UserId ).Set<string>( "SystemId", this.SystemId ).Set<string>( "ErrorMessage", this.ErrorMessage ).Set<DateTime?>( "CancelledTime", this.CancelledTime ).Set<DateTime?>( "MatchedTime", this.MatchedTime ).Set<BaseEntitySet<StrategyTrade>>( "Trades", this.Trades );
        }
    }
}
