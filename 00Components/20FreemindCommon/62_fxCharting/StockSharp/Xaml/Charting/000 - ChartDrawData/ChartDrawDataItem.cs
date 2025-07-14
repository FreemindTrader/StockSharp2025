using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
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
using System.Collections.Generic;
using fx.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using fx.Bars;

namespace StockSharp.Xaml.Charting
{
    public partial class ChartDrawDataEx
    {
        public sealed class ChartDrawDataItem
        {
            public ChartDrawDataItem( ChartDrawDataEx data, DateTimeOffset timestamp )
            {
                if ( data == null )
                    throw new ArgumentNullException( "ChartDrawData is null" );
                
                _drawData  = data;
                _timeStamp = timestamp;
                _xValue    = double.NaN;
            }

            public ChartDrawDataItem( ChartDrawDataEx data, double xValue )
            {
                if ( data == null )
                    throw new ArgumentNullException( "ChartDrawData is null" );

                _drawData = data;
                _xValue   = xValue;
            }

            /// <summary>The time stamp of the new data generation.</summary>
            private readonly DateTimeOffset _timeStamp;

            public DateTimeOffset TimeStamp
            {
                get
                {
                    return _timeStamp;
                }
            }

            /// <summary>
            /// Value of X coordinate for <see cref="T:StockSharp.Xaml.Charting.ChartLineElement" />.
            /// </summary>
            private readonly double _xValue;

            public double XValue
            {
                get
                {
                    return _xValue;
                }
            }

            private readonly ChartDrawDataEx _drawData;

            private ChartDrawDataItem Add<TElement, TValue>( PooledDictionary<TElement, PooledList<TValue>> _param1, TElement _param2, TValue _param3 )
            {
                _param1.SafeAdd( _param2 ).Add( _param3 );
                return this;
            }

            
            /// <summary>Put candle color data.</summary>
            /// <param name="element">The chart element representing a candle.</param>
            /// <param name="color">Candle draw color.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( ChartCandleElement candleUI, Color? color )
            {
                _drawData.GetCandleColorMap().SafeAdd( candleUI ).Add( new sCandleColor( TimeStamp, color ) );

                return this;
            }

            /// <summary>Put candle color data.</summary>
            /// <param name="element">The chart element representing a candle.</param>
            /// <param name="color">Candle draw color.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( ChartCandleElement candleUI, fxHistoricBarsRepo barList, uint barIndex )
            {
                _drawData.GetCandleMap().SafeAdd( candleUI ).Add( barList, barIndex );

                return this;
            }

            
            /// <summary>Put the indicator data.</summary>
            /// <param name="element">The chart element representing the indicator.</param>
            /// <param name="value">The indicator value.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( ChartIndicatorElement indicatorUI, IIndicator indicator, IIndicatorValue value )
            {
                if ( value == null )
                    return this;

                var indicatorMap = _drawData.GetIndicatorMap();

                if ( indicatorMap.ContainsKey( indicatorUI ) )
                {
                    var indicatorList = indicatorMap[ indicatorUI ];
                    indicatorList.SetIndicatorValue( TimeStamp, value );
                }
                else
                {
                    throw new InvalidCastException();
                }

                //return Add<ChartIndicatorElement, sIndicator>( , element, new sIndicator( TimeStamp, value ) );

                return this;
            }

            /// <summary>Put the order data.</summary>
            /// <param name="element">The chart element representing orders.</param>
            /// <param name="order">The order value.</param>
            /// <param name="errorMessage">Error registering/cancelling order.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( OrdersUI element, Order order, string errorMessage = null )
            {
                if ( order == null )
                    return this;
                if ( errorMessage.IsEmpty() && order.State == OrderStates.Failed )
                    errorMessage = LocalizedStrings.Failed;
                return Add( element, order.TransactionId, null, order.Direction, order.Price, order.Volume, errorMessage );
            }

            /// <summary>Put the trade data.</summary>
            /// <param name="element">The chart element representing orders.</param>
            /// <param name="orderId">Order ID.</param>
            /// <param name="orderStringId">Order ID (as string, if electronic board does not use numeric order ID representation).</param>
            /// <param name="side">Order side (buy or sell).</param>
            /// <param name="price">Order price.</param>
            /// <param name="volume">Number of contracts in the order.</param>
            /// <param name="errorMessage">Error registering/cancelling order.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( OrdersUI element, long orderId, string orderStringId, Sides side, Decimal price, Decimal volume, string errorMessage = null )
            {
                return Add( _drawData.GetOrderMap(), element, new sTrade( TimeStamp, orderId, orderStringId, side, price, volume, errorMessage ) );
            }

            /// <summary>Put the order data.</summary>
            /// <param name="element">The chart element representing trades.</param>
            /// <param name="trade">The trade value.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( TradesUI element, MyTrade trade )
            {
                if ( trade == null )
                    return this;
                Trade trade1 = trade.Trade;
                return Add( element, trade1.Id.Value, trade1.StringId, trade.Order.Direction, trade1.Price, trade1.Volume );
            }

