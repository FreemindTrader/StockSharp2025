using System;
using System.Linq;
using System.Windows.Controls;

namespace FreemindAITrade.View
{
    /// <summary>
    /// Interaction logic for FreemindSymbolControl.xaml
    /// </summary>
    public partial class SymbolSelectView : UserControl
    {
        public SymbolSelectView()
        {
            InitializeComponent();
        }

        private void GridControl_SelectionChanged( object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e )
        {

        }
    }
}
