using DevExpress.Mvvm;
using SciChart.Charting.Visuals;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
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


namespace StockSharp.Xaml.Charting
{
    /// <summary>
    /// Interaction logic for ChartExView.xaml
    /// </summary>
    public partial class ChartExView : UserControl
    {        
        ChartExViewModel _chartVM;

        bool _initQuoteVM = false;

        public ChartExView( )
        {                        
            LicenseManager.CreateInstance( );

            InitializeComponent( );

            _chartVM = ( ChartExViewModel ) DataContext;

            Messenger.Default.Register<QuoteMessage>( this, x => OnQuoteUpdate( x ) );
        }



        private void OnQuoteUpdate( QuoteMessage quote )
        {
            if ( ! _initQuoteVM )
            {
                if ( _chartVM.SurfaceVM != null )
                {
                    var pane = _chartVM.SurfaceVM;

                    if ( pane != null )
                    {
                        if ( _chartVM.CanExecuteAddQuotes( ) )
                        {
                            _chartVM.ExecuteAddQuotes( pane.Area, new Tuple<double, double>( quote.Bid, quote.Ask ) );

                            _initQuoteVM = true;
                        }
                    }
                }                
            }
            else
            {
                if ( _chartVM.SurfaceVM != null && _chartVM.IsActive && quote.Security == _chartVM.SelectedSecurity )
                {
                    throw new NotImplementedException();
                    //_chartVM.SurfaceVM.UpdateQuote( quote.QuoteTime, quote.Bid, quote.Ask );
                }
            }                        
        }

        internal static void SetupScichartSurface( SciChartSurface chartSurface )
        {
            chartSurface.DebugWhyDoesntSciChartRender = true;
           
            if ( chartSurface.DataContext != null )
            {
                ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
            }
            else
            {
                chartSurface.DataContextChanged += ( s, e ) =>
                {
                    ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
                };
            }
        }

        private void OnInitialized( object sender, EventArgs e )
        {
            SetupScichartSurface( ( SciChartSurface )sender );
        }
    }
}
