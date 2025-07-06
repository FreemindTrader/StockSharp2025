using SciChart.Charting.ChartModifiers;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Extensions;
using SciChart.Core.Framework;
using SciChart.Core.Utility.Mouse;
using StockSharp.Xaml.Charting.HewFibonacci;
using System;
using System.Windows;
using MoreLinq;
using StockSharp.Xaml.Charting.CustomAnnotations;
using System.Windows.Media;
using Ecng.Xaml;
using SciChart.Charting.Numerics.CoordinateCalculators;
using StockSharp.Localization;
using Ecng.Common;
using System.Runtime.InteropServices;
using Ecng.Collections;
using SciChart.Charting;
using SciChart.Charting.Common;
using System.CodeDom.Compiler;
using System.Collections.Generic; 
using fx.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using StockSharp.Xaml.Charting.Definitions;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.DrawingTools.TradingModifiers;
using SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation;
using DevExpress.Mvvm;
using fx.Definitions;
using StockSharp.Algo.Candles;
using DevExpress.Xpf.Bars;
using StockSharp.Xaml;
using SciChart.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Xaml;
using DevExpress.Mvvm.Native;

namespace StockSharp.Xaml.Charting
{
    /// <summary>
    /// Interaction logic for FxAnnotationModifier.xaml
    /// </summary>

    /* -----------------------------------------------------------------------------------------------------------------------------------------------------------------
     * 
     * 1)   In the ChartPanel, clicking on any on Annotation will cause Chart's AnnotationType to be changed.
     *      IsChecked="{Binding ElementName=Chart, Path=AnnotationType, Converter={StaticResource EnumBooleanConverter}, ConverterParameter=TextAnnotation}"
     * 
     * 2)   Chart's AnnotationType is bind to AnnotationModifier's UserAnnotationTypeProperty dependency property
     *      AnnotationModifier.SetBindings( ultrachartannotationmodifier.UserAnnotationTypeProperty, chart, "AnnotationType", BindingMode.TwoWay, null, null );
     *      
     * 3)   Clicking on different Annotation Button on the UI will cause OnUserAnnotationTypePropertyChanged to be invoked.     
     * 
     * ----------------------------------------------------------------------------------------------------------------------------------------------------------------- */

    public partial class FxAnnotationModifier : fxAnnotationCreationModifier
    {        
        #region variables
        private readonly PairSet<AnnotationBase, ChartAnnotation> _baseToAnnotationPair = new PairSet<AnnotationBase, ChartAnnotation>( );
        private readonly UltrachartAnnotationEditor            _annotationEditor     = new UltrachartAnnotationEditor( );
        private readonly PooledSet<AnnotationBase>             _annotationBaseSet    = new PooledSet<AnnotationBase>( );

        private readonly AnnotationCollection                  _annotationCollection;
        private RulerAnnotation                                _rulerAnnotation;
        private readonly ChartArea                             _chartArea;        
        private bool                                           _isUpdating;

        //private bool _oneClickAnnotation = true;
        #endregion
        public FxAnnotationModifier( )
        {
            InitializeComponent( );
        }

        public FxAnnotationModifier( ChartArea area, AnnotationCollection collection )
        {
            InitializeComponent( );

            if ( area == null )
            {
                throw new ArgumentNullException( "area" );
            }

            _chartArea = area;


            if ( collection == null )
            {
                throw new ArgumentNullException( "annotations" );
            }

            _annotationCollection = collection;            
        }

        static FxAnnotationModifier( )
        {
            AnnotationTypeProperty.OverrideMetadata( typeof( FxAnnotationModifier ), new PropertyMetadata( null, new PropertyChangedCallback( OnAnnotationTypeChanged ) ) );
        }


        #region Change of Annotation Type
        public static readonly DependencyProperty UserAnnotationTypeProperty = DependencyProperty.Register( nameof( UserAnnotationType ), typeof( ChartAnnotationTypes ), typeof( FxAnnotationModifier ), new PropertyMetadata( ChartAnnotationTypes.None, new PropertyChangedCallback( OnUserAnnotationTypePropertyChanged ) ) );

        
        public ChartAnnotationTypes UserAnnotationType
        {
            get
            {
                return ( ChartAnnotationTypes ) GetValue( UserAnnotationTypeProperty );
            }
            set
            {
                SetValue( UserAnnotationTypeProperty, value );
            }
        }

