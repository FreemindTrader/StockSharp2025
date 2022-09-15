// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.NonTopmostPopup
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace Ecng.Xaml
{
  /// <summary>Non topmost popup.</summary>
  /// <remarks>http://chriscavanagh.wordpress.com/2008/08/13/non-topmost-wpf-popup/</remarks>
  public class NonTopmostPopup : Popup
  {
    /// <summary>
    /// </summary>
    public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof (NonTopmostPopup), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzP6qs3\u0024IzMxQQ))));
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty NoActivateProperty = DependencyProperty.Register(nameof(2127280070), typeof (bool), typeof (NonTopmostPopup), new PropertyMetadata((object) false, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzP6qs3\u0024IzMxQQ))));

    /// <summary>
    /// </summary>
    public bool Topmost
    {
      get
      {
        return (bool) this.GetValue(NonTopmostPopup.TopmostProperty);
      }
      set
      {
        this.SetValue(NonTopmostPopup.TopmostProperty, (object) value);
      }
    }

    /// <summary>
    /// </summary>
    public bool NoActivate
    {
      get
      {
        return (bool) this.GetValue(NonTopmostPopup.NoActivateProperty);
      }
      set
      {
        this.SetValue(NonTopmostPopup.NoActivateProperty, (object) value);
      }
    }

    private static void \u0023\u003DzP6qs3\u0024IzMxQQ(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      ((NonTopmostPopup) _param0).\u0023\u003DzYbSQrYo\u003D();
    }

    /// <inheritdoc />
    protected override void OnOpened(EventArgs e)
    {
      this.\u0023\u003DzYbSQrYo\u003D();
      base.OnOpened(e);
    }

    private void \u0023\u003DzYbSQrYo\u003D()
    {
      if (this.Child == null)
        return;
      PresentationSource presentationSource = PresentationSource.FromVisual((Visual) this.Child);
      if (presentationSource == null)
        return;
      IntPtr handle = ((HwndSource) presentationSource).Handle;
      NonTopmostPopup.RECT dje_zCKWJ3383STK2ZFQ_ejd;
      if (!NonTopmostPopup.dje_zCNJ4ZP3LY7F5UW2_ejd(handle, out dje_zCKWJ3383STK2ZFQ_ejd))
        return;
      NonTopmostPopup.dje_zUNA2WU3MGSQU74Z_ejd(handle, this.Topmost ? -1 : -2, dje_zCKWJ3383STK2ZFQ_ejd.Left, dje_zCKWJ3383STK2ZFQ_ejd.Top, (int) this.Width, (int) this.Height, this.NoActivate ? 16 : 0);
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool dje_zCNJ4ZP3LY7F5UW2_ejd(
      IntPtr dje_zXSB79HXY_ejd,
      out NonTopmostPopup.RECT dje_zCKWJ3383STK2ZFQ_ejd);

    [DllImport("user32", EntryPoint = "SetWindowPos")]
    private static extern int dje_zUNA2WU3MGSQU74Z_ejd(
      IntPtr dje_zXSB79HXY_ejd,
      int dje_zUSFHB73NS8XARXA_ejd,
      int dje_zE8VRDHPW_ejd,
      int dje_z4YFRLMQS_ejd,
      int dje_zNGTFWRKQ_ejd,
      int dje_zB5K5KWB2_ejd,
      int dje_z579QX4EG_ejd);

    /// <summary>
    /// </summary>
    public struct RECT
    {
      /// <summary>
      /// </summary>
      public int Left;
      /// <summary>
      /// </summary>
      public int Top;
      /// <summary>
      /// </summary>
      public int Right;
      /// <summary>
      /// </summary>
      public int Bottom;
    }
  }
}
