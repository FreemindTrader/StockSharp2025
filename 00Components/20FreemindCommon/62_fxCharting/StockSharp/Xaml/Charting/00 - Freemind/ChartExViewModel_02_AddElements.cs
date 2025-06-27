using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using fx.Charting;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using fx.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml;
using StockSharp.Messages;
using fx.Indicators;
using fx.Charting.IndicatorPainters;
using StockSharp.Charting;


#pragma warning disable 067

namespace fx.Charting
{
    public partial class ChartExViewModel
    {
        public void LoadCandlesOfXTimeFrame( TimeSpan newTimeSpan )
        {
            var uiSeriesPair = GetCandleUISeries();
            if ( uiSeriesPair == null )
                return;

            var oldTimeSpan = (TimeSpan) uiSeriesPair.Item2.Arg;

            if ( oldTimeSpan != TimeSpan.Zero && ( oldTimeSpan == newTimeSpan ) )
            {
                return;
            }

            var newSeries        = uiSeriesPair.Item2.Clone();
            newSeries.Arg = newTimeSpan;
            newSeries.CandleType = typeof( TimeFrameCandle );

            OnRebuildCandles( uiSeriesPair.Item1, newSeries );
        }


        private void OnRebuildCandles( IfxChartElement element, CandleSeries series )
        {
            var candleUI = element as CandlestickUI;
            if ( candleUI == null )
                return;

            var candleArea   = candleUI.ChartArea;
            var candleSeries = GetSource< CandleSeries > ( candleUI );
            var indicatorUI  = _uiDatasource.Where( p =>
                                                            {
                                                                var candleS = ( CandleSeries ) p.Value;
                                                                if( candleS == candleSeries )
                                                                {
                                                                    /* -------------------------------------------------------------------------------------------------------------------------------------------
                                                                     * 
                                                                     *  If the UI has the same CandleSeries, but not the CandlestickUI,
                                                                     *  then it must be IndicatorUI
                                                                     * 
                                                                     * ------------------------------------------------------------------------------------------------------------------------------------------- */
                                                                     
                                                                    return p.Key != ( IfxChartElement ) candleUI;
                                                                }

                                                                return false;
                                                            }
                                                     ).Select( p => ( IndicatorUI ) p.Key );

            var indicatorUiDict = indicatorUI.ToDictionary( ( p => p ), ind => Tuple.Create( GetIndicator( ind ), ind.ChartArea ) );


            OnRemoveElement( candleUI );

            Ecng.Collections.CollectionHelper.ForEach( indicatorUiDict.Keys, OnRemoveElement );

            

            ( ( IElementWithXYAxes ) candleUI ).ResetUI();
            AddElement( candleArea, candleUI, series );

            foreach ( var indicatorPair in indicatorUiDict )
            {
                var area  = indicatorPair.Value.Item2;
                var ui    = indicatorPair.Key;
                var val   = indicatorPair.Value.Item1;

                AddElement( area, ui, series, val );
            }

            RefreshView();
        }

        private void RefreshView()
        {
            Tuple< CandlestickUI, CandleSeries> tuple = GetCandleUISeries();

            if ( tuple == null )
                return;

            if ( CandleSeriesRebuilt == tuple?.Item2 && SelectedSecurity == tuple?.Item2.Security )
                return;

            CandleSeriesRebuilt = tuple?.Item2;
            SelectedSecurity = CandleSeriesRebuilt.Security;
            Action myEvent = RefreshEvent;
            if ( myEvent == null )
                return;
            myEvent();
        }

        private Tuple<CandlestickUI, CandleSeries> GetCandleUISeries()
        {
            foreach ( var chartElement in ChartAreas.SelectMany( a => a.Elements ) )
            {
                var candle = chartElement as CandlestickUI;

                if ( candle != null )
                {
                    CandleSeries candleSeries = GetSource< CandleSeries > ( candle );
                    if ( candleSeries?.Security != null )
                        return Tuple.Create( candle, candleSeries );
                }
            }
            return null;
        }

        /* --------------------------------------------------------------------------------------------------------------------------------------------------
        *  
        *  Step A ---------->   1a The above _chartAreas.Add( area ) caused the collection's Added Event to be Invoked which register all the UI Element
        *                       Added Eventhandler
        * 
        * -------------------------------------------------------------------------------------------------------------------------------------------------- */
        private void OnNewAreaAddedToChartArea( ChartArea area )
        {
            area.Elements.Added += new Action<IfxChartElement>( Step3b_OnNewChartAreaAdded );
            area.Elements.Removed += new Action<IfxChartElement>( OnUIRemovedFromArea );

            area.Chart = this;

            ScichartSurfaceViewModels.Add( ( ScichartSurfaceMVVM ) area.ChartSurfaceViewModel );

            Ecng.Collections.CollectionHelper.ForEach( area.Elements, Step3b_OnNewChartAreaAdded );

            
        }

