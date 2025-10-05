
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
using DevExpress.Xpf.Core;


#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting;

/// <summary>
/// The UI business logic to draw the active orders on the chart
/// </summary>
internal sealed class ChartActiveOrdersElementUiDomain : ChartCompentWpfUiDomain<ChartActiveOrdersElement>
{
    internal static Binding CreateBinding( object obj, string propertyName )
    {
        return new Binding( propertyName ) { Source = obj, Mode = BindingMode.OneWay };
    }



    private readonly PairSet<Order, ObservableActiveOrdersInfo> _orderInfoAnnotation = new PairSet<Order, ObservableActiveOrdersInfo>();

    public ChartActiveOrdersElementUiDomain( ChartActiveOrdersElement ao ) : base( ao )
    {
    }

    protected override void UpdateUi()
    {
        PerformUiActionSync( new Action( RemoveActiveOrdersInfo ), true );
    }


    private void RemoveActiveOrdersInfo()
    {
        Ecng.Collections.CollectionHelper.ForEach( _orderInfoAnnotation.Keys.ToArray(), new Action<Order>( RemoveActiveOrderInfo ) );
    }

    protected override void Clear()
    {
    }

    /// <summary>
    /// This function check if the order has been filled. Or if the order has either failed or been cancelled, or it is done.
    /// 
    /// Only when it is in a state of Done or either failed, then we will want to show the active order on the chart.
    /// </summary>
    /// <param name="activeOrder"></param>
    /// <param name="autoRemove"></param>
    /// <param name="states"></param>
    /// <param name="isError"></param>
    /// <returns></returns>
    private static bool IsOrderFinal( Order activeOrder, bool autoRemove, OrderStates states, bool isError )
    {
        if ( isError )
            return true;

        if ( activeOrder.Price <= Decimal.Zero )
            return false;

        if ( !autoRemove )
            return true;

        return !states.IsFinal();
    }


    /// <summary>
    /// Get Active Orders that fulfill specific condition.
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public IEnumerable<Order> GetActiveOrders( Func<Order, bool> condition )
    {
        if ( !IsUiThread() )
            throw new InvalidOperationException( "UI Thread" );

        if ( condition == null )
            condition = new Func<Order, bool>( p => true );

        return _orderInfoAnnotation.Keys.Where( condition );
    }

    /// <summary>
    /// Draw the active order on the chart
    /// </summary>
    /// <param name="order"></param>
    private void DrawActiveOrder( ChartDrawData.sActiveOrder order )
    {
        if ( !IsUiThread() )
        {
            PerformUiActionSync( new Action( () => DrawActiveOrder( order ) ), true );
        }
        else
        {
            var myOrder = order.Order;

            if ( !_orderInfoAnnotation.TryGetValue( myOrder, out var aoInfo ) )
            {
                if ( !IsOrderFinal( myOrder, order.AutoRemove, order.OrderStates, order.IsError ) )
                    return;

                aoInfo = SetupAnnotationBinding( order );
                _orderInfoAnnotation.Add( myOrder, aoInfo );
                DrawingSurface.AddAxisMakerAnnotation( RootElem, aoInfo.Annotation, myOrder );
                OrderAnimation( aoInfo, order, true );
            }
            else if ( IsOrderFinal( myOrder, aoInfo.AutoRemoveFromChart, order.OrderStates, order.IsError ) )
            {
                OrderAnimation( aoInfo, order, true );
            }
            else
            {
                OrderAnimation( aoInfo, order, false );
            }
        }
    }

    /// <summary>
    /// Animate the order if it is in a state of Done, Failed or Error
    /// </summary>
    /// <param name="orderInfo"></param>
    /// <param name="order"></param>
    /// <param name="enableAnimation"></param>
    private void OrderAnimation( ObservableActiveOrdersInfo orderInfo, ChartDrawData.sActiveOrder order, bool enableAnimation )
    {
        var ao        = orderInfo.Annotation;
        var notDone   = true;
        var notFilled = orderInfo.Balance != order.Balance;

        PopulateAnnotationWithOrderInfo( orderInfo, order );

        if ( ao.IsAnimationEnabled )
        {
            if ( order.IsError )
            {
                ao.AnimateError();
                notDone = false;

                if ( !enableAnimation )
                {
                    ao.IsAnimationEnabled = false;
                }
            }
            else if ( ( ( order.OrderStates != OrderStates.Done ? 0 : ( order.Balance == Decimal.Zero ? 1 : 0 ) ) | ( notFilled ? 1 : 0 ) ) != 0 )
            {
                ao.AnimateOrderFill();
                notDone = false;

                if ( !enableAnimation )
                {
                    ao.IsAnimationEnabled = false;
                }
            }
        }
        if ( !( !enableAnimation & notDone ) )
            return;

        RemoveActiveOrderInfo( order.Order );
    }

