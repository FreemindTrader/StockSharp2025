// Decompiled with JetBrains decompiler
// Type: #=zEp503ezAshtH55ArQ$ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4$0_B
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.Ultrachart;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public static class AnnotationExtensionHelper2025
{
    private static readonly IDictionary<ChartAnnotationTypes, Type> _annotationTypeMap = (IDictionary<ChartAnnotationTypes, Type>) new Dictionary<ChartAnnotationTypes, Type>()
  {
    { ChartAnnotationTypes.None, (Type) null },
    { ChartAnnotationTypes.LineAnnotation, typeof (LineAnnotation) },
    { ChartAnnotationTypes.LineArrowAnnotation, typeof (LineArrowAnnotation) },
    { ChartAnnotationTypes.TextAnnotation, typeof (TextAnnotation) },
    { ChartAnnotationTypes.BoxAnnotation, typeof (BoxAnnotation) },
    { ChartAnnotationTypes.HorizontalLineAnnotation, typeof (HorizontalLineAnnotation) },
    { ChartAnnotationTypes.VerticalLineAnnotation, typeof (VerticalLineAnnotation) },
    { ChartAnnotationTypes.RulerAnnotation, typeof (RulerAnnotation)
    }
  };

    public static Type GetType( this ChartAnnotationTypes type )
    {
        return AnnotationExtensionHelper2025._annotationTypeMap[ type ];
    }

    public static ChartAnnotationTypes GetType( this IAnnotation annotation )
    {
        AnnotationExtensionHelper2025.SomeClass1234 _someMemebers1234 = new AnnotationExtensionHelper2025.SomeClass1234();
        Type myType = annotation != null ? annotation.GetType() : throw new ArgumentNullException( "annotation" );
        if ( !annotation.HasType())
      throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnknownType, new object[ 1 ]
      {
        annotation.Name
      } ) );
        return _annotationTypeMap.First( t => t.Value == annotation.GetType() ).Key;
    }

    public static bool HasType( this IAnnotation annotation )
    {
        if ( annotation == null )
            throw new ArgumentNullException( "annotation" );

        return _annotationTypeMap.Values.Contains( annotation.GetType() );
    }

//    private sealed class SomeClass1234
//    {
//        public Type \u0023\u003DzLLebWNY\u003D;

//    public bool \u0023\u003DzSBgb6xDzEUsSoiug1w\u003D\u003D(
//      KeyValuePair<ChartAnnotationTypes, Type> _param1)
//    {
//      return _param1.Value == this.\u0023\u003DzLLebWNY\u003D;
//    }
//}
}
