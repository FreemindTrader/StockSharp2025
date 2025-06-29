// Decompiled with JetBrains decompiler
// Type: -.dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd : 
  NonTopmostPopup,
  IComponentConnector
{
  
  public static readonly DependencyProperty \u0023\u003DzZ_thYDJka4Sb = DependencyProperty.Register("", typeof (IEnumerable<DevExpress.Xpf.PropertyGrid.PropertyDefinition>), typeof (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  internal dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd \u0023\u003Dzv4BS1WQ\u003D;
  
  private bool \u0023\u003DzQGCmQMjHdLKS;

  public dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd()
  {
    this.InitializeComponent();
  }

  public IEnumerable<DevExpress.Xpf.PropertyGrid.PropertyDefinition> PropertyDefinitions
  {
    get
    {
      return (IEnumerable<DevExpress.Xpf.PropertyGrid.PropertyDefinition>) this.GetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd.\u0023\u003DzZ_thYDJka4Sb);
    }
    set
    {
      this.SetValue(dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd.\u0023\u003DzZ_thYDJka4Sb, (object) value);
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this.\u0023\u003DzQGCmQMjHdLKS)
      return;
    this.\u0023\u003DzQGCmQMjHdLKS = true;
    Application.LoadComponent((object) this, new Uri("", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
  {
    if (_param1 == 1)
      this.\u0023\u003Dzv4BS1WQ\u003D = (dje_zY25VVVU5M2ZF8FXMUB8J3DLXXCBCF2R6BFXD35AJMNZAG8VPWQ3UE6ZFKM53T5BWGTPBKV9Z_ejd) _param2;
    else
      this.\u0023\u003DzQGCmQMjHdLKS = true;
  }
}
