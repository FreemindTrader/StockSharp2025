// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioCommonSettings
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Studio.Core
{
    public class StudioCommonSettings : IPersistable
    {
        [Display( Description = "DarkTheme", GroupName = "Appearance", Name = "Dark", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsDark { get; set; } = true;

        [Display( Description = "MarketDataTimeZone", GroupName = "Appearance", Name = "TimeZone", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public TimeZoneInfo TimeZone { get; set; }

        [Display( Description = "Language", GroupName = "Appearance", Name = "Language", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        [ItemsSource( typeof( LanguageSource ) )]
        public string Language
        {
            get
            {
                return LocalizedStrings.ActiveLanguage;
            }
            set
            {
                LocalizedStrings.ActiveLanguage = value;
            }
        }

        [Display( Description = "ErrorsDialogs", GroupName = "General", Name = "Dialogs", Order = 100, ResourceType = typeof( LocalizedStrings ) )]
        public bool ErrorsDialogs { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.IsDark = ( bool ) storage.GetValue<bool>( "IsDark",  ( this.IsDark  ) );
            this.TimeZone = ( TimeZoneInfo ) Converter.To<TimeZoneInfo>( ( object ) storage.GetValue<string>( "TimeZone",  null ) );
            this.ErrorsDialogs = ( bool ) storage.GetValue<bool>( "ErrorsDialogs",  true );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue<bool>( "IsDark",  ( this.IsDark  ) );
            storage.SetValue<string>( "TimeZone", Converter.To<string>( ( object ) this.TimeZone ) );
            storage.SetValue<bool>( "ErrorsDialogs",  ( this.ErrorsDialogs ) );
        }
    }
}
