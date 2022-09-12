using DevExpress.Xpf.Editors;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class PortfolioEditor : ComboBoxEdit
    {
        public static readonly DependencyProperty PortfoliosProperty = DependencyProperty.Register( nameof( Portfolios ), typeof( PortfolioDataSource ), typeof( PortfolioEditor ), new PropertyMetadata( null, new PropertyChangedCallback( PortfolioEditor.OnPortfoliosPropertyChanged ) ) );
        public static readonly DependencyProperty SelectedPortfolioProperty = DependencyProperty.Register( nameof( SelectedPortfolio ), typeof( Portfolio ), typeof( PortfolioEditor ), new FrameworkPropertyMetadata( null, new PropertyChangedCallback( PortfolioEditor.OnSelectedPortfolioPropertyChanged ) ) );
        private PortfolioDataSource _dataSource;
        private Portfolio _portfolio;

        public PortfolioEditor( )
        {
            InitializeComponent( );
            EditValueChanging += new EditValueChangingEventHandler( PortfolioEditor_EditValueChanging );
        }

        private static void OnPortfoliosPropertyChanged(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e )
        {
            ( ( PortfolioEditor )d ).ProcessPortfoliosPropertyChanged( ( PortfolioDataSource )e.NewValue );
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return _dataSource;
            }
            set
            {
                SetValue( PortfolioEditor.PortfoliosProperty, value );
            }
        }

        private static void OnSelectedPortfolioPropertyChanged(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e )
        {
            PortfolioEditor portfolioEditor = ( PortfolioEditor )d;
            Portfolio newValue = ( Portfolio )e.NewValue;
            portfolioEditor._portfolio = newValue;
            portfolioEditor.SetPortfolio( newValue );
        }

        public Portfolio SelectedPortfolio
        {
            get
            {
                return _portfolio;
            }
            set
            {
                SetValue( PortfolioEditor.SelectedPortfolioProperty, value );
            }
        }

        private void PortfolioEditor_EditValueChanging( object sender, EditValueChangingEventArgs e )
        {
            if ( e.IsCancel )
            {
                return;
            }

            if ( e.NewValue is PortfolioPropertyChangeHandler newValue )
            {
                SelectedPortfolio = ( Portfolio )newValue.Position;
            }
            else
            {
                SelectedPortfolio = null;
            }
        }

        private void ProcessPortfoliosPropertyChanged( PortfolioDataSource datasource )
        {
            Portfolio selectedPortfolio = SelectedPortfolio;
            _dataSource = datasource;
            ItemsSource = _dataSource?.Items;
            SetPortfolio( selectedPortfolio );
        }

        private void SetPortfolio( Portfolio portfolio_1 )
        {
            if ( Portfolios == null || portfolio_1 == null )
            {
                return;
            }

            EditValue = Portfolios.GetPortfolioCollection( ).TryGet( portfolio_1 );
        }

        private void SearchBtn_Click( object sender, RoutedEventArgs e )
        {
            if ( IsReadOnly )
            {
                return;
            }

            PortfolioPickerWindow wnd = new PortfolioPickerWindow( );
            if ( Portfolios != null )
            {
                wnd.Portfolios = Portfolios;
                wnd.SelectedPortfolio = SelectedPortfolio;
            }
            if ( !wnd.ShowModal( this ) )
            {
                return;
            }

            Portfolio selectedPortfolio = wnd.SelectedPortfolio;
            if ( selectedPortfolio != null && Portfolios != null )
            {
                EditValue = Portfolios.GetPortfolioCollection( ).TryGet( selectedPortfolio );
            }
            else
            {
                EditValue = new PortfolioPropertyChangeHandler( selectedPortfolio, p => { } );
            }
        }

        private void CancelBtn_Click( object sender, RoutedEventArgs e )
        {
            BaseEdit ownerEdit = BaseEdit.GetOwnerEdit( ( DependencyObject )sender );
            if ( ownerEdit == null || IsReadOnly )
            {
                return;
            }

            ownerEdit.EditValue = null;
        }        
    }
}
