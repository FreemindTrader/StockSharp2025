// Decompiled with JetBrains decompiler
// Type: #=zBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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
public sealed class \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x : 
  \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D
{
  
  private readonly WriteableBitmap \u0023\u003DzpnLYJ1Q\u003D;
  
  private readonly uint[] \u0023\u003DzcNkyaHI7GTSW;
  
  protected readonly Graphics2D \u0023\u003DzeUisW3maAY9U;
  
  private readonly Image \u0023\u003Dz2TNhyDg\u003D;
  
  private readonly \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D \u0023\u003DzK3L_6jB1hKFR;
  
  private readonly \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOa9PbMTlFJUwDxIT7iKoZ2Lr \u0023\u003Dzdp7hSzeBLlAo = new \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOa9PbMTlFJUwDxIT7iKoZ2Lr();
  
  private readonly List<IDisposable> \u0023\u003DzBzoKzORE1dsQ = new List<IDisposable>();
  
  protected readonly Size \u0023\u003DzgYZhPyPIW8zq;
  
  private object myLock = new object();

  public \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x(
    Image _param1,
    WriteableBitmap _param2,
    uint[] _param3,
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D _param4,
    Graphics2D _param5,
    \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D _param6)
    : base(_param6)
  {
    if (_param6 == null)
      throw new ArgumentNullException();
    this.\u0023\u003DzgYZhPyPIW8zq = new Size((double) _param2.PixelWidth, (double) _param2.PixelHeight);
    this.\u0023\u003Dz2TNhyDg\u003D = _param1;
    this.\u0023\u003DzpnLYJ1Q\u003D = _param2;
    this.\u0023\u003DzcNkyaHI7GTSW = _param3;
    this.\u0023\u003DzK3L_6jB1hKFR = _param4;
    this.\u0023\u003DzeUisW3maAY9U = _param5;
    this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
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

  public override unsafe void Dispose()
  {
    if (this.\u0023\u003DzgYZhPyPIW8zq.Width == 0.0 || this.\u0023\u003DzgYZhPyPIW8zq.Height == 0.0)
      return;
    this.\u0023\u003DzpnLYJ1Q\u003D.Lock();
    fixed (byte* numPtr = &this.\u0023\u003DzK3L_6jB1hKFR.\u0023\u003Dz9b1_JhA\u003D()[0])
    {
      byte* pointer = (byte*) this.\u0023\u003DzpnLYJ1Q\u003D.BackBuffer.ToPointer();
      Size zgYzhPyPiW8zq = this.\u0023\u003DzgYZhPyPIW8zq;
      double width = zgYzhPyPiW8zq.Width;
      zgYzhPyPiW8zq = this.\u0023\u003DzgYZhPyPIW8zq;
      double height = zgYzhPyPiW8zq.Height;
      int num = (int) (width * height * 4.0);
      \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.\u0023\u003Dz8YbqsoQ7EyhA(numPtr, 0, pointer, 0, num);
    }
    this.\u0023\u003DzpnLYJ1Q\u003D.AddDirtyRect(new Int32Rect(0, 0, this.\u0023\u003DzpnLYJ1Q\u003D.PixelWidth, this.\u0023\u003DzpnLYJ1Q\u003D.PixelHeight));
    this.\u0023\u003DzpnLYJ1Q\u003D.Unlock();
    if (this.\u0023\u003DzpnLYJ1Q\u003D != this.\u0023\u003Dz2TNhyDg\u003D.Source)
      this.\u0023\u003Dz2TNhyDg\u003D.Source = (ImageSource) this.\u0023\u003DzpnLYJ1Q\u003D;
    foreach (IDisposable disposable in this.\u0023\u003DzBzoKzORE1dsQ)
      disposable.Dispose();
  }

  public override IBrush2D \u0023\u003Dze8WyDhI\u003D(
    Color _param1,
    double _param2 = 1.0,
    bool? _param3 = null)
  {
    return (IBrush2D) new \u0023\u003DzMLvZWaqDqEKovfY1GVv1jDvRHJm4zN8x0ttFQf_UGKPMTTL3MdOFwfA\u003D(_param1, true, _param2);
  }

  public override IBrush2D \u0023\u003Dze8WyDhI\u003D(
    Brush _param1,
    double _param2 = 1.0,
    \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4 _param3 = \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen)
  {
    if (_param1 == null)
      return this.\u0023\u003Dze8WyDhI\u003D(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0), 1.0, new bool?());
    return _param1 is SolidColorBrush ? this.\u0023\u003Dze8WyDhI\u003D(((SolidColorBrush) _param1).Color, _param2, new bool?(true)) : (IBrush2D) new \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D(_param1, _param3, this.\u0023\u003DzNFOu7BeFZYda());
  }

  public override \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzL3In9ls\u003D(
    Color _param1,
    bool _param2,
    float _param3,
    double _param4,
    double[] _param5 = null,
    PenLineCap _param6 = PenLineCap.Round)
  {
    return (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J) new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D(_param1, _param3, _param6, _param2, _param4, _param5);
  }

  public override ISprite2D \u0023\u003DzC1WFQPaV7rDp(
    FrameworkElement _param1)
  {
    return (ISprite2D) new \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D(this.\u0023\u003DzNFOu7BeFZYda().\u0023\u003DzuAOGltcBIMXJ(_param1));
  }

  public override void Clear()
  {
    int num1 = 0;
    while (true)
    {
      double num2 = (double) num1;
      Size zgYzhPyPiW8zq = this.\u0023\u003DzgYZhPyPIW8zq;
      double height = zgYzhPyPiW8zq.Height;
      if (num2 < height)
      {
        uint[] zcNkyaHi7Gtsw = this.\u0023\u003DzcNkyaHI7GTSW;
        byte[] dst = this.\u0023\u003DzK3L_6jB1hKFR.\u0023\u003Dz9b1_JhA\u003D();
        double num3 = (double) (4 * num1);
        zgYzhPyPiW8zq = this.\u0023\u003DzgYZhPyPIW8zq;
        double width = zgYzhPyPiW8zq.Width;
        int dstOffset = (int) (num3 * width);
        int count = 4 * this.\u0023\u003DzcNkyaHI7GTSW.Length;
        Buffer.BlockCopy((Array) zcNkyaHi7Gtsw, 0, (Array) dst, dstOffset, count);
        ++num1;
      }
      else
        break;
    }
  }

  public override void \u0023\u003DzVRUUvzhAr5SR(
    IBrush2D _param1,
    Point _param2,
    Point _param3,
    double _param4 = 0.0)
  {
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.SomeClass6409();
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
    this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
    Rect rect = new Rect(new Point(_param2.X, _param2.Y), new Point(_param3.X, _param3.Y));
    RoundedRect eoCvYtfPe7CbzyQzw = new RoundedRect(rect.Left, rect.Bottom, rect.Right, rect.Top, 0.0);
    this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) eoCvYtfPe7CbzyQzw);
    if (_param1 is \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D)
    {
      v4vdZv8GtEzAmB0rzFq.\u0023\u003Dzr7d9o3c_cuwg = new Rect(_param2, _param3);
      v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz32bgjTM\u003D = (\u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D) _param1;
      v4vdZv8GtEzAmB0rzFq.\u0023\u003DzVIwEApk\u003D = v4vdZv8GtEzAmB0rzFq.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzlOSbHnJKdduh(this.\u0023\u003Dz8DEW4l1E337F());
      new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), new Func<int, int, RGBA_Bytes>(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzAGjD8bO2vEL6bBEhwQ\u003D\u003D));
    }
    else
      new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003DzpRjqnnegg8cM(_param1.Color));
  }

  public override void \u0023\u003DzZC71OkxY_wdX(
    IBrush2D _param1,
    IEnumerable<Tuple<Point, Point>> _param2,
    bool _param3 = false,
    double _param4 = 0.0)
  {
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.SomeClass398 jq9Llz3ahZ2LrQl4 = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.SomeClass398();
    jq9Llz3ahZ2LrQl4.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D = _param4;
    foreach (IEnumerable<Tuple<Point, Point>> tuples in _param2.\u0023\u003Dz4tVt5y5Hyap1kzcfrQ\u003D\u003D())
    {
      PathStorage os3xfyzYzSge7gXjQq7Cww = new PathStorage();
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
      Point[] array = this.\u0023\u003Dz8zZ1iXDzEMaO(tuples).ToArray<Point>();
      if (array.Length < 2)
        break;
      os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzfRDRUq8\u003D(array[0].X, array[0].Y);
      for (int index = 1; index < array.Length; ++index)
        os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzQVwIFA0\u003D(array[index].X, array[index].Y);
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) os3xfyzYzSge7gXjQq7Cww);
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
      if (_param1 is \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D)
      {
        \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzckhZYWd3zPHu0dS2ZQhuzuE\u003D wd3zPhu0dS2ZqhuzuE = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzckhZYWd3zPHu0dS2ZQhuzuE\u003D()
        {
          \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = jq9Llz3ahZ2LrQl4,
          \u0023\u003Dz32bgjTM\u003D = (\u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D) _param1
        };
        wd3zPhu0dS2ZqhuzuE.\u0023\u003DzVIwEApk\u003D = wd3zPhu0dS2ZqhuzuE.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzlOSbHnJKdduh(this.\u0023\u003Dz8DEW4l1E337F());
        new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), new Func<int, int, RGBA_Bytes>(wd3zPhu0dS2ZqhuzuE.\u0023\u003Dzs3XZaL28BMmdJ3BwPQ\u003D\u003D));
      }
      else
        new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003DzpRjqnnegg8cM(_param1.Color));
    }
  }

  public void \u0023\u003DzcwowdvolC4hP(
    Point _param1,
    Point _param2,
    Point _param3,
    Color _param4)
  {
    PathStorage os3xfyzYzSge7gXjQq7Cww = new PathStorage();
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzfRDRUq8\u003D(_param1.X, _param1.Y);
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzQVwIFA0\u003D(_param2.X, _param2.Y);
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzQVwIFA0\u003D(_param3.X, _param3.Y);
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003Dznqbixy_A1don();
    this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) os3xfyzYzSge7gXjQq7Cww);
    new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003DzpRjqnnegg8cM(_param4));
  }

  public void \u0023\u003DzTNCC1vWt\u0024o04(
    Point _param1,
    Point _param2,
    Point _param3,
    float _param4,
    Color _param5)
  {
    double num = (double) _param4;
    \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D gd2Iv5d6o6Os2AavA = new \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D(num, (IGammaFunction) new \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKi7jhHLu5DpbE0xFGvTCVFPXqO9nkEDeJATaeiDXzhoFhnp3Glw\u003D());
    \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D fs9LlNatJdmwElzgA = new \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D((\u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerRnFnKJZv3yJKLQRpJ) new \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl22T3I94knCn3cCNsJ0Q(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), gd2Iv5d6o6Os2AavA));
    fs9LlNatJdmwElzgA.\u0023\u003Dz5ZnLRPWs3dst(num > 2.0 ? (\u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D.\u0023\u003DzjEBybwlTsZD_N0TmcdXBQuo\u003D) 2 : (\u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D.\u0023\u003DzjEBybwlTsZD_N0TmcdXBQuo\u003D) 0);
    fs9LlNatJdmwElzgA.\u0023\u003Dzu7HB9HlV3JM70ktODA\u003D\u003D(num > 2.0);
    PathStorage os3xfyzYzSge7gXjQq7Cww = new PathStorage();
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzfRDRUq8\u003D(_param1.X, _param1.Y);
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzQVwIFA0\u003D(_param2.X, _param2.Y);
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzQVwIFA0\u003D(_param3.X, _param3.Y);
    os3xfyzYzSge7gXjQq7Cww.\u0023\u003DzQVwIFA0\u003D(_param1.X, _param1.Y);
    RGBA_Bytes[] nwsEwePinXgsJj4QArray = new RGBA_Bytes[1]
    {
      this.\u0023\u003DzpRjqnnegg8cM(_param5)
    };
    int[] numArray = new int[1];
    fs9LlNatJdmwElzgA.\u0023\u003DzafR2X_lhWKg9((IVertexSource) os3xfyzYzSge7gXjQq7Cww, nwsEwePinXgsJj4QArray, numArray, 1);
  }

  public override void \u0023\u003DzX6V3YcdlNDO2(IDisposable _param1)
  {
    if (_param1 == null)
      return;
    this.\u0023\u003DzBzoKzORE1dsQ.Add(_param1);
  }

  private RGBA_Bytes \u0023\u003DzpRjqnnegg8cM(
    Color _param1)
  {
    return new RGBA_Bytes((int) _param1.R, (int) _param1.G, (int) _param1.B, (int) _param1.A);
  }

  private int \u0023\u003Dzpm7JaBRuMN4M(ref int _param1, ref double _param2)
  {
    int num = 0;
    if (_param1 < 0 && (double) _param1 + _param2 > 0.0)
    {
      num = -_param1;
      _param2 -= (double) num;
      _param1 += num;
    }
    return num;
  }

  public override void \u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(
    ISprite2D _param1,
    Rect _param2,
    Point _param3)
  {
    if (!(_param1 is \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D zp5Hp06qfOdZ0uWlg))
      throw new ArgumentException($"Input Sprite must be of type {typeof (\u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D)}");
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D ppsbKthY7Nkewpng = ((\u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GFfexjP4JI2sgmYygvp7gvAFg\u003D\u003D) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf()).\u0023\u003DzJFw\u0024Meew18NY() as \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D;
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0IIMw_vgUHCtHi5cN88nyND2bXVqyA2T5fC n88nyNd2bXvqyA2T5fC = new \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0IIMw_vgUHCtHi5cN88nyND2bXVqyA2T5fC();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D bybbzwjpmnVrSkcrBieQ3Srw = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D();
    byte[] numArray1 = zp5Hp06qfOdZ0uWlg.\u0023\u003Dz9b1_JhA\u003D();
    byte[] numArray2 = ppsbKthY7Nkewpng.\u0023\u003Dz9b1_JhA\u003D();
    int y = (int) _param3.Y;
    int x = (int) _param3.X;
    double width = (double) zp5Hp06qfOdZ0uWlg.Width;
    double height = (double) zp5Hp06qfOdZ0uWlg.Height;
    int num1 = this.\u0023\u003Dzpm7JaBRuMN4M(ref x, ref width);
    int num2 = this.\u0023\u003Dzpm7JaBRuMN4M(ref y, ref height);
    if (y < 0 || x < 0)
      return;
    for (int index1 = 0; (double) index1 < height && index1 + y < ppsbKthY7Nkewpng.Height && x < ppsbKthY7Nkewpng.Width; ++index1)
    {
      int num3 = zp5Hp06qfOdZ0uWlg.\u0023\u003DzHlHGfKJZNJsq(num1, index1 + num2);
      int num4 = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(x, index1 + y);
      for (int index2 = 0; (double) index2 < width && index2 + x < ppsbKthY7Nkewpng.Width; ++index2)
      {
        switch (numArray1[num3 + 3])
        {
          case 0:
            num3 += 4;
            num4 += 4;
            break;
          case byte.MaxValue:
            byte[] numArray3 = numArray2;
            int index3 = num4;
            int num5 = index3 + 1;
            byte[] numArray4 = numArray1;
            int index4 = num3;
            int num6 = index4 + 1;
            int num7 = (int) numArray4[index4];
            numArray3[index3] = (byte) num7;
            byte[] numArray5 = numArray2;
            int index5 = num5;
            int num8 = index5 + 1;
            byte[] numArray6 = numArray1;
            int index6 = num6;
            int num9 = index6 + 1;
            int num10 = (int) numArray6[index6];
            numArray5[index5] = (byte) num10;
            byte[] numArray7 = numArray2;
            int index7 = num8;
            int num11 = index7 + 1;
            byte[] numArray8 = numArray1;
            int index8 = num9;
            int num12 = index8 + 1;
            int num13 = (int) numArray8[index8];
            numArray7[index7] = (byte) num13;
            byte[] numArray9 = numArray2;
            int index9 = num11;
            num4 = index9 + 1;
            byte[] numArray10 = numArray1;
            int index10 = num12;
            num3 = index10 + 1;
            int num14 = (int) numArray10[index10];
            numArray9[index9] = (byte) num14;
            break;
          default:
            RGBA_Bytes nwsEwePinXgsJj4Q = n88nyNd2bXvqyA2T5fC.\u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(numArray1, num3);
            int num15;
            byte[] numArray11 = ppsbKthY7Nkewpng.\u0023\u003DznPLKTp_rfCU9(x + index2, index1 + y, out num15);
            bybbzwjpmnVrSkcrBieQ3Srw.\u0023\u003Dz1sAbEWOIYGyA(numArray11, num15, nwsEwePinXgsJj4Q);
            num3 += 4;
            num4 += 4;
            break;
        }
      }
    }
  }

  public void \u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(
    Rect _param1,
    ISprite2D _param2)
  {
    if (!(_param2 is \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D zp5Hp06qfOdZ0uWlg))
      throw new ArgumentException($"Input Sprite must be of type {typeof (\u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl2000aB2dZP5Hp06qfODZ0uWlg\u003D\u003D)}");
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D ppsbKthY7Nkewpng = ((\u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GFfexjP4JI2sgmYygvp7gvAFg\u003D\u003D) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf()).\u0023\u003DzJFw\u0024Meew18NY() as \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D;
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0IIMw_vgUHCtHi5cN88nyND2bXVqyA2T5fC n88nyNd2bXvqyA2T5fC = new \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0IIMw_vgUHCtHi5cN88nyND2bXVqyA2T5fC();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D bybbzwjpmnVrSkcrBieQ3Srw = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh4FbS\u00248Pz4j346KYR6ydwQrnpyPIo5wTcylJitBybbzwjpmnVrSkcrBIeQ3Srw\u003D\u003D();
    byte[] numArray1 = zp5Hp06qfOdZ0uWlg.\u0023\u003Dz9b1_JhA\u003D();
    byte[] numArray2 = ppsbKthY7Nkewpng.\u0023\u003Dz9b1_JhA\u003D();
    double width1 = (double) zp5Hp06qfOdZ0uWlg.Width;
    double height1 = (double) zp5Hp06qfOdZ0uWlg.Height;
    int left = (int) _param1.Left;
    int top = (int) _param1.Top;
    int width2 = (int) _param1.Width;
    int height2 = (int) _param1.Height;
    for (int index1 = 0; index1 < width2; ++index1)
    {
      for (int index2 = 0; index2 < height2; ++index2)
      {
        if (index1 + left >= 0 && index1 + left < ppsbKthY7Nkewpng.Width && index2 + top >= 0 && index2 + top < ppsbKthY7Nkewpng.Height)
        {
          int num1 = (int) ((double) index1 / (double) width2 * width1);
          int num2 = (int) ((double) index2 / (double) height2 * height1);
          int num3 = zp5Hp06qfOdZ0uWlg.\u0023\u003DzHlHGfKJZNJsq(num1, num2);
          switch (numArray1[num3 + 3])
          {
            case 0:
              continue;
            case byte.MaxValue:
              int num4 = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(index1 + left, index2 + top);
              byte[] numArray3 = numArray2;
              int index3 = num4;
              int num5 = index3 + 1;
              byte[] numArray4 = numArray1;
              int index4 = num3;
              int num6 = index4 + 1;
              int num7 = (int) numArray4[index4];
              numArray3[index3] = (byte) num7;
              byte[] numArray5 = numArray2;
              int index5 = num5;
              int num8 = index5 + 1;
              byte[] numArray6 = numArray1;
              int index6 = num6;
              int num9 = index6 + 1;
              int num10 = (int) numArray6[index6];
              numArray5[index5] = (byte) num10;
              byte[] numArray7 = numArray2;
              int index7 = num8;
              int num11 = index7 + 1;
              byte[] numArray8 = numArray1;
              int index8 = num9;
              int num12 = index8 + 1;
              int num13 = (int) numArray8[index8];
              numArray7[index7] = (byte) num13;
              byte[] numArray9 = numArray2;
              int index9 = num11;
              int num14 = index9 + 1;
              byte[] numArray10 = numArray1;
              int index10 = num12;
              int num15 = index10 + 1;
              int num16 = (int) numArray10[index10];
              numArray9[index9] = (byte) num16;
              continue;
            default:
              RGBA_Bytes nwsEwePinXgsJj4Q = n88nyNd2bXvqyA2T5fC.\u0023\u003Dzk1hQXoI0sJ98Ijd3VA\u003D\u003D(numArray1, num3);
              int num17;
              byte[] numArray11 = ppsbKthY7Nkewpng.\u0023\u003DznPLKTp_rfCU9(index1 + left, index2 + top, out num17);
              bybbzwjpmnVrSkcrBieQ3Srw.\u0023\u003Dz1sAbEWOIYGyA(numArray11, num17, nwsEwePinXgsJj4Q);
              continue;
          }
        }
      }
    }
  }

  public override void \u0023\u003DzsdRDlCsxeeTGJXELSQ\u003D\u003D(
    ISprite2D _param1,
    Rect _param2,
    IEnumerable<Point> _param3)
  {
    foreach (Point point in _param3)
      this.\u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(_param1, _param2, point);
  }

  public override void \u0023\u003DzsdRDlCsxeeTGJXELSQ\u003D\u003D(
    ISprite2D _param1,
    IEnumerable<Rect> _param2)
  {
    foreach (Rect rect in _param2)
      this.\u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(rect, _param1);
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
    lock (this.myLock)
    {
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
      Ellipse xzwywvTcxLe07Gzsg = new Ellipse(_param3.X, _param3.Y, _param4 / 2.0, _param5 / 2.0);
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) xzwywvTcxLe07Gzsg);
      new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003DzpRjqnnegg8cM(_param2.Color));
      Stroke joFuIkNaGsBguUdsg = new Stroke((IVertexSource) xzwywvTcxLe07Gzsg);
      joFuIkNaGsBguUdsg.\u0023\u003Dz5ZnLRPWs3dst((\u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUoq5_\u0024aGR\u0024QRKFQpDKpzknh8Hw\u003D\u003D) 2);
      joFuIkNaGsBguUdsg.\u0023\u003DzuvXschIwRlBB((\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSjnI7PDWlDQ3n0M0rhn3l4NM\u0024crmdnk2VIcfqI_3j4E7qw\u003D\u003D) 3);
      joFuIkNaGsBguUdsg.\u0023\u003DzVzpGJCleWxzj((\u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM7s\u0024e9YkTjxwLTTUHQO7LYUFw\u003D\u003D) 0);
      joFuIkNaGsBguUdsg.\u0023\u003DzCIN619c\u003D((double) _param1.StrokeThickness);
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) joFuIkNaGsBguUdsg);
      new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003DzpRjqnnegg8cM(_param1.Color));
      this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
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
    int num1 = Math.Max(_param2, _param3);
    _param3 = Math.Min(_param2, _param3);
    _param2 = num1;
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D ppsbKthY7Nkewpng = ((\u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GFfexjP4JI2sgmYygvp7gvAFg\u003D\u003D) this.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf()).\u0023\u003DzJFw\u0024Meew18NY() as \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D;
    byte[] numArray1 = ppsbKthY7Nkewpng.\u0023\u003Dz9b1_JhA\u003D();
    int height = ppsbKthY7Nkewpng.Height;
    if (_param2 == _param3)
      return;
    for (int index1 = Math.Min(_param2, height); index1 >= _param3 && index1 >= 0; --index1)
    {
      if (index1 >= 0 && index1 < height)
      {
        int index2 = (_param2 - index1) * _param4.Count / (_param2 - _param3);
        if (_param6)
          index2 = _param4.Count - 1 - index2;
        if (index2 >= 0 && index2 < _param4.Count)
        {
          int num2 = _param4[index2];
          int index3 = ppsbKthY7Nkewpng.\u0023\u003DzHlHGfKJZNJsq(_param1, index1);
          byte num3 = (byte) ((double) (((long) num2 & 4278190080L /*0xFF000000*/) >> 24) * _param5);
          if (num3 < byte.MaxValue)
          {
            int num4 = (int) (byte) ((num2 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
            byte num5 = (byte) ((num2 & 65280) >> 8);
            byte num6 = (byte) num2;
            byte num7 = numArray1[index3 + 2];
            byte num8 = numArray1[index3 + 1];
            byte num9 = numArray1[index3];
            byte num10 = numArray1[index3 + 3];
            int num11 = (int) num3;
            int num12 = num4 * num11 / (int) byte.MaxValue + (int) num7 * (int) num10 * ((int) byte.MaxValue - (int) num3) / 65025;
            int num13 = (int) num5 * (int) num3 / (int) byte.MaxValue + (int) num8 * (int) num10 * ((int) byte.MaxValue - (int) num3) / 65025;
            int num14 = (int) num6 * (int) num3 / (int) byte.MaxValue + (int) num9 * (int) num10 * ((int) byte.MaxValue - (int) num3) / 65025;
            int num15 = (int) num3 + (int) num10 * ((int) byte.MaxValue - (int) num3) / (int) byte.MaxValue;
            numArray1[index3 + 2] = (byte) num12;
            numArray1[index3 + 1] = (byte) num13;
            numArray1[index3] = (byte) num14;
            numArray1[index3 + 3] = (byte) num15;
          }
          else
          {
            byte[] numArray2 = numArray1;
            int index4 = index3;
            int num16 = index4 + 1;
            int num17 = (int) (byte) num2;
            numArray2[index4] = (byte) num17;
            byte[] numArray3 = numArray1;
            int index5 = num16;
            int num18 = index5 + 1;
            int num19 = (int) (byte) ((num2 & 65280) >> 8);
            numArray3[index5] = (byte) num19;
            byte[] numArray4 = numArray1;
            int index6 = num18;
            int index7 = index6 + 1;
            int num20 = (int) (byte) ((num2 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
            numArray4[index6] = (byte) num20;
            numArray1[index7] = (byte) (((long) num2 & 4278190080L /*0xFF000000*/) >> 24);
          }
        }
      }
    }
  }

  public sealed override \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzQFx6sbo\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    double _param2,
    double _param3)
  {
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DziSg2yR0Zst8P((\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D) _param1, this, _param2, _param3);
  }

  public sealed override \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzD4fw8fY\u003D(
    IBrush2D _param1,
    double _param2,
    double _param3,
    double _param4 = 0.0)
  {
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzmJRlt6lBeKjN zmJrlt6lBeKjN = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzmJRlt6lBeKjN(_param1, this, _param2, _param3);
    zmJrlt6lBeKjN.\u0023\u003DzVNMIwZjSvQYYYca2Lg\u003D\u003D(_param4);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) zmJrlt6lBeKjN;
  }

  private static void \u0023\u003DzVUQzlzp_K4Dd(
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D _param0,
    byte[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    Color _param6)
  {
    if (_param5 >= _param3 || _param5 < 0 || _param4 >= _param2 || _param4 < 0)
      return;
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003Dz1sAbEWOIYGyA(_param0, _param1, _param2, _param6, _param5, _param4);
  }

  private static void \u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D _param0,
    byte[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    Color _param8)
  {
    if (_param5 < 0 && _param7 < 0 || _param5 > _param3 && _param7 > _param3)
      return;
    if (_param4 == _param6 && _param5 == _param7)
    {
      \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzVUQzlzp_K4Dd(_param0, _param1, _param2, _param3, _param4, _param5, _param8);
    }
    else
    {
      if (!\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(new Rect(0.0, 0.0, (double) _param2, (double) _param3), ref _param4, ref _param5, ref _param6, ref _param7))
        return;
      int num1 = _param6 - _param4;
      int num2 = _param7 - _param5;
      int num3 = 0;
      if (num1 < 0)
      {
        num1 = -num1;
        num3 = -1;
      }
      else if (num1 > 0)
        num3 = 1;
      int num4 = 0;
      if (num2 < 0)
      {
        num2 = -num2;
        num4 = -1;
      }
      else if (num2 > 0)
        num4 = 1;
      int num5;
      int num6;
      int num7;
      int num8;
      int num9;
      int num10;
      if (num1 > num2)
      {
        num5 = num3;
        num6 = 0;
        num7 = num3;
        num8 = num4;
        num9 = num2;
        num10 = num1;
      }
      else
      {
        num5 = 0;
        num6 = num4;
        num7 = num3;
        num8 = num4;
        num9 = num1;
        num10 = num2;
      }
      int num11 = _param4;
      int num12 = _param5;
      int num13 = num10 >> 1;
      if (num12 < _param3 && num12 >= 0 && num11 < _param2 && num11 >= 0)
        \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003Dz1sAbEWOIYGyA(_param0, _param1, _param2, _param8, num12, num11);
      for (int index = 0; index < num10; ++index)
      {
        num13 -= num9;
        if (num13 < 0)
        {
          num13 += num10;
          num11 += num7;
          num12 += num8;
        }
        else
        {
          num11 += num5;
          num12 += num6;
        }
        if (num12 < _param3 && num12 >= 0 && num11 < _param2 && num11 >= 0)
          \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003Dz1sAbEWOIYGyA(_param0, _param1, _param2, _param8, num12, num11);
      }
    }
  }

  private static void \u0023\u003Dz1sAbEWOIYGyA(
    \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D _param0,
    byte[] _param1,
    int _param2,
    Color _param3,
    int _param4,
    int _param5)
  {
    int index = (_param4 * _param2 + _param5) * 4;
    byte a = _param3.A;
    byte r = _param3.R;
    byte g = _param3.G;
    byte b = _param3.B;
    if (_param3.A == byte.MaxValue)
    {
      _param1[index + 3] = a;
      _param1[index + 2] = r;
      _param1[index + 1] = g;
      _param1[index] = b;
    }
    else
    {
      byte num1 = _param1[index + 3];
      int num2 = (int) _param1[index + 2];
      byte num3 = _param1[index + 1];
      byte num4 = _param1[index];
      int num5 = (int) num1;
      int num6 = num2 * num5 / (int) byte.MaxValue + (int) r * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
      int num7 = (int) num3 * (int) num1 / (int) byte.MaxValue + (int) g * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
      int num8 = (int) num4 * (int) num1 / (int) byte.MaxValue + (int) b * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
      int num9 = (int) num1 + (int) a * ((int) byte.MaxValue - (int) num1) / (int) byte.MaxValue;
      _param1[index + 3] = (byte) num9;
      _param1[index + 2] = (byte) num6;
      _param1[index + 1] = (byte) num7;
      _param1[index] = (byte) num8;
    }
  }

  private sealed class SomeClass398
  {
    public double \u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D;
  }

  private sealed class \u0023\u003DzYhUP8qR6UO_noDtNZA\u003D\u003D : 
    IGammaFunction
  {
    public double \u0023\u003DzoxmYZFvB84ZN(double _param1) => 1.0;
  }

  private sealed class \u0023\u003DzckhZYWd3zPHu0dS2ZQhuzuE\u003D
  {
    public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D \u0023\u003Dz32bgjTM\u003D;
    public byte[] \u0023\u003DzVIwEApk\u003D;
    public \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.SomeClass398 \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

    public RGBA_Bytes \u0023\u003Dzs3XZaL28BMmdJ3BwPQ\u003D\u003D(
      int _param1,
      int _param2)
    {
      int index = this.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzsWL8AoDV5hOplPUdrMxd1H\u0024DVzcEoR95WA\u003D\u003D(_param1, _param2, this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D);
      return new RGBA_Bytes((int) this.\u0023\u003DzVIwEApk\u003D[index + 2], (int) this.\u0023\u003DzVIwEApk\u003D[index + 1], (int) this.\u0023\u003DzVIwEApk\u003D[index], (int) this.\u0023\u003DzVIwEApk\u003D[index + 3]);
    }
  }

  private sealed class SomeClass6409
  {
    public double \u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D;
    public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D \u0023\u003Dz32bgjTM\u003D;
    public Rect \u0023\u003Dzr7d9o3c_cuwg;
    public byte[] \u0023\u003DzVIwEApk\u003D;

    public RGBA_Bytes \u0023\u003DzAGjD8bO2vEL6bBEhwQ\u003D\u003D(
      int _param1,
      int _param2)
    {
      int index = this.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzzG4VYzuERUhu4NopskLB3k1V0Pwdew0Cew\u003D\u003D(_param1, _param2, this.\u0023\u003Dzr7d9o3c_cuwg, this.\u0023\u003DzO4I1OIcddZko_wb\u00241LSFM2c\u003D);
      return new RGBA_Bytes((int) this.\u0023\u003DzVIwEApk\u003D[index + 2], (int) this.\u0023\u003DzVIwEApk\u003D[index + 1], (int) this.\u0023\u003DzVIwEApk\u003D[index], (int) this.\u0023\u003DzVIwEApk\u003D[index + 3]);
    }
  }

  public sealed class \u0023\u003DziSg2yR0Zst8P : 
    IDisposable,
    \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024
  {
    
    private readonly \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x \u0023\u003DzrZyFxk8\u003D;
    
    private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D \u0023\u003DzU\u0024ZxpXE\u003D;
    
    private double \u0023\u003DzFEDR40ugZMK3;
    
    private double \u0023\u003DzGcDWpYNQwUmC;
    
    private Size \u0023\u003DzgYZhPyPIW8zq;
    
    private readonly \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D \u0023\u003Dz1l66LxWIUC10;
    
    private bool \u0023\u003DzfhO7iL9Vfl9R;
    
    private PathStorage \u0023\u003DzC8YqVfw\u003D;
    
    private \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D \u0023\u003DzUWgGpuy5HIAmEgbqgQ\u003D\u003D;
    
    private Stroke _propertyGridEx;

    public \u0023\u003DziSg2yR0Zst8P(
      \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D _param1,
      \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x _param2,
      double _param3,
      double _param4)
    {
      this.\u0023\u003DzrZyFxk8\u003D = _param2;
      this.\u0023\u003DzgYZhPyPIW8zq = this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003Dz8DEW4l1E337F();
      this.\u0023\u003Dz1l66LxWIUC10 = ((\u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GFfexjP4JI2sgmYygvp7gvAFg\u003D\u003D) this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf()).\u0023\u003DzJFw\u0024Meew18NY() as \u0023\u003DzwAFz4a79BKMMb8uGV0S8\u0024aurvjmQtbZLb71GKZHjczV_N6A4_lPpsbKthY7Nkewpng\u003D\u003D;
      this.\u0023\u003Dz7ZSU06M\u003D((IPathColor) _param1, _param3, _param4);
    }

    public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
      IPathColor _param1,
      double _param2,
      double _param3)
    {
      this.\u0023\u003DzU\u0024ZxpXE\u003D = (\u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEey_f4R0MOzHGwaxmAbCj5xs5RE\u0024YGqvsZ4\u003D) _param1;
      this.\u0023\u003DzfhO7iL9Vfl9R = this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D() || (double) this.\u0023\u003DzU\u0024ZxpXE\u003D.StrokeThickness > 1.0;
      this.\u0023\u003DzFEDR40ugZMK3 = _param2;
      this.\u0023\u003DzGcDWpYNQwUmC = _param3;
      if (this.\u0023\u003DzfhO7iL9Vfl9R)
      {
        this.\u0023\u003DzC8YqVfw\u003D = new PathStorage();
        if (this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzBZpmQRzEu_oDG0nIEA\u003D\u003D())
        {
          \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM7s\u0024e9YkTjxwLTTUHQO7LYUFw\u003D\u003D tjxwLttuhqO7LyuFw = this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003Dz37Umq2J4Bj\u0024RNyVC\u0024g\u003D\u003D() == PenLineCap.Square ? (\u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM7s\u0024e9YkTjxwLTTUHQO7LYUFw\u003D\u003D) 1 : (\u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM7s\u0024e9YkTjxwLTTUHQO7LYUFw\u003D\u003D) 2;
          this._propertyGridEx = new Stroke((IVertexSource) this.\u0023\u003DzC8YqVfw\u003D, (double) this.\u0023\u003DzU\u0024ZxpXE\u003D.StrokeThickness);
          this._propertyGridEx.\u0023\u003Dz5ZnLRPWs3dst((\u0023\u003DzjFV3E4nzZ192p80vvoilfzf1vG0DP\u0024PINEJIKqfnUoq5_\u0024aGR\u0024QRKFQpDKpzknh8Hw\u003D\u003D) 2);
          this._propertyGridEx.\u0023\u003DzuvXschIwRlBB((\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSjnI7PDWlDQ3n0M0rhn3l4NM\u0024crmdnk2VIcfqI_3j4E7qw\u003D\u003D) 3);
          this._propertyGridEx.\u0023\u003DzVzpGJCleWxzj(tjxwLttuhqO7LyuFw);
          this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
        }
        else
        {
          IGammaFunction vmhXuogDkNvXbX5gA = !this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D() ? (IGammaFunction) new \u0023\u003DzpKvy0OA0_My0Sg27HiUJacKhC6mpEBN\u0024qqo1hAnpxUQOLKfHdunRpSWIJ35WN1K04Q\u003D\u003D(0.5) : (IGammaFunction) new \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKi7jhHLu5DpbE0xFGvTCVFPXqO9nkEDeJATaeiDXzhoFhnp3Glw\u003D();
          double strokeThickness = (double) this.\u0023\u003DzU\u0024ZxpXE\u003D.StrokeThickness;
          \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D gd2Iv5d6o6Os2AavA = new \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D(strokeThickness, vmhXuogDkNvXbX5gA);
          this.\u0023\u003DzUWgGpuy5HIAmEgbqgQ\u003D\u003D = new \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D((\u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerRnFnKJZv3yJKLQRpJ) new \u0023\u003DzPauio66DvxKtWOFEEHOV9SoX2tFlIoBYnTAx1kMIl22T3I94knCn3cCNsJ0Q(this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), gd2Iv5d6o6Os2AavA));
          bool flag = strokeThickness >= 2.0;
          this.\u0023\u003DzUWgGpuy5HIAmEgbqgQ\u003D\u003D.\u0023\u003Dz5ZnLRPWs3dst(flag ? (\u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D.\u0023\u003DzjEBybwlTsZD_N0TmcdXBQuo\u003D) 2 : (\u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7_uBH616IqY6gSsYWlIWivPoFS9LlNAtJDMWElzgA\u003D\u003D.\u0023\u003DzjEBybwlTsZD_N0TmcdXBQuo\u003D) 0);
          this.\u0023\u003DzUWgGpuy5HIAmEgbqgQ\u003D\u003D.\u0023\u003Dzu7HB9HlV3JM70ktODA\u003D\u003D(flag);
        }
        this.\u0023\u003DzC8YqVfw\u003D.\u0023\u003DzfRDRUq8\u003D(_param2, _param3);
      }
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
      if (this.\u0023\u003DzfhO7iL9Vfl9R)
        this.\u0023\u003DzC8YqVfw\u003D.\u0023\u003DzQVwIFA0\u003D(_param1, _param2);
      else
        \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(this.\u0023\u003Dz1l66LxWIUC10, this.\u0023\u003Dz1l66LxWIUC10.\u0023\u003Dz9b1_JhA\u003D(), (int) this.\u0023\u003DzgYZhPyPIW8zq.Width, (int) this.\u0023\u003DzgYZhPyPIW8zq.Height, (int) this.\u0023\u003DzFEDR40ugZMK3.\u0023\u003DzcYUW_6FX9t5L(), (int) this.\u0023\u003DzGcDWpYNQwUmC.\u0023\u003DzcYUW_6FX9t5L(), (int) _param1.\u0023\u003DzcYUW_6FX9t5L(), (int) _param2.\u0023\u003DzcYUW_6FX9t5L(), this.\u0023\u003DzU\u0024ZxpXE\u003D.Color);
      this.\u0023\u003DzFEDR40ugZMK3 = _param1;
      this.\u0023\u003DzGcDWpYNQwUmC = _param2;
    }

    public void \u0023\u003DzBNsE20w\u003D()
    {
      if (!this.\u0023\u003DzfhO7iL9Vfl9R)
        return;
      if (this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzBZpmQRzEu_oDG0nIEA\u003D\u003D())
      {
        Graphics2D zeUisW3maAy9U = this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzeUisW3maAY9U;
        zeUisW3maAy9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) this._propertyGridEx);
        if (!this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D())
          zeUisW3maAy9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzruxDfy35wG0J((IGammaFunction) new \u0023\u003DzpKvy0OA0_My0Sg27HiUJacKhC6mpEBN\u0024qqo1hAnpxUQOLKfHdunRpSWIJ35WN1K04Q\u003D\u003D(0.5));
        new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(zeUisW3maAy9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) zeUisW3maAy9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), zeUisW3maAy9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzpRjqnnegg8cM(this.\u0023\u003DzU\u0024ZxpXE\u003D.Color));
        if (this.\u0023\u003DzU\u0024ZxpXE\u003D.\u0023\u003DzUfU2e2X_KoN9mXJHMfcWKZc\u003D())
          return;
        zeUisW3maAy9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzruxDfy35wG0J((IGammaFunction) new \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKi7jhHLu5DpbE0xFGvTCVFPXqO9nkEDeJATaeiDXzhoFhnp3Glw\u003D());
      }
      else
        this.\u0023\u003DzUWgGpuy5HIAmEgbqgQ\u003D\u003D.\u0023\u003DzafR2X_lhWKg9((IVertexSource) this.\u0023\u003DzC8YqVfw\u003D, new RGBA_Bytes[1]
        {
          this.\u0023\u003DzrZyFxk8\u003D.\u0023\u003DzpRjqnnegg8cM(this.\u0023\u003DzU\u0024ZxpXE\u003D.Color)
        }, new int[1], 1);
    }

    void IDisposable.Dispose()
    {
      this.\u0023\u003DzBNsE20w\u003D();
    }
  }

  public sealed class \u0023\u003DzmJRlt6lBeKjN : 
    IDisposable,
    \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024
  {
    
    private IBrush2D \u0023\u003DzPbF2kpY\u003D;
    
    private readonly \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x \u0023\u003Dzui0AS26tu5YE;
    
    private PathStorage \u0023\u003DzC8YqVfw\u003D;
    
    private double \u0023\u003DzxQYZMi0S4xrwAUmlOi_ANoo\u003D;

    public \u0023\u003DzmJRlt6lBeKjN(
      IBrush2D _param1,
      \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x _param2,
      double _param3,
      double _param4)
    {
      this.\u0023\u003Dzui0AS26tu5YE = _param2;
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
      this.\u0023\u003DzC8YqVfw\u003D = new PathStorage();
      this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003Dzp_DWHgc\u003D();
      this.\u0023\u003DzC8YqVfw\u003D.\u0023\u003DzfRDRUq8\u003D(_param2, _param3);
      return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
    }

    public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
      double _param1,
      double _param2)
    {
      this.\u0023\u003DzC8YqVfw\u003D.\u0023\u003DzQVwIFA0\u003D(_param1, _param2);
      return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
    }

    public void \u0023\u003DzBNsE20w\u003D()
    {
      \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzmJRlt6lBeKjN.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D k0hz6MwLrPm7JfgVw01g = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzmJRlt6lBeKjN.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D();
      k0hz6MwLrPm7JfgVw01g._variableSome3535 = this;
      this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzR7pES1JXr5Q9((IVertexSource) this.\u0023\u003DzC8YqVfw\u003D);
      this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
      k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz32bgjTM\u003D = this.\u0023\u003DzPbF2kpY\u003D as \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D;
      if (k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz32bgjTM\u003D != null)
      {
        k0hz6MwLrPm7JfgVw01g.\u0023\u003DzVIwEApk\u003D = k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzlOSbHnJKdduh(this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003Dz8DEW4l1E337F());
        new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), new Func<int, int, RGBA_Bytes>(k0hz6MwLrPm7JfgVw01g.\u0023\u003Dz8NxWWFEIxVja));
      }
      else
        new \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSQOueIoti2vqJUSOrkdmZilHpP8I_E\u003D().\u0023\u003Dz6W_LCQE4E4aPriWcSDUd6wdbLe8\u0024Mmp70w\u003D\u003D(this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzZRNA7tv6M_pf(), (IRasterizer) this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D(), this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzeUisW3maAY9U.\u0023\u003DzTzTVRyr3Jkv_Tg5N2R_ARRI\u003D(), this.\u0023\u003Dzui0AS26tu5YE.\u0023\u003DzpRjqnnegg8cM(this.\u0023\u003DzPbF2kpY\u003D.Color));
    }

    void IDisposable.Dispose()
    {
      this.\u0023\u003DzBNsE20w\u003D();
    }

    private sealed class \u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D
    {
      public \u0023\u003Dz03BSxVLolBnG92GmtCJpdk\u00249dLYzyTumCw\u003D\u003D \u0023\u003Dz32bgjTM\u003D;
      public \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiaSQqWCDAaSbFLFPC8x.\u0023\u003DzmJRlt6lBeKjN _variableSome3535;
      public byte[] \u0023\u003DzVIwEApk\u003D;

      public RGBA_Bytes \u0023\u003Dz8NxWWFEIxVja(
        int _param1,
        int _param2)
      {
        int index = this.\u0023\u003Dz32bgjTM\u003D.\u0023\u003DzsWL8AoDV5hOplPUdrMxd1H\u0024DVzcEoR95WA\u003D\u003D(_param1, _param2, this._variableSome3535.\u0023\u003DzIALY0EIJmzAYFffZZA\u003D\u003D());
        return new RGBA_Bytes((int) this.\u0023\u003DzVIwEApk\u003D[index + 2], (int) this.\u0023\u003DzVIwEApk\u003D[index + 1], (int) this.\u0023\u003DzVIwEApk\u003D[index], (int) this.\u0023\u003DzVIwEApk\u003D[index + 3]);
      }
    }
  }
}
