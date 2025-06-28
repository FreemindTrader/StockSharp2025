// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyOptimizationOptions
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyOptimizationOptions : StrategyBacktestOptions
{
    public StrategyOptimizations Available { get; set; }

    public int MaxIterations { get; set; }

    public int BatchSize { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.Available = storage.GetValue<StrategyOptimizations>("Available", StrategyOptimizations.None);
        this.MaxIterations = storage.GetValue<int>("MaxIterations", 0);
        this.BatchSize = storage.GetValue<int>("BatchSize", 0);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<StrategyOptimizations>("Available", this.Available).Set<int>("MaxIterations", this.MaxIterations).Set<int>("BatchSize", this.BatchSize);
    }
}
