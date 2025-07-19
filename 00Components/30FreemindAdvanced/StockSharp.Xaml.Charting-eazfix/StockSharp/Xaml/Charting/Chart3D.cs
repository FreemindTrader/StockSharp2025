// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Chart3D
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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

public class Chart3D : Chart3DControl, IComponentConnector
{
  
  public AxisTitle \u0023\u003Dze_0mKZFnOTAk;
  
  public AxisTitle \u0023\u003DzQvGu9aka9P32;
  
  public AxisTitle \u0023\u003DzRC0RyhMpSlb_;
  
  public SeriesPoint3DStorage \u0023\u003DzedqXoAU\u003D;
  
  private bool _someInternalBoolean;

  public Chart3D() => this.InitializeComponent();

  public void Clear() => this.\u0023\u003DzedqXoAU\u003D.Points.Clear();

  public void Draw(IEnumerable<SeriesPoint3D> values, string xTitle = null, string yTitle = null, string zTitle = null)
  {
    if (values == null)
      throw new ArgumentNullException(nameof (values));
    this.\u0023\u003DzedqXoAU\u003D.Points.Clear();
    this.\u0023\u003DzedqXoAU\u003D.Points.AddRange(values);
    this.\u0023\u003Dze_0mKZFnOTAk.Content = (object) xTitle;
    this.\u0023\u003DzQvGu9aka9P32.Content = (object) yTitle;
    this.\u0023\u003DzRC0RyhMpSlb_.Content = (object) zTitle;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._someInternalBoolean)
      return;
    this._someInternalBoolean = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/chart3d.xaml", UriKind.Relative));
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
        this._someInternalBoolean = true;
        break;
    }
  }
}
