using StockSharp.Algo.Candles;
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
    /// Interaction logic for BackTesterView.xaml
    /// </summary>
    public partial class BackTesterView : UserControl
    {
        public BackTesterView( )
        {
            InitializeComponent( );

            

            //DatePickerBegin.DateTime = new DateTime( 2017, 10, 01 );
            //DatePickerEnd.DateTime = DateTime.UtcNow;
        }
    }
}
