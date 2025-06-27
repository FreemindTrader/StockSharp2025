using fx.Definitions;
using SciChart.Charting.Model.DataSeries;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Reflection;
using StockSharp.Algo.Candles;
using StockSharp.Messages;
using StockSharp.BusinessEntities;
using System.Runtime.CompilerServices;
using fx.Bars;


#pragma warning disable 067

namespace fx.Bars
{
    [StructLayout( LayoutKind.Sequential, Size = 56 )]
    public partial struct SBar : IPointMetadata, IEquatable<SBar>
    {
        #region Variables 
        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Always make sure the largest size memory has to be put in the first place.
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */
        public event PropertyChangedEventHandler      PropertyChanged;            // 8    => 8        
        
        private SBarList          _parent;                                        // 8    => 16        
        public long               LinuxTime;                                      // 8    => 24

        //private uint _barDiff;
        // _low   =  0 -  9 bits
        // _close = 10 - 19 bits
        // _high  = 20 - 29 bits

        private float              _open;
        private float              _high;                                           // 4    => 28
        private float              _low;                                            // 4    => 32        
        private float              _close;                                          // 4    => 36
        public uint               Volume;                                         // 4    => 40

        public uint               _barIndex;                                      // 4    => 44                

        public SBarFeatures       Features;                                       // 4    => 52                        
        
        

        public float High
        {
            get
            {
                return _high;
            }

            set
            {
                _high = value;
            }
        }

        public float Low
        {
            get
            {
                return _low;
            }

            set
            {
                _low = value;
            }
        }

        public float Close
        {
            get
            {
                return _close;
            }

            set
            {
                _close = value;
            }
        }

        public float Open
        {
            get
            {
                return _open;
            }

            set
            {
                _open = value;
            }
        }




        #endregion

        public static SBar  EmptySBar = new SBar( 0 );

        public uint BarIndex
        {
            get => _barIndex;
        }

        public SBarList Parent
        {
            get => _parent;
            set => _parent = value;
        }

        public SymbolEx SymbolEx
        {
            get
            {
                if ( _parent != null )
                {
                    return _parent.SymbolEx;
                }

                return default;
            }
        }

        public int Index
        {
            get => ( int ) _barIndex;

        }

        public bool IsSelected { get => Features.IsSelected; set => Features.IsSelected = value; }

        public SBar( int i )
        {
            _parent = null;
            PropertyChanged      = null;
            LinuxTime            = 0;

            _high                 = -1;
            _low                  = -1;
            _open                 = -1;
            _close                = -1;

            Volume               = 1;
            
            _barIndex            = 0;
            Features             = new SBarFeatures();

            Features.State       = CandleStates.None;
        }


        public SBar( int i, ICandleMessage candle, AdvBarInfo advBarInfo )
        {
            _parent = null;

            PropertyChanged      = null;
            LinuxTime            = candle.OpenTime.UtcDateTime.ToLinuxTime();

            _high                 = ( float ) candle.HighPrice;
            _low                  = ( float ) candle.LowPrice;
            _open                 = ( float ) candle.OpenPrice;
            _close                = ( float ) candle.ClosePrice;

            Volume               = ( uint ) candle.TotalVolume;
            
            _barIndex            = ( uint ) i;
            Features             = new SBarFeatures();                      

            Features.State       = candle.State;

            SetBarSessionStatus();
        }


        public SBar(ICandleMessage candle )
        {
            _parent         = null;
            PropertyChanged = null;
            LinuxTime       = candle.OpenTime.UtcDateTime.ToLinuxTime();

            _high            = ( float ) candle.HighPrice;
            _low             = ( float ) candle.LowPrice;
            _open            = ( float ) candle.OpenPrice;
            _close           = ( float ) candle.ClosePrice;

            Volume          = ( uint ) candle.TotalVolume;
            
            _barIndex       = 0;
            Features        = new SBarFeatures();            
            
            Features.State  = candle.State;

            SetBarSessionStatus();
        }

        public SBar( SymbolEx symbol, uint i )
        {
            _parent         = null;
            PropertyChanged = null;
            LinuxTime       = 0;

            _high            = 0.0f;
            _low             = 0.0f;
            _open            = 0.0f;
            _close           = 0.0f;

            Volume          = 0;
            
            _barIndex       = i;
            Features        = new SBarFeatures();                      
        }


        public SBar( SBarList parent, float test, uint i )
        {
            _parent         = parent;
            PropertyChanged = null;
            LinuxTime       = i * 60;

            _high            = test;
            _low             = test;
            _open            = test;
            _close           = test;

            Volume          = 0;
            
            _barIndex       = i;
            Features        = new SBarFeatures();                     
        }

