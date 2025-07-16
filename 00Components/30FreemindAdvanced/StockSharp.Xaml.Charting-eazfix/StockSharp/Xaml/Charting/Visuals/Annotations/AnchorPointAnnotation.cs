// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.AnchorPointAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;

#nullable disable
namespace StockSharp.Xaml.Charting.Visuals.Annotations;

public abstract class AnchorPointAnnotation : 
  AnnotationBase,
  IXmlSerializable,
  IPublishMouseEvents,
  IAnnotation,
  IHitTestable,
  IAnchorPointAnnotation
{
  public static readonly DependencyProperty HorizontalAnchorPointProperty = DependencyProperty.Register(nameof (HorizontalAnchorPoint), typeof (HorizontalAnchorPoint), typeof (AnchorPointAnnotation), new PropertyMetadata((object) HorizontalAnchorPoint.Left, new PropertyChangedCallback(AnchorPointAnnotation.OnAnchorPointChanged)));
  public static readonly DependencyProperty VerticalAnchorPointProperty = DependencyProperty.Register(nameof (VerticalAnchorPoint), typeof (VerticalAnchorPoint), typeof (AnchorPointAnnotation), new PropertyMetadata((object) VerticalAnchorPoint.Top, new PropertyChangedCallback(AnchorPointAnnotation.OnAnchorPointChanged)));

  protected AnchorPointAnnotation() => this.IsResizable = false;

  event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchDown
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchMove
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchUp
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  public HorizontalAnchorPoint HorizontalAnchorPoint
  {
    get
    {
      return (HorizontalAnchorPoint) this.GetValue(AnchorPointAnnotation.HorizontalAnchorPointProperty);
    }
    set => this.SetValue(AnchorPointAnnotation.HorizontalAnchorPointProperty, (object) value);
  }

  public VerticalAnchorPoint VerticalAnchorPoint
  {
    get
    {
      return (VerticalAnchorPoint) this.GetValue(AnchorPointAnnotation.VerticalAnchorPointProperty);
    }
    set => this.SetValue(AnchorPointAnnotation.VerticalAnchorPointProperty, (object) value);
  }

  public double VerticalOffset
  {
    get
    {
      if (this.AnnotationRoot != null && this.VerticalAnchorPoint != VerticalAnchorPoint.Top)
      {
        if (this.VerticalAnchorPoint == VerticalAnchorPoint.Center)
          return this.AnnotationRoot.ActualHeight * 0.5;
        if (this.VerticalAnchorPoint == VerticalAnchorPoint.Bottom)
          return this.AnnotationRoot.ActualHeight;
      }
      return 0.0;
    }
  }

  public double HorizontalOffset
  {
    get
    {
      if (this.AnnotationRoot != null && this.HorizontalAnchorPoint != HorizontalAnchorPoint.Left)
      {
        if (this.HorizontalAnchorPoint == HorizontalAnchorPoint.Center)
          return this.AnnotationRoot.ActualWidth * 0.5;
        if (this.HorizontalAnchorPoint == HorizontalAnchorPoint.Right)
          return this.AnnotationRoot.ActualWidth;
      }
      return 0.0;
    }
  }

  private static void OnAnchorPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    AnchorPointAnnotation.SomeClass398 jq9Llz3ahZ2LrQl4 = new AnchorPointAnnotation.SomeClass398();
    jq9Llz3ahZ2LrQl4.\u0023\u003Dz6iFJYho\u003D = d;
    jq9Llz3ahZ2LrQl4.\u0023\u003Dz1BK01YA\u003D = e;
    ((DispatcherObject) jq9Llz3ahZ2LrQl4.\u0023\u003Dz6iFJYho\u003D).Dispatcher.BeginInvoke((Delegate) new Action(jq9Llz3ahZ2LrQl4.\u0023\u003DzqD_p60kW19tqQsYC8u0B0xg\u003D), (DispatcherPriority) 8, Array.Empty<object>());
  }

  protected AnnotationCoordinates GetAnchorAnnotationCoordinates(
    AnnotationCoordinates annotationCoordinates)
  {
    annotationCoordinates.\u0023\u003DzS2_K6sVvd5IY -= this.HorizontalOffset;
    annotationCoordinates.\u0023\u003Dz2J4l3QUGwZHE -= this.VerticalOffset;
    annotationCoordinates.\u0023\u003Dz6aJoeqoqAzym -= this.HorizontalOffset;
    annotationCoordinates.\u0023\u003DzWp13vlQiZCJc -= this.VerticalOffset;
    return annotationCoordinates;
  }

  protected override Cursor GetSelectedCursor() => Cursors.SizeAll;

  object IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eget_DataContext()
  {
    return this.DataContext;
  }

  void IAnnotation.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eset_DataContext(
    object value)
  {
    this.DataContext = value;
  }

  double IHitTestable.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualWidth()
  {
    return this.ActualWidth;
  }

  double IHitTestable.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EIHitTestable\u002Eget_ActualHeight()
  {
    return this.ActualHeight;
  }

  private sealed class SomeClass398
  {
    public DependencyObject \u0023\u003Dz6iFJYho\u003D;
    public DependencyPropertyChangedEventArgs \u0023\u003Dz1BK01YA\u003D;

    public void \u0023\u003DzqD_p60kW19tqQsYC8u0B0xg\u003D()
    {
      AnnotationBase.OnRenderablePropertyChanged(this.\u0023\u003Dz6iFJYho\u003D, this.\u0023\u003Dz1BK01YA\u003D);
    }
  }
}
