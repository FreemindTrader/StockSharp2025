using System;
using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Indicators
{
    public static class IndicatorHelper
    {
        public static T Next<T>( this T src ) where T : struct
        {
            if ( !typeof( T ).IsEnum ) throw new ArgumentException( String.Format( "Argument {0} is not an Enum", typeof( T ).FullName ) );

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf( Arr, src) + 1;
            return ( Arr.Length == j ) ? Arr[ 0 ] : Arr[ j ];
        }

        public static T Previous<T>( this T src ) where T : struct
        {
            if ( !typeof( T ).IsEnum ) throw new ArgumentException( String.Format( "Argument {0} is not an Enum", typeof( T ).FullName ) );

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf( Arr, src) - 1;
            return ( j == 0 ) ? Arr[ 0 ] : Arr[ j ];
        }
    }
    public enum CandleSettingEnum : byte
    {
        BodyLong          = 10,
        BodyVeryLong      = 20,
        BodyShort         = 30,
        BodyDoji          = 40,
        ShadowLong        = 50,
        ShadowVeryLong    = 60,
        ShadowShort       = 70,
        ShadowVeryShort   = 80,
        Near              = 90,
        Far               = 100,
        Equal             = 110,
        AllCandleSettings = 120
    }

    public enum RetCode : byte
    {
        Success,
        LibNotInitialize,
        BadParam,
        AllocErr,
        GroupNotFound,
        FuncNotFound,
        InvalidHandle,
        InvalidParamHolder,
        InvalidParamHolderType,
        InvalidParamFunction,
        InputNotAllInitialize,
        OutputNotAllInitialize,
        OutOfRangeStartIndex,
        OutOfRangeEndIndex,
        InvalidListType,
        BadObject,
        NotSupported,
        InternalError,
        UnknownErr
    }

    public enum RangeEnum : byte
    {
        NoAvailable       = 0,
        RealBodyLength    = 1,
        HighLow           = 2,
        Shadows           = 3
    }

    public struct CandleSetting
    {
        private RangeEnum _rangeType;

        /// <summary>
        /// Actual common identification structure.
        /// </summary>
        public RangeEnum RangeType
        {
            get { return _rangeType; }
        }

        private int _avgPeriod;

        public int AvgPeriod
        {
            get { return _avgPeriod; }
        }

        private double _factor;

        public double Factor
        {
            get { return _factor; }
        }


        public CandleSetting( RangeEnum rangeType, int avgPeriod, double factor )
        {
            _rangeType = rangeType;
            _avgPeriod = avgPeriod;
            _factor = factor;
        }
    };
}
