using DevExpress.Xpf.Core;
using FreemindAITrade.ViewModels;
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
using System.Windows.Shapes;

namespace FreemindAITrade.View
{
    /// <summary>
    /// Interaction logic for PortfolioPickerView.xaml
    /// </summary>
    public partial class PortfolioPickerView : UserControl
    {        
        public PortfolioPickerView( )
        {
            InitializeComponent( );            
        }

        private void PortfoliosCtrl_PortfolioSelected( )
        {

        }

        private void PortfoliosCtrl_PortfolioDoubleClicked( )
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
