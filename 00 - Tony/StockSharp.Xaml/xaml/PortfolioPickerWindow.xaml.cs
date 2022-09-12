using DevExpress.Xpf.Core;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class PortfolioPickerWindow : DXWindow
    {        
        public PortfolioPickerWindow( )
        {
            InitializeComponent( );
        }

        public Portfolio SelectedPortfolio
        {
            get
            {
                return this.PortfoliosCtrl.SelectedPortfolio;
            }
            set
            {
                this.PortfoliosCtrl.SelectedPortfolio = value;
            }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return this.PortfoliosCtrl.Portfolios;
            }
            set
            {
                this.PortfoliosCtrl.Portfolios = value;
            }
        }

        private void PortfoliosCtrl_PortfolioSelected( )
        {
            OkBtn.IsEnabled = this.SelectedPortfolio != null;
        }

        private void PortfoliosCtrl_PortfolioDoubleClicked( )
        {
            this.DialogResult = true;
        }
    }

    
}
