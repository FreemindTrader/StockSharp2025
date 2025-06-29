// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.IAnchorPointAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System.Xml.Serialization;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

internal interface IAnchorPointAnnotation : 
  \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D,
  \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z,
  \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV,
  IXmlSerializable
{
  dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd HorizontalAnchorPoint { get; set; }

  dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd VerticalAnchorPoint { get; set; }

  double VerticalOffset { get; }

  double HorizontalOffset { get; }
}
