using Ecng.Common;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using xNet;

#pragma warning disable 649

namespace StockSharp.Algo.History.Russian
{
    public class MfdHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private static readonly TimeSpan[ ] timeSpan_0 = ( ( IEnumerable< long > )new long[ 9 ]
        {
      600000000L,
      3000000000L,
      6000000000L,
      9000000000L,
      18000000000L,
      36000000000L,
      864000000000L,
      6048000000000L,
      25920000000000L
        } ).Select< long, TimeSpan >( new Func< long, TimeSpan >( MfdHistorySource.Class120.class120_0.method_0 ) ).ToArray< TimeSpan >( );
        public const string NativeIdStorageName = "MFD";

        public MfdHistorySource(
          INativeIdStorage nativeIdStorage,
          IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        public static IEnumerable< TimeSpan > TimeFrames
        {
            get
            {
                return ( IEnumerable< TimeSpan > )MfdHistorySource.timeSpan_0;
            }
        }

        private static object smethod_0( string string_1, Action< HttpRequest > action_0 )
        {
            if( string_1.IsEmpty( ) )
            {
                throw new ArgumentNullException( "method" );
            }

            if( action_0 == null )
            {
                throw new ArgumentNullException( "init" );
            }

            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                httpRequest.UserAgent = Http.ChromeUserAgent( );
                httpRequest.Referer = "http://mfd.ru/export/";
                action_0( httpRequest );
                object obj1 = JsonConvert.DeserializeObject( httpRequest.Post( "http://mfd.ru/export/" + string_1 ).ToString( ) );
                if( MfdHistorySource.Class117.callSite_0 == null )
                {
                    MfdHistorySource.Class117.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "d", typeof( MfdHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                object obj2 = MfdHistorySource.Class117.callSite_0.Target( ( CallSite )MfdHistorySource.Class117.callSite_0, obj1 );
                if( MfdHistorySource.Class117.callSite_2 == null )
                {
                    MfdHistorySource.Class117.callSite_2 = CallSite< Func< CallSite, object, bool > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( bool ), typeof( MfdHistorySource ) ) );
                }

                Func< CallSite, object, bool > target1 = MfdHistorySource.Class117.callSite_2.Target;
                CallSite< Func< CallSite, object, bool > > callSite2 = MfdHistorySource.Class117.callSite_2;
                if( MfdHistorySource.Class117.callSite_1 == null )
                {
                    MfdHistorySource.Class117.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "Success", typeof( MfdHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                object obj3 = MfdHistorySource.Class117.callSite_1.Target( ( CallSite )MfdHistorySource.Class117.callSite_1, obj2 );
                if( !target1( ( CallSite )callSite2, obj3 ) )
                {
                    if( MfdHistorySource.Class117.callSite_4 == null )
                    {
                        MfdHistorySource.Class117.callSite_4 = CallSite< Func< CallSite, Type, object, InvalidOperationException > >.Create( Binder.InvokeConstructor( CSharpBinderFlags.None, typeof( MfdHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                       } ) );
                    }

                    Func< CallSite, Type, object, InvalidOperationException > target2 = MfdHistorySource.Class117.callSite_4.Target;
                    CallSite< Func< CallSite, Type, object, InvalidOperationException > > callSite4 = MfdHistorySource.Class117.callSite_4;
                    Type type = typeof( InvalidOperationException );
                    if( MfdHistorySource.Class117.callSite_3 == null )
                    {
                        MfdHistorySource.Class117.callSite_3 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "Message", typeof( MfdHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                       } ) );
                    }

                    object obj4 = MfdHistorySource.Class117.callSite_3.Target( ( CallSite )MfdHistorySource.Class117.callSite_3, obj2 );
                    throw target2( ( CallSite )callSite4, type, obj4 );
                }
                return obj2;
            }
        }

        private static IEnumerable< Tuple< string, string, OptionTypes?, DateTimeOffset? > > smethod_1(
          MfdHistorySource.Enum1 enum1_0,
          Security security_0 )
        {
            throw new NotImplementedException( );
            //return ( IEnumerable<Tuple<string, string, OptionTypes?, DateTimeOffset?>> )new MfdHistorySource.Class121( -2 )
            //{
            //    enum1_1 = enum1_0,
            //    security_1 = security_0
            //};
        }

