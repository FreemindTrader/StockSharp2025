// Decompiled with JetBrains decompiler
// Type: -.dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

#nullable disable
namespace \u002D;

[System.Windows.Markup.ContentProperty("Children")]
internal sealed class dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd : 
  ContentControl,
  \u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D,
  IHitTestable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Canvas \u0023\u003DzF8_YcFVDbTFB = new Canvas();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ObservableCollection<UIElement> _childElements;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzPqXZPEJZP3as = DependencyProperty.Register(nameof (ClipToBounds), typeof (bool), typeof (dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd.\u0023\u003DzqcJkhvYEtK_9IQ_Dhw\u003D\u003D)));

  public dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd()
  {
    this._childElements = new ObservableCollection<UIElement>();
    this._childElements.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003DzPDTy9VNUu6UM);
    this.Content = (object) this.\u0023\u003DzF8_YcFVDbTFB;
    this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
    this.VerticalContentAlignment = VerticalAlignment.Stretch;
  }

  public new bool ClipToBounds
  {
    get
    {
      return (bool) this.GetValue(dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd.\u0023\u003DzPqXZPEJZP3as);
    }
    set
    {
      this.SetValue(dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd.\u0023\u003DzPqXZPEJZP3as, (object) value);
    }
  }

  [SpecialName]
  public ObservableCollection<UIElement> \u0023\u003DzBDSV99pPo8hY()
  {
    return this._childElements;
  }

  public void \u0023\u003DzUf222sU\u003D() => this.\u0023\u003DzBDSV99pPo8hY().Clear();

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
    return !(_param1 is Visual visual) ? Rect.Empty : this.TransformToVisual(visual).TransformBounds(LayoutInformation.GetLayoutSlot((FrameworkElement) this));
  }

  private void \u0023\u003DzPDTy9VNUu6UM(object _param1, NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.Action == NotifyCollectionChangedAction.Reset)
    {
      this.\u0023\u003DzF8_YcFVDbTFB.Children.Clear();
      this._childElements.\u0023\u003Dz30RSSSygABj_<UIElement>(new Action<UIElement>(this.\u0023\u003DzUOytnwv4WJ5__oZ9iQm4N8k\u003D));
    }
    if (_param2.NewItems != null)
      _param2.NewItems.Cast<UIElement>().\u0023\u003Dz30RSSSygABj_<UIElement>(new Action<UIElement>(this.\u0023\u003DzgGK3iO7YQs\u0024JAApqx5sILjM\u003D));
    if (_param2.OldItems == null)
      return;
    _param2.OldItems.Cast<UIElement>().\u0023\u003Dz30RSSSygABj_<UIElement>(new Action<UIElement>(this.\u0023\u003DzxeKhWcgbYb3lwyDRcXAhfPw\u003D));
  }

  private static void \u0023\u003DzqcJkhvYEtK_9IQ_Dhw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.SetClipToBounds((DependencyObject) ((dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd) _param0).\u0023\u003DzF8_YcFVDbTFB, (bool) _param1.NewValue);
  }

  internal Canvas \u0023\u003DzpPgLy35fsf6m() => this.\u0023\u003DzF8_YcFVDbTFB;

  double IHitTestable.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double IHitTestable.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }

  private void \u0023\u003DzUOytnwv4WJ5__oZ9iQm4N8k\u003D(UIElement _param1)
  {
    this.\u0023\u003DzF8_YcFVDbTFB.Children.Add(_param1);
  }

  private void \u0023\u003DzgGK3iO7YQs\u0024JAApqx5sILjM\u003D(UIElement _param1)
  {
    this.\u0023\u003DzF8_YcFVDbTFB.Children.Add(_param1);
  }

  private void \u0023\u003DzxeKhWcgbYb3lwyDRcXAhfPw\u003D(UIElement _param1)
  {
    this.\u0023\u003DzF8_YcFVDbTFB.Children.Remove(_param1);
  }
}
