// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBacktestReport
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public class StrategyBacktestReport : StrategyBaseReport, IPersistable
{
    public StrategyCandle[] Candles { get; set; }

    public StrategyIndicator[] Indicators { get; set; }

    public StrategyOrder[] Orders { get; set; }

    public StrategyTrade[] Trades { get; set; }

    public StrategyPosition[] Positions { get; set; }

    public StrategyEquityChange[] Equity { get; set; }

    public StrategyStatParameter[] StatParameters { get; set; }

    public StrategyStatParameter[] StatParametersLong { get; set; }

    public StrategyStatParameter[] StatParametersShort { get; set; }

    public StrategyBacktestChartArea[] ChartAreas { get; set; }

    void IPersistable.Load(SettingsStorage storage)
    {
        this.Candles = Load<StrategyCandle>("Candles");
        this.Indicators = Load<StrategyIndicator>("Indicators");
        this.Orders = Load2<StrategyOrder>("Orders", (Func<SettingsStorage, StrategyOrder>)(s =>
        {
            return new StrategyOrder()
            {
                CreationDate = TimeHelper.UtcKind(s.GetValue<DateTime>("CreationDate", new DateTime())),
                SystemId = s.GetValue<string>("SystemId", (string)null),
                UserId = s.GetValue<string>("UserId", (string)null),
                Side = s.GetValue<Sides>("Side", (Sides)0),
                Price = s.GetValue<Decimal>("Price", 0M),
                Type = s.GetValue<OrderTypes>("Type", (OrderTypes)0),
                Volume = s.GetValue<Decimal>("Volume", 0M),
                Balance = s.GetValue<Decimal>("Balance", 0M),
                State = s.GetValue<OrderStates>("State", (OrderStates)0),
                Name = s.GetValue<string>("Name", (string)null),
                Security = s.GetValue<string>("Security", (string)null).ToInstrumentInfo(),
                ErrorMessage = s.GetValue<string>("ErrorMessage", (string)null),
                Account = new StrategyAccount()
                {
                    Name = s.GetValue<string>("Account", (string)null)
                },
                Tif = s.GetValue<TimeInForce?>("Tif", new TimeInForce?()),
                Till = s.GetValue<DateTime?>("Till", new DateTime?()),
                PositionEffect = s.GetValue<OrderPositionEffects?>("PositionEffect", new OrderPositionEffects?()),
                PostOnly = s.GetValue<bool?>("PostOnly", new bool?()),
                VisibleVolume = s.GetValue<Decimal?>("VisibleVolume", new Decimal?()),
                CancelledTime = s.GetValue<DateTime?>("CancelledTime", new DateTime?()),
                MatchedTime = s.GetValue<DateTime?>("MatchedTime", new DateTime?())
            };
        }));
        this.Trades = Load2<StrategyTrade>("Trades", (Func<SettingsStorage, StrategyTrade>)(s =>
        {
            return new StrategyTrade()
            {
                CreationDate = TimeHelper.UtcKind(s.GetValue<DateTime>("CreationDate", new DateTime())),
                SystemId = s.GetValue<string>("SystemId", (string)null),
                UserId = s.GetValue<string>("UserId", (string)null),
                Order = new StrategyOrder()
                {
                    UserId = s.GetValue<string>("Order", (string)null)
                },
                Price = s.GetValue<Decimal>("Price", 0M),
                Volume = s.GetValue<Decimal>("Volume", 0M)
            };
        }));
        this.Positions = Load2<StrategyPosition>("Positions", (Func<SettingsStorage, StrategyPosition>)(s1 =>
        {
            StrategyPosition strategyPosition = new StrategyPosition();
            strategyPosition.CreationDate = TimeHelper.UtcKind(s1.GetValue<DateTime>("CreationDate", new DateTime()));
            strategyPosition.Security = s1.GetValue<string>("Security", (string)null).ToInstrumentInfo();
            IEnumerable<SettingsStorage> source = s1.GetValue<IEnumerable<SettingsStorage>>("Changes", (IEnumerable<SettingsStorage>)null);
            strategyPosition.Changes = source != null ? source.Select<SettingsStorage, StrategyPositionChange>((Func<SettingsStorage, StrategyPositionChange>)(s2 =>
        {
            return new StrategyPositionChange()
            {
                CreationDate = TimeHelper.UtcKind(s2.GetValue<DateTime>("CreationDate", new DateTime())),
                Value = s2.GetValue<Decimal>("Value", 0M)
            };
        })).ToArray<StrategyPositionChange>() : (StrategyPositionChange[])null;
            return strategyPosition;
        }));
        this.Equity = Load2<StrategyEquityChange>("Equity", (Func<SettingsStorage, StrategyEquityChange>)(s =>
        {
            return new StrategyEquityChange()
            {
                CreationDate = TimeHelper.UtcKind(s.GetValue<DateTime>("CreationDate", new DateTime())),
                PnL = s.GetValue<StrategyPnL>("PnL", (StrategyPnL)null)
            };
        }));
        this.StatParameters = Load<StrategyStatParameter>("StatParameters");
        this.StatParametersLong = Load<StrategyStatParameter>("StatParametersLong");
        this.StatParametersShort = Load<StrategyStatParameter>("StatParametersShort");
        this.ChartAreas = Load<StrategyBacktestChartArea>("ChartAreas");
        this.Logs = Load2<StrategyLog>("Logs", (Func<SettingsStorage, StrategyLog>)(s =>
        {
            return new StrategyLog()
            {
                CreationDate = TimeHelper.UtcKind(s.GetValue<DateTime>("CreationDate", new DateTime())),
                Level = s.GetValue<LogLevels>("Level", (LogLevels)0),
                Text = s.GetValue<string>("Text", (string)null)
            };
        }));
        this.Error = storage.GetValue<StrategyErrorInfo>("Error", (StrategyErrorInfo)null);

        T[] Load<T>(string name) where T : IPersistable
        {
            return Load2<T>(name, (Func<SettingsStorage, T>)(s => PersistableHelper.Load<T>(s)));
        }

        T[] Load2<T>(string name, Func<SettingsStorage, T> creator) where T : IPersistable
        {
            IEnumerable<SettingsStorage> source = storage.GetValue<IEnumerable<SettingsStorage>>(name, (IEnumerable<SettingsStorage>)null);
            return source == null ? (T[])null : source.Select<SettingsStorage, T>(creator).ToArray<T>();
        }
    }

    void IPersistable.Save(SettingsStorage storage)
    {
        SettingsStorage settingsStorage1 = storage;
        StrategyCandle[] candles = this.Candles;
        SettingsStorage[] array1 = candles != null ? ((IEnumerable<StrategyCandle>)candles).Select<StrategyCandle, SettingsStorage>((Func<StrategyCandle, SettingsStorage>)(p => PersistableHelper.Save((IPersistable)p))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("Candles", array1);
        StrategyIndicator[] indicators = this.Indicators;
        SettingsStorage[] array2 = indicators != null ? ((IEnumerable<StrategyIndicator>)indicators).Select<StrategyIndicator, SettingsStorage>((Func<StrategyIndicator, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage3 = settingsStorage2.Set<SettingsStorage[]>("Indicators", array2);
        StrategyOrder[] orders = this.Orders;
        SettingsStorage[] array3 = orders != null ? ((IEnumerable<StrategyOrder>)orders).Select<StrategyOrder, SettingsStorage>((Func<StrategyOrder, SettingsStorage>)(o => new SettingsStorage().Set<DateTime>("CreationDate", o.CreationDate).Set<string>("SystemId", o.SystemId).Set<string>("UserId", o.UserId).Set<Sides>("Side", o.Side).Set<Decimal>("Price", o.Price).Set<OrderTypes>("Type", o.Type).Set<Decimal>("Volume", o.Volume).Set<Decimal>("Balance", o.Balance).Set<OrderStates>("State", o.State).Set<string>("Name", o.Name).Set<string>("Security", o.Security.ToStringId()).Set<string>("ErrorMessage", o.ErrorMessage).Set<string>("Account", TypeHelper.CheckOnNull<StrategyAccount>(o.Account, "Account").Name).Set<TimeInForce?>("Tif", o.Tif).Set<DateTime?>("Till", o.Till).Set<OrderPositionEffects?>("PositionEffect", o.PositionEffect).Set<bool?>("PostOnly", o.PostOnly).Set<Decimal?>("VisibleVolume", o.VisibleVolume).Set<DateTime?>("MatchedTime", o.MatchedTime).Set<DateTime?>("CancelledTime", o.CancelledTime))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage4 = settingsStorage3.Set<SettingsStorage[]>("Orders", array3);
        StrategyTrade[] trades = this.Trades;
        SettingsStorage[] array4 = trades != null ? ((IEnumerable<StrategyTrade>)trades).Select<StrategyTrade, SettingsStorage>((Func<StrategyTrade, SettingsStorage>)(t => new SettingsStorage().Set<DateTime>("CreationDate", t.CreationDate).Set<string>("SystemId", t.SystemId).Set<string>("UserId", t.UserId).Set<string>("Order", TypeHelper.CheckOnNull<StrategyOrder>(t.Order, "Order").UserId).Set<Decimal>("Price", t.Price).Set<Decimal>("Volume", t.Volume))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage5 = settingsStorage4.Set<SettingsStorage[]>("Trades", array4);
        StrategyPosition[] positions = this.Positions;
        SettingsStorage[] array5 = positions != null ? ((IEnumerable<StrategyPosition>)positions).Select<StrategyPosition, SettingsStorage>((Func<StrategyPosition, SettingsStorage>)(p =>
        {
            SettingsStorage settingsStorage6 = new SettingsStorage().Set<DateTime>("CreationDate", p.CreationDate).Set<string>("Security", p.Security.ToStringId());
            StrategyPositionChange[] changes = p.Changes;
            SettingsStorage[] array6 = changes != null ? ((IEnumerable<StrategyPositionChange>)changes).Select<StrategyPositionChange, SettingsStorage>((Func<StrategyPositionChange, SettingsStorage>)(c => new SettingsStorage().Set<DateTime>("CreationDate", c.CreationDate).Set<Decimal>("Value", c.Value))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
            return settingsStorage6.Set<SettingsStorage[]>("Changes", array6);
        })).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage7 = settingsStorage5.Set<SettingsStorage[]>("Positions", array5);
        StrategyEquityChange[] equity = this.Equity;
        SettingsStorage[] array7 = equity != null ? ((IEnumerable<StrategyEquityChange>)equity).Select<StrategyEquityChange, SettingsStorage>((Func<StrategyEquityChange, SettingsStorage>)(p => new SettingsStorage().Set<DateTime>("CreationDate", p.CreationDate).Set<StrategyPnL>("PnL", p.PnL))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage8 = settingsStorage7.Set<SettingsStorage[]>("Equity", array7);
        StrategyStatParameter[] statParameters = this.StatParameters;
        SettingsStorage[] array8 = statParameters != null ? ((IEnumerable<StrategyStatParameter>)statParameters).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage9 = settingsStorage8.Set<SettingsStorage[]>("StatParameters", array8);
        StrategyStatParameter[] statParametersLong = this.StatParametersLong;
        SettingsStorage[] array9 = statParametersLong != null ? ((IEnumerable<StrategyStatParameter>)statParametersLong).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage10 = settingsStorage9.Set<SettingsStorage[]>("StatParametersLong", array9);
        StrategyStatParameter[] statParametersShort = this.StatParametersShort;
        SettingsStorage[] array10 = statParametersShort != null ? ((IEnumerable<StrategyStatParameter>)statParametersShort).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage11 = settingsStorage10.Set<SettingsStorage[]>("StatParametersShort", array10);
        StrategyBacktestChartArea[] chartAreas = this.ChartAreas;
        SettingsStorage[] array11 = chartAreas != null ? ((IEnumerable<StrategyBacktestChartArea>)chartAreas).Select<StrategyBacktestChartArea, SettingsStorage>((Func<StrategyBacktestChartArea, SettingsStorage>)(e => PersistableHelper.Save((IPersistable)e))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        SettingsStorage settingsStorage12 = settingsStorage11.Set<SettingsStorage[]>("ChartAreas", array11);
        StrategyLog[] logs = this.Logs;
        SettingsStorage[] array12 = logs != null ? ((IEnumerable<StrategyLog>)logs).Select<StrategyLog, SettingsStorage>((Func<StrategyLog, SettingsStorage>)(e => new SettingsStorage().Set<DateTime>("CreationDate", e.CreationDate).Set<LogLevels>("Level", e.Level).Set<string>("Text", e.Text))).ToArray<SettingsStorage>() : (SettingsStorage[])null;
        settingsStorage12.Set<SettingsStorage[]>("Logs", array12).Set<StrategyErrorInfo>("Error", this.Error);
    }
}
