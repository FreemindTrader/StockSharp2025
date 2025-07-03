// Decompiled with JetBrains decompiler
// Type: #=zdU$qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE$YGqvsZ4=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D : 
  \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J,
  IPathColor,
  IDisposable,
  \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024
{
  
  private int \u0023\u003Dz2hK6CB_HnLdnXfkqDHqGc38\u003D;
  
  private double \u0023\u003DzCBcNvb2NmGvS53uRbQFOJpZFb2Ia;
  
  private float \u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;
  
  private Color \u0023\u003Dzfzo3Zt0\u003D;
  
  private bool \u0023\u003Dzg3xZfFWMp7dcW\u0024h9DOojnRM\u003D;
  
  private readonly double[] \u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D;
  
  private bool \u0023\u003DzPsHNvsjqIq72;
  
  private bool \u0023\u003DzHmVpviN0HpGru\u0024NZJ3VStlg\u003D;
  
  private PenLineCap \u0023\u003DzCGem0PExVYDPR4B2GzA75a4\u003D;

  internal \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D(
    Color _param1,
    float _param2,
    bool _param3,
    double _param4 = 1.0,
    double[] _param5 = null)
    : this(_param1, _param2, PenLineCap.Round, _param3, _param4, _param5)
  {
  }

  internal \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D(
    Color _param1,
    float _param2,
    PenLineCap _param3,
    bool _param4,
    double _param5 = 1.0,
    double[] _param6 = null)
  {
    this.\u0023\u003DzPsHNvsjqIq72 = _param1.A == (byte) 0;
    this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D = _param2;
    this.\u0023\u003Dzfzo3Zt0\u003D = Color.FromArgb((byte) ((double) _param1.A * _param5), _param1.R, _param1.G, _param1.B);
    this.\u0023\u003Dzg3xZfFWMp7dcW\u0024h9DOojnRM\u003D = _param4;
    this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D = _param6;
    this.\u0023\u003DzTUgGEZvPwpjspFNWPQ\u003D\u003D(this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D != null && this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D.Length >= 2);
    this.\u0023\u003Dzr46TbFUbtzZY3yP09g\u003D\u003D(_param3);
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

  public double[] StrokeDashArray => this.\u0023\u003DzuGUVW3D\u0024dHUILYV7nA\u003D\u003D;

  public float StrokeThickness => this.\u0023\u003Dz9g4LKqGb_N_KCf\u0024R6Q\u003D\u003D;

  [SpecialName]
  public bool \u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D()
  {
    return this.\u0023\u003Dzg3xZfFWMp7dcW\u0024h9DOojnRM\u003D;
  }

  public Color Color => this.\u0023\u003Dzfzo3Zt0\u003D;

  [SpecialName]
  public int ColorCode => -1;

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

  public void Dispose()
  {
  }
}
