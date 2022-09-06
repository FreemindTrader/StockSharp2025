using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using Ecng.Web;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using xNet;

namespace StockSharp.Algo.History
{
    public class YahooHistorySource : BaseHistorySource, ISecurityDownloader
    {
        private readonly YahooHistorySource.Class154 class154_0 = new YahooHistorySource.Class154( );
        private static readonly CachedSynchronizedDictionary< TimeSpan, Tuple< int, string > > cachedSynchronizedDictionary_0;
        private readonly IExchangeInfoProvider iexchangeInfoProvider_0;

        public YahooHistorySource( IExchangeInfoProvider exchangeInfoProvider )
        {
            IExchangeInfoProvider exchangeInfoProvider1 = exchangeInfoProvider;
            if( exchangeInfoProvider1 == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            this.iexchangeInfoProvider_0 = exchangeInfoProvider1;
        }

        static YahooHistorySource( )
        {
            CachedSynchronizedDictionary< TimeSpan, Tuple< int, string > > synchronizedDictionary = new CachedSynchronizedDictionary< TimeSpan, Tuple< int, string > >( );
            synchronizedDictionary.Add( TimeSpan.FromMinutes( 1.0 ), Tuple.Create< int, string >( 1, "m" ) );
            synchronizedDictionary.Add( TimeSpan.FromMinutes( 5.0 ), Tuple.Create< int, string >( 5, "m" ) );
            synchronizedDictionary.Add( TimeSpan.FromMinutes( 15.0 ), Tuple.Create< int, string >( 15, "m" ) );
            synchronizedDictionary.Add( TimeSpan.FromHours( 1.0 ), Tuple.Create< int, string >( 1, "h" ) );
            synchronizedDictionary.Add( TimeSpan.FromDays( 1.0 ), Tuple.Create< int, string >( 1, "d" ) );
            synchronizedDictionary.Add( TimeSpan.FromDays( 7.0 ), Tuple.Create< int, string >( 1, "wk" ) );
            synchronizedDictionary.Add( 25920000000000L.To< TimeSpan >( ), Tuple.Create< int, string >( 1, "mo" ) );
            YahooHistorySource.cachedSynchronizedDictionary_0 = synchronizedDictionary;
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return this.iexchangeInfoProvider_0;
            }
        }

        public static IEnumerable< Level1Fields > Fields
        {
            get
            {
                return Enumerable.Empty< Level1Fields >( );
            }
        }

        public static IEnumerable< TimeSpan > TimeFrames
        {
            get
            {
                return cachedSynchronizedDictionary_0.CachedKeys;
            }
        }

