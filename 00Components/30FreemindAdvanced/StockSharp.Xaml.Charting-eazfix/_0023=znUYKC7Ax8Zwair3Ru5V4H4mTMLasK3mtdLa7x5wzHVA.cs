// Decompiled with JetBrains decompiler
// Type: #=znUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG$N92YKzAECB
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
internal static class \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB
{
  internal static WriteableBitmap GetMath(int _param0, int _param1)
  {
    if (_param1 < 1)
      _param1 = 1;
    if (_param0 < 1)
      _param0 = 1;
    return new WriteableBitmap(_param0, _param1, 96.0, 96.0, PixelFormats.Pbgra32, (BitmapPalette) null);
  }
}
