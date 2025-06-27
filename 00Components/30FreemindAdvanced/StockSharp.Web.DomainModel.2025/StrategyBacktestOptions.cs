// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyBacktestOptions
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Logging;
using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
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

        public virtual void Load( SettingsStorage storage )
        {
            this.PerDay = ( int ) storage.GetValue<int>( "PerDay", 0 );
            this.Delay = ( TimeSpan ) storage.GetValue<TimeSpan>( "Delay", new TimeSpan() );
            this.MaxDuration = ( TimeSpan ) storage.GetValue<TimeSpan>( "MaxDuration", new TimeSpan() );
            this.MaxPeriod = ( TimeSpan ) storage.GetValue<TimeSpan>( "MaxPeriod", new TimeSpan() );
            this.Group = ( StrategyGroups ) storage.GetValue<StrategyGroups>( "Group", 0 );
            this.AllowContent = ( ProductContentTypes2 ) storage.GetValue<ProductContentTypes2>( "AllowContent", 0L );
            this.MaxParallel = ( int ) storage.GetValue<int>( "MaxParallel", 0 );
            this.MaxMessageCount = ( int ) storage.GetValue<int>( "MaxMessageCount", 0 );
            this.LogLevel = ( LogLevels ) storage.GetValue<LogLevels>( "LogLevel", 0 );
            this.LogMax = ( int ) storage.GetValue<int>( "LogMax", 0 );
            this.ChartMax = ( int ) storage.GetValue<int>( "ChartMax", 0 );
            this.LogTextMax = ( int ) storage.GetValue<int>( "LogTextMax", 0 );
            this.CommentMax = ( int ) storage.GetValue<int>( "CommentMax", 0 );
            this.AllowCustomRefs = ( bool ) storage.GetValue<bool>( "AllowCustomRefs", false );
            this.AllowDataTypes = ( BaseEntitySet<DataType> ) storage.GetValue<BaseEntitySet<DataType>>( "AllowDataTypes", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<int>( "PerDay", this.PerDay ).Set<TimeSpan>( "Delay", this.Delay ).Set<TimeSpan>( "MaxDuration", this.MaxDuration ).Set<TimeSpan>( "MaxPeriod", this.MaxPeriod ).Set<StrategyGroups>( "Group", this.Group ).Set<ProductContentTypes2>( "AllowContent", this.AllowContent ).Set<int>( "MaxParallel", this.MaxParallel ).Set<int>( "MaxMessageCount", this.MaxMessageCount ).Set<LogLevels>( "LogLevel", this.LogLevel ).Set<int>( "LogMax", this.LogMax ).Set<int>( "ChartMax", this.ChartMax ).Set<int>( "LogTextMax", this.LogTextMax ).Set<int>( "CommentMax", this.CommentMax ).Set<bool>( "AllowCustomRefs", ( this.AllowCustomRefs ) ).Set<BaseEntitySet<DataType>>( "AllowDataTypes", this.AllowDataTypes );
        }
    }
}
