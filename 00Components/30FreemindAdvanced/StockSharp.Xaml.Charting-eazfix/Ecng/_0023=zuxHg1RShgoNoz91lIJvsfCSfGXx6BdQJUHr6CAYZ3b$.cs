// Decompiled with JetBrains decompiler
// Type: #=zuxHg1RShgoNoz91lIJvsfCSfGXx6BdQJUHr6CAYZ3b$y
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;

#nullable disable
public sealed class FreezeHelper2025
{
  public static readonly DependencyProperty FreezeProperty = DependencyProperty.RegisterAttached("Freeze", typeof (bool), typeof (FreezeHelper2025), new PropertyMetadata((object) false, new PropertyChangedCallback(FreezeHelper2025.OnFreezePropertyChanged)));

  private static void OnFreezePropertyChanged(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is Freezable freezable) || !true.Equals(_param1.NewValue) || !freezable.CanFreeze)
      return;
    freezable.Freeze();
  }

  public static void SetFreeze(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(FreezeHelper2025.FreezeProperty, (object) _param1);
  }

  public static bool GetFreeze(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(FreezeHelper2025.FreezeProperty);
  }
}
