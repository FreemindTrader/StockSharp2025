using DevExpress.Xpf.PropertyGrid;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Markup;

namespace fx.Charting
{
    public partial class ChartElementEditor : UserControl, IComponentConnector
    {
        public ChartElementEditor( )
        {
            InitializeComponent( );
        }

        private void OnCustomExpand( object sender, CustomExpandEventArgs e )
        {
            if ( !e.IsInitializing )
            {
                return;
            }

            int num = e.Row.FullPath.Count( c => c == '.' );
            e.IsExpanded = num < 5;
        }
    }
}
