using Ecng.Collections;
using fx.Algorithm;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.Annotations;
using fx.Definitions;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using fx.Bars;

namespace StockSharp.Xaml.Charting
{
    public partial class ChartDrawData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartDrawData" />.
        /// </summary>
        public ChartDrawData()
        {
        }

        public IndicatorValuesList SetIndicatorSource( IndicatorUI indicatorUI, int capacity )
        {
            var indicatorMap = GetIndicatorMap();

            if ( indicatorMap.ContainsKey( indicatorUI ) )
            {
                return indicatorMap[ indicatorUI ];
            }
            else
            {
                var indicatorList = new IndicatorValuesList( capacity );

                indicatorMap.Add( indicatorUI, indicatorList );

                return indicatorList;
            }
        }

        public bool SetCandleSource( CandlestickUI candleUI, fxHistoricBarsRepo bars, uint begin, uint end )
        {
            if ( end - begin + 1 > 0 )
            {
                _candleMap = new PooledDictionary<CandlestickUI, sCandleEx>();

                var candleEx = new sCandleEx( bars, begin, end );

                _candleMap.Add( candleUI, candleEx );

                return true;
            }

            return false;
        }


        public bool SetCandleSource( CandlestickUI candleUI, fxHistoricBarsRepo bars, (uint begin, uint end) range )
        {
            if ( range != default )
            {
                _candleMap = new PooledDictionary<CandlestickUI, sCandleEx>();

                var candleEx = new sCandleEx( bars, range.begin, range.end );

                _candleMap.Add( candleUI, candleEx );

                return true;
            }

            return false;
        }

        public bool SetCandleSource( CandlestickUI candleUI, fxHistoricBarsRepo bars, ref SBar singleBar )
        {
            _candleMap = new PooledDictionary<CandlestickUI, sCandleEx>();

            var candleEx = new sCandleEx( bars, singleBar.BarIndex, singleBar.BarIndex );

            _candleMap.Add( candleUI, candleEx );

            return true;
        }

        public interface IDrawValue { }
        /// <summary>Interface which represents all chart draw data types.</summary>
        //public interface IDrawValue<T> where T : struct, IDrawValue<T> { }


        //public struct sCandleColor : IDrawValue<sCandleColor>
        
        private PooledDictionary<CandlestickUI, sCandleEx > _candleMap;

        private PooledDictionary<CandlestickUI, PooledList<sCandleColor>> _candleColorMap;



        

        public PooledDictionary<CandlestickUI, sCandleEx > GetCandleMap( )
        {
            return _candleMap ?? ( _candleMap = new PooledDictionary<CandlestickUI, sCandleEx>() );
        }

        public bool HasCandleData
        {
            get
            {
                return _candleMap != null;
            }
        }

        public PooledDictionary<CandlestickUI, PooledList<sCandleColor>> GetCandleColorMap( )
        {
            return _candleColorMap ?? ( _candleColorMap = new PooledDictionary<CandlestickUI, PooledList<sCandleColor>>( ) );
        }

        private PooledDictionary<IndicatorUI, IndicatorValuesList > _indicatorMap;

        private PooledDictionary<OrdersUI, PooledList<sTrade>> _orderMap;

        private PooledDictionary<TradesUI, PooledList<sTrade>> _tradeMap;

        private PooledDictionary<ChartActiveOrdersElement, PooledList<sActiveOrder>> _activeOrdersMap;

        private PooledDictionary<LineUI, PooledList<sxTuple<DateTime>>>_lineTimeMap;

        private PooledDictionary<LineUI, PooledList<sxTuple<double>>> _lineValueMap;

        private PooledDictionary<BandsUI, PooledList<sxTuple<DateTime>>> _bandTimeMap;

        private PooledDictionary<BandsUI, PooledList<sxTuple<double>>> _bandValueMap;

        private PooledDictionary<AnnotationUI, sAnnotation> _annotationMap;

        public PooledDictionary<IndicatorUI, IndicatorValuesList> GetIndicatorMap( )
        {
            return _indicatorMap ?? ( _indicatorMap = new PooledDictionary<IndicatorUI, IndicatorValuesList>( ) );
        }

        public PooledDictionary<OrdersUI, PooledList<sTrade>> GetOrderMap( )
        {
            return _orderMap ?? ( _orderMap = new PooledDictionary<OrdersUI, PooledList<sTrade>>( ) );
        }

