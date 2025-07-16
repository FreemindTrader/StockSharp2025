// Decompiled with JetBrains decompiler
// Type: -.AxisDragCursor
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisDragCursor : Control
{
  
  public static readonly DependencyProperty \u0023\u003DzyrnkYrk\u003D = DependencyProperty.Register(nameof (Angle), typeof (double), typeof (AxisDragCursor), new PropertyMetadata((object) 0.0));

  public AxisDragCursor()
  {
    this.DefaultStyleKey = (object) typeof (AxisDragCursor);
  }

  public double Angle
  {
    get
    {
      return (double) this.GetValue(AxisDragCursor.\u0023\u003DzyrnkYrk\u003D);
    }
    set
    {
      this.SetValue(AxisDragCursor.\u0023\u003DzyrnkYrk\u003D, (object) value);
    }
  }
}
