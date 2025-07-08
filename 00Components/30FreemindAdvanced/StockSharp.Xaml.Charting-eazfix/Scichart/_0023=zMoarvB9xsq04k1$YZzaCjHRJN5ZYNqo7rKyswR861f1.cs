// Decompiled with JetBrains decompiler
// Type: #=zMoarvB9xsq04k1$YZzaCjHRJN5ZYNqo7rKyswR861f1C
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

#nullable disable
internal sealed class \u0023\u003DzMoarvB9xsq04k1\u0024YZzaCjHRJN5ZYNqo7rKyswR861f1C(
  IAnnotation _param1) : 
  \u0023\u003DzjIfS4CbXGFDPWmVOPAZGmqCb1u9vHqE9pa_ddXk\u003D(_param1),
  \u0023\u003DzFphlrC3tGBVP73muJW4N1sp2o\u0024hCxXn5DXylgtbrM25x,
  \u0023\u003Dzq8lPttT4Qpp4TSswk_CaTZNhZ9vYpqD7krOMbM3CC9ny
{
  
  private readonly List<Thumb> \u0023\u003Dzg87ZCkaztoO1 = new List<Thumb>();
  
  private Point[] \u0023\u003DzYw05nwk\u003D;
  
  private double \u0023\u003Dz4RCEv9uwIUW8;
  
  private double \u0023\u003Dztzbh7we4lkON;
  
  private bool \u0023\u003Dzk\u00246wUexmCawb;

  public override void \u0023\u003DzFeNr2Uw\u003D()
  {
    this.\u0023\u003DzUf222sU\u003D();
    this.\u0023\u003DzGDdLHa8\u003D();
  }

  private void \u0023\u003DzNOLDMwoM8cQu(Point _param1)
  {
    Thumb element = new Thumb();
    if (this.\u0023\u003Dzy2oKVLXXOFmI() is AnnotationBase annotationBase && annotationBase.ResizingGripsStyle != null)
      element.Style = annotationBase.ResizingGripsStyle;
    this.\u0023\u003DzVuf430fCLR2l().Children.Add((UIElement) element);
    element.DragStarted += new DragStartedEventHandler(this.\u0023\u003Dz3zcT52ZYhMwg);
    element.DragDelta += new DragDeltaEventHandler(this.\u0023\u003Dz3G8QAyxYl6wt);
    element.DragCompleted += new DragCompletedEventHandler(this.\u0023\u003Dzkf856Im2V4QQ);
    this.\u0023\u003Dzg87ZCkaztoO1.Add(element);
  }

  private void \u0023\u003Dzkf856Im2V4QQ(object _param1, DragCompletedEventArgs _param2)
  {
    (_param1 as Thumb).ReleaseMouseCapture();
    if (!(this.\u0023\u003Dzy2oKVLXXOFmI() is AnnotationBase annotationBase) || !annotationBase.IsEditable)
      return;
    annotationBase.RaiseAnnotationDragEnded(true, true);
  }

  private void \u0023\u003Dz3zcT52ZYhMwg(object _param1, DragStartedEventArgs _param2)
  {
    (_param1 as Thumb).CaptureMouse();
    if (!(this.\u0023\u003Dzy2oKVLXXOFmI() is AnnotationBase annotationBase) || !annotationBase.IsEditable)
      return;
    annotationBase.RaiseAnnotationDragStarted(true, true);
  }

  public override void \u0023\u003DzUf222sU\u003D()
  {
    this.\u0023\u003Dzg87ZCkaztoO1.\u0023\u003Dz30RSSSygABj_<Thumb>(new Action<Thumb>(this.\u0023\u003DzdvueoYNiS7F4));
    this.\u0023\u003Dzg87ZCkaztoO1.Clear();
  }

  private void \u0023\u003DzdvueoYNiS7F4(Thumb _param1)
  {
    _param1.DragDelta -= new DragDeltaEventHandler(this.\u0023\u003Dz3G8QAyxYl6wt);
    this.\u0023\u003DzVuf430fCLR2l().Children.Remove((UIElement) _param1);
  }

  private void \u0023\u003DzGDdLHa8\u003D(Point[] _param1)
  {
    foreach (Thumb element in this.\u0023\u003Dzg87ZCkaztoO1)
    {
      Point point = _param1[this.\u0023\u003Dzg87ZCkaztoO1.IndexOf(element)];
      point.X -= element.Width / 2.0;
      point.Y -= element.Height / 2.0;
      Canvas.SetLeft((UIElement) element, point.X);
      Canvas.SetTop((UIElement) element, point.Y);
    }
  }

  public override void \u0023\u003DzGDdLHa8\u003D()
  {
    if (this.\u0023\u003Dzk\u00246wUexmCawb)
      return;
    try
    {
      this.\u0023\u003Dzk\u00246wUexmCawb = true;
      this.\u0023\u003DzYw05nwk\u003D = this.\u0023\u003Dzy2oKVLXXOFmI().GetBasePoints() ?? Array.Empty<Point>();
      if (this.\u0023\u003DzYw05nwk\u003D.Length != this.\u0023\u003Dzg87ZCkaztoO1.Count)
      {
        this.\u0023\u003DzUf222sU\u003D();
        ((IEnumerable<Point>) this.\u0023\u003DzYw05nwk\u003D).\u0023\u003Dz30RSSSygABj_<Point>(new Action<Point>(this.\u0023\u003DzNOLDMwoM8cQu));
      }
      this.\u0023\u003DzGDdLHa8\u003D(this.\u0023\u003DzYw05nwk\u003D);
      this.\u0023\u003Dzg87ZCkaztoO1.ForEach(new Action<Thumb>(this.\u0023\u003DzNCNHMX8AWrhwe_HKFolcD1o\u003D));
    }
    finally
    {
      this.\u0023\u003Dzk\u00246wUexmCawb = false;
    }
  }

  private void \u0023\u003Dz3G8QAyxYl6wt(object _param1, DragDeltaEventArgs _param2)
  {
    IAnnotation hhh93Q0DqkV5Sv90k = this.\u0023\u003Dzy2oKVLXXOFmI();
    if (!hhh93Q0DqkV5Sv90k.get_IsEditable())
      return;
    int index = this.\u0023\u003Dzg87ZCkaztoO1.IndexOf(_param1 as Thumb);
    this.\u0023\u003Dz4RCEv9uwIUW8 = _param2.HorizontalChange;
    this.\u0023\u003Dztzbh7we4lkON = _param2.VerticalChange;
    double num1 = hhh93Q0DqkV5Sv90k.get_ResizeDirections() == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? 0.0 : this.\u0023\u003Dz4RCEv9uwIUW8;
    double num2 = hhh93Q0DqkV5Sv90k.get_ResizeDirections() == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection ? 0.0 : this.\u0023\u003Dztzbh7we4lkON;
    Point point = this.\u0023\u003DzYw05nwk\u003D[index];
    point.X += num1;
    point.Y += num2;
    if (hhh93Q0DqkV5Sv90k.get_IsResizable())
      hhh93Q0DqkV5Sv90k.SetBasePoint(point, index);
    else
      hhh93Q0DqkV5Sv90k.MoveAnnotation(num1, num2);
    if (!(hhh93Q0DqkV5Sv90k is AnnotationBase annotationBase))
      return;
    annotationBase.RaiseAnnotationDragging(0.0, 0.0, true, true);
  }

  [SpecialName]
  public IEnumerable<Thumb> \u0023\u003Dzcm6MlqCRXg0F()
  {
    return (IEnumerable<Thumb>) this.\u0023\u003Dzg87ZCkaztoO1;
  }

  private void \u0023\u003DzNCNHMX8AWrhwe_HKFolcD1o\u003D(Thumb _param1)
  {
    _param1.ContextMenu = this.\u0023\u003Dzy2oKVLXXOFmI() is AnnotationBase annotationBase ? annotationBase.ContextMenu : (ContextMenu) null;
  }
}
