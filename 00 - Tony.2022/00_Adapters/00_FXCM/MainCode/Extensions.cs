#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.BitStamp.BitStamp
File: Extensions.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.FxConnectFXCM
{
    using System;
    using System.Reflection;
    using System.Threading;
    using Ecng.Common;
    using fxcore2;
    using StockSharp.Localization;
    using StockSharp.Messages;

    public static class ThreadHelper
    {
        /// <summary>
        /// Updates the name of the thread.
        /// </summary>
        /// <param name="strName" type="System.String">Name of the string.</param>
        /// <param name="paramObjects" type="System.Object[]">The parameter objects.</param>
        ///<remarks>if strName is null, just reset the name do not assign a new one</remarks>
        static public void UpdateThreadName( string strName )
        {
            //
            // if we already have a name reset it
            //
            if ( null != Thread.CurrentThread.Name )
            {
                ResetThreadName( Thread.CurrentThread );
            }

            if ( null != strName )
            {
                Thread.CurrentThread.Name = strName;
            }
        }

        /// <summary>
        /// Reset the name of the set thread.
        /// </summary>
        /// <param name="thread" type="Thread">The thread.</param>
        /// <exception cref="NullReferenceException">Thread cannot be null</exception>
        static private void ResetThreadName( Thread thread )
        {
            if ( null == thread ) throw new NullReferenceException( "Thread cannot be null" );
            lock ( thread )
            {
                //
                // This is a private member of Thread, if they ever change the name this will not work
                //
                var field = thread.GetType( ).GetField( "m_Name", BindingFlags.Instance | BindingFlags.NonPublic );
                if ( null != field )
                {
                    //
                    // Change the Name to null (nothing)
                    //
                    field.SetValue( thread, null );

                    //
                    // This 'extra' null set notifies Visual Studio about the change
                    //
                    thread.Name = null;
                }
            }
        }
    }

    internal static class FxcmFxconnectHelper
    {
        public static string ToReadable( this TimeSpan input )
        {
            if ( input == TimeSpan.FromDays( 30 ) )
            {
                return "Monthly";
            }
            else if ( input == TimeSpan.FromDays( 7 ) )
            {
                return "Weekly";
            }
            else if ( input == TimeSpan.FromDays( 1 ) )
            {
                return "Daily";
            }
            else if ( input == TimeSpan.FromHours( 8 ) )
            {
                return "8 Hr";
            }
            else if ( input == TimeSpan.FromHours( 6 ) )
            {
                return "6 Hr";
            }
            else if ( input == TimeSpan.FromHours( 4 ) )
            {
                return "4 Hr";
            }
            else if ( input == TimeSpan.FromHours( 3 ) )
            {
                return "3 Hr";
            }
            else if ( input == TimeSpan.FromHours( 2 ) )
            {
                return "2 Hr";
            }
            else if ( input == TimeSpan.FromHours( 1 ) )
            {
                return "1 Hr";
            }
            else if ( input == TimeSpan.FromMinutes( 60 ) )
            {
                return "60 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 30 ) )
            {
                return "30 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 15 ) )
            {
                return "15 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 5 ) )
            {
                return "5 Min";
            }
            else if ( input == TimeSpan.FromMinutes( 1 ) )
            {
                return "1 Min";
            }
            else if ( input == TimeSpan.FromTicks( 1 ) )
            {
                return "Tick";
            }

            return ( "No supported" );
        }

        public static string ToSidesString( this Sides sides_0 )
        {
            if ( sides_0 == Sides.Buy )
            {
                return "B";
            }

            if ( sides_0 != Sides.Sell )
            {
                throw new ArgumentOutOfRangeException( "side", sides_0, LocalizedStrings.Str1219 );
            }

            return "S";
        }

        public static Sides ToSides( this string buySell )
        {
            if ( buySell == "B" )
            {
                return Sides.Buy;
            }

            if ( buySell == "S" )
            {
                return Sides.Sell;
            }

            throw new ArgumentOutOfRangeException( "side", buySell, LocalizedStrings.Str1219 );
        }

        public static Sides ToOppSides( this string buySell )
        {
            if ( buySell == "B" )
            {
                return Sides.Sell;
            }

            if ( buySell == "S" )
            {
                return Sides.Buy;
            }

            throw new ArgumentOutOfRangeException( "side", buySell, LocalizedStrings.Str1219 );
        }

        public static string ToFxcmTIF( this TimeInForce? tif, DateTimeOffset? offset )
        {
            if ( tif.HasValue )
            {
                switch ( tif.GetValueOrDefault( ) )
                {
                    case TimeInForce.PutInQueue:
                        break;

                    case TimeInForce.MatchOrCancel:
                        return "FOK";

                    case TimeInForce.CancelBalance:
                        return "IOC";

                    default:
                        throw new ArgumentOutOfRangeException( "tif", tif, LocalizedStrings.Str1219 );
                }
            }
            if ( !offset.HasValue )
            {
                return "GTC";
            }

            return offset.Value.Date == DateTime.Today ? "DAY" : "GTD";
        }

        public static TimeInForce ToSxTIF( this string string_0 )
        {
            if ( string_0 == "GTC" || string_0 == "GTD" || string_0 == "DAY" )
            {
                return 0;
            }

            if ( string_0 == "FOK" )
            {
                return TimeInForce.MatchOrCancel;
            }

            if ( !( string_0 == "IOC" ) )
            {
                throw new ArgumentOutOfRangeException( "value", string_0, LocalizedStrings.Str1219 );
            }

            return ( TimeInForce ) 2;
        }

        public static string ToNativeString( this SecurityId securityId_0 )
        {
            object native = securityId_0.Native;
            if ( native == null )
            {
                throw new ArgumentNullException( "securityId" );
            }

            return ( string ) native;
        }

        public static SecurityId ToSecurityId( this string string_0 )
        {
            SecurityId securityId = new SecurityId( );
            securityId.Native = string_0;

            return securityId;
        }

        public static SecurityTypes? ToSecurityType( this O2GInstrumentType secType )
        {
            switch ( secType )
            {
                case O2GInstrumentType.InstrumentForex:
                    return new SecurityTypes?( SecurityTypes.Currency );

                case O2GInstrumentType.InstrumentIndices:
                    return new SecurityTypes?( SecurityTypes.Index );

                case O2GInstrumentType.InstrumentCommodity:
                    return new SecurityTypes?( SecurityTypes.Commodity );

                case O2GInstrumentType.InstrumentTreasury:
                    return new SecurityTypes?( SecurityTypes.Adr );

                case O2GInstrumentType.InstrumentBullion:
                    return new SecurityTypes?( SecurityTypes.Commodity );

                case O2GInstrumentType.InstrumentShares:
                    return new SecurityTypes?( SecurityTypes.Stock );

                case O2GInstrumentType.InstrumentFXIndex:
                    return new SecurityTypes?( SecurityTypes.Index );

                case O2GInstrumentType.InstrumentCFDShares:
                    return new SecurityTypes?( SecurityTypes.Cfd );

                case O2GInstrumentType.InstrumentCrypto:
                    return new SecurityTypes?( SecurityTypes.CryptoCurrency );

                default:
                    return new SecurityTypes?( );
            }
        }

        public static SecurityStates? ToSecurityStates( this string stateString )
        {
            if ( stateString == "O" )
            {
                return new SecurityStates?( SecurityStates.Trading );
            }

            if ( !( stateString == "C" ) )
            {
                return new SecurityStates?( );
            }

            return new SecurityStates?( SecurityStates.Stoped );
        }

        public static O2GTimeframe ToFxcmTimeFrame( this TimeSpan ts, O2GTimeframeCollection tc )
        {
            switch ( ts.Ticks )
            {
                case 1:
                    return tc[ "t1" ];
                case 600000000:
                    return tc[ "m1" ];
                case 3000000000:
                    return tc[ "m5" ];
                case 9000000000:
                    return tc[ "m15" ];
                case 18000000000:
                    return tc[ "m30" ];
                case 36000000000:
                    return tc[ "H1" ];
                case 72000000000:
                    return tc[ "H2" ];
                case 108000000000:
                    return tc[ "H3" ];
                case 144000000000:
                    return tc[ "H4" ];
                case 216000000000:
                    return tc[ "H6" ];
                case 288000000000:
                    return tc[ "H8" ];
                case 864000000000:
                    return tc[ "D1" ];
                case 6048000000000:
                    return tc[ "W1" ];
                case 25920000000000:
                    return tc[ "M1" ];
                default:
                    throw new ArgumentOutOfRangeException( "value", ts, LocalizedStrings.Str1219 );
            }
        }


        public static string ToShortName(
          this FxcmExtendedOrderTypes orderType )
        {
            switch ( orderType )
            {
                case FxcmExtendedOrderTypes.MarketOpen:
                    return "O";
                case FxcmExtendedOrderTypes.MarketOpenRange:
                    return "OR";
                case FxcmExtendedOrderTypes.TrueMarketClose:
                    return "CM";
                case FxcmExtendedOrderTypes.MarketClose:
                    return "C";
                case FxcmExtendedOrderTypes.MarketCloseRange:
                    return "CR";
                case FxcmExtendedOrderTypes.StopEntry:
                    return "SE";
                case FxcmExtendedOrderTypes.LimitEntry:
                    return "LE";
                case FxcmExtendedOrderTypes.RangeEntry:
                    return "RE";
                case FxcmExtendedOrderTypes.RangeTrailingEntry:
                    return "RTE";
                case FxcmExtendedOrderTypes.Limit:
                    return "L";
                case FxcmExtendedOrderTypes.Stop:
                    return "S";
                case FxcmExtendedOrderTypes.OpenLimit:
                    return "OL";
                case FxcmExtendedOrderTypes.CloseLimit:
                    return "CL";
                default:
                    throw new ArgumentOutOfRangeException( "type", orderType, LocalizedStrings.Str1219 );
            }
        }

        public static OrderStates ToSxOrderStates( this string fxcmStatus )
        {
            if ( fxcmStatus == "E" || fxcmStatus == "I" || fxcmStatus == "U" || fxcmStatus == "Q" || fxcmStatus == "S" || fxcmStatus == "W" )
            {
                return OrderStates.Active;
            }
            else if ( fxcmStatus == "F" || fxcmStatus == "C" || fxcmStatus == "T" )
            {
                return OrderStates.Done;
            }
            else if ( fxcmStatus == "P" )
            {
                return OrderStates.Pending;
            }
            else if ( fxcmStatus == "R" )
            {
                return OrderStates.Failed;
            }

            throw new ArgumentOutOfRangeException( "value", fxcmStatus, LocalizedStrings.Str1219 );

        }

        public static OrderTypes ToSxOrderType( this string fxcmOrder, out FxcmExtendedOrderTypes? extOrderType )
        {
            extOrderType = new FxcmExtendedOrderTypes?( );

            if ( fxcmOrder == "SE" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.StopEntry );
                return OrderTypes.Conditional;
            }

            if ( fxcmOrder == "CR" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.MarketCloseRange );
                return OrderTypes.Conditional;
            }

            //if ( fxcmOrder == "LE" )
            //{
            //    extendedOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.LimitEntry );
            //    return OrderTypes.Conditional;
            //}

            if ( fxcmOrder == "RE" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.RangeEntry );
                return OrderTypes.Conditional;
            }

            if ( fxcmOrder == "OR" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.MarketOpenRange );
                return OrderTypes.Conditional;
            }


            if ( fxcmOrder == "CL" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.CloseLimit );
                return OrderTypes.Conditional;
            }

            // An open order opens a position at the specified market rate or at a more favorable rate in case such rate is available on the market.
            // I guess this is the market order, but we don't want it to deviate too far from what we saw on Screen
            if ( fxcmOrder == "OL" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.OpenLimit );
                return OrderTypes.Conditional;
            }

            if ( fxcmOrder == "CM" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.TrueMarketClose );
                return OrderTypes.Market;
            }

            // This is the true Market price. An open market order opens a position at any currently available market rate.
            if ( fxcmOrder == "OM" )
            {
                return OrderTypes.Market;
            }

            /*
             * There are two types of Entry orders : Limit Entry and Stop Entry. This command allows you to create an entry order without specifying order type. 
             * The system will determine order type automatically, based on three parameters:
             * 
             * Order direction (Buy or Sell).
             * Desired order rate.
             * Current market price of a trading instrument.
             * 
             * The system will create a Limit Entry order if:
             *      Rate for a buy order is below current market price.
             *      Rate for a sell order is above current market price.
             *      
             * The system will create a Stop Entry order if:
             *      Rate for a buy order is above current market price.
             *      Rate for a sell order is below current market price.
             *      
             *      Note: This command only available when using O2GTableManager.         
             * */
            if ( fxcmOrder == "E" || fxcmOrder == "LE" )
            {
                return OrderTypes.Limit;
            }

            // Limit order is for closing price at the speicified price or better
            if ( fxcmOrder == "L" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.Limit );
                return OrderTypes.Conditional;
            }


            // An open order opens a position at the specified market rate in case such rate is available on the market.
            if ( fxcmOrder == "O" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.MarketOpen );
                return OrderTypes.Conditional;
            }

            /*
             * A stop order is used for limiting losses of the existing position when the market condition is met.
             * Stop orders can be created for existing trades as well as for existing entry orders. 
             * Stop orders created for entry orders remain inactive until the trade is created by the entry order.
             * */
            if ( fxcmOrder == "S" )
            {
                extOrderType = new FxcmExtendedOrderTypes?( FxcmExtendedOrderTypes.Stop );
                return OrderTypes.Conditional;
            }

            throw new ArgumentOutOfRangeException( "value", fxcmOrder, LocalizedStrings.Str1219 );
        }

        public static FxcmContingencyTypes ToFxcmContingencyType( this O2GContingencyType conType )
        {
            switch ( conType )
            {
                case O2GContingencyType.ContingencyTypeNo:
                    return FxcmContingencyTypes.NA;

                case O2GContingencyType.ContingencyTypeOCO:
                    return FxcmContingencyTypes.Oco;

                case O2GContingencyType.ContingencyTypeOTO:
                    return FxcmContingencyTypes.Oto;

                case O2GContingencyType.ContingencyTypeELS:
                    return FxcmContingencyTypes.Els;

                case O2GContingencyType.ContingencyTypeOTOCO:
                    return FxcmContingencyTypes.Otoco;
            }

            return FxcmContingencyTypes.NA;
        }

        public static FxcmContingencyTypes ToFxcmContingencyType( this int conType )
        {
            switch ( conType )
            {
                case 0:
                    return FxcmContingencyTypes.NA;

                case 1:
                    return FxcmContingencyTypes.Oco;

                case 2:
                    return FxcmContingencyTypes.Oto;

                case 3:
                    return FxcmContingencyTypes.Els;

                case 4:
                    return FxcmContingencyTypes.Otoco;
            }

            return FxcmContingencyTypes.NA;
        }

        public static int? ToNullableInt( this int int_0 )
        {
            if ( int_0 != 0 )
            {
                return new int?( int_0 );
            }

            return new int?( );
        }
    }
    static class Extensions
    {
        public static Sides ToSide( this int type )
        {
            return type == 0 ? Sides.Buy : Sides.Sell;
        }

        public static string ToCurrency( this SecurityId securityId )
        {
            return securityId.SecurityCode?.Remove( "/" ).ToLowerInvariant( );
        }

        public static readonly string BitStampBoard = "BSTMP";

        public static bool IsAssociated( this SecurityId secId )
        {
            return secId.BoardCode.EqualsIgnoreCase( BitStampBoard );
        }

        public static SecurityId ToStockSharp( this string currency, bool format = true )
        {
            if ( format )
            {
                if ( currency.Length > 3 && !currency.Contains( "/" ) )
                    currency = currency.Insert( 3, "/" );

                currency = currency.ToUpperInvariant( );
            }

            return new SecurityId
            {
                SecurityCode = currency,
                BoardCode = BitStampBoard,
            };
        }

        public static DateTimeOffset ToDto( this string value, string format = "yyyy-MM-dd HH:mm:ss" )
        {
            return value.ToDateTime( format ).ApplyUtc( );
        }
    }
}