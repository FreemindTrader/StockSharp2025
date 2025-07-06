using Ecng.Common;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.HewFibonacci;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Linq;
using StockSharp.Xaml.Charting.CustomAnnotations;
using StockSharp.Charting;

internal static class ExtensionHelper2
{
    // BUG:
    private static readonly IDictionary<ChartAnnotationTypes, Type> _chartAnnotationTypesToType = new PooledDictionary<ChartAnnotationTypes, Type>( )
    {
        { ChartAnnotationTypes.None                             , null },
        { ChartAnnotationTypes.LineAnnotation                   , typeof (LineAnnotation) },
        { ChartAnnotationTypes.LineArrowAnnotation              , typeof (LineArrowAnnotation) },
        { ChartAnnotationTypes.VerticalLineAnnotation           , typeof (VerticalLineAnnotation) },
        { ChartAnnotationTypes.TextAnnotation                   , typeof (TextAnnotation) },
        { ChartAnnotationTypes.BoxAnnotation                    , typeof (BoxAnnotation) },
        { ChartAnnotationTypes.HorizontalLineAnnotation         , typeof (HorizontalLineAnnotation) },
        { ChartAnnotationTypes.fxElliotWaveAnnotation           , typeof (fxElliotWaveAnnotation) },
        { ChartAnnotationTypes.fxFibonacciRetracementAnnotation , typeof (fxFibonacciRetracementAnnotation) },
        { ChartAnnotationTypes.fxFibonacciExtensionAnnotation   , typeof (fxFibonacciExtensionAnnotation) },
        { ChartAnnotationTypes.RulerAnnotation                  , typeof (RulerAnnotation) },

    };

    public static Type GetType( this ChartAnnotationTypes chartAnnotationTypes )
    {
        return _chartAnnotationTypesToType[ chartAnnotationTypes ];
    }

    public static ChartAnnotationTypes GetAnnotationType( this IAnnotation annotation )
    {
        if ( annotation == null )
        {
            throw new ArgumentNullException( "annotation" );
        }

        if ( !annotation.HasType( ) )
        {
            //throw new InvalidOperationException( LocalizedStrings.Str2061Params.Put( annotation.GetType( ).Name ) );
        }

        return _chartAnnotationTypesToType.First( t => t.Value == annotation.GetType( ) ).Key;
    }

    public static bool HasType( this IAnnotation annotation )
    {
        if ( annotation == null )
        {
            throw new ArgumentNullException( "annotation" );
        }

        return _chartAnnotationTypesToType.Values.Contains( annotation.GetType( ) );
    }
}
