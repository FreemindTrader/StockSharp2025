// Decompiled with JetBrains decompiler
// Type: #=za9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd$cQ3C42
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
internal sealed class \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42 : 
  \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<IAnnotation>
{
  private static \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42 \u0023\u003Dzj9RABVg\u003D;

  private \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42()
  {
    this.\u0023\u003Dz6DunSwc\u003D = new string[14]
    {
      "IsEditable",
      "IsHidden",
      "IsSelected",
      "XAxisId",
      "YAxisId",
      "CoordinateMode",
      "Background",
      "BorderBrush",
      "BorderThickness",
      "Foreground",
      "FontSize",
      "FontWeight",
      "FontFamily",
      "FontStyle"
    };
    this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D = new Dictionary<Type, string[]>()
    {
      {
        typeof (TextAnnotation),
        new string[2]{ "Text", "TextAlignment" }
      },
      {
        typeof (LineAnnotationBase),
        new string[2]{ "Stroke", "StrokeThickness" }
      },
      {
        typeof (LineArrowAnnotation),
        new string[2]{ "HeadWidth", "HeadLength" }
      },
      {
        typeof (LineAnnotationWithLabelsBase),
        new string[2]{ "ShowLabel", "LabelPlacement" }
      },
      {
        typeof (HorizontalLineAnnotation),
        new string[1]{ "HorizontalAlignment" }
      },
      {
        typeof (VerticalLineAnnotation),
        new string[2]{ "LabelsOrientation", "VerticalAlignment" }
      }
    };
  }

  internal static \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42 \u0023\u003DzFvAsfEI\u003D()
  {
    return \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003Dzj9RABVg\u003D ?? (\u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003Dzj9RABVg\u003D = new \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42());
  }

  public override void \u0023\u003Dz4EJs3pc\u003D(
    IAnnotation _param1,
    XmlReader _param2)
  {
    base.\u0023\u003Dz4EJs3pc\u003D(_param1, _param2);
    string typeName1 = _param2["XType"];
    if (typeName1 != null)
    {
      Type type = Type.GetType(typeName1);
      _param1.set_X1((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("X1", type));
      _param1.set_X2((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("X2", type));
    }
    string typeName2 = _param2["YType"];
    if (typeName2 == null)
      return;
    Type type1 = Type.GetType(typeName2);
    _param1.set_Y1((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("Y1", type1));
    _param1.set_Y2((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("Y2", type1));
  }

  public override void \u0023\u003Dz7SZ\u0024Lrw\u003D(
    IAnnotation _param1,
    XmlWriter _param2)
  {
    base.\u0023\u003Dz7SZ\u0024Lrw\u003D(_param1, _param2);
    if (_param1.get_X1() != null)
    {
      Type type = _param1.get_X1().GetType();
      _param2.WriteAttributeString("XType", type.\u0023\u003Dzb_Ih6a0\u003D());
      _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "X1");
      _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "X2");
    }
    if (_param1.get_Y1() == null)
      return;
    Type type1 = _param1.get_Y1().GetType();
    _param2.WriteAttributeString("YType", type1.\u0023\u003Dzb_Ih6a0\u003D());
    _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "Y1");
    _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "Y2");
  }
}
