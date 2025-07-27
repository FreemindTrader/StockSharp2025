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
  
  public ComboBoxEditEx ElementsCtrl;
  
  public SimpleButton Ok;
  
  private bool _someInternalBoolean;

  public ChartCandleElementPicker() => this.InitializeComponent();

  public IEnumerable<IChartCandleElement> Elements
  {
    get => throw new NotSupportedException();
    set
    {
      this.ElementsCtrl.SetItemsSource<IChartCandleElement>(value, ChartCandleElementPicker.SomeClass34343383.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D ?? (ChartCandleElementPicker.SomeClass34343383.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D = new Func<IChartCandleElement, string>(ChartCandleElementPicker.SomeClass34343383.SomeMethond0343.\u0023\u003Dz7sz5TH9XD46\u0024YVn18a9wvho\u003D)));
    }
  }

  public IChartCandleElement SelectedElement
  {
    get => this.ElementsCtrl.GetSelected<IChartCandleElement>();
    set => this.ElementsCtrl.SetSelected<IChartCandleElement>(value);
  }

  private void SomeMethods0382(
    object _param1,
    EditValueChangedEventArgs _param2)
  {
    this.Ok.IsEnabled = this.SelectedElement != null;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._someInternalBoolean)
      return;
    this._someInternalBoolean = true;
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
        this.Ok = (SimpleButton) target;
      else
        this._someInternalBoolean = true;
    }
    else
    {
      this.ElementsCtrl = (ComboBoxEditEx) target;
      this.ElementsCtrl.EditValueChanged += new EditValueChangedEventHandler(this.SomeMethods0382);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly ChartCandleElementPicker.SomeClass34343383 SomeMethond0343 = new ChartCandleElementPicker.SomeClass34343383();
    public static Func<IChartCandleElement, string> \u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D;

    public string \u0023\u003Dz7sz5TH9XD46\u0024YVn18a9wvho\u003D(IChartCandleElement _param1)
    {
      return ((IChartComponent) _param1).GetGeneratedTitle();
    }
  }
}
