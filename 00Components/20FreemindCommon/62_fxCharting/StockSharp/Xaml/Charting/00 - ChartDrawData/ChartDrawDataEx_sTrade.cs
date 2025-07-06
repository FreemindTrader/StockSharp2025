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
    public partial class ChartDrawData
    {
        public struct sActiveOrder : IDrawValue
        {
            private readonly Order _order;
            private readonly Decimal _balance;
            private readonly OrderStates _orderStates;
            private readonly Decimal _priceStep;
            private readonly bool _autoRemove;
            private readonly bool _isFrozen;
            private readonly bool _isHidden;
            private readonly bool _hasError;
            private readonly Decimal _price;

            public sActiveOrder(
              Order myOrder,
              Decimal balance,
              OrderStates _param3,
              Decimal priceStep,
              bool autoRemove,
              bool isFrozen,
              bool isHidden,
              bool hasError,
              Decimal price )
            {
                if ( myOrder == null && !hasError )
                {
                    throw new ArgumentException( "Order is Null" );
                }


                if ( myOrder == null )
                {
                    myOrder = new Order()
                    {
                        State = StockSharp.Messages.OrderStates.Failed,
                        Volume = balance,
                        Balance = balance
                    };
                }

                _order = myOrder;
                _balance = balance;
                _orderStates = myOrder == null ? StockSharp.Messages.OrderStates.Failed : _param3;
                _priceStep = priceStep;
                _autoRemove = autoRemove || myOrder == null;
                _isFrozen = isFrozen || myOrder == null;
                _isHidden = isHidden;
                _hasError = hasError;
                _price = price;
            }


            public Order Order()
            {
                return _order;
            }


            public Decimal Balance()
            {
                return _balance;
            }


            public OrderStates OrderStates()
            {
                return _orderStates;
            }


            public Decimal PriceStep()
            {
                return _priceStep;
            }


            public bool AutoRemoveFromChart()
            {
                return _autoRemove;
            }


            public bool IsFrozen()
            {
                return _isFrozen;
            }


            public bool IsHidden()
            {
                return _isHidden;
            }

            public bool IsError
            {
                get
                {
                    return _hasError;
                }
            }


            public Decimal Price()
            {
                return _price;
            }
        }

        public struct sTrade : IDrawValue
        {
            private readonly DateTime _utcTime;
            private readonly long _tradeId;
            private readonly string _stringId;
            private readonly Sides _orderSide;
            private readonly double _price;
            private readonly long _volume;
            private readonly string _someString;

            public sTrade( DateTimeOffset barTime, long tradeId, string tradeStringId, Sides orderSide, Decimal price, Decimal volume, string _param7 )
            {
                _utcTime = barTime.UtcDateTime;
                _tradeId = tradeId;
                _stringId = tradeStringId;
                _orderSide = orderSide;
                _price = ( double ) price;
                _volume = ( long ) volume;
                _someString = _param7;
            }


            public DateTime UtcTime
            {
                get
                {
                    return _utcTime;
                }
            }


            public long TradeId
            {
                get
                {
                    return _tradeId;
                }
            }


            public string TradeStringId
            {
                get
                {
                    return _stringId;
                }
            }


            public Sides OrderSide
            {
                get
                {
                    return _orderSide;
                }
            }


            public double Price
            {
                get
                {
                    return _price;
                }
            }

            public long Volume
            {
                get
                {
                    return _volume;
                }
            }


            public string SomeString()
            {
                return _someString;
            }
        }
    }
}
