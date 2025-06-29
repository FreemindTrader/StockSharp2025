// Decompiled with JetBrains decompiler
// Type: #=za9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd$cQ3C42
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
internal sealed class \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42 : 
  \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>
{
  private static \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42 \u0023\u003Dzj9RABVg\u003D;

  private \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42()
  {
    this.\u0023\u003Dz6DunSwc\u003D = new string[14]
    {
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      ""
    };
    this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D = new Dictionary<Type, string[]>()
    {
      {
        typeof (TextAnnotation),
        new string[2]
        {
          "",
          ""
        }
      },
      {
        typeof (LineAnnotationBase),
        new string[2]
        {
          "",
          ""
        }
      },
      {
        typeof (LineArrowAnnotation),
        new string[2]
        {
          "",
          ""
        }
      },
      {
        typeof (LineAnnotationWithLabelsBase),
        new string[2]
        {
          "",
          ""
        }
      },
      {
        typeof (HorizontalLineAnnotation),
        new string[1]
        {
          ""
        }
      },
      {
        typeof (VerticalLineAnnotation),
        new string[2]
        {
          "",
          ""
        }
      }
    };
  }

  internal static \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42 \u0023\u003DzFvAsfEI\u003D()
  {
    return \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003Dzj9RABVg\u003D ?? (\u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42.\u0023\u003Dzj9RABVg\u003D = new \u0023\u003Dza9eQbgAsftIGbI_4wdfcZPOeT4St0p3lrdcxd\u0024cQ3C42());
  }

  public override void \u0023\u003Dz4EJs3pc\u003D(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1,
    XmlReader _param2)
  {
    base.\u0023\u003Dz4EJs3pc\u003D(_param1, _param2);
    string typeName1 = _param2[""];
    if (typeName1 != null)
    {
      Type type = Type.GetType(typeName1);
      _param1.set_X1((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("", type));
      _param1.set_X2((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("", type));
    }
    string typeName2 = _param2[""];
    if (typeName2 == null)
      return;
    Type type1 = Type.GetType(typeName2);
    _param1.set_Y1((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("", type1));
    _param1.set_Y2((IComparable) _param2.\u0023\u003Dzm2nn9hA\u003D("", type1));
  }

  public override void \u0023\u003Dz7SZ\u0024Lrw\u003D(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1,
    XmlWriter _param2)
  {
    base.\u0023\u003Dz7SZ\u0024Lrw\u003D(_param1, _param2);
    if (_param1.get_X1() != null)
    {
      Type type = _param1.get_X1().GetType();
      _param2.WriteAttributeString("", type.\u0023\u003Dzb_Ih6a0\u003D());
      _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "");
      _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "");
    }
    if (_param1.get_Y1() == null)
      return;
    Type type1 = _param1.get_Y1().GetType();
    _param2.WriteAttributeString("", type1.\u0023\u003Dzb_Ih6a0\u003D());
    _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "");
    _param2.\u0023\u003DzVjDFK7Q\u003D((object) _param1, "");
  }
}
