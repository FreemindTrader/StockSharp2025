// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.IpAddressEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ecng.Xaml
{
  /// <summary>
  /// Editor for <see cref="T:System.Net.IPAddress" />.
  /// </summary>
  /// <summary>IpAddressEditor</summary>
  public class IpAddressEditor : UserControl, IComponentConnector
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.IpAddressEditor.Address" />.
    ///     </summary>
    public static readonly DependencyProperty AddressProperty = DependencyProperty.Register(nameof(2127279968), typeof (IPAddress), typeof (IpAddressEditor), new PropertyMetadata((object) null));
    
    internal TextBox \u0023\u003DzKwqh1bJW\u0024wnJ;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.IpAddressEditor" />.
    /// </summary>
    public IpAddressEditor()
    {
      this.InitializeComponent();
    }

    /// <summary>Address.</summary>
    public IPAddress Address
    {
      get
      {
        return (IPAddress) this.GetValue(IpAddressEditor.AddressProperty);
      }
      set
      {
        this.SetValue(IpAddressEditor.AddressProperty, (object) value);
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
      Application.LoadComponent((object) this, new Uri(nameof(2127279966), UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      if (_param1 == 1)
        this.\u0023\u003DzKwqh1bJW\u0024wnJ = (TextBox) _param2;
      else
        this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
    }
  }
}
