using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals;
using SciChart.Core.Utility.Mouse;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// To customize the UI of the chart, we use modifier to draw custom order line and adorner ( this is used to display the buying or selling price )
/// </summary>
/// <param name="_param1"></param>
internal sealed class ChartOrderModifier(ChartArea _param1) : ChartModifierBase
{
    private readonly ChartArea _chartArea = _param1 ?? throw new ArgumentNullException("area");

    private ISciChartSurface _drawingSurface;

    private Line _orderline;

    private OrderAdorner _orderAdorner;

    private bool _isMouseOver;

    public static readonly DependencyProperty ShowHorizontalLineProperty = DependencyProperty.Register(nameof(ShowHorizontalLine),typeof(bool),typeof(ChartOrderModifier),new PropertyMetadata(true));

    /// <summary>
    /// This property will control whether we will draw the order line on the chart to display the price of buying or selling
    /// </summary>
    public bool ShowHorizontalLine
    {
        get
        {
            return (bool) GetValue(ShowHorizontalLineProperty);
        }
        set
        {
            SetValue(ShowHorizontalLineProperty,  value);
        }
    }

    public override void OnAttached()
    {
        base.OnAttached();
        _drawingSurface = ParentSurface;
        ((UIElement) _drawingSurface).PreviewMouseDown += new MouseButtonEventHandler(OnPreviewMouseDown);
    }

    public override void OnDetached()
    {
        base.OnDetached();
        if(_drawingSurface != null)
        {
            ((UIElement) _drawingSurface).PreviewMouseDown -= new MouseButtonEventHandler(OnPreviewMouseDown);
            _drawingSurface = (ISciChartSurface) null;
        }
        Clear();
    }

    protected override void OnIsEnabledChanged()
    {
        base.OnIsEnabledChanged();
        Clear();
    }

    public override void OnMasterMouseLeave(ModifierMouseArgs e)
    {
        base.OnMasterMouseLeave(e);
        Clear();
        _isMouseOver = false;
    }

    protected override void OnParentSurfaceMouseEnter()
    {
        _isMouseOver = true;
        base.OnParentSurfaceMouseEnter();
    }

    public override void OnModifierMouseMove(ModifierMouseArgs e)
    {
        base.OnModifierMouseMove(e);
        if(!e.IsMaster)
            return;
        UpdateOrderAdornerLayer(e.MousePoint);
    }

    private void OnPreviewMouseDown(object _param1, MouseButtonEventArgs _param2)
    {
        Clear();
    }

    public override void OnModifierMouseUp(ModifierMouseArgs e)
    {
        base.OnModifierMouseUp(e);
        if ( !( _chartArea.Chart is Chart chart ) || !e.IsMaster ||
            e.Modifier != MouseModifier.Ctrl ||
            e.MouseButtons != MouseButtons.Left &&
            e.MouseButtons != MouseButtons.Right )
        {
            return;
        }
            
        e.Handled = true;
        Sides sides = e.MouseButtons == MouseButtons.Left ? Sides.Buy : Sides.Sell;
        double num = (double) YAxis.GetDataValue(e.MousePoint.Y);
        Order order = new Order()
        {
            Side = sides,
            Price = Converter.To<Decimal>( num),
            Type = OrderTypes.Limit
        };
        
        chart?.InvokeCreateOrderEvent(_chartArea, order);
    }

