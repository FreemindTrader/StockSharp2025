using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class PortfolioPicker : UserControl    
    {
        private PortfolioDataSource _portfolioDataSource;

        public event Action PortfolioSelected;

        public event Action PortfolioDoubleClicked;

        public event Action GridChanged;

        private readonly string _counterText;
                        
        public PortfolioPicker( )
        {
            InitializeComponent( );

            _counterText = Counter.Text;
            _portfolioDataSource = new PortfolioDataSource( );

            UpdateView( );
        }

        public Portfolio SelectedPortfolio
        {
            get
            {
                var inter = ( PortfolioPropertyChangeHandler ) PortfoliosCtrl.SelectedItem;

                var output = ( Portfolio )inter;

                return ( output ) ;
            }
            set
            {
                PortfoliosCtrl.SelectedItem = value;
            }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return _portfolioDataSource;
            }
            set
            {                
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                _portfolioDataSource = value;

                ( ( DataControlBase ) PortfoliosCtrl ).ItemsSource = value.Items;
                UpdateView( );
            }
        }

        public string PortfolioFilter
        {
            get
            {
                return PortfolioFilterCtrl.Text;
            }
            set
            {
                PortfolioFilterCtrl.Text = value;
            }
        }

        private void UpdateView( )
        {
            GridSummaryItem gridSummaryItem = ( PortfoliosCtrl.TotalSummary ).OfType<GridSummaryItem>().FirstOrDefault<GridSummaryItem>();
            if ( gridSummaryItem == null )
            {
                Counter.Text = StringHelper.Put( _counterText, new object[ 2 ]
                {
                  0,
                  Portfolios.ItemsourceCount()
                } );
            }
            else
            {
                Counter.Text = StringHelper.Put( _counterText, new object[ 2 ]
                {
                          ((DataControlBase) PortfoliosCtrl).GetTotalSummaryValue((SummaryItemBase) gridSummaryItem),
                          Portfolios.ItemsourceCount()
                } );
            }
        }

       

        private void PortfoliosCtrl_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            var myEvent = PortfolioSelected;

            if ( myEvent != null )
            {
                myEvent.Invoke( );
            }            
        }

        private void PortfoliosCtrl_ItemDoubleClick( object sender, ItemDoubleClickEventArgs e )
        {
            var inter = ( PortfolioPropertyChangeHandler )( ( DataControlBase )PortfoliosCtrl ).CurrentItem;

            SelectedPortfolio = ( Portfolio )inter;

            var myEvent = PortfolioDoubleClicked;

            if ( myEvent != null )
            {
                myEvent.Invoke( );
            }            
        }

        private void PortfoliosCtrl_LayoutChanged( )
        {
            var myEvent = GridChanged;

            if ( myEvent != null )
            {
                myEvent.Invoke( );
            }
        }

        private void PortfolioFilterCtrl_TextChanged( object sender, TextChangedEventArgs e )
        {
            PortfoliosCtrl.BeginDataUpdate( );

            try
            {
                string str = PortfolioFilter?.Trim();

                if ( ! string.IsNullOrEmpty( str ) )
                {
                    PortfoliosCtrl.FilterCriteria = CriteriaOperator.Parse( string.Format( "Contains([{0}], '{1}')", "Name", str ), new object[ 0 ] );
                }                
            }
            finally
            {
                PortfoliosCtrl.BeginDataUpdate( );
                UpdateView( );
            }

            PortfoliosCtrl_LayoutChanged( );
        }

        
    }
}
