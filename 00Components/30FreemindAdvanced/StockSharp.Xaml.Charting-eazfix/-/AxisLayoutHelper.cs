// Decompiled with JetBrains decompiler
// Type: -.AxisLayoutHelper
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisLayoutHelper
{
  public static readonly DependencyProperty \u0023\u003DzfMY988N0StOA = DependencyProperty.RegisterAttached("AxisAlignment", typeof (AxisAlignment), typeof (AxisLayoutHelper), new PropertyMetadata((object) AxisAlignment.Default, new PropertyChangedCallback(AxisLayoutHelper.\u0023\u003DzOPvUPixjU\u00244Y)));
  public static readonly DependencyProperty \u0023\u003DzleqM3VUz5r6I = DependencyProperty.RegisterAttached("IsInsideItem", typeof (bool), typeof (AxisLayoutHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(AxisLayoutHelper.SomeClass34343383.SomeMethond0343.\u0023\u003DzkyaUoNb3_wZaUlrnAd4Di9o\u003D)));
  public static readonly DependencyProperty \u0023\u003Dzux6qwNDX3dIn = DependencyProperty.RegisterAttached("IsOutsideItem", typeof (bool), typeof (AxisLayoutHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(AxisLayoutHelper.SomeClass34343383.SomeMethond0343.\u0023\u003DzpY7n52ahyqAxE3pQLbGjA5w\u003D)));

  public static AxisAlignment GetAxisAlignment(
    DependencyObject _param0)
  {
    return (AxisAlignment) _param0.GetValue(AxisLayoutHelper.\u0023\u003DzfMY988N0StOA);
  }

  public static void SetAxisAlignment(
    DependencyObject _param0,
    AxisAlignment _param1)
  {
    _param0.SetValue(AxisLayoutHelper.\u0023\u003DzfMY988N0StOA, (object) _param1);
  }

  public static bool GetIsInsideItem(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(AxisLayoutHelper.\u0023\u003DzleqM3VUz5r6I);
  }

  public static void SetIsInsideItem(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(AxisLayoutHelper.\u0023\u003DzleqM3VUz5r6I, (object) _param1);
  }

  public static bool GetIsOutsideItem(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(AxisLayoutHelper.\u0023\u003Dzux6qwNDX3dIn);
  }

  public static void SetIsOutsideItem(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(AxisLayoutHelper.\u0023\u003Dzux6qwNDX3dIn, (object) _param1);
  }

  private static void \u0023\u003DzOPvUPixjU\u00244Y(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is StackPanel stackPanel))
      return;
    AxisLayoutHelper.\u0023\u003DzZyQWE_89kzT_(stackPanel);
  }

  internal static void \u0023\u003DzZyQWE_89kzT_(StackPanel _param0)
  {
    if (_param0.FlowDirection == FlowDirection.RightToLeft)
      return;
    AxisAlignment demydmpA2K68QEjd = (AxisAlignment) _param0.GetValue(AxisLayoutHelper.\u0023\u003DzfMY988N0StOA);
    bool flag = demydmpA2K68QEjd == AxisAlignment.Bottom || demydmpA2K68QEjd == AxisAlignment.Top;
    _param0.Orientation = flag ? Orientation.Vertical : Orientation.Horizontal;
    FrameworkElement frameworkElement1 = (FrameworkElement) _param0.Children.\u0023\u003DzjgOr4ajGBpa0(AxisLayoutHelper.SomeClass34343383.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (AxisLayoutHelper.SomeClass34343383.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Predicate<UIElement>(AxisLayoutHelper.SomeClass34343383.SomeMethond0343.\u0023\u003Dz2eauhM7lKjDd6QTC\u0024t7yLig\u003D)));
    FrameworkElement frameworkElement2 = (FrameworkElement) _param0.Children.\u0023\u003DzjgOr4ajGBpa0(AxisLayoutHelper.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D ?? (AxisLayoutHelper.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D = new Predicate<UIElement>(AxisLayoutHelper.SomeClass34343383.SomeMethond0343.\u0023\u003Dzm8PPAS0ZnW6qP8J6jTtdPT0\u003D)));
    int num = demydmpA2K68QEjd == AxisAlignment.Left ? 1 : (demydmpA2K68QEjd == AxisAlignment.Top ? 1 : 0);
    _param0.\u0023\u003DziYdJ\u00246cCiBha((object) frameworkElement1);
    _param0.\u0023\u003DziYdJ\u00246cCiBha((object) frameworkElement2);
    if (num != 0)
    {
      _param0.\u0023\u003DzH0osWQkV_Y8_((object) frameworkElement2, 0);
      _param0.\u0023\u003DzH0osWQkV_Y8_((object) frameworkElement1, -1);
    }
    else
    {
      _param0.\u0023\u003DzH0osWQkV_Y8_((object) frameworkElement1, 0);
      _param0.\u0023\u003DzH0osWQkV_Y8_((object) frameworkElement2, -1);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly AxisLayoutHelper.SomeClass34343383 SomeMethond0343 = new AxisLayoutHelper.SomeClass34343383();
    public static Predicate<UIElement> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;
    public static Predicate<UIElement> \u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D;

    internal bool \u0023\u003Dz2eauhM7lKjDd6QTC\u0024t7yLig\u003D(UIElement _param1)
    {
      return (bool) _param1.GetValue(AxisLayoutHelper.\u0023\u003DzleqM3VUz5r6I);
    }

    internal bool \u0023\u003Dzm8PPAS0ZnW6qP8J6jTtdPT0\u003D(UIElement _param1)
    {
      return (bool) _param1.GetValue(AxisLayoutHelper.\u0023\u003Dzux6qwNDX3dIn);
    }

    internal void \u0023\u003DzkyaUoNb3_wZaUlrnAd4Di9o\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      AxisLayoutHelper.\u0023\u003DzOPvUPixjU\u00244Y(((FrameworkElement) _param1).Parent, _param2);
    }

    internal void \u0023\u003DzpY7n52ahyqAxE3pQLbGjA5w\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      AxisLayoutHelper.\u0023\u003DzOPvUPixjU\u00244Y(((FrameworkElement) _param1).Parent, _param2);
    }
  }
}
