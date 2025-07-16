// Decompiled with JetBrains decompiler
// Type: -.TooltipControl
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class TooltipControl : 
  TemplatableControl
{
  
  public static readonly DependencyProperty \u0023\u003DzEuuv4pc\u003D = DependencyProperty.Register(nameof (Selector), typeof (IDataTemplateSelector), typeof (TooltipControl), new PropertyMetadata((object) null, new PropertyChangedCallback(TooltipControl.\u0023\u003DzYfWGIYVgG9Om)));
  
  public static readonly DependencyProperty \u0023\u003Dz2CuHKWI5gsAP = DependencyProperty.Register(nameof (SelectorContext), typeof (object), typeof (TooltipControl), new PropertyMetadata(new PropertyChangedCallback(TooltipControl.\u0023\u003DzqdCmf9pyTS3icOrrZA\u003D\u003D)));

  public TooltipControl()
  {
    this.DefaultStyleKey = (object) typeof (TooltipControl);
  }

  public IDataTemplateSelector Selector
  {
    get
    {
      return (IDataTemplateSelector) this.GetValue(TooltipControl.\u0023\u003DzEuuv4pc\u003D);
    }
    set
    {
      this.SetValue(TooltipControl.\u0023\u003DzEuuv4pc\u003D, (object) value);
    }
  }

  public object SelectorContext
  {
    get
    {
      return this.GetValue(TooltipControl.\u0023\u003Dz2CuHKWI5gsAP);
    }
    set
    {
      this.SetValue(TooltipControl.\u0023\u003Dz2CuHKWI5gsAP, value);
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
    if (!(_param0 is TooltipControl cnjeD6UpthjB96FaEjd))
      return;
    if (_param1.NewValue is IDataTemplateSelector newValue)
      newValue.\u0023\u003DzPIKAksIWrmT_(new EventHandler(cnjeD6UpthjB96FaEjd.\u0023\u003Dzo0Dl\u0024GTImB3r));
    if (_param1.OldValue is IDataTemplateSelector oldValue)
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
    if (!(_param0 is TooltipControl cnjeD6UpthjB96FaEjd))
      return;
    cnjeD6UpthjB96FaEjd.\u0023\u003Dzo0Dl\u0024GTImB3r(cnjeD6UpthjB96FaEjd.SelectorContext);
  }
}
