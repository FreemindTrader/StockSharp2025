// Decompiled with JetBrains decompiler
// Type: #=zVZAnYWMfoaQCzNrFMqw3u9INFqZgJCYx2M2HF$eccqaxUkBriA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
public static class \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u9INFqZgJCYx2M2HF\u0024eccqaxUkBriA\u003D\u003D
{
  public static BitmapSource \u0023\u003Dz12fMuw\u0024m\u002480t(this UIElement _param0)
  {
    double height = _param0.RenderSize.Height;
    RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int) _param0.RenderSize.Width, (int) height, 96.0, 96.0, PixelFormats.Pbgra32);
    renderTargetBitmap.Render((Visual) _param0.\u0023\u003DzhQrUVPJauEUN().Single<DependencyObject>());
    return (BitmapSource) renderTargetBitmap;
  }

  public static void \u0023\u003DzopWkwiY5TTl5(
    BitmapSource _param0,
    string _param1,
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA8xQithIyJQfPQ\u003D\u003D _param2)
  {
    BitmapEncoder bitmapEncoder = \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u9INFqZgJCYx2M2HF\u0024eccqaxUkBriA\u003D\u003D.\u0023\u003DzoFwoMUE\u003D(_param2);
    using (FileStream fileStream = new FileStream(_param1, FileMode.Create))
      _param0.\u0023\u003Dzb\u0024ya79WHAcWI((Stream) fileStream, bitmapEncoder);
  }

  private static BitmapEncoder \u0023\u003DzoFwoMUE\u003D(
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA8xQithIyJQfPQ\u003D\u003D _param0)
  {
    if (_param0 == (\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA8xQithIyJQfPQ\u003D\u003D) 0)
      return (BitmapEncoder) new PngBitmapEncoder();
    if (_param0 == (\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA8xQithIyJQfPQ\u003D\u003D) 2)
      return (BitmapEncoder) new BmpBitmapEncoder();
    if (_param0 == (\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA8xQithIyJQfPQ\u003D\u003D) 1)
      return (BitmapEncoder) new JpegBitmapEncoder();
    throw new InvalidEnumArgumentException("Unsupported ExportType");
  }

  private static void \u0023\u003Dzb\u0024ya79WHAcWI(
    this BitmapSource _param0,
    Stream _param1,
    BitmapEncoder _param2)
  {
    _param2.Frames.Add(BitmapFrame.Create(_param0));
    _param2.Save(_param1);
  }
}
