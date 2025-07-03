// Decompiled with JetBrains decompiler
// Type: #=zPlFvps97y7rWR4vc5KUjRzGzgtl$Ea14Eg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal static class \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D
{
  private static readonly List<IDisposable> _myList = new List<IDisposable>();

  public static IDisposable AddPropertyListener(
    this DependencyObject _param0,
    DependencyProperty _param1,
    Action<DependencyPropertyChangedEventArgs> _param2)
  {
    \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass zxYtXibpnAml0 = new \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass(_param0, _param1, _param2);
    \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D._myList.Add((IDisposable) zxYtXibpnAml0);
    return (IDisposable) zxYtXibpnAml0;
  }

  public static Color \u0023\u003Dz3XFp9f4\u003D(this object _param0)
  {
    switch (_param0)
    {
      case null:
        throw new ArgumentNullException("value");
      case long color1:
        return ((int) color1).ToColor();
      case int color2:
        return color2.ToColor();
      default:
        SettingsStorage settingsStorage = (SettingsStorage) _param0;
        return Color.FromArgb(settingsStorage.GetValue<byte>("A", (byte) 0), settingsStorage.GetValue<byte>("R", (byte) 0), settingsStorage.GetValue<byte>("G", (byte) 0), settingsStorage.GetValue<byte>("B", (byte) 0));
    }
  }

  public static SettingsStorage \u0023\u003DzXzUiEzE\u003D(this Brush _param0)
  {
    if (_param0 == null)
      throw new ArgumentNullException("brush");
    SettingsStorage settingsStorage = new SettingsStorage();
    if (!(_param0 is SolidColorBrush solidColorBrush))
    {
      if (!(_param0 is LinearGradientBrush linearGradientBrush))
      {
        if (!(_param0 is RadialGradientBrush radialGradientBrush))
          throw new ArgumentOutOfRangeException("brush", (object) _param0, LocalizedStrings.InvalidValue);
        settingsStorage.SetValue<string>("Type", TypeHelper.GetTypeName(typeof (RadialGradientBrush), true));
        settingsStorage.SetValue<ColorInterpolationMode>("ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode);
        settingsStorage.SetValue<double>("Opacity", radialGradientBrush.Opacity);
        settingsStorage.SetValue<Point>("Center", radialGradientBrush.Center);
        settingsStorage.SetValue<Point>("GradientOrigin", radialGradientBrush.GradientOrigin);
        settingsStorage.SetValue<GradientSpreadMethod>("SpreadMethod", radialGradientBrush.SpreadMethod);
        settingsStorage.SetValue<SettingsStorage[]>("GradientStops", radialGradientBrush.GradientStops.\u0023\u003Dzpns3ZB6WZd\u00243Lc\u0024VVA\u003D\u003D());
        settingsStorage.SetValue<double>("RadiusX", radialGradientBrush.RadiusX);
        settingsStorage.SetValue<double>("RadiusY", radialGradientBrush.RadiusY);
        settingsStorage.SetValue<BrushMappingMode>("MappingMode", radialGradientBrush.MappingMode);
      }
      else
      {
        settingsStorage.SetValue<string>("Type", TypeHelper.GetTypeName(typeof (LinearGradientBrush), true));
        settingsStorage.SetValue<ColorInterpolationMode>("ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode);
        settingsStorage.SetValue<double>("Opacity", linearGradientBrush.Opacity);
        settingsStorage.SetValue<Point>("StartPoint", linearGradientBrush.StartPoint);
        settingsStorage.SetValue<Point>("EndPoint", linearGradientBrush.EndPoint);
        settingsStorage.SetValue<GradientSpreadMethod>("SpreadMethod", linearGradientBrush.SpreadMethod);
        settingsStorage.SetValue<SettingsStorage[]>("GradientStops", linearGradientBrush.GradientStops.\u0023\u003Dzpns3ZB6WZd\u00243Lc\u0024VVA\u003D\u003D());
      }
    }
    else
    {
      settingsStorage.SetValue<string>("Type", TypeHelper.GetTypeName(typeof (SolidColorBrush), true));
      settingsStorage.SetValue<int>("Color", solidColorBrush.Color.ToInt());
      settingsStorage.SetValue<double>("Opacity", solidColorBrush.Opacity);
    }
    return settingsStorage;
  }

  public static SettingsStorage[] \u0023\u003Dzpns3ZB6WZd\u00243Lc\u0024VVA\u003D\u003D(
    this GradientStopCollection _param0)
  {
    if (_param0 == null)
      throw new ArgumentNullException("items");
    List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
    foreach (GradientStop gradientStop in _param0)
    {
      SettingsStorage settingsStorage = new SettingsStorage();
      settingsStorage.SetValue<int>("Color", gradientStop.Color.ToInt());
      settingsStorage.SetValue<double>("Offset", gradientStop.Offset);
      settingsStorageList.Add(settingsStorage);
    }
    return settingsStorageList.ToArray();
  }

  public static Brush \u0023\u003DzMlbK6H8\u003D(this SettingsStorage _param0)
  {
    Type actualValue = _param0 != null ? Converter.To<Type>((object) _param0.GetValue<string>("Type", (string) null)) : throw new ArgumentNullException("storage");
    if ((object) actualValue == null)
      return (Brush) null;
    if (actualValue == typeof (SolidColorBrush))
    {
      SolidColorBrush solidColorBrush = new SolidColorBrush();
      solidColorBrush.Color = _param0.GetValue<object>("Color", (object) null).\u0023\u003Dz3XFp9f4\u003D();
      solidColorBrush.Opacity = _param0.GetValue<double>("Opacity", 0.0);
      return (Brush) solidColorBrush;
    }
    if (actualValue == typeof (LinearGradientBrush))
    {
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
      linearGradientBrush.ColorInterpolationMode = _param0.GetValue<ColorInterpolationMode>("ColorInterpolationMode", linearGradientBrush.ColorInterpolationMode);
      linearGradientBrush.Opacity = _param0.GetValue<double>("Opacity", linearGradientBrush.Opacity);
      linearGradientBrush.StartPoint = _param0.GetValue<Point>("StartPoint", linearGradientBrush.StartPoint);
      linearGradientBrush.EndPoint = _param0.GetValue<Point>("EndPoint", linearGradientBrush.EndPoint);
      linearGradientBrush.SpreadMethod = _param0.GetValue<GradientSpreadMethod>("SpreadMethod", linearGradientBrush.SpreadMethod);
      linearGradientBrush.\u0023\u003DzsPT8Hz9P\u0024YcS(_param0.GetValue<IEnumerable<SettingsStorage>>("GradientStops", (IEnumerable<SettingsStorage>) null));
      return (Brush) linearGradientBrush;
    }
    if (!(actualValue == typeof (RadialGradientBrush)))
      throw new ArgumentOutOfRangeException("storage", (object) actualValue, LocalizedStrings.InvalidValue);
    RadialGradientBrush radialGradientBrush = new RadialGradientBrush();
    radialGradientBrush.ColorInterpolationMode = _param0.GetValue<ColorInterpolationMode>("ColorInterpolationMode", radialGradientBrush.ColorInterpolationMode);
    radialGradientBrush.Opacity = _param0.GetValue<double>("Opacity", radialGradientBrush.Opacity);
    radialGradientBrush.Center = _param0.GetValue<Point>("Center", radialGradientBrush.Center);
    radialGradientBrush.GradientOrigin = _param0.GetValue<Point>("GradientOrigin", radialGradientBrush.GradientOrigin);
    radialGradientBrush.SpreadMethod = _param0.GetValue<GradientSpreadMethod>("SpreadMethod", radialGradientBrush.SpreadMethod);
    radialGradientBrush.RadiusX = _param0.GetValue<double>("RadiusX", radialGradientBrush.RadiusX);
    radialGradientBrush.RadiusY = _param0.GetValue<double>("RadiusY", radialGradientBrush.RadiusY);
    radialGradientBrush.MappingMode = _param0.GetValue<BrushMappingMode>("MappingMode", radialGradientBrush.MappingMode);
    radialGradientBrush.\u0023\u003DzsPT8Hz9P\u0024YcS(_param0.GetValue<IEnumerable<SettingsStorage>>("GradientStops", (IEnumerable<SettingsStorage>) null));
    return (Brush) radialGradientBrush;
  }

  public static void \u0023\u003DzsPT8Hz9P\u0024YcS(
    this GradientBrush _param0,
    IEnumerable<SettingsStorage> _param1)
  {
    if (_param0 == null)
      throw new ArgumentNullException("brush");
    if (_param1 == null)
      throw new ArgumentNullException("storages");
    foreach (SettingsStorage settingsStorage in _param1)
    {
      Color color = settingsStorage.GetValue<object>("Color", (object) null).\u0023\u003Dz3XFp9f4\u003D();
      double offset = settingsStorage.GetValue<double>("Offset", 0.0);
      _param0.GradientStops.Add(new GradientStop(color, offset));
    }
  }

  public static Thickness \u0023\u003DzNnTxBiiEvu0_2NsnpA\u003D\u003D(this SettingsStorage _param0)
  {
    return new Thickness(_param0.GetValue<double>("Left", 0.0), _param0.GetValue<double>("Top", 0.0), _param0.GetValue<double>("Right", 0.0), _param0.GetValue<double>("Bottom", 0.0));
  }

  public static SettingsStorage \u0023\u003DzXzUiEzE\u003D(this Thickness _param0)
  {
    return new SettingsStorage().Set<double>("Left", _param0.Left).Set<double>("Top", _param0.Top).Set<double>("Right", _param0.Right).Set<double>("Bottom", _param0.Bottom);
  }

  private sealed class ProxyDependencyPropertyClass : DependencyObject, IDisposable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly DependencyProperty ProxyProperty = DependencyProperty.Register("Proxy", typeof (object), typeof (\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass), new PropertyMetadata((object) null, new PropertyChangedCallback(\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass.DpoChangedEventArgsCallBack)));
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Action<DependencyPropertyChangedEventArgs> dpoChangedEventArgs;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool _disposing;

    public ProxyDependencyPropertyClass(
      DependencyObject _param1,
      DependencyProperty _param2,
      Action<DependencyPropertyChangedEventArgs> _param3)
    {
      this.SetBindings(\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass.ProxyProperty, (object) _param1, new PropertyPath((object) _param2), BindingMode.OneWay);
      this.dpoChangedEventArgs = _param3;
    }

    void IDisposable.Dispose()
    {
      if (this._disposing)
        return;
      this._disposing = true;
      BindingOperations.ClearBinding((DependencyObject) this, \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass.ProxyProperty);
    }

    private static void DpoChangedEventArgsCallBack(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass zxYtXibpnAml0 = (\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.ProxyDependencyPropertyClass) _param0;
      if (zxYtXibpnAml0._disposing)
        return;
      Action<DependencyPropertyChangedEventArgs> z7CxThCs = zxYtXibpnAml0.dpoChangedEventArgs;
      if (z7CxThCs == null)
        return;
      z7CxThCs(_param1);
    }
  }
}
