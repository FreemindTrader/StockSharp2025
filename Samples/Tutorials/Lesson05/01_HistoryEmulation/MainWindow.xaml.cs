using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
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

        public MainWindow()
        {
            InitializeComponent();

            CandleSettingsEditor.Settings = new StockSharp.Algo.Candles.CandleSeries()
            {
                CandleType = typeof( TimeFrameCandle ),
                Arg = TimeSpan.FromMinutes( 5 )
            };

            DatePickerBegin.SelectedDate = new DateTime( 2020, 10, 1 );
            DatePickerEnd.SelectedDate = new DateTime( 2020, 10, 30 );
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

            _connector = new HistoryEmulationConnector( )
        }
    }
}
