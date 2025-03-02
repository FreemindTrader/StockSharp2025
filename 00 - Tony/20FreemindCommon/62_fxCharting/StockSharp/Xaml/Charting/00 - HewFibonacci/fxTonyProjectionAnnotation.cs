using fx.Algorithm;
using fx.Common;
using MoreLinq;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Data.Model;
using fx.Definitions;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using fx.Bars;

namespace fx.Charting.HewFibonacci
{
    public class fxTonyProjectionAnnotation : fxTonyProjectionAnnotationBase
    {
        public static int _instanceCount = 0;

        public fxTonyProjectionAnnotation( HewFibGannTargets fib, ref SBar lastBar ) : base( fib, ref lastBar )
        {
            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/fx.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 2;

            var startPoint    = fib.StartPoint;
            var endPoint      = fib.EndPoint;

            var endingWave    = fib.GetEndingIndexTime( );

            if ( endPoint != default )
            {
                SetRetracementEndPoint( endPoint.Time, endPoint.Value );
            }

            if ( endingWave != DateTime.MinValue )
            {
                SetNextWaveEndPoint( endingWave, endPoint.Value );
            }

            if ( startPoint != default )
            {
                SetRetracementStartPoint( startPoint.Time, startPoint.Value );
            }

            ImportantLines = fib.GetFibonacciSRLevels( );

            SetAllBasePoints( );
        }

        public fxTonyProjectionAnnotation( HewFibGannTargets fib, ref SBar lastBar, PooledList<SBar> targetPoints ) : base( fib, ref lastBar, targetPoints )
        {
            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/fx.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 2;

            var startPoint    = fib.TonyStartPoint;
            var endPoint      = fib.TonyEndPoint;

            var endingWave    = fib.GetEndingIndexTime( );

            if ( endPoint != default )
            {
                SetRetracementEndPoint( endPoint.Time, endPoint.Value );
            }

            if ( endingWave != DateTime.MinValue )
            {
                SetNextWaveEndPoint( endingWave, endPoint.Value );
            }

            if ( startPoint != default )
            {
                SetRetracementStartPoint( startPoint.Time, startPoint.Value );
            }

            ImportantLines = fib.GetFibonacciSRLevels( );

            SetAllBasePoints( );
        }

        public fxTonyProjectionAnnotation( HewFibGannTargets fib, ref SBar lastBar, PooledList<SBar> targetPoints, bool isSecond ) : base( fib, ref lastBar, targetPoints )
        {
            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/fx.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 2;

            var startPoint    = fib.TonyStartPoint2;
            var endPoint      = fib.TonyEndPoint2;

            var endingWave    = fib.GetEndingIndexTime( );

            if ( endPoint != default )
            {
                SetRetracementEndPoint( endPoint.Time, endPoint.Value );
            }

            if ( endingWave != DateTime.MinValue )
            {
                SetNextWaveEndPoint( endingWave, endPoint.Value );
            }

            if ( startPoint != default )
            {
                SetRetracementStartPoint( startPoint.Time, startPoint.Value );
            }

            ImportantLines = fib.GetFibonacciSRLevels( );

            SetAllBasePoints( );
        }





        public static int InstanceCount
        {
            get
            {
                return _instanceCount;
            }
        }

        public int MyCount { get; set; }


        public fxTonyProjectionAnnotation( ) : base( null, ref SBar.EmptySBar )
        {
            _instanceCount++;

            MyCount = _instanceCount;

            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/fx.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 2;

            var deleteCommand = new ActionCommand<AnnotationBase>( b =>
            {
                var surface = ParentSurface.Annotations;
                PooledList< IAnnotation > toBeRemove = new PooledList< IAnnotation >( );

                int myCount = 0;

                foreach (IAnnotation annotation in surface )
                {
                    if ( annotation is fxTonyProjectionAnnotation )
                    {
                        var ret = ( fxTonyProjectionAnnotation ) annotation;

                        if ( annotation == b )
                        {
                            toBeRemove.Add( b );

                            myCount = ret.MyCount;
                        }

                        if ( ret.MyCount == myCount + 1 )
                        {
                            toBeRemove.Add( annotation );
                        }

                    }
                }

                foreach (IAnnotation annotation in toBeRemove)
                {
                    surface.Remove( annotation );
                }
            } );


            ContextMenu systemMenu;
            var BuildMenu      = new ContextMenu( );
            var propCollection = BuildMenu.Items;
            var propMenu       = new MenuItem( );

            propMenu.Header = ( "LocalizedStrings.Str1507" + "…" );

            propMenu.Command = new ActionCommand<AnnotationBase>( b =>
            {
                //_annotationEditor.IsOpen = false;
                ParentSurface.Annotations.Where( i => i != b ).ForEach( i => i.IsSelected = false );
                b.IsSelected = true;
                //_annotationEditor.PlacementTarget = b;
                //_annotationEditor.IsOpen = true;
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



        private void OnSelected( object sender, EventArgs e )
        {
            foreach ( IAnnotation annotation in Annotations.Where( a => a is fxFibonacciTableAnnotation ) )
            {
                var table = ( fxFibonacciTableAnnotation )annotation;

                table.MakeRatioLineSelected( true );

                annotation.IsSelected = false;
                annotation.IsEditable = true;
            }
        }

        private void OnUnselected( object sender, EventArgs e )
        {
            foreach ( IAnnotation annotation in Annotations.Where( a => a is fxFibonacciTableAnnotation ) )
            {
                if ( !IsLocked )
                {
                    var table = ( fxFibonacciTableAnnotation )annotation;

                    table.MakeRatioLineSelected( false );

                    annotation.IsSelected = false;
                }
            }
        }
    }
}

