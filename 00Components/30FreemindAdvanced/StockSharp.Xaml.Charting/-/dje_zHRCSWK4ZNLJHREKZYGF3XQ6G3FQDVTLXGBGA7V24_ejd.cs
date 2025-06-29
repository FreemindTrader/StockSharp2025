// Decompiled with JetBrains decompiler
// Type: -.dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Bars;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd : 
  PopupMenu,
  IComponentConnector
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzqU86l1s\u003D = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428579), typeof (ChartArea), typeof (dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd \u0023\u003Dz8PNYF8U\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd() => this.InitializeComponent();

  public ChartArea Area
  {
    get
    {
      return (ChartArea) this.GetValue(dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd.\u0023\u003DzqU86l1s\u003D);
    }
    set
    {
      this.SetValue(dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd.\u0023\u003DzqU86l1s\u003D, (object) value);
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428586), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    if (_param1 == 1)
      this.\u0023\u003Dz8PNYF8U\u003D = (dje_zHRCSWK4ZNLJHREKZYGF3XQ6G3FQDVTLXGBGA7V24_ejd) _param2;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }
}
