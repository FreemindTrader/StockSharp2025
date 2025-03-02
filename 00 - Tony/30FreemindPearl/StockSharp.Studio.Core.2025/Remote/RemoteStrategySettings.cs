// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Remote.RemoteStrategySettings
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Studio.Core.Remote
{
    [TypeConverter( typeof( ExpandableObjectConverter ) )]
    public class RemoteStrategySettings : NotifiableObject, IPersistable
    {
        private bool _remoteControl;
        private ITelegramChannel _remoteChannel;
        private LogLevels _remoteLogLevel;
        private bool _logOrders;
        private bool _logTrades;
        private bool _logPositions;
        private bool _logEquity;

        [Display( Description = "RemoteControl", GroupName = "Remotely", Name = "Enabled", Order = 1000, ResourceType = typeof( LocalizedStrings ) )]
        public bool RemoteControl
        {
            get
            {
                return this._remoteControl;
            }
            set
            {
                if ( this._remoteControl == value )
                    return;
                this._remoteControl = value;
                this.NotifyChanged( nameof( RemoteControl ) );
            }
        }

        [Display( Description = "NotificationChannel", GroupName = "Remotely", Name = "Notification", Order = 1001, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( ITelegramChannelEditor ), typeof( ITelegramChannelEditor ) )]
        public ITelegramChannel RemoteChannel
        {
            get
            {
                return this._remoteChannel;
            }
            set
            {
                if ( this._remoteChannel == value )
                    return;
                this._remoteChannel = value;
                this.NotifyChanged( nameof( RemoteChannel ) );
            }
        }

        [Display( Description = "LogLevelDot", GroupName = "Remotely", Name = "LogLevel", Order = 1002, ResourceType = typeof( LocalizedStrings ) )]
        public LogLevels RemoteLogLevel
        {
            get
            {
                return this._remoteLogLevel;
            }
            set
            {
                if ( this._remoteLogLevel == value )
                    return;
                this._remoteLogLevel = value;
                this.NotifyChanged( nameof( RemoteLogLevel ) );
            }
        }

        [Display( Description = "Logging", GroupName = "Remotely", Name = "Orders", Order = 1003, ResourceType = typeof( LocalizedStrings ) )]
        public bool LogOrders
        {
            get
            {
                return this._logOrders;
            }
            set
            {
                if ( this._logOrders == value )
                    return;
                this._logOrders = value;
                this.NotifyChanged( nameof( LogOrders ) );
            }
        }

        [Display( Description = "Logging", GroupName = "Remotely", Name = "Trades", Order = 1004, ResourceType = typeof( LocalizedStrings ) )]
        public bool LogTrades
        {
            get
            {
                return this._logTrades;
            }
            set
            {
                if ( this._logTrades == value )
                    return;
                this._logTrades = value;
                this.NotifyChanged( nameof( LogTrades ) );
            }
        }

        [Display( Description = "Logging", GroupName = "Remotely", Name = "Positions", Order = 1005, ResourceType = typeof( LocalizedStrings ) )]
        public bool LogPositions
        {
            get
            {
                return this._logPositions;
            }
            set
            {
                if ( this._logPositions == value )
                    return;
                this._logPositions = value;
                this.NotifyChanged( nameof( LogPositions ) );
            }
        }

        [Display( Description = "Logging", GroupName = "Remotely", Name = "PnL", Order = 1006, ResourceType = typeof( LocalizedStrings ) )]
        public bool LogEquity
        {
            get
            {
                return this._logEquity;
            }
            set
            {
                if ( this._logEquity == value )
                    return;
                this._logEquity = value;
                this.NotifyChanged( nameof( LogEquity ) );
            }
        }

        public virtual void Load( SettingsStorage storage )
        {
            this.RemoteControl = ( bool ) storage.GetValue<bool>( "RemoteControl",  this.RemoteControl );
            this.RemoteLogLevel = ( LogLevels ) storage.GetValue<LogLevels>( "RemoteLogLevel",  this.RemoteLogLevel );
            long? nullable = (long?) storage.GetValue<long?>("RemoteChannel",  new long?());
            ref long? local = ref nullable;
            this.RemoteChannel = local.HasValue ? local.GetValueOrDefault().TryFindChannel() : ( ITelegramChannel ) null;
            this.LogOrders = ( bool ) storage.GetValue<bool>( "LogOrders", this.LogOrders );
            this.LogTrades = ( bool ) storage.GetValue<bool>( "LogTrades", this.LogTrades );
            this.LogPositions = ( bool ) storage.GetValue<bool>( "LogPositions", this.LogPositions );
            this.LogEquity = ( bool ) storage.GetValue<bool>( "LogEquity", this.LogEquity );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<bool>( "RemoteControl", this.RemoteControl ).Set<LogLevels>( "RemoteLogLevel",  this.RemoteLogLevel ).Set<long?>( "RemoteChannel",  this.RemoteChannel?.Id ).Set<bool>( "LogOrders", this.LogOrders ).Set<bool>( "LogTrades", this.LogTrades ).Set<bool>( "LogPositions", this.LogPositions ).Set<bool>( "LogEquity", this.LogEquity );
        }

        public RemoteStrategySettings()
        {            
        }
    }
}
