// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.UltrachartScrollbar
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace Ecng.Xaml.Charting
{
    [TemplatePart( Name = "PART_NonSelectedArea", Type = typeof( Path ) )]
    [TemplatePart( Name = "PART_Border", Type = typeof( Border ) )]
    [TemplatePart( Name = "PART_BottomThumb", Type = typeof( UltraThumb ) )]
    [TemplatePart( Name = "PART_TopThumb", Type = typeof( UltraThumb ) )]
    [TemplatePart( Name = "PART_LeftThumb", Type = typeof( UltraThumb ) )]
    [TemplatePart( Name = "PART_MiddleThumb", Type = typeof( UltraThumb ) )]
    [TemplatePart( Name = "PART_RightThumb", Type = typeof( UltraThumb ) )]
    public class UltrachartScrollbar : Control
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(nameof (Axis), typeof (IAxis), typeof (UltrachartScrollbar), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartScrollbar.OnAxisDependencyPropertyChanged)));
        public static readonly DependencyProperty SelectedRangeProperty = DependencyProperty.Register(nameof (SelectedRange), typeof (IRange), typeof (UltrachartScrollbar), new PropertyMetadata(new PropertyChangedCallback(UltrachartScrollbar.OnSelectedRangeDependencyPropertyChanged)));
        public static readonly DependencyProperty SelectedRangePointProperty = DependencyProperty.Register("SelectedRangePoint", typeof (Point), typeof (UltrachartScrollbar), new PropertyMetadata((object) new Point(), new PropertyChangedCallback(UltrachartScrollbar.OnSelectedRangePointDependencyPropertyChanged)));
        public static readonly DependencyProperty GripsThicknessProperty = DependencyProperty.Register(nameof (GripsThickness), typeof (double), typeof (UltrachartScrollbar), new PropertyMetadata((object) 10.0));
        public static readonly DependencyProperty GripsLengthProperty = DependencyProperty.Register(nameof (GripsLength), typeof (double), typeof (UltrachartScrollbar), new PropertyMetadata((object) double.NaN));
        public static readonly DependencyProperty GripsStyleProperty = DependencyProperty.Register(nameof (GripsStyle), typeof (Style), typeof (UltrachartScrollbar), new PropertyMetadata((object) null));
        public static readonly DependencyProperty ViewportStyleProperty = DependencyProperty.Register(nameof (ViewportStyle), typeof (Style), typeof (UltrachartScrollbar), new PropertyMetadata((object) null));
        public static readonly DependencyProperty NonSelectedAreaStyleProperty = DependencyProperty.Register(nameof (NonSelectedAreaStyle), typeof (Style), typeof (UltrachartScrollbar), new PropertyMetadata((object) null));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (UltrachartScrollbar), new PropertyMetadata((object) Orientation.Horizontal));
        public static readonly DependencyProperty ZoomLimitProperty = DependencyProperty.Register(nameof (ZoomLimit), typeof (double), typeof (UltrachartScrollbar), new PropertyMetadata((object) 20.0, new PropertyChangedCallback(UltrachartScrollbar.OnZoomLimitDependencyPropertyChanged)));
        private UltraThumb _centerThumb;
        private UltraThumb _leftThumb;
        private UltraThumb _rightThumb;
        private UltraThumb _topThumb;
        private UltraThumb _bottomThumb;
        private Border _border;
        private Path _path;
        private RectangleGeometry _holeRectangle;
        private ScrollbarCalculationgHelper _helper;
        private readonly RenderTimerHelper _renderTimerHelper;
        private SelectedRangeEventType _eventType;

        public event EventHandler<SelectedRangeChangedEventArgs> SelectedRangeChanged;

        public UltrachartScrollbar()
        {
            this.DefaultStyleKey = ( object ) typeof( UltrachartScrollbar );
            this._renderTimerHelper = new RenderTimerHelper( new Action( this.OnInvalidateRenderTimer ), ( IDispatcherFacade ) new DispatcherUtil( this.Dispatcher ) );
            this.Loaded += ( RoutedEventHandler ) ( ( sender, args ) => this._renderTimerHelper.OnLoaded() );
            this.Unloaded += ( RoutedEventHandler ) ( ( sender, args ) => this._renderTimerHelper.OnUnlodaed() );
            this.SizeChanged += new SizeChangedEventHandler( this.OnSizeChanged );
            this.AddHandler( UIElement.MouseLeftButtonUpEvent, ( Delegate ) new MouseButtonEventHandler( this.OnNonSelectedAreaMouseLeftButtonUp ), false );
        }

        private void OnInvalidateRenderTimer()
        {
            if ( this.Axis == null || this.SelectedRange == null )
                return;
            this.UpdateScrollbar( this.SelectedRange );
        }

        public IRange SelectedRange
        {
            get
            {
                return ( IRange ) this.GetValue( UltrachartScrollbar.SelectedRangeProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.SelectedRangeProperty, ( object ) value );
            }
        }

        public IAxis Axis
        {
            get
            {
                return ( IAxis ) this.GetValue( UltrachartScrollbar.AxisProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.AxisProperty, ( object ) value );
            }
        }

        public double GripsThickness
        {
            get
            {
                return ( double ) this.GetValue( UltrachartScrollbar.GripsThicknessProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.GripsThicknessProperty, ( object ) value );
            }
        }

        public double GripsLength
        {
            get
            {
                return ( double ) this.GetValue( UltrachartScrollbar.GripsLengthProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.GripsLengthProperty, ( object ) value );
            }
        }

        public Style NonSelectedAreaStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltrachartScrollbar.NonSelectedAreaStyleProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.NonSelectedAreaStyleProperty, ( object ) value );
            }
        }

        public Style ViewportStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltrachartScrollbar.ViewportStyleProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.ViewportStyleProperty, ( object ) value );
            }
        }

        public Style GripsStyle
        {
            get
            {
                return ( Style ) this.GetValue( UltrachartScrollbar.GripsStyleProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.GripsStyleProperty, ( object ) value );
            }
        }

        public Orientation Orientation
        {
            get
            {
                return ( Orientation ) this.GetValue( UltrachartScrollbar.OrientationProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.OrientationProperty, ( object ) value );
            }
        }

        public double ZoomLimit
        {
            get
            {
                return ( double ) this.GetValue( UltrachartScrollbar.ZoomLimitProperty );
            }
            set
            {
                this.SetValue( UltrachartScrollbar.ZoomLimitProperty, ( object ) value );
            }
        }

        private void UpdateScrollbar( IRange range )
        {
            this._helper.UpdateRange( range );
            this.UpdateThumbs();
        }

        private void InvalidateElement()
        {
            this._renderTimerHelper.Invalidate();
        }

        private void OnSizeChanged( object sender, SizeChangedEventArgs e )
        {
            if ( this.Axis != null )
                this.RecreateHelper( this.Axis, this.ZoomLimit );
            this.UpdateThumbs();
        }

        private void RecreateHelper( IAxis axis, double zoomLimit )
        {
            double actulaSize = this.Orientation == Orientation.Horizontal ? this.ActualWidth : this.ActualHeight;
            this._helper = new ScrollbarCalculationgHelper( axis, actulaSize, zoomLimit );
        }

        private void UpdateThumbs()
        {
            if ( this._border == null || this.Axis == null )
                return;
            double startOffset = this._helper.StartOffset;
            double stopOffset = this._helper.StopOffset;
            double start = this._helper.Start;
            double num = Math.Max(this._helper.Stop - this._helper.Start, 0.0);
            if ( this.Orientation == Orientation.Horizontal )
            {
                this._border.Padding = new Thickness( startOffset, 0.0, stopOffset, 0.0 );
                this._holeRectangle.Rect = new Rect( start, 0.0, num, 1.0 );
            }
            else
            {
                this._border.Padding = new Thickness( 0.0, startOffset, 0.0, stopOffset );
                this._holeRectangle.Rect = new Rect( 0.0, start, 1.0, num );
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._centerThumb = this.EnforceInstance<UltraThumb>( "PART_MiddleThumb" );
            this._leftThumb = this.EnforceInstance<UltraThumb>( "PART_LeftThumb" );
            this._rightThumb = this.EnforceInstance<UltraThumb>( "PART_RightThumb" );
            this._topThumb = this.EnforceInstance<UltraThumb>( "PART_TopThumb" );
            this._bottomThumb = this.EnforceInstance<UltraThumb>( "PART_BottomThumb" );
            this._border = this.EnforceInstance<Border>( "PART_Border" );
            this._path = this.EnforceInstance<Path>( "PART_NonSelectedArea" );
            GeometryCollection geometryCollection = new GeometryCollection();
            this._holeRectangle = new RectangleGeometry();
            RectangleGeometry rectangleGeometry = new RectangleGeometry() { Rect = new Rect(0.0, 0.0, 1.0, 1.0) };
            geometryCollection.Add( ( Geometry ) rectangleGeometry );
            geometryCollection.Add( ( Geometry ) this._holeRectangle );
            this._path.Data = ( Geometry ) new GeometryGroup()
            {
                Children = geometryCollection
            };
            this.SubscribeEvents();
            this.OnSizeChanged( ( object ) this, ( SizeChangedEventArgs ) null );
        }

        private T EnforceInstance<T>( string partName ) where T : FrameworkElement, new()
        {
            T obj = this.GetTemplateChild(partName) as T;
            if ( ( object ) obj == null )
                obj = Activator.CreateInstance<T>();
            return obj;
        }

        private void SubscribeEvents()
        {
            this._leftThumb.UltraDragDelta += new DragDeltaEventHandler( this.LeftThumbDragDelta );
            this._rightThumb.UltraDragDelta += new DragDeltaEventHandler( this.RightThumbDragDelta );
            this._topThumb.UltraDragDelta += new DragDeltaEventHandler( this.TopThumbDragDelta );
            this._bottomThumb.UltraDragDelta += new DragDeltaEventHandler( this.BottomThumbDragDelta );
            this._centerThumb.UltraDragDelta += new DragDeltaEventHandler( this.CenterThumbDragDelta );
            this._path.MouseLeftButtonUp += new MouseButtonEventHandler( this.OnNonSelectedAreaMouseLeftButtonUp );
        }

        private void RightThumbDragDelta( object sender, DragDeltaEventArgs e )
        {
            this.UpdateEventType( SelectedRangeEventType.Resize );
            this.ResizeThumb( 0.0, e.HorizontalChange );
        }

        private void LeftThumbDragDelta( object sender, DragDeltaEventArgs e )
        {
            this.UpdateEventType( SelectedRangeEventType.Resize );
            this.ResizeThumb( e.HorizontalChange, 0.0 );
        }

        private void TopThumbDragDelta( object sender, DragDeltaEventArgs e )
        {
            this.UpdateEventType( SelectedRangeEventType.Resize );
            this.ResizeThumb( e.VerticalChange, 0.0 );
        }

        private void BottomThumbDragDelta( object sender, DragDeltaEventArgs e )
        {
            this.UpdateEventType( SelectedRangeEventType.Resize );
            this.ResizeThumb( 0.0, e.VerticalChange );
        }

        private void CenterThumbDragDelta( object sender, DragDeltaEventArgs e )
        {
            double num = this.Orientation == Orientation.Horizontal ? e.HorizontalChange : e.VerticalChange;
            this.UpdateEventType( SelectedRangeEventType.Drag );
            this.ResizeThumb( num, num );
        }

        private void ResizeThumb( double start, double stop )
        {
            if ( this.Axis == null )
                return;
            this.UpdateSelectedRange( this._helper.Resize( start, stop ), false );
        }

        private void OnNonSelectedAreaMouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            if ( sender != this._path || this.Axis == null )
                return;
            Point position = e.GetPosition((IInputElement) this);
            double coordinate = this.Orientation == Orientation.Horizontal ? position.X : position.Y;
            this.UpdateEventType( SelectedRangeEventType.Moved );
            this.UpdateSelectedRange( this._helper.MoveTo( coordinate ), true );
        }

        private void UpdateEventType( SelectedRangeEventType eventType )
        {
            this._eventType = eventType;
        }

        private void UpdateSelectedRange( IRange newRange, bool isAnimated = false )
        {
            if ( this.Axis == null || this.Axis.VisibleRange == null || newRange == null )
                return;
            if ( isAnimated )
                this.AnimateSelectedRangeTo( newRange, TimeSpan.FromMilliseconds( 500.0 ) );
            else
                this.SetSelectedRangeInternal( newRange, true );
        }

        private void SetSelectedRangeInternal( IRange newRange, bool resetEventType = true )
        {
            this.SetCurrentValue( UltrachartScrollbar.SelectedRangeProperty, ( object ) newRange );
            if ( !resetEventType )
                return;
            this.ResetEventType();
        }

        private void ResetEventType()
        {
            this._eventType = SelectedRangeEventType.ExternalSource;
        }

        public void AnimateSelectedRangeTo( IRange to, TimeSpan duration )
        {
            Point point1;
            Point point2;
            if ( this.Axis.IsLogarithmicAxis )
            {
                double logarithmicBase = ((ILogarithmicAxis) this.Axis).LogarithmicBase;
                point1 = new Point( Math.Log( this.SelectedRange.Min.ToDouble(), logarithmicBase ), Math.Log( this.SelectedRange.Max.ToDouble(), logarithmicBase ) );
                point2 = new Point( Math.Log( to.Min.ToDouble(), logarithmicBase ), Math.Log( to.Max.ToDouble(), logarithmicBase ) );
            }
            else
            {
                point1 = new Point( this.SelectedRange.Min.ToDouble(), this.SelectedRange.Max.ToDouble() );
                point2 = new Point( to.Min.ToDouble(), to.Max.ToDouble() );
            }
            PointAnimation pointAnimation1 = new PointAnimation();
            pointAnimation1.From = new Point?( point1 );
            pointAnimation1.To = new Point?( point2 );
            pointAnimation1.Duration = ( Duration ) duration;
            ExponentialEase exponentialEase = new ExponentialEase();
            exponentialEase.EasingMode = EasingMode.EaseOut;
            exponentialEase.Exponent = 7.0;
            pointAnimation1.EasingFunction = ( IEasingFunction ) exponentialEase;
            PointAnimation pointAnimation = pointAnimation1;
            Storyboard.SetTarget( ( DependencyObject ) pointAnimation, ( DependencyObject ) this );
            Storyboard.SetTargetProperty( ( DependencyObject ) pointAnimation, new PropertyPath( "SelectedRangePoint", new object[ 0 ] ) );
            Storyboard storyboard = new Storyboard();
            pointAnimation.Completed += ( EventHandler ) ( ( s, e ) =>
            {
                this.SetSelectedRangeInternal( to, true );
                Storyboard.SetTarget( ( DependencyObject ) pointAnimation, ( DependencyObject ) null );
                pointAnimation.FillBehavior = FillBehavior.Stop;
            } );
            storyboard.Duration = ( Duration ) duration;
            storyboard.Children.Add( ( Timeline ) pointAnimation );
            storyboard.Begin();
        }

        private static void OnSelectedRangePointDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Point p = (Point) e.NewValue;
            UltrachartScrollbar scrollbar = (UltrachartScrollbar) d;
            scrollbar.Dispatcher.BeginInvokeAlways( ( Action ) ( () =>
            {
                IRange newRange;
                if ( scrollbar.Axis.IsLogarithmicAxis )
                {
                    double logarithmicBase = ((ILogarithmicAxis) scrollbar.Axis).LogarithmicBase;
                    newRange = RangeFactory.NewRange( scrollbar.SelectedRange.GetType(), ( IComparable ) Math.Pow( logarithmicBase, p.X ), ( IComparable ) Math.Pow( logarithmicBase, p.Y ) );
                }
                else
                    newRange = RangeFactory.NewRange( scrollbar.SelectedRange.GetType(), ( IComparable ) p.X, ( IComparable ) p.Y );
                scrollbar.SetSelectedRangeInternal( newRange, false );
            } ) );
        }

        private static void OnAxisDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartScrollbar ultrachartScrollbar = d as UltrachartScrollbar;
            if ( ultrachartScrollbar == null )
                return;
            IAxis oldValue = e.OldValue as IAxis;
            if ( oldValue != null )
                oldValue.DataRangeChanged -= new EventHandler<EventArgs>( ultrachartScrollbar.OnDataRangeChanged );
            IAxis newValue = e.NewValue as IAxis;
            if ( newValue != null )
            {
                newValue.DataRangeChanged += new EventHandler<EventArgs>( ultrachartScrollbar.OnDataRangeChanged );
                ultrachartScrollbar.AttachNewAxis( newValue );
            }
            else
                ultrachartScrollbar.ClearValue( UltrachartScrollbar.SelectedRangeProperty );
            ultrachartScrollbar.InvalidateElement();
        }

        private void OnDataRangeChanged( object sender, EventArgs eventArgs )
        {
            this.InvalidateElement();
        }

        private void AttachNewAxis( IAxis axis )
        {
            this.RecreateHelper( axis, this.ZoomLimit );
            this.UpdateThumbs();
            Binding binding = new Binding() { Source = (object) axis, Path = new PropertyPath((object) AxisBase.VisibleRangeProperty), Mode = BindingMode.TwoWay };
            this.SetBinding( UltrachartScrollbar.SelectedRangeProperty, ( BindingBase ) binding );
        }

        private static void OnZoomLimitDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartScrollbar ultrachartScrollbar = d as UltrachartScrollbar;
            if ( ultrachartScrollbar == null || ultrachartScrollbar.Axis == null )
                return;
            double newValue = (double) e.NewValue;
            ultrachartScrollbar.RecreateHelper( ultrachartScrollbar.Axis, newValue );
        }

        private static void OnSelectedRangeDependencyPropertyChanged( DependencyObject s, DependencyPropertyChangedEventArgs e )
        {
            UltrachartScrollbar ultraChartScrollbar = (UltrachartScrollbar) s;
            IRange oldValue = e.OldValue as IRange;
            IRange newValue = e.NewValue as IRange;
            if ( oldValue != null )
                oldValue.PropertyChanged -= new PropertyChangedEventHandler( ultraChartScrollbar.OnMaxMinSelectedRangePropertiesChanged );
            if ( newValue == null )
                return;
            newValue.PropertyChanged += new PropertyChangedEventHandler( ultraChartScrollbar.OnMaxMinSelectedRangePropertiesChanged );
            UltrachartScrollbar.UpdateScrollbar( ultraChartScrollbar, newValue );
        }

        private static void UpdateScrollbar( UltrachartScrollbar ultraChartScrollbar, IRange newRange )
        {
            if ( ultraChartScrollbar.Axis == null )
                return;
            ultraChartScrollbar.UpdateScrollbar( newRange );
            ultraChartScrollbar.OnSelectedRangeChagned();
        }

        private void OnMaxMinSelectedRangePropertiesChanged( object sender, PropertyChangedEventArgs e )
        {
            IComparable min = this.SelectedRange.Min;
            IComparable max = this.SelectedRange.Max;
            string propertyName = e.PropertyName;
            if ( !( propertyName == "Min" ) )
            {
                if ( propertyName == "Max" )
                    max = ( IComparable ) ( ( PropertyChangedEventArgsWithValues ) e ).OldValue;
            }
            else
                min = ( IComparable ) ( ( PropertyChangedEventArgsWithValues ) e ).OldValue;
            if ( this.SelectedRange.Equals( ( object ) RangeFactory.NewWithMinMax( this.SelectedRange, min, max ) ) )
                return;
            UltrachartScrollbar.UpdateScrollbar( this, this.SelectedRange );
            BindingExpression bindingExpression = this.GetBindingExpression(UltrachartScrollbar.SelectedRangeProperty);
            if ( bindingExpression == null || bindingExpression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit )
                return;
            bindingExpression.UpdateSource();
            bindingExpression.UpdateTarget();
        }

        private void OnSelectedRangeChagned()
        {
            SelectedRangeChangedEventArgs e = new SelectedRangeChangedEventArgs(this.SelectedRange, this._eventType);
            // ISSUE: reference to a compiler-generated field
            EventHandler<SelectedRangeChangedEventArgs> selectedRangeChanged = this.SelectedRangeChanged;
            if ( selectedRangeChanged == null )
                return;
            selectedRangeChanged( ( object ) this, e );
        }
    }
}