    private void UpdateOrderAdornerLayer(Point pt)
    {
        if(!_isMouseOver || Mouse.LeftButton != MouseButtonState.Released)
            return;
        if(_orderline == null)
        {
            var modifierSurface = ModifierSurface;
            if(modifierSurface == null)
                return;

            Line line = new Line();
            line.X1 = 0.0;
            line.X2 = modifierSurface.ActualWidth;
            line.Y1 = pt.Y;
            line.Y2 = pt.Y;
            line.StrokeThickness = 1.0;
            Line line2 = line;
            DoubleCollection doubleCollection = new DoubleCollection();
            double[] numArray = new double[2] { 7.0, 4.0 };
            foreach(double num in numArray)
                doubleCollection.Add(num);
            line2.StrokeDashArray = doubleCollection;
            line.Stroke = (Brush) new SolidColorBrush(Colors.OrangeRed);
            line.IsHitTestVisible = false;
            _orderline = line;
            _orderline
                .SetBindings(
                    UIElement.VisibilityProperty,
                     this,
                    "ShowHorizontalLine",
                    BindingMode.OneWay,
                    (IValueConverter) new BoolToVisibilityConverter() { FalseValue = Visibility.Hidden });
            modifierSurface.Children.Add((UIElement) _orderline);
            _orderAdorner = new OrderAdorner(_orderline);
            AdornerLayer.GetAdornerLayer((Visual) _orderline).Add((Adorner) _orderAdorner);
        }
        double dataValue = (double) YAxis.GetDataValue(pt.Y);
        _orderline.Y1 = pt.Y;
        _orderline.Y2 = pt.Y;
        _orderAdorner.SellTextBlock().Text = StringHelper.Put(
            "Sell {0} {1:.00}",
            new object[ 2 ]
            {
                GetOrderTypeFromPrice(Sides.Sell, dataValue) == OrderTypes.Limit
                ?  "Limit"
                :  "Market",
                 dataValue
            });
        _orderAdorner.BuyTextBlock().Text = StringHelper.Put(
            "Buy {0} {1:.00}",
            new object[ 2 ]
            {
                GetOrderTypeFromPrice(Sides.Buy, dataValue) == OrderTypes.Limit
                ?  "Limit"
                :  "Market",
                 dataValue
            });
        _orderAdorner.OpenTooltip(_orderAdorner.IsMouseOver);
    }

    private OrderTypes GetOrderTypeFromPrice(Sides orderSides, double _param2)
    {
        FastCandlestickRenderableSeries renderableSeries = ParentSurface.RenderableSeries
            .OfType<FastCandlestickRenderableSeries>()
            .FirstOrDefault<FastCandlestickRenderableSeries>();

        if(renderableSeries == null)
            return OrderTypes.Limit;

        IList list = ((IOhlcDataSeries) renderableSeries.DataSeries).CloseValues;

        if(list.Count == 0)
            return OrderTypes.Limit;

        double count = (double) list[list.Count - 1];
        return orderSides != Sides.Buy
            ? (_param2 >= count ? OrderTypes.Limit : OrderTypes.Market)
            : (_param2 <= count ? OrderTypes.Limit : OrderTypes.Market);
    }

    private void Clear()
    {
        if(ModifierSurface == null)
            return;
        if(_orderline != null)
            ModifierSurface.Children.Remove((UIElement) _orderline);
        _orderline = (Line) null;
    }

    private sealed class OrderAdorner : Adorner
    {
        private readonly Grid _grid;

        private readonly TextBlock _sellTextBlock;

        private readonly TextBlock _buyTextBlock;

        public OrderAdorner(UIElement _param1) : base(_param1)
        {
            IsHitTestVisible = false;
            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = "Sell";
            textBlock1.Background = (Brush) new SolidColorBrush(Colors.Red);
            textBlock1.Foreground = (Brush) new SolidColorBrush(Colors.White);
            textBlock1.FontSize = 11.0;
            textBlock1.VerticalAlignment = VerticalAlignment.Center;
            _sellTextBlock = textBlock1;
            TextBlock textBlock2 = new TextBlock();
            textBlock2.Text = "Buy";
            textBlock2.Background = (Brush) new SolidColorBrush(Colors.Green);
            textBlock2.Foreground = (Brush) new SolidColorBrush(Colors.White);
            textBlock2.FontSize = 11.0;
            textBlock2.VerticalAlignment = VerticalAlignment.Center;
            _buyTextBlock = textBlock2;
            Grid grid = new Grid();
            System.Windows.Controls.ToolTip toolTip = new System.Windows.Controls.ToolTip();
            toolTip.Content =  (LocalizedStrings.BuyCtrlLeftMouse +
                Environment.NewLine +
                LocalizedStrings.SellCtrlRightMouse);
            grid.ToolTip =  toolTip;
            _grid = grid;
            _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.0, GridUnitType.Auto) });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.0, GridUnitType.Auto) });
            _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.0, GridUnitType.Auto) });
            Grid.SetRow((UIElement) SellTextBlock(), 0);
            Grid.SetRow((UIElement) BuyTextBlock(), 1);
            _grid.Children.Add((UIElement) SellTextBlock());
            _grid.Children.Add((UIElement) BuyTextBlock());
        }

        public TextBlock SellTextBlock()
        {
            return _sellTextBlock;
        }

        public TextBlock BuyTextBlock()
        {
            return _buyTextBlock;
        }

        public void OpenTooltip(bool _param1)
        {
            ((System.Windows.Controls.ToolTip) _grid.ToolTip).IsOpen = _param1;
        }

        protected override Size MeasureOverride(Size _param1)
        {
            _grid.Measure(_param1);
            return _param1;
        }

        protected override Size ArrangeOverride(Size _param1)
        {
            Line adornedElement = (Line) AdornedElement;
            _grid
                .Arrange(
                    new Rect(
                        adornedElement.X1,
                        adornedElement.Y1 - _grid.ActualHeight / 2.0,
                        _grid.ActualWidth,
                        _grid.ActualHeight));
            return _param1;
        }

        protected override Visual GetVisualChild(int _param1)
        {
            return (Visual) _grid;
        }

        protected override int VisualChildrenCount => 1;
    }
}



