using fx.Collections;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace fx.DefinitionsWnd
{
    public class easter
    {

        int _d;
        int _m;

        public int m
        {
            get { return _m; }
            set
            {
                _m = value;
            }
        }

        
        public int d
        {
            get { return _d; }
            set
            {
                _d = value;
            }
        }
        

        //======================================= 返回该年的复活节(春分后第一次满月周后的第一主日)
        public easter( int y )
        {
            var term2     = ChronosDefintion.sTerm( y, 5 );                            //取得春分日期
            var dayTerm2  = new DateTime( y, 2, term2, 0, 0, 0, 0, DateTimeKind.Utc ); //取得春分的公历日期控件(春分一定出现在3月)
            var lDayTerm2 = new Lunar( dayTerm2 );                                     //取得取得春分农历

            int lMlen     = 0;

            if ( lDayTerm2.Day < 15 )                                                  //取得下个月圆的相差天数
            {
                lMlen = 15 - lDayTerm2.Day;
            }                
            else
            {
                lMlen = ( lDayTerm2.IsLeap ? ChronosDefintion.leapDays( y ) : ChronosDefintion.monthDays( y, lDayTerm2.Month ) ) - lDayTerm2.Day + 15;
            }

            //一天等于 1000*60*60*24 = 86400000 毫秒
            var epoch = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );

            var from1970 = dayTerm2 - epoch;
            var l15 = epoch.AddMilliseconds( from1970.TotalMilliseconds + 86400000 * lMlen );

            var esterFrom1970 = l15 - epoch;
            var dayEaster = epoch.AddMilliseconds( esterFrom1970.TotalMilliseconds + 86400000 * ( 7 - ( int )l15.DayOfWeek ) );
            
            m = dayEaster.Month;
            d = ( int ) dayEaster.DayOfWeek;
        }
    }

    public class CalendarBaiZi
    {
        private int length;
        private int firstWeek;
        private PooledList< calElement > _calElements = new PooledList< calElement >( );


        public CalendarBaiZi( int y, int m )
        {
            DateTime sDObj; Lunar lDObj;
            int lY = 0, lM = 0,  lD = 1,  lX = 0, tmp1, tmp2, tmp3, lM2, lY2 = 0, lD2 ;
            bool lL = false;

            string xs = "", fs = "", cs = "";
            string cY = "", cM = "", cD = "";                           //年柱,月柱,日柱
            
            
            var lDPOS       = new int[ 3 ];
            var n           = 0;
            var firstLM     = 0;
            sDObj           = new DateTime( y, m, 1, 0, 0, 0, 0 );      //当月一日日期
            length     = ChronosDefintion.solarDays( y, m );       //公历当月天数
            firstWeek  = (int) sDObj.DayOfWeek;                    //公历当月1日星期几

            /*-----------------------年柱 1900年立春后为庚子年(60进制36)----------------------------*/
            if ( m < 2 )
            {
                cY = ChronosDefintion.cyclical( y - 1900 + 36 - 1 );
            }
            else
            {
                cY = ChronosDefintion.cyclical( y - 1900 + 36 );
            }

            var term2       = ChronosDefintion.sTerm( y, 2 ); //立春日期

            /*-----------------------月柱 1900年1月小寒以前为 丙子月(60进制12)-----------------------*/
            var firstNode   = ChronosDefintion.sTerm( y, m * 2 );       //返回当月「节」为几日开始
            cM              = ChronosDefintion.cyclical( ( y - 1900 ) * 12 + m + 12 );
            lM2             = ( y - 1900 ) * 12 + m + 12;

            //当月一日与 1900/1/1 相差天数
            //1900/1/1与 1970/1/1 相差25567日, 1900/1/1 日柱为甲戌日(60进制10)
            DateTime epoch  = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );
            DateTime input  = new DateTime( y, m, 1, 0, 0, 0, 0, DateTimeKind.Utc );
            TimeSpan diff   = input - epoch;

            int dayCyclical = (int) diff.TotalDays + 25567 + 10;

            for ( var i = 0; i < length; i++ )
            {
                if ( lD > lX )
                {
                    sDObj        = new DateTime( y, m, i + 1 );                                                 //当月一日日期
                    lDObj        = new Lunar( sDObj );                                                          //农历
                    lY           = lDObj.Year;                                                                  //农历年
                    lM           = lDObj.Month;                                                                 //农历月
                    lD           = lDObj.Day;                                                                   //农历日
                    lL           = lDObj.IsLeap;                                                                //农历是否闰月
                    lX           = lL ? ChronosDefintion.leapDays( lY ) : ChronosDefintion.monthDays( lY, lM ); //农历当月最后一天
                    if ( n       == 0 ) firstLM = lM;
                    lDPOS[ n++ ] = i - lD + 1;
                }

                /*依节气调整二月分的年柱, 以立春为界*/
                if ( m == 1 && ( i + 1 ) == term2 )
                {
                    cY = ChronosDefintion.cyclical( y - 1900 + 36 );
                    lY2 = ( y - 1900 + 36 );
                }

                //依节气月柱, 以「节」为界
                if ( ( i + 1 ) == firstNode )
                {
                    cM = ChronosDefintion.cyclical( ( y - 1900 ) * 12 + m + 13 );
                    lM2 = ( y - 1900 ) * 12 + m + 13;
                }

                //日柱
                cD = ChronosDefintion.cyclical( dayCyclical + i );
                lD2 = ( dayCyclical + i );
                //sYear,sMonth,sDay,week,
                //lYear,lMonth,lDay,isLeap,
                //cYear,cMonth,cDay

                var cal = new calElement( y, m + 1, i + 1, ChronosDefintion.nStr1[ ( i + firstWeek ) % 7 ], lY, lM, lD++, lL, cY, cM, cD );

                _calElements.Add( cal );
                cal.sgz5 = ChronosDefintion.CalConv2( lY2 % 12, lM2 % 12, ( lD2 ) % 12, lY2 % 10, ( lD2 ) % 10, lM, lD - 1, m + 1 );
                cal.sgz3 = ChronosDefintion.cyclical2( lM2 % 12, ( lD2 ) % 12 );
                cal.dGz  = ChronosDefintion.cyclical3( lD2 );
                cal.sgz  = ChronosDefintion.cyclical4( lD2 );

                //喜神、福神、财神位
                if ( ( lD2 ) % 10 == 0 || ( lD2 ) % 10 == 5 ) { xs = "东北"; }
                else if ( ( lD2 ) % 10 == 1 || ( lD2 ) % 10 == 6 ) { xs = "西北"; }
                else if ( ( lD2 ) % 10 == 2 || ( lD2 ) % 10 == 7 ) { xs = "西南"; }
                else if ( ( lD2 ) % 10 == 3 || ( lD2 ) % 10 == 8 ) { xs = "正南"; }
                else if ( ( lD2 ) % 10 == 4 || ( lD2 ) % 10 == 9 ) { xs = "东南"; }

                if ( ( lD2 ) % 10 == 0 || ( lD2 ) % 10 == 1 ) { fs = "东南"; }
                else if ( ( lD2 ) % 10 == 2 || ( lD2 ) % 10 == 3 ) { fs = "正东"; }
                else if ( ( lD2 ) % 10 == 4 ) { fs = "正北"; }
                else if ( ( lD2 ) % 10 == 5 ) { fs = "正南"; }
                else if ( ( lD2 ) % 10 == 6 || ( lD2 ) % 10 == 7 ) { fs = "西南"; }
                else if ( ( lD2 ) % 10 == 8 ) { fs = "西北"; }
                else if ( ( lD2 ) % 10 == 9 ) { fs = "正西"; }

                if ( ( lD2 ) % 10 == 0 || ( lD2 ) % 10 == 1 ) { cs = "东北"; }
                else if ( ( lD2 ) % 10 == 2 || ( lD2 ) % 10 == 3 ) { cs = "西南"; }
                else if ( ( lD2 ) % 10 == 4 || ( lD2 ) % 10 == 5 ) { cs = "正北"; }
                else if ( ( lD2 ) % 10 == 6 || ( lD2 ) % 10 == 7 ) { cs = "正东"; }
                else if ( ( lD2 ) % 10 == 8 || ( lD2 ) % 10 == 9 ) { cs = "正南"; }

                cal.xsfw = "<font color='#800080'>\u25C7喜神：</font>" + xs;
                cal.fsfw = "<font color='#800080'>\u25C7福神：</font>" + fs;
                cal.csfw = "<font color='#800080'>\u25C7财神：</font>" + cs;
            }

            //节气
            tmp1 = ChronosDefintion.sTerm( y, m * 2 ) - 1;
            tmp2 = ChronosDefintion.sTerm( y, m * 2 + 1 ) - 1;
            _calElements[ tmp1 ].solarTerms = ChronosDefintion.SolarTerm[ m * 2 ];
            _calElements[ tmp2 ].solarTerms = ChronosDefintion.SolarTerm[ m * 2 + 1 ];

            if ( m == 3 ) _calElements[ tmp1 ].color = Colors.Red; //清明颜色

            //公历节日
            foreach ( var i in ChronosDefintion.sFtv )
            {
                Regex rx = new Regex( @"^(\d{2})(\d{2})([\s\*])(.+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                Match match = rx.Match( i );

                // Here we check the Match instance.
                if ( match.Success )
                {
                    if ( Int32.Parse( match.Groups[ 1 ].Value ) == ( m + 1 ) )
                    {
                        _calElements[ Int32.Parse( match.Groups[ 2 ].Value ) - 1 ].solarFestival += match.Groups[ 4 ].Value + " ";

                        if ( match.Groups[ 3 ].Value == "*") _calElements[ Int32.Parse( match.Groups[ 2 ].Value ) - 1 ].color = Colors.Red;
                    }
                }
            }


            //月周节日
            foreach ( var i in ChronosDefintion.wFtv )
            {
                Regex rx = new Regex( @"^(\d{2})(\d)(\d)([\s\*])(.+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                Match match = rx.Match( i );

                // Here we check the Match instance.
                if ( match.Success )
                {
                    if ( Int32.Parse( match.Groups[ 1 ].Value ) == ( m + 1 ) )
                    {
                        tmp1 = Int32.Parse( match.Groups[ 2 ].Value );
                        tmp2 = Int32.Parse( match.Groups[ 3 ].Value );

                        if ( tmp1 < 5 )
                        {
                            _calElements[ ( ( firstWeek > tmp2 ) ? 7 : 0 ) + 7 * ( tmp1 - 1 ) + tmp2 - firstWeek ].solarFestival += match.Groups[ 5 ].Value + ' ';
                        }
                            
                        else
                        {
                            tmp1 -= 5;
                            tmp3 = ( firstWeek + length - 1 ) % 7; //当月最后一天星期?
                            _calElements[ length - tmp3 - 7 * tmp1 + tmp2 - ( tmp2 > tmp3 ? 7 : 0 ) - 1 ].solarFestival += match.Groups[ 5 ].Value + ' ';
                        }
                    }
                }
            }

            foreach ( var i in ChronosDefintion.lFtv )
            {
                Regex rx = new Regex( @"^(\d{2})(.{2})([\s\*])(.+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                Match match = rx.Match( i );

                // Here we check the Match instance.
                if ( match.Success )
                {
                    tmp1 = Int32.Parse( match.Groups[ 1 ].Value ) - firstLM;
                    if ( tmp1 == -11 ) tmp1 = 1;
                    if ( tmp1 >= 0 && tmp1 < n )
                    {
                        tmp2 = lDPOS[ tmp1 ] + Int32.Parse( match.Groups[ 2 ].Value ) - 1;
                        if ( tmp2 >= 0 && tmp2 < length && _calElements[ tmp2 ].isLeap != true )
                        {
                            _calElements[ tmp2 ].lunarFestival += match.Groups[ 4 ].Value + ' ';
                            if ( match.Groups[ 3 ].Value == "*") _calElements[ tmp2 ].color = Colors.Red;
                        }
                    }
                }
            }


            //农历节日
           
            //复活节只出现在3或4月
            if ( m == 2 || m == 3 )
            {
                var estDay = new easter( y );
                if ( m == estDay.m )
                    _calElements[ estDay.d - 1 ].solarFestival = _calElements[ estDay.d - 1 ].solarFestival + " 复活节 Easter Sunday";
            }
            if ( m == 2 )
            {
                _calElements[ 20 ].solarFestival = _calElements[ 20 ].solarFestival + Regex.Unescape( "%20%u6D35%u8CE2%u751F%u65E5" );
            }

            //黑色星期五
            if ( ( firstWeek + 12 ) % 7 == 5 )
            {
                _calElements[ 12 ].solarFestival += "黑色星期五";
            }

            var Today = DateTime.UtcNow;
            var tY = Today.Year;
            var tM = Today.Month;
            var tD = Today.Day;

            //今日
            if ( y == tY && m == tM ) _calElements[ tD - 1 ].isToday = true;
        }
    }

    //============================== 阴历属性
    public class calElement
    {
        bool _isToday;
        string _lunarFestival;
        bool _isLeap;
        string _solarFestival;
        string _solarTerms;
        string _csfw;
        string _fsfw;
        string _xsfw;
        string _sgz;
        string _dGz;
        string _sgz3;
        string _sgz5;        
        private int sYear;
        private int sMonth;
        private int sDay;
        private string week;
        private int lYear;
        private int lMonth;
        private int lDay;
        private string cYear;
        private string cMonth;
        private string cDay;
        private Color _color;

        
        public bool isToday
        {
            get { return _isToday; }
            set
            {
                _isToday = value;
            }
        }
        


        public string lunarFestival
        {
            get { return _lunarFestival; }
            set
            {
                _lunarFestival = value;
            }
        }
        

        public bool isLeap
        {
            get { return _isLeap; }
            set
            {
                _isLeap = value;
            }
        }
        

        public string solarFestival
        {
            get { return _solarFestival; }
            set
            {
                _solarFestival = value;
            }
        }
        

        public Color color
        {
            get { return _color; }
            set
            {
                _color = value;
            }
        }

        public string sgz5
        {
            get { return _sgz5; }
            set
            {
                _sgz5 = value;
            }
        }


        public string sgz3
        {
            get { return _sgz3; }
            set
            {
                _sgz3 = value;
            }
        }


        public string dGz
        {
            get { return _dGz; }
            set
            {
                _dGz = value;
            }
        }


        public string sgz
        {
            get { return _sgz; }
            set
            {
                _sgz = value;
            }
        }


        public string xsfw
        {
            get { return _xsfw; }
            set
            {
                _xsfw = value;
            }
        }


        public string fsfw
        {
            get { return _fsfw; }
            set
            {
                _fsfw = value;
            }
        }


        public string csfw
        {
            get { return _csfw; }
            set
            {
                _csfw = value;
            }
        }

        
        public string solarTerms
        {
            get { return _solarTerms; }
            set
            {
                _solarTerms = value;
            }
        }    

        public calElement( int sYear, int sMonth, int sDay, string week, int lYear, int lMonth, int lDay, bool isLeap, string cYear, string cMonth, string cDay )
        {
            isToday       = false;
            //瓣句
            this.sYear         = sYear;     //公元年4位数字
            this.sMonth        = sMonth;    //公元月数字
            this.sDay          = sDay;      //公元日数字
            this.week          = week;      //星期, 1个中文
            
            //农历
            this.lYear         = lYear;     //公元年4位数字
            this.lMonth        = lMonth;    //农历月数字
            this.lDay          = lDay;      //农历日数字
            _isLeap       = isLeap;    //是否为农历闰月?
            
            //八字
            this.cYear         = cYear;     //年柱, 2个中文
            this.cMonth        = cMonth;    //月柱, 2个中文
            this.cDay          = cDay;      //日柱, 2个中文

            color         = Colors.Transparent;
            lunarFestival = "";        //农历节日
            solarFestival = "";        //公历节日
            solarTerms    = "";        //节气
        }
    }

    public class Lunar
    {
        
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
            }
        }

        
        public int Month
        {
            get { return _month; }
            set
            {
                _month = value;
            }
        }

        
        public int Day
        {
            get { return _day; }
            set
            {
                _day = value;
            }
        }

        
        public bool IsLeap
        {
            get { return _isLeap; }
            set
            {
                _isLeap = value;
            }
        }
        

        int _day;
        private int _year = 0;
        private bool _isLeap = false;
        private int _month = 0;
        
        //====================================== 算出农历, 传入日期控件, 返回农历日期控件, 该控件属性有 .year .month .day .isLeap
        public Lunar( DateTime objDate )
        {
            int i, leap = 0, temp = 0;
            var offset = ( new DateTime( objDate.Year, objDate.Month, objDate.Day ) - new DateTime( 1900, 0, 31 ) ).TotalDays / 86400000;
            for ( i = 1900; i < 2100 && offset > 0; i++ )
            {
                temp = ChronosDefintion.lYearDays( i );
                offset -= temp;
            }
            if ( offset < 0 )
            {
                offset += temp;
                i--;
            }
            _year = i;
            leap = ChronosDefintion.leapMonth( i ); //闰哪个月
            _isLeap = false;

            for ( i = 1; i < 13 && offset > 0; i++ )
            {
                //闰月
                if ( leap > 0 && i == ( leap + 1 ) && _isLeap == false )
                {
                    --i;
                    _isLeap = true;
                    temp = ChronosDefintion.leapDays( _year );
                }
                else
                {
                    temp = ChronosDefintion.monthDays( _year, i );
                }
                //解除闰月
                if ( _isLeap == true && i == ( leap + 1 ) )
                {
                    _isLeap = false;
                }

                offset -= temp;
            }
            if ( offset == 0 && leap > 0 && i == leap + 1 )
            {
                if ( _isLeap )
                {
                    _isLeap = false;
                }
                else
                {
                    _isLeap = true;
                    --i;
                }
            }

            if ( offset < 0 )
            {
                offset += temp;
                --i;
            }
            _month = i;
            _day = ( int )offset + 1;
        }
    }

    public static class ChronosDefintion
    {
        public static int[ ] LunarInfoList = new int[ ]
        {
            0x04bd8, 0x04ae0, 0x0a570, 0x054d5, 0x0d260, 0x0d950, 0x16554, 0x056a0, 0x09ad0, 0x055d2,   // 1900-1909
	        0x04ae0, 0x0a5b6, 0x0a4d0, 0x0d250, 0x1d255, 0x0b540, 0x0d6a0, 0x0ada2, 0x095b0, 0x14977,   // 1910-1919
	        0x04970, 0x0a4b0, 0x0b4b5, 0x06a50, 0x06d40, 0x1ab54, 0x02b60, 0x09570, 0x052f2, 0x04970,   // 1920-1929
	        0x06566, 0x0d4a0, 0x0ea50, 0x06e95, 0x05ad0, 0x02b60, 0x186e3, 0x092e0, 0x1c8d7, 0x0c950,   // 1930-1939
	        0x0d4a0, 0x1d8a6, 0x0b550, 0x056a0, 0x1a5b4, 0x025d0, 0x092d0, 0x0d2b2, 0x0a950, 0x0b557,   // 1940-1949
	        0x06ca0, 0x0b550, 0x15355, 0x04da0, 0x0a5b0, 0x14573, 0x052b0, 0x0a9a8, 0x0e950, 0x06aa0,   // 1950-1959
	        0x0aea6, 0x0ab50, 0x04b60, 0x0aae4, 0x0a570, 0x05260, 0x0f263, 0x0d950, 0x05b57, 0x056a0,   // 1960-1969
	        0x096d0, 0x04dd5, 0x04ad0, 0x0a4d0, 0x0d4d4, 0x0d250, 0x0d558, 0x0b540, 0x0b6a0, 0x195a6,   // 1970-1979
	        0x095b0, 0x049b0, 0x0a974, 0x0a4b0, 0x0b27a, 0x06a50, 0x06d40, 0x0af46, 0x0ab60, 0x09570,   // 1980-1989
	        0x04af5, 0x04970, 0x064b0, 0x074a3, 0x0ea50, 0x06b58, 0x055c0, 0x0ab60, 0x096d5, 0x092e0,   // 1990-1999
	        0x0c960, 0x0d954, 0x0d4a0, 0x0da50, 0x07552, 0x056a0, 0x0abb7, 0x025d0, 0x092d0, 0x0cab5,   // 2000-2009
	        0x0a950, 0x0b4a0, 0x0baa4, 0x0ad50, 0x055d9, 0x04ba0, 0x0a5b0, 0x15176, 0x052b0, 0x0a930,   // 2010-2019
	        0x07954, 0x06aa0, 0x0ad50, 0x05b52, 0x04b60, 0x0a6e6, 0x0a4e0, 0x0d260, 0x0ea65, 0x0d530,   // 2020-2029
	        0x05aa0, 0x076a3, 0x096d0, 0x04afb, 0x04ad0, 0x0a4d0, 0x1d0b6, 0x0d250, 0x0d520, 0x0dd45,   // 2030-2039
	        0x0b5a0, 0x056d0, 0x055b2, 0x049b0, 0x0a577, 0x0a4b0, 0x0aa50, 0x1b255, 0x06d20, 0x0ada0,   // 2040-2049
	        0x14b63, 0x09370, 0x049f8, 0x04970, 0x064b0, 0x168a6, 0x0ea50, 0x06b20, 0x1a6c4, 0x0aae0,   // 2050-2059
	        0x0a2e0, 0x0d2e3, 0x0c960, 0x0d557, 0x0d4a0, 0x0da50, 0x05d55, 0x056a0, 0x0a6d0, 0x055d4,   // 2060-2069
	        0x052d0, 0x0a9b8, 0x0a950, 0x0b4a0, 0x0b6a6, 0x0ad50, 0x055a0, 0x0aba4, 0x0a5b0, 0x052b0,   // 2070-2079
	        0x0b273, 0x06930, 0x07337, 0x06aa0, 0x0ad50, 0x14b55, 0x04b60, 0x0a570, 0x054e4, 0x0d160,   // 2080-2089
	        0x0e968, 0x0d520, 0x0daa0, 0x16aa6, 0x056d0, 0x04ae0, 0x0a9d4, 0x0a2d0, 0x0d150, 0x0f252,   // 2090-2099
	        0x0d520,                                                                                    // 2100
        };

        public static int[ ] SolarMonth = new int[ ]
        {
            31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
        };

        public static string[ ] HeavenlyStem = new string[ ]
        {
            "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸"
        };

        public static string[ ] EarthyBranch = new string[ ]
        {
            "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥"
        };

        public static string[ ] StemBranchTable1 = new string[ ]
        {
            "甲子乙丑丙寅丁卯戊辰己巳庚午辛未壬申癸酉甲戌乙亥", "丙子丁丑戊寅己卯庚辰辛巳壬午癸未甲申乙酉丙戌丁亥",
            "戊子己丑庚寅辛卯壬辰癸巳甲午乙未丙申丁酉戊戌己亥", "庚子辛丑壬寅癸卯甲辰乙巳丙午丁未戊申己酉庚戌辛亥",
            "壬子癸丑甲寅乙卯丙辰丁巳戊午己未庚申辛酉壬戌癸亥", "甲子乙丑丙寅丁卯戊辰己巳庚午辛未壬申癸酉甲戌乙亥",
            "丙子丁丑戊寅己卯庚辰辛巳壬午癸未甲申乙酉丙戌丁亥", "戊子己丑庚寅辛卯壬辰癸巳甲午乙未丙申丁酉戊戌己亥",
            "庚子辛丑壬寅癸卯甲辰乙巳丙午丁未戊申己酉庚戌辛亥", "壬子癸丑甲寅乙卯丙辰丁巳戊午己未庚申辛酉壬戌癸亥"
        };

        public static string[ ] StemBranchTable2 = new string[ ]
        {
            "1金匮1天德2白虎1玉堂2天牢2玄武1司命2勾陈1青龙1明堂2天刑2朱雀",
            "2天刑2朱雀1金匮1天德2白虎1玉堂2天牢2玄武1司命2勾陈1青龙1明堂",
            "1青龙1明堂2天刑2朱雀1金匮1天德2白虎1玉堂2天牢2玄武1司命2勾陈",
            "1司命2勾陈1青龙1明堂2天刑2朱雀1金匮1天德2白虎1玉堂2天牢2玄武",
            "2天牢2玄武1司命2勾陈1青龙1明堂2天刑2朱雀1金匮1天德2白虎1玉堂",
            "2白虎1玉堂2天牢2玄武1司命2勾陈1青龙1明堂2天刑2朱雀1金匮1天德",
            "1金匮1天德2白虎1玉堂2天牢2玄武1司命2勾陈1青龙1明堂2天刑2朱雀",
            "2天刑2朱雀1金匮1天德2白虎1玉堂2天牢2玄武1司命2勾陈1青龙1明堂",
            "1青龙1明堂2天刑2朱雀1金匮1天德2白虎1玉堂2天牢2玄武1司命2勾陈",
            "1司命2勾陈1青龙1明堂2天刑2朱雀1金匮1天德2白虎1玉堂2天牢2玄武",
            "2天牢2玄武1司命2勾陈1青龙1明堂2天刑2朱雀1金匮1天德2白虎1玉堂",
            "2白虎1玉堂2天牢2玄武1司命2勾陈1青龙1明堂2天刑2朱雀1金匮1天德"
        };

        public static string[ ] Animals = new string[ ]
        {
            "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪"
        };

        public static string[ ] SolarTerm = new string[ ]
        {
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
        };

        public static string[ ] STA = new string[ ]
        {
            "摩蝎座：12 . 22—01 . 19", "水瓶座：01 . 20—02 . 18", "双鱼座：02 . 19—03 . 20", "白羊座：03 . 21—04 . 19",
            "金牛座：04 . 20—05 . 20", "双子座：05 . 21—06 . 20", "巨蟹座：06 . 21—07 . 21", "狮子座：07 . 22—08 . 22",
            "处女座：08 . 23—09 . 22", "天秤座：09 . 23—10 . 22", "天蝎座：10 . 23—11 . 21", "射手座：11 . 22—12 . 21"
        };

        public static int[ ] sTermInfo = new int[ ]
        {
            0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758
        };

        public static string[ ] nStr1 = new string[ ] { "日", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        public static string[ ] nStr2 = new string[ ] { "初", "十", "廿", "卅", "□" };

        //国历节日 *表示放假日
        public static string[ ] sFtv = new string[ ] {
            "0101*元旦",
            "0110 中国110宣传日",

            "0202 世界湿地日",
            "0204 世界抗癌症日",
            "0207 国际声援南非日",
            "0210 国际气象节",
            "0214 情人节",
            "0221 国际母语日",

            "0301 国际海豹日",
            "0303 全国爱耳日",
            "0305 学雷锋纪念日",
            "0308 妇女节",
            "0312 植树节 孙中山逝世纪念日",
            "0314 国际警察日",
            "0315 消费者权益日",
            "0317 中国国医节 国际航海日",
            "0321 世界森林日 消除种族歧视国际日 世界儿歌日",
            "0322 世界水日",
            "0323 世界气象日",
            "0324 世界防治结核病日",
            "0325 全国中小学生安全教育日",

            "0401 愚人节 全国爱国卫生运动月(四月) 税收宣传月(四月)",
            "0407 世界卫生日",
            "0422 世界地球日",
            "0423 世界图书和版权日",
            "0424 亚非新闻工作者日",

            "0501*劳动节",
            "0504 青年节",
            "0505 碘缺乏病防治日",
            "0508 世界红十字日",
            "0512 国际护士节",
            "0515 国际家庭日",
            "0517 世界电信日",
            "0518 国际博物馆日",
            "0519 全国助残日",
            "0520 全国学生营养日",
            "0522 国际生物多样性日",
            "0523 国际牛奶日",
            "0531 世界无烟日",

            "0601 国际儿童节",
            "0605 世界环境保护日",
            "0606 全国爱眼日",
            "0617 防治荒漠化和干旱日",
            "0623 国际奥林匹克日",
            "0625 全国土地日",
            "0626 国际禁毒日",

            "0701 中共诞辰 香港回归纪念日 世界建筑日",
            "0702 国际体育记者日",
            "0707 抗日战争纪念日",
            "0711 世界人口日",

            "0801 八一建军节",
            "0808 中国男子节(爸爸节)",
            "0815 抗日战争胜利纪念",

            "0908 国际扫盲日 国际新闻工作者日",
            "0909 毛泽东逝世纪念",
            "0910 教师节",
            "0914 世界清洁地球日",
            "0916 国际臭氧层保护日",
            "0917 国际和平日",
            "0918 九·一八事变纪念日",
            "0920 国际爱牙日",
            "0927 世界旅游日",
            "0928 孔子诞辰",

            "1001*国庆节 世界音乐节 国际老人节",
            "1002 国际和平与民主自由斗争日",
            "1004 世界动物日",
            "1006 老人节",
            "1007 国际住房日",
            "1008 全国高血压日 世界视觉日",
            "1009 世界邮政日 万国邮联日",
            "1010 辛亥革命纪念日 世界精神卫生日",
            "1013 世界保健日 国际教师节",
            "1014 世界标准日",
            "1015 国际盲人节(白手杖节)",
            "1016 世界粮食日",
            "1017 世界消除贫困日",
            "1022 世界传统医药日",
            "1024 联合国日",
            "1026 足球诞生日",
            "1031 世界勤俭日 万圣节(鬼节)",

            "1108 中国记者日",
            "1109 消防宣传日",
            "1110 世界青年节",
            "1111 光棍节 国际科学与和平周(本日所属的一周)",
            "1112 孙中山诞辰",
            "1114 世界糖尿病日",
            "1117 国际大学生节 世界学生节",
            "1120 彝族年",
            "1121 彝族年 世界问候日 世界电视日",
            "1122 彝族年",

            "1201 世界艾滋病日",
            "1203 世界残疾人日",
            "1205 国际经济和社会发展志愿人员日",
            "1208 国际儿童电视日",
            "1209 世界足球日",
            "1210 世界人权日",
            "1213 南京大屠杀(1937年)纪念日！",
            "1220 澳门回归纪念",
            "1221 国际篮球日",
            "1224 平安夜",
            "1225 圣诞节 世界强化免疫日",
            "1226 毛泽东诞辰纪念"
        };

        //农历节日 *表示放假日
        public static string[ ] lFtv = new string[ ]
        {
            "0101*春节",
            "0102*初二",
            "0103*初三",
            "0105 路神生日",
            "0115 元宵节",
            "0125 填仓节",
            "0126 生菜会",
            "0202 龙头节",
            "0206 东华帝君诞",
            "0215 涅槃节",
            "0219 观世音圣诞",
            "0323 妈祖诞、天后诞",
            "0404 寒食节",
            "0408 佛诞节 牛王诞",
            "0505*端午节",
            "0508 龙母诞",
            "0520 分龙节",
            "0606 天贶节 姑姑节",
            "0616 鲁班节",
            "0630 围香节",
            "0624 彝族火把节 关帝节",
            "0707 七夕情人节",
            "0714 鬼节(南方)",
            "0715 中元节 盂兰节",
            "0730 地藏节",
            "0802 灶君诞",
            "0815*中秋节",
            "0827 先师诞",
            "0909 重阳节",
            "1001 祭祖节 祀靴节",
            "1025 感天上帝诞",
            "1117 阿弥陀佛圣诞",
            "1208 腊八节 释迦如来成道日",
            "1220 鲁班公诞",
            "1223 小年",
            "1224 祀灶",
            "0100*除夕"
        };

        //某月的第几个星期几
        public static string[ ] wFtv = new string[ ]
        {
            "0150 世界麻风日", //一月的最后一个星期日（月倒数第一个星期日）
            "0520 国际母亲节",
            "0530 全国助残日",
            "0630 父亲节",
            "0716 合作节",
            "0730 被奴役国家周",
            "0932 国际和平日",
            "0940 国际聋人节 世界儿童日",
            "0950 世界海事日",
            "1011 国际住房日",
            "1013 国际减轻自然灾害日(减灾日)",
            "1144 感恩节"
        };

        public static string[ ] jcName0 = new string[ ] { "建", "除", "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭" };
        public static string[ ] jcName1 = new string[ ] { "闭", "建", "除", "满", "平", "定", "执", "破", "危", "成", "收", "开" };
        public static string[ ] jcName2 = new string[ ] { "开", "闭", "建", "除", "满", "平", "定", "执", "破", "危", "成", "收" };
        public static string[ ] jcName3 = new string[ ] { "收", "开", "闭", "建", "除", "满", "平", "定", "执", "破", "危", "成" };
        public static string[ ] jcName4 = new string[ ] { "成", "收", "开", "闭", "建", "除", "满", "平", "定", "执", "破", "危" };
        public static string[ ] jcName5 = new string[ ] { "危", "成", "收", "开", "闭", "建", "除", "满", "平", "定", "执", "破" };
        public static string[ ] jcName6 = new string[ ] { "破", "危", "成", "收", "开", "闭", "建", "除", "满", "平", "定", "执" };
        public static string[ ] jcName7 = new string[ ] { "执", "破", "危", "成", "收", "开", "闭", "建", "除", "满", "平", "定" };
        public static string[ ] jcName8 = new string[ ] { "定", "执", "破", "危", "成", "收", "开", "闭", "建", "除", "满", "平" };
        public static string[ ] jcName9 = new string[ ] { "平", "定", "执", "破", "危", "成", "收", "开", "闭", "建", "除", "满" };
        public static string[ ] jcName10 = new string[ ] { "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭", "建", "除" };
        public static string[ ] jcName11 = new string[ ] { "除", "满", "平", "定", "执", "破", "危", "成", "收", "开", "闭", "建" };

        public static string[ ] jcrjxy = new string[ ]
        {
            "出行.上任.会友.上书.见工", "除服.疗病.出行.拆卸.入宅",
            "祈福.祭祀.结亲.开市.交易", "祭祀.修填.涂泥.余事勿取",
            "易.立券.会友.签约.纳畜", "祈福.祭祀.求子.结婚.立约",
            "求医.赴考.祭祀.余事勿取", "经营.交易.求官.纳畜.动土",
            "祈福.入学.开市.求医.成服", "祭祀.求财.签约.嫁娶.订盟",
            "疗病.结婚.交易.入仓.求职", "祭祀.交易.收财.安葬"
        };

        public static string[ ] jcrjxj = new string[ ]{
            "动土.开仓.嫁娶.纳采", "求官.上任.开张.搬家.探病",
            "服药.求医.栽种.动土.迁移", "移徙.入宅.嫁娶.开市.安葬",
            "种植.置业.卖田.掘井.造船", "开市.交易.搬家.远行",
            "动土.出行.移徙.开市.修造", "登高.行船.安床.入宅.博彩",
            "词讼.安门.移徙", "开市.安床.安葬.入宅.破土",
            "安葬.动土.针灸", "宴会.安床.出行.嫁娶.移徙"
        };

        public static string yj0 = "<font style='background-color:red; color:#FFF'>&nbsp;宜&nbsp;</font>&nbsp;";
        public static string yj1 = "<font style='background-color:green; color:#FFF'>&nbsp;忌&nbsp;</font>&nbsp;";

        public static string[ ] sk = new string[ ]{
            "子时", "一刻", "凶。", "二刻", "平。平。", "三刻", "吉。旺财丁。", "四刻", "吉。旺财丁。", "五刻", "吉。旺人丁。", "六刻", "平。平。", "七刻", "凶。", "八刻", "凶。",
            "丑时", "一刻", "凶。", "二刻", "凶。", "三刻", "吉。生贵子。", "四刻", "吉。吉利。", "五刻", "吉。大吉利。", "六刻", "凶。", "七刻", "凶。", "八刻", "凶。",
            "寅时", "一刻", "凶。", "二刻", "吉。旺子孙。", "三刻", "吉。发大财。", "四刻", "凶。", "五刻", "吉。旺人丁。", "六刻", "吉。旺人丁。", "七刻", "凶。", "八刻", "凶。"
            , "卯时", "一刻", "吉。发福吉。", "二刻", "凶。", "三刻", "凶。", "四刻", "吉。吉利。", "五刻", "凶。", "六刻", "吉。发人丁。", "七刻", "吉。吉利。", "八刻", "吉。发横财。",
            "辰时", "一刻", "吉。旺财吉。", "二刻", "凶。", "三刻", "吉。旺财丁。", "四刻", "吉。大旺财。", "五刻", "凶。", "六刻", "吉。大旺财。", "七刻", "吉。吉利。", "八刻", "吉。发福吉。",
            "巳时", "一刻", "吉。发横财。", "二刻", "吉。发横财。", "三刻", "吉。发横财。", "四刻", "吉。发横财。", "五刻", "吉。大福贵。", "六刻", "吉。大福贵。", "七刻", "凶。", "八刻", "凶。",
            "午时", "一刻", "凶。", "二刻", "平。平。", "三刻", "吉。吉利。", "四刻", "吉。旺财吉。", "五刻", "吉。吉利。", "六刻", "吉。旺六畜。", "七刻", "凶。", "八刻", "凶。",
            "未时", "一刻", "凶。", "二刻", "凶。", "三刻", "吉。旺财丁。", "四刻", "吉。旺财丁。", "五刻", "吉。旺财丁。", "六刻", "吉。旺财丁。", "七刻", "吉。旺六畜。", "八刻", "凶。",
            "申时", "一刻", "凶。", "二刻", "凶。", "三刻", "凶。", "四刻", "凶。", "五刻", "吉。旺财丁。", "六刻", "吉。旺财丁。", "七刻", "凶。", "八刻", "凶。",
            "酉时", "一刻", "吉。发财吉。", "二刻", "吉。发财吉。", "三刻", "吉。发财吉。", "四刻", "吉。发财吉。", "五刻", "吉。大吉利。", "六刻", "吉。发财吉。", "七刻", "凶。", "八刻", "凶。",
            "戍时", "一刻", "吉。发财丁。", "二刻", "吉。发财丁。", "三刻", "凶。", "四刻", "凶。", "五刻", "凶。", "六刻", "凶。", "七刻", "凶。", "八刻", "凶。",
            "亥时", "一刻", "吉。吉利。", "二刻", "凶。", "三刻", "凶。", "四刻", "凶。", "五刻", "凶。", "六刻", "凶。", "七刻", "凶。", "八刻", "凶。"
        };

        public static string jcrt( string d )
        {
            string jcrjxt = "";
            if ( d == "建" )
            {
                jcrjxt = yj0 + jcrjxy[ 0 ] + "&nbsp; " + yj1 + jcrjxj[ 0 ];
            }

            if ( d == "除" )
            {
                jcrjxt = yj0 + jcrjxy[ 1 ] + "&nbsp; " + yj1 + jcrjxj[ 1 ];
            }

            if ( d == "满" )
            {
                jcrjxt = yj0 + jcrjxy[ 2 ] + "&nbsp; " + yj1 + jcrjxj[ 2 ];
            }

            if ( d == "平" )
            {
                jcrjxt = yj0 + jcrjxy[ 3 ] + "&nbsp; " + yj1 + jcrjxj[ 3 ];
            }

            if ( d == "定" )
            {
                jcrjxt = yj0 + jcrjxy[ 4 ] + "&nbsp; " + yj1 + jcrjxj[ 4 ];
            }

            if ( d == "执" )
            {
                jcrjxt = yj0 + jcrjxy[ 5 ] + "&nbsp; " + yj1 + jcrjxj[ 5 ];
            }

            if ( d == "破" )
            {
                jcrjxt = yj0 + jcrjxy[ 6 ] + "&nbsp; " + yj1 + jcrjxj[ 6 ];
            }

            if ( d == "危" )
            {
                jcrjxt = yj0 + jcrjxy[ 7 ] + "&nbsp; " + yj1 + jcrjxj[ 7 ];
            }

            if ( d == "成" )
            {
                jcrjxt = yj0 + jcrjxy[ 8 ] + "&nbsp; " + yj1 + jcrjxj[ 8 ];
            }

            if ( d == "收" )
            {
                jcrjxt = yj0 + jcrjxy[ 9 ] + "&nbsp; " + yj1 + jcrjxj[ 9 ];
            }

            if ( d == "开" )
            {
                jcrjxt = yj0 + jcrjxy[ 10 ] + "&nbsp; " + yj1 + jcrjxj[ 10 ];
            }

            if ( d == "闭" )
            {
                jcrjxt = yj0 + jcrjxy[ 11 ] + "&nbsp; " + yj1 + jcrjxj[ 11 ];
            }

            return ( jcrjxt );
        }

        public static string jcr( string d )
        {
            string jcrjx = "";
            if ( d == "建" )
            {
                jcrjx = yj0 + jcrjxy[ 0 ] + "<br>" + yj1 + jcrjxj[ 0 ];
            }

            if ( d == "除" )
            {
                jcrjx = yj0 + jcrjxy[ 1 ] + "<br>" + yj1 + jcrjxj[ 1 ];
            }

            if ( d == "满" )
            {
                jcrjx = yj0 + jcrjxy[ 2 ] + "<br>" + yj1 + jcrjxj[ 2 ];
            }

            if ( d == "平" )
            {
                jcrjx = yj0 + jcrjxy[ 3 ] + "<br>" + yj1 + jcrjxj[ 3 ];
            }

            if ( d == "定" )
            {
                jcrjx = yj0 + jcrjxy[ 4 ] + "<br>" + yj1 + jcrjxj[ 4 ];
            }

            if ( d == "执" )
            {
                jcrjx = yj0 + jcrjxy[ 5 ] + "<br>" + yj1 + jcrjxj[ 5 ];
            }

            if ( d == "破" )
            {
                jcrjx = yj0 + jcrjxy[ 6 ] + "<br>" + yj1 + jcrjxj[ 6 ];
            }

            if ( d == "危" )
            {
                jcrjx = yj0 + jcrjxy[ 7 ] + "<br>" + yj1 + jcrjxj[ 7 ];
            }

            if ( d == "成" )
            {
                jcrjx = yj0 + jcrjxy[ 8 ] + "<br>" + yj1 + jcrjxj[ 8 ];
            }

            if ( d == "收" )
            {
                jcrjx = yj0 + jcrjxy[ 9 ] + "<br>" + yj1 + jcrjxj[ 9 ];
            }

            if ( d == "开" )
            {
                jcrjx = yj0 + jcrjxy[ 10 ] + "<br>" + yj1 + jcrjxj[ 10 ];
            }

            if ( d == "闭" )
            {
                jcrjx = yj0 + jcrjxy[ 11 ] + "<br>" + yj1 + jcrjxj[ 11 ];
            }

            return ( jcrjx );
        }

        public static string cyclical2( int num, int num2 )
        {
            if ( num == 0 )
            {
                return ( jcName0[ num2 ] );
            }

            if ( num == 1 )
            {
                return ( jcName1[ num2 ] );
            }

            if ( num == 2 )
            {
                return ( jcName2[ num2 ] );
            }

            if ( num == 3 )
            {
                return ( jcName3[ num2 ] );
            }

            if ( num == 4 )
            {
                return ( jcName4[ num2 ] );
            }

            if ( num == 5 )
            {
                return ( jcName5[ num2 ] );
            }

            if ( num == 6 )
            {
                return ( jcName6[ num2 ] );
            }

            if ( num == 7 )
            {
                return ( jcName7[ num2 ] );
            }

            if ( num == 8 )
            {
                return ( jcName8[ num2 ] );
            }

            if ( num == 9 )
            {
                return ( jcName9[ num2 ] );
            }

            if ( num == 10 )
            {
                return ( jcName10[ num2 ] );
            }

            if ( num == 11 )
            {
                return ( jcName11[ num2 ] );
            }

            return string.Empty;
        }

        public static string CalConv2( int yy, int mm, int dd, int y, int d, int m, int dt, int nm )
        {
            var dy = d + "" + dd;

            if ( ( yy == 0 && dd == 6 ) || ( yy == 6 && dd == 0 ) || ( yy == 1 && dd == 7 ) || ( yy == 7 && dd == 1 ) || ( yy == 2 && dd == 8 ) || ( yy == 8 && dd == 2 ) || ( yy == 3 && dd == 9 ) || ( yy == 9 && dd == 3 ) || ( yy == 4 && dd == 10 ) || ( yy == 10 && dd == 4 ) || ( yy == 5 && dd == 11 ) || ( yy == 11 && dd == 5 ) )
            {
                return "<FONT color='#0000A0'>日值岁破 大事不宜</font>";
            }
            else if ( ( mm == 0 && dd == 6 ) || ( mm == 6 && dd == 0 ) || ( mm == 1 && dd == 7 ) || ( mm == 7 && dd == 1 ) || ( mm == 2 && dd == 8 ) || ( mm == 8 && dd == 2 ) || ( mm == 3 && dd == 9 ) || ( mm == 9 && dd == 3 ) || ( mm == 4 && dd == 10 ) || ( mm == 10 && dd == 4 ) || ( mm == 5 && dd == 11 ) || ( mm == 11 && dd == 5 ) )
            {
                return "<FONT color='#0000A0'>日值月破 大事不宜</font>";
            }
            else if ( ( y == 0 && dy == "911" ) || ( y == 1 && dy == "55" ) || ( y == 2 && dy == "111" ) || ( y == 3 && dy == "75" ) || ( y == 4 && dy == "311" ) || ( y == 5 && dy == "95" ) || ( y == 6 && dy == "511" ) || ( y == 7 && dy == "15" ) || ( y == 8 && dy == "711" ) || ( y == 9 && dy == "35" ) )
            {
                return "<FONT color='#0000A0'>日值上朔 大事不宜</font>";
            }
            else if ( ( m == 1 && dt == 13 ) || ( m == 2 && dt == 11 ) || ( m == 3 && dt == 9 ) || ( m == 4 && dt == 7 ) || ( m == 5 && dt == 5 ) || ( m == 6 && dt == 3 ) || ( m == 7 && dt == 1 ) || ( m == 7 && dt == 29 ) || ( m == 8 && dt == 27 ) || ( m == 9 && dt == 25 ) || ( m == 10 && dt == 23 ) || ( m == 11 && dt == 21 ) || ( m == 12 && dt == 19 ) )
            {
                return "<FONT color='#0000A0'>日值杨公十三忌 大事不宜</font>";
            }
            else
            {
                return string.Empty;
            }
        }

        /*****************************************************************************
        日期计算
        *****************************************************************************/

        //====================================== 返回农历 y年的总天数
        public static int lYearDays( int y )
        {
            int i, sum = 348;

            for ( i = 0x8000; i > 0x8; i >>= 1 )
            {
                sum += ( LunarInfoList[ y - 1900 ] & i ) > 0 ? 1 : 0;
            }

            return ( sum + leapDays( y ) );
        }


        //====================================== 返回农历 y年闰月的天数
        public static int leapDays( int y )
        {
            if ( leapMonth( y ) > 0 )
            {
                return ( ( LunarInfoList[ y - 1899 ] & 0xf ) == 0xf ? 30 : 29 );
            }
            else
            {
                return ( 0 );
            }
        }

        public static bool isLeapYear( int y )
        {
            return ( y % 4 == 0 && y % 100 != 0 || y % 400 == 0 );
        }

        

        //====================================== 返回农历 y年闰哪个月 1-12 , 没闰返回 0
        public static int leapMonth( int y )
        {
            var lm = LunarInfoList[ y - 1900 ] & 0xf;
            return ( lm == 0xf ? 0 : lm );
        }

        //====================================== 返回农历 y年m月的总天数
        public static int monthDays( int y, int m )
        {
            return ( ( LunarInfoList[ y - 1900 ] & ( 0x10000 >> m ) ) > 0 ? 30 : 29 );
        }



        //==============================返回公历 y年某m+1月的天数
        public static int solarDays( int y, int m)
        {
            if ( m == 1 )
                return ( ( ( y % 4 == 0 ) && ( y % 100 != 0 ) || ( y % 400 == 0 ) ) ? 29 : 28 );
            else
                return ( SolarMonth[ m ] );
        }

        //============================== 传入 offset 返回干支, 0=甲子
        public static string cyclical( int num)
        {
            return ( HeavenlyStem[ num % 10 ] + EarthyBranch[ num % 12 ] );
        }



        //===== 某年的第n个节气为几日(从0小寒起算)
        public static int sTermJS( int y, int n)
        {
            double mill = ( 31556925974.7 * ( y - 1900 ) * 1000 + sTermInfo[ n ] * 60000 );
            var offDate = new DateTime( 1900, 1, 6, 2, 5, 0, DateTimeKind.Utc ).AddMilliseconds( mill );
            return ( offDate.Day );
        }

        // https://kknews.cc/tech/ljj5y92.html
        public static int sTerm( int y, int n )
        {
            // 某年的第n個節氣為幾日(從0小寒起算) 
            double ms = 31556925974.7*(y-1900); // 31556925974.7是一年(365.2422)的豪秒數 
            double tp = sTermInfo[ n ] * 60;    // 1分鐘60000毫秒,sTermInfo記錄的是分鐘數 

            for ( int ii = 0; ii < 3; ii++ )
            {
                // 處理溢出 
                tp *= 10;
            }

            ms += tp;
            ms += ((6*24+2)*60+5)*60*1000; // 1900年1月6號2時5分為小寒 

            for ( int i = 1900; i < y; i++ )
            {
                for ( int k = 1; k <= 365; k++ )
                {
                    ms -= 24 * 3600000;
                }

                if ( isLeapYear( i ) )
                {
                    ms -= 24 * 3600000;
                }
                
                //ms-=366*24*3600000; // integral constant overflow } 
            }

            double ds = ms / ( 24 * 3600000 );
            int[ ]  monDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if ( isLeapYear( y ) )
            {
                monDays[ 1 ] = 29;
                for ( int j = 0; j < 12; j++ )
                {
                    if ( ds > monDays[ j ] )
                    {
                        ds -= monDays[ j ];
                    }
                    
                }
            }

            return ( ( int )ds );
        }

            

            //============================== 返回阴历控件 (y年,m+1月)
            /*
            功能说明: 返回整个月的日期资料控件
            使用方式: OBJ = new calendar(年,零起算月);
            OBJ.length      返回当月最大日
            OBJ.firstWeek   返回当月一日星期
            由 OBJ[日期].属性名称 即可取得各项值
            OBJ[日期].isToday  返回是否为今日 true 或 false
            其他 OBJ[日期] 属性参见 calElement() 中的注解
            */

        public static string cyclical3( int num)
        {
            return ( StemBranchTable1[ num % 10 ] );
        }

        public static string cyclical4( int num)
        {
            return ( StemBranchTable2[ num % 12 ] );
        }


        public static string cDay( int d )
        {
            string s;
            switch ( d )
            {
                case 10:
                {
                    s = "初十";
                }
                break;

                case 20:
                {
                    s = "二十";
                }
                break;

                case 30:
                {
                    s = "三十"; 
                }
                break;
                
                default:
                {
                    s = nStr2[ d / 10 ];
                    s += nStr1[ d % 10 ];
                }
                break;
                
            }
            return ( s );
        }

    }
}
