// Decompiled with JetBrains decompiler
// Type: -.FrameworkVisibilityManager
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class FrameworkVisibilityManager : FrameworkElement
{
  
  public static readonly DependencyProperty \u0023\u003Dzb9NlFA1HMDOO = DependencyProperty.RegisterAttached("VisibleIn", typeof (FrameworkVisibility), typeof (FrameworkVisibilityManager), new PropertyMetadata((object) FrameworkVisibility.All, new PropertyChangedCallback(FrameworkVisibilityManager.\u0023\u003DzWE_UfM17p3U9)));

  public static void SetVisibleIn(
    DependencyObject _param0,
    FrameworkVisibility _param1)
  {
    _param0.SetValue(FrameworkVisibilityManager.\u0023\u003Dzb9NlFA1HMDOO, (object) _param1);
  }

  public static FrameworkVisibility GetVisibleIn(
    DependencyObject _param0)
  {
    return (FrameworkVisibility) _param0.GetValue(FrameworkVisibilityManager.\u0023\u003Dzb9NlFA1HMDOO);
  }

  private static void \u0023\u003DzWE_UfM17p3U9(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    Visibility visibility = (FrameworkVisibility) _param1.NewValue == FrameworkVisibility.Silverlight ? Visibility.Collapsed : Visibility.Visible;
    (_param0 as UIElement).Visibility = visibility;
  }
}
