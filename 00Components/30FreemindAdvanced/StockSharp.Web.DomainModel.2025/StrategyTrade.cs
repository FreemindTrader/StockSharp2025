// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyTrade
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class StrategyTrade : BaseEntity, IUserIdEntity, ISystemIdEntity
    {
        public StrategyOrder Order { get; set; }

        public Decimal Price { get; set; }

        public Decimal Volume { get; set; }

        public string UserId { get; set; }

        public string SystemId { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Order = ( StrategyOrder ) storage.GetValue<StrategyOrder>( "Order", null );
            this.Price = ( Decimal ) storage.GetValue<Decimal>( "Price", Decimal.Zero );
            this.Volume = ( Decimal ) storage.GetValue<Decimal>( "Volume", Decimal.Zero );
            this.UserId = ( string ) storage.GetValue<string>( "UserId", null );
            this.SystemId = ( string ) storage.GetValue<string>( "SystemId", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<StrategyOrder>( "Order", this.Order ).Set<Decimal>( "Price", this.Price ).Set<Decimal>( "Volume", this.Volume ).Set<string>( "UserId", this.UserId ).Set<string>( "SystemId", this.SystemId );
        }
    }
}