        private void OnAreaRemovedFromChartArea( ChartArea area )
        {
            area.Elements.Added -= new Action<IfxChartElement>( Step3b_OnNewChartAreaAdded );
            area.Elements.Removed -= new Action<IfxChartElement>( OnUIRemovedFromArea );

            ScichartSurfaceViewModels.Remove( ( ScichartSurfaceMVVM ) area.ChartSurfaceViewModel );

            Ecng.Collections.CollectionHelper.ForEach( area.Elements, OnUIRemovedFromArea );
            
            area.Chart = null;

            area.Dispose();
        }

        private bool OnClearChartArea()
        {
            foreach ( ChartArea area in ChartAreas )
            {
                OnAreaRemovedFromChartArea( area );
            }

            InitVisibleRangeDP();
            return true;
        }

        public int ChartCount { get; set; }

        List< ChartArea > _indicatorAreas = new List<ChartArea>();
        private List< ScichartSurfaceMVVM >  _indicatorSurfaces = new List<ScichartSurfaceMVVM>();




        

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
        *  
        *  Step A ----------> 1
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void OnAddCandlesArea( ChartArea candleArea )
        {
            _candleStickUI = new CandlestickUI();

            _candleStickUI.ShowAxisMarker = false;

            if ( _candleSeries == null )
            {
                var candleSeries        = new CandleSeries( );
                candleSeries.CandleType = typeof( TimeFrameCandle );
                candleSeries.Arg        = TimeSpan.FromMinutes( 5.0 );
                _candleSeries           = candleSeries;
            }

            var selectedSeries     = CustomShowWindowService.ShowCandleWindow( _candleSeries );
            _candleSeries          = selectedSeries.Clone();
            _period                = (TimeSpan) _candleSeries.Arg;

            //_candleSeries.Security = null;

            AddElement( candleArea, _candleStickUI, selectedSeries );
            selectedSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
        }


        #region Add Orders
        private void OnAddOrdersArea( ChartArea area )
        {
            var selectedSecurity = CustomShowWindowService.ShowSecurityPickerWindow( MultiSelectMode.Row );

            OrdersUI element = new OrdersUI( );

            AddElement( area, element, selectedSecurity );
        }

        public void AddElement( ChartArea area, OrdersUI element, Security security )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            if ( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            _uiDatasource.Add( element, security );

            if ( element.FullTitle.IsEmpty() )
            {
                element.FullTitle = "{0} ({1})".Put( security.Code, element.GetType().GetDisplayName( null ).ToLower() );
            }

            AddElement( area, element );
        }
        #endregion


        #region Add Indicators 

        private void OnAddIndicatorArea( ChartArea area )
        {
            var indicatorPicker = CustomShowWindowService.ShowIndicatorWindow( true, IndicatorTypes );

            var array   = Elements.OfType<CandlestickUI>( ).ToArray( );
            var chartUi = area.Elements.OfType<CandlestickUI>( ).Concat( array ).FirstOrDefault( );

            if ( chartUi == null )
            {
                MessageBoxResult canCloseDocument = MessageBoxService.Show( messageBoxText: LocalizedStrings.ChartAreaName, caption: "Add Indicator", button: MessageBoxButton.OK );
            }
            else
            {
                if ( !indicatorPicker.AutoSelectCandles )
                {
                    var selectedElement = CustomShowWindowService.ShowCandlestickUIPicker( array, chartUi );

                    chartUi = selectedElement;
                }

                /* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Tony Indicator Step 1: The above code will show the indicator Window and let us select the wanted indicator and the following will create the Indicator and the 
                 *                  corresponding indicator painter.
                 *                  
                 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------                 
                 */

                if ( indicatorPicker.SelectedIndicatorType == null )
                    return;

                var indicatorUI              = new IndicatorUI( );

                var indicatorPainter                  = indicatorPicker.SelectedIndicatorType.CreatePainter();

                

                indicatorUI.IndicatorPainter = (fx.Charting.IChartIndicatorPainter ) indicatorPainter;

                var tonyCandleSeries         = GetSeries<CandleSeries>( chartUi );

                AddElement( area, indicatorUI, tonyCandleSeries, indicatorPicker.Indicator );
            }
        }



        public void AddElement( ChartArea area, IndicatorUI indicatorUI, CandleSeries candleSeries, IIndicator indicator )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( indicatorUI == null )
            {
                throw new ArgumentNullException( nameof( indicatorUI ) );
            }