            /// <summary>Put the trade data.</summary>
            /// <param name="element">The chart element representing trades.</param>
            /// <param name="tradeId">Trade ID.</param>
            /// <param name="tradeStringId">Trade ID (as string, if electronic board does not use numeric order ID representation).</param>
            /// <param name="side">Order side (buy or sell).</param>
            /// <param name="price">Trade price.</param>
            /// <param name="volume">Number of contracts in the trade.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( TradesUI element, long tradeId, string tradeStringId, Sides side, Decimal price, Decimal volume )
            {
                return Add( _drawData.GetTradeMap(), element, new sTrade( TimeStamp, tradeId, tradeStringId, side, price, volume, null ) );
            }

            /// <summary>Put the line data.</summary>
            /// <param name="element">The chart element representing a line.</param>
            /// <param name="value1">The value1.</param>
            /// <param name="value2">The value2.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( ChartLineElement element, double value1, double value2 = double.NaN )
            {
                if ( !XValue.IsNaN() )
                    return Add( _drawData.GetLineValueMap(), element, sxTuple<double>.CreateSxTuple( XValue, value1, value2 ) );
                return Add( _drawData.GetLineTimeMap(), element, sxTuple<DateTime>.CreateSxTuple( TimeStamp, value1, value2 ) );
            }

            /// <summary>Put the line data.</summary>
            /// <param name="element">The chart element representing a band.</param>
            /// <param name="value">The value.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( ChartBandElement element, Decimal value )
            {
                return Add( element, ( double ) value, 0.0 );
            }

            /// <summary>Put the line data.</summary>
            /// <param name="element">The chart element representing a band.</param>
            /// <param name="value1">The value1.</param>
            /// <param name="value2">The value2.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( ChartBandElement element, double value1, double value2 )
            {
                if ( !XValue.IsNaN() )
                    return Add( _drawData.GetBandValueMap(), element, sxTuple<double>.CreateSxTuple( XValue, value1, value2 ) );
                return Add( _drawData.GetBandTimeMap(), element, sxTuple<DateTime>.CreateSxTuple( TimeStamp, value1, value2 ) );
            }

            /// <summary>Put the chart data.</summary>
            /// <param name="element">The chart element.</param>
            /// <param name="value">The chart value.</param>
            /// <returns>
            /// <see cref="T:StockSharp.Xaml.Charting.ChartDrawData.ChartDrawDataItem" /> instance.</returns>
            public ChartDrawDataItem Add( IChartElement element, object value )
            {
                if ( element == null )
                {
                    throw new ArgumentNullException( nameof( element ) );
                }

                ChartCandleElement candleUi = element as ChartCandleElement;

                if ( candleUi != null )
                {
                    var candleObj = value as Candle;

                    if ( candleObj != null )
                    {
                        return Add( candleUi, ( Candle ) value );
                    }
                    else
                    {
                        return Add( candleUi, ( sCandle ) value );
                    }
                }

                ChartIndicatorElement element2 = element as ChartIndicatorElement;
                if ( element2 != null )
                    return Add( element2, ( IIndicatorValue ) value );
                OrdersUI element3 = element as OrdersUI;
                if ( element3 == null )
                {
                    TradesUI element4 = element as TradesUI;
                    if ( element4 != null )
                        return Add( element4, ( MyTrade ) value );
                    ChartLineElement element5 = element as ChartLineElement;
                    if ( element5 == null )
                    {
                        ChartBandElement element6 = element as ChartBandElement;
                        if ( element6 != null )
                        {
                            if ( value == null )
                                throw new ArgumentNullException( nameof( value ) );
                            if ( value is double )
                            {
                                double num = (double) value;
                                return Add( element6, num, 0.0 );
                            }
                            if ( value is Decimal )
                            {
                                Decimal num = (Decimal) value;
                                return Add( element6, num );
                            }
                            Tuple<double, double> tuple = value as Tuple<double, double>;
                            if ( tuple != null )
                                return Add( element6, tuple.Item1, tuple.Item2 );
                            throw new ArgumentException( "LocalizedStrings.Str888Params.Put( value.GetType().Name )" );
                        }
                        throw new ArgumentException( "LocalizedStrings.Str2062Params.Put( element )" );
                    }
                    if ( value == null )
                        throw new ArgumentNullException( nameof( value ) );
                    if ( value is double )
                    {
                        double num = (double) value;
                        return Add( element5, num, double.NaN );
                    }
                    if ( value is Decimal )
                    {
                        Decimal num = (Decimal) value;
                        return Add( element5, ( double ) num, double.NaN );
                    }
                    Tuple<double, double> tuple1 = value as Tuple<double, double>;
                    if ( tuple1 != null )
                        return Add( element5, tuple1.Item1, tuple1.Item2 );
                    throw new ArgumentException( "LocalizedStrings.Str888Params.Put( value.GetType().Name )" );
                }
                Order order = (Order) value;
                return Add( element3, order, order.State != OrderStates.Failed ? null : LocalizedStrings.Failed );
            }
        }
    }
}
