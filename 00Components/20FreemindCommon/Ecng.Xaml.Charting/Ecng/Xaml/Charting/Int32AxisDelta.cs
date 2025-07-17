// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Int32AxisDelta
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    public class Int32AxisDelta : IAxisDelta<int>, IAxisDelta, ICloneable, IEquatable<Int32AxisDelta>
    {
        public Int32AxisDelta()
        {
        }

        public Int32AxisDelta( int minorDelta, int majorDelta )
        {
            this.MinorDelta = minorDelta;
            this.MajorDelta = majorDelta;
        }

        public int MajorDelta
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

        public int MinorDelta
        {
            get; set;
        }

        public object Clone()
        {
            return ( object ) new Int32AxisDelta( this.MinorDelta, this.MajorDelta );
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as Int32AxisDelta );
        }

        public bool Equals( Int32AxisDelta other )
        {
            if ( ( object ) other == null )
                return false;
            if ( ( object ) this == ( object ) other )
                return true;
            int num = other.MajorDelta;
            if ( !num.Equals( this.MajorDelta ) )
                return false;
            num = other.MinorDelta;
            return num.Equals( this.MinorDelta );
        }

        public override int GetHashCode()
        {
            int num1 = this.MajorDelta;
            int num2 = num1.GetHashCode() * 397;
            num1 = this.MinorDelta;
            int hashCode = num1.GetHashCode();
            return num2 ^ hashCode;
        }

        public static bool operator ==( Int32AxisDelta left, Int32AxisDelta right )
        {
            return object.Equals( ( object ) left, ( object ) right );
        }

        public static bool operator !=( Int32AxisDelta left, Int32AxisDelta right )
        {
            return !object.Equals( ( object ) left, ( object ) right );
        }

        public override string ToString()
        {
            return string.Format( "MinorDelta: {0}, MajorDelta: {1}", ( object ) this.MinorDelta, ( object ) this.MajorDelta );
        }
    }
}