//using Ecng.Common;
//using Ecng.Xaml;
//using SciChart.Charting.ChartModifiers;
//using SciChart.Charting.Model.DataSeries;
//using SciChart.Charting.Visuals;
//using SciChart.Charting.Visuals.RenderableSeries;
//using Ecng.Xaml.Converters;
//using StockSharp.BusinessEntities;
//using StockSharp.Localization;
//using StockSharp.Messages;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Collections;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using SciChart.Core.Utility.Mouse;
//using SciChart.Core.Framework;

///// <summary>
///// 
///// </summary>
//internal sealed class ChartOrderModifier : ChartModifierBase
//{
//    public static readonly DependencyProperty CanCreateOrdersProperty = DependencyProperty.Register( nameof( CanCreateOrders ), typeof( bool ), typeof( ChartOrderModifier ), new PropertyMetadata( false, new PropertyChangedCallback( CanCreateOrdersPropertyChanged ) ) );
//    public static readonly DependencyProperty ShowHorizontalLineProperty = DependencyProperty.Register( nameof( ShowHorizontalLine ), typeof( bool ), typeof( ChartOrderModifier ), new PropertyMetadata( true ) );
//    private readonly ChartArea _area;

//    private Line _orderLine;
//    private OrderAdorner _orderAdorner;
//    private bool _isMouseOver;

//    public ChartOrderModifier( ChartArea area )
//    {        
//        if( area == null )
//        {
//            throw new ArgumentNullException( "area" );
//        }
//        _area = area;
//    }

//    private static void CanCreateOrdersPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
//    {
//        ( ( ChartOrderModifier )d ).Clear( );
//    }

//    public bool CanCreateOrders
//    {
//        get
//        {
//            return ( bool )GetValue( CanCreateOrdersProperty );
//        }
//        set
//        {
//            SetValue( CanCreateOrdersProperty, value );
//        }
//    }

//    public bool ShowHorizontalLine
//    {
//        get
//        {
//            return ( bool )GetValue( ShowHorizontalLineProperty );
//        }
//        set
//        {
//            SetValue( ShowHorizontalLineProperty, value );
//        }
//    }

//    public override void OnAttached( )
//    {
//        base.OnAttached( );

//        ( ( UIElement ) ParentSurface ).PreviewMouseDown += new MouseButtonEventHandler( OnPreviewMouseDown );
//    }

//    public override void OnDetached( )
//    {
//        base.OnDetached( );

//        var surface = ( UIElement ) ParentSurface;

//        if ( surface != null )
//        {
//            surface.PreviewMouseDown -= new MouseButtonEventHandler( OnPreviewMouseDown );
//        }



//        Clear( );
//    }

//    protected override void OnIsEnabledChanged( )
//    {
//        base.OnIsEnabledChanged( );
//        Clear( );
//    }

//    public override void OnMasterMouseLeave( ModifierMouseArgs e )
//    {
//        base.OnMasterMouseLeave( e );
//        Clear( );
//        _isMouseOver = false;
//    }

