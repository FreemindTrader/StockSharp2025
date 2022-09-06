using Ecng.Collections;
using Ecng.Common;
using MoreLinq;
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

internal sealed class RtsInternalClass : Class21
{
    private DateTime dateTime_0;
    private string[ ] string_1;
    private readonly ExchangeBoard exchangeBoard_1;
    private bool _stdTrades;
    private bool _rtsStdCombinedOnly;

    public RtsInternalClass( RtsHistorySource rtsHistorySource_1 ) : base( rtsHistorySource_1 )
    {
        this.exchangeBoard_1 = StockSharp.BusinessEntities.ExchangeBoard.Forts;
    }

    public bool GetStdTrades()
    {
        return this._stdTrades;
    }

    public void SetStdTrades( bool bool_3 )
    {
        this._stdTrades = bool_3;
    }

    public bool GetSaveRtsStdCombinedOnly()
    {
        return this._rtsStdCombinedOnly;
    }

    public void SetSaveRtsStdCombinedOnly( bool bool_3 )
    {
        this._rtsStdCombinedOnly = bool_3;
    }

    public IDictionary<Security, List<ExecutionMessage>> method_13( ISecurityStorage storage, System.Net.FtpClient.FtpClient ftp, DateTime dateTime_1 )
    {
        if ( this.dateTime_0.Date != DateTime.Today )
        {
            this.string_1 = ftp.GetListing( this.GetDirectory() ).Select<FtpListItem, string>( RtsInternalClass.Class20.func_0 ?? ( RtsInternalClass.Class20.func_0 = new Func<FtpListItem, string>( RtsInternalClass.Class20.class20_0.method_0 ) ) ).ToArray<string>();
            this.dateTime_0 = DateTime.Today;
        }
        Dictionary<Security, List<ExecutionMessage>> dictionary_0 = new Dictionary<Security, List<ExecutionMessage>>();
        bool bool_3;
        string str = this.method_14( ftp, dateTime_1, out bool_3 );
        if ( str.IsEmpty() )
        {
            return dictionary_0;
        }

        this.method_15( bool_3, storage, str, dictionary_0, dateTime_1 );
        if ( !bool_3 )
        {
            this.method_17( storage, dictionary_0, dateTime_1.ApplyTimeZone( this.ExchangeBoard().TimeZone ) );
        }

        return dictionary_0;
    }

