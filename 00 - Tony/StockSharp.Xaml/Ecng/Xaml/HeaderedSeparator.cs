// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.HeaderedSeparator
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System.Windows;
using System.Windows.Controls;

namespace Ecng.Xaml
{
  /// <summary>Separator with header.</summary>
  public class HeaderedSeparator : Control
  {
    /// <summary>
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// <see cref="P:Ecng.Xaml.HeaderedSeparator.Header" />.
    ///     </summary>
    public static DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(2127279663), typeof (string), typeof (HeaderedSeparator));

    /// <summary>Header.</summary>
    public string Header
    {
      get
      {
        return (string) this.GetValue(HeaderedSeparator.HeaderProperty);
      }
      set
      {
        this.SetValue(HeaderedSeparator.HeaderProperty, (object) value);
      }
    }
  }
}
