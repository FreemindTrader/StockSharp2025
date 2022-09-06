using Ecng.Collections;
using Ecng.Common;
using Ecng.Interop;
using StockSharp.Algo;
using StockSharp.Algo.History.Russian.Rts;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.FtpClient;

internal sealed class Class23 : Class21
{
    private string[] string_1;
    private string[] string_2;
    private string[] string_3;
    private DateTime dateTime_0;

    public Class23( RtsHistorySource rtsHistorySource_1 ) : base( rtsHistorySource_1 )
    {
    }

    public IDictionary< Security, List< ExecutionMessage > > method_9(
    ISecurityStorage isecurityStorage_0,
    System.Net.FtpClient.FtpClient ftpClient_0,
    DateTime dateTime_1,
    bool bool_1 )
    {
        Class23.Class37 class37 = new Class23.Class37( );
        class37.ftpClient_0 = ftpClient_0;
        class37.class23_0 = this;
        if( this.dateTime_0.Date != DateTime.Today )
        {
            this.string_1 = ( ( IEnumerable< FtpListItem > ) class37.ftpClient_0.GetListing( this.method_3( ) ) ).Where< FtpListItem >( Class23.Class35.func_0 ?? ( Class23.Class35.func_0 = new Func< FtpListItem, bool >( Class23.Class35.class35_0.method_0 ) ) ).Select< FtpListItem, string >( Class23.Class35.func_1 ?? ( Class23.Class35.func_1 = new Func< FtpListItem, string >( Class23.Class35.class35_0.method_1 ) ) ).SelectMany< string, string >( new Func< string, IEnumerable< string > >( class37.method_0 ) ).ToArray< string >( );
            this.dateTime_0 = DateTime.Today;
            this.string_2 = ( ( IEnumerable< string > ) this.string_1 ).Where< string >( Class23.Class35.func_2 ?? ( Class23.Class35.func_2 = new Func< string, bool >( Class23.Class35.class35_0.method_2 ) ) ).ToArray< string >( );
            this.string_3 = ( ( IEnumerable< string > ) this.string_1 ).Where< string >( Class23.Class35.func_3 ?? ( Class23.Class35.func_3 = new Func< string, bool >( Class23.Class35.class35_0.method_3 ) ) ).ToArray< string >( );
        }
        this.string_1 = !bool_1 ? this.string_2 : this.string_3;
        string str = this.method_10( class37.ftpClient_0, dateTime_1, bool_1 );
        Dictionary< Security, List< ExecutionMessage > > dictionary = new Dictionary< Security, List< ExecutionMessage > >( );
        if( !str.IsEmpty( ) )
        {
            this.method_11( isecurityStorage_0, ( IDictionary< Security, List< ExecutionMessage > > ) dictionary, str, dateTime_1, bool_1 );
            this.method_12( isecurityStorage_0, ( IDictionary< Security, List< ExecutionMessage > > ) dictionary, str );
        }
        return ( IDictionary< Security, List< ExecutionMessage > > ) dictionary;
    }

    private string method_10( System.Net.FtpClient.FtpClient ftpClient_0, DateTime dateTime_1, bool bool_1 )
    {
        Class23.Class33 class33 = new Class23.Class33( );
        class33.string_0 = "{0:00}{1:00}{2:00}".Put( ( object ) ( dateTime_1.Year - 2000 ), ( object ) dateTime_1.Month, ( object ) dateTime_1.Day );
        string str1 = ( ( IEnumerable< string > ) this.string_1 ).FirstOrDefault< string >( new Func< string, bool >( class33.method_0 ) );
        if( str1.IsEmpty( ) )
        {
            return ( string )null;
        }

        if( bool_1 )
        {
            int num = this.string_1.IndexOf< string >( str1 );
            if( num >= this.string_1.Length - 1 )
            {
                return ( string )null;
            }

            str1 = this.string_1[ num + 1 ];
            class33.string_0 = Path.GetFileNameWithoutExtension( str1 ).Substring( 2, 6 );
        }
        string dumpFile = this.method_0( ).GetDumpFile( ( Security ) null, dateTime_1, dateTime_1, typeof( ExecutionMessage ), ( object ) ExecutionTypes.Tick );
        string string_1 = Path.Combine( dumpFile, "txt" );
        Directory.CreateDirectory( dumpFile );
        string str2 = Path.Combine( dumpFile, ( ( Equatable< ExchangeBoard > ) this.method_5( ) == ExchangeBoard.Ux ? string.Empty : ( bool_1 ? "TE" : "FT" ) ) + class33.string_0 + ".zip" );
        if( !File.Exists( str2 ) )
        {
            ftpClient_0.smethod_1( this.method_3( ), str1, str2 );
        }

        str2.smethod_3( string_1 );
        return string_1;
    }

