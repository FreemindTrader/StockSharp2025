// Decompiled with JetBrains decompiler
// Type: -.dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace \u002D;

internal sealed class dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd : Control
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzyrnkYrk\u003D = DependencyProperty.Register(nameof (Angle), typeof (double), typeof (dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd), new PropertyMetadata((object) 0.0));

  public dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd);
  }

  public double Angle
  {
    get
    {
      return (double) this.GetValue(dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd.\u0023\u003DzyrnkYrk\u003D);
    }
    set
    {
      this.SetValue(dje_zPC3M3YAJUSMFFMDBKQGYAGLUKD3KDUCX54V6XBQ7HH4LKV662D8AE_ejd.\u0023\u003DzyrnkYrk\u003D, (object) value);
    }
  }
}
