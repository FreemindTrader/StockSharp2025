using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace _03_Create_simple_indicator
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

            var indicatorElement1 = new ChartIndicatorElement()
            {
                Color = Colors.Brown,
                DrawStyle = StockSharp.Charting.ChartIndicatorDrawStyles.Line
            };

            var indicatorElement2 = new ChartIndicatorElement()
            {
                Color = Colors.DodgerBlue,
                DrawStyle = StockSharp.Charting.ChartIndicatorDrawStyles.Line
            };

            Chart.AddElement( area, indicatorElement1 );
            Chart.AddElement( area, indicatorElement2 );

            var security = new Security()
            {
                Id = "SPX500@FXCM"
            };

            var candleStorage = new StorageRegistry().GetCandleStorage( typeof( TimeFrameCandle ), security, TimeSpan.FromMinutes( 5 ), new LocalMarketDataDrive( _pathHistory ), StorageFormats.Binary );

            var candles = candleStorage.Load( new DateTime( 2022, 10, 01 ), new DateTime( 2022, 10, 31 ) );

            var indicator1 = new SimpleMovingAverage() { Length = 10 };

            var indicator2 = new LazyMovingAverage() { Length = 10 };

            foreach ( var candleMsg in candles )
            {
                var candle = candleMsg.ToCandle( security );
                var indicatorValue1 = indicator1.Process( candle );
                var indicatorValue2 = indicator2.Process( candle );

                var drawData = new ChartDrawData();

                drawData
                    .Group( candleMsg.OpenTime )
                    .Add( candleElement, candle )
                    .Add( indicatorElement1, indicatorValue1 )
                    .Add( indicatorElement2, indicatorValue2 );

                Chart.Draw( drawData );
            }
        }
    }

    class LazyMovingAverage : BaseIndicator
    {
        public int Length { get; set; } = 10;

        private decimal? _outputValue;

        public override bool IsFormed { get; protected set; } = true;

        protected override IIndicatorValue OnProcess( IIndicatorValue input )
        {
            var value = input.GetValue<decimal>();

            if ( _outputValue == null )
                _outputValue = value;

            _outputValue = _outputValue + ( value - _outputValue ) / Length;

            return new DecimalIndicatorValue( this, _outputValue.Value );
        }
    }
}




