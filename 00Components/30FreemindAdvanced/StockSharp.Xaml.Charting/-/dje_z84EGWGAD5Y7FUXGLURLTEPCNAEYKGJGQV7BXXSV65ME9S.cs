// Decompiled with JetBrains decompiler
// Type: -.dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd
{
  public static readonly DependencyProperty \u0023\u003DzfMY988N0StOA = DependencyProperty.RegisterAttached("", typeof (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd), typeof (dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd), new PropertyMetadata((object) dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default, new PropertyChangedCallback(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzOPvUPixjU\u00244Y)));
  public static readonly DependencyProperty \u0023\u003DzleqM3VUz5r6I = DependencyProperty.RegisterAttached("", typeof (bool), typeof (dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzkyaUoNb3_wZaUlrnAd4Di9o\u003D)));
  public static readonly DependencyProperty \u0023\u003Dzux6qwNDX3dIn = DependencyProperty.RegisterAttached("", typeof (bool), typeof (dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzpY7n52ahyqAxE3pQLbGjA5w\u003D)));

  public static dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd GetAxisAlignment(
    DependencyObject _param0)
  {
    return (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) _param0.GetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzfMY988N0StOA);
  }

  public static void SetAxisAlignment(
    DependencyObject _param0,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param1)
  {
    _param0.SetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzfMY988N0StOA, (object) _param1);
  }

  public static bool GetIsInsideItem(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzleqM3VUz5r6I);
  }

  public static void SetIsInsideItem(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzleqM3VUz5r6I, (object) _param1);
  }

  public static bool GetIsOutsideItem(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dzux6qwNDX3dIn);
  }

  public static void SetIsOutsideItem(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dzux6qwNDX3dIn, (object) _param1);
  }

  private static void \u0023\u003DzOPvUPixjU\u00244Y(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is StackPanel stackPanel))
      return;
    dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzZyQWE_89kzT_(stackPanel);
  }

  internal static void \u0023\u003DzZyQWE_89kzT_(StackPanel _param0)
  {
    if (_param0.FlowDirection == FlowDirection.RightToLeft)
      return;
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) _param0.GetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzfMY988N0StOA);
    bool flag = demydmpA2K68QEjd == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom || demydmpA2K68QEjd == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top;
    _param0.Orientation = flag ? Orientation.Vertical : Orientation.Horizontal;
    FrameworkElement frameworkElement1 = (FrameworkElement) _param0.Children.\u0023\u003DzjgOr4ajGBpa0(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Predicate<UIElement>(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz2eauhM7lKjDd6QTC\u0024t7yLig\u003D)));
    FrameworkElement frameworkElement2 = (FrameworkElement) _param0.Children.\u0023\u003DzjgOr4ajGBpa0(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D ?? (dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D = new Predicate<UIElement>(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzm8PPAS0ZnW6qP8J6jTtdPT0\u003D)));
    int num = demydmpA2K68QEjd == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left ? 1 : (demydmpA2K68QEjd == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top ? 1 : 0);
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
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Predicate<UIElement> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;
    public static Predicate<UIElement> \u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D;

    internal bool \u0023\u003Dz2eauhM7lKjDd6QTC\u0024t7yLig\u003D(UIElement _param1)
    {
      return (bool) _param1.GetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzleqM3VUz5r6I);
    }

    internal bool \u0023\u003Dzm8PPAS0ZnW6qP8J6jTtdPT0\u003D(UIElement _param1)
    {
      return (bool) _param1.GetValue(dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003Dzux6qwNDX3dIn);
    }

    internal void \u0023\u003DzkyaUoNb3_wZaUlrnAd4Di9o\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzOPvUPixjU\u00244Y(((FrameworkElement) _param1).Parent, _param2);
    }

    internal void \u0023\u003DzpY7n52ahyqAxE3pQLbGjA5w\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      dje_z84EGWGAD5Y7FUXGLURLTEPCNAEYKGJGQV7BXXSV65ME9SUA_ejd.\u0023\u003DzOPvUPixjU\u00244Y(((FrameworkElement) _param1).Parent, _param2);
    }
  }
}
