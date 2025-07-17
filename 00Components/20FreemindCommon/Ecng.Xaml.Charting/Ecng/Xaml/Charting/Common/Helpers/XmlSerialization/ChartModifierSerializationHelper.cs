// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Helpers.XmlSerialization.ChartModifierSerializationHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using Ecng.Xaml.Charting.ChartModifiers;

namespace Ecng.Xaml.Charting.Common.Helpers.XmlSerialization
{
    internal class ChartModifierSerializationHelper : SerializationHelper<ChartModifierBase>
    {
        private static ChartModifierSerializationHelper _instance;

        internal static ChartModifierSerializationHelper Instance
        {
            get
            {
                return ChartModifierSerializationHelper._instance ?? ( ChartModifierSerializationHelper._instance = new ChartModifierSerializationHelper() );
            }
        }

        private ChartModifierSerializationHelper()
        {
            this.BaseProperties = new string[ 3 ]
            {
        "ExecuteOn",
        "ReceiveHandledEvents",
        "IsEnabled"
            };
            this.AddittionalPropertiesDictionary = new Dictionary<Type, string[ ]>()
      {
        {
          typeof (AnnotationCreationModifier),
          new string[1]{ "YAxisId" }
        },
        {
          typeof (ZoomPanModifier),
          new string[3]{ "ZoomExtentsY", "XyDirection", "ClipModeX" }
        },
        {
          typeof (ZoomExtentsModifier),
          new string[2]{ "XyDirection", "IsAnimated" }
        },
        {
          typeof (AxisDragModifierBase),
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
    }
}
