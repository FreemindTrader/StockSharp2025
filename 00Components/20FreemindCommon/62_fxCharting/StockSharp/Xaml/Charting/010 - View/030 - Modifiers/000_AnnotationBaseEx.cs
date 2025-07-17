
using System.Windows.Controls;
using System;
using System.Windows.Input;
using SciChart.Charting.Visuals.Annotations;

public class AnnotationBaseEx : AnnotationBase
{    
    protected override Cursor GetSelectedCursor()
    {
        return Cursors.SizeNS;
    }

    /// <summary>
    /// It seems like StockSharp version of Scichart has some customize code, so I am extracting those code out to it's EX version to duplicate its functionalities
    /// </summary>
    public void UpdateAdorners()
    {
        Canvas adornerLayer = this.GetAdornerLayer();
        if ( adornerLayer == null )
            return;
        var adorners = this.GetUsedAdorners<IAnnotationAdorner>(adornerLayer);

        foreach ( var adorner in adorners )
        {
            adorner.UpdatePositions();
        }                        
    }
}

//using SciChart.Charting.ChartModifiers;
//using SciChart.Charting.Visuals.Annotations;
//using SciChart.Charting.Visuals.Axes;
//using SciChart.Charting.Visuals.Events;
//using StockSharp.Charting;
//using SciChart.Core.Framework;
//using StockSharp.Charting;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Threading;
//using System.Xml;
//using System.Xml.Schema;
//using System.Xml.Serialization;
//using StockSharp.Xaml.Charting.Visuals.Events;
//using SciChart.Charting;
//using SciChart.Charting.Numerics.CoordinateCalculators;
//using StockSharp.Xaml.Charting.ATony;


//#nullable disable
//namespace StockSharp.Xaml.Charting.Visuals.Annotations;

//public abstract class AnnotationBase : ApiElementBase,
//                                          IXmlSerializable,
//                                          ISuspendable,
//                                          StockSharp.Xaml.Charting.Utility.Mouse.IPublishMouseEvents,
//                                          IAnnotation,
//                                          IHitTestable
//{
//    public static readonly DependencyProperty XAxisIdProperty = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (AnnotationBase), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(OnXAxisIdChanged)));
//    public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (AnnotationBase), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(OnYAxisIdChanged)));
//    public static readonly DependencyProperty X1Property = DependencyProperty.Register(nameof (X1), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(OnAnnotationPositionChanged)));
//    public static readonly DependencyProperty Y1Property = DependencyProperty.Register(nameof (Y1), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(OnAnnotationPositionChanged)));
//    public static readonly DependencyProperty X2Property = DependencyProperty.Register(nameof (X2), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(OnAnnotationPositionChanged)));
//    public static readonly DependencyProperty Y2Property = DependencyProperty.Register(nameof (Y2), typeof (IComparable), typeof (AnnotationBase), new PropertyMetadata((object) null, new PropertyChangedCallback(OnAnnotationPositionChanged)));
//    public static readonly DependencyProperty AnnotationCanvasProperty = DependencyProperty.Register(nameof (AnnotationCanvas), typeof (AnnotationCanvas), typeof (AnnotationBase), new PropertyMetadata((object) AnnotationCanvas.AboveChart, new PropertyChangedCallback(OnRenderablePropertyChanged)));
//    public static readonly DependencyProperty CoordinateModeProperty = DependencyProperty.Register(nameof (CoordinateMode), typeof (AnnotationCoordinateMode), typeof (AnnotationBase), new PropertyMetadata((object) AnnotationCoordinateMode.Absolute, new PropertyChangedCallback(OnRenderablePropertyChanged)));
//    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(OnIsSelectedChanged)));
//    public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(nameof (IsEditable), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(OnIsEditableChanged)));
//    public static readonly DependencyProperty IsHiddenProperty = DependencyProperty.Register(nameof (IsHidden), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false, new PropertyChangedCallback(OnIsHiddenChanged)));
//    public static readonly DependencyProperty DragDirectionsProperty = DependencyProperty.Register(nameof (DragDirections), typeof (XyDirection), typeof (AnnotationBase), new PropertyMetadata((object) XyDirection.XYDirection));
//    public static readonly DependencyProperty ResizeDirectionsProperty = DependencyProperty.Register(nameof (ResizeDirections), typeof (XyDirection), typeof (AnnotationBase), new PropertyMetadata((object) XyDirection.XYDirection));
//    public static readonly DependencyProperty CanEditTextProperty = DependencyProperty.Register(nameof (CanEditText), typeof (bool), typeof (AnnotationBase), new PropertyMetadata((object) false));
//    public static readonly DependencyProperty ResizingGripsStyleProperty = DependencyProperty.Register(nameof (ResizingGripsStyle), typeof (Style), typeof (AnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));
//    private bool _isAttached;
//    private bool _templateApplied;
//    protected FrameworkElement AnnotationRoot;
//    private bool _isDragging;
//    private bool _isPrimaryDrag;
//    private Point _startPoint;
//    private bool _isMouseLeftDown;
//    private bool _isResizable;
//    private DateTime _mouseLeftDownTimestamp;
//    private AnnotationCoordinates _startDragAnnotationCoordinates;
//    private IList<IAnnotationAdorner> _myAdorners = (IList<IAnnotationAdorner>) new List<IAnnotationAdorner>();
//    private IAxis _yAxis;
//    private IAxis _xAxis;
//    private bool _isLoaded;

//    protected AnnotationBase()
//    {
//        DefaultStyleKey = typeof( AnnotationBase );
//        IsResizable = true;
//    }

//    protected Point DragStartPoint => _startPoint;

//    event EventHandler<TouchManipulationEventArgs> StockSharp.Xaml.Charting.Utility.Mouse.IPublishMouseEvents.TouchDown
//    {
//        add => throw new NotImplementedException();
//        remove => throw new NotImplementedException();
//    }

//    event EventHandler<TouchManipulationEventArgs> StockSharp.Xaml.Charting.Utility.Mouse.IPublishMouseEvents.TouchMove
//    {
//        add => throw new NotImplementedException();
//        remove => throw new NotImplementedException();
//    }

//    event EventHandler<TouchManipulationEventArgs> StockSharp.Xaml.Charting.Utility.Mouse.IPublishMouseEvents.TouchUp
//    {
//        add => throw new NotImplementedException();
//        remove => throw new NotImplementedException();
//    }

//    public event EventHandler Selected;

//    public event EventHandler Unselected;

//    public event EventHandler<EventArgs> DragStarted;

//    public event EventHandler<EventArgs> DragEnded;

//    public event EventHandler<AnnotationDragDeltaEventArgs> DragDelta;

//    public event EventHandler IsHiddenChanged;

//    public event MouseButtonEventHandler MouseMiddleButtonDown;

//    public event MouseButtonEventHandler MouseMiddleButtonUp;

//    /// <summary>
//    /// Preview mouse is the Middle mouse button
//    /// </summary>
//    /// <param name="sender"></param>
//    /// <param name="e"></param>
//    private void PreviewMouseUpHandler( object sender, MouseButtonEventArgs e )
//    {
//        if ( e.ChangedButton != MouseButton.Middle )
//            return;
//        var mouseMiddleButtonUp = MouseMiddleButtonUp;
//        if ( mouseMiddleButtonUp == null )
//            return;
//        mouseMiddleButtonUp( sender, e );
//    }