        public IEnumerable< Level1ChangeMessage > GetLevel1(
      SecurityId securityId,
      DateTime beginDate,
      DateTime endDate,
      IEnumerable< Level1Fields > fields = null )
        {
            YahooHistorySource.Class152 class152 = new YahooHistorySource.Class152( );
            class152.yahooHistorySource_0 = this;
            class152.securityId_0 = securityId;
            class152.dateTime_0 = beginDate;
            class152.dateTime_1 = endDate;
            class152.hashSet_0 = new HashSet< Level1Fields >( );
            if( fields != null )
            {
                class152.hashSet_0.AddRange< Level1Fields >( fields );
            }

            if( class152.hashSet_0.Count == 0 )
            {
                class152.hashSet_0.AddRange< Level1Fields >( Ecng.Common.Enumerator.GetValues< Level1Fields >( ) );
            }

            class152.list_0 = new List< Level1ChangeMessage >( );
            CultureInfo.InvariantCulture.DoInCulture( new Action( class152.method_0 ) );
            return class152.list_0.OrderBy<Level1ChangeMessage, DateTimeOffset>( YahooHistorySource.Class163.func_0 ?? ( YahooHistorySource.Class163.func_0 = new Func<Level1ChangeMessage, DateTimeOffset>( YahooHistorySource.Class163.class163_0.method_0 ) ) ).ToArray<Level1ChangeMessage>();
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
      Security security,
      TimeSpan timeFrame,
      DateTime beginDate,
      DateTime endDate )
        {
            YahooHistorySource.Class164 class164 = new YahooHistorySource.Class164( );
            class164.yahooHistorySource_0 = this;
            class164.timeSpan_0 = timeFrame;
            class164.dateTime_0 = beginDate;
            class164.dateTime_1 = endDate;
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( class164.dateTime_0 > class164.dateTime_1 )
            {
                throw new ArgumentOutOfRangeException( nameof( beginDate ), LocalizedStrings.Str1119Params.Put( class164.dateTime_0, class164.dateTime_1 ) );
            }

            class164.securityId_0 = security.ToSecurityId( this.SecurityIdGenerator );
            return CultureInfo.InvariantCulture.DoInCulture<TimeFrameCandleMessage[ ]>( new Func<TimeFrameCandleMessage[ ]>( class164.method_0 ) );
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

            IEnumerable< string > strings;
            if( !criteria.Code.IsEmpty( ) )
            {
                strings =   ( new string[1]
               {
          criteria.Code
               } );
            }
            else
            {
                strings = Enumerable.Range( 65, 26 ).Select< int, string >( YahooHistorySource.Class163.func_1 ?? ( YahooHistorySource.Class163.func_1 = new Func< int, string >( YahooHistorySource.Class163.class163_0.method_1 ) ) );
            }

            foreach( string str1 in strings )
            {
                using( HttpRequest httpRequest = new HttpRequest( ) )
                {
                    object obj1 = JsonConvert.DeserializeObject( httpRequest.Get( "http://d.yimg.com/aq/autoc?query=" + str1 + "&region=US&lang=en-US", null ).ToString( ) );
                    if( YahooHistorySource.Class153.callSite_10 == null )
                    {
                        YahooHistorySource.Class153.callSite_10 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( YahooHistorySource ) ) );
                    }

                    Func< CallSite, object, IEnumerable > target1 = YahooHistorySource.Class153.callSite_10.Target;
                    CallSite< Func< CallSite, object, IEnumerable > > callSite10 = YahooHistorySource.Class153.callSite_10;
                    if( YahooHistorySource.Class153.callSite_1 == null )
                    {
                        YahooHistorySource.Class153.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "Result", typeof( YahooHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    Func< CallSite, object, object > target2 = YahooHistorySource.Class153.callSite_1.Target;
                    CallSite< Func< CallSite, object, object > > callSite1 = YahooHistorySource.Class153.callSite_1;
                    if( YahooHistorySource.Class153.callSite_0 == null )
                    {
                        YahooHistorySource.Class153.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "ResultSet", typeof( YahooHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    object obj2 = YahooHistorySource.Class153.callSite_0.Target( Class153.callSite_0, obj1 );
                    object obj3 = target2( callSite1, obj2 );
                    foreach( object obj4 in target1( callSite10, obj3 ) )
                    {
                        if( isCancelled( ) )
                        {
                            return;
                        }

                        if( YahooHistorySource.Class153.callSite_3 == null )
                        {
                            YahooHistorySource.Class153.callSite_3 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( YahooHistorySource ) ) );
                        }

                        Func< CallSite, object, string > target3 = YahooHistorySource.Class153.callSite_3.Target;
                        CallSite< Func< CallSite, object, string > > callSite3 = YahooHistorySource.Class153.callSite_3;
                        if( YahooHistorySource.Class153.callSite_2 == null )
                        {
                            YahooHistorySource.Class153.callSite_2 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "symbol", typeof( YahooHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        object obj5 = YahooHistorySource.Class153.callSite_2.Target( Class153.callSite_2, obj4 );
                        string str2 = target3( callSite3, obj5 );
                        if( YahooHistorySource.Class153.callSite_5 == null )
                        {
                            YahooHistorySource.Class153.callSite_5 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( YahooHistorySource ) ) );
                        }

                        Func< CallSite, object, string > target4 = YahooHistorySource.Class153.callSite_5.Target;
                        CallSite< Func< CallSite, object, string > > callSite5 = YahooHistorySource.Class153.callSite_5;
                        if( YahooHistorySource.Class153.callSite_4 == null )
                        {
                            YahooHistorySource.Class153.callSite_4 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "name", typeof( YahooHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        object obj6 = YahooHistorySource.Class153.callSite_4.Target( Class153.callSite_4, obj4 );
                        string str1_1 = target4( callSite5, obj6 );
                        if( YahooHistorySource.Class153.callSite_7 == null )
                        {
                            YahooHistorySource.Class153.callSite_7 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( YahooHistorySource ) ) );
                        }

                        Func< CallSite, object, string > target5 = YahooHistorySource.Class153.callSite_7.Target;
                        CallSite< Func< CallSite, object, string > > callSite7 = YahooHistorySource.Class153.callSite_7;
                        if( YahooHistorySource.Class153.callSite_6 == null )
                        {
                            YahooHistorySource.Class153.callSite_6 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "exch", typeof( YahooHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        object obj7 = YahooHistorySource.Class153.callSite_6.Target( Class153.callSite_6, obj4 );
                        string code = target5( callSite7, obj7 );
                        if( YahooHistorySource.Class153.callSite_9 == null )
                        {
                            YahooHistorySource.Class153.callSite_9 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( YahooHistorySource ) ) );
                        }

                        Func< CallSite, object, string > target6 = YahooHistorySource.Class153.callSite_9.Target;
                        CallSite< Func< CallSite, object, string > > callSite9 = YahooHistorySource.Class153.callSite_9;
                        if( YahooHistorySource.Class153.callSite_8 == null )
                        {
                            YahooHistorySource.Class153.callSite_8 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "type", typeof( YahooHistorySource ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        object obj8 = YahooHistorySource.Class153.callSite_8.Target( Class153.callSite_8, obj4 );
                        string str3 = target6( callSite9, obj8 );
                        ExchangeBoard board = this.ExchangeInfoProvider.GetOrCreateBoard( code, null );
                        string id = this.SecurityIdGenerator.GenerateId( str2, board );
                        if( securityStorage.LookupById( id ) == null )
                        {
                            SecurityTypes? nullable1 = new SecurityTypes?( );
                            if( !( str3 == "S" ) )
                            {
                                if( !( str3 == "E" ) )
                                {
                                    if( !( str3 == "I" ) )
                                    {
                                        if( !( str3 == "F" ) )
                                        {
                                            if( !( str3 == "O" ) )
                                            {
                                                if( str3 == "C" )
                                                {
                                                    nullable1 = new SecurityTypes?( SecurityTypes.Currency );
                                                }
                                            }
                                            else
                                            {
                                                nullable1 = new SecurityTypes?( SecurityTypes.Option );
                                            }
                                        }
                                        else
                                        {
                                            nullable1 = new SecurityTypes?( SecurityTypes.Future );
                                        }
                                    }
                                    else
                                    {
                                        nullable1 = new SecurityTypes?( SecurityTypes.Index );
                                    }
                                }
                                else
                                {
                                    nullable1 = new SecurityTypes?( SecurityTypes.Fund );
                                }
                            }
                            else
                            {
                                nullable1 = new SecurityTypes?( SecurityTypes.Stock );
                            }

                            if( criteria.Code.IsEmpty( ) || str2.ContainsIgnoreCase( criteria.Code ) || str1_1.ContainsIgnoreCase( criteria.Code ) )
                            {
                                if( criteria.Type.HasValue )
                                {
                                    SecurityTypes? type = criteria.Type;
                                    SecurityTypes? nullable2 = nullable1;
                                    if( !( type.GetValueOrDefault( ) == nullable2.GetValueOrDefault( ) & type.HasValue == nullable2.HasValue ) )
                                    {
                                        continue;
                                    }
                                }
                                Security security = new Security( )
                {
                  Id = id,
                  Code = str2,
                  Name = str1_1,
                  Board = board,
                  Type = nullable1
                };
                                securityStorage.Save( security, false );
                                newSecurity( security );
                            }
                        }
                    }
                }
            }
        }

        private sealed class Class152
        {
            public HashSet< Level1Fields > hashSet_0;
            public List< Level1ChangeMessage > list_0;
            public YahooHistorySource yahooHistorySource_0;
            public SecurityId securityId_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;

            internal void method_0( )
            {
                if( this.hashSet_0.Contains( Level1Fields.Dividend ) )
                {
                    this.list_0.AddRange( this.yahooHistorySource_0.class154_0.method_2( this.securityId_0, new DateTime?( this.dateTime_0 ), new DateTime?( this.dateTime_1 ) ) );
                }

                if( !this.hashSet_0.Contains( Level1Fields.AfterSplit ) && !this.hashSet_0.Contains( Level1Fields.BeforeSplit ) )
                {
                    return;
                }

                this.list_0.AddRange( this.yahooHistorySource_0.class154_0.method_3( this.securityId_0, new DateTime?( this.dateTime_0 ), new DateTime?( this.dateTime_1 ) ) );
            }
        }

        private static class Class153
        {
            public static CallSite< Func< CallSite, object, object > > callSite_0;
            public static CallSite< Func< CallSite, object, object > > callSite_1;
            public static CallSite< Func< CallSite, object, object > > callSite_2;
            public static CallSite< Func< CallSite, object, string > > callSite_3;
            public static CallSite< Func< CallSite, object, object > > callSite_4;
            public static CallSite< Func< CallSite, object, string > > callSite_5;
            public static CallSite< Func< CallSite, object, object > > callSite_6;
            public static CallSite< Func< CallSite, object, string > > callSite_7;
            public static CallSite< Func< CallSite, object, object > > callSite_8;
            public static CallSite< Func< CallSite, object, string > > callSite_9;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_10;
        }

        private sealed class Class154
        {
            private static readonly TimeZoneInfo timeZoneInfo_0 = TimeZoneInfo.GetSystemTimeZones( ).Single< TimeZoneInfo >( new Func< TimeZoneInfo, bool >( YahooHistorySource.Class154.Class159.class159_0.method_0 ) );
            private HttpRequest httpRequest_0;
            private string string_0;

            public IEnumerable< YahooHistorySource.Class161 > method_0(
        string[] string_1,
        string[] string_2 )
            {
                if( !Class2.smethod_0< string >( string_1 ) )
                {
                    throw new InvalidOperationException( LocalizedStrings.Str2292 );
                }

                string str1 = string_1.Duplicates( ).FirstOrDefault< string >( );
                if( str1 != null )
                {
                    throw new InvalidOperationException( LocalizedStrings.Str415Params.Put( str1 ) );
                }

                Url url = new Url( "https://query1.finance.yahoo.com/v7/finance/quote" );
                QueryString queryString = url.QueryString;
                queryString.Append( "symbols", string_1.Join( "," ) );
                if( Class2.smethod_0< string >( string_2 ) )
                {
                    string str2 = string_2.Duplicates( ).FirstOrDefault< string >( );
                    if( str2 != null )
                    {
                        throw new InvalidOperationException( LocalizedStrings.Str415Params.Put( str2 ) );
                    }

                    queryString.Append( "fields", string_2.Join( "," ) );
                }
                string str3;
                try
                {
                    using( HttpRequest httpRequest = new HttpRequest( ) )
                    {
                        str3 = httpRequest.Get( url, null ).ToString( );
                    }
                }
                catch( HttpException ex )
                {
                    if( ex.HttpStatusCode == HttpStatusCode.NotFound )
                    {
                        return Enumerable.Empty< YahooHistorySource.Class161 >( );
                    }

                    throw;
                }
                object obj1 = JsonConvert.DeserializeObject< object >( str3 );
                if( YahooHistorySource.Class154.Class156.callSite_0 == null )
                {
                    YahooHistorySource.Class154.Class156.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "quoteResponse", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj2 = YahooHistorySource.Class154.Class156.callSite_0.Target( Class156.callSite_0, obj1 );
                if( YahooHistorySource.Class154.Class156.callSite_1 == null )
                {
                    YahooHistorySource.Class154.Class156.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "error", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj3 = YahooHistorySource.Class154.Class156.callSite_1.Target( Class156.callSite_1, obj2 );
                if( YahooHistorySource.Class154.Class156.callSite_3 == null )
                {
                    YahooHistorySource.Class154.Class156.callSite_3 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                Func< CallSite, object, bool > target1 = YahooHistorySource.Class154.Class156.callSite_3.Target;
                CallSite< Func< CallSite, object, bool > > callSite3 = YahooHistorySource.Class154.Class156.callSite_3;
                if( YahooHistorySource.Class154.Class156.callSite_2 == null )
                {
                    YahooHistorySource.Class154.Class156.callSite_2 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.NotEqual, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                   } ) );
                }

                object obj4 = YahooHistorySource.Class154.Class156.callSite_2.Target( Class156.callSite_2, obj3, null );
                if( target1( callSite3, obj4 ) )
                {
                    throw new InvalidDataException( LocalizedStrings.Str2744 + string.Format( " {0}", obj3 ) );
                }

                List< YahooHistorySource.Class161 > class161List = new List< YahooHistorySource.Class161 >( );
                if( YahooHistorySource.Class154.Class156.callSite_5 == null )
                {
                    YahooHistorySource.Class154.Class156.callSite_5 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( YahooHistorySource.Class154 ) ) );
                }

                Func< CallSite, object, IEnumerable > target2 = YahooHistorySource.Class154.Class156.callSite_5.Target;
                CallSite< Func< CallSite, object, IEnumerable > > callSite5 = YahooHistorySource.Class154.Class156.callSite_5;
                if( YahooHistorySource.Class154.Class156.callSite_4 == null )
                {
                    YahooHistorySource.Class154.Class156.callSite_4 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "result", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj5 = YahooHistorySource.Class154.Class156.callSite_4.Target( Class156.callSite_4, obj2 );
                foreach( object obj6 in target2( callSite5, obj5 ) )
                {
                    if( YahooHistorySource.Class154.Class156.callSite_6 == null )
                    {
                        YahooHistorySource.Class154.Class156.callSite_6 = CallSite< Func< CallSite, object, JObject > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( JObject ), typeof( YahooHistorySource.Class154 ) ) );
                    }

                    JObject jobject_1 = YahooHistorySource.Class154.Class156.callSite_6.Target( Class156.callSite_6, obj6 );
                    class161List.Add( new YahooHistorySource.Class161( jobject_1 ) );
                }
                return class161List;
            }

