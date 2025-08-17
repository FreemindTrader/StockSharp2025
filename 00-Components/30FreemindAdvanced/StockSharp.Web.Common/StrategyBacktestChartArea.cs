// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBacktestChartArea
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.Common;

public class StrategyBacktestChartArea : IPersistable
{
    public string GroupId { get; set; }

    public StrategyBacktestChartItem[] Items { get; set; }

    public string OrdersTitle { get; set; }

    public string TradesTitle { get; set; }

    void IPersistable.Load(SettingsStorage storage)
    {
        this.GroupId = storage.GetValue<string>("GroupId", (string)null);
        this.Items = storage.GetValue<IEnumerable<SettingsStorage>>("Items", (IEnumerable<SettingsStorage>)null).Select<SettingsStorage, StrategyBacktestChartItem>((Func<SettingsStorage, StrategyBacktestChartItem>)(s => PersistableHelper.Load<StrategyBacktestChartItem>(s))).ToArray<StrategyBacktestChartItem>();
        this.OrdersTitle = storage.GetValue<string>("OrdersTitle", (string)null);
        this.TradesTitle = storage.GetValue<string>("TradesTitle", (string)null);
    }

    void IPersistable.Save(SettingsStorage storage)
    {
        storage.Set<string>("GroupId", this.GroupId).Set<SettingsStorage[]>("Items", ((IEnumerable<StrategyBacktestChartItem>)this.Items).Select<StrategyBacktestChartItem, SettingsStorage>((Func<StrategyBacktestChartItem, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>()).Set<string>("OrdersTitle", this.OrdersTitle).Set<string>("TradesTitle", this.TradesTitle);
    }
}