        public PooledDictionary<TradesUI, PooledList<sTrade>> GetTradeMap( )
        {
            return _tradeMap ?? ( _tradeMap = new PooledDictionary<TradesUI, PooledList<sTrade>>( ) );
        }

        public PooledDictionary<ChartActiveOrdersElement, PooledList<sActiveOrder>> GetActiveOrderMa( )
        {
            return _activeOrdersMap ?? ( _activeOrdersMap = new PooledDictionary<ChartActiveOrdersElement, PooledList<sActiveOrder>>( ) );
        }

        public PooledDictionary<LineUI, PooledList<sxTuple<DateTime>>> GetLineTimeMap( )
        {
            return _lineTimeMap ?? ( _lineTimeMap = new PooledDictionary<LineUI, PooledList<sxTuple<DateTime>>>( ) );
        }

        public PooledDictionary<LineUI, PooledList<sxTuple<double>>> GetLineValueMap( )
        {
            return _lineValueMap ?? ( _lineValueMap = new PooledDictionary<LineUI, PooledList<sxTuple<double>>>( ) );
        }

        public PooledDictionary<BandsUI, PooledList<sxTuple<DateTime>>> GetBandTimeMap( )
        {
            return _bandTimeMap ?? ( _bandTimeMap = new PooledDictionary<BandsUI, PooledList<sxTuple<DateTime>>>( ) );
        }

        public PooledDictionary<BandsUI, PooledList<sxTuple<double>>> GetBandValueMap( )
        {
            return _bandValueMap ?? ( _bandValueMap = new PooledDictionary<BandsUI, PooledList<sxTuple<double>>>( ) );
        }

        public PooledDictionary<AnnotationUI, sAnnotation> GetAnnotationMap( )
        {
            return _annotationMap ?? ( _annotationMap = new PooledDictionary<AnnotationUI, sAnnotation>( ) );
        }

        //public ChartDrawData( IEnumerable<RefPair<DateTimeOffset, IDictionary<IChartElement, object>>> values )
        //{
        //    if ( values == null )
        //    {
        //        throw new ArgumentNullException( "values" );
        //    }

        //    foreach ( RefPair<DateTimeOffset, IDictionary<IChartElement, object>> refPair in values )
        //    {
        //        DateTimeOffset first = refPair.First;
        //        foreach ( KeyValuePair<IChartElement, object> keyValuePair in refPair.Second )
        //        {
        //            IChartElement uiElement = keyValuePair.Key;
        //            object obj = keyValuePair.Value;
        //            CandlestickUI candleUI = uiElement as CandlestickUI;
        //            if ( candleUI != null )
        //            {
        //                Candle candle = obj as Candle;

        //                if ( candle != null )
        //                {
        //                    var candleMap = GetCandleMap( );