            public IEnumerable< TimeFrameCandleMessage > method_1(
        SecurityId securityId_0,
        TimeSpan timeSpan_0,
        DateTime? nullable_0,
        DateTime? nullable_1 )
            {
                Tuple< int, string > tuple = YahooHistorySource.cachedSynchronizedDictionary_0.TryGetValue< TimeSpan, Tuple< int, string > >( timeSpan_0 );
                if( tuple == null )
                {
                    throw new ArgumentOutOfRangeException( "timeFrame", timeSpan_0, LocalizedStrings.Str2079 );
                }

                Dictionary< string, object > dictionary = new Dictionary< string, object >( )
        {
          {
            "interval",
               string.Format( "{0}{1}",    tuple.Item1,    tuple.Item2 )
          }
        };
                object obj1 = JsonConvert.DeserializeObject( this.method_5( securityId_0, nullable_0, nullable_1, "https://query1.finance.yahoo.com/v8/finance/chart/{0}", dictionary ) );
                if( YahooHistorySource.Class154.Class158.callSite_0 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "chart", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj2 = YahooHistorySource.Class154.Class158.callSite_0.Target( Class158.callSite_0, obj1 );
                object obj3;
                if( obj2 == null )
                {
                    obj3 = null;
                }
                else
                {
                    if( YahooHistorySource.Class154.Class158.callSite_2 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_2 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant,    null )
                       } ) );
                    }

