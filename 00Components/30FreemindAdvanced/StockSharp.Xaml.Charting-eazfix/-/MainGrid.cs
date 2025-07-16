// Decompiled with JetBrains decompiler
// Type: -.MainGrid
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace StockSharp.Charting;

public sealed class MainGrid : 
  Grid,
  IPublishMouseEvents,
  \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D,
  IHitTestable
{
  
  private EventHandler<TouchManipulationEventArgs> \u0023\u003DzzsmHSkfDouc4;
  
  private EventHandler<TouchManipulationEventArgs> \u0023\u003Dz43zUgeo\u003D;
  
  private EventHandler<TouchManipulationEventArgs> \u0023\u003DzoLEhkoxH9YzJ;
  
  private readonly IList<TouchPoint> \u0023\u003Dzt0Qmpsjfylij = (IList<TouchPoint>) new List<TouchPoint>();
  
  private readonly IList<TouchPoint> \u0023\u003DzDUv3Acj1oZXB = (IList<TouchPoint>) new List<TouchPoint>();
  
  private readonly IList<TouchPoint> \u0023\u003Dzicg_lR7qq4W7 = (IList<TouchPoint>) new List<TouchPoint>();
  
  private readonly RoutedEventHandler \u0023\u003DzDBof02j7wNLE;
  
  private readonly RoutedEventHandler \u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;

  public MainGrid()
  {
    MainGrid.SomeClass398 jq9Llz3ahZ2LrQl4 = new MainGrid.SomeClass398();
    this.PreviewMouseDown += new MouseButtonEventHandler(this.\u0023\u003DzGQwYmeYUIIaAjk_61g\u003D\u003D);
    this.PreviewMouseUp += new MouseButtonEventHandler(this.\u0023\u003DznkUq71oWHpDplx3htA\u003D\u003D);
    jq9Llz3ahZ2LrQl4.\u0023\u003DzZla_cGQ\u003D = new TouchFrameEventHandler(this.\u0023\u003Dzf8Py\u0024SRf1KtV);
    this.\u0023\u003DzDBof02j7wNLE = new RoutedEventHandler(jq9Llz3ahZ2LrQl4.\u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D);
    this.\u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D = new RoutedEventHandler(jq9Llz3ahZ2LrQl4.OnChartAreaElementsRemovingAt);
    this.Loaded += this.\u0023\u003DzDBof02j7wNLE;
    this.Unloaded += this.\u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzD\u0024VBwMxDvQPr(
    EventHandler<TouchManipulationEventArgs> _param1)
  {
    EventHandler<TouchManipulationEventArgs> eventHandler = this.\u0023\u003DzzsmHSkfDouc4;
    EventHandler<TouchManipulationEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<TouchManipulationEventArgs>>(ref this.\u0023\u003DzzsmHSkfDouc4, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzARbagqKDKSpP(
    EventHandler<TouchManipulationEventArgs> _param1)
  {
    EventHandler<TouchManipulationEventArgs> eventHandler = this.\u0023\u003DzzsmHSkfDouc4;
    EventHandler<TouchManipulationEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<TouchManipulationEventArgs>>(ref this.\u0023\u003DzzsmHSkfDouc4, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzMGZAEi71Lpw4(
    EventHandler<TouchManipulationEventArgs> _param1)
  {
    EventHandler<TouchManipulationEventArgs> eventHandler = this.\u0023\u003Dz43zUgeo\u003D;
    EventHandler<TouchManipulationEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<TouchManipulationEventArgs>>(ref this.\u0023\u003Dz43zUgeo\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzuEG7DhKs0pUh(
    EventHandler<TouchManipulationEventArgs> _param1)
  {
    EventHandler<TouchManipulationEventArgs> eventHandler = this.\u0023\u003Dz43zUgeo\u003D;
    EventHandler<TouchManipulationEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<TouchManipulationEventArgs>>(ref this.\u0023\u003Dz43zUgeo\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzHc1yVlkYVP_y(
    EventHandler<TouchManipulationEventArgs> _param1)
  {
    EventHandler<TouchManipulationEventArgs> eventHandler = this.\u0023\u003DzoLEhkoxH9YzJ;
    EventHandler<TouchManipulationEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<TouchManipulationEventArgs>>(ref this.\u0023\u003DzoLEhkoxH9YzJ, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzeFA5d6tiJ6hI(
    EventHandler<TouchManipulationEventArgs> _param1)
  {
    EventHandler<TouchManipulationEventArgs> eventHandler = this.\u0023\u003DzoLEhkoxH9YzJ;
    EventHandler<TouchManipulationEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<TouchManipulationEventArgs>>(ref this.\u0023\u003DzoLEhkoxH9YzJ, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public event MouseButtonEventHandler MouseMiddleButtonDown;

  public event MouseButtonEventHandler MouseMiddleButtonUp;

  private void \u0023\u003DznkUq71oWHpDplx3htA\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (_param2.ChangedButton != MouseButton.Middle)
      return;
    MouseButtonEventHandler zKcxoDk6i3tb = this.\u0023\u003DzKCXODk6i\u00243tb;
    if (zKcxoDk6i3tb == null)
      return;
    zKcxoDk6i3tb(_param1, _param2);
  }

  private void \u0023\u003DzGQwYmeYUIIaAjk_61g\u003D\u003D(
    object _param1,
    MouseButtonEventArgs _param2)
  {
    if (_param2.ChangedButton != MouseButton.Middle)
      return;
    MouseButtonEventHandler zDjofnu4HzDa9 = this.\u0023\u003DzDjofnu4HzDA9;
    if (zDjofnu4HzDa9 == null)
      return;
    zDjofnu4HzDa9(_param1, _param2);
  }

  public void \u0023\u003Dz2_LGffrmWHwH()
  {
    Touch.FrameReported -= new TouchFrameEventHandler(this.\u0023\u003Dzf8Py\u0024SRf1KtV);
    this.Loaded -= this.\u0023\u003DzDBof02j7wNLE;
    this.Unloaded -= this.\u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;
  }

  private void \u0023\u003DzSJt0ohabg457(
    object _param1,
    TouchManipulationEventArgs _param2)
  {
    EventHandler<TouchManipulationEventArgs> zzsmHskfDouc4 = this.\u0023\u003DzzsmHSkfDouc4;
    if (zzsmHskfDouc4 == null)
      return;
    zzsmHskfDouc4(_param1, _param2);
  }

  private void \u0023\u003DzPicpNtoN07wg(
    object _param1,
    TouchManipulationEventArgs _param2)
  {
    EventHandler<TouchManipulationEventArgs> zoLehkoxH9YzJ = this.\u0023\u003DzoLEhkoxH9YzJ;
    if (zoLehkoxH9YzJ == null)
      return;
    zoLehkoxH9YzJ(_param1, _param2);
  }

  private void \u0023\u003Dz1EkwwSOVzlCI(
    object _param1,
    TouchManipulationEventArgs _param2)
  {
    EventHandler<TouchManipulationEventArgs> z43zUgeo = this.\u0023\u003Dz43zUgeo\u003D;
    if (z43zUgeo == null)
      return;
    z43zUgeo(_param1, _param2);
  }

  private void \u0023\u003Dzf8Py\u0024SRf1KtV(object _param1, TouchFrameEventArgs _param2)
  {
    TouchPointCollection touchPoints;
    try
    {
      touchPoints = _param2.GetTouchPoints((IInputElement) this);
    }
    catch (Exception ex)
    {
      return;
    }
    this.\u0023\u003Dzt0Qmpsjfylij.Clear();
    this.\u0023\u003DzDUv3Acj1oZXB.Clear();
    this.\u0023\u003Dzicg_lR7qq4W7.Clear();
    foreach (TouchPoint touchPoint in (Collection<TouchPoint>) touchPoints)
    {
      switch (touchPoint.Action)
      {
        case TouchAction.Down:
          this.\u0023\u003Dzt0Qmpsjfylij.Add(touchPoint);
          continue;
        case TouchAction.Move:
          this.\u0023\u003Dzicg_lR7qq4W7.Add(touchPoint);
          continue;
        case TouchAction.Up:
          this.\u0023\u003DzDUv3Acj1oZXB.Add(touchPoint);
          continue;
        default:
          continue;
      }
    }
    if (this.\u0023\u003DzDUv3Acj1oZXB.Count > 0)
      this.\u0023\u003DzPicpNtoN07wg(_param1, new TouchManipulationEventArgs((IEnumerable<TouchPoint>) this.\u0023\u003DzDUv3Acj1oZXB));
    if (this.\u0023\u003Dzicg_lR7qq4W7.Count > 0)
      this.\u0023\u003Dz1EkwwSOVzlCI(_param1, new TouchManipulationEventArgs((IEnumerable<TouchPoint>) this.\u0023\u003Dzicg_lR7qq4W7));
    if (this.\u0023\u003Dzt0Qmpsjfylij.Count <= 0)
      return;
    this.\u0023\u003DzSJt0ohabg457(_param1, new TouchManipulationEventArgs((IEnumerable<TouchPoint>) this.\u0023\u003Dzt0Qmpsjfylij));
  }

  public Point TranslatePoint(
    Point _param1,
    IHitTestable _param2)
  {
    return this.\u0023\u003DzaPPLsvfM_Sst(_param1, _param2);
  }

  public bool IsPointWithinBounds(Point _param1) => this.\u0023\u003DzbOxVzAyGdX66(_param1);

  public Rect GetBoundsRelativeTo(
    IHitTestable _param1)
  {
    return this.\u0023\u003DzdC9whUui_gN\u0024(_param1);
  }

  double IHitTestable.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double IHitTestable.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }

  private sealed class SomeClass398
  {
    public TouchFrameEventHandler \u0023\u003DzZla_cGQ\u003D;

    public void \u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D(
      object _param1,
      RoutedEventArgs _param2)
    {
      Touch.FrameReported -= this.\u0023\u003DzZla_cGQ\u003D;
      Touch.FrameReported += this.\u0023\u003DzZla_cGQ\u003D;
    }

    public void OnChartAreaElementsRemovingAt(
      object _param1,
      RoutedEventArgs _param2)
    {
      Touch.FrameReported -= this.\u0023\u003DzZla_cGQ\u003D;
    }
  }
}
