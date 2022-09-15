using Ecng.Collections;
using Ecng.Configuration;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.GridControl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace StockSharp.Xaml
{


    internal sealed class MyPnLChangeConverter : IValueConverter
    {
        private ImageSource _upImage;
        private ImageSource _downImage;
        private ImageSource _zeroImage;

        public ImageSource UpImage
        {
            get
            {
                return _upImage;
            }
            set
            {
                _upImage = value;
            }
        }

        

        public ImageSource DownImage
        {
            get
            {
                return _downImage;
            }
            set
            {
                _downImage = value;
            }
        }

        public ImageSource ZeroImage
        {
            get
            {
                return _zeroImage;
            }
            set
            {
                _zeroImage = value;
            }
        }
        

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == DependencyProperty.UnsetValue || !( value is Decimal num ) )
            {
                return Binding.DoNothing;
            }

            if ( num > Decimal.Zero )
            {
                return UpImage;
            }

            if ( num < Decimal.Zero )
            {
                return DownImage;
            }

            return ZeroImage;
        }

        object IValueConverter.ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture )
        {
            throw new NotSupportedException( );
        }
    }
    public partial class StrategiesDashboard : BaseGridControl
    {
        public static readonly RoutedCommand StartCommand = new RoutedCommand( );
        public static readonly RoutedCommand StopCommand  = new RoutedCommand( );

        public static readonly DependencyProperty SecurityProviderProperty = DependencyProperty.Register( nameof( SecurityProvider ), typeof( ISecurityProvider ), typeof( StrategiesDashboard ) );
        public static readonly DependencyProperty PortfoliosProperty       = DependencyProperty.Register( nameof( Portfolios ), typeof( PortfolioDataSource ), typeof( StrategiesDashboard ) );
        
        private readonly ThreadSafeObservableCollection<StrategiesDashboardItem> _itemSource;        

        public StrategiesDashboard( )
        {
            SecurityProvider = ServicesRegistry.TrySecurityProvider;
            Portfolios = ConfigManager.TryGetService<PortfolioDataSource>( );
            InitializeComponent( );
            ObservableCollectionEx<StrategiesDashboardItem> observableCollectionEx = new ObservableCollectionEx<StrategiesDashboardItem>( );
            _itemSource = new ThreadSafeObservableCollection<StrategiesDashboardItem>( observableCollectionEx );

            ItemsSource = observableCollectionEx;

            CommandBinding startCommandBinding = new CommandBinding( StartCommand, OnExecuteStart, CanExecuteStartHandler );
            CommandBinding StopCommandBinding = new CommandBinding( StopCommand, OnExecuteStop, CanExecuteStopHandler );

            this.CommandBindings.Add( startCommandBinding );
            this.CommandBindings.Add( StopCommandBinding );

        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return ( ISecurityProvider )GetValue( StrategiesDashboard.SecurityProviderProperty );
            }
            set
            {
                SetValue( StrategiesDashboard.SecurityProviderProperty, value );
            }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return ( PortfolioDataSource )GetValue( StrategiesDashboard.PortfoliosProperty );
            }
            set
            {
                SetValue( StrategiesDashboard.PortfoliosProperty, value );
            }
        }

        public IList<StrategiesDashboardItem> Items
        {
            get
            {
                return _itemSource;
            }
        }

        public event Func<StrategiesDashboardItem, bool> CanExecuteStart;

        public event Func<StrategiesDashboardItem, bool> CanExecuteStop;

        public event Action<StrategiesDashboardItem> ExecuteStart;

        public event Action<StrategiesDashboardItem> ExecuteStop;

        private void CanExecuteStartHandler( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            Func<StrategiesDashboardItem, bool> func0 = CanExecuteStart;
            int num = func0 != null ? ( func0( ( StrategiesDashboardItem )e.Parameter ) ? 1 : 0 ) : 0;
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private void OnExecuteStart( object sender, ExecutedRoutedEventArgs e )
        {
            Action<StrategiesDashboardItem> action3 = ExecuteStart;
            if ( action3 == null )
            {
                return;
            }

            action3( ( StrategiesDashboardItem )e.Parameter );
        }

        private void CanExecuteStopHandler( object sender, CanExecuteRoutedEventArgs e )
        {
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            Func<StrategiesDashboardItem, bool> func1 = CanExecuteStop;
            int num = func1 != null ? ( func1( ( StrategiesDashboardItem )e.Parameter ) ? 1 : 0 ) : 0;
            executeRoutedEventArgs.CanExecute = num != 0;
        }

        private void OnExecuteStop( object sender, ExecutedRoutedEventArgs e )
        {
            Action<StrategiesDashboardItem> action4 = ExecuteStop;
            if ( action4 == null )
            {
                return;
            }

            action4( ( StrategiesDashboardItem )e.Parameter );
        }
    }
}