    private string method_14( System.Net.FtpClient.FtpClient ftpClient_0, DateTime dateTime_1, out bool bool_3 )
    {
        RtsInternalClass.Class18 class18 = new RtsInternalClass.Class18();
        class18.ftpClient_0 = ftpClient_0;
        string path2 = "{0}{1:00}{2:00}".Put( dateTime_1.Year, dateTime_1.Month, dateTime_1.Day );
        class18.string_0 = path2 + ".zip";
        class18.string_1 = path2 + "_csv.zip";
        string string_1 = this.string_1.FirstOrDefault<string>( new Func<string, bool>( class18.method_0 ) );
        if ( string_1 == null && !this.string_1.Contains<string>( path2 ) )
        {
            bool_3 = false;
            return null;
        }
        string dumpFile = this.method_0().GetDumpFile( null, dateTime_1, dateTime_1, typeof( ExecutionMessage ), ExecutionTypes.Tick );
        class18.string_3 = Path.Combine( dumpFile, "dbf" );
        string str1 = Path.Combine( dumpFile, "csv" );
        class18.string_2 = Path.Combine( dumpFile, class18.string_0 );
        string str2 = Path.Combine( dumpFile, class18.string_1 );
        Directory.CreateDirectory( dumpFile );
        if ( File.Exists( str2 ) )
        {
            str2.smethod_3( str1 );
            bool_3 = true;
            return str1;
        }
        if ( File.Exists( class18.string_2 ) )
        {
            class18.string_2.smethod_3( class18.string_3 );
            bool_3 = false;
            return class18.string_3;
        }
        if ( string_1 != null )
        {
            class18.ftpClient_0.smethod_1( this.GetDirectory(), string_1, class18.string_2 ).smethod_4( class18.string_3 );
        }
        else
        {
            RtsInternalClass.Class17 class17 = new RtsInternalClass.Class17();
            class17.class18_0 = class18;
            class17.string_0 = Path.Combine( this.GetDirectory(), path2 );
            FtpListItem[ ] listing = class17.class18_0.ftpClient_0.GetListing( class17.string_0 );
            FtpListItem[ ] array = listing.Where<FtpListItem>( RtsInternalClass.Class20.func_1 ?? ( RtsInternalClass.Class20.func_1 = new Func<FtpListItem, bool>( RtsInternalClass.Class20.class20_0.method_1 ) ) ).ToArray<FtpListItem>();
            FtpListItem ftpListItem1 = listing.FirstOrDefault<FtpListItem>( new Func<FtpListItem, bool>( class17.class18_0.method_1 ) );
            FtpListItem ftpListItem2 = listing.FirstOrDefault<FtpListItem>( new Func<FtpListItem, bool>( class17.class18_0.method_2 ) );
            if ( ftpListItem1 != null )
            {
                class17.class18_0.ftpClient_0.smethod_1( class17.string_0, ftpListItem1.Name, str2 ).smethod_4( str1 );
                bool_3 = true;
                return str1;
            }
            if ( ftpListItem2 != null )
            {
                if ( array.Any<FtpListItem>( new Func<FtpListItem, bool>( class17.class18_0.method_3 ) ) )
                {
                    class17.class18_0.ftpClient_0.smethod_1( class17.string_0, ftpListItem2.Name, class17.class18_0.string_2 ).smethod_4( class17.class18_0.string_3 );
                }
            }
            else
            {
                Directory.CreateDirectory( class17.class18_0.string_3 );
                array.ForEach<FtpListItem>( new Action<FtpListItem>( class17.method_0 ) );
            }
        }
        bool_3 = false;
        return class18.string_3;
    }