//    private void PreviewMouseDownHandler( object sender, MouseButtonEventArgs e )
//    {
//        if ( e.ChangedButton != MouseButton.Middle )
//            return;

//        var middleButtonDown = MouseMiddleButtonDown;
//        if ( middleButtonDown == null )
//            return;
//        middleButtonDown( sender, e );
//    }

//    public Style ResizingGripsStyle
//    {
//        get => ( Style ) GetValue( ResizingGripsStyleProperty );
//        set => SetValue( ResizingGripsStyleProperty, value );
//    }

//    public bool CanEditText
//    {
//        get => ( bool ) GetValue( CanEditTextProperty );
//        set => SetValue( CanEditTextProperty, value );
//    }

//    public bool IsResizable
//    {
//        get => _isResizable;
//        protected set
//        {
//            _isResizable = value;
//            OnPropertyChanged( nameof( IsResizable ) );
//        }
//    }

//    public string XAxisId
//    {
//        get => ( string ) GetValue( XAxisIdProperty );
//        set => SetValue( XAxisIdProperty, value );
//    }

//    public string YAxisId
//    {
//        get => ( string ) GetValue( YAxisIdProperty );
//        set => SetValue( YAxisIdProperty, value );
//    }

//    public XyDirection DragDirections
//    {
//        get
//        {
//            return ( XyDirection ) GetValue( DragDirectionsProperty );
//        }
//        set => SetValue( DragDirectionsProperty, value );
//    }

//    public XyDirection ResizeDirections
//    {
//        get
//        {
//            return ( XyDirection ) GetValue( ResizeDirectionsProperty );
//        }
//        set => SetValue( ResizeDirectionsProperty, value );
//    }

//    public StockSharp.Charting.AnnotationCoordinateMode CoordinateMode
//    {
//        get => ( StockSharp.Charting.AnnotationCoordinateMode ) GetValue( CoordinateModeProperty );
//        set => SetValue( CoordinateModeProperty, value );
//    }

//    public AnnotationCanvas AnnotationCanvas
//    {
//        get
//        {
//            return ( AnnotationCanvas ) GetValue( AnnotationCanvasProperty );
//        }
//        set => SetValue( AnnotationCanvasProperty, value );
//    }

//    public bool IsSelected
//    {
//        get => ( bool ) GetValue( IsSelectedProperty );
//        set => SetValue( IsSelectedProperty, value );
//    }

//    public bool IsEditable
//    {
//        get => ( bool ) GetValue( IsEditableProperty );
//        set => SetValue( IsEditableProperty, value );
//    }

//    public bool IsHidden
//    {
//        get => ( bool ) GetValue( IsHiddenProperty );
//        set => SetValue( IsHiddenProperty, value );
//    }

//    [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
//    public IComparable X1
//    {
//        get => ( IComparable ) GetValue( X1Property );
//        set => SetValue( X1Property, value );
//    }

//    [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
//    public IComparable X2
//    {
//        get => ( IComparable ) GetValue( X2Property );
//        set => SetValue( X2Property, value );
//    }

//    [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
//    public IComparable Y1
//    {
//        get => ( IComparable ) GetValue( Y1Property );
//        set => SetValue( Y1Property, value );
//    }

//    [TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
//    public IComparable Y2
//    {
//        get => ( IComparable ) GetValue( Y2Property );
//        set => SetValue( Y2Property, value );
//    }

//    protected abstract Cursor GetSelectedCursor();

//    protected virtual void OnDragStarted()
//    {
//    }

//    protected virtual void OnDragEnded()
//    {
//    }

//    protected virtual void OnDragDelta( double hOffset, double vOffset )
//    {
//    }

//    //
//    // Summary:
//    //     Gets an SciChart.Charting.Visuals.Annotations.AnnotationCoordinates struct containing
//    //     pixel coordinates to place or update the annotation in the current render pass
//    //
//    //
//    // Parameters:
//    //   canvas:
//    //     The canvas the annotation will be placed on
//    //
//    //   xCalc:
//    //     The current XAxis SciChart.Charting.Numerics.CoordinateCalculators.ICoordinateCalculator`1
//    //     to perform data to pixel transformations
//    //
//    //   yCalc:
//    //     The current YAxis SciChart.Charting.Numerics.CoordinateCalculators.ICoordinateCalculator`1
//    //     to perform data to pixel transformations
//    //
//    // Returns:
//    //     The SciChart.Charting.Visuals.Annotations.AnnotationCoordinates struct containing
//    //     pixel coordinates

//    protected AnnotationCoordinates GetCoordinates( IAnnotationCanvas canvas, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc )
//    {
//        Point coordinates1 = ToCoordinates(X1, Y1, canvas, xCalc, yCalc);
//        Point coordinates2 = ToCoordinates(X2, Y2, canvas, xCalc, yCalc);

//        return new AnnotationCoordinates()
//        {
//            X1Coord = coordinates1.X,
//            Y1Coord = coordinates1.Y,
//            X2Coord = coordinates2.X,
//            Y2Coord = coordinates2.Y,
//            YOffset = YAxis != null ? YAxis.GetOffsetForLabels() : 0,
//            XOffset = XAxis != null ? XAxis.GetOffsetForLabels() : 0
//        };
//    }

//    protected virtual Point ToCoordinates( IComparable xDataValue, IComparable yDataValue, IAnnotationCanvas canvas, ICoordinateCalculator<double> xCoordCalc, ICoordinateCalculator<double> yCoordCalc )
//    {
//        double coordinate1 = GetCoordinate(xDataValue, canvas, xCoordCalc);
//        double coordinate2 = GetCoordinate(yDataValue, canvas, yCoordCalc);
//        if ( xCoordCalc != null && !xCoordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K())
//      NumberUtil.Swap( ref coordinate1, ref coordinate2 );
//        return new Point( coordinate1, coordinate2 );
//    }

//    public void StartDrag( bool isPrimaryDrag )
//    {
//        _isDragging = true;
//        _isPrimaryDrag = isPrimaryDrag;
//        var canvas = GetCanvas(AnnotationCanvas);
//        var yCalc = YAxis?.GetCurrentCoordinateCalculator();
//        var xCalc = XAxis?.GetCurrentCoordinateCalculator();
//        _startDragAnnotationCoordinates = GetCoordinates( canvas, xCalc, yCalc );
//        OnDragStarted();
//        RaiseAnnotationDragStarted( _isPrimaryDrag, false );
//    }

//    public void EndDrag()
//    {
//        OnDragEnded();
//        bool isPrimaryDrag = _isPrimaryDrag;
//        _isDragging = _isPrimaryDrag = false;
//        RaiseAnnotationDragEnded( isPrimaryDrag, false );
//    }

//    public void Drag( double hOffset, double vOffset )
//    {
//        if ( !_isDragging )
//            return;
//        using ( SuspendUpdates() )
//            (hOffset, vOffset) = MoveAnnotationTo( _startDragAnnotationCoordinates, hOffset, vOffset );
//        OnDragDelta( hOffset, vOffset );
//        RaiseAnnotationDragging( hOffset, vOffset, _isPrimaryDrag, false );
//    }

