using DevExpress.Mvvm;
using fx.Algorithm;
using MoreLinq;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Data.Model;
using fx.Definitions;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using fx.Bars;

#pragma warning disable 219

namespace StockSharp.Xaml.Charting.HewFibonacci
{    
    public class fxElliotWaveAnnotation : fxElliotWaveAnnotationBase
    {        
        private int _pointsCount;


        public static int _instanceCount = 0;
        public static int InstanceCount
        {
            get
            {
                return _instanceCount;
            }
        }

        public int MyCount { get; set; }

        public fxElliotWaveAnnotation( ref SBar bar ) : base ( ref bar )
        {
            BasePointsCount = 6;
        }

        public fxElliotWaveAnnotation( ) : base( ref SBar.EmptySBar )
        {
            BasePointsCount = 6;

            _instanceCount++;

            MyCount = _instanceCount;

            var deleteCommand = new ActionCommand<AnnotationBase>( b =>
                                                                        {
                                                                            var surface = ParentSurface.Annotations;
                                                                            PooledList< IAnnotation > toBeRemove = new PooledList< IAnnotation >( );

                                                                            int myCount = 0;

                                                                            foreach (IAnnotation annotation in surface )
                                                                            {
                                                                                if ( annotation is fxElliotWaveAnnotation )
                                                                                {
                                                                                    toBeRemove.Add( annotation );                        
                                                                                }
                                                                            }

                                                                            foreach (IAnnotation annotation in toBeRemove)
                                                                            {
                                                                                surface.Remove( annotation );
                                                                            }
                                                                        }
                                                                 );


            ContextMenu systemMenu;
            var BuildMenu      = new ContextMenu( );
            var propCollection = BuildMenu.Items;
            var propMenu       = new MenuItem( );

            propMenu.Header = ( "LocalizedStrings.Str1507" + "…" );

            propMenu.Command = new ActionCommand<AnnotationBase>( b =>
            {
                ParentSurface.Annotations.Where( i => i != b ).ForEach( i => i.IsSelected = false );
                b.IsSelected = true;
            } );

            propMenu.CommandParameter = this;
            propCollection.Add( propMenu );

            var deleteCollection      = BuildMenu.Items;
            var delMenu               = new MenuItem( );
            delMenu.Header = "LocalizedStrings.Str2060";
            delMenu.Command = deleteCommand;
            delMenu.CommandParameter = this;
            deleteCollection.Add( delMenu );
            systemMenu = BuildMenu;


            ContextMenu = systemMenu;
        }

        public override void SetBasePoint( IComparable x, IComparable y )
        {
            base.SetBasePoint( x, y );

            if ( !IsAttached )
            {
                return;
            }

            if ( _pointsCount < BasePointsCount )
            {
                TextAnnotation waveName = new TextAnnotation( );

                waveName.X1 = x;
                waveName.Y1 = y;
                waveName.Text = string.Format( "({0})", _pointsCount );
                waveName.RenderTransform = new TranslateTransform( 0.0, 0.0 );

                Annotations.Add( waveName );
                ++_pointsCount;

                Update( XAxis.GetCurrentCoordinateCalculator( ), YAxis.GetCurrentCoordinateCalculator( ) );

                if ( _pointsCount == BasePointsCount )
                {
                    Messenger.Default.Send( new TradingApiDoneMessage( 6 ) );
                }
            }
            else
            {
                UpdateBasePoint( 4, x, y );
            }
        }

        public override void UpdateBasePoint( int index, IComparable x, IComparable y )
        {
            base.UpdateBasePoint( index, x, y );

            if ( !IsAttached || IsCreated )
            {
                return;
            }

            IAnnotation annotation = Annotations.LastOrDefault( a =>
                                                                        {
                                                                            if ( GetMovingPart( ( DependencyObject )a ) )
                                                                            {
                                                                                return a is TextAnnotation;
                                                                            }
                                                                            return false;
                                                                        } );

            Point[ ] basePoints = GetBasePoints( );
            Point lastPt = basePoints.LastOrDefault( );

            if ( annotation == null )
            {
                return;
            }

            Point point2 = basePoints[ basePoints.Length - 2 ];
            IComparable dataValue1 = XAxis.GetDataValue( point2.X - annotation.ActualWidth / 2.0 );
            IComparable dataValue2 = YAxis.GetDataValue( point2.Y );
            if ( point2.Y < lastPt.Y )
            {
                dataValue2 = YAxis.GetDataValue( point2.Y - annotation.ActualHeight - 2.0 );
            }
            annotation.X1 = dataValue1;
            annotation.Y1 = dataValue2;
        }

        protected override void SetBasePoint( IComparable x, IComparable y, int index )
        {
            base.SetBasePoint( x, y, index );
            if ( !IsCreated )
            {
                return;
            }
            Point[ ] basePoints = GetBasePoints( );
            IAnnotation[ ] array = Annotations.Where( anno => anno is TextAnnotation ).ToArray( );
            int length = array.Length;

            if ( index == 0 )
            {
                // This is the first point, so the there is no previous point and therefore previous point = current point
                UpdateTextAnnotationBasePoint( array[ index ], basePoints[ index ], basePoints[ index ], basePoints[ index + 1 ] );
            }
            else if ( index == length - 1 )
            {
                // This is the last point, so the there is no next point and therefore next point = current point
                UpdateTextAnnotationBasePoint( array[ index ], basePoints[ index ], basePoints[ index - 1 ], basePoints[ index ] );
            }
            else
            {
                UpdateTextAnnotationBasePoint( array[ index ], basePoints[ index ], basePoints[ index - 1 ], basePoints[ index + 1 ] );
            }
        }

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            if ( !isReattached && IsLoaded )
            {
                method_21( );
            }

            base.Update( xCoordinateCalculator, yCoordinateCalculator );
        }

        private void method_21( )
        {
            Point[ ] basePoints = GetBasePoints( );

            IAnnotation[ ] waveNames = Annotations.Where( anno => anno is TextAnnotation ).ToArray( );

            for ( int index = 0; index < waveNames.Length; ++index )
            {
                Point point = basePoints[ index ];
                IAnnotation annotation = waveNames[ index ];
                TranslateTransform renderTransform = ( TranslateTransform )( ( UIElement )annotation ).RenderTransform;
                renderTransform.X = 0.0;
                renderTransform.Y = 0.0;

                IComparable yPrice = FromCoordinate( point.Y, YAxis );
                IComparable xDate = FromCoordinate( point.X, XAxis );
                renderTransform.X = -annotation.ActualWidth / 2.0;
                int num = waveNames.Count( ) - 1;
                if ( index == 0 )
                {
                    if ( basePoints[ index + 1 ].Y > point.Y )
                    {
                        renderTransform.Y = -annotation.ActualHeight - 2.0;
                    }
                }
                else if ( index == num )
                {
                    if ( basePoints[ index - 1 ].Y > point.Y )
                    {
                        renderTransform.Y = -annotation.ActualHeight - 2.0;
                    }
                }
                else if ( basePoints[ index - 1 ].Y > point.Y || basePoints[ index + 1 ].Y > point.Y )
                {
                    renderTransform.Y = -annotation.ActualHeight - 2.0;
                }
                annotation.X1 = xDate;
                annotation.Y1 = yPrice;
            }
        }
    }
}

