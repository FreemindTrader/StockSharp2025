using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using SciChart.Charting.ChartModifiers;
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

#nullable disable
internal sealed class ChartOrderModifier( ChartArea _param1 ) : ChartModifierBase
{

    private readonly ChartArea _chartArea = _param1 ?? throw new ArgumentNullException("area");

    private ISciChartSurface _drawingSurface;

    private Line _orderline;

    private ChartOrderModifier.OrderAdorner _orderAdorner;

    private bool _isMouseOver;

    public static readonly DependencyProperty ShowHorizontalLineProperty = DependencyProperty.Register(nameof (ShowHorizontalLine), typeof (bool), typeof (ChartOrderModifier), new PropertyMetadata( true));

    public bool ShowHorizontalLine
    {
        get
        {
            return ( bool ) this.GetValue( ChartOrderModifier.ShowHorizontalLineProperty );
        }
        set
        {
            this.SetValue( ChartOrderModifier.ShowHorizontalLineProperty, ( object ) value );
        }
    }

    public override void OnAttached()
    {
        base.OnAttached();
        this._drawingSurface = this.ParentSurface;
        ( ( UIElement ) this._drawingSurface ).PreviewMouseDown += new MouseButtonEventHandler( this.OnPreviewMouseDown );
    }

    public override void OnDetached()
    {
        base.OnDetached();
        if ( this._drawingSurface != null )
        {
            ( ( UIElement ) this._drawingSurface ).PreviewMouseDown -= new MouseButtonEventHandler( this.OnPreviewMouseDown );
            this._drawingSurface = ( ISciChartSurface ) null;
        }
        this.Clear();
    }

    protected override void OnIsEnabledChanged()
    {
        base.OnIsEnabledChanged();
        this.Clear();
    }

    public override void OnMasterMouseLeave( ModifierMouseArgs e )
    {
        base.OnMasterMouseLeave( e );
        this.Clear();
        this._isMouseOver = false;
    }

    protected override void OnParentSurfaceMouseEnter()
    {
        this._isMouseOver = true;
        base.OnParentSurfaceMouseEnter();
    }

    public override void OnModifierMouseMove( ModifierMouseArgs e )
    {
        base.OnModifierMouseMove( e );
        if ( !e.IsMaster() )
            return;
        this.UpdateOrderAdornerLayer( e.MousePoint() );
    }

    private void OnPreviewMouseDown( object _param1, MouseButtonEventArgs _param2 )
    {
        this.Clear();
    }

    public override void OnModifierMouseUp( ModifierMouseArgs _param1 )
    {
        base.OnModifierMouseUp( e );
        if ( !( this._chartArea.Chart is Chart chart ) || !e.IsMaster() || e.Modifier() != MouseModifier.Ctrl || e.MouseButtons() != MouseButtons.Left && e.MouseButtons() != MouseButtons.Right )
            return;
        e.Handled( true );
        Sides sides = e.MouseButtons() == MouseButtons.Left ? Sides.Buy : Sides.Sell;
        double num = (double) this.YAxis.GetDataValue( e.MousePoint().Y);
        Order order = new Order()
        {
            Side = sides,
            Price = Converter.To<Decimal>((object) num),
            Type = OrderTypes.Limit;
        };
        chart?.InvokeCreateOrderEvent( this._chartArea, order );
    }

    private void UpdateOrderAdornerLayer( Point pt )
    {
        if ( !this._isMouseOver || Mouse.LeftButton != MouseButtonState.Released )
            return;
        if ( this._orderline == null )
        {
            var modifierSurface = this.ModifierSurface;
            if ( modifierSurface == null )
                return;

            Line line = new Line();
            line.X1 = 0.0;
            line.X2 = modifierSurface.ActualWidth;
            line.Y1 = pt.Y;
            line.Y2 = pt.Y;
            line.StrokeThickness = 1.0;
            Line line2 = line;
            DoubleCollection doubleCollection = new DoubleCollection();
            double[] numArray = new double[2]{ 7.0, 4.0 };
            foreach ( double num in numArray )
                doubleCollection.Add( num );
            line2.StrokeDashArray = doubleCollection;
            line.Stroke = ( Brush ) new SolidColorBrush( Colors.OrangeRed );
            line.IsHitTestVisible = false;
            this._orderline = line;
            this._orderline.SetBindings( UIElement.VisibilityProperty, ( object ) this, "ShowHorizontalLine", BindingMode.OneWay, ( IValueConverter ) new BoolToVisibilityConverter()
            {
                FalseValue = Visibility.Hidden
            } );
            modifierSurface.Children().Add( ( UIElement ) this._orderline );
            this._orderAdorner = new ChartOrderModifier.OrderAdorner( this._orderline );
            AdornerLayer.GetAdornerLayer( ( Visual ) this._orderline ).Add( ( Adorner ) this._orderAdorner );
        }
        double dataValue = (double) this.YAxis.GetDataValue( pt.Y );
        this._orderline.Y1 = pt.Y;
        this._orderline.Y2 = pt.Y;
        this._orderAdorner.SellTextBlock().Text = StringHelper.Put( "Sell {0} {1:.00}", new object[ 2 ]
        {
      this.GetOrderTypeFromPrice(Sides.Sell, dataValue) == null ? (object) "Limit" : (object) "Market",
      (object) dataValue
        } );
        this._orderAdorner.BuyTextBlock().Text = StringHelper.Put( "Buy {0} {1:.00}", new object[ 2 ]
        {
      this.GetOrderTypeFromPrice(Sides.Buy, dataValue) == null ? (object) "Limit" : (object) "Market",
      (object) dataValue
        } );
        this._orderAdorner.OpenTooltip( this._orderAdorner.IsMouseOver );
    }

