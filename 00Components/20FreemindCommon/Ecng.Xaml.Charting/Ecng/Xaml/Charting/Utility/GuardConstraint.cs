// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Utility.GuardConstraint
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public class GuardConstraint
    {
        private readonly IComparable _value;
        private readonly string _argName;

        public GuardConstraint( IComparable value, string argName )
        {
            this._value = value;
            this._argName = argName;
        }

        public void IsLessThan( IComparable other, string otherArgName )
        {
            if ( this._value.CompareTo( ( object ) other ) >= 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must be less than argument \"{2}\", value={3}", ( object ) this._argName, ( object ) this._value, ( object ) otherArgName, ( object ) other ) );
        }

        public void IsNotEqualTo( IComparable other, string otherArgName )
        {
            if ( this._value.CompareTo( ( object ) other ) == 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must not be equal to argument \"{2}\", value={3}", ( object ) this._argName, ( object ) this._value, ( object ) otherArgName, ( object ) other ) );
        }

        public void IsEqualTo( IComparable other, string otherArgName )
        {
            if ( this._value.CompareTo( ( object ) other ) != 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must be equal to argument \"{2}\", value={3}", ( object ) this._argName, ( object ) this._value, ( object ) otherArgName, ( object ) other ) );
        }

        public void IsNotEqualTo( IComparable other )
        {
            if ( this._value.CompareTo( ( object ) other ) == 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must not be equal to {2}", ( object ) this._argName, ( object ) this._value, ( object ) other ) );
        }

        public void IsLessThanOrEqualTo( IComparable other, string otherArgName )
        {
            if ( this._value.CompareTo( ( object ) other ) > 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must be less than or equal to argument \"{2}\", value={3}", ( object ) this._argName, ( object ) this._value, ( object ) otherArgName, ( object ) other ) );
        }

        public void IsGreaterThanOrEqualTo( IComparable other )
        {
            if ( this._value.CompareTo( ( object ) other ) < 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must be greater than or equal to {2}", ( object ) this._argName, ( object ) this._value, ( object ) other ) );
        }

        public void IsGreaterThan( IComparable other )
        {
            if ( this._value.CompareTo( ( object ) other ) <= 0 )
                throw new InvalidOperationException( string.Format( "The argument \"{0}\", value={1}, must be greater than {2}", ( object ) this._argName, ( object ) this._value, ( object ) other ) );
        }
    }
}
