// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBubbleElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Drawing;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace StockSharp.Xaml.Charting;

[Display(ResourceType = typeof (LocalizedStrings), Name = "Bubble")]
public class ChartBubbleElement : ChartLineElement
{
  public ChartBubbleElement() => this.Style = DrawStyles.Bubble;
}
