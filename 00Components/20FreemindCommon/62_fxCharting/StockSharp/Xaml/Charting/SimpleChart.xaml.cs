using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using SciChart.Charting.Visuals;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml.Charting
{
    public sealed partial class SimpleChartDx : UserControl, INotifyPropertyChanged, IComponentConnector, IPersistable, INotifyPropertyChangedEx, IChart, IThemeableChart
    {
        private ChartAxisType _xAxisType = ChartAxisType.Numeric;
        private readonly ChartArea _area;
        private PropertyChangedEventHandler _propertyChangedEventHandler;

        static SimpleChartDx( )
        {
            LicenseManager.CreateInstance( );
        }

        public SimpleChartDx( )
        {
            InitializeComponent( );

            //Chart.SetupUltraChartSurface( Surface );

            _area = new ChartArea( );
            var viewModel = new ScichartSurfaceMVVM( _area );
            _area.ChartSurfaceViewModel = viewModel;


            DataContext = Area.ChartSurfaceViewModel;

            _area.XAxisType = XAxisType;

            _area.Chart = this;
        }


        internal static void SetupUltraChartSurface( SciChartSurface chartSurface )
        {
            if ( chartSurface.DataContext != null )
            {
                ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
            }
            else
            {
                chartSurface.DataContextChanged += ( s, e ) => ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
            }
        }

        private void OnInitialized( object sender, EventArgs e )
        {
            SetupUltraChartSurface( ( SciChartSurface )sender );
        }

        public ChartArea Area
        {
            get
            {
                return _area;
            }
        }

        public ChartAxisType XAxisType
        {
            get
            {
                return _xAxisType;
            }
            set
            {
                if ( _xAxisType == value )
                {
                    return;
                }

                if ( Area.Elements.Any( ) )
                {
                    throw new InvalidOperationException( LocalizedStrings.AxisTypeCantBeSet );
                }

                _xAxisType = value;
                Area.XAxisType = _xAxisType;
            }
        }



        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChangedEventHandler += value;
            }
            remove
            {
                _propertyChangedEventHandler -= value;
            }
        }



        void INotifyPropertyChangedEx.NotifyPropertyChanged( string propertyName )
        {            
            _propertyChangedEventHandler?.Invoke( this, propertyName );
        }

        public INotifyList<ChartArea> ChartAreas
        {
            get
            {
                propertyName lst = new propertyName( );
                lst.Add( Area );
                return lst;
            }
        }

        public void Draw( ChartDrawData data )
        {
            Area.ChartSurfaceViewModel.Draw( data );
        }

        public void Reset( IEnumerable<IChartElement> elements )
        {
            Area.ChartSurfaceViewModel.Reset( elements );
        }

        IList<IndicatorType> IChart.IndicatorTypes
        {
            get
            {
                return null;
            }
        }





        bool IChart.IsAutoScroll
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotSupportedException( );
            }
        }



        bool IChart.IsAutoRange
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotSupportedException( );
            }
        }



        string IThemeableChart.ChartTheme
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotSupportedException( );
            }
        }



        IIndicator IChart.GetIndicator( IndicatorUI element )
        {
            throw new NotSupportedException( );
        }

        object IChart.GetSource( IChartElement element )
        {
            throw new NotSupportedException( );
        }

        void IChart.AddArea( ChartArea area )
        {
            throw new NotSupportedException( );
        }

        void IChart.RemoveArea( ChartArea area )
        {
            throw new NotSupportedException( );
        }

        void IChart.ClearAreas( )
        {
            throw new NotSupportedException( );
        }

        void IChart.AddElement( ChartArea area, IChartElement element )
        {
            throw new NotSupportedException( );
        }

        void IChart.AddElement(
          ChartArea area,
          CandlestickUI element,
          CandleSeries candleSeries )
        {
            throw new NotSupportedException( );
        }

        void IChart.AddElement(
          ChartArea area,
          IndicatorUI element,
          CandleSeries candleSeries,
          IIndicator indicator )
        {
            throw new NotSupportedException( );
        }

        void IChart.AddElement( ChartArea area, OrdersUI element, Security security )
        {
            throw new NotSupportedException( );
        }

        void IChart.AddElement( ChartArea area, TradesUI element, Security security )
        {
            throw new NotSupportedException( );
        }

        void IChart.RemoveElement( ChartArea area, IChartElement element )
        {
            throw new NotSupportedException( );
        }

        void IPersistable.Load( SettingsStorage settings )
        {
            throw new NotSupportedException( );
        }

        void IPersistable.Save( SettingsStorage settings )
        {
            throw new NotSupportedException( );
        }

        public void InvokeAnnotationCreatedEvent( ChartAnnotation annotation )
        {
            throw new NotImplementedException( );
        }

        public void InvokeAnnotationModifiedEvent( ChartAnnotation annotation, ChartDrawData.sAnnotation aData )
        {
            throw new NotImplementedException( );
        }

        public void InvokeAnnotationSelectedEvent( ChartAnnotation annotation, ChartDrawData.sAnnotation aData )
        {
            throw new NotImplementedException( );
        }

        public void InvokeAnnotationDeletedEvent( ChartAnnotation annotation )
        {
            throw new NotImplementedException( );
        }

        private sealed class propertyName : BaseList<ChartArea>
        {
        }
    }
}
