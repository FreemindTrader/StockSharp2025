// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartCandleElementPicker
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Xaml;
using StockSharp.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class ChartCandleElementPicker : ThemedWindow, IComponentConnector
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal ComboBoxEditEx \u0023\u003DzjQEUHP_xvugS;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal SimpleButton \u0023\u003Dzs4BdTaM\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public ChartCandleElementPicker() => this.InitializeComponent();

  public IEnumerable<IChartCandleElement> Elements
  {
    get => throw new NotSupportedException();
    set
    {
      this.\u0023\u003DzjQEUHP_xvugS.SetItemsSource<IChartCandleElement>(value, ChartCandleElementPicker.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D ?? (ChartCandleElementPicker.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D = new Func<IChartCandleElement, string>(ChartCandleElementPicker.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz7sz5TH9XD46\u0024YVn18a9wvho\u003D)));
    }
  }

  public IChartCandleElement SelectedElement
  {
    get => this.\u0023\u003DzjQEUHP_xvugS.GetSelected<IChartCandleElement>();
    set => this.\u0023\u003DzjQEUHP_xvugS.SetSelected<IChartCandleElement>(value);
  }

  private void \u0023\u003DzI9GYslJ_NW0_rxbvlA\u003D\u003D(
    object _param1,
    EditValueChangedEventArgs _param2)
  {
    this.\u0023\u003Dzs4BdTaM\u003D.IsEnabled = this.SelectedElement != null;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/chartcandleelementpicker.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 1)
    {
      if (connectionId == 2)
        this.\u0023\u003Dzs4BdTaM\u003D = (SimpleButton) target;
      else
        this.\u0023\u003DzQGCmQMjHdLKS = true;
    }
    else
    {
      this.\u0023\u003DzjQEUHP_xvugS = (ComboBoxEditEx) target;
      this.\u0023\u003DzjQEUHP_xvugS.EditValueChanged += new EditValueChangedEventHandler(this.\u0023\u003DzI9GYslJ_NW0_rxbvlA\u003D\u003D);
    }
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly ChartCandleElementPicker.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartCandleElementPicker.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IChartCandleElement, string> \u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D;

    internal string \u0023\u003Dz7sz5TH9XD46\u0024YVn18a9wvho\u003D(IChartCandleElement _param1)
    {
      return ((IChartComponent) _param1).GetGeneratedTitle();
    }
  }
}
