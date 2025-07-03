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

internal abstract class AnchorPointAnnotation : 
  AnnotationBase,
  IXmlSerializable,
  \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV,
  \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D,
  IHitTestable,
  IAnchorPointAnnotation
{
  public static readonly DependencyProperty HorizontalAnchorPointProperty = DependencyProperty.Register(nameof (HorizontalAnchorPoint), typeof (dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd), typeof (AnchorPointAnnotation), new PropertyMetadata((object) dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd.Left, new PropertyChangedCallback(AnchorPointAnnotation.OnAnchorPointChanged)));
  public static readonly DependencyProperty VerticalAnchorPointProperty = DependencyProperty.Register(nameof (VerticalAnchorPoint), typeof (dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd), typeof (AnchorPointAnnotation), new PropertyMetadata((object) dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd.Top, new PropertyChangedCallback(AnchorPointAnnotation.OnAnchorPointChanged)));

  protected AnchorPointAnnotation() => this.IsResizable = false;

  event EventHandler<\u0023\u003DzaDDeYuGlsOp51QXy5MWJZxERLr9hDQLdDJPw_pXdD1WK> \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchDown
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<\u0023\u003DzaDDeYuGlsOp51QXy5MWJZxERLr9hDQLdDJPw_pXdD1WK> \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchMove
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  event EventHandler<\u0023\u003DzaDDeYuGlsOp51QXy5MWJZxERLr9hDQLdDJPw_pXdD1WK> \u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV.StockSharp\u002EXaml\u002ECharting\u002EUtility\u002EMouse\u002EIPublishMouseEvents\u002ETouchUp
  {
    add => throw new NotImplementedException();
    remove => throw new NotImplementedException();
  }

  public dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd HorizontalAnchorPoint
  {
    get
    {
      return (dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd) this.GetValue(AnchorPointAnnotation.HorizontalAnchorPointProperty);
    }
    set => this.SetValue(AnchorPointAnnotation.HorizontalAnchorPointProperty, (object) value);
  }

  public dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd VerticalAnchorPoint
  {
    get
    {
      return (dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd) this.GetValue(AnchorPointAnnotation.VerticalAnchorPointProperty);
    }
    set => this.SetValue(AnchorPointAnnotation.VerticalAnchorPointProperty, (object) value);
  }

  public double VerticalOffset
  {
    get
    {
      if (this.AnnotationRoot != null && this.VerticalAnchorPoint != dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd.Top)
      {
        if (this.VerticalAnchorPoint == dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd.Center)
          return this.AnnotationRoot.ActualHeight * 0.5;
        if (this.VerticalAnchorPoint == dje_z2QKG6FGBPK6Q5G57SD2MV2G2DK6AQNZUDB5H6R852JMG36S4ENAUS_ejd.Bottom)
          return this.AnnotationRoot.ActualHeight;
      }
      return 0.0;
    }
  }

  public double HorizontalOffset
  {
    get
    {
      if (this.AnnotationRoot != null && this.HorizontalAnchorPoint != dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd.Left)
      {
        if (this.HorizontalAnchorPoint == dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd.Center)
          return this.AnnotationRoot.ActualWidth * 0.5;
        if (this.HorizontalAnchorPoint == dje_zM9MCUSYCVBVQUAFNW4CK2F33NM7V2VAWDUAE4V6AWNNSEJCNKX3MW_ejd.Right)
          return this.AnnotationRoot.ActualWidth;
      }
      return 0.0;
    }
  }

  private static void OnAnchorPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    AnchorPointAnnotation.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D jq9Llz3ahZ2LrQl4 = new AnchorPointAnnotation.\u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D();
    jq9Llz3ahZ2LrQl4.\u0023\u003Dz6iFJYho\u003D = d;
    jq9Llz3ahZ2LrQl4.\u0023\u003Dz1BK01YA\u003D = e;
    ((DispatcherObject) jq9Llz3ahZ2LrQl4.\u0023\u003Dz6iFJYho\u003D).Dispatcher.BeginInvoke((Delegate) new Action(jq9Llz3ahZ2LrQl4.\u0023\u003DzqD_p60kW19tqQsYC8u0B0xg\u003D), (DispatcherPriority) 8, Array.Empty<object>());
  }

  protected \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D GetAnchorAnnotationCoordinates(
    \u0023\u003DzDB45NmFy1DDUpCYhH1HtWfNnpojF4sCpkA8pp0g\u003D annotationCoordinates)
  {
    annotationCoordinates.\u0023\u003DzS2_K6sVvd5IY -= this.HorizontalOffset;
    annotationCoordinates.\u0023\u003Dz2J4l3QUGwZHE -= this.VerticalOffset;
    annotationCoordinates.\u0023\u003Dz6aJoeqoqAzym -= this.HorizontalOffset;
    annotationCoordinates.\u0023\u003DzWp13vlQiZCJc -= this.VerticalOffset;
    return annotationCoordinates;
  }

  protected override Cursor GetSelectedCursor() => Cursors.SizeAll;

  object \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eget_DataContext()
  {
    return this.DataContext;
  }

  void \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D.StockSharp\u002EXaml\u002ECharting\u002EVisuals\u002EAnnotations\u002EIAnnotation\u002Eset_DataContext(
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

  private sealed class \u0023\u003DzNwXxhkJq9Llz3ah\u0024z2LRQl4\u003D
  {
    public DependencyObject \u0023\u003Dz6iFJYho\u003D;
    public DependencyPropertyChangedEventArgs \u0023\u003Dz1BK01YA\u003D;

    internal void \u0023\u003DzqD_p60kW19tqQsYC8u0B0xg\u003D()
    {
      AnnotationBase.OnRenderablePropertyChanged(this.\u0023\u003Dz6iFJYho\u003D, this.\u0023\u003Dz1BK01YA\u003D);
    }
  }
}
