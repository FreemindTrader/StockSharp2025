using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Testing;
using StockSharp.Xaml;
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
using System.Xml.Serialization;
using System.Xml;
using DevExpress.Xpf.Core.Serialization;
using StockSharp.Configuration;
using Ecng.ComponentModel;
using System.Data;
using Ecng.Common;
using Ecng.Configuration;
using StockSharp.Messages;

namespace _01_Connect_and_get_securities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPersistable
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
            _connector.Connect();
        }

        public void Load( SettingsStorage storage )
        {
            throw new NotImplementedException();
        }

        public void Save( SettingsStorage storage )
        {
            throw new NotImplementedException();
        }
    }
}
