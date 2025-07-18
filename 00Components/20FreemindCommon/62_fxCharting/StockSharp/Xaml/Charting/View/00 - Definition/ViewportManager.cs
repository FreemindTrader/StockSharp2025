using Ecng.Common;
using StockSharp.Charting;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Services;
using SciChart.Charting.ViewportManagers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Data.Model;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;

/*
 * Tony: Will study this
 * 
 * https://www.scichart.com/questions/wpf/text-annotations-and-the-zoom-extents-modifier
 * 
 * */
public sealed class ViewportManager : ViewportManagerBase
{
    protected override IRange OnCalculateNewXRange( IAxis xAxis )
    {
        if( xAxis.AutoRange != AutoRange.Always )
        {
            return xAxis.VisibleRange;
        }

        IRange maximumRange = xAxis.GetMaximumRange( );

        if( maximumRange != null && maximumRange.IsDefined )
        {
            return maximumRange;
        }

        return xAxis.VisibleRange;
    }

    protected override IRange OnCalculateNewYRange( IAxis yAxis, RenderPassInfo info )
    {
        if( yAxis.AutoRange != AutoRange.Always || info.PointSeries == null || info.RenderableSeries == null )
        {
            return yAxis.VisibleRange;
        }

        if ( info.RenderableSeries.Count() == 0 )
        {
            return yAxis.VisibleRange;
        }

        var yrange = yAxis.CalculateYRange( info );

        if( yrange == null )
        {
            return yAxis.VisibleRange;
        }

        var service          = Services.GetService<ISciChartSurface>( );
        var ultraChartPaneVM = ( IDrawingSurfaceVM )( ( FrameworkElement )service ).DataContext;
        var yAxisAnnotation  = service.Annotations.Where( i => i.YAxis == yAxis ).ToArray( );

        if ( yAxisAnnotation.Length == 0 )
        {
            if( !yrange.IsDefined )
            {
                return yAxis.VisibleRange;
            }
            return yrange;
        }

        bool isYrangeDefined = false;

        Chart mainChart = ultraChartPaneVM.Chart as Chart;

        if ( mainChart != null )
        {
            isYrangeDefined = mainChart.AutoRangeByAnnotations || !yrange.IsDefined;
        }
        else
        {
            if ( ultraChartPaneVM.ChartExViewModel != null )
            {
                isYrangeDefined = ultraChartPaneVM.ChartExViewModel.AutoRangeByAnnotations || !yrange.IsDefined;
            }
            else
            {
                isYrangeDefined = !yrange.IsDefined;
            }
        }

         

        var max101 = isYrangeDefined ? double.MaxValue : yrange.Max.To< double >( ) * 1.01;
        var max099 = isYrangeDefined ? double.MinValue : yrange.Max.To< double >( ) * 0.99;

        Func<UltraChartCustomAnnotation, bool> myWhere = a =>
        {
            if ( a.X1 is DateTimeOffset )
            {
                a.X1 = ( ( DateTimeOffset )a.X1 ).LocalDateTime;
            }

            var calc = a.XAxis.GetCurrentCoordinateCalculator( );

            if ( !calc.IsCategoryAxisCalculator )
            {
                return false;
            }

            int index = ( ( ICategoryCoordinateCalculator )calc ).TransformDataToIndex( a.X1 );
            return a.XAxis.VisibleRange.IsValueWithinRange( index );
        };

        Func<UltraChartCustomAnnotation, double> mySelect = a =>
        {
            double yLoc = a.YAxis.GetCoordinate( a.Y1 ) + a.ActualHeight * ( a is UltrachartBuymakerAnnotation ? 1.0 : -1.0 );

            return ( double )Converter.To<double>( a.YAxis.GetDataValue( yLoc ) );
        };

        double[ ] customAnnotations =   yAxisAnnotation
                                        .OfType<UltraChartCustomAnnotation>( )
                                        .Where( myWhere )
                                        .Select( mySelect )
                                        .Where( d => {
                                                                if ( d >= max101 )
                                                                {
                                                                    return d <= max099;
                                                                }
                                                                return false;
                                                             }
                                                                                                                                            
                                        ).ToArray( );

        if ( customAnnotations.Length == 0 )
        {
            if( !yrange.IsDefined )
            {
                return yAxis.VisibleRange;
            }
            return yrange;
        }

        double min = customAnnotations.Min( );
        double max = customAnnotations.Max( );

        if( yrange.IsDefined )
        {
            return new DoubleRange( yrange.Min.To<double>( ).Min( min ), yrange.Max.To<double>( ).Max( max ) );
        }

        return new DoubleRange( min, max );
    }      
}
