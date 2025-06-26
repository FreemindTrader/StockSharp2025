//********************************************************************************************
// This code based on public domain code by Sergey Stoyan.
//********************************************************************************************

using fx.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace fx.Definitions
{
    public interface IPublicHolidayProvider
    {
        IEnumerable<PublicHoliday> Get( int year );
    }

    public static class MyTimeUnit
    {
        // NOTE: currently not supported
        //public const string Nanoseconds  = "n";

        // NOTE: currently not supported
        //public const string Microseconds = "u";

        public const string Milliseconds   = "ms";

        public const string Seconds        = "s";

        public const string Minutes        = "m";

        public const string Hours          = "h";
    }

    public abstract class CatholicBaseProvider : IPublicHolidayProvider
    {
        public abstract IEnumerable<PublicHoliday> Get( int year );

        /// <summary>
        /// Get Catholic easter for requested year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DateTime EasterSunday( int year )
        {
            //http://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year

            var g = year % 19;
            var c = year / 100;
            var h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
            var i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));

            var day = i - ((year + year / 4 + i + 2 - c + c / 4 ) % 7) + 28;
            var month = 3;

            if ( day > 31 )
            {
                month++;
                day -= 31;
            }

            return new DateTime( year, month, day );
        }

        /// <summary>
        /// Get advent sunday for requested year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DateTime AdventSunday( int year )
        {
            var christmasDate = new DateTime(year, 12, 24);
            var daysToAdvent = 21 + (int)christmasDate.DayOfWeek;

            return christmasDate.AddDays( -daysToAdvent );
        }
    }

    public enum CountryCode
    {
        AD,
        AR,
        AT,
        AU,
        BE,
        BG,
        BO,
        BR,
        BS,
        BW,
        BY,
        CA,
        CH,
        CL,
        //CN,
        CO,
        CR,
        CU,
        CY,
        CZ,
        DE,
        DK,
        DO,
        EC,
        EE,
        ES,
        FI,
        FR,
        GB,
        GL,
        GR,
        GT,
        HN,
        HR,
        HT,
        HU,
        IE,
        IM,
        IS,
        IT,
        JE,
        JM,
        LI,
        LT,
        LU,
        LV,
        MC,
        MG,
        MT,
        NA,
        NL,
        NO,
        NZ,
        PA,
        PE,
        PL,
        PR,
        PT,
        PY,
        RO,
        RU,
        SE,
        SI,
        SK,
        TR,
        VE,
        US,
        UY,
        ZA
    }


    public enum PublicHolidayType : int
    {
        Public      = 1, //public holiday
        Bank        = 2, //bank holiday, banks and offices are closed
        School      = 4, //school holiday, schools are closed
        Authorities = 8, //authorities are closed
        Optional    = 16, //majority of people take a day off
        Observance  = 32, //optional festivity, no paid day off
    }

    public class PublicHoliday
    {
        public DateTime Date { get; set; }
        public string LocalName { get; set; }
        public string Name { get; set; }
        //ISO 3166-1 alpha-2
        public CountryCode CountryCode { get; set; }
        public bool Fixed { get; set; }
        public bool CountyOfficialHoliday { get; set; }
        public bool CountyAdministrationHoliday { get; set; }
        public bool Global { get { return Counties?.Length > 0 ? false : true; } }
        //ISO-3166-2
        public string [ ] Counties { get; set; }
        public PublicHolidayType Type { get; set; }
        public int? LaunchYear { get; set; }

        /// <summary>
        /// Add Public Holiday (fixed is true)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="localName"></param>
        /// <param name="englishName"></param>
        /// <param name="countryCode">ISO 3166-1 ALPHA-2</param>
        /// <param name="launchYear"></param>
        /// <param name="counties">ISO-3166-2</param>
        /// <param name="countyOfficialHoliday"></param>
        /// <param name="countyAdministrationHoliday"></param>
        public PublicHoliday( int year, int month, int day, string localName, string englishName, CountryCode countryCode, int? launchYear = null, string [ ] counties = null, bool countyOfficialHoliday = true, bool countyAdministrationHoliday = true, PublicHolidayType type = PublicHolidayType.Public )
        {
            Date                        = new DateTime( year, month, day );
            LocalName                   = localName;
            Name                        = englishName;
            CountryCode                 = countryCode;
            Fixed                       = true;
            CountyOfficialHoliday       = countyOfficialHoliday;
            CountyAdministrationHoliday = countyAdministrationHoliday;
            Type                        = type;
            LaunchYear                  = launchYear;

            if ( counties?.Length > 0 )
            {
                Counties = counties;
            }
        }

        /// <summary>
        /// Add Public Holiday (fixed is false)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="localName"></param>
        /// <param name="englishName"></param>
        /// <param name="countryCode">ISO 3166-1 ALPHA-2</param>
        /// <param name="launchYear"></param>
        /// <param name="counties">ISO-3166-2</param>
        /// <param name="countyOfficialHoliday"></param>
        /// <param name="countyAdministrationHoliday"></param>
        public PublicHoliday( DateTime date, string localName, string englishName, CountryCode countryCode, int? launchYear = null, string [ ] counties = null, bool countyOfficialHoliday = true, bool countyAdministrationHoliday = true, PublicHolidayType type = PublicHolidayType.Public )
        {
            Date                        = date;
            LocalName                   = localName;
            Name                        = englishName;
            CountryCode                 = countryCode;
            Fixed                       = false;
            CountyOfficialHoliday       = countyOfficialHoliday;
            CountyAdministrationHoliday = countyAdministrationHoliday;
            Type                        = type;
            LaunchYear                  = launchYear;

            if ( counties?.Length > 0 )
            {
                Counties = counties;
            }
        }

        public override string ToString( )
        {
            return $"{Date:yyyy-MM-dd} {Name}";
        }
    }



    public static class DateTimeExtension
    {
        public static bool IsWeekend( this DateTime dateTime, CountryCode countryCode )
        {
            //For feature weekend is different need countryCode
            //https://en.wikipedia.org/wiki/Workweek_and_weekend

            if ( dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday )
            {
                return true;
            }

            return false;
        }

        public static DateTime Shift( this DateTime value, Func<DateTime, DateTime> saturday, Func<DateTime, DateTime> sunday, Func<DateTime, DateTime> monday = null )
        {
            var daysOff = new PooledList<DateTime>();
            switch ( value.DayOfWeek )
            {
                case DayOfWeek.Saturday:
                    return saturday.Invoke( value );
                case DayOfWeek.Sunday:
                    return sunday.Invoke( value );
                case DayOfWeek.Monday:
                    if ( monday != null )
                    {
                        return monday.Invoke( value );
                    }
                    break;
                default:
                    break;
            }

            return value;
        }
    }

    /// <summary>
    /// Miscellaneous and parsing thread-safe methods for DateTime,
    /// class helps with parsing multiple date time formats.
    /// </summary>
    /// 
    public static class DateTimeHelper
    {
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime Truncate( this DateTime dateTime, TimeSpan timeSpan )
        {
            if ( timeSpan == TimeSpan.Zero ) return dateTime; // Or could throw an ArgumentException
            return dateTime.AddTicks( -( dateTime.Ticks % timeSpan.Ticks ) );
        }

        private static object lock_variable = new object( );

        
        #region miscellaneous methods

        public static uint GetSecondsSinceEpochTime( DateTime date_time )
        {
            lock ( lock_variable )
            {
                TimeSpan t = date_time - new DateTime( 1970, 1, 1 );
                int ss = ( int ) t.TotalSeconds;
                if ( ss < 0 )
                {
                    return 0;
                }
                return ( uint ) ss;
            }
        }

        #endregion

        public static DateTime NextFirday( )
        {
            var from = DateTime.UtcNow;

            int start = (int)from.DayOfWeek;
            int target = 5;
            if ( target <= start )
                target += 7;

            return from.AddDays( target - start );
        }

        public static DateTime LastDateOfMonth()
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);

            return lastDay;
        }

        public static IEnumerable<DateTime> ReturnNextNthWeekdaysOfMonth( DateTime dt, DayOfWeek weekday, int amounttoshow = 4 )
        {
            while ( dt.DayOfWeek != weekday )
                dt = dt.AddDays( 1 );

            for ( int i = 0 ; i < amounttoshow ; i++ )
            {
                yield return dt;
                dt = dt.AddDays( 7 );
            }
        }

        
        public static DateTime PreviousWorkingDate( DateTime inputDate )
        {
            DateTime outputDate = inputDate.AddDays( -1 );

            if ( outputDate.DayOfWeek != DayOfWeek.Saturday && outputDate.DayOfWeek != DayOfWeek.Sunday )
            {
                return outputDate;
            }

            do
            {
                outputDate = outputDate.AddDays( -1 );
            }
            while ( outputDate.DayOfWeek == DayOfWeek.Saturday || outputDate.DayOfWeek == DayOfWeek.Sunday );

            return outputDate;

        }

        public static DateTime FirstDayOfWeek( this DateTime dt )
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if ( diff < 0 )
            {
                diff += 7;
            }

            return dt.AddDays( -diff ).Date;
        }

        public static DateTime LastDayOfWeek( this DateTime dt ) => dt.FirstDayOfWeek().AddDays( 6 );


        public static DateTime GetLastDayOfMonth( this DateTime dateTime )
        {
            return new DateTime( dateTime.Year, dateTime.Month, DateTime.DaysInMonth( dateTime.Year, dateTime.Month ), dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc );
        }

        public static DateTime GetFirstDayOfMonth( this DateTime dateTime )
        {
            return new DateTime( dateTime.Year, dateTime.Month, 1, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc );
        }

        public static DateTime RoundUp( this DateTime dt, TimeSpan d )
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime( dt.Ticks + delta, dt.Kind );
        }

        public static DateTime RoundDown( this DateTime dt, TimeSpan d )
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime( dt.Ticks - delta, dt.Kind );
        }

        public static DateTime RoundToNearest( this DateTime dt, TimeSpan d )
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return new DateTime( dt.Ticks + offset - delta, dt.Kind );
        }

        #region parsing definitions

        /// <summary>
        /// Defines a substring where date-time was found and result of conversion
        /// </summary>
        public class ParsedDateTime
        {
            public readonly int IndexOfDate = -1;
            public readonly int LengthOfDate = -1;
            public readonly int IndexOfTime = -1;
            public readonly int LengthOfTime = -1;
            public readonly DateTime DateTime;

            /// <summary>
            /// True if a date was found within string
            /// </summary>
            public readonly bool IsDateFound;

            /// <summary>
            /// True if a time was found within string
            /// </summary>
            public readonly bool IsTimeFound;

            internal ParsedDateTime( int index_of_date, int length_of_date, int index_of_time, int length_of_time, DateTime date_time )
            {
                IndexOfDate  = index_of_date;
                LengthOfDate = length_of_date;
                IndexOfTime  = index_of_time;
                LengthOfTime = length_of_time;
                DateTime     = date_time;
                IsDateFound  = index_of_date > -1;
                IsTimeFound  = index_of_time > -1;
            }
        }

        /// <summary>
        /// Date that is accepted in the following cases:
        /// - no date was parsed by TryParse();
        /// - no year was found by TryParseDate();
        /// It is ignored when DefaultDateIsCurrent = true
        /// </summary>
        public static DateTime DefaultDate
        {
            set { _DefaultDate = value; }
            get
            {
                if ( DefaultDateIsNow )
                {
                    return DateTime.Now;
                }
                else
                {
                    return _DefaultDate;
                }
            }
        }

        private static DateTime _DefaultDate = DateTime.Now;

        /// <summary>
        /// If true then DefaultDate property is ignored and DefaultDate is always DateTime.Now
        /// </summary>
        public static bool DefaultDateIsNow = true;

        /// <summary>
        /// Defines default date-time format.
        /// </summary>
        public enum DateTimeFormat
        {
            /// <summary>
            /// month number goes before day number
            /// </summary>
            USA_DATE,

            /// <summary>
            /// day number goes before month number
            /// </summary>
            UK_DATE,
            ///// <summary>
            ///// time is specifed through AM or PM
            ///// </summary>
            //USA_TIME,
        }

        #endregion

        #region parsing derived methods for DateTime output

        /// <summary>
        /// Tries to find date and time within the passed string and return it as DateTime structure.
        /// </summary>
        /// <param name="str">string that contains date and(or) time</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="date_time">parsed date-time output</param>
        /// <returns>true if both date and time were found, else false</returns>
        public static bool TryParseDateTime( string str, DateTimeFormat default_format, out DateTime date_time )
        {
            lock ( lock_variable )
            {
                ParsedDateTime parsed_date_time;
                if ( !TryParseDateTime( str, default_format, out parsed_date_time ) )
                {
                    date_time = new DateTime( 1, 1, 1 );
                    return false;
                }
                date_time = parsed_date_time.DateTime;
                return true;
            }
        }

        /// <summary>
        /// Tries to find date and(or) time within the passed string and return it as DateTime structure.
        /// If only date was found, time in the returned DateTime is always 0:0:0.
        /// If only time was found, date in the returned DateTime is DefaultDate.
        /// </summary>
        /// <param name="str">string that contains date and(or) time</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="date_time">parsed date-time output</param>
        /// <returns>true if date and(or) time was found, else false</returns>
        public static bool TryParse( string str, DateTimeFormat default_format, out DateTime date_time )
        {
            lock ( lock_variable )
            {
                ParsedDateTime parsed_date_time;
                if ( !TryParse( str, default_format, out parsed_date_time ) )
                {
                    date_time = new DateTime( 1, 1, 1 );
                    return false;
                }
                date_time = parsed_date_time.DateTime;
                return true;
            }
        }

        /// <summary>
        /// Tries to find time within the passed string and return it as DateTime structure.
        /// It recognizes only time while ignoring date, so date in the returned DateTime is always 1/1/1.
        /// </summary>
        /// <param name="str">string that contains time</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="time">parsed time output</param>
        /// <returns>true if time was found, else false</returns>
        public static bool TryParseTime( string str, DateTimeFormat default_format, out DateTime time )
        {
            lock ( lock_variable )
            {
                ParsedDateTime parsed_time;
                if ( !TryParseTime( str, default_format, out parsed_time, null ) )
                {
                    time = new DateTime( 1, 1, 1 );
                    return false;
                }
                time = parsed_time.DateTime;
                return true;
            }
        }

        /// <summary>
        /// Tries to find date within the passed string and return it as DateTime structure.
        /// It recognizes only date while ignoring time, so time in the returned DateTime is always 0:0:0.
        /// If year of the date was not found then it accepts the current year.
        /// </summary>
        /// <param name="str">string that contains date</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="date">parsed date output</param>
        /// <returns>true if date was found, else false</returns>
        public static bool TryParseDate( string str, DateTimeFormat default_format, out DateTime date )
        {
            lock ( lock_variable )
            {
                ParsedDateTime parsed_date;
                if ( !TryParseDate( str, default_format, out parsed_date ) )
                {
                    date = new DateTime( 1, 1, 1 );
                    return false;
                }
                date = parsed_date.DateTime;
                return true;
            }
        }

        #endregion

        #region parsing derived methods for ParsedDateTime output

        /// <summary>
        /// Tries to find date and time within the passed string and return it as ParsedDateTime object.
        /// </summary>
        /// <param name="str">string that contains date-time</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="parsed_date_time">parsed date-time output</param>
        /// <returns>true if both date and time were found, else false</returns>
        public static bool TryParseDateTime( string str, DateTimeFormat default_format, out ParsedDateTime parsed_date_time )
        {
            lock ( lock_variable )
            {
                if ( TryParse( str, DateTimeFormat.USA_DATE, out parsed_date_time ) &&
                     parsed_date_time.IsDateFound &&
                     parsed_date_time.IsTimeFound )
                {
                    return true;
                }

                parsed_date_time = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to find time within the passed string and return it as ParsedDateTime object.
        /// It recognizes only time while ignoring date, so date in the returned ParsedDateTime is always 1/1/1
        /// </summary>
        /// <param name="str">string that contains date-time</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="parsed_date_time">parsed date-time output</param>
        /// <returns>true if time was found, else false</returns>
        public static bool TryParseTime( string str, DateTimeFormat default_format, out ParsedDateTime parsed_time )
        {
            lock ( lock_variable )
            {
                return TryParseTime( str, default_format, out parsed_time, null );
            }
        }

        /// <summary>
        /// Tries to find date and(or) time within the passed string and return it as ParsedDateTime object.
        /// If only date was found, time in the returned ParsedDateTime is always 0:0:0.
        /// If only time was found, date in the returned ParsedDateTime is DefaultDate.
        /// </summary>
        /// <param name="str">string that contains date-time</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="parsed_date_time">parsed date-time output</param>
        /// <returns>true if date or time was found, else false</returns>
        public static bool TryParse( string str, DateTimeFormat default_format, out ParsedDateTime parsed_date_time )
        {
            lock ( lock_variable )
            {
                parsed_date_time = null;

                ParsedDateTime parsed_date;
                ParsedDateTime parsed_time;
                if ( !TryParseDate( str, default_format, out parsed_date ) )
                {
                    if ( !TryParseTime( str, default_format, out parsed_time, null ) )
                    {
                        return false;
                    }

                    DateTime date_time = new DateTime( DefaultDate.Year, DefaultDate.Month, DefaultDate.Day, parsed_time.DateTime.Hour, parsed_time.DateTime.Minute, parsed_time.DateTime.Second );
                    parsed_date_time = new ParsedDateTime( -1, -1, parsed_time.IndexOfTime, parsed_time.LengthOfTime, date_time );
                }
                else
                {
                    if ( !TryParseTime( str, default_format, out parsed_time, parsed_date ) )
                    {
                        DateTime date_time = new DateTime( parsed_date.DateTime.Year, parsed_date.DateTime.Month, parsed_date.DateTime.Day, 0, 0, 0 );
                        parsed_date_time = new ParsedDateTime( parsed_date.IndexOfDate, parsed_date.LengthOfDate, -1, -1, date_time );
                    }
                    else
                    {
                        DateTime date_time = new DateTime(
                            parsed_date.DateTime.Year, parsed_date.DateTime.Month, parsed_date.DateTime.Day, parsed_time.DateTime.Hour, parsed_time.DateTime.Minute, parsed_time.DateTime.Second );
                        parsed_date_time = new ParsedDateTime( parsed_date.IndexOfDate, parsed_date.LengthOfDate, parsed_time.IndexOfTime, parsed_time.LengthOfTime, date_time );
                    }
                }

                return true;
            }
        }

        #endregion

        #region parsing base methods

        /// <summary>
        /// Tries to find time within the passed string (relatively to the passed parsed_date if any) and return it as ParsedDateTime object.
        /// It recognizes only time while ignoring date, so date in the returned ParsedDateTime is always 1/1/1
        /// </summary>
        /// <param name="str">string that contains date</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="parsed_time">parsed date-time output</param>
        /// <param name="parsed_date">ParsedDateTime object if the date was found within this string, else NULL</param>
        /// <returns>true if time was found, else false</returns>
        public static bool TryParseTime( string str, DateTimeFormat default_format, out ParsedDateTime parsed_time, ParsedDateTime parsed_date )
        {
            lock ( lock_variable )
            {
                parsed_time = null;

                Match m;
                if ( parsed_date != null &&
                     parsed_date.IndexOfDate > -1 )
                {
                    //look around the found date
                    //look for <date> [h]h:mm[:ss] [PM/AM]
                    m = Regex.Match(
                        str.Substring( parsed_date.IndexOfDate + parsed_date.LengthOfDate ),
                        @"(?<=^\s*,?\s+|^\s*at\s*|^\s*[T\-]\s*)(?'hour'\d{1,2})\s*:\s*(?'minute'\d{2})\s*(?::\s*(?'second'\d{2}))?(?:\s*([AP]M))?(?=$|[^\d\w])",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    if ( !m.Success )
                    {
                        //look for [h]h:mm:ss <date>
                        m = Regex.Match(
                            str.Substring( 0, parsed_date.IndexOfDate ), @"(?<=^|[^\d])(?'hour'\d{1,2})\s*:\s*(?'minute'\d{2})\s*(?::\s*(?'second'\d{2}))?(?:\s*([AP]M))?(?=$|[\s,]+)",
                            RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    }
                }
                else //look anywere within string
                {
                    //look for [h]h:mm[:ss] [PM/AM]
                    m = Regex.Match(
                        str, @"(?<=^|\s+|\s*T\s*)(?'hour'\d{1,2})\s*:\s*(?'minute'\d{2})\s*(?::\s*(?'second'\d{2}))?(?:\s*([AP]M))?(?=$|[^\d\w])", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                }

                if ( m.Success )
                {
                    try
                    {
                        int hour = int.Parse( m.Groups[ "hour" ].Value );
                        if ( hour < 0 ||
                             hour > 23 )
                        {
                            return false;
                        }

                        int minute = int.Parse( m.Groups[ "minute" ].Value );
                        if ( minute < 0 ||
                             minute > 59 )
                        {
                            return false;
                        }

                        int second = 0;
                        if ( !string.IsNullOrEmpty( m.Groups [ "second" ].Value ) )
                        {
                            second = int.Parse( m.Groups [ "second" ].Value );
                            if ( second < 0 ||
                                 second > 59 )
                            {
                                return false;
                            }
                        }

                        if ( string.Compare( m.Groups [ 4 ].Value, "PM", true ) > -1 )
                        {
                            hour += 12;
                        }

                        DateTime date_time = new DateTime( 1, 1, 1, hour, minute, second );
                        parsed_time = new ParsedDateTime( -1, -1, m.Index, m.Length, date_time );
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Tries to find date within the passed string and return it as ParsedDateTime object.
        /// It recognizes only date while ignoring time, so time in the returned ParsedDateTime is always 0:0:0.
        /// If year of the date was not found then it accepts the current year.
        /// </summary>
        /// <param name="str">string that contains date</param>
        /// <param name="default_format">format that must be used preferably in ambivalent instances</param>
        /// <param name="parsed_date">parsed date output</param>
        /// <returns>true if date was found, else false</returns>
        public static bool TryParseDate( string str, DateTimeFormat default_format, out ParsedDateTime parsed_date )
        {
            lock ( lock_variable )
            {
                parsed_date = null;

                if ( string.IsNullOrEmpty( str ) )
                {
                    return false;
                }

                //look for dd/mm/yy
                Match m = Regex.Match(
                    str, @"(?<=^|[^\d])(?'day'\d{1,2})\s*(?'separator'[\\/\.])+\s*(?'month'\d{1,2})\s*\'separator'+\s*(?'year'\d{2,4})(?=$|[^\d])", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                if ( m.Success &&
                     m.Groups [ "year" ].Value.Length != 3 )
                {
                    DateTime date;
                    if ( ( default_format ^ DateTimeFormat.USA_DATE ) == DateTimeFormat.USA_DATE )
                    {
                        if ( !convert_to_date( int.Parse( m.Groups [ "year" ].Value ), int.Parse( m.Groups [ "day" ].Value ), int.Parse( m.Groups [ "month" ].Value ), out date ) )
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if ( !convert_to_date( int.Parse( m.Groups [ "year" ].Value ), int.Parse( m.Groups [ "month" ].Value ), int.Parse( m.Groups [ "day" ].Value ), out date ) )
                        {
                            return false;
                        }
                    }
                    parsed_date = new ParsedDateTime( m.Index, m.Length, -1, -1, date );
                    return true;
                }

                //look for yy-mm-dd
                m = Regex.Match(
                    str, @"(?<=^|[^\d])(?'year'\d{2,4})\s*(?'separator'[\-])\s*(?'month'\d{1,2})\s*\'separator'+\s*(?'day'\d{1,2})(?=$|[^\d])", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                if ( m.Success &&
                     m.Groups [ "year" ].Value.Length != 3 )
                {
                    DateTime date;
                    if ( !convert_to_date( int.Parse( m.Groups [ "year" ].Value ), int.Parse( m.Groups [ "month" ].Value ), int.Parse( m.Groups [ "day" ].Value ), out date ) )
                    {
                        return false;
                    }
                    parsed_date = new ParsedDateTime( m.Index, m.Length, -1, -1, date );
                    return true;
                }

                //look for month dd yyyy
                m = Regex.Match(
                    str, @"(?:^|[^\d\w])(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})(?:-?st|-?th)?\s*,?\s*(?'year'\d{4})(?=$|[^\d\w])",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase );
                if ( !m.Success )
                {
                    //look for dd month [yy]yy
                    m = Regex.Match(
                        str,
                        @"(?:^|[^\d\w:])(?'day'\d{1,2})(?:-?st|-?th)?\s+(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*(?:\s*,?\s*(?:'?(?'year'\d{2})|(?'year'\d{4})))?(?=$|[^\d\w])",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase );
                }
                if ( !m.Success )
                {
                    //look for yyyy month dd
                    m = Regex.Match(
                        str, @"(?:^|[^\d\w])(?'year'\d{4})\s+(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})(?:-?st|-?th)?(?=$|[^\d\w])",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase );
                }
                if ( !m.Success )
                {
                    //look for  month dd [yyyy]
                    m = Regex.Match(
                        str, @"(?:^|[^\d\w])(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})(?:-?st|-?th)?(?:\s*,?\s*(?'year'\d{4}))?(?=$|[^\d\w])",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase );
                }
                if ( m.Success )
                {
                    int month = -1;
                    int index_of_date = m.Index;
                    int length_of_date = m.Length;

                    switch ( m.Groups [ "month" ].Value )
                    {
                        case "Jan":
                        month = 1;
                        break;
                        case "Feb":
                        month = 2;
                        break;
                        case "Mar":
                        month = 3;
                        break;
                        case "Apr":
                        month = 4;
                        break;
                        case "May":
                        month = 5;
                        break;
                        case "Jun":
                        month = 6;
                        break;
                        case "Jul":
                        month = 7;
                        break;
                        case "Aug":
                        month = 8;
                        break;
                        case "Sep":
                        month = 9;
                        break;
                        case "Oct":
                        month = 10;
                        break;
                        case "Nov":
                        month = 11;
                        break;
                        case "Dec":
                        month = 12;
                        break;
                    }

                    int year;
                    if ( !string.IsNullOrEmpty( m.Groups [ "year" ].Value ) )
                    {
                        year = int.Parse( m.Groups [ "year" ].Value );
                    }
                    else
                    {
                        year = DefaultDate.Year;
                    }

                    DateTime date;
                    if ( !convert_to_date( year, month, int.Parse( m.Groups [ "day" ].Value ), out date ) )
                    {
                        return false;
                    }
                    parsed_date = new ParsedDateTime( index_of_date, length_of_date, -1, -1, date );
                    return true;
                }

                return false;
            }
        }

        private static bool convert_to_date( int year, int month, int day, out DateTime date )
        {
            if ( year >= 100 )
            {
                if ( year < 1000 )
                {
                    date = new DateTime( 1, 1, 1 );
                    return false;
                }
            }
            else if ( year > 30 )
            {
                year += 1900;
            }
            else
            {
                year += 2000;
            }

            try
            {
                date = new DateTime( year, month, day );
            }
            catch
            {
                date = new DateTime( 1, 1, 1 );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Converts DateTime to unix time (defaults to milliseconds).
        /// </summary>
        /// <param name="date">DateTime to convert.</param>
        /// <param name="precision">Precision (optional, defaults to milliseconds)</param>
        /// <returns>Unix-style timestamp in milliseconds.</returns>
        public static long ToLinuxTime( this DateTime date, string precision = MyTimeUnit.Milliseconds )
        {
            var span = date - _epoch;
            double fractionalSpan;

            switch ( precision )
            {
                case MyTimeUnit.Milliseconds:
                    fractionalSpan = span.TotalMilliseconds;
                    break;
                case MyTimeUnit.Seconds:
                    fractionalSpan = span.TotalSeconds;
                    break;
                case MyTimeUnit.Minutes:
                    fractionalSpan = span.TotalMinutes;
                    break;
                case MyTimeUnit.Hours:
                    fractionalSpan = span.TotalHours;
                    break;
                default:
                    fractionalSpan = span.TotalMilliseconds;
                    break;
            }

            return Convert.ToInt64( fractionalSpan );
        }

        public static DateTime FromLinuxTime( this long linuxTime, string precision = MyTimeUnit.Milliseconds )
        {
            switch ( precision )
            {
                case MyTimeUnit.Milliseconds:
                    return _epoch.AddMilliseconds( linuxTime );
                case MyTimeUnit.Seconds:
                    return _epoch.AddSeconds( linuxTime );
                case MyTimeUnit.Minutes:
                    return _epoch.AddMinutes( linuxTime );
                case MyTimeUnit.Hours:
                    return _epoch.AddHours( linuxTime );
                default:
                    return _epoch.AddMilliseconds( linuxTime );
            }
        }



        #endregion
    }


    public static class TimeZoneHelper
    {
        /// <summary>
        /// Gets the TimeZoneInfo.AdjustmentRule in effect for the given year.
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForYear( this TimeZoneInfo timeZoneInfo, int year )
        {
            TimeZoneInfo.AdjustmentRule[] adjustments = timeZoneInfo.GetAdjustmentRules();
            // Iterate adjustment rules for time zone 
            foreach ( TimeZoneInfo.AdjustmentRule adjustment in adjustments )
            {
                // Determine if this adjustment rule covers year desired 
                if ( adjustment.DateStart.Year <= year && adjustment.DateEnd.Year >= year )
                {
                    return adjustment;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the Daylight Savings Time start date for the given year.
        /// </summary>
        /// <param name="adjustmentRule"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetDaylightTransitionStartForYear( this TimeZoneInfo.AdjustmentRule adjustmentRule, int year )
        {
            return adjustmentRule.DaylightTransitionStart.GetDateForYear( year );
        }

        /// <summary>
        /// Gets the Daylight Savings Time end date for the given year.
        /// </summary>
        /// <param name="adjustmentRule"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetDaylightTransitionEndForYear( this TimeZoneInfo.AdjustmentRule adjustmentRule, int year )
        {
            return adjustmentRule.DaylightTransitionEnd.GetDateForYear( year );
        }

        /// <summary>
        /// Gets the date of the transition for the given year.
        /// </summary>
        /// <param name="transitionTime"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetDateForYear( this TimeZoneInfo.TransitionTime transitionTime, int year )
        {
            if ( transitionTime.IsFixedDateRule )
            {
                return GetFixedDateRuleDate( transitionTime, year );
            }
            else
            {
                return GetFloatingDateRuleDate( transitionTime, year );
            }
        }

        private static DateTime GetFixedDateRuleDate( TimeZoneInfo.TransitionTime transitionTime, int year )
        {
            return new DateTime( year,
                               transitionTime.Month,
                               transitionTime.Day,
                               transitionTime.TimeOfDay.Hour,
                               transitionTime.TimeOfDay.Minute,
                               transitionTime.TimeOfDay.Second,
                               DateTimeKind.Unspecified );
        }

        private static DateTime GetFloatingDateRuleDate( TimeZoneInfo.TransitionTime transitionTime, int year )
        {
            // For non-fixed date rules, get local calendar
            Calendar localCalendar = CultureInfo.CurrentCulture.Calendar;

            // Get first day of week for transition
            // For example, the 3rd week starts no earlier than the 15th of the month
            int startOfWeek = transitionTime.Week * 7 - 6;

            // What day of the week does the month start on?
            int firstDayOfWeek = (int)localCalendar.GetDayOfWeek(new DateTime(year, transitionTime.Month, 1));

            // Determine how much start date has to be adjusted
            int transitionDay;
            int changeDayOfWeek = (int)transitionTime.DayOfWeek;
            if ( firstDayOfWeek <= changeDayOfWeek )
                transitionDay = startOfWeek + ( changeDayOfWeek - firstDayOfWeek );
            else
                transitionDay = startOfWeek + ( 7 - firstDayOfWeek + changeDayOfWeek );

            // Adjust for months with no fifth week
            if ( transitionDay > localCalendar.GetDaysInMonth( year, transitionTime.Month ) )
                transitionDay -= 7;

            return new DateTime( year,
                       transitionTime.Month,
                       transitionDay,
                       transitionTime.TimeOfDay.Hour,
                       transitionTime.TimeOfDay.Minute,
                       transitionTime.TimeOfDay.Second,
                       DateTimeKind.Unspecified );
        }
    }
}