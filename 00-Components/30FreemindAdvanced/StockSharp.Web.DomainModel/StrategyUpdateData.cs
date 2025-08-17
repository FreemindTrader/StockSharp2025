// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyUpdateData
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyUpdateData : IPersistable
{
    public StrategyExecutionModes ExecMode { get; set; }

    public Strategy Strategy { get; set; }

    public StrategyOrder Order { get; set; }

    public StrategyTrade Trade { get; set; }

    public StrategyPosition Position { get; set; }

    public int? WaitQueueNum { get; set; }

    public TimeSpan? WaitAvgTime { get; set; }

    public StrategyCancelReasons? CancelReason { get; set; }

    public StrategyErrorInfo Error { get; set; }

    public StrategyLog Log { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.ExecMode = storage.GetValue<StrategyExecutionModes>("ExecMode", StrategyExecutionModes.None);
        this.Strategy = storage.GetValue<Strategy>("Strategy", (Strategy)null);
        this.Order = storage.GetValue<StrategyOrder>("Order", (StrategyOrder)null);
        this.Trade = storage.GetValue<StrategyTrade>("Trade", (StrategyTrade)null);
        this.Position = storage.GetValue<StrategyPosition>("Position", (StrategyPosition)null);
        this.WaitQueueNum = storage.GetValue<int?>("WaitQueueNum", new int?());
        this.WaitAvgTime = storage.GetValue<TimeSpan?>("WaitAvgTime", new TimeSpan?());
        this.CancelReason = storage.GetValue<StrategyCancelReasons?>("CancelReason", new StrategyCancelReasons?());
        this.Error = storage.GetValue<StrategyErrorInfo>("Error", (StrategyErrorInfo)null);
        this.Log = storage.GetValue<StrategyLog>("Log", (StrategyLog)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<StrategyExecutionModes>("ExecMode", this.ExecMode).Set<Strategy>("Strategy", this.Strategy).Set<StrategyOrder>("Order", this.Order).Set<StrategyTrade>("Trade", this.Trade).Set<StrategyPosition>("Position", this.Position).Set<int?>("WaitQueueNum", this.WaitQueueNum).Set<TimeSpan?>("WaitAvgTime", this.WaitAvgTime).Set<StrategyCancelReasons?>("CancelReason", this.CancelReason).Set<StrategyErrorInfo>("Error", this.Error).Set<StrategyLog>("Log", this.Log);
    }
}
