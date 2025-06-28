// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyBacktestOptions
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Logging;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public class StrategyBacktestOptions : IPersistable
{
    public int PerDay { get; set; }

    public TimeSpan Delay { get; set; }

    public TimeSpan MaxDuration { get; set; }

    public TimeSpan MaxPeriod { get; set; }

    public StrategyGroups Group { get; set; }

    public ProductContentTypes2 AllowContent { get; set; }

    public int MaxParallel { get; set; }

    public int MaxMessageCount { get; set; }

    public LogLevels LogLevel { get; set; }

    public int LogMax { get; set; }

    public int ChartMax { get; set; }

    public int LogTextMax { get; set; }

    public int CommentMax { get; set; }

    public bool AllowCustomRefs { get; set; }

    public BaseEntitySet<DataType> AllowDataTypes { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.PerDay = storage.GetValue<int>("PerDay", 0);
        this.Delay = storage.GetValue<TimeSpan>("Delay", new TimeSpan());
        this.MaxDuration = storage.GetValue<TimeSpan>("MaxDuration", new TimeSpan());
        this.MaxPeriod = storage.GetValue<TimeSpan>("MaxPeriod", new TimeSpan());
        this.Group = storage.GetValue<StrategyGroups>("Group", StrategyGroups.Common);
        this.AllowContent = storage.GetValue<ProductContentTypes2>("AllowContent", (ProductContentTypes2)0);
        this.MaxParallel = storage.GetValue<int>("MaxParallel", 0);
        this.MaxMessageCount = storage.GetValue<int>("MaxMessageCount", 0);
        this.LogLevel = storage.GetValue<LogLevels>("LogLevel", (LogLevels)0);
        this.LogMax = storage.GetValue<int>("LogMax", 0);
        this.ChartMax = storage.GetValue<int>("ChartMax", 0);
        this.LogTextMax = storage.GetValue<int>("LogTextMax", 0);
        this.CommentMax = storage.GetValue<int>("CommentMax", 0);
        this.AllowCustomRefs = storage.GetValue<bool>("AllowCustomRefs", false);
        this.AllowDataTypes = storage.GetValue<BaseEntitySet<DataType>>("AllowDataTypes", (BaseEntitySet<DataType>)null);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<int>("PerDay", this.PerDay).Set<TimeSpan>("Delay", this.Delay).Set<TimeSpan>("MaxDuration", this.MaxDuration).Set<TimeSpan>("MaxPeriod", this.MaxPeriod).Set<StrategyGroups>("Group", this.Group).Set<ProductContentTypes2>("AllowContent", this.AllowContent).Set<int>("MaxParallel", this.MaxParallel).Set<int>("MaxMessageCount", this.MaxMessageCount).Set<LogLevels>("LogLevel", this.LogLevel).Set<int>("LogMax", this.LogMax).Set<int>("ChartMax", this.ChartMax).Set<int>("LogTextMax", this.LogTextMax).Set<int>("CommentMax", this.CommentMax).Set<bool>("AllowCustomRefs", this.AllowCustomRefs).Set<BaseEntitySet<DataType>>("AllowDataTypes", this.AllowDataTypes);
    }
}