//    public virtual bool CanMultiSelect(
//      IAnnotation[ ] annotations )
//    {
//        return annotations.Length == 1;
//    }

//    public override bool IsAttached
//    {
//        get => _isAttached;
//        set
//        {
//            _isAttached = value;
//            if ( _templateApplied )
//                return;
//            ApplyTemplate();
//            _templateApplied = true;
//        }
//    }

//    public override IAxis YAxis
//    {
//        get => _yAxis ?? ( _yAxis = GetYAxis( YAxisId ) );
//    }

//    public override IAxis XAxis
//    {
//        get => _xAxis ?? ( _xAxis = GetXAxis( XAxisId ) );
//    }

//    protected IAnnotationCanvas AnnotationOverlaySurface
//    {
//        get
//        {
//            return ParentSurface == null ? ( IAnnotationCanvas ) null : ParentSurface.AnnotationOverlaySurface;
//        }
//    }

//    protected IAnnotationCanvas AnnotationUnderlaySurface
//    {
//        get
//        {
//            return ParentSurface == null ? ( IAnnotationCanvas ) null : ParentSurface.AnnotationUnderlaySurface();
//        }
//    }

//    void IAnnotation.OnXAxesCollectionChanged(
//        object sender,
//        NotifyCollectionChangedEventArgs args )
//    {
//        \u0023\u003DzHGGuFpQ\u003D(( DispatcherPriority ) 8, new Action( new \u0023\u003Dzffg\u0024YXBnGm7H\u0024PCqlQv7PCc\u003D()
//        {
//                        _variableSome3535 = this,
//      \u0023\u003DzwM8aRUE\u003D = sender,
//      \u0023\u003DzTi2kmf4\u003D = args
//                    }.\u0023\u003Dz51OAXpgKHWL1SXLaCA5i5tpZSV6aNEG7pUicQk0yky3I6F_EHJGXbx7TM94H));
//    }

//    void IAnnotation.OnYAxesCollectionChanged(
//      object sender,
//      NotifyCollectionChangedEventArgs args )
//    {
//        \u0023\u003DzHGGuFpQ\u003D(( DispatcherPriority ) 8, new Action( new \u0023\u003DzPDNKpHuRG7yCW5JW8_EOM4E\u003D()
//        {
//                        _variableSome3535 = this,
//      \u0023\u003DzwM8aRUE\u003D = sender,
//      \u0023\u003DzTi2kmf4\u003D = args
//                    }.\u0023\u003DzIOaiByPzcFm0hgjIqRsFUKJzOnbT2YChDvFyp8X_Z2t0giSUkPjoyzwmi0T_));
//    }

//    private void OnAxisAlignmentChanged(
//      object sender,
//          \u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D e)
//    {
//        if ( !( e.\u0023\u003DzAz1mGnFFNNic() == XAxisId) && !( e.\u0023\u003DzAz1mGnFFNNic() == YAxisId))
//      return;
//        OnAxisAlignmentChanged( e.\u0023\u003DzAz1mGnFFNNic() == XAxisId ? XAxis : YAxis, e.\u0023\u003Dz4LPnYX_iGIbT() );
//    }

//    protected virtual void OnAxisAlignmentChanged(
//      IAxis axis,
//      dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd oldAlignment )
//    {
//    }

//    protected virtual void OnXAxesCollectionChanged(
//      object sender,
//      NotifyCollectionChangedEventArgs args )
//    {
//    }

//    protected virtual void OnYAxesCollectionChanged(
//      object sender,
//      NotifyCollectionChangedEventArgs args )
//    {
//    }

//    protected virtual void OnYAxisIdChanged()
//    {
//    }

//    protected virtual void OnXAxisIdChanged()
//    {
//    }

//    protected virtual void FocusInputTextArea()
//    {
//    }

//    protected virtual void RemoveFocusInputTextArea()
//    {
//    }

//    public override void OnAttached()
//    {
//        AttachInteractionHandlersTo( ( FrameworkElement ) this );
//        Loaded += new RoutedEventHandler( OnAnnotationLoaded );
//        ParentSurface.\u0023\u003DzexJGsaAi6rVI( new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>( OnAxisAlignmentChanged ) );
//    }

//    protected virtual void OnAnnotationLoaded( object sender, RoutedEventArgs e )
//    {
//        PrepareForRendering();
//    }

//    private void PrepareForRendering()
//    {
//        if ( !_isLoaded )
//            _isLoaded = true;
//        Refresh();
//        PerformFocusOnInputTextArea();
//    }

//    protected void PerformFocusOnInputTextArea()
//    {
//        if ( CanEditText && IsSelected )
//            FocusInputTextArea();
//        else
//            RemoveFocusInputTextArea();
//    }

//    protected virtual void AttachInteractionHandlersTo( FrameworkElement source )
//    {
//        source.MouseLeftButtonDown += new MouseButtonEventHandler( OnAnnotationMouseDown );
//        source.MouseLeftButtonUp += new MouseButtonEventHandler( OnAnnotationMouseUp );
//        source.MouseMove += new MouseEventHandler( OnAnnotationMouseMove );
//        source.PreviewMouseDown += new MouseButtonEventHandler( PreviewMouseDownHandler );
//        source.PreviewMouseUp += new MouseButtonEventHandler( PreviewMouseUpHandler );
//    }

//    protected virtual void OnAnnotationMouseDown( object sender, MouseButtonEventArgs e )
//    {
//        if ( e.ChangedButton != MouseButton.Left )
//            return;
//        e.Handled = TrySelectAnnotation();
//        if ( !IsSelected || !IsEditable )
//            return;
//        _startPoint = Mouse.GetPosition( ( IInputElement ) ( \u0023\u003Dzwc4Gzka23TGB() as UIElement ));
//        _isMouseLeftDown = true;
//        _mouseLeftDownTimestamp = DateTime.UtcNow;
//        CaptureMouse();
//        e.Handled = true;
//    }

//    protected virtual void OnAnnotationMouseUp( object sender, MouseButtonEventArgs e )
//    {
//        if ( e.ChangedButton != MouseButton.Left )
//            return;
//        ReleaseMouseCapture();
//        _isMouseLeftDown = false;
//        if ( _isDragging )
//            EndDrag();
//        else
//            PerformFocusOnInputTextArea();
//    }

//    protected virtual void OnAnnotationMouseMove( object sender, MouseEventArgs e )
//    {
//        if ( !_isMouseLeftDown || DateTime.UtcNow - _mouseLeftDownTimestamp < TimeSpan.FromMilliseconds( 2.0 ) || e.LeftButton != MouseButtonState.Pressed )
//            return;
//        if ( !_isDragging )
//            StartDrag( true );
//        Point position = Mouse.GetPosition((IInputElement) (\u0023\u003Dzwc4Gzka23TGB() as UIElement));
//        Drag( DragDirections == XyDirection.YDirection ? 0.0 : position.X - _startPoint.X, DragDirections == XyDirection.XDirection ? 0.0 : position.Y - _startPoint.Y );
//    }

