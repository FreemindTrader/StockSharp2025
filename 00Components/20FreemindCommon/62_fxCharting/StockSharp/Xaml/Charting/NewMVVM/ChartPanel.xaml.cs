using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Backup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    public partial class ChartPanelEx : UserControl, IPersistable, IChart, IThemeableChart
    {
        public static RoutedCommand RegisterOrderCommand = new RoutedCommand( );
        public static RoutedCommand AddAreaCommand       = new RoutedCommand( );
        public static RoutedCommand AddCandlesCommand    = new RoutedCommand( );
        public static RoutedCommand AddIndicatorCommand  = new RoutedCommand( );

        private DispatcherTimer _timer;
        private readonly ChartPanelOrderSettings _orderSettings;
        private readonly ChartPanelShareSettings _shareSettings;
        private IBackupService _backupService;


        public ChartPanelEx( )
        {
            InitializeComponent( );
            if ( this.IsDesignMode( ) )
            {
                return;
            }

            _orderSettings = new ChartPanelOrderSettings( );
            OrderSettings.PropertyChanged += ( ( s, e ) => SettingsChanged?.Invoke( ) );
            OrderSettingsPropertyGrid.SelectedObject = OrderSettings;

            _shareSettings = new ChartPanelShareSettings( );
            ShareSettings.PropertyChanged += new PropertyChangedEventHandler( OnShareSettingsPropertyChanged );
            ShareSettingsPropertyGrid.SelectedObject = ShareSettings;

            Chart.CreateOrder               += OnCreateOrder;
            Chart.MoveOrder                 += ( o, val )  => MoveOrder?.Invoke( o, val );
            Chart.CancelOrder               += ( o         => CancelOrder?.Invoke( o ) );
            Chart.AnnotationCreated         += ( a         => AnnotationCreated?.Invoke( a ) );
            Chart.AnnotationModified        += ( a, d )    => AnnotationModified?.Invoke( a, d );
            Chart.AnnotationDeleted         += ( a         => AnnotationDeleted?.Invoke( a ) );
            Chart.AnnotationSelected        += ( a, d )    => AnnotationSelected?.Invoke( a, d );
            Chart.SubscribeCandleElement    += ( e, s )    => SubscribeCandleElement?.Invoke( e, s );
            Chart.SubscribeIndicatorElement += ( e, s, i ) => SubscribeIndicatorElement?.Invoke( e, s, i );
            Chart.SubscribeOrderElement     += ( e, s )    => SubscribeOrderElement?.Invoke( e, s );
            Chart.SubscribeTradeElement     += ( e, s )    => SubscribeTradeElement?.Invoke( e, s );
            Chart.UnSubscribeElement        += ( e         => UnSubscribeElement?.Invoke( e ) );



            ImageNameDrawStyle[ ] itemSourece = new ImageNameDrawStyle[ 10 ]
            {
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesBars")      , LocalizedStrings.Bars          , ChartCandleDrawStyles.Ohlc           ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesJapan")     , LocalizedStrings.CandleStick   , ChartCandleDrawStyles.CandleStick    ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesLine")      , LocalizedStrings.LineOpen      , ChartCandleDrawStyles.LineOpen       ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesLine")      , LocalizedStrings.LineClose     , ChartCandleDrawStyles.LineClose      ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesLine")      , LocalizedStrings.LineHigh      , ChartCandleDrawStyles.LineHigh       ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesLine")      , LocalizedStrings.LineLow       , ChartCandleDrawStyles.LineLow        ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesLineArea" ) , LocalizedStrings.Area          , ChartCandleDrawStyles.Area           ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("GanttChart")       , LocalizedStrings.BoxChart      , ChartCandleDrawStyles.BoxVolume      ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("GanttChart")       , LocalizedStrings.ClusterProfile, ChartCandleDrawStyles.ClusterProfile ),
                new ImageNameDrawStyle( ThemedIconsExtension.GetImage("CandlesXo")        , LocalizedStrings.PnFCandle     , ChartCandleDrawStyles.PnF)
            };

            CandleStylesSettings.ItemsSource = itemSourece;
            CandleStyles.EditValue = itemSourece[ 1 ];

            ConfigManager.ServiceRegistered += BackupServiceRegistered;
            BackupService = ConfigManager.TryGetService<IBackupService>( );
        }

        public int ChartCount { get; set; }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return Chart.SecurityProvider;
            }
            set
            {
                Chart.SecurityProvider = value;
            }
        }

        public event Action SettingsChanged;

        public event Action<ChartArea, Order> RegisterOrder;

        public event Action<Order, Decimal> MoveOrder;

        public event Action<Order> CancelOrder;

        public event Action<ChartAnnotation> AnnotationCreated;

        public event Action<ChartAnnotation, ChartDrawData.sAnnotation> AnnotationModified;

        public event Action<ChartAnnotation> AnnotationDeleted;

        public event Action<ChartAnnotation, ChartDrawData.sAnnotation> AnnotationSelected;

        public event Action<ChartCandleElement, CandleSeries> SubscribeCandleElement;

        public event Action<ChartIndicatorElement, CandleSeries, IIndicator> SubscribeIndicatorElement;

        public event Action<OrdersUI, Security> SubscribeOrderElement;

        public event Action<TradesUI, Security> SubscribeTradeElement;

        public event Action<IChartElement> UnSubscribeElement;

        public IEnumerable<IChartElement> Elements
        {
            get
            {
                return Chart.Elements;
            }
        }

        public bool IsAutoRange
        {
            get
            {
                return Chart.IsAutoRange;
            }
            set
            {
                Chart.IsAutoRange = value;
            }
        }

        public ChartAxisType XAxisType
        {
            get
            {
                return Chart.XAxisType;
            }
            set
            {
                Chart.XAxisType = value;
            }
        }

        public string ChartTheme
        {
            get
            {
                return Chart.ChartTheme;
            }
            set
            {
                Chart.ChartTheme = value;
            }
        }

        public ChartPanelOrderSettings OrderSettings
        {
            get
            {
                return _orderSettings;
            }
        }

        public ChartPanelShareSettings ShareSettings
        {
            get
            {
                return _shareSettings;
            }
        }

        public bool DisableIndicatorReset
        {
            get
            {
                return Chart.DisableIndicatorReset;
            }
            set
            {
                Chart.DisableIndicatorReset = value;
            }
        }

        public void AddArea( ChartArea area )
        {
            Chart.AddArea( area );
        }

        public void RemoveArea( ChartArea area )
        {
            Chart.RemoveArea( area );
        }

        public void ClearAreas( )
        {
            Chart.ClearAreas( );
        }

        public void AddElement( ChartArea area, IChartElement element )
        {
            Chart.AddElement( area, element );
        }

        public void AddElement( ChartArea area, ChartCandleElement element, CandleSeries candleSeries )
        {
            Chart.AddElement( area, element, candleSeries );
        }

        public void AddElement( ChartArea area, ChartIndicatorElement element, CandleSeries candleSeries, IIndicator indicator )
        {
            Chart.AddElement( area, element, candleSeries, indicator );
        }

        public void AddElement( ChartArea area, OrdersUI element, Security security )
        {
            Chart.AddElement( area, element );
        }

        public void AddElement( ChartArea area, TradesUI element, Security security )
        {
            Chart.AddElement( area, element );
        }

        public void RemoveElement( ChartArea area, IChartElement element )
        {
            ( ( IChart )Chart ).RemoveElement( area, element );
        }

        public IIndicator GetIndicator( ChartIndicatorElement element )
        {
            return Chart.GetIndicator( element );
        }

        public object GetSource( IChartElement element )
        {
            return Chart.GetSource( element );
        }

        public void SetSource( IChartElement element, object source )
        {
            Chart.SetSource( element, source );
        }

        public bool IsInteracted
        {
            get
            {
                return Chart.IsInteracted;
            }
            set
            {
                Chart.IsInteracted = value;
            }
        }

        public bool OrderCreationMode
        {
            get
            {
                return Chart.OrderCreationMode;
            }
            set
            {
                Chart.OrderCreationMode = value;
            }
        }

        public int MinimumRange
        {
            get
            {
                return Chart.MinimumRange;
            }
            set
            {
                Chart.MinimumRange = value;
            }
        }

        public bool ShowOverview
        {
            get
            {
                return Chart.ShowOverview;
            }
            set
            {
                Chart.ShowOverview = value;
            }
        }

        public TimeZoneInfo TimeZone
        {
            get
            {
                return Chart.TimeZone;
            }
            set
            {
                Chart.TimeZone = value;
            }
        }

        public IList<IndicatorType> IndicatorTypes
        {
            get
            {
                return Chart.IndicatorTypes;
            }
        }

        public void ReSubscribeElements( )
        {
            Chart.ReSubscribeElements( );
        }

        private void OnShareSettingsPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            string propertyName = e.PropertyName;
            if ( propertyName != "IsEnabled" )
            {
                if ( propertyName == "Period" && _timer != null )
                {
                    _timer.Interval = ShareSettings.Period;
                }
            }
            else if ( ShareSettings.IsEnabled )
            {
                Upload( ShareSettings.FileName, !ShareSettings.Published );

                _timer = new DispatcherTimer( ShareSettings.Period, DispatcherPriority.Background, new EventHandler( UploadEventHandler ), Dispatcher );
                _timer.Start( );

                ShareSettings.Published = true;
            }
            else if ( _timer != null )
            {
                _timer.Stop( );
                _timer = null;
            }

            SettingsChanged?.Invoke( );
        }

        private void OnSettingsClicked( object sender, ItemClickEventArgs e )
        {
            SettingsChanged?.Invoke( );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "OrderSettings", OrderSettings.Save( ) );
            storage.SetValue( "ShareSettings", ShareSettings.Save( ) );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl( ) );
            storage.SetValue( "Chart", Chart.Save( ) );
        }

        public void Load( SettingsStorage storage )
        {
            OrderSettings.Load( storage.GetValue<SettingsStorage>( "OrderSettings", null ) );
            ShareSettings.Load( storage.GetValue<SettingsStorage>( "ShareSettings", null ) );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings != null )
                BarManager.LoadDevExpressControl( settings );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "Chart", null );
            if ( storage1 == null )
                return;
            Chart.Load( storage1 );
        }

        public INotifyList<ChartArea> ChartAreas
        {
            get
            {
                return Chart.ChartAreas;
            }
        }

        public bool IsAutoScroll
        {
            get
            {
                return Chart.IsAutoScroll;
            }
            set
            {
                Chart.IsAutoScroll = value;
            }
        }

        public void Reset( IEnumerable<IChartElement> elements )
        {
            Chart.Reset( elements );
        }

        public void Draw( ChartDrawData data )
        {
            Chart.Draw( data );
        }

        private void ExecuteRegisterOrderCommand( object sender, ExecutedRoutedEventArgs e )
        {
        }

        private void CanExecuteRegisterOrderCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = RegisterOrder != null;
        }

        private void ExecuteAddAreaCommand( object sender, ExecutedRoutedEventArgs e )
        {
            ChartArea area = new ChartArea( )
            {
                Title = LocalizedStrings.Panel + " " +   ( Chart.ChartAreas .Count + 1 )
            };

            var viewModel = new ScichartSurfaceMVVM( area );
            area.ChartSurfaceViewModel = viewModel;

            var timeZoneInfo = Chart.GetTimeZone( );

            foreach ( ChartAxis chartAxis in area.XAxises )
            {
                chartAxis.TimeZone = timeZoneInfo;
            }

            Chart.AddArea( area );
        }

        private void CanExecuteAddAreaCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Chart != null && IsInteracted;
        }

        public IBackupService BackupService
        {
            get
            {
                return _backupService;
            }
            set
            {
                _backupService = value;
                UpdateBackupButton( _backupService != null );
            }
        }

        private void Upload( string fileName, bool isPublished )
        {
            IBackupService backupService = BackupService;
            if ( backupService == null )
            {
                return;
            }

            Stream image = Chart.SaveToImage( );
            BackupEntry backup = new BackupEntry( );
            backup.Name = ( fileName );

            //using ( new Scope<Window>( XamlHelper.GetWindow( ( DependencyObject )this ) ) )
            //{
            //    if ( backupService.Upload( backup, image, a => { } ).IsCancellationRequested || !isPublished )
            //    {
            //        return;
            //    }

            //    string url = backupService.Publish( backup );
            //    if ( StringHelper.IsEmpty( url ) )
            //    {
            //        return;
            //    }

            //    url.CopyToClipboard( );
            //    url.TryOpenLink( ( DependencyObject )this );
            //}
        }

        private void SaveButtonClicked( object sender, ItemClickEventArgs e )
        {
            //VistaSaveFileDialog vistaSaveFileDialog = new VistaSaveFileDialog( );
            //vistaSaveFileDialog.RestoreDirectory = true;
            //vistaSaveFileDialog.Filter = "Png files (*.png)|*.png|All files (*.*)|*.*";
            //vistaSaveFileDialog.DefaultExt = "png";
            //VistaSaveFileDialog dlg = vistaSaveFileDialog;
            //if ( !dlg.ShowModal( this ) )
            //    return;
            //string fileName = dlg.FileName;
            //Chart.SaveToImage( ).Save( fileName );
            //if ( new MessageBoxBuilder( ).Owner( this ).Caption( LocalizedStrings.Export ).Text( LocalizedStrings.ExportDoneOpenFile.Put( ( object )Path.GetFileName( fileName ) ) ).YesNo( ).Show( ) != MessageBoxResult.Yes )
            //    return;
            //fileName.TryOpenLink( this );
        }

        private void CandleStyles_EditValueChanged( object sender, RoutedEventArgs e )
        {
            var candleElements = Chart.ChartAreas.SelectMany( a => a.Elements.OfType<ChartCandleElement>( ) );

            foreach ( var candleElment in candleElements )
            {
                candleElment.DrawStyle = ( ( ImageNameDrawStyle )CandleStyles.EditValue ).DrawStyle( );
            }
        }

        private void RegisterOrderCommandItemClick( object sender, ItemClickEventArgs e )
        {
            string string_0;
            if ( !ShareSettings.IsEnabled )
                string_0 = "Chart_{0:yyyyMMdd_HHmmssfff}.png".Put( DateTime.Now );
            else
                string_0 = ShareSettings.FileName;
            Upload( string_0, true );
        }

        private void UpdateBackupButton( bool hasBackupService )
        {
            Action tobeDone = delegate ( )
            {
                Ctrl.ShareButton.IsEnabled = hasBackupService;
            };

            this.GuiAsync( tobeDone );
        }

        private void OnCreateOrder( ChartArea area, Order order )
        {
            if ( OrderSettings.Portfolio == null )
            {
                PortfolioPickerWindow wnd = new PortfolioPickerWindow( )
                {
                    Portfolios = ConfigManager.GetService<PortfolioDataSource>( )
                };
                if ( wnd.ShowModal( this ) )
                    OrderSettings.Portfolio = wnd.SelectedPortfolio;
            }

            var array       = area.Elements.OfType<ChartCandleElement>( ).ToArray( );
            order.Security  = ( array.Length == 1 ? ( ( CandleSeries )GetSource( array[ 0 ] ) )?.Security : OrderSettings.Security );
            order.Portfolio = ( OrderSettings.Portfolio );
            order.Volume    = ( OrderSettings.Volume );

            var action1 = RegisterOrder;
            if ( action1 == null )
                return;
            action1( area, order );
        }



        private void BackupServiceRegistered( Type type_0, object obj )
        {
            if ( !( obj is IBackupService backupService ) )
                return;
            BackupService = backupService;
        }

        private void UploadEventHandler( object sender, EventArgs e )
        {
            Upload( ShareSettings.FileName, !ShareSettings.Published );
        }

        public void InvokeAnnotationCreatedEvent( ChartAnnotation annotation )
        {
            AnnotationCreated?.Invoke( annotation );
        }

        public void InvokeAnnotationModifiedEvent( ChartAnnotation annotation, ChartDrawData.sAnnotation aData )
        {
            AnnotationModified?.Invoke( annotation, aData );
        }

        public void InvokeAnnotationDeletedEvent( ChartAnnotation annotation )
        {
            AnnotationDeleted?.Invoke( annotation );
        }

        public void InvokeAnnotationSelectedEvent( ChartAnnotation annotation, ChartDrawData.sAnnotation aData )
        {
            AnnotationSelected?.Invoke( annotation, aData );
        }


        //public void InvokeAnnotationCreatedEvent( ChartAnnotation annotation )
        //{
        //    ( ( IChart )Chart ).InvokeAnnotationCreatedEvent( annotation );
        //}

        //public void InvokeAnnotationModifiedEvent( ChartAnnotation annotation, ChartDrawData.sAnnotation aData )
        //{
        //    ( ( IChart )Chart ).InvokeAnnotationModifiedEvent( annotation, aData );
        //}

        //public void InvokeAnnotationSelectedEvent( ChartAnnotation annotation, ChartDrawData.sAnnotation aData )
        //{
        //    ( ( IChart )Chart ).InvokeAnnotationSelectedEvent( annotation, aData );
        //}

        //public void InvokeAnnotationDeletedEvent( ChartAnnotation annotation )
        //{
        //    ( ( IChart )Chart ).InvokeAnnotationDeletedEvent( annotation );
        //}

        private sealed class ImageNameDrawStyle
        {
            private readonly ImageSource _image;
            private readonly string _name;
            private readonly ChartCandleDrawStyles _candleDrawStyle;

            public ImageNameDrawStyle( ImageSource source, string name, ChartCandleDrawStyles candleDrawStyle )
            {
                if ( name.IsEmpty( ) )
                {
                    throw new ArgumentNullException( "name" );
                }
                    
                ImageSource imageSource = source;

                if ( imageSource == null )
                {
                    throw new ArgumentNullException( "image" );
                }
                    
                _image = imageSource;
                _name = name;
                _candleDrawStyle = candleDrawStyle;
            }

            public ImageSource Image
            {
                get
                {
                    return _image;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public ChartCandleDrawStyles DrawStyle( )
            {
                return _candleDrawStyle;
            }
        }
    }
}
