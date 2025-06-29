// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Chart3D
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Charts;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>Chart 3D.</summary>
/// <summary>Chart3D</summary>
public class Chart3D : Chart3DControl, IComponentConnector
{
  
  internal AxisTitle \u0023\u003Dze_0mKZFnOTAk;
  
  internal AxisTitle \u0023\u003DzQvGu9aka9P32;
  
  internal AxisTitle \u0023\u003DzRC0RyhMpSlb_;
  
  internal SeriesPoint3DStorage \u0023\u003DzedqXoAU\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  /// <summary>
  /// Initialize <see cref="T:StockSharp.Xaml.Charting.Chart3D" />.
  /// </summary>
  public Chart3D() => this.InitializeComponent();

  /// <summary>Clear chart.</summary>
  public void Clear() => this.\u0023\u003DzedqXoAU\u003D.Points.Clear();

  /// <summary>Draw data.</summary>
  /// <param name="values">Values.</param>
  /// <param name="xTitle">Title for X axis.</param>
  /// <param name="yTitle">Title for Y axis.</param>
  /// <param name="zTitle">Title for Z axis.</param>
  public void Draw(IEnumerable<SeriesPoint3D> values, string xTitle = null, string yTitle = null, string zTitle = null)
  {
    if (values == null)
      throw new ArgumentNullException(XXX.SSS(-539432292));
    this.\u0023\u003DzedqXoAU\u003D.Points.Clear();
    this.\u0023\u003DzedqXoAU\u003D.Points.AddRange(values);
    this.\u0023\u003Dze_0mKZFnOTAk.Content = (object) xTitle;
    this.\u0023\u003DzQvGu9aka9P32.Content = (object) yTitle;
    this.\u0023\u003DzRC0RyhMpSlb_.Content = (object) zTitle;
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(XXX.SSS(-539432303), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.\u0023\u003Dze_0mKZFnOTAk = (AxisTitle) target;
        break;
      case 2:
        this.\u0023\u003DzQvGu9aka9P32 = (AxisTitle) target;
        break;
      case 3:
        this.\u0023\u003DzRC0RyhMpSlb_ = (AxisTitle) target;
        break;
      case 4:
        this.\u0023\u003DzedqXoAU\u003D = (SeriesPoint3DStorage) target;
        break;
      default:
        this.\u0023\u003DzQGCmQMjHdLKS = true;
        break;
    }
  }
}
