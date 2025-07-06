using DevExpress.Xpf.Core;
using Ecng.Common;
using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting
{
    public partial class CandlestickUIPicker : DXWindow
    {


        public CandlestickUIPicker( )
        {
            InitializeComponent( );
        }

        public IEnumerable<ChartCandleElement> Elements
        {
            get
            {
                return ( IEnumerable<ChartCandleElement> )ElementsCtrl.ItemsSource;
            }
            set
            {
                ElementsCtrl.ItemsSource = value;
            }
        }

        public ChartCandleElement SelectedElement
        {
            get
            {
                return ( ChartCandleElement )ElementsCtrl.SelectedItem;
            }
            set
            {
                ElementsCtrl.SelectedItem = value;
            }
        }

        private void ElementsCtrl_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            Ok.IsEnabled = SelectedElement != null;
        }


    }
}
