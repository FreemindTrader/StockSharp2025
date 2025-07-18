// Decompiled with JetBrains decompiler
// Type: #=zdrN0sF$gsZwp28k07ZqFHiTdVAFrQacgUQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

#nullable disable
public sealed class \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D
{
  public static readonly DependencyProperty \u0023\u003DzvwIiO5ko1yGZ = DependencyProperty.RegisterAttached("HorizontalChartGroup", typeof (string), typeof (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D), new PropertyMetadata((object) null, new PropertyChangedCallback(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzGDCsRX7fHtlf)));
  public static Dictionary<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> \u0023\u003DzovkpR0EIGwzr = new Dictionary<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>();

  public static void SetHorizontalChartGroup(DependencyObject _param0, string _param1)
  {
    _param0.SetValue(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzvwIiO5ko1yGZ, (object) _param1);
  }

  public static string GetHorizontalChartGroup(DependencyObject _param0)
  {
    return (string) _param0.GetValue(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzvwIiO5ko1yGZ);
  }

  private static void \u0023\u003DzGDCsRX7fHtlf(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is SciChartSurface elwvdvgwnmJ5AjuaEjd))
      throw new InvalidOperationException("HorizontalChartGroupProperty can only be applied to types UltrachartSurface derived types");
    string newValue = _param1.NewValue as string;
    string oldValue = _param1.OldValue as string;
    if (string.IsNullOrEmpty(newValue))
    {
      elwvdvgwnmJ5AjuaEjd.Loaded -= \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzvPNhkA9WmuO0));
      elwvdvgwnmJ5AjuaEjd.Unloaded -= \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D = new RoutedEventHandler(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz0zuDFtRKqfs9));
      \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(elwvdvgwnmJ5AjuaEjd);
    }
    else
    {
      if (!(newValue != oldValue))
        return;
      if (!string.IsNullOrEmpty(oldValue))
        \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(elwvdvgwnmJ5AjuaEjd);
      elwvdvgwnmJ5AjuaEjd.Loaded -= \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzvPNhkA9WmuO0));
      elwvdvgwnmJ5AjuaEjd.Unloaded -= \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D = new RoutedEventHandler(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz0zuDFtRKqfs9));
      elwvdvgwnmJ5AjuaEjd.Loaded += \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D = new RoutedEventHandler(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzvPNhkA9WmuO0));
      elwvdvgwnmJ5AjuaEjd.Unloaded += \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D = new RoutedEventHandler(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz0zuDFtRKqfs9));
      if (!elwvdvgwnmJ5AjuaEjd.IsLoaded)
        return;
      \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz3IT6iI0OoT6Opi1nPzm__kSnx9aj((ISciChartSurface) elwvdvgwnmJ5AjuaEjd, newValue);
    }
  }

  private static void \u0023\u003DzvPNhkA9WmuO0(object _param0, RoutedEventArgs _param1)
  {
    SciChartSurface elwvdvgwnmJ5AjuaEjd = _param0 as SciChartSurface;
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz3IT6iI0OoT6Opi1nPzm__kSnx9aj((ISciChartSurface) elwvdvgwnmJ5AjuaEjd, \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.GetHorizontalChartGroup((DependencyObject) elwvdvgwnmJ5AjuaEjd));
  }

  private static void \u0023\u003Dz0zuDFtRKqfs9(object _param0, RoutedEventArgs _param1)
  {
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(_param0 as SciChartSurface);
  }

  private static void \u0023\u003Dz3IT6iI0OoT6Opi1nPzm__kSnx9aj(
    ISciChartSurface _param0,
    string _param1)
  {
    \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D key = new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D(_param0);
    if (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzovkpR0EIGwzr.ContainsKey(key))
      return;
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzovkpR0EIGwzr.Add(key, _param1);
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz3dE3KTgP\u0024pXw(_param0);
    _param0.\u0023\u003DzKPHSi1vgK\u0024Fx(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 = new EventHandler<EventArgs>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D)));
  }

  private static void \u0023\u003DzC641\u0024PpiHUo22N9TFU19b5A7dw9m(
    SciChartSurface _param0)
  {
    foreach (KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> keyValuePair in \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzovkpR0EIGwzr)
    {
      if (keyValuePair.Key.\u0023\u003DzUSgr9afrgN_J81TFBOEZdy4\u003D() == _param0)
        keyValuePair.Key.\u0023\u003Dz_ub4hhw\u003D();
    }
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzovkpR0EIGwzr.Remove(new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D((ISciChartSurface) _param0));
    _param0.\u0023\u003DzrRRdxqQwy\u0024OJ(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4 = new EventHandler<EventArgs>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D)));
  }

  private static void \u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D(object _param0, EventArgs _param1)
  {
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003Dz3dE3KTgP\u0024pXw((ISciChartSurface) _param0);
  }

  private static void \u0023\u003Dz3dE3KTgP\u0024pXw(
    ISciChartSurface _param0)
  {
    \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D k0hz6MwLrPm7JfgVw01g = new \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D();
    if (!\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzovkpR0EIGwzr.TryGetValue(new \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D(_param0), out k0hz6MwLrPm7JfgVw01g.\u0023\u003Dzvd7k66M\u003D))
      return;
    \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D[] array = \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzovkpR0EIGwzr.Where<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>>(new Func<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, bool>(k0hz6MwLrPm7JfgVw01g.\u0023\u003DzUB2PB4j3oKnShyLauh0J0fA\u003D)).Select<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D = new Func<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzL6P38Nt3ByHiE6Ehg2GH\u00247w\u003D))).ToArray<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>();
    k0hz6MwLrPm7JfgVw01g.\u0023\u003DzmfE\u0024jucTzddtybnMGg\u003D\u003D = \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzngzyCR\u0024m4xucYS6Umw\u003D\u003D((IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>) array, dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom);
    k0hz6MwLrPm7JfgVw01g.\u0023\u003DzHQz9fVShYfxRR3dztw\u003D\u003D = \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.\u0023\u003DzngzyCR\u0024m4xucYS6Umw\u003D\u003D((IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>) array, dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top);
    ((IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D>) array).Select<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, ISciChartSurface>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003DzEfO0YchCrnkh1uXOeQ\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003DzEfO0YchCrnkh1uXOeQ\u003D\u003D = new Func<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, ISciChartSurface>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzQW8\u0024ngWQrNuehXxNdJc5YQM\u003D))).OfType<SciChartSurface>().\u0023\u003Dz30RSSSygABj_<SciChartSurface>(new Action<SciChartSurface>(k0hz6MwLrPm7JfgVw01g.\u0023\u003DzBgT6HSE20ulby6RTtGwqWX8\u003D));
  }

  private static double \u0023\u003DzngzyCR\u0024m4xucYS6Umw\u003D\u003D(
    IEnumerable<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D> _param0,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param1)
  {
    IEnumerable<IEnumerable<AxisBase>> source = _param0.Select<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, IEnumerable<AxisBase>>(new Func<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, IEnumerable<AxisBase>>(new \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.PrivateSealedClassDz4hWzOvDOp2Sz_a2WchXH2wc()
    {
      \u0023\u003Dz0V69zGwQUFh\u0024 = _param1
    }.\u0023\u003DzGjtqUzQMvdrNrjgx7OasttW1sq2c));
    double? nullable;
    if (source == null)
    {
      nullable = new double?();
    }
    else
    {
      IEnumerable<double> doubles = source.Select<IEnumerable<AxisBase>, double>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003Dztw\u0024s\u0024BHvfpXoZJHcIQ\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003Dztw\u0024s\u0024BHvfpXoZJHcIQ\u003D\u003D = new Func<IEnumerable<AxisBase>, double>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzCr_bnXqDdnc8Ycd6c8ZKxzmQ8\u0024AS)));
      nullable = doubles != null ? doubles.\u0023\u003DzAAksTMXIKE7d<double>() : new double?();
    }
    return nullable.GetValueOrDefault();
  }

  private sealed class PrivateSealedClassDz4hWzOvDOp2Sz_a2WchXH2wc
  {
    public dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd \u0023\u003Dz0V69zGwQUFh\u0024;
    public Func<AxisBase, bool> \u0023\u003DzoD2HtVGZvKav;

    public IEnumerable<AxisBase> \u0023\u003DzGjtqUzQMvdrNrjgx7OasttW1sq2c(
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D _param1)
    {
      AxisCollection yaxes = _param1.\u0023\u003DzUSgr9afrgN_J81TFBOEZdy4\u003D().get_YAxes();
      return yaxes == null ? (IEnumerable<AxisBase>) null : yaxes.OfType<AxisBase>().Where<AxisBase>(this.\u0023\u003DzoD2HtVGZvKav ?? (this.\u0023\u003DzoD2HtVGZvKav = new Func<AxisBase, bool>(this.\u0023\u003DzOaW1WOaSuiY3z\u0024u5DGB4v5mpnjoA)));
    }

    public bool \u0023\u003DzOaW1WOaSuiY3z\u0024u5DGB4v5mpnjoA(
      AxisBase _param1)
    {
      return _param1.AxisAlignment == this.\u0023\u003Dz0V69zGwQUFh\u0024;
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383();
    public static Func<KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string>, \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D> \u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D;
    public static Func<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, ISciChartSurface> \u0023\u003DzEfO0YchCrnkh1uXOeQ\u003D\u003D;
    public static Func<double, AxisBase, double> \u0023\u003Dzj2\u0024kP85fw6FKRq7qMg\u003D\u003D;
    public static Func<IEnumerable<AxisBase>, double> \u0023\u003Dztw\u0024s\u0024BHvfpXoZJHcIQ\u003D\u003D;

    public \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D \u0023\u003DzL6P38Nt3ByHiE6Ehg2GH\u00247w\u003D(
      KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> _param1)
    {
      return _param1.Key;
    }

    public ISciChartSurface \u0023\u003DzQW8\u0024ngWQrNuehXxNdJc5YQM\u003D(
      \u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D _param1)
    {
      return _param1.\u0023\u003DzUSgr9afrgN_J81TFBOEZdy4\u003D();
    }

    public double \u0023\u003DzCr_bnXqDdnc8Ycd6c8ZKxzmQ8\u0024AS(
      IEnumerable<AxisBase> _param1)
    {
      return _param1.Aggregate<AxisBase, double>(0.0, \u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003Dzj2\u0024kP85fw6FKRq7qMg\u003D\u003D ?? (\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.\u0023\u003Dzj2\u0024kP85fw6FKRq7qMg\u003D\u003D = new Func<double, AxisBase, double>(\u0023\u003DzdrN0sF\u0024gsZwp28k07ZqFHiTdVAFrQacgUQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DztDWSJwWERpu\u0024zyFsZsOUqftqxQN3)));
    }

    public double \u0023\u003DztDWSJwWERpu\u0024zyFsZsOUqftqxQN3(
      double _param1,
      AxisBase _param2)
    {
      return _param1 + _param2.ActualHeight;
    }
  }

  private sealed class \u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D
  {
    public string \u0023\u003Dzvd7k66M\u003D;
    public double \u0023\u003DzmfE\u0024jucTzddtybnMGg\u003D\u003D;
    public double \u0023\u003DzHQz9fVShYfxRR3dztw\u003D\u003D;

    public bool \u0023\u003DzUB2PB4j3oKnShyLauh0J0fA\u003D(
      KeyValuePair<\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyakKyozf37YUHg\u003D\u003D, string> _param1)
    {
      return _param1.Value == this.\u0023\u003Dzvd7k66M\u003D;
    }

    public void \u0023\u003DzBgT6HSE20ulby6RTtGwqWX8\u003D(
      SciChartSurface _param1)
    {
      if (_param1.\u0023\u003DzTUFNh6E2QDAjxphyfw\u003D\u003D() != null)
        _param1.\u0023\u003DzTUFNh6E2QDAjxphyfw\u003D\u003D().Margin = new Thickness(0.0, 0.0, 0.0, this.\u0023\u003DzmfE\u0024jucTzddtybnMGg\u003D\u003D - _param1.\u0023\u003DzTUFNh6E2QDAjxphyfw\u003D\u003D().ActualHeight);
      if (_param1.\u0023\u003Dz3ALyJldLTYC8rUKNSw\u003D\u003D() == null)
        return;
      _param1.\u0023\u003Dz3ALyJldLTYC8rUKNSw\u003D\u003D().Margin = new Thickness(0.0, this.\u0023\u003DzHQz9fVShYfxRR3dztw\u003D\u003D - _param1.\u0023\u003Dz3ALyJldLTYC8rUKNSw\u003D\u003D().ActualHeight, 0.0, 0.0);
    }
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static RoutedEventHandler \u0023\u003DzXup_g8P19mSOPUgVXw\u003D\u003D;
    public static RoutedEventHandler \u0023\u003DzUCpeCDId_uWja9GX4w\u003D\u003D;
    public static EventHandler<EventArgs> \u0023\u003DzYYR_vB10pB8hREkqsGlfVqT3uHU4;
  }
}
