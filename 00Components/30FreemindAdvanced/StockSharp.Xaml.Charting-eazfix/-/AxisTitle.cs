// Decompiled with JetBrains decompiler
// Type: -.AxisTitle
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisTitle : ContentControl
{
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (AxisTitle), new PropertyMetadata((object) Orientation.Horizontal));

  public AxisTitle()
  {
    this.DefaultStyleKey = (object) typeof (AxisTitle);
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(AxisTitle.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(AxisTitle.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }
}
