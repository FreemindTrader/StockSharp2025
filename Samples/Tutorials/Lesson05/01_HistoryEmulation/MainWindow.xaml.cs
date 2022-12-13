using DevExpress.Xpo.Logger;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Logging;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01_HistoryEmulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Security _security;
        private Portfolio _portfolio;
        private string _pathHistory = @"U:\ForexData";
        private HistoryEmulationConnector _connector;
        private StockSharp.Logging.LogManager _logManager;
        private CandleSeries _candleSeries;

        private ChartCandleElement _candleElement;
        private ChartTradeElement _tradeElement;

        private CandleManager _candleManager;

        public MainWindow()
        {
            InitializeComponent();

            //_logManager = new StockSharp.Logging.LogManager();
            //_logManager.Listeners.Add( new FileLogListener( "log.txt" ) );
            //_logManager.Listeners.Add( Monitor );


            CandleSettingsEditor.Settings = new StockSharp.Algo.Candles.CandleSeries()
            {
                CandleType = typeof( TimeFrameCandle ),
                Arg = TimeSpan.FromMinutes( 1 )
            };

            DatePickerBegin.SelectedDate = new DateTime( 2022, 10, 1 );
            DatePickerEnd.SelectedDate = new DateTime( 2022, 10, 5 );
        }

        private void Start_Click( object sender, RoutedEventArgs e )
        {
            _security = new Security
            {
                Id = "SPX500@FXCM",
                Code = "SPX500",
                PriceStep = 1,
                Board = ExchangeBoard.Fxcm
            };

            _portfolio = new Portfolio()
            {
                Name = "Test Account",
                BeginValue = 150000
            };

            var storageRegistry = new StorageRegistry()
            {
                DefaultDrive = new LocalMarketDataDrive( _pathHistory )
            };

            // Tony: The following ways of initializing the varaibles of History Message Adapter are the same.

            _connector = new HistoryEmulationConnector( new[ ] { _security }, new[ ] { _portfolio } )
            {
                HistoryMessageAdapter =
                {
                    StorageRegistry = storageRegistry,
                    StorageFormat = StorageFormats.Binary,                   
                    StartDate = DatePickerBegin.SelectedDate.Value,
                    StopDate = DatePickerEnd.SelectedDate.Value,
                    GenerateOrderBookFromLevel1 = false
                },
                LogLevel = StockSharp.Logging.LogLevels.Info,
            };

            //var historyMsg = _connector.HistoryMessageAdapter;

            //historyMsg.StorageRegistry = storageRegistry;
            //historyMsg.StorageFormat = StorageFormats.Binary;
            //historyMsg.StartDate = DatePickerBegin.SelectedDate.Value;
            //historyMsg.StopDate = DatePickerEnd.SelectedDate.Value;

            _connector.LogLevel = StockSharp.Logging.LogLevels.Info;

            //_logManager.Sources.Add( _connector );

            _candleSeries = new CandleSeries( CandleSettingsEditor.Settings.CandleType, _security, CandleSettingsEditor.Settings.Arg )
            {
                BuildCandlesMode = StockSharp.Messages.MarketDataBuildModes.Load,
                From = DatePickerBegin.SelectedDate.Value,
                To = DatePickerEnd.SelectedDate.Value
            };
            
            InitializeChart();

            _connector.CandleSeriesProcessing += Connector_CandleSeriesProcessing;
            _connector.SecurityReceived += Connector_SecurityReceived;
            //_connector.MarketDepthReceived += Connector_MarketDepthReceived;

            _connector.Connect();
        }

        private void Connector_MarketDepthReceived( Subscription sub, MarketDepth depth )
        {
            MarketDepthControl.UpdateDepth( depth );
        }

        
        private void Connector_SecurityReceived( StockSharp.Algo.Subscription sub, Security security )
        {
            _connector.SubscribeTrades( security );

            _candleManager = new CandleManager( _connector );

            _candleManager.Start( _candleSeries );

            
        }

        

        private void Connector_CandleSeriesProcessing( CandleSeries candleSeries, Candle candle )
        {
            // ----------------------------------------------------------------
            // The following will draw one candle at a time.
            // ----------------------------------------------------------------
            IChartDrawData chartData = Chart.CreateData();
            var chartGroup = chartData.Group( candle.OpenTime );
            chartGroup.Add( _candleElement, candle );

            if ( chartData != null )
                Chart.Draw( chartData );
        }

        private void InitializeChart()
        {
            Chart.ClearAreas();

            var area = new ChartArea();

            Chart.AddArea( area );

            _candleElement = new ChartCandleElement();

            _tradeElement = new ChartTradeElement() { FullTitle = "Trade" };

            Chart.AddElement( area, _candleElement );
            Chart.AddElement( area, _tradeElement );
        }
    }
}
