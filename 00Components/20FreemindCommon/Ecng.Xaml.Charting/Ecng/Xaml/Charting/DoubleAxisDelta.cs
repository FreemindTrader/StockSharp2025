// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.DoubleAxisDelta
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public class DoubleAxisDelta : IAxisDelta<double>, IAxisDelta, ICloneable, IEquatable<DoubleAxisDelta>
    {
        public DoubleAxisDelta()
        {
        }

        public DoubleAxisDelta( double minorDelta, double majorDelta )
        {
            this.MinorDelta = minorDelta;
            this.MajorDelta = majorDelta;
        }

        public double MajorDelta
        {
            get; set;
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

        public double MinorDelta
        {
            get; set;
        }

        public object Clone()
        {
            return ( object ) new DoubleAxisDelta( this.MinorDelta, this.MajorDelta );
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as DoubleAxisDelta );
        }

        public bool Equals( DoubleAxisDelta other )
        {
            if ( ( object ) other == null )
                return false;
            if ( ( object ) this == ( object ) other )
                return true;
            double num = other.MajorDelta;
            if ( !num.Equals( this.MajorDelta ) )
                return false;
            num = other.MinorDelta;
            return num.Equals( this.MinorDelta );
        }

        public override int GetHashCode()
        {
            double num1 = this.MajorDelta;
            int num2 = num1.GetHashCode() * 397;
            num1 = this.MinorDelta;
            int hashCode = num1.GetHashCode();
            return num2 ^ hashCode;
        }

        public static bool operator ==( DoubleAxisDelta left, DoubleAxisDelta right )
        {
            return object.Equals( ( object ) left, ( object ) right );
        }

        public static bool operator !=( DoubleAxisDelta left, DoubleAxisDelta right )
        {
            return !object.Equals( ( object ) left, ( object ) right );
        }

        public override string ToString()
        {
            return string.Format( "MinorDelta: {0}, MajorDelta: {1}", ( object ) this.MinorDelta, ( object ) this.MajorDelta );
        }
    }
}
