using DevExpress.Mvvm;
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

namespace FreemindAITrade.View
{
    /// <summary>
    /// Interaction logic for FreemindSymbolControl.xaml
    /// </summary>
    public partial class SymbolSelectView : UserControl
    {       
        public SymbolSelectView( )
        {
            InitializeComponent( );            
        }

        private void GridControl_SelectionChanged( object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e )
        {

        }
    }
}
