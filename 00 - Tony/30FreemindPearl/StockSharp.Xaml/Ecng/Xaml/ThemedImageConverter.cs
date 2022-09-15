// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.ThemedImageConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using StockSharp.Xaml;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public class ThemedImageConverter : IMultiValueConverter
  {
    
    private readonly DrawingImage \u0023\u003DzzhbF9Fo\u003D;

    /// <summary>
    /// </summary>
    public ThemedImageConverter(DrawingImage image)
    {
      DrawingImage drawingImage = image;
      if (drawingImage == null)
        throw new ArgumentNullException(nameof(2127279486));
      this.\u0023\u003DzzhbF9Fo\u003D = drawingImage;
    }

    /// <summary>
    /// </summary>
    public ThemedImageConverter()
    {
    }

    object IMultiValueConverter.Convert(
      object[] _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      DependencyObject dependencyObject = _param1[0] as DependencyObject;
      WpfSvgPalette wpfSvgPalette = _param1[2] as WpfSvgPalette;
      WpfSvgPalette svgPalette = (_param1[1] as ThemeTreeWalker)?.get_InplaceResourceProvider().GetSvgPalette((object) dependencyObject, (ThemeTreeWalker) null);
      if (this.\u0023\u003DzzhbF9Fo\u003D != null)
        return (object) ThemedImageConverter.\u0023\u003DzKwEyjrI\u003D(this.\u0023\u003DzzhbF9Fo\u003D, wpfSvgPalette, svgPalette);
      DrawingImage drawingImage = _param1[3] as DrawingImage;
      if (drawingImage != null)
        return (object) ThemedImageConverter.\u0023\u003DzKwEyjrI\u003D(drawingImage, wpfSvgPalette, svgPalette);
      string key = _param1[3] as string;
      if (key == null)
        return (object) null;
      return (object) ThemedIconsExtension.GetImage(key);
    }

    object[] IMultiValueConverter.ConvertBack(
      object _param1,
      Type[] _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }

    private static Brush \u0023\u003DzSzj_tIpCb2JU(
      Brush _param0,
      WpfSvgPalette _param1,
      WpfSvgPalette _param2)
    {
      if (!(_param0 is SolidColorBrush))
        return (Brush) null;
      string str = ((object) _param0).ToString().Remove(1, 2);
      Brush brush = (Brush) null;
      if ((_param1 == null ? 0 : (_param1.ReplaceBrush(str, (string) null, str, ref brush) ? 1 : 0)) != 0 || _param2 == null || (!_param2.ReplaceBrush(str, (string) null, str, ref brush) || _param1 == null))
        return brush;
      _param1.get_OverridesThemeColors();
      return brush;
    }

    private static DrawingImage \u0023\u003DzKwEyjrI\u003D(
      DrawingImage _param0,
      WpfSvgPalette _param1,
      WpfSvgPalette _param2)
    {
      Brush brush1 = _param0.GetBrush();
      if (brush1 == null)
        return _param0;
      Brush brush2 = ThemedImageConverter.\u0023\u003DzSzj_tIpCb2JU(brush1, _param1, _param2);
      if (brush2 != null && ((object) brush2).ToString() != ((object) brush1).ToString())
      {
        _param0 = _param0.Clone();
        _param0.UpdateBrush(brush2);
      }
      return _param0;
    }
  }
}
