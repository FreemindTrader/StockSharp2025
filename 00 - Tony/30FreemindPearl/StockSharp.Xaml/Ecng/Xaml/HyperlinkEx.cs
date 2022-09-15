// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.HyperlinkEx
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Ecng.Xaml
{
  /// <summary>
  /// Extended version <see cref="T:System.Windows.Documents.Hyperlink" /> that opens link automatically.
  /// </summary>
  public class HyperlinkEx : Hyperlink
  {
    
    private static readonly Uri \u0023\u003Dz\u00240kIEbNi3Q\u0024X = new Uri(nameof(2127279760), UriKind.RelativeOrAbsolute);

    static HyperlinkEx()
    {
      Hyperlink.NavigateUriProperty.OverrideMetadata(typeof (HyperlinkEx), (PropertyMetadata) new FrameworkPropertyMetadata((object) HyperlinkEx.\u0023\u003Dz\u00240kIEbNi3Q\u0024X));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.HyperlinkEx" />.
    /// </summary>
    public HyperlinkEx()
    {
      this.RequestNavigate += new RequestNavigateEventHandler(this.\u0023\u003Dzzgpb4h7KCgFEqn8hPQ\u003D\u003D);
    }

    private void \u0023\u003Dzzgpb4h7KCgFEqn8hPQ\u003D\u003D(
      object _param1,
      RequestNavigateEventArgs _param2)
    {
      if (this.NavigateUri == HyperlinkEx.\u0023\u003Dz\u00240kIEbNi3Q\u0024X)
        return;
      this.NavigateUri.ToString().OpenLink(false);
      _param2.Handled = true;
    }
  }
}
