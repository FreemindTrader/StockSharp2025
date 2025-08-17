// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyOptimizationReportItem
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public class StrategyOptimizationReportItem : IPersistable
{
    public int[] ParamTypeIdxs { get; set; }

    public StrategyParam[] Params { get; set; }

    public (DateTime time, StrategyPnL value)[] PnL { get; set; }

    public StrategyStatParameter[] StatParameters { get; set; }

    void IPersistable.Load(SettingsStorage storage)
    {
        this.ParamTypeIdxs = storage.GetValue<int[]>("ParamTypeIdxs", (int[])null);
        IEnumerable<SettingsStorage> source1 = storage.GetValue<IEnumerable<SettingsStorage>>("Params", (IEnumerable<SettingsStorage>)null);
        this.Params = source1 != null ? source1.Select<SettingsStorage, StrategyParam>((Func<SettingsStorage, StrategyParam>)(s => new StrategyParam()
        {
            Name = s.GetValue<string>("Name", (string)null),
            UserId = s.GetValue<string>("UserId", (string)null),
            Value = s.GetValue<string>("Value", (string)null)
        })).ToArray<StrategyParam>() : (StrategyParam[])null;
        IEnumerable<SettingsStorage> source2 = storage.GetValue<IEnumerable<SettingsStorage>>("PnL", (IEnumerable<SettingsStorage>)null);
        this.PnL = source2 != null ? source2.Select<SettingsStorage, (DateTime, StrategyPnL)>((Func<SettingsStorage, (DateTime, StrategyPnL)>)(s => (s.GetValue<DateTime>("time", new DateTime()), s.GetValue<StrategyPnL>("value", (StrategyPnL)null)))).ToArray<(DateTime, StrategyPnL)>() : ((DateTime, StrategyPnL)[])null;
        IEnumerable<SettingsStorage> source3 = storage.GetValue<IEnumerable<SettingsStorage>>("StatParameters", (IEnumerable<SettingsStorage>)null);
        this.StatParameters = source3 != null ? source3.Select<SettingsStorage, StrategyStatParameter>((Func<SettingsStorage, StrategyStatParameter>)(s => PersistableHelper.Load<StrategyStatParameter>(s))).ToArray<StrategyStatParameter>() : (StrategyStatParameter[])null;
    }

    void IPersistable.Save(SettingsStorage storage)
    {
        SettingsStorage settingsStorage1 = storage.Set<int[]>("ParamTypeIdxs", this.ParamTypeIdxs);
        StrategyParam[] strategyParamArray = this.Params;
        SettingsStorage[] settingsStorageArray1 = strategyParamArray != null ? ((IEnumerable<StrategyParam>)strategyParamArray).Select<StrategyParam, SettingsStorage>((Func<StrategyParam, SettingsStorage>)(p => new SettingsStorage().Set<string>("Name", p.Name).Set<string>("UserId", p.UserId).Set<string>("Value", p.Value))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("Params", settingsStorageArray1).Set<SettingsStorage[]>("PnL", ((IEnumerable<ValueTuple<DateTime, StrategyPnL>>)this.PnL).Select<ValueTuple<DateTime, StrategyPnL>, SettingsStorage>((Func<ValueTuple<DateTime, StrategyPnL>, SettingsStorage>)(p => new SettingsStorage().Set<DateTime>("time", p.Item1).Set<StrategyPnL>("value", p.Item2))).ToArray<SettingsStorage>());
        StrategyStatParameter[] statParameters = this.StatParameters;
        SettingsStorage[] settingsStorageArray2 = statParameters != null ? ((IEnumerable<StrategyStatParameter>)statParameters).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        settingsStorage2.Set<SettingsStorage[]>("StatParameters", settingsStorageArray2);
    }
}
