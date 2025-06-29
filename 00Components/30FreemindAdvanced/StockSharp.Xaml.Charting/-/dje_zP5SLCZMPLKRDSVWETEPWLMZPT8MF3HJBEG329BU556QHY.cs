// Decompiled with JetBrains decompiler
// Type: -.dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

#nullable disable
namespace StockSharp.Xaml.Charting;

[TemplatePart(Name = "PART_ContentHost", Type = typeof (ContentPresenter))]
[TemplatePart(Name = "PART_Header", Type = typeof (Grid))]
[TemplatePart(Name = "PART_TopSplitter", Type = typeof (Thumb))]
internal sealed class dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd : ContentControl
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzZXWtkeeJbx_j = DependencyProperty.Register(XXX.SSS(-539332590), typeof (DataTemplate), typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd.\u0023\u003Dz\u0024YszLknqkUvS)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private EventHandler<DragDeltaEventArgs> \u0023\u003DzKoryGH8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private EventHandler<DragCompletedEventArgs> \u0023\u003DzsVjPoH0ZQke5;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ContentPresenter \u0023\u003DzBexXE3VONLO6;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Thumb \u0023\u003DzJUyG4Ire5BiC;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Grid \u0023\u003DzIcIt5i8Z\u0024_b2;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzpVA9oSWG8tMHnyyPMQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dz6JKKH2a32fJ1lVqWOA\u003D\u003D;

  public dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd);
    this.SetCurrentValue(FrameworkElement.MinHeightProperty, (object) 50.0);
    this.SetCurrentValue(FrameworkElement.HeightProperty, (object) 50.0);
  }

  public void \u0023\u003Dz0hN6sXzm\u0024hNI(EventHandler<DragDeltaEventArgs> _param1)
  {
    EventHandler<DragDeltaEventArgs> eventHandler = this.\u0023\u003DzKoryGH8\u003D;
    EventHandler<DragDeltaEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<DragDeltaEventArgs>>(ref this.\u0023\u003DzKoryGH8\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public void \u0023\u003DztDKfd8Um6eoX(EventHandler<DragDeltaEventArgs> _param1)
  {
    EventHandler<DragDeltaEventArgs> eventHandler = this.\u0023\u003DzKoryGH8\u003D;
    EventHandler<DragDeltaEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<DragDeltaEventArgs>>(ref this.\u0023\u003DzKoryGH8\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public void \u0023\u003Dzl15u7RiCXQ5Z\u0024LD2pw\u003D\u003D(
    EventHandler<DragCompletedEventArgs> _param1)
  {
    EventHandler<DragCompletedEventArgs> eventHandler = this.\u0023\u003DzsVjPoH0ZQke5;
    EventHandler<DragCompletedEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<DragCompletedEventArgs>>(ref this.\u0023\u003DzsVjPoH0ZQke5, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public void \u0023\u003DzDSAYx73Sokd65zKLxQ\u003D\u003D(
    EventHandler<DragCompletedEventArgs> _param1)
  {
    EventHandler<DragCompletedEventArgs> eventHandler = this.\u0023\u003DzsVjPoH0ZQke5;
    EventHandler<DragCompletedEventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<DragCompletedEventArgs>>(ref this.\u0023\u003DzsVjPoH0ZQke5, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  public DataTemplate HeaderTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd.\u0023\u003DzZXWtkeeJbx_j);
    }
    set
    {
      this.SetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd.\u0023\u003DzZXWtkeeJbx_j, (object) value);
    }
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzBexXE3VONLO6 = (ContentPresenter) this.GetTemplateChild(XXX.SSS(-539332359));
    this.\u0023\u003DzIcIt5i8Z\u0024_b2 = (Grid) this.GetTemplateChild(XXX.SSS(-539332410));
    this.\u0023\u003DzJUyG4Ire5BiC = (Thumb) this.GetTemplateChild(XXX.SSS(-539332400));
    if (this.\u0023\u003DzJUyG4Ire5BiC != null)
    {
      this.\u0023\u003DzJUyG4Ire5BiC.DragDelta += new DragDeltaEventHandler(this.\u0023\u003DzlMFKvzCL8Fzd);
      this.\u0023\u003DzJUyG4Ire5BiC.DragCompleted += new DragCompletedEventHandler(this.\u0023\u003DzrQ4U1VmG57b8);
    }
    this.\u0023\u003Dz2YTeZYpcmqcM();
  }

  internal double \u0023\u003DzZcUjmkQY5ewz()
  {
    Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
    this.\u0023\u003DzIcIt5i8Z\u0024_b2.Measure(availableSize);
    this.\u0023\u003DzJUyG4Ire5BiC.Measure(availableSize);
    double minHeight = this.MinHeight;
    Size desiredSize = this.\u0023\u003DzIcIt5i8Z\u0024_b2.DesiredSize;
    double height1 = desiredSize.Height;
    desiredSize = this.\u0023\u003DzJUyG4Ire5BiC.DesiredSize;
    double height2 = desiredSize.Height;
    double val2 = height1 + height2;
    return Math.Max(minHeight, val2);
  }

  private void \u0023\u003DzrQ4U1VmG57b8(object _param1, DragCompletedEventArgs _param2)
  {
    this.\u0023\u003DzswQwe\u0024nkJy88xtrQBw\u003D\u003D(_param2);
  }

  private void \u0023\u003DzlMFKvzCL8Fzd(object _param1, DragDeltaEventArgs _param2)
  {
    this.\u0023\u003Dz6JKKH2a32fJ1lVqWOA\u003D\u003D = _param2.HorizontalChange;
    this.\u0023\u003DzpVA9oSWG8tMHnyyPMQ\u003D\u003D = _param2.VerticalChange;
    this.\u0023\u003DzM004Fv4\u003D(new DragDeltaEventArgs(this.\u0023\u003Dz6JKKH2a32fJ1lVqWOA\u003D\u003D, this.\u0023\u003DzpVA9oSWG8tMHnyyPMQ\u003D\u003D));
  }

  private void \u0023\u003DzM004Fv4\u003D(DragDeltaEventArgs _param1)
  {
    EventHandler<DragDeltaEventArgs> zKoryGh8 = this.\u0023\u003DzKoryGH8\u003D;
    if (zKoryGh8 == null)
      return;
    zKoryGh8((object) this, _param1);
  }

  private void \u0023\u003DzswQwe\u0024nkJy88xtrQBw\u003D\u003D(DragCompletedEventArgs _param1)
  {
    EventHandler<DragCompletedEventArgs> zsVjPoH0Zqke5 = this.\u0023\u003DzsVjPoH0ZQke5;
    if (zsVjPoH0Zqke5 == null)
      return;
    zsVjPoH0Zqke5((object) this, _param1);
  }

  private void \u0023\u003Dz2YTeZYpcmqcM()
  {
    if (this.HeaderTemplate == null || this.\u0023\u003DzIcIt5i8Z\u0024_b2 == null || !(this.HeaderTemplate.LoadContent() is FrameworkElement element))
      return;
    this.\u0023\u003DzIcIt5i8Z\u0024_b2.Children.Add((UIElement) element);
  }

  private static void \u0023\u003Dz\u0024YszLknqkUvS(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    (_param0 as dje_zP5SLCZMPLKRDSVWETEPWLMZPT8MF3HJBEG329BU556QHYKV4MLT6V_ejd).\u0023\u003Dz2YTeZYpcmqcM();
  }
}
