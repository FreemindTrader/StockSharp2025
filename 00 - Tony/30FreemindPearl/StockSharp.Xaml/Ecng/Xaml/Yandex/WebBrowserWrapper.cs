// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Yandex.WebBrowserWrapper
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Disk.SDK;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Ecng.Xaml.Yandex
{
  /// <summary>
  /// Represents wrapper for platform specific WebBrowser component.
  /// </summary>
  public class WebBrowserWrapper : IBrowser
  {
    
    private readonly WebBrowser \u0023\u003DzRJTDiSc\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Yandex.WebBrowserWrapper" /> class.
    /// </summary>
    /// <param name="browser">The browser.</param>
    public WebBrowserWrapper(WebBrowser browser)
    {
      this.\u0023\u003DzRJTDiSc\u003D = browser;
      this.\u0023\u003DzRJTDiSc\u003D.Navigating += new NavigatingCancelEventHandler(this.\u0023\u003Dzxub2mRNqRnkL);
    }

    /// <summary>Navigates to the specified URL.</summary>
    /// <param name="url">The URL.</param>
    public void Navigate(string url)
    {
      this.\u0023\u003DzRJTDiSc\u003D.Navigate(new Uri(url));
    }

    /// <summary>Occurs just before navigation to a document.</summary>
    public event EventHandler<GenericSdkEventArgs<string>> Navigating;

    private void \u0023\u003Dzxub2mRNqRnkL(object _param1, NavigatingCancelEventArgs _param2)
    {
      EventHandler<GenericSdkEventArgs<string>> zX5CurS0 = this.\u0023\u003DzX5CURS0\u003D;
      if (zX5CurS0 == null)
        return;
      zX5CurS0((object) this, new GenericSdkEventArgs<string>(_param2.Uri.ToString()));
    }
  }
}
