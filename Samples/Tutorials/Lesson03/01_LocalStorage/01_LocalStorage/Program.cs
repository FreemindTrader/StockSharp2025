// ------------------------ Security --------------------------------------
using Ecng.Compilation;
using Ecng.Compilation.Roslyn;
using Ecng.Configuration;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Expressions;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
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

var basketStorage = new IndexSecurityMarketDataStorage<CandleMessage>( indexSecurity, TimeSpan.FromMinutes( 5 ) ); 

Console.ReadLine();