    /// <summary>
    /// Override the virtual draw function to draw the active orders
    /// </summary>
    /// <param name="drawData"></param>
    /// <returns></returns>
    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> drawData )
    {
        if ( drawData == null || drawData.Count == 0 )
            return false;

        foreach ( var order in drawData.Cast<ChartDrawData.sActiveOrder>() )
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

    /// <summary>
    /// Setup the ActiveOrderAnnotation binding
    /// </summary>
    /// <param name="sActiveOrder"></param>
    /// <returns></returns>
    private ObservableActiveOrdersInfo SetupAnnotationBinding( ChartDrawData.sActiveOrder sActiveOrder )
    {
        var order            = sActiveOrder.Order;
        var ao               = new ActiveOrderAnnotation();
        ao.CoordinateMode    = SciChart.Charting.Visuals.Annotations.AnnotationCoordinateMode.RelativeX;
        ao.DragDirections    = XyDirection.YDirection;
        ao.ResizeDirections  = XyDirection.XDirection;
        ao.OrderErrorText    = LocalizedStrings.OrderError.ToUpperInvariant();
        ao.OrderText         = order.Side == Sides.Sell ? "Sell" : "Buy";
        ao.X1                = 0.8;
        var orderInfo        = new ObservableActiveOrdersInfo(ao);
        var toBrushConverter = new ColorToBrushConverter();
        var buySellColor     = order.Side == Sides.Sell ? "SellBlinkColor" : "BuyBlinkColor";

        ao.SetBindings( AnnotationBase.XAxisIdProperty, ChartComponentView, "XAxisId", BindingMode.TwoWay, null, null );
        ao.SetBindings( AnnotationBase.YAxisIdProperty, ChartComponentView, "YAxisId", BindingMode.TwoWay, null, null );
        ao.SetBindings( AnnotationBase.IsHiddenProperty, ChartComponentView, "IsVisible", BindingMode.TwoWay, new InverseBooleanConverter(), null );
        ao.SetBindings( AnnotationBase.IsEditableProperty, orderInfo, "IsFrozen", BindingMode.OneWay, new InverseBooleanConverter(), null );
        ao.SetBindings( Control.ForegroundProperty, ChartComponentView, "ForegroundColor", BindingMode.OneWay, toBrushConverter, null );
        ao.SetBindings( ActiveOrderAnnotation.StrokeProperty, ChartComponentView, "ForegroundColor", BindingMode.OneWay, toBrushConverter, null );
        ao.SetBindings( ActiveOrderAnnotation.CancelButtonFillProperty, ChartComponentView, "CancelButtonBackground", BindingMode.OneWay, toBrushConverter, null );
        ao.SetBindings( ActiveOrderAnnotation.CancelButtonColorProperty, ChartComponentView, "CancelButtonColor", BindingMode.OneWay, toBrushConverter, null );
        ao.SetBindings( ActiveOrderAnnotation.IsAnimationEnabledProperty, ChartComponentView, "IsAnimationEnabled", BindingMode.OneWay, null, null );
        ao.SetBindings( ActiveOrderAnnotation.BlinkColorProperty, ChartComponentView, buySellColor, BindingMode.OneWay, null, null );

        MultiBinding mb = new MultiBinding()
        {
            Converter = new OrderStatesToColorConverter(),
            Mode      = BindingMode.OneWay
        };

        mb.Bindings.Add( CreateBinding( orderInfo, "State" ) );
        mb.Bindings.Add( CreateBinding( orderInfo, "Direction" ) );
        mb.Bindings.Add( CreateBinding( ChartComponentView, "BuyPendingColor" ) );
        mb.Bindings.Add( CreateBinding( ChartComponentView, "BuyColor" ) );
        mb.Bindings.Add( CreateBinding( ChartComponentView, "SellPendingColor" ) );
        mb.Bindings.Add( CreateBinding( ChartComponentView, "SellColor" ) );

        ao.SetBinding( Control.BackgroundProperty, mb );
        PopulateAnnotationWithOrderInfo( orderInfo, sActiveOrder );

        ao.CancelClick   += new Action<ActiveOrderAnnotation>( OnCancelClicked );
        ao.DragEnded     += OnDragEnded;
        ao.AnimationDone += OnAnimationDone;

        return orderInfo;
    }

    /// <summary>
    /// Populate the ActiveOrderAnnotation with the OrderInfo. When content in ObservableActiveOrdersInfo aOrderInfo changed, 
    /// the oberservers will get notification of the change and update the UI accordingly.
    /// </summary>
    /// <param name="aOrderInfo"></param>
    /// <param name="aOrder"></param>
    private static void PopulateAnnotationWithOrderInfo( ObservableActiveOrdersInfo aOrderInfo, ChartDrawData.sActiveOrder aOrder )
    {
        var aoAnnot                    = aOrderInfo.Annotation;
        var order                      = aOrder.Order;

        aOrderInfo.Balance             = aOrder.Balance;
        aOrderInfo.State               = aOrder.OrderStates;
        aOrderInfo.PriceStep           = aOrder.PriceStep;
        aOrderInfo.IsFrozen            = aOrder.IsFrozen;
        aOrderInfo.AutoRemoveFromChart = aOrder.AutoRemove;

        aoAnnot.Y1                     = ( double ) aOrder.Price;
        aoAnnot.OrderSizeText          = string.Format( "{0} {1}", order.Volume - aOrder.Balance, order.Volume );
        aoAnnot.YDragStep              = ( double ) aOrder.PriceStep;
    }


    private Order GetActiveOrderInfo( ActiveOrderAnnotation annot, out ObservableActiveOrdersInfo ObsAO )
    {
        var ao1st = _orderInfoAnnotation.FirstOrDefault(p => p.Value.Annotation == annot);
        ObsAO = ao1st.Value;
        return ao1st.Key;
    }

    /// <summary>
    /// It seems like dragging the active Order to a different price will change the active order to another price
    /// </summary>
    /// <param name="ao"></param>
    /// <param name="e"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private void OnDragEnded( object ao, EventArgs e )
    {
        var actOrder = (ActiveOrderAnnotation) ao;        
        var order    = GetActiveOrderInfo( actOrder, out var orderInfo );

        if ( order == null )
            return;

        var price         = order.Price;
        var aoNewY1       = actOrder.Y1 as double?;
        
        double newPrice;

        if ( !aoNewY1.HasValue )
        {
            newPrice      = !( actOrder.Y1 is Decimal ) ? 0.0 : ( double ) ( ( Decimal ) actOrder.Y1 );
        }
        else
        {
            newPrice      = aoNewY1.GetValueOrDefault();
        }

        var validNewPrice = newPrice;
        var ydragStep     = actOrder.YDragStep;

        if ( ydragStep > 0.0 )
        {
            validNewPrice = Math.Round( ( ( double ) validNewPrice ) / ydragStep ) * ydragStep;
        }

        if ( validNewPrice == 0.0 )
            throw new InvalidOperationException( nameof( validNewPrice ) );
        
        if ( price == ( decimal ) validNewPrice )
            return;

        ( ( Chart ) DrawingSurface.Chart ).InvokeMoveOrderEvent( order, ( Decimal ) validNewPrice );
    }

    /// <summary>
    /// When the animation is done, we will check if the order is in a final state of Done, Failed or Error
    /// 
    /// If the active Order is not final ( meaning doesn't have error and not done or failed ) then we will redraw the active order on the chart
    /// </summary>
    /// <param name="ao"></param>
    private void OnAnimationDone( ActiveOrderAnnotation ao )
    {
        var order = GetActiveOrderInfo(ao, out var aoInfo);

        if ( order == null || IsOrderFinal( order, aoInfo.AutoRemoveFromChart, aoInfo.State, false ) )
            return;

        DrawActiveOrder( new ChartDrawData.sActiveOrder( order, aoInfo.Balance, aoInfo.State, aoInfo.PriceStep, aoInfo.AutoRemoveFromChart, false, false, false, order.Price ) );
    }


    /// <summary>
    /// Cancel the active order
    /// </summary>
    /// <param name="ao"></param>
    private void OnCancelClicked( ActiveOrderAnnotation ao )
    {
        var order = GetActiveOrderInfo(ao, out var aoInfo);
        if ( order == null )
            return;


        ( ( Chart ) DrawingSurface.Chart ).InvokeCancelOrderEvent( order );
    }

    /// <summary>
    /// Convert the OrderStates and Sides to a color
    /// </summary>
    private sealed class OrderStatesToColorConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            if ( values.Length != 6 )
            {
                throw new InvalidOperationException( string.Format( "Unexpected argument count: {0}", values.Length ) );
            }

            OrderStates? myOrderStates       = values[0] as OrderStates?;
            Sides? myOrderSides              = values[1] as Sides?;
            Color? nonePendingBuyColor       = values[2] as Color?;
            Color? activeDoneFailedBuyColor  = values[3] as Color?;
            Color? nonePendingSellColor      = values[4] as Color?;
            Color? activeDoneFailedSellColor = values[5] as Color?;

            if ( myOrderStates.HasValue &&
                 myOrderSides.HasValue &&
                 ( nonePendingBuyColor.HasValue && activeDoneFailedBuyColor.HasValue ) &&
                 ( nonePendingSellColor.HasValue && activeDoneFailedSellColor.HasValue ) )
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
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// ObservableActiveOrdersInfo object can be "observed" by other components, such as ChartActiveOrdersElementUiDomain. 
    /// When a property on the object changes, it "notifies" its observers by raising the PropertyChanged event.
    /// 
    /// an object that implements the INotifyPropertyChanged interface is often called an observable object or a bindable object. 
    /// This is because it provides notifications to clients—typically UI data-binding clients—that a property value has changed.
    /// </summary>
    private sealed class ObservableActiveOrdersInfo : ViewModelBase
    {
        private bool _autoRemoveFromChart = true;
        private readonly ActiveOrderAnnotation _activeOrderAnnotation;
        private Decimal _balance;
        private OrderStates _orderStates;
        private Decimal _priceStep;
        private Decimal _price;
        private bool _isFrozen;

        public ObservableActiveOrdersInfo( ActiveOrderAnnotation ao )
        {
            if ( ao == null )
                throw new ArgumentNullException( nameof( ao ) );

            _activeOrderAnnotation = ao;
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

        /// <summary>
        /// 
        /// </summary>
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
}