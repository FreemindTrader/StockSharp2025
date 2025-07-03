// Decompiled with JetBrains decompiler
// Type: -.dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace \u002D;

internal sealed class dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd : 
  dje_zWLGYTTT5DUFM55EFRPBBAZMZXPMVBQNX4VDZEWMJLU768RLDDSRVTC6SXVJQ2DSMRBMWZUQJ5VY6CVZ_ejd
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzEuuv4pc\u003D = DependencyProperty.Register(nameof (Selector), typeof (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D), typeof (dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd.\u0023\u003DzYfWGIYVgG9Om)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz2CuHKWI5gsAP = DependencyProperty.Register(nameof (SelectorContext), typeof (object), typeof (dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd.\u0023\u003DzqdCmf9pyTS3icOrrZA\u003D\u003D)));

  public dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd);
  }

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D Selector
  {
    get
    {
      return (\u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D) this.GetValue(dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd.\u0023\u003DzEuuv4pc\u003D);
    }
    set
    {
      this.SetValue(dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd.\u0023\u003DzEuuv4pc\u003D, (object) value);
    }
  }

  public object SelectorContext
  {
    get
    {
      return this.GetValue(dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd.\u0023\u003Dz2CuHKWI5gsAP);
    }
    set
    {
      this.SetValue(dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd.\u0023\u003Dz2CuHKWI5gsAP, value);
    }
  }

  private void \u0023\u003Dzo0Dl\u0024GTImB3r(object _param1)
  {
    if (this.Selector == null)
      return;
    this.ContentTemplate = this.Selector.\u0023\u003Dzmy_tWbS7jzNB(_param1, (DependencyObject) this);
  }

  private static void \u0023\u003DzYfWGIYVgG9Om(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd cnjeD6UpthjB96FaEjd))
      return;
    if (_param1.NewValue is \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D newValue)
      newValue.\u0023\u003DzPIKAksIWrmT_(new EventHandler(cnjeD6UpthjB96FaEjd.\u0023\u003Dzo0Dl\u0024GTImB3r));
    if (_param1.OldValue is \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D oldValue)
      oldValue.\u0023\u003DzGdEyHkIpEpoz(new EventHandler(cnjeD6UpthjB96FaEjd.\u0023\u003Dzo0Dl\u0024GTImB3r));
    cnjeD6UpthjB96FaEjd.\u0023\u003Dzo0Dl\u0024GTImB3r(cnjeD6UpthjB96FaEjd.SelectorContext);
  }

  private void \u0023\u003Dzo0Dl\u0024GTImB3r(object _param1, EventArgs _param2)
  {
    this.\u0023\u003Dzo0Dl\u0024GTImB3r(this.SelectorContext);
  }

  private static void \u0023\u003DzqdCmf9pyTS3icOrrZA\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_z5Q63TZN6Q7Z55DU6ZVEKXJV8USEH64ZT7ZSGP6B98ECYG24PF5CNJED6UPTHJB96FA_ejd cnjeD6UpthjB96FaEjd))
      return;
    cnjeD6UpthjB96FaEjd.\u0023\u003Dzo0Dl\u0024GTImB3r(cnjeD6UpthjB96FaEjd.SelectorContext);
  }
}
