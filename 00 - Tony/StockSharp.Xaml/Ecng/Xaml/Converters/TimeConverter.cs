using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.DateTimeOffset" /> or <see cref="T:System.DateTime" /> converter to a specified time zone.
    ///     </summary>
    public class TimeConverter : IValueConverter
    {
        
        private static TimeZoneInfo _globalTimeZone;
        
        private TimeZoneInfo _timeZone;

        /// <summary>Global time zone for whole app.</summary>
        public static TimeZoneInfo GlobalTimeZone
        {
            get
            {
                return TimeConverter._globalTimeZone;
            }
            set
            {
                TimeConverter._globalTimeZone = value;
            }
        }

        /// <summary>Time zone.</summary>
        public TimeZoneInfo TimeZone
        {
            get
            {
                return this._timeZone ?? TimeConverter.GlobalTimeZone;
            }
        }

        /// <summary>
        /// <see cref="P:Ecng.Xaml.Converters.TimeConverter.TimeZone" /> is <see cref="P:System.TimeZoneInfo.Local" />.
        ///     </summary>
        public bool ConvertToLocal
        {
            get
            {
                return TimeZoneInfo.Local.Equals( this._timeZone );
            }
            set
            {
                this._timeZone = value ? TimeZoneInfo.Local : ( TimeZoneInfo )null;
            }
        }

        /// <summary>
        /// Set <see cref="P:Ecng.Xaml.Converters.TimeConverter.TimeZone" /> by string id.
        /// </summary>
        public string TimeZoneId
        {
            get
            {
                return ( string )Converter.To<string>( ( object )this._timeZone );
            }
            set
            {
                this._timeZone = ( TimeZoneInfo )Converter.To<TimeZoneInfo>( ( object )value );
            }
        }

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            TimeZoneInfo timeZone = this.TimeZone;
            if ( value is DateTimeOffset )
            {
                DateTimeOffset dateTimeOffset = ( DateTimeOffset )value;
                return ( object )( timeZone == null ? dateTimeOffset : TimeZoneInfo.ConvertTime( dateTimeOffset, timeZone ) );
            }
            if ( !( value is DateTime ) )
                return Binding.DoNothing;
            DateTime dateTime = ( DateTime )value;
            return ( object )( timeZone == null ? dateTime : TimeZoneInfo.ConvertTime( dateTime, timeZone ) );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )( value == null ? TimeSpan.Zero : ( ( DateTime )value ).TimeOfDay );
        }
    }
}