//    protected override void OnParentSurfaceMouseEnter( )
//    {
//        _isMouseOver = true;
//        base.OnParentSurfaceMouseEnter( );
//    }

//    public override void OnModifierMouseMove( ModifierMouseArgs e )
//    {
//        base.OnModifierMouseMove( e );

//        if ( !e.IsMaster )
//        {
//            return;
//        }

//        UpdateOrderAdornerLayer( e.MousePoint );
//    }

//    private void OnPreviewMouseDown( object sender, MouseButtonEventArgs e )
//    {
//        Clear( );
//    }

//    public override void OnModifierMouseUp( ModifierMouseArgs e )
//    {
//        base.OnModifierMouseUp( e );
//        Chart chart;

//        if ( ( chart = _area.Chart as Chart ) == null || !e.IsMaster || ( !CanCreateOrders || e.Modifier != MouseModifier.Ctrl ) || e.MouseButtons != MouseButtons.Left && e.MouseButtons != MouseButtons.Right )
//        {
//            return;
//        }

//        e.Handled = true;

//        Sides sides      = e.MouseButtons == MouseButtons.Left ? Sides.Buy : Sides.Sell;
//        double dataValue = ( double )    YAxis.GetDataValue( e.MousePoint.Y );

//        var order        = new Order( );
//        order.Direction  = ( sides );
//        order.Price      = dataValue.To<Decimal>( );
//        order.Type       = OrderTypes.Limit;

//        chart?.InvokeCreateOrderEvent( _area, order );
//    }

//    private void UpdateOrderAdornerLayer( Point pt )
//    {
//        if ( !CanCreateOrders || !_isMouseOver || Mouse.LeftButton != MouseButtonState.Released )
//        {
//            return;
//        }

//        if ( _orderLine == null )
//        {
//            Line line             = new Line( );
//            line.X1               = 0.0;
//            line.X2               = ModifierSurface.ActualWidth;
//            line.Y1               = pt.Y;
//            line.Y2               = pt.Y;
//            line.StrokeThickness  = 1.0;
//            line.StrokeDashArray  = new DoubleCollection( new double[ 2 ] { 7.0, 4.0 } );
//            line.Stroke           = new SolidColorBrush( Colors.OrangeRed );
//            line.IsHitTestVisible = false;

//            _orderLine            = line;

//            DependencyProperty visibilityProperty = VisibilityProperty;
//            string str = "ShowHorizontalLine";

//            var converter = new BoolToVisibilityConverter( );
//            converter.FalseValue = Visibility.Hidden;


//            _orderLine.SetBindings( visibilityProperty, this, str, BindingMode.OneWay, converter, null );
//            ModifierSurface.Children.Add( _orderLine );
//            _orderAdorner = new OrderAdorner( _orderLine );
//            AdornerLayer.GetAdornerLayer( _orderLine ).Add( _orderAdorner );
//        }

//        double dataValue = ( double )    YAxis.GetDataValue( pt.Y );
//        _orderLine.Y1 = pt.Y;
//        _orderLine.Y2 = pt.Y;

//        _orderAdorner.SellTextBlock.Text = StringHelper.Put( "Sell {0} {1:.00}", new object[ ]
//                                                                                    {
//                                                                                        GetOrderTypeFromPrice( Sides.Sell, dataValue) == OrderTypes.Limit ?  "Limit" :  "Market",
//                                                                                        dataValue
//                                                                                    }
//                                                           );

//        _orderAdorner.BuyTextBlock.Text = StringHelper.Put( "Buy {0} {1:.00}", new object[ ]
//                                                                                    {
//                                                                                        GetOrderTypeFromPrice( Sides.Buy, dataValue) == OrderTypes.Limit ?  "Limit" :  "Market",
//                                                                                        dataValue
//                                                                                    }
//                                                          );

//        _orderAdorner.OpenTooltip( _orderAdorner.IsMouseOver );
//    }

