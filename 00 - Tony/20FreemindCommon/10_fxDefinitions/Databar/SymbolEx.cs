using StockSharp.BusinessEntities;
using StockSharp.Algo;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum TimeSpanEnum
    {
        Tick    = 0,
        Sec01   = 1,
        Min01   = 2,
        Min04   = 3,
        Min05   = 4,
        Min15   = 5,
        Min30   = 6,
        Hrs01   = 7,
        Hrs02   = 8,
        Hrs03   = 9,
        Hrs04   = 10,
        Hrs06   = 11,
        Hrs08   = 12,
        Daily   = 13,
        Weekly  = 14,
        Monthly = 15,
    }

    public enum SessionEnum 
    {
        NONE                  = 0,
        DailySessionStart     = 1,
        DailySessionEnd       = 2,
        EuropeanSessionStart  = 3,
        EuropeanSessionEnd    = 4,
        UsaSessionStart       = 5,
        WeeklySessionBegin    = 6,
        NonActive             = 7,                
        Active                = 8,
        START                 = 9,
        AustraliaSessionStart = 10,
        AustraliaSessionEnd   = 11,
        AsiaSessionStart      = 12,
        AsiaSessionEnd        = 13,
        VolatileNewsEvent     = 14,        
    }

    public enum SmallSecurityTypes : byte
    {
        Currency       = 0,
        Stock          = 1,
        Cfd            = 2,
        Commodity      = 3,
        CryptoCurrency = 4,
        Etf            = 5,
        Bond           = 6,
        Future         = 7,
        Option         = 8,
        Index          = 9,        
        Fund           = 10,
        Warrant        = 11,
        Forward        = 12,
        Swap           = 13,
    }

    /// <summary>
    /// 


    /// Bit 32 - 09  = Will be used to represent the offer ID of the Instrument ( Since I am using mainly FXCM for now )
    /// Bit 08 - 05  = Four bit will be used to represent  SmallSecurityTypes    
    /// Bit 04 - 01  = Four bit will be used to represent timeframe
    public struct SymbolEx : IEquatable<SymbolEx>
    {
        private uint _symbolBit;

        public SymbolEx( Security security, SecurityTypes secType, TimeSpan period )
        {
            _symbolBit  = 0;
            var offerId = GetOfferId( security );

            if ( offerId  > 0 )
            {
                var periodBit = ( uint ) GetPeriodEnum( period );

                _symbolBit    = _symbolBit | periodBit;                

                uint myType   = (uint) ToSecurityBit( secType );

                _symbolBit    = BitHelper.SetBits( _symbolBit, myType, 1, BarBits.POS_SYM_TYPE, 4 );

                _symbolBit    = BitHelper.SetBits( _symbolBit, (uint) offerId, 1, BarBits.POS_SYM_OFFERID, 24 );
            }            
        }

        public SmallSecurityTypes SecurityType
        {
            get
            {
                var type = BitHelper.GetBits( _symbolBit, BarBits.POS_SYM_TYPE, 4, false );

                return ( SmallSecurityTypes ) type;
            }

            set
            {
                uint secType = (uint) value;

                _symbolBit = BitHelper.SetBits( _symbolBit, secType, 1, BarBits.POS_SYM_TYPE, 4 );
            }
        }

        public TimeSpan Period
        {
            get
            {
                if ( IsValid )
                {
                    var period = BitHelper.GetBits( _symbolBit, BarBits.POS_SYM_TIME, 4, false );

                    return ( GetPeriod( period ) );
                }

                return TimeSpan.Zero;
            }
        }

        public int OfferId
        {
            get
            {
                if ( IsValid )
                {                    
                    return ( int ) BitHelper.GetBits( _symbolBit, BarBits.POS_SYM_OFFERID, 24, false );
                }

                return 0;
            }
        }

        public bool IsForexPair()
        {
            return SecurityType == SmallSecurityTypes.Currency;
        }

        public float PriceStep
        {
            get
            {
                return SymbolHelper.GetInstrumentPointSize( OfferId );
            }
        }


        public static SmallSecurityTypes ToSecurityBit( SecurityTypes type )
        {
            switch ( type )
            {
                case SecurityTypes.Stock:
                    return SmallSecurityTypes.Stock;
                    
                case SecurityTypes.Future:
                    return SmallSecurityTypes.Future;

                case SecurityTypes.Option:
                    return SmallSecurityTypes.Option;

                case SecurityTypes.Index:
                    return SmallSecurityTypes.Index;

                case SecurityTypes.Currency:
                    return SmallSecurityTypes.Currency;

                case SecurityTypes.Bond:
                    return SmallSecurityTypes.Bond;

                case SecurityTypes.Warrant:
                    return SmallSecurityTypes.Warrant;

                case SecurityTypes.Forward:
                    return SmallSecurityTypes.Forward;

                case SecurityTypes.Swap:
                    return SmallSecurityTypes.Swap;

                case SecurityTypes.Commodity:
                    return SmallSecurityTypes.Commodity;

                case SecurityTypes.Cfd:
                    return SmallSecurityTypes.Cfd;
                
                case SecurityTypes.Fund:
                    return SmallSecurityTypes.Fund;
                
                case SecurityTypes.CryptoCurrency:
                    return SmallSecurityTypes.CryptoCurrency;

                case SecurityTypes.Etf:
                    return SmallSecurityTypes.Etf;

                default:
                    throw new NotSupportedException( );
            }
        }


        

        public bool IsValid
        {
            get
            {
                return _symbolBit > 0;
            }
        }

        

        

        public static int GetOfferId( Security instrument )
        {
            var id = instrument.ToSecurityId( );

            var storage = ServicesRegistry.NativeIdStorage;

            if ( storage != null )
            {
                string native = ( string ) storage.TryGetBySecurityId( "FxConnectFXCM", id );

                if ( native != null )
                return Int32.Parse( native );
            }

            return 1;
        }

        public static TimeSpanEnum GetPeriodEnum( TimeSpan period )
        {
            if ( period == TimeSpan.FromTicks( 1 ) )
            {
                return TimeSpanEnum.Tick;
            }
            else if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return TimeSpanEnum.Sec01;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return TimeSpanEnum.Min01;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return TimeSpanEnum.Min04;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return TimeSpanEnum.Min05;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return TimeSpanEnum.Min15;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return TimeSpanEnum.Min30;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return TimeSpanEnum.Hrs01;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return TimeSpanEnum.Hrs02;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                return TimeSpanEnum.Hrs03;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return TimeSpanEnum.Hrs04;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                return TimeSpanEnum.Hrs06;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                return TimeSpanEnum.Hrs08;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return TimeSpanEnum.Daily;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return TimeSpanEnum.Weekly;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return TimeSpanEnum.Monthly;
            }
            else
            {
                return TimeSpanEnum.Tick;
            }

        }

        public static TimeSpan GetPeriod( uint period )
        {
            if ( period == 0 )
            {
                return TimeSpan.FromTicks( 1 );
            }
            else if ( period == 1 )
            {
                return TimeSpan.FromSeconds( 1 );
            }
            else if ( period == 2 )
            {
                return TimeSpan.FromMinutes( 1 );
            }
            else if ( period == 3 )
            {
                return TimeSpan.FromMinutes( 4 );
            }
            else if ( period == 4 )
            {
                return TimeSpan.FromMinutes( 5 );
            }
            else if ( period == 5 )
            {
                return TimeSpan.FromMinutes( 15 );
            }
            else if ( period == 6 )
            {
                return TimeSpan.FromMinutes( 30 );
            }
            else if ( period == 7 )
            {
                return TimeSpan.FromHours( 1 );
            }
            else if ( period == 8 )
            {
                return TimeSpan.FromHours( 2 );
            }
            else if ( period == 9 )
            {
                return TimeSpan.FromHours( 3 );
            }
            else if ( period == 10 )
            {
                return TimeSpan.FromHours( 4 );
            }
            else if ( period == 11 )
            {
                return TimeSpan.FromHours( 6 );
            }
            else if ( period == 12 )
            {
                return TimeSpan.FromHours( 8 );
            }
            else if ( period == 13 )
            {
                return TimeSpan.FromDays( 1 );
            }
            else if ( period == 14 )
            {
                return TimeSpan.FromDays( 7 );
            }
            else if ( period == 15 )
            {
                return TimeSpan.FromDays( 30 );
            }
            else
            {
                throw new NotImplementedException( );
            }

        }

        public override bool Equals( object obj )
        {
            if ( obj is SymbolEx )
            {
                return Equals( ( SymbolEx ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( SymbolEx first, SymbolEx second )
        {
            return first.Equals( second );
        }

        public static bool operator !=( SymbolEx first, SymbolEx second )
        {
            return !( first == second );
        }

        public bool Equals( SymbolEx other )
        {
            return OfferId.Equals( other.OfferId ) && Period.Equals( other.Period );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;                                
                hashCode = ( hashCode * 53 ) ^ OfferId.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( Period );
                return hashCode;
            }
        }
    }
}