        public void Refresh(
          ISecurityStorage securityStorage,
          Security criteria,
          Action< Security > newSecurity,
          Func< bool > isCancelled )
        {
            if( securityStorage == null )
            {
                throw new ArgumentNullException( nameof( securityStorage ) );
            }

            if( criteria == null )
            {
                throw new ArgumentNullException( nameof( criteria ) );
            }

            if( newSecurity == null )
            {
                throw new ArgumentNullException( nameof( newSecurity ) );
            }

            if( isCancelled == null )
            {
                throw new ArgumentNullException( nameof( isCancelled ) );
            }

            SecurityTypes? type1 = criteria.Type;
            bool flag = type1.GetValueOrDefault( ) == SecurityTypes.Option & type1.HasValue;
            MfdHistorySource.Enum1[ ] enum1Array = new MfdHistorySource.Enum1[ 20 ]
            {
        ( MfdHistorySource.Enum1 ) 11,
        ( MfdHistorySource.Enum1 ) 12,
        ( MfdHistorySource.Enum1 ) 3,
        ( MfdHistorySource.Enum1 ) 25,
        ( MfdHistorySource.Enum1 ) 26,
        ( MfdHistorySource.Enum1 ) 35,
        ( MfdHistorySource.Enum1 ) 23,
        ( MfdHistorySource.Enum1 ) 36,
        ( MfdHistorySource.Enum1 ) 34,
        ( MfdHistorySource.Enum1 ) 13,
        ( MfdHistorySource.Enum1 ) 29,
        ( MfdHistorySource.Enum1 ) 16,
        ( MfdHistorySource.Enum1 ) 7,
        ( MfdHistorySource.Enum1 ) 30,
        ( MfdHistorySource.Enum1 ) 6,
        ( MfdHistorySource.Enum1 ) 14,
        ( MfdHistorySource.Enum1 ) 17,
        ( MfdHistorySource.Enum1 ) 18,
        ( MfdHistorySource.Enum1 ) 33,
        ( MfdHistorySource.Enum1 ) 2
            };
            foreach( MfdHistorySource.Enum1 enum1_0 in enum1Array )
            {
                if( flag )
                {
                    if( enum1_0 != ( MfdHistorySource.Enum1 )25 )
                    {
                        continue;
                    }
                }
                else if( enum1_0 == ( MfdHistorySource.Enum1 )25 )
                {
                    continue;
                }

                int num1 = ( int )enum1_0;
                foreach( Tuple< string, string, OptionTypes?, DateTimeOffset? > tuple1 in MfdHistorySource.smethod_1( enum1_0, criteria ) )
                {
                    if( isCancelled( ) )
                    {
                        return;
                    }

                    string str1 = tuple1.Item2;
                    string str1_1 = tuple1.Item1;
                    int startIndex = str1_1.IndexOf( '(' );
                    int num2 = str1_1.IndexOf( ')' );
                    if( startIndex != -1 && num2 > startIndex )
                    {
                        string str2 = str1_1.Substring( startIndex + 1, num2 - startIndex - 1 );
                        if( str2 != "вал" && str2 != "цен" && str2 != "XS" )
                        {
                            str1_1 = str1_1.Remove( startIndex, num2 - startIndex + 1 ).Trim( );
                        }
                    }
                    if( criteria.Code.IsEmpty( ) || str1_1.ContainsIgnoreCase( criteria.Code ) )
                    {
                        Tuple< int, string > tuple2 = Tuple.Create< int, string >( num1, str1 );
                        Security security1 = securityStorage.LookupByNativeId( this.NativeIdStorage, "MFD", ( object )tuple2 );
                        if( security1 == null )
                        {
                            Security security2 = new Security( )
                            {
                                Id = ( string )null,
                                Code = str1_1,
                                Name = str1_1,
                                OptionType = tuple1.Item3,
                                ExpiryDate = tuple1.Item4
                            };
                            switch( enum1_0 )
                            {
                                case ( MfdHistorySource.Enum1 )2:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Commodity );
                                    security2.Board = ExchangeBoard.Mfd;
                                    break;
                                case ( MfdHistorySource.Enum1 )3:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Currency );
                                    security2.Board = ExchangeBoard.Mfd;
                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )6:
                                    security2.Board = ExchangeBoard.Micex;
                                    break;
                                case ( MfdHistorySource.Enum1 )7:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Currency );
                                    security2.Board = ExchangeBoard.Micex;
                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )11:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Adr );
                                    security2.Board = ExchangeBoard.Mfd;
                                    break;
                                case ( MfdHistorySource.Enum1 )12:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Adr );
                                    security2.Board = ExchangeBoard.Mfd;
                                    break;
                                case ( MfdHistorySource.Enum1 )13:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Index );
                                    security2.Board = ExchangeBoard.Mfd;
                                    break;
                                case ( MfdHistorySource.Enum1 )14:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Index );
                                    security2.Board = ExchangeBoard.Micex;
                                    break;
                                case ( MfdHistorySource.Enum1 )16:
                                    MoexDownloader.GetSecurities( security2.Code, new SecurityTypes?( SecurityTypes.Stock ), false ).FirstOrDefault< SecurityInfo >( )?.FillTo( security2, this.ExchangeInfoProvider );
                                    if( !security2.Type.HasValue )
                                    {
                                        security2.Type = new SecurityTypes?( SecurityTypes.Stock );
                                    }

                                    if( ( Equatable< ExchangeBoard > )security2.Board == ( ExchangeBoard )null )
                                    {
                                        security2.Board = ExchangeBoard.Micex;
                                    }

                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )17:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Bond );
                                    security2.Board = ExchangeBoard.Micex;
                                    break;
                                case ( MfdHistorySource.Enum1 )18:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Stock );
                                    security2.Board = ExchangeBoard.Forts;
                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )23:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Commodity );
                                    security2.Board = ExchangeBoard.Mfd;
                                    break;
                                case ( MfdHistorySource.Enum1 )25:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Option );
                                    security2.Board = ExchangeBoard.Forts;
                                    SecurityInfo securityInfo1 = MoexDownloader.GetSecurities( security2.Code, security2.Type, false ).FirstOrDefault< SecurityInfo >( );
                                    if( securityInfo1 != null )
                                    {
                                        securityInfo1.FillTo( security2, this.ExchangeInfoProvider );
                                        break;
                                    }
                                    break;
                                case ( MfdHistorySource.Enum1 )26:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Future );
                                    security2.Board = ExchangeBoard.Forts;
                                    SecurityInfo securityInfo2 = MoexDownloader.GetSecurities( security2.Code, security2.Type, true ).FirstOrDefault< SecurityInfo >( );
                                    if( securityInfo2 != null )
                                    {
                                        securityInfo2.FillTo( security2, this.ExchangeInfoProvider );
                                        break;
                                    }
                                    break;
                                case ( MfdHistorySource.Enum1 )29:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Future );
                                    security2.Board = ExchangeBoard.Micex;
                                    break;
                                case ( MfdHistorySource.Enum1 )30:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Index );
                                    security2.Board = ExchangeBoard.Micex;
                                    break;
                                case ( MfdHistorySource.Enum1 )33:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Stock );
                                    security2.Board = ExchangeBoard.Spb;
                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )34:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Currency );
                                    security2.Board = ExchangeBoard.Micex;
                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )35:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Stock );
                                    security2.Board = ExchangeBoard.Forts;
                                    security2.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )2 ) );
                                    break;
                                case ( MfdHistorySource.Enum1 )36:
                                    security2.Type = new SecurityTypes?( SecurityTypes.Commodity );
                                    security2.Board = ExchangeBoard.Micex;
                                    break;
                                default:
                                    security2.Board = ExchangeBoard.Mfd;
                                    break;
                            }
                            if( criteria.Type.HasValue )
                            {
                                SecurityTypes? type2 = security2.Type;
                                SecurityTypes? type3 = criteria.Type;
                                if( !( type2.GetValueOrDefault( ) == type3.GetValueOrDefault( ) & type2.HasValue == type3.HasValue ) )
                                {
                                    continue;
                                }
                            }
                            security2.Id = this.SecurityIdGenerator.GenerateId( security2.Code, security2.Board );
                            Security security3 = securityStorage.LookupById( security2.Id );
                            if( security3 != null )
                            {
                                security2 = security3;
                            }
                            else
                            {
                                newSecurity( security2 );
                            }

                            securityStorage.Save( security2, false );
                            this.NativeIdStorage.TryAdd( "MFD", security2.ToSecurityId( ( SecurityIdGenerator )null ), ( object )tuple2, true );
                        }
                        else if( num1 != this.method_2( security1 ) )
                        {
                            securityStorage.Save( security1, false );
                            this.NativeIdStorage.TryAdd( "MFD", security1.ToSecurityId( ( SecurityIdGenerator )null ), ( object )tuple2, true );
                        }
                    }
                }
            }
        }

        public override string GetDumpFile(
          Security security,
          DateTime from,
          DateTime to,
          Type dataType,
          object arg )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( dataType == ( Type )null )
            {
                throw new ArgumentNullException( nameof( dataType ) );
            }

            if( dataType != typeof( ExecutionMessage ) && !dataType.IsCandleMessage( ) )
            {
                throw new ArgumentOutOfRangeException( nameof( dataType ), ( object )dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, this.method_2( security ).ToString( ) + "_" + this.GetSecurityCode( security ).SecurityIdToFolderName( ), "{0}_{1}_{2:yyyy_MM_dd}_{3:yyyy_MM_dd}.txt".Put( ( object )dataType.Name.Remove( "Message", false ).ToLowerInvariant( ), arg is ExecutionTypes ? ( object )( string )null : ( object )TraderHelper.CandleArgToFolderName( arg ), ( object )from, ( object )to ) );
        }

        public IEnumerable< ExecutionMessage > GetTicks(
          Security security,
          DateTime from,
          DateTime to )
        {
            return this.method_0< ExecutionMessage >( security, from, to, typeof( ExecutionMessage ), ( object )ExecutionTypes.Tick, this.method_1( security, TimeSpan.FromTicks( 1L ), from, to ), new Func< FastCsvReader, ExecutionMessage >( new MfdHistorySource.Class125( )
            {
                securityId_0 = security.ToSecurityId( this.SecurityIdGenerator )
            }.method_0 ) );
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
          Security security,
          TimeSpan timeFrame,
          DateTime from,
          DateTime to )
        {
            MfdHistorySource.Class119 class119 = new MfdHistorySource.Class119( );
            class119.timeSpan_0 = timeFrame;
            class119.securityId_0 = security.ToSecurityId( this.SecurityIdGenerator );
            return this.method_0< TimeFrameCandleMessage >( security, from, to, typeof( TimeFrameCandleMessage ), ( object )class119.timeSpan_0, this.method_1( security, class119.timeSpan_0, from, to ), new Func< FastCsvReader, TimeFrameCandleMessage >( class119.method_0 ) );
        }

        private static DateTimeOffset smethod_2( FastCsvReader fastCsvReader_0 )
        {
            return ( fastCsvReader_0.ReadDateTime( "yyyyMMdd" ) + fastCsvReader_0.ReadTimeSpan( "hhmmss" ) ).ApplyTimeZone( TimeHelper.Moscow );
        }

        private IEnumerable< T > method_0< T >(
          Security security_0,
          DateTime dateTime_0,
          DateTime dateTime_1,
          Type type_0,
          object object_0,
          string string_1,
          Func< FastCsvReader, T > func_0 )
        {
            MfdHistorySource.Class124< T > class124 = new MfdHistorySource.Class124< T >( );
            class124.string_0 = string_1;
            class124.func_0 = func_0;
            class124.mfdHistorySource_0 = this;
            class124.security_0 = security_0;
            class124.dateTime_0 = dateTime_0;
            class124.dateTime_1 = dateTime_1;
            class124.type_0 = type_0;
            class124.object_0 = object_0;
            if( class124.string_0.IsEmpty( ) )
            {
                throw new ArgumentNullException( "url" );
            }

            if( class124.func_0 == null )
            {
                throw new ArgumentNullException( "converter" );
            }

            TextReader reader = this.Process( class124.security_0, class124.dateTime_0, class124.dateTime_1, class124.type_0, class124.object_0, new Func< string >( class124.method_0 ) );
            using( reader )
            {
                MfdHistorySource.Class118< T > class118 = new MfdHistorySource.Class118< T >( );
                class118.class124_0 = class124;
                class118.fastCsvReader_0 = new FastCsvReader( reader );
                do
                {
                    ;
                }
                while ( class118.fastCsvReader_0.NextLine( ) && class118.fastCsvReader_0.CurrentLine.IsEmpty( ) );
                if( class118.fastCsvReader_0.CurrentLine.IsEmpty( ) )
                {
                    return Enumerable.Empty< T >( );
                }

                return ( IEnumerable< T > )CultureInfo.InvariantCulture.DoInCulture< List< T > >( new Func< List< T > >( class118.method_0 ) );
            }
        }

        private string method_1(
          Security security_0,
          TimeSpan timeSpan_1,
          DateTime dateTime_0,
          DateTime dateTime_1 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            int num1;
            if( timeSpan_1.Ticks == 1L )
            {
                num1 = 1;
            }
            else
            {
                int num2 = MfdHistorySource.timeSpan_0.IndexOf< TimeSpan >( timeSpan_1 );
                if( num2 == -1 )
                {
                    throw new ArgumentOutOfRangeException( "timeFrame", ( object )timeSpan_1, LocalizedStrings.Str2102 );
                }

                num1 = num2 + 1;
            }
            Tuple< int, string > bySecurityId = ( Tuple< int, string > )this.NativeIdStorage.TryGetBySecurityId( "MFD", security_0.ToSecurityId( ( SecurityIdGenerator )null ) );
            if( bySecurityId == null )
            {
                throw new ArgumentException( LocalizedStrings.Str2099Params.Put( ( object )security_0, ( object )"MFD" ) );
            }

            return "http://mfd.ru/export/handler.ashx/TempData.txt?TickerGroup={0}&Tickers={1}&Alias=false&Period={2}&timeframeValue=1&timeframeDatePart=day&StartDate={3:dd.MM.yyyy}&EndDate={4:dd.MM.yyyy}&SaveFormat=0&SaveMode=0&FileName=TempData.txt&FieldSeparator=%253b&DecimalSeparator=.&DateFormat=yyyyMMdd&TimeFormat=HHmmss&DateFormatCustom=&TimeFormatCustom=&AddHeader=false&RecordFormat={5}&Fill=false".Put( ( object )bySecurityId.Item1, ( object )bySecurityId.Item2, ( object )num1, ( object )dateTime_0, ( object )dateTime_1, ( object )( timeSpan_1.Ticks == 1L ? 2 : 0 ) );
        }

        private int method_2( Security security_0 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            Tuple< int, string > bySecurityId = ( Tuple< int, string > )this.NativeIdStorage.TryGetBySecurityId( "MFD", security_0.ToSecurityId( ( SecurityIdGenerator )null ) );
            if( bySecurityId == null )
            {
                throw new ArgumentException( LocalizedStrings.Str2099Params.Put( ( object )security_0, ( object )"MFD (market)" ) );
            }

            return bySecurityId.Item1;
        }

        private static class Class115
        {
            public static CallSite< Func< CallSite, object, object > > callSite_0;
            public static CallSite< Func< CallSite, object, object > > callSite_1;
            public static CallSite< Func< CallSite, object, string > > callSite_2;
            public static CallSite< Func< CallSite, object, object > > callSite_3;
            public static CallSite< Func< CallSite, object, int > > callSite_4;
            public static CallSite< Func< CallSite, object, object > > callSite_5;
            public static CallSite< Func< CallSite, object, object > > callSite_6;
            public static CallSite< Func< CallSite, object, string > > callSite_7;
            public static CallSite< Func< CallSite, object, object > > callSite_8;
            public static CallSite< Func< CallSite, object, object > > callSite_9;
            public static CallSite< Func< CallSite, object, string > > callSite_10;
            public static CallSite< Func< CallSite, object, object > > callSite_11;
            public static CallSite< Func< CallSite, object, string > > callSite_12;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_13;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_14;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_15;
            public static CallSite< Func< CallSite, object, object > > callSite_16;
            public static CallSite< Func< CallSite, object, string > > callSite_17;
        }

        private sealed class Class116
        {
            public int int_0;

            internal void method_0( HttpRequest httpRequest_0 )
            {
                httpRequest_0.AddParam( "contractId", ( object )this.int_0 );
            }
        }

        private static class Class117
        {
            public static CallSite< Func< CallSite, object, object > > callSite_0;
            public static CallSite< Func< CallSite, object, object > > callSite_1;
            public static CallSite< Func< CallSite, object, bool > > callSite_2;
            public static CallSite< Func< CallSite, object, object > > callSite_3;
            public static CallSite< Func< CallSite, Type, object, InvalidOperationException > > callSite_4;
        }

        private sealed class Class118< T >
        {
            public FastCsvReader fastCsvReader_0;
            public MfdHistorySource.Class124< T > class124_0;

            internal List< T > method_0( )
            {
                List< T > objList = new List< T >( );
                do
                {
                    try
                    {
                        objList.Add( this.class124_0.func_0( this.fastCsvReader_0 ) );
                    }
                    catch( Exception ex )
                    {
                        string dumpFile = this.class124_0.mfdHistorySource_0.GetDumpFile( this.class124_0.security_0, this.class124_0.dateTime_0, this.class124_0.dateTime_1, this.class124_0.type_0, this.class124_0.object_0 );
                        if( this.class124_0.mfdHistorySource_0.CanDump )
                        {
                            File.Delete( dumpFile );
                        }

                        throw new InvalidOperationException( LocalizedStrings.Str2098Params.Put( ( object )dumpFile, ( object )this.fastCsvReader_0.CurrentLine ), ex );
                    }
                }
                while ( this.fastCsvReader_0.NextLine( ) );
                return objList;
            }
        }

        private sealed class Class119
        {
            public SecurityId securityId_0;
            public TimeSpan timeSpan_0;

            internal TimeFrameCandleMessage method_0( FastCsvReader fastCsvReader_0 )
            {
                fastCsvReader_0.Skip( 2 );
                DateTimeOffset dateTimeOffset = MfdHistorySource.smethod_2( fastCsvReader_0 );
                TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                frameCandleMessage.SecurityId = this.securityId_0;
                frameCandleMessage.OpenTime = dateTimeOffset;
                frameCandleMessage.CloseTime = dateTimeOffset + this.timeSpan_0;
                frameCandleMessage.TimeFrame = this.timeSpan_0;
                frameCandleMessage.OpenPrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.HighPrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.LowPrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.ClosePrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.TotalVolume = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.OpenInterest = new Decimal?( fastCsvReader_0.ReadDecimal( ) );
                frameCandleMessage.State = CandleStates.Finished;
                return frameCandleMessage;
            }
        }

        [Serializable]
        private sealed class Class120
        {
            public static readonly MfdHistorySource.Class120 class120_0 = new MfdHistorySource.Class120( );

            internal TimeSpan method_0( long long_0 )
            {
                return long_0.To< TimeSpan >( );
            }
        }

        //    private sealed class Class121 : IEnumerable<Tuple<string, string, OptionTypes?, DateTimeOffset?>>, IEnumerator<Tuple<string, string, OptionTypes?, DateTimeOffset?>>, IEnumerable, IDisposable, IEnumerator
        //    {
        //      private int int_0;
        //      private Tuple<string, string, OptionTypes?, DateTimeOffset?> tuple_0;
        //      private int int_1;
        //      private Security security_0;
        //      public Security security_1;
        //      private MfdHistorySource.Enum1 enum1_0;
        //      public MfdHistorySource.Enum1 enum1_1;
        //      private MfdHistorySource.Class116 class116_0;
        //      private IEnumerator ienumerator_0;
        //      private OptionTypes? nullable_0;
        //      private IEnumerator ienumerator_1;
        //      private DateTimeOffset dateTimeOffset_0;
        //      private IEnumerator ienumerator_2;
        //      private IEnumerator<XElement> ienumerator_3;

        //      [DebuggerHidden]
        //      public Class121(int int_2)
        //      {
        //        this.int_0 = int_2;
        //        this.int_1 = Environment.CurrentManagedThreadId;
        //      }

        //      [DebuggerHidden]
        //      void IDisposable.Dispose()
        //      {
        //        int int0 = this.int_0;
        //        switch (int0)
        //        {
        //          case -6:
        //          case 2:
        //            try
        //            {
        //            }
        //            finally
        //            {
        //              this.method_3();
        //            }
        //            break;
        //          case -5:
        //          case -4:
        //          case -3:
        //          case 1:
        //            try
        //            {
        //              switch (int0)
        //              {
        //                case -5:
        //                case -4:
        //                case 1:
        //                  try
        //                  {
        //                    if (int0 != -5 && int0 != 1)
        //                      return;
        //                    try
        //                    {
        //                    }
        //                    finally
        //                    {
        //                      this.method_2();
        //                    }
        //                    return;
        //                  }
        //                  finally
        //                  {
        //                    this.method_1();
        //                  }
        //                default:
        //                  return;
        //              }
        //            }
        //            finally
        //            {
        //              this.method_0();
        //            }
        //        }
        //      }

        //      bool IEnumerator.MoveNext()
        //      {
        //        // ISSUE: fault handler
        //        try
        //        {
        //          switch (this.int_0)
        //          {
        //            case 0:
        //              this.int_0 = -1;
        //              MfdHistorySource.Class122 class122 = new MfdHistorySource.Class122();
        //              if (this.security_0 == null)
        //                throw new ArgumentNullException("criteria");
        //              class122.int_0 = (int) this.enum1_0;
        //              if (this.enum1_0 == (MfdHistorySource.Enum1) 25)
        //              {
        //                if (MfdHistorySource.Class115.callSite_0 == null)
        //                  MfdHistorySource.Class115.callSite_0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Data", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //                  {
        //                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //                  }));
        //                object obj = MfdHistorySource.Class115.callSite_0.Target((CallSite) MfdHistorySource.Class115.callSite_0, MfdHistorySource.smethod_0("getcontracts", new Action<HttpRequest>(class122.method_0)));
        //                if (MfdHistorySource.Class115.callSite_15 == null)
        //                  MfdHistorySource.Class115.callSite_15 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (MfdHistorySource)));
        //                this.ienumerator_0 = MfdHistorySource.Class115.callSite_15.Target((CallSite) MfdHistorySource.Class115.callSite_15, obj).GetEnumerator();
        //                this.int_0 = -3;
        //                break;
        //              }
        //              if (MfdHistorySource.Class115.callSite_17 == null)
        //                MfdHistorySource.Class115.callSite_17 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MfdHistorySource)));
        //              Func<CallSite, object, string> target1 = MfdHistorySource.Class115.callSite_17.Target;
        //              CallSite<Func<CallSite, object, string>> callSite17 = MfdHistorySource.Class115.callSite_17;
        //              if (MfdHistorySource.Class115.callSite_16 == null)
        //                MfdHistorySource.Class115.callSite_16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Message", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //                {
        //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //                }));
        //              object obj1 = MfdHistorySource.Class115.callSite_16.Target((CallSite) MfdHistorySource.Class115.callSite_16, MfdHistorySource.smethod_0("getinstrumentoptions", new Action<HttpRequest>(class122.method_1)));
        //              this.ienumerator_3 = "<options>{0}</options>".Put((object) (target1((CallSite) callSite17, obj1).Replace("<option", "</option><option") + "</option>").Substring(9)).To<XDocument>().Elements().First<XElement>().Elements().GetEnumerator();
        //              this.int_0 = -6;
        //              goto label_64;
        //            case 1:
        //              this.int_0 = -5;
        //              goto label_52;
        //            case 2:
        //              this.int_0 = -6;
        //              goto label_64;
        //            default:
        //              return false;
        //          }
        //label_36:
        //          while (this.ienumerator_0.MoveNext())
        //          {
        //            object current = this.ienumerator_0.Current;
        //            this.class116_0 = new MfdHistorySource.Class116();
        //            if (MfdHistorySource.Class115.callSite_2 == null)
        //              MfdHistorySource.Class115.callSite_2 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MfdHistorySource)));
        //            Func<CallSite, object, string> target2 = MfdHistorySource.Class115.callSite_2.Target;
        //            CallSite<Func<CallSite, object, string>> callSite2 = MfdHistorySource.Class115.callSite_2;
        //            if (MfdHistorySource.Class115.callSite_1 == null)
        //              MfdHistorySource.Class115.callSite_1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "text", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //              {
        //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //              }));
        //            object obj2 = MfdHistorySource.Class115.callSite_1.Target((CallSite) MfdHistorySource.Class115.callSite_1, current);
        //            string lowerInvariant = target2((CallSite) callSite2, obj2)?.ToLowerInvariant();
        //            if (this.security_0.Code.IsEmpty() || this.security_0.Code.StartsWithIgnoreCase(lowerInvariant.Remove(" опцион колл", true).Remove(" опцион пут", true)))
        //            {
        //              this.nullable_0 = new OptionTypes?();
        //              if (lowerInvariant != null && lowerInvariant.EndsWith("колл") || lowerInvariant != null && lowerInvariant.EndsWith("call"))
        //                this.nullable_0 = new OptionTypes?(OptionTypes.Call);
        //              else if (lowerInvariant != null && lowerInvariant.EndsWith("пут") || lowerInvariant != null && lowerInvariant.EndsWith("put"))
        //                this.nullable_0 = new OptionTypes?(OptionTypes.Put);
        //              OptionTypes? optionType = this.security_0.OptionType;
        //              if (optionType.HasValue && this.nullable_0.HasValue)
        //              {
        //                optionType = this.security_0.OptionType;
        //                if (optionType.Value != this.nullable_0.Value)
        //                  continue;
        //              }
        //              MfdHistorySource.Class116 class1160 = this.class116_0;
        //              if (MfdHistorySource.Class115.callSite_4 == null)
        //                MfdHistorySource.Class115.callSite_4 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (MfdHistorySource)));
        //              Func<CallSite, object, int> target3 = MfdHistorySource.Class115.callSite_4.Target;
        //              CallSite<Func<CallSite, object, int>> callSite4 = MfdHistorySource.Class115.callSite_4;
        //              if (MfdHistorySource.Class115.callSite_3 == null)
        //                MfdHistorySource.Class115.callSite_3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "value", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //                {
        //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //                }));
        //              object obj3 = MfdHistorySource.Class115.callSite_3.Target((CallSite) MfdHistorySource.Class115.callSite_3, current);
        //              int num = target3((CallSite) callSite4, obj3);
        //              class1160.int_0 = num;
        //              if (MfdHistorySource.Class115.callSite_5 == null)
        //                MfdHistorySource.Class115.callSite_5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Data", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //                {
        //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //                }));
        //              object obj4 = MfdHistorySource.Class115.callSite_5.Target((CallSite) MfdHistorySource.Class115.callSite_5, MfdHistorySource.smethod_0("getcontractmonths", new Action<HttpRequest>(this.class116_0.method_0)));
        //              if (MfdHistorySource.Class115.callSite_14 == null)
        //                MfdHistorySource.Class115.callSite_14 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (MfdHistorySource)));
        //              this.ienumerator_1 = MfdHistorySource.Class115.callSite_14.Target((CallSite) MfdHistorySource.Class115.callSite_14, obj4).GetEnumerator();
        //              this.int_0 = -4;
        //              goto label_46;
        //            }
        //          }
        //          this.method_0();
        //          this.ienumerator_0 = (IEnumerator) null;
        //          goto label_66;
        //label_46:
        //          while (this.ienumerator_1.MoveNext())
        //          {
        //            object current = this.ienumerator_1.Current;
        //            MfdHistorySource.Class123 class123_1 = new MfdHistorySource.Class123();
        //            class123_1.class116_0 = this.class116_0;
        //            MfdHistorySource.Class123 class123_2 = class123_1;
        //            if (MfdHistorySource.Class115.callSite_7 == null)
        //              MfdHistorySource.Class115.callSite_7 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MfdHistorySource)));
        //            Func<CallSite, object, string> target2 = MfdHistorySource.Class115.callSite_7.Target;
        //            CallSite<Func<CallSite, object, string>> callSite7 = MfdHistorySource.Class115.callSite_7;
        //            if (MfdHistorySource.Class115.callSite_6 == null)
        //              MfdHistorySource.Class115.callSite_6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "value", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //              {
        //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //              }));
        //            object obj2 = MfdHistorySource.Class115.callSite_6.Target((CallSite) MfdHistorySource.Class115.callSite_6, current);
        //            string str = target2((CallSite) callSite7, obj2);
        //            class123_2.string_0 = str;
        //            this.dateTimeOffset_0 = new DateTime(class123_1.string_0.Substring(0, 4).To<int>(), class123_1.string_0.Substring(4, 2).To<int>(), 15).ApplyTimeZone(TimeHelper.Moscow);
        //            DateTimeOffset? expiryDate = this.security_0.ExpiryDate;
        //            if (expiryDate.HasValue)
        //            {
        //              expiryDate = this.security_0.ExpiryDate;
        //              DateTimeOffset dateTimeOffset = expiryDate.Value;
        //              if (dateTimeOffset.Year == this.dateTimeOffset_0.Year)
        //              {
        //                expiryDate = this.security_0.ExpiryDate;
        //                dateTimeOffset = expiryDate.Value;
        //                if (dateTimeOffset.Month != this.dateTimeOffset_0.Month)
        //                  continue;
        //              }
        //              else
        //                continue;
        //            }
        //            if (MfdHistorySource.Class115.callSite_8 == null)
        //              MfdHistorySource.Class115.callSite_8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Data", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //              {
        //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //              }));
        //            object obj3 = MfdHistorySource.Class115.callSite_8.Target((CallSite) MfdHistorySource.Class115.callSite_8, MfdHistorySource.smethod_0("getcontracttickers", new Action<HttpRequest>(class123_1.method_0)));
        //            if (MfdHistorySource.Class115.callSite_13 == null)
        //              MfdHistorySource.Class115.callSite_13 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (MfdHistorySource)));
        //            this.ienumerator_2 = MfdHistorySource.Class115.callSite_13.Target((CallSite) MfdHistorySource.Class115.callSite_13, obj3).GetEnumerator();
        //            this.int_0 = -5;
        //            goto label_52;
        //          }
        //          this.method_1();
        //          this.ienumerator_1 = (IEnumerator) null;
        //          this.class116_0 = (MfdHistorySource.Class116) null;
        //          this.nullable_0 = new OptionTypes?();
        //          goto label_36;
        //label_52:
        //          if (!this.ienumerator_2.MoveNext())
        //          {
        //            this.method_2();
        //            this.ienumerator_2 = (IEnumerator) null;
        //            this.dateTimeOffset_0 = new DateTimeOffset();
        //            goto label_46;
        //          }
        //          else
        //          {
        //            object current = this.ienumerator_2.Current;
        //            if (MfdHistorySource.Class115.callSite_10 == null)
        //              MfdHistorySource.Class115.callSite_10 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MfdHistorySource)));
        //            Func<CallSite, object, string> target2 = MfdHistorySource.Class115.callSite_10.Target;
        //            CallSite<Func<CallSite, object, string>> callSite10 = MfdHistorySource.Class115.callSite_10;
        //            if (MfdHistorySource.Class115.callSite_9 == null)
        //              MfdHistorySource.Class115.callSite_9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "text", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //              {
        //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //              }));
        //            object obj2 = MfdHistorySource.Class115.callSite_9.Target((CallSite) MfdHistorySource.Class115.callSite_9, current);
        //            string str1 = target2((CallSite) callSite10, obj2);
        //            if (MfdHistorySource.Class115.callSite_12 == null)
        //              MfdHistorySource.Class115.callSite_12 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MfdHistorySource)));
        //            Func<CallSite, object, string> target3 = MfdHistorySource.Class115.callSite_12.Target;
        //            CallSite<Func<CallSite, object, string>> callSite12 = MfdHistorySource.Class115.callSite_12;
        //            if (MfdHistorySource.Class115.callSite_11 == null)
        //              MfdHistorySource.Class115.callSite_11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "value", typeof (MfdHistorySource), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        //              {
        //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        //              }));
        //            object obj3 = MfdHistorySource.Class115.callSite_11.Target((CallSite) MfdHistorySource.Class115.callSite_11, current);
        //            string str2 = target3((CallSite) callSite12, obj3);
        //            OptionTypes? nullable0 = this.nullable_0;
        //            DateTimeOffset? nullable = new DateTimeOffset?(this.dateTimeOffset_0);
        //            this.tuple_0 = Tuple.Create<string, string, OptionTypes?, DateTimeOffset?>(str1, str2, nullable0, nullable);
        //            this.int_0 = 1;
        //            return true;
        //          }
        //label_64:
        //          if (!this.ienumerator_3.MoveNext())
        //          {
        //            this.method_3();
        //            this.ienumerator_3 = (IEnumerator<XElement>) null;
        //          }
        //          else
        //          {
        //            XElement current = this.ienumerator_3.Current;
        //            this.tuple_0 = Tuple.Create<string, string, OptionTypes?, DateTimeOffset?>(current.Value, current.GetAttributeValue<string>("value", (string) null), new OptionTypes?(), new DateTimeOffset?());
        //            this.int_0 = 2;
        //            return true;
        //          }
        //label_66:
        //          return false;
        //        }
        //        finally
        //        {
        //          //this.System\u002EIDisposable\u002EDispose();
        //        }
        //      }

        //      private void method_0()
        //      {
        //        this.int_0 = -1;
        //        (this.ienumerator_0 as IDisposable)?.Dispose();
        //      }

        //      private void method_1()
        //      {
        //        this.int_0 = -3;
        //        (this.ienumerator_1 as IDisposable)?.Dispose();
        //      }

        //      private void method_2()
        //      {
        //        this.int_0 = -4;
        //        (this.ienumerator_2 as IDisposable)?.Dispose();
        //      }

        //      private void method_3()
        //      {
        //        this.int_0 = -1;
        //        if (this.ienumerator_3 == null)
        //          return;
        //        this.ienumerator_3.Dispose();
        //      }

        //      Tuple<string, string, OptionTypes?, DateTimeOffset?> IEnumerator<Tuple<string, string, OptionTypes?, DateTimeOffset?>>.Current
        //      {
        //        [DebuggerHidden] get
        //        {
        //          return this.tuple_0;
        //        }
        //      }

        //      [DebuggerHidden]
        //      Tuple<string, string, OptionTypes?, DateTimeOffset?> IEnumerator<Tuple<string, string, OptionTypes?, DateTimeOffset?>>.get_Current()
        //      {
        //        return this.tuple_0;
        //      }

        //      [DebuggerHidden]
        //      void IEnumerator.Reset()
        //      {
        //        throw new NotSupportedException();
        //      }

        //      object IEnumerator.Current
        //      {
        //        [DebuggerHidden] get
        //        {
        //          return (object) this.tuple_0;
        //        }
        //      }

        //      [DebuggerHidden]
        //      object IEnumerator.get_Current()
        //      {
        //        return (object) this.tuple_0;
        //      }

        //      [DebuggerHidden]
        //      IEnumerator<Tuple<string, string, OptionTypes?, DateTimeOffset?>> IEnumerable<Tuple<string, string, OptionTypes?, DateTimeOffset?>>.GetEnumerator()
        //      {
        //        MfdHistorySource.Class121 class121;
        //        if (this.int_0 == -2 && this.int_1 == Environment.CurrentManagedThreadId)
        //        {
        //          this.int_0 = 0;
        //          class121 = this;
        //        }
        //        else
        //          class121 = new MfdHistorySource.Class121(0);
        //        class121.enum1_0 = this.enum1_1;
        //        class121.security_0 = this.security_1;
        //        return (IEnumerator<Tuple<string, string, OptionTypes?, DateTimeOffset?>>) class121;
        //      }

        //      [DebuggerHidden]
        //      IEnumerator IEnumerable.GetEnumerator()
        //      {
        //        return (IEnumerator) this.System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CSystem\u002ETuple\u003CSystem\u002EString\u002CSystem\u002EString\u002CSystem\u002ENullable\u003CStockSharp\u002EMessages\u002EOptionTypes\u003E\u002CSystem\u002ENullable\u003CSystem\u002EDateTimeOffset\u003E\u003E\u003E\u002EGetEnumerator();
        //      }
        //    }

        private sealed class Class122
        {
            public int int_0;

            internal void method_0( HttpRequest httpRequest_0 )
            {
                httpRequest_0.AddParam( "marketId", ( object )this.int_0 );
            }

            internal void method_1( HttpRequest httpRequest_0 )
            {
                httpRequest_0.AddParam( "marketId", ( object )this.int_0 );
            }
        }

        private enum Enum1
        {
        }

        private sealed class Class123
        {
            public string string_0;
            public MfdHistorySource.Class116 class116_0;

            internal void method_0( HttpRequest httpRequest_0 )
            {
                httpRequest_0.AddParam( "contractId", ( object )this.class116_0.int_0 ).AddParam( "contractMonth", ( object )this.string_0 );
            }
        }

        private sealed class Class124< T >
        {
            public string string_0;
            public Func< FastCsvReader, T > func_0;
            public MfdHistorySource mfdHistorySource_0;
            public Security security_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public Type type_0;
            public object object_0;

            internal string method_0( )
            {
                using( HttpRequest httpRequest = new HttpRequest( )
                {
                    CharacterSet = Encoding.UTF8
                } )
                {
                    return httpRequest.Get( this.string_0, ( RequestParams )null ).ToString( );
                }
            }
        }

        private sealed class Class125
        {
            public SecurityId securityId_0;

            internal ExecutionMessage method_0( FastCsvReader fastCsvReader_0 )
            {
                fastCsvReader_0.Skip( 2 );
                DateTimeOffset dateTimeOffset = MfdHistorySource.smethod_2( fastCsvReader_0 );
                return new ExecutionMessage( )
                {
                    SecurityId = this.securityId_0,
                    ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick ),
                    ServerTime = dateTimeOffset,
                    TradePrice = new Decimal?( fastCsvReader_0.ReadDecimal( ) ),
                    TradeVolume = new Decimal?( fastCsvReader_0.ReadDecimal( ) )
                };
            }
        }
    }
}
