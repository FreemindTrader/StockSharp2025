// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Yandex.YandexLoginWindowOAuthProvider
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Disk.SDK;
using Ecng.Backup.Yandex;
using Ecng.Common;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Ecng.Xaml.Yandex
{
  /// <summary>Authentization handler.</summary>
  public class YandexLoginWindowOAuthProvider : IYandexDiskOAuthProvider
  {
    
    private readonly Func<Window> \u0023\u003DzZZNDRs4H3nuR;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Yandex.YandexLoginWindow" />.
    /// </summary>
    /// <param name="getOwner">Get window's owner handler.</param>
    public YandexLoginWindowOAuthProvider(Func<Window> getOwner = null)
    {
      this.\u0023\u003DzZZNDRs4H3nuR = getOwner;
      if (this.\u0023\u003DzZZNDRs4H3nuR != null)
        return;
      this.\u0023\u003DzZZNDRs4H3nuR = YandexLoginWindowOAuthProvider.SomeShit.ShitMethod01 ?? (YandexLoginWindowOAuthProvider.SomeShit.ShitMethod01 = new Func<Window>(YandexLoginWindowOAuthProvider.SomeShit.ShitMethod02.\u0023\u003DzuCUEN5lGDAyFdXoMWw\u003D\u003D));
    }

    string IYandexDiskOAuthProvider.\u0023\u003DzPqTuIOerdNFk9IB9XHNor\u0024ruP4XTM5XmS9Qwbx_SwZB\u0024()
    {
      YandexLoginWindowOAuthProvider.\u0023\u003DzWiCnw9A2mb6WXQNQ6w\u003D\u003D cnw9A2mb6WxqnQ6w = new YandexLoginWindowOAuthProvider.\u0023\u003DzWiCnw9A2mb6WXQNQ6w\u003D\u003D();
      cnw9A2mb6WxqnQ6w._delayActionHelper = this;
      bool flag = false;
      cnw9A2mb6WxqnQ6w.\u0023\u003DzUGk3x08\u003D = (string) null;
      cnw9A2mb6WxqnQ6w.\u0023\u003DzajTO4UI\u003D = (Exception) null;
      if (!GuiDispatcher.GlobalDispatcher.Dispatcher.GuiSync<bool>(new Func<bool>(cnw9A2mb6WxqnQ6w.\u0023\u003Dzbmq4nUuiW3U3Or7tv4V2AF1sdz7WTju9S9gQr\u0024j\u0024J3f6\u0024qqJBgXCyWc\u003D)))
        flag = true;
      Exception zajTo4Ui = cnw9A2mb6WxqnQ6w.\u0023\u003DzajTO4UI\u003D;
      if (zajTo4Ui != null)
        zajTo4Ui.Throw();
      if (flag)
        return string.Empty;
      return cnw9A2mb6WxqnQ6w.\u0023\u003DzUGk3x08\u003D;
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly YandexLoginWindowOAuthProvider.SomeShit ShitMethod02 = new YandexLoginWindowOAuthProvider.SomeShit();
      public static Func<Window> ShitMethod01;

      internal Window \u0023\u003DzuCUEN5lGDAyFdXoMWw\u003D\u003D()
      {
        return Application.Current.MainWindow;
      }
    }

    private sealed class \u0023\u003DzWiCnw9A2mb6WXQNQ6w\u003D\u003D
    {
      public string \u0023\u003DzUGk3x08\u003D;
      public Exception \u0023\u003DzajTO4UI\u003D;
      public YandexLoginWindowOAuthProvider _delayActionHelper;

      internal YandexLoginWindow \u0023\u003DqlVcxwmr1aI5OAXvi1zGPpomxAS7P\u0024OE2cd1fxDHGlrVFTEcF4X4rdJUhOeetrSaMZyBQoep4yISh4L_lhq2XO8mfiGGbI6xix3Cq3GmAGWQ\u003D()
      {
        YandexLoginWindow yandexLoginWindow = new YandexLoginWindow();
        yandexLoginWindow.AuthCompleted += new EventHandler<GenericSdkEventArgs<string>>(this.\u0023\u003DzZY6e3RQlc4w2h2Td5hpdBE0COLTZEFs_hgjhqqxhL89MIO_4I7_C\u0024Ds\u003D);
        return yandexLoginWindow;
      }

      internal void \u0023\u003DzZY6e3RQlc4w2h2Td5hpdBE0COLTZEFs_hgjhqqxhL89MIO_4I7_C\u0024Ds\u003D(
        [Nullable(2)] object _param1,
        GenericSdkEventArgs<string> _param2)
      {
        if (_param2.Error == null)
          this.\u0023\u003DzUGk3x08\u003D = _param2.Result;
        else
          this.\u0023\u003DzajTO4UI\u003D = (Exception) _param2.Error;
      }

      internal bool \u0023\u003Dzbmq4nUuiW3U3Or7tv4V2AF1sdz7WTju9S9gQr\u0024j\u0024J3f6\u0024qqJBgXCyWc\u003D()
      {
        Window owner = this._delayActionHelper.\u0023\u003DzZZNDRs4H3nuR();
        YandexLoginWindow yandexLoginWindow = this.\u0023\u003DqlVcxwmr1aI5OAXvi1zGPpomxAS7P\u0024OE2cd1fxDHGlrVFTEcF4X4rdJUhOeetrSaMZyBQoep4yISh4L_lhq2XO8mfiGGbI6xix3Cq3GmAGWQ\u003D();
        if (owner != null)
          return XamlHelper.ShowModal((Window) yandexLoginWindow, owner);
        bool? nullable = yandexLoginWindow.ShowDialog();
        return nullable.GetValueOrDefault() & nullable.HasValue;
      }
    }
  }
}
