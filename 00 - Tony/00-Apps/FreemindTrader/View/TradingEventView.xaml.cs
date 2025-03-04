using DevExpress.Xpf.Grid;
using System;
using System.Linq;
using System.Windows.Controls;

namespace FreemindTrader
{
    /// <summary>
    /// Interaction logic for fxTradingEvent.xaml
    /// </summary>
    public partial class TradingEventView : UserControl
    {
        public TradingEventView()
        {
            InitializeComponent();
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
