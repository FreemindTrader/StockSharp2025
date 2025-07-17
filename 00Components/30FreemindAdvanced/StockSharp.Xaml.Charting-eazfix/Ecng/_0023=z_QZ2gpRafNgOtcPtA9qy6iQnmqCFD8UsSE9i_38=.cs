// Decompiled with JetBrains decompiler
// Type: #=z_QZ2gpRafNgOtcPtA9qy6iQnmqCFD8UsSE9i_38=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Diagnostics;
using System.Threading;

#nullable disable
public sealed class \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6iQnmqCFD8UsSE9i_38\u003D : 
  ChartModifierBase
{
  
  private Action<ModifierMouseArgs> MouseUpEvent;
  
  private Action<ModifierMouseArgs> MouseDownEvent;

  public void \u0023\u003DzQNMSGlzReVSKbDSdEA\u003D\u003D(
    Action<ModifierMouseArgs> _param1)
  {
    Action<ModifierMouseArgs> action = this.MouseUpEvent;
    Action<ModifierMouseArgs> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ModifierMouseArgs>>(ref this.MouseUpEvent, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzKMNJUrebJ6bHfEfMgg\u003D\u003D(
    Action<ModifierMouseArgs> _param1)
  {
    Action<ModifierMouseArgs> action = this.MouseUpEvent;
    Action<ModifierMouseArgs> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ModifierMouseArgs>>(ref this.MouseUpEvent, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzHs7QOJE3efiH3JF5Bw\u003D\u003D(
    Action<ModifierMouseArgs> _param1)
  {
    Action<ModifierMouseArgs> action = this.MouseDownEvent;
    Action<ModifierMouseArgs> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ModifierMouseArgs>>(ref this.MouseDownEvent, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void \u0023\u003DzcMDiaOXtryw1mZGXQA\u003D\u003D(
    Action<ModifierMouseArgs> _param1)
  {
    Action<ModifierMouseArgs> action = this.MouseDownEvent;
    Action<ModifierMouseArgs> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<ModifierMouseArgs>>(ref this.MouseDownEvent, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  public override void OnModifierMouseDown(
    ModifierMouseArgs _param1)
  {
    base.OnModifierMouseDown(_param1);
    if (_param1.MouseButtons() != (MouseButtons) 1)
      return;
    Action<ModifierMouseArgs> zrmlvxkfuVpe1 = this.MouseDownEvent;
    if (zrmlvxkfuVpe1 == null)
      return;
    zrmlvxkfuVpe1(_param1);
  }

  public override void \u0023\u003DzQTINWhMByBmJ(
    ModifierMouseArgs _param1)
  {
    base.\u0023\u003DzQTINWhMByBmJ(_param1);
    Action<ModifierMouseArgs> z0oGdcgXnO1Ze = this.MouseUpEvent;
    if (z0oGdcgXnO1Ze == null)
      return;
    z0oGdcgXnO1Ze(_param1);
  }
}
