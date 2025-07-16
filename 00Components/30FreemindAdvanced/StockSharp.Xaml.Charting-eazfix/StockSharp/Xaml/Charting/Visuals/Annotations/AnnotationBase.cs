// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

public abstract class AnnotationBase :
  ApiElementBase,
  IXmlSerializable,
  ISuspendable,
  IPublishMouseEvents,
  IAnnotation,
  IHitTestable
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
    private IList<IAnnotationAdorner> _myAdorners = (IList<IAnnotationAdorner>) new List<IAnnotationAdorner>();
    private IAxis _yAxis;
    private IAxis _xAxis;
    private bool _isLoaded;

    protected IAnnotationCanvas GetCanvas(
    AnnotationCanvas annotationCanvas )
    {
        if ( this.ParentSurface == null )
            return (IAnnotationCanvas) null;
        switch ( annotationCanvas )
        {
            case AnnotationCanvas.AboveChart:
                return this.ParentSurface.\u0023\u003DzFPPJbPlQRagwT6aZuQ\u003D\u003D();
            case AnnotationCanvas.BelowChart:
                return this.ParentSurface.\u0023\u003Dz7EP15yq7Yz\u0024jLVX6GgE8gjs\u003D();
            case AnnotationCanvas.YAxis:
                return this.YAxis == null ? (IAnnotationCanvas) null : this.YAxis.ModifierAxisCanvas;
            case AnnotationCanvas.XAxis:
                return this.XAxis == null ? (IAnnotationCanvas) null : this.XAxis.ModifierAxisCanvas;
            default:
                throw new InvalidOperationException( $"Cannot get an annotation surface for AnnotationCanvas.{annotationCanvas}" );
        }
    }

    protected AnnotationBase()
    {
        this.DefaultStyleKey = ( object ) typeof( AnnotationBase );
        this.IsResizable = true;
    }

    protected Point DragStartPoint => this._startPoint;

    event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.StockSharp.Xaml.Charting.Utility.Mouse.IPublishMouseEvents.TouchDown
    {
        add => throw new NotImplementedException();
        remove => throw new NotImplementedException();
    }

    event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchMove
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchUp
  {
    add => throw new NotImplementedException();
remove => throw new NotImplementedException();
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
    MouseButtonEventHandler mouseMiddleButtonUp = this.MouseMiddleButtonUp;
    if ( mouseMiddleButtonUp == null )
        return;
    mouseMiddleButtonUp( sender, e );
}

private void PreviewMouseDownHandler( object sender, MouseButtonEventArgs e )
{
    if ( e.ChangedButton != MouseButton.Middle )
        return;
    MouseButtonEventHandler middleButtonDown = this.MouseMiddleButtonDown;
    if ( middleButtonDown == null )
        return;
    middleButtonDown( sender, e );
}

public Style ResizingGripsStyle
{
    get => ( Style ) this.GetValue( AnnotationBase.ResizingGripsStyleProperty );
    set => this.SetValue( AnnotationBase.ResizingGripsStyleProperty, ( object ) value );
}

public bool CanEditText
{
    get => ( bool ) this.GetValue( AnnotationBase.CanEditTextProperty );
    set => this.SetValue( AnnotationBase.CanEditTextProperty, ( object ) value );
}

public bool IsResizable
{
    get => this._isResizable;
    protected set
    {
        this._isResizable = value;
        this.OnPropertyChanged( nameof( IsResizable ) );
    }
}

public string XAxisId
{
    get => ( string ) this.GetValue( AnnotationBase.XAxisIdProperty );
    set => this.SetValue( AnnotationBase.XAxisIdProperty, ( object ) value );
}

public string YAxisId
{
    get => ( string ) this.GetValue( AnnotationBase.YAxisIdProperty );
    set => this.SetValue( AnnotationBase.YAxisIdProperty, ( object ) value );
}

public XyDirection DragDirections
{
    get
    {
        return ( XyDirection ) this.GetValue( AnnotationBase.DragDirectionsProperty );
    }
    set => this.SetValue( AnnotationBase.DragDirectionsProperty, ( object ) value );
}

public XyDirection ResizeDirections
{
    get
    {
        return ( XyDirection ) this.GetValue( AnnotationBase.ResizeDirectionsProperty );
    }
    set => this.SetValue( AnnotationBase.ResizeDirectionsProperty, ( object ) value );
}

public AnnotationCoordinateMode CoordinateMode
{
    get => ( AnnotationCoordinateMode ) this.GetValue( AnnotationBase.CoordinateModeProperty );
    set => this.SetValue( AnnotationBase.CoordinateModeProperty, ( object ) value );
}

public AnnotationCanvas AnnotationCanvas
{
    get
    {
        return ( AnnotationCanvas ) this.GetValue( AnnotationBase.AnnotationCanvasProperty );
    }
    set => this.SetValue( AnnotationBase.AnnotationCanvasProperty, ( object ) value );
}

public bool IsSelected
{
    get => ( bool ) this.GetValue( AnnotationBase.IsSelectedProperty );
    set => this.SetValue( AnnotationBase.IsSelectedProperty, ( object ) value );
}

public bool IsEditable
{
    get => ( bool ) this.GetValue( AnnotationBase.IsEditableProperty );
    set => this.SetValue( AnnotationBase.IsEditableProperty, ( object ) value );
}

public bool IsHidden
{
    get => ( bool ) this.GetValue( AnnotationBase.IsHiddenProperty );
    set => this.SetValue( AnnotationBase.IsHiddenProperty, ( object ) value );
}

[TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
public IComparable X1
{
    get => ( IComparable ) this.GetValue( AnnotationBase.X1Property );
    set => this.SetValue( AnnotationBase.X1Property, ( object ) value );
}

[TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
public IComparable X2
{
    get => ( IComparable ) this.GetValue( AnnotationBase.X2Property );
    set => this.SetValue( AnnotationBase.X2Property, ( object ) value );
}

[TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
public IComparable Y1
{
    get => ( IComparable ) this.GetValue( AnnotationBase.Y1Property );
    set => this.SetValue( AnnotationBase.Y1Property, ( object ) value );
}

[TypeConverter( typeof( StringToAnnotationCoordinateConverter ) )]
public IComparable Y2
{
    get => ( IComparable ) this.GetValue( AnnotationBase.Y2Property );
    set => this.SetValue( AnnotationBase.Y2Property, ( object ) value );
}

protected abstract Cursor GetSelectedCursor();

protected virtual void OnDragStarted()
{
}

protected virtual void OnDragEnded()
{
}

protected virtual void OnDragDelta( double hOffset, double vOffset )
{
}

public void StartDrag( bool isPrimaryDrag )
{
    this._isDragging = true;
    this._isPrimaryDrag = isPrimaryDrag;
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D< double > yCalc = this.YAxis?.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D< double > xCalc = this.XAxis?.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    this._startDragAnnotationCoordinates = this.GetCoordinates( canvas, xCalc, yCalc );
    this.OnDragStarted();
    this.RaiseAnnotationDragStarted( this._isPrimaryDrag, false );
}

public void EndDrag()
{
    this.OnDragEnded();
    bool isPrimaryDrag = this._isPrimaryDrag;
    this._isDragging = this._isPrimaryDrag = false;
    this.RaiseAnnotationDragEnded( isPrimaryDrag, false );
}

public void Drag( double hOffset, double vOffset )
{
    if ( !this._isDragging )
        return;
    using ( this.SuspendUpdates() )
        (hOffset, vOffset) = this.MoveAnnotationTo( this._startDragAnnotationCoordinates, hOffset, vOffset );
    this.OnDragDelta( hOffset, vOffset );
    this.RaiseAnnotationDragging( hOffset, vOffset, this._isPrimaryDrag, false );
}

public virtual bool CanMultiSelect(
  IAnnotation[ ] annotations )
{
    return annotations.Length == 1;
}

public override bool IsAttached
{
    get => this._isAttached;
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
    get => this._yAxis ?? ( this._yAxis = this.\u0023\u003Dz4uoxB8oLWxeL(this.YAxisId));
}

public override IAxis XAxis
{
    get => this._xAxis ?? ( this._xAxis = this.\u0023\u003DzI0EiGDjWkH8S(this.XAxisId));
}

protected IAnnotationCanvas AnnotationOverlaySurface
{
    get
    {
        return this.ParentSurface == null ? (IAnnotationCanvas) null : this.ParentSurface.\u0023\u003DzFPPJbPlQRagwT6aZuQ\u003D\u003D();
    }
}

protected IAnnotationCanvas AnnotationUnderlaySurface
{
    get
    {
        return this.ParentSurface == null ? (IAnnotationCanvas) null : this.ParentSurface.\u0023\u003Dz7EP15yq7Yz\u0024jLVX6GgE8gjs\u003D();
    }
}

void IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002EOnXAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
    this.\u0023\u003DzHGGuFpQ\u003D((DispatcherPriority) 8, new Action( new AnnotationBase.\u0023\u003Dzffg\u0024YXBnGm7H\u0024PCqlQv7PCc\u003D()
    {
      _variableSome3535 = this,
      \u0023\u003DzwM8aRUE\u003D = sender,
      \u0023\u003DzTi2kmf4\u003D = args
    }.\u0023\u003Dz51OAXpgKHWL1SXLaCA5i5tpZSV6aNEG7pUicQk0yky3I6F_EHJGXbx7TM94H));
  }

  void IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002EOnYAxesCollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs args)
  {
    this.\u0023\u003DzHGGuFpQ\u003D((DispatcherPriority) 8, new Action( new AnnotationBase.\u0023\u003DzPDNKpHuRG7yCW5JW8_EOM4E\u003D()
    {
      _variableSome3535 = this,
      \u0023\u003DzwM8aRUE\u003D = sender,
      \u0023\u003DzTi2kmf4\u003D = args
    }.\u0023\u003DzIOaiByPzcFm0hgjIqRsFUKJzOnbT2YChDvFyp8X_Z2t0giSUkPjoyzwmi0T_));
  }

  private void OnAxisAlignmentChanged(
    object sender,
    \u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D e)
{
    if ( !( e.\u0023\u003DzAz1mGnFFNNic() == this.XAxisId) && !( e.\u0023\u003DzAz1mGnFFNNic() == this.YAxisId))
      return;
    this.OnAxisAlignmentChanged( e.\u0023\u003DzAz1mGnFFNNic() == this.XAxisId ? this.XAxis : this.YAxis, e.\u0023\u003Dz4LPnYX_iGIbT() );
}

protected virtual void OnAxisAlignmentChanged(
  IAxis axis,
  AxisAlignment oldAlignment )
{
}

protected virtual void OnXAxesCollectionChanged(
  object sender,
  NotifyCollectionChangedEventArgs args )
{
}

protected virtual void OnYAxesCollectionChanged(
  object sender,
  NotifyCollectionChangedEventArgs args )
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
    this.ParentSurface.\u0023\u003DzexJGsaAi6rVI( new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>( this.OnAxisAlignmentChanged ) );
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
    if ( e.ChangedButton != MouseButton.Left )
        return;
    e.Handled = this.TrySelectAnnotation();
    if ( !this.IsSelected || !this.IsEditable )
        return;
    this._startPoint = Mouse.GetPosition( ( IInputElement ) ( this.\u0023\u003Dzwc4Gzka23TGB() as UIElement ));
    this._isMouseLeftDown = true;
    this._mouseLeftDownTimestamp = DateTime.UtcNow;
    this.CaptureMouse();
    e.Handled = true;
}

