// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartElementEditor
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart elements editor.</summary>
/// <summary>ChartElementEditor</summary>
public class ChartElementEditor : UserControl, IComponentConnector
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal CheckEdit \u0023\u003DzW4kuZJQvOGhM;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzQGCmQMjHdLKS;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartElementEditor" />.
  /// </summary>
  public ChartElementEditor() => this.InitializeComponent();

  private void \u0023\u003DzhBeNwsJCF1_yklWLow\u003D\u003D(
    object _param1,
    CustomExpandEventArgs _param2)
  {
    if (!_param2.IsInitializing)
      return;
    int num = _param2.Row.FullPath.Count<char>(ChartElementEditor.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D ?? (ChartElementEditor.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D = new Func<char, bool>(ChartElementEditor.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzAaU0cZFSAtN99pV5BtGNHjfrUBPr)));
    _param2.IsExpanded = num < 5;
  }

  /// <summary>InitializeComponent</summary>
  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri(XXX.SSS(-539427431), UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 1)
    {
      if (connectionId == 2)
        ((PropertyGridControl) target).CustomExpand += new CustomExpandEventHandler(this.\u0023\u003DzhBeNwsJCF1_yklWLow\u003D\u003D);
      else
        this.\u0023\u003DzQGCmQMjHdLKS = true;
    }
    else
      this.\u0023\u003DzW4kuZJQvOGhM = (CheckEdit) target;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly ChartElementEditor.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartElementEditor.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<char, bool> \u0023\u003DzMJsSoL5rX24oRR6rOw\u003D\u003D;

    internal bool \u0023\u003DzAaU0cZFSAtN99pV5BtGNHjfrUBPr(char _param1) => _param1 == '.';
  }
}
