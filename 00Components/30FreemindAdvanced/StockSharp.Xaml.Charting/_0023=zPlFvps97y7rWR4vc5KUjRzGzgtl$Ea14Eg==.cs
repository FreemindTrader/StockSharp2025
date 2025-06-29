// Decompiled with JetBrains decompiler
// Type: #=zPlFvps97y7rWR4vc5KUjRzGzgtl$Ea14Eg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
  private static readonly List<IDisposable> \u0023\u003Dzp45P\u0024YzlzdD9IksQXA\u003D\u003D = new List<IDisposable>();

  public static IDisposable \u0023\u003DzCZHTVdsqw3mR(
    this DependencyObject _param0,
    DependencyProperty _param1,
    Action<DependencyPropertyChangedEventArgs> _param2)
  {
    \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0 zxYtXibpnAml0 = new \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0(_param0, _param1, _param2);
    \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003Dzp45P\u0024YzlzdD9IksQXA\u003D\u003D.Add((IDisposable) zxYtXibpnAml0);
    return (IDisposable) zxYtXibpnAml0;
  }

  public static Color \u0023\u003Dz3XFp9f4\u003D(this object _param0)
  {
    switch (_param0)
    {
      case null:
        throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426879));
      case long color1:
        return ((int) color1).ToColor();
      case int color2:
        return color2.ToColor();
      default:
        SettingsStorage settingsStorage = (SettingsStorage) _param0;
        return Color.FromArgb(settingsStorage.GetValue<byte>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426854), (byte) 0), settingsStorage.GetValue<byte>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426862), (byte) 0), settingsStorage.GetValue<byte>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426902), (byte) 0), settingsStorage.GetValue<byte>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426910), (byte) 0));
    }
  }

  public static SettingsStorage \u0023\u003DzXzUiEzE\u003D(this Brush _param0)
  {
    if (_param0 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426886));
    SettingsStorage settingsStorage = new SettingsStorage();
    if (!(_param0 is SolidColorBrush solidColorBrush))
    {
      if (!(_param0 is LinearGradientBrush linearGradientBrush))
      {
        if (!(_param0 is RadialGradientBrush radialGradientBrush))
          throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427127), (object) _param0, LocalizedStrings.InvalidValue);
        settingsStorage.SetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433895), TypeHelper.GetTypeName(typeof (RadialGradientBrush), true));
        settingsStorage.SetValue<ColorInterpolationMode>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426915), radialGradientBrush.ColorInterpolationMode);
        settingsStorage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426929), radialGradientBrush.Opacity);
        settingsStorage.SetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427271), radialGradientBrush.Center);
        settingsStorage.SetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427316), radialGradientBrush.GradientOrigin);
        settingsStorage.SetValue<GradientSpreadMethod>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427264), radialGradientBrush.SpreadMethod);
        settingsStorage.SetValue<SettingsStorage[]>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427283), radialGradientBrush.GradientStops.\u0023\u003Dzpns3ZB6WZd\u00243Lc\u0024VVA\u003D\u003D());
        settingsStorage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427301), radialGradientBrush.RadiusX);
        settingsStorage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427095), radialGradientBrush.RadiusY);
        settingsStorage.SetValue<BrushMappingMode>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427073), radialGradientBrush.MappingMode);
      }
      else
      {
        settingsStorage.SetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433895), TypeHelper.GetTypeName(typeof (LinearGradientBrush), true));
        settingsStorage.SetValue<ColorInterpolationMode>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426915), linearGradientBrush.ColorInterpolationMode);
        settingsStorage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426929), linearGradientBrush.Opacity);
        settingsStorage.SetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427232), linearGradientBrush.StartPoint);
        settingsStorage.SetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427213), linearGradientBrush.EndPoint);
        settingsStorage.SetValue<GradientSpreadMethod>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427264), linearGradientBrush.SpreadMethod);
        settingsStorage.SetValue<SettingsStorage[]>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427283), linearGradientBrush.GradientStops.\u0023\u003Dzpns3ZB6WZd\u00243Lc\u0024VVA\u003D\u003D());
      }
    }
    else
    {
      settingsStorage.SetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433895), TypeHelper.GetTypeName(typeof (SolidColorBrush), true));
      settingsStorage.SetValue<int>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433444), solidColorBrush.Color.ToInt());
      settingsStorage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426929), solidColorBrush.Opacity);
    }
    return settingsStorage;
  }

  public static SettingsStorage[] \u0023\u003Dzpns3ZB6WZd\u00243Lc\u0024VVA\u003D\u003D(
    this GradientStopCollection _param0)
  {
    if (_param0 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427134));
    List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
    foreach (GradientStop gradientStop in _param0)
    {
      SettingsStorage settingsStorage = new SettingsStorage();
      settingsStorage.SetValue<int>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433444), gradientStop.Color.ToInt());
      settingsStorage.SetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427113), gradientStop.Offset);
      settingsStorageList.Add(settingsStorage);
    }
    return settingsStorageList.ToArray();
  }

  public static Brush \u0023\u003DzMlbK6H8\u003D(this SettingsStorage _param0)
  {
    Type actualValue = _param0 != null ? Converter.To<Type>((object) _param0.GetValue<string>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433895), (string) null)) : throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427158));
    if ((object) actualValue == null)
      return (Brush) null;
    if (actualValue == typeof (SolidColorBrush))
    {
      SolidColorBrush solidColorBrush = new SolidColorBrush();
      solidColorBrush.Color = _param0.GetValue<object>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433444), (object) null).\u0023\u003Dz3XFp9f4\u003D();
      solidColorBrush.Opacity = _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426929), 0.0);
      return (Brush) solidColorBrush;
    }
    if (actualValue == typeof (LinearGradientBrush))
    {
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
      linearGradientBrush.ColorInterpolationMode = _param0.GetValue<ColorInterpolationMode>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426915), linearGradientBrush.ColorInterpolationMode);
      linearGradientBrush.Opacity = _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426929), linearGradientBrush.Opacity);
      linearGradientBrush.StartPoint = _param0.GetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427232), linearGradientBrush.StartPoint);
      linearGradientBrush.EndPoint = _param0.GetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427213), linearGradientBrush.EndPoint);
      linearGradientBrush.SpreadMethod = _param0.GetValue<GradientSpreadMethod>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427264), linearGradientBrush.SpreadMethod);
      linearGradientBrush.\u0023\u003DzsPT8Hz9P\u0024YcS(_param0.GetValue<IEnumerable<SettingsStorage>>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427283), (IEnumerable<SettingsStorage>) null));
      return (Brush) linearGradientBrush;
    }
    if (!(actualValue == typeof (RadialGradientBrush)))
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427137), (object) actualValue, LocalizedStrings.InvalidValue);
    RadialGradientBrush radialGradientBrush = new RadialGradientBrush();
    radialGradientBrush.ColorInterpolationMode = _param0.GetValue<ColorInterpolationMode>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426915), radialGradientBrush.ColorInterpolationMode);
    radialGradientBrush.Opacity = _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426929), radialGradientBrush.Opacity);
    radialGradientBrush.Center = _param0.GetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427271), radialGradientBrush.Center);
    radialGradientBrush.GradientOrigin = _param0.GetValue<Point>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427316), radialGradientBrush.GradientOrigin);
    radialGradientBrush.SpreadMethod = _param0.GetValue<GradientSpreadMethod>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427264), radialGradientBrush.SpreadMethod);
    radialGradientBrush.RadiusX = _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427301), radialGradientBrush.RadiusX);
    radialGradientBrush.RadiusY = _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427095), radialGradientBrush.RadiusY);
    radialGradientBrush.MappingMode = _param0.GetValue<BrushMappingMode>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427073), radialGradientBrush.MappingMode);
    radialGradientBrush.\u0023\u003DzsPT8Hz9P\u0024YcS(_param0.GetValue<IEnumerable<SettingsStorage>>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427283), (IEnumerable<SettingsStorage>) null));
    return (Brush) radialGradientBrush;
  }

  public static void \u0023\u003DzsPT8Hz9P\u0024YcS(
    this GradientBrush _param0,
    IEnumerable<SettingsStorage> _param1)
  {
    if (_param0 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427152));
    if (_param1 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427195));
    foreach (SettingsStorage settingsStorage in _param1)
    {
      Color color = settingsStorage.GetValue<object>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433444), (object) null).\u0023\u003Dz3XFp9f4\u003D();
      double offset = settingsStorage.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427113), 0.0);
      _param0.GradientStops.Add(new GradientStop(color, offset));
    }
  }

  public static Thickness \u0023\u003DzNnTxBiiEvu0_2NsnpA\u003D\u003D(this SettingsStorage _param0)
  {
    return new Thickness(_param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427170), 0.0), _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427181), 0.0), _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428507), 0.0), _param0.GetValue<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428487), 0.0));
  }

  public static SettingsStorage \u0023\u003DzXzUiEzE\u003D(this Thickness _param0)
  {
    return new SettingsStorage().Set<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427170), _param0.Left).Set<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539427181), _param0.Top).Set<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428507), _param0.Right).Set<double>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539428487), _param0.Bottom);
  }

  private sealed class \u0023\u003DzxYtXIbpnAMl0 : DependencyObject, IDisposable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly DependencyProperty \u0023\u003DzlJGaRWxhHZPw = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539426867), typeof (object), typeof (\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0), new PropertyMetadata((object) null, new PropertyChangedCallback(\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0.\u0023\u003Dz_hMBr1Clbh8u)));
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Action<DependencyPropertyChangedEventArgs> \u0023\u003Dz7CXThCs\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool \u0023\u003DzTH8vUR03qtWI;

    public \u0023\u003DzxYtXIbpnAMl0(
      DependencyObject _param1,
      DependencyProperty _param2,
      Action<DependencyPropertyChangedEventArgs> _param3)
    {
      this.SetBindings(\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0.\u0023\u003DzlJGaRWxhHZPw, (object) _param1, new PropertyPath((object) _param2), BindingMode.OneWay);
      this.\u0023\u003Dz7CXThCs\u003D = _param3;
    }

    void IDisposable.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D()
    {
      if (this.\u0023\u003DzTH8vUR03qtWI)
        return;
      this.\u0023\u003DzTH8vUR03qtWI = true;
      BindingOperations.ClearBinding((DependencyObject) this, \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0.\u0023\u003DzlJGaRWxhHZPw);
    }

    private static void \u0023\u003Dz_hMBr1Clbh8u(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      \u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0 zxYtXibpnAml0 = (\u0023\u003DzPlFvps97y7rWR4vc5KUjRzGzgtl\u0024Ea14Eg\u003D\u003D.\u0023\u003DzxYtXIbpnAMl0) _param0;
      if (zxYtXibpnAml0.\u0023\u003DzTH8vUR03qtWI)
        return;
      Action<DependencyPropertyChangedEventArgs> z7CxThCs = zxYtXibpnAml0.\u0023\u003Dz7CXThCs\u003D;
      if (z7CxThCs == null)
        return;
      z7CxThCs(_param1);
    }
  }
}
