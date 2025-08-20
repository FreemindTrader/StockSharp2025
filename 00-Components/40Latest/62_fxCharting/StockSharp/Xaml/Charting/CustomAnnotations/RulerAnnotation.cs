using Ecng.Common;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.StrategyManager;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.Events;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace StockSharp.Xaml.Charting.CustomAnnotations
{
    public class RulerAnnotation : CustomAnnotation
    {
        public static readonly DependencyProperty RulerWidthProperty  = DependencyProperty.Register(nameof( RulerWidth), typeof (double), typeof (RulerAnnotation), new PropertyMetadata( 0.0));
        public static readonly DependencyProperty RulerHeightProperty = DependencyProperty.Register(nameof( RulerHeight), typeof (double), typeof (RulerAnnotation), new PropertyMetadata( 0.0));
        public static readonly DependencyProperty Text1Property       = DependencyProperty.Register(nameof( Text1), typeof (string), typeof (RulerAnnotation), new PropertyMetadata( null));
        public static readonly DependencyProperty Text2Property       = DependencyProperty.Register(nameof( Text2), typeof (string), typeof (RulerAnnotation), new PropertyMetadata( null));
        private static DispatcherTimer _timer;
        private double _priceStep;
        private int _numberOfDigits = 2;
        private bool _subscribed;
        private bool _needToUpdate;
        private bool _removed;
        private Grid _rulerArea;
        private Border _textBorder;

        static RulerAnnotation( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( RulerAnnotation ), new FrameworkPropertyMetadata( typeof( RulerAnnotation ) ) );
            BackgroundProperty.OverrideMetadata( typeof( RulerAnnotation ), new FrameworkPropertyMetadata( new SolidColorBrush( Colors.Cornsilk ) ) );
            OpacityProperty.OverrideMetadata( typeof( RulerAnnotation ), new FrameworkPropertyMetadata( 0.8 ) );
            AnnotationCanvasProperty.OverrideMetadata( typeof( RulerAnnotation ), new FrameworkPropertyMetadata( AnnotationCanvas.BelowChart ) );
        }

        public RulerAnnotation( )
        {
            DefaultStyleKey = typeof( RulerAnnotation );
            if ( _timer != null )
                return;
            _timer = new DispatcherTimer( )
            {
                Interval = TimeSpan.FromMilliseconds( 200.0 )
            };
            _timer.Start( );
            _timer.Tick += _timer_Tick;

            Background = new SolidColorBrush( Colors.Cornsilk );
            AnnotationCanvas = AnnotationCanvas.BelowChart;
        }

        private void _timer_Tick( object sender, EventArgs e )
        {
            Action timerTick = TimerTick;
            if ( timerTick == null )
                return;
            timerTick( );
        }



        public double RulerWidth
        {
            get
            {
                return ( double ) GetValue( RulerWidthProperty );
            }
            set
            {
                SetValue( RulerWidthProperty, value );
            }
        }

        public double RulerHeight
        {
            get
            {
                return ( double ) GetValue( RulerHeightProperty );
            }
            set
            {
                SetValue( RulerHeightProperty, value );
            }
        }

        public string Text1
        {
            get
            {
                return ( string ) GetValue( Text1Property );
            }
            set
            {
                SetValue( Text1Property, value );
            }
        }

        public string Text2
        {
            get
            {
                return ( string ) GetValue( Text2Property );
            }
            set
            {
                SetValue( Text2Property, value );
            }
        }

        private static event Action TimerTick;

        public double PriceStep
        {
            get
            {
                return _priceStep;
            }
            set
            {
                if ( value < 0.0 )
                    throw new ArgumentException( nameof( value ) );
                _priceStep = value;

                _numberOfDigits = value.ToString( System.Globalization.CultureInfo.InvariantCulture )
                                        .TrimEnd('0') 
                                        .SkipWhile( c => c != '.' )
                                        .Skip( 1 )
                                        .Count( );

                _needToUpdate = true;
            }
        }

        public bool RemoveOnClick { get; set; }

        public override void OnApplyTemplate( )
        {
            base.OnApplyTemplate( );
            AnnotationRoot = GetAndAssertTemplateChild<Grid>( "PART_AnnotationRoot" );
            _rulerArea     = GetAndAssertTemplateChild<Grid>( "PART_RulerArea" );
            _textBorder    = GetAndAssertTemplateChild<Border>( "PART_TextBorder" );
        }

        protected override Cursor GetSelectedCursor( )
        {
            return null;
        }

        protected override void MakeInvisible( )
        {
            base.MakeInvisible( );
            if ( !_subscribed )
                return;
            _subscribed = false;
            TimerTick -= new Action( TimerOnTick );
        }

        protected override void MakeVisible( AnnotationCoordinates coordinates )
        {
            base.MakeVisible( coordinates );
            if ( _subscribed )
                return;
            _subscribed = true;
            TimerTick += new Action( TimerOnTick );
            _needToUpdate = true;
        }

        protected override void AttachInteractionHandlersTo( FrameworkElement source )
        {
            _removed = false;
            ( ( UIElement ) ParentSurface ).PreviewMouseDown += new MouseButtonEventHandler( OnSurfacePreviewMouseDown );
        }

        protected override void DetachInteractionHandlersFrom( FrameworkElement source )
        {
            TryRemoveSelf( );
        }

        private void OnSurfacePreviewMouseDown( object sender, MouseButtonEventArgs e )
        {
            TryRemoveSelf( );
        }

        private void TryRemoveSelf( )
        {
            if ( _removed || !RemoveOnClick )
                return;
            _removed = true;
            ( ( UIElement ) ParentSurface ).PreviewMouseDown -= new MouseButtonEventHandler( OnSurfacePreviewMouseDown );
            ParentSurface.Annotations.Remove( this );
        }

        private static string FormatDimeDiff( TimeSpan ts )
        {
            PooledList<string> parts = new PooledList<string>();
            if ( ts > TimeSpan.FromDays( 1.0 ) )
                parts.Add( ( ( int ) ts.TotalDays ).ToString( ) + LocalizedStrings.DaysLetter );
            if ( ts.Hours != 0 )
                parts.Add( ts.Hours.ToString( ) + LocalizedStrings.HoursLetter );
            if ( ts.Minutes != 0 )
                parts.Add( ts.Minutes.ToString( ) + LocalizedStrings.MinutesLetter );
            if ( ts.Seconds != 0 )
                parts.Add( ts.Seconds.ToString( ) + LocalizedStrings.SecondsLetter );
            return parts.Join( " " );
        }

        private void UpdateData( )
        {
            _needToUpdate = false;

            var xCalc = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator;

            int x1Value = 0;
            int x2Value = 0;

            if ( X1 is DateTime )
            {
                x1Value = xCalc.TransformDataToIndex( X1 );
            }
            else if (X1 is int )
            {
                x1Value = ( int ) X1;
            }

            if ( X2 is DateTime )
            {
                x2Value = xCalc.TransformDataToIndex( X2 );
            }
            else if ( X2 is int )
            {
                x2Value = ( int ) X2;
            }

            if( ( Y1 == null ) || ( Y2 == null ) )
                return;

            
            double y1Pips = Math.Round( ((double) Y1) / _priceStep ) * _priceStep;
            double y2Pips = Math.Round( ((double) Y2) / _priceStep ) * _priceStep;

            

            if ( x1Value > x2Value )
            {
                var temp = x1Value;
                x1Value = x2Value;
                x2Value = temp;

            }

            if ( y1Pips > y2Pips )
            {
                var temp = y1Pips;
                y1Pips = y2Pips;
                y2Pips = temp;
            }

            double pipDiff = y2Pips - y1Pips;
            int pips = (int) Math.Round( pipDiff / _priceStep);
            string str =  null;

            if ( xCalc != null )
            {
                DateTime x1DateTime = (DateTime) xCalc.TransformIndexToData( x1Value );
                DateTime x2DateTime = (DateTime) xCalc.TransformIndexToData( x2Value );
                str = FormatDimeDiff( x2DateTime - x1DateTime );
            }

            var diff = Math.Round( pipDiff, _numberOfDigits );

            Text1 = string.Format( "{0}, {1} {2}", diff, pips, LocalizedStrings.Pips );
            Text2 = string.Format( "{0}: {1}, {2}", LocalizedStrings.Bars, x2Value - x1Value, str );
        }

        private void TimerOnTick( )
        {
            if ( !_needToUpdate || !IsAttached )
                return;
            UpdateData( );
            Refresh( );
        }

        public override bool IsPointWithinBounds( Point point )
        {
            AnnotationCoordinates coordinates = GetCoordinates( GetCanvas( AnnotationCanvas ), XAxis.GetCurrentCoordinateCalculator(), YAxis.GetCurrentCoordinateCalculator() );
            return new Rect( new Point( coordinates.X1Coord, coordinates.Y1Coord ), new Point( coordinates.X2Coord, coordinates.Y2Coord ) ).Contains( point );
        }

        protected override IComparable FromCoordinate( double coord, IAxis axis )
        {
            IComparable comparable = base.FromCoordinate(coord, axis);
            if ( axis != YAxis )
                return comparable;
            double num = (double) comparable;
            return _priceStep > 0.0 ? Math.Round( num / _priceStep ) * _priceStep : num;
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy( )
        {
            return new RulerPlacementStrategy( this );
        }

        public class RulerPlacementStrategy : IAnnotationPlacementStrategy
        {
            RulerAnnotation _rulerAnnotation;
            private readonly ITransformationStrategy _transformStrategy;
            public RulerPlacementStrategy( RulerAnnotation rulerAnnotation )
            {
                _rulerAnnotation = rulerAnnotation;

                _transformStrategy = rulerAnnotation.Services.GetService<IStrategyManager>( ).GetTransformationStrategy( );
            }

            public void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                _rulerAnnotation.PlaceAnnotationImpl( coordinates );
            }

            public Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return null;
            }

            public void SetBasePoint( Point newPoint, int index )
            {
                _rulerAnnotation.SetBasePoint( _transformStrategy.Transform( newPoint ), index, _rulerAnnotation.XAxis, _rulerAnnotation.YAxis );
            }

            public bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                return ( coordinates.X1Coord < 0.0 && coordinates.X2Coord < 0.0 || coordinates.X1Coord > canvas.ActualWidth && coordinates.X2Coord > canvas.ActualWidth || coordinates.Y1Coord < 0.0 && coordinates.Y2Coord < 0.0 ? 1 : ( coordinates.Y1Coord <= canvas.ActualHeight ? 0 : ( coordinates.Y2Coord > canvas.ActualHeight ? 1 : 0 ) ) ) == 0;
            }


            public void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas )
            {
                InternalMoveAnntotationTo( coordinates, ref horizontalOffset, ref verticalOffset, annotationCanvas );
                _rulerAnnotation.OnAnnotationDragging( new AnnotationDragDeltaEventArgs( horizontalOffset, verticalOffset ) );
            }

            private void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, ref double horizOffset, ref double vertOffset, IAnnotationCanvas canvas )
            {
                double num1 = coordinates.X1Coord + horizOffset;
                double num2 = coordinates.X2Coord + horizOffset;
                double num3 = coordinates.Y1Coord + vertOffset;
                double num4 = coordinates.Y2Coord + vertOffset;
                if ( !IsCoordinateValid( num1, canvas.ActualWidth ) || !IsCoordinateValid( num3, canvas.ActualHeight ) || ( !IsCoordinateValid( num2, canvas.ActualWidth ) || !IsCoordinateValid( num4, canvas.ActualHeight ) ) )
                {
                    double val1_1 = double.IsNaN(num1) ? 0.0 : num1;
                    double val2_1 = double.IsNaN(num2) ? 0.0 : num2;
                    double val1_2 = double.IsNaN(num3) ? 0.0 : num3;
                    double val2_2 = double.IsNaN(num4) ? 0.0 : num4;
                    if ( Math.Max( val1_1, val2_1 ) < 0.0 )
                    {
                        horizOffset -= Math.Max( val1_1, val2_1 );
                    }

                    if ( Math.Min( val1_1, val2_1 ) > canvas.ActualWidth )
                    {
                        horizOffset -= Math.Min( val1_1, val2_1 ) - ( canvas.ActualWidth - 1.0 );
                    }

                    if ( Math.Max( val1_2, val2_2 ) < 0.0 )
                    {
                        vertOffset -= Math.Max( val1_2, val2_2 );
                    }

                    if ( Math.Min( val1_2, val2_2 ) > canvas.ActualHeight )
                    {
                        vertOffset -= Math.Min( val1_2, val2_2 ) - ( canvas.ActualHeight - 1.0 );
                    }
                }
                coordinates.X1Coord += horizOffset;
                coordinates.X2Coord += horizOffset;
                coordinates.Y1Coord += vertOffset;
                coordinates.Y2Coord += vertOffset;
                _rulerAnnotation.SetBasePoint( new Point( coordinates.X1Coord, coordinates.Y1Coord ), 0, _rulerAnnotation.XAxis, _rulerAnnotation.YAxis );
                _rulerAnnotation.SetBasePoint( new Point( coordinates.X2Coord, coordinates.Y2Coord ), 2, _rulerAnnotation.XAxis, _rulerAnnotation.YAxis );
            }

            protected bool IsCoordinateValid( double coord, double canvasMeasurement )
            {
                return _rulerAnnotation.IsCoordinateValid( coord, canvasMeasurement );
            }
        }

        public void PlaceAnnotationImpl( AnnotationCoordinates coord )
        {
            UpdateData( );
            double x1Coor = coord.X1Coord;
            double x2Coor = coord.X2Coord;
            double y1Coor = coord.Y1Coord;
            double y2Coor = coord.Y2Coord;

            _textBorder.Measure( new Size( double.PositiveInfinity, double.PositiveInfinity ) );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             *  Tony - This bug is so stupid.             
             * 
             *  var temp = x1Coor;
             *  x2Coor = x1Coor;
             *  x1Coor = temp;
             *  
             *  It should be like 
             *  
             *  var temp = x1Coor;
             *  x1Coor = x2Coor;
             *  x2Coor = temp.
             *  
             *  LOL, so fucked up.
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            if ( x2Coor < x1Coor )
            {
                x1Coor = coord.X2Coord;
                x2Coor = coord.X1Coord;
            }

            if ( y2Coor < y1Coor )
            {
                y1Coor = coord.Y2Coord;
                y2Coor = coord.Y1Coord;
            }

            RulerWidth = x2Coor - x1Coor;
            RulerHeight = y2Coor - y1Coor;
            double rulerWidth = RulerWidth;
            Size desiredSize = _textBorder.DesiredSize;
            double width = desiredSize.Width;
            if ( rulerWidth < width )
            {
                double num1 = x1Coor;
                desiredSize = _textBorder.DesiredSize;
                double num2 = (desiredSize.Width - RulerWidth) / 2.0;
                x1Coor = num1 - num2;
            }

            Canvas.SetLeft( this, x1Coor );
            Canvas.SetTop( this, y1Coor );
            Canvas.SetRight( this, x2Coor );
            Canvas.SetBottom( this, y2Coor );
        }
    }
}

