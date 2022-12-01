using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.IndicatorPainters;
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

namespace _02_Complex_Indicator
{ 
/// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _pathHistory = @"U:\ForexData";
        public MainWindow()
        {
            InitializeComponent();

            var area = new ChartArea();
            Chart.AddArea( area );

            var candleElement = new ChartCandleElement();
            Chart.AddElement( area, candleElement );

            var indicatorElement = new ChartIndicatorElement()
            {
                Color = Colors.Brown,
                DrawStyle = StockSharp.Charting.ChartIndicatorDrawStyles.Line,
                IndicatorPainter = new BollingerBandsPainter()
            };

            Chart.AddElement( area, indicatorElement );

            var security = new Security()
            {
                Id = "SPX500@FXCM"
            };

            var candleStorage = new StorageRegistry().GetCandleStorage( typeof( TimeFrameCandle ), security, TimeSpan.FromMinutes( 5 ), new LocalMarketDataDrive( _pathHistory ), StorageFormats.Binary );

            var candles = candleStorage.Load( new DateTime( 2022, 10, 01 ), new DateTime( 2022, 10, 31 ) );

            var indicator = new BollingerBands( );

            foreach ( var candleMsg in candles )
            {
                var candle = candleMsg.ToCandle( security );
                var indicatorValue = indicator.Process( candle );

                var drawData = new ChartDrawData();

                drawData
                    .Group( candleMsg.OpenTime )
                    .Add( candleElement, candle )
                    .Add( indicatorElement, indicatorValue );

                Chart.Draw( drawData );
            }
        }
    }
}
