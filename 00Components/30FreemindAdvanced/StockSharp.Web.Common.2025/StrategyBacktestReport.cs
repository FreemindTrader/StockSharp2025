// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBacktestReport
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.Common
{
    public class StrategyBacktestReport : StrategyBaseReport, IPersistable
    {
        public StrategyCandle [ ] Candles { get; set; }

        public StrategyIndicator [ ] Indicators { get; set; }

        public StrategyOrder [ ] Orders { get; set; }

        public StrategyTrade [ ] Trades { get; set; }

        public StrategyPosition [ ] Positions { get; set; }

        public StrategyEquityChange [ ] Equity { get; set; }

        public StrategyStatParameter [ ] StatParameters { get; set; }

        public StrategyStatParameter [ ] StatParametersLong { get; set; }

        public StrategyStatParameter [ ] StatParametersShort { get; set; }

        public StrategyBacktestChartArea [ ] ChartAreas { get; set; }

        void IPersistable.Load( SettingsStorage storage )
        {
            this.Candles = Load<StrategyCandle>( "Candles" );
            this.Indicators = Load<StrategyIndicator>( "Indicators" );
            this.Orders = Load2<StrategyOrder>( "Orders", ( Func<SettingsStorage, StrategyOrder> ) ( s =>
            {
                return new StrategyOrder()
                {
                    CreationDate = TimeHelper.UtcKind( ( DateTime ) s.GetValue<DateTime>( "CreationDate",  new DateTime() ) ),
                    SystemId = ( string ) s.GetValue<string>( "SystemId",  null ),
                    UserId = ( string ) s.GetValue<string>( "UserId",  null ),
                    Side = ( Sides ) s.GetValue<Sides>( "Side",  0 ),
                    Price = ( Decimal ) s.GetValue<Decimal>( "Price",  Decimal.Zero ),
                    Type = ( OrderTypes ) s.GetValue<OrderTypes>( "Type",  0 ),
                    Volume = ( Decimal ) s.GetValue<Decimal>( "Volume",  Decimal.Zero ),
                    Balance = ( Decimal ) s.GetValue<Decimal>( "Balance",  Decimal.Zero ),
                    State = ( OrderStates ) s.GetValue<OrderStates>( "State",  0 ),
                    Name = ( string ) s.GetValue<string>( "Name",  null ),
                    Security = ( ( string ) s.GetValue<string>( "Security",  null ) ).ToInstrumentInfo(),
                    ErrorMessage = ( string ) s.GetValue<string>( "ErrorMessage",  null ),
                    Account = new StrategyAccount()
                    {
                        Name = ( string ) s.GetValue<string>( "Account",  null )
                    },
                    Tif = ( TimeInForce? ) s.GetValue<TimeInForce?>( "Tif",  new TimeInForce?() ),
                    Till = ( DateTime? ) s.GetValue<DateTime?>( "Till",  new DateTime?() ),
                    PositionEffect = ( OrderPositionEffects? ) s.GetValue<OrderPositionEffects?>( "PositionEffect",  new OrderPositionEffects?() ),
                    PostOnly = ( bool? ) s.GetValue<bool?>( "PostOnly",  new bool?() ),
                    VisibleVolume = ( Decimal? ) s.GetValue<Decimal?>( "VisibleVolume",  new Decimal?() ),
                    CancelledTime = ( DateTime? ) s.GetValue<DateTime?>( "CancelledTime",  new DateTime?() ),
                    MatchedTime = ( DateTime? ) s.GetValue<DateTime?>( "MatchedTime",  new DateTime?() )
                };
            } ) );
            this.Trades = Load2<StrategyTrade>( "Trades", ( Func<SettingsStorage, StrategyTrade> ) ( s =>
            {
                return new StrategyTrade()
                {
                    CreationDate = TimeHelper.UtcKind( ( DateTime ) s.GetValue<DateTime>( "CreationDate",  new DateTime() ) ),
                    SystemId = ( string ) s.GetValue<string>( "SystemId",  null ),
                    UserId = ( string ) s.GetValue<string>( "UserId",  null ),
                    Order = new StrategyOrder()
                    {
                        UserId = ( string ) s.GetValue<string>( "Order",  null )
                    },
                    Price = ( Decimal ) s.GetValue<Decimal>( "Price",  Decimal.Zero ),
                    Volume = ( Decimal ) s.GetValue<Decimal>( "Volume",  Decimal.Zero )
                };
            } ) );
            this.Positions = Load2<StrategyPosition>( "Positions", ( Func<SettingsStorage, StrategyPosition> ) ( s1 =>
            {
                StrategyPosition strategyPosition = new StrategyPosition();
                strategyPosition.CreationDate = TimeHelper.UtcKind( ( DateTime ) s1.GetValue<DateTime>( "CreationDate",  new DateTime() ) );
                strategyPosition.Security = ( ( string ) s1.GetValue<string>( "Security",  null ) ).ToInstrumentInfo();
                var m0 = s1.GetValue<IEnumerable<SettingsStorage>>("Changes",  null);
                strategyPosition.Changes = m0 != null ? ( ( IEnumerable<SettingsStorage> ) m0 ).Select<SettingsStorage, StrategyPositionChange>( ( Func<SettingsStorage, StrategyPositionChange> ) ( s2 =>
          {
              return new StrategyPositionChange()
              {
                  CreationDate = TimeHelper.UtcKind( ( DateTime ) s2.GetValue<DateTime>( "CreationDate",  new DateTime() ) ),
                  Value = ( Decimal ) s2.GetValue<Decimal>( "Value",  Decimal.Zero )
              };
          } ) ).ToArray<StrategyPositionChange>() : ( StrategyPositionChange [ ] ) null;
                return strategyPosition;
            } ) );
            this.Equity = Load2<StrategyEquityChange>( "Equity", ( Func<SettingsStorage, StrategyEquityChange> ) ( s =>
            {
                return new StrategyEquityChange()
                {
                    CreationDate = TimeHelper.UtcKind( ( DateTime ) s.GetValue<DateTime>( "CreationDate",  new DateTime() ) ),
                    PnL = ( StrategyPnL ) s.GetValue<StrategyPnL>( "PnL",  null )
                };
            } ) );
            this.StatParameters = Load<StrategyStatParameter>( "StatParameters" );
            this.StatParametersLong = Load<StrategyStatParameter>( "StatParametersLong" );
            this.StatParametersShort = Load<StrategyStatParameter>( "StatParametersShort" );
            this.ChartAreas = Load<StrategyBacktestChartArea>( "ChartAreas" );
            this.Logs = Load2<StrategyLog>( "Logs", ( Func<SettingsStorage, StrategyLog> ) ( s =>
            {
                return new StrategyLog()
                {
                    CreationDate = TimeHelper.UtcKind( ( DateTime ) s.GetValue<DateTime>( "CreationDate",  new DateTime() ) ),
                    Level = ( LogLevels ) s.GetValue<LogLevels>( "Level",  0 ),
                    Text = ( string ) s.GetValue<string>( "Text",  null )
                };
            } ) );
            this.Error = ( StrategyErrorInfo ) storage.GetValue<StrategyErrorInfo>( "Error",  null );

            T [ ] Load<T>( string name ) where T : IPersistable
            {
                return Load2<T>( name, ( Func<SettingsStorage, T> ) ( s => PersistableHelper.Load<T>( s ) ) );
            }

            T [ ] Load2<T>( string name, Func<SettingsStorage, T> creator ) where T : IPersistable
            {
                var m0 = storage.GetValue<IEnumerable<SettingsStorage>>(name,  null);
                if ( m0 == null )
                    return ( T [ ] ) null;
                return ( ( IEnumerable<SettingsStorage> ) m0 ).Select<SettingsStorage, T>( creator ).ToArray<T>();
            }
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            SettingsStorage settingsStorage1 = storage;
            StrategyCandle[] candles = this.Candles;
            SettingsStorage[] settingsStorageArray1 = candles != null ? ((IEnumerable<StrategyCandle>) candles).Select<StrategyCandle, SettingsStorage>((Func<StrategyCandle, SettingsStorage>) (p => PersistableHelper.Save((IPersistable) p))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("Candles",  settingsStorageArray1);
            StrategyIndicator[] indicators = this.Indicators;
            SettingsStorage[] settingsStorageArray2 = indicators != null ? ((IEnumerable<StrategyIndicator>) indicators).Select<StrategyIndicator, SettingsStorage>((Func<StrategyIndicator, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage3 = settingsStorage2.Set<SettingsStorage[]>("Indicators",  settingsStorageArray2);
            StrategyOrder[] orders = this.Orders;
            SettingsStorage[] settingsStorageArray3 = orders != null ? ((IEnumerable<StrategyOrder>) orders).Select<StrategyOrder, SettingsStorage>((Func<StrategyOrder, SettingsStorage>) (o => new SettingsStorage().Set<DateTime>("CreationDate",  o.CreationDate).Set<string>("SystemId",  o.SystemId).Set<string>("UserId",  o.UserId).Set<Sides>("Side",  o.Side).Set<Decimal>("Price",  o.Price).Set<OrderTypes>("Type",  o.Type).Set<Decimal>("Volume",  o.Volume).Set<Decimal>("Balance",  o.Balance).Set<OrderStates>("State",  o.State).Set<string>("Name",  o.Name).Set<string>("Security",  o.Security.ToStringId()).Set<string>("ErrorMessage",  o.ErrorMessage).Set<string>("Account",  ((StrategyAccount) TypeHelper.CheckOnNull<StrategyAccount>( o.Account, "Account")).Name).Set<TimeInForce?>("Tif",  o.Tif).Set<DateTime?>("Till",  o.Till).Set<OrderPositionEffects?>("PositionEffect",  o.PositionEffect).Set<bool?>("PostOnly",  o.PostOnly).Set<Decimal?>("VisibleVolume",  o.VisibleVolume).Set<DateTime?>("MatchedTime",  o.MatchedTime).Set<DateTime?>("CancelledTime",  o.CancelledTime))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage4 = settingsStorage3.Set<SettingsStorage[]>("Orders",  settingsStorageArray3);
            StrategyTrade[] trades = this.Trades;
            SettingsStorage[] settingsStorageArray4 = trades != null ? ((IEnumerable<StrategyTrade>) trades).Select<StrategyTrade, SettingsStorage>((Func<StrategyTrade, SettingsStorage>) (t => new SettingsStorage().Set<DateTime>("CreationDate",  t.CreationDate).Set<string>("SystemId",  t.SystemId).Set<string>("UserId",  t.UserId).Set<string>("Order",  ((StrategyOrder) TypeHelper.CheckOnNull<StrategyOrder>( t.Order, "Order")).UserId).Set<Decimal>("Price",  t.Price).Set<Decimal>("Volume",  t.Volume))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage5 = settingsStorage4.Set<SettingsStorage[]>("Trades",  settingsStorageArray4);
            StrategyPosition[] positions = this.Positions;
            SettingsStorage[] settingsStorageArray5 = positions != null ? ((IEnumerable<StrategyPosition>) positions).Select<StrategyPosition, SettingsStorage>((Func<StrategyPosition, SettingsStorage>) (p =>
      {
          SettingsStorage settingsStorage6 = new SettingsStorage().Set<DateTime>("CreationDate",  p.CreationDate).Set<string>("Security",  p.Security.ToStringId());
          StrategyPositionChange[] changes = p.Changes;
          SettingsStorage[] settingsStorageArray6 = changes != null ? ((IEnumerable<StrategyPositionChange>) changes).Select<StrategyPositionChange, SettingsStorage>((Func<StrategyPositionChange, SettingsStorage>) (c => new SettingsStorage().Set<DateTime>("CreationDate",  c.CreationDate).Set<Decimal>("Value",  c.Value))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
          return settingsStorage6.Set<SettingsStorage[]>("Changes",  settingsStorageArray6);
      })).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage7 = settingsStorage5.Set<SettingsStorage[]>("Positions",  settingsStorageArray5);
            StrategyEquityChange[] equity = this.Equity;
            SettingsStorage[] settingsStorageArray7 = equity != null ? ((IEnumerable<StrategyEquityChange>) equity).Select<StrategyEquityChange, SettingsStorage>((Func<StrategyEquityChange, SettingsStorage>) (p => new SettingsStorage().Set<DateTime>("CreationDate",  p.CreationDate).Set<StrategyPnL>("PnL",  p.PnL))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage8 = settingsStorage7.Set<SettingsStorage[]>("Equity",  settingsStorageArray7);
            StrategyStatParameter[] statParameters = this.StatParameters;
            SettingsStorage[] settingsStorageArray8 = statParameters != null ? ((IEnumerable<StrategyStatParameter>) statParameters).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage9 = settingsStorage8.Set<SettingsStorage[]>("StatParameters",  settingsStorageArray8);
            StrategyStatParameter[] statParametersLong = this.StatParametersLong;
            SettingsStorage[] settingsStorageArray9 = statParametersLong != null ? ((IEnumerable<StrategyStatParameter>) statParametersLong).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage10 = settingsStorage9.Set<SettingsStorage[]>("StatParametersLong",  settingsStorageArray9);
            StrategyStatParameter[] statParametersShort = this.StatParametersShort;
            SettingsStorage[] settingsStorageArray10 = statParametersShort != null ? ((IEnumerable<StrategyStatParameter>) statParametersShort).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage11 = settingsStorage10.Set<SettingsStorage[]>("StatParametersShort",  settingsStorageArray10);
            StrategyBacktestChartArea[] chartAreas = this.ChartAreas;
            SettingsStorage[] settingsStorageArray11 = chartAreas != null ? ((IEnumerable<StrategyBacktestChartArea>) chartAreas).Select<StrategyBacktestChartArea, SettingsStorage>((Func<StrategyBacktestChartArea, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage12 = settingsStorage11.Set<SettingsStorage[]>("ChartAreas",  settingsStorageArray11);
            StrategyLog[] logs = this.Logs;
            SettingsStorage[] settingsStorageArray12 = logs != null ? ((IEnumerable<StrategyLog>) logs).Select<StrategyLog, SettingsStorage>((Func<StrategyLog, SettingsStorage>) (e => new SettingsStorage().Set<DateTime>("CreationDate",  e.CreationDate).Set<LogLevels>("Level",  e.Level).Set<string>("Text",  e.Text))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            settingsStorage12.Set<SettingsStorage [ ]>( "Logs",  settingsStorageArray12 ).Set<StrategyErrorInfo>( "Error",  this.Error );
        }
    }
}
