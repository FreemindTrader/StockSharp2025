// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyPosition
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyPosition : BaseEntity, IInstrumentEntity, IStrategyEntity
    {
        public Strategy Strategy { get; set; }

        public InstrumentInfo Security { get; set; }

        public StrategyAccount Account { get; set; }

        public StrategyPositionChange [ ] Changes { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Strategy = ( Strategy ) storage.GetValue<Strategy>( "Strategy", null );
            this.Security = ( InstrumentInfo ) storage.GetValue<InstrumentInfo>( "Security", null );
            this.Account = ( StrategyAccount ) storage.GetValue<StrategyAccount>( "Account", null );
            this.Changes = ( StrategyPositionChange [ ] ) storage.GetValue<StrategyPositionChange [ ]>( "Changes", null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<Strategy>( "Strategy", this.Strategy ).Set<InstrumentInfo>( "Security", this.Security ).Set<StrategyAccount>( "Account", this.Account ).Set<StrategyPositionChange [ ]>( "Changes", this.Changes );
        }
    }
}