protected virtual void OnAnnotationMouseUp( object sender, MouseButtonEventArgs e )
{
    if ( e.ChangedButton != MouseButton.Left )
        return;
    this.ReleaseMouseCapture();
    this._isMouseLeftDown = false;
    if ( this._isDragging )
        this.EndDrag();
    else
        this.PerformFocusOnInputTextArea();
}

protected virtual void OnAnnotationMouseMove( object sender, MouseEventArgs e )
{
    if ( !this._isMouseLeftDown || DateTime.UtcNow - this._mouseLeftDownTimestamp < TimeSpan.FromMilliseconds( 2.0 ) || e.LeftButton != MouseButtonState.Pressed )
        return;
    if ( !this._isDragging )
        this.StartDrag( true );
    Point position = Mouse.GetPosition((IInputElement) (this.\u0023\u003Dzwc4Gzka23TGB() as UIElement));
    this.Drag( this.DragDirections == XyDirection.YDirection ? 0.0 : position.X - this._startPoint.X, this.DragDirections == XyDirection.XDirection ? 0.0 : position.Y - this._startPoint.Y );
}

public override void OnDetached()
{
    using ( IUpdateSuspender fq05jnDg3bOrIrgCjote = this.SuspendUpdates() )
    {
        fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose = false;
        this.IsSelected = false;
        this.MakeInvisible();
        ( this.Parent as IAnnotationCanvas).\u0023\u003DziYdJ\u00246cCiBha( ( object ) this );
        this.DetachInteractionHandlersFrom( ( FrameworkElement ) this );
        if ( this.ParentSurface != null )
            this.ParentSurface.\u0023\u003Dz38JNnebwqLph( new EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>( this.OnAxisAlignmentChanged ) );
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
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordinateCalculator = this.XAxis != null ? this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordinateCalculator = this.YAxis != null ? this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
    if (xCoordinateCalculator != null && yCoordinateCalculator != null)
      this.Update(xCoordinateCalculator, yCoordinateCalculator);
    return true;
  }

  public virtual void Update(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordinateCalculator,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordinateCalculator)
  {
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    if (canvas == null)
      return;
    canvas.\u0023\u003DzH0osWQkV_Y8_((object) this, -1);
    if (!this._isLoaded)
      return;
    AnnotationCoordinates coordinates = this.GetCoordinates(canvas, xCoordinateCalculator, yCoordinateCalculator);
    if (this.IsInBounds(coordinates, canvas))
    {
      if (!this.IsHidden)
      {
        this.MakeVisible(coordinates);
      }
      else
      {
        if (this.Visibility == Visibility.Collapsed)
          return;
        this.MakeInvisible();
      }
    }
    else
      this.MakeInvisible();
  }

  public void Hide() => this.IsHidden = true;

  public void Show() => this.IsHidden = false;

  protected virtual void MakeInvisible()
  {
    this.HideAdornerMarkers();
    this.Visibility = Visibility.Collapsed;
  }

  protected void HideAdornerMarkers()
  {
    foreach (IAnnotationAdorner adorner in (IEnumerable<IAnnotationAdorner>) this._myAdorners)
      adorner.Clear();
  }

  protected IEnumerable<T> GetUsedAdorners<T>(Canvas adornerLayer) where T : IAnnotationAdorner
  {
    return (IEnumerable<T>) adornerLayer.Children.OfType<T>().Where<T>((Func<T, bool>) (x => x.\u0023\u003Dzy2oKVLXXOFmI() == this)).ToList<T>();
  }

  protected virtual void MakeVisible(
    AnnotationCoordinates coordinates)
  {
    this.Visibility = Visibility.Visible;
    if (this.AnnotationRoot != null)
    {
      if (!this._isLoaded || Size.op_Equality(this.AnnotationRoot.RenderSize, new Size()))
        this.AnnotationRoot.MeasureArrange();
      this.PlaceAnnotation(coordinates);
    }
    this.UpdateAdorners();
  }

  public void UpdateAdorners()
  {
    Canvas adornerLayer = this.GetAdornerLayer();
    if (adornerLayer == null)
      return;
    this.GetUsedAdorners<IAnnotationAdorner>(adornerLayer).\u0023\u003Dz30RSSSygABj_<IAnnotationAdorner>(AnnotationBase.SomeClass34343383.\u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D ?? (AnnotationBase.SomeClass34343383.\u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D = new Action<IAnnotationAdorner>(AnnotationBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzjRwQMrvkACc83ywGyp_nJAU\u003D)));
  }

  protected virtual bool IsInBounds(
    AnnotationCoordinates coordinates,
    IAnnotationCanvas canvas)
  {
    return this.GetCurrentPlacementStrategy().\u0023\u003DzxGhbraO0gg9\u0024(coordinates, canvas);
  }

  protected virtual void PlaceAnnotation(
    AnnotationCoordinates coordinates)
  {
    this.GetCurrentPlacementStrategy().\u0023\u003DzNUoYFVRHgzxB(coordinates);
  }

  

  protected virtual \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    return this.XAxis != null && this.XAxis.get_IsPolarAxis() ? (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new AnnotationBase.\u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<AnnotationBase>(this) : (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<AnnotationBase>(this);
  }

  protected static void OnRenderablePropertyChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    ((AnnotationBase) d).Refresh();
  }

  protected static void OnAnnotationPositionChanged(
    DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    ((AnnotationBase) d).Refresh();
    ((ApiElementBase) d).OnPropertyChanged("PositionChanged");
  }

  public void UpdatePosition(Point point1, Point point2)
  {
    using (this.SuspendUpdates())
    {
      IComparable[] comparableArray1 = this.FromCoordinates(point1);
      this.SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray1[0]);
      this.SetCurrentValue(AnnotationBase.Y1Property, (object) comparableArray1[1]);
      if (this is IAnchorPointAnnotation)
        return;
      IComparable[] comparableArray2 = this.FromCoordinates(point2);
      this.SetCurrentValue(AnnotationBase.X2Property, (object) comparableArray2[0]);
      this.SetCurrentValue(AnnotationBase.Y2Property, (object) comparableArray2[1]);
    }
  }

  protected virtual IComparable[] FromCoordinates(Point coords)
  {
    return this.FromCoordinates(coords.X, coords.Y);
  }

  protected virtual IComparable[] FromCoordinates(double xCoord, double yCoord)
  {
    IAxis xaxis = this.XAxis;
    IAxis yaxis = this.YAxis;
    bool isHorizontalAxis = xaxis.IsHorizontalAxis;
    return new IComparable[2]
    {
      isHorizontalAxis ? this.FromCoordinate(xCoord, xaxis) : this.FromCoordinate(yCoord, xaxis),
      isHorizontalAxis ? this.FromCoordinate(yCoord, yaxis) : this.FromCoordinate(xCoord, yaxis)
    };
  }

  protected virtual IComparable FromCoordinate(
    double coord,
    IAxis axis)
  {
    if (axis == null)
      throw new ArgumentNullException(nameof (axis));
    XyDirection ks34Z259A4NengcEjd = axis.IsHorizontalAxis ? XyDirection.XDirection : XyDirection.YDirection;
    return this.CoordinateMode == AnnotationCoordinateMode.Relative || this.CoordinateMode == AnnotationCoordinateMode.RelativeX && ks34Z259A4NengcEjd == XyDirection.XDirection || this.CoordinateMode == AnnotationCoordinateMode.RelativeY && ks34Z259A4NengcEjd == XyDirection.YDirection ? this.FromRelativeCoordinate(coord, axis) : (!(axis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0) ? axis.GetDataValue(coord) : (IComparable) (int) q9i0MXI7Qb9c1V6c0.GetDataValue(coord));
  }

  protected virtual IComparable FromRelativeCoordinate(
    double coord,
    IAxis axis)
  {
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    double num = axis.IsHorizontalAxis ? canvas.ActualWidth : canvas.ActualHeight;
    return (IComparable) (coord / num);
  }

  protected double ToCoordinate(
    IComparable dataValue,
    IAxis axis)
  {
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc = axis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    XyDirection direction = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? XyDirection.XDirection : XyDirection.YDirection;
    double canvasMeasurement = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? canvas.ActualWidth : canvas.ActualHeight;
    return this.ToCoordinate(dataValue, canvasMeasurement, coordCalc, direction);
  }

  protected virtual Point ToCoordinates(
    IComparable xDataValue,
    IComparable yDataValue,
    IAnnotationCanvas canvas,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordCalc,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordCalc)
  {
    double coordinate1 = this.GetCoordinate(xDataValue, canvas, xCoordCalc);
    double coordinate2 = this.GetCoordinate(yDataValue, canvas, yCoordCalc);
    if (xCoordCalc != null && !xCoordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K())
      NumberUtil.Swap(ref coordinate1, ref coordinate2);
    return new Point(coordinate1, coordinate2);
  }

  private double GetCoordinate(
    IComparable dataValue,
    IAnnotationCanvas canvas,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc)
  {
    if (coordCalc == null)
      return 0.0;
    XyDirection direction = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? XyDirection.XDirection : XyDirection.YDirection;
    double canvasMeasurement = coordCalc.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? canvas.ActualWidth : canvas.ActualHeight;
    return this.ToCoordinate(dataValue, canvasMeasurement, coordCalc, direction);
  }

  protected virtual double ToCoordinate(
    IComparable dataValue,
    double canvasMeasurement,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> coordCalc,
    XyDirection direction)
  {
    if (dataValue == null)
      return double.NaN;
    if (this.CoordinateMode == AnnotationCoordinateMode.Relative || this.CoordinateMode == AnnotationCoordinateMode.RelativeX && direction == XyDirection.XDirection || this.CoordinateMode == AnnotationCoordinateMode.RelativeY && direction == XyDirection.YDirection)
      return dataValue.ToDouble() * canvasMeasurement;
    return coordCalc.\u0023\u003DzcNWwm_gWa4NJdtQNJ1Cl\u0024zStdK0t() && dataValue is DateTime ? this.GetCategoryCoordinate(dataValue, coordCalc as \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D) : coordCalc.\u0023\u003DzhL6gsJw\u003D(dataValue.ToDouble());
  }

  private double GetCategoryCoordinate(
    IComparable dataValue,
    \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D categoryCalc)
  {
    int num1 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 0);
    if (num1 != -1)
      return categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num1);
    int num2 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2);
    int num3 = categoryCalc.\u0023\u003DzFk6sufr\u0024co4e((DateTime) dataValue, (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 3);
    DateTime dateTime = categoryCalc.\u0023\u003DzWZQlXHuDrnKc(num2);
    double num4 = categoryCalc.\u0023\u003DzWZQlXHuDrnKc(num3).ToDouble() - dateTime.ToDouble();
    double num5 = (dataValue.ToDouble() - dateTime.ToDouble()) / num4;
    double val1 = categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num2);
    double num6 = categoryCalc.\u0023\u003DzhL6gsJw\u003D((double) num3) - Math.Max(val1, 0.0);
    return num6 <= 0.0 ? -1.0 : val1 + num6 * num5;
  }

  protected AnnotationCoordinates GetCoordinates(
    IAnnotationCanvas canvas,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc)
  {
    Point coordinates1 = this.ToCoordinates(this.X1, this.Y1, canvas, xCalc, yCalc);
    Point coordinates2 = this.ToCoordinates(this.X2, this.Y2, canvas, xCalc, yCalc);
    double num = 0.0;
    return new AnnotationCoordinates()
    {
      \u0023\u003DzS2_K6sVvd5IY = coordinates1.X,
      \u0023\u003Dz2J4l3QUGwZHE = coordinates1.Y,
      \u0023\u003Dz6aJoeqoqAzym = coordinates2.X,
      \u0023\u003DzWp13vlQiZCJc = coordinates2.Y,
      \u0023\u003DzjAUs6E8\u003D = this.YAxis != null ? this.YAxis.\u0023\u003Dz4wEfDhMr\u0024V6c() : num,
      \u0023\u003DzN0WZcqs\u003D = this.XAxis != null ? this.XAxis.\u0023\u003Dz4wEfDhMr\u0024V6c() : num
    };
  }

  public void MoveAnnotation(double horizOffset, double vertOffset)
  {
    if (!this.IsEditable)
      return;
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    if (this.XAxis == null || this.YAxis == null)
      return;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
    using (this.SuspendUpdates())
      this.MoveAnnotationTo(this.GetCoordinates(canvas, xCalc, yCalc), horizOffset, vertOffset);
  }

  protected virtual (double fixedHOffset, double fixedVOffset) MoveAnnotationTo(
    AnnotationCoordinates coordinates,
    double horizOffset,
    double vertOffset)
  {
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    this.GetCurrentPlacementStrategy().\u0023\u003DzuPL3ELSPZybJ(coordinates, horizOffset, vertOffset, canvas);
    return (horizOffset, vertOffset);
  }

  protected bool IsCoordinateValid(double coord, double canvasMeasurement)
  {
    return coord >= 0.0 && coord < canvasMeasurement;
  }

  public Point[] GetBasePoints()
  {
    return this.GetBasePoints(this.GetCoordinates(this.GetCanvas(this.AnnotationCanvas), this.XAxis != null ? this.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null, this.YAxis != null ? this.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null));
  }

  protected virtual Point[] GetBasePoints(
    AnnotationCoordinates coordinates)
  {
    return this.GetCurrentPlacementStrategy().\u0023\u003DzfJgp916l7LbX(coordinates);
  }

  public void SetBasePoint(Point newPoint, int index)
  {
    if (!this.IsEditable)
      return;
    using (this.SuspendUpdates())
      this.GetCurrentPlacementStrategy().\u0023\u003DzzNonn\u0024lG8ddm(newPoint, index);
  }

  protected virtual void SetBasePoint(
    Point newPoint,
    int index,
    IAxis xAxis,
    IAxis yAxis)
  {
    IComparable[] comparableArray = this.FromCoordinates(newPoint);
    DependencyProperty x;
    DependencyProperty y;
    this.GetPropertiesFromIndex(index, out x, out y);
    this.SetCurrentValue(x, (object) comparableArray[0]);
    this.SetCurrentValue(y, (object) comparableArray[1]);
  }

  protected virtual void GetPropertiesFromIndex(
    int index,
    out DependencyProperty x,
    out DependencyProperty y)
  {
    x = AnnotationBase.X1Property;
    y = AnnotationBase.Y1Property;
    switch (index)
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
    this.SetCurrentValue(FrameworkElement.CursorProperty, (object) cursor);
    this.PerformFocusOnInputTextArea();
  }

  protected Canvas GetAdornerLayer()
  {
    return this.ParentSurface == null ? (Canvas) null : this.ParentSurface.\u0023\u003DzjEjGZ817bm4EOO82ig\u003D\u003D();
  }

  protected virtual void AddAdorners(Canvas adornerLayer)
  {
    \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C nqo7rKyswR861f1C = new \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C((IAnnotation) this);
    nqo7rKyswR861f1C.\u0023\u003Dzbw4WNWtere7d(adornerLayer);
    this._myAdorners.Add((IAnnotationAdorner) nqo7rKyswR861f1C);
  }

  protected virtual void RemoveAdorners(Canvas adornerLayer)
  {
    this.GetUsedAdorners<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(adornerLayer).\u0023\u003Dz30RSSSygABj_<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(AnnotationBase.SomeClass34343383.\u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D ?? (AnnotationBase.SomeClass34343383.\u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D = new Action<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D>(AnnotationBase.SomeClass34343383.SomeMethond0343.\u0023\u003DzmwmE1Rru1D_mRmp3V7zIKTM\u003D)));
  }

  public virtual Point TranslatePoint(
    Point point,
    IHitTestable relativeTo)
  {
    return this.\u0023\u003DzaPPLsvfM_Sst(point, relativeTo);
  }

  public virtual bool IsPointWithinBounds(Point point) => this.\u0023\u003DzbOxVzAyGdX66(point);

  public virtual Rect GetBoundsRelativeTo(
    IHitTestable relativeTo)
  {
    return this.\u0023\u003DzdC9whUui_gN\u0024(relativeTo);
  }

  public bool TrySelectAnnotation()
  {
    return this.ParentSurface != null && this.ParentSurface.get_Annotations().\u0023\u003DzaO_rUKeW5Orq((IAnnotation) this);
  }

  private void OnSelected()
  {
    EventHandler selected = this.Selected;
    if (selected == null)
      return;
    selected((object) this, EventArgs.Empty);
  }

  private void OnUnselected()
  {
    EventHandler unselected = this.Unselected;
    if (unselected == null)
      return;
    unselected((object) this, EventArgs.Empty);
  }

  private void OnIsHiddenChanged()
  {
    EventHandler isHiddenChanged = this.IsHiddenChanged;
    if (isHiddenChanged == null)
      return;
    isHiddenChanged((object) this, EventArgs.Empty);
  }

  public void RaiseAnnotationDragStarted(bool isPrimaryDrag, bool isResize)
  {
    EventHandler<EventArgs> dragStarted = this.DragStarted;
    if (dragStarted == null)
      return;
    dragStarted((object) this, new EventArgs(isPrimaryDrag, isResize));
  }

  public void RaiseAnnotationDragEnded(bool isPrimaryDrag, bool isResize)
  {
    EventHandler<EventArgs> dragEnded = this.DragEnded;
    if (dragEnded == null)
      return;
    dragEnded((object) this, new EventArgs(isPrimaryDrag, isResize));
  }

  public void RaiseAnnotationDragging(
    double hOffset,
    double vOffset,
    bool isPrimaryDrag,
    bool isResize)
  {
    EventHandler<AnnotationDragDeltaEventArgs> dragDelta = this.DragDelta;
    if (dragDelta == null)
      return;
    dragDelta((object) this, new AnnotationDragDeltaEventArgs(isPrimaryDrag, isResize, hOffset, vOffset));
  }

  private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    Canvas adornerLayer = annotationBase.GetAdornerLayer();
    if ((bool) e.NewValue)
    {
      annotationBase.AddAdorners(adornerLayer);
      annotationBase.OnSelected();
    }
    else
    {
      annotationBase.ReleaseMouseCapture();
      annotationBase.RemoveAdorners(adornerLayer);
      annotationBase.PerformFocusOnInputTextArea();
      annotationBase.OnUnselected();
    }
  }

  private static void OnIsEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    annotationBase.HandleIsEditable();
  }

  private static void OnIsHiddenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase) || !annotationBase.IsAttached)
      return;
    if ((bool) e.NewValue)
    {
      annotationBase.MakeInvisible();
    }
    else
    {
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = annotationBase.XAxis != null ? annotationBase.XAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
      \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = annotationBase.YAxis != null ? annotationBase.YAxis.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() : (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double>) null;
      IAnnotationCanvas canvas = annotationBase.GetCanvas(annotationBase.AnnotationCanvas);
      AnnotationCoordinates coordinates = new AnnotationCoordinates();
      if (xCalc != null && yCalc != null && canvas != null)
        coordinates = annotationBase.GetCoordinates(canvas, xCalc, yCalc);
      annotationBase.MakeVisible(coordinates);
      annotationBase.Refresh();
    }
    annotationBase.OnIsHiddenChanged();
  }

  private static void OnYAxisIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    annotationBase._yAxis = annotationBase.\u0023\u003Dz4uoxB8oLWxeL(annotationBase.YAxisId);
    annotationBase.OnYAxisIdChanged();
    ((AnnotationBase) d).Refresh();
  }

  private static void OnXAxisIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (!(d is AnnotationBase annotationBase))
      return;
    annotationBase._xAxis = annotationBase.\u0023\u003DzI0EiGDjWkH8S(annotationBase.XAxisId);
    annotationBase.OnXAxisIdChanged();
    ((AnnotationBase) d).Refresh();
  }

  public bool IsSuspended
  {
    get
    {
      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this);
    }
  }

  public IUpdateSuspender SuspendUpdates()
  {
    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this);
  }

  public void ResumeUpdates(
    IUpdateSuspender updateSuspender)
  {
    if (!updateSuspender.ResumeTargetOnDispose)
      return;
    this.Refresh();
  }

  public void DecrementSuspend()
  {
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public virtual void ReadXml(XmlReader reader)
  {
    if (reader.MoveToContent() != XmlNodeType.Element || !(reader.LocalName == ((object) this).GetType().Name))
      return;
    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D((IAnnotation) this, reader);
  }

  public virtual void WriteXml(XmlWriter writer)
  {
    \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D((IAnnotation) this, writer);
  }

  public FrameworkElement RootElement => this.AnnotationRoot;

  public bool IsDragging => this._isDragging;

  public bool IsDraggingByUser => this._isPrimaryDrag;

  object IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eget_DataContext()
  {
    return this.DataContext;
  }

  void IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eset_DataContext(
    object value)
  {
    this.DataContext = value;
  }

  double IHitTestable.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualWidth()
  {
    return this.ActualWidth;
  }

  double IHitTestable.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualHeight()
  {
    return this.ActualHeight;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly AnnotationBase.SomeClass34343383 SomeMethond0343 = new AnnotationBase.SomeClass34343383();
    public static Action<IAnnotationAdorner> \u0023\u003DzqG9qUFbTruJKYxlHUw\u003D\u003D;
    public static Action<\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D> \u0023\u003Dzv1VAZ\u0024AecqzdKka0gQ\u003D\u003D;

    public void \u0023\u003DzjRwQMrvkACc83ywGyp_nJAU\u003D(
      IAnnotationAdorner _param1)
    {
      _param1.\u0023\u003DzGDdLHa8\u003D();
    }

    public void \u0023\u003DzmwmE1Rru1D_mRmp3V7zIKTM\u003D(
      \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D _param1)
    {
      _param1.\u0023\u003DzcNW2KR8\u003D();
    }
  }

  private sealed class \u0023\u003DzPDNKpHuRG7yCW5JW8_EOM4E\u003D
  {
    public AnnotationBase _variableSome3535;
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    public void \u0023\u003DzIOaiByPzcFm0hgjIqRsFUKJzOnbT2YChDvFyp8X_Z2t0giSUkPjoyzwmi0T_()
    {
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      this._variableSome3535._yAxis = __nonvirtual (this._variableSome3535.\u0023\u003Dz4uoxB8oLWxeL(__nonvirtual (this._variableSome3535.YAxisId)));
      this._variableSome3535.OnYAxesCollectionChanged(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  public class \u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<T>(
    T _param1) : 
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P8\u00249VOyCNzxoI\u0024gA\u002447LO3X8<T>(_param1)
    where T : AnnotationBase
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      return (Point[]) null;
    }

    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
    {
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(_param1, _param2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      AnnotationCoordinates _param1,
      IAnnotationCanvas _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 && _param1.\u0023\u003Dz6aJoeqoqAzym < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.ActualWidth && _param1.\u0023\u003Dz6aJoeqoqAzym > _param2.ActualWidth || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 && _param1.\u0023\u003DzWp13vlQiZCJc < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE <= _param2.ActualHeight ? 0 : (_param1.\u0023\u003DzWp13vlQiZCJc > _param2.ActualHeight ? 1 : 0))) == 0;
    }

    public override void \u0023\u003DzuPL3ELSPZybJ(
      AnnotationCoordinates _param1,
      double _param2,
      double _param3,
      IAnnotationCanvas _param4)
    {
      this.\u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(_param1, ref _param2, ref _param3, _param4);
    }

    protected virtual void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      AnnotationCoordinates _param1,
      ref double _param2,
      ref double _param3,
      IAnnotationCanvas _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2;
      double d2 = _param1.\u0023\u003Dz6aJoeqoqAzym + _param2;
      double d3 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param3;
      double d4 = _param1.\u0023\u003DzWp13vlQiZCJc + _param3;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.ActualWidth) || !this.\u0023\u003DzpTsgWlwWfZwP(d3, _param4.ActualHeight) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.ActualWidth) || !this.\u0023\u003DzpTsgWlwWfZwP(d4, _param4.ActualHeight))
      {
        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
        double val2_1 = double.IsNaN(d2) ? 0.0 : d2;
        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
        double val2_2 = double.IsNaN(d4) ? 0.0 : d4;
        if (Math.Max(val1_1, val2_1) < 0.0)
          _param2 -= Math.Max(val1_1, val2_1);
        if (Math.Min(val1_1, val2_1) > _param4.ActualWidth)
          _param2 -= Math.Min(val1_1, val2_1) - (_param4.ActualWidth - 1.0);
        if (Math.Max(val1_2, val2_2) < 0.0)
          _param3 -= Math.Max(val1_2, val2_2);
        if (Math.Min(val1_2, val2_2) > _param4.ActualHeight)
          _param3 -= Math.Min(val1_2, val2_2) - (_param4.ActualHeight - 1.0);
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2;
      _param1.\u0023\u003Dz6aJoeqoqAzym += _param2;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param3;
      _param1.\u0023\u003DzWp13vlQiZCJc += _param3;
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE), 0, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc), 2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    protected bool \u0023\u003DzpTsgWlwWfZwP(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().IsCoordinateValid(_param1, _param2);
    }

    protected IComparable[] \u0023\u003DzvQ1aszE\u003D(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().FromCoordinates(_param1, _param2);
    }
  }

  private sealed class \u0023\u003Dzffg\u0024YXBnGm7H\u0024PCqlQv7PCc\u003D
  {
    public AnnotationBase _variableSome3535;
    public object \u0023\u003DzwM8aRUE\u003D;
    public NotifyCollectionChangedEventArgs \u0023\u003DzTi2kmf4\u003D;

    public void \u0023\u003Dz51OAXpgKHWL1SXLaCA5i5tpZSV6aNEG7pUicQk0yky3I6F_EHJGXbx7TM94H()
    {
      // ISSUE: explicit non-virtual call
      this._variableSome3535._xAxis = this._variableSome3535.\u0023\u003DzI0EiGDjWkH8S(__nonvirtual (this._variableSome3535.XAxisId));
      this._variableSome3535.OnXAxesCollectionChanged(this.\u0023\u003DzwM8aRUE\u003D, this.\u0023\u003DzTi2kmf4\u003D);
    }
  }

  public class \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D<T> : 
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P8\u00249VOyCNzxoI\u0024gA\u002447LO3X8<T>
    where T : AnnotationBase
  {
    private readonly \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;

    public \u0023\u003Dzo2w1pth1o\u0024Z9uhNNd3fCWNU\u003D(T _param1)
      : base(_param1)
    {
      this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J = _param1.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
    }

    protected \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dzy9phceyLTfoo()
    {
      return this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;
    }

    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      return (Point[]) null;
    }

    public override void \u0023\u003DzzNonn\u0024lG8ddm(Point _param1, int _param2)
    {
      Point newPoint = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(newPoint, _param2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      AnnotationCoordinates _param1,
      IAnnotationCanvas _param2)
    {
      Size size = this.\u0023\u003DzIr6xoc_4P2lw(_param2);
      return this.\u0023\u003DzRe9EEbV7q4ey(_param1, size);
    }

    private Size \u0023\u003DzIr6xoc_4P2lw(
      IAnnotationCanvas _param1)
    {
      return new Size(360.0, \u0023\u003DzNNZS77x6QJuSCltptljzdAcsAmoG_AT4Zu2VfvM\u003D.\u0023\u003Dz62MsOEK3dnlV(_param1.ActualWidth, _param1.ActualHeight));
    }

    protected virtual bool \u0023\u003DzRe9EEbV7q4ey(
      AnnotationCoordinates _param1,
      Size _param2)
    {
      return (_param1.\u0023\u003DzS2_K6sVvd5IY < 0.0 && _param1.\u0023\u003Dz6aJoeqoqAzym < 0.0 || _param1.\u0023\u003DzS2_K6sVvd5IY > _param2.Width && _param1.\u0023\u003Dz6aJoeqoqAzym > _param2.Width || _param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 && _param1.\u0023\u003DzWp13vlQiZCJc < 0.0 ? 1 : (_param1.\u0023\u003Dz2J4l3QUGwZHE <= _param2.Height ? 0 : (_param1.\u0023\u003DzWp13vlQiZCJc > _param2.Height ? 1 : 0))) == 0;
    }

    public override void \u0023\u003DzuPL3ELSPZybJ(
      AnnotationCoordinates _param1,
      double _param2,
      double _param3,
      IAnnotationCanvas _param4)
    {
      Tuple<Point, Point> tuple = this.\u0023\u003DzQDA5x2uuH9m3(_param1, _param2, _param3);
      Size size = this.\u0023\u003DzIr6xoc_4P2lw(_param4);
      this.\u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(_param1, tuple.Item1, tuple.Item2, size);
    }

    protected virtual Tuple<Point, Point> \u0023\u003DzQDA5x2uuH9m3(
      AnnotationCoordinates _param1,
      double _param2,
      double _param3)
    {
      AnnotationCoordinates nnpojF4sCpkA8pp0g = this.\u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(_param1);
      return new Tuple<Point, Point>(this.\u0023\u003DztHc_NcH8us53(new Point(nnpojF4sCpkA8pp0g.\u0023\u003DzS2_K6sVvd5IY, nnpojF4sCpkA8pp0g.\u0023\u003Dz2J4l3QUGwZHE), _param2, _param3), this.\u0023\u003DztHc_NcH8us53(new Point(nnpojF4sCpkA8pp0g.\u0023\u003Dz6aJoeqoqAzym, nnpojF4sCpkA8pp0g.\u0023\u003DzWp13vlQiZCJc), _param2, _param3));
    }

    protected virtual AnnotationCoordinates \u0023\u003DzRGxj_ocSA6WWU4hH88BXD0c\u003D(
      AnnotationCoordinates _param1)
    {
      Point point1 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003DzsTReN_n58EEf(new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE));
      Point point2 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003DzsTReN_n58EEf(new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc));
      return new AnnotationCoordinates()
      {
        \u0023\u003DzS2_K6sVvd5IY = point1.X,
        \u0023\u003Dz2J4l3QUGwZHE = point1.Y,
        \u0023\u003Dz6aJoeqoqAzym = point2.X,
        \u0023\u003DzWp13vlQiZCJc = point2.Y
      };
    }

    private Point \u0023\u003DztHc_NcH8us53(Point _param1, double _param2, double _param3)
    {
      Point point1 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
      _param1.X += _param2;
      _param1.Y += _param3;
      Point point2 = this.\u0023\u003Dzy9phceyLTfoo().\u0023\u003Dz8miGAzg\u003D(_param1);
      return new Point(point2.X - point1.X, point2.Y - point1.Y);
    }

    protected virtual void \u0023\u003Dz1AMMqyD2rBjvD_AwSl5uj2E\u003D(
      AnnotationCoordinates _param1,
      Point _param2,
      Point _param3,
      Size _param4)
    {
      double d1 = _param1.\u0023\u003DzS2_K6sVvd5IY + _param2.X;
      double d2 = _param1.\u0023\u003Dz6aJoeqoqAzym + _param3.X;
      double d3 = _param1.\u0023\u003Dz2J4l3QUGwZHE + _param2.Y;
      double d4 = _param1.\u0023\u003DzWp13vlQiZCJc + _param3.Y;
      if (!this.\u0023\u003DzpTsgWlwWfZwP(d1, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d3, _param4.Height) || !this.\u0023\u003DzpTsgWlwWfZwP(d2, _param4.Width) || !this.\u0023\u003DzpTsgWlwWfZwP(d4, _param4.Height))
      {
        double val1_1 = double.IsNaN(d1) ? 0.0 : d1;
        double val2_1 = double.IsNaN(d2) ? 0.0 : d2;
        double val1_2 = double.IsNaN(d3) ? 0.0 : d3;
        double val2_2 = double.IsNaN(d4) ? 0.0 : d4;
        if (Math.Max(val1_1, val2_1) < 0.0)
        {
          double num = Math.Max(val1_1, val2_1);
          _param2.X -= num;
          _param3.X -= num;
        }
        if (Math.Min(val1_1, val2_1) > _param4.Width)
        {
          double num = Math.Min(val1_1, val2_1) - (_param4.Width - 1.0);
          _param2.X -= num;
          _param3.X -= num;
        }
        if (Math.Max(val1_2, val2_2) < 0.0)
        {
          double num = Math.Max(val1_2, val2_2);
          _param2.Y -= num;
          _param3.Y -= num;
        }
        if (Math.Min(val1_2, val2_2) > _param4.Height)
        {
          double num = Math.Min(val1_2, val2_2) - (_param4.Height - 1.0);
          _param2.Y -= num;
          _param3.Y -= num;
        }
      }
      _param1.\u0023\u003DzS2_K6sVvd5IY += _param2.X;
      _param1.\u0023\u003Dz6aJoeqoqAzym += _param3.X;
      _param1.\u0023\u003Dz2J4l3QUGwZHE += _param2.Y;
      _param1.\u0023\u003DzWp13vlQiZCJc += _param3.Y;
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE), 0, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
      this.\u0023\u003Dz_iIh83yfe01U().SetBasePoint(new Point(_param1.\u0023\u003Dz6aJoeqoqAzym, _param1.\u0023\u003DzWp13vlQiZCJc), 2, this.\u0023\u003Dz_iIh83yfe01U().XAxis, this.\u0023\u003Dz_iIh83yfe01U().YAxis);
    }

    protected bool \u0023\u003DzpTsgWlwWfZwP(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().IsCoordinateValid(_param1, _param2);
    }

    protected IComparable[] \u0023\u003DzvQ1aszE\u003D(double _param1, double _param2)
    {
      return this.\u0023\u003Dz_iIh83yfe01U().FromCoordinates(_param1, _param2);
    }
  }
}
