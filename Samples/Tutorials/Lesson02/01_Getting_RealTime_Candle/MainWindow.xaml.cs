using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using MathNet.Numerics;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Charting;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.PropertyGrid;
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
using TheArtOfDev.HtmlRenderer.Adapters;

namespace _01_Getting_RealTime_Candle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private readonly string _defaultDataPath = "Data";
        private static string _settingsFile = $"connection{Paths.DefaultSettingsExt}";
        private Connector _connector;
        private CandleSeries _candleSeries;
        private ChartCandleElement _candleElement;

        public MainWindow()
        {
            InitializeComponent();

            _defaultDataPath = _defaultDataPath.ToFullPath();

            _settingsFile = System.IO.Path.Combine( _defaultDataPath, $"connection{Paths.DefaultSettingsExt}" );
        }

        private void Window_Loaded( object sender, RoutedEventArgs e )
        {
            var logManager = new LogManager();
            logManager.Listeners.Add( new FileLogListener { LogDirectory = System.IO.Path.Combine( _defaultDataPath, "Logs" ) } );

            _connector = new Connector();
            logManager.Sources.Add( _connector );

            CandleSettingsEditor.Settings = new StockSharp.Algo.Candles.CandleSeries()
            {
                CandleType = typeof( TimeFrameCandle ),
                Arg = TimeSpan.FromMinutes( 5 )
            };

            InitConnector();
        }

        private void InitConnector()
        {

            /* ------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Tony:   Inside InMemoryMessageAdapterProvider constructor, it calls GetAdapters which will load all the StockSharp.XXXXX adapters dlls
             *          from Assemblies.
             *          
             *          So after deserialize of the SettingsStorage, our added adapter (FXCMConnect) can be finally instantiated
             *          Or else we won't see the adapter even though the settings are stored.
             * ------------------------------------------------------------------------------------------------------------------------------------------
             */
            _connector.Adapter.SupportLookupTracking = false;
            _connector.Adapter.IsSupportTransactionLog = false;
            _connector.Adapter.IsSupportOrderBookSort = false;
            _connector.Adapter.Level1Extend = false;
            _connector.Adapter.SupportPartialDownload = false;
            _connector.Adapter.SupportBuildingFromOrderLog = false;
            _connector.Adapter.SupportOrderBookTruncate = false;
            _connector.Adapter.SupportCandlesCompression = false;

            ConfigManager.RegisterService<IMessageAdapterProvider>( new FullInMemoryMessageAdapterProvider( _connector.Adapter.InnerAdapters ) );

            try
            {
                if ( _settingsFile.IsConfigExists() )
                {
                    var ctx = new ContinueOnExceptionContext();
                    ctx.Error += ex => ex.LogError();

                    var setting = _settingsFile.Deserialize<SettingsStorage>();

                    using ( ctx.ToScope() )
                    {
                        _connector.LoadIfNotNull( setting );
                    }
                }
            }
            catch
            {
            }

            _connector.CandleSeriesProcessing += Connector_CandleSeriesProcessing;
        }

        private void Connector_CandleSeriesProcessing( CandleSeries series, Candle candle )
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

        private void Setting_Click( object sender, RoutedEventArgs e )
        {
            if ( _connector.Configure( this ) )
            {
                _connector.Save().Serialize( _settingsFile );
            }
        }

        private void Connect_Click( object sender, RoutedEventArgs e )
        {
            SecurityPicker.SecurityProvider = _connector;

            _connector.Connect();
        }

        private void SecurityPicker_SecuritySelected( StockSharp.BusinessEntities.Security security )
        {
            if ( security == null ) return;       
            
            if ( _candleSeries != null )
            {
                var subscription = _connector.Subscriptions.FirstOrDefault( s => s.CandleSeries == _candleSeries );

                if ( subscription is null )
                {
                    if ( _connector is ILogReceiver logs )
                        logs.AddWarningLog( LocalizedStrings.SubscriptionNonExist, _candleSeries );
                }
                else
                {
                    _connector.UnSubscribe( subscription );
                }                                   
            }

            // 00. --------------------------------- Chart ----------------------------------------
            Chart.ClearAreas();
            
            // 01. Create an Chart Area and add to chart
            var area = new ChartArea();
            Chart.AddArea( area );

            _candleElement = new ChartCandleElement();

            _candleSeries = new CandleSeries( CandleSettingsEditor.Settings.CandleType, security, CandleSettingsEditor.Settings.Arg )
            {
                BuildCandlesMode = MarketDataBuildModes.LoadAndBuild,
                //BuildCandlesFrom2 = DataType.CandleTimeFrame,
                //AllowBuildFromSmallerTimeFrame = true,
                //IsCalcVolumeProfile = true
            };

            // 02. Create to the area that we just created an Candle UI, and the Candle
            //     data is from _candleSeries
            Chart.AddElement( area, _candleElement, _candleSeries );

            // 03. We ask connector for an subscription to its candle
            var subs = new Subscription( _candleSeries );

            var mdMsg = ( MarketDataMessage )subs.SubscriptionMessage;
            mdMsg.From = DateTime.Today.Subtract( TimeSpan.FromDays( 5 ) ); 
            mdMsg.To = DateTime.Now;

            
            /* --------------------------------------------------------------------------------------------------
            *  I need the liveTrading to download all the databar before send out message so that 
            *  Scichart, databars can be inserted all together.
            * 
            *  As for Octopus, I need it to send out databars for every day so that we can save all the everyday bars
            *  One day by one day, just in case we have a long time to redownload
            * 
            * -------------------------------------------------------------------------------------------------- 
            */
            mdMsg.ExtensionInfo = new Dictionary<string, object>();
            mdMsg.ExtensionInfo.Add( "FullDownload", true );

            _connector.Subscribe( subs );

            //_connector.SubscribeCandles( _candleSeries, from, to );
            
        }

        
    }
}
