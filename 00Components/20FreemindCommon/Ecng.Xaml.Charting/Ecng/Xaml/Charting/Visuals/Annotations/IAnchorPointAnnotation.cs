// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.IAnchorPointAnnotation
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Xml.Serialization;
namespace fx.Xaml.Charting
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
