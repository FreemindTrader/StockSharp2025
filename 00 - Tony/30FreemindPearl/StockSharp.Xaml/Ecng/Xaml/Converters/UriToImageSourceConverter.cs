// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.UriToImageSourceConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Ecng.Xaml.Converters
{
  /// <summary>
  /// Converts pack URI to image source. Supports svg and png.
  /// </summary>
  public class UriToImageSourceConverter : IValueConverter
  {
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      Uri uriSource = _param1 as Uri;
      if ((object) uriSource == null)
        return Binding.DoNothing;
      if (!uriSource.IsAbsoluteUri)
      {
        string str1 = nameof(2127281427);
        Uri uri = uriSource;
        string str2 = (object) uri != null ? uri.ToString() : (string) null;
        uriSource = new Uri(str1 + str2, UriKind.Absolute);
      }
      if (!uriSource.ToString().ToLowerInvariant().EndsWith(nameof(2127278040)))
        return (object) new BitmapImage(uriSource);
      return (object) WpfSvgRenderer.CreateImageSource(uriSource, (Uri) null, new Size?(), (WpfSvgPalette) null, (string) null, new bool?(), true, true);
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }
}
