// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioNotificationSettings
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Studio.Core
{
    public class StudioNotificationSettings : StudioCommonSettings
    {
        [Display( Description = "OrderChange", GroupName = "Notification", Name = "OrderChange", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
        public AlertNotifications? OrderChanged { get; set; }

        [Display( Description = "OrderMatched2", GroupName = "Notification", Name = "Matching", Order = 41, ResourceType = typeof( LocalizedStrings ) )]
        public AlertNotifications? OrderMatched { get; set; } = new AlertNotifications?( AlertNotifications.Popup );

        [Display( Description = "OrderError", GroupName = "Notification", Name = "Error", Order = 42, ResourceType = typeof( LocalizedStrings ) )]
        public AlertNotifications? OrderError { get; set; } = new AlertNotifications?( AlertNotifications.Popup );

        [Display( Description = "ConnectionLost", GroupName = "Notification", Name = "Lost", Order = 43, ResourceType = typeof( LocalizedStrings ) )]
        public AlertNotifications? ConnectionLost { get; set; } = new AlertNotifications?( AlertNotifications.Popup );

        [Display( Description = "ConnectionRestored", GroupName = "Notification", Name = "Restored", Order = 44, ResourceType = typeof( LocalizedStrings ) )]
        public AlertNotifications? ConnectionRestored { get; set; }

        [Display( Description = "SubscribedError2", GroupName = "Notification", Name = "Subscription", Order = 45, ResourceType = typeof( LocalizedStrings ) )]
        public AlertNotifications? SubscriptionError { get; set; } = new AlertNotifications?( AlertNotifications.Popup );

        [Display( Description = "TelegramAlerts", GroupName = "Notification", Name = "Telegram", Order = 46, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( ITelegramChannelEditor ), typeof( ITelegramChannelEditor ) )]
        public ITelegramChannel TelegramChannel { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.OrderChanged = ( AlertNotifications? ) storage.GetValue<AlertNotifications?>( "OrderChanged",  this.OrderChanged );
            this.OrderMatched = ( AlertNotifications? ) storage.GetValue<AlertNotifications?>( "OrderMatched",  this.OrderMatched );
            this.OrderError = ( AlertNotifications? ) storage.GetValue<AlertNotifications?>( "OrderError",  this.OrderError );
            this.ConnectionLost = ( AlertNotifications? ) storage.GetValue<AlertNotifications?>( "ConnectionLost",  this.ConnectionLost );
            this.ConnectionRestored = ( AlertNotifications? ) storage.GetValue<AlertNotifications?>( "ConnectionRestored",  this.ConnectionRestored );
            this.SubscriptionError = ( AlertNotifications? ) storage.GetValue<AlertNotifications?>( "SubscriptionError",  this.SubscriptionError );
            long? nullable = (long?) storage.GetValue<long?>("TelegramChannel",  new long?());
            ref long? local = ref nullable;
            this.TelegramChannel = local.HasValue ? local.GetValueOrDefault().TryFindChannel() : ( ITelegramChannel ) null;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<AlertNotifications?>( "OrderChanged",  this.OrderChanged ).Set<AlertNotifications?>( "OrderMatched",  this.OrderMatched ).Set<AlertNotifications?>( "OrderError",  this.OrderError ).Set<AlertNotifications?>( "ConnectionLost",  this.ConnectionLost ).Set<AlertNotifications?>( "ConnectionRestored",  this.ConnectionRestored ).Set<AlertNotifications?>( "SubscriptionError",  this.SubscriptionError ).Set<long?>( "TelegramChannel",  this.TelegramChannel?.Id );
        }
    }
}
