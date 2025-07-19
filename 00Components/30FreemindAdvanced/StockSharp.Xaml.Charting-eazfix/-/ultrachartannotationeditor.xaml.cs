// Decompiled with JetBrains decompiler
// Type: -.UltrachartAnnotationEditor
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Charting;

public sealed class UltrachartAnnotationEditor : 
  NonTopmostPopup,
  IComponentConnector
{
  
  public static readonly DependencyProperty \u0023\u003DzZ_thYDJka4Sb = DependencyProperty.Register(nameof (PropertyDefinitions), typeof (IEnumerable<DevExpress.Xpf.PropertyGrid.PropertyDefinition>), typeof (UltrachartAnnotationEditor), new PropertyMetadata((PropertyChangedCallback) null));
  
  public UltrachartAnnotationEditor _annotationEditor;
  
  private bool _someInternalBoolean;

  public UltrachartAnnotationEditor()
  {
    this.InitializeComponent();
  }

  public IEnumerable<DevExpress.Xpf.PropertyGrid.PropertyDefinition> PropertyDefinitions
  {
    get
    {
      return (IEnumerable<DevExpress.Xpf.PropertyGrid.PropertyDefinition>) this.GetValue(UltrachartAnnotationEditor.\u0023\u003DzZ_thYDJka4Sb);
    }
    set
    {
      this.SetValue(UltrachartAnnotationEditor.\u0023\u003DzZ_thYDJka4Sb, (object) value);
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._someInternalBoolean)
      return;
    this._someInternalBoolean = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Xaml.Charting;V5.0.0;component/ultrachart/ultrachartannotationeditor.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    if (_param1 == 1)
      this._annotationEditor = (UltrachartAnnotationEditor) _param2;
    else
      this._someInternalBoolean = true;
  }
}
