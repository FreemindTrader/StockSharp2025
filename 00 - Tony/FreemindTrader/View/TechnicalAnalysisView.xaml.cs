using StockSharp.Algo.Strategies;
using StockSharp.Studio.Controls;
using System;
using System.Linq;

namespace FreemindTrader
{
    /// <summary>
    /// Interaction logic for TechnicalAnalysisView.xaml
    /// </summary>
    public partial class TechnicalAnalysisView : BaseStudioControl
    {
        private TechnicalAnalysisViewModel _viewModel = null;


        private readonly Strategy _state = new Strategy();
        //private readonly LayoutManager _layoutManager;

        public object State
        {
            get
            {
                return _state;
            }
        }

        public TechnicalAnalysisView()
        {
            InitializeComponent();

            _viewModel = new TechnicalAnalysisViewModel();

            DataContext = _viewModel;
        }
    }
}
