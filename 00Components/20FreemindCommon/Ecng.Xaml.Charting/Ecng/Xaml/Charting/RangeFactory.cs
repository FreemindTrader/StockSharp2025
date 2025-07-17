// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.RangeFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public static class RangeFactory
    {
        public static IRange NewWithMinMax( IRange originalRange, IComparable min, IComparable max )
        {
            Guard.Assert( min, nameof( min ) ).IsLessThanOrEqualTo( max, nameof( max ) );
            IRange range = (IRange) originalRange.Clone();
            range.SetMinMax( min.ToDouble(), max.ToDouble() );
            return range;
        }

        public static IRange NewWithMinMax( IRange originalRange, double min, double max, IRange rangeLimit )
        {
            IRange range = (IRange) originalRange.Clone();
            range.SetMinMaxWithLimit( min, max, rangeLimit );
            return range;
        }

        public static IRange NewRange( IComparable min, IComparable max )
        {
            Type type = min.GetType();
            return !( type == typeof( float ) ) ? ( !( type == typeof( int ) ) ? ( !( type == typeof( long ) ) ? ( !( type == typeof( DateTime ) ) ? ( !( type == typeof( TimeSpan ) ) ? ( IRange ) new DoubleRange( min.ToDouble(), max.ToDouble() ) : ( IRange ) new TimeSpanRange( ( TimeSpan ) min, ( TimeSpan ) max ) ) : ( IRange ) new DateRange( ( DateTime ) min, ( DateTime ) max ) ) : ( IRange ) new Int64Range( ( long ) min, ( long ) max ) ) : ( IRange ) new IntegerRange( ( int ) min, ( int ) max ) ) : ( IRange ) new FloatRange( ( float ) min, ( float ) max );
        }

        public static IRange NewRange( Type rangeType, IComparable min, IComparable max )
        {
            IRange instance = Activator.CreateInstance(rangeType) as IRange;
            instance.SetMinMax( min.ToDouble(), max.ToDouble() );
            return instance;
        }
    }
}