//    public override void OnDetached()
//    {
//        using ( IUpdateSuspender fq05jnDg3bOrIrgCjote = SuspendUpdates() )
//        {
//            fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose = false;
//            IsSelected = false;
//            MakeInvisible();
//            ( Parent as IAnnotationCanvas ).\u0023\u003DziYdJ\u00246cCiBha( this );
//            DetachInteractionHandlersFrom( ( FrameworkElement ) this );
//            if ( ParentSurface != null )
//                ParentSurface.\u0023\u003Dz38JNnebwqLph( new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>( OnAxisAlignmentChanged ) );
//            Loaded -= new RoutedEventHandler( OnAnnotationLoaded );
//        }
//    }

//    protected virtual void DetachInteractionHandlersFrom( FrameworkElement source )
//    {
//        source.MouseLeftButtonDown -= new MouseButtonEventHandler( OnAnnotationMouseDown );
//        source.MouseLeftButtonUp -= new MouseButtonEventHandler( OnAnnotationMouseUp );
//        source.MouseMove -= new MouseEventHandler( OnAnnotationMouseMove );
//        source.PreviewMouseDown -= new MouseButtonEventHandler( PreviewMouseDownHandler );
//        source.PreviewMouseUp -= new MouseButtonEventHandler( PreviewMouseUpHandler );
//    }

//    public bool Refresh()
//    {
//        if ( IsSuspended || !_isLoaded || !IsAttached )
//            return false;
//        ICoordinateCalculator< double > xCoordinateCalculator = XAxis != null ? XAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator< double >) null;
//        ICoordinateCalculator< double > yCoordinateCalculator = YAxis != null ? YAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator< double >) null;
//        if ( xCoordinateCalculator != null && yCoordinateCalculator != null )
//            Update( xCoordinateCalculator, yCoordinateCalculator );
//        return true;
//    }

//    public virtual void Update(
//    ICoordinateCalculator<double> xCoordinateCalculator,
//        ICoordinateCalculator<double> yCoordinateCalculator )
//    {
//        IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
//        if ( canvas == null )
//            return;
//        canvas.\u0023\u003DzH0osWQkV_Y8_( this, -1 );
//        if ( !_isLoaded )
//            return;
//        AnnotationCoordinates coordinates = GetCoordinates( canvas, xCoordinateCalculator, yCoordinateCalculator );
//        if ( IsInBounds( coordinates, canvas ) )
//        {
//            if ( !IsHidden )
//            {
//                MakeVisible( coordinates );
//            }
//            else
//            {
//                if ( Visibility == Visibility.Collapsed )
//                    return;
//                MakeInvisible();
//            }
//        }
//        else
//            MakeInvisible();
//    }

//    public void Hide() => IsHidden = true;

//    public void Show() => IsHidden = false;

//    protected virtual void MakeInvisible()
//    {
//        HideAdornerMarkers();
//        Visibility = Visibility.Collapsed;
//    }

//    protected void HideAdornerMarkers()
//    {
//        foreach ( IAnnotationAdorner adorner in ( IEnumerable<IAnnotationAdorner> ) _myAdorners )
//            adorner.Clear();
//    }

//    protected IEnumerable<T> GetUsedAdorners<T>( Canvas adornerLayer ) where T : IAnnotationAdorner
//    {
//        return ( IEnumerable<T> ) adornerLayer.Children.OfType<T>().Where<T>( ( Func<T, bool> ) ( x => x.\u0023\u003Dzy2oKVLXXOFmI() == this )).ToList<T>();
//    }

//    protected virtual void MakeVisible(
//      AnnotationCoordinates coordinates )
//    {
//        Visibility = Visibility.Visible;
//        if ( AnnotationRoot != null )
//        {
//            if ( !_isLoaded || Size.op_Equality( AnnotationRoot.RenderSize, new Size() ) )
//                AnnotationRoot.\u0023\u003DzI0WdlDcUgrX_();
//            PlaceAnnotation( coordinates );
//        }
//        UpdateAdorners();
//    }

//    internal void UpdateAdorners()
//    {
//        Canvas adornerLayer = GetAdornerLayer();
//        if ( adornerLayer == null )
//            return;
//        GetUsedAdorners<IAnnotationAdorner>( adornerLayer ).\u0023\u003Dz30RSSSygABj_<IAnnotationAdorner>( SomeClass34343383.\u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D ?? ( SomeClass34343383.\u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D = new Action<IAnnotationAdorner>( SomeClass34343383.SomeMethond0343.\u0023\u003DzjRwQMrvkACc83ywGyp_nJAU\u003D) ));
//    }

//    protected virtual bool IsInBounds(
//      AnnotationCoordinates coordinates,
//      IAnnotationCanvas canvas )
//    {
//        return GetCurrentPlacementStrategy().\u0023\u003DzxGhbraO0gg9\u0024(coordinates, canvas);
//    }

//    protected virtual void PlaceAnnotation(
//      AnnotationCoordinates coordinates )
//    {
//        GetCurrentPlacementStrategy().\u0023\u003DzNUoYFVRHgzxB( coordinates );
//    }

//    protected IAnnotationCanvas GetCanvas(
//      AnnotationCanvas annotationCanvas )
//    {
//        if ( ParentSurface == null )
//            return ( IAnnotationCanvas ) null;
//        switch ( annotationCanvas )
//        {
//            case AnnotationCanvas.AboveChart:
//                return ParentSurface.AnnotationOverlaySurface;
//            case AnnotationCanvas.BelowChart:
//                return ParentSurface.AnnotationUnderlaySurface();
//            case AnnotationCanvas.YAxis:
//                return YAxis == null ? ( IAnnotationCanvas ) null : YAxis.get_ModifierAxisCanvas();
//            case AnnotationCanvas.XAxis:
//                return XAxis == null ? ( IAnnotationCanvas ) null : XAxis.get_ModifierAxisCanvas();
//            default:
//                throw new InvalidOperationException( $"Cannot get an annotation surface for AnnotationCanvas.{annotationCanvas}" );
//        }
//    }

//    protected virtual \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
//    {
//        return XAxis != null && XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D< AnnotationBase > ( this ) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new \u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<AnnotationBase>( this );
//    }

//    protected static void OnRenderablePropertyChanged(
//      DependencyObject d,
//      DependencyPropertyChangedEventArgs e )
//    {
//        ( ( AnnotationBase ) d ).Refresh();
//    }

//    protected static void OnAnnotationPositionChanged(
//      DependencyObject d,
//      DependencyPropertyChangedEventArgs e )
//    {
//        ( ( AnnotationBase ) d ).Refresh();
//        ( ( ApiElementBase ) d ).OnPropertyChanged( "PositionChanged" );
//    }

//    public void UpdatePosition( Point point1, Point point2 )
//    {
//        using ( SuspendUpdates() )
//        {
//            IComparable[] comparableArray1 = FromCoordinates(point1);
//            SetCurrentValue( X1Property, ( object ) comparableArray1[ 0 ] );
//            SetCurrentValue( Y1Property, ( object ) comparableArray1[ 1 ] );
//            if ( this is IAnchorPointAnnotation )
//                return;
//            IComparable[] comparableArray2 = FromCoordinates(point2);
//            SetCurrentValue( X2Property, ( object ) comparableArray2[ 0 ] );
//            SetCurrentValue( Y2Property, ( object ) comparableArray2[ 1 ] );
//        }
//    }

//    protected virtual IComparable[ ] FromCoordinates( Point coords )
//    {
//        return FromCoordinates( coords.X, coords.Y );
//    }

