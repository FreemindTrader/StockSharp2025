using fx.Algorithm;
using fx.Common;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation;
using SciChart.Charting.DrawingTools.TradingAnnotations.Models;
using SciChart.Charting.DrawingTools.TradingAnnotations.ViewModels;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.Events;
using SciChart.Core.Utility.Mouse;
using SciChart.Data.Model;
using fx.Definitions;
using System;
using System.Collections;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using fx.Bars;

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    public class MyTradingAnnotationBase : CompositeAnnotation, ITradingAnnotationEx, IfxImportantLevel
    {
        protected readonly PooledList<ComparablePoint> _cashedBasePoints              = new PooledList<ComparablePoint>( );
        public static readonly DependencyProperty IsMouseOverParentAnnotationProperty = DependencyProperty.RegisterAttached( "IsMouseOverParentAnnotation", typeof( bool ),                                  typeof( MyTradingAnnotationBase ), new PropertyMetadata( false, new PropertyChangedCallback( OnIsMouseOverCompositeAnnotationPropertyChanged ) ) );
        public static readonly DependencyProperty IsFilledAreaHitTestVisibleProperty  = DependencyProperty.Register( nameof( IsFilledAreaHitTestVisible ),  typeof( bool ),                                  typeof( MyTradingAnnotationBase ), new PropertyMetadata( false ) );
        public static readonly DependencyProperty MovingPartProperty                  = DependencyProperty.RegisterAttached( "MovingPart",                  typeof( bool ),                                  typeof( MyTradingAnnotationBase ), new PropertyMetadata( false ) );
        public static readonly DependencyProperty BasePointsCountProperty             = DependencyProperty.Register( nameof( BasePointsCount ),             typeof( int ),                                   typeof( MyTradingAnnotationBase ), new PropertyMetadata( 0 ) );
        public static readonly DependencyProperty StrokeThicknessProperty             = DependencyProperty.Register( nameof( StrokeThickness ),             typeof( double ),                                typeof( MyTradingAnnotationBase ), new PropertyMetadata( 1.0 ) );
        public static readonly DependencyProperty InitialBasePointsProperty           = DependencyProperty.Register( nameof( InitialBasePoints ),           typeof( ObservableCollection<ComparablePoint> ), typeof( MyTradingAnnotationBase ), new PropertyMetadata( null, new PropertyChangedCallback( OnBasePointsPropertyChanged ) ) );
        public static readonly DependencyProperty StrokeProperty                      = DependencyProperty.Register( nameof( Stroke ),                      typeof( Brush ),                                 typeof( MyTradingAnnotationBase ), new PropertyMetadata( Brushes.Green ) );
        public static readonly DependencyProperty LineStyleProperty                   = DependencyProperty.Register( nameof( LineStyle ),                   typeof( Style ),                                 typeof( MyTradingAnnotationBase ), new PropertyMetadata( null ) );
        private Point[ ] point_1;
        protected bool isReattached;
        protected bool _timeFrameWasChanged;
        private bool _isReadyToUpdateAfterUnload;
        PooledList< double > _selectedLines = new PooledList< double >( );

        static MyTradingAnnotationBase( )
        {
            IsEditableProperty.OverrideMetadata( typeof( MyTradingAnnotationBase ), new PropertyMetadata( false, new PropertyChangedCallback( OnIsEditableChanged ) ) );
        }

        public MyTradingAnnotationBase( ref SBar lastBar )
        {
            Annotations = new ObservableCollection<IAnnotation>( );
            DataContextChanged += ( sender, e ) => OnDataContextChanged( sender as DependencyObject, e );
            SetCurrentValue( InitialBasePointsProperty, new ObservableCollection<ComparablePoint>( ) );

            _lastBar = lastBar;

            LineGuid = Guid.NewGuid( );
        }

        public static void SetIsMouseOverParentAnnotation( AnnotationBase element, bool value )
        {
            element.SetValue( IsMouseOverParentAnnotationProperty, value );
        }

        public static bool GetIsMouseOverParentAnnotation( AnnotationBase element )
        {
            return ( bool )element.GetValue( IsMouseOverParentAnnotationProperty );
        }

        public static void SetMovingPart( DependencyObject element, bool value )
        {
            element.SetValue( MovingPartProperty, value );
        }

        public static bool GetMovingPart( DependencyObject element )
        {
            return ( bool )element.GetValue( MovingPartProperty );
        }

        public event EventHandler<AnnotationCreationArgs> AnnotationCreated;

        private static void OnIsEditableChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            MyTradingAnnotationBase tradingAnnotationBase = d as MyTradingAnnotationBase;

            if ( tradingAnnotationBase == null )
            {
                return;
            }

            foreach ( AnnotationBase annotation in tradingAnnotationBase.Annotations )
            {
                if ( annotation != null )
                {
                    Cursor cursor = ( bool )e.NewValue ? Cursors.SizeAll : Cursors.Arrow;
                    annotation.SetCurrentValue( CursorProperty, cursor );
                }
            }
        }

        protected new Cursor GetSelectedCursor( )
        {
            return Cursors.SizeNS;
        }

        public int BasePointsCount
        {
            get
            {
                return ( int )GetValue( BasePointsCountProperty );
            }
            protected set
            {
                SetValue( BasePointsCountProperty, value );
            }
        }

        public ObservableCollection<ComparablePoint> InitialBasePoints
        {
            get
            {
                return ( ObservableCollection<ComparablePoint> )GetValue( InitialBasePointsProperty );
            }
            set
            {
                SetValue( InitialBasePointsProperty, value );
            }
        }

        public bool IsFilledAreaHitTestVisible
        {
            get
            {
                return ( bool )GetValue( IsFilledAreaHitTestVisibleProperty );
            }
            set
            {
                SetValue( IsFilledAreaHitTestVisibleProperty, value );
            }
        }

        public bool IsCreated
        {
            get;
            private set;
        }

        public Brush Stroke
        {
            get
            {
                return ( Brush )GetValue( StrokeProperty );
            }
            set
            {
                SetValue( StrokeProperty, value );
            }
        }

        public Style LineStyle
        {
            get
            {
                return ( Style )GetValue( LineStyleProperty );
            }
            set
            {
                SetValue( LineStyleProperty, value );
            }
        }

        public double StrokeThickness
        {
            get
            {
                return ( double )GetValue( StrokeThicknessProperty );
            }
            set
            {
                SetValue( StrokeThicknessProperty, value );
            }
        }

        public IEnumerable<IAnnotation> MovingLinesPartAnnotations
        {
            get
            {
                return Annotations.Where( anno =>
                                                             {
                                                                 if ( GetMovingPart( ( DependencyObject ) anno ) )
                                                                 {
                                                                     return anno is LineAnnotation;
                                                                 }
                                                                 return false;
                                                             } 
                                                     );
            }
        }

        protected bool IsReadyToUpdateAfterUnload
        {
            get
            {
                return _isReadyToUpdateAfterUnload;
            }
        }

        public virtual void SetBasePoint( IComparable x, IComparable y )
        {
            _cashedBasePoints.Add( new ComparablePoint( x, y ) );
            if ( InitialBasePoints == null || IsAttached )
            {
                return;
            }
            InitialBasePoints.Add( new ComparablePoint( x, y ) );
        }

        public virtual void UpdateBasePoint( int index, IComparable x, IComparable y )
        {
            if ( InitialBasePoints == null || IsAttached || ( index < 0 || index >= InitialBasePoints.Count ) )
            {
                return;
            }
            ComparablePoint initialBasePoint = InitialBasePoints[ index ];
            initialBasePoint.X = x;
            initialBasePoint.Y = y;
            InitialBasePoints[ index ] = initialBasePoint;
        }

        public override void Update( ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc )
        {
            base.Update( xCalc, yCalc );
            if ( isReattached || !IsLoaded )
            {
                return;
            }
            _isReadyToUpdateAfterUnload = false;
        }

        protected virtual void OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( isReattached )
            {
                _isReadyToUpdateAfterUnload = true;
                isReattached = false;
            }

            UpdateInitialBasePoints( );
        }

        private static void OnBasePointsPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            MyTradingAnnotationBase tradingAnnotationBase = ( MyTradingAnnotationBase )d;
            ObservableCollection<ComparablePoint> newValue = ( ObservableCollection<ComparablePoint> )e.NewValue;
            if ( newValue != null )
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( tradingAnnotationBase.OnBasePointsCollectionChanged );
            }
            ObservableCollection<ComparablePoint> oldValue = ( ObservableCollection<ComparablePoint> )e.OldValue;
            if ( oldValue == null )
            {
                return;
            }
            oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( tradingAnnotationBase.OnBasePointsCollectionChanged );
        }

        private void OnBasePointsCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            IList newItems = e.NewItems;
            if ( !IsLoaded || !IsAttached )
            {
                return;
            }
            foreach ( ComparablePoint comparablePoint in newItems )
            {
                SetBasePoint( comparablePoint.X, comparablePoint.Y );
            }
        }

        private void UpdateInitialBasePoints( )
        {
            if ( !IsLoaded || !IsAttached )
            {
                return;
            }

            foreach ( ComparablePoint initialBasePoint in InitialBasePoints )
            {
                SetBasePoint( initialBasePoint.X, initialBasePoint.Y );
            }
        }

        public virtual void UpdatePolygonById( string id )
        {
        }

        protected IComparable GetYDataValue( double y )
        {
            return YAxis.GetDataValue( y );
        }

        protected IComparable GetXDataValue( double x )
        {
            return XAxis.GetDataValue( x );
        }

        protected void UpdateTextAnnotationBasePoint( IAnnotation textAnnotation, Point currentBasePoint, Point previousBasePoint, Point nextBasePoint )
        {
            if ( textAnnotation == null )
            {
                return;
            }
            IComparable comparable1 = FromCoordinate( currentBasePoint.Y, YAxis );
            IComparable comparable2 = FromCoordinate( currentBasePoint.X, XAxis );
            TranslateTransform renderTransform = ( TranslateTransform )( ( UIElement )textAnnotation ).RenderTransform;
            renderTransform.X = 0.0;
            renderTransform.Y = 0.0;
            renderTransform.X = -textAnnotation.ActualWidth / 2.0;
            if ( previousBasePoint.Y > currentBasePoint.Y || nextBasePoint.Y > currentBasePoint.Y )
            {
                renderTransform.Y = -textAnnotation.ActualHeight - 2.0;
            }
            textAnnotation.X1 = comparable2;
            textAnnotation.Y1 = comparable1;
        }

        public void SetOnTop( bool isMouseOver = false )
        {
            int num = IsSelected ? 9 : 0;
            Panel.SetZIndex( this, isMouseOver ? 10 : num );
        }

        protected override void OnAnnotationPointerPressed( ModifierMouseArgs e )
        {
            point_1 = GetBasePoints( SavedCoordinates );
            base.OnAnnotationPointerPressed( e );
        }

        protected override void MoveBasePointTo( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            SetBasePoint( newPoint, index, xAxis, yAxis );
        }

        protected override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizOffset, double vertOffset )
        {
            Point[ ] point1 = point_1;
            for ( int index = 0; index < point1.Length; ++index )
            {
                Point newPoint = point1[ index ];
                newPoint.X += horizOffset;
                newPoint.Y += vertOffset;
                MoveBasePointTo( newPoint, index, XAxis, YAxis );
            }
            OnAnnotationDragging( new AnnotationDragDeltaEventArgs( horizOffset, vertOffset ) );
        }

        protected override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
        {
            IAnnotation[ ] array = MovingLinesPartAnnotations.ToArray( );
            int num = array.Count( );
            Point[ ] pointArray = new Point[ num + 1 ];
            for ( int index = 0; index < num; ++index )
            {
                IAnnotation annotation = array[ index ];
                double coordinate1 = XAxis.GetCoordinate( annotation.X1 );
                double coordinate2 = YAxis.GetCoordinate( annotation.Y1 );
                pointArray[ index ] = new Point( coordinate1, coordinate2 );
                if ( index == num - 1 )
                {
                    double coordinate3 = XAxis.GetCoordinate( annotation.X2 );
                    double coordinate4 = YAxis.GetCoordinate( annotation.Y2 );
                    pointArray[ index + 1 ] = new Point( coordinate3, coordinate4 );
                }
            }
            return pointArray;
        }

        public ComparablePoint[ ] GetBaseDataValues( )
        {
            Point[ ] basePoints = GetBasePoints( );
            ComparablePoint[ ] comparablePointArray = new ComparablePoint[ basePoints.Length ];
            for ( int index = 0; index < basePoints.Count( ); ++index )
            {
                Point point = basePoints[ index ];
                IComparable dataValue1 = XAxis.GetDataValue( point.X );
                IComparable dataValue2 = YAxis.GetDataValue( point.Y );
                comparablePointArray[ index ] = new ComparablePoint( dataValue1, dataValue2 );
            }
            return comparablePointArray;
        }

        protected virtual void SetBasePoint( IComparable xDataValue, IComparable yDataValue, int index )
        {
        }

        public void SetCashedBasePoints( )
        {
            if ( !XAxis.IsCategoryAxis )
            {
                return;
            }
            _timeFrameWasChanged = true;
            for ( int index = 0; index < _cashedBasePoints.Count; ++index )
            {
                ComparablePoint cashedBasePoint = _cashedBasePoints[ index ];
                SetBasePoint( cashedBasePoint.X, cashedBasePoint.Y, index );
            }
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            if ( !xAxis.IsCategoryAxis )
            {
                return;
            }
            ComparablePoint comparablePoint = new ComparablePoint( XAxis.GetDataValue( newPoint.X ), YAxis.GetDataValue( newPoint.Y ) );
            _cashedBasePoints[ index ] = comparablePoint;
        }

        protected void OnAnnotationCreated( )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<AnnotationCreationArgs> eventHandler6 = AnnotationCreated;
            if ( eventHandler6 != null )
            {
                eventHandler6( this, new AnnotationCreationArgs( this ) );
            }
            IsCreated = true;
        }

        public override void OnAttached( )
        {
            base.OnAttached( );
            Selected += new EventHandler( OnAnnotationSelected );
            Unselected += new EventHandler( OnAnnotationSelected );
            Loaded += new RoutedEventHandler( OnLoaded );
            isReattached = Annotations.Any( );
            IsCreated = isReattached;
        }

        public override void OnDetached( )
        {
            base.OnDetached( );
            Selected -= new EventHandler( OnAnnotationSelected );
            Unselected -= new EventHandler( OnAnnotationSelected );
            Loaded -= new RoutedEventHandler( OnLoaded );
        }

        private void OnAnnotationSelected( object sender, EventArgs e )
        {
            SetOnTop( false );
        }

        private static void OnIsMouseOverCompositeAnnotationPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
        {
            ( ( MyTradingAnnotationBase )( ( AnnotationBase )d ).ParentAnnotation ).SetOnTop( ( bool )e.NewValue );
        }

        private static void OnDataContextChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e )
        {
            ITradingAnnotation tradingAnnotation = ( ITradingAnnotation )d;
            if ( tradingAnnotation == null || !( e.NewValue is ITradingAnnotationViewModel newValue ) )
            {
                return;
            }
            newValue.Annotation = tradingAnnotation;
        }

        protected override void DetachAnnotation( IAnnotation item )
        {
            base.DetachAnnotation( item );
            AnnotationBase annotationBase = item as AnnotationBase;
            if ( annotationBase != null )
            {
                BindingOperations.ClearBinding( annotationBase, ContextMenuProperty );
                BindingOperations.ClearBinding( annotationBase, DataContextProperty );
            }
            LineAnnotation lineAnnotation = item as LineAnnotation;
            if ( lineAnnotation != null )
            {
                BindingOperations.ClearBinding( lineAnnotation, LineAnnotationBase.StrokeProperty );
                BindingOperations.ClearBinding( lineAnnotation, LineAnnotationBase.StrokeThicknessProperty );
            }
            PolygonAnnotation polygonAnnotation = item as PolygonAnnotation;
            if ( polygonAnnotation != null )
            {
                BindingOperations.ClearBinding( polygonAnnotation, IsHitTestVisibleProperty );
            }
            TextAnnotation textAnnotation = item as TextAnnotation;
            if ( textAnnotation == null )
            {
                return;
            }
            //textAnnotation.TextBox.SizeChanged -= new SizeChangedEventHandler( OnTextAnnotationSizeChanged );
        }

        protected override void AttachAnnotation( IAnnotation item )
        {
            base.AttachAnnotation( item );
            AnnotationBase annotationBase = item as AnnotationBase;
            if ( annotationBase != null )
            {
                Binding binding1 = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   ContextMenuProperty ) };
                annotationBase.SetBinding( ContextMenuProperty, binding1 );
                Binding binding2 = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   DataContextProperty ) };
                annotationBase.SetBinding( DataContextProperty, binding2 );
            }

            LineAnnotation lineAnnotation = item as LineAnnotation;
            if ( lineAnnotation != null )
            {
                Binding binding1 = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   StrokeProperty ) };
                lineAnnotation.SetBinding( LineAnnotationBase.StrokeProperty, binding1 );
                Binding binding2 = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   StrokeThicknessProperty ) };
                lineAnnotation.SetBinding( LineAnnotationBase.StrokeThicknessProperty, binding2 );
                Binding binding3 = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   LineStyleProperty ) };
                lineAnnotation.SetBinding( StyleProperty, binding3 );
            }
            TextAnnotation textAnnotation = item as TextAnnotation;
            if ( textAnnotation != null )
            {
                //textAnnotation.TextBox.SizeChanged += new SizeChangedEventHandler( OnTextAnnotationSizeChanged );
            }
            PolygonAnnotation polygonAnnotation = item as PolygonAnnotation;
            if ( polygonAnnotation != null )
            {
                Binding binding = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   IsFilledAreaHitTestVisibleProperty ) };
                polygonAnnotation.SetBinding( IsHitTestVisibleProperty, binding );
            }
            FibonacciTableAnnotation fibonacciTableAnnotation = item as FibonacciTableAnnotation;
            if ( fibonacciTableAnnotation == null )
            {
                return;
            }
            Binding binding4 = new Binding( ) { Source =   ( this ), Path = new PropertyPath(   FontSizeProperty ) };
            fibonacciTableAnnotation.SetBinding( FontSizeProperty, binding4 );
        }

        private void OnTextAnnotationSizeChanged( object sender, SizeChangedEventArgs e )
        {
            Refresh( );
        }

        public override void WriteXml( XmlWriter writer )
        {
            base.WriteXml( writer );
        }

        public override void ReadXml( XmlReader reader )
        {
            base.ReadXml( reader );
        }

        public bool IsLocked { get; set; }

        protected SBar      _lastBar     = default;

        public void UpdateLastX( ref SBar bar )
        {
            _lastBar = bar;

            foreach ( IAnnotation annotation in Annotations )
            {
                if ( annotation is fxFibonacciTableAnnotation )
                {
                    var a = ( fxFibonacciTableAnnotation )annotation;

                }
            }
        }

        private IList< SRlevel > _fibLevels;

        public IList<SRlevel> ImportantLines
        {
            get
            {
                return _fibLevels;
            }
            
            set
            {
                _fibLevels = value;
            }
        }

        public Guid LineGuid { get; set ; }

        
        

        public PooledList<double> GetSelectedLines( )
        {
            return _selectedLines;
        }

        public PooledList< double> HighlightedSelected( bool CTRLKEY, IRange xRange, PooledDictionary<string, IRange> yRange )
        {
            if ( !CTRLKEY )
            {
                _selectedLines.Clear( );
            }

            foreach ( IAnnotation annotation in Annotations )
            {
                if ( annotation is fxFibonacciTableAnnotation )
                {
                    var a = (fxFibonacciTableAnnotation)annotation;

                    var selected = a.HighLightSelected( CTRLKEY, xRange, yRange );

                    if ( selected.Count > 0 )
                    {
                        _selectedLines.AddRange( selected );
                    }
                }
            }

            return _selectedLines;
        }

        public void HighlightConfluence( double fibLine )
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                if ( annotation is fxFibonacciTableAnnotation )
                {
                    var a = (fxFibonacciTableAnnotation)annotation;

                    a.HighlightConfluence( fibLine );                    
                }
            }

        }

        public void HighlightComingSRLines( ref SBar bar, double fibLine )
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                if ( annotation is fxFibonacciTableAnnotation )
                {
                    var a = (fxFibonacciTableAnnotation)annotation;

                    a.HighlightComingSRLines( ref bar, fibLine );
                }
            }
        }
        

        public void HighlightSingleConfluence( double fibLine )
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                if ( annotation is fxFibonacciTableAnnotation )
                {
                    var a = (fxFibonacciTableAnnotation)annotation;

                    a.HighlightSingleConfluence( fibLine );
                }
            }

        }

        public void DimAllImportantLines()
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                if ( annotation is fxFibonacciTableAnnotation )
                {
                    var a = (fxFibonacciTableAnnotation)annotation;

                    a.DimAllImportantLines( );
                }
                else if ( annotation is LineAnnotation )
                {
                    var a = (LineAnnotation)annotation;

                    a.Opacity = 0.02;
                }
            }
        }

    }
}



