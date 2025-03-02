using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using fx.Algorithm;
using fx.Bars;

namespace fx.Charting.HewFibonacci
{
    public class fxElliotWaveAnnotationBase : MyTradingAnnotationBase
    {
        public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register( nameof( TextStyle ), typeof( Style ), typeof( fxElliotWaveAnnotationBase ), new PropertyMetadata( null ) );

        public fxElliotWaveAnnotationBase( ref SBar bar ) : base( ref bar )
        {
            BasePointsCount = 6;
        }

        public Style TextStyle
        {
            get
            {
                return ( Style )GetValue( TextStyleProperty );
            }
            set
            {
                SetValue( TextStyleProperty, value );
            }
        }

        public override void SetBasePoint( IComparable x, IComparable y )
        {
            base.SetBasePoint( x, y );
            if ( !IsAttached )
            {
                return;
            }
            Point[ ] basePoints = GetBasePoints( );
            if ( basePoints.Length == BasePointsCount )
            {
                UpdateBasePoint( BasePointsCount - 1, x, y );
                OnAnnotationCreated( );
            }
            else
            {
                LineAnnotation line = new LineAnnotation( );
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x;
                line.Y2 = y;

                SetMovingPart( line, true );
                Annotations.Add( line );
                
                UpdateBasePoint( basePoints.Length - 1, x, y );
            }
        }

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            base.Update( xCoordinateCalculator, yCoordinateCalculator );
            if ( isReattached || !IsLoaded || !_timeFrameWasChanged )
            {
                return;
            }
            RefreshBasePoints( );
        }

        private void RefreshBasePoints( )
        {
            Point[ ] basePoints = GetBasePoints( );
            for ( int index = 0; index < basePoints.Count( ); ++index )
            {
                Point point = basePoints[ index ];
                SetBasePoint( FromCoordinate( point.X, XAxis ), FromCoordinate( point.Y, YAxis ), index );
            }
            _timeFrameWasChanged = false;
            Refresh( );
        }

        public override void UpdateBasePoint( int index, IComparable x, IComparable y )
        {
            base.UpdateBasePoint( index, x, y );
            if ( !IsAttached )
            {
                return;
            }
            if ( index >= 0 && index <= BasePointsCount )
            {
                SetBasePoint( x, y, index );
            }
            TryUpdate( XAxis.GetCurrentCoordinateCalculator( ), YAxis.GetCurrentCoordinateCalculator( ) );
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            base.SetBasePoint( newPoint, index, xAxis, yAxis );
            var xDate = XAxis.GetDataValue( newPoint.X );
            var xCalc = xAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;

            if ( xCalc != null )
            {
                xDate = xCalc.GetDataValue( newPoint.X );
            }
            IComparable yPrice = YAxis.GetDataValue( newPoint.Y );
            SetBasePoint( xDate, yPrice, index );
        }

        protected override void SetBasePoint( IComparable xDataValue, IComparable yDataValue, int index )
        {
            IAnnotation[ ] array = MovingLinesPartAnnotations.ToArray( );
            int length = array.Length;
            if ( index == 0 )
            {
                array[ index ].X1 = xDataValue;
                array[ index ].Y1 = yDataValue;
            }
            else if ( index == length )
            {
                array[ index - 1 ].X2 = xDataValue;
                array[ index - 1 ].Y2 = yDataValue;
            }
            else
            {
                array[ index - 1 ].X2 = array[ index ].X1 = xDataValue;
                array[ index - 1 ].Y2 = array[ index ].Y1 = yDataValue;
            }
        }

        protected override void DetachAnnotation( IAnnotation item )
        {
            base.DetachAnnotation( item );
            TextAnnotation textAnnotation = item as TextAnnotation;
            if ( textAnnotation == null )
            {
                return;
            }
            BindingOperations.ClearBinding( textAnnotation, ForegroundProperty );
            BindingOperations.ClearBinding( textAnnotation, FontSizeProperty );
            BindingOperations.ClearBinding( textAnnotation, FontFamilyProperty );
            BindingOperations.ClearBinding( textAnnotation, FontWeightProperty );
            BindingOperations.ClearBinding( textAnnotation, StyleProperty );
        }

        protected override void AttachAnnotation( IAnnotation item )
        {
            base.AttachAnnotation( item );
            TextAnnotation textAnnotation = item as TextAnnotation;
            if ( textAnnotation == null )
            {
                return;
            }
            Binding binding1 = new Binding( ) { Source =   ( this ), Path = new PropertyPath( ForegroundProperty ) };
            textAnnotation.SetBinding( ForegroundProperty, binding1 );
            Binding binding2 = new Binding( ) { Source =   ( this ), Path = new PropertyPath( FontSizeProperty ) };
            textAnnotation.SetBinding( FontSizeProperty, binding2 );
            Binding binding3 = new Binding( ) { Source =   ( this ), Path = new PropertyPath( FontFamilyProperty ) };
            textAnnotation.SetBinding( FontFamilyProperty, binding3 );
            Binding binding4 = new Binding( ) { Source =   ( this ), Path = new PropertyPath( FontWeightProperty ) };
            textAnnotation.SetBinding( FontWeightProperty, binding4 );
            Binding binding5 = new Binding( ) { Source =   ( this ), Path = new PropertyPath( TextStyleProperty ) };
            textAnnotation.SetBinding( StyleProperty, binding5 );
        }
    }
}

