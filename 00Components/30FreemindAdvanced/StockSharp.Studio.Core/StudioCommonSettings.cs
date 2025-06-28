using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace StockSharp.Studio.Core;

public class StudioCommonSettings : IPersistable
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Dark", Description = "DarkTheme", GroupName = "Appearance", Order = 0)]
    public bool IsDark { get; set; } = true;

    [Display(ResourceType = typeof(LocalizedStrings), Name = "TimeZone", Description = "MarketDataTimeZone", GroupName = "Appearance", Order = 1)]
    public TimeZoneInfo TimeZone { get; set; }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Language", Description = "Language", GroupName = "Appearance", Order = 2)]
    [ItemsSource(typeof(LanguageSource))]
    public string Language
    {
        get => LocalizedStrings.ActiveLanguage;
        set => LocalizedStrings.ActiveLanguage = value;
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Dialogs", Description = "ErrorsDialogs", GroupName = "General", Order = 100)]
    public bool ErrorsDialogs { get; set; }

    public virtual void Load(SettingsStorage storage)
    {
        this.IsDark = storage.GetValue<bool>("IsDark", this.IsDark);
        this.TimeZone = Converter.To<TimeZoneInfo>((object)storage.GetValue<string>("TimeZone", (string)null));
        this.ErrorsDialogs = storage.GetValue<bool>("ErrorsDialogs", true);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.SetValue<bool>("IsDark", this.IsDark);
        storage.SetValue<string>("TimeZone", Converter.To<string>((object)this.TimeZone));
        storage.SetValue<bool>("ErrorsDialogs", this.ErrorsDialogs);
    }
}