                    Func< CallSite, object, int, object > target = YahooHistorySource.Class154.Class158.callSite_2.Target;
                    CallSite< Func< CallSite, object, int, object > > callSite2 = YahooHistorySource.Class154.Class158.callSite_2;
                    if( YahooHistorySource.Class154.Class158.callSite_1 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.ResultIndexed, "result", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    object obj4 = YahooHistorySource.Class154.Class158.callSite_1.Target( Class158.callSite_1, obj2 );
                    obj3 = target( callSite2, obj4, 0 );
                }
                object obj5 = obj3;
                if( YahooHistorySource.Class154.Class158.callSite_5 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_5 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                Func< CallSite, object, bool > target1 = YahooHistorySource.Class154.Class158.callSite_5.Target;
                CallSite< Func< CallSite, object, bool > > callSite5 = YahooHistorySource.Class154.Class158.callSite_5;
                if( YahooHistorySource.Class154.Class158.callSite_4 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_4 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                   } ) );
                }

                Func< CallSite, object, object, object > target2 = YahooHistorySource.Class154.Class158.callSite_4.Target;
                CallSite< Func< CallSite, object, object, object > > callSite4 = YahooHistorySource.Class154.Class158.callSite_4;
                object obj6 = obj5;
                object obj7;
                if( obj6 == null )
                {
                    obj7 = null;
                }
                else
                {
                    if( YahooHistorySource.Class154.Class158.callSite_3 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_3 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "timestamp", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    obj7 = YahooHistorySource.Class154.Class158.callSite_3.Target( Class158.callSite_3, obj6 );
                }
                object obj8 = target2( callSite4, obj7, null );
                if( target1( callSite5, obj8 ) )
                {
                    return Enumerable.Empty< TimeFrameCandleMessage >( );
                }

                if( YahooHistorySource.Class154.Class158.callSite_6 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_6 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "indicators", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj9 = YahooHistorySource.Class154.Class158.callSite_6.Target( Class158.callSite_6, obj5 );
                object obj10;
                if( obj9 == null )
                {
                    obj10 = null;
                }
                else
                {
                    if( YahooHistorySource.Class154.Class158.callSite_8 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_8 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant,    null )
                       } ) );
                    }

                    Func< CallSite, object, int, object > target3 = YahooHistorySource.Class154.Class158.callSite_8.Target;
                    CallSite< Func< CallSite, object, int, object > > callSite8 = YahooHistorySource.Class154.Class158.callSite_8;
                    if( YahooHistorySource.Class154.Class158.callSite_7 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_7 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.ResultIndexed, "quote", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    object obj4 = YahooHistorySource.Class154.Class158.callSite_7.Target( Class158.callSite_7, obj9 );
                    obj10 = target3( callSite8, obj4, 0 );
                }
                object obj11 = obj10;
                if( YahooHistorySource.Class154.Class158.callSite_10 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_10 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                Func< CallSite, object, bool > target4 = YahooHistorySource.Class154.Class158.callSite_10.Target;
                CallSite< Func< CallSite, object, bool > > callSite10 = YahooHistorySource.Class154.Class158.callSite_10;
                if( YahooHistorySource.Class154.Class158.callSite_9 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_9 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                   } ) );
                }

                object obj12 = YahooHistorySource.Class154.Class158.callSite_9.Target( Class158.callSite_9, obj11, null );
                if( target4( callSite10, obj12 ) )
                {
                    return Enumerable.Empty< TimeFrameCandleMessage >( );
                }

                if( YahooHistorySource.Class154.Class158.callSite_11 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_11 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "open", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj13 = YahooHistorySource.Class154.Class158.callSite_11.Target( Class158.callSite_11, obj11 );
                if( YahooHistorySource.Class154.Class158.callSite_12 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_12 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "high", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj14 = YahooHistorySource.Class154.Class158.callSite_12.Target( Class158.callSite_12, obj11 );
                if( YahooHistorySource.Class154.Class158.callSite_13 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_13 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "low", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj15 = YahooHistorySource.Class154.Class158.callSite_13.Target( Class158.callSite_13, obj11 );
                if( YahooHistorySource.Class154.Class158.callSite_14 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_14 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "close", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj16 = YahooHistorySource.Class154.Class158.callSite_14.Target( Class158.callSite_14, obj11 );
                if( YahooHistorySource.Class154.Class158.callSite_15 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_15 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "volume", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj17 = YahooHistorySource.Class154.Class158.callSite_15.Target( Class158.callSite_15, obj11 );
                List< TimeFrameCandleMessage > frameCandleMessageList1 = new List< TimeFrameCandleMessage >( );
                int num = 0;
                if( YahooHistorySource.Class154.Class158.callSite_41 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_41 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( YahooHistorySource.Class154 ) ) );
                }

                Func< CallSite, object, IEnumerable > target5 = YahooHistorySource.Class154.Class158.callSite_41.Target;
                CallSite< Func< CallSite, object, IEnumerable > > callSite41 = YahooHistorySource.Class154.Class158.callSite_41;
                if( YahooHistorySource.Class154.Class158.callSite_16 == null )
                {
                    YahooHistorySource.Class154.Class158.callSite_16 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "timestamp", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                   } ) );
                }

                object obj18 = YahooHistorySource.Class154.Class158.callSite_16.Target( Class158.callSite_16, obj5 );
                foreach( object obj4 in target5( callSite41, obj18 ) )
                {
                    if( YahooHistorySource.Class154.Class158.callSite_42 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_42 = CallSite< Func< CallSite, object, long > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( long ), typeof( YahooHistorySource.Class154 ) ) );
                    }

                    DateTime dateTime = YahooHistorySource.Class154.Class158.callSite_42.Target( Class158.callSite_42, obj4 ).FromUnix( true );
                    if( YahooHistorySource.Class154.Class158.callSite_17 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_17 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType,    null )
                       } ) );
                    }

                    object obj19 = YahooHistorySource.Class154.Class158.callSite_17.Target( Class158.callSite_17, obj13, num );
                    if( YahooHistorySource.Class154.Class158.callSite_18 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_18 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType,    null )
                       } ) );
                    }

                    object obj20 = YahooHistorySource.Class154.Class158.callSite_18.Target( Class158.callSite_18, obj14, num );
                    if( YahooHistorySource.Class154.Class158.callSite_19 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_19 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType,    null )
                       } ) );
                    }

                    object obj21 = YahooHistorySource.Class154.Class158.callSite_19.Target( Class158.callSite_19, obj15, num );
                    if( YahooHistorySource.Class154.Class158.callSite_20 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_20 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType,    null )
                       } ) );
                    }

                    object obj22 = YahooHistorySource.Class154.Class158.callSite_20.Target( Class158.callSite_20, obj16, num );
                    if( YahooHistorySource.Class154.Class158.callSite_21 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_21 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType,    null )
                       } ) );
                    }

                    object obj23 = YahooHistorySource.Class154.Class158.callSite_21.Target( Class158.callSite_21, obj17, num );
                    if( YahooHistorySource.Class154.Class158.callSite_22 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_22 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                       } ) );
                    }

                    object obj24 = YahooHistorySource.Class154.Class158.callSite_22.Target( Class158.callSite_22, obj19, null );
                    if( YahooHistorySource.Class154.Class158.callSite_25 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_25 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    object obj25;
                    if( !YahooHistorySource.Class154.Class158.callSite_25.Target( Class158.callSite_25, obj24 ) )
                    {
                        if( YahooHistorySource.Class154.Class158.callSite_24 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_24 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        Func< CallSite, object, object, object > target3 = YahooHistorySource.Class154.Class158.callSite_24.Target;
                        CallSite< Func< CallSite, object, object, object > > callSite24 = YahooHistorySource.Class154.Class158.callSite_24;
                        object obj26 = obj24;
                        if( YahooHistorySource.Class154.Class158.callSite_23 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_23 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                           } ) );
                        }

                        object obj27 = YahooHistorySource.Class154.Class158.callSite_23.Target( Class158.callSite_23, obj20, null );
                        obj25 = target3( callSite24, obj26, obj27 );
                    }
                    else
                    {
                        obj25 = obj24;
                    }

                    object obj28 = obj25;
                    if( YahooHistorySource.Class154.Class158.callSite_28 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_28 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    object obj29;
                    if( !YahooHistorySource.Class154.Class158.callSite_28.Target( Class158.callSite_28, obj28 ) )
                    {
                        if( YahooHistorySource.Class154.Class158.callSite_27 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_27 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        Func< CallSite, object, object, object > target3 = YahooHistorySource.Class154.Class158.callSite_27.Target;
                        CallSite< Func< CallSite, object, object, object > > callSite27 = YahooHistorySource.Class154.Class158.callSite_27;
                        object obj26 = obj28;
                        if( YahooHistorySource.Class154.Class158.callSite_26 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_26 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                           } ) );
                        }

                        object obj27 = YahooHistorySource.Class154.Class158.callSite_26.Target( Class158.callSite_26, obj21, null );
                        obj29 = target3( callSite27, obj26, obj27 );
                    }
                    else
                    {
                        obj29 = obj28;
                    }

                    object obj30 = obj29;
                    if( YahooHistorySource.Class154.Class158.callSite_31 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_31 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    object obj31;
                    if( !YahooHistorySource.Class154.Class158.callSite_31.Target( Class158.callSite_31, obj30 ) )
                    {
                        if( YahooHistorySource.Class154.Class158.callSite_30 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_30 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        Func< CallSite, object, object, object > target3 = YahooHistorySource.Class154.Class158.callSite_30.Target;
                        CallSite< Func< CallSite, object, object, object > > callSite30 = YahooHistorySource.Class154.Class158.callSite_30;
                        object obj26 = obj30;
                        if( YahooHistorySource.Class154.Class158.callSite_29 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_29 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                           } ) );
                        }

                        object obj27 = YahooHistorySource.Class154.Class158.callSite_29.Target( Class158.callSite_29, obj22, null );
                        obj31 = target3( callSite30, obj26, obj27 );
                    }
                    else
                    {
                        obj31 = obj30;
                    }

                    object obj32 = obj31;
                    if( YahooHistorySource.Class154.Class158.callSite_35 == null )
                    {
                        YahooHistorySource.Class154.Class158.callSite_35 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                       } ) );
                    }

                    if( !YahooHistorySource.Class154.Class158.callSite_35.Target( Class158.callSite_35, obj32 ) )
                    {
                        if( YahooHistorySource.Class154.Class158.callSite_34 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_34 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        Func< CallSite, object, bool > target3 = YahooHistorySource.Class154.Class158.callSite_34.Target;
                        CallSite< Func< CallSite, object, bool > > callSite34 = YahooHistorySource.Class154.Class158.callSite_34;
                        if( YahooHistorySource.Class154.Class158.callSite_33 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_33 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                           } ) );
                        }

                        Func< CallSite, object, object, object > target6 = YahooHistorySource.Class154.Class158.callSite_33.Target;
                        CallSite< Func< CallSite, object, object, object > > callSite33 = YahooHistorySource.Class154.Class158.callSite_33;
                        object obj26 = obj32;
                        if( YahooHistorySource.Class154.Class158.callSite_32 == null )
                        {
                            YahooHistorySource.Class154.Class158.callSite_32 = CallSite< Func< CallSite, object, object, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.Equal, typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.Constant,    null )
                           } ) );
                        }

                        object obj27 = YahooHistorySource.Class154.Class158.callSite_32.Target( Class158.callSite_32, obj23, null );
                        object obj33 = target6( callSite33, obj26, obj27 );
                        if( !target3( callSite34, obj33 ) )
                        {
                            List< TimeFrameCandleMessage > frameCandleMessageList2 = frameCandleMessageList1;
                            TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                            frameCandleMessage.SecurityId = securityId_0;
                            frameCandleMessage.Arg = timeSpan_0;
                            frameCandleMessage.OpenTime = ( DateTimeOffset ) dateTime;
                            if( YahooHistorySource.Class154.Class158.callSite_36 == null )
                            {
                                YahooHistorySource.Class154.Class158.callSite_36 = CallSite< Func< CallSite, object, Decimal > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal ), typeof( YahooHistorySource.Class154 ) ) );
                            }

                            frameCandleMessage.OpenPrice = YahooHistorySource.Class154.Class158.callSite_36.Target( Class158.callSite_36, obj19 );
                            if( YahooHistorySource.Class154.Class158.callSite_37 == null )
                            {
                                YahooHistorySource.Class154.Class158.callSite_37 = CallSite< Func< CallSite, object, Decimal > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal ), typeof( YahooHistorySource.Class154 ) ) );
                            }

                            frameCandleMessage.HighPrice = YahooHistorySource.Class154.Class158.callSite_37.Target( Class158.callSite_37, obj20 );
                            if( YahooHistorySource.Class154.Class158.callSite_38 == null )
                            {
                                YahooHistorySource.Class154.Class158.callSite_38 = CallSite< Func< CallSite, object, Decimal > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal ), typeof( YahooHistorySource.Class154 ) ) );
                            }

                            frameCandleMessage.LowPrice = YahooHistorySource.Class154.Class158.callSite_38.Target( Class158.callSite_38, obj21 );
                            if( YahooHistorySource.Class154.Class158.callSite_39 == null )
                            {
                                YahooHistorySource.Class154.Class158.callSite_39 = CallSite< Func< CallSite, object, Decimal > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal ), typeof( YahooHistorySource.Class154 ) ) );
                            }

                            frameCandleMessage.ClosePrice = YahooHistorySource.Class154.Class158.callSite_39.Target( Class158.callSite_39, obj22 );
                            if( YahooHistorySource.Class154.Class158.callSite_40 == null )
                            {
                                YahooHistorySource.Class154.Class158.callSite_40 = CallSite< Func< CallSite, object, Decimal > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal ), typeof( YahooHistorySource.Class154 ) ) );
                            }

                            frameCandleMessage.TotalVolume = YahooHistorySource.Class154.Class158.callSite_40.Target( Class158.callSite_40, obj23 );
                            frameCandleMessage.State = CandleStates.Finished;
                            frameCandleMessageList2.Add( frameCandleMessage );
              ++num;
                        }
                    }
                }
                return frameCandleMessageList1;
            }

            public IEnumerable< Level1ChangeMessage > method_2(
        SecurityId securityId_0,
        DateTime? nullable_0,
        DateTime? nullable_1 )
            {
                return this.method_4<Level1ChangeMessage>( securityId_0, nullable_0, nullable_1, "div", new Func<SecurityId, List<string>, Level1ChangeMessage>( YahooHistorySource.Class154.Class155.smethod_0 ) );
            }

            public IEnumerable< Level1ChangeMessage > method_3(
        SecurityId securityId_0,
        DateTime? nullable_0,
        DateTime? nullable_1 )
            {
                return this.method_4<Level1ChangeMessage>( securityId_0, nullable_0, nullable_1, "split", new Func<SecurityId, List<string>, Level1ChangeMessage>( YahooHistorySource.Class154.Class155.smethod_1 ) );
            }

            private List< TMessage > method_4< TMessage >(
        SecurityId securityId_0,
        DateTime? nullable_0,
        DateTime? nullable_1,
        string string_1,
        Func< SecurityId, List< string >, TMessage > func_0 )
        where TMessage : Message
            {
                if( func_0 == null )
                {
                    throw new ArgumentNullException( "parser" );
                }

                Dictionary< string, object > dictionary = new Dictionary< string, object >( )
        {
          {
            "interval",
               "1d"
          },
          {
            "events",
               string_1
          }
        };
                CsvFileReader csvFileReader1 = new CsvFileReader( new StringReader( this.method_5( securityId_0, nullable_0, nullable_1, "https://query1.finance.yahoo.com/v7/finance/download/{0}", dictionary ) ), EmptyLineBehavior.NoColumns );
                csvFileReader1.Delimiter = ',';
                using( CsvFileReader csvFileReader2 = csvFileReader1 )
                {
                    List< string > columns = new List< string >( );
                    csvFileReader2.ReadRow( columns );
                    List< TMessage > messageList = new List< TMessage >( );
                    while( csvFileReader2.ReadRow( columns ) )
                    {
                        TMessage message = func_0( securityId_0, columns );
                        if( message != null )
                        {
                            messageList.Add( message );
                        }
                    }
                    return messageList;
                }
            }

            private static DateTime? smethod_0( DateTime? nullable_0 )
            {
                if( nullable_0.HasValue )
                {
                    return new DateTime?( TimeZoneInfo.ConvertTimeToUtc( DateTime.SpecifyKind( nullable_0.Value, DateTimeKind.Unspecified ), YahooHistorySource.Class154.timeZoneInfo_0 ) );
                }

                return new DateTime?( );
            }

            private string method_5(
        SecurityId securityId_0,
        DateTime? nullable_0,
        DateTime? nullable_1,
        string string_1,
        IDictionary< string, object > idictionary_0 )
            {
                string str1 = securityId_0.SecurityCode;
                SecurityTypes? securityType = securityId_0.SecurityType;
                if( securityType.GetValueOrDefault( ) == SecurityTypes.Future & securityType.HasValue )
                {
                    str1 = str1 + "." + securityId_0.BoardCode;
                }

                for( int index = 0; index < 5; ++index )
                {
                    try
                    {
                        if( this.httpRequest_0 == null )
                        {
                            this.httpRequest_0 = YahooHistorySource.Class154.smethod_2( );
                            this.string_0 = this.httpRequest_0.Get( "https://query1.finance.yahoo.com/v1/test/getcrumb", null ).ToString( );
                        }
                        DateTime? nullable = YahooHistorySource.Class154.smethod_0( nullable_0 );
                        nullable_0 = new DateTime?( nullable ?? TimeHelper.GregorianStart );
                        nullable = YahooHistorySource.Class154.smethod_0( nullable_1 );
                        nullable_1 = new DateTime?( nullable ?? DateTime.UtcNow );
                        Url url = new Url( string_1.Put( str1 ) );
                        QueryString queryString = url.QueryString;
                        queryString.Append( "period1", YahooHistorySource.Class154.smethod_1( nullable_0.Value ) ).Append( "period2", YahooHistorySource.Class154.smethod_1( nullable_1.Value ) );
                        foreach( KeyValuePair< string, object > keyValuePair in ( IEnumerable< KeyValuePair< string, object > > ) idictionary_0 )
                        {
                            queryString.Append( keyValuePair.Key, keyValuePair.Value );
                        }

                        queryString.Append( "crumb", string_0 );
                        return this.httpRequest_0.Get( url, null ).ToString( );
                    }
                    catch( HttpException ex )
                    {
                        if( ex.HttpStatusCode == HttpStatusCode.Unauthorized )
                        {
                            if( this.httpRequest_0 != null )
                            {
                                throw;
                            }
                            else
                            {
                                this.httpRequest_0 = null;
                            }
                        }
                        else
                        {
                            string str2 = this.httpRequest_0.Response.ToString( );
                            if( !str2.IsEmpty( ) )
                            {
                                string str3 = null;
                                try
                                {
                                    object obj1 = JsonConvert.DeserializeObject( str2 );
                                    if( YahooHistorySource.Class154.Class157.callSite_3 == null )
                                    {
                                        YahooHistorySource.Class154.Class157.callSite_3 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( YahooHistorySource.Class154 ) ) );
                                    }

                                    Func< CallSite, object, string > target = YahooHistorySource.Class154.Class157.callSite_3.Target;
                                    CallSite< Func< CallSite, object, string > > callSite3 = YahooHistorySource.Class154.Class157.callSite_3;
                                    if( YahooHistorySource.Class154.Class157.callSite_0 == null )
                                    {
                                        YahooHistorySource.Class154.Class157.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "chart", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                                       {
                      CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                                       } ) );
                                    }

                                    object obj2 = YahooHistorySource.Class154.Class157.callSite_0.Target( Class157.callSite_0, obj1 );
                                    object obj3;
                                    if( obj2 == null )
                                    {
                                        obj3 = null;
                                    }
                                    else
                                    {
                                        if( YahooHistorySource.Class154.Class157.callSite_1 == null )
                                        {
                                            YahooHistorySource.Class154.Class157.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "error", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                                           {
                        CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                                           } ) );
                                        }

                                        object obj4 = YahooHistorySource.Class154.Class157.callSite_1.Target( Class157.callSite_1, obj2 );
                                        if( obj4 == null )
                                        {
                                            obj3 = null;
                                        }
                                        else
                                        {
                                            if( YahooHistorySource.Class154.Class157.callSite_2 == null )
                                            {
                                                YahooHistorySource.Class154.Class157.callSite_2 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "description", typeof( YahooHistorySource.Class154 ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                                               {
                          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None,    null )
                                               } ) );
                                            }

                                            obj3 = YahooHistorySource.Class154.Class157.callSite_2.Target( Class157.callSite_2, obj4 );
                                        }
                                    }
                                    str3 = target( callSite3, obj3 );
                                }
                                catch
                                {
                                }
                                if( !str3.IsEmpty( ) )
                                {
                                    throw new InvalidOperationException( str3, ex );
                                }
                            }
                            throw;
                        }
                    }
                }
                throw new InvalidOperationException( LocalizedStrings.Str2959 );
            }

            internal static string smethod_1( DateTime dateTime_0 )
            {
                return dateTime_0.ToUnix( true ).ToString( "F0" );
            }

            internal static HttpRequest smethod_2( )
            {
                for( int index = 0; index < 5; ++index )
                {
                    HttpRequest httpRequest = new HttpRequest( )
          {
            UserAgent = Http.ChromeUserAgent( ),
            Cookies = new CookieDictionary( false )
          };
                    httpRequest.Get( "https://finance.yahoo.com?" + Guid.NewGuid( ).ToString( ).Substring( 0, 8 ), null );
                    CookieDictionary cookies = httpRequest.Cookies;
                    // ISSUE: explicit non-virtual call
                    if( ( cookies != null ? ( ( cookies.Count ) > 0 ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return httpRequest;
                    }

                    TimeSpan.FromMilliseconds( 100.0 ).Sleep( );
                }
                throw new InvalidOperationException( LocalizedStrings.Str1561 );
            }

            private static class Class155
            {
                public static Level1ChangeMessage smethod_0(
          SecurityId securityId_0,
          List< string > list_0 )
                {
                    Decimal num = YahooHistorySource.Class154.Class155.smethod_3( list_0[ 1 ] );
                    if( num == Decimal.Zero )
                    {
                        return null;
                    }

                    Level1ChangeMessage message = new Level1ChangeMessage( );
                    message.SecurityId = securityId_0;
                    message.ServerTime = ( DateTimeOffset ) YahooHistorySource.Class154.Class155.smethod_2( list_0[ 0 ] );
                    return message.Add< Level1ChangeMessage, Level1Fields >( Level1Fields.Dividend, num );
                }

                public static Level1ChangeMessage smethod_1(
          SecurityId securityId_0,
          List< string > list_0 )
                {
                    string[] strArray = list_0[ 1 ].Split( '/' );
                    Decimal num1 = new Decimal( );
                    Decimal num2 = new Decimal( );
                    if( strArray.Length == 2 )
                    {
                        num1 = YahooHistorySource.Class154.Class155.smethod_3( strArray[ 0 ] );
                        num2 = YahooHistorySource.Class154.Class155.smethod_3( strArray[ 1 ] );
                    }
                    if( num1 == Decimal.Zero && num2 == Decimal.Zero )
                    {
                        return null;
                    }

                    Level1ChangeMessage message = new Level1ChangeMessage( );
                    message.SecurityId = securityId_0;
                    message.ServerTime = ( DateTimeOffset ) YahooHistorySource.Class154.Class155.smethod_2( list_0[ 0 ] );
                    return message.Add< Level1ChangeMessage, Level1Fields >( Level1Fields.AfterSplit, num1 ).Add< Level1ChangeMessage, Level1Fields >( Level1Fields.BeforeSplit, num2 );
                }

                private static DateTime smethod_2( string string_0 )
                {
                    DateTime result;
                    if( !DateTime.TryParse( string_0, CultureInfo.InvariantCulture, DateTimeStyles.None, out result ) )
                    {
                        throw new InvalidOperationException( "Could not convert '" + string_0 + "' to DateTime." );
                    }

                    return result;
                }

                private static Decimal smethod_3( string string_0 )
                {
                    Decimal result;
                    Decimal.TryParse( string_0, NumberStyles.Any, CultureInfo.InvariantCulture, out result );
                    return result;
                }

                private static long smethod_4( string string_0 )
                {
                    long result;
                    long.TryParse( string_0, NumberStyles.Any, CultureInfo.InvariantCulture, out result );
                    return result;
                }
            }

            private static class Class156
            {
                public static CallSite< Func< CallSite, object, object > > callSite_0;
                public static CallSite< Func< CallSite, object, object > > callSite_1;
                public static CallSite< Func< CallSite, object, object, object > > callSite_2;
                public static CallSite< Func< CallSite, object, bool > > callSite_3;
                public static CallSite< Func< CallSite, object, object > > callSite_4;
                public static CallSite< Func< CallSite, object, IEnumerable > > callSite_5;
                public static CallSite< Func< CallSite, object, JObject > > callSite_6;
            }

            private static class Class157
            {
                public static CallSite< Func< CallSite, object, object > > callSite_0;
                public static CallSite< Func< CallSite, object, object > > callSite_1;
                public static CallSite< Func< CallSite, object, object > > callSite_2;
                public static CallSite< Func< CallSite, object, string > > callSite_3;
            }

            private static class Class158
            {
                public static CallSite< Func< CallSite, object, object > > callSite_0;
                public static CallSite< Func< CallSite, object, object > > callSite_1;
                public static CallSite< Func< CallSite, object, int, object > > callSite_2;
                public static CallSite< Func< CallSite, object, object > > callSite_3;
                public static CallSite< Func< CallSite, object, object, object > > callSite_4;
                public static CallSite< Func< CallSite, object, bool > > callSite_5;
                public static CallSite< Func< CallSite, object, object > > callSite_6;
                public static CallSite< Func< CallSite, object, object > > callSite_7;
                public static CallSite< Func< CallSite, object, int, object > > callSite_8;
                public static CallSite< Func< CallSite, object, object, object > > callSite_9;
                public static CallSite< Func< CallSite, object, bool > > callSite_10;
                public static CallSite< Func< CallSite, object, object > > callSite_11;
                public static CallSite< Func< CallSite, object, object > > callSite_12;
                public static CallSite< Func< CallSite, object, object > > callSite_13;
                public static CallSite< Func< CallSite, object, object > > callSite_14;
                public static CallSite< Func< CallSite, object, object > > callSite_15;
                public static CallSite< Func< CallSite, object, object > > callSite_16;
                public static CallSite< Func< CallSite, object, int, object > > callSite_17;
                public static CallSite< Func< CallSite, object, int, object > > callSite_18;
                public static CallSite< Func< CallSite, object, int, object > > callSite_19;
                public static CallSite< Func< CallSite, object, int, object > > callSite_20;
                public static CallSite< Func< CallSite, object, int, object > > callSite_21;
                public static CallSite< Func< CallSite, object, object, object > > callSite_22;
                public static CallSite< Func< CallSite, object, object, object > > callSite_23;
                public static CallSite< Func< CallSite, object, object, object > > callSite_24;
                public static CallSite< Func< CallSite, object, bool > > callSite_25;
                public static CallSite< Func< CallSite, object, object, object > > callSite_26;
                public static CallSite< Func< CallSite, object, object, object > > callSite_27;
                public static CallSite< Func< CallSite, object, bool > > callSite_28;
                public static CallSite< Func< CallSite, object, object, object > > callSite_29;
                public static CallSite< Func< CallSite, object, object, object > > callSite_30;
                public static CallSite< Func< CallSite, object, bool > > callSite_31;
                public static CallSite< Func< CallSite, object, object, object > > callSite_32;
                public static CallSite< Func< CallSite, object, object, object > > callSite_33;
                public static CallSite< Func< CallSite, object, bool > > callSite_34;
                public static CallSite< Func< CallSite, object, bool > > callSite_35;
                public static CallSite< Func< CallSite, object, Decimal > > callSite_36;
                public static CallSite< Func< CallSite, object, Decimal > > callSite_37;
                public static CallSite< Func< CallSite, object, Decimal > > callSite_38;
                public static CallSite< Func< CallSite, object, Decimal > > callSite_39;
                public static CallSite< Func< CallSite, object, Decimal > > callSite_40;
                public static CallSite< Func< CallSite, object, IEnumerable > > callSite_41;
                public static CallSite< Func< CallSite, object, long > > callSite_42;
            }

            [Serializable]
      private sealed class Class159
      {
          public static readonly YahooHistorySource.Class154.Class159 class159_0 = new YahooHistorySource.Class154.Class159( );

          internal bool method_0( TimeZoneInfo timeZoneInfo_0 )
          {
              if( !( timeZoneInfo_0.Id == "Eastern Standard Time" ) )
              {
                  return timeZoneInfo_0.Id == "America/New_York";
              }

              return true;
          }
      }
        }

        private static class Class160
        {
        }

        private sealed class Class161
        {
            private readonly JObject jobject_0;

            public Class161( JObject jobject_1 )
            {
                JObject jobject = jobject_1;
                if( jobject == null )
                {
                    throw new ArgumentNullException( "obj" );
                }

                this.jobject_0 = jobject;
            }

            public object method_0( string string_0 )
            {
                return this.jobject_0[string_0];
            }
        }

        private static class Class162
        {
        }

        [Serializable]
    private sealed class Class163
    {
        public static readonly YahooHistorySource.Class163 class163_0 = new YahooHistorySource.Class163( );
        public static Func< Level1ChangeMessage, DateTimeOffset > func_0;
        public static Func< int, string > func_1;

        internal DateTimeOffset method_0( Level1ChangeMessage level1ChangeMessage_0 )
        {
            return level1ChangeMessage_0.ServerTime;
        }

        internal string method_1( int int_0 )
        {
            return ( ( char ) int_0 ).To< string >( );
        }
    }

        private sealed class Class164
        {
            public YahooHistorySource yahooHistorySource_0;
            public SecurityId securityId_0;
            public TimeSpan timeSpan_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public Func< TimeFrameCandleMessage, TimeFrameCandleMessage > func_0;

            internal TimeFrameCandleMessage[] method_0( )
            {
                return this.yahooHistorySource_0.class154_0.method_1( this.securityId_0, this.timeSpan_0, new DateTime?( this.dateTime_0 ), new DateTime?( this.dateTime_1 ) ).Select< TimeFrameCandleMessage, TimeFrameCandleMessage >( this.func_0 ?? ( this.func_0 = new Func< TimeFrameCandleMessage, TimeFrameCandleMessage >( this.method_1 ) ) ).ToArray< TimeFrameCandleMessage >( );
            }

            internal TimeFrameCandleMessage method_1(
        TimeFrameCandleMessage timeFrameCandleMessage_0 )
            {
                timeFrameCandleMessage_0.TimeFrame = this.timeSpan_0;
                timeFrameCandleMessage_0.State = CandleStates.Finished;
                return timeFrameCandleMessage_0;
            }
        }
    }
}
