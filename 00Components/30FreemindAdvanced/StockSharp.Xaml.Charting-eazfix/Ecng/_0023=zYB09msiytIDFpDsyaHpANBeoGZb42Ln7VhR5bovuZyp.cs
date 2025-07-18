// Decompiled with JetBrains decompiler
// Type: #=zYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;

#nullable disable
public sealed class \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D : 
  \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<ChartModifierBase>
{
  private static \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D \u0023\u003Dzj9RABVg\u003D;

  private \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D()
  {
    this.\u0023\u003Dz6DunSwc\u003D = new string[3]
    {
      "ExecuteOn",
      "ReceiveHandledEvents",
      "IsEnabled"
    };
    this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D = new Dictionary<Type, string[]>()
    {
      {
        typeof (fxAnnotationCreationModifier),
        new string[1]{ "YAxisId" }
      },
      {
        typeof (fxZoomPanModifier),
        new string[3]{ "ZoomExtentsY", "XyDirection", "ClipModeX" }
      },
      {
        typeof (ZoomExtentsModifier),
        new string[2]{ "XyDirection", "IsAnimated" }
      },
      {
        typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSQFzQmDE4iTy9ixA_wLBLIYQ),
        new string[2]{ "DragMode", "AxisId" }
      },
      {
        typeof (XAxisDragModifier),
        new string[1]{ "ClipModeX" }
      },
      {
        typeof (TooltipModifierBase),
        new string[2]{ "ShowTooltipOn", "ShowAxisLabels" }
      },
      {
        typeof (RubberBandXyZoomModifier),
        new string[5]
        {
          "IsAnimated",
          "IsXAxisOnly",
          "ZoomExtentsY",
          "RubberBandFill",
          "RubberBandStroke"
        }
      }
    };
  }

  public static \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D \u0023\u003DzFvAsfEI\u003D()
  {
    return \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003Dzj9RABVg\u003D ?? (\u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D.\u0023\u003Dzj9RABVg\u003D = new \u0023\u003DzYB09msiytIDFpDsyaHpANBeoGZb42Ln7VhR5bovuZyp_7I4QJA\u003D\u003D());
  }
}
