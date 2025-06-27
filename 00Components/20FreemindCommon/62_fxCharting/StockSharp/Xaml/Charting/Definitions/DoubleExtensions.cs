using fx.Charting.ATony;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
//using TracerAttributes;

namespace fx.Charting.Definitions
{
    //[NoTrace]
    internal static class DoubleExtensions
    {
        internal static bool IsRealNumber( this double number )
        {
            if ( !double.IsNaN( number ) && !double.IsInfinity( number ) )
            {
                double num = double.MaxValue;
                if ( !num.Equals( number ) )
                {
                    num = double.MinValue;
                    return !num.Equals( number );
                }
            }
            return false;
        }

        //internal static bool IsNaN( this double value )
        //{
        //    return Double.IsNaN( value );
        //}

        internal static double RoundOff( this double d )
        {
            return Math.Round( d );
        }

        internal static double Ceiling( this double d )
        {
            return Math.Ceiling( d );
        }

        internal static double Floor( this double d )
        {
            return Math.Floor( d );
        }

        internal static double RoundOff( this double d, MidpointRounding mode )
        {
            return d.RoundOff( 0, mode );
        }

        internal static double RoundOff( this double d, int decimals, MidpointRounding mode )
        {
            if ( mode == MidpointRounding.ToEven )
            {
                return Math.Round( d, decimals );
            }

            Decimal num1 = Convert.ToDecimal( Math.Pow( 10.0,   decimals ) );
            int num2 = Math.Sign( d );
            return ( double )( Decimal.Truncate( ( Decimal )d * num1 + new Decimal( 5, 0, 0, false, 1 ) * num2 ) / num1 );
        }

        internal static DateTime ToDateTime( this double ticks )
        {
            long num = ( long )ticks;
            DateTime dateTime = DateTime.MinValue;
            long ticks1 = dateTime.Ticks;
            dateTime = DateTime.MaxValue;
            long ticks2 = dateTime.Ticks;
            return new DateTime( NumberUtil.Constrain( num, ticks1, ticks2 ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        internal static double ClipToInt( this double d )
        {
            if ( d > int.MaxValue )
            {
                return int.MaxValue;
            }

            if ( d < int.MinValue )
            {
                return int.MinValue;
            }

            return d;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        internal static int ClipToIntValue( this double d )
        {
            if ( d > int.MaxValue )
            {
                return int.MaxValue;
            }

            if ( d < int.MinValue )
            {
                return int.MinValue;
            }

            return ( int )d;
        }
    }
}
