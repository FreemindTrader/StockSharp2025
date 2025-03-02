// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyAccount
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyAccount : BaseEntity, INameEntity
    {
        public Client Client { get; set; }

        public string Name { get; set; }

        public StrategyConnection Connection { get; set; }

        public BaseEntitySet<StrategyOrder> Orders { get; set; }

        public BaseEntitySet<StrategyTrade> Trades { get; set; }

        public BaseEntitySet<StrategyPositionChange> Positions { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client = ( Client ) storage.GetValue<Client>( "Client", null );
            this.Name = ( string ) storage.GetValue<string>( "Name", null );
            this.Connection = ( StrategyConnection ) storage.GetValue<StrategyConnection>( "Connection", null );
            this.Orders = ( BaseEntitySet<StrategyOrder> ) storage.GetValue<BaseEntitySet<StrategyOrder>>( "Orders", null );
            this.Trades = ( BaseEntitySet<StrategyTrade> ) storage.GetValue<BaseEntitySet<StrategyTrade>>( "Trades", null );
            this.Positions = ( BaseEntitySet<StrategyPositionChange> ) storage.GetValue<BaseEntitySet<StrategyPositionChange>>( "Positions", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Client>( "Client", this.Client ).Set<string>( "Name", this.Name ).Set<StrategyConnection>( "Connection", this.Connection ).Set<BaseEntitySet<StrategyOrder>>( "Orders", this.Orders ).Set<BaseEntitySet<StrategyTrade>>( "Trades", this.Trades ).Set<BaseEntitySet<StrategyPositionChange>>( "Positions", this.Positions );
        }
    }
}