//    protected virtual IComparable[ ] FromCoordinates( double xCoord, double yCoord )
//    {
//        IAxis xaxis = XAxis;
//        IAxis yaxis = YAxis;
//        bool isHorizontalAxis = xaxis.IsHorizontalAxis;
//        return new IComparable[ 2 ]
//        {
//      isHorizontalAxis ? FromCoordinate(xCoord, xaxis) : FromCoordinate(yCoord, xaxis),
//      isHorizontalAxis ? FromCoordinate(yCoord, yaxis) : FromCoordinate(xCoord, yaxis)
//        };
//    }

//    protected virtual IComparable FromCoordinate(
//      double coord,
//      IAxis axis )
//    {
//        if ( axis == null )
//            throw new ArgumentNullException( nameof( axis ) );
//        XyDirection ks34Z259A4NengcEjd = axis.IsHorizontalAxis ? XyDirection.XDirection : XyDirection.YDirection;
//        return CoordinateMode == AnnotationCoordinateMode.Relative || CoordinateMode == AnnotationCoordinateMode.RelativeX && ks34Z259A4NengcEjd == XyDirection.XDirection || CoordinateMode == AnnotationCoordinateMode.RelativeY && ks34Z259A4NengcEjd == XyDirection.YDirection ? FromRelativeCoordinate( coord, axis ) : ( !( axis.GetCurrentCoordinateCalculator() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0) ? axis.GetDataValue( coord ) : ( IComparable ) ( int ) q9i0MXI7Qb9c1V6c0.GetDataValue( coord ));
//    }

//    protected virtual IComparable FromRelativeCoordinate(
//      double coord,
//      IAxis axis )
//    {
//        IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
//        double num = axis.IsHorizontalAxis ? canvas.ActualWidth : canvas.ActualHeight;
//        return ( IComparable ) ( coord / num );
//    }

//    protected double ToCoordinate(
//      IComparable dataValue,
//      IAxis axis )
//    {
//        IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
//        ICoordinateCalculator<double> coordCalc = axis.GetCurrentCoordinateCalculator();
//        XyDirection direction = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? XyDirection.XDirection : XyDirection.YDirection;
//        double canvasMeasurement = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? canvas.ActualWidth : canvas.ActualHeight;
//        return ToCoordinate( dataValue, canvasMeasurement, coordCalc, direction );
//    }



//    private double GetCoordinate(
//      IComparable dataValue,
//      IAnnotationCanvas canvas,
//      ICoordinateCalculator<double> coordCalc )
//    {
//        if ( coordCalc == null )
//            return 0.0;
//        XyDirection direction = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? XyDirection.XDirection : XyDirection.YDirection;
//        double canvasMeasurement = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? canvas.ActualWidth : canvas.ActualHeight;
//        return ToCoordinate( dataValue, canvasMeasurement, coordCalc, direction );
//    }

//    protected virtual double ToCoordinate(
//      IComparable dataValue,
//      double canvasMeasurement,
//      ICoordinateCalculator<double> coordCalc,
//      XyDirection direction )
//    {
//        if ( dataValue == null )
//            return double.NaN;
//        if ( CoordinateMode == AnnotationCoordinateMode.Relative || CoordinateMode == AnnotationCoordinateMode.RelativeX && direction == XyDirection.XDirection || CoordinateMode == AnnotationCoordinateMode.RelativeY && direction == XyDirection.YDirection )
//            return dataValue.ToDouble() * canvasMeasurement;
//        return coordCalc.\u0023\u003DzcNWwm_gWa4NJdtQNJ1Cl\u0024zStdK0t() && dataValue is DateTime ? GetCategoryCoordinate( dataValue, coordCalc as \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D) : coordCalc.\u0023\u003DzhL6gsJw\u003D( dataValue.ToDouble() );
//    }

//    private double GetCategoryCoordinate(
//      IComparable dataValue,
//      \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D categoryCalc)
//    {
//        int num1 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e( ( DateTime ) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
//        if ( num1 != -1 )
//            return categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num1);
//    int num2 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2);
//    int num3 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
//    DateTime dateTime = categoryCalc.\u0023\u003DzWZQlXHuDrnKc(num2);
//    double num4 = categoryCalc.\u0023\u003DzWZQlXHuDrnKc(num3).ToDouble() - dateTime.ToDouble();
//    double num5 = (dataValue.ToDouble() - dateTime.ToDouble()) / num4;
//    double val1 = categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num2);
//    double num6 = categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num3) - Math.Max(val1, 0.0);
//    return num6 <= 0.0 ? -1.0 : val1 + num6 * num5;
//  }



//  public void MoveAnnotation(double horizOffset, double vertOffset)
//  {
//    if (!IsEditable)
//      return;
//    IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
//    if (XAxis == null || YAxis == null)
//      return;
//    ICoordinateCalculator<double> yCalc = YAxis.GetCurrentCoordinateCalculator();
//    ICoordinateCalculator<double> xCalc = XAxis.GetCurrentCoordinateCalculator();
//    using (SuspendUpdates())
//      MoveAnnotationTo(GetCoordinates(canvas, xCalc, yCalc), horizOffset, vertOffset);
//  }

//  protected virtual (double fixedHOffset, double fixedVOffset) MoveAnnotationTo(
//    AnnotationCoordinates coordinates,
//    double horizOffset,
//    double vertOffset)
//  {
//    IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
//    GetCurrentPlacementStrategy().\u0023\u003DzuPL3ELSPZybJ(coordinates, horizOffset, vertOffset, canvas);
//    return (horizOffset, vertOffset);
//  }

//  protected bool IsCoordinateValid(double coord, double canvasMeasurement)
//  {
//    return coord >= 0.0 && coord < canvasMeasurement;
//  }

//  public Point[] GetBasePoints()
//  {
//    return GetBasePoints(GetCoordinates(GetCanvas(AnnotationCanvas), XAxis != null ? XAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null, YAxis != null ? YAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null));
//  }

//  protected virtual Point[] GetBasePoints(
//    AnnotationCoordinates coordinates)
//  {
//    return GetCurrentPlacementStrategy().\u0023\u003DzfJgp916l7LbX(coordinates);
//  }

//  public void SetBasePoint(Point newPoint, int index)
//  {
//    if (!IsEditable)
//      return;
//    using (SuspendUpdates())
//      GetCurrentPlacementStrategy().\u0023\u003DzzNonn\u0024lG8ddm(newPoint, index);
//  }

//  protected virtual void SetBasePoint(
//    Point newPoint,
//    int index,
//    IAxis xAxis,
//    IAxis yAxis)
//  {
//    IComparable[] comparableArray = FromCoordinates(newPoint);
//    DependencyProperty x;
//    DependencyProperty y;
//    GetPropertiesFromIndex(index, out x, out y);
//    SetCurrentValue(x, (object) comparableArray[0]);
//    SetCurrentValue(y, (object) comparableArray[1]);
//  }

//  protected virtual void GetPropertiesFromIndex(
//    int index,
//    out DependencyProperty x,
//    out DependencyProperty y)
//  {
//    x = X1Property;
//    y = Y1Property;
//    switch (index)
//    {
//      case 0:
//        x = X1Property;
//        y = Y1Property;
//        break;
//      case 1:
//        x = X2Property;
//        y = Y1Property;
//        break;
//      case 2:
//        x = X2Property;
//        y = Y2Property;
//        break;
//      case 3:
//        x = X1Property;
//        y = Y2Property;
//        break;
//    }
//  }

