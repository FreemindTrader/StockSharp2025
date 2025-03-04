using System;
using System.Linq;
using System.Windows.Controls;

namespace FreemindAITrade.View
{
    /// <summary>
    /// Interaction logic for PortfolioPickerView.xaml
    /// </summary>
    public partial class PortfolioPickerView : UserControl
    {
        public PortfolioPickerView()
        {
            InitializeComponent();
        }

        private void PortfoliosCtrl_PortfolioSelected()
        {

        }

        private void PortfoliosCtrl_PortfolioDoubleClicked()
        {

        }

        //private void PortfoliosCtrl_PortfolioSelected( )
        //{
        //    OkBtn.IsEnabled = _viewModel.SelectedPortfolio != null;
        //}

        //private void PortfoliosCtrl_PortfolioDoubleClicked( )
        //{
        //    this.DialogResult = new bool?( true );
        //}
    }
}
