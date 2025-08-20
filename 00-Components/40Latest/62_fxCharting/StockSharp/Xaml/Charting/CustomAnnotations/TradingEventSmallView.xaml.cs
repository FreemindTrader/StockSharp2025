using SciChart.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic; using fx.Collections;
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

namespace StockSharp.Xaml.Charting
{
    /// <summary>
    /// Interaction logic for TaSignalGroupBox.xaml
    /// </summary>
    public partial class TradingEventSmallView : CustomAnnotation
    {
        TradingEventSmallViewModel _viewModel;
        public TradingEventSmallView( )
        {
            InitializeComponent( );

            _viewModel = ( TradingEventSmallViewModel ) DataContext;
        }

        private void GridControl_CustomColumnDisplayText( object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e )
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

        public void StartItemSource( string symbol )
        {
            _viewModel.StartItemSource( symbol );
        }
    }
}
