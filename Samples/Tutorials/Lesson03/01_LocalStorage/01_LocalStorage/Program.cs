// ------------------------ Security --------------------------------------
using Ecng.Common;
using Ecng.Compilation;
using Ecng.Compilation.Roslyn;
using Ecng.Configuration;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Expressions;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Storages.Csv;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Logging;
using StockSharp.Messages;

var pathHistory = @"U:\ForexData";
var localDrive = new LocalMarketDataDrive( pathHistory );

var securities = localDrive.AvailableSecurities;

foreach( var security in securities )
{
    Console.WriteLine( security );
}


var mySecurity = new Security
{
    Id = "SPX500@FXCM",
    Code = "SPX500",
    Board = ExchangeBoard.Fxcm
};

var storageRegistry = new StorageRegistry
{
    DefaultDrive = localDrive,
};

var candleStorage = storageRegistry.GetCandleStorage( typeof( TimeFrameCandle ), mySecurity, TimeSpan.FromMinutes( 15 ) );

var candles = candleStorage.Load( new DateTime( 2022, 1, 1 ), new DateTime( 2022, 2, 1 ) );

foreach ( var candle in candles )
{
    Console.WriteLine( candle );
}


// The trades will be store in the Security Each date's folder with a filename of
//+++ trades.bin
var tradesStorage = storageRegistry.GetTradeStorage( mySecurity, format: StorageFormats.Binary );

var trades = tradesStorage.Load( new DateTime( 2022, 1, 1 ), new DateTime( 2022, 2, 1 ) );

foreach ( var trade in trades )
{
    Console.WriteLine( trade );
}

// The market Detph will be store in the Security Each date's folder with a filename of
//+++ quotes.bin
var marketDepthStorage = storageRegistry.GetMarketDepthStorage( mySecurity, format: StorageFormats.Binary );

var depths = tradesStorage.Load( new DateTime( 2022, 1, 1 ), new DateTime( 2022, 2, 1 ) );

foreach ( var depth in depths )
{
    Console.WriteLine( depth );
}

var secId = MessageConverterHelper.ToSecurityId( mySecurity );

// The market Detph will be store in the Security Each date's folder with a filename of
//+++ security.bin
var levelOneStorage = storageRegistry.GetLevel1MessageStorage( secId, format: StorageFormats.Binary );

var levelOnes = levelOneStorage.Load( new DateTime( 2022, 1, 1 ), new DateTime( 2022, 2, 1 ) );

foreach ( var levelOne in levelOnes )
{
    Console.WriteLine( levelOne );
}

//+++ ------------Index--------------------
ConfigManager.RegisterService<ICompilerService>( new RoslynCompilerService() );


var indexSecurity = new ExpressionIndexSecurity()
{
    Id = "IndexSec@FXCM",
    Code = "IndexSec",
    Expression = "SPX500@FXCM*2",
    Board = ExchangeBoard.Fxcm
};



var basketStorage = new BasketMarketDataStorage<CandleMessage>( );

candleStorage = storageRegistry.GetCandleStorage( typeof( TimeFrameCandle ), mySecurity, TimeSpan.FromMinutes( 5 ) );

basketStorage.InnerStorages.Add( candleStorage, 0 );

var candleIndex = basketStorage.Load( new DateTime( 2022, 1, 1 ), new DateTime( 2022, 2, 1 ) );

foreach ( var icandle in candleIndex )
{
    Console.WriteLine( icandle );
}

Console.ReadLine();

//public class SimpleConnector 
//{
//    private readonly string _defaultDataPath = "Data";
//    private static string _settingsFile = $"connection{Paths.DefaultSettingsExt}";
//    private Connector _connector;
//    private CandleSeries _candleSeries;    
//    private string _pathHistory = @"U:\ForexData";

//    public SimpleConnector()
//    {        
//        _defaultDataPath = _defaultDataPath.ToFullPath();

//        _settingsFile = System.IO.Path.Combine( _defaultDataPath, $"connection{Paths.DefaultSettingsExt}" );
//    }

//    private void Setup( )
//    {
//        InitConnectorAndLog();

//        SetConnectorProperties();
//    }

//    private void InitConnectorAndLog()
//    {
//        var entityRegistry = new CsvEntityRegistry( Paths.AppDataPath );
//        var exchangeInfoProvider = new StorageExchangeInfoProvider( entityRegistry, false );


//        var storageRegistry = new StorageRegistry( exchangeInfoProvider )
//        {
//            DefaultDrive = new LocalMarketDataDrive( _pathHistory )
//        };


//        INativeIdStorage nativeIdStorage = new CsvNativeIdStorage( System.IO.Path.Combine( Paths.AppDataPath, "NativeId" ) )
//        {
//            DelayAction = entityRegistry.DelayAction
//        };

//        ConfigManager.RegisterService<IExchangeInfoProvider>( exchangeInfoProvider );
//        ConfigManager.RegisterService<IEntityRegistry>( entityRegistry );
//        ConfigManager.RegisterService<IStorageRegistry>( storageRegistry );
//        ConfigManager.RegisterService( nativeIdStorage );

//        var snapshotRegistry = new SnapshotRegistry( System.IO.Path.Combine( Paths.AppDataPath, "Snapshots" ) );

//        _connector = new Connector( entityRegistry.Securities, entityRegistry.PositionStorage, exchangeInfoProvider, storageRegistry, snapshotRegistry, new StorageBuffer() );

//        /* ------------------------------------------------------------------------------------------------------------------------------------------
//         * 
//         *  Tony:   Inside InMemoryMessageAdapterProvider constructor, it calls GetAdapters which will load all the StockSharp.XXXXX adapters dlls
//         *          from Assemblies.
//         *          
//         *          So after deserialize of the SettingsStorage, our added adapter (FXCMConnect) can be finally instantiated
//         *          Or else we won't see the adapter even though the settings are stored.
//         * ------------------------------------------------------------------------------------------------------------------------------------------
//         */
//        ConfigManager.RegisterService<IMessageAdapterProvider>( new FullInMemoryMessageAdapterProvider( _connector.Adapter.InnerAdapters ) );

//        try
//        {
//            if ( _settingsFile.IsConfigExists() )
//            {
//                var ctx = new ContinueOnExceptionContext();
//                ctx.Error += ex => ex.LogError();

//                var setting = _settingsFile.Deserialize<SettingsStorage>();

//                using ( ctx.ToScope() )
//                {
//                    _connector.LoadIfNotNull( setting );
//                }
//            }
//        }
//        catch
//        {

//        }

//        var logManager = new LogManager();
//        logManager.Listeners.Add( new FileLogListener { LogDirectory = System.IO.Path.Combine( Paths.AppDataPath, "Logs" ) } );

//        logManager.Sources.Add( _connector );
//    }

//    private void SetConnectorProperties()
//    {
//        _connector.Adapter.SupportLookupTracking = false;
//        _connector.Adapter.IsSupportTransactionLog = false;
//        _connector.Adapter.IsSupportOrderBookSort = false;
//        _connector.Adapter.Level1Extend = false;
//        _connector.Adapter.SupportPartialDownload = false;
//        _connector.Adapter.SupportBuildingFromOrderLog = false;
//        _connector.Adapter.SupportOrderBookTruncate = false;
//        _connector.Adapter.SupportCandlesCompression = false;

//        if ( _connector.StorageAdapter != null )
//        {
//            _connector.Adapter.StorageSettings.DaysLoad = TimeSpan.FromDays( 30 );
//        }

//        _connector.SupportBasketSecurities = true;

//        //_connector.CandleSeriesProcessing += Connector_CandleSeriesProcessing;
//    }

    

    
//}