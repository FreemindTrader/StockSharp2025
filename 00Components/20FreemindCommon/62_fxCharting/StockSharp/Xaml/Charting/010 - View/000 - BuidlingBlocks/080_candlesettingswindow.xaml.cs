using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

public partial class CandleSettingsWindow : ThemedWindow, IComponentConnector
{
    private Subscription _subscription;

    public CandleSettingsWindow() => InitializeComponent();

    public Subscription Subscription
    {
        get => _subscription;
        set
        {
            _subscription = value;
            SubscriptionCtrl.SelectedObject = value == null ? (CandleSettingsWindow.SubscriptionBindingList)null : new CandleSettingsWindow.SubscriptionBindingList(value);
            OkBtn.IsEnabled = value != null;
        }
    }

    public ISecurityProvider SecurityProvider
    {
        get => SubscriptionCtrl.SecurityProvider;
        set => SubscriptionCtrl.SecurityProvider = value;
    }

    private void OkButtonClicked(object _param1, RoutedEventArgs _param2)
    {
        if ( !Subscription.SecurityId.HasValue )
        {
            new MessageBoxBuilder().Owner((Window)this).Error().Text(LocalizedStrings.SecurityNotSpecified).Show();
        }
        else
        {
            MarketDataMessage marketData = Subscription.MarketData;
            DateTimeOffset? from = marketData.From;
            DateTimeOffset? to = marketData.To;

            if ( ( from.HasValue & to.HasValue ? ( from.GetValueOrDefault() > to.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
            {
                new MessageBoxBuilder().Owner((Window)this).Error().Text(StringHelper.Put(LocalizedStrings.StartCannotBeMoreEnd, marketData.From, marketData.To )).Show();
            }
            else
                DialogResult = new bool?(true);
        }
    }



    private sealed class SubscriptionBindingList : NotifiableObject
    {

        private readonly MarketDataMessage _marketDataMsg;

        private Security _security;

        public SubscriptionBindingList(Subscription _param1)
        {
            _marketDataMsg = _param1 != null ? _param1.MarketData : throw new ArgumentNullException("subscription");
            DateTimeOffset? nullable = _marketDataMsg.From;
            if ( nullable.HasValue )
                return;
            nullable = _marketDataMsg.To;
            if ( nullable.HasValue )
                return;
            _marketDataMsg.From = new DateTimeOffset?((DateTimeOffset)DateTime.Today.Subtract(TimeSpan.FromDays(10.0)));
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "CandlesType", Description = "CandlesType", GroupName = "General", Order = 1)]
        [Editor(typeof(ICandleDataTypeEditor), typeof(ICandleDataTypeEditor))]
        public Messages.DataType CandleType
        {
            get => _marketDataMsg.DataType2;
            set
            {
                _marketDataMsg.DataType2 = value;
                if ( ( value != null ? ( !StockSharp.Messages.Extensions.IsBuildOnly(value.MessageType) ? 1 : 0 ) : 1 ) != 0 )
                    return;
                BuildMode = (MarketDataBuildModes)2;
                NotifyChanged("BuildMode");
            }
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Security", Description = "SecurityDot", GroupName = "General", Order = 0)]
        public Security Security
        {
            get => _security;
            set
            {
                _security = value;
                if ( value != null )
                {
                    EntitiesExtensions.ToMessage(value, null, 0L, false).CopyTo(_marketDataMsg);
                }                
            }
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "VolumeProfile", Description = "VolumeProfileCalc", GroupName = "General", Order = 2)]
        public bool IsCalcVolumeProfile
        {
            get => _marketDataMsg.IsCalcVolumeProfile;
            set
            {
                _marketDataMsg.IsCalcVolumeProfile = value;
                if ( !value )
                    return;
                BuildMode = (MarketDataBuildModes)2;
                NotifyChanged("BuildMode");
            }
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "From", Description = "StartDateDesc", GroupName = "General", Order = 3)]
        public DateTimeOffset? From
        {
            get => _marketDataMsg.From;
            set => _marketDataMsg.From = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Until", Description = "ToDateDesc", GroupName = "General", Order = 4)]
        public DateTimeOffset? To
        {
            get => _marketDataMsg.To;
            set => _marketDataMsg.To = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "SmallerTimeFrame", Description = "SmallerTimeFrameDesc", GroupName = "General", Order = 5)]
        public bool AllowBuildFromSmallerTimeFrame
        {
            get => _marketDataMsg.AllowBuildFromSmallerTimeFrame;
            set => _marketDataMsg.AllowBuildFromSmallerTimeFrame = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "RegularHours", Description = "RegularTradingHours", GroupName = "General", Order = 6)]
        public bool? IsRegularTradingHours
        {
            get => _marketDataMsg.IsRegularTradingHours;
            set => _marketDataMsg.IsRegularTradingHours = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Count", Description = "CandlesCount", GroupName = "General", Order = 7)]
        public long? Count
        {
            get => _marketDataMsg.Count;
            set => _marketDataMsg.Count = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Mode", Description = "BuildMode", GroupName = "Build", Order = 20)]
        public MarketDataBuildModes BuildMode
        {
            get => _marketDataMsg.BuildMode;
            set => _marketDataMsg.BuildMode = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Source", Description = "CandlesBuildSource", GroupName = "Build", Order = 21)]
        [ItemsSource(typeof(BuildCandlesFromSource))]
        public Messages.DataType BuildFrom
        {
            get => _marketDataMsg.BuildFrom;
            set => _marketDataMsg.BuildFrom = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Field", Description = "Level1Field", GroupName = "Build", Order = 22)]
        [ItemsSource(typeof(BuildCandlesFieldSource))]
        public Level1Fields? BuildField
        {
            get => _marketDataMsg.BuildField;
            set => _marketDataMsg.BuildField = value;
        }

        [Display(ResourceType = typeof(LocalizedStrings), Name = "Finished", Description = "Finished", GroupName = "Build", Order = 23)]
        public bool IsFinishedOnly
        {
            get => _marketDataMsg.IsFinishedOnly;
            set => _marketDataMsg.IsFinishedOnly = value;
        }
    }
}
