// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyEquityChange
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyEquityChange : BaseEntity, IStrategyEntity
    {
        public Strategy Strategy { get; set; }

        public StrategyPnL PnL { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Strategy = ( Strategy ) storage.GetValue<Strategy>( "Strategy", null );
            this.PnL = ( StrategyPnL ) storage.GetValue<StrategyPnL>( "PnL", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Strategy>( "Strategy", this.Strategy ).Set<StrategyPnL>( "PnL", this.PnL );
        }
    }
}
