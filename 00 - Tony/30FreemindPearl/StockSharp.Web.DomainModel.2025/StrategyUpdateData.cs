// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyUpdateData
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
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

        public virtual void Load( SettingsStorage storage )
        {
            this.ExecMode = ( StrategyExecutionModes ) storage.GetValue<StrategyExecutionModes>( "ExecMode", 0 );
            this.Strategy = ( Strategy ) storage.GetValue<Strategy>( "Strategy", null );
            this.Order = ( StrategyOrder ) storage.GetValue<StrategyOrder>( "Order", null );
            this.Trade = ( StrategyTrade ) storage.GetValue<StrategyTrade>( "Trade", null );
            this.Position = ( StrategyPosition ) storage.GetValue<StrategyPosition>( "Position", null );
            this.WaitQueueNum = ( int? ) storage.GetValue<int?>( "WaitQueueNum", new int?() );
            this.WaitAvgTime = ( TimeSpan? ) storage.GetValue<TimeSpan?>( "WaitAvgTime", new TimeSpan?() );
            this.CancelReason = ( StrategyCancelReasons? ) storage.GetValue<StrategyCancelReasons?>( "CancelReason", new StrategyCancelReasons?() );
            this.Error = ( StrategyErrorInfo ) storage.GetValue<StrategyErrorInfo>( "Error", null );
            this.Log = ( StrategyLog ) storage.GetValue<StrategyLog>( "Log", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<StrategyExecutionModes>( "ExecMode", this.ExecMode ).Set<Strategy>( "Strategy", this.Strategy ).Set<StrategyOrder>( "Order", this.Order ).Set<StrategyTrade>( "Trade", this.Trade ).Set<StrategyPosition>( "Position", this.Position ).Set<int?>( "WaitQueueNum", this.WaitQueueNum ).Set<TimeSpan?>( "WaitAvgTime", this.WaitAvgTime ).Set<StrategyCancelReasons?>( "CancelReason", this.CancelReason ).Set<StrategyErrorInfo>( "Error", this.Error ).Set<StrategyLog>( "Log", this.Log );
        }
    }
}
