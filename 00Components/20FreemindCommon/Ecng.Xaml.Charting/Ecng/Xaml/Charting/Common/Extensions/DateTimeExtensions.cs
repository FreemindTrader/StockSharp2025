// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.DateTimeExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    internal static class DateTimeExtensions
    {
        internal static bool IsDefined( this DateTime current )
        {
            if ( current != DateTime.MaxValue )
                return current != DateTime.MinValue;
            return false;
        }

        internal static bool IsAdditionValid( this DateTime current, TimeSpan delta )
        {
            bool flag = false;
            if ( ( double ) current.Year + delta.TotalDays / 365.0 < ( double ) DateTime.MaxValue.Year )
                flag = true;
            return flag;
        }

        internal static DateTime AddDelta( this DateTime current, TimeSpan delta )
        {
            if ( delta.IsDivisibleBy( TimeSpanExtensions.FromYears( 1 ) ) )
                return current.AddYears( ( int ) ( delta.Ticks / TimeSpanExtensions.FromYears( 1 ).Ticks ) );
            if ( delta.IsDivisibleBy( TimeSpanExtensions.FromMonths( 1 ) ) )
                return current.AddMonths( ( int ) ( delta.Ticks / TimeSpanExtensions.FromMonths( 1 ).Ticks ) );
            return current.Add( delta );
        }

        internal static DateTime AddMonths( this DateTime current, int months )
        {
            int num1 = 0;
            int num2 = months;
            while ( num2 > 12 )
            {
                ++num1;
                num2 -= 12;
            }
            return new DateTime( current.Year + num1, current.Month + num2, current.Day, current.Hour, current.Minute, current.Second, current.Millisecond );
        }

        internal static DateTime AddYears( this DateTime current, int years )
        {
            return new DateTime( current.Year + years, current.Month, current.Day, current.Hour, current.Minute, current.Second, current.Millisecond );
        }
    }
}