    private void method_15(
    bool bool_3,
    ISecurityStorage isecurityStorage_0,
    string string_2,
    IDictionary<Security, List<ExecutionMessage>> idictionary_0,
    DateTime dateTime_1 )
    {
        Dictionary<string, Security> dict = new Dictionary<string, Security>( StringComparer.InvariantCultureIgnoreCase );
        string[ ] strArray;
        if ( !bool_3 )
        {
            strArray = new string[2] { "f07.dbf", "o07.dbf" };
        }
        else
        {
            strArray = new string[2] { "f07.csv", "o07.csv" };
        }

        foreach ( string str1 in strArray )
        {
            string str2 = Path.Combine( string_2, str1 );
            if ( !File.Exists( str2 ) )
            {
                str2 = Path.Combine( string_2, "day" + str1 );
                if ( !File.Exists( str2 ) )
                {
                    continue;
                }
            }
            bool flag1 = !str1.ContainsIgnoreCase( "o" );
            using ( Class16 class16 = new Class16( bool_3, str2 ) )
            {
                int index1 = class16.method_1( "CONTRACT" );
                int index2 = class16.method_1( "NAME" );
                int index3 = class16.method_1( "TICK" );
                int index4 = class16.method_1( "TICK_PRICE" );
                int index5 = flag1 ? class16.method_1( "LOT_VOLUME" ) : -1;
                int index6 = class16.method_1( "EXECUTION" );
                int index7 = flag1 ? class16.method_1( "SPOT" ) : -1;
                int index8 = flag1 ? class16.method_1( "BASE" ) : -1;
                int index9 = flag1 ? class16.method_1( "BASE_FUT" ) : -1;
                int index10 = !flag1 ? class16.method_1( "FUT_CONTR" ) : -1;
                int index11 = flag1 ? class16.method_1( "MULTILEG" ) : -1;
                object[ ] objArray;
                while ( ( objArray = class16.method_2() ) != null )
                {
                    if ( objArray[index3] != null && ( index11 <= -1 || !( objArray[index11].To<Decimal>() == Decimal.One ) ) )
                    {
                        string secCode = objArray[index2].To<string>().Trim();
                        string str2_1 = objArray[index1].To<string>().Trim();
                        DateTime? nullable1 = RtsInternalClass.smethod_0( objArray[index6].To<string>() );
                        string str2_2 = string.Empty;
                        SecurityTypes securityTypes1 = flag1 ? SecurityTypes.Future : SecurityTypes.Option;
                        bool flag2 = true;
                        if ( flag1 )
                        {
                            string str3 = index7 > -1 ? objArray[index7].To<string>().Trim() : string.Empty;
                            if ( index8 > -1 && str3 == "SPOT" )
                            {
                                str2_2 = this.method_1().GenerateId( index8 > -1 ? objArray[index8].To<string>().Trim() : string.Empty, this.ExchangeBoard() );
                                securityTypes1 = SecurityTypes.Stock;
                                secCode = str2_1;
                                flag2 = !this.GetSaveRtsStdCombinedOnly();
                                if ( str2_2 == "VTBR@RTS" )
                                {
                                    objArray[index3] = new Decimal( 1, 0, 0, false, 5 );
                                    objArray[index4] = new Decimal( 1, 0, 0, false, 2 );
                                }
                            }
                            else if ( index9 > -1 )
                            {
                                str2_2 = this.method_1().GenerateId( objArray[index9].To<string>().Trim(), this.ExchangeBoard() );
                            }
                        }
                        else if ( index10 > -1 )
                        {
                            string index12 = objArray[index10].To<string>().Trim();
                            str2_2 = dict[index12].Id;
                        }
                        string id = this.method_1().GenerateId( secCode, this.ExchangeBoard() );
                        Security security1 = isecurityStorage_0.LookupById( id );
                        int num1 = flag1 ? objArray[index5].To<int>() : 1;
                        Decimal num2 = objArray[index3].To<Decimal>().RemoveTrailingZeros();
                        if ( security1 == null )
                        {
                            security1 = new Security()
                            {
                                Id = id,
                                Code = secCode,
                                Name = str2_1,
                                UnderlyingSecurityId = str2_2,
                                Type = new SecurityTypes?( securityTypes1 ),
                                ExpiryDate = nullable1.HasValue ? new DateTimeOffset?( nullable1.GetValueOrDefault().ApplyTimeZone( this.method_2() ) ) : new DateTimeOffset?(),
                                Multiplier = new Decimal?( num1 ),
                                PriceStep = new Decimal?( num2 ),
                                Board = this.ExchangeBoard()
                            };
                            if ( flag2 )
                            {
                                isecurityStorage_0.Save( security1, false );
                            }
                        }
                        else
                        {
                            bool flag3 = false;
                            SecurityTypes? type = security1.Type;
                            SecurityTypes securityTypes2 = securityTypes1;
                            if ( !( type.GetValueOrDefault() == securityTypes2 & type.HasValue ) )
                            {
                                security1.Type = new SecurityTypes?( securityTypes1 );
                                flag3 = true;
                            }
                            if ( !security1.Name.CompareIgnoreCase( str2_1 ) )
                            {
                                security1.Name = str2_1;
                                flag3 = true;
                            }
                            if ( !security1.UnderlyingSecurityId.CompareIgnoreCase( str2_2 ) )
                            {
                                security1.UnderlyingSecurityId = str2_2;
                                flag3 = true;
                            }
                            DateTimeOffset? expiryDate = security1.ExpiryDate;
                            DateTime? nullable2 = nullable1;
                            DateTimeOffset? nullable3 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                            if ( ( expiryDate.HasValue == nullable3.HasValue ? ( expiryDate.HasValue ? ( expiryDate.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0 ) : 0 ) : 1 ) != 0 )
                            {
                                Security security2 = security1;
                                DateTimeOffset? nullable4;
                                if ( !nullable1.HasValue )
                                {
                                    nullable3 = new DateTimeOffset?();
                                    nullable4 = nullable3;
                                }
                                else
                                {
                                    nullable4 = new DateTimeOffset?( nullable1.GetValueOrDefault().ApplyTimeZone( this.method_2() ) );
                                }

                                security2.ExpiryDate = nullable4;
                                flag3 = true;
                            }
                            Decimal? nullable5 = security1.Multiplier;
                            Decimal num3 = num1;
                            if ( !( nullable5.GetValueOrDefault() == num3 & nullable5.HasValue ) )
                            {
                                security1.Multiplier = new Decimal?( num1 );
                                flag3 = true;
                            }
                            nullable5 = security1.PriceStep;
                            Decimal num4 = num2;
                            if ( !( nullable5.GetValueOrDefault() == num4 & nullable5.HasValue ) )
                            {
                                security1.PriceStep = new Decimal?( num2 );
                                flag3 = true;
                            }
                            if ( flag2 & flag3 )
                            {
                                isecurityStorage_0.Save( security1, false );
                            }
                        }
                        dict[str2_1] = security1;
                    }
                }
            }
        }
        if ( dict.Count == 0 )
        {
            return;
        }

        foreach ( string file in Directory.GetFiles( string_2, "*deal." + ( bool_3 ? "csv" : "dbf" ) ) )
        {
            if ( !file.ContainsIgnoreCase( "multileg" ) )
            {
                using ( Class16 class16 = new Class16( bool_3, file ) )
                {
                    int index1 = class16.method_1( "ISIN" );
                    int index2 = class16.method_1( "ID_DEAL" );
                    int index3 = class16.method_1( "VOL" );
                    int index4 = class16.method_1( "PRICE" );
                    int index5 = class16.method_1( "DATE" );
                    int index6 = class16.method_1( "TIME" );
                    int index7 = class16.method_1( "TYPE" );
                    object[ ] objArray;
                    while ( ( objArray = class16.method_2() ) != null )
                    {
                        Decimal num = objArray[index4].To<Decimal>().RemoveTrailingZeros();
                        if ( !( num == Decimal.Zero ) )
                        {
                            DateTime dt;
                            if ( bool_3 )
                            {
                                dt = objArray[index5].To<string>().ToDateTime( "dd.MM.yyyy", null ).Add( objArray[index6].To<string>().ToDateTime( "HH:mm:ss", null ).TimeOfDay );
                            }
                            else
                            {
                                dt = ( DateTime )objArray[index5];
                                dt = dt.Add( objArray[index6].To<DateTime>().TimeOfDay );
                            }
                            long long_0 = objArray[index2].To<long>();
                            if ( long_0 > 0L )
                            {
                                string str = objArray[index1].To<string>().Trim();
                                Security security = dict.TryGetValue<string, Security>( str ) ?? this.method_16( str, dict, isecurityStorage_0, dateTime_1, long_0 );
                                bool flag = objArray[index7].To<int>() == 0;
                                if ( !this.GetIsSystemOnly() || flag )
                                {
                                    idictionary_0.SafeAdd<Security, List<ExecutionMessage>>( security ).Add( new ExecutionMessage()
                                    {
                                        ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick ),
                                        TradeId = new long?( long_0 ),
                                        TradeVolume = new Decimal?( objArray[index3].To<Decimal>() ),
                                        TradePrice = new Decimal?( num ),
                                        ServerTime = dt.ApplyTimeZone( this.method_2() ),
                                        SecurityId = security.ToSecurityId( this.method_1() ),
                                        IsSystem = new bool?( flag )
                                    } );
                                }
                            }
                            else
                            {
                                throw new InvalidOperationException( LocalizedStrings.Str2108Params.Put( long_0 ) );
                            }
                        }
                    }
                }
            }
        }
    }

    private Security method_16(
    string string_2,
    IDictionary<string, Security> idictionary_0,
    ISecurityStorage isecurityStorage_0,
    DateTime dateTime_1,
    long long_0 )
    {
        Security security = isecurityStorage_0.Lookup( new Security()
        {
            Name = string_2
        } ).FirstOrDefault<Security>();
        if ( security != null )
        {
            idictionary_0.Add( string_2, security );
            return security;
        }
        if ( this.ExchangeBoard() == StockSharp.BusinessEntities.ExchangeBoard.Forts && dateTime_1 == new DateTime( 2007, 12, 28 ) )
        {
            string id = this.method_1().GenerateId( "GM55000O8", StockSharp.BusinessEntities.ExchangeBoard.Forts );
            security = isecurityStorage_0.LookupById( id );
            if ( security == null || ( !security.PriceStep.HasValue || !security.Name.CompareIgnoreCase( "GMKR-3.08_120308PA 55000" ) ) )
            {
                if ( security == null )
                {
                    security = new Security()
                    {
                        Id = id,
                        Code = "GM55000O8",
                        Board = this.ExchangeBoard()
                    };
                }

                security.Name = "GMKR-3.08_120308PA 55000";
                security.Type = new SecurityTypes?( SecurityTypes.Option );
                security.UnderlyingSecurityId = this.method_1().GenerateId( "GMH8", StockSharp.BusinessEntities.ExchangeBoard.Forts );
                security.ExpiryDate = new DateTimeOffset?( new DateTime( 2008, 3, 12 ).ApplyTimeZone( this.method_2() ) );
                security.Multiplier = new Decimal?( 1 );
                security.PriceStep = new Decimal?( 1 );
                isecurityStorage_0.Save( security, false );
            }
            idictionary_0.Add( security.Name, security );
        }
        if ( security == null )
        {
            throw new InvalidOperationException( LocalizedStrings.Str2109Params.Put( string_2, long_0 ) );
        }

        return security;
    }

    private static DateTime? smethod_0( string string_2 )
    {
        DateTime result;
        if ( DateTime.TryParseExact( string_2, "yyyy\\/MM\\/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result ) )
        {
            return new DateTime?( result );
        }

        if ( DateTime.TryParseExact( string_2, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result ) )
        {
            return new DateTime?( result );
        }

        return new DateTime?();
    }

    private void method_17(
    ISecurityStorage isecurityStorage_0,
    Dictionary<Security, List<ExecutionMessage>> dictionary_0,
    DateTimeOffset dateTimeOffset_0 )
    {
        if ( dictionary_0 == null )
        {
            throw new ArgumentNullException( "ticks" );
        }

        DateTimeOffset dateTimeOffset1 = this.exchangeBoard_1.AddOrSubtractTradingDays( dateTimeOffset_0, 4, true );
        foreach ( Security key in dictionary_0.Keys.Where<Security>( RtsInternalClass.Class20.func_2 ?? ( RtsInternalClass.Class20.func_2 = new Func<Security, bool>( RtsInternalClass.Class20.class20_0.method_2 ) ) ).ToArray<Security>() )
        {
            if ( !this.GetStdTrades() )
            {
                dictionary_0.Remove( key );
            }
            else
            {
                DateTimeOffset? expiryDate = key.ExpiryDate;
                DateTimeOffset dateTimeOffset2 = dateTimeOffset1;
                if ( ( expiryDate.HasValue ? ( expiryDate.HasValue ? ( expiryDate.GetValueOrDefault() == dateTimeOffset2 ? 1 : 0 ) : 1 ) : 0 ) != 0 )
                {
                    RtsInternalClass.Class19 class19 = new RtsInternalClass.Class19();
                    class19.class22_0 = this;
                    class19.security_0 = isecurityStorage_0.LookupById( key.UnderlyingSecurityId );
                    if ( class19.security_0 != null )
                    {
                        Decimal? priceStep1 = class19.security_0.PriceStep;
                        if ( priceStep1.HasValue )
                        {
                            priceStep1 = class19.security_0.PriceStep;
                            Decimal? priceStep2 = key.PriceStep;
                            if ( priceStep1.GetValueOrDefault() > priceStep2.GetValueOrDefault() & ( priceStep1.HasValue & priceStep2.HasValue ) )
                            {
                                throw new InvalidOperationException( LocalizedStrings.Str2110Params.Put( key.Id, key.PriceStep, class19.security_0.Id, class19.security_0.PriceStep ) );
                            }

                            goto label_12;
                        }
                    }
                    SecurityId securityId = this.method_1().Split( key.UnderlyingSecurityId, false );
                    if ( class19.security_0 == null )
                    {
                        class19.security_0 = new Security()
                        {
                            Id = key.UnderlyingSecurityId,
                            Code = securityId.SecurityCode,
                            Name = securityId.SecurityCode,
                            Board = this.ExchangeBoard()
                        };
                    }

                    class19.security_0.Type = key.Type;
                    class19.security_0.Multiplier = key.Multiplier;
                    class19.security_0.PriceStep = key.PriceStep;
                    isecurityStorage_0.Save( class19.security_0, false );
                label_12:
                    List<ExecutionMessage> executionMessageList = dictionary_0[key];
                    executionMessageList.RemoveWhere<ExecutionMessage>( new Func<ExecutionMessage, bool>( class19.method_0 ) );
                    dictionary_0.SafeAdd<Security, List<ExecutionMessage>>( class19.security_0 ).AddRange( executionMessageList.Select<ExecutionMessage, ExecutionMessage>( new Func<ExecutionMessage, ExecutionMessage>( class19.method_1 ) ) );
                }
                if ( this.GetSaveRtsStdCombinedOnly() )
                {
                    dictionary_0.Remove( key );
                }
            }
        }
    }

    private sealed class Class17
    {
        public string string_0;
        public RtsInternalClass.Class18 class18_0;

        internal void method_0( FtpListItem ftpListItem_0 )
        {
            string str = Path.Combine( this.class18_0.string_3, ftpListItem_0.Name );
            if ( File.Exists( str ) )
            {
                return;
            }

            this.class18_0.ftpClient_0.smethod_1( this.string_0, ftpListItem_0.Name, str );
        }
    }

    private sealed class Class18
    {
        public string string_0;
        public string string_1;
        public string string_2;
        public string string_3;
        public System.Net.FtpClient.FtpClient ftpClient_0;

        internal bool method_0( string string_4 )
        {
            return string_4.CompareIgnoreCase( this.string_0 );
        }

        internal bool method_1( FtpListItem ftpListItem_0 )
        {
            return ftpListItem_0.Name.CompareIgnoreCase( this.string_1 );
        }

        internal bool method_2( FtpListItem ftpListItem_0 )
        {
            return ftpListItem_0.Name.CompareIgnoreCase( this.string_0 );
        }

        internal bool method_3( FtpListItem ftpListItem_0 )
        {
            return !File.Exists( Path.Combine( this.string_2, ftpListItem_0.Name ) );
        }
    }

    private sealed class Class19
    {
        public Security security_0;
        public RtsInternalClass class22_0;

        internal bool method_0( ExecutionMessage executionMessage_0 )
        {
            Decimal? tradePrice = executionMessage_0.TradePrice;
            Decimal? priceStep = this.security_0.PriceStep;
            return ( tradePrice.HasValue & priceStep.HasValue ? new Decimal?( tradePrice.GetValueOrDefault() % priceStep.GetValueOrDefault() ) : new Decimal?() ).HasValue;
        }

        internal ExecutionMessage method_1( ExecutionMessage executionMessage_0 )
        {
            return new ExecutionMessage()
            {
                ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick ),
                TradeId = executionMessage_0.TradeId,
                SecurityId = this.security_0.ToSecurityId( this.class22_0.method_1() ),
                TradePrice = executionMessage_0.TradePrice,
                ServerTime = executionMessage_0.ServerTime,
                TradeVolume = executionMessage_0.TradeVolume
            };
        }
    }

    [Serializable]
    private sealed class Class20
    {
        public static readonly RtsInternalClass.Class20 class20_0 = new RtsInternalClass.Class20();
        public static Func<FtpListItem, string> func_0;
        public static Func<FtpListItem, bool> func_1;
        public static Func<Security, bool> func_2;

        internal string method_0( FtpListItem ftpListItem_0 )
        {
            return ftpListItem_0.Name;
        }

        internal bool method_1( FtpListItem ftpListItem_0 )
        {
            return ftpListItem_0.Name.ContainsIgnoreCase( ".dbf" );
        }

        internal bool method_2( Security security_0 )
        {
            SecurityTypes? type = security_0.Type;
            return type.GetValueOrDefault() == SecurityTypes.Stock & type.HasValue;
        }
    }
}