        private IChart ChartArea
        {
            get
            {
                return _chartArea.Chart;
            }
        }

        private static void OnUserAnnotationTypePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( FxAnnotationModifier ) d ).SetAnnotationStyleAndType( ( ChartAnnotationTypes ) e.NewValue );
        }

        private void SetAnnotationStyleAndType( ChartAnnotationTypes annotationTypes )
        {
            if ( annotationTypes == ChartAnnotationTypes.None )
            {
                Ecng.Collections.CollectionHelper.ForEach( _annotationCollection, i => i.IsEditable = true );
                
                AnnotationType = null;
                IsEnabled = false;
            }
            else
            {
                Type type = ExtensionHelper2.GetType( annotationTypes );

                string str = type.Name + "Style";

                if ( Resources.Contains( str ) )
                {
                    AnnotationStyle = ( Style ) Resources[ str ];
                }

                AnnotationType = type;
                Annotation     = null;
                IsEnabled      = true;

                //if ( annotationTypes == ChartAnnotationTypes.HorizontalLineAnnotation || annotationTypes == ChartAnnotationTypes.VerticalLineAnnotation )
                //{
                //    _oneClickAnnotation = true;
                //}
                //else
                //{
                //    _oneClickAnnotation = false;
                //}
                
            }
        }
        #endregion



        private Point _lastClick;
        private int _clickCount;
        private bool _isMouseDown;

        public event EventHandler<AnnotationCreationArgs> fxTraderAnnotationCreated;

        

        protected override void OnIsEnabledChanged( )
        {
            Annotation = null;
        }

        

        
        

        protected override AnnotationBase CreateAnnotation( Type annotationType, Style annotationStyle )
        {
            if ( annotationType == typeof( RulerAnnotation ) )
            {
                RemoveRulerAnnotation( );
                double num = (double) ( ( ( _chartArea.Chart.GetSource( _chartArea.Elements.OfType<CandlestickUI>().FirstOrDefault() ) as CandleSeries )?.Security?.PriceStep ) ?? new Decimal( 1, 0, 0, false, 2 ) );
                RulerAnnotation ruler = new RulerAnnotation();
                ruler.YAxisId = YAxisId;
                ruler.XAxisId = XAxisId;
                ruler.PriceStep = num;
                ruler.RemoveOnClick = true;

                _rulerAnnotation = ruler;

                ParentSurface.Annotations.Add( ruler );
                return ruler;
            }
            else
            {
                AnnotationBase instance = ( AnnotationBase )Activator.CreateInstance( annotationType );

                instance.YAxisId = YAxisId;
                instance.XAxisId = XAxisId;

                if ( annotationStyle != null && annotationStyle.TargetType == annotationType )
                {
                    Style style = new Style( annotationType ) { BasedOn = annotationStyle };
                    instance.Style = style;
                }                
                                
                ParentSurface.Annotations.Add( instance );
                return instance;
            }            
        }

        private void RemoveRulerAnnotation( )
        {
            if ( _rulerAnnotation == null )
                return;
            ParentSurface.Annotations.Remove( _rulerAnnotation );
            _rulerAnnotation = null;
        }

        #region Mouse Movement 
        public override void OnModifierMouseUp( ModifierMouseArgs mouseButtonEventArgs )
        {
            if ( AnnotationType == null || !MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, ExecuteOn ) || !mouseButtonEventArgs.IsMaster )
            {
                return;
            }

            if ( !_isMouseDown )
            {
                OnModifierMouseDown( mouseButtonEventArgs );
                ParentSurface.Annotations.ForEachDo( iannotation_0 => iannotation_0.IsSelected = false );
            }

            if ( Annotation == null )
            {
                _lastClick = GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ModifierSurface );

                Annotation = CreateAnnotation( AnnotationType, AnnotationStyle );

                ITradingAnnotation annotation = Annotation as ITradingAnnotation;

                if ( annotation != null )
                {
                    annotation.AnnotationCreated += new EventHandler<AnnotationCreationArgs>( AdvancedAnnotationCreated );
                    IComparable xAxisDateTime     = XAxis.GetDataValue( _lastClick.X );
                    IComparable yAxisPrice        = YAxis.GetDataValue( _lastClick.Y );

                    annotation.SetBasePoint( xAxisDateTime, yAxisPrice );
                    _clickCount = 1;
                }
            }
            else
            {
                if ( Annotation is IfxFibonacciAnnotation annotation )
                {
                    _lastClick = GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ModifierSurface );

                    IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
                    IComparable yAxisPrice    = YAxis.GetDataValue( _lastClick.Y );

                    annotation.SetBasePoint( xAxisDateTime, yAxisPrice );

                    ++_clickCount;

                    if ( _clickCount >= annotation.BasePointsCount )
                    {
                        OnAnnotationCreated( );
                    }

                }
                else if ( Annotation is fxElliotWaveAnnotation ew )
                {
                    if ( _clickCount >= ew.BasePointsCount )
                    {
                        OnAnnotationCreated( );
                    }
                }
                else
                {
                    if ( Annotation != null && Annotation.IsAttached )
                    {
                        Annotation.IsSelected = false;
                    }

                    if ( _clickCount == 1 )
                    {
                        OnAnnotationCreated( );
                    }

                }
            }

            _isMouseDown = false;
        }

        public override void OnModifierMouseMove( ModifierMouseArgs mouseEventArgs )
        {
            if ( AnnotationType == null || Annotation == null || ( !Annotation.IsAttached || Annotation.IsSelected ) )
            {
                return;
            }

            /*
             * 
             * Transforms the input point relative to the SciChart.Core.Framework.IHitTestable element. 
             * Can be used to transform points relative to the SciChart.Charting.Visuals.SciChartSurfaceBase.ModifierSurface, or SciChart.Charting.Visuals.SciChartSurface.XAxis for instance.
             * 
             */

            Point pointRelativeTo = GetPointRelativeTo( mouseEventArgs.MousePoint, ModifierSurface );

            if ( Annotation is ITradingAnnotation annotation )
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( pointRelativeTo.X );
                IComparable yAxisPrice    = YAxis.GetDataValue( pointRelativeTo.Y );

                annotation.UpdateBasePoint( _clickCount, xAxisDateTime, yAxisPrice );
            }
            else if ( Annotation is fxElliotWaveAnnotation ew )
            {

            }
            else
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( pointRelativeTo.X );
                IComparable yAxisPrice    = YAxis.GetDataValue( pointRelativeTo.Y );

                Annotation.X2 = xAxisDateTime;
                Annotation.Y2 = yAxisPrice;
            }
        }

        public override void OnModifierMouseDown( ModifierMouseArgs mouseButtonEventArgs )
        {
            if ( AnnotationType == null || ( !MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, ExecuteOn ) || !mouseButtonEventArgs.IsMaster ) )
            {
                return;
            }

            _lastClick = GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ModifierSurface );

            _isMouseDown = true;

            if ( Annotation is fxElliotWaveAnnotation ew )
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
                IComparable yAxisPrice = YAxis.GetDataValue( _lastClick.Y );
                ew.SetBasePoint( xAxisDateTime, yAxisPrice );

                _clickCount++;
            }

            if ( Annotation != null )
            {
                return;
            }

            
            var instance = CreateAnnotation( AnnotationType, AnnotationStyle );
            Annotation = instance;

            if ( instance is ITradingAnnotation annotation )
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
                IComparable yAxisPrice = YAxis.GetDataValue( _lastClick.Y );

                annotation.SetBasePoint( xAxisDateTime, yAxisPrice );

                _clickCount = 1;
            }
            else if ( instance is VerticalLineAnnotation )
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );

                ICategoryCoordinateCalculator coordCalc = XAxis.GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
                int index = coordCalc.TransformDataToIndex( xAxisDateTime );
                instance.X1 = index;

                OnAnnotationCreated( );
            }
            else if ( instance is HorizontalLineAnnotation )
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
                IComparable yAxisPrice    = YAxis.GetDataValue( _lastClick.Y );

                instance.X1 = xAxisDateTime;
                instance.Y1 = yAxisPrice;

                OnAnnotationCreated( );
            }
            else
            {
                IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
                IComparable yAxisPrice    = YAxis.GetDataValue( _lastClick.Y );

                instance.X1 = xAxisDateTime;
                instance.Y1 = yAxisPrice;

                _clickCount = 1;
            }
        }

        #endregion

        private void AdvancedAnnotationCreated( object sender, AnnotationCreationArgs annotationCreationArgs )
        {
            ITradingAnnotation tradingAnnotation = sender as ITradingAnnotation;

            if ( tradingAnnotation != null )
            {
                tradingAnnotation.AnnotationCreated -= new EventHandler<AnnotationCreationArgs>( AdvancedAnnotationCreated );
            }

            fxTraderAnnotationCreated?.Invoke( this, new AnnotationCreationArgs( ( AnnotationBase ) Annotation ) );

            OnAnnotationCreated( );
        }

        private void fxAnnotationCreationModifier_AnnotationCreated( object sender, AnnotationCreationArgs e )
        {
            var lastAnnotation    = ( AnnotationBase )Annotation;
            var justAddedAnnoType = UserAnnotationType;

            UserAnnotationType = ChartAnnotationTypes.None;
            AnnotationType = null;
            IsEnabled = false;


            if ( lastAnnotation == null )
            {
                return;
            }

            bool notRuler = !( lastAnnotation is RulerAnnotation);

            if ( lastAnnotation is IfxFibonacciAnnotation )
            {
                // Here we add the delete menu to the Class itself.
            }
            else
            {
                AddNewMenu( lastAnnotation, notRuler );
                AddDependencyProperties( lastAnnotation );
            }



            var chartAnnotation = new ChartAnnotation( )
            {
                Type = justAddedAnnoType
            };

            _baseToAnnotationPair.Add( lastAnnotation, chartAnnotation );
            _chartArea.Elements.Add( chartAnnotation );

            MayBe.Do(ChartArea, c =>
            {
                c.InvokeAnnotationCreatedEvent( chartAnnotation );
                c.InvokeAnnotationModifiedEvent( chartAnnotation, GetAnnotationData( lastAnnotation ) );
            } );



            if ( !lastAnnotation.IsSelected )
            {
                return;
            }

            Keyboard.Focus( lastAnnotation );

            ChartArea?.InvokeAnnotationSelectedEvent( chartAnnotation, GetAnnotationData( lastAnnotation ) );
        }

        private static void OnAnnotationTypeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            FxAnnotationModifier creationModifier = ( FxAnnotationModifier )d;

            var surface = creationModifier.ParentSurface;

            if ( surface == null ) return;

            if ( ( Type ) e.NewValue == typeof( BrushAnnotation ) )
            {
                surface.Annotations.ForEachDo( annotation =>
                {
                    annotation.IsSelected = false;
                    annotation.IsEditable = false;
                } );
            }
            else
            {
                surface.Annotations.ForEachDo( annotation => annotation.IsEditable = true );
            }
        }

        public void SetupAnnotation( ChartAnnotation annotation, ChartDrawData.sAnnotation data )
        {
            Struct0 s;
            //bool? nullable;

            if ( !_baseToAnnotationPair.TryGetKey( annotation, out s.b ) )
            {
                Type type    = ExtensionHelper2.GetType( annotation.Type );
                s.b = ( AnnotationBase ) Activator.CreateInstance( type );
                s.b.XAxisId = annotation.XAxisId;
                s.b.YAxisId = annotation.YAxisId;
                s.b.IsHidden = false;

                //AnnotationBase annotationBase0 = s.b;

                var hasAnno = ( data.IsEditable.GetValueOrDefault( ) & data.IsEditable.HasValue );
                AddNewMenu( s.b, hasAnno );
                AddDependencyProperties( s.b );

                _baseToAnnotationPair[ s.b ] = annotation;
                _annotationCollection.Add( s.b );

                ChartArea.InvokeAnnotationCreatedEvent( annotation );
            }

            s.sCalc = s.b.XAxis?.GetCurrentCoordinateCalculator( );

            _isUpdating = true;

            try
            {
                if ( data.IsVisible.HasValue )
                {
                    s.b.IsHidden = !data.IsVisible.Value;
                }

                if ( data.IsEditable.HasValue )
                {
                    AddNewMenu( s.b, data.IsEditable.Value );
                }

                if ( data.CoordinateMode.HasValue )
                {
                    s.b.CoordinateMode = data.CoordinateMode.Value;
                }

                IComparable x1 = GetYCoordinate( data.X1, ref s );
                IComparable x2 = GetYCoordinate( data.X2, ref s );
                IComparable y1 = GetYCoordinate( data.Y1 );
                IComparable y2 = GetYCoordinate( data.Y2 );

                if ( x1 != null )
                    s.b.X1 = x1;
                if ( x2 != null )
                    s.b.X2 = x2;
                if ( y1 != null )
                    s.b.Y1 = y1;
                if ( y2 != null )
                    s.b.Y2 = y2;

                if ( s.b is LineAnnotationBase lineAnno )
                {
                    if ( data.Stroke != null )
                        lineAnno.Stroke = data.Stroke;
                    if ( data.Thickness.HasValue )
                        lineAnno.StrokeThickness = data.Thickness.Value.Left;
                }
                else if ( s.b is HorizontalLineAnnotation hLine )
                {
                    if ( data.HorizontalAlignment.HasValue )
                    {
                        hLine.HorizontalAlignment = data.HorizontalAlignment.Value;
                    }

                    if ( data.LabelPlacement.HasValue )
                    {
                        hLine.LabelPlacement = data.LabelPlacement.Value;
                    }

                    if ( !data.ShowLabel.HasValue )
                        return;

                    hLine.ShowLabel = data.ShowLabel.Value;
                }
                else if ( s.b is VerticalLineAnnotation vLine )
                {
                    if ( data.VerticalAlignment.HasValue )
                        vLine.VerticalAlignment = data.VerticalAlignment.Value;
                    if ( data.LabelPlacement.HasValue )
                        vLine.LabelPlacement = data.LabelPlacement.Value;

                    if ( !data.ShowLabel.HasValue )
                        return;

                    vLine.ShowLabel = data.ShowLabel.Value;
                }
                else if ( s.b is BoxAnnotation box )
                {
                    if ( data.Fill != null )
                        box.Background = data.Fill;
                    if ( data.Stroke != null )
                        box.BorderBrush = data.Stroke;
                    if ( !data.Thickness.HasValue )
                        return;
                    box.BorderThickness = data.Thickness.Value;
                }
                else if ( s.b is TextAnnotation text )
                {
                    if ( data.Foreground != null )
                        text.Foreground = data.Foreground;
                    if ( data.Text != null )
                        text.Text = data.Text;
                    if ( data.Fill != null )
                        text.Background = data.Fill;
                    if ( data.Stroke != null )
                        text.BorderBrush = data.Stroke;
                    if ( !data.Thickness.HasValue )
                        return;
                    text.BorderThickness = data.Thickness.Value;
                }
                else
                {
                    RulerAnnotation ruler = s.b as RulerAnnotation;
                    if ( ruler == null || data.Fill == null )
                        return;
                    RulerAnnotation rulerAnnotation = ruler;
                    SolidColorBrush fill = data.Fill as SolidColorBrush;
                    Brush brush = fill != null ? new SolidColorBrush( fill.Color.ToTransparent( 50 ) ) : data.Fill;
                    rulerAnnotation.Background = brush;
                }
            }
            finally
            {
                _isUpdating = false;
                ChartArea?.InvokeAnnotationModifiedEvent( annotation, GetAnnotationData( s.b ) );
            }
        }

        private ChartDrawData.sAnnotation GetAnnotationData( AnnotationBase anno )
        {
            Struct1 s;

            s.b = anno;
            var data            = new ChartDrawData.sAnnotation( );
            s.sCalc = s.b.XAxis?.GetCurrentCoordinateCalculator( );

            data.IsVisible = new bool?( !s.b.IsHidden );
            data.IsEditable = new bool?( HasAnnotation( s.b ) );
            data.CoordinateMode = new AnnotationCoordinateMode?( s.b.CoordinateMode );

            data.X1 = IndexToData( s.b.X1, ref s );
            data.X2 = IndexToData( s.b.X2, ref s );
            data.Y1 = DataToIndex( s.b.Y1, ref s );
            data.Y2 = DataToIndex( s.b.Y2, ref s );

            if ( s.b is LineAnnotationBase lineAnno )
            {
                data.Stroke = lineAnno.Stroke;
                data.Thickness = new Thickness?( new Thickness( lineAnno.StrokeThickness ) );
            }
            else if ( s.b is HorizontalLineAnnotation hLine )
            {
                data.HorizontalAlignment = new HorizontalAlignment?( hLine.HorizontalAlignment );
                data.LabelPlacement = new LabelPlacement?( hLine.LabelPlacement );
                data.ShowLabel = new bool?( hLine.ShowLabel );
            }
            else if ( s.b is VerticalLineAnnotation vLine )
            {
                data.VerticalAlignment = new VerticalAlignment?( vLine.VerticalAlignment );
                data.LabelPlacement = new LabelPlacement?( vLine.LabelPlacement );
                data.ShowLabel = new bool?( vLine.ShowLabel );
            }
            else if ( s.b is BoxAnnotation box )
            {
                data.Fill = box.Background;
                data.Stroke = box.BorderBrush;
                data.Thickness = new Thickness?( box.BorderThickness );
            }
            else if ( s.b is TextAnnotation text )
            {
                data.Foreground = text.Foreground;
                data.Text = text.Text;
                data.Fill = text.Background;
                data.Stroke = text.BorderBrush;
                data.Thickness = new Thickness?( text.BorderThickness );
            }


            return data;
        }

        public void RemoveAnnotation( ChartAnnotation annotation )
        {
            AnnotationBase annotationBase;
            if ( !_baseToAnnotationPair.TryGetKey( annotation, out annotationBase ) )
            {
                return;
            }

            _annotationCollection.Remove( annotationBase );
            _baseToAnnotationPair.Remove( annotationBase );
            _chartArea.Elements.Remove( annotation );

            ChartArea?.InvokeAnnotationDeletedEvent( annotation );
        }





        public static IComparable IndexToData( IComparable input, ref Struct1 s )
        {
            if ( input == null )
                return null;

            if ( input is int )
            {
                int index = ( int )input;

                if ( !( s.sCalc is ICategoryCoordinateCalculator calculator ) )
                {
                    throw new InvalidOperationException( LocalizedStrings.UnexpectedCoordTypeParams.Put( "int" ) );
                }

                var datetimeData = (DateTime) calculator.TransformIndexToData( index );

                return new DateTimeOffset( datetimeData, TimeSpan.Zero );
            }

            if ( input is DateTime )
            {
                return new DateTimeOffset( ( DateTime ) input, TimeSpan.Zero );
            }

            if ( input is double )
            {
                return ( double ) input;
            }

            throw new InvalidOperationException( LocalizedStrings.UnexpectedCoordTypeParams.Put( input.GetType().Name ) );
        }

        internal static IComparable DataToIndex( IComparable input, ref Struct1 s )
        {
            if ( input == null )
            {
                return null;
            }

            if ( input is double )
            {
                double output = ( double )input;

                if ( s.b.CoordinateMode != AnnotationCoordinateMode.Relative && s.b.CoordinateMode != AnnotationCoordinateMode.RelativeY )
                {
                    return output.To<Decimal>( );
                }

                return output;
            }

            throw new InvalidOperationException( LocalizedStrings.UnexpectedCoordTypeParams.Put( input.GetType().Name ) );
        }

        internal static IComparable GetYCoordinate( IComparable input, ref Struct0 s )
        {
            if ( input == null )
            {
                return null;
            }

            if ( input is DateTimeOffset )
            {
                return ( ( DateTimeOffset ) input ).UtcDateTime;
            }

            if ( s.sCalc is ICategoryCoordinateCalculator && ( s.b.CoordinateMode == AnnotationCoordinateMode.Absolute || s.b.CoordinateMode == AnnotationCoordinateMode.RelativeY ) )
            {
                throw new InvalidOperationException( LocalizedStrings.UnexpectedCoordTypeParams.Put( input.GetType().Name ) );
            }

            if ( input is Decimal )
            {
                return Decimal.ToDouble( ( Decimal ) input );
            }

            if ( !( input is double ) )
            {
                throw new InvalidOperationException( LocalizedStrings.UnexpectedCoordTypeParams.Put( input.GetType().Name ) );
            }

            return ( double ) input;
        }

        internal static IComparable GetYCoordinate( IComparable input )
        {
            if ( input == null )
            {
                return null;
            }

            if ( input is Decimal )
            {
                return Decimal.ToDouble( ( Decimal ) input );
            }

            if ( input is double )
            {
                return ( double ) input;
            }


            throw new InvalidOperationException( LocalizedStrings.UnexpectedCoordTypeParams.Put( input.GetType().Name ) );
        }

        private void AddNewMenu( AnnotationBase annoBase, bool hasAnnotation )
        {
            if ( hasAnnotation == HasAnnotation( annoBase ) )
            {
                return;
            }

            if ( hasAnnotation )
            {
                _annotationBaseSet.Add( annoBase );
            }
            else
            {
                _annotationBaseSet.Remove( annoBase );
            }

            annoBase.IsEditable = hasAnnotation;
            annoBase.CanEditText = hasAnnotation;
            annoBase.FocusVisualStyle = null;
            //vlaHqpwJgrmBsvfI.\u0023\u003Dz8yBlZ7wLyGMQ = new \u0023\u003DzSQJobdqtH0NktyvbaGGemSPQUc7tX1uNXA\u003D\u003D< AnnotationBase > ( new Action<AnnotationBase>( vlaHqpwJgrmBsvfI.Func1 ) );


            PopupMenu menu = new PopupMenu();
            //CommonBarItemCollection items1 = menu.Items;
            BarButtonItem mItem = new BarButtonItem();
            mItem.Glyph = ThemedIconsExtension.GetImage( "remove" );
            mItem.Content = ( "LocalizedStrings.Str1507" + "…" );
            mItem.Command = new ActionCommand<AnnotationBase>( b =>
            {
                _annotationEditor.IsOpen = false;

                Ecng.Collections.CollectionHelper.ForEach( ParentSurface.Annotations.Where( i => i != b ), i => i.IsSelected = false );

                
                b.IsSelected = true;
                _annotationEditor.PlacementTarget = b;
                _annotationEditor.IsOpen = true;
            } );
            mItem.CommandParameter = annoBase;
            menu.Items.Add( mItem );

            var deleteCommand = new ActionCommand<AnnotationBase>( b =>
            {
                _annotationCollection.Remove( b );

                ChartAnnotation annotation;
                if ( !_baseToAnnotationPair.TryGetValue( b, out annotation ) )
                {
                    return;
                }

                //_baseToAnnotationPair.Remove( b );
                _chartArea.Elements.Remove( annotation );

                ChartArea?.InvokeAnnotationDeletedEvent( annotation );
            } );

            BarButtonItem mItem2 = new BarButtonItem();
            mItem2.Glyph = ThemedIconsExtension.GetImage( "remove2" );
            mItem2.Content = "LocalizedStrings.Str2060";
            mItem2.Command = deleteCommand;
            mItem2.CommandParameter = annoBase;
            menu.Items.Add( mItem2 );


            if ( hasAnnotation )
            {
                BarManager.SetDXContextMenu( annoBase, menu );
            }


            EventHandler<EventArgs> dragEndedCommand = ( s, e ) =>
            {
                if ( !_annotationEditor.IsOpen )
                {
                    return;
                }

                _annotationEditor.IsOpen = false;
                _annotationEditor.IsOpen = true;
            };

            KeyEventHandler keyDownHandler = ( s, e ) =>
            {
                if ( !annoBase.IsSelected || Keyboard.Modifiers != ModifierKeys.None )
                {
                    return;
                }

                if ( e.Key == Key.Delete && deleteCommand.CanExecute( annoBase ) )
                {
                    deleteCommand.Execute( annoBase );
                }
                else
                {
                    if ( e.Key != Key.Escape )
                    {
                        return;
                    }

                    // Tony Fix
                    // ( annoBase as TextAnnotation )?.RemoveFocusFromInputTextArea( );
                }
            };

            if ( hasAnnotation )
            {
                annoBase.KeyDown += keyDownHandler;
                annoBase.Selected += ( s, e ) => Keyboard.Focus( annoBase );
                annoBase.PreviewMouseLeftButtonDown += ( s, e ) => Keyboard.Focus( annoBase );
                annoBase.Unselected += ( s, e ) => _annotationEditor.IsOpen = false;
                annoBase.DragStarted += AnnoBase_DragStarted;
                annoBase.DragEnded += dragEndedCommand;
            }
            else
            {
                annoBase.KeyDown -= keyDownHandler;
                annoBase.Selected -= ( s, e ) => Keyboard.Focus( annoBase );
                annoBase.PreviewMouseLeftButtonDown -= ( s, e ) => Keyboard.Focus( annoBase );
                annoBase.Unselected -= ( s, e ) => _annotationEditor.IsOpen = false;
                annoBase.DragStarted -= AnnoBase_DragStarted;
                annoBase.DragEnded -= dragEndedCommand;
            }
        }

        private void AddDependencyProperties( AnnotationBase b )
        {
            if ( ChartArea == null )
            {
                return;
            }

            b.Selected += ( s, e ) =>
            {
                var myBase = ( AnnotationBase )s;

                var a = Ecng.Collections.CollectionHelper.TryGetValue( _baseToAnnotationPair, myBase );
                 
                ChartArea?.InvokeAnnotationSelectedEvent( a, ( a == null ) ? null : GetAnnotationData( myBase ) );
            };

            b.Unselected += ( s, e ) =>
            {
                ChartArea?.InvokeAnnotationSelectedEvent( null, null );
            };


            PooledList<DependencyProperty> propList = new PooledList<DependencyProperty>( )
            {
                AnnotationBase.IsHiddenProperty,
                AnnotationBase.IsEditableProperty,
                AnnotationBase.X1Property,
                AnnotationBase.X2Property,
                AnnotationBase.Y1Property,
                AnnotationBase.Y2Property,
                AnnotationBase.CoordinateModeProperty
            };

            if ( b is LineAnnotationBase )
            {
                propList.Add( LineAnnotationBase.StrokeThicknessProperty );
                propList.Add( LineAnnotationBase.StrokeProperty );
            }

            if ( b is HorizontalLineAnnotation )
            {
                propList.Add( HorizontalAlignmentProperty );
            }
            else if ( b is VerticalLineAnnotation )
            {
                propList.Add( VerticalAlignmentProperty );
            }
            else if ( b is BoxAnnotation )
            {
                propList.Add( BackgroundProperty );
                propList.Add( BorderBrushProperty );
                propList.Add( BorderThicknessProperty );
            }
            else if ( b is TextAnnotation )
            {
                propList.Add( TextAnnotation.TextProperty );
                propList.Add( BackgroundProperty );
                propList.Add( BorderBrushProperty );
                propList.Add( BorderThicknessProperty );
            }
            else if ( b is RulerAnnotation )
            {
                propList.Add( BackgroundProperty );
            }

            foreach ( DependencyProperty d in propList )
            {
                b.AddPropertyListener( d, e =>
                {
                    var annotation = Ecng.Collections.CollectionHelper.TryGetValue( _baseToAnnotationPair, b );
                    

                    if ( annotation == null )
                    {
                        return;
                    }

                    Maybe.Do( annotation, a =>
                    {
                        if ( _isUpdating )
                        {
                            return;
                        }

                        ChartArea.InvokeAnnotationModifiedEvent( a, GetAnnotationData( b ) );
                    } );
                } );
            }
        }

        private void AnnoBase_DragStarted( object sender, EventArgs e )
        {
            _isUpdating = true;
        }

        private bool HasAnnotation( AnnotationBase b )
        {
            return _annotationBaseSet.Contains( b );
        }

        [StructLayout( LayoutKind.Auto )]
        public struct Struct0
        {
            public ICoordinateCalculator<double> sCalc;
            public AnnotationBase b;
        }

        [StructLayout( LayoutKind.Auto )]
        public struct Struct1
        {
            public ICoordinateCalculator<double> sCalc;
            public AnnotationBase b;
        }

        
    }
}

