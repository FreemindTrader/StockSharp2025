// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.TimeSpanDelta
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public class TimeSpanDelta : IAxisDelta<TimeSpan>, IAxisDelta, ICloneable, IEquatable<TimeSpanDelta>
    {
        public TimeSpanDelta()
        {
        }

        public TimeSpanDelta( TimeSpan minorDelta, TimeSpan majorDelta )
        {
            this.MinorDelta = minorDelta;
            this.MajorDelta = majorDelta;
        }

        IComparable IAxisDelta.MajorDelta
        {
            get
            {
                return ( IComparable ) this.MajorDelta;
            }
        }

        IComparable IAxisDelta.MinorDelta
        {
            get
            {
                return ( IComparable ) this.MinorDelta;
            }
        }

        public TimeSpan MajorDelta
        {
            get; set;
        }

        public TimeSpan MinorDelta
        {
            get; set;
        }

        public object Clone()
        {
            return ( object ) new TimeSpanDelta( this.MinorDelta, this.MajorDelta );
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as TimeSpanDelta );
        }

        public bool Equals( TimeSpanDelta other )
        {
            if ( ( object ) other == null )
                return false;
            if ( ( object ) this == ( object ) other )
                return true;
            TimeSpan timeSpan = other.MajorDelta;
            if ( !timeSpan.Equals( this.MajorDelta ) )
                return false;
            timeSpan = other.MinorDelta;
            return timeSpan.Equals( this.MinorDelta );
        }

        public override int GetHashCode()
        {
            TimeSpan timeSpan = this.MajorDelta;
            int num = timeSpan.GetHashCode() * 397;
            timeSpan = this.MinorDelta;
            int hashCode = timeSpan.GetHashCode();
            return num ^ hashCode;
        }

        public static bool operator ==( TimeSpanDelta left, TimeSpanDelta right )
        {
            return object.Equals( ( object ) left, ( object ) right );
        }

        public static bool operator !=( TimeSpanDelta left, TimeSpanDelta right )
        {
            return !object.Equals( ( object ) left, ( object ) right );
        }

        public override string ToString()
        {
            return string.Format( "MinorDelta: {0}, MajorDelta, {1}", ( object ) this.MinorDelta, ( object ) this.MajorDelta );
        }
    }
}
