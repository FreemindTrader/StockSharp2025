// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.ActiveOrderAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

#nullable enable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

public class ActiveOrderAnnotation : AnnotationBase
{
  public static readonly 
  #nullable disable
  DependencyProperty OrderTextProperty = DependencyProperty.Register(nameof (OrderText), typeof (string), typeof (ActiveOrderAnnotation), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty OrderSizeTextProperty = DependencyProperty.Register(nameof (OrderSizeText), typeof (string), typeof (ActiveOrderAnnotation), new PropertyMetadata((PropertyChangedCallback) null));
  public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof (Stroke), typeof (Brush), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Brushes.White));
  public static readonly DependencyProperty CancelButtonFillProperty = DependencyProperty.Register(nameof (CancelButtonFill), typeof (Brush), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Brushes.DarkGray));
  public static readonly DependencyProperty CancelButtonColorProperty = DependencyProperty.Register(nameof (CancelButtonColor), typeof (Brush), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Brushes.Black));
  public static readonly DependencyProperty YDragStepProperty = DependencyProperty.Register(nameof (YDragStep), typeof (double), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) 0.0));
  public static readonly DependencyProperty IsAnimationEnabledProperty = DependencyProperty.Register(nameof (IsAnimationEnabled), typeof (bool), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) true));
  public static readonly DependencyProperty OrderErrorTextProperty = DependencyProperty.Register(nameof (OrderErrorText), typeof (string), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) "ERROR"));
  public static readonly DependencyProperty BlinkColorProperty = DependencyProperty.Register(nameof (BlinkColor), typeof (Color), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Colors.Black));
  private const double MinScrollSpeed = 0.3;
  private const double MaxScrollSpeed = 2.0;
  private static readonly TimeSpan _scrollTimerInterval = TimeSpan.FromMilliseconds(50.0);
  private Line _line;
  private Grid _gridOrderInfo;
  private Button _cancelButton;
  private Border _borderOrderCount;
  private Border _borderOrderText;
  private Polygon _orderPointer;
  private TextBlock _txtOrderText;
  private TextBlock _txtCount;
  private readonly AxisMarkerAnnotation _axisMarker;
  private bool _templateInitialized;
  private readonly DispatcherTimer _scrollTimer;
  private bool _isOutOfBounds;
  private double _totalScrollOffset;
  private Storyboard _fillAnimation;
  private Storyboard _errorAnimation;

  public ActiveOrderAnnotation()
  {
    this.DefaultStyleKey = (object) typeof (ActiveOrderAnnotation);
    AxisMarkerAnnotation markerAnnotation = new AxisMarkerAnnotation();
    markerAnnotation.IsEditable = false;
    markerAnnotation.IsSelected = false;
    this._axisMarker = markerAnnotation;
    this._axisMarker.SetBindings(UIElement.VisibilityProperty, (object) this, "Visibility", BindingMode.OneWay);
    this._axisMarker.SetBindings(Control.ForegroundProperty, (object) this, "Foreground", BindingMode.OneWay);
    this._axisMarker.SetBindings(Control.BackgroundProperty, (object) this, "Background", BindingMode.OneWay);
    this._axisMarker.SetBindings(Control.BorderBrushProperty, (object) this, "Background", BindingMode.OneWay);
    this._axisMarker.SetBindings(AnnotationBase.XAxisIdProperty, (object) this, "XAxisId", BindingMode.OneWay);
    this._axisMarker.SetBindings(AnnotationBase.YAxisIdProperty, (object) this, "YAxisId", BindingMode.OneWay);
    this._axisMarker.SetBindings(AnnotationBase.Y1Property, (object) this, "Y1", BindingMode.OneWay);
    this._axisMarker.SetBindings(AnnotationBase.IsHiddenProperty, (object) this, "IsHidden", BindingMode.OneWay);
    this._scrollTimer = new DispatcherTimer()
    {
      Interval = ActiveOrderAnnotation._scrollTimerInterval
    };
    this._scrollTimer.Tick += new EventHandler(this.ScrollTimerOnTick);
  }

  public string OrderText
  {
    get => (string) this.GetValue(ActiveOrderAnnotation.OrderTextProperty);
    set => this.SetValue(ActiveOrderAnnotation.OrderTextProperty, (object) value);
  }

  public string OrderSizeText
  {
    get => (string) this.GetValue(ActiveOrderAnnotation.OrderSizeTextProperty);
    set => this.SetValue(ActiveOrderAnnotation.OrderSizeTextProperty, (object) value);
  }

  public Brush Stroke
  {
    get => (Brush) this.GetValue(ActiveOrderAnnotation.StrokeProperty);
    set => this.SetValue(ActiveOrderAnnotation.StrokeProperty, (object) value);
  }

  public Brush CancelButtonFill
  {
    get => (Brush) this.GetValue(ActiveOrderAnnotation.CancelButtonFillProperty);
    set => this.SetValue(ActiveOrderAnnotation.CancelButtonFillProperty, (object) value);
  }

  public Brush CancelButtonColor
  {
    get => (Brush) this.GetValue(ActiveOrderAnnotation.CancelButtonColorProperty);
    set => this.SetValue(ActiveOrderAnnotation.CancelButtonColorProperty, (object) value);
  }

  public double YDragStep
  {
    get => (double) this.GetValue(ActiveOrderAnnotation.YDragStepProperty);
    set => this.SetValue(ActiveOrderAnnotation.YDragStepProperty, (object) value);
  }

  public bool IsAnimationEnabled
  {
    get => (bool) this.GetValue(ActiveOrderAnnotation.IsAnimationEnabledProperty);
    set => this.SetValue(ActiveOrderAnnotation.IsAnimationEnabledProperty, (object) value);
  }

  public string OrderErrorText
  {
    get => (string) this.GetValue(ActiveOrderAnnotation.OrderErrorTextProperty);
    set => this.SetValue(ActiveOrderAnnotation.OrderErrorTextProperty, (object) value);
  }

  public Color BlinkColor
  {
    get => (Color) this.GetValue(ActiveOrderAnnotation.BlinkColorProperty);
    set => this.SetValue(ActiveOrderAnnotation.BlinkColorProperty, (object) value);
  }

  public event Action<ActiveOrderAnnotation> CancelClick;

  protected override void HandleIsEditable()
  {
    Cursor cursor = this.IsEditable ? Cursors.SizeNS : Cursors.Arrow;
    this._gridOrderInfo?.SetValue(FrameworkElement.CursorProperty, (object) cursor);
  }

  protected override Cursor GetSelectedCursor() => Cursors.Arrow;

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.AnnotationRoot = (FrameworkElement) this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>("PART_AnnotationRoot");
    this._line = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Line>("PART_Line");
    this._gridOrderInfo = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Grid>("PART_GridOrderInfo");
    this._borderOrderCount = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Border>("PART_GridOrderCount");
    this._borderOrderText = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Border>("PART_GridOrderText");
    this._orderPointer = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Polygon>("PART_OrderPointer");
    this._txtCount = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<TextBlock>("PART_OrderCountText");
    this._txtOrderText = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<TextBlock>("PART_OrderText");
    if (this._cancelButton == null)
    {
      this._cancelButton = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Button>("PART_CancelButton");
      this._cancelButton.Click += (RoutedEventHandler) ((sender, args) =>
      {
        Action<ActiveOrderAnnotation> cancelClick = this.CancelClick;
        if (cancelClick == null)
          return;
        cancelClick(this);
      });
      this._cancelButton.SetBindings(UIElement.IsEnabledProperty, (object) this, "IsEditable", BindingMode.OneWay);
    }
    this._templateInitialized = true;
    this.HandleIsEditable();
    this.Refresh();
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this._axisMarker.Services = this.Services;
    this._axisMarker.ParentSurface = this.ParentSurface;
    this._axisMarker.IsAttached = true;
    this._axisMarker.OnAttached();
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this._axisMarker.OnDetached();
    this._axisMarker.Services = (IServiceContainer) null;
    this._axisMarker.ParentSurface = (ISciChartSurface) null;
    this._axisMarker.IsAttached = false;
  }

  public override void Update(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCoordinateCalculator,
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCoordinateCalculator)
  {
    base.Update(xCoordinateCalculator, yCoordinateCalculator);
    this._axisMarker.Update(xCoordinateCalculator, yCoordinateCalculator);
  }

  protected override void GetPropertiesFromIndex(
    int index,
    out DependencyProperty X,
    out DependencyProperty Y)
  {
    X = AnnotationBase.X1Property;
    Y = AnnotationBase.Y1Property;
  }

  protected override void SetBasePoint(
    Point newPoint,
    int index,
    IAxis xAxis,
    IAxis yAxis)
  {
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    IComparable[] comparableArray = this.FromCoordinates(newPoint);
    IComparable comparable = comparableArray[0];
    DependencyProperty x;
    DependencyProperty y;
    this.GetPropertiesFromIndex(index, out x, out y);
    if (this.IsCoordinateValid(newPoint.X, canvas.ActualWidth))
      this.SetCurrentValue(x, (object) comparable);
    this.SetCurrentValue(y, (object) comparableArray[1]);
  }

  protected override void OnDragEnded()
  {
    this._isOutOfBounds = false;
    this._totalScrollOffset = 0.0;
    this._scrollTimer.Stop();
  }

  private void ScrollTimerOnTick(object sender, EventArgs e)
  {
    IAxis yaxis = this.YAxis;
    if (!this.IsDragging || !this._isOutOfBounds || yaxis == null)
      return;
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    Point position = Mouse.GetPosition((IInputElement) canvas);
    if (position.Y >= 0.0 && position.Y < canvas.ActualHeight)
      return;
    int num1 = Math.Sign(position.Y);
    double num2 = num1 > 0 ? Math.Min(1.0, (position.Y - canvas.ActualHeight) / canvas.ActualHeight) : Math.Min(1.0, -position.Y / canvas.ActualHeight);
    double num3 = (double) num1 * (0.3 + num2 * 1.7);
    double num4 = canvas.ActualHeight * num3 * this._scrollTimer.Interval.TotalMilliseconds / 1000.0;
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xCalc = this.XAxis?.GetCurrentCoordinateCalculator();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> yCalc = this.YAxis.GetCurrentCoordinateCalculator();
    AnnotationCoordinates coordinates1 = this.GetCoordinates(canvas, xCalc, yCalc);
    if (this.YDragStep > 0.0)
    {
      double coord = num1 > 0 ? canvas.ActualHeight - 1.0 + num4 : -num4;
      IComparable comparable1 = this.FromCoordinate(coordinates1.\u0023\u003Dz2J4l3QUGwZHE, yaxis);
      IComparable comparable2 = this.FromCoordinate(coord, yaxis);
      int num5 = (int) Math.Round(Math.Abs(comparable1.ToDouble() - comparable2.ToDouble()) / this.YDragStep);
      int num6 = yaxis.get_FlipCoordinates() ? num1 : -num1;
      num4 = this.ToCoordinate((IComparable) (comparable1.ToDouble() + (double) (num6 * num5) * this.YDragStep), yaxis) - coordinates1.\u0023\u003Dz2J4l3QUGwZHE;
    }
    using (this.ParentSurface.SuspendUpdates())
    {
      this._totalScrollOffset += num4;
      yaxis.\u0023\u003DzquLnA5Y\u003D(num4, ClipMode.None);
      AnnotationCoordinates coordinates2 = this.GetCoordinates(canvas, this.XAxis?.GetCurrentCoordinateCalculator(), this.YAxis?.GetCurrentCoordinateCalculator());
      Point point = new Point();
      point.X = coordinates2.\u0023\u003DzS2_K6sVvd5IY;
      point.Y = num1 > 0 ? canvas.ActualHeight - 1.0 : 0.0;
      Point newPoint = point;
      this.GetPropertiesFromIndex(0, out DependencyProperty _, out DependencyProperty _);
      this.SetBasePoint(newPoint, 0, this.XAxis, this.YAxis);
      double y1 = newPoint.Y;
      point = this.DragStartPoint;
      double y2 = point.Y;
      this.RaiseAnnotationDragging(0.0, y1 - y2, (this.IsDraggingByUser ? 1 : 0) != 0, false);
    }
  }

  protected override (double fixedHOffset, double fixedVOffset) MoveAnnotationTo(
    AnnotationCoordinates coordinates,
    double horizOffset,
    double vertOffset)
  {
    IAxis yaxis = this.YAxis;
    if (yaxis == null)
      return (0.0, 0.0);
    IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
    double coord = coordinates.\u0023\u003Dz2J4l3QUGwZHE + vertOffset;
    if (this.IsDraggingByUser && !this.IsCoordinateValid(coord, canvas.ActualHeight))
    {
      if (yaxis.get_AutoRange() == AutoRange.Always)
        ((DependencyObject) yaxis).SetCurrentValue(AxisBase.AutoRangeProperty, (object) AutoRange.Once);
      if (!this._isOutOfBounds)
      {
        this._isOutOfBounds = true;
        this._scrollTimer.Start();
      }
      if (coord < 0.0)
        vertOffset -= coord - 1.0;
      if (coord > canvas.ActualHeight)
        vertOffset -= coord - (canvas.ActualHeight - 1.0);
      coord = coordinates.\u0023\u003Dz2J4l3QUGwZHE + vertOffset;
    }
    else
    {
      this._isOutOfBounds = false;
      this._totalScrollOffset = 0.0;
      this._scrollTimer.Stop();
    }
    if (this.YDragStep > 0.0)
    {
      IComparable comparable1 = this.FromCoordinate(coordinates.\u0023\u003Dz2J4l3QUGwZHE, yaxis);
      IComparable comparable2 = this.FromCoordinate(coord, yaxis);
      int num1 = (int) Math.Round(Math.Abs(comparable1.ToDouble() - comparable2.ToDouble()) / this.YDragStep);
      int num2 = !yaxis.get_FlipCoordinates() ? -Math.Sign(vertOffset) : Math.Sign(vertOffset);
      coord = this.ToCoordinate((IComparable) (comparable1.ToDouble() + (double) (num2 * num1) * this.YDragStep), yaxis);
    }
    vertOffset = coord - coordinates.\u0023\u003Dz2J4l3QUGwZHE;
    if (!this.IsCoordinateValid(coord, canvas.ActualHeight) && this.IsDraggingByUser)
      return (0.0, 0.0);
    this.SetBasePoint(new Point()
    {
      X = coordinates.\u0023\u003DzS2_K6sVvd5IY,
      Y = coord
    }, 0, this.XAxis, this.YAxis);
    return (0.0, vertOffset);
  }

  protected override \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq GetCurrentPlacementStrategy()
  {
    if (this.XAxis != null && this.XAxis.get_IsPolarAxis())
      throw new InvalidOperationException("Polar axis is not supported for this type of annotation");
    return (\u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDgSOEFmb0yDbA4SN8HzRCkq) new ActiveOrderAnnotation.\u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(this);
  }

  public override bool CanMultiSelect(
    IAnnotation[] annotations)
  {
    return ((IEnumerable<IAnnotation>) annotations).All<IAnnotation>(ActiveOrderAnnotation.SomeClass34343383.\u0023\u003DznzFgTdlItmhQVnVTUQ\u003D\u003D ?? (ActiveOrderAnnotation.SomeClass34343383.\u0023\u003DznzFgTdlItmhQVnVTUQ\u003D\u003D = new Func<IAnnotation, bool>(ActiveOrderAnnotation.SomeClass34343383.SomeMethond0343.\u0023\u003DzdM_Hrfu\u0024qp11xw0hbj1NIyE\u003D)));
  }

  public event Action<ActiveOrderAnnotation> AnimationDone;

  public void AnimateOrderFill()
  {
    if (this.IsAnimationEnabled)
      this.GetFillAnimation()?.Begin((FrameworkElement) this, true);
    else
      this.TryInvokeAnimationDone();
  }

  public void AnimateError()
  {
    if (this.IsAnimationEnabled)
      this.GetErrorAnimation()?.Begin((FrameworkElement) this, true);
    else
      this.TryInvokeAnimationDone();
  }

  private Storyboard GetFillAnimation()
  {
    if (!this._templateInitialized)
      return (Storyboard) null;
    Color blinkColor = this.BlinkColor;
    if (this._fillAnimation != null)
    {
      ((ColorAnimation) this._fillAnimation.Children[2]).From = new Color?(blinkColor);
      ((ColorAnimation) this._fillAnimation.Children[3]).From = new Color?(blinkColor);
      ((ColorAnimation) this._fillAnimation.Children[4]).From = new Color?(blinkColor);
      return this._fillAnimation;
    }
    this._fillAnimation = new Storyboard();
    this._fillAnimation.Completed += (EventHandler) ((sender, args) => this.TryInvokeAnimationDone());
    DoubleAnimation doubleAnimation1 = this.InitAnimation<DoubleAnimation>(this._fillAnimation, (DependencyObject) this._borderOrderCount, "RenderTransform.ScaleX");
    DoubleAnimation doubleAnimation2 = this.InitAnimation<DoubleAnimation>(this._fillAnimation, (DependencyObject) this._borderOrderCount, "RenderTransform.ScaleY");
    ColorAnimation colorAnimation1 = this.InitAnimation<ColorAnimation>(this._fillAnimation, (DependencyObject) this._borderOrderText, "Background.Color");
    ColorAnimation colorAnimation2 = this.InitAnimation<ColorAnimation>(this._fillAnimation, (DependencyObject) this._borderOrderCount, "Background.Color");
    ColorAnimation colorAnimation3 = this.InitAnimation<ColorAnimation>(this._fillAnimation, (DependencyObject) this._orderPointer, "Fill.Color");
    DoubleAnimation doubleAnimation3 = doubleAnimation2;
    double? nullable1 = new double?(1.5);
    double? nullable2 = nullable1;
    doubleAnimation3.To = nullable2;
    doubleAnimation1.To = nullable1;
    doubleAnimation1.Duration = doubleAnimation2.Duration = (Duration) TimeSpan.FromMilliseconds(75.0);
    doubleAnimation1.EasingFunction = doubleAnimation2.EasingFunction = (IEasingFunction) new ExponentialEase();
    ColorAnimation colorAnimation4 = colorAnimation1;
    ColorAnimation colorAnimation5 = colorAnimation2;
    ColorAnimation colorAnimation6 = colorAnimation3;
    RepeatBehavior repeatBehavior1 = new RepeatBehavior(3.0);
    RepeatBehavior repeatBehavior2 = repeatBehavior1;
    colorAnimation6.RepeatBehavior = repeatBehavior2;
    RepeatBehavior repeatBehavior3;
    RepeatBehavior repeatBehavior4 = repeatBehavior3 = repeatBehavior1;
    colorAnimation5.RepeatBehavior = repeatBehavior3;
    RepeatBehavior repeatBehavior5 = repeatBehavior4;
    colorAnimation4.RepeatBehavior = repeatBehavior5;
    ColorAnimation colorAnimation7 = colorAnimation1;
    ColorAnimation colorAnimation8 = colorAnimation2;
    ColorAnimation colorAnimation9 = colorAnimation3;
    Color? nullable3 = new Color?(blinkColor);
    Color? nullable4 = nullable3;
    colorAnimation9.From = nullable4;
    Color? nullable5;
    Color? nullable6 = nullable5 = nullable3;
    colorAnimation8.From = nullable5;
    Color? nullable7 = nullable6;
    colorAnimation7.From = nullable7;
    colorAnimation1.Duration = colorAnimation2.Duration = colorAnimation3.Duration = (Duration) TimeSpan.FromMilliseconds(100.0);
    ColorAnimation colorAnimation10 = colorAnimation1;
    ColorAnimation colorAnimation11 = colorAnimation2;
    ColorAnimation colorAnimation12 = colorAnimation3;
    ExponentialEase exponentialEase = new ExponentialEase();
    exponentialEase.EasingMode = EasingMode.EaseIn;
    exponentialEase.Exponent = 3.0;
    IEasingFunction easingFunction1 = (IEasingFunction) exponentialEase;
    colorAnimation12.EasingFunction = (IEasingFunction) exponentialEase;
    IEasingFunction easingFunction2;
    IEasingFunction easingFunction3 = easingFunction2 = easingFunction1;
    colorAnimation11.EasingFunction = easingFunction2;
    IEasingFunction easingFunction4 = easingFunction3;
    colorAnimation10.EasingFunction = easingFunction4;
    return this._fillAnimation;
  }

  private Storyboard GetErrorAnimation()
  {
    if (this._errorAnimation != null || !this._templateInitialized)
      return this._errorAnimation;
    Storyboard storyboard = new Storyboard();
    storyboard.FillBehavior = FillBehavior.Stop;
    this._errorAnimation = storyboard;
    this._errorAnimation.Completed += (EventHandler) ((sender, args) => this.TryInvokeAnimationDone());
    Color red = Colors.Red;
    Color black = Colors.Black;
    this.InitErrorColorAnimation(this._errorAnimation, (DependencyObject) this._borderOrderText, "Background.Color", red, black);
    this.InitErrorColorAnimation(this._errorAnimation, (DependencyObject) this._borderOrderCount, "Background.Color", red, black);
    this.InitErrorColorAnimation(this._errorAnimation, (DependencyObject) this._orderPointer, "Fill.Color", red, black);
    this.InitErrorColorAnimation(this._errorAnimation, (DependencyObject) this._txtCount, "Foreground.Color", black, red);
    this.InitErrorColorAnimation(this._errorAnimation, (DependencyObject) this._txtOrderText, "Foreground.Color", black, red);
    StringAnimationUsingKeyFrames animationUsingKeyFrames = this.InitAnimation<StringAnimationUsingKeyFrames>(this._errorAnimation, (DependencyObject) this._txtOrderText, "Text");
    animationUsingKeyFrames.FillBehavior = FillBehavior.HoldEnd;
    StringKeyFrameCollection keyFrames = animationUsingKeyFrames.KeyFrames;
    DiscreteStringKeyFrame keyFrame = new DiscreteStringKeyFrame();
    keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
    keyFrame.Value = this.OrderErrorText;
    keyFrames.Add((StringKeyFrame) keyFrame);
    return this._errorAnimation;
  }

  private T InitAnimation<T>(Storyboard sb, DependencyObject target, string path) where T : AnimationTimeline, new()
  {
    T obj = new T();
    obj.AutoReverse = true;
    obj.FillBehavior = FillBehavior.Stop;
    T element = obj;
    Storyboard.SetTarget((DependencyObject) (object) element, target);
    Storyboard.SetTargetProperty((DependencyObject) (object) element, new PropertyPath(path, Array.Empty<object>()));
    sb.Children.Add((Timeline) element);
    return element;
  }

  private void InitErrorColorAnimation(
    Storyboard sb,
    DependencyObject target,
    string path,
    Color col1,
    Color col2)
  {
    ColorAnimationUsingKeyFrames animationUsingKeyFrames = this.InitAnimation<ColorAnimationUsingKeyFrames>(sb, target, path);
    animationUsingKeyFrames.RepeatBehavior = new RepeatBehavior(5.0);
    animationUsingKeyFrames.Duration = (Duration) TimeSpan.FromMilliseconds(100.0);
    ColorKeyFrameCollection keyFrames1 = animationUsingKeyFrames.KeyFrames;
    DiscreteColorKeyFrame keyFrame1 = new DiscreteColorKeyFrame();
    keyFrame1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0.0));
    keyFrame1.Value = col1;
    keyFrames1.Add((ColorKeyFrame) keyFrame1);
    ColorKeyFrameCollection keyFrames2 = animationUsingKeyFrames.KeyFrames;
    DiscreteColorKeyFrame keyFrame2 = new DiscreteColorKeyFrame();
    keyFrame2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(50.0));
    keyFrame2.Value = col2;
    keyFrames2.Add((ColorKeyFrame) keyFrame2);
  }

  private void TryInvokeAnimationDone()
  {
    Storyboard errorAnimation = this._errorAnimation;
    ClockState clockState1 = errorAnimation != null ? errorAnimation.GetCurrentState((FrameworkElement) this) : ClockState.Stopped;
    Storyboard fillAnimation = this._fillAnimation;
    ClockState clockState2 = fillAnimation != null ? fillAnimation.GetCurrentState((FrameworkElement) this) : ClockState.Stopped;
    if (this.IsAnimationEnabled && (clockState1 == ClockState.Active || clockState2 == ClockState.Active))
      return;
    Action<ActiveOrderAnnotation> animationDone = this.AnimationDone;
    if (animationDone == null)
      return;
    animationDone(this);
  }

  private sealed class \u0023\u003Dz38BC6oc3_RZWxnXw6Xnz7zE\u003D(ActiveOrderAnnotation _param1) : 
    AnnotationBase.\u0023\u003DzZ8mHGwKUmQVwqESFtdY8Hx9t4kZY<ActiveOrderAnnotation>(_param1)
  {
    public override void \u0023\u003DzNUoYFVRHgzxB(
      AnnotationCoordinates _param1)
    {
      IAnnotationCanvas canvas = this.\u0023\u003Dz_iIh83yfe01U().GetCanvas(this.\u0023\u003Dz_iIh83yfe01U().AnnotationCanvas);
      double z2J4l3QuGwZhe = _param1.\u0023\u003Dz2J4l3QUGwZHE;
      if (!z2J4l3QuGwZhe.\u0023\u003Dz_Bj0HmLWq3hY() || canvas == null)
        return;
      double num1 = this.\u0023\u003Dz_iIh83yfe01U().AnnotationRoot.ActualHeight / 2.0;
      double num2 = Math.Max(10.0, canvas.ActualWidth - _param1.\u0023\u003DzS2_K6sVvd5IY);
      Line line = this.\u0023\u003Dz_iIh83yfe01U()._line;
      if (!line.X1.DoubleEquals(0.0) || !line.X2.DoubleEquals(num2) || !line.Y1.DoubleEquals(num1) || !line.Y2.DoubleEquals(num1))
      {
        line.X1 = 0.0;
        line.X2 = num2;
        line.Y1 = this.\u0023\u003Dz_iIh83yfe01U()._line.Y2 = num1;
        line.UpdateLayout();
      }
      double num3 = z2J4l3QuGwZhe - num1;
      double num4 = canvas.ActualWidth - this.\u0023\u003Dz_iIh83yfe01U().ActualWidth;
      this.\u0023\u003Dz_iIh83yfe01U().SetValue(Canvas.LeftProperty, (object) num4);
      this.\u0023\u003Dz_iIh83yfe01U().SetValue(Canvas.TopProperty, (object) num3);
    }

    public override Point[] \u0023\u003DzfJgp916l7LbX(
      AnnotationCoordinates _param1)
    {
      return new Point[1]
      {
        new Point(_param1.\u0023\u003DzS2_K6sVvd5IY, _param1.\u0023\u003Dz2J4l3QUGwZHE)
      };
    }

    public override bool \u0023\u003DzxGhbraO0gg9\u0024(
      AnnotationCoordinates _param1,
      IAnnotationCanvas _param2)
    {
      return (_param1.\u0023\u003Dz2J4l3QUGwZHE < 0.0 || _param1.\u0023\u003Dz2J4l3QUGwZHE > _param2.ActualHeight ? 1 : (_param1.\u0023\u003DzS2_K6sVvd5IY > _param2.ActualWidth ? 1 : 0)) == 0;
    }
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ActiveOrderAnnotation.SomeClass34343383 SomeMethond0343 = new ActiveOrderAnnotation.SomeClass34343383();
    public static Func<IAnnotation, bool> \u0023\u003DznzFgTdlItmhQVnVTUQ\u003D\u003D;

    public bool \u0023\u003DzdM_Hrfu\u0024qp11xw0hbj1NIyE\u003D(
      IAnnotation _param1)
    {
      return _param1 is ActiveOrderAnnotation;
    }
  }
}