            if ( candleSeries == null )
            {
                throw new ArgumentNullException( nameof( candleSeries ) );
            }

            if ( indicator == null )
            {
                throw new ArgumentNullException( nameof( indicator ) );
            }

            _uiDatasource.Add( indicatorUI, candleSeries );

            _indicators.Add( indicatorUI, indicator );

            if ( !DisableIndicatorReset )
            {
                indicator.Reseted += () => OnIndicatorReset( indicatorUI, indicator );
            }

            if ( StringHelper.IsEmpty( indicatorUI.FullTitle ) )
            {
                indicatorUI.FullTitle = indicator.ToString();
            }

            indicatorUI.CreateIndicatorPainter( IndicatorTypes, indicator );
            AddElement( area, indicatorUI );
        }

        

        #endregion


        #region Add Candles


        

        

        private void OnCodingAddQuotes( ChartArea area, Tuple<double, double> quotes )
        {
            //var candleUI = new CandlestickUI( );
            //_candleSeries = candleSeries.Clone( );
            //_candleSeries.Security = null;

            //AddElement( area, candleUI, candleSeries );

            //candleSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
        }



        public void AddElement( ChartArea area, CandlestickUI element, CandleSeries candleSeries )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            if ( candleSeries == null )
            {
                throw new ArgumentNullException( nameof( candleSeries ) );
            }

            _uiDatasource.Add( element, candleSeries );

            element.Title = candleSeries.Title();

