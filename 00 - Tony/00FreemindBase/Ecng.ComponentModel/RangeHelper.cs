// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.RangeHelper
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecng.ComponentModel
{
    public static class RangeHelper
    {
        public static bool IsEmpty<T>( this Range<T> range ) where T : IComparable<T>
        {
            if ( range == null )
                throw new ArgumentNullException( nameof( range ) );
            if ( !range.HasMaxValue )
                return !range.HasMinValue;
            return false;
        }

        public static IEnumerable<Range<T>> JoinRanges<T>(
          this IEnumerable<Range<T>> ranges )
          where T : IComparable<T>
        {
            List<Range<T>> list = ranges.OrderBy<Range<T>, T>( ( Func<Range<T>, T> )( r => r.Min ) ).ToList<Range<T>>();
            int index = 0;
            while ( index < list.Count - 1 )
            {
                if ( list[index].Contains( list[index + 1] ) )
                    list.RemoveAt( index + 1 );
                else if ( list[index + 1].Contains( list[index] ) )
                    list.RemoveAt( index );
                else if ( ( Equatable<Range<T>> )list[index].Intersect( list[index + 1] ) != ( Range<T> )null )
                {
                    list[index] = new Range<T>( list[index].Min, list[index + 1].Max );
                    list.RemoveAt( index + 1 );
                }
                else
                    ++index;
            }
            return ( IEnumerable<Range<T>> )list;
        }

        public static IEnumerable<Range<DateTimeOffset>> Exclude(
          this Range<DateTimeOffset> from,
          Range<DateTimeOffset> excludingRange )
        {
            DateTimeOffset dateTimeOffset = from.Min;
            long utcTicks1 = dateTimeOffset.UtcTicks;
            dateTimeOffset = from.Max;
            long utcTicks2 = dateTimeOffset.UtcTicks;
            Range<long> from1 = new Range<long>( utcTicks1, utcTicks2 );
            dateTimeOffset = excludingRange.Min;
            long utcTicks3 = dateTimeOffset.UtcTicks;
            dateTimeOffset = excludingRange.Max;
            long utcTicks4 = dateTimeOffset.UtcTicks;
            Range<long> excludingRange1 = new Range<long>( utcTicks3, utcTicks4 );
            return from1.Exclude( excludingRange1 ).Select<Range<long>, Range<DateTimeOffset>>( ( Func<Range<long>, Range<DateTimeOffset>> )( r => new Range<DateTimeOffset>( r.Min.To<DateTimeOffset>(), r.Max.To<DateTimeOffset>() ) ) );
        }

        public static IEnumerable<Range<DateTime>> Exclude(
          this Range<DateTime> from,
          Range<DateTime> excludingRange )
        {
            DateTime dateTime = from.Min;
            long ticks1 = dateTime.Ticks;
            dateTime = from.Max;
            long ticks2 = dateTime.Ticks;
            Range<long> from1 = new Range<long>( ticks1, ticks2 );
            dateTime = excludingRange.Min;
            long ticks3 = dateTime.Ticks;
            dateTime = excludingRange.Max;
            long ticks4 = dateTime.Ticks;
            Range<long> excludingRange1 = new Range<long>( ticks3, ticks4 );
            return from1.Exclude( excludingRange1 ).Select<Range<long>, Range<DateTime>>( ( Func<Range<long>, Range<DateTime>> )( r => new Range<DateTime>( r.Min.To<DateTime>(), r.Max.To<DateTime>() ) ) );
        }

        public static IEnumerable<Range<long>> Exclude(
          this Range<long> from,
          Range<long> excludingRange )
        {
            Range<long> intersectedRange = from.Intersect( excludingRange );
            if ( intersectedRange == null )
                yield return from;
            else if ( !( ( Equatable<Range<long>> )from == intersectedRange ) )
            {
                if ( from.Contains( intersectedRange ) )
                {
                    if ( from.Min != intersectedRange.Min )
                        yield return new Range<long>( from.Min, intersectedRange.Min - 1L );
                    if ( from.Max != intersectedRange.Max )
                        yield return new Range<long>( intersectedRange.Max + 1L, from.Max );
                }
                else if ( from.Min < intersectedRange.Min )
                    yield return new Range<long>( from.Min, intersectedRange.Min );
                else
                    yield return new Range<long>( intersectedRange.Max, from.Max );
            }
        }

        public static IEnumerable<Range<DateTimeOffset>> GetRanges(
          this IEnumerable<DateTimeOffset> dates,
          DateTimeOffset from,
          DateTimeOffset to )
        {
            return dates.Select<DateTimeOffset, long>( ( Func<DateTimeOffset, long> )( d => d.To<long>() ) ).GetRanges( from.UtcTicks, to.UtcTicks ).Select<Range<long>, Range<DateTimeOffset>>( ( Func<Range<long>, Range<DateTimeOffset>> )( r => new Range<DateTimeOffset>( r.Min.To<DateTimeOffset>(), r.Max.To<DateTimeOffset>() ) ) );
        }

        public static IEnumerable<Range<DateTime>> GetRanges(
          this IEnumerable<DateTime> dates,
          DateTime from,
          DateTime to )
        {
            return dates.Select<DateTime, long>( ( Func<DateTime, long> )( d => d.To<long>() ) ).GetRanges( from.Ticks, to.Ticks ).Select<Range<long>, Range<DateTime>>( ( Func<Range<long>, Range<DateTime>> )( r => new Range<DateTime>( r.Min.To<DateTime>(), r.Max.To<DateTime>() ) ) );
        }

        public static IEnumerable<Range<long>> GetRanges(
          this IEnumerable<long> dates,
          long from,
          long to )
        {
            if ( !dates.IsEmpty<long>() )
            {
                long min = from;
                long num1 = min + 864000000000L;
                foreach ( long num2 in dates.Skip<long>( 1 ) )
                {
                    if ( num2 != num1 )
                    {
                        yield return new Range<long>( min, num1 - 1L );
                        min = num2;
                    }
                    num1 = num2 + 864000000000L;
                }
                yield return new Range<long>( min, num1 - 1L );
            }
        }

        public static SettingsStorage ToStorage<T>( this Range<T> range ) where T : IComparable<T>
        {
            if ( range == null )
                throw new ArgumentNullException( nameof( range ) );
            return new SettingsStorage().Set<object>( "Min", range.HasMinValue ? ( object )range.Min : ( object )( IComparable<T> )null ).Set<object>( "Max", range.HasMaxValue ? ( object )range.Max : ( object )( IComparable<T> )null );
        }

        public static Range<T> ToRange<T>( this SettingsStorage storage ) where T : IComparable<T>
        {
            if ( storage == null )
                throw new ArgumentNullException( nameof( storage ) );
            Range<T> range = new Range<T>();
            object obj1 = storage.GetValue<object>( "Min", ( object )null );
            object obj2 = storage.GetValue<object>( "Max", ( object )null );
            if ( obj1 != null )
                range.Min = obj1.To<T>();
            if ( obj2 != null )
                range.Max = obj2.To<T>();
            return range;
        }
    }
}
