// Decompiled with JetBrains decompiler
// Type: #=zCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU$D_ZV6c$V_0H
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
public sealed class \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H : 
  \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D
{
  
  private BitmapContext \u0023\u003DzNhq58rfMvbBc;
  
  private readonly Image \u0023\u003Dz2TNhyDg\u003D;
  
  private readonly \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOa9PbMTlFJUwDxIT7iKoZ2Lr \u0023\u003Dzdp7hSzeBLlAo = new \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOa9PbMTlFJUwDxIT7iKoZ2Lr();
  
  private readonly WriteableBitmap \u0023\u003DzWfNK9E4egtoR;
  
  private readonly List<IDisposable> \u0023\u003DzBzoKzORE1dsQ = new List<IDisposable>();
  
  private Size \u0023\u003DzgYZhPyPIW8zq;

  public \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H(
    Image _param1,
    WriteableBitmap _param2,
    Size _param3,
    \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D _param4)
    : base(_param4)
  {
    this.\u0023\u003Dz2TNhyDg\u003D = _param1;
    this.\u0023\u003DzWfNK9E4egtoR = _param2;
    this.\u0023\u003DzgYZhPyPIW8zq = _param3;
    this.\u0023\u003DzNhq58rfMvbBc = this.\u0023\u003DzWfNK9E4egtoR.\u0023\u003DzjnjmjBtrwZM5();
  }

  private \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D \u0023\u003DzNFOu7BeFZYda()
  {
    return (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D) this.\u0023\u003Dzwa3i3hwVZeqr;
  }

  [SpecialName]
  public override \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOa9PbMTlFJUwDxIT7iKoZ2Lr \u0023\u003Dz7eGjoBhvKuFN()
  {
    return this.\u0023\u003Dzdp7hSzeBLlAo;
  }

  [SpecialName]
  public override Size \u0023\u003Dz8DEW4l1E337F() => this.\u0023\u003DzgYZhPyPIW8zq;

  public override IBrush2D \u0023\u003Dze8WyDhI\u003D(
    Color _param1,
    double _param2 = 1.0,
    bool? _param3 = null)
  {
    return (IBrush2D) new \u0023\u003DzMDDpCIYr0KRiCa3HPMUgujf7HNK4iKZmDV3at90jxS3JM0NsILRb5TM\u003D(_param1, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_DB895w\u003D(_param2, _param1), ((int) _param3 ?? ((double) _param1.A * _param2 < (double) byte.MaxValue ? 1 : 0)) != 0);
  }

  public override IBrush2D \u0023\u003Dze8WyDhI\u003D(
    Brush _param1,
    double _param2 = 1.0,
    \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4 _param3 = \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen)
  {
    if (_param1 == null)
      return this.\u0023\u003Dze8WyDhI\u003D(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0), 1.0, new bool?());
    return _param1 is SolidColorBrush ? this.\u0023\u003Dze8WyDhI\u003D(((SolidColorBrush) _param1).Color, _param2, new bool?()) : (IBrush2D) new \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D(_param1, _param3, this.\u0023\u003DzNFOu7BeFZYda());
  }

  public override \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzL3In9ls\u003D(
    Color _param1,
    bool _param2,
    float _param3,
    double _param4 = 1.0,
    double[] _param5 = null,
    PenLineCap _param6 = PenLineCap.Round)
  {
    return (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) new \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D(_param1, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_DB895w\u003D(_param4, _param1), _param3, _param6, _param2, _param4, _param5);
  }

  public override ISprite2D \u0023\u003DzC1WFQPaV7rDp(
    FrameworkElement _param1)
  {
    return (ISprite2D) new \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTNP2RJkJyBpHnvye19IK3Nco\u0024rbGeT_23aNGi1tfLTsAtQ\u003D\u003D(this.\u0023\u003DzNFOu7BeFZYda().\u0023\u003DzuAOGltcBIMXJ(_param1));
  }

  public override void Clear()
  {
    this.\u0023\u003DzNhq58rfMvbBc.Clear();
  }

  public override void \u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(
    ISprite2D _param1,
    Rect _param2,
    Point _param3)
  {
    this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzSyuZtKbRCpVU(new Rect(_param3.X, _param3.Y, (double) _param1.Width, (double) _param1.Height), (_param1 as \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTNP2RJkJyBpHnvye19IK3Nco\u0024rbGeT_23aNGi1tfLTsAtQ\u003D\u003D).\u0023\u003DzZin35e8ltnFe(), _param2);
  }

  public override void \u0023\u003DzsdRDlCsxeeTGJXELSQ\u003D\u003D(
    ISprite2D _param1,
    Rect _param2,
    IEnumerable<Point> _param3)
  {
    Rect rect = new Rect(0.0, 0.0, (double) _param1.Width, (double) _param1.Height);
    WriteableBitmap writeableBitmap = (_param1 as \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTNP2RJkJyBpHnvye19IK3Nco\u0024rbGeT_23aNGi1tfLTsAtQ\u003D\u003D).\u0023\u003DzZin35e8ltnFe();
    foreach (Point point in _param3)
    {
      rect.X = point.X;
      rect.Y = point.Y;
      this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzSyuZtKbRCpVU(rect, writeableBitmap, _param2);
    }
  }

  public override void \u0023\u003DzsdRDlCsxeeTGJXELSQ\u003D\u003D(
    ISprite2D _param1,
    IEnumerable<Rect> _param2)
  {
    WriteableBitmap writeableBitmap = (_param1 as \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTNP2RJkJyBpHnvye19IK3Nco\u0024rbGeT_23aNGi1tfLTsAtQ\u003D\u003D).\u0023\u003DzZin35e8ltnFe();
    foreach (Rect rect in _param2)
      this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzSyuZtKbRCpVU(rect, writeableBitmap, new Rect(0.0, 0.0, (double) _param1.Width, (double) _param1.Height));
  }

  public override void \u0023\u003DzVRUUvzhAr5SR(
    IBrush2D _param1,
    Point _param2,
    Point _param3,
    double _param4 = 0.0)
  {
    \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeClass6409();
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D = _param4;
    if (_param2.X < 0.0 && _param3.X < 0.0 || _param2.Y < 0.0 && _param3.Y < 0.0)
      return;
    double y1 = _param2.Y;
    Size size = this.\u0023\u003Dz8DEW4l1E337F();
    double height1 = size.Height;
    if (y1 > height1)
    {
      double y2 = _param3.Y;
      size = this.\u0023\u003Dz8DEW4l1E337F();
      double height2 = size.Height;
      if (y2 > height2)
        return;
    }
    double x1 = _param2.X;
    size = this.\u0023\u003Dz8DEW4l1E337F();
    double width1 = size.Width;
    if (x1 > width1)
    {
      double x2 = _param3.X;
      size = this.\u0023\u003Dz8DEW4l1E337F();
      double width2 = size.Width;
      if (x2 > width2)
        return;
    }
    this.\u0023\u003Dzpd5t5heTSEu0(ref _param2, ref _param3);
    v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg = new Rect(_param2, _param3);
    if (_param1 is \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D)
    {
      v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz32bgjTM\u003D = (\u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D) _param1;
      v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzjzt0LxM\u003D = v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzCQL\u0024Quea_QT8(this.\u0023\u003Dz8DEW4l1E337F());
      this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzVRUUvzhAr5SR((int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Left, (int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Top, (int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Right, (int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Bottom, new Func<int, int, int>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzAGjD8bO2vEL6bBEhwQ\u003D\u003D), _param1.\u0023\u003DzZTHbSX1_i1\u0024W() ? (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0 : (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6);
    }
    else
      this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzVRUUvzhAr5SR((int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Left, (int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Top, (int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Right, (int) v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg.Bottom, _param1.ColorCode, _param1.\u0023\u003DzZTHbSX1_i1\u0024W() ? (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0 : (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6);
  }

  public override void \u0023\u003DzZC71OkxY_wdX(
    IBrush2D _param1,
    IEnumerable<Tuple<Point, Point>> _param2,
    bool _param3 = false,
    double _param4 = 0.0)
  {
    \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeClass398 jq9Llz3ahZ2LrQl4 = new \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeClass398();
    jq9Llz3ahZ2LrQl4.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D = _param4;
    foreach (IEnumerable<Tuple<Point, Point>> tuples in _param2.\u0023\u003Dz4tVt5y5Hyap1kzcfrQ\u003D\u003D())
    {
      Point[] array = this.\u0023\u003Dz8zZ1iXDzEMaO(tuples).ToArray<Point>();
      int[] numArray1 = new int[array.Length * 2 + 2];
      int index1 = 0;
      int num1 = 0;
      for (; index1 < array.Length; ++index1)
      {
        int[] numArray2 = numArray1;
        int index2 = num1;
        int num2 = index2 + 1;
        int x = (int) array[index1].X;
        numArray2[index2] = x;
        int[] numArray3 = numArray1;
        int index3 = num2;
        num1 = index3 + 1;
        int num3 = (int) array[index1].Y - 1;
        numArray3[index3] = num3;
      }
      int[] numArray4 = numArray1;
      int index4 = num1;
      int index5 = index4 + 1;
      int x1 = (int) array[0].X;
      numArray4[index4] = x1;
      numArray1[index5] = (int) array[0].Y - 1;
      if (_param1 is \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D)
      {
        \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeSealedClassAgain032 wd3zPhu0dS2ZqhuzuE = new \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeSealedClassAgain032()
        {
          \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = jq9Llz3ahZ2LrQl4,
          \u0023\u003Dz32bgjTM\u003D = (\u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D) _param1
        };
        wd3zPhu0dS2ZqhuzuE.\u0023\u003Dzjzt0LxM\u003D = wd3zPhu0dS2ZqhuzuE.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzCQL\u0024Quea_QT8(this.\u0023\u003Dz8DEW4l1E337F());
        this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003Dz_I15ZX7u91\u0024T(numArray1, new Func<int, int, int>(wd3zPhu0dS2ZqhuzuE.\u0023\u003Dzs3XZaL28BMmdJ3BwPQ\u003D\u003D), _param1.\u0023\u003DzZTHbSX1_i1\u0024W() ? (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0 : (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6);
      }
      else
        this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003Dz_I15ZX7u91\u0024T(numArray1, _param1.ColorCode, _param1.\u0023\u003DzZTHbSX1_i1\u0024W() ? (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0 : (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6);
    }
  }

  public override void \u0023\u003DzX6V3YcdlNDO2(IDisposable _param1)
  {
    if (_param1 == null)
      return;
    this.\u0023\u003DzBzoKzORE1dsQ.Add(_param1);
  }

  public void \u0023\u003DzSyuZtKbRCpVU(Rect _param1, WriteableBitmap _param2, Rect _param3)
  {
    this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzSyuZtKbRCpVU(_param1, _param2, _param3);
  }

  public void \u0023\u003DzVRUUvzhAr5SR(
    IBrush2D _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DzVRUUvzhAr5SR(_param2, _param3, _param4, _param5, _param1.ColorCode, _param1.\u0023\u003DzZTHbSX1_i1\u0024W() ? (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0 : (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6);
  }

  public override void \u0023\u003DzIZCdW2WR6Rxw(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    IBrush2D _param2,
    Point _param3,
    double _param4,
    double _param5)
  {
    if (!this.\u0023\u003DzxGhbraO0gg9\u0024(_param3))
      return;
    if (_param4 <= 1.0 && _param5 <= 1.0)
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzVUQzlzp_K4Dd(this.\u0023\u003DzNhq58rfMvbBc, (int) this.\u0023\u003Dz8DEW4l1E337F().Width, (int) this.\u0023\u003Dz8DEW4l1E337F().Height, (int) _param3.X, (int) _param3.Y, _param2.ColorCode);
    }
    else
    {
      if (_param2 != null && !_param2.IsTransparent)
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(this.\u0023\u003DzNhq58rfMvbBc, (int) _param3.X, (int) _param3.Y, (int) _param4 / 2, (int) _param5 / 2, _param2.ColorCode, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
      if (_param1 == null || _param1.IsTransparent)
        return;
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(this.\u0023\u003DzNhq58rfMvbBc, (int) _param3.X, (int) _param3.Y, (int) _param4 / 2, (int) _param5 / 2, _param1.ColorCode, (int) _param1.StrokeThickness);
    }
  }

  public override void \u0023\u003DzoUffnI1MQnWod0s9Xg\u003D\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    IBrush2D _param2,
    IEnumerable<Point> _param3,
    double _param4,
    double _param5)
  {
    foreach (Point point in _param3)
      this.\u0023\u003DzIZCdW2WR6Rxw(_param1, _param2, point, _param4, _param5);
  }

  public override void \u0023\u003DztJb5\u0024zF1SLeC(
    int _param1,
    int _param2,
    int _param3,
    IList<int> _param4,
    double _param5,
    bool _param6)
  {
    this.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003DztJb5\u0024zF1SLeC(_param1, _param2, _param3, _param4, _param5, _param6);
  }

  public override void Dispose()
  {
    this.\u0023\u003DzNhq58rfMvbBc.Dispose();
    if (this.\u0023\u003Dz2TNhyDg\u003D != null && this.\u0023\u003Dz2TNhyDg\u003D.Source != this.\u0023\u003DzWfNK9E4egtoR)
      this.\u0023\u003Dz2TNhyDg\u003D.Source = (ImageSource) this.\u0023\u003DzWfNK9E4egtoR;
    foreach (IDisposable disposable in this.\u0023\u003DzBzoKzORE1dsQ)
      disposable.Dispose();
  }

  public sealed override \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzQFx6sbo\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    double _param2,
    double _param3)
  {
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) new \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.\u0023\u003DzjfphrXnI7aCc((\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D) _param1, this, _param2, _param3);
  }

  public sealed override \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzD4fw8fY\u003D(
    IBrush2D _param1,
    double _param2,
    double _param3,
    double _param4 = 0.0)
  {
    \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.\u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D rnsui8dDaHhmSxiNgA = new \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.\u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D(_param1, this, _param2, _param3);
    rnsui8dDaHhmSxiNgA.\u0023\u003DzVNMIwZjSvQYYYca2Lg\u003D\u003D(_param4);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) rnsui8dDaHhmSxiNgA;
  }

  private sealed class SomeClass398
  {
    public double \u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D;
  }

  public sealed class \u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D : 
    IDisposable,
    \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024
  {
    
    private IBrush2D \u0023\u003DzPbF2kpY\u003D;
    
    private readonly \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H \u0023\u003DzVxwXLcXPtvCC;
    
    private List<int> \u0023\u003DzYw05nwk\u003D = new List<int>();
    
    private double \u0023\u003DzxQYZMi0S4xrwAUmlOi_ANoo\u003D;

    public \u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D(
      IBrush2D _param1,
      \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H _param2,
      double _param3,
      double _param4)
    {
      this.\u0023\u003DzVxwXLcXPtvCC = _param2;
      this.\u0023\u003Dz7ZSU06M\u003D((IPathColor) _param1, _param3, _param4);
    }

    public double \u0023\u003DzIALY0EIJmzAYFffZZA\u003D\u003D()
    {
      return this.\u0023\u003DzxQYZMi0S4xrwAUmlOi_ANoo\u003D;
    }

    public void \u0023\u003DzVNMIwZjSvQYYYca2Lg\u003D\u003D(double _param1)
    {
      this.\u0023\u003DzxQYZMi0S4xrwAUmlOi_ANoo\u003D = _param1;
    }

    public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
      IPathColor _param1,
      double _param2,
      double _param3)
    {
      this.\u0023\u003DzPbF2kpY\u003D = (IBrush2D) _param1;
      this.\u0023\u003DzYw05nwk\u003D.Add((int) _param2.\u0023\u003DzcYUW_6FX9t5L());
      this.\u0023\u003DzYw05nwk\u003D.Add((int) _param3.\u0023\u003DzcYUW_6FX9t5L());
      return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
    }

    public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
      double _param1,
      double _param2)
    {
      this.\u0023\u003DzYw05nwk\u003D.Add((int) _param1.\u0023\u003DzcYUW_6FX9t5L());
      this.\u0023\u003DzYw05nwk\u003D.Add((int) _param2.\u0023\u003DzcYUW_6FX9t5L());
      return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
    }

    public void \u0023\u003DzBNsE20w\u003D()
    {
      \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.\u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D k0hz6MwLrPm7JfgVw01g = new \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.\u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D();
      k0hz6MwLrPm7JfgVw01g._variableSome3535 = this;
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 zEzdVavx1gj20 = this.\u0023\u003DzPbF2kpY\u003D.\u0023\u003DzZTHbSX1_i1\u0024W() ? (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0 : (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6;
      k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz32bgjTM\u003D = this.\u0023\u003DzPbF2kpY\u003D as \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D;
      if (k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz32bgjTM\u003D != null)
      {
        k0hz6MwLrPm7JfgVw01g.\u0023\u003Dzjzt0LxM\u003D = k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzCQL\u0024Quea_QT8(this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003Dz8DEW4l1E337F());
        this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003Dz_I15ZX7u91\u0024T(this.\u0023\u003DzYw05nwk\u003D.ToArray(), new Func<int, int, int>(k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz8NxWWFEIxVja), zEzdVavx1gj20);
      }
      else
        this.\u0023\u003DzVxwXLcXPtvCC.\u0023\u003DzNhq58rfMvbBc.\u0023\u003DzZin35e8ltnFe().\u0023\u003Dz_I15ZX7u91\u0024T(this.\u0023\u003DzYw05nwk\u003D.ToArray(), this.\u0023\u003DzPbF2kpY\u003D.ColorCode, zEzdVavx1gj20);
    }

    void IDisposable.Dispose()
    {
      this.\u0023\u003DzBNsE20w\u003D();
    }

    private sealed class \u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D
    {
      public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D \u0023\u003Dz32bgjTM\u003D;
      public \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.\u0023\u003DzRNsui8dDaHHMSxiNgA\u003D\u003D _variableSome3535;
      public int[] \u0023\u003Dzjzt0LxM\u003D;

      public int \u0023\u003Dz8NxWWFEIxVja(int _param1, int _param2)
      {
        return this.\u0023\u003Dzjzt0LxM\u003D[this.\u0023\u003Dz32bgjTM\u003D.\u0023\u003Dz3JSOgExMLPL4MzHkFlni7xSDFMvwvbrIEQ\u003D\u003D(_param1, _param2, this._variableSome3535.\u0023\u003DzIALY0EIJmzAYFffZZA\u003D\u003D())];
      }
    }
  }

  private sealed class SomeSealedClassAgain032
  {
    public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D \u0023\u003Dz32bgjTM\u003D;
    public int[] \u0023\u003Dzjzt0LxM\u003D;
    public \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H.SomeClass398 \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

    public int \u0023\u003Dzs3XZaL28BMmdJ3BwPQ\u003D\u003D(int _param1, int _param2)
    {
      return this.\u0023\u003Dzjzt0LxM\u003D[this.\u0023\u003Dz32bgjTM\u003D.\u0023\u003Dz3JSOgExMLPL4MzHkFlni7xSDFMvwvbrIEQ\u003D\u003D(_param1, _param2, this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D)];
    }
  }

  private sealed class SomeClass6409
  {
    public Rect \u0023\u003Dzr7d9o3c_cuwg;
    public double \u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D;
    public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D \u0023\u003Dz32bgjTM\u003D;
    public int[] \u0023\u003Dzjzt0LxM\u003D;

    public int \u0023\u003DzAGjD8bO2vEL6bBEhwQ\u003D\u003D(int _param1, int _param2)
    {
      return this.\u0023\u003Dzjzt0LxM\u003D[this.\u0023\u003Dz32bgjTM\u003D.\u0023\u003Dz\u0024xczSuty0EvqUf4ZL1LMev_qjFEtd6bSBQ\u003D\u003D(_param1, _param2, this.\u0023\u003Dzr7d9o3c_cuwg, this.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D)];
    }
  }

  private sealed class \u0023\u003DzjfphrXnI7aCc : 
    IDisposable,
    \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024
  {
    
    private readonly \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H \u0023\u003DzrZyFxk8\u003D;
    
    private \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D \u0023\u003DzU\u0024ZxpXE\u003D;
    
    private double \u0023\u003DzFEDR40ugZMK3;
    
    private double \u0023\u003DzGcDWpYNQwUmC;
    
    private readonly BitmapContext \u0023\u003DzNhq58rfMvbBc;
    
    private Size \u0023\u003DzgYZhPyPIW8zq;
    
    private int \u0023\u003DzZGdF7d5Y4MV\u0024 = -1;
    
    private int \u0023\u003DzDMmnOlzph\u0024UM = -1;

    public \u0023\u003DzjfphrXnI7aCc(
      \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D _param1,
      \u0023\u003DzCp5d2Zte2oCosmmx2S7no_qZ5mg8Hgg3z7JzuvgnBlRXkxWU\u0024D_ZV6c\u0024V_0H _param2,
      double _param3,
      double _param4)
    {
      this.\u0023\u003DzrZyFxk8\u003D = _param2;
      this.\u0023\u003DzNhq58rfMvbBc = this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzNhq58rfMvbBc;
      this.\u0023\u003DzgYZhPyPIW8zq = this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003Dz8DEW4l1E337F();
      this.\u0023\u003Dz7ZSU06M\u003D((IPathColor) _param1, _param3, _param4);
    }

    public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
      IPathColor _param1,
      double _param2,
      double _param3)
    {
      this.\u0023\u003DzU\u0024ZxpXE\u003D = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_WkCZbC9tNduj5PEZdclZC_JbnzvCt4g3do\u003D) _param1;
      this.\u0023\u003DzFEDR40ugZMK3 = _param2;
      this.\u0023\u003DzGcDWpYNQwUmC = _param3;
      return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
    }

    public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
      double _param1,
      double _param2)
    {
      if (!this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzBZpmQRzEu_oDG0nIEA\u003D\u003D())
      {
        this.\u0023\u003DzSY9FyJkLO9on(_param1, _param2);
        this.\u0023\u003DzFEDR40ugZMK3 = _param1;
        this.\u0023\u003DzGcDWpYNQwUmC = _param2;
        return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
      }
      Point point1 = new Point(this.\u0023\u003DzFEDR40ugZMK3, this.\u0023\u003DzGcDWpYNQwUmC);
      Point point2 = new Point(_param1, _param2);
      \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz3XPLnJc\u003D(ref point1, ref point2, this.\u0023\u003DzgYZhPyPIW8zq);
      \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D ptilrAzSxiAspzkU3Q = this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003Dz7KDv5k43c1Mo();
      ptilrAzSxiAspzkU3Q.Reset(point1, point2, this.\u0023\u003DzgYZhPyPIW8zq, (\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024) this.\u0023\u003DzU\u0024ZxpXE\u003D);
      while (ptilrAzSxiAspzkU3Q.MoveNext())
      {
        \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5REq_FH1BSAP6ZMnJks\u003D current = ptilrAzSxiAspzkU3Q.Current;
        this.\u0023\u003DzBNsE20w\u003D();
        this.\u0023\u003Dz7ZSU06M\u003D((IPathColor) this.\u0023\u003DzU\u0024ZxpXE\u003D, current.\u0023\u003DzunK60DE\u003D, current.\u0023\u003DzAcftkI4\u003D);
        this.\u0023\u003DzSY9FyJkLO9on(current.\u0023\u003Dz9xgeQi0\u003D, current.\u0023\u003DzZ0gmED0\u003D);
      }
      this.\u0023\u003DzFEDR40ugZMK3 = _param1;
      this.\u0023\u003DzGcDWpYNQwUmC = _param2;
      return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
    }

    private void \u0023\u003DzSY9FyJkLO9on(double _param1, double _param2)
    {
      if ((double) this.\u0023\u003DzU\u0024ZxpXE\u003D.StrokeThickness > 1.0)
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzzWEiifkWFV4ENgL9xQ\u003D\u003D(this.\u0023\u003DzNhq58rfMvbBc, (int) this.\u0023\u003DzgYZhPyPIW8zq.Width, (int) this.\u0023\u003DzgYZhPyPIW8zq.Height, (int) this.\u0023\u003DzFEDR40ugZMK3.\u0023\u003DzcYUW_6FX9t5L(), (int) this.\u0023\u003DzGcDWpYNQwUmC.\u0023\u003DzcYUW_6FX9t5L(), (int) _param1.\u0023\u003DzcYUW_6FX9t5L(), (int) _param2.\u0023\u003DzcYUW_6FX9t5L(), this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzvrdHPuKzjDX2());
      else if (this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D())
      {
        int num1 = (int) this.\u0023\u003DzFEDR40ugZMK3.\u0023\u003DzcYUW_6FX9t5L();
        int num2 = (int) this.\u0023\u003DzGcDWpYNQwUmC.\u0023\u003DzcYUW_6FX9t5L();
        int num3 = (int) _param1.\u0023\u003DzcYUW_6FX9t5L();
        int num4 = (int) _param2.\u0023\u003DzcYUW_6FX9t5L();
        bool flag = num1 == this.\u0023\u003DzZGdF7d5Y4MV\u0024 && num2 == this.\u0023\u003DzDMmnOlzph\u0024UM;
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DztpcxZUcHpycq(this.\u0023\u003DzNhq58rfMvbBc, (int) this.\u0023\u003DzgYZhPyPIW8zq.Width, (int) this.\u0023\u003DzgYZhPyPIW8zq.Height, num1, num2, num3, num4, this.\u0023\u003DzU\u0024ZxpXE\u003D.ColorCode, flag);
        this.\u0023\u003DzZGdF7d5Y4MV\u0024 = num3;
        this.\u0023\u003DzDMmnOlzph\u0024UM = num4;
      }
      else
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(this.\u0023\u003DzNhq58rfMvbBc, (int) this.\u0023\u003DzgYZhPyPIW8zq.Width, (int) this.\u0023\u003DzgYZhPyPIW8zq.Height, (int) this.\u0023\u003DzFEDR40ugZMK3.\u0023\u003DzcYUW_6FX9t5L(), (int) this.\u0023\u003DzGcDWpYNQwUmC.\u0023\u003DzcYUW_6FX9t5L(), (int) _param1.\u0023\u003DzcYUW_6FX9t5L(), (int) _param2.\u0023\u003DzcYUW_6FX9t5L(), this.\u0023\u003DzU\u0024ZxpXE\u003D.ColorCode);
      this.\u0023\u003DzFEDR40ugZMK3 = _param1;
      this.\u0023\u003DzGcDWpYNQwUmC = _param2;
    }

    public void \u0023\u003DzBNsE20w\u003D()
    {
    }

    void IDisposable.Dispose()
    {
      this.\u0023\u003DzBNsE20w\u003D();
    }
  }
}
