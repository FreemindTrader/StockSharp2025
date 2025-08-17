using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace StockSharp.Studio.Core;

public class StudioNotificationSettings : StudioCommonSettings
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "OrderChange", Description = "OrderChange", GroupName = "Notification", Order = 40)]
    public AlertNotifications? OrderChanged { get; set; }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Matching", Description = "OrderMatched2", GroupName = "Notification", Order = 41)]
    public AlertNotifications? OrderMatched { get; set; } = new AlertNotifications?(AlertNotifications.Popup);

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Error", Description = "OrderError", GroupName = "Notification", Order = 42)]
    public AlertNotifications? OrderError { get; set; } = new AlertNotifications?(AlertNotifications.Popup);

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Lost", Description = "ConnectionLost", GroupName = "Notification", Order = 43)]
    public AlertNotifications? ConnectionLost { get; set; } = new AlertNotifications?(AlertNotifications.Popup);

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Restored", Description = "ConnectionRestored", GroupName = "Notification", Order = 44)]
    public AlertNotifications? ConnectionRestored { get; set; }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Subscription", Description = "SubscribedError2", GroupName = "Notification", Order = 45)]
    public AlertNotifications? SubscriptionError { get; set; } = new AlertNotifications?(AlertNotifications.Popup);

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Telegram", Description = "TelegramAlerts", GroupName = "Notification", Order = 46)]
    [Editor(typeof(ITelegramChannelEditor), typeof(ITelegramChannelEditor))]
    public ITelegramChannel TelegramChannel { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.OrderChanged = storage.GetValue<AlertNotifications?>("OrderChanged", this.OrderChanged);
        this.OrderMatched = storage.GetValue<AlertNotifications?>("OrderMatched", this.OrderMatched);
        this.OrderError = storage.GetValue<AlertNotifications?>("OrderError", this.OrderError);
        this.ConnectionLost = storage.GetValue<AlertNotifications?>("ConnectionLost", this.ConnectionLost);
        this.ConnectionRestored = storage.GetValue<AlertNotifications?>("ConnectionRestored", this.ConnectionRestored);
        this.SubscriptionError = storage.GetValue<AlertNotifications?>("SubscriptionError", this.SubscriptionError);
        long? nullable = storage.GetValue<long?>("TelegramChannel", new long?());
        ref long? local = ref nullable;
        this.TelegramChannel = local.HasValue ? local.GetValueOrDefault().TryFindChannel() : (ITelegramChannel)null;
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<AlertNotifications?>("OrderChanged", this.OrderChanged).Set<AlertNotifications?>("OrderMatched", this.OrderMatched).Set<AlertNotifications?>("OrderError", this.OrderError).Set<AlertNotifications?>("ConnectionLost", this.ConnectionLost).Set<AlertNotifications?>("ConnectionRestored", this.ConnectionRestored).Set<AlertNotifications?>("SubscriptionError", this.SubscriptionError).Set<long?>("TelegramChannel", this.TelegramChannel?.Id);
    }
}
