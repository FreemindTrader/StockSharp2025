// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyPnL
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class StrategyPnL : IPersistable
    {
        public Decimal Realized { get; set; }

        public Decimal? Unrealized { get; set; }

        public Decimal? Commission { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Realized = ( Decimal ) storage.GetValue<Decimal>( "Realized", Decimal.Zero );
            this.Unrealized = ( Decimal? ) storage.GetValue<Decimal?>( "Unrealized", new Decimal?() );
            this.Commission = ( Decimal? ) storage.GetValue<Decimal?>( "Commission", new Decimal?() );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<Decimal>( "Realized", this.Realized ).Set<Decimal?>( "Unrealized", this.Unrealized ).Set<Decimal?>( "Commission", this.Commission );
        }
    }
}
