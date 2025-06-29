// Decompiled with JetBrains decompiler
// Type: -.dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal static class dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd
{
  public static readonly DependencyProperty \u0023\u003DzPqXZPEJZP3as = DependencyProperty.RegisterAttached("", typeof (bool), typeof (dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzqcJkhvYEtK_9IQ_Dhw\u003D\u003D)));
  public static readonly DependencyProperty \u0023\u003DziVSP6OFqTkR8hihgtQ\u003D\u003D = DependencyProperty.RegisterAttached("", typeof (bool), typeof (dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzGMStW6rixIDeThlHSw\u003D\u003D)));

  public static bool GetClipToBounds(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzPqXZPEJZP3as);
  }

  public static void SetClipToBounds(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzPqXZPEJZP3as, (object) _param1);
  }

  public static bool GetClipToEllipseBounds(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DziVSP6OFqTkR8hihgtQ\u003D\u003D);
  }

  public static void SetClipToEllipseBounds(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DziVSP6OFqTkR8hihgtQ\u003D\u003D, (object) _param1);
  }

  private static void \u0023\u003DzqcJkhvYEtK_9IQ_Dhw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is FrameworkElement frameworkElement))
      return;
    frameworkElement.ClipToBounds = (bool) _param1.NewValue;
  }

  private static void \u0023\u003Dz2xcanLolC38g(FrameworkElement _param0)
  {
    if (dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.GetClipToBounds((DependencyObject) _param0))
      _param0.Clip = (Geometry) new RectangleGeometry()
      {
        Rect = new Rect(0.0, 0.0, _param0.ActualWidth, _param0.ActualHeight)
      };
    else
      _param0.Clip = (Geometry) null;
  }

  private static void \u0023\u003DzGMStW6rixIDeThlHSw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is FrameworkElement frameworkElement))
      return;
    dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzvgmToiciTxzW(frameworkElement);
    frameworkElement.Loaded += dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D ?? (dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D = new RoutedEventHandler(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzey4uj458pf56Oqw2JddifQGfxNx1));
    frameworkElement.SizeChanged += dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyrbJPDSEp4QjajjhiA\u003D\u003D ?? (dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyrbJPDSEp4QjajjhiA\u003D\u003D = new SizeChangedEventHandler(dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzIdjSw7XvQEG2GMw8R94pNFvcNUgD));
  }

  private static void \u0023\u003DzvgmToiciTxzW(FrameworkElement _param0)
  {
    if (dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.GetClipToEllipseBounds((DependencyObject) _param0))
    {
      double num1 = _param0.ActualWidth / 2.0;
      double num2 = _param0.ActualHeight / 2.0;
      _param0.Clip = (Geometry) new EllipseGeometry()
      {
        Center = new Point(num1, num2),
        RadiusX = num1,
        RadiusY = num2
      };
    }
    else
      _param0.Clip = (Geometry) null;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static RoutedEventHandler \u0023\u003DzjJ3vlfxVH92KtuUzew\u003D\u003D;
    public static SizeChangedEventHandler \u0023\u003DzyrbJPDSEp4QjajjhiA\u003D\u003D;

    internal void \u0023\u003Dzey4uj458pf56Oqw2JddifQGfxNx1(object _param1, RoutedEventArgs _param2)
    {
      dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzvgmToiciTxzW(_param1 as FrameworkElement);
    }

    internal void \u0023\u003DzIdjSw7XvQEG2GMw8R94pNFvcNUgD(
      object _param1,
      SizeChangedEventArgs _param2)
    {
      dje_zYP2YHYT7GJH54HLZN4EKVWEKMCU8KQMHVMVATD3PKGGYCK2_ejd.\u0023\u003DzvgmToiciTxzW(_param1 as FrameworkElement);
    }
  }
}
