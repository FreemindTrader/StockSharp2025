// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBacktestChartItem
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public class StrategyBacktestChartItem : IPersistable
{
    public DateTime TimeStamp { get; set; }

    public StrategyCandleValue[] CandlesValues { get; set; }

    public StrategyIndicatorValue[] IndicatorsValues { get; set; }

    public StrategyOrder[] Orders { get; set; }

    public StrategyTrade[] Trades { get; set; }

    void IPersistable.Load(SettingsStorage storage)
    {
        this.TimeStamp = TimeHelper.UtcKind(storage.GetValue<DateTime>("TimeStamp", new DateTime()));
        IEnumerable<SettingsStorage> source1 = storage.GetValue<IEnumerable<SettingsStorage>>("CandlesValues", (IEnumerable<SettingsStorage>)null);
        this.CandlesValues = source1 != null ? source1.Select<SettingsStorage, StrategyCandleValue>((Func<SettingsStorage, StrategyCandleValue>)(s => PersistableHelper.Load<StrategyCandleValue>(s))).ToArray<StrategyCandleValue>() : (StrategyCandleValue[])null;
        IEnumerable<SettingsStorage> source2 = storage.GetValue<IEnumerable<SettingsStorage>>("IndicatorsValues", (IEnumerable<SettingsStorage>)null);
        this.IndicatorsValues = source2 != null ? source2.Select<SettingsStorage, StrategyIndicatorValue>((Func<SettingsStorage, StrategyIndicatorValue>)(s => PersistableHelper.Load<StrategyIndicatorValue>(s))).ToArray<StrategyIndicatorValue>() : (StrategyIndicatorValue[])null;
        IEnumerable<SettingsStorage> source3 = storage.GetValue<IEnumerable<SettingsStorage>>("Orders", (IEnumerable<SettingsStorage>)null);
        this.Orders = source3 != null ? source3.Select<SettingsStorage, StrategyOrder>((Func<SettingsStorage, StrategyOrder>)(s => new StrategyOrder()
        {
            SystemId = s.GetValue<string>("SystemId", (string)null),
            UserId = s.GetValue<string>("UserId", (string)null),
            Side = s.GetValue<Sides>("Side", (Sides)0),
            Price = s.GetValue<Decimal>("Price", 0M),
            Volume = s.GetValue<Decimal>("Volume", 0M),
            State = s.GetValue<OrderStates>("State", (OrderStates)0),
            ErrorMessage = s.GetValue<string>("ErrorMessage", (string)null)
        })).ToArray<StrategyOrder>() : (StrategyOrder[])null;
        IEnumerable<SettingsStorage> source4 = storage.GetValue<IEnumerable<SettingsStorage>>("Trades", (IEnumerable<SettingsStorage>)null);
        this.Trades = source4 != null ? source4.Select<SettingsStorage, StrategyTrade>((Func<SettingsStorage, StrategyTrade>)(s => new StrategyTrade()
        {
            Order = new StrategyOrder()
            {
                Side = s.GetValue<Sides>("Order", (Sides)0)
            },
            SystemId = s.GetValue<string>("SystemId", (string)null),
            UserId = s.GetValue<string>("UserId", (string)null),
            Price = s.GetValue<Decimal>("Price", 0M),
            Volume = s.GetValue<Decimal>("Volume", 0M)
        })).ToArray<StrategyTrade>() : (StrategyTrade[])null;
    }

    void IPersistable.Save(SettingsStorage storage)
    {
        SettingsStorage settingsStorage1 = storage.Set<DateTime>("TimeStamp", this.TimeStamp);
        StrategyCandleValue[] candlesValues = this.CandlesValues;
        SettingsStorage[] array1 = candlesValues != null ? ((IEnumerable<StrategyCandleValue>)candlesValues).Select<StrategyCandleValue, SettingsStorage>((Func<StrategyCandleValue, SettingsStorage>)(p => PersistableHelper.Save((IPersistable)p))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("CandlesValues", array1);
        StrategyIndicatorValue[] indicatorsValues = this.IndicatorsValues;
        SettingsStorage[] array2 = indicatorsValues != null ? ((IEnumerable<StrategyIndicatorValue>)indicatorsValues).Select<StrategyIndicatorValue, SettingsStorage>((Func<StrategyIndicatorValue, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage3 = settingsStorage2.Set<SettingsStorage[]>("IndicatorsValues", array2);
        StrategyOrder[] orders = this.Orders;
        SettingsStorage[] array3 = orders != null ? ((IEnumerable<StrategyOrder>)orders).Select<StrategyOrder, SettingsStorage>((Func<StrategyOrder, SettingsStorage>)(o => new SettingsStorage().Set<string>("SystemId", o.SystemId).Set<string>("UserId", o.UserId).Set<Sides>("Side", o.Side).Set<Decimal>("Price", o.Price).Set<Decimal>("Volume", o.Volume).Set<OrderStates>("State", o.State).Set<string>("ErrorMessage", o.ErrorMessage))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage4 = settingsStorage3.Set<SettingsStorage[]>("Orders", array3);
        StrategyTrade[] trades = this.Trades;
        SettingsStorage[] array4 = trades != null ? ((IEnumerable<StrategyTrade>)trades).Select<StrategyTrade, SettingsStorage>((Func<StrategyTrade, SettingsStorage>)(t => new SettingsStorage().Set<string>("SystemId", t.SystemId).Set<Sides>("Order", TypeHelper.CheckOnNull<StrategyOrder>(t.Order, "Order").Side).Set<string>("UserId", t.UserId).Set<Decimal>("Price", t.Price).Set<Decimal>("Volume", t.Volume))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        settingsStorage4.Set<SettingsStorage[]>("Trades", array4);
    }
}
