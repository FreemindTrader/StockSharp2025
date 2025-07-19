// Decompiled with JetBrains decompiler
// Type: #=zEp503ezAshtH55ArQ$ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4$0_B
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.CustomAnnotations;
using StockSharp.Xaml.Charting.HewFibonacci;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This extension helper create a link between the ChartAnnotationTypes enum to its corresponding ChartAnnotationTypes.
/// </summary>
public static class AnnotationExtensionHelper
{
    private static readonly IDictionary<ChartAnnotationTypes, Type> _annotationTypeMap = (IDictionary<ChartAnnotationTypes, Type>) new Dictionary<ChartAnnotationTypes, Type>()
    {
        { ChartAnnotationTypes.None, (Type) null },
        { ChartAnnotationTypes.LineAnnotation, typeof(LineAnnotation) },
        { ChartAnnotationTypes.LineArrowAnnotation, typeof(LineArrowAnnotation) },
        { ChartAnnotationTypes.TextAnnotation, typeof(TextAnnotation) },
        { ChartAnnotationTypes.BoxAnnotation, typeof(BoxAnnotation) },
        { ChartAnnotationTypes.HorizontalLineAnnotation, typeof(HorizontalLineAnnotation) },
        { ChartAnnotationTypes.VerticalLineAnnotation, typeof(VerticalLineAnnotation) },
        { ChartAnnotationTypes.RulerAnnotation, typeof(RulerAnnotation) },

        #region Tony New Defines
        { ChartAnnotationTypes.fxElliotWaveAnnotation, typeof(fxElliotWaveAnnotation) },
        { ChartAnnotationTypes.fxFibonacciRetracementAnnotation, typeof(fxFibonacciRetracementAnnotation) },
        { ChartAnnotationTypes.fxFibonacciExtensionAnnotation, typeof(fxFibonacciExtensionAnnotation) }
        #endregion
    };
  

    public static Type GetType( this ChartAnnotationTypes type )
    {
        return AnnotationExtensionHelper._annotationTypeMap[ type ];
    }

    public static ChartAnnotationTypes GetType( this IAnnotation annotation )
    {        
        Type myType = annotation != null ? annotation.GetType() : throw new ArgumentNullException( "annotation" );

        if ( !annotation.HasType() )
        {
            throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnknownType, myType.Name ) );
        }
            
        return _annotationTypeMap.First( t => t.Value == annotation.GetType() ).Key;
    }

    public static bool HasType( this IAnnotation annotation )
    {
        if ( annotation == null )
            throw new ArgumentNullException( "annotation" );

        return _annotationTypeMap.Values.Contains( annotation.GetType() );
    }
}


//internal static class ExtensionHelper2
//{
//    // BUG:
//    private static readonly IDictionary<ChartAnnotationTypes, Type> _annotationTypeMap = new PooledDictionary<ChartAnnotationTypes, Type>( )
//    {
//        { ChartAnnotationTypes.None                             , null },
//        { ChartAnnotationTypes.LineAnnotation                   , typeof (LineAnnotation) },
//        { ChartAnnotationTypes.LineArrowAnnotation              , typeof (LineArrowAnnotation) },
//        { ChartAnnotationTypes.VerticalLineAnnotation           , typeof (VerticalLineAnnotation) },
//        { ChartAnnotationTypes.TextAnnotation                   , typeof (TextAnnotation) },
//        { ChartAnnotationTypes.BoxAnnotation                    , typeof (BoxAnnotation) },
//        { ChartAnnotationTypes.HorizontalLineAnnotation         , typeof (HorizontalLineAnnotation) },
//        { ChartAnnotationTypes.fxElliotWaveAnnotation           , typeof (fxElliotWaveAnnotation) },
//        { ChartAnnotationTypes.fxFibonacciRetracementAnnotation , typeof (fxFibonacciRetracementAnnotation) },
//        { ChartAnnotationTypes.fxFibonacciExtensionAnnotation   , typeof (fxFibonacciExtensionAnnotation) },
//        { ChartAnnotationTypes.RulerAnnotation                  , typeof (RulerAnnotation) },

//    };

//    public static Type GetType( this ChartAnnotationTypes chartAnnotationTypes )
//    {
//        return _annotationTypeMap[ chartAnnotationTypes ];
//    }

//    public static ChartAnnotationTypes GetAnnotationType( this IAnnotation annotation )
//    {
//        if ( annotation == null )
//        {
//            throw new ArgumentNullException( "annotation" );
//        }

//        if ( !annotation.HasType( ) )
//        {
//            //throw new InvalidOperationException( LocalizedStrings.Str2061Params.Put( annotation.GetType( ).Name ) );
//        }

//        return _annotationTypeMap.First( t => t.Value == annotation.GetType( ) ).Key;
//    }

//    //public static bool HasType( this IAnnotation annotation )
//    //{
//    //    if ( annotation == null )
//    //    {
//    //        throw new ArgumentNullException( "annotation" );
//    //    }

//    //    return _annotationTypeMap.Values.Contains( annotation.GetType( ) );
//    //}
//}
