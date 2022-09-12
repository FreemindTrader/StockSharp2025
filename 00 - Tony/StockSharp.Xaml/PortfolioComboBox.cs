using StockSharp.BusinessEntities;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace StockSharp.Xaml
{
    public class PortfolioComboBox : ComboBox
    {
        public static readonly DependencyProperty PortfoliosProperty = DependencyProperty.Register(nameof (Portfolios), typeof (PortfolioDataSource), typeof (PortfolioComboBox), new PropertyMetadata((object) null, new PropertyChangedCallback( OnPropertyChangedCallback )));

        private static void OnPropertyChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( PortfolioComboBox ) d ).SetItemSource( ( PortfolioDataSource ) e.NewValue );            
        }

        public static readonly DependencyProperty SelectedPortfolioProperty = DependencyProperty.Register(nameof (SelectedPortfolio), typeof (Portfolio), typeof (PortfolioComboBox), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback( SelectedPortfolioPropertyChanged)));
        private PortfolioDataSource _dataSource;

        public PortfolioComboBox( )
        {
            DisplayMemberPath = "Name";
        }

        private void SetItemSource( PortfolioDataSource dataSource )
        {
            _dataSource = dataSource;
            ItemsSource = ( IEnumerable ) _dataSource?.Items;
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return _dataSource;
            }
            set
            {
                SetValue( PortfolioComboBox.PortfoliosProperty, ( object ) value );
            }
        }

        public Portfolio SelectedPortfolio
        {
            get
            {
                return ( Portfolio ) GetValue( PortfolioComboBox.SelectedPortfolioProperty );
            }
            set
            {
                SetValue( PortfolioComboBox.SelectedPortfolioProperty, ( object ) value );
            }
        }

        private static void SelectedPortfolioPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Portfolio newValue = ( Portfolio )e.NewValue;
            ( ( Selector )d ).SelectedItem = ( object )newValue;
        }

        protected override void OnSelectionChanged( SelectionChangedEventArgs selectionChangedEventArgs_0 )
        {
            SelectedPortfolio = ( Portfolio ) SelectedItem;
            base.OnSelectionChanged( selectionChangedEventArgs_0 );
        }        
    }
}