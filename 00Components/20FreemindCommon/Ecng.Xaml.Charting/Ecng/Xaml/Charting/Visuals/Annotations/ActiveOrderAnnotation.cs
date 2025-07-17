// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.ActiveOrderAnnotation
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace fx.Xaml.Charting
{
    public class ActiveOrderAnnotation : AnnotationBase
    {
        public static readonly DependencyProperty OrderTextProperty = DependencyProperty.Register(nameof (OrderText), typeof (string), typeof (ActiveOrderAnnotation), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty OrderSizeTextProperty = DependencyProperty.Register(nameof (OrderSizeText), typeof (string), typeof (ActiveOrderAnnotation), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof (Stroke), typeof (Brush), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Brushes.White));
        public static readonly DependencyProperty CancelButtonFillProperty = DependencyProperty.Register(nameof (CancelButtonFill), typeof (Brush), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Brushes.DarkGray));
        public static readonly DependencyProperty CancelButtonColorProperty = DependencyProperty.Register(nameof (CancelButtonColor), typeof (Brush), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Brushes.Black));
        public static readonly DependencyProperty YDragStepProperty = DependencyProperty.Register(nameof (YDragStep), typeof (double), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) 0.0));
        public static readonly DependencyProperty IsAnimationEnabledProperty = DependencyProperty.Register(nameof (IsAnimationEnabled), typeof (bool), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) true));
        public static readonly DependencyProperty OrderErrorTextProperty = DependencyProperty.Register(nameof (OrderErrorText), typeof (string), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) "ERROR"));
        public static readonly DependencyProperty BlinkColorProperty = DependencyProperty.Register(nameof (BlinkColor), typeof (Color), typeof (ActiveOrderAnnotation), new PropertyMetadata((object) Colors.Black));
        private static readonly TimeSpan _scrollTimerInterval = TimeSpan.FromMilliseconds(50.0);
        private const double MinScrollSpeed = 0.3;
        private const double MaxScrollSpeed = 2.0;
        private System.Windows.Shapes.Line _line;
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

        public string OrderText
        {
            get
            {
                return ( string ) GetValue( ActiveOrderAnnotation.OrderTextProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.OrderTextProperty, ( object ) value );
            }
        }

        public string OrderSizeText
        {
            get
            {
                return ( string ) GetValue( ActiveOrderAnnotation.OrderSizeTextProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.OrderSizeTextProperty, ( object ) value );
            }
        }

        public Brush Stroke
        {
            get
            {
                return ( Brush ) GetValue( ActiveOrderAnnotation.StrokeProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.StrokeProperty, ( object ) value );
            }
        }

        public Brush CancelButtonFill
        {
            get
            {
                return ( Brush ) GetValue( ActiveOrderAnnotation.CancelButtonFillProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.CancelButtonFillProperty, ( object ) value );
            }
        }

        public Brush CancelButtonColor
        {
            get
            {
                return ( Brush ) GetValue( ActiveOrderAnnotation.CancelButtonColorProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.CancelButtonColorProperty, ( object ) value );
            }
        }

        public double YDragStep
        {
            get
            {
                return ( double ) GetValue( ActiveOrderAnnotation.YDragStepProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.YDragStepProperty, ( object ) value );
            }
        }

        public bool IsAnimationEnabled
        {
            get
            {
                return ( bool ) GetValue( ActiveOrderAnnotation.IsAnimationEnabledProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.IsAnimationEnabledProperty, ( object ) value );
            }
        }

        public string OrderErrorText
        {
            get
            {
                return ( string ) GetValue( ActiveOrderAnnotation.OrderErrorTextProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.OrderErrorTextProperty, ( object ) value );
            }
        }

        public Color BlinkColor
        {
            get
            {
                return ( Color ) GetValue( ActiveOrderAnnotation.BlinkColorProperty );
            }
            set
            {
                SetValue( ActiveOrderAnnotation.BlinkColorProperty, ( object ) value );
            }
        }

        public event Action<ActiveOrderAnnotation> CancelClick;

        public ActiveOrderAnnotation()
        {
            DefaultStyleKey = ( object ) typeof( ActiveOrderAnnotation );
            AxisMarkerAnnotation markerAnnotation = new AxisMarkerAnnotation();
            markerAnnotation.IsEditable = false;
            markerAnnotation.IsSelected = false;
            _axisMarker = markerAnnotation;


            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, UIElement.VisibilityProperty, ( object ) this, "Visibility", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, Control.ForegroundProperty, ( object ) this, "Foreground", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, Control.BackgroundProperty, ( object ) this, "Background", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, Control.BorderBrushProperty, ( object ) this, "Background", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, AnnotationBase.XAxisIdProperty, ( object ) this, "XAxisId", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, AnnotationBase.YAxisIdProperty, ( object ) this, "YAxisId", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, AnnotationBase.Y1Property, ( object ) this, "Y1", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            Ecng.Xaml.XamlHelper.SetBindings( _axisMarker, AnnotationBase.IsHiddenProperty, ( object ) this, "IsHidden", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            _scrollTimer = new DispatcherTimer()
            {
                Interval = ActiveOrderAnnotation._scrollTimerInterval
            };
            _scrollTimer.Tick += new EventHandler( ScrollTimerOnTick );
        }

        protected override void HandleIsEditable()
        {
            Cursor cursor = IsEditable ? Cursors.SizeNS : Cursors.Arrow;
            _gridOrderInfo?.SetValue( FrameworkElement.CursorProperty, ( object ) cursor );
        }

        protected override Cursor GetSelectedCursor()
        {
            return Cursors.Arrow;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AnnotationRoot = ( FrameworkElement ) GetAndAssertTemplateChild<Grid>( "PART_AnnotationRoot" );
            _line = GetAndAssertTemplateChild<System.Windows.Shapes.Line>( "PART_Line" );
            _gridOrderInfo = GetAndAssertTemplateChild<Grid>( "PART_GridOrderInfo" );
            _borderOrderCount = GetAndAssertTemplateChild<Border>( "PART_GridOrderCount" );
            _borderOrderText = GetAndAssertTemplateChild<Border>( "PART_GridOrderText" );
            _orderPointer = GetAndAssertTemplateChild<Polygon>( "PART_OrderPointer" );
            _txtCount = GetAndAssertTemplateChild<TextBlock>( "PART_OrderCountText" );
            _txtOrderText = GetAndAssertTemplateChild<TextBlock>( "PART_OrderText" );
            if ( _cancelButton == null )
            {
                _cancelButton = GetAndAssertTemplateChild<Button>( "PART_CancelButton" );
                _cancelButton.Click += ( RoutedEventHandler ) ( ( sender, args ) =>
                {
                    // ISSUE: reference to a compiler-generated field
                    Action<ActiveOrderAnnotation> cancelClick = CancelClick;
                    if ( cancelClick == null )
                        return;
                    cancelClick( this );
                } );

                Ecng.Xaml.XamlHelper.SetBindings( _cancelButton, UIElement.IsEnabledProperty, ( object ) this, "IsEditable", BindingMode.OneWay, ( IValueConverter ) null, ( object ) null );
            }
            _templateInitialized = true;
            HandleIsEditable();
            Refresh();
        }

        public override void OnAttached()
        {
            base.OnAttached();
            _axisMarker.Services = Services;
            _axisMarker.ParentSurface = ParentSurface;
            _axisMarker.IsAttached = true;
            _axisMarker.OnAttached();
        }

        public override void OnDetached()
        {
            base.OnDetached();
            _axisMarker.OnDetached();
            _axisMarker.Services = ( IServiceContainer ) null;
            _axisMarker.ParentSurface = ( ISciChartSurface ) null;
            _axisMarker.IsAttached = false;
        }

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            base.Update( xCoordinateCalculator, yCoordinateCalculator );
            _axisMarker.Update( xCoordinateCalculator, yCoordinateCalculator );
        }

        protected override void GetPropertiesFromIndex( int index, out DependencyProperty X, out DependencyProperty Y )
        {
            X = AnnotationBase.X1Property;
            Y = AnnotationBase.Y1Property;
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
            IComparable[] comparableArray = FromCoordinates(newPoint);
            IComparable comparable = comparableArray[0];
            DependencyProperty x;
            DependencyProperty y;
            GetPropertiesFromIndex( index, out x, out y );
            if ( IsCoordinateValid( newPoint.X, canvas.ActualWidth ) )
                SetCurrentValue( x, ( object ) comparable );
            SetCurrentValue( y, ( object ) comparableArray[ 1 ] );
        }

        public override void OnDragEnded()
        {
            base.OnDragEnded();
            _isOutOfBounds = false;
            _totalScrollOffset = 0.0;
            _scrollTimer.Stop();
        }

        private void ScrollTimerOnTick( object sender, EventArgs e )
        {
            IAxis yaxis = YAxis;
            if ( !IsDragging || !_isOutOfBounds || yaxis == null )
                return;
            IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
            Point position = Mouse.GetPosition((IInputElement) canvas);
            if ( position.Y >= 0.0 && position.Y < canvas.ActualHeight )
                return;
            int num1 = Math.Sign(position.Y);
            double num2 = num1 > 0 ? Math.Min(1.0, (position.Y - canvas.ActualHeight) / canvas.ActualHeight) : Math.Min(1.0, -position.Y / canvas.ActualHeight);
            double num3 = (double) num1 * (0.3 + num2 * 1.7);
            double pixelsToScroll = canvas.ActualHeight * num3 * _scrollTimer.Interval.TotalMilliseconds / 1000.0;
            if ( YDragStep > 0.0 )
            {
                double coord = num1 > 0 ? canvas.ActualHeight - 1.0 + pixelsToScroll : -pixelsToScroll;
                AnnotationCoordinates coordinates = GetCoordinates(canvas, XAxis?.GetCurrentCoordinateCalculator(), YAxis?.GetCurrentCoordinateCalculator());
                IComparable c = FromCoordinate(coordinates.Y1Coord, yaxis);
                int num4 = (int) Math.Round(Math.Abs(c.ToDouble() - FromCoordinate(coord, yaxis).ToDouble()) / YDragStep);
                pixelsToScroll = ToCoordinate( ( IComparable ) ( c.ToDouble() + ( double ) ( ( yaxis.FlipCoordinates ? num1 : -num1 ) * num4 ) * YDragStep ), yaxis ) - coordinates.Y1Coord;
            }
            using ( ParentSurface.SuspendUpdates() )
            {
                _totalScrollOffset += pixelsToScroll;
                yaxis.Scroll( pixelsToScroll, ClipMode.None );
                AnnotationCoordinates coordinates = GetCoordinates(canvas, XAxis?.GetCurrentCoordinateCalculator(), YAxis?.GetCurrentCoordinateCalculator());
                SetBasePoint( new Point()
                {
                    X = coordinates.X1Coord,
                    Y = num1 > 0 ? canvas.ActualHeight - 1.0 : 0.0
                }, 0, XAxis, YAxis );
            }
        }

        protected override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizOffset, double vertOffset )
        {
            IAxis yaxis = YAxis;
            if ( yaxis == null )
                return;
            IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
            double coord = coordinates.Y1Coord + vertOffset;
            if ( !IsCoordinateValid( coord, canvas.ActualHeight ) )
            {
                if ( yaxis.AutoRange == AutoRange.Always )
                    ( ( DependencyObject ) yaxis ).SetCurrentValue( AxisBase.AutoRangeProperty, ( object ) AutoRange.Once );
                if ( !_isOutOfBounds )
                {
                    _isOutOfBounds = true;
                    _scrollTimer.Start();
                }
                if ( coord < 0.0 )
                    vertOffset -= coord - 1.0;
                if ( coord > canvas.ActualHeight )
                    vertOffset -= coord - ( canvas.ActualHeight - 1.0 );
                coord = coordinates.Y1Coord + vertOffset;
            }
            else
            {
                _isOutOfBounds = false;
                _totalScrollOffset = 0.0;
                _scrollTimer.Stop();
            }
            if ( YDragStep > 0.0 )
            {
                IComparable c = FromCoordinate(coordinates.Y1Coord, yaxis);
                int num = (int) Math.Round(Math.Abs(c.ToDouble() - FromCoordinate(coord, yaxis).ToDouble()) / YDragStep);
                coord = ToCoordinate( ( IComparable ) ( c.ToDouble() + ( double ) ( ( !yaxis.FlipCoordinates ? -Math.Sign( vertOffset ) : Math.Sign( vertOffset ) ) * num ) * YDragStep ), yaxis );
                vertOffset = coord - coordinates.Y1Coord;
            }
            if ( !IsCoordinateValid( coord, canvas.ActualHeight ) )
                return;
            SetBasePoint( new Point()
            {
                X = coordinates.X1Coord,
                Y = coord
            }, 0, XAxis, YAxis );
            OnAnnotationDragging( new AnnotationDragDeltaEventArgs( 0.0, vertOffset + _totalScrollOffset ) );
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( XAxis != null && XAxis.IsPolarAxis )
                throw new InvalidOperationException( "Polar axis is not supported for this type of annotation" );
            return ( IAnnotationPlacementStrategy ) new ActiveOrderAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        public event Action<ActiveOrderAnnotation> AnimationDone;

        public void AnimateOrderFill()
        {
            if ( IsAnimationEnabled )
                GetFillAnimation()?.Begin( ( FrameworkElement ) this, true );
            else
                TryInvokeAnimationDone();
        }

        public void AnimateError()
        {
            if ( IsAnimationEnabled )
                GetErrorAnimation()?.Begin( ( FrameworkElement ) this, true );
            else
                TryInvokeAnimationDone();
        }

        private Storyboard GetFillAnimation()
        {
            if ( !_templateInitialized )
                return ( Storyboard ) null;
            Color blinkColor = BlinkColor;
            if ( _fillAnimation != null )
            {
                ( ( ColorAnimation ) _fillAnimation.Children[ 2 ] ).From = new Color?( blinkColor );
                ( ( ColorAnimation ) _fillAnimation.Children[ 3 ] ).From = new Color?( blinkColor );
                ( ( ColorAnimation ) _fillAnimation.Children[ 4 ] ).From = new Color?( blinkColor );
                return _fillAnimation;
            }
            _fillAnimation = new Storyboard();
            _fillAnimation.Completed += ( EventHandler ) ( ( sender, args ) => TryInvokeAnimationDone() );
            DoubleAnimation doubleAnimation1 = InitAnimation<DoubleAnimation>(_fillAnimation, (DependencyObject) _borderOrderCount, "RenderTransform.ScaleX");
            DoubleAnimation doubleAnimation2 = InitAnimation<DoubleAnimation>(_fillAnimation, (DependencyObject) _borderOrderCount, "RenderTransform.ScaleY");
            ColorAnimation colorAnimation1 = InitAnimation<ColorAnimation>(_fillAnimation, (DependencyObject) _borderOrderText, "Background.Color");
            ColorAnimation colorAnimation2 = InitAnimation<ColorAnimation>(_fillAnimation, (DependencyObject) _borderOrderCount, "Background.Color");
            ColorAnimation colorAnimation3 = InitAnimation<ColorAnimation>(_fillAnimation, (DependencyObject) _orderPointer, "Fill.Color");
            DoubleAnimation doubleAnimation3 = doubleAnimation2;
            double? nullable1 = new double?(1.5);
            double? nullable2 = nullable1;
            doubleAnimation3.To = nullable2;
            doubleAnimation1.To = nullable1;
            doubleAnimation1.Duration = doubleAnimation2.Duration = ( Duration ) TimeSpan.FromMilliseconds( 75.0 );
            doubleAnimation1.EasingFunction = doubleAnimation2.EasingFunction = ( IEasingFunction ) new ExponentialEase();
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
            colorAnimation1.Duration = colorAnimation2.Duration = colorAnimation3.Duration = ( Duration ) TimeSpan.FromMilliseconds( 100.0 );
            ColorAnimation colorAnimation10 = colorAnimation1;
            ColorAnimation colorAnimation11 = colorAnimation2;
            ColorAnimation colorAnimation12 = colorAnimation3;
            ExponentialEase exponentialEase = new ExponentialEase();
            exponentialEase.EasingMode = EasingMode.EaseIn;
            exponentialEase.Exponent = 3.0;
            IEasingFunction easingFunction1 = (IEasingFunction) exponentialEase;
            colorAnimation12.EasingFunction = ( IEasingFunction ) exponentialEase;
            IEasingFunction easingFunction2;
            IEasingFunction easingFunction3 = easingFunction2 = easingFunction1;
            colorAnimation11.EasingFunction = easingFunction2;
            IEasingFunction easingFunction4 = easingFunction3;
            colorAnimation10.EasingFunction = easingFunction4;
            return _fillAnimation;
        }

        private Storyboard GetErrorAnimation()
        {
            if ( _errorAnimation != null || !_templateInitialized )
                return _errorAnimation;
            Storyboard storyboard = new Storyboard();
            storyboard.FillBehavior = FillBehavior.Stop;
            _errorAnimation = storyboard;
            _errorAnimation.Completed += ( EventHandler ) ( ( sender, args ) => TryInvokeAnimationDone() );
            Color red = Colors.Red;
            Color black = Colors.Black;
            InitErrorColorAnimation( _errorAnimation, ( DependencyObject ) _borderOrderText, "Background.Color", red, black );
            InitErrorColorAnimation( _errorAnimation, ( DependencyObject ) _borderOrderCount, "Background.Color", red, black );
            InitErrorColorAnimation( _errorAnimation, ( DependencyObject ) _orderPointer, "Fill.Color", red, black );
            InitErrorColorAnimation( _errorAnimation, ( DependencyObject ) _txtCount, "Foreground.Color", black, red );
            InitErrorColorAnimation( _errorAnimation, ( DependencyObject ) _txtOrderText, "Foreground.Color", black, red );
            StringAnimationUsingKeyFrames animationUsingKeyFrames = InitAnimation<StringAnimationUsingKeyFrames>(_errorAnimation, (DependencyObject) _txtOrderText, "Text");
            animationUsingKeyFrames.FillBehavior = FillBehavior.HoldEnd;
            StringKeyFrameCollection keyFrames = animationUsingKeyFrames.KeyFrames;
            DiscreteStringKeyFrame discreteStringKeyFrame = new DiscreteStringKeyFrame();
            discreteStringKeyFrame.KeyTime = KeyTime.FromTimeSpan( TimeSpan.Zero );
            discreteStringKeyFrame.Value = OrderErrorText;
            keyFrames.Add( ( StringKeyFrame ) discreteStringKeyFrame );
            return _errorAnimation;
        }

        private T InitAnimation<T>( Storyboard sb, DependencyObject target, string path ) where T : AnimationTimeline, new()
        {
            T instance = Activator.CreateInstance<T>();
            instance.AutoReverse = true;
            instance.FillBehavior = FillBehavior.Stop;
            T obj = instance;
            Storyboard.SetTarget( ( DependencyObject ) obj, target );
            Storyboard.SetTargetProperty( ( DependencyObject ) obj, new PropertyPath( path, new object[ 0 ] ) );
            sb.Children.Add( ( Timeline ) obj );
            return obj;
        }

        private void InitErrorColorAnimation( Storyboard sb, DependencyObject target, string path, Color col1, Color col2 )
        {
            ColorAnimationUsingKeyFrames animationUsingKeyFrames = InitAnimation<ColorAnimationUsingKeyFrames>(sb, target, path);
            animationUsingKeyFrames.RepeatBehavior = new RepeatBehavior( 5.0 );
            animationUsingKeyFrames.Duration = ( Duration ) TimeSpan.FromMilliseconds( 100.0 );
            ColorKeyFrameCollection keyFrames1 = animationUsingKeyFrames.KeyFrames;
            DiscreteColorKeyFrame discreteColorKeyFrame1 = new DiscreteColorKeyFrame();
            discreteColorKeyFrame1.KeyTime = KeyTime.FromTimeSpan( TimeSpan.FromMilliseconds( 0.0 ) );
            discreteColorKeyFrame1.Value = col1;
            keyFrames1.Add( ( ColorKeyFrame ) discreteColorKeyFrame1 );
            ColorKeyFrameCollection keyFrames2 = animationUsingKeyFrames.KeyFrames;
            DiscreteColorKeyFrame discreteColorKeyFrame2 = new DiscreteColorKeyFrame();
            discreteColorKeyFrame2.KeyTime = KeyTime.FromTimeSpan( TimeSpan.FromMilliseconds( 50.0 ) );
            discreteColorKeyFrame2.Value = col2;
            keyFrames2.Add( ( ColorKeyFrame ) discreteColorKeyFrame2 );
        }

        private void TryInvokeAnimationDone()
        {
            if ( IsAnimationEnabled && ( _errorAnimation.Return<Storyboard, ClockState>( ( Func<Storyboard, ClockState> ) ( a => a.GetCurrentState( ( FrameworkElement ) this ) ), ClockState.Stopped ) == ClockState.Active || _fillAnimation.Return<Storyboard, ClockState>( ( Func<Storyboard, ClockState> ) ( a => a.GetCurrentState( ( FrameworkElement ) this ) ), ClockState.Stopped ) == ClockState.Active ) )
                return;
            // ISSUE: reference to a compiler-generated field
            Action<ActiveOrderAnnotation> animationDone = AnimationDone;
            if ( animationDone == null )
                return;
            animationDone( this );
        }

        private class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<ActiveOrderAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( ActiveOrderAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                IAnnotationCanvas canvas = Annotation.GetCanvas(Annotation.AnnotationCanvas);
                double y1Coord = coordinates.Y1Coord;
                if ( !y1Coord.IsRealNumber() || canvas == null )
                    return;
                double other1 = Annotation.AnnotationRoot.ActualHeight / 2.0;
                double other2 = Math.Max(10.0, canvas.ActualWidth - coordinates.X1Coord);
                System.Windows.Shapes.Line line = Annotation._line;
                if ( !line.X1.DoubleEquals( 0.0 ) || !line.X2.DoubleEquals( other2 ) || ( !line.Y1.DoubleEquals( other1 ) || !line.Y2.DoubleEquals( other1 ) ) )
                {
                    line.X1 = 0.0;
                    line.X2 = other2;
                    line.Y1 = Annotation._line.Y2 = other1;
                    line.UpdateLayout();
                }
                double num1 = y1Coord - other1;
                double num2 = canvas.ActualWidth - Annotation.ActualWidth;
                Annotation.SetValue( Canvas.LeftProperty, ( object ) num2 );
                Annotation.SetValue( Canvas.TopProperty, ( object ) num1 );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return new Point[ 1 ]
                {
          new Point(coordinates.X1Coord, coordinates.Y1Coord)
                };
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                return ( coordinates.Y1Coord < 0.0 || coordinates.Y1Coord > canvas.ActualHeight ? 1 : ( coordinates.X1Coord > canvas.ActualWidth ? 1 : 0 ) ) == 0;
            }
        }
    }
}
