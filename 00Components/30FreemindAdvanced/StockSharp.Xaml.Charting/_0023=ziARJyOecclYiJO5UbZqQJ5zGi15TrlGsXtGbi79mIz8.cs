// Decompiled with JetBrains decompiler
// Type: #=ziARJyOecclYiJO5UbZqQJ5zGi15TrlGsXtGbi79mIz8ypjHCWp1KngOnCtdn
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
internal sealed class \u0023\u003DziARJyOecclYiJO5UbZqQJ5zGi15TrlGsXtGbi79mIz8ypjHCWp1KngOnCtdn : 
  IBrush2D,
  IPathColor,
  IDisposable
{
  
  private readonly Brush \u0023\u003DzPbF2kpY\u003D;
  
  private bool \u0023\u003DzxiNuzSVW5XI8jjmXxQ\u003D\u003D;
  
  private Size \u0023\u003Dz8FHweBCuOlUf = Size.Empty;
  
  private byte[] \u0023\u003Dz2IFpKD3PCrRb;

  public \u0023\u003DziARJyOecclYiJO5UbZqQJ5zGi15TrlGsXtGbi79mIz8ypjHCWp1KngOnCtdn(Brush _param1)
  {
    this.\u0023\u003DzEJj25CVDlBfT(_param1.Opacity == 0.0 || _param1 is SolidColorBrush && ((SolidColorBrush) _param1).Color.A == (byte) 0);
    this.\u0023\u003DzPbF2kpY\u003D = _param1;
  }

  public Color Color => throw new NotImplementedException();

  [SpecialName]
  public int ColorCode => throw new NotImplementedException();

  [SpecialName]
  public bool \u0023\u003DzZTHbSX1_i1\u0024W() => throw new NotImplementedException();

  [CompilerGenerated]
  [SpecialName]
  public bool IsTransparent => this.\u0023\u003DzxiNuzSVW5XI8jjmXxQ\u003D\u003D;

  private void \u0023\u003DzEJj25CVDlBfT(bool _param1)
  {
    this.\u0023\u003DzxiNuzSVW5XI8jjmXxQ\u003D\u003D = _param1;
  }

  public void Dispose()
  {
  }

  public unsafe byte[] \u0023\u003DzzhMOYlo\u003D(Size _param1)
  {
    if (Size.op_Inequality(this.\u0023\u003Dz8FHweBCuOlUf, _param1) || this.\u0023\u003Dz2IFpKD3PCrRb == null)
    {
      Rectangle rectangle = new Rectangle();
      rectangle.Width = _param1.Width;
      rectangle.Height = _param1.Height;
      rectangle.Fill = this.\u0023\u003DzPbF2kpY\u003D;
      rectangle.\u0023\u003DzI0WdlDcUgrX_();
      WriteableBitmap writeableBitmap = rectangle.\u0023\u003DzWmxEXx9881f\u0024((int) _param1.Width, (int) _param1.Height);
      this.\u0023\u003Dz2IFpKD3PCrRb = new byte[writeableBitmap.PixelWidth * writeableBitmap.PixelHeight * 4];
      using (BitmapContext bitmapContext = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
      {
        fixed (byte* numPtr = this.\u0023\u003Dz2IFpKD3PCrRb)
          \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA((byte*) bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc(), 0, numPtr, 0, writeableBitmap.PixelWidth * writeableBitmap.PixelHeight * 4);
      }
      this.\u0023\u003Dz8FHweBCuOlUf = _param1;
    }
    return this.\u0023\u003Dz2IFpKD3PCrRb;
  }
}
