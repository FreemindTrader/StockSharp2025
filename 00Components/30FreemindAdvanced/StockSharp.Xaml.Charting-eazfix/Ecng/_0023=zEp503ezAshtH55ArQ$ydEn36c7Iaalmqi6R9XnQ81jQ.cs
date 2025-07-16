// Decompiled with JetBrains decompiler
// Type: #=zEp503ezAshtH55ArQ$ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4$0_B
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.Ultrachart;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public static class \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4\u00240_B
{
  private static readonly IDictionary<ChartAnnotationTypes, Type> \u0023\u003DzuIb\u0024MT5vuNxR = (IDictionary<ChartAnnotationTypes, Type>) new Dictionary<ChartAnnotationTypes, Type>()
  {
    {
      ChartAnnotationTypes.None,
      (Type) null
    },
    {
      ChartAnnotationTypes.LineAnnotation,
      typeof (LineAnnotation)
    },
    {
      ChartAnnotationTypes.LineArrowAnnotation,
      typeof (LineArrowAnnotation)
    },
    {
      ChartAnnotationTypes.TextAnnotation,
      typeof (TextAnnotation)
    },
    {
      ChartAnnotationTypes.BoxAnnotation,
      typeof (BoxAnnotation)
    },
    {
      ChartAnnotationTypes.HorizontalLineAnnotation,
      typeof (HorizontalLineAnnotation)
    },
    {
      ChartAnnotationTypes.VerticalLineAnnotation,
      typeof (VerticalLineAnnotation)
    },
    {
      ChartAnnotationTypes.RulerAnnotation,
      typeof (RulerAnnotation)
    }
  };

  public static Type \u0023\u003Dz6wKFLhaRAAzr(this ChartAnnotationTypes _param0)
  {
    return \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4\u00240_B.\u0023\u003DzuIb\u0024MT5vuNxR[_param0];
  }

  public static ChartAnnotationTypes \u0023\u003Dz6wKFLhaRAAzr(
    this IAnnotation _param0)
  {
    \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4\u00240_B.SomeClass1234 _someMemebers1234 = new \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4\u00240_B.SomeClass1234();
    _someMemebers1234.\u0023\u003DzLLebWNY\u003D = _param0 != null ? _param0.GetType() : throw new ArgumentNullException("annotation");
    if (!_param0.\u0023\u003DzOICIoAl6CQu1())
      throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnknownType, new object[1]
      {
        (object) _someMemebers1234.\u0023\u003DzLLebWNY\u003D.Name
      }));
    return \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4\u00240_B.\u0023\u003DzuIb\u0024MT5vuNxR.First<KeyValuePair<ChartAnnotationTypes, Type>>(new Func<KeyValuePair<ChartAnnotationTypes, Type>, bool>(_someMemebers1234.\u0023\u003DzSBgb6xDzEUsSoiug1w\u003D\u003D)).Key;
  }

  public static bool \u0023\u003DzOICIoAl6CQu1(
    this IAnnotation _param0)
  {
    if (_param0 == null)
      throw new ArgumentNullException("annotation");
    return \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEn36c7Iaalmqi6R9XnQ81jQ0rEZqUPodXoc4\u00240_B.\u0023\u003DzuIb\u0024MT5vuNxR.Values.Contains(_param0.GetType());
  }

  private sealed class SomeClass1234
  {
    public Type \u0023\u003DzLLebWNY\u003D;

    public bool \u0023\u003DzSBgb6xDzEUsSoiug1w\u003D\u003D(
      KeyValuePair<ChartAnnotationTypes, Type> _param1)
    {
      return _param1.Value == this.\u0023\u003DzLLebWNY\u003D;
    }
  }
}