        //                    ( new sCandle( first, candle.Arg, candle.OpenPrice, candle.HighPrice, candle.LowPrice, candle.ClosePrice, candle.PriceLevels, candle.Security.PriceStep ) );
        //                }
        //                else if ( obj is Color )
        //                {
        //                    Color color = (Color) obj;
        //                    GetCandleColorMap( ).SafeAdd( candleUI ).Add( new sCandleColor( first, new Color?( color ) ) );
        //                }
        //            }
        //            else
        //            {
        //                IndicatorUI key3 = uiElement as IndicatorUI;
        //                if ( key3 != null )
        //                {
        //                    IIndicatorValue val = (IIndicatorValue) obj;
        //                    GetIndicatorMap( ).SafeAdd( key3 ).Add( new sIndicator( first, val ) );
        //                }
        //                else
        //                {
        //                    OrdersUI key4 = uiElement as OrdersUI;
        //                    if ( key4 != null )
        //                    {
        //                        Order order = (Order) obj;
        //                        GetOrderMap( ).SafeAdd( key4 ).Add( new sTrade( first, order.TransactionId, null, order.Direction, order.Price, order.Volume, order.State != OrderStates.Failed ? null : LocalizedStrings.Failed ) );
        //                    }
        //                    else
        //                    {
        //                        TradesUI key5 = uiElement as TradesUI;
        //                        if ( key5 != null )
        //                        {
        //                            MyTrade myTrade = (MyTrade) obj;
        //                            Trade trade = myTrade.Trade;
        //                            GetTradeMap( ).SafeAdd( key5 ).Add( new sTrade( first, trade.Id, trade.StringId, myTrade.Order.Direction, trade.Price, trade.Volume, null ) );
        //                        }
        //                        else
        //                        {
        //                            ChartActiveOrdersElement key6 = uiElement as ChartActiveOrdersElement;
        //                            if ( key6 != null )
        //                                GetActiveOrderMa( ).SafeAdd( key6 ).Add( ( sActiveOrder ) obj );
        //                            else
        //                                throw new ArgumentException( LocalizedStrings.Str2062Params.Put( ( object ) uiElement ) );
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}



        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" />.
        /// </summary>
        /// <param name="timeStamp">The time stamp of the new data generation.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
        public ChartDrawDataItem Group( DateTimeOffset timeStamp )
        {
            return new ChartDrawDataItem( this, timeStamp );
        }

        /// <summary>Put the annotation data.</summary>
        /// <param name="element">The chart element representing an annotation.</param>
        /// <param name="data">Annotation draw data.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
        public void Add( AnnotationUI element, sAnnotation data )
        {
            PooledDictionary<AnnotationUI, sAnnotation> dictionary = GetAnnotationMap( );
            AnnotationUI index = element;
            sAnnotation annotationData = data;
            if ( annotationData == null )
                throw new ArgumentNullException( "annotationData is null" );
            dictionary[ index ] = annotationData;
        }

        /// <summary>Put the active order data.</summary>
        /// <param name="element">The chart element representing active orders.</param>
        /// <param name="order">The order. Can be null to draw just error animation without data.</param>
        /// <param name="isFrozen">Do not allow user to edit the order from chart.</param>
        /// <param name="autoRemoveFromChart">Auto remove this order from chart when its state is final (<see cref="F:StockSharp.Messages.OrderStates.Done" />, <see cref="F:StockSharp.Messages.OrderStates.Failed" />).</param>
        /// <param name="isHidden">Whether an order operation has failed.</param>
        /// <param name="isError">Whether an order operation has failed.</param>
        /// <param name="price">Order price.</param>
        /// <param name="balance">Balance.</param>
        /// <param name="state">Use this state to draw the order.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData" /> instance.</returns>
        public ChartDrawData Add( ChartActiveOrdersElement element,
                                    Order order,
                                    bool? isFrozen = null,
                                    bool autoRemoveFromChart = true,
                                    bool isHidden = false,
                                    bool? isError = null,
                                    Decimal? price = null,
                                    Decimal? balance = null,
                                    OrderStates? state = null )
        {
            state.GetValueOrDefault( );
            if ( !state.HasValue )
                state = new OrderStates?( order != null ? order.State : OrderStates.None );
            Decimal valueOrDefault = price.GetValueOrDefault();
            if ( !price.HasValue )
            {
                if ( order == null )
                    throw new ArgumentException( "Order is null" );
                price = new Decimal?( order.Price );
            }
            valueOrDefault = balance.GetValueOrDefault( );
            if ( !balance.HasValue )
                balance = new Decimal?( order != null ? order.Balance : Decimal.Zero );
            isFrozen.GetValueOrDefault( );
            OrderStates? nullable1;
            if ( !isFrozen.HasValue )
            {
                nullable1 = state;
                OrderStates orderStates1 = OrderStates.None;
                int num;
                if ( !( nullable1.GetValueOrDefault( ) == orderStates1 & nullable1.HasValue ) )
                {
                    nullable1 = state;
                    OrderStates orderStates2 = OrderStates.Pending;
                    num = nullable1.GetValueOrDefault( ) == orderStates2 & nullable1.HasValue ? 1 : 0;
                }
                else
                    num = 1;
                isFrozen = new bool?( num != 0 );
            }
            PooledList<sActiveOrder> activeOrdersList = GetActiveOrderMa().SafeAdd( element );
            Order order1 = order;

            int orderState = (int) state.Value;
            Decimal priceStep =  ( order?.Security?.PriceStep ) ?? new Decimal(1, 0, 0, false,  2);
            int autoRemove = autoRemoveFromChart ? 1 : 0;

            bool weHaveError;
            if ( !isError.HasValue )
            {
                weHaveError = state.GetValueOrDefault( ) == OrderStates.Failed & state.HasValue;
            }
            else
            {
                weHaveError = isError.GetValueOrDefault( );
            }

            sActiveOrder activeOrder = new sActiveOrder(order1, balance.Value, (OrderStates) orderState, priceStep, autoRemove != 0, isFrozen.Value, isHidden, weHaveError, price.Value );
            activeOrdersList.Add( activeOrder );
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" />.
        /// </summary>
        /// <param name="xValue">Value of X coordinate for the data.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
        public ChartDrawDataItem Group( double xValue )
        {
            return new ChartDrawDataItem( this, xValue );
        }

        public sCandleEx GetCandle( CandlestickUI ui )
        {
            return _candleMap.TryGetValue( ui );
        }

        public PooledList<sCandleColor> GetCandleColor( CandlestickUI _param1 )
        {
            PooledDictionary<CandlestickUI, PooledList<sCandleColor>> candleColor = _candleColorMap;
            if ( candleColor == null )
                return null;

            return candleColor.TryGetValue( _param1 );
        }

        public IndicatorValuesList GetIndicator( IndicatorUI _param1 )
        {
            PooledDictionary<IndicatorUI, IndicatorValuesList > indicator = _indicatorMap;
            if ( indicator == null )
                return null;
            return indicator.TryGetValue( _param1 );
        }

        public PooledList<sTrade> GetOrder( OrdersUI _param1 )
        {
            PooledDictionary<OrdersUI, PooledList<sTrade>> order = _orderMap;
            if ( order == null )
                return null;
            return order.TryGetValue( _param1 );
        }

        public PooledList<sTrade> GetTrade( TradesUI _param1 )
        {
            PooledDictionary<TradesUI, PooledList<sTrade>> tradeMap = _tradeMap;
            if ( tradeMap == null )
                return null;
            return tradeMap.TryGetValue( _param1 );
        }

        public PooledList<sActiveOrder> GetActiveOrders( ChartActiveOrdersElement _param1 )
        {
            PooledDictionary<ChartActiveOrdersElement, PooledList<sActiveOrder>> activeOrders = _activeOrdersMap;
            if ( activeOrders == null )
                return null;
            return activeOrders.TryGetValue( _param1 );
        }

        public PooledList<sxTuple<DateTime>> GetLineTime( LineUI _param1 )
        {
            PooledDictionary<LineUI, PooledList<sxTuple<DateTime>>> lineTime = _lineTimeMap;
            if ( lineTime == null )
                return null;
            return lineTime.TryGetValue( _param1 );
        }

        public PooledList<sxTuple<double>> GetLineValue( LineUI _param1 )
        {
            PooledDictionary<LineUI, PooledList<sxTuple<double>>> lineValue = _lineValueMap;

            if ( lineValue == null )
                return null;

            return lineValue.TryGetValue( _param1 );
        }

        public PooledList<sxTuple<DateTime>> GetBandTime( BandsUI _param1 )
        {
            PooledDictionary<BandsUI, PooledList<sxTuple<DateTime>>> bandTime = _bandTimeMap;
            if ( bandTime == null )
                return null;
            return bandTime.TryGetValue( _param1 );
        }

        public PooledList<sxTuple<double>> GetBandValue( BandsUI _param1 )
        {
            PooledDictionary<BandsUI, PooledList<sxTuple<double>>> bandValue = _bandValueMap;

            if ( bandValue == null )
                return null;

            return bandValue.TryGetValue( _param1 );
        }
        public IEnumerableEx<IDrawValue> GetLineDrawValues( LineUI line )
        {
            var lineTime = GetLineTime( line );

            if ( lineTime != null && lineTime.Count > 0 )
            {
                return lineTime.Cast<IDrawValue>( ).ToEx( lineTime.Count );
            }

            var lineValue = GetLineValue( line );

            if ( lineValue == null )
            {
                return null;
            }

            return lineValue.Cast<IDrawValue>( ).ToEx( lineValue.Count );
        }

        public IEnumerableEx<IDrawValue> GetBandDrawValues( BandsUI band )
        {
            PooledList<sxTuple<DateTime>> bandTime = GetBandTime( band );

            if ( bandTime != null && bandTime.Count > 0 )
            {
                return bandTime.Cast<IDrawValue>( ).ToEx( bandTime.Count );
            }

            PooledList<sxTuple<double>> bandValue = GetBandValue( band );
            if ( bandValue == null )
            {
                return null;
            }

            return bandValue.Cast<IDrawValue>( ).ToEx( bandValue.Count );
        }


        public sAnnotation GetAnnotation( AnnotationUI annotation )
        {
            if ( _annotationMap == null )
            {
                return null;
            }

            return _annotationMap.TryGetValue( annotation );
        }
    }
}
