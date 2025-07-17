// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Utility.Guard
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Utility
{
    internal static class Guard
    {
        internal static void IsTrue( bool value, string message )
        {
            if ( !value )
                throw new ArgumentException( message );
        }

        internal static void NotNull( object arg, string name )
        {
            if ( arg == null )
                throw new ArgumentNullException( name, string.Format( "The Argument {0} cannot be null", ( object ) name ) );
        }

        internal static void ArrayLengthsSame( int count1, string array1Name, int count2, string array2Name )
        {
            if ( count1 != count2 )
                throw new InvalidOperationException( string.Format( "Arrays {0} and {1} must have the same length", ( object ) array1Name, ( object ) array2Name ) );
        }

        public static void ArgumentIsRealNumber( double doubleValue )
        {
            if ( !doubleValue.IsRealNumber() )
                throw new InvalidOperationException( string.Format( "Value {0} is not a real number", ( object ) doubleValue ) );
        }

        public static void DateTimeArgumentIsDefined( DateTime value, string argName )
        {
            if ( DateTime.MinValue == value || DateTime.MaxValue == value )
                throw new InvalidOperationException( string.Format( "DateTime Argument {0} is not defined", ( object ) argName ) );
        }

        public static GuardConstraint Assert( IComparable value, string argName )
        {
            return new GuardConstraint( value, argName );
        }
    }
}
