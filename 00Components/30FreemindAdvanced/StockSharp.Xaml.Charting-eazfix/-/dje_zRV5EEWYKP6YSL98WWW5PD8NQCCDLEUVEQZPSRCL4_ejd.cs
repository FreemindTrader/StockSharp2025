// Decompiled with JetBrains decompiler
// Type: -.dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace \u002D;

internal abstract class dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd : 
  ContentControl,
  \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo8HI_6JOBYhgDQ\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzTeFEnJJlKZlL = DependencyProperty.Register(nameof (DefaultTemplate), typeof (DataTemplate), typeof (dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private EventHandler \u0023\u003DzIXxjpEgBlZml;

  public DataTemplate DefaultTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd.\u0023\u003DzTeFEnJJlKZlL);
    }
    set
    {
      this.SetValue(dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd.\u0023\u003DzTeFEnJJlKZlL, (object) value);
    }
  }

  protected void \u0023\u003DzCZf_oX5aGk\u0024Y()
  {
    if (this.ContentTemplate != null)
      return;
    this.ContentTemplate = this.\u0023\u003Dzmy_tWbS7jzNB(this.Content, (DependencyObject) this);
  }

  public virtual DataTemplate \u0023\u003Dzmy_tWbS7jzNB(object _param1, DependencyObject _param2)
  {
    return this.DefaultTemplate;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzPIKAksIWrmT_(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzIXxjpEgBlZml;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzIXxjpEgBlZml, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzGdEyHkIpEpoz(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzIXxjpEgBlZml;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzIXxjpEgBlZml, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  protected override void OnContentChanged(object _param1, object _param2)
  {
    base.OnContentChanged(_param1, _param2);
    this.ContentTemplate = this.\u0023\u003Dzmy_tWbS7jzNB(_param2, (DependencyObject) this);
  }

  protected void \u0023\u003DzfpkvVDHp_6LL()
  {
    EventHandler zIxxjpEgBlZml = this.\u0023\u003DzIXxjpEgBlZml;
    if (zIxxjpEgBlZml == null)
      return;
    zIxxjpEgBlZml((object) this, EventArgs.Empty);
  }

  protected static void \u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zRV5EEWYKP6YSL98WWW5PD8NQCCDLEUVEQZPSRCL4_ejd nqccdleuveqzpsrcL4Ejd))
      return;
    nqccdleuveqzpsrcL4Ejd.\u0023\u003DzCZf_oX5aGk\u0024Y();
    nqccdleuveqzpsrcL4Ejd.\u0023\u003DzfpkvVDHp_6LL();
  }
}