            AddElement( area, element );
        }

        private void OnCandleSeriesPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !IsInteracted )
            {
                return;
            }

            var candleSeries = ( CandleSeries )sender;

            foreach ( IfxChartElement element in Elements.Where( x => GetSource( x ) == candleSeries ).ToArray() )
            {
                var CandlestickUI = element as CandlestickUI;

                if ( CandlestickUI != null )
                {
                    CandlestickUI.Title = candleSeries.Title();
                }

                ResetElement( element, true );
            }
        }
        #endregion






        #region Add Trades
        private void OnAddTradesArea( ChartArea area )
        {
            var selectedSecurity = CustomShowWindowService.ShowSecurityPickerWindow( MultiSelectMode.Row );

            TradesUI element = new TradesUI( );
            AddElement( area, element, selectedSecurity );
        }

        public void AddElement( ChartArea area, TradesUI element, Security security )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            if ( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            _uiDatasource.Add( element, security );

            if ( element.FullTitle.IsEmpty() )
            {
                element.FullTitle = "{0} ({1})".Put( security.Code, element.GetType().GetDisplayName( null ).ToLower() );
            }

            AddElement( area, element );
        }
        #endregion



        #region Remove Element
        private void OnRemoveElement( IfxChartElement element )
        {
            if ( element is IndicatorUI indicator && indicator.ParentElement != null )
            {
                element = indicator.ParentElement;
            }

            ( ( IChart ) this ).RemoveElement( element.ChartArea, element );

            RefreshView();
        }

        #endregion


        #region Common Add Area

        public void AddElement( ChartArea area, IfxChartElement element )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            DispatcherService.BeginInvoke( () =>
            {
                area.Elements.Add( element );
            } );
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
        *  
        *  Step A ----------> 3 Now that NotifyList OnAdded Event has been invoked.
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void Step3b_OnNewChartAreaAdded( IfxChartElement element )
        {
            RefreshView();

            if ( !IsInteracted )
            {
                return;
            }

            AddElement( element );
        }

        private void AddElement( IfxChartElement element )
        {
            if ( GetSource( element ) == null )
            {
                return;
            }

            _uiList.Add( element );
            RaiseChartElementSubscribedEvent( element );
        }

        private void RaiseChartElementSubscribedEvent( IfxChartElement chartElement )
        {
            switch ( chartElement )
            {
                case CandlestickUI CandlestickUI:
                {
                    /* -------------------------------------------------------------------------------------------------------------------------------------------
                    *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
                    *  
                    *  Step A ----------> 4 After Candle get added to the UI PooledList, we raise Chart Element Subscribed Event.
                    * 
                    * ------------------------------------------------------------------------------------------------------------------------------------------- 
                    */
                    SubscribeCandleElement?.Invoke( CandlestickUI, GetSeries<CandleSeries>( CandlestickUI ) );
                }
                break;

                case IndicatorUI element:
                {
                    SubscribeIndicatorElement?.Invoke( element, GetSeries<CandleSeries>( element ), GetIndicator( element ) );
                }
                break;

                case OrdersUI chartOrderElement:
                {
                    SubscribeOrderElement?.Invoke( chartOrderElement, GetSeries<Security>( chartOrderElement ) );
                }
                break;

                case TradesUI chartTradeElement:
                {
                    SubscribeTradeElement?.Invoke( chartTradeElement, GetSeries<Security>( chartTradeElement ) );
                }
                break;
            }
        }




        public TimeZoneInfo GetTimeZone()
        {
            return ChartAreas.Select( a => a.XAxises.FirstOrDefault( i => i.TimeZone != null ) ).LastOrDefault( ax => ax != null )?.TimeZone;
        }



        public void RemoveArea( ChartArea area )
        {
            DispatcherService.BeginInvoke( () => _chartAreas.Remove( area ) );
        }

        public void ReSubscribeElements()
        {
            if ( !IsInteracted )
            {
                return;
            }

            foreach ( IfxChartElement chartElement in Elements )
            {
                RemoveAndRaiseUnsubscribeElementEvent( chartElement );
                AddElement( chartElement );
            }
        }

        private void OnUIRemovedFromArea( IfxChartElement element )
        {
            RefreshView();
            ResetElement( element, false );
        }

        private void OnIndicatorReset( IndicatorUI indicatorElement, IIndicator indicator )
        {
            indicatorElement.FullTitle = indicator.ToString();
            ResetElement( indicatorElement, true );
        }

        public IEnumerable<IfxChartElement> Elements
        {
            get
            {
                return _uiList.Cache;
            }
        }

        private void ResetElement( IfxChartElement element, bool needAddElement )
        {
            IfxChartElement[ ] elementArray;
            if ( element is CandlestickUI )
            {
                elementArray = Elements.Where( e => GetSource( e ) == GetSeries<CandleSeries>( element ) ).ToArray();
            }
            else
            {
                elementArray = new IfxChartElement[ 1 ]
                {
                    element
                };
            }

            if ( needAddElement )
            {
                if ( IsInteracted )
                {
                    Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IfxChartElement>( RaiseUnsubscribeElementEvent ) );
                    
                }

                Reset( elementArray );

                if ( !IsInteracted )
                {
                    return;
                }


                Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IfxChartElement>( RaiseChartElementSubscribedEvent ) );
                
            }
            else
            {
                if ( !IsInteracted )
                {
                    return;
                }

                Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IfxChartElement>( RemoveAndRaiseUnsubscribeElementEvent ) );                
            }
        }

        private void RaiseUnsubscribeElementEvent( IfxChartElement element )
        {
            UnSubscribeElement?.Invoke( element );
        }


        private void RemoveAndRaiseUnsubscribeElementEvent( IfxChartElement element )
        {
            if ( GetSource( element ) == null )
            {
                return;
            }

            _uiList.Remove( element );

            UnSubscribeElement?.Invoke( element );
        }

        public IIndicator GetIndicator( IndicatorUI element )
        {
            return _indicators.TryGetValue( element );
        }

        private T GetSource<T>( IfxChartElement _param1 ) where T : class
        {
            return ( T ) GetSource( _param1 );
        }

        public object GetSource( IfxChartElement element )
        {
            return _uiDatasource.TryGetValue( element );
        }

        public void SetSource( IfxChartElement element, object source )
        {
            _uiDatasource[ element ] = source;
            ( ( IElementWithXYAxes ) element ).ResetUI();
        }

        public void Reset( IEnumerable<IfxChartElement> elements )
        {
            _chartAreas.ResetChartAreas( elements.ToArray() );
        }



        private T GetSeries<T>( IfxChartElement element ) where T : class
        {
            return ( T ) GetSource( element );
        }


        void RemoveElement( ChartArea area, IfxChartElement element )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            IndicatorUI indicator = element as IndicatorUI;
            if ( indicator != null )
            {
                _indicators.Remove( indicator );
            }

            DispatcherService.BeginInvoke( () =>
            {
                area.Elements.Remove( element );
                _uiDatasource.Remove( element );
            }
            );
        }
        #endregion

        #region OnAreaAdded

        private readonly ChartAreasList _chartAreas;

        public INotifyList<ChartArea> ChartAreas
        {
            get
            {
                return _chartAreas;
            }
        }


        public void AddArea( ChartArea area )
        {
            DispatcherService.BeginInvoke( () =>
                                                {
                                                    _chartAreas.Add( area );
                                                }
            );
        }


        #endregion

        #region Add Quotes
        private void OnAddQuotesEvent( ChartArea area, Tuple<double, double> quote )
        {
            var quotesUI = new QuotesUI( );

            _uiDatasource.Add( quotesUI, quote );

            AddElement( area, quotesUI );
        }
        #endregion

    }
}
