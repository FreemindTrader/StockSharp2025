// Decompiled with JetBrains decompiler
// Type: #=zkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

#nullable disable
internal sealed class \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D : 
  IDisposable,
  IPathColor,
  \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J,
  \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024
{
  
  private int \u0023\u003Dz2hK6CB_HnLdnXfkqDHqGc38\u003D;
  
  private double \u0023\u003DzCBcNvb2NmGvS53uRbQFOJpZFb2Ia;
  
  private readonly float \u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
  
  private readonly bool \u0023\u003Dzg3xZfFWMp7dcW\u0024h9DOojnRM\u003D;
  
  private readonly Color \u0023\u003Dzfzo3Zt0\u003D;
  
  private readonly int \u0023\u003DzY0hdWq2CWq89;
  
  private BitmapContext \u0023\u003DzU\u0024ZxpXE\u003D;
  
  private double[] \u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D;
  
  private bool \u0023\u003DzPsHNvsjqIq72;
  
  private PenLineCap \u0023\u003DzCGem0PExVYDPR4B2GzA75a4\u003D;
  
  private bool \u0023\u003DzHmVpviN0HpGru\u0024NZJ3VStlg\u003D;

  internal \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D(
    Color _param1,
    int _param2,
    float _param3,
    bool _param4,
    double _param5 = 1.0,
    double[] _param6 = null)
    : this(_param1, _param2, _param3, PenLineCap.Round, _param4, _param5, _param6)
  {
  }

  internal \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D(
    Color _param1,
    int _param2,
    float _param3,
    PenLineCap _param4,
    bool _param5,
    double _param6 = 1.0,
    double[] _param7 = null)
  {
    this.\u0023\u003DzPsHNvsjqIq72 = _param1.A == (byte) 0;
    this.\u0023\u003Dzfzo3Zt0\u003D = _param1;
    this.\u0023\u003DzY0hdWq2CWq89 = _param2;
    this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = _param3;
    this.\u0023\u003Dzg3xZfFWMp7dcW\u0024h9DOojnRM\u003D = _param5;
    this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D = _param7;
    this.\u0023\u003DzTUgGEZvPwpjspFNWPQ\u003D\u003D(this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D != null && this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D.Length >= 2);
    this.\u0023\u003DzgiiprBg40DPh(_param4, _param6);
  }

  [CompilerGenerated]
  [SpecialName]
  public int \u0023\u003Dz94d6KNkFELrvsSVOyQ\u003D\u003D()
  {
    return this.\u0023\u003Dz2hK6CB_HnLdnXfkqDHqGc38\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz7jjovJyOcjl0y23idA\u003D\u003D(int _param1)
  {
    this.\u0023\u003Dz2hK6CB_HnLdnXfkqDHqGc38\u003D = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public double \u0023\u003DzQrg7PLXQAunq_KZlOvhA_wM\u003D()
  {
    return this.\u0023\u003DzCBcNvb2NmGvS53uRbQFOJpZFb2Ia;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzHX4QluI_kG8NGHqDtYeOSVM\u003D(double _param1)
  {
    this.\u0023\u003DzCBcNvb2NmGvS53uRbQFOJpZFb2Ia = _param1;
  }

  internal BitmapContext \u0023\u003DzvrdHPuKzjDX2() => this.\u0023\u003DzU\u0024ZxpXE\u003D;

  public double[] StrokeDashArray => this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D;

  public float StrokeThickness => this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;

  [SpecialName]
  public bool \u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D()
  {
    return this.\u0023\u003Dzg3xZfFWMp7dcW\u0024h9DOojnRM\u003D;
  }

  public Color Color => this.\u0023\u003Dzfzo3Zt0\u003D;

  [SpecialName]
  public int ColorCode => this.\u0023\u003DzY0hdWq2CWq89;

  [SpecialName]
  public bool IsTransparent => this.\u0023\u003DzPsHNvsjqIq72;

  [CompilerGenerated]
  [SpecialName]
  public PenLineCap \u0023\u003Dz37Umq2J4Bj\u0024RNyVC\u0024g\u003D\u003D()
  {
    return this.\u0023\u003DzCGem0PExVYDPR4B2GzA75a4\u003D;
  }

  private void \u0023\u003Dzr46TbFUbtzZY3yP09g\u003D\u003D(PenLineCap _param1)
  {
    this.\u0023\u003DzCGem0PExVYDPR4B2GzA75a4\u003D = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003DzBZpmQRzEu_oDG0nIEA\u003D\u003D()
  {
    return this.\u0023\u003DzHmVpviN0HpGru\u0024NZJ3VStlg\u003D;
  }

  private void \u0023\u003DzTUgGEZvPwpjspFNWPQ\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzHmVpviN0HpGru\u0024NZJ3VStlg\u003D = _param1;
  }

  private void \u0023\u003DzgiiprBg40DPh(PenLineCap _param1, double _param2)
  {
    if ((double) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D <= 1.0)
      return;
    Shape shape;
    switch (_param1)
    {
      case PenLineCap.Round:
        shape = (Shape) new Ellipse();
        this.\u0023\u003Dzr46TbFUbtzZY3yP09g\u003D\u003D(PenLineCap.Round);
        break;
      default:
        shape = (Shape) new Rectangle();
        this.\u0023\u003Dzr46TbFUbtzZY3yP09g\u003D\u003D(PenLineCap.Square);
        break;
    }
    shape.Width = (double) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
    shape.Height = (double) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
    shape.Opacity = _param2;
    shape.Fill = (Brush) new SolidColorBrush(Color.FromArgb((byte) (_param2 * (double) this.\u0023\u003Dzfzo3Zt0\u003D.A), this.\u0023\u003Dzfzo3Zt0\u003D.R, this.\u0023\u003Dzfzo3Zt0\u003D.G, this.\u0023\u003Dzfzo3Zt0\u003D.B));
    shape.Arrange(new Rect(0.0, 0.0, (double) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D, (double) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D));
    this.\u0023\u003DzU\u0024ZxpXE\u003D = shape.\u0023\u003DzWmxEXx9881f\u0024((int) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D, (int) this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D).\u0023\u003DzjnjmjBtrwZM5();
  }

  public void Dispose()
  {
    BitmapContext zUZxpXe = this.\u0023\u003DzU\u0024ZxpXE\u003D;
    if (zUZxpXe.\u0023\u003DzZin35e8ltnFe() == null)
      return;
    ((DispatcherObject) zUZxpXe.\u0023\u003DzZin35e8ltnFe()).Dispatcher.\u0023\u003Dz40vIrjqAtFMX(new Action(zUZxpXe.Dispose));
  }
}
