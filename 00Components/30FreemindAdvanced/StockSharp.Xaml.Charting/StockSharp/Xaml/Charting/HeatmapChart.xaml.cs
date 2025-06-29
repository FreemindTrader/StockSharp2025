// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.HeatmapChart
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>The graphical component to display the heatmap.</summary>
/// <summary>HeatmapChart</summary>
public class HeatmapChart : UserControl, IComponentConnector
{
  
  private readonly HeatmapMatrixAdapter \u0023\u003Dzy9ZP39o\u003D = new HeatmapMatrixAdapter();
  
  private static readonly double[,] \u0023\u003DzpYNJSsBLV187 = new double[0, 0];
  
  internal HeatmapControl \u0023\u003Dz5ZPAD\u0024Ikyftb;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.HeatmapChart" />.
  /// </summary>
  public HeatmapChart()
  {
    this.InitializeComponent();
    this.\u0023\u003Dz5ZPAD\u0024Ikyftb.DataAdapter = (HeatmapDataAdapterBase) this.\u0023\u003Dzy9ZP39o\u003D;
  }

  /// <summary>Draw chart values.</summary>
  /// <param name="xTitles">Titles on X axis.</param>
  /// <param name="yTitles">Titles on Y axis.</param>
  /// <param name="data">Values.</param>
  public void Draw(string[] xTitles, string[] yTitles, double[,] data)
  {
    HeatmapMatrixAdapter zy9Zp39o1 = this.\u0023\u003Dzy9ZP39o\u003D;
    zy9Zp39o1.XArguments = (IEnumerable) (xTitles ?? throw new ArgumentNullException(XXX.SSS(-539441268)));
    HeatmapMatrixAdapter zy9Zp39o2 = this.\u0023\u003Dzy9ZP39o\u003D;
    zy9Zp39o2.YArguments = (IEnumerable) (yTitles ?? throw new ArgumentNullException(XXX.SSS(-539441279)));
    HeatmapMatrixAdapter zy9Zp39o3 = this.\u0023\u003Dzy9ZP39o\u003D;
    zy9Zp39o3.Values = data ?? throw new ArgumentNullException(XXX.SSS(-539441254));
  }

  /// <summary>Clear chart.</summary>
  public void Clear()
  {
    this.Draw(Array.Empty<string>(), Array.Empty<string>(), HeatmapChart.\u0023\u003DzpYNJSsBLV187);
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(XXX.SSS(-539441553), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.\u0023\u003Dz5ZPAD\u0024Ikyftb = (HeatmapControl) target;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }
}
