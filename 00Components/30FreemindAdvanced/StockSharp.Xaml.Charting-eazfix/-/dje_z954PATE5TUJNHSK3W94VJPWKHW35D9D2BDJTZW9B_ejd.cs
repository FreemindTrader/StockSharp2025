// Decompiled with JetBrains decompiler
// Type: -.dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace \u002D;

internal sealed class dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd : FrameworkElement
{
  
  public static readonly DependencyProperty \u0023\u003DzKpMjgglc9VTa5MTZvA\u003D\u003D = DependencyProperty.RegisterAttached("SnapsToDevicePixels", typeof (bool), typeof (dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.\u0023\u003Dz8k9aaPizAFciBJYAVA\u003D\u003D)));

  public static void SetSnapsToDevicePixels(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.\u0023\u003DzKpMjgglc9VTa5MTZvA\u003D\u003D, (object) _param1);
  }

  public static bool GetSnapsToDevicePixels(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.\u0023\u003DzKpMjgglc9VTa5MTZvA\u003D\u003D);
  }

  private static void \u0023\u003Dz8k9aaPizAFciBJYAVA\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    FrameworkElement frameworkElement = (FrameworkElement) _param0;
    bool newValue = (bool) _param1.NewValue;
    frameworkElement.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, (object) newValue);
    frameworkElement.SetCurrentValue(dje_z954PATE5TUJNHSK3W94VJPWKHW35D9D2BDJTZW9B_ejd.\u0023\u003DzKpMjgglc9VTa5MTZvA\u003D\u003D, (object) newValue);
  }
}
