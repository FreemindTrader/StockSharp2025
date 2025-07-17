// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.DoubleExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
namespace fx.Xaml.Charting
{
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

        internal static bool IsNaN( this double value )
        {
            return Double.IsNaN( value );
        }

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
                return Math.Round( d, decimals );
            Decimal num1 = Convert.ToDecimal(Math.Pow(10.0, (double) decimals));
            int num2 = Math.Sign(d);
            return ( double ) ( Decimal.Truncate( ( Decimal ) d * num1 + new Decimal( 5, 0, 0, false, ( byte ) 1 ) * ( Decimal ) num2 ) / num1 );
        }

        internal static DateTime ToDateTime( this double ticks )
        {
            long num = (long) ticks;
            DateTime dateTime = DateTime.MinValue;
            long ticks1 = dateTime.Ticks;
            dateTime = DateTime.MaxValue;
            long ticks2 = dateTime.Ticks;
            return new DateTime( NumberUtil.Constrain( num, ticks1, ticks2 ) );
        }

        internal static double ClipToInt( this double d )
        {
            if ( d > ( double ) int.MaxValue )
                return ( double ) int.MaxValue;
            if ( d < ( double ) int.MinValue )
                return ( double ) int.MinValue;
            return d;
        }

        internal static int ClipToIntValue( this double d )
        {
            if ( d > ( double ) int.MaxValue )
                return int.MaxValue;
            if ( d < ( double ) int.MinValue )
                return int.MinValue;
            return ( int ) d;
        }
    }
}
