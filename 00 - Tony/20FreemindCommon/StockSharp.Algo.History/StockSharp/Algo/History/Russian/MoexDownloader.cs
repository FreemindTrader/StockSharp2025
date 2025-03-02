using Ecng.Collections;
using Ecng.Common;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;


#pragma warning disable 649

namespace StockSharp.Algo.History.Russian
{
    public static class MoexDownloader
    {
        private static readonly HashSet< string > hashSet_0 = new HashSet< string >( ( IEqualityComparer< string > )StringComparer.InvariantCultureIgnoreCase );

        static MoexDownloader( )
        {
            MoexDownloader.hashSet_0.AddRange< string >( ( IEnumerable< string > )Class38.smethod_4( ).Split( ) );
        }

        private static void smethod_0( )
        {
            Tuple< string, string >[ ] tupleArray = new Tuple< string, string >[ 6 ]
            {
        Tuple.Create< string, string >( "stock", "shares" ),
        Tuple.Create< string, string >( "stock", "index" ),
        Tuple.Create< string, string >( "stock", "bonds" ),
        Tuple.Create< string, string >( "futures", "forts" ),
        Tuple.Create< string, string >( "commodity", "futures" ),
        Tuple.Create< string, string >( "currency", "futures" )
            };
            foreach( Tuple< string, string > tuple in tupleArray )
            {
                object obj1 = JsonConvert.DeserializeObject( MoexDownloader.smethod_3( "https://iss.moex.com/iss/engines/" + tuple.Item1 + "/markets/" + tuple.Item2 + "/securities.json" ) );
                int num1 = -1;
                int num2 = -1;
                int num3 = -1;
                int num4 = -1;
                int num5 = -1;
                int num6 = -1;
                int num7 = -1;
                int num8 = -1;
                int num9 = -1;
                int num10 = -1;
                int num11 = -1;
                int num12 = -1;
                int num13 = -1;
                int num14 = -1;
                int num15 = 0;
                if( MoexDownloader.Class128.callSite_2 == null )
                {
                    MoexDownloader.Class128.callSite_2 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( MoexDownloader ) ) );
                }

                Func< CallSite, object, IEnumerable > target1 = MoexDownloader.Class128.callSite_2.Target;
                CallSite< Func< CallSite, object, IEnumerable > > callSite2 = MoexDownloader.Class128.callSite_2;
                if( MoexDownloader.Class128.callSite_1 == null )
                {
                    MoexDownloader.Class128.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "columns", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                Func< CallSite, object, object > target2 = MoexDownloader.Class128.callSite_1.Target;
                CallSite< Func< CallSite, object, object > > callSite1 = MoexDownloader.Class128.callSite_1;
                if( MoexDownloader.Class128.callSite_0 == null )
                {
                    MoexDownloader.Class128.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "securities", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                object obj2 = MoexDownloader.Class128.callSite_0.Target( ( CallSite )MoexDownloader.Class128.callSite_0, obj1 );
                object obj3 = target2( ( CallSite )callSite1, obj2 );
                foreach( object obj4 in target1( ( CallSite )callSite2, obj3 ) )
                {
                    if( MoexDownloader.Class128.callSite_3 == null )
                    {
                        MoexDownloader.Class128.callSite_3 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                    }

                    string string_0 = MoexDownloader.Class128.callSite_3.Target( ( CallSite )MoexDownloader.Class128.callSite_3, obj4 );
                    //switch ( Class24.smethod_0( string_0 ) )
                    //{
                    //    case 207426305:
                    //        if ( string_0 == "LASTTRADEDATE" )
                    //        {
                    //            num11 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 396207230:
                    //        if ( string_0 == "SETTLEDATE" )
                    //        {
                    //            num12 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 418093446:
                    //        if ( string_0 == "BOARDID" )
                    //        {
                    //            num2 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 449831473:
                    //        if ( string_0 == "ISSUESIZE" )
                    //        {
                    //            num7 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 460349364:
                    //        if ( string_0 == "ISSUEDATE" )
                    //        {
                    //            num8 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 686308805:
                    //        if ( string_0 == "MINSTEP" )
                    //        {
                    //            num9 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 1217376863:
                    //        if ( string_0 == "SECID" )
                    //        {
                    //            num1 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 2678427033:
                    //        if ( string_0 == "SECNAME" )
                    //        {
                    //            num4 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 3017670126:
                    //        if ( string_0 == "ISIN" )
                    //        {
                    //            num6 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 3171000234:
                    //        if ( string_0 == "LOTVOLUME" )
                    //        {
                    //            num5 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 3307378946:
                    //        if ( string_0 == "ASSETCODE" )
                    //        {
                    //            num13 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 3552844363:
                    //        if ( string_0 == "CURRENCYID" )
                    //        {
                    //            num14 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 3579029398:
                    //        if ( string_0 == "SHORTNAME" )
                    //        {
                    //            num3 = num15;
                    //            break;
                    //        }
                    //        break;
                    //    case 3949999597:
                    //        if ( string_0 == "DECIMALS" )
                    //        {
                    //            num10 = num15;
                    //            break;
                    //        }
                    //        break;
                    //}
                    ++num15;
                }
                if( MoexDownloader.Class128.callSite_34 == null )
                {
                    MoexDownloader.Class128.callSite_34 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( MoexDownloader ) ) );
                }

                Func< CallSite, object, IEnumerable > target3 = MoexDownloader.Class128.callSite_34.Target;
                CallSite< Func< CallSite, object, IEnumerable > > callSite34 = MoexDownloader.Class128.callSite_34;
                if( MoexDownloader.Class128.callSite_5 == null )
                {
                    MoexDownloader.Class128.callSite_5 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "data", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                Func< CallSite, object, object > target4 = MoexDownloader.Class128.callSite_5.Target;
                CallSite< Func< CallSite, object, object > > callSite5 = MoexDownloader.Class128.callSite_5;
                if( MoexDownloader.Class128.callSite_4 == null )
                {
                    MoexDownloader.Class128.callSite_4 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "securities", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                object obj5 = MoexDownloader.Class128.callSite_4.Target( ( CallSite )MoexDownloader.Class128.callSite_4, obj1 );
                object obj6 = target4( ( CallSite )callSite5, obj5 );
                foreach( object obj4 in target3( ( CallSite )callSite34, obj6 ) )
                {
                    MoexDownloader.Class126 class126_1 = new MoexDownloader.Class126( );
                    if( MoexDownloader.Class128.callSite_7 == null )
                    {
                        MoexDownloader.Class128.callSite_7 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                    }

                    Func< CallSite, object, string > target5 = MoexDownloader.Class128.callSite_7.Target;
                    CallSite< Func< CallSite, object, string > > callSite7 = MoexDownloader.Class128.callSite_7;
                    if( MoexDownloader.Class128.callSite_6 == null )
                    {
                        MoexDownloader.Class128.callSite_6 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                       } ) );
                    }

