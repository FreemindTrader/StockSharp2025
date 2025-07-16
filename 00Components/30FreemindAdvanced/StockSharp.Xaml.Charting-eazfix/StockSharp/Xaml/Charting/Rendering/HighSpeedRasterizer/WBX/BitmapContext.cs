// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX.BitmapContext
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

#nullable disable
namespace StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX;

public struct BitmapContext : IDisposable
{
  
  private readonly WriteableBitmap \u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D;
  
  private readonly int \u0023\u003DzvuxzmOyMmMi2;
  
  private readonly int \u0023\u003DzLbUfdCC4TeZU;
  
  private static readonly IDictionary<WriteableBitmap, int> \u0023\u003DzGVij5mrYWE_S = (IDictionary<WriteableBitmap, int>) new Dictionary<WriteableBitmap, int>();
  
  private readonly \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB \u0023\u003DztYFyZvw\u003D;
  
  private readonly unsafe int* \u0023\u003DzwoNHM0hR9QC1;
  
  private readonly int \u0023\u003DzNkjnJzE\u003D;

  public unsafe BitmapContext(WriteableBitmap _param1)
  {
    this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D = _param1;
    this.\u0023\u003DzvuxzmOyMmMi2 = this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.PixelWidth;
    this.\u0023\u003DzLbUfdCC4TeZU = this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.PixelHeight;
    this.\u0023\u003DztYFyZvw\u003D = (\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 1;
    lock (BitmapContext.\u0023\u003DzGVij5mrYWE_S)
    {
      if (!BitmapContext.\u0023\u003DzGVij5mrYWE_S.ContainsKey(this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D))
      {
        BitmapContext.\u0023\u003DzGVij5mrYWE_S.Add(this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D, 0);
        this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.Lock();
      }
      BitmapContext.\u0023\u003DzGVij5mrYWE_S[this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D]++;
    }
    this.\u0023\u003DzwoNHM0hR9QC1 = (int*) (void*) this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.BackBuffer;
    this.\u0023\u003DzNkjnJzE\u003D = (int) ((double) (this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.BackBufferStride / 4) * (double) this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.PixelHeight);
  }

  public BitmapContext(
    WriteableBitmap _param1,
    \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB _param2)
    : this(_param1)
  {
    this.\u0023\u003DztYFyZvw\u003D = _param2;
  }

  public WriteableBitmap \u0023\u003DzZin35e8ltnFe()
  {
    return this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D;
  }

  public int \u0023\u003DzOc4Ixb6AQPL8() => this.\u0023\u003DzvuxzmOyMmMi2;

  public int \u0023\u003DzeaNYBEDp1wgD() => this.\u0023\u003DzLbUfdCC4TeZU;

  public unsafe int* \u0023\u003DzSKG\u0024_qBsOJZc() => this.\u0023\u003DzwoNHM0hR9QC1;

  public int \u0023\u003DzxhbmvAVxpXvh() => this.\u0023\u003DzNkjnJzE\u003D;

  public static unsafe void \u0023\u003Dzk\u0024wemaJeIY7r(
    BitmapContext _param0,
    int _param1,
    BitmapContext _param2,
    int _param3,
    int _param4)
  {
    \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) _param0.\u0023\u003DzSKG\u0024_qBsOJZc(), _param1, (byte*) _param2.\u0023\u003DzSKG\u0024_qBsOJZc(), _param3, _param4);
  }

  public static unsafe void \u0023\u003Dzk\u0024wemaJeIY7r(
    int[] _param0,
    int _param1,
    BitmapContext _param2,
    int _param3,
    int _param4)
  {
    fixed (int* numPtr = _param0)
      \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) numPtr, _param1, (byte*) _param2.\u0023\u003DzSKG\u0024_qBsOJZc(), _param3, _param4);
  }

  public static unsafe void \u0023\u003Dzk\u0024wemaJeIY7r(
    byte[] _param0,
    int _param1,
    BitmapContext _param2,
    int _param3,
    int _param4)
  {
    fixed (byte* numPtr = _param0)
      \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA(numPtr, _param1, (byte*) _param2.\u0023\u003DzSKG\u0024_qBsOJZc(), _param3, _param4);
  }

  public static unsafe void \u0023\u003Dzk\u0024wemaJeIY7r(
    BitmapContext _param0,
    int _param1,
    byte[] _param2,
    int _param3,
    int _param4)
  {
    fixed (byte* numPtr = _param2)
      \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) _param0.\u0023\u003DzSKG\u0024_qBsOJZc(), _param1, numPtr, _param3, _param4);
  }

  public static unsafe void \u0023\u003Dzk\u0024wemaJeIY7r(
    BitmapContext _param0,
    int _param1,
    int[] _param2,
    int _param3,
    int _param4)
  {
    fixed (int* numPtr = _param2)
      \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) _param0.\u0023\u003DzSKG\u0024_qBsOJZc(), _param1, (byte*) numPtr, _param3, _param4);
  }

  public void Clear()
  {
    \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003DzvOds\u0024YwY7CNP(this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.BackBuffer, 0, this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.BackBufferStride * this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.PixelHeight);
  }

  private static int Dec(WriteableBitmap _param0)
  {
    int num1;
    if (!BitmapContext.\u0023\u003DzGVij5mrYWE_S.TryGetValue(_param0, out num1))
      return -1;
    int num2 = num1 - 1;
    BitmapContext.\u0023\u003DzGVij5mrYWE_S[_param0] = num2;
    return num2;
  }

  public void Dispose()
  {
    lock (BitmapContext.\u0023\u003DzGVij5mrYWE_S)
    {
      if (BitmapContext.Dec(this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D) != 0)
        return;
      BitmapContext.\u0023\u003DzGVij5mrYWE_S.Remove(this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D);
      if (this.\u0023\u003DztYFyZvw\u003D == (\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 1)
        this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.AddDirtyRect(new Int32Rect(0, 0, this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.PixelWidth, this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.PixelHeight));
      this.\u0023\u003DzJaGAb17LF3w6rjuFVgYtzFM\u003D.Unlock();
    }
  }
}