        public SBar NewNullBar()
        {
            var tmpOpen    = _close;

            var newBarTime = LinuxTime.FromLinuxTime() + BarPeriod;
            LinuxTime      = newBarTime.ToLinuxTime();

            _high           = tmpOpen;
            _low            = tmpOpen;
            _open           = tmpOpen;
            _close          = tmpOpen;

            _barIndex++;

            return this;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void Update(ICandleMessage candle )
        {
            _high   = ( float ) candle.HighPrice;
            _low    = ( float ) candle.LowPrice;
            _open   = ( float ) candle.OpenPrice;
            _close  = ( float ) candle.ClosePrice;
            Volume = ( uint ) candle.TotalVolume;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void CopyFrom(ICandleMessage candle )
        {
            LinuxTime      = candle.OpenTime.UtcDateTime.ToLinuxTime();

            _high           = ( float ) candle.HighPrice;
            _low            = ( float ) candle.LowPrice;
            _open           = ( float ) candle.OpenPrice;
            _close          = ( float ) candle.ClosePrice;
            Volume         = ( uint ) candle.TotalVolume;
            
            Features       = new SBarFeatures();                      
            Features.State = candle.State;            
        }

        public DateTimeOffset ServerTime()
        {
            var returnTime = DateTimeHelper.FromLinuxTime( LinuxTime );

            return returnTime;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public void CopyFrom( int barIndex, ICandleMessage candle, AdvBarInfo info )
        {
            LinuxTime = candle.OpenTime.UtcDateTime.ToLinuxTime();

            _high      = ( float ) candle.HighPrice;
            _low       = ( float ) candle.LowPrice;
            _open      = ( float ) candle.OpenPrice;
            _close     = ( float ) candle.ClosePrice;
            Volume    = ( uint ) candle.TotalVolume;

            _barIndex = ( uint ) barIndex;            
            Features  = new SBarFeatures();            
            
            Features.State = candle.State;            
        }



        public CandleStates State
        {
            get => Features.State;
            set => Features.State = value;
        }

        public SessionEnum BarSession
        {
            get => Features.BarSession;
            set => Features.BarSession = value;
        }

        public float HighPrice => _high;
        public float LowPrice => _low;
        public float OpenPrice => _open;
        public float ClosePrice => _close;

        public string Code
        {
            get
            {
                return SymbolHelper.GetSymbolFromOfferId( SymbolEx.OfferId );
            }
        }

        private void DetectSession15t( DateTime newYorkTime, DateTime frankfurtTime, DateTime wellingtonTime, DateTime tokyoTime )
        {
            if ( wellingtonTime.Hour == 8 && wellingtonTime.Minute == 0 && wellingtonTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AustraliaSessionStart;
            }
            else if ( tokyoTime.Hour == 8 && tokyoTime.Minute == 0 && tokyoTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AsiaSessionStart;
            }
            else if ( frankfurtTime.Hour == 7 && frankfurtTime.Minute == 55 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.START;
            }
            else if ( frankfurtTime.Hour == 8 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.EuropeanSessionStart;
            }
            else if ( newYorkTime.Hour == 8 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.UsaSessionStart;
            }
            else if ( wellingtonTime.Hour == 17 && wellingtonTime.Minute == 0 && wellingtonTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AustraliaSessionEnd;
            }
            else if ( tokyoTime.Hour == 18 && tokyoTime.Minute == 0 && tokyoTime.Second == 0 )
            {
                // End of Hong Kong Session
                Features.BarSession = SessionEnum.AsiaSessionEnd;
            }
            else if ( frankfurtTime.Hour == 18 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
            {
                // End of London Session
                Features.BarSession = SessionEnum.EuropeanSessionEnd;
            }
            else if ( newYorkTime.Hour == 16 && newYorkTime.Minute == 55 && newYorkTime.Second == 0 )
            {
                // End of Chicago Session
                Features.BarSession = SessionEnum.DailySessionEnd;
            }
            else if ( newYorkTime.Hour == 17 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.DailySessionStart;
            }
        }

        private void DetectSession15m( DateTime newYorkTime, DateTime frankfurtTime, DateTime wellingtonTime, DateTime tokyoTime )
        {
            if ( wellingtonTime.Hour == 8 && wellingtonTime.Minute == 0 && wellingtonTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AustraliaSessionStart;
            }
            else if ( tokyoTime.Hour == 8 && tokyoTime.Minute == 0 && tokyoTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AsiaSessionStart;
            }
            else if ( frankfurtTime.Hour == 7 && frankfurtTime.Minute == 45 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.START;
            }
            else if ( frankfurtTime.Hour == 8 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.EuropeanSessionStart;
            }
            else if ( newYorkTime.Hour == 8 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.UsaSessionStart;
            }
            else if ( wellingtonTime.Hour == 17 && wellingtonTime.Minute == 0 && wellingtonTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AustraliaSessionEnd;
            }
            else if ( tokyoTime.Hour == 18 && tokyoTime.Minute == 0 && tokyoTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AsiaSessionEnd;
            }
            else if ( frankfurtTime.Hour == 18 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.EuropeanSessionEnd;
            }
            else if ( newYorkTime.Hour == 16 && newYorkTime.Minute == 45 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.DailySessionEnd;
            }
            else if ( newYorkTime.Hour == 17 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.DailySessionStart;
            }
        }

        private void DetectSession1Hr( DateTime newYorkTime, DateTime frankfurtTime, DateTime wellingtonTime, DateTime tokyoTime )
        {
            if ( wellingtonTime.Hour == 8 && wellingtonTime.Minute == 0 && wellingtonTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AustraliaSessionStart;
            }
            else if ( tokyoTime.Hour == 8 && tokyoTime.Minute == 0 && tokyoTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AsiaSessionStart;
            }
            else if ( frankfurtTime.Hour == 8 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.EuropeanSessionStart;
            }
            else if ( newYorkTime.Hour == 8 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.UsaSessionStart;
            }
            else if ( wellingtonTime.Hour == 17 && wellingtonTime.Minute == 0 && wellingtonTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AustraliaSessionEnd;
            }
            else if ( tokyoTime.Hour == 18 && tokyoTime.Minute == 0 && tokyoTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.AsiaSessionEnd;
            }
            else if ( frankfurtTime.Hour == 18 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.EuropeanSessionEnd;
            }
            else if ( newYorkTime.Hour == 16 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.DailySessionEnd;
            }
            else if ( newYorkTime.Hour == 17 && newYorkTime.Minute == 0 && newYorkTime.Second == 0 )
            {
                Features.BarSession = SessionEnum.DailySessionStart;
            }
        }

        void SetBarSessionStatus()
        {
            DateTime newYorkTime    = NewYorkTime.Truncate( TimeSpan.FromMinutes( 1 ) );
            DateTime frankfurtTime  = FrankfurtTime.Truncate( TimeSpan.FromMinutes( 1 ) );
            DateTime wellingtonTime = WellingtonTime.Truncate( TimeSpan.FromMinutes( 1 ) );
            DateTime tokyoTime      = TokyoTime.Truncate( TimeSpan.FromMinutes( 1 ) );
            DateTime chinaTime      = ChinaTime.Truncate( TimeSpan.FromMinutes( 1 ) );

            if ( SymbolEx.SecurityType == SmallSecurityTypes.Currency )
            {
                if ( ( BarPeriod == TimeSpan.FromMinutes( 5 ) ) || ( BarPeriod == TimeSpan.FromMinutes( 1 ) ) || ( BarPeriod == TimeSpan.FromTicks( 1 ) ) )
                {
                    DetectSession15t( newYorkTime, frankfurtTime, wellingtonTime, tokyoTime );
                }
                else if ( BarPeriod == TimeSpan.FromMinutes( 15 ) )
                {
                    DetectSession15m( newYorkTime, frankfurtTime, wellingtonTime, tokyoTime );
                }
                else if ( BarPeriod == TimeSpan.FromMinutes( 1 ) )
                {
                    DetectSession1Hr( newYorkTime, frankfurtTime, wellingtonTime, tokyoTime );
                }
            }

            switch ( Code )
            {
                case "EUR/USD":
                case "USD/CHF":
                case "GBP/USD":
                case "XAU/USD":
                {
                    if ( Features.BarSession == SessionEnum.NONE )
                    {
                        if ( frankfurtTime.Hour >= 8 && newYorkTime.Hour < 18 )
                        {
                            Features.BarSession = SessionEnum.Active;
                        }
                        else
                        {
                            Features.BarSession = SessionEnum.NonActive;
                        }
                    }
                }
                break;

                case "USD/JPY":
                {
                    if ( Features.BarSession == SessionEnum.NONE )
                    {
                        if ( tokyoTime.Hour >= 8 && tokyoTime.Hour < 18 )
                        {
                            Features.BarSession = SessionEnum.Active;
                        }
                        else
                        {
                            Features.BarSession = SessionEnum.NonActive;
                        }
                    }
                }
                break;

                case "UK100":
                {
                    if ( frankfurtTime.Hour == 8 && frankfurtTime.Minute == 0 && frankfurtTime.Second == 0 )
                    {
                        Features.BarSession = SessionEnum.EuropeanSessionStart;
                    }
                    else if ( frankfurtTime.Hour == 17 && frankfurtTime.Minute == 30 && frankfurtTime.Second == 0 )
                    {
                        Features.BarSession = SessionEnum.EuropeanSessionEnd;
                    }
                    else if ( frankfurtTime.Hour >= 8 && frankfurtTime.Hour <= 17 )
                    {
                        Features.BarSession = SessionEnum.Active;
                    }
                    else
                    {
                        Features.BarSession = SessionEnum.NonActive;
                    }
                }
                break;

                case "HKG33":
                {
                    if ( chinaTime.Hour == 9 && chinaTime.Minute == 15 && chinaTime.Second == 0 )
                    {
                        Features.BarSession = SessionEnum.AsiaSessionStart;
                    }
                    else if ( chinaTime.Hour == 16 && chinaTime.Minute == 10 && chinaTime.Second == 0 )
                    {
                        Features.BarSession = SessionEnum.AsiaSessionEnd;
                    }
                    else
                    {
                        Features.BarSession = SessionEnum.Active;
                    }
                }
                break;
            }

            if ( newYorkTime.DayOfWeek == DayOfWeek.Sunday && newYorkTime.Hour == 21 && newYorkTime.Minute == 0 )
            {
                Features.BarSession = SessionEnum.WeeklySessionBegin;
            }
        }

        public bool IsRising
        {
            get
            {
                return _close > _open;
            }
        }

        

        public string PeriodFormat()
        {
            string output = "MM/dd/yy hh:mm";

            if ( BarPeriod > TimeSpan.FromHours( 1 ) )
            {
                output = "MM/dd/yy hh";
            }
            //else if ( BarPeriod == TimeSpan.FromMinutes( 5 ) )
            //{
            //    output = "MM/dd/yy hh:mm";
            //}
            //else if ( BarPeriod == TimeSpan.FromMinutes( 15 ) )
            //{
            //    output = "MM/dd/yy hh:mm";
            //}
            //else if ( BarPeriod == TimeSpan.FromMinutes( 30 ) )
            //{
            //    output = "MM/dd/yy hh:mm";
            //}
            //else if ( BarPeriod == TimeSpan.FromHours( 1 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromHours( 2 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromHours( 3 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromHours( 4 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromHours( 6 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromHours( 8 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromDays( 1 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromDays( 7 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}
            //else if ( BarPeriod == TimeSpan.FromDays( 30 ) )
            //{
            //    output = "MM/dd/yy hh";
            //}

            return output;
        }

        public override string ToString()
        {
            string format = ( BarPeriod > TimeSpan.FromHours( 1 ) ) ? "MM/dd/yy hh tt" :"MM/dd/yy hh:mm tt";

            string output = BarIndex.ToString().PadLeft( 4 ) + "-" + "[" + BarTime.ToString( format ) + "]";            

            if ( HasSignal )
            {
                output = output + "," + GetBarSignalString();
            }

            if ( HasCandleStickPattern )
            {
                output = output + "," + CandleStickPatten;
            }

            if ( HasWaveRotation )
            {
                output = output + "," + WaveRotationInfoString;
            }

            if ( HasGannSquare )
            {
                output = output + "," + GannSquareInfoString;
            }

            if ( HasDivergence )
            {
                output = output + "," + DivergenceInfoString;
            }

            return output;
        }

        public override bool Equals( object obj )
        {
            if ( obj is SBar )
            {
                return Equals( ( SBar ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( SBar first, SBar second )
        {
            return first.Equals( second );
        }

        public static bool operator !=( SBar first, SBar second )
        {
            return !( first == second );
        }

        public bool Equals( SBar other )
        {
            return 
                    LinuxTime.Equals( other.LinuxTime ) &&
                    _high.Equals( other._high ) &&
                    _low.Equals( other._low ) &&
                    _open.Equals( other._open ) &&
                    _close.Equals( other._close ) &&
                    BarIndex.Equals( other.BarIndex ) &&
                    Volume.Equals( other.Volume ) &&
                    SymbolEx.Equals( other.SymbolEx ) &&
                    Features.Equals( other.Features );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                
                hashCode = ( hashCode * 53 ) ^ LinuxTime.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _high.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _low.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _open.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _close.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ BarIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ Volume.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<SymbolEx>.Default.GetHashCode( SymbolEx );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<SBarFeatures>.Default.GetHashCode( Features );                

                return hashCode;
            }
        }

        
    }
}
