using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fx.Charting
{
    public sealed class ChartPanelShareSettings : NotifiableObject, IPersistable
    {
        private string string_0 = "Chart_{0:yyyyMMdd_HHmmssfff}.png".Put( DateTime.Now );
        private TimeSpan timeSpan_0 = TimeSpan.FromMinutes( 5.0 );
        private bool bool_0;
        private bool bool_1;

        [Display( Description = "Str1999", GroupName = "General", Name = "Str1998", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsEnabled
        {
            get
            {
                return bool_0;
            }
            set
            {
                if( bool_0 == value )
                {
                    return;
                }
                bool_0 = value;
                NotifyChanged( nameof( IsEnabled ) );
            }
        }

        [Display( Description = "Str2000", GroupName = "General", Name = "Str736", Order = 20, ResourceType = typeof( LocalizedStrings ) )]
        [TimeSpanEditor( Mask = TimeSpanEditorMask.Days | TimeSpanEditorMask.Hours | TimeSpanEditorMask.Minutes | TimeSpanEditorMask.Seconds )]
        public TimeSpan Period
        {
            get
            {
                return timeSpan_0;
            }
            set
            {
                if( timeSpan_0 <= TimeSpan.Zero )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), "LocalizedStrings.Str2001" );
                }
                if( timeSpan_0 == value )
                {
                    return;
                }
                timeSpan_0 = value;
                NotifyChanged( nameof( Period ) );
            }
        }

        [Display( Description = "Str2003", GroupName = "General", Name = "Str2002", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public string FileName
        {
            get
            {
                return string_0;
            }
            set
            {
                if( string_0 == value )
                {
                    return;
                }
                Published = false;
                string_0 = value;
                NotifyChanged( nameof( FileName ) );
            }
        }

        [Browsable( false )]
        public bool Published
        {
            get
            {
                return bool_1;
            }
            set
            {
                if( bool_1 == value )
                {
                    return;
                }
                bool_1 = value;
                NotifyChanged( nameof( Published ) );
            }
        }

        public void Load( SettingsStorage storage )
        {
            Period = storage.GetValue( "Period", Period );
            FileName = storage.GetValue( "FileName", FileName );
            Published = storage.GetValue( "Published", Published );
            IsEnabled = storage.GetValue( "IsEnabled", IsEnabled );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "IsEnabled", IsEnabled );
            storage.SetValue( "Period", Period );
            storage.SetValue( "FileName", FileName );
            storage.SetValue( "Published", Published );
        }
    }
}
