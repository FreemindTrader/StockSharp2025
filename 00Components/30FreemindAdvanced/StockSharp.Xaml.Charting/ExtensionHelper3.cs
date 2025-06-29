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
internal static class ExtensionHelper3
{
    private static readonly List<IDisposable> \u0023\u003Dzp45P\u0024YzlzdD9IksQXA\u003D\u003D = new List<IDisposable>();

  public static IDisposable \u0023\u003DzCZHTVdsqw3mR(
    this DependencyObject _param0,
    DependencyProperty _param1,
    Action<DependencyPropertyChangedEventArgs> _param2)
    {
        ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0 zxYtXibpnAml0 = new ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0(_param0, _param1, _param2);
        ExtensionHelper3.\u0023\u003Dzp45P\u0024YzlzdD9IksQXA\u003D\u003D.Add((IDisposable)zxYtXibpnAml0);
        return (IDisposable)zxYtXibpnAml0;
    }

    public static Color FromARGB(this object _param0)
    {
        switch (_param0)
        {
            case null:
                throw new ArgumentNullException("null");
            case long color1:
                return ((int)color1).ToColor();
            case int color2:
                return color2.ToColor();
            default:
                SettingsStorage settingsStorage = (SettingsStorage)_param0;
                return Color.FromArgb(settingsStorage.GetValue<byte>("A", (byte)0), settingsStorage.GetValue<byte>("R", (byte)0), settingsStorage.GetValue<byte>("G", (byte)0), settingsStorage.GetValue<byte>("B", (byte)0));
        }
    }

    public static SettingsStorage SaveBrush(this Brush myBrush)
    {
        if (myBrush == null)
            throw new ArgumentNullException("");
        SettingsStorage settingsStorage = new SettingsStorage();
        if (!(myBrush is SolidColorBrush solidColorBrush))
        {
            if (!(myBrush is LinearGradientBrush linearGradientBrush))
            {
                if (!(myBrush is RadialGradientBrush radialGradientBrush))
                    throw new ArgumentOutOfRangeException("", (object)myBrush, LocalizedStrings.InvalidValue);
                settingsStorage.SetValue<string>("", TypeHelper.GetTypeName(typeof(RadialGradientBrush), true));
                settingsStorage.SetValue<ColorInterpolationMode>("", radialGradientBrush.ColorInterpolationMode);
                settingsStorage.SetValue<double>("", radialGradientBrush.Opacity);
                settingsStorage.SetValue<Point>("", radialGradientBrush.Center);
                settingsStorage.SetValue<Point>("", radialGradientBrush.GradientOrigin);
                settingsStorage.SetValue<GradientSpreadMethod>("", radialGradientBrush.SpreadMethod);
                settingsStorage.SetValue<SettingsStorage[]>("", radialGradientBrush.GradientStops.SaveStops());
                settingsStorage.SetValue<double>("", radialGradientBrush.RadiusX);
                settingsStorage.SetValue<double>("", radialGradientBrush.RadiusY);
                settingsStorage.SetValue<BrushMappingMode>("", radialGradientBrush.MappingMode);
            }
            else
            {
                settingsStorage.SetValue<string>("", TypeHelper.GetTypeName(typeof(LinearGradientBrush), true));
                settingsStorage.SetValue<ColorInterpolationMode>("", linearGradientBrush.ColorInterpolationMode);
                settingsStorage.SetValue<double>("", linearGradientBrush.Opacity);
                settingsStorage.SetValue<Point>("", linearGradientBrush.StartPoint);
                settingsStorage.SetValue<Point>("", linearGradientBrush.EndPoint);
                settingsStorage.SetValue<GradientSpreadMethod>("", linearGradientBrush.SpreadMethod);
                settingsStorage.SetValue<SettingsStorage[]>("", linearGradientBrush.GradientStops.SaveStops());
            }
        }
        else
        {
            settingsStorage.SetValue<string>("", TypeHelper.GetTypeName(typeof(SolidColorBrush), true));
            settingsStorage.SetValue<int>("", solidColorBrush.Color.ToInt());
            settingsStorage.SetValue<double>("", solidColorBrush.Opacity);
        }
        return settingsStorage;
    }

