using SciChart.Charting.ChartModifiers;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Extensions;
using SciChart.Core.Framework;
using SciChart.Core.Utility.Mouse;
using fx.Charting.HewFibonacci;
using System;
using System.Windows;
using MoreLinq;

namespace fx.Charting
{
    public class fxTradingAnnotationCreationModifier : fxAnnotationCreationModifier
    {
        //private readonly AnnotationCollection _annotationCollection = null;

        public static readonly DependencyProperty UserAnnotationTypeProperty = DependencyProperty.Register( nameof( UserAnnotationType ), typeof( ChartAnnotationTypes ), typeof( ultrachartannotationmodifier ), new PropertyMetadata( ChartAnnotationTypes.None, new PropertyChangedCallback( OnUserAnnotationTypePropertyChanged ) ) );

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

        private static void OnUserAnnotationTypePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( fxTradingAnnotationCreationModifier ) d ).SetAnnotationStyleAndType( ( ChartAnnotationTypes ) e.NewValue );
        }

        private void SetAnnotationStyleAndType( ChartAnnotationTypes annotationTypes )
        {
            //if ( annotationTypes == ChartAnnotationTypes.None )
            //{
            //    _annotationCollection.ForEach( i => i.IsEditable = true );
            //    AnnotationType = null;
            //    IsEnabled = false;

            //    //if ( _tradingAPI != null )
            //    //{
            //    //    _tradingAPI.AnnotationType = null;
            //    //}
            //}
            //else
            //{
            //    Type type = ExtensionHelper2.GetType( annotationTypes );

            //    if ( type == typeof( fxElliotWaveAnnotation ) ||
            //         type == typeof( fxFibonacciRetracementAnnotation ) ||
            //         type == typeof( fxFibonacciExtensionAnnotation )
            //        )
            //    {
            //        if ( _tradingAPI != null )
            //        {
            //            _tradingAPI.AnnotationType = type;
            //        }
            //    }
            //    else
            //    {
            //        if ( _tradingAPI != null )
            //        {
            //            _tradingAPI.AnnotationType = null;
            //        }
            //    }

            //    string str = type.Name + "Style";

            //    if ( Resources.Contains( str ) )
            //    {
            //        AnnotationStyle = ( Style ) Resources[ str ];
            //    }

            //    AnnotationType = type;
            //    IsEnabled = true;
            //}
        }

        private Point _lastClick;
        private int _clickCount;
        private bool _isMouseDown;

        public event EventHandler<AnnotationCreationArgs> fxTraderAnnotationCreated;

        static fxTradingAnnotationCreationModifier( )
        {
            AnnotationTypeProperty.OverrideMetadata( typeof( fxTradingAnnotationCreationModifier ), new PropertyMetadata( null, new PropertyChangedCallback( OnAnnotationTypeChanged ) ) );
        }

        protected override void OnIsEnabledChanged( )
        {
            Annotation = null;
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
            ITradingAnnotation annotation = Annotation as ITradingAnnotation;

            if ( annotation == null )
            {
                return;
            }

            IComparable xAxisDateTime = XAxis.GetDataValue( pointRelativeTo.X );
            IComparable yAxisPrice    = YAxis.GetDataValue( pointRelativeTo.Y );

            annotation.UpdateBasePoint( _clickCount, xAxisDateTime, yAxisPrice );
        }

        public override void OnModifierMouseDown( ModifierMouseArgs mouseButtonEventArgs )
        {
            if ( AnnotationType == null || ( !MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, ExecuteOn ) || !mouseButtonEventArgs.IsMaster ) )
            {
                return;
            }

            _isMouseDown = true;

            if ( Annotation != null )
            {
                return;
            }

            _lastClick = GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ModifierSurface );
            Annotation = CreateAnnotation( AnnotationType, AnnotationStyle );

            ITradingAnnotation annotation = Annotation as ITradingAnnotation;

            if ( annotation == null )
            {
                return;
            }

            annotation.AnnotationCreated += new EventHandler<AnnotationCreationArgs>( OnTraderAnnotationCreated );

            IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
            IComparable yAxisPrice = YAxis.GetDataValue( _lastClick.Y );

            annotation.SetBasePoint( xAxisDateTime, yAxisPrice );
            _clickCount = 1;
        }

        public override void OnModifierMouseUp( ModifierMouseArgs mouseButtonEventArgs )
        {
            if ( AnnotationType == null || !MatchesExecuteOn( mouseButtonEventArgs.MouseButtons, ExecuteOn ) || !mouseButtonEventArgs.IsMaster )
            {
                return;
            }

            if ( !_isMouseDown  )
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
                    annotation.AnnotationCreated += new EventHandler<AnnotationCreationArgs>( OnTraderAnnotationCreated );
                    IComparable xAxisDateTime     = XAxis.GetDataValue( _lastClick.X );
                    IComparable yAxisPrice        = YAxis.GetDataValue( _lastClick.Y );

                    annotation.SetBasePoint( xAxisDateTime, yAxisPrice );
                    _clickCount = 1;
                }
            }
            else
            {
                var annotation = Annotation as IfxFibonacciAnnotation;
                
                if ( annotation != null )
                {
                    _lastClick = GetPointRelativeTo( mouseButtonEventArgs.MousePoint, ModifierSurface );

                    IComparable xAxisDateTime = XAxis.GetDataValue( _lastClick.X );
                    IComparable yAxisPrice    = YAxis.GetDataValue( _lastClick.Y );

                    annotation.SetBasePoint( xAxisDateTime, yAxisPrice );
                    
                    //if ( _clickCount == 1 )
                    //{
                    //    
                    //}
                    //else
                    //{
                    //    
                    //}

                    ++_clickCount;
                }
            }

            _isMouseDown = false;
        }

        private void OnTraderAnnotationCreated( object sender, AnnotationCreationArgs annotationCreationArgs )
        {
            ITradingAnnotation tradingAnnotation = sender as ITradingAnnotation;

            if ( tradingAnnotation != null )
            {
                tradingAnnotation.AnnotationCreated -= new EventHandler<AnnotationCreationArgs>( OnTraderAnnotationCreated );
            }

            fxTraderAnnotationCreated?.Invoke( this, new AnnotationCreationArgs( ( AnnotationBase ) Annotation ) );

            OnAnnotationCreated( );
        }

        protected override AnnotationBase CreateAnnotation( Type annotationType, Style annotationStyle )
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

        private static void OnAnnotationTypeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            fxTradingAnnotationCreationModifier creationModifier = ( fxTradingAnnotationCreationModifier )d;

            if ( ( Type ) e.NewValue == typeof( BrushAnnotation ) )
            {
                creationModifier.ParentSurface.Annotations.ForEachDo( annotation =>
                {
                    annotation.IsSelected = false;
                    annotation.IsEditable = false;
                } );
            }
            else
            {
                creationModifier.ParentSurface.Annotations.ForEachDo( annotation => annotation.IsEditable = true );
            }
        }
    }
}
