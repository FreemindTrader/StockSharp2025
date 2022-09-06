using DevExpress.Xpf.Core;
using Ecng.Collections;
using FreemindAITrade.ViewModels;
using StockSharp.Algo;
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
using System.Windows.Shapes;

namespace FreemindAITrade
{
    /// <summary>
    /// Interaction logic for SymbolSelectViewWindow.xaml
    /// </summary>
    public partial class SymbolSelectWindow : DXWindow
    {
        SymbolSelectViewModel _viewModel = null;

        public SymbolSelectWindow( )
        {
            InitializeComponent( );

            DataContext = this;

            _viewModel = ( SymbolSelectViewModel )SymbolSelectView.DataContext;

            //_viewModel.HasSelection += _viewModel_HasSelection;
        }

        private void _viewModel_HasSelection( bool hasSelection )
        {
            if ( hasSelection )
            {
                Ok.IsEnabled = true;
            }
            else
            {
                Ok.IsEnabled = false;
            }
        }

        public IEnumerable<Security> SelectedSecurities
        {
            get
            {                
                return _viewModel.Securities.LookupAll( );
            }
            set
            {
                _viewModel.Securities.AddRange( value.ToArray( ) );
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
               

                return _viewModel.SecurityProvider;
            }
            set
            {
                _viewModel.SecurityProvider = value;
            }
        }

        public bool IsEnable
        {
            get
            {
                return _viewModel.SelectedItem != null;
            }
        }

        private void DXWindow_Loaded( object sender, RoutedEventArgs e )
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            Left = mainWindow.Left + ( mainWindow.Width - ActualWidth ) / 2;
            Top = mainWindow.Top + ( mainWindow.Height - ActualHeight ) / 2;
        }
    }
}
