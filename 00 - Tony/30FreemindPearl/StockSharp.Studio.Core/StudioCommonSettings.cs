// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioCommonSettings
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Common;
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

        public virtual void Load( SettingsStorage storage )
        {
            this.IsDark = storage.GetValue<bool>( "IsDark", this.IsDark );
            this.TimeZone = storage.GetValue<string>( "TimeZone", ( string )null ).To<TimeZoneInfo>();
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue<bool>( "IsDark", this.IsDark );
            storage.SetValue<string>( "TimeZone", this.TimeZone.To<string>() );
        }
    }
}