//  protected virtual void HandleIsEditable()
//  {
//    Cursor cursor = IsEditable ? GetSelectedCursor() : Cursors.Arrow;
//    SetCurrentValue(FrameworkElement.CursorProperty, (object) cursor);
//    PerformFocusOnInputTextArea();
//  }

//  protected Canvas GetAdornerLayer()
//  {
//    return ParentSurface == null ? (Canvas) null : ParentSurface.\u0023\u003DzjEjGZ817bm4EOO82ig\u003D\u003D();
//  }

//  protected virtual void AddAdorners(Canvas adornerLayer)
//  {
//    \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C nqo7rKyswR861f1C = new \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C((IAnnotation) this);
//    nqo7rKyswR861f1C.\u0023\u003Dzbw4WNWtere7d(adornerLayer);
//    _myAdorners.Add((IAnnotationAdorner) nqo7rKyswR861f1C);
//  }

//  protected virtual void RemoveAdorners(Canvas adornerLayer)
//  {
//    GetUsedAdorners<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(adornerLayer).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(SomeClass34343383.\u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D ?? (SomeClass34343383.\u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D = new Action<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(SomeClass34343383.SomeMethond0343.\u0023\u003DzmwmE1Rru1D_mRmp3V7zIKTM\u003D)));
//  }

//  public virtual Point TranslatePoint(
//    Point point,
//    IHitTestable relativeTo)
//  {
//    return \u0023\u003DzaPPLsvfM_Sst(point, relativeTo);
//  }

//  public virtual bool IsPointWithinBounds(Point point) => \u0023\u003DzbOxVzAyGdX66(point);

//  public virtual Rect GetBoundsRelativeTo(
//    IHitTestable relativeTo)
//  {
//    return \u0023\u003DzdC9whUui_gN\u0024(relativeTo);
//  }

//  internal bool TrySelectAnnotation()
//  {
//    return ParentSurface != null && ParentSurface.get_Annotations().\u0023\u003DzaO_rUKeW5Orq((IAnnotation) this);
//  }

//  private void OnSelected()
//  {
//    EventHandler selected = Selected;
//    if (selected == null)
//      return;
//    selected((object) this, EventArgs.Empty);
//  }

//  private void OnUnselected()
//  {
//    EventHandler unselected = Unselected;
//    if (unselected == null)
//      return;
//    unselected((object) this, EventArgs.Empty);
//  }

//  private void OnIsHiddenChanged()
//  {
//    EventHandler isHiddenChanged = IsHiddenChanged;
//    if (isHiddenChanged == null)
//      return;
//    isHiddenChanged((object) this, EventArgs.Empty);
//  }

//  internal void RaiseAnnotationDragStarted(bool isPrimaryDrag, bool isResize)
//  {
//    EventHandler<EventArgs> dragStarted = DragStarted;
//    if (dragStarted == null)
//      return;
//    dragStarted((object) this, new EventArgs(isPrimaryDrag, isResize));
//  }

//  internal void RaiseAnnotationDragEnded(bool isPrimaryDrag, bool isResize)
//  {
//    EventHandler<EventArgs> dragEnded = DragEnded;
//    if (dragEnded == null)
//      return;
//    dragEnded((object) this, new EventArgs(isPrimaryDrag, isResize));
//  }

//  internal void RaiseAnnotationDragging(
//    double hOffset,
//    double vOffset,
//    bool isPrimaryDrag,
//    bool isResize)
//  {
//    EventHandler<AnnotationDragDeltaEventArgs> dragDelta = DragDelta;
//    if (dragDelta == null)
//      return;
//    dragDelta((object) this, new AnnotationDragDeltaEventArgs(isPrimaryDrag, isResize, hOffset, vOffset));
//  }

//  private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//  {
//    if (!(d is AnnotationBase annotationBase))
//      return;
//    Canvas adornerLayer = annotationBase.GetAdornerLayer();
//    if ((bool) e.NewValue)
//    {
//      annotationBase.AddAdorners(adornerLayer);
//      annotationBase.OnSelected();
//    }
//    else
//    {
//      annotationBase.ReleaseMouseCapture();
//      annotationBase.RemoveAdorners(adornerLayer);
//      annotationBase.PerformFocusOnInputTextArea();
//      annotationBase.OnUnselected();
//    }
//  }

//  private static void OnIsEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//  {
//    if (!(d is AnnotationBase annotationBase))
//      return;
//    annotationBase.HandleIsEditable();
//  }

//  private static void OnIsHiddenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//  {
//    if (!(d is AnnotationBase annotationBase) || !annotationBase.IsAttached)
//      return;
//    if ((bool) e.NewValue)
//    {
//      annotationBase.MakeInvisible();
//    }
//    else
//    {
//      ICoordinateCalculator<double> xCalc = annotationBase.XAxis != null ? annotationBase.XAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
//      ICoordinateCalculator<double> yCalc = annotationBase.YAxis != null ? annotationBase.YAxis.GetCurrentCoordinateCalculator() : (ICoordinateCalculator<double>) null;
//      IAnnotationCanvas canvas = annotationBase.GetCanvas(annotationBase.AnnotationCanvas);
//      AnnotationCoordinates coordinates = new AnnotationCoordinates();
//      if (xCalc != null && yCalc != null && canvas != null)
//        coordinates = annotationBase.GetCoordinates(canvas, xCalc, yCalc);
//      annotationBase.MakeVisible(coordinates);
//      annotationBase.Refresh();
//    }
//    annotationBase.OnIsHiddenChanged();
//  }

//  private static void OnYAxisIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//  {
//    if (!(d is AnnotationBase annotationBase))
//      return;
//    annotationBase._yAxis = annotationBase.GetYAxis(annotationBase.YAxisId);
//    annotationBase.OnYAxisIdChanged();
//    ((AnnotationBase) d).Refresh();
//  }

//  private static void OnXAxisIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//  {
//    if (!(d is AnnotationBase annotationBase))
//      return;
//    annotationBase._xAxis = annotationBase.GetXAxis(annotationBase.XAxisId);
//    annotationBase.OnXAxisIdChanged();
//    ((AnnotationBase) d).Refresh();
//  }

//  public bool IsSuspended
//  {
//    get
//    {
//      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this);
//    }
//  }

//  public IUpdateSuspender SuspendUpdates()
//  {
//    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this);
//  }

//  public void ResumeUpdates(
//    IUpdateSuspender updateSuspender)
//  {
//    if (!updateSuspender.ResumeTargetOnDispose)
//      return;
//    Refresh();
//  }

//  public void DecrementSuspend()
//  {
//  }

//  public XmlSchema GetSchema() => (XmlSchema) null;

//  public virtual void ReadXml(XmlReader reader)
//  {
//    if (reader.MoveToContent() != XmlNodeType.Element || !(reader.LocalName == ((object) this).GetType().Name))
//      return;
//    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D((IAnnotation) this, reader);
//  }

//  public virtual void WriteXml(XmlWriter writer)
//  {
//    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D((IAnnotation) this, writer);
//  }

