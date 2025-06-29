// Decompiled with JetBrains decompiler
// Type: #=z03BSxVLolBnG92GmtCJpdk$9dLYzyTumCw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

#nullable disable
internal sealed class \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D : 
  \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D,
  \u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_bD_Pyrb\u0024d0P2noEI5c\u003D,
  IDisposable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D \u0023\u003Dzwa3i3hwVZeqr;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4 \u0023\u003DzdoZ6J3YmzL6l;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int[] \u0023\u003DzwZXGaKKEActf;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private byte[] \u0023\u003Dzz4EAzUSAzMlk;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Brush \u0023\u003DzPbF2kpY\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color \u0023\u003Dzfzo3Zt0\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Size \u0023\u003Dzr20zizEqZixc = Size.Empty;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzxiNuzSVW5XI8jjmXxQ\u003D\u003D;

  public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D(
    Brush _param1,
    \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4 _param2,
    \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D _param3)
  {
    if (_param3 == null)
      throw new ArgumentNullException();
    this.\u0023\u003DzEJj25CVDlBfT(_param1.\u0023\u003Dzprl3UX8\u003D());
    this.\u0023\u003Dzwa3i3hwVZeqr = _param3;
    this.\u0023\u003DzdoZ6J3YmzL6l = _param2;
    this.\u0023\u003DzPbF2kpY\u003D = _param1;
    this.\u0023\u003Dzfzo3Zt0\u003D = this.\u0023\u003DzPbF2kpY\u003D.\u0023\u003DzTI4bfbI\u003D();
  }

  public Color Color => this.\u0023\u003Dzfzo3Zt0\u003D;

  [SpecialName]
  public int \u0023\u003DzjOBmdfcoOy1e() => throw new NotImplementedException();

  [SpecialName]
  public bool \u0023\u003DzZTHbSX1_i1\u0024W() => true;

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003Dz7zS5QbVF0tOL() => this.\u0023\u003DzxiNuzSVW5XI8jjmXxQ\u003D\u003D;

  private void \u0023\u003DzEJj25CVDlBfT(bool _param1)
  {
    this.\u0023\u003DzxiNuzSVW5XI8jjmXxQ\u003D\u003D = _param1;
  }

  public void Dispose()
  {
  }

  public unsafe int[] \u0023\u003DzCQL\u0024Quea_QT8(Size _param1)
  {
    if (this.\u0023\u003DzwZXGaKKEActf == null || this.\u0023\u003DzdoZ6J3YmzL6l == \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen && Size.op_Inequality(this.\u0023\u003Dzr20zizEqZixc, _param1))
    {
      this.\u0023\u003DzwZXGaKKEActf = this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003DzCQL\u0024Quea_QT8(_param1, this.\u0023\u003DzPbF2kpY\u003D);
      if (this.\u0023\u003DzwZXGaKKEActf == null)
      {
        Rectangle rectangle = new Rectangle();
        rectangle.Width = _param1.Width;
        rectangle.Height = _param1.Height;
        rectangle.Fill = this.\u0023\u003DzPbF2kpY\u003D;
        rectangle.\u0023\u003DzI0WdlDcUgrX_();
        WriteableBitmap writeableBitmap = rectangle.\u0023\u003DzWmxEXx9881f\u0024((int) _param1.Width, (int) _param1.Height);
        this.\u0023\u003DzwZXGaKKEActf = new int[writeableBitmap.PixelWidth * writeableBitmap.PixelHeight];
        using (BitmapContext bitmapContext = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
        {
          fixed (int* numPtr = this.\u0023\u003DzwZXGaKKEActf)
            \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA(bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc(), numPtr, writeableBitmap.PixelWidth * writeableBitmap.PixelHeight);
        }
        this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003DzeYeAjD8\u003D(_param1, this.\u0023\u003DzPbF2kpY\u003D, this.\u0023\u003DzwZXGaKKEActf);
      }
      this.\u0023\u003Dzr20zizEqZixc = _param1;
    }
    return this.\u0023\u003DzwZXGaKKEActf;
  }

  public unsafe byte[] \u0023\u003DzlOSbHnJKdduh(Size _param1)
  {
    if (this.\u0023\u003Dzz4EAzUSAzMlk == null || this.\u0023\u003DzdoZ6J3YmzL6l == \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen && Size.op_Inequality(this.\u0023\u003Dzr20zizEqZixc, _param1))
    {
      this.\u0023\u003Dzz4EAzUSAzMlk = this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003DzlOSbHnJKdduh(_param1, this.\u0023\u003DzPbF2kpY\u003D);
      if (this.\u0023\u003Dzz4EAzUSAzMlk == null)
      {
        Rectangle rectangle = new Rectangle();
        rectangle.Width = _param1.Width;
        rectangle.Height = _param1.Height;
        rectangle.Fill = this.\u0023\u003DzPbF2kpY\u003D;
        rectangle.\u0023\u003DzI0WdlDcUgrX_();
        WriteableBitmap writeableBitmap = rectangle.\u0023\u003DzWmxEXx9881f\u0024((int) _param1.Width, (int) _param1.Height);
        this.\u0023\u003Dzz4EAzUSAzMlk = new byte[writeableBitmap.PixelWidth * writeableBitmap.PixelHeight * 4];
        using (BitmapContext bitmapContext = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
        {
          fixed (byte* numPtr = this.\u0023\u003Dzz4EAzUSAzMlk)
            \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc(), 0, numPtr, 0, writeableBitmap.PixelWidth * writeableBitmap.PixelHeight * 4);
        }
        for (int index = 0; index < this.\u0023\u003Dzz4EAzUSAzMlk.Length; index += 4)
        {
          int num = Math.Max((int) this.\u0023\u003Dzz4EAzUSAzMlk[index + 3], 1);
          this.\u0023\u003Dzz4EAzUSAzMlk[index + 2] = (byte) ((int) this.\u0023\u003Dzz4EAzUSAzMlk[index + 2] * (int) byte.MaxValue / num);
          this.\u0023\u003Dzz4EAzUSAzMlk[index + 1] = (byte) ((int) this.\u0023\u003Dzz4EAzUSAzMlk[index + 1] * (int) byte.MaxValue / num);
          this.\u0023\u003Dzz4EAzUSAzMlk[index] = (byte) ((int) this.\u0023\u003Dzz4EAzUSAzMlk[index] * (int) byte.MaxValue / num);
        }
        this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003DzeYeAjD8\u003D(_param1, this.\u0023\u003DzPbF2kpY\u003D, this.\u0023\u003Dzz4EAzUSAzMlk);
      }
      this.\u0023\u003Dzr20zizEqZixc = _param1;
    }
    return this.\u0023\u003Dzz4EAzUSAzMlk;
  }

  public int \u0023\u003DzzG4VYzuERUhu4NopskLB3k1V0Pwdew0Cew\u003D\u003D(
    int _param1,
    int _param2,
    Rect _param3,
    double _param4)
  {
    return this.\u0023\u003Dz\u0024xczSuty0EvqUf4ZL1LMev_qjFEtd6bSBQ\u003D\u003D(_param1, _param2, _param3, _param4) * 4;
  }

  public int \u0023\u003DzzG4VYzuERUhu4NopskLB3k1V0Pwdew0Cew\u003D\u003D(
    int _param1,
    int _param2,
    double _param3)
  {
    return this.\u0023\u003Dz\u0024xczSuty0EvqUf4ZL1LMev_qjFEtd6bSBQ\u003D\u003D(_param1, _param2, _param3) * 4;
  }

  public int \u0023\u003DzsWL8AoDV5hOplPUdrMxd1H\u0024DVzcEoR95WA\u003D\u003D(
    int _param1,
    int _param2,
    double _param3)
  {
    return this.\u0023\u003Dz3JSOgExMLPL4MzHkFlni7xSDFMvwvbrIEQ\u003D\u003D(_param1, _param2, _param3) * 4;
  }

  public int \u0023\u003Dz\u0024xczSuty0EvqUf4ZL1LMev_qjFEtd6bSBQ\u003D\u003D(
    int _param1,
    int _param2,
    Rect _param3,
    double _param4)
  {
    if (this.\u0023\u003DzdoZ6J3YmzL6l == \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen)
      return this.\u0023\u003Dz3JSOgExMLPL4MzHkFlni7xSDFMvwvbrIEQ\u003D\u003D(_param1, _param2, _param4);
    int width = (int) this.\u0023\u003Dzr20zizEqZixc.Width;
    int height = (int) this.\u0023\u003Dzr20zizEqZixc.Height;
    int num1 = (int) (((double) _param1 - _param3.Left) / _param3.Width * (double) width);
    int num2 = (int) (((double) _param2 - _param3.Top) / _param3.Height * (double) height);
    this.\u0023\u003DzIkPx2kxLH_MDCBXQJw\u003D\u003D(ref num1, ref num2, _param4);
    return num2 * width + num1;
  }

  public int \u0023\u003Dz3JSOgExMLPL4MzHkFlni7xSDFMvwvbrIEQ\u003D\u003D(
    int _param1,
    int _param2,
    double _param3)
  {
    this.\u0023\u003DzIkPx2kxLH_MDCBXQJw\u003D\u003D(ref _param1, ref _param2, _param3);
    return _param2 * (int) this.\u0023\u003Dzr20zizEqZixc.Width + _param1;
  }

  private void \u0023\u003DzIkPx2kxLH_MDCBXQJw\u003D\u003D(
    ref int _param1,
    ref int _param2,
    double _param3)
  {
    int width = (int) this.\u0023\u003Dzr20zizEqZixc.Width;
    int height = (int) this.\u0023\u003Dzr20zizEqZixc.Height;
    if (_param3 != 0.0)
    {
      int num1 = width / 2;
      int num2 = height / 2;
      double num3 = (double) (_param1 - num1) / (double) width;
      double num4 = (double) (_param2 - num2) / (double) height;
      double num5 = num3 * Math.Cos(_param3) - num4 * Math.Sin(_param3);
      double num6 = num3 * Math.Sin(_param3) + num4 * Math.Cos(_param3);
      _param1 = num1 + (int) (num5 * (double) width);
      _param2 = num2 + (int) (num6 * (double) width);
    }
    if (_param1 >= width)
      _param1 = width - 1;
    else if (_param1 < 0)
      _param1 = 0;
    if (_param2 >= height)
    {
      _param2 = height - 1;
    }
    else
    {
      if (_param2 >= 0)
        return;
      _param2 = 0;
    }
  }

  public int \u0023\u003Dz\u0024xczSuty0EvqUf4ZL1LMev_qjFEtd6bSBQ\u003D\u003D(
    int _param1,
    int _param2,
    double _param3)
  {
    if (this.\u0023\u003DzdoZ6J3YmzL6l == \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen)
      return this.\u0023\u003Dz3JSOgExMLPL4MzHkFlni7xSDFMvwvbrIEQ\u003D\u003D(_param1, _param2, _param3);
    Rect rect = new Rect(new Point(0.0, 0.0), this.\u0023\u003Dzr20zizEqZixc);
    int width = (int) this.\u0023\u003Dzr20zizEqZixc.Width;
    int height = (int) this.\u0023\u003Dzr20zizEqZixc.Height;
    int num1 = (int) (((double) _param1 - rect.Left) / rect.Width * (double) width);
    int num2 = (int) (((double) _param2 - rect.Top) / rect.Height * (double) height);
    this.\u0023\u003DzIkPx2kxLH_MDCBXQJw\u003D\u003D(ref num1, ref num2, _param3);
    return num2 * width + num1;
  }
}
