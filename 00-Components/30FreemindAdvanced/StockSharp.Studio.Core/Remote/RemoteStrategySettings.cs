using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace StockSharp.Studio.Core.Remote;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class RemoteStrategySettings : NotifiableObject, IPersistable
{
    private bool _remoteControl = true;
    private ITelegramChannel _remoteChannel;
    private LogLevels _remoteLogLevel = (LogLevels)5;
    private bool _logOrders = true;
    private bool _logTrades = true;
    private bool _logPositions = true;
    private bool _logEquity = true;

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Enabled", Description = "RemoteControl", GroupName = "Remotely", Order = 1000)]
    public bool RemoteControl
    {
        get => this._remoteControl;
        set
        {
            if (this._remoteControl == value)
                return;
            this._remoteControl = value;
            this.NotifyChanged(nameof(RemoteControl));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Notification", Description = "NotificationChannel", GroupName = "Remotely", Order = 1001)]
    [Editor(typeof(ITelegramChannelEditor), typeof(ITelegramChannelEditor))]
    public ITelegramChannel RemoteChannel
    {
        get => this._remoteChannel;
        set
        {
            if (this._remoteChannel == value)
                return;
            this._remoteChannel = value;
            this.NotifyChanged(nameof(RemoteChannel));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "LogLevel", Description = "LogLevelDot", GroupName = "Remotely", Order = 1002)]
    public LogLevels RemoteLogLevel
    {
        get => this._remoteLogLevel;
        set
        {
            if (this._remoteLogLevel == value)
                return;
            this._remoteLogLevel = value;
            this.NotifyChanged(nameof(RemoteLogLevel));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Orders", Description = "Logging", GroupName = "Remotely", Order = 1003)]
    public bool LogOrders
    {
        get => this._logOrders;
        set
        {
            if (this._logOrders == value)
                return;
            this._logOrders = value;
            this.NotifyChanged(nameof(LogOrders));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Trades", Description = "Logging", GroupName = "Remotely", Order = 1004)]
    public bool LogTrades
    {
        get => this._logTrades;
        set
        {
            if (this._logTrades == value)
                return;
            this._logTrades = value;
            this.NotifyChanged(nameof(LogTrades));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Positions", Description = "Logging", GroupName = "Remotely", Order = 1005)]
    public bool LogPositions
    {
        get => this._logPositions;
        set
        {
            if (this._logPositions == value)
                return;
            this._logPositions = value;
            this.NotifyChanged(nameof(LogPositions));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "PnL", Description = "Logging", GroupName = "Remotely", Order = 1006)]
    public bool LogEquity
    {
        get => this._logEquity;
        set
        {
            if (this._logEquity == value)
                return;
            this._logEquity = value;
            this.NotifyChanged(nameof(LogEquity));
        }
    }

    public virtual void Load(SettingsStorage storage)
    {
        this.RemoteControl = storage.GetValue<bool>("RemoteControl", this.RemoteControl);
        this.RemoteLogLevel = storage.GetValue<LogLevels>("RemoteLogLevel", this.RemoteLogLevel);
        long? nullable = storage.GetValue<long?>("RemoteChannel", new long?());
        ref long? local = ref nullable;
        this.RemoteChannel = local.HasValue ? local.GetValueOrDefault().TryFindChannel() : (ITelegramChannel)null;
        this.LogOrders = storage.GetValue<bool>("LogOrders", this.LogOrders);
        this.LogTrades = storage.GetValue<bool>("LogTrades", this.LogTrades);
        this.LogPositions = storage.GetValue<bool>("LogPositions", this.LogPositions);
        this.LogEquity = storage.GetValue<bool>("LogEquity", this.LogEquity);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<bool>("RemoteControl", this.RemoteControl).Set<LogLevels>("RemoteLogLevel", this.RemoteLogLevel).Set<long?>("RemoteChannel", this.RemoteChannel?.Id).Set<bool>("LogOrders", this.LogOrders).Set<bool>("LogTrades", this.LogTrades).Set<bool>("LogPositions", this.LogPositions).Set<bool>("LogEquity", this.LogEquity);
    }
}
