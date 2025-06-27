// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBacktestChartItem
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.Common
{
    public class StrategyBacktestChartItem : IPersistable
    {
        public DateTime TimeStamp { get; set; }

        public StrategyCandleValue [ ] CandlesValues { get; set; }

        public StrategyIndicatorValue [ ] IndicatorsValues { get; set; }

        public StrategyOrder [ ] Orders { get; set; }

        public StrategyTrade [ ] Trades { get; set; }

        void IPersistable.Load( SettingsStorage storage )
        {
            this.TimeStamp = TimeHelper.UtcKind( ( DateTime ) storage.GetValue<DateTime>( "TimeStamp",  new DateTime() ) );
            var candleValues = storage.GetValue<IEnumerable<SettingsStorage>>("CandlesValues",  null);
            this.CandlesValues = candleValues != null ? ( ( IEnumerable<SettingsStorage> ) candleValues ).Select<SettingsStorage, StrategyCandleValue>( ( Func<SettingsStorage, StrategyCandleValue> ) ( s => ( StrategyCandleValue ) PersistableHelper.Load<StrategyCandleValue>( s ) ) ).ToArray<StrategyCandleValue>() : ( StrategyCandleValue [ ] ) null;
            var indicatorsValues = storage.GetValue<IEnumerable<SettingsStorage>>("IndicatorsValues",  null);
            this.IndicatorsValues = indicatorsValues != null ? ( ( IEnumerable<SettingsStorage> ) indicatorsValues ).Select<SettingsStorage, StrategyIndicatorValue>( ( Func<SettingsStorage, StrategyIndicatorValue> ) ( s => ( StrategyIndicatorValue ) PersistableHelper.Load<StrategyIndicatorValue>( s ) ) ).ToArray<StrategyIndicatorValue>() : ( StrategyIndicatorValue [ ] ) null;
            var orders = storage.GetValue<IEnumerable<SettingsStorage>>("Orders",  null);
            this.Orders = orders != null ? ( ( IEnumerable<SettingsStorage> ) orders ).Select<SettingsStorage, StrategyOrder>( ( Func<SettingsStorage, StrategyOrder> ) ( s => new StrategyOrder()
            {
                SystemId = ( string ) s.GetValue<string>( "SystemId",  null ),
                UserId = ( string ) s.GetValue<string>( "UserId",  null ),
                Side = ( Sides ) s.GetValue<Sides>( "Side",  0 ),
                Price = ( Decimal ) s.GetValue<Decimal>( "Price",  Decimal.Zero ),
                Volume = ( Decimal ) s.GetValue<Decimal>( "Volume",  Decimal.Zero ),
                State = ( OrderStates ) s.GetValue<OrderStates>( "State",  0 ),
                ErrorMessage = ( string ) s.GetValue<string>( "ErrorMessage",  null )
            } ) ).ToArray<StrategyOrder>() : ( StrategyOrder [ ] ) null;
            var m0_4 = storage.GetValue<IEnumerable<SettingsStorage>>("Trades",  null);
            this.Trades = m0_4 != null ? ( ( IEnumerable<SettingsStorage> ) m0_4 ).Select<SettingsStorage, StrategyTrade>( ( Func<SettingsStorage, StrategyTrade> ) ( s => new StrategyTrade()
            {
                Order = new StrategyOrder()
                {
                    Side = ( Sides ) s.GetValue<Sides>( "Order",  0 )
                },
                SystemId = ( string ) s.GetValue<string>( "SystemId",  null ),
                UserId = ( string ) s.GetValue<string>( "UserId",  null ),
                Price = ( Decimal ) s.GetValue<Decimal>( "Price",  Decimal.Zero ),
                Volume = ( Decimal ) s.GetValue<Decimal>( "Volume",  Decimal.Zero )
            } ) ).ToArray<StrategyTrade>() : ( StrategyTrade [ ] ) null;
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            SettingsStorage settingsStorage1 = storage.Set<DateTime>("TimeStamp",  this.TimeStamp);
            StrategyCandleValue[] candlesValues = this.CandlesValues;
            SettingsStorage[] settingsStorageArray1 = candlesValues != null ? ((IEnumerable<StrategyCandleValue>) candlesValues).Select<StrategyCandleValue, SettingsStorage>((Func<StrategyCandleValue, SettingsStorage>) (p => PersistableHelper.Save((IPersistable) p))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("CandlesValues",  settingsStorageArray1);
            StrategyIndicatorValue[] indicatorsValues = this.IndicatorsValues;
            SettingsStorage[] settingsStorageArray2 = indicatorsValues != null ? ((IEnumerable<StrategyIndicatorValue>) indicatorsValues).Select<StrategyIndicatorValue, SettingsStorage>((Func<StrategyIndicatorValue, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage3 = settingsStorage2.Set<SettingsStorage[]>("IndicatorsValues",  settingsStorageArray2);
            StrategyOrder[] orders = this.Orders;
            SettingsStorage[] settingsStorageArray3 = orders != null ? ((IEnumerable<StrategyOrder>) orders).Select<StrategyOrder, SettingsStorage>((Func<StrategyOrder, SettingsStorage>) (o => new SettingsStorage().Set<string>("SystemId",  o.SystemId).Set<string>("UserId",  o.UserId).Set<Sides>("Side",  o.Side).Set<Decimal>("Price",  o.Price).Set<Decimal>("Volume",  o.Volume).Set<OrderStates>("State",  o.State).Set<string>("ErrorMessage",  o.ErrorMessage))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage4 = settingsStorage3.Set<SettingsStorage[]>("Orders",  settingsStorageArray3);
            StrategyTrade[] trades = this.Trades;
            SettingsStorage[] settingsStorageArray4 = trades != null ? ((IEnumerable<StrategyTrade>) trades).Select<StrategyTrade, SettingsStorage>((Func<StrategyTrade, SettingsStorage>) (t => new SettingsStorage().Set<string>("SystemId",  t.SystemId).Set<Sides>("Order",  ((StrategyOrder) TypeHelper.CheckOnNull<StrategyOrder>( t.Order, "Order")).Side).Set<string>("UserId",  t.UserId).Set<Decimal>("Price",  t.Price).Set<Decimal>("Volume",  t.Volume))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            settingsStorage4.Set<SettingsStorage [ ]>( "Trades",  settingsStorageArray4 );
        }
    }
}