    private OrderTypes GetOrderTypeFromPrice( Sides orderSides, double _param2 )
    {
        FastCandlestickRenderableSeries renderableSeries = this.ParentSurface.RenderableSeries.OfType<FastCandlestickRenderableSeries>().FirstOrDefault<FastCandlestickRenderableSeries>();

        if ( renderableSeries == null )
            return OrderTypes.Limit;

        IList list = ((IOhlcDataSeries) renderableSeries.DataSeries).CloseValues();

        if ( list.Count == 0 )
            return OrderTypes.Limit;

        double count = (double) list[list.Count - 1];
        return orderSides != Sides.Buy ? ( _param2 >= count ? OrderTypes.Limit : OrderTypes.Market ) : ( _param2 <= count ? OrderTypes.Limit : OrderTypes.Market );
    }

    private void Clear()
    {
        if ( this.ModifierSurface == null )
            return;
        if ( this._orderline != null )
            this.ModifierSurface.Children().Remove( ( UIElement ) this._orderline );
        this._orderline = ( Line ) null;
    }

    private sealed class OrderAdorner : Adorner
    {

        private readonly Grid _grid;

        private readonly TextBlock _sellTextBlock;

        private readonly TextBlock _buyTextBlock;

        public OrderAdorner( UIElement _param1 )
          : base( _param1 )
        {
            this.IsHitTestVisible = false;
            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = "Sell";
            textBlock1.Background = ( Brush ) new SolidColorBrush( Colors.Red );
            textBlock1.Foreground = ( Brush ) new SolidColorBrush( Colors.White );
            textBlock1.FontSize = 11.0;
            textBlock1.VerticalAlignment = VerticalAlignment.Center;
            this._sellTextBlock = textBlock1;
            TextBlock textBlock2 = new TextBlock();
            textBlock2.Text = "Buy";
            textBlock2.Background = ( Brush ) new SolidColorBrush( Colors.Green );
            textBlock2.Foreground = ( Brush ) new SolidColorBrush( Colors.White );
            textBlock2.FontSize = 11.0;
            textBlock2.VerticalAlignment = VerticalAlignment.Center;
            this._buyTextBlock = textBlock2;
            Grid grid = new Grid();
            System.Windows.Controls.ToolTip toolTip = new System.Windows.Controls.ToolTip();
            toolTip.Content = ( object ) ( LocalizedStrings.BuyCtrlLeftMouse + Environment.NewLine + LocalizedStrings.SellCtrlRightMouse );
            grid.ToolTip = ( object ) toolTip;
            this._grid = grid;
            this._grid.ColumnDefinitions.Add( new ColumnDefinition()
            {
                Width = new GridLength( 0.0, GridUnitType.Auto )
            } );
            this._grid.RowDefinitions.Add( new RowDefinition()
            {
                Height = new GridLength( 0.0, GridUnitType.Auto )
            } );
            this._grid.RowDefinitions.Add( new RowDefinition()
            {
                Height = new GridLength( 0.0, GridUnitType.Auto )
            } );
            Grid.SetRow( ( UIElement ) this.SellTextBlock(), 0 );
            Grid.SetRow( ( UIElement ) this.BuyTextBlock(), 1 );
            this._grid.Children.Add( ( UIElement ) this.SellTextBlock() );
            this._grid.Children.Add( ( UIElement ) this.BuyTextBlock() );
        }

        public TextBlock SellTextBlock()
        {
            return this._sellTextBlock;
        }

        public TextBlock BuyTextBlock()
        {
            return this._buyTextBlock;
        }

        public void OpenTooltip( bool _param1 )
        {
            ( ( System.Windows.Controls.ToolTip ) this._grid.ToolTip ).IsOpen = _param1;
        }

        protected override Size MeasureOverride( Size _param1 )
        {
            this._grid.Measure( _param1 );
            return _param1;
        }

        protected override Size ArrangeOverride( Size _param1 )
        {
            Line adornedElement = (Line) this.AdornedElement;
            this._grid.Arrange( new Rect( adornedElement.X1, adornedElement.Y1 - this._grid.ActualHeight / 2.0, this._grid.ActualWidth, this._grid.ActualHeight ) );
            return _param1;
        }

        protected override Visual GetVisualChild( int _param1 )
        {
            return ( Visual ) this._grid;
        }

        protected override int VisualChildrenCount => 1;
    }
}