//  internal FrameworkElement RootElement => AnnotationRoot;

//  internal bool IsDragging => _isDragging;

//  internal bool IsDraggingByUser => _isPrimaryDrag;

//  object IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eget_DataContext()
//  {
//    return DataContext;
//  }

//  void IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eset_DataContext(
//    object value)
//  {
//    DataContext = value;
//  }

//  double IHitTestable.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualWidth()
//  {
//    return ActualWidth;
//  }

//  double IHitTestable.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualHeight()
//  {
//    return ActualHeight;
//  }

//  [Serializable]
//  private new sealed class SomeClass34343383
//  {
//    public static readonly SomeClass34343383 SomeMethond0343 = new SomeClass34343383();
//    public static Action<IAnnotationAdorner> \u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D;
//    public static Action<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D> \u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D;

//    internal void \u0023\u003DzjRwQMrvkACc83ywGyp_nJAU\u003D(
//      IAnnotationAdorner _param1)
//    {
//      _param1.\u0023\u003DzGDdLHa8\u003D();
//    }

//    internal void \u0023\u003DzmwmE1Rru1D_mRmp3V7zIKTM\u003D(
//      \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D _param1)
//    {
//      _param1.\u0023\u003DzcNW2KR8\u003D();
//    }
//  }

//  private sealed class \u0023\u003DzPDNKpHuRG7yCW5JW8_EOM4E\u003D
//  {
//    public AnnotationBase _variableSome3535;
//    public object \u0023\u003DzwM8aRUE\u003D;
//    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

//    internal void \u0023\u003DzIOaiByPzcFm0hgjIqRsFUKJzOnbT2YChDvFyp8X_Z2t0giSUkPjoyzwmi0T_()
//    {
//      // ISSUE: explicit non-virtual call
//      // ISSUE: explicit non-virtual call
//      _variableSome3535._yAxis = __nonvirtual (_variableSome3535.GetYAxis(__nonvirtual (_variableSome3535.YAxisId)));
//      _variableSome3535.OnYAxesCollectionChanged(\u0023\u003DzwM8aRUE\u003D, \u0023\u003DzTi2kmf4\u003D);
//    }
//  }

//  public class \u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<T>(
//    T _param1) : 
//    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P8\u00249VOyCNzxoI\u0024gA\u002447LO3X8<T>(_param1)
//    where T : AnnotationBase
//  {
//    public override void \u0023\u003DzNUoYFVRHgzxB(
//      AnnotationCoordinates _param1)
//    {
//    }

//    public override Point[] \u0023\u003DzfJgp916l7LbX(
//      AnnotationCoordinates _param1)
//    {
//      return (Point[]) null;
//    }

//    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
//    {
//      \u0023\u003Dz_iIh83yfe01U().SetBasePoint(_param1, _param2, \u0023\u003Dz_iIh83yfe01U().XAxis, \u0023\u003Dz_iIh83yfe01U().YAxis);
//    }

//    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
//      AnnotationCoordinates _param1,
//      IAnnotationCanvas _param2)
//    {
//      return (_param1.X1Coord < 0.0 && _param1.X2Coord < 0.0 || _param1.X1Coord > _param2.ActualWidth && _param1.X2Coord > _param2.ActualWidth || _param1.Y1Coord< 0.0 && _param1.Y2Coord < 0.0 ? 1 : (_param1.Y1Coord<= _param2.ActualHeight ? 0 : (_param1.Y2Coord > _param2.ActualHeight ? 1 : 0))) == 0;
//    }

//    public override void \u0023\u003DzuPL3ELSPZybJ(
//      AnnotationCoordinates _param1,
//      double _param2,
//      double _param3,
//      IAnnotationCanvas _param4)
//    {
//      \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(_param1, ref _param2, ref _param3, _param4);
//    }

//    protected virtual void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
//      AnnotationCoordinates _param1,
//      ref double _param2,
//      ref double _param3,
//      IAnnotationCanvas _param4)
//    {
//      double d1 = _param1.X1Coord + _param2;
//      double d2 = _param1.X2Coord + _param2;
//      double d3 = _param1.Y1Coord+ _param3;
//      double d4 = _param1.Y2Coord + _param3;
//      if (!\u0023\u003DzpTsgWlwWfZwP(d1, _param4.ActualWidth) || !\u0023\u003DzpTsgWlwWfZwP(d3, _param4.ActualHeight) || !\u0023\u003DzpTsgWlwWfZwP(d2, _param4.ActualWidth) || !\u0023\u003DzpTsgWlwWfZwP(d4, _param4.ActualHeight))
//      {
//        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
//        double val2_1 = double.IsNaN(d2) ? 0.0 : d2;
//        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
//        double val2_2 = double.IsNaN(d4) ? 0.0 : d4;
//        if (Math.Max(val1_1, val2_1) < 0.0)
//          _param2 -= Math.Max(val1_1, val2_1);
//        if (Math.Min(val1_1, val2_1) > _param4.ActualWidth)
//          _param2 -= Math.Min(val1_1, val2_1) - (_param4.ActualWidth - 1.0);
//        if (Math.Max(val1_2, val2_2) < 0.0)
//          _param3 -= Math.Max(val1_2, val2_2);
//        if (Math.Min(val1_2, val2_2) > _param4.ActualHeight)
//          _param3 -= Math.Min(val1_2, val2_2) - (_param4.ActualHeight - 1.0);
//      }
//      _param1.X1Coord += _param2;
//      _param1.X2Coord += _param2;
//      _param1.Y1Coord+= _param3;
//      _param1.Y2Coord += _param3;
//      \u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.X1Coord, _param1.\u0023\u003Dz2J4l3QUGwZHE), 0, \u0023\u003Dz_iIh83yfe01U().XAxis, \u0023\u003Dz_iIh83yfe01U().YAxis);
//      \u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.X2Coord, _param1.Y2Coord), 2, \u0023\u003Dz_iIh83yfe01U().XAxis, \u0023\u003Dz_iIh83yfe01U().YAxis);
//    }

//    protected bool \u0023\u003DzpTsgWlwWfZwP(double _param1, double _param2)
//    {
//      return \u0023\u003Dz_iIh83yfe01U().IsCoordinateValid(_param1, _param2);
//    }

//    protected IComparable[] \u0023\u003DzvQ1aszE\u003D(double _param1, double _param2)
//    {
//      return \u0023\u003Dz_iIh83yfe01U().FromCoordinates(_param1, _param2);
//    }
//  }

//  private sealed class \u0023\u003Dzffg\u0024YXBnGm7H\u0024PCqlQv7PCc\u003D
//  {
//    public AnnotationBase _variableSome3535;
//    public object \u0023\u003DzwM8aRUE\u003D;
//    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

//    internal void \u0023\u003Dz51OAXpgKHWL1SXLaCA5i5tpZSV6aNEG7pUicQk0yky3I6F_EHJGXbx7TM94H()
//    {
//      // ISSUE: explicit non-virtual call
//      _variableSome3535._xAxis = _variableSome3535.GetXAxis(__nonvirtual (_variableSome3535.XAxisId));
//      _variableSome3535.OnXAxesCollectionChanged(\u0023\u003DzwM8aRUE\u003D, \u0023\u003DzTi2kmf4\u003D);
//    }
//  }