                    object obj7 = MoexDownloader.Class128.callSite_6.Target( ( CallSite )MoexDownloader.Class128.callSite_6, obj4, num1 );
                    string string_0 = target5( ( CallSite )callSite7, obj7 );
                    if( MoexDownloader.Class128.callSite_9 == null )
                    {
                        MoexDownloader.Class128.callSite_9 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                    }

                    Func< CallSite, object, string > target6 = MoexDownloader.Class128.callSite_9.Target;
                    CallSite< Func< CallSite, object, string > > callSite9 = MoexDownloader.Class128.callSite_9;
                    if( MoexDownloader.Class128.callSite_8 == null )
                    {
                        MoexDownloader.Class128.callSite_8 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                       } ) );
                    }

                    object obj8 = MoexDownloader.Class128.callSite_8.Target( ( CallSite )MoexDownloader.Class128.callSite_8, obj4, num2 );
                    string string_1 = target6( ( CallSite )callSite9, obj8 );
                    MoexDownloader.Class126 class126_2 = class126_1;
                    if( MoexDownloader.Class128.callSite_11 == null )
                    {
                        MoexDownloader.Class128.callSite_11 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                    }

                    Func< CallSite, object, string > target7 = MoexDownloader.Class128.callSite_11.Target;
                    CallSite< Func< CallSite, object, string > > callSite11 = MoexDownloader.Class128.callSite_11;
                    if( MoexDownloader.Class128.callSite_10 == null )
                    {
                        MoexDownloader.Class128.callSite_10 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                       {
              typeof( string )
                       }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                       } ) );
                    }

                    object obj9 = MoexDownloader.Class128.callSite_10.Target( ( CallSite )MoexDownloader.Class128.callSite_10, typeof( MoexDownloader ), obj4, num3 );
                    string str1 = target7( ( CallSite )callSite11, obj9 );
                    class126_2.string_0 = str1;
                    SecurityInfo[ ] securityInfoArray = Extension.smethod_1( string_0, string_1 );
                    if( securityInfoArray == null || securityInfoArray.Length == 0 || !( ( IEnumerable< SecurityInfo > )securityInfoArray ).Any< SecurityInfo >( new Func< SecurityInfo, bool >( class126_1.method_0 ) ) )
                    {
                        string str2 = tuple.Item2;
                        if( str2 == "forts" )
                        {
                            str2 = "futures";
                        }

                        SecurityInfo securityInfo_0 = new SecurityInfo( );
                        securityInfo_0.Code = string_0;
                        securityInfo_0.Board = string_1;
                        securityInfo_0.Type = str2;
                        securityInfo_0.ShortName = class126_1.string_0;
                        SecurityInfo securityInfo1 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_13 == null )
                        {
                            MoexDownloader.Class128.callSite_13 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target8 = MoexDownloader.Class128.callSite_13.Target;
                        CallSite< Func< CallSite, object, string > > callSite13 = MoexDownloader.Class128.callSite_13;
                        if( MoexDownloader.Class128.callSite_12 == null )
                        {
                            MoexDownloader.Class128.callSite_12 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj10 = MoexDownloader.Class128.callSite_12.Target( ( CallSite )MoexDownloader.Class128.callSite_12, typeof( MoexDownloader ), obj4, num4 );
                        string str3 = target8( ( CallSite )callSite13, obj10 );
                        securityInfo1.Name = str3;
                        SecurityInfo securityInfo2 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_15 == null )
                        {
                            MoexDownloader.Class128.callSite_15 = CallSite< Func< CallSite, object, int? > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( int? ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, int? > target9 = MoexDownloader.Class128.callSite_15.Target;
                        CallSite< Func< CallSite, object, int? > > callSite15 = MoexDownloader.Class128.callSite_15;
                        if( MoexDownloader.Class128.callSite_14 == null )
                        {
                            MoexDownloader.Class128.callSite_14 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( int? )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj11 = MoexDownloader.Class128.callSite_14.Target( ( CallSite )MoexDownloader.Class128.callSite_14, typeof( MoexDownloader ), obj4, num10 );
                        int? nullable1 = target9( ( CallSite )callSite15, obj11 );
                        securityInfo2.Decimals = nullable1;
                        SecurityInfo securityInfo3 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_17 == null )
                        {
                            MoexDownloader.Class128.callSite_17 = CallSite< Func< CallSite, object, Decimal? > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal? ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, Decimal? > target10 = MoexDownloader.Class128.callSite_17.Target;
                        CallSite< Func< CallSite, object, Decimal? > > callSite17 = MoexDownloader.Class128.callSite_17;
                        if( MoexDownloader.Class128.callSite_16 == null )
                        {
                            MoexDownloader.Class128.callSite_16 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( Decimal? )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj12 = MoexDownloader.Class128.callSite_16.Target( ( CallSite )MoexDownloader.Class128.callSite_16, typeof( MoexDownloader ), obj4, num9 );
                        Decimal? nullable2 = target10( ( CallSite )callSite17, obj12 );
                        securityInfo3.PriceStep = nullable2;
                        SecurityInfo securityInfo4 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_19 == null )
                        {
                            MoexDownloader.Class128.callSite_19 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target11 = MoexDownloader.Class128.callSite_19.Target;
                        CallSite< Func< CallSite, object, string > > callSite19 = MoexDownloader.Class128.callSite_19;
                        if( MoexDownloader.Class128.callSite_18 == null )
                        {
                            MoexDownloader.Class128.callSite_18 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj13 = MoexDownloader.Class128.callSite_18.Target( ( CallSite )MoexDownloader.Class128.callSite_18, typeof( MoexDownloader ), obj4, num11 );
                        DateTime? dateTime1 = target11( ( CallSite )callSite19, obj13 ).TryToDateTime( "yyyy-MM-dd", CultureInfo.InvariantCulture );
                        securityInfo4.LastDate = dateTime1;
                        SecurityInfo securityInfo5 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_21 == null )
                        {
                            MoexDownloader.Class128.callSite_21 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target12 = MoexDownloader.Class128.callSite_21.Target;
                        CallSite< Func< CallSite, object, string > > callSite21 = MoexDownloader.Class128.callSite_21;
                        if( MoexDownloader.Class128.callSite_20 == null )
                        {
                            MoexDownloader.Class128.callSite_20 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj14 = MoexDownloader.Class128.callSite_20.Target( ( CallSite )MoexDownloader.Class128.callSite_20, typeof( MoexDownloader ), obj4, num13 );
                        string str4 = target12( ( CallSite )callSite21, obj14 );
                        securityInfo5.Asset = str4;
                        SecurityInfo securityInfo6 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_23 == null )
                        {
                            MoexDownloader.Class128.callSite_23 = CallSite< Func< CallSite, object, Decimal? > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal? ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, Decimal? > target13 = MoexDownloader.Class128.callSite_23.Target;
                        CallSite< Func< CallSite, object, Decimal? > > callSite23 = MoexDownloader.Class128.callSite_23;
                        if( MoexDownloader.Class128.callSite_22 == null )
                        {
                            MoexDownloader.Class128.callSite_22 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( Decimal? )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj15 = MoexDownloader.Class128.callSite_22.Target( ( CallSite )MoexDownloader.Class128.callSite_22, typeof( MoexDownloader ), obj4, num5 );
                        Decimal? nullable3 = target13( ( CallSite )callSite23, obj15 );
                        securityInfo6.Multiplier = nullable3;
                        SecurityInfo securityInfo7 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_25 == null )
                        {
                            MoexDownloader.Class128.callSite_25 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target14 = MoexDownloader.Class128.callSite_25.Target;
                        CallSite< Func< CallSite, object, string > > callSite25 = MoexDownloader.Class128.callSite_25;
                        if( MoexDownloader.Class128.callSite_24 == null )
                        {
                            MoexDownloader.Class128.callSite_24 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj16 = MoexDownloader.Class128.callSite_24.Target( ( CallSite )MoexDownloader.Class128.callSite_24, typeof( MoexDownloader ), obj4, num14 );
                        string str5 = target14( ( CallSite )callSite25, obj16 );
                        securityInfo7.Currency = str5;
                        SecurityInfo securityInfo8 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_27 == null )
                        {
                            MoexDownloader.Class128.callSite_27 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target15 = MoexDownloader.Class128.callSite_27.Target;
                        CallSite< Func< CallSite, object, string > > callSite27 = MoexDownloader.Class128.callSite_27;
                        if( MoexDownloader.Class128.callSite_26 == null )
                        {
                            MoexDownloader.Class128.callSite_26 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj17 = MoexDownloader.Class128.callSite_26.Target( ( CallSite )MoexDownloader.Class128.callSite_26, typeof( MoexDownloader ), obj4, num6 );
                        string str6 = target15( ( CallSite )callSite27, obj17 );
                        securityInfo8.Isin = str6;
                        SecurityInfo securityInfo9 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_29 == null )
                        {
                            MoexDownloader.Class128.callSite_29 = CallSite< Func< CallSite, object, Decimal? > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( Decimal? ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, Decimal? > target16 = MoexDownloader.Class128.callSite_29.Target;
                        CallSite< Func< CallSite, object, Decimal? > > callSite29 = MoexDownloader.Class128.callSite_29;
                        if( MoexDownloader.Class128.callSite_28 == null )
                        {
                            MoexDownloader.Class128.callSite_28 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( Decimal? )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj18 = MoexDownloader.Class128.callSite_28.Target( ( CallSite )MoexDownloader.Class128.callSite_28, typeof( MoexDownloader ), obj4, num7 );
                        Decimal? nullable4 = target16( ( CallSite )callSite29, obj18 );
                        securityInfo9.IssueSize = nullable4;
                        SecurityInfo securityInfo10 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_31 == null )
                        {
                            MoexDownloader.Class128.callSite_31 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target17 = MoexDownloader.Class128.callSite_31.Target;
                        CallSite< Func< CallSite, object, string > > callSite31 = MoexDownloader.Class128.callSite_31;
                        if( MoexDownloader.Class128.callSite_30 == null )
                        {
                            MoexDownloader.Class128.callSite_30 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj19 = MoexDownloader.Class128.callSite_30.Target( ( CallSite )MoexDownloader.Class128.callSite_30, typeof( MoexDownloader ), obj4, num8 );
                        DateTime? dateTime2 = target17( ( CallSite )callSite31, obj19 ).TryToDateTime( "yyyy-MM-dd", CultureInfo.InvariantCulture );
                        securityInfo10.IssueDate = dateTime2;
                        SecurityInfo securityInfo11 = securityInfo_0;
                        if( MoexDownloader.Class128.callSite_33 == null )
                        {
                            MoexDownloader.Class128.callSite_33 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        Func< CallSite, object, string > target18 = MoexDownloader.Class128.callSite_33.Target;
                        CallSite< Func< CallSite, object, string > > callSite33 = MoexDownloader.Class128.callSite_33;
                        if( MoexDownloader.Class128.callSite_32 == null )
                        {
                            MoexDownloader.Class128.callSite_32 = CallSite< Func< CallSite, Type, object, int, object > >.Create( Binder.InvokeMember( CSharpBinderFlags.None, "#=zJibg6rA=", ( IEnumerable< Type > )new Type[ 1 ]
                           {
                typeof( string )
                           }, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 3 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj20 = MoexDownloader.Class128.callSite_32.Target( ( CallSite )MoexDownloader.Class128.callSite_32, typeof( MoexDownloader ), obj4, num12 );
                        DateTime? dateTime3 = target18( ( CallSite )callSite33, obj20 ).TryToDateTime( "yyyy-MM-dd", CultureInfo.InvariantCulture );
                        securityInfo11.SettleDate = dateTime3;
                        Extension.smethod_3( securityInfo_0 );
                    }
                }
            }
        }

        private static T smethod_1< T >( object object_0, int int_0 )
        {
            if( int_0 == -1 )
            {
                return default( T );
            }

            if( MoexDownloader.Class127< T >.callSite_1 == null )
            {
                MoexDownloader.Class127< T >.callSite_1 = CallSite< Func< CallSite, object, T > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( T ), typeof( MoexDownloader ) ) );
            }

            Func< CallSite, object, T > target = MoexDownloader.Class127< T >.callSite_1.Target;
            CallSite< Func< CallSite, object, T > > callSite1 = MoexDownloader.Class127< T >.callSite_1;
            if( MoexDownloader.Class127< T >.callSite_0 == null )
            {
                MoexDownloader.Class127< T >.callSite_0 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
               } ) );
            }

            object obj = MoexDownloader.Class127< T >.callSite_0.Target( ( CallSite )MoexDownloader.Class127< T >.callSite_0, object_0, int_0 );
            return target( ( CallSite )callSite1, obj );
        }

        public static IEnumerable< SecurityInfo > GetSecurities(
          string code,
          SecurityTypes? type,
          bool isDownload = true )
        {
            MoexDownloader.Class130 class130 = new MoexDownloader.Class130( );
            class130.nullable_0 = type;
            if( code.IsEmpty( ) )
            {
                throw new ArgumentNullException( nameof( code ) );
            }

            if( MoexDownloader.hashSet_0.Contains( code ) )
            {
                return Enumerable.Empty< SecurityInfo >( );
            }

            SecurityInfo[ ] securityInfoArray = Extension.smethod_0( code );
            if( securityInfoArray != null && securityInfoArray.Length != 0 )
            {
                if( class130.nullable_0.HasValue )
                {
                    securityInfoArray = ( ( IEnumerable< SecurityInfo > )securityInfoArray ).Where< SecurityInfo >( new Func< SecurityInfo, bool >( class130.method_0 ) ).ToArray< SecurityInfo >( );
                }

                return ( IEnumerable< SecurityInfo > )securityInfoArray;
            }
            SecurityInfo securityInfo = Extension.smethod_2( code );
            if( securityInfo != null )
            {
                return ( IEnumerable< SecurityInfo > )new SecurityInfo[ 1 ]
               {
          securityInfo
               };
            }

            if( !isDownload )
            {
                return Enumerable.Empty< SecurityInfo >( );
            }

            if( code.EndsWithIgnoreCase( ".T+" ) )
            {
                code = code.Remove( code.Length - 3 );
            }

            return MoexDownloader.smethod_2( code );
        }

        private static IEnumerable< SecurityInfo > smethod_2( string string_0 )
        {
            SecurityInfo securityInfo1 = new SecurityInfo( )
            {
                Code = string_0
            };
            object obj1 = JsonConvert.DeserializeObject( MoexDownloader.smethod_3( "https://iss.moex.com/iss/securities/" + securityInfo1.Code + ".jsonp" ) );
            if( MoexDownloader.Class129.callSite_13 == null )
            {
                MoexDownloader.Class129.callSite_13 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( MoexDownloader ) ) );
            }

            Func< CallSite, object, IEnumerable > target1 = MoexDownloader.Class129.callSite_13.Target;
            CallSite< Func< CallSite, object, IEnumerable > > callSite13 = MoexDownloader.Class129.callSite_13;
            if( MoexDownloader.Class129.callSite_1 == null )
            {
                MoexDownloader.Class129.callSite_1 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "data", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
               } ) );
            }

            Func< CallSite, object, object > target2 = MoexDownloader.Class129.callSite_1.Target;
            CallSite< Func< CallSite, object, object > > callSite1 = MoexDownloader.Class129.callSite_1;
            if( MoexDownloader.Class129.callSite_0 == null )
            {
                MoexDownloader.Class129.callSite_0 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "description", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
               } ) );
            }

            object obj2 = MoexDownloader.Class129.callSite_0.Target( ( CallSite )MoexDownloader.Class129.callSite_0, obj1 );
            object obj3 = target2( ( CallSite )callSite1, obj2 );
            foreach( object obj4 in target1( ( CallSite )callSite13, obj3 ) )
            {
                if( MoexDownloader.Class129.callSite_2 == null )
                {
                    MoexDownloader.Class129.callSite_2 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, ( string ) null )
                   } ) );
                }

                object obj5 = MoexDownloader.Class129.callSite_2.Target( ( CallSite )MoexDownloader.Class129.callSite_2, obj4, 2 );
                if( MoexDownloader.Class129.callSite_4 == null )
                {
                    MoexDownloader.Class129.callSite_4 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                }

                Func< CallSite, object, string > target3 = MoexDownloader.Class129.callSite_4.Target;
                CallSite< Func< CallSite, object, string > > callSite4 = MoexDownloader.Class129.callSite_4;
                if( MoexDownloader.Class129.callSite_3 == null )
                {
                    MoexDownloader.Class129.callSite_3 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, ( string ) null )
                   } ) );
                }

                object obj6 = MoexDownloader.Class129.callSite_3.Target( ( CallSite )MoexDownloader.Class129.callSite_3, obj4, 0 );
                string upperInvariant = target3( ( CallSite )callSite4, obj6 ).ToUpperInvariant( );

                //switch ( Class24.smethod_0( upperInvariant ) )
                //{
                //    case 338683789:
                //        if ( upperInvariant == "TYPE" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_12 == null )
                //                MoexDownloader.Class129.callSite_12 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                //            string str = MoexDownloader.Class129.callSite_12.Target( ( CallSite )MoexDownloader.Class129.callSite_12, obj5 );
                //            securityInfo2.Type = str;
                //            continue;
                //        }
                //        continue;
                //    case 449831473:
                //        if ( upperInvariant == "ISSUESIZE" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_11 == null )
                //                MoexDownloader.Class129.callSite_11 = CallSite<Func<CallSite, object, Decimal>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( Decimal ), typeof( MoexDownloader ) ) );
                //            Decimal? nullable = new Decimal?( MoexDownloader.Class129.callSite_11.Target( ( CallSite )MoexDownloader.Class129.callSite_11, obj5 ) );
                //            securityInfo2.IssueSize = nullable;
                //            continue;
                //        }
                //        continue;
                //    case 460349364:
                //        if ( upperInvariant == "ISSUEDATE" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_10 == null )
                //                MoexDownloader.Class129.callSite_10 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                //            DateTime? dateTime = MoexDownloader.Class129.callSite_10.Target( ( CallSite )MoexDownloader.Class129.callSite_10, obj5 ).TryToDateTime( "yyyy-MM-dd", CultureInfo.InvariantCulture );
                //            securityInfo2.IssueDate = dateTime;
                //            continue;
                //        }
                //        continue;
                //    case 604585842:
                //        if ( upperInvariant == "LSTTRADE" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_9 == null )
                //                MoexDownloader.Class129.callSite_9 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                //            DateTime? dateTime = MoexDownloader.Class129.callSite_9.Target( ( CallSite )MoexDownloader.Class129.callSite_9, obj5 ).TryToDateTime( "yyyy-MM-dd", CultureInfo.InvariantCulture );
                //            securityInfo2.LastDate = dateTime;
                //            continue;
                //        }
                //        continue;
                //    case 1387956774:
                //        if ( upperInvariant == "NAME" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_5 == null )
                //                MoexDownloader.Class129.callSite_5 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                //            string str = MoexDownloader.Class129.callSite_5.Target( ( CallSite )MoexDownloader.Class129.callSite_5, obj5 );
                //            securityInfo2.Name = str;
                //            continue;
                //        }
                //        continue;
                //    case 3017670126:
                //        if ( upperInvariant == "ISIN" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_8 == null )
                //                MoexDownloader.Class129.callSite_8 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                //            string str = MoexDownloader.Class129.callSite_8.Target( ( CallSite )MoexDownloader.Class129.callSite_8, obj5 );
                //            securityInfo2.Isin = str;
                //            continue;
                //        }
                //        continue;
                //    case 3307378946:
                //        if ( upperInvariant == "ASSETCODE" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_7 == null )
                //                MoexDownloader.Class129.callSite_7 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                //            string str = MoexDownloader.Class129.callSite_7.Target( ( CallSite )MoexDownloader.Class129.callSite_7, obj5 );
                //            securityInfo2.Asset = str;
                //            continue;
                //        }
                //        continue;
                //    case 3579029398:
                //        if ( upperInvariant == "SHORTNAME" )
                //        {
                //            SecurityInfo securityInfo2 = securityInfo1;
                //            if ( MoexDownloader.Class129.callSite_6 == null )
                //                MoexDownloader.Class129.callSite_6 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.None, typeof( string ), typeof( MoexDownloader ) ) );
                //            string str = MoexDownloader.Class129.callSite_6.Target( ( CallSite )MoexDownloader.Class129.callSite_6, obj5 );
                //            securityInfo2.ShortName = str;
                //            continue;
                //        }
                //        continue;
                //    default:
                //        continue;
                //}
            }
            int num1 = -1;
            int num2 = -1;
            int num3 = -1;
            int num4 = 0;
            if( MoexDownloader.Class129.callSite_17 == null )
            {
                MoexDownloader.Class129.callSite_17 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( MoexDownloader ) ) );
            }

            Func< CallSite, object, IEnumerable > target4 = MoexDownloader.Class129.callSite_17.Target;
            CallSite< Func< CallSite, object, IEnumerable > > callSite17 = MoexDownloader.Class129.callSite_17;
            if( MoexDownloader.Class129.callSite_15 == null )
            {
                MoexDownloader.Class129.callSite_15 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "columns", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
               } ) );
            }

            Func< CallSite, object, object > target5 = MoexDownloader.Class129.callSite_15.Target;
            CallSite< Func< CallSite, object, object > > callSite15 = MoexDownloader.Class129.callSite_15;
            if( MoexDownloader.Class129.callSite_14 == null )
            {
                MoexDownloader.Class129.callSite_14 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "boards", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
               } ) );
            }

            object obj7 = MoexDownloader.Class129.callSite_14.Target( ( CallSite )MoexDownloader.Class129.callSite_14, obj1 );
            object obj8 = target5( ( CallSite )callSite15, obj7 );
            foreach( object obj4 in target4( ( CallSite )callSite17, obj8 ) )
            {
                if( MoexDownloader.Class129.callSite_16 == null )
                {
                    MoexDownloader.Class129.callSite_16 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                }

                string lowerInvariant = MoexDownloader.Class129.callSite_16.Target( ( CallSite )MoexDownloader.Class129.callSite_16, obj4 ).ToLowerInvariant( );
                if( !( lowerInvariant == "boardid" ) )
                {
                    if( !( lowerInvariant == "market" ) )
                    {
                        if( lowerInvariant == "engine" )
                        {
                            num3 = num4;
                        }
                    }
                    else
                    {
                        num2 = num4;
                    }
                }
                else
                {
                    num1 = num4;
                }

                ++num4;
            }
            List< SecurityInfo > securityInfoList = new List< SecurityInfo >( );
            if( MoexDownloader.Class129.callSite_45 == null )
            {
                MoexDownloader.Class129.callSite_45 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( MoexDownloader ) ) );
            }

            Func< CallSite, object, IEnumerable > target6 = MoexDownloader.Class129.callSite_45.Target;
            CallSite< Func< CallSite, object, IEnumerable > > callSite45 = MoexDownloader.Class129.callSite_45;
            if( MoexDownloader.Class129.callSite_19 == null )
            {
                MoexDownloader.Class129.callSite_19 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "data", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
               } ) );
            }

            Func< CallSite, object, object > target7 = MoexDownloader.Class129.callSite_19.Target;
            CallSite< Func< CallSite, object, object > > callSite19 = MoexDownloader.Class129.callSite_19;
            if( MoexDownloader.Class129.callSite_18 == null )
            {
                MoexDownloader.Class129.callSite_18 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "boards", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
               {
          CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
               } ) );
            }

            object obj9 = MoexDownloader.Class129.callSite_18.Target( ( CallSite )MoexDownloader.Class129.callSite_18, obj1 );
            object obj10 = target7( ( CallSite )callSite19, obj9 );
            foreach( object obj4 in target6( ( CallSite )callSite45, obj10 ) )
            {
                object[ ] objArray = new object[ 4 ];
                if( MoexDownloader.Class129.callSite_20 == null )
                {
                    MoexDownloader.Class129.callSite_20 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                   } ) );
                }

                objArray[ 0 ] = MoexDownloader.Class129.callSite_20.Target( ( CallSite )MoexDownloader.Class129.callSite_20, obj4, num3 );
                if( MoexDownloader.Class129.callSite_21 == null )
                {
                    MoexDownloader.Class129.callSite_21 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                   } ) );
                }

                objArray[ 1 ] = MoexDownloader.Class129.callSite_21.Target( ( CallSite )MoexDownloader.Class129.callSite_21, obj4, num2 );
                if( MoexDownloader.Class129.callSite_22 == null )
                {
                    MoexDownloader.Class129.callSite_22 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                   } ) );
                }

                objArray[ 2 ] = MoexDownloader.Class129.callSite_22.Target( ( CallSite )MoexDownloader.Class129.callSite_22, obj4, num1 );
                objArray[ 3 ] = ( object )string_0;
                object obj5 = JsonConvert.DeserializeObject( MoexDownloader.smethod_3( string.Format( "https://iss.moex.com/iss/engines/{0}/markets/{1}/boards/{2}/securities/{3}.jsonp", objArray ) ) );
                SecurityInfo securityInfo_0 = securityInfo1.Clone( );
                SecurityInfo securityInfo2 = securityInfo_0;
                if( MoexDownloader.Class129.callSite_24 == null )
                {
                    MoexDownloader.Class129.callSite_24 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                }

                Func< CallSite, object, string > target3 = MoexDownloader.Class129.callSite_24.Target;
                CallSite< Func< CallSite, object, string > > callSite24 = MoexDownloader.Class129.callSite_24;
                if( MoexDownloader.Class129.callSite_23 == null )
                {
                    MoexDownloader.Class129.callSite_23 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                   } ) );
                }

                object obj6 = MoexDownloader.Class129.callSite_23.Target( ( CallSite )MoexDownloader.Class129.callSite_23, obj4, num1 );
                string str1 = target3( ( CallSite )callSite24, obj6 );
                securityInfo2.Board = str1;
                if( MoexDownloader.Class129.callSite_29 == null )
                {
                    MoexDownloader.Class129.callSite_29 = CallSite< Func< CallSite, object, bool > >.Create( Binder.UnaryOperation( CSharpBinderFlags.None, ExpressionType.IsTrue, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                Func< CallSite, object, bool > target8 = MoexDownloader.Class129.callSite_29.Target;
                CallSite< Func< CallSite, object, bool > > callSite29 = MoexDownloader.Class129.callSite_29;
                if( MoexDownloader.Class129.callSite_28 == null )
                {
                    MoexDownloader.Class129.callSite_28 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.BinaryOperation( CSharpBinderFlags.None, ExpressionType.GreaterThan, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, ( string ) null )
                   } ) );
                }

                Func< CallSite, object, int, object > target9 = MoexDownloader.Class129.callSite_28.Target;
                CallSite< Func< CallSite, object, int, object > > callSite28 = MoexDownloader.Class129.callSite_28;
                if( MoexDownloader.Class129.callSite_27 == null )
                {
                    MoexDownloader.Class129.callSite_27 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "Count", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                Func< CallSite, object, object > target10 = MoexDownloader.Class129.callSite_27.Target;
                CallSite< Func< CallSite, object, object > > callSite27 = MoexDownloader.Class129.callSite_27;
                if( MoexDownloader.Class129.callSite_26 == null )
                {
                    MoexDownloader.Class129.callSite_26 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "data", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                Func< CallSite, object, object > target11 = MoexDownloader.Class129.callSite_26.Target;
                CallSite< Func< CallSite, object, object > > callSite26 = MoexDownloader.Class129.callSite_26;
                if( MoexDownloader.Class129.callSite_25 == null )
                {
                    MoexDownloader.Class129.callSite_25 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "securities", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                   {
            CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                   } ) );
                }

                object obj11 = MoexDownloader.Class129.callSite_25.Target( ( CallSite )MoexDownloader.Class129.callSite_25, obj5 );
                object obj12 = target11( ( CallSite )callSite26, obj11 );
                object obj13 = target10( ( CallSite )callSite27, obj12 );
                object obj14 = target9( ( CallSite )callSite28, obj13, 0 );
                if( target8( ( CallSite )callSite29, obj14 ) )
                {
                    if( MoexDownloader.Class129.callSite_32 == null )
                    {
                        MoexDownloader.Class129.callSite_32 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, ( string ) null )
                       } ) );
                    }

                    Func< CallSite, object, int, object > target12 = MoexDownloader.Class129.callSite_32.Target;
                    CallSite< Func< CallSite, object, int, object > > callSite32 = MoexDownloader.Class129.callSite_32;
                    if( MoexDownloader.Class129.callSite_31 == null )
                    {
                        MoexDownloader.Class129.callSite_31 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.ResultIndexed, "data", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                       } ) );
                    }

                    Func< CallSite, object, object > target13 = MoexDownloader.Class129.callSite_31.Target;
                    CallSite< Func< CallSite, object, object > > callSite31 = MoexDownloader.Class129.callSite_31;
                    if( MoexDownloader.Class129.callSite_30 == null )
                    {
                        MoexDownloader.Class129.callSite_30 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "securities", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                       } ) );
                    }

                    object obj15 = MoexDownloader.Class129.callSite_30.Target( ( CallSite )MoexDownloader.Class129.callSite_30, obj5 );
                    object obj16 = target13( ( CallSite )callSite31, obj15 );
                    object obj17 = target12( ( CallSite )callSite32, obj16, 0 );
                    int num5 = 0;
                    if( MoexDownloader.Class129.callSite_44 == null )
                    {
                        MoexDownloader.Class129.callSite_44 = CallSite< Func< CallSite, object, IEnumerable > >.Create( Binder.Convert( CSharpBinderFlags.None, typeof( IEnumerable ), typeof( MoexDownloader ) ) );
                    }

                    Func< CallSite, object, IEnumerable > target14 = MoexDownloader.Class129.callSite_44.Target;
                    CallSite< Func< CallSite, object, IEnumerable > > callSite44 = MoexDownloader.Class129.callSite_44;
                    if( MoexDownloader.Class129.callSite_34 == null )
                    {
                        MoexDownloader.Class129.callSite_34 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "columns", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                       } ) );
                    }

                    Func< CallSite, object, object > target15 = MoexDownloader.Class129.callSite_34.Target;
                    CallSite< Func< CallSite, object, object > > callSite34 = MoexDownloader.Class129.callSite_34;
                    if( MoexDownloader.Class129.callSite_33 == null )
                    {
                        MoexDownloader.Class129.callSite_33 = CallSite< Func< CallSite, object, object > >.Create( Binder.GetMember( CSharpBinderFlags.None, "securities", typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 1 ]
                       {
              CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null )
                       } ) );
                    }

                    object obj18 = MoexDownloader.Class129.callSite_33.Target( ( CallSite )MoexDownloader.Class129.callSite_33, obj5 );
                    object obj19 = target15( ( CallSite )callSite34, obj18 );
                    foreach( object obj20 in target14( ( CallSite )callSite44, obj19 ) )
                    {
                        if( MoexDownloader.Class129.callSite_35 == null )
                        {
                            MoexDownloader.Class129.callSite_35 = CallSite< Func< CallSite, object, int, object > >.Create( Binder.GetIndex( CSharpBinderFlags.None, typeof( MoexDownloader ), ( IEnumerable< CSharpArgumentInfo > )new CSharpArgumentInfo[ 2 ]
                           {
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, ( string ) null ),
                CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, ( string ) null )
                           } ) );
                        }

                        object obj21 = MoexDownloader.Class129.callSite_35.Target( ( CallSite )MoexDownloader.Class129.callSite_35, obj17, num5 );
                        if( MoexDownloader.Class129.callSite_36 == null )
                        {
                            MoexDownloader.Class129.callSite_36 = CallSite< Func< CallSite, object, string > >.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        }

                        string upperInvariant = MoexDownloader.Class129.callSite_36.Target( ( CallSite )MoexDownloader.Class129.callSite_36, obj20 ).ToUpperInvariant( );
                        //switch ( Class24.smethod_0( upperInvariant ) )
                        //{
                        //    case 686308805:
                        //        if ( upperInvariant == "MINSTEP" )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_42 == null )
                        //                MoexDownloader.Class129.callSite_42 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        //            Decimal? nullable = new Decimal?( ( Decimal )double.Parse( MoexDownloader.Class129.callSite_42.Target( ( CallSite )MoexDownloader.Class129.callSite_42, obj21 ), ( IFormatProvider )CultureInfo.InvariantCulture ) );
                        //            securityInfo3.PriceStep = nullable;
                        //            break;
                        //        }
                        //        break;
                        //    case 1091447865:
                        //        if ( upperInvariant == "LOTSIZE" )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_37 == null )
                        //                MoexDownloader.Class129.callSite_37 = CallSite<Func<CallSite, object, int>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( int ), typeof( MoexDownloader ) ) );
                        //            Decimal? nullable = new Decimal?( ( Decimal )MoexDownloader.Class129.callSite_37.Target( ( CallSite )MoexDownloader.Class129.callSite_37, obj21 ) );
                        //            securityInfo3.Multiplier = nullable;
                        //            break;
                        //        }
                        //        break;
                        //    case 1387956774:
                        //        if ( upperInvariant == "NAME" && securityInfo_0.Name.IsEmpty( ) )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_39 == null )
                        //                MoexDownloader.Class129.callSite_39 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        //            string str2 = MoexDownloader.Class129.callSite_39.Target( ( CallSite )MoexDownloader.Class129.callSite_39, obj21 );
                        //            securityInfo3.Name = str2;
                        //            break;
                        //        }
                        //        break;
                        //    case 3307378946:
                        //        if ( upperInvariant == "ASSETCODE" && securityInfo_0.Asset.IsEmpty( ) )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_41 == null )
                        //                MoexDownloader.Class129.callSite_41 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        //            string str2 = MoexDownloader.Class129.callSite_41.Target( ( CallSite )MoexDownloader.Class129.callSite_41, obj21 );
                        //            securityInfo3.Asset = str2;
                        //            break;
                        //        }
                        //        break;
                        //    case 3552844363:
                        //        if ( upperInvariant == "CURRENCYID" )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_43 == null )
                        //                MoexDownloader.Class129.callSite_43 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        //            string str2 = MoexDownloader.Class129.callSite_43.Target( ( CallSite )MoexDownloader.Class129.callSite_43, obj21 );
                        //            securityInfo3.Currency = str2;
                        //            break;
                        //        }
                        //        break;
                        //    case 3579029398:
                        //        if ( upperInvariant == "SHORTNAME" && securityInfo_0.ShortName.IsEmpty( ) )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_40 == null )
                        //                MoexDownloader.Class129.callSite_40 = CallSite<Func<CallSite, object, string>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( string ), typeof( MoexDownloader ) ) );
                        //            string str2 = MoexDownloader.Class129.callSite_40.Target( ( CallSite )MoexDownloader.Class129.callSite_40, obj21 );
                        //            securityInfo3.ShortName = str2;
                        //            break;
                        //        }
                        //        break;
                        //    case 3949999597:
                        //        if ( upperInvariant == "DECIMALS" )
                        //        {
                        //            SecurityInfo securityInfo3 = securityInfo_0;
                        //            if ( MoexDownloader.Class129.callSite_38 == null )
                        //                MoexDownloader.Class129.callSite_38 = CallSite<Func<CallSite, object, int>>.Create( Binder.Convert( CSharpBinderFlags.ConvertExplicit, typeof( int ), typeof( MoexDownloader ) ) );
                        //            int? nullable = new int?( MoexDownloader.Class129.callSite_38.Target( ( CallSite )MoexDownloader.Class129.callSite_38, obj21 ) );
                        //            securityInfo3.Decimals = nullable;
                        //            break;
                        //        }
                        //        break;
                        //}
                        ++num5;
                    }
                }
                Extension.smethod_3( securityInfo_0 );
                securityInfoList.Add( securityInfo_0 );
            }
            return ( IEnumerable< SecurityInfo > )securityInfoList;
        }

        private static string smethod_3( string string_0 )
        {
            using( WebClient webClient = new WebClient( )
            {
                Encoding = Encoding.UTF8
            } )
            {
                return webClient.DownloadString( string_0 );
            }
        }

        private sealed class Class126
        {
            public string string_0;

            internal bool method_0( SecurityInfo securityInfo_0 )
            {
                if( !securityInfo_0.ShortName.CompareIgnoreCase( this.string_0 ) )
                {
                    return securityInfo_0.Name.CompareIgnoreCase( this.string_0 );
                }

                return true;
            }
        }

        private static class Class127< T >
        {
            public static CallSite< Func< CallSite, object, int, object > > callSite_0;
            public static CallSite< Func< CallSite, object, T > > callSite_1;
        }

        private static class Class128
        {
            public static CallSite< Func< CallSite, object, object > > callSite_0;
            public static CallSite< Func< CallSite, object, object > > callSite_1;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_2;
            public static CallSite< Func< CallSite, object, string > > callSite_3;
            public static CallSite< Func< CallSite, object, object > > callSite_4;
            public static CallSite< Func< CallSite, object, object > > callSite_5;
            public static CallSite< Func< CallSite, object, int, object > > callSite_6;
            public static CallSite< Func< CallSite, object, string > > callSite_7;
            public static CallSite< Func< CallSite, object, int, object > > callSite_8;
            public static CallSite< Func< CallSite, object, string > > callSite_9;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_10;
            public static CallSite< Func< CallSite, object, string > > callSite_11;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_12;
            public static CallSite< Func< CallSite, object, string > > callSite_13;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_14;
            public static CallSite< Func< CallSite, object, int? > > callSite_15;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_16;
            public static CallSite< Func< CallSite, object, Decimal? > > callSite_17;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_18;
            public static CallSite< Func< CallSite, object, string > > callSite_19;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_20;
            public static CallSite< Func< CallSite, object, string > > callSite_21;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_22;
            public static CallSite< Func< CallSite, object, Decimal? > > callSite_23;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_24;
            public static CallSite< Func< CallSite, object, string > > callSite_25;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_26;
            public static CallSite< Func< CallSite, object, string > > callSite_27;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_28;
            public static CallSite< Func< CallSite, object, Decimal? > > callSite_29;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_30;
            public static CallSite< Func< CallSite, object, string > > callSite_31;
            public static CallSite< Func< CallSite, Type, object, int, object > > callSite_32;
            public static CallSite< Func< CallSite, object, string > > callSite_33;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_34;
        }

        private static class Class129
        {
            public static CallSite< Func< CallSite, object, object > > callSite_0;
            public static CallSite< Func< CallSite, object, object > > callSite_1;
            public static CallSite< Func< CallSite, object, int, object > > callSite_2;
            public static CallSite< Func< CallSite, object, int, object > > callSite_3;
            public static CallSite< Func< CallSite, object, string > > callSite_4;
            public static CallSite< Func< CallSite, object, string > > callSite_5;
            public static CallSite< Func< CallSite, object, string > > callSite_6;
            public static CallSite< Func< CallSite, object, string > > callSite_7;
            public static CallSite< Func< CallSite, object, string > > callSite_8;
            public static CallSite< Func< CallSite, object, string > > callSite_9;
            public static CallSite< Func< CallSite, object, string > > callSite_10;
            public static CallSite< Func< CallSite, object, Decimal > > callSite_11;
            public static CallSite< Func< CallSite, object, string > > callSite_12;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_13;
            public static CallSite< Func< CallSite, object, object > > callSite_14;
            public static CallSite< Func< CallSite, object, object > > callSite_15;
            public static CallSite< Func< CallSite, object, string > > callSite_16;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_17;
            public static CallSite< Func< CallSite, object, object > > callSite_18;
            public static CallSite< Func< CallSite, object, object > > callSite_19;
            public static CallSite< Func< CallSite, object, int, object > > callSite_20;
            public static CallSite< Func< CallSite, object, int, object > > callSite_21;
            public static CallSite< Func< CallSite, object, int, object > > callSite_22;
            public static CallSite< Func< CallSite, object, int, object > > callSite_23;
            public static CallSite< Func< CallSite, object, string > > callSite_24;
            public static CallSite< Func< CallSite, object, object > > callSite_25;
            public static CallSite< Func< CallSite, object, object > > callSite_26;
            public static CallSite< Func< CallSite, object, object > > callSite_27;
            public static CallSite< Func< CallSite, object, int, object > > callSite_28;
            public static CallSite< Func< CallSite, object, bool > > callSite_29;
            public static CallSite< Func< CallSite, object, object > > callSite_30;
            public static CallSite< Func< CallSite, object, object > > callSite_31;
            public static CallSite< Func< CallSite, object, int, object > > callSite_32;
            public static CallSite< Func< CallSite, object, object > > callSite_33;
            public static CallSite< Func< CallSite, object, object > > callSite_34;
            public static CallSite< Func< CallSite, object, int, object > > callSite_35;
            public static CallSite< Func< CallSite, object, string > > callSite_36;
            public static CallSite< Func< CallSite, object, int > > callSite_37;
            public static CallSite< Func< CallSite, object, int > > callSite_38;
            public static CallSite< Func< CallSite, object, string > > callSite_39;
            public static CallSite< Func< CallSite, object, string > > callSite_40;
            public static CallSite< Func< CallSite, object, string > > callSite_41;
            public static CallSite< Func< CallSite, object, string > > callSite_42;
            public static CallSite< Func< CallSite, object, string > > callSite_43;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_44;
            public static CallSite< Func< CallSite, object, IEnumerable > > callSite_45;
        }

        private sealed class Class130
        {
            public SecurityTypes? nullable_0;

            internal bool method_0( SecurityInfo securityInfo_0 )
            {
                SecurityTypes? securityType = securityInfo_0.GetSecurityType( );
                SecurityTypes? nullable0 = this.nullable_0;
                return securityType.GetValueOrDefault( ) == nullable0.GetValueOrDefault( ) & securityType.HasValue == nullable0.HasValue;
            }
        }
    }
}
