using StockSharp.Algo.Strategies;
using StockSharp.Studio.Controls;
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
    /// Interaction logic for TechnicalAnalysisView.xaml
    /// </summary>
    public partial class TechnicalAnalysisView : BaseStudioControl
    {
        private TechnicalAnalysisViewModel _viewModel = null;


        private readonly Strategy _state = new Strategy( );
        //private readonly LayoutManager _layoutManager;

        public object State
        {
            get
            {
                return _state;
            }
        }

        public TechnicalAnalysisView( )
        {
            InitializeComponent( );

            _viewModel = new TechnicalAnalysisViewModel( );

            DataContext = _viewModel;
        }
    }
}
