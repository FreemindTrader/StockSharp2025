// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Helpers.RenderableSeriesSerializationHelper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace fx.Xaml.Charting
{
    internal sealed class RenderableSeriesSerializationHelper : SerializationHelper<IRenderableSeries>
    {
        private static RenderableSeriesSerializationHelper _instance;

        internal static RenderableSeriesSerializationHelper Instance
        {
            get
            {
                return RenderableSeriesSerializationHelper._instance ?? ( RenderableSeriesSerializationHelper._instance = new RenderableSeriesSerializationHelper() );
            }
        }

        private RenderableSeriesSerializationHelper()
        {
            this.BaseProperties = new string[ 7 ]
            {
        "XAxisId",
        "YAxisId",
        "IsVisible",
        "StrokeThickness",
        "AntiAliasing",
        "ResamplingMode",
        "SeriesColor"
            };
            this.AddittionalPropertiesDictionary = new Dictionary<Type, string[ ]>()
      {
        {
          typeof (FastLineRenderableSeries),
          new string[2]{ "DrawNaNAs", "IsDigitalLine" }
        },
        {
          typeof (FastOhlcRenderableSeries),
          new string[3]
          {
            "UpWickColor",
            "DownWickColor",
            "DataPointWidth"
          }
        },
        {
          typeof (FastBandRenderableSeries),
          new string[4]
          {
            "IsDigitalLine",
            "Series1Color",
            "BandDownColor",
            "BandUpColor"
          }
        },
        {
          typeof (FastBoxPlotRenderableSeries),
          new string[2]{ "DataPointWidth", "BodyBrush" }
        },
        {
          typeof (FastBubbleRenderableSeries),
          new string[3]{ "AutoZRange", "ZScaleFactor", "BubbleColor" }
        },
        {
          typeof (BaseColumnRenderableSeries),
          new string[4]
          {
            "FillBrush",
            "FillBrushMappingMode",
            "UseUniformWidth",
            "DataPointWidth"
          }
        },
        {
          typeof (StackedColumnRenderableSeries),
          new string[1]{ "StackedGroupId" }
        },
        {
          typeof (BaseMountainRenderableSeries),
          new string[2]{ "IsDigitalLine", "AreaBrush" }
        },
        {
          typeof (StackedMountainRenderableSeries),
          new string[1]{ "StackedGroupId" }
        },
        {
          typeof (FastCandlestickRenderableSeries),
          new string[5]
          {
            "UpWickColor",
            "DownWickColor",
            "DataPointWidth",
            "UpBodyBrush",
            "DownBodyBrush"
          }
        },
        {
          typeof (FastErrorBarsRenderableSeries),
          new string[1]{ "DataPointWidth" }
        }
      };
        }
    }
}
