// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.EndPointListEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  /// <summary>EndPointListEditor</summary>
  public class EndPointListEditor : UserControl, IComponentConnector
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.EndPointListEditor.EndPoints" />.
    ///     </summary>
    public static readonly DependencyProperty EndPointsProperty = DependencyProperty.Register(nameof(2127280186), typeof (IEnumerable<EndPoint>), typeof (EndPointListEditor), new PropertyMetadata((object) Enumerable.Empty<EndPoint>()));
    
    internal TextEdit \u0023\u003DzZtuU8es\u003D;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// </summary>
    public EndPointListEditor()
    {
      this.InitializeComponent();
    }

    /// <summary>Addresses.</summary>
    public IEnumerable<EndPoint> EndPoints
    {
      get
      {
        return (IEnumerable<EndPoint>) this.GetValue(EndPointListEditor.EndPointsProperty);
      }
      set
      {
        this.SetValue(EndPointListEditor.EndPointsProperty, (object) value);
      }
    }

    /// <summary>InitializeComponent</summary>
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127280170), UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [DebuggerNonUserCode]
    internal Delegate \u0023\u003Dzk_6RQsm5oL9L(Type _param1, string _param2)
    {
      return Delegate.CreateDelegate(_param1, (object) this, _param2);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      if (_param1 == 1)
        this.\u0023\u003DzZtuU8es\u003D = (TextEdit) _param2;
      else
        this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
    }
  }
}
