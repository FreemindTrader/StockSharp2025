using DevExpress.Xpf.Grid;
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

namespace FreemindTrader
{
    /// <summary>
    /// Interaction logic for fxTradingEvent.xaml
    /// </summary>
    public partial class TradingEventView : UserControl
    {
        public TradingEventView( )
        {
            InitializeComponent( );
            DataControlBase.AllowInfiniteGridSize = true;
        }

        private void GridControl_CustomColumnDisplayText( object sender, CustomColumnDisplayTextEventArgs e )
        {
            if ( ( e.Column.FieldName == "Sma50xSignal" ) ||
                ( e.Column.FieldName == "MacdCrossSignal" ) ||
                ( e.Column.FieldName == "MaXoverSignal" ) ||
                ( e.Column.FieldName == "EmaTrend" ) ||
                ( e.Column.FieldName == "AroonXSignal" ) ||
                ( e.Column.FieldName == "PsarSignal" )
                )
                e.DisplayText = null;
        }
    }
}
