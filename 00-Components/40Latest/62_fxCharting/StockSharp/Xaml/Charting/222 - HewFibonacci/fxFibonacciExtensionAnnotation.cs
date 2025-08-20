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

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    public class fxFibonacciExtensionAnnotation : fxFibonacciExtensionAnnotationBase
    {
        public fxFibonacciExtensionAnnotation( HewFibGannTargets fib, ref SBar lastBar ) : base( fib, ref lastBar )
        {
            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/StockSharp.Xaml.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 3;

            var startPoint           = fib.StartPoint;
            var endPoint             = fib.EndPoint;
            var proPoint             = fib.ProjectionPoint;

            var endingWave           = fib.GetEndingIndexTime( );


            if ( startPoint != default )
            {
                SetProjectionStartPoint( startPoint.Time, startPoint.Value );
            }

            if ( endPoint != default )
            {
                SetProjectionEndPoint( endPoint.Time, endPoint.Value );
            }

            if ( endingWave != default )
            {
                SetEndingIndexTime( endingWave );
            }
            else
            {
                var comingFridayUTC = DateTimeHelper.ReturnNextNthWeekdaysOfMonth( DateTime.UtcNow, DayOfWeek.Friday, 1 ).First( );
                SetEndingIndexTime( comingFridayUTC );
            }



            if ( proPoint != default )
            {
                SetProjectionProjPoint( proPoint.Time, proPoint.Value );
            }

            ImportantLines = fib.GetFibonacciSRLevels( );

            SetAllBasePoints( );
        }

        

        public fxFibonacciExtensionAnnotation( HewFibGannTargets fib, ref SBar lastBar , PooledList<SBar> targetPoint ) : base( fib, ref lastBar, targetPoint )
        {            
            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/StockSharp.Xaml.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 3;

            var startPoint           = fib.StartPoint;
            var endPoint             = fib.EndPoint;
            var proPoint             = fib.ProjectionPoint;

            var endingWave           = fib.GetEndingIndexTime( );


            if ( startPoint != default )
            {
                SetProjectionStartPoint( startPoint.Time, startPoint.Value );
            }

            if ( endPoint != default )
            {
                SetProjectionEndPoint( endPoint.Time, endPoint.Value );
            }

            if ( endingWave != default )
            {
                SetEndingIndexTime( endingWave );
            }
            else
            {
                var comingFridayUTC = DateTimeHelper.ReturnNextNthWeekdaysOfMonth( DateTime.UtcNow, DayOfWeek.Friday, 1 ).First( );

                var lastMinute = comingFridayUTC.Date.AddDays( 1 ).AddTicks( -1 );

                SetEndingIndexTime( lastMinute );
            }



            if ( proPoint != default )
            {
                SetProjectionProjPoint( proPoint.Time, proPoint.Value );
            }

            ImportantLines = fib.GetFibonacciSRLevels( );

            SetAllBasePoints( );
        }


        public static int _instanceCount = 0;
        public static int InstanceCount
        {
            get
            {
                return _instanceCount;
            }
        }

        public int MyCount { get; set; }

        public fxFibonacciExtensionAnnotation( ) : base( null, ref SBar.EmptySBar )
        {
            _instanceCount++;

            MyCount = _instanceCount;

            Selected += new EventHandler( OnSelected );
            Unselected += new EventHandler( OnUnselected );

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/StockSharp.Xaml.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style ) rd[ "GripStyle" ];
                ResizingGripsStyle = _gripStyle;
            }

            BasePointsCount = 3;

            var deleteCommand = new ActionCommand<AnnotationBase>( b =>
                                                                        {
                                                                            var surface = ParentSurface.Annotations;
                                                                            PooledList< IAnnotation > toBeRemove = new PooledList< IAnnotation >( );

                                                                            int myCount = 0;

                                                                            foreach (IAnnotation annotation in surface )
                                                                            {
                                                                                if ( annotation is fxFibonacciExtensionAnnotation )
                                                                                {
                                                                                    var exp = ( fxFibonacciExtensionAnnotation ) annotation;

                                                                                    if ( annotation == b )
                                                                                    {
                                                                                        toBeRemove.Add( b );

                                                                                        myCount = exp.MyCount;
                                                                                    }

                                                                                    if ( exp.MyCount == myCount + 1 )
                                                                                    {
                                                                                        toBeRemove.Add( annotation );
                                                                                    }

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
            delMenu.Header            = "LocalizedStrings.Str2060";
            delMenu.Command           = deleteCommand;
            delMenu.CommandParameter  = this;
            deleteCollection.Add( delMenu );
            systemMenu                = BuildMenu;


            ContextMenu = systemMenu;
        }



        private void OnSelected( object sender, EventArgs e )
        {
            foreach ( IAnnotation annotation in Annotations.Where( a => a is fxFibonacciTableAnnotation ) ) 
            {
                var table = ( fxFibonacciTableAnnotation )annotation;

                table.MakeRatioLineSelected( true );

                annotation.IsSelected = false;
                annotation.IsEditable = false;
            }
        }

        private void OnUnselected( object sender, EventArgs e )
        {
            foreach ( IAnnotation annotation in Annotations.Where( a => a is fxFibonacciTableAnnotation ) ) 
            {
                if ( ! IsLocked )
                {
                    var table = ( fxFibonacciTableAnnotation )annotation;

                    table.MakeRatioLineSelected( false );

                    annotation.IsSelected = false;
                    annotation.IsEditable = false;
                }                
            }
        }        
    }
}
