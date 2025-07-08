// Decompiled with JetBrains decompiler
// Type: #=zPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX;
using System;
using System.Diagnostics;
using System.Windows.Media.Imaging;

#nullable disable
internal sealed class \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D : 
  IDisposable,
  \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D
{
  
  private readonly WriteableBitmap \u0023\u003DzpnLYJ1Q\u003D;
  
  private \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D \u0023\u003DzK3L_6jB1hKFR;
  
  private float \u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
  
  private float \u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;

  public unsafe \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D(
    WriteableBitmap _param1)
  {
    this.\u0023\u003DzpnLYJ1Q\u003D = _param1;
    this.\u0023\u003DzK3L_6jB1hKFR = new \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D(_param1.PixelWidth, _param1.PixelHeight, 32 /*0x20*/, (\u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOESA9yl1dafdSqQDeAMKXaBPbCxPlPQgez5bfFbgS\u0024CknPn64g\u003D) new \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0IIMw_vgUHCtHi5cN88nyND2bXVqyA2T5fC());
    using (BitmapContext bitmapContext = this.\u0023\u003DzpnLYJ1Q\u003D.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
    {
      fixed (byte* numPtr = &this.\u0023\u003DzK3L_6jB1hKFR.\u0023\u003Dz9b1_JhA\u003D()[0])
        \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc(), 0, numPtr, 0, _param1.PixelWidth * _param1.PixelHeight * 4);
    }
    this.Width = (float) this.\u0023\u003DzpnLYJ1Q\u003D.PixelWidth;
    this.Height = (float) this.\u0023\u003DzpnLYJ1Q\u003D.PixelHeight;
  }

  public byte[] \u0023\u003Dz9b1_JhA\u003D()
  {
    return this.\u0023\u003DzK3L_6jB1hKFR.\u0023\u003Dz9b1_JhA\u003D();
  }

  public int \u0023\u003DzHlHGfKJZNJsq(int _param1, int _param2)
  {
    return this.\u0023\u003DzK3L_6jB1hKFR.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
  }

  public void Dispose()
  {
  }

  public float Width
  {
    get => this.\u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
    private set => this.\u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D = value;
  }

  public float Height
  {
    get => this.\u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;
    private set => this.\u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D = value;
  }
}
