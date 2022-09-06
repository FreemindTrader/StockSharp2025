using DevExpress.Xpf.Core;
using Ecng.Common;
using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Windows.Controls;

namespace fx.Charting
{
    public partial class CandlestickUIPicker : DXWindow
    {


        public CandlestickUIPicker( )
        {
            InitializeComponent( );
        }

        public IEnumerable<CandlestickUI> Elements
        {
            get
            {
                return ( IEnumerable<CandlestickUI> )ElementsCtrl.ItemsSource;
            }
            set
            {
                ElementsCtrl.ItemsSource = value;
            }
        }

        public CandlestickUI SelectedElement
        {
            get
            {
                return ( CandlestickUI )ElementsCtrl.SelectedItem;
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
