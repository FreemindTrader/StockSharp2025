// Decompiled with JetBrains decompiler
// Type: #=zSnHC0BRBQCx0F$gJzRjVTNP2RJkJyBpHnvye19IK3Nco$rbGeT_23aNGi1tfLTsAtQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows.Media.Imaging;

#nullable disable
internal sealed class \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTNP2RJkJyBpHnvye19IK3Nco\u0024rbGeT_23aNGi1tfLTsAtQ\u003D\u003D : 
  IDisposable,
  \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly WriteableBitmap \u0023\u003DzaL\u0024oZOu2gmct;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private float \u0023\u003Dz\u0024F\u0024ERMIoRsdr6mT2iQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private float \u0023\u003DzpNyBX\u00240Erlkj2Cvvig\u003D\u003D;

  public \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTNP2RJkJyBpHnvye19IK3Nco\u0024rbGeT_23aNGi1tfLTsAtQ\u003D\u003D(
    WriteableBitmap _param1)
  {
    this.\u0023\u003DzaL\u0024oZOu2gmct = _param1;
    this.Width = (float) this.\u0023\u003DzaL\u0024oZOu2gmct.PixelWidth;
    this.Height = (float) this.\u0023\u003DzaL\u0024oZOu2gmct.PixelHeight;
  }

  public WriteableBitmap \u0023\u003DzZin35e8ltnFe() => this.\u0023\u003DzaL\u0024oZOu2gmct;

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