//  internal class \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<T> : 
//    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P8\u00249VOyCNzxoI\u0024gA\u002447LO3X8<T>
//    where T : AnnotationBase
//  {
//    private readonly \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;

//    public \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D(T _param1)
//      : base(_param1)
//    {
//      \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J = _param1.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
//    }

//    protected \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dzy9phceyLTfoo()
//    {
//      return \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;
//    }

//    public override void \u0023\u003DzNUoYFVRHgzxB(
//      AnnotationCoordinates _param1)
//    {
//    }

//    public override Point[] \u0023\u003DzfJgp916l7LbX(
//      AnnotationCoordinates _param1)
//    {
//      return (Point[]) null;
//    }

//    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
//    {
//      Point newPoint = \u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
//      \u0023\u003Dz_iIh83yfe01U().SetBasePoint(newPoint, _param2, \u0023\u003Dz_iIh83yfe01U().XAxis, \u0023\u003Dz_iIh83yfe01U().YAxis);
//    }

//    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
//      AnnotationCoordinates _param1,
//      IAnnotationCanvas _param2)
//    {
//      Size size = \u0023\u003DzIr6xoc_4P2lw(_param2);
//      return \u0023\u003DzRe9EEbV7q4ey(_param1, size);
//    }

//    private Size \u0023\u003DzIr6xoc_4P2lw(
//      IAnnotationCanvas _param1)
//    {
//      return new Size(360.0, \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param1.ActualWidth, _param1.ActualHeight));
//    }

//    protected virtual bool \u0023\u003DzRe9EEbV7q4ey(
//      AnnotationCoordinates _param1,
//      Size _param2)
//    {
//      return (_param1.X1Coord < 0.0 && _param1.X2Coord < 0.0 || _param1.X1Coord > _param2.Width && _param1.X2Coord > _param2.Width || _param1.Y1Coord< 0.0 && _param1.Y2Coord < 0.0 ? 1 : (_param1.Y1Coord<= _param2.Height ? 0 : (_param1.Y2Coord > _param2.Height ? 1 : 0))) == 0;
//    }

//    public override void \u0023\u003DzuPL3ELSPZybJ(
//      AnnotationCoordinates _param1,
//      double _param2,
//      double _param3,
//      IAnnotationCanvas _param4)
//    {
//      Tuple<Point, Point> tuple = \u0023\u003DzQDA5x2uuH9m3(_param1, _param2, _param3);
//      Size size = \u0023\u003DzIr6xoc_4P2lw(_param4);
//      \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(_param1, tuple.Item1, tuple.Item2, size);
//    }

//    protected virtual Tuple<Point, Point> \u0023\u003DzQDA5x2uuH9m3(
//      AnnotationCoordinates _param1,
//      double _param2,
//      double _param3)
//    {
//      AnnotationCoordinates nnpojF4sCpkA8pp0g = \u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
//      return new Tuple<Point, Point>(\u0023\u003DztHc_NcH8us53(new Point(nnpojF4sCpkA8pp0g.X1Coord, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE), _param2, _param3), \u0023\u003DztHc_NcH8us53(new Point(nnpojF4sCpkA8pp0g.X2Coord, nnpojF4sCpkA8pp0g.Y2Coord), _param2, _param3));
//    }

//    protected virtual AnnotationCoordinates \u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(
//      AnnotationCoordinates _param1)
//    {
//      Point point1 = \u0023\u003Dzy9phceyLTfoo().\u0023\u003DzsTReN_n58EEf(new Point(_param1.X1Coord, _param1.\u0023\u003Dz2J4l3QUGwZHE));
//      Point point2 = \u0023\u003Dzy9phceyLTfoo().\u0023\u003DzsTReN_n58EEf(new Point(_param1.X2Coord, _param1.Y2Coord));
//      return new AnnotationCoordinates()
//      {
//        X1Coord = point1.X,
//        Y1Coord= point1.Y,
//        X2Coord = point2.X,
//        Y2Coord = point2.Y
//      };
//    }

//    private Point \u0023\u003DztHc_NcH8us53(Point _param1, double _param2, double _param3)
//    {
//      Point point1 = \u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
//      _param1.X += _param2;
//      _param1.Y += _param3;
//      Point point2 = \u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
//      return new Point(point2.X - point1.X, point2.Y - point1.Y);
//    }

//    protected virtual void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
//      AnnotationCoordinates _param1,
//      Point _param2,
//      Point _param3,
//      Size _param4)
//    {
//      double d1 = _param1.X1Coord + _param2.X;
//      double d2 = _param1.X2Coord + _param3.X;
//      double d3 = _param1.Y1Coord+ _param2.Y;
//      double d4 = _param1.Y2Coord + _param3.Y;
//      if (!\u0023\u003DzpTsgWlwWfZwP(d1, _param4.Width) || !\u0023\u003DzpTsgWlwWfZwP(d3, _param4.Height) || !\u0023\u003DzpTsgWlwWfZwP(d2, _param4.Width) || !\u0023\u003DzpTsgWlwWfZwP(d4, _param4.Height))
//      {
//        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
//        double val2_1 = double.IsNaN(d2) ? 0.0 : d2;
//        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
//        double val2_2 = double.IsNaN(d4) ? 0.0 : d4;
//        if (Math.Max(val1_1, val2_1) < 0.0)
//        {
//          double num = Math.Max(val1_1, val2_1);
//          _param2.X -= num;
//          _param3.X -= num;
//        }
//        if (Math.Min(val1_1, val2_1) > _param4.Width)
//        {
//          double num = Math.Min(val1_1, val2_1) - (_param4.Width - 1.0);
//          _param2.X -= num;
//          _param3.X -= num;
//        }
//        if (Math.Max(val1_2, val2_2) < 0.0)
//        {
//          double num = Math.Max(val1_2, val2_2);
//          _param2.Y -= num;
//          _param3.Y -= num;
//        }
//        if (Math.Min(val1_2, val2_2) > _param4.Height)
//        {
//          double num = Math.Min(val1_2, val2_2) - (_param4.Height - 1.0);
//          _param2.Y -= num;
//          _param3.Y -= num;
//        }
//      }
//      _param1.X1Coord += _param2.X;
//      _param1.X2Coord += _param3.X;
//      _param1.Y1Coord+= _param2.Y;
//      _param1.Y2Coord += _param3.Y;
//      \u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.X1Coord, _param1.\u0023\u003Dz2J4l3QUGwZHE), 0, \u0023\u003Dz_iIh83yfe01U().XAxis, \u0023\u003Dz_iIh83yfe01U().YAxis);
//      \u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.X2Coord, _param1.Y2Coord), 2, \u0023\u003Dz_iIh83yfe01U().XAxis, \u0023\u003Dz_iIh83yfe01U().YAxis);
//    }

//    protected bool \u0023\u003DzpTsgWlwWfZwP(double _param1, double _param2)
//    {
//      return \u0023\u003Dz_iIh83yfe01U().IsCoordinateValid(_param1, _param2);
//    }

//    protected IComparable[] \u0023\u003DzvQ1aszE\u003D(double _param1, double _param2)
//    {
//      return \u0023\u003Dz_iIh83yfe01U().FromCoordinates(_param1, _param2);
//    }
//  }
//}
