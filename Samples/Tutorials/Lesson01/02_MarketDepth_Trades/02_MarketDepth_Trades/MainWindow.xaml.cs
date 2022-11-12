using Ecng.Configuration;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Messages;
using StockSharp.Xaml;
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

namespace _02_MarketDepth_Trades
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Connector _connector = new Connector();
        private static readonly string _settingsFile = $"connection{Paths.DefaultSettingsExt}";

        public MainWindow()
        {
            InitializeComponent();

            /* ------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Tony:   Inside InMemoryMessageAdapterProvider constructor, it calls GetAdapters which will load all the StockSharp.XXXXX adapters dlls
             *          from Assemblies.
             *          
             *          So after deserialize of the SettingsStorage, our added adapter (FXCMConnect) can be finally instantiated
             *          Or else we won't see the adapter even though the settings are stored.
             * ------------------------------------------------------------------------------------------------------------------------------------------
             */
            ConfigManager.RegisterService<IMessageAdapterProvider>( new FullInMemoryMessageAdapterProvider( _connector.Adapter.InnerAdapters ) );

            if ( _settingsFile.IsConfigExists() )
            {
                var setting = _settingsFile.Deserialize<SettingsStorage>();
                _connector.Load( setting );
            }
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
            SecurityPicker.MarketDataProvider = _connector;

            //_connector.Connected += _connector_Connected;
            _connector.Connect();
        }

        //private void _connector_Connected()
        //{
        //    _connector.LookupSecurities( new Security() { Code = "EUR/USD" } );
        //}

        private void SecurityPicker_SecuritySelected( StockSharp.BusinessEntities.Security security )
        {
            if ( security == null ) return;

            var whatToSubscribe = new MarketDataMessage
            {
                DataType2 = DataType.Level1,
                IsSubscribe = true,
                From = null,
                To = null,
                Count = null,
                BuildMode = MarketDataBuildModes.Load,
                BuildFrom = null,
                BuildField = null,
                Adapter = null,
                Skip = null,
            };

            _connector.SubscribeMarketData( security, whatToSubscribe );
        }
    }
}

