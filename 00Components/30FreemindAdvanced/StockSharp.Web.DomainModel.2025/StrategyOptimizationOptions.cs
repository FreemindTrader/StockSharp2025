// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyOptimizationOptions
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyOptimizationOptions : StrategyBacktestOptions
    {
        public StrategyOptimizations Available { get; set; }

        public int MaxIterations { get; set; }

        public int BatchSize { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Available = ( StrategyOptimizations ) storage.GetValue<StrategyOptimizations>( "Available", 0 );
            this.MaxIterations = ( int ) storage.GetValue<int>( "MaxIterations", 0 );
            this.BatchSize = ( int ) storage.GetValue<int>( "BatchSize", 0 );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<StrategyOptimizations>( "Available", this.Available ).Set<int>( "MaxIterations", this.MaxIterations ).Set<int>( "BatchSize", this.BatchSize );
        }
    }
}
