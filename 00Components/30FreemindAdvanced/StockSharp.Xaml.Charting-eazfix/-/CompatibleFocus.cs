// Decompiled with JetBrains decompiler
// Type: -.CompatibleFocus
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Charting;

public sealed class CompatibleFocus
{
  public static readonly DependencyProperty \u0023\u003Dz8gpJZfsjfhZY = DependencyProperty.RegisterAttached("IsFocusable", typeof (bool), typeof (CompatibleFocus), new PropertyMetadata((object) true, new PropertyChangedCallback(CompatibleFocus.\u0023\u003Dz9rT93zhf3BVL)));

  public static bool GetIsFocusable(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(CompatibleFocus.\u0023\u003Dz8gpJZfsjfhZY);
  }

  public static void SetIsFocusable(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(CompatibleFocus.\u0023\u003Dz8gpJZfsjfhZY, (object) _param1);
  }

  private static void \u0023\u003Dz9rT93zhf3BVL(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    Control control = _param0 as Control;
    bool newValue = (bool) _param1.NewValue;
    if (control == null)
      return;
    control.Focusable = newValue;
  }
}
