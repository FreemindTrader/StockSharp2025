using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Logging;
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



namespace _03_Work_with_orders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _defaultDataPath = "Data";
        private Connector _connector = new Connector();
        private static string _settingsFile = $"connection{Paths.DefaultSettingsExt}";

        public event Func<string, Connector> CreateConnector;

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

            _connector = CreateConnector?.Invoke( _defaultDataPath ) ?? new Connector();
            logManager.Sources.Add( _connector );

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
            SecurityEditor.SecurityProvider = _connector;
            PortfolioEditor.Portfolios = new PortfolioDataSource( _connector );

            _connector.NewOrder += Connector_NewOrder;
            _connector.OrderRegisterFailed += Connector_OrderRegisterFailed;
            _connector.NewMyTrade += Connector_NewMyTrade;

            _connector.Connect();
        }

        private void Connector_NewMyTrade( MyTrade trade )
        {
            MyTradeGrid.Trades.Add( trade );
        }

        private void Connector_OrderRegisterFailed( OrderFail order )
        {
            OrderGrid.AddRegistrationFail( order );
        }

        private void Connector_NewOrder( Order order )
        {
            OrderGrid.Orders.Add( order );
        }

        private void Buy_Click( object sender, RoutedEventArgs e )
        {
            var newOrder = new Order()
            {
                Security = SecurityEditor.SelectedSecurity,
                Portfolio = PortfolioEditor.SelectedPortfolio,
                Volume = 1,
                Direction = Sides.Buy,
                Price = decimal.Parse( TextBoxPrice.Text )
            };

            _connector.RegisterOrder( newOrder );
        }

        private void Sell_Click( object sender, RoutedEventArgs e )
        {
            var newOrder = new Order()
            {
                Security = SecurityEditor.SelectedSecurity,
                Portfolio = PortfolioEditor.SelectedPortfolio,
                Volume = 1,
                Direction = Sides.Sell,
                Price = decimal.Parse( TextBoxPrice.Text )
            };

            _connector.RegisterOrder( newOrder );
        }

        
    }
}