    private void method_11(
    ISecurityStorage isecurityStorage_0,
    IDictionary< Security, List< ExecutionMessage > > idictionary_0,
    string string_4,
    DateTime dateTime_1,
    bool bool_1 )
    {
        Class23.Class36 class36 = new Class23.Class36( );
        class36.bool_0 = bool_1;
        string[] array1 = ( ( IEnumerable< string > ) Directory.GetFiles( string_4, "*.csv" ) ).Where< string >( new Func< string, bool >( class36.method_0 ) ).ToArray< string >( );
        HashSet< Security > securitySet = new HashSet< Security >( );
        foreach( string path in array1 )
        {
            string withoutExtension = Path.GetFileNameWithoutExtension( path );
            if( withoutExtension != null )
            {
                string lowerInvariant = withoutExtension.ToLowerInvariant( );
                SecurityTypes securityTypes1;
                if( !class36.bool_0 )
                {
                    if( lowerInvariant.EndsWith( "ft" ) )
                    {
                        securityTypes1 = SecurityTypes.Future;
                    }
                    else if( lowerInvariant.EndsWith( "ot" ) )
                    {
                        securityTypes1 = SecurityTypes.Option;
                    }
                    else
                    {
                        throw new InvalidOperationException( LocalizedStrings.Str2113Params.Put( ( object )path ) );
                    }
                }
                else if( lowerInvariant.EndsWith( "fe" ) )
                {
                    securityTypes1 = SecurityTypes.Future;
                }
                else if( lowerInvariant.EndsWith( "oe" ) )
                {
                    securityTypes1 = SecurityTypes.Option;
                }
                else
                {
                    throw new InvalidOperationException( LocalizedStrings.Str2113Params.Put( ( object )path ) );
                }

                using( StreamReader streamReader = new StreamReader( path ) )
                {
                    string str1 = streamReader.ReadLine( );
                    if( str1 == null )
                    {
                        throw new InvalidOperationException( LocalizedStrings.Str2107Params.Put( ( object )path ) );
                    }

                    string[ ] array2 = str1.Split( ';' );
                    int index1 = array2.IndexOf< string >( "code" );
                    int index2 = array2.IndexOf< string >( "contract" );
                    int index3 = array2.IndexOf< string >( "price" );
                    int index4 = array2.IndexOf< string >( "amount" );
                    int index5 = array2.IndexOf< string >( "dat_time" );
                    int index6 = array2.IndexOf< string >( "trade_id" );
                    int index7 = array2.IndexOf< string >( "Nosystem" );
                    string str2;
                    while( ( str2 = streamReader.ReadLine( ) ) != null )
                    {
                        string[] strArray = str2.Split( ';' );
                        string secCode = strArray[ index1 ];
                        string str2_1 = strArray[ index2 ];
                        Decimal num1 = strArray[ index3 ].RemoveTrailingZeros( ).To< Decimal >( );
                        Decimal num2 = strArray[ index4 ].To< Decimal >( );
                        DateTimeOffset dateTimeOffset = strArray[ index5 ].ToDateTime( "yyyy-MM-dd HH\\:mm\\:ss.fff", ( CultureInfo ) null ).ApplyTimeZone( this.method_2( ) );
                        bool flag1 = index7 >= 0 && ( strArray[ index7 ] != "0" && !strArray[ index7 ].IsEmpty( ) );
                        if( !( this.method_7( ) & flag1 ) )
                        {
                            long num3 = strArray[ index6 ].To< long >( );
                            if( str2_1 == string.Empty && dateTime_1 == new DateTime( 2008, 9, 15 ) )
                            {
                                if( num3 != 1453121L )
                                {
                                    switch( num3 - 1453187L )
                                    {
                                        case 0:
                                            secCode = "SV12.5L8";
                                            str2_1 = "SILV-12.08_121208CA 12.5";
                                            break;
                                        case 1:
                    case 2:
                                            throw new InvalidOperationException( LocalizedStrings.Str2114Params.Put( ( object ) num3 ) );
                                        case 3:
                                            secCode = "SV11X8";
                                            str2_1 = "SILV-12.08_121208PA 11";
                                            break;
                                        case 4:
                                            secCode = "SV11L8";
                                            str2_1 = "SILV-12.08_121208CA 11";
                                            break;
                                        default:
                                            if( num3 == 1453308L )
                                            {
                                                secCode = "VB3500X8";
                                                str2_1 = "VTBR-12.08_101208PA 3500";
                                                break;
                                            }
                                            goto case 1;
                                    }
                                }
                                else
                                {
                                    secCode = "RI120000X8";
                                    str2_1 = "RTS-12.08_121208PA 120000";
                                }
                            }
                            string id = this.method_1( ).GenerateId( secCode, this.method_5( ) );
                            Security security = isecurityStorage_0.LookupById( id );
                            if( security == null )
                            {
                                security = new Security( )
                {
                  Id = id,
                  Code = secCode,
                  Name = str2_1,
                  Board = this.method_5( ),
                  Type = new SecurityTypes?( securityTypes1 )
                };
                                isecurityStorage_0.Save( security, false );
                                securitySet.Add( security );
                            }
                            else
                            {
                                bool flag2 = false;
                                SecurityTypes? type = security.Type;
                                SecurityTypes securityTypes2 = securityTypes1;
                                if( !( type.GetValueOrDefault( ) == securityTypes2 & type.HasValue ) )
                                {
                                    security.Type = new SecurityTypes?( securityTypes1 );
                                    flag2 = true;
                                }
                                if( !security.Name.CompareIgnoreCase( str2_1 ) )
                                {
                                    security.Name = str2_1;
                                    flag2 = true;
                                }
                                if( security.PriceStep.HasValue )
                                {
                                    Decimal? nullable = security.PriceStep;
                                    Decimal num4 = new Decimal( );
                                    if( !( nullable.GetValueOrDefault( ) == num4 & nullable.HasValue ) )
                                    {
                                        Decimal num5 = num1;
                                        Decimal? priceStep = security.PriceStep;
                                        nullable = priceStep.HasValue ? new Decimal?( num5 % priceStep.GetValueOrDefault( ) ) : new Decimal?( );
                                        Decimal num6 = new Decimal( );
                                        if( nullable.GetValueOrDefault( ) == num6 & nullable.HasValue )
                                        {
                                            goto label_35;
                                        }
                                    }
                                }
                                flag2 = true;
                                label_35:
if( flag2 )
{
    securitySet.Add( security );
}
                            }
                            idictionary_0.SafeAdd< Security, List< ExecutionMessage > >( security ).Add( new ExecutionMessage( )
              {
                ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick ),
                TradeId = new long?( num3 ),
                SecurityId = security.ToSecurityId( this.method_1( ) ),
                TradePrice = new Decimal?( num1 ),
                ServerTime = dateTimeOffset,
                TradeVolume = new Decimal?( num2 ),
                IsSystem = new bool?( !flag1 )
              } );
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException( LocalizedStrings.Str2113Params.Put( ( object )path ) );
            }
        }
        foreach( KeyValuePair< Security, List< ExecutionMessage > > keyValuePair in ( IEnumerable< KeyValuePair< Security, List< ExecutionMessage > > > ) idictionary_0 )
        {
            keyValuePair.Value.Sort( new Comparison< ExecutionMessage >( Class23.smethod_0 ) );
        }

        Class23.smethod_1( isecurityStorage_0, ( IEnumerable< Security > ) securitySet, idictionary_0 );
    }

    private static int smethod_0(
    ExecutionMessage executionMessage_0,
    ExecutionMessage executionMessage_1 )
    {
        long? tradeId1 = executionMessage_0.TradeId;
        long? tradeId2 = executionMessage_1.TradeId;
        if( tradeId1.GetValueOrDefault( ) == tradeId2.GetValueOrDefault( ) & tradeId1.HasValue == tradeId2.HasValue )
        {
            return 0;
        }

        if( executionMessage_0.ServerTime == executionMessage_1.ServerTime )
        {
            long? tradeId3 = executionMessage_0.TradeId;
            long? tradeId4 = executionMessage_1.TradeId;
            return !( tradeId3.GetValueOrDefault( ) > tradeId4.GetValueOrDefault( ) & ( tradeId3.HasValue & tradeId4.HasValue ) ) ? -1 : 1;
        }
        return !( executionMessage_0.ServerTime > executionMessage_1.ServerTime ) ? -1 : 1;
    }

    private void method_12(
    ISecurityStorage isecurityStorage_0,
    IDictionary< Security, List< ExecutionMessage > > idictionary_0,
    string string_4 )
    {
        HashSet< Security > securitySet = new HashSet< Security >( );
        foreach( string file in Directory.GetFiles( string_4, "*.xls" ) )
        {
            using( ExcelWorker excelWorker = new ExcelWorker( file, true ) )
            {
                string[] strArray = new string[2]
        {
          "futures_trades",
          "options_trades"
        };
                foreach( string sheetName in strArray )
                {
                    if( excelWorker.ContainsSheet( sheetName ) )
                    {
                        excelWorker.SwitchSheet( sheetName );
                        List< string > stringList = new List< string >( );
                        for( int col = 0; col < excelWorker.GetColumnsCount( ); ++col )
                        {
                            stringList.Add( excelWorker.GetCell< string >( col, 0 ) );
                        }

                        int col1 = stringList.IndexOf( "dat_time" );
                        int col2 = stringList.IndexOf( "code" );
                        int col3 = stringList.IndexOf( "contract" );
                        int col4 = stringList.IndexOf( "price" );
                        int col5 = stringList.IndexOf( "amount" );
                        int col6 = stringList.IndexOf( "trade_id" );
                        int rowsCount = excelWorker.GetRowsCount( );
                        SecurityTypes securityTypes1 = sheetName == "futures_trades" ? SecurityTypes.Future : SecurityTypes.Option;
                        for( int row = 1; row < rowsCount; ++row )
                        {
                            DateTimeOffset dateTimeOffset = excelWorker.GetCell< DateTime >( col1, row ).ApplyTimeZone( this.method_2( ) );
                            string cell1 = excelWorker.GetCell< string >( col2, row );
                            string cell2 = excelWorker.GetCell< string >( col3, row );
                            Decimal cell3 = excelWorker.GetCell< Decimal >( col4, row );
                            Decimal cell4 = excelWorker.GetCell< Decimal >( col5, row );
                            long num1 = col6 == -1 ? 111L : excelWorker.GetCell< long >( col6, row );
                            string id = this.method_1( ).GenerateId( cell1, this.method_5( ) );
                            Security security = isecurityStorage_0.LookupById( id );
                            if( security == null )
                            {
                                security = new Security( )
                {
                  Id = id,
                  Code = cell1,
                  Name = cell2,
                  Type = new SecurityTypes?( securityTypes1 ),
                  Board = this.method_5( )
                };
                                isecurityStorage_0.Save( security, false );
                                securitySet.Add( security );
                            }
                            else
                            {
                                bool flag = false;
                                SecurityTypes? type = security.Type;
                                SecurityTypes securityTypes2 = securityTypes1;
                                if( !( type.GetValueOrDefault( ) == securityTypes2 & type.HasValue ) )
                                {
                                    security.Type = new SecurityTypes?( securityTypes1 );
                                    flag = true;
                                }
                                if( !security.Name.CompareIgnoreCase( cell2 ) )
                                {
                                    security.Name = cell2;
                                    flag = true;
                                }
                                if( security.PriceStep.HasValue )
                                {
                                    Decimal? nullable = security.PriceStep;
                                    Decimal num2 = new Decimal( );
                                    if( !( nullable.GetValueOrDefault( ) == num2 & nullable.HasValue ) )
                                    {
                                        Decimal num3 = cell3;
                                        Decimal? priceStep = security.PriceStep;
                                        nullable = priceStep.HasValue ? new Decimal?( num3 % priceStep.GetValueOrDefault( ) ) : new Decimal?( );
                                        Decimal num4 = new Decimal( );
                                        if( nullable.GetValueOrDefault( ) == num4 & nullable.HasValue )
                                        {
                                            goto label_18;
                                        }
                                    }
                                }
                                flag = true;
                                label_18:
if( flag )
{
    securitySet.Add( security );
}
                            }
                            idictionary_0.SafeAdd< Security, List< ExecutionMessage > >( security ).Add( new ExecutionMessage( )
              {
                ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick ),
                TradeId = new long?( num1 ),
                SecurityId = security.ToSecurityId( this.method_1( ) ),
                TradePrice = new Decimal?( cell3 ),
                ServerTime = dateTimeOffset,
                TradeVolume = new Decimal?( cell4 )
              } );
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException( LocalizedStrings.Str2115Params.Put( ( object )sheetName, ( object )file ) );
                    }
                }
            }
        }
        Class23.smethod_1( isecurityStorage_0, ( IEnumerable< Security > ) securitySet, idictionary_0 );
    }

    private static void smethod_1(
    ISecurityStorage isecurityStorage_0,
    IEnumerable< Security > ienumerable_0,
    IDictionary< Security, List< ExecutionMessage > > idictionary_0 )
    {
        foreach( Security security in ienumerable_0 )
        {
            security.PriceStep = new Decimal?( Class23.smethod_2( security, idictionary_0 ) );
            isecurityStorage_0.Save( security, false );
        }
    }

    private static Decimal smethod_2(
    Security security_0,
    IDictionary< Security, List< ExecutionMessage > > idictionary_0 )
    {
        SecurityTypes? type = security_0.Type;
        if( type.GetValueOrDefault( ) == SecurityTypes.Future & type.HasValue && security_0.Name.ContainsIgnoreCase( "RTS-" ) )
        {
            return new Decimal( 5 );
        }

        if( security_0.Name.ContainsIgnoreCase( "GOLD-" ) )
        {
            return new Decimal( 1, 0, 0, false, ( byte )1 );
        }

        if( security_0.Name.ContainsIgnoreCase( "MIBR-" ) )
        {
            return new Decimal( 1, 0, 0, false, ( byte )2 );
        }

        if( security_0.Name.ContainsIgnoreCase( "RTSc-" ) )
        {
            return new Decimal( 5, 0, 0, false, ( byte )2 );
        }

        if( security_0.Name.ContainsIgnoreCase( "RTSo-" ) )
        {
            return new Decimal( 5, 0, 0, false, ( byte )2 );
        }

        if( security_0.Id == "URH7@FORTS" )
        {
            return new Decimal( 1, 0, 0, false, ( byte )2 );
        }

        int[ ] array = idictionary_0[ security_0 ].Where< ExecutionMessage >( Class23.Class35.func_4 ?? ( Class23.Class35.func_4 = new Func< ExecutionMessage, bool >( Class23.Class35.class35_0.method_4 ) ) ).Select< ExecutionMessage, int >( Class23.Class35.func_5 ?? ( Class23.Class35.func_5 = new Func< ExecutionMessage, int >( Class23.Class35.class35_0.method_5 ) ) ).ToArray< int >( );
        if( array.IsEmpty< int >( ) )
        {
            return new Decimal( 1, 0, 0, false, ( byte )2 );
        }

        return ( ( IEnumerable< int > ) array ).Max( ).GetPriceStep( );
    }

    private sealed class Class33
    {
        public string string_0;

        internal bool method_0( string string_1 )
        {
            return string_1.ContainsIgnoreCase( this.string_0 );
        }
    }

    private sealed class Class34
    {
        public string string_0;

        internal string method_0( FtpListItem ftpListItem_0 )
        {
            return this.string_0 + "/" + ftpListItem_0.Name;
        }
    }

    [Serializable]
  private sealed class Class35
  {
      public static readonly Class23.Class35 class35_0 = new Class23.Class35( );
      public static Func< FtpListItem, bool > func_0;
      public static Func< FtpListItem, string > func_1;
      public static Func< string, bool > func_2;
      public static Func< string, bool > func_3;
      public static Func< ExecutionMessage, bool > func_4;
      public static Func< ExecutionMessage, int > func_5;

      internal bool method_0( FtpListItem ftpListItem_0 )
      {
          return ftpListItem_0.Type == FtpFileSystemObjectType.Directory;
      }

      internal string method_1( FtpListItem ftpListItem_0 )
      {
          return ftpListItem_0.Name;
      }

      internal bool method_2( string string_0 )
      {
          return string_0.ContainsIgnoreCase( "FT" );
      }

      internal bool method_3( string string_0 )
      {
          return string_0.ContainsIgnoreCase( "TE" );
      }

      internal bool method_4( ExecutionMessage executionMessage_0 )
      {
          bool? isSystem = executionMessage_0.IsSystem;
          return !( !isSystem.GetValueOrDefault( ) & isSystem.HasValue );
      }

      internal int method_5( ExecutionMessage executionMessage_0 )
      {
          return executionMessage_0.TradePrice.Value.GetDecimalInfo( ).EffectiveScale;
      }
  }

    private sealed class Class36
    {
        public bool bool_0;

        internal bool method_0( string string_0 )
        {
            if( !this.bool_0 )
            {
                if( !string_0.ContainsIgnoreCase( "ft" ) )
                {
                    return string_0.ContainsIgnoreCase( "ot" );
                }

                return true;
            }
            if( !string_0.ContainsIgnoreCase( "fe" ) )
            {
                return string_0.ContainsIgnoreCase( "oe" );
            }

            return true;
        }
    }

    private sealed class Class37
    {
        public System.Net.FtpClient.FtpClient ftpClient_0;
        public Class23 class23_0;
        public Func< FtpListItem, bool > func_0;

        internal IEnumerable< string > method_0( string string_0 )
        {
            Class23.Class34 class34 = new Class23.Class34( );
            class34.string_0 = string_0;
            return ( ( IEnumerable< FtpListItem > ) this.ftpClient_0.GetListing( this.class23_0.method_3( ) + "/" + class34.string_0 ) ).Where< FtpListItem >( this.func_0 ?? ( this.func_0 = new Func< FtpListItem, bool >( this.method_1 ) ) ).Select< FtpListItem, string >( new Func< FtpListItem, string >( class34.method_0 ) );
        }

        internal bool method_1( FtpListItem ftpListItem_0 )
        {
            if( !ftpListItem_0.Name.ContainsIgnoreCase( ".zip" ) )
            {
                return false;
            }

            if( !( ( Equatable< ExchangeBoard > ) this.class23_0.method_5( ) == ExchangeBoard.Ux ) && !ftpListItem_0.Name.ContainsIgnoreCase( "FT" ) )
            {
                return ftpListItem_0.Name.ContainsIgnoreCase( "TE" );
            }

            return true;
        }
    }
}
