// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyPositionChange
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class StrategyPositionChange : BaseEntity
    {
        public StrategyPosition Position { get; set; }

        public Decimal Value { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Position = ( StrategyPosition ) storage.GetValue<StrategyPosition>( "Position", null );
            this.Value = ( Decimal ) storage.GetValue<Decimal>( "Value", Decimal.Zero );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<StrategyPosition>( "Position", this.Position ).Set<Decimal>( "Value", this.Value );
        }
    }
}