//    private OrderTypes GetOrderTypeFromPrice( Sides orderSides, double price )
//    {
//        FastCandlestickRenderableSeries renderableSeries = ParentSurface.RenderableSeries.OfType< FastCandlestickRenderableSeries >( ).FirstOrDefault( );
//        if( renderableSeries == null )
//        {
//            return OrderTypes.Limit;
//        }
//        IList closeValues = ( ( IOhlcDataSeries )renderableSeries.DataSeries ).CloseValues;
//        if( closeValues.Count == 0 )
//        {
//            return OrderTypes.Limit;
//        }
//        double num = ( double )closeValues[ closeValues.Count - 1 ];
//        if( orderSides != Sides.Buy )
//        {
//            if( price >= num )
//            {
//                return OrderTypes.Limit;
//            }
//            return OrderTypes.Market;
//        }
//        if( price <= num )
//        {
//            return OrderTypes.Limit;
//        }
//        return OrderTypes.Market;
//    }

//    private void Clear( )
//    {
//        if( ModifierSurface == null )
//        {
//            return;
//        }
//        if( _orderLine != null )
//        {
//            ModifierSurface.Children.Remove( _orderLine );
//        }
//        _orderLine = null;
//    }

//    /// <summary>
//    /// This class will draw a textbook on the chart to indicate the order type and price when the user hovers over the order line.
//    /// </summary>
//    private sealed class OrderAdorner : Adorner
//    {
//        private readonly Grid _grid;
//        private readonly TextBlock _sellTextBlock;
//        private readonly TextBlock _buyTextBlock;

//        public OrderAdorner( UIElement uielement_0 ) : base( uielement_0 )
//        {
//            IsHitTestVisible = false;

//            TextBlock sell         = new TextBlock( );
//            sell.Text              = "Sell";
//            sell.Background        = new SolidColorBrush( Colors.Red );
//            sell.Foreground        = new SolidColorBrush( Colors.White );
//            sell.FontSize          = 11.0;
//            sell.VerticalAlignment = VerticalAlignment.Center;
//            _sellTextBlock         = sell;

//            TextBlock buy          = new TextBlock( );
//            buy.Text               = "Buy";
//            buy.Background         = new SolidColorBrush( Colors.Green );
//            buy.Foreground         = new SolidColorBrush( Colors.White );
//            buy.FontSize           = 11.0;
//            buy.VerticalAlignment  = VerticalAlignment.Center;
//            _buyTextBlock          = buy;

//            Grid grid = new Grid( );
//            ToolTip toolTip = new ToolTip( );
//            toolTip.Content = ( LocalizedStrings.BuyCtrlLeftMouse + Environment.NewLine + LocalizedStrings.SellCtrlRightMouse );
//            grid.ToolTip = toolTip;
//            _grid = grid;

//            _grid.ColumnDefinitions.Add( new ColumnDefinition( ) { Width = new GridLength( 0.0, GridUnitType.Auto ) } );
//            _grid.RowDefinitions.Add( new RowDefinition( ) { Height = new GridLength( 0.0, GridUnitType.Auto ) } );
//            _grid.RowDefinitions.Add( new RowDefinition( ) { Height = new GridLength( 0.0, GridUnitType.Auto ) } );
//            Grid.SetRow( SellTextBlock, 0 );
//            Grid.SetRow( BuyTextBlock, 1 );
//            _grid.Children.Add( SellTextBlock );
//            _grid.Children.Add( BuyTextBlock );
//        }

//        public TextBlock SellTextBlock
//        {
//            get
//            {
//                return _sellTextBlock;
//            }

//        }

//        public TextBlock BuyTextBlock
//        {
//            get
//            {
//                return _buyTextBlock;
//            }            
//        }

//        public void OpenTooltip( bool moveOver )
//        {
//            ( ( ToolTip )_grid.ToolTip ).IsOpen = moveOver;
//        }

//        protected override Size MeasureOverride( Size availableSize )
//        {
//            _grid.Measure( availableSize );
//            return availableSize;
//        }

//        protected override Size ArrangeOverride( Size arrangeBounds )
//        {
//            Line adornedElement = ( Line )AdornedElement;
//            _grid.Arrange( new Rect( adornedElement.X1, adornedElement.Y1 - _grid.ActualHeight / 2.0, _grid.ActualWidth, _grid.ActualHeight ) );
//            return arrangeBounds;
//        }

//        protected override Visual GetVisualChild( int index )
//        {
//            return _grid;
//        }

//        protected override int VisualChildrenCount
//        {
//            get
//            {
//                return 1;
//            }
//        }
//    }
//}
