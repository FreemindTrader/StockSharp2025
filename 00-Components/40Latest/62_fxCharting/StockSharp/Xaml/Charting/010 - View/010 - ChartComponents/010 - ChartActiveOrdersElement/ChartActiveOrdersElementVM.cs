
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.Utility;
using SciChart.Charting.Visuals.Annotations;
using Ecng.Xaml.Converters;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using StockSharp.Xaml.Charting.ATony;
using StockSharp.Algo;
using Ecng.ComponentModel;
using SciChart.Charting;


#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    internal sealed class ChartActiveOrdersElementVM : ChartCompentWpfBaseViewModel<ChartActiveOrdersElement>
    {
        internal static Binding CreateBinding( object obj, string propertyName )
        {
            return new Binding( propertyName ) { Source = obj, Mode = BindingMode.OneWay };
        }

        private sealed class OrderStatesToColorConverter : IMultiValueConverter
        {
            object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
            {
                if ( values.Length != 6 )
                {
                    throw new InvalidOperationException( string.Format( "Unexpected argument count: {0}", values.Length ) );
                }

                OrderStates? myOrderStates       = values[ 0 ] as OrderStates?;
                Sides? myOrderSides              = values[ 1 ] as Sides?;
                Color? nonePendingBuyColor       = values[ 2 ] as Color?;
                Color? activeDoneFailedBuyColor  = values[ 3 ] as Color?;
                Color? nonePendingSellColor      = values[ 4 ] as Color?;
                Color? activeDoneFailedSellColor = values[ 5 ] as Color?;

                if ( myOrderStates.HasValue && myOrderSides.HasValue && ( nonePendingBuyColor.HasValue && activeDoneFailedBuyColor.HasValue ) && ( nonePendingSellColor.HasValue && activeDoneFailedSellColor.HasValue ) )
                {
                    OrderStates orderStates = myOrderStates.Value;
                    if ( orderStates != OrderStates.None && orderStates != OrderStates.Pending )
                    {
                        return new SolidColorBrush( myOrderSides.Value == Sides.Buy ? activeDoneFailedBuyColor.Value : activeDoneFailedSellColor.Value );
                    }

                    return new SolidColorBrush( myOrderSides.Value == Sides.Buy ? nonePendingBuyColor.Value : nonePendingSellColor.Value );
                }
                throw new ArgumentNullException( string.Format( "Incomplete params: ({0},{1},{2},{3},{4},{5})", myOrderStates, myOrderSides, nonePendingBuyColor, activeDoneFailedBuyColor, nonePendingSellColor, activeDoneFailedSellColor ) );
            }

            object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
            {
                throw new NotSupportedException( );
            }
        }

        private sealed class ChartActiveOrderInfo : ViewModelBase
        {
            private bool _autoRemoveFromChart = true;
            private readonly ActiveOrderAnnotation _activeOrderAnnotation;
            private Decimal _balance;
            private OrderStates _orderStates;
            private Decimal _priceStep;
            private Decimal _price;
            private bool _isFrozen;

            public ChartActiveOrderInfo( ActiveOrderAnnotation _param1 )
            {
                ActiveOrderAnnotation activeOrderAnnotation = _param1;
                if ( activeOrderAnnotation == null )
                    throw new ArgumentNullException( nameof( activeOrderAnnotation ) );
                _activeOrderAnnotation = activeOrderAnnotation;
            }

            public ActiveOrderAnnotation Annotation
            {
                get
                {
                    return _activeOrderAnnotation;
                }
            }

            public Decimal Balance
            {
                get
                {
                    return _balance;
                }
                set
                {
                    SetField( ref _balance, value, nameof( Balance ) );
                }
            }

            public OrderStates State
            {
                get
                {
                    return _orderStates;
                }
                set
                {
                    SetField( ref _orderStates, value, nameof( State ) );
                }
            }

            public Decimal PriceStep
            {
                get
                {
                    return _priceStep;
                }
                set
                {
                    SetField( ref _priceStep, value, nameof( PriceStep ) );
                }
            }

            public Decimal Price
            {
                get
                {
                    return _price;
                }
                set
                {
                    SetField( ref _price, value, nameof( Price ) );
                }
            }

            public bool AutoRemoveFromChart
            {
                get
                {
                    return _autoRemoveFromChart;
                }
                set
                {
                    SetField( ref _autoRemoveFromChart, value, nameof( AutoRemoveFromChart ) );
                }
            }

            public bool IsFrozen
            {
                get
                {
                    return _isFrozen;
                }
                set
                {
                    SetField( ref _isFrozen, value, nameof( IsFrozen ) );
                }
            }
        }

        private readonly PairSet<Order, ChartActiveOrderInfo> _orderInfoAnnotation = new PairSet<Order, ChartActiveOrderInfo>();

        public ChartActiveOrdersElementVM( ChartActiveOrdersElement _param1 )
          : base( _param1 )
        {
        }

        protected override void UpdateUi( )
        {
            PerformUiAction( new Action( RemoveActiveOrdersInfo ), true );
        }


        private void RemoveActiveOrdersInfo( )
        {
            Ecng.Collections.CollectionHelper.ForEach( _orderInfoAnnotation.Keys.ToArray(), new Action<Order>( RemoveActiveOrderInfo ) );
            
        }

        protected override void Clear( )
        {
        }

        private static bool IsValidOrder( Order activeOrder, bool _param1, OrderStates states, bool _param3 )
        {
            if ( _param3 )
                return true;
            if ( activeOrder.Price <= Decimal.Zero )
                return false;
            if ( !_param1 )
                return true;
            return !states.IsFinal( );
        }

        public IEnumerable<Order> GetActiveOrders( Func<Order, bool> _param1 )
        {
            if ( !IsUiThread( ) )
                throw new InvalidOperationException( "UI Thread" );
            if ( _param1 == null )
                _param1 = new Func<Order, bool>( p => true );

            return _orderInfoAnnotation.Keys.Where( _param1 );
        }

        private void DrawActiveOrder( ChartDrawData.sActiveOrder order )
        {
            if ( !IsUiThread( ) )
            {
                PerformUiAction( new Action( ( ) => DrawActiveOrder( order ) ), true );
            }
            else
            {
                ChartActiveOrderInfo activeOrderInfo;

                var myOrder = order.Order;

                if ( !_orderInfoAnnotation.TryGetValue( myOrder, out activeOrderInfo ) )
                {
                    if ( !IsValidOrder( myOrder, order.AutoRemove, order.OrderStates, order.IsError ) )
                        return;

                    activeOrderInfo = SetupAnnotationBinding( order );
                    _orderInfoAnnotation.Add( myOrder, activeOrderInfo );
                    DrawingSurface.AddAxisMakerAnnotation( RootElem, activeOrderInfo.Annotation, myOrder );
                    OrderAnimation( activeOrderInfo, order, true );
                }
                else if ( IsValidOrder( myOrder, activeOrderInfo.AutoRemoveFromChart, order.OrderStates, order.IsError ) )
                {
                    OrderAnimation( activeOrderInfo, order, true );
                }
                else
                {
                    OrderAnimation( activeOrderInfo, order, false );
                }

            }
        }

        private void OrderAnimation( ChartActiveOrderInfo orderInfo, ChartDrawData.sActiveOrder order, bool _param2 )
        {
            ActiveOrderAnnotation annotation = orderInfo.Annotation;
            bool flag1 = true;
            bool flag2 = orderInfo.Balance != order.Balance;
            PopulateAnnotationWithOrderInfo( orderInfo, order );

            if ( annotation.IsAnimationEnabled )
            {
                if ( order.IsError )
                {
                    annotation.AnimateError( );
                    flag1 = false;
                    if ( !_param2 )
                        annotation.IsAnimationEnabled = false;
                }
                else if ( ( ( order.OrderStates != OrderStates.Done ? 0 : ( order.Balance == Decimal.Zero ? 1 : 0 ) ) | ( flag2 ? 1 : 0 ) ) != 0 )
                {
                    annotation.AnimateOrderFill( );
                    flag1 = false;
                    if ( !_param2 )
                        annotation.IsAnimationEnabled = false;
                }
            }
            if ( !( !_param2 & flag1 ) )
                return;

            RemoveActiveOrderInfo( order.Order);
        }

        public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
        {
            if ( _param1 == null || _param1.Count == 0 )
                return false;

            //return Draw<T>( _param1.Cast<ChartDrawData.sxTuple<T>>( ).ToEx<ChartDrawData.sxTuple<T>>( _param1.Count ) );

            foreach ( var order in _param1.Cast<ChartDrawData.sActiveOrder>( ) )
            {
                DrawActiveOrder( order );
            }

            return true;
        }



        private void RemoveActiveOrderInfo( Order orderInfo )
        {
            if ( _orderInfoAnnotation.TryGetValue( orderInfo ) == null )
                return;

            _orderInfoAnnotation.Remove( orderInfo );
            DrawingSurface.RemoveAnnotation( RootElem, orderInfo );
        }



        private ChartActiveOrderInfo SetupAnnotationBinding(ChartDrawData.sActiveOrder sActiveOrder)
        {
            Order order         = sActiveOrder.Order;
            var ao              = new ActiveOrderAnnotation();
            ao.CoordinateMode   = SciChart.Charting.Visuals.Annotations.AnnotationCoordinateMode.RelativeX;
            ao.DragDirections   = XyDirection.YDirection;
            ao.ResizeDirections = XyDirection.XDirection;
            ao.OrderErrorText   = LocalizedStrings.OrderError.ToUpperInvariant( );
            ao.OrderText        = order.Side == Sides.Sell ? "Sell" : "Buy";
            ao.X1               = 0.8;
            var orderInfo       = new ChartActiveOrderInfo(ao);

            ao.SetBindings( AnnotationBase.XAxisIdProperty, ChartComponentView, "XAxisId", BindingMode.TwoWay, null, null );
            ao.SetBindings( AnnotationBase.YAxisIdProperty, ChartComponentView, "YAxisId", BindingMode.TwoWay, null, null );
            ao.SetBindings( AnnotationBase.IsHiddenProperty, ChartComponentView, "IsVisible", BindingMode.TwoWay, new InverseBooleanConverter( ), null );
            ao.SetBindings( AnnotationBase.IsEditableProperty, orderInfo, "IsFrozen", BindingMode.OneWay, new InverseBooleanConverter( ), null );

            var toBrushConverter       = new ColorToBrushConverter( );


            ao.SetBindings( Control.ForegroundProperty, ChartComponentView, "ForegroundColor", BindingMode.OneWay, toBrushConverter, null );
            ao.SetBindings( ActiveOrderAnnotation.StrokeProperty, ChartComponentView, "ForegroundColor", BindingMode.OneWay, toBrushConverter, null );
            ao.SetBindings( ActiveOrderAnnotation.CancelButtonFillProperty, ChartComponentView, "CancelButtonBackground", BindingMode.OneWay, toBrushConverter, null );
            ao.SetBindings( ActiveOrderAnnotation.CancelButtonColorProperty, ChartComponentView, "CancelButtonColor", BindingMode.OneWay, toBrushConverter, null );
            ao.SetBindings( ActiveOrderAnnotation.IsAnimationEnabledProperty, ChartComponentView, "IsAnimationEnabled", BindingMode.OneWay, null, null );
            ao.SetBindings( ActiveOrderAnnotation.BlinkColorProperty, ChartComponentView, order.Direction == Sides.Sell ? "SellBlinkColor" : "BuyBlinkColor", BindingMode.OneWay, null, null );

            MultiBinding mb = new MultiBinding( ) { Converter = new OrderStatesToColorConverter( ), Mode = BindingMode.OneWay };
            mb.Bindings.Add( CreateBinding( orderInfo, "State" ) );
            mb.Bindings.Add( CreateBinding( orderInfo, "Direction" ) );
            mb.Bindings.Add( CreateBinding( ChartComponentView, "BuyPendingColor" ) );
            mb.Bindings.Add( CreateBinding( ChartComponentView, "BuyColor" ) );
            mb.Bindings.Add( CreateBinding( ChartComponentView, "SellPendingColor" ) );
            mb.Bindings.Add( CreateBinding( ChartComponentView, "SellColor" ) );

            ao.SetBinding( Control.BackgroundProperty, mb );
            PopulateAnnotationWithOrderInfo( orderInfo, sActiveOrder );
            ao.CancelClick += new Action<ActiveOrderAnnotation>( OnCancelClicked );
            ao.DragEnded += OnDragEnded;
            ao.AnimationDone += OnAnimationDone;
            return orderInfo;
        }

        private static void PopulateAnnotationWithOrderInfo(ChartActiveOrderInfo aOrderInfo, ChartDrawData.sActiveOrder aOrder)
        {
            var annotation                 = aOrderInfo.Annotation;
            var order                      = aOrder.Order;
            aOrderInfo.Balance             = aOrder.Balance;
            aOrderInfo.State               = aOrder.OrderStates;
            aOrderInfo.PriceStep           = aOrder.PriceStep;
            aOrderInfo.IsFrozen            = aOrder.IsFrozen;
            aOrderInfo.AutoRemoveFromChart = aOrder.AutoRemove;
            annotation.Y1                  = ( double ) aOrder.Price;
            annotation.OrderSizeText       = string.Format( "{0} {1}", order.Volume - aOrder.Balance, order.Volume );
            annotation.YDragStep           = ( double ) aOrder.PriceStep;
        }

        private Order GetActiveOrderInfo( ActiveOrderAnnotation _param1, out ChartActiveOrderInfo _param2 )
        {
            var keyValuePair = _orderInfoAnnotation.FirstOrDefault( p => p.Value.Annotation == _param1);
            _param2 = keyValuePair.Value;
            return keyValuePair.Key;
        }

        private void OnDragEnded( object _param1, EventArgs _param2 )
        {
            ActiveOrderAnnotation actOrder = (ActiveOrderAnnotation) _param1;
            ChartActiveOrderInfo orderInfo;
            Order order = GetActiveOrderInfo( actOrder, out orderInfo );
            if ( order == null )
                return;
            Decimal price = order.Price;
            double? y1Value = actOrder.Y1 as double?;
            double y1;

            if ( !y1Value.HasValue )
            {
                y1 = !( actOrder.Y1 is Decimal ) ? 0.0 : ( double ) ( ( Decimal ) actOrder.Y1 );
            }
            else
            {
                y1 = y1Value.GetValueOrDefault( );
            }

            double y1DragStep = y1;
            double ydragStep = actOrder.YDragStep;

            if ( ydragStep > 0.0 )
            {
                y1DragStep = Math.Round( ( ( double ) y1DragStep ) / ydragStep ) * ydragStep;
            }

            if ( y1DragStep == 0.0 )
                throw new InvalidOperationException( nameof( y1DragStep ) );
            Decimal num3 = (Decimal) y1DragStep;
            Decimal num4 = num3;
            if ( price == num4 )
                return;

            throw new NotImplementedException();

            //DrawingSurface.GroupChartEx.InvokeMoveOrderEvent( order, num3 );
        }

        private void OnAnimationDone( ActiveOrderAnnotation _param1 )
        {
            ChartActiveOrderInfo zb8sIius;
            Order order = GetActiveOrderInfo( _param1, out zb8sIius );
            if ( order == null || IsValidOrder( order, zb8sIius.AutoRemoveFromChart, zb8sIius.State, false ) )
                return;
            DrawActiveOrder( new ChartDrawData.sActiveOrder( order, zb8sIius.Balance, zb8sIius.State, zb8sIius.PriceStep, zb8sIius.AutoRemoveFromChart, false, false, false, order.Price ) );
        }

        private void OnCancelClicked( ActiveOrderAnnotation _param1 )
        {
            ChartActiveOrderInfo activeOrderInfo;
            Order order = GetActiveOrderInfo( _param1, out activeOrderInfo );
            if ( order == null )
                return;

            throw new NotImplementedException();
            //DrawingSurface.GroupChartEx.InvokeCancelOrderEvent( order );
        }
    }
}