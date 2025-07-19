// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.HeatmapChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Charts.Heatmap;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class HeatmapChart : UserControl, IComponentConnector
{
  
  private readonly HeatmapMatrixAdapter \u0023\u003Dzy9ZP39o\u003D = new HeatmapMatrixAdapter();
  
  private static readonly double[,] \u0023\u003DzpYNJSsBLV187 = new double[0, 0];
  
  public HeatmapControl \u0023\u003Dz5ZPAD\u0024Ikyftb;
  
  private bool _someInternalBoolean;

  public HeatmapChart()
  {
    this.InitializeComponent();
    this.\u0023\u003Dz5ZPAD\u0024Ikyftb.DataAdapter = (HeatmapDataAdapterBase) this.\u0023\u003Dzy9ZP39o\u003D;
  }

  public void Draw(string[] xTitles, string[] yTitles, double[,] data)
  {
    HeatmapMatrixAdapter zy9Zp39o1 = this.\u0023\u003Dzy9ZP39o\u003D;
    zy9Zp39o1.XArguments = (IEnumerable) (xTitles ?? throw new ArgumentNullException(nameof (xTitles)));
    HeatmapMatrixAdapter zy9Zp39o2 = this.\u0023\u003Dzy9ZP39o\u003D;
    zy9Zp39o2.YArguments = (IEnumerable) (yTitles ?? throw new ArgumentNullException(nameof (yTitles)));
    HeatmapMatrixAdapter zy9Zp39o3 = this.\u0023\u003Dzy9ZP39o\u003D;
    zy9Zp39o3.Values = data ?? throw new ArgumentNullException(nameof (data));
  }

  public void Clear()
  {
    this.Draw(Array.Empty<string>(), Array.Empty<string>(), HeatmapChart.\u0023\u003DzpYNJSsBLV187);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._someInternalBoolean)
      return;
    this._someInternalBoolean = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/heatmapchart.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003Dz5ZPAD\u0024Ikyftb = (HeatmapControl) target;
    else
      this._someInternalBoolean = true;
  }
}
