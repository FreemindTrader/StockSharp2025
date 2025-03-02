using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo.History.Russian;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;

internal static class Extension
{
    private static readonly SynchronizedDictionary< string, CachedSynchronizedList< SecurityInfo > > synchronizedDictionary_0 = new SynchronizedDictionary< string, CachedSynchronizedList< SecurityInfo > >( StringComparer.InvariantCultureIgnoreCase );
    private static readonly SynchronizedDictionary< string, SecurityInfo > synchronizedDictionary_1 = new SynchronizedDictionary< string, SecurityInfo >( StringComparer.InvariantCultureIgnoreCase );

    static Extension( )
    {
        using( ZipArchive zipArchive = new ZipArchive( Class38.smethod_6().To<MemoryStream>() ) )
        {
            using( Stream stream = zipArchive.Entries.First< ZipArchiveEntry >( ).Open( ) )
            {
                Extension.Class13 class13 = new Extension.Class13( );
                class13.xelement_0 = XDocument.Load( stream ).Root;
                if( class13.xelement_0 == null )
                {
                    throw new InvalidOperationException( );
                }

                SecurityInfo[ ] securityInfoArray = CultureInfo.InvariantCulture.DoInCulture< SecurityInfo[ ] >( new Func< SecurityInfo[ ] >( class13.method_0 ) );
                foreach( SecurityInfo securityInfo in securityInfoArray )
                {
                    Extension.synchronizedDictionary_0.SafeAdd< string, CachedSynchronizedList< SecurityInfo > >( securityInfo.Code ).Add( securityInfo );
                }

                foreach( SecurityInfo securityInfo in securityInfoArray )
                {
                    if( !securityInfo.ShortName.IsEmpty( ) )
                    {
                        Extension.synchronizedDictionary_1.TryAdd< string, SecurityInfo >( securityInfo.ShortName, securityInfo );
                    }
                    else if( !securityInfo.Name.IsEmpty( ) )
                    {
                        Extension.synchronizedDictionary_1.TryAdd< string, SecurityInfo >( securityInfo.Name, securityInfo );
                    }
                }
            }
        }
    }

    public static SecurityInfo[ ] smethod_0( string string_0 )
    {
        return Extension.synchronizedDictionary_0.TryGetValue< string, CachedSynchronizedList< SecurityInfo > >( string_0 )?.Cache;
    }

    public static SecurityInfo[ ] smethod_1( string string_0, string string_1 )
    {
        Extension.Class15 class15 = new Extension.Class15( );
        class15.string_0 = string_1;
        SecurityInfo[ ] securityInfoArray = Extension.smethod_0( string_0 );
        if( securityInfoArray == null )
        {
            return null;
        }

        return securityInfoArray.Where< SecurityInfo >( new Func< SecurityInfo, bool >( class15.method_0 ) ).ToArray< SecurityInfo >( );
    }

    public static SecurityInfo smethod_2( string string_0 )
    {
        return Extension.synchronizedDictionary_1.TryGetValue< string, SecurityInfo >( string_0 );
    }

    public static void smethod_3( SecurityInfo securityInfo_0 )
    {
        if( securityInfo_0 == null )
        {
            throw new ArgumentNullException( "info" );
        }

        Extension.synchronizedDictionary_0.SafeAdd< string, CachedSynchronizedList< SecurityInfo > >( securityInfo_0.Code ).Add( securityInfo_0 );
        if( securityInfo_0.ShortName.IsEmpty( ) )
        {
            return;
        }

        Extension.synchronizedDictionary_1.TryAdd< string, SecurityInfo >( securityInfo_0.ShortName, securityInfo_0 );
    }

    private sealed class Class13
    {
        public XElement xelement_0;

        internal SecurityInfo[ ] method_0( )
        {
            return this.xelement_0.Elements( ).Select< XElement, SecurityInfo >( Extension.Class14.func_0 ?? ( Extension.Class14.func_0 = new Func< XElement, SecurityInfo >( Extension.Class14.class14_0.method_0 ) ) ).ToArray< SecurityInfo >( );
        }
    }

    [Serializable]
    private sealed class Class14
    {
        public static readonly Extension.Class14 class14_0 = new Extension.Class14( );
        public static Func< XElement, SecurityInfo > func_0;

        internal SecurityInfo method_0( XElement xelement_0 )
        {
            return new SecurityInfo( )
            {
                Board = xelement_0.GetAttributeValue< string >( "board", null ),
                Multiplier = xelement_0.GetAttributeValue< Decimal? >( "multiplier", new Decimal?( ) ),
                Decimals = xelement_0.GetAttributeValue< int? >( "decimals", new int?( ) ),
                PriceStep = xelement_0.GetAttributeValue< Decimal? >( "priceStep", new Decimal?( ) ),
                Code = xelement_0.GetAttributeValue< string >( "code", null ),
                ShortName = xelement_0.GetAttributeValue< string >( "shortName", null ),
                Name = xelement_0.GetAttributeValue< string >( "name", null ),
                Isin = xelement_0.GetAttributeValue< string >( "isin", null ),
                Asset = xelement_0.GetAttributeValue< string >( "asset", null ),
                Type = xelement_0.GetAttributeValue< string >( "type", null ),
                Currency = xelement_0.GetAttributeValue< string >( "currency", null ),
                IssueSize = xelement_0.GetAttributeValue< Decimal? >( "issueSize", new Decimal?( ) ),
                IssueDate = xelement_0.GetAttributeValue< string >( "issueDate", null ).TryToDateTime( "yyyyMMdd", null ),
                LastDate = xelement_0.GetAttributeValue< string >( "lastDate", null ).TryToDateTime( "yyyyMMdd", null )
            };
        }
    }

    private sealed class Class15
    {
        public string string_0;

        internal bool method_0( SecurityInfo securityInfo_0 )
        {
            return securityInfo_0.Board.CompareIgnoreCase( this.string_0 );
        }
    }
}
