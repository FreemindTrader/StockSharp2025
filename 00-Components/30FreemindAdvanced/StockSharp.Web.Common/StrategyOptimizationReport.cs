// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyOptimizationReport
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public class StrategyOptimizationReport : StrategyBaseReport, IPersistable
{
    public string[] Types { get; set; }

    public StrategyOptimizationReportItem[] Items { get; set; }

    void IPersistable.Load(SettingsStorage storage)
    {
        this.Types = storage.GetValue<string[]>("Types", (string[])null);
        IEnumerable<SettingsStorage> source1 = storage.GetValue<IEnumerable<SettingsStorage>>("Items", (IEnumerable<SettingsStorage>)null);
        this.Items = source1 != null ? source1.Select<SettingsStorage, StrategyOptimizationReportItem>((Func<SettingsStorage, StrategyOptimizationReportItem>)(s => PersistableHelper.Load<StrategyOptimizationReportItem>(s))).ToArray<StrategyOptimizationReportItem>() : (StrategyOptimizationReportItem[])null;
        IEnumerable<SettingsStorage> source2 = storage.GetValue<IEnumerable<SettingsStorage>>("Logs", (IEnumerable<SettingsStorage>)null);
        this.Logs = source2 != null ? source2.Select<SettingsStorage, StrategyLog>((Func<SettingsStorage, StrategyLog>)(s =>
        {
            return new StrategyLog()
            {
                CreationDate = TimeHelper.UtcKind(s.GetValue<DateTime>("CreationDate", new DateTime())),
                Level = s.GetValue<LogLevels>("Level", (LogLevels)0),
                Text = s.GetValue<string>("Text", (string)null)
            };
        })).ToArray<StrategyLog>() : (StrategyLog[])null;
        this.Error = storage.GetValue<StrategyErrorInfo>("Error", (StrategyErrorInfo)null);
    }

    void IPersistable.Save(SettingsStorage storage)
    {
        SettingsStorage settingsStorage1 = storage.Set<string[]>("Types", this.Types);
        StrategyOptimizationReportItem[] items = this.Items;
        SettingsStorage[] array1 = items != null ? ((IEnumerable<StrategyOptimizationReportItem>)items).Select<StrategyOptimizationReportItem, SettingsStorage>((Func<StrategyOptimizationReportItem, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("Items", array1);
        StrategyLog[] logs = this.Logs;
        SettingsStorage[] array2 = logs != null ? ((IEnumerable<StrategyLog>)logs).Select<StrategyLog, SettingsStorage>((Func<StrategyLog, SettingsStorage>)(e => new SettingsStorage().Set<DateTime>("CreationDate", e.CreationDate).Set<LogLevels>("Level", e.Level).Set<string>("Text", e.Text))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        settingsStorage2.Set<SettingsStorage[]>("Logs", array2).Set<StrategyErrorInfo>("Error", this.Error);
    }
}
