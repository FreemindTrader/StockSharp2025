// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.DataDistributionCalculatorFactory
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    internal static class DataDistributionCalculatorFactory
    {
        internal static IDataDistributionCalculator<TX> Create<TX>( bool isFifo ) where TX : IComparable
        {
            if ( typeof( TX ) == typeof( float ) )
            {
                if ( !isFifo )
                    return new ListSingleDataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new SingleDataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( double ) )
            {
                if ( !isFifo )
                    return new ListDoubleDataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new DoubleDataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( Decimal ) )
            {
                if ( !isFifo )
                    return new ListDecimalDataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new DecimalDataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( short ) )
            {
                if ( !isFifo )
                    return new ListInt16DataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new Int16DataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( int ) )
            {
                if ( !isFifo )
                    return new ListInt32DataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new Int32DataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( long ) )
            {
                if ( !isFifo )
                    return new ListInt64DataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new Int64DataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( ushort ) )
            {
                if ( !isFifo )
                    return new ListUInt16DataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new UInt16DataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( uint ) )
            {
                if ( !isFifo )
                    return new ListUInt32DataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new UInt32DataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( ulong ) )
            {
                if ( !isFifo )
                    return new ListUInt64DataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new UInt64DataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( byte ) )
            {
                if ( !isFifo )
                    return new ListByteDataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new ByteDataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( sbyte ) )
            {
                if ( !isFifo )
                    return new ListSByteDataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new SByteDataDistributionCalculator();
            }
            if ( typeof( TX ) == typeof( DateTime ) )
            {
                if ( !isFifo )
                    return new ListDateTimeDataDistributionCalculator() as IDataDistributionCalculator<TX>;
                return ( IDataDistributionCalculator<TX> ) new DateTimeDataDistributionCalculator();
            }
            if ( !( typeof( TX ) == typeof( TimeSpan ) ) )
                throw new NotImplementedException( string.Format( "Cannot create a DataDistributionCalculator for the type TX={0}", ( object ) typeof( TX ) ) );
            if ( !isFifo )
                return new ListTimeSpanDataDistributionCalculator() as IDataDistributionCalculator<TX>;
            return ( IDataDistributionCalculator<TX> ) new TimeSpanDataDistributionCalculator();
        }
    }
}
