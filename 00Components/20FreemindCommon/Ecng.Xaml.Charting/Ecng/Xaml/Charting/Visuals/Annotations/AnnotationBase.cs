
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace Ecng.Xaml.Charting
{
    public abstract class AnnotationBase : ApiElementBase, IAnnotation, IHitTestable, IPublishMouseEvents, IXmlSerializable, ISuspendable
    {
        public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (AnnotationBase), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(AnnotationBase.OnXAxisIdChanged)));
        public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (AnnotationBase), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(AnnotationBase.OnYAxisIdChanged)));
        public static readonly DependencyProperty X1Property = DependencyProperty.Register(nameof (X1), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register(nameof (Y1), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
        public static readonly DependencyProperty X2Property = DependencyProperty.Register(nameof (X2), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register(nameof (Y2), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(AnnotationBase.OnAnnotationPositionChanged)));
        public static readonly DependencyProperty AnnotationCanvasProperty = DependencyProperty.Register(nameof (AnnotationCanvas), typeof (AnnotationCanvas), typeof (AnnotationBase), new PropertyMetadata((object) AnnotationCanvas.AboveChart, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty CoordinateModeProperty = DependencyProperty.Register(nameof (CoordinateMode), typeof (AnnotationCoordinateMode), typeof (AnnotationBase), new PropertyMetadata((object) AnnotationCoordinateMode.Absolute, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(AnnotationBase.OnIsSelectedChanged)));
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(nameof (IsEditable), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(AnnotationBase.OnIsEditableChanged)));
        public static readonly DependencyProperty IsHiddenProperty = DependencyProperty.Register(nameof (IsHidden), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(AnnotationBase.OnIsHiddenChanged)));
        public static readonly DependencyProperty DragDirectionsProperty = DependencyProperty.Register(nameof (DragDirections), typeof (XyDirection), typeof (AnnotationBase), new PropertyMetadata((object) XyDirection.XYDirection));
        public static readonly DependencyProperty ResizeDirectionsProperty = DependencyProperty.Register(nameof (ResizeDirections), typeof (XyDirection), typeof (AnnotationBase), new PropertyMetadata((object) XyDirection.XYDirection));
        public static readonly DependencyProperty CanEditTextProperty = DependencyProperty.Register(nameof (CanEditText), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false));
        public static readonly DependencyProperty ResizingGripsStyleProperty = DependencyProperty.Register(nameof (ResizingGripsStyle), typeof (Style), typeof (AnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));
        private IList<IAnnotationAdorner> _myAdorners = (IList<IAnnotationAdorner>) new List<IAnnotationAdorner>();
        private bool _isAttached;
        private bool _templateApplied;
        protected FrameworkElement AnnotationRoot;
        private bool _isDragging;
        private bool _isPrimaryDrag;
        private Point _startPoint;
        private bool _isMouseLeftDown;
        private bool _isResizable;
        private DateTime _mouseLeftDownTimestamp;
        private AnnotationCoordinates _startDragAnnotationCoordinates;
        private IAxis _yAxis;
        private IAxis _xAxis;
        private bool _isLoaded;

        event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.TouchDown
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.TouchMove
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.TouchUp
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler Selected;

        public event EventHandler Unselected;

        public event EventHandler<EventArgs> DragStarted;

        public event EventHandler<EventArgs> DragEnded;

        public event EventHandler<AnnotationDragDeltaEventArgs> DragDelta;

        public event EventHandler IsHiddenChanged;

        public event MouseButtonEventHandler MouseMiddleButtonDown;

        public event MouseButtonEventHandler MouseMiddleButtonUp;

        private void PreviewMouseUpHandler( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton != MouseButton.Middle )
                return;
            // ISSUE: reference to a compiler-generated field
            MouseButtonEventHandler mouseMiddleButtonUp = this.MouseMiddleButtonUp;
            if ( mouseMiddleButtonUp == null )
                return;
            mouseMiddleButtonUp( sender, e );
        }

        private void PreviewMouseDownHandler( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton != MouseButton.Middle )
                return;
            // ISSUE: reference to a compiler-generated field
            MouseButtonEventHandler middleButtonDown = this.MouseMiddleButtonDown;
            if ( middleButtonDown == null )
                return;
            middleButtonDown( sender, e );
        }

        protected AnnotationBase()
        {
            this.DefaultStyleKey = ( object ) typeof( AnnotationBase );
            this.IsResizable = true;
        }

        public Style ResizingGripsStyle
        {
            get
            {
                return ( Style ) this.GetValue( AnnotationBase.ResizingGripsStyleProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.ResizingGripsStyleProperty, ( object ) value );
            }
        }

        public bool CanEditText
        {
            get
            {
                return ( bool ) this.GetValue( AnnotationBase.CanEditTextProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.CanEditTextProperty, ( object ) value );
            }
        }

        public bool IsResizable
        {
            get
            {
                return this._isResizable;
            }
            protected set
            {
                this._isResizable = value;
                this.OnPropertyChanged( nameof( IsResizable ) );
            }
        }

        public string XAxisId
        {
            get
            {
                return ( string ) this.GetValue( AnnotationBase.XAxisIdProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.XAxisIdProperty, ( object ) value );
            }
        }

        public string YAxisId
        {
            get
            {
                return ( string ) this.GetValue( AnnotationBase.YAxisIdProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.YAxisIdProperty, ( object ) value );
            }
        }

        public XyDirection DragDirections
        {
            get
            {
                return ( XyDirection ) this.GetValue( AnnotationBase.DragDirectionsProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.DragDirectionsProperty, ( object ) value );
            }
        }

        public XyDirection ResizeDirections
        {
            get
            {
                return ( XyDirection ) this.GetValue( AnnotationBase.ResizeDirectionsProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.ResizeDirectionsProperty, ( object ) value );
            }
        }

        public AnnotationCoordinateMode CoordinateMode
        {
            get
            {
                return ( AnnotationCoordinateMode ) this.GetValue( AnnotationBase.CoordinateModeProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.CoordinateModeProperty, ( object ) value );
            }
        }

        public AnnotationCanvas AnnotationCanvas
        {
            get
            {
                return ( AnnotationCanvas ) this.GetValue( AnnotationBase.AnnotationCanvasProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.AnnotationCanvasProperty, ( object ) value );
            }
        }

        public bool IsSelected
        {
            get
            {
                return ( bool ) this.GetValue( AnnotationBase.IsSelectedProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.IsSelectedProperty, ( object ) value );
            }
        }

        public bool IsEditable
        {
            get
            {
                return ( bool ) this.GetValue( AnnotationBase.IsEditableProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.IsEditableProperty, ( object ) value );
            }
        }

        public bool IsHidden
        {
            get
            {
                return ( bool ) this.GetValue( AnnotationBase.IsHiddenProperty );
            }
            set
            {
                this.SetValue( AnnotationBase.IsHiddenProperty, ( object ) value );
            }
        }

        [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
        public IComparable X1
        {
            get
            {
                return ( IComparable ) this.GetValue( AnnotationBase.X1Property );
            }
            set
            {
                this.SetValue( AnnotationBase.X1Property, ( object ) value );
            }
        }

        [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
        public IComparable X2
        {
            get
            {
                return ( IComparable ) this.GetValue( AnnotationBase.X2Property );
            }
            set
            {
                this.SetValue( AnnotationBase.X2Property, ( object ) value );
            }
        }

        [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
        public IComparable Y1
        {
            get
            {
                return ( IComparable ) this.GetValue( AnnotationBase.Y1Property );
            }
            set
            {
                this.SetValue( AnnotationBase.Y1Property, ( object ) value );
            }
        }

        [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
        public IComparable Y2
        {
            get
            {
                return ( IComparable ) this.GetValue( AnnotationBase.Y2Property );
            }
            set
            {
                this.SetValue( AnnotationBase.Y2Property, ( object ) value );
            }
        }

        protected abstract Cursor GetSelectedCursor();

        public virtual void OnDragStarted()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<EventArgs> dragStarted = this.DragStarted;
            if ( dragStarted == null )
                return;
            dragStarted( ( object ) this, EventArgs.Empty );
        }

        public virtual void OnDragEnded()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<EventArgs> dragEnded = this.DragEnded;
            if ( dragEnded == null )
                return;
            dragEnded( ( object ) this, EventArgs.Empty );
        }

        public virtual void OnDragDelta()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<AnnotationDragDeltaEventArgs> dragDelta = this.DragDelta;
            if ( dragDelta == null )
                return;
            dragDelta( ( object ) this, new AnnotationDragDeltaEventArgs( 0.0, 0.0 ) );
        }

        public override bool IsAttached
        {
            get
            {
                return this._isAttached;
            }
            set
            {
                this._isAttached = value;
                if ( this._templateApplied )
                    return;
                this.ApplyTemplate();
                this._templateApplied = true;
            }
        }

        public override IAxis YAxis
        {
            get
            {
                return this._yAxis ?? ( this._yAxis = this.GetYAxis( this.YAxisId ) );
            }
        }

        public override IAxis XAxis
        {
            get
            {
                return this._xAxis ?? ( this._xAxis = this.GetXAxis( this.XAxisId ) );
            }
        }

        protected IAnnotationCanvas AnnotationOverlaySurface
        {
            get
            {
                if ( this.ParentSurface == null )
                    return ( IAnnotationCanvas ) null;
                return this.ParentSurface.AnnotationOverlaySurface;
            }
        }

        protected IAnnotationCanvas AnnotationUnderlaySurface
        {
            get
            {
                if ( this.ParentSurface == null )
                    return ( IAnnotationCanvas ) null;
                return this.ParentSurface.AnnotationUnderlaySurface;
            }
        }

        void IAnnotation.OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            this.Schedule( DispatcherPriority.DataBind, ( Action ) ( () =>
            {
                this._xAxis = this.GetXAxis( this.XAxisId );
                this.OnXAxesCollectionChanged( sender, args );
            } ) );
        }

        void IAnnotation.OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            this.Schedule( DispatcherPriority.DataBind, ( Action ) ( () =>
            {
                this._yAxis = this.GetYAxis( this.YAxisId );
                this.OnYAxesCollectionChanged( sender, args );
            } ) );
        }

        private void OnAxisAlignmentChanged( object sender, AxisAlignmentChangedEventArgs e )
        {
            if ( !( e.AxisId == this.XAxisId ) && !( e.AxisId == this.YAxisId ) )
                return;
            this.OnAxisAlignmentChanged( e.AxisId == this.XAxisId ? this.XAxis : this.YAxis, e.OldAlignment );
        }

        protected virtual void OnAxisAlignmentChanged( IAxis axis, AxisAlignment oldAlignment )
        {
        }

        protected virtual void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
        }

        protected virtual void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
        }

        protected virtual void OnYAxisIdChanged()
        {
        }

        protected virtual void OnXAxisIdChanged()
        {
        }

        protected virtual void FocusInputTextArea()
        {
        }

        protected virtual void RemoveFocusInputTextArea()
        {
        }

        public override void OnAttached()
        {
            this.AttachInteractionHandlersTo( ( FrameworkElement ) this );
            this.Loaded += new RoutedEventHandler( this.OnAnnotationLoaded );
            this.ParentSurface.AxisAlignmentChanged += new EventHandler<AxisAlignmentChangedEventArgs>( this.OnAxisAlignmentChanged );
        }

        protected virtual void OnAnnotationLoaded( object sender, RoutedEventArgs e )
        {
            this.PrepareForRendering();
        }

        private void PrepareForRendering()
        {
            if ( !this._isLoaded )
                this._isLoaded = true;
            this.Refresh();
            this.PerformFocusOnInputTextArea();
        }

        protected void PerformFocusOnInputTextArea()
        {
            if ( this.CanEditText && this.IsSelected )
                this.FocusInputTextArea();
            else
                this.RemoveFocusInputTextArea();
        }

        protected virtual void AttachInteractionHandlersTo( FrameworkElement source )
        {
            source.MouseLeftButtonDown += new MouseButtonEventHandler( this.OnAnnotationMouseDown );
            source.MouseLeftButtonUp += new MouseButtonEventHandler( this.OnAnnotationMouseUp );
            source.MouseMove += new MouseEventHandler( this.OnAnnotationMouseMove );
            source.PreviewMouseDown += new MouseButtonEventHandler( this.PreviewMouseDownHandler );
            source.PreviewMouseUp += new MouseButtonEventHandler( this.PreviewMouseUpHandler );
        }

        protected virtual void OnAnnotationMouseDown( object sender, MouseButtonEventArgs e )
        {
            e.Handled = this.TrySelectAnnotation();
            if ( !this.IsSelected || !this.IsEditable )
                return;
            this._isMouseLeftDown = true;
            this._startPoint = e.GetPosition( ( IInputElement ) ( this.RootGrid as UIElement ) );
            this._mouseLeftDownTimestamp = DateTime.UtcNow;
            this.CaptureMouse();
            e.Handled = true;
            this.OnDragStarted();
        }

        protected virtual void OnAnnotationMouseUp( object sender, MouseButtonEventArgs e )
        {
            if ( this._isDragging )
            {
                this._isDragging = false;
                this.OnDragEnded();
            }
            else
                this.PerformFocusOnInputTextArea();
            this.ReleaseMouseCapture();
            this._isMouseLeftDown = false;
        }

        protected virtual void OnAnnotationMouseMove( object sender, MouseEventArgs e )
        {
            Point position = e.GetPosition((IInputElement) (this.RootGrid as UIElement));
            if ( !this._isMouseLeftDown || DateTime.UtcNow - this._mouseLeftDownTimestamp < TimeSpan.FromMilliseconds( 2.0 ) || e.LeftButton != MouseButtonState.Pressed )
                return;
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            ICoordinateCalculator<double> yCalc = this.YAxis != null ? this.YAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
            ICoordinateCalculator<double> xCalc = this.XAxis != null ? this.XAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
            if ( this._isDragging )
            {
                double num1 = position.X - this._startPoint.X;
                double num2 = position.Y - this._startPoint.Y;
                double horizOffset = this.DragDirections == XyDirection.YDirection ? 0.0 : num1;
                double vertOffset = this.DragDirections == XyDirection.XDirection ? 0.0 : num2;
                using ( this.SuspendUpdates() )
                    this.MoveAnnotationTo( this._startDragAnnotationCoordinates, horizOffset, vertOffset );
                this.OnDragDelta();
            }
            else
            {
                this._startPoint = position;
                this._isDragging = true;
                this._startDragAnnotationCoordinates = this.GetCoordinates( canvas, xCalc, yCalc );
            }
        }

        public override void OnDetached()
        {
            using ( IUpdateSuspender updateSuspender = this.SuspendUpdates() )
            {
                updateSuspender.ResumeTargetOnDispose = false;
                this.IsSelected = false;
                this.MakeInvisible();
                ( this.Parent as IAnnotationCanvas ).SafeRemoveChild( ( object ) this );
                this.DetachInteractionHandlersFrom( ( FrameworkElement ) this );
                if ( this.ParentSurface != null )
                    this.ParentSurface.AxisAlignmentChanged -= new EventHandler<AxisAlignmentChangedEventArgs>( this.OnAxisAlignmentChanged );
                this.Loaded -= new RoutedEventHandler( this.OnAnnotationLoaded );
            }
        }

        protected virtual void DetachInteractionHandlersFrom( FrameworkElement source )
        {
            source.MouseLeftButtonDown -= new MouseButtonEventHandler( this.OnAnnotationMouseDown );
            source.MouseLeftButtonUp -= new MouseButtonEventHandler( this.OnAnnotationMouseUp );
            source.MouseMove -= new MouseEventHandler( this.OnAnnotationMouseMove );
            source.PreviewMouseDown -= new MouseButtonEventHandler( this.PreviewMouseDownHandler );
            source.PreviewMouseUp -= new MouseButtonEventHandler( this.PreviewMouseUpHandler );
        }

        public bool Refresh()
        {
            if ( this.IsSuspended || !this._isLoaded || !this.IsAttached )
                return false;
            ICoordinateCalculator<double> xCoordinateCalculator = this.XAxis != null ? this.XAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
            ICoordinateCalculator<double> yCoordinateCalculator = this.YAxis != null ? this.YAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
            if ( xCoordinateCalculator != null && yCoordinateCalculator != null )
                this.Update( xCoordinateCalculator, yCoordinateCalculator );
            return true;
        }

        public virtual void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            if ( canvas == null )
                return;
            canvas.SafeAddChild( ( object ) this, -1 );
            if ( !this._isLoaded )
                return;
            AnnotationCoordinates coordinates = this.GetCoordinates(canvas, xCoordinateCalculator, yCoordinateCalculator);
            if ( this.IsInBounds( coordinates, canvas ) )
            {
                if ( !this.IsHidden )
                {
                    this.MakeVisible( coordinates );
                }
                else
                {
                    if ( this.Visibility == Visibility.Collapsed )
                        return;
                    this.MakeInvisible();
                }
            }
            else
                this.MakeInvisible();
        }

        public void Hide()
        {
            this.IsHidden = true;
        }

        public void Show()
        {
            this.IsHidden = false;
        }

        protected virtual void MakeInvisible()
        {
            this.HideAdornerMarkers();
            this.Visibility = Visibility.Collapsed;
        }

        protected void HideAdornerMarkers()
        {
            foreach ( IAnnotationAdorner adorner in ( IEnumerable<IAnnotationAdorner> ) this._myAdorners )
                adorner.Clear();
        }

        protected IEnumerable<T> GetUsedAdorners<T>( Canvas adornerLayer ) where T : IAnnotationAdorner
        {
            return ( IEnumerable<T> ) adornerLayer.Children.OfType<T>().Where<T>( ( Func<T, bool> ) ( x => x.AdornedAnnotation == this ) ).ToList<T>();
        }

        protected virtual void MakeVisible( AnnotationCoordinates coordinates )
        {
            this.Visibility = Visibility.Visible;
            if ( this.AnnotationRoot != null )
            {
                if ( !this._isLoaded || this.AnnotationRoot.RenderSize == new Size() )
                    this.AnnotationRoot.MeasureArrange();
                this.PlaceAnnotation( coordinates );
            }
            this.UpdateAdorners();
        }

        internal void UpdateAdorners()
        {
            Canvas adornerLayer = this.GetAdornerLayer();
            if ( adornerLayer == null )
                return;
            this.GetUsedAdorners<IAnnotationAdorner>( adornerLayer ).ForEachDo<IAnnotationAdorner>( ( Action<IAnnotationAdorner> ) ( adorner => adorner.UpdatePositions() ) );
        }

        protected virtual bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
        {
            return this.GetCurrentPlacementStrategy().IsInBounds( coordinates, canvas );
        }

        protected virtual void PlaceAnnotation( AnnotationCoordinates coordinates )
        {
            this.GetCurrentPlacementStrategy().PlaceAnnotation( coordinates );
        }

        protected IAnnotationCanvas GetCanvas( AnnotationCanvas annotationCanvas )
        {
            if ( this.ParentSurface == null )
                return ( IAnnotationCanvas ) null;
            switch ( annotationCanvas )
            {
                case AnnotationCanvas.AboveChart:
                    return this.ParentSurface.AnnotationOverlaySurface;
                case AnnotationCanvas.BelowChart:
                    return this.ParentSurface.AnnotationUnderlaySurface;
                case AnnotationCanvas.YAxis:
                    if ( this.YAxis == null )
                        return ( IAnnotationCanvas ) null;
                    return this.YAxis.ModifierAxisCanvas;
                case AnnotationCanvas.XAxis:
                    if ( this.XAxis == null )
                        return ( IAnnotationCanvas ) null;
                    return this.XAxis.ModifierAxisCanvas;
                default:
                    throw new InvalidOperationException( string.Format( "Cannot get an annotation surface for AnnotationCanvas.{0}", ( object ) annotationCanvas ) );
            }
        }

        protected virtual IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( this.XAxis != null && this.XAxis.IsPolarAxis )
                return ( IAnnotationPlacementStrategy ) new AnnotationBase.PolarAnnotationPlacementStrategyBase<AnnotationBase>( this );
            return ( IAnnotationPlacementStrategy ) new AnnotationBase.CartesianAnnotationPlacementStrategyBase<AnnotationBase>( this );
        }

        protected static void OnRenderablePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( AnnotationBase ) d ).Refresh();
        }

        protected static void OnAnnotationPositionChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( AnnotationBase ) d ).Refresh();
            ( ( AnnotationBase ) d ).OnPropertyChanged( "PositionChanged" );
        }



        public void UpdatePosition( Point point1, Point point2 )
        {
            using ( this.SuspendUpdates() )
            {
                IComparable[] comparableArray1 = this.FromCoordinates(point1);
                this.SetCurrentValue( AnnotationBase.X1Property, ( object ) comparableArray1[ 0 ] );
                this.SetCurrentValue( AnnotationBase.Y1Property, ( object ) comparableArray1[ 1 ] );
                if ( this is IAnchorPointAnnotation )
                    return;
                IComparable[] comparableArray2 = this.FromCoordinates(point2);
                this.SetCurrentValue( AnnotationBase.X2Property, ( object ) comparableArray2[ 0 ] );
                this.SetCurrentValue( AnnotationBase.Y2Property, ( object ) comparableArray2[ 1 ] );
            }
        }

        protected virtual IComparable[ ] FromCoordinates( Point coords )
        {
            return this.FromCoordinates( coords.X, coords.Y );
        }

        protected virtual IComparable[ ] FromCoordinates( double xCoord, double yCoord )
        {
            return new IComparable[ 2 ]
            {
        this.XAxis.IsHorizontalAxis ? this.FromCoordinate(xCoord, this.XAxis) : this.FromCoordinate(yCoord, this.XAxis),
        this.XAxis.IsHorizontalAxis ? this.FromCoordinate(yCoord, this.YAxis) : this.FromCoordinate(xCoord, this.YAxis)
            };
        }

        protected virtual IComparable FromCoordinate( double coord, IAxis axis )
        {
            XyDirection xyDirection = axis.IsHorizontalAxis ? XyDirection.XDirection : XyDirection.YDirection;
            IComparable comparable;
            if ( this.CoordinateMode == AnnotationCoordinateMode.Relative || this.CoordinateMode == AnnotationCoordinateMode.RelativeX && xyDirection == XyDirection.XDirection || this.CoordinateMode == AnnotationCoordinateMode.RelativeY && xyDirection == XyDirection.YDirection )
            {
                comparable = this.FromRelativeCoordinate( coord, axis );
            }
            else
            {
                ICategoryCoordinateCalculator coordinateCalculator = axis.GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
                comparable = coordinateCalculator == null ? axis.GetDataValue( coord ) : ( IComparable ) ( int ) coordinateCalculator.GetDataValue( coord );
            }
            return comparable;
        }

        protected virtual IComparable FromRelativeCoordinate( double coord, IAxis axis )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            double num = axis.IsHorizontalAxis ? canvas.ActualWidth : canvas.ActualHeight;
            return ( IComparable ) ( coord / num );
        }

        protected double ToCoordinate( IComparable dataValue, IAxis axis )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            ICoordinateCalculator<double> coordinateCalculator = axis.GetCurrentCoordinateCalculator();
            XyDirection direction = coordinateCalculator.IsHorizontalAxisCalculator ? XyDirection.XDirection : XyDirection.YDirection;
            double canvasMeasurement = coordinateCalculator.IsHorizontalAxisCalculator ? canvas.ActualWidth : canvas.ActualHeight;
            return this.ToCoordinate( dataValue, canvasMeasurement, coordinateCalculator, direction );
        }

        protected virtual Point ToCoordinates( IComparable xDataValue, IComparable yDataValue, IAnnotationCanvas canvas, ICoordinateCalculator<double> xCoordCalc, ICoordinateCalculator<double> yCoordCalc )
        {
            double coordinate1 = this.GetCoordinate(xDataValue, canvas, xCoordCalc);
            double coordinate2 = this.GetCoordinate(yDataValue, canvas, yCoordCalc);
            if ( xCoordCalc != null && !xCoordCalc.IsHorizontalAxisCalculator )
                NumberUtil.Swap( ref coordinate1, ref coordinate2 );
            return new Point( coordinate1, coordinate2 );
        }

        private double GetCoordinate( IComparable dataValue, IAnnotationCanvas canvas, ICoordinateCalculator<double> coordCalc )
        {
            if ( coordCalc == null )
                return 0.0;
            XyDirection direction = coordCalc.IsHorizontalAxisCalculator ? XyDirection.XDirection : XyDirection.YDirection;
            double canvasMeasurement = coordCalc.IsHorizontalAxisCalculator ? canvas.ActualWidth : canvas.ActualHeight;
            return this.ToCoordinate( dataValue, canvasMeasurement, coordCalc, direction );
        }

        protected virtual double ToCoordinate( IComparable dataValue, double canvasMeasurement, ICoordinateCalculator<double> coordCalc, XyDirection direction )
        {
            if ( dataValue == null )
                return double.NaN;
            if ( this.CoordinateMode == AnnotationCoordinateMode.Relative || this.CoordinateMode == AnnotationCoordinateMode.RelativeX && direction == XyDirection.XDirection || this.CoordinateMode == AnnotationCoordinateMode.RelativeY && direction == XyDirection.YDirection )
                return dataValue.ToDouble() * canvasMeasurement;
            if ( coordCalc.IsCategoryAxisCalculator && dataValue is DateTime )
                return this.GetCategoryCoordinate( dataValue, coordCalc as ICategoryCoordinateCalculator );
            return coordCalc.GetCoordinate( dataValue.ToDouble() );
        }

        private double GetCategoryCoordinate( IComparable dataValue, ICategoryCoordinateCalculator categoryCalc )
        {
            int index1 = categoryCalc.TransformDataToIndex((DateTime) dataValue, SearchMode.Exact);
            if ( index1 != -1 )
                return categoryCalc.GetCoordinate( ( double ) index1 );
            int index2 = categoryCalc.TransformDataToIndex((DateTime) dataValue, SearchMode.RoundDown);
            int index3 = categoryCalc.TransformDataToIndex((DateTime) dataValue, SearchMode.RoundUp);
            DateTime data = categoryCalc.TransformIndexToData(index2);
            double num1 = categoryCalc.TransformIndexToData(index3).ToDouble() - data.ToDouble();
            double num2 = (dataValue.ToDouble() - data.ToDouble()) / num1;
            double coordinate = categoryCalc.GetCoordinate((double) index2);
            double num3 = categoryCalc.GetCoordinate((double) index3) - Math.Max(coordinate, 0.0);
            if ( num3 <= 0.0 )
                return -1.0;
            return coordinate + num3 * num2;
        }

        protected AnnotationCoordinates GetCoordinates( IAnnotationCanvas canvas, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc )
        {
            Point coordinates1 = this.ToCoordinates(this.X1, this.Y1, canvas, xCalc, yCalc);
            Point coordinates2 = this.ToCoordinates(this.X2, this.Y2, canvas, xCalc, yCalc);
            double num = 0.0;
            return new AnnotationCoordinates()
            {
                X1Coord = coordinates1.X,
                Y1Coord = coordinates1.Y,
                X2Coord = coordinates2.X,
                Y2Coord = coordinates2.Y,
                YOffset = this.YAxis != null ? this.YAxis.GetAxisOffset() : num,
                XOffset = this.XAxis != null ? this.XAxis.GetAxisOffset() : num
            };
        }

        public void MoveAnnotation( double horizOffset, double vertOffset )
        {
            if ( !this.IsEditable )
                return;
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            if ( this.XAxis == null || this.YAxis == null )
                return;
            ICoordinateCalculator<double> coordinateCalculator1 = this.YAxis.GetCurrentCoordinateCalculator();
            ICoordinateCalculator<double> coordinateCalculator2 = this.XAxis.GetCurrentCoordinateCalculator();
            using ( this.SuspendUpdates() )
                this.MoveAnnotationTo( this.GetCoordinates( canvas, coordinateCalculator2, coordinateCalculator1 ), horizOffset, vertOffset );
        }

        protected virtual void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizOffset, double vertOffset )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            this.GetCurrentPlacementStrategy().MoveAnnotationTo( coordinates, horizOffset, vertOffset, canvas );
        }

        protected bool IsCoordinateValid( double coord, double canvasMeasurement )
        {
            if ( coord >= 0.0 )
                return coord < canvasMeasurement;
            return false;
        }

        public Point[ ] GetBasePoints()
        {
            return this.GetBasePoints( this.GetCoordinates( this.GetCanvas( this.AnnotationCanvas ), this.XAxis != null ? this.XAxis.GetCurrentCoordinateCalculator() : ( ICoordinateCalculator<double> ) null, this.YAxis != null ? this.YAxis.GetCurrentCoordinateCalculator() : ( ICoordinateCalculator<double> ) null ) );
        }

        protected virtual Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
        {
            return this.GetCurrentPlacementStrategy().GetBasePoints( coordinates );
        }

        public void SetBasePoint( Point newPoint, int index )
        {
            if ( !this.IsEditable )
                return;
            using ( this.SuspendUpdates() )
                this.GetCurrentPlacementStrategy().SetBasePoint( newPoint, index );
        }

        protected virtual void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            IComparable[] comparableArray = this.FromCoordinates(newPoint);
            DependencyProperty x;
            DependencyProperty y;
            this.GetPropertiesFromIndex( index, out x, out y );
            this.SetCurrentValue( x, ( object ) comparableArray[ 0 ] );
            this.SetCurrentValue( y, ( object ) comparableArray[ 1 ] );
        }

        protected virtual void GetPropertiesFromIndex( int index, out DependencyProperty x, out DependencyProperty y )
        {
            x = AnnotationBase.X1Property;
            y = AnnotationBase.Y1Property;
            switch ( index )
            {
                case 0:
                    x = AnnotationBase.X1Property;
                    y = AnnotationBase.Y1Property;
                    break;
                case 1:
                    x = AnnotationBase.X2Property;
                    y = AnnotationBase.Y1Property;
                    break;
                case 2:
                    x = AnnotationBase.X2Property;
                    y = AnnotationBase.Y2Property;
                    break;
                case 3:
                    x = AnnotationBase.X1Property;
                    y = AnnotationBase.Y2Property;
                    break;
            }
        }

        protected virtual void HandleIsEditable()
        {
            Cursor cursor = this.IsEditable ? this.GetSelectedCursor() : Cursors.Arrow;
            this.SetCurrentValue( FrameworkElement.CursorProperty, ( object ) cursor );
            this.PerformFocusOnInputTextArea();
        }

        protected Canvas GetAdornerLayer()
        {
            if ( this.ParentSurface == null )
                return ( Canvas ) null;
            return this.ParentSurface.AdornerLayerCanvas;
        }

        protected virtual void AddAdorners( Canvas adornerLayer )
        {
            AnnotationResizeAdorner annotationResizeAdorner = new AnnotationResizeAdorner((IAnnotation) this);
            annotationResizeAdorner.ParentCanvas = adornerLayer;
            this._myAdorners.Add( ( IAnnotationAdorner ) annotationResizeAdorner );
        }

        protected virtual void RemoveAdorners( Canvas adornerLayer )
        {
            this.GetUsedAdorners<AdornerBase>( adornerLayer ).ForEachDo<AdornerBase>( ( Action<AdornerBase> ) ( adorner => adorner.OnDetached() ) );
        }

        public virtual Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            return ElementExtensions.TranslatePoint( this, point, relativeTo );
        }

        public virtual bool IsPointWithinBounds( Point point )
        {
            return HitTestableExtensions.IsPointWithinBounds( this, point );
        }

        public virtual Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            return ElementExtensions.GetBoundsRelativeTo( this, relativeTo );
        }

        internal bool TrySelectAnnotation()
        {
            if ( this.ParentSurface != null )
                return this.ParentSurface.Annotations.TrySelectAnnotation( ( IAnnotation ) this );
            return false;
        }

        private void OnSelected()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler selected = this.Selected;
            if ( selected == null )
                return;
            selected( ( object ) this, EventArgs.Empty );
        }

        private void OnUnselected()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler unselected = this.Unselected;
            if ( unselected == null )
                return;
            unselected( ( object ) this, EventArgs.Empty );
        }

        private void OnIsHiddenChanged()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler isHiddenChanged = this.IsHiddenChanged;
            if ( isHiddenChanged == null )
                return;
            isHiddenChanged( ( object ) this, EventArgs.Empty );
        }

        protected void OnAnnotationDragging( AnnotationDragDeltaEventArgs args )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<AnnotationDragDeltaEventArgs> dragDelta = this.DragDelta;
            if ( dragDelta == null )
                return;
            dragDelta( ( object ) this, args );
        }

        private static void OnIsSelectedChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AnnotationBase annotationBase = d as AnnotationBase;
            if ( annotationBase == null )
                return;
            Canvas adornerLayer = annotationBase.GetAdornerLayer();
            if ( ( bool ) e.NewValue )
            {
                annotationBase.AddAdorners( adornerLayer );
                annotationBase.OnSelected();
            }
            else
            {
                annotationBase.ReleaseMouseCapture();
                annotationBase.RemoveAdorners( adornerLayer );
                annotationBase.PerformFocusOnInputTextArea();
                annotationBase.OnUnselected();
            }
        }

        private static void OnIsEditableChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as AnnotationBase )?.HandleIsEditable();
        }

        private static void OnIsHiddenChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AnnotationBase annotationBase = d as AnnotationBase;
            if ( annotationBase == null || !annotationBase.IsAttached )
                return;
            if ( ( bool ) e.NewValue )
            {
                annotationBase.MakeInvisible();
            }
            else
            {
                ICoordinateCalculator<double> xCalc = annotationBase.XAxis != null ? annotationBase.XAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
                ICoordinateCalculator<double> yCalc = annotationBase.YAxis != null ? annotationBase.YAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
                IAnnotationCanvas canvas = annotationBase.GetCanvas(annotationBase.AnnotationCanvas);
                AnnotationCoordinates coordinates = new AnnotationCoordinates();
                if ( xCalc != null && yCalc != null && canvas != null )
                    coordinates = annotationBase.GetCoordinates( canvas, xCalc, yCalc );
                annotationBase.MakeVisible( coordinates );
                annotationBase.Refresh();
            }
            annotationBase.OnIsHiddenChanged();
        }

        private static void OnYAxisIdChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AnnotationBase annotationBase = d as AnnotationBase;
            if ( annotationBase == null )
                return;
            annotationBase._yAxis = annotationBase.GetYAxis( annotationBase.YAxisId );
            annotationBase.OnYAxisIdChanged();
            ( ( AnnotationBase ) d ).Refresh();
        }

        private static void OnXAxisIdChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AnnotationBase annotationBase = d as AnnotationBase;
            if ( annotationBase == null )
                return;
            annotationBase._xAxis = annotationBase.GetXAxis( annotationBase.XAxisId );
            annotationBase.OnXAxisIdChanged();
            ( ( AnnotationBase ) d ).Refresh();
        }

        public bool IsSuspended
        {
            get
            {
                return UpdateSuspender.GetIsSuspended( ( ISuspendable ) this );
            }
        }

        public IUpdateSuspender SuspendUpdates()
        {
            return ( IUpdateSuspender ) new UpdateSuspender( ( ISuspendable ) this );
        }

        public void ResumeUpdates( IUpdateSuspender updateSuspender )
        {
            if ( !updateSuspender.ResumeTargetOnDispose )
                return;
            this.Refresh();
        }

        public void DecrementSuspend()
        {
        }

        public XmlSchema GetSchema()
        {
            return ( XmlSchema ) null;
        }

        public virtual void ReadXml( XmlReader reader )
        {
            if ( reader.MoveToContent() != XmlNodeType.Element || !( reader.LocalName == this.GetType().Name ) )
                return;
            AnnotationSerializationHelper.Instance.DeserializeProperties( ( IAnnotation ) this, reader );
        }

        public virtual void WriteXml( XmlWriter writer )
        {
            AnnotationSerializationHelper.Instance.SerializeProperties( ( IAnnotation ) this, writer );
        }

        internal FrameworkElement RootElement
        {
            get
            {
                return this.AnnotationRoot;
            }
        }

        internal bool IsDragging
        {
            get
            {
                return this._isDragging;
            }
        }

        internal bool IsMouseLeftDown
        {
            get
            {
                return this._isMouseLeftDown;
            }
        }

        [SpecialName]
        object IAnnotation.DataContext
        {
            get
            {
                return this.DataContext;
            }

            set
            {
                this.DataContext = value;
            }
        }

        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }
        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }

        internal class CartesianAnnotationPlacementStrategyBase<T> : AnnotationPlacementStrategyBase<T> where T : AnnotationBase
        {
            public CartesianAnnotationPlacementStrategyBase( T annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return ( Point[ ] ) null;
            }

            public override void SetBasePoint( Point newPoint, int index )
            {
                this.Annotation.SetBasePoint( newPoint, index, this.Annotation.XAxis, this.Annotation.YAxis );
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                return ( coordinates.X1Coord < 0.0 && coordinates.X2Coord < 0.0 || coordinates.X1Coord > canvas.ActualWidth && coordinates.X2Coord > canvas.ActualWidth || coordinates.Y1Coord < 0.0 && coordinates.Y2Coord < 0.0 ? 1 : ( coordinates.Y1Coord <= canvas.ActualHeight ? 0 : ( coordinates.Y2Coord > canvas.ActualHeight ? 1 : 0 ) ) ) == 0;
            }

            public override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas )
            {
                this.InternalMoveAnntotationTo( coordinates, ref horizontalOffset, ref verticalOffset, annotationCanvas );
                this.Annotation.OnAnnotationDragging( new AnnotationDragDeltaEventArgs( horizontalOffset, verticalOffset ) );
            }

            protected virtual void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, ref double horizOffset, ref double vertOffset, IAnnotationCanvas canvas )
            {
                double num1 = coordinates.X1Coord + horizOffset;
                double num2 = coordinates.X2Coord + horizOffset;
                double num3 = coordinates.Y1Coord + vertOffset;
                double num4 = coordinates.Y2Coord + vertOffset;
                if ( !this.IsCoordinateValid( num1, canvas.ActualWidth ) || !this.IsCoordinateValid( num3, canvas.ActualHeight ) || ( !this.IsCoordinateValid( num2, canvas.ActualWidth ) || !this.IsCoordinateValid( num4, canvas.ActualHeight ) ) )
                {
                    double val1_1 = double.IsNaN(num1) ? 0.0 : num1;
                    double val2_1 = double.IsNaN(num2) ? 0.0 : num2;
                    double val1_2 = double.IsNaN(num3) ? 0.0 : num3;
                    double val2_2 = double.IsNaN(num4) ? 0.0 : num4;
                    if ( Math.Max( val1_1, val2_1 ) < 0.0 )
                        horizOffset -= Math.Max( val1_1, val2_1 );
                    if ( Math.Min( val1_1, val2_1 ) > canvas.ActualWidth )
                        horizOffset -= Math.Min( val1_1, val2_1 ) - ( canvas.ActualWidth - 1.0 );
                    if ( Math.Max( val1_2, val2_2 ) < 0.0 )
                        vertOffset -= Math.Max( val1_2, val2_2 );
                    if ( Math.Min( val1_2, val2_2 ) > canvas.ActualHeight )
                        vertOffset -= Math.Min( val1_2, val2_2 ) - ( canvas.ActualHeight - 1.0 );
                }
                coordinates.X1Coord += horizOffset;
                coordinates.X2Coord += horizOffset;
                coordinates.Y1Coord += vertOffset;
                coordinates.Y2Coord += vertOffset;
                this.Annotation.SetBasePoint( new Point( coordinates.X1Coord, coordinates.Y1Coord ), 0, this.Annotation.XAxis, this.Annotation.YAxis );
                this.Annotation.SetBasePoint( new Point( coordinates.X2Coord, coordinates.Y2Coord ), 2, this.Annotation.XAxis, this.Annotation.YAxis );
            }

            protected bool IsCoordinateValid( double coord, double canvasMeasurement )
            {
                return this.Annotation.IsCoordinateValid( coord, canvasMeasurement );
            }

            protected IComparable[ ] FromCoordinates( double xCoord, double yCoord )
            {
                return this.Annotation.FromCoordinates( xCoord, yCoord );
            }
        }

        internal class PolarAnnotationPlacementStrategyBase<T> : AnnotationPlacementStrategyBase<T> where T : AnnotationBase
        {
            private readonly ITransformationStrategy _transformationStrategy;

            public PolarAnnotationPlacementStrategyBase( T annotation )
              : base( annotation )
            {
                this._transformationStrategy = annotation.Services.GetService<IStrategyManager>().GetTransformationStrategy();
            }

            protected ITransformationStrategy TransformationStrategy
            {
                get
                {
                    return this._transformationStrategy;
                }
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return ( Point[ ] ) null;
            }

            public override void SetBasePoint( Point newPoint, int index )
            {
                this.Annotation.SetBasePoint( this.TransformationStrategy.Transform( newPoint ), index, this.Annotation.XAxis, this.Annotation.YAxis );
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                Size canvasSize = this.CalculateCanvasSize(canvas);
                return this.IsInBoundsInternal( coordinates, canvasSize );
            }

            private Size CalculateCanvasSize( IAnnotationCanvas panel )
            {
                return new Size( 360.0, PolarUtil.CalculateViewportRadius( panel.ActualWidth, panel.ActualHeight ) );
            }

            protected virtual bool IsInBoundsInternal( AnnotationCoordinates coordinates, Size canvasSize )
            {
                return ( coordinates.X1Coord < 0.0 && coordinates.X2Coord < 0.0 || coordinates.X1Coord > canvasSize.Width && coordinates.X2Coord > canvasSize.Width || coordinates.Y1Coord < 0.0 && coordinates.Y2Coord < 0.0 ? 1 : ( coordinates.Y1Coord <= canvasSize.Height ? 0 : ( coordinates.Y2Coord > canvasSize.Height ? 1 : 0 ) ) ) == 0;
            }

            public override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas )
            {
                Tuple<Point, Point> annotationOffsets = this.CalculateAnnotationOffsets(coordinates, horizontalOffset, verticalOffset);
                Size canvasSize = this.CalculateCanvasSize(annotationCanvas);
                this.InternalMoveAnntotationTo( coordinates, annotationOffsets.Item1, annotationOffsets.Item2, canvasSize );
                this.Annotation.OnAnnotationDragging( new AnnotationDragDeltaEventArgs( horizontalOffset, verticalOffset ) );
            }

            protected virtual Tuple<Point, Point> CalculateAnnotationOffsets( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset )
            {
                AnnotationCoordinates annotationCoordinates = this.GetCartesianAnnotationCoordinates(coordinates);
                return new Tuple<Point, Point>( this.CalculatePointOffset( new Point( annotationCoordinates.X1Coord, annotationCoordinates.Y1Coord ), horizontalOffset, verticalOffset ), this.CalculatePointOffset( new Point( annotationCoordinates.X2Coord, annotationCoordinates.Y2Coord ), horizontalOffset, verticalOffset ) );
            }

            protected virtual AnnotationCoordinates GetCartesianAnnotationCoordinates( AnnotationCoordinates coordinates )
            {
                Point point1 = this.TransformationStrategy.ReverseTransform(new Point(coordinates.X1Coord, coordinates.Y1Coord));
                Point point2 = this.TransformationStrategy.ReverseTransform(new Point(coordinates.X2Coord, coordinates.Y2Coord));
                return new AnnotationCoordinates()
                {
                    X1Coord = point1.X,
                    Y1Coord = point1.Y,
                    X2Coord = point2.X,
                    Y2Coord = point2.Y
                };
            }

            private Point CalculatePointOffset( Point point, double horizontalOffset, double verticalOffset )
            {
                Point point1 = this.TransformationStrategy.Transform(point);
                point.X += horizontalOffset;
                point.Y += verticalOffset;
                Point point2 = this.TransformationStrategy.Transform(point);
                return new Point( point2.X - point1.X, point2.Y - point1.Y );
            }

            protected virtual void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, Point x1y1Offset, Point x2y2Offset, Size canvasSize )
            {
                double num1 = coordinates.X1Coord + x1y1Offset.X;
                double num2 = coordinates.X2Coord + x2y2Offset.X;
                double num3 = coordinates.Y1Coord + x1y1Offset.Y;
                double num4 = coordinates.Y2Coord + x2y2Offset.Y;
                if ( !this.IsCoordinateValid( num1, canvasSize.Width ) || !this.IsCoordinateValid( num3, canvasSize.Height ) || ( !this.IsCoordinateValid( num2, canvasSize.Width ) || !this.IsCoordinateValid( num4, canvasSize.Height ) ) )
                {
                    double val1_1 = double.IsNaN(num1) ? 0.0 : num1;
                    double val2_1 = double.IsNaN(num2) ? 0.0 : num2;
                    double val1_2 = double.IsNaN(num3) ? 0.0 : num3;
                    double val2_2 = double.IsNaN(num4) ? 0.0 : num4;
                    if ( Math.Max( val1_1, val2_1 ) < 0.0 )
                    {
                        double num5 = Math.Max(val1_1, val2_1);
                        x1y1Offset.X -= num5;
                        x2y2Offset.X -= num5;
                    }
                    if ( Math.Min( val1_1, val2_1 ) > canvasSize.Width )
                    {
                        double num5 = Math.Min(val1_1, val2_1) - (canvasSize.Width - 1.0);
                        x1y1Offset.X -= num5;
                        x2y2Offset.X -= num5;
                    }
                    if ( Math.Max( val1_2, val2_2 ) < 0.0 )
                    {
                        double num5 = Math.Max(val1_2, val2_2);
                        x1y1Offset.Y -= num5;
                        x2y2Offset.Y -= num5;
                    }
                    if ( Math.Min( val1_2, val2_2 ) > canvasSize.Height )
                    {
                        double num5 = Math.Min(val1_2, val2_2) - (canvasSize.Height - 1.0);
                        x1y1Offset.Y -= num5;
                        x2y2Offset.Y -= num5;
                    }
                }
                coordinates.X1Coord += x1y1Offset.X;
                coordinates.X2Coord += x2y2Offset.X;
                coordinates.Y1Coord += x1y1Offset.Y;
                coordinates.Y2Coord += x2y2Offset.Y;
                this.Annotation.SetBasePoint( new Point( coordinates.X1Coord, coordinates.Y1Coord ), 0, this.Annotation.XAxis, this.Annotation.YAxis );
                this.Annotation.SetBasePoint( new Point( coordinates.X2Coord, coordinates.Y2Coord ), 2, this.Annotation.XAxis, this.Annotation.YAxis );
            }

            protected bool IsCoordinateValid( double coord, double canvasMeasurement )
            {
                return this.Annotation.IsCoordinateValid( coord, canvasMeasurement );
            }

            protected IComparable[ ] FromCoordinates( double xCoord, double yCoord )
            {
                return this.Annotation.FromCoordinates( xCoord, yCoord );
            }
        }
    }
}
