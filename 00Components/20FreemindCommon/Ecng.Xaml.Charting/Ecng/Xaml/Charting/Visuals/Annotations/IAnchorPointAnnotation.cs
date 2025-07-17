// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.IAnchorPointAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Xml.Serialization;
using StockSharp.Xaml.Charting.Utility.Mouse;

namespace StockSharp.Xaml.Charting.Visuals.Annotations
{
    public interface IAnchorPointAnnotation : IAnnotation, IHitTestable, IPublishMouseEvents, IXmlSerializable
    {
        HorizontalAnchorPoint HorizontalAnchorPoint
        {
            get; set;
        }

        VerticalAnchorPoint VerticalAnchorPoint
        {
            get; set;
        }

        double VerticalOffset
        {
            get;
        }

        double HorizontalOffset
        {
            get;
        }
    }
}
