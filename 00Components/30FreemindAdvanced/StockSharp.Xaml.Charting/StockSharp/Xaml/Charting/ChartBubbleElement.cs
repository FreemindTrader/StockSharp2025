// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBubbleElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Drawing;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing bubbles.</summary>
[Display(ResourceType = typeof (LocalizedStrings), Name = "Bubble")]
public class ChartBubbleElement : ChartLineElement
{
  /// <summary>Create instance.</summary>
  public ChartBubbleElement() => this.Style = DrawStyles.Bubble;
}
