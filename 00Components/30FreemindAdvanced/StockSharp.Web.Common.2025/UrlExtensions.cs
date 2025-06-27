// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.UrlExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using Ecng.Reflection;
using Newtonsoft.Json.Linq;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace StockSharp.Web.Common
{
    public static class UrlExtensions
    {
        private static readonly SynchronizedPairSet<string, Type> _newIdentifiers = new SynchronizedPairSet<string, Type>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
        private static readonly SynchronizedPairSet<string, Type> _oldIdentifiers = new SynchronizedPairSet<string, Type>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
        public const int MaxWidth = 200;
        public const int MaxHeight = 200;
        private static readonly PairSet<string, string> _tags;
        private const string _fieldPrefix = "___";
        private static readonly Regex _re;
        public const string SizeKey = "size";

        static UrlExtensions()
        {
            PairSet<string, string> pairSet = new PairSet<string, string>();
            ( ( KeyedCollection<string, string> ) pairSet ).Add( "c#", "csharp" );
            ( ( KeyedCollection<string, string> ) pairSet ).Add( "s#", "stocksharp" );
            ( ( KeyedCollection<string, string> ) pairSet ).Add( ".net", "dotnet" );
            ( ( KeyedCollection<string, string> ) pairSet ).Add( "c++", "cpp" );
            ( ( KeyedCollection<string, string> ) pairSet ).Add( "s#.api", "api" );
            ( ( KeyedCollection<string, string> ) pairSet ).Add( "s#.studio", "studio" );
            ( ( KeyedCollection<string, string> ) pairSet ).Add( "s#.data", "hydra" );
            UrlExtensions._tags = pairSet;
            UrlExtensions._re = new Regex( "___\\w+___", RegexOptions.IgnoreCase | RegexOptions.Compiled );
            foreach ( Type implementation in ReflectionHelper.FindImplementations<BaseEntity>( typeof( BaseEntity ).Assembly, false, false, false, ( Func<Type, bool> ) null ) )
            {
                ( ( KeyedCollection<string, Type> ) UrlExtensions._newIdentifiers ).Add( StringHelper.ToLower( implementation.Name [0], true ).ToString() + implementation.Name.Substring( 1, implementation.Name.Length - 1 ) + "Id", implementation );
                string str = new string(((IEnumerable<char>) implementation.Name.ToCharArray()).Where<char>(new Func<char, bool>(char.IsUpper)).ToArray<char>()).ToLowerInvariant();
                if ( implementation == typeof( Favorite ) )
                    str = "fv";
                else if ( implementation == typeof( Email ) )
                    str = "em";
                else if ( implementation == typeof( Strategy ) )
                    str = "sr";
                else if ( implementation == typeof( StrategyPositionChange ) )
                    str = "srpc";
                else if ( implementation == typeof( StrategyEquityChange ) )
                    str = "srec";
                else if ( implementation == typeof( Domain ) )
                    str = "dom";
                else if ( implementation == typeof( Poll ) )
                    str = "pl";
                else if ( implementation == typeof( PollVote ) )
                    str = "plv";
                else if ( implementation == typeof( Payment ) )
                    str = "pay";
                else if ( implementation == typeof( PayGateway ) )
                    str = "paygw";
                else if ( implementation == typeof( PayGatewayDomain ) )
                    str = "paygwd";
                else if ( implementation == typeof( Subscription ) )
                    str = "sub";
                else if ( implementation == typeof( Suspicious ) )
                    str = "sus";
                else if ( implementation == typeof( Social ) )
                    str = "soc";
                else if ( implementation == typeof( StrategyType ) )
                    str = "stype";
                else if ( implementation == typeof( StrategyParam ) )
                    str = "sparam";
                else if ( implementation == typeof( DataPermission ) )
                    str = "dper";
                else if ( implementation == typeof( SocialToken ) )
                    str = "stkn";
                ( ( KeyedCollection<string, Type> ) UrlExtensions._oldIdentifiers ).Add( str + "id", implementation );
            }
        }

        public static string GetGravatarUrl( this string gravatarToken, string avatarSize )
        {
            if ( StringHelper.IsEmpty( gravatarToken ) )
                throw new ArgumentNullException( nameof( gravatarToken ) );
            int size = StringHelper.IsEmpty(avatarSize) ? 200 : (int) Converter.To<int>( avatarSize.Substring(0, avatarSize.IndexOf('x')));
            return gravatarToken.GetGravatarUrl( size );
        }

        public static string GetTagUrlName( this string tagName )
        {
            return ( string ) CollectionHelper.TryGetValue<string, string>(  UrlExtensions._tags, tagName.ToLowerInvariant() ) ?? tagName;
        }

        public static string GetTagName( this string tagUrl )
        {
            return ( string ) CollectionHelper.TryGetKey<string, string>( UrlExtensions._tags,  tagUrl.ToLowerInvariant() ) ?? tagUrl;
        }

        public static string ToFullAbsolute( this string virtualPath, Domain domain, bool urlEscape = true )
        {
            return virtualPath.ToFullAbsolute( ( ( Domain ) TypeHelper.CheckOnNull<Domain>( domain, nameof( domain ) ) ).Code, urlEscape );
        }

        public static Type GetEntityType( string queryKey, bool newFormat )
        {
            Type type;
            if ( !UrlExtensions.TryGetEntityType( queryKey, newFormat, out type ) )
                throw new ArgumentException( "For key " + queryKey + " no any type associated.", nameof( queryKey ) );
            return type;
        }

        public static bool TryGetEntityType( string queryKey, bool newFormat, out Type type )
        {
            if ( StringHelper.EqualsIgnoreCase( queryKey, "profileId" ) )
                queryKey = "clientId";
            return ( newFormat ? ( KeyedCollection<string, Type> ) UrlExtensions._newIdentifiers : ( KeyedCollection<string, Type> ) UrlExtensions._oldIdentifiers ).TryGetValue( queryKey, out type );
        }

        public static string GetIdentity( Type entityType, bool newFormat )
        {
            return ( newFormat ? UrlExtensions._newIdentifiers : UrlExtensions._oldIdentifiers )[entityType];
        }

        public static string GetIdentity<T>( bool newFormat )
        {
            return UrlExtensions.GetIdentity( typeof( T ), newFormat );
        }

        public static IEnumerable<string> SplitTags( this string groups )
        {
            if ( StringHelper.IsEmpty( groups ) )
                return ( IEnumerable<string> ) Array.Empty<string>();
            string str = groups.IndexOf(',') != -1 ? "," : " ";
            return ( ( IEnumerable<string> ) StringHelper.SplitBySep( groups, str, true ) ).Select<string, string>( ( Func<string, string> ) ( g => g.Trim().Replace( "?", string.Empty ).Replace( "$", string.Empty ).Replace( "<", string.Empty ).Replace( ">", string.Empty ) ) ).Where<string>( ( Func<string, bool> ) ( s => !StringHelper.IsEmpty( s ) ) ).Distinct<string>( ( IEqualityComparer<string> ) StringComparer.InvariantCultureIgnoreCase );
        }

        public static string GetIdNameUrlPart( this long id, string name = null )
        {
            string str = (string) Converter.To<string>( id);
            if ( !StringHelper.IsEmpty( name ) )
            {
                name = name.Replace( '.', '_' ).Replace( ' ', '-' ).CheckUrl( true, true, true );
                if ( !StringHelper.IsEmpty( name ) )
                    str = str + "/" + name;
            }
            return str;
        }

        public static string GetProductUrlPart( this long id, string urlAlias, string packageId )
        {
            return StringHelper.IsEmpty( urlAlias, StringHelper.IsEmpty( packageId, ( string ) Converter.To<string>( ( object ) id ) ) ).ToLowerInvariant();
        }

        public static bool IsArg( this string key )
        {
            if ( key.StartsWith( "___" ) )
                return key.EndsWith( "___" );
            return false;
        }

        
        public static IEnumerable<(string name, string value)> SplitArgs( this string args )
        {
            return _( args ).Reverse<ValueTuple<string, string>>();

            IEnumerable<ValueTuple<string, string>> _( string args )
            {
                IOrderedEnumerable<Match> source = UrlExtensions._re.Matches(args).Cast<Match>().OrderByDescending<Match, int>((Func<Match, int>) (m => m.Index));
                int prevIdx = args.Length;
                Func<Match, ValueTuple<string, string>> selector = (Func<Match, ValueTuple<string, string>>) (m =>
        {
            string str1 = m.Value;
            int startIndex = m.Index + str1.Length + 1;
            string str2 = startIndex >= args.Length ? string.Empty : args.Substring(startIndex, MathHelper.Max(prevIdx - startIndex, 0));
            if (str2.EndsWith("\r\n"))
                str2 = str2.Substring(0, str2.Length - "\r\n".Length);
            prevIdx = m.Index;
            return new ValueTuple<string, string>(str1, str2);
        });
                return source.Select<Match, ValueTuple<string, string>>( selector );
            }
        }

        public static string JoinArgs( this IEnumerable<(string name, string value)> args )
        {
            return StringHelper.JoinRN( args.Select<ValueTuple<string, string>, string>( ( Func<ValueTuple<string, string>, string> ) ( a => a.Item1 + "=" + a.Item2 ) ) );
        }

        public static string GetIdNameUrlPart<TEntity>( this TEntity entity ) where TEntity : BaseEntity, INameEntity
        {
            if ( ( object ) entity == null )
                throw new ArgumentNullException( nameof( entity ) );
            return entity.Id.GetIdNameUrlPart( entity.Name );
        }

        public static string ToRelative( this string absoluteUrl )
        {
            return StringHelper.Remove( StringHelper.Remove( StringHelper.Remove( StringHelper.Remove( StringHelper.Remove( absoluteUrl, "http://localhost/stocksharp/", true ), "http://stocksharp.com/", true ), "http://stocksharp.ru/", true ), "https://stocksharp.com/", true ), "https://stocksharp.ru/", true );
        }
    }
}