    public static SettingsStorage[] SaveStops(
      this GradientStopCollection _param0)
    {
        if (_param0 == null)
            throw new ArgumentNullException("");
        List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
        foreach (GradientStop gradientStop in _param0)
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.SetValue<int>("", gradientStop.Color.ToInt());
            settingsStorage.SetValue<double>("", gradientStop.Offset);
            settingsStorageList.Add(settingsStorage);
        }
        return settingsStorageList.ToArray();
    }

    public static Brush GetBrush(this SettingsStorage _param0)
    {
        Type actualValue = _param0 != null ? Converter.To<Type>((object)_param0.GetValue<string>("", (string)null)) : throw new ArgumentNullException("");
        if ((object)actualValue == null)
            return (Brush)null;
        if (actualValue == typeof(SolidColorBrush))
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = _param0.GetValue<object>("", (object)null).FromARGB();
            solidColorBrush.Opacity = _param0.GetValue<double>("", 0.0);
            return (Brush)solidColorBrush;
        }
        if (actualValue == typeof(LinearGradientBrush))
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.ColorInterpolationMode = _param0.GetValue<ColorInterpolationMode>("", linearGradientBrush.ColorInterpolationMode);
            linearGradientBrush.Opacity = _param0.GetValue<double>("", linearGradientBrush.Opacity);
            linearGradientBrush.StartPoint = _param0.GetValue<Point>("", linearGradientBrush.StartPoint);
            linearGradientBrush.EndPoint = _param0.GetValue<Point>("", linearGradientBrush.EndPoint);
            linearGradientBrush.SpreadMethod = _param0.GetValue<GradientSpreadMethod>("", linearGradientBrush.SpreadMethod);
            linearGradientBrush.GetStops(_param0.GetValue<IEnumerable<SettingsStorage>>("", (IEnumerable<SettingsStorage>)null));
            return (Brush)linearGradientBrush;
        }
        if (!(actualValue == typeof(RadialGradientBrush)))
            throw new ArgumentOutOfRangeException("", (object)actualValue, LocalizedStrings.InvalidValue);
        RadialGradientBrush radialGradientBrush = new RadialGradientBrush();
        radialGradientBrush.ColorInterpolationMode = _param0.GetValue<ColorInterpolationMode>("", radialGradientBrush.ColorInterpolationMode);
        radialGradientBrush.Opacity = _param0.GetValue<double>("", radialGradientBrush.Opacity);
        radialGradientBrush.Center = _param0.GetValue<Point>("", radialGradientBrush.Center);
        radialGradientBrush.GradientOrigin = _param0.GetValue<Point>("", radialGradientBrush.GradientOrigin);
        radialGradientBrush.SpreadMethod = _param0.GetValue<GradientSpreadMethod>("", radialGradientBrush.SpreadMethod);
        radialGradientBrush.RadiusX = _param0.GetValue<double>("", radialGradientBrush.RadiusX);
        radialGradientBrush.RadiusY = _param0.GetValue<double>("", radialGradientBrush.RadiusY);
        radialGradientBrush.MappingMode = _param0.GetValue<BrushMappingMode>("", radialGradientBrush.MappingMode);
        radialGradientBrush.GetStops(_param0.GetValue<IEnumerable<SettingsStorage>>("", (IEnumerable<SettingsStorage>)null));
        return (Brush)radialGradientBrush;
    }

    public static void GetStops(
      this GradientBrush _param0,
      IEnumerable<SettingsStorage> _param1)
    {
        if (_param0 == null)
            throw new ArgumentNullException("");
        if (_param1 == null)
            throw new ArgumentNullException("");
        foreach (SettingsStorage settingsStorage in _param1)
        {
            Color color = settingsStorage.GetValue<object>("", (object)null).FromARGB();
            double offset = settingsStorage.GetValue<double>("", 0.0);
            _param0.GradientStops.Add(new GradientStop(color, offset));
        }
    }

    public static Thickness CreateThickness(this SettingsStorage _param0)
    {
        return new Thickness(_param0.GetValue<double>("Left", 0.0), _param0.GetValue<double>("Top", 0.0), _param0.GetValue<double>("Right", 0.0), _param0.GetValue<double>("Bottom", 0.0));
    }

    public static SettingsStorage SaveBrush(this Thickness _param0)
    {
        return new SettingsStorage().Set<double>("Left", _param0.Left).Set<double>("Top", _param0.Top).Set<double>("Right", _param0.Right).Set<double>("Bottom", _param0.Bottom);
    }

    private sealed class \u0023\u003DzxYtXIbpnAMl0 : DependencyObject, IDisposable
  {
    
    private static readonly DependencyProperty \u0023\u003DzlJGaRWxhHZPw = DependencyProperty.Register("", typeof (object), typeof (ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0), new PropertyMetadata((object) null, new PropertyChangedCallback(ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0.\u0023\u003Dz_hMBr1Clbh8u)));
    
    private readonly Action<DependencyPropertyChangedEventArgs> \u0023\u003Dz7CXThCs\u003D;
    
    private bool \u0023\u003DzTH8vUR03qtWI;

    public \u0023\u003DzxYtXIbpnAMl0(
      DependencyObject _param1,
      DependencyProperty _param2,
      Action<DependencyPropertyChangedEventArgs> _param3)
    {
        this.SetBindings(ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0.\u0023\u003DzlJGaRWxhHZPw, (object)_param1, new PropertyPath((object)_param2), BindingMode.OneWay);
        this.\u0023\u003Dz7CXThCs\u003D = _param3;
    }

    void IDisposable.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D()
    {
        if (this.\u0023\u003DzTH8vUR03qtWI)
        return;
        this.\u0023\u003DzTH8vUR03qtWI = true;
        BindingOperations.ClearBinding((DependencyObject)this, ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0.\u0023\u003DzlJGaRWxhHZPw);
    }

    private static void \u0023\u003Dz_hMBr1Clbh8u(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
        ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0 zxYtXibpnAml0 = (ExtensionHelper3.\u0023\u003DzxYtXIbpnAMl0) _param0;
        if (zxYtXibpnAml0.\u0023\u003DzTH8vUR03qtWI)
        return;
        Action<DependencyPropertyChangedEventArgs> z7CxThCs = zxYtXibpnAml0.\u0023\u003Dz7CXThCs\u003D;
        if (z7CxThCs == null)
            return;
        z7CxThCs(_param1);
    }
}
}
