// Decompiled with JetBrains decompiler
// Type: #=zuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc=
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

#nullable disable
internal abstract class \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D : 
  IDisposable,
  IRenderContext2D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected readonly \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D \u0023\u003Dzwa3i3hwVZeqr;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D \u0023\u003Dzvix_YyOZ9Wo4 = new \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly Dictionary<string, FontFamily> \u0023\u003DzuF0tResk6jHB = new Dictionary<string, FontFamily>();

  protected \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D(
    \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D _param1)
  {
    this.\u0023\u003Dzwa3i3hwVZeqr = _param1;
  }

  internal \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D \u0023\u003Dz7KDv5k43c1Mo()
  {
    return this.\u0023\u003Dzvix_YyOZ9Wo4;
  }

  [SpecialName]
  public abstract \u0023\u003Dzx24ajpn1eHsuu_VYWsSDOa9PbMTlFJUwDxIT7iKoZ2Lr \u0023\u003Dz7eGjoBhvKuFN();

  [SpecialName]
  public abstract Size \u0023\u003Dz8DEW4l1E337F();

  public virtual void \u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(bool _param1)
  {
  }

  public abstract IBrush2D \u0023\u003Dze8WyDhI\u003D(
    Color _param1,
    double _param2 = 1.0,
    bool? _param3 = null);

  public abstract IBrush2D \u0023\u003Dze8WyDhI\u003D(
    Brush _param1,
    double _param2 = 1.0,
    \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4 _param3 = \u0023\u003DzQN2Zes8h9tElvYmX48o49IEXwvVSyIzumkGBhIv4w4j4.PerScreen);

  public abstract \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003DzL3In9ls\u003D(
    Color _param1,
    bool _param2,
    float _param3,
    double _param4 = 1.0,
    double[] _param5 = null,
    PenLineCap _param6 = PenLineCap.Round);

  public abstract \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D \u0023\u003DzC1WFQPaV7rDp(
    FrameworkElement _param1);

  public abstract void \u0023\u003DzUf222sU\u003D();

  public abstract void \u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D _param1,
    Rect _param2,
    Point _param3);

  public abstract void \u0023\u003DzsdRDlCsxeeTGJXELSQ\u003D\u003D(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D _param1,
    Rect _param2,
    IEnumerable<Point> _param3);

  public abstract void \u0023\u003DzsdRDlCsxeeTGJXELSQ\u003D\u003D(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D _param1,
    IEnumerable<Rect> _param2);

  public abstract void \u0023\u003DzVRUUvzhAr5SR(
    IBrush2D _param1,
    Point _param2,
    Point _param3,
    double _param4 = 0.0);

  public abstract void \u0023\u003DzZC71OkxY_wdX(
    IBrush2D _param1,
    IEnumerable<Tuple<Point, Point>> _param2,
    bool _param3 = false,
    double _param4 = 0.0);

  public abstract void \u0023\u003DzIZCdW2WR6Rxw(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    IBrush2D _param2,
    Point _param3,
    double _param4,
    double _param5);

  public abstract void \u0023\u003DzoUffnI1MQnWod0s9Xg\u003D\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    IBrush2D _param2,
    IEnumerable<Point> _param3,
    double _param4,
    double _param5);

  public abstract void Dispose();

  public abstract void \u0023\u003DzX6V3YcdlNDO2(IDisposable _param1);

  public abstract void \u0023\u003DztJb5\u0024zF1SLeC(
    int _param1,
    int _param2,
    int _param3,
    IList<int> _param4,
    double _param5,
    bool _param6);

  public virtual void \u0023\u003Dz7zUbWtTKc3tA(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    Point _param2,
    Point _param3)
  {
    if (_param2.X < 0.0 && _param3.X < 0.0 || _param2.Y < 0.0 && _param3.Y < 0.0 || _param2.Y > this.\u0023\u003Dz8DEW4l1E337F().Height && _param3.Y > this.\u0023\u003Dz8DEW4l1E337F().Height || _param2.X > this.\u0023\u003Dz8DEW4l1E337F().Width && _param3.X > this.\u0023\u003Dz8DEW4l1E337F().Width)
      return;
    this.\u0023\u003Dzpd5t5heTSEu0(ref _param2, ref _param3, 1, 1);
    using (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq = this.\u0023\u003DzQFx6sbo\u003D(_param1, _param3.X, _param2.Y))
    {
      v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(_param3.X, _param3.Y);
      v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(_param2.X, _param3.Y);
      v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(_param2.X, _param2.Y);
      v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(_param3.X, _param2.Y);
    }
  }

  public virtual void \u0023\u003Dzk8_eoWQ\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    Point _param2,
    Point _param3)
  {
    using (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq = this.\u0023\u003DzQFx6sbo\u003D(_param1, _param2.X, _param2.Y))
      v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(_param3.X, _param3.Y);
  }

  public virtual void \u0023\u003DzKbzNI_AipRxe(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    IEnumerable<Point> _param2)
  {
    IEnumerator<Point> enumerator = _param2.GetEnumerator();
    enumerator.MoveNext();
    Point current1 = enumerator.Current;
    using (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq = this.\u0023\u003DzQFx6sbo\u003D(_param1, current1.X, current1.Y))
    {
      while (enumerator.MoveNext())
      {
        Point current2 = enumerator.Current;
        v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(current2.X, current2.Y);
      }
    }
  }

  public virtual void \u0023\u003Dz_I15ZX7u91\u0024T(
    IBrush2D _param1,
    IEnumerable<Point> _param2)
  {
    IEnumerator<Point> enumerator = _param2.GetEnumerator();
    enumerator.MoveNext();
    Point current1 = enumerator.Current;
    using (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 v1qkdyQymVhxLr4oDq = this.\u0023\u003DzD4fw8fY\u003D(_param1, current1.X, current1.Y))
    {
      while (enumerator.MoveNext())
      {
        Point current2 = enumerator.Current;
        v1qkdyQymVhxLr4oDq.\u0023\u003DzfRDRUq8\u003D(current2.X, current2.Y);
      }
    }
  }

  public void \u0023\u003DzABTWuusdnbm5(
    string _param1,
    float _param2,
    Color _param3,
    out float _param4,
    out float _param5,
    string _param6 = null,
    FontWeight _param7 = default (FontWeight))
  {
    this.\u0023\u003Dz2Wbt842C_6lU(_param1, _param2, _param3, out _param4, out _param5, _param6, _param7);
  }

  public void \u0023\u003DzI6mwN\u0024I\u003D(
    string _param1,
    Rect _param2,
    AlignmentX _param3,
    AlignmentY _param4,
    Color _param5,
    float _param6,
    string _param7 = null,
    FontWeight _param8 = default (FontWeight))
  {
    float num1;
    float num2;
    IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D> squxH0BdrCzw3R4A1gs = this.\u0023\u003Dz2Wbt842C_6lU(_param1, _param6, _param5, out num1, out num2, _param7, _param8);
    if ((double) num1 > _param2.Width || (double) num2 > _param2.Height)
      return;
    Point point = \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzA\u00249lzz5I37VY(_param2, new Rect(new Size((double) num1, (double) num2)), _param3, _param4);
    double x = point.X;
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D squxH0BdrCzw3R4A1g in squxH0BdrCzw3R4A1gs)
    {
      this.\u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(squxH0BdrCzw3R4A1g, new Rect(0.0, 0.0, (double) squxH0BdrCzw3R4A1g.Width, (double) squxH0BdrCzw3R4A1g.Height), new Point(x, point.Y));
      x += (double) squxH0BdrCzw3R4A1g.Width;
    }
  }

  public void \u0023\u003DzI6mwN\u0024I\u003D(
    string _param1,
    Point _param2,
    AlignmentX _param3,
    AlignmentY _param4,
    Color _param5,
    float _param6,
    string _param7 = null,
    FontWeight _param8 = default (FontWeight))
  {
    float num1;
    float num2;
    IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D> squxH0BdrCzw3R4A1gs = this.\u0023\u003Dz2Wbt842C_6lU(_param1, _param6, _param5, out num1, out num2, _param7, _param8);
    Point point = \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzA\u00249lzz5I37VY(_param2, new Rect(new Size((double) num1, (double) num2)), _param3, _param4);
    double x = point.X;
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D squxH0BdrCzw3R4A1g in squxH0BdrCzw3R4A1gs)
    {
      this.\u0023\u003Dzjx5oA1ZnAPnihuu9VA\u003D\u003D(squxH0BdrCzw3R4A1g, new Rect(0.0, 0.0, (double) squxH0BdrCzw3R4A1g.Width, (double) squxH0BdrCzw3R4A1g.Height), new Point(x, point.Y));
      x += (double) squxH0BdrCzw3R4A1g.Width;
    }
  }

  public Size \u0023\u003DzM2zC99cVJOSN(float _param1, string _param2 = null, FontWeight _param3 = default (FontWeight))
  {
    \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D yeuvfbi2ga1Q3dva4g = new \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D();
    yeuvfbi2ga1Q3dva4g.\u0023\u003DzRRvwDu67s9Rm = this;
    yeuvfbi2ga1Q3dva4g.\u0023\u003Dzz3d39DAyibKX = _param2;
    yeuvfbi2ga1Q3dva4g.\u0023\u003DzyvmR52E\u003D = _param1;
    yeuvfbi2ga1Q3dva4g.\u0023\u003Dz_xu4jb2w_ISy = _param3;
    if (yeuvfbi2ga1Q3dva4g.\u0023\u003Dzz3d39DAyibKX == null)
      yeuvfbi2ga1Q3dva4g.\u0023\u003Dzz3d39DAyibKX = "Tahoma";
    if (yeuvfbi2ga1Q3dva4g.\u0023\u003Dz_xu4jb2w_ISy == new FontWeight())
      yeuvfbi2ga1Q3dva4g.\u0023\u003Dz_xu4jb2w_ISy = FontWeights.Regular;
    yeuvfbi2ga1Q3dva4g.\u0023\u003DzyvmR52E\u003D = yeuvfbi2ga1Q3dva4g.\u0023\u003DzyvmR52E\u003D.Round(0.5f);
    Tuple<string, float, FontWeight> key = Tuple.Create<string, float, FontWeight>(yeuvfbi2ga1Q3dva4g.\u0023\u003Dzz3d39DAyibKX, yeuvfbi2ga1Q3dva4g.\u0023\u003DzyvmR52E\u003D, yeuvfbi2ga1Q3dva4g.\u0023\u003Dz_xu4jb2w_ISy);
    Size size;
    if (this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003Dz02RC_lOz3gDS.TryGetValue(key, out size))
      return size;
    double num1 = double.MinValue;
    double num2 = double.MinValue;
    foreach (\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D squxH0BdrCzw3R4A1g in ((IEnumerable<char>) new char[10]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9'
    }).Select<char, \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>(yeuvfbi2ga1Q3dva4g.\u0023\u003Dzon\u0024_RZacJIPJ ?? (yeuvfbi2ga1Q3dva4g.\u0023\u003Dzon\u0024_RZacJIPJ = new Func<char, \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>(yeuvfbi2ga1Q3dva4g.\u0023\u003DzFd_eHDbriD9KB6iy6g\u003D\u003D))))
    {
      if ((double) squxH0BdrCzw3R4A1g.Width > num1)
        num1 = (double) squxH0BdrCzw3R4A1g.Width;
      if ((double) squxH0BdrCzw3R4A1g.Height > num2)
        num2 = (double) squxH0BdrCzw3R4A1g.Height;
    }
    size = new Size(num1, num2);
    this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003Dz02RC_lOz3gDS[key] = size;
    return size;
  }

  private IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D> \u0023\u003Dz2Wbt842C_6lU(
    string _param1,
    float _param2,
    Color _param3,
    out float _param4,
    out float _param5,
    string _param6,
    FontWeight _param7)
  {
    \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D f6r12fDpdxPyyP9aEtuM = new \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D();
    f6r12fDpdxPyyP9aEtuM.\u0023\u003DzRRvwDu67s9Rm = this;
    f6r12fDpdxPyyP9aEtuM.\u0023\u003Dzz3d39DAyibKX = _param6;
    f6r12fDpdxPyyP9aEtuM.\u0023\u003DzyvmR52E\u003D = _param2;
    f6r12fDpdxPyyP9aEtuM.\u0023\u003Dz_xu4jb2w_ISy = _param7;
    f6r12fDpdxPyyP9aEtuM.\u0023\u003Dz9GKNZpE\u003D = _param3;
    if (f6r12fDpdxPyyP9aEtuM.\u0023\u003Dzz3d39DAyibKX == null)
      f6r12fDpdxPyyP9aEtuM.\u0023\u003Dzz3d39DAyibKX = "Tahoma";
    if (f6r12fDpdxPyyP9aEtuM.\u0023\u003Dz_xu4jb2w_ISy == new FontWeight())
      f6r12fDpdxPyyP9aEtuM.\u0023\u003Dz_xu4jb2w_ISy = FontWeights.Regular;
    f6r12fDpdxPyyP9aEtuM.\u0023\u003DzyvmR52E\u003D = f6r12fDpdxPyyP9aEtuM.\u0023\u003DzyvmR52E\u003D.Round(0.5f);
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D[] array = _param1.Select<char, \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>(new Func<char, \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>(f6r12fDpdxPyyP9aEtuM.\u0023\u003Dzt2VsFZgiJvx3sBds6JkN7sI\u003D)).ToArray<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>();
    if (array.Length == 0)
    {
      _param4 = _param5 = 0.0f;
      return (IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>) array;
    }
    _param4 = ((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>) array).Sum<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2evoJ3eFo9o\u0024LSMlgA\u003D\u003D ?? (\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2evoJ3eFo9o\u0024LSMlgA\u003D\u003D = new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D, float>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzzDoSKNsCWFOBbkjLQ9gARTDb9OQE)));
    _param5 = ((IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>) array).Max<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzdQzi_BEk7Gw\u0024M9vJMw\u003D\u003D ?? (\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzdQzi_BEk7Gw\u0024M9vJMw\u003D\u003D = new Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D, float>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzAu4dE94x3JUra9eOoSaMbA41EWq5)));
    return (IEnumerable<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D>) array;
  }

  private \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D \u0023\u003DzNDXEipsPX5MUibJptw\u003D\u003D(
    char _param1,
    string _param2,
    float _param3,
    FontWeight _param4,
    Color _param5)
  {
    \u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L e2A9Fzw6Nreq9GI2L = new \u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L();
    e2A9Fzw6Nreq9GI2L.\u0023\u003Dz8Odk1LFUZS2b(_param1);
    e2A9Fzw6Nreq9GI2L.\u0023\u003Dzt6hPoGyP_GFA(_param5);
    e2A9Fzw6Nreq9GI2L.\u0023\u003DzWkJ\u0024_LzO16MF(_param2);
    e2A9Fzw6Nreq9GI2L.\u0023\u003DzG1z2GkU3awEN(_param4);
    e2A9Fzw6Nreq9GI2L.FontSize = _param3;
    \u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L key = e2A9Fzw6Nreq9GI2L;
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D squxH0BdrCzw3R4A1g;
    if (!this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003DzrWDYXY9klZwP.TryGetValue(key, out squxH0BdrCzw3R4A1g))
    {
      TextBlock textBlock = new TextBlock();
      textBlock.Text = new string(_param1, 1);
      textBlock.Foreground = (Brush) new SolidColorBrush(_param5);
      textBlock.FontFamily = \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzmD8htMGNTWQ4(_param2);
      textBlock.FontSize = (double) _param3;
      textBlock.FontWeight = _param4;
      textBlock.Margin = new Thickness(0.0);
      squxH0BdrCzw3R4A1g = this.\u0023\u003DzC1WFQPaV7rDp((FrameworkElement) textBlock);
      this.\u0023\u003Dzwa3i3hwVZeqr.\u0023\u003DzrWDYXY9klZwP.Add(key, squxH0BdrCzw3R4A1g);
    }
    return squxH0BdrCzw3R4A1g;
  }

  public abstract \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzQFx6sbo\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1,
    double _param2,
    double _param3);

  public abstract \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzD4fw8fY\u003D(
    IBrush2D _param1,
    double _param2,
    double _param3,
    double _param4 = 0.0);

  internal static bool \u0023\u003Dz3XPLnJc\u003D(
    ref Point _param0,
    ref Point _param1,
    Size _param2)
  {
    Rect rect = new Rect(0.0, 0.0, _param2.Width, _param2.Height);
    if (_param0.\u0023\u003DzxGhbraO0gg9\u0024(_param2) && _param1.\u0023\u003DzxGhbraO0gg9\u0024(_param2))
      return true;
    double num1 = _param0.X.\u0023\u003DzcYUW_6FX9t5L();
    double num2 = _param0.Y.\u0023\u003DzcYUW_6FX9t5L();
    double num3 = _param1.X.\u0023\u003DzcYUW_6FX9t5L();
    double num4 = _param1.Y.\u0023\u003DzcYUW_6FX9t5L();
    int num5 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(rect, ref num1, ref num2, ref num3, ref num4) ? 1 : 0;
    _param0.X = num1;
    _param0.Y = num2;
    _param1.X = num3;
    _param1.Y = num4;
    return num5 != 0;
  }

  protected void \u0023\u003Dzpd5t5heTSEu0(
    ref Point _param1,
    ref Point _param2,
    int _param3,
    int _param4)
  {
    _param1 = _param1.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz8DEW4l1E337F(), _param3, _param4);
    _param2 = _param2.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz8DEW4l1E337F(), _param3, _param4);
  }

  protected void \u0023\u003Dzpd5t5heTSEu0(ref Point _param1, ref Point _param2)
  {
    _param1 = _param1.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz8DEW4l1E337F(), 0, 0);
    _param2 = _param2.\u0023\u003DzrLjTTj5p96o9(this.\u0023\u003Dz8DEW4l1E337F(), 0, 0);
  }

  protected double \u0023\u003DzZ9VdH7jzXaCITtJdlw\u003D\u003D(double _param1, bool _param2)
  {
    if (_param1 < 0.0)
      return 0.0;
    if (_param2)
    {
      double num = _param1;
      Size size = this.\u0023\u003Dz8DEW4l1E337F();
      double width = size.Width;
      if (num > width)
      {
        size = this.\u0023\u003Dz8DEW4l1E337F();
        return size.Width;
      }
    }
    else
    {
      double num = _param1;
      Size size = this.\u0023\u003Dz8DEW4l1E337F();
      double height = size.Height;
      if (num > height)
      {
        size = this.\u0023\u003Dz8DEW4l1E337F();
        return size.Height;
      }
    }
    return _param1;
  }

  protected IEnumerable<Point> \u0023\u003Dz8zZ1iXDzEMaO(
    IEnumerable<Point> _param1,
    int _param2,
    int _param3)
  {
    return \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzUIfyPrQYBsuR(_param1, this.\u0023\u003Dz8DEW4l1E337F(), _param2, _param3);
  }

  protected bool \u0023\u003DzxGhbraO0gg9\u0024(Point _param1)
  {
    if (_param1.X >= 0.0)
    {
      double x = _param1.X;
      Size size = this.\u0023\u003Dz8DEW4l1E337F();
      double width = size.Width;
      if (x <= width && _param1.Y >= 0.0)
      {
        double y = _param1.Y;
        size = this.\u0023\u003Dz8DEW4l1E337F();
        double height = size.Height;
        return y <= height;
      }
    }
    return false;
  }

  protected IEnumerable<Point> \u0023\u003Dz8zZ1iXDzEMaO(IEnumerable<Tuple<Point, Point>> _param1)
  {
    Tuple<Point, Point>[] array = _param1.ToArray<Tuple<Point, Point>>();
    return this.\u0023\u003Dz8zZ1iXDzEMaO(((IEnumerable<Tuple<Point, Point>>) array).Select<Tuple<Point, Point>, Point>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzRvDbWZIRxYgzjR1S\u0024g\u003D\u003D ?? (\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzRvDbWZIRxYgzjR1S\u0024g\u003D\u003D = new Func<Tuple<Point, Point>, Point>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzDg6mo2MEiaYNMCHzJKIJl5w\u003D))), 0, 0).Concat<Point>(this.\u0023\u003Dz8zZ1iXDzEMaO(((IEnumerable<Tuple<Point, Point>>) array).Select<Tuple<Point, Point>, Point>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyemWZ3TsoGdbt2LrKg\u003D\u003D ?? (\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyemWZ3TsoGdbt2LrKg\u003D\u003D = new Func<Tuple<Point, Point>, Point>(\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzXC7FT3ts2hCp609BBf5\u0024NGQ\u003D))), 0, 0).Reverse<Point>());
  }

  private static Point \u0023\u003DzA\u00249lzz5I37VY(
    Rect _param0,
    Rect _param1,
    AlignmentX _param2,
    AlignmentY _param3)
  {
    if (_param1.Width > _param0.Width || _param1.Height > _param0.Height)
      throw new ArgumentOutOfRangeException("innerRect");
    double num1;
    switch (_param2)
    {
      case AlignmentX.Left:
        num1 = _param0.Left;
        break;
      case AlignmentX.Right:
        num1 = _param0.Right - _param1.Width;
        break;
      default:
        num1 = _param0.Left + _param0.Width / 2.0 - _param1.Width / 2.0;
        break;
    }
    double num2;
    switch (_param3)
    {
      case AlignmentY.Top:
        num2 = _param0.Top;
        break;
      case AlignmentY.Bottom:
        num2 = _param0.Bottom - _param1.Height;
        break;
      default:
        num2 = _param0.Top + _param0.Height / 2.0 - _param1.Height / 2.0;
        break;
    }
    return new Point(num1, num2);
  }

  private static Point \u0023\u003DzA\u00249lzz5I37VY(
    Point _param0,
    Rect _param1,
    AlignmentX _param2,
    AlignmentY _param3)
  {
    double num1;
    switch (_param2)
    {
      case AlignmentX.Left:
        num1 = _param0.X;
        break;
      case AlignmentX.Right:
        num1 = _param0.X - _param1.Width;
        break;
      default:
        num1 = _param0.X - _param1.Width / 2.0;
        break;
    }
    double num2;
    switch (_param3)
    {
      case AlignmentY.Top:
        num2 = _param0.Y;
        break;
      case AlignmentY.Bottom:
        num2 = _param0.Y - _param1.Height;
        break;
      default:
        num2 = _param0.Y - _param1.Height / 2.0;
        break;
    }
    return new Point(num1, num2);
  }

  private static FontFamily \u0023\u003DzmD8htMGNTWQ4(string _param0)
  {
    FontFamily fontFamily1;
    if (\u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzuF0tResk6jHB.TryGetValue(_param0, out fontFamily1))
      return fontFamily1;
    FontFamily fontFamily2 = new FontFamily(_param0);
    \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003DzuF0tResk6jHB.Add(_param0, fontFamily2);
    return fontFamily2;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D, float> \u0023\u003Dz2evoJ3eFo9o\u0024LSMlgA\u003D\u003D;
    public static Func<\u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D, float> \u0023\u003DzdQzi_BEk7Gw\u0024M9vJMw\u003D\u003D;
    public static Func<Tuple<Point, Point>, Point> \u0023\u003DzRvDbWZIRxYgzjR1S\u0024g\u003D\u003D;
    public static Func<Tuple<Point, Point>, Point> \u0023\u003DzyemWZ3TsoGdbt2LrKg\u003D\u003D;

    internal float \u0023\u003DzzDoSKNsCWFOBbkjLQ9gARTDb9OQE(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D _param1)
    {
      return _param1.Width;
    }

    internal float \u0023\u003DzAu4dE94x3JUra9eOoSaMbA41EWq5(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D _param1)
    {
      return _param1.Height;
    }

    internal Point \u0023\u003DzDg6mo2MEiaYNMCHzJKIJl5w\u003D(Tuple<Point, Point> _param1)
    {
      return _param1.Item2;
    }

    internal Point \u0023\u003DzXC7FT3ts2hCp609BBf5\u0024NGQ\u003D(Tuple<Point, Point> _param1)
    {
      return _param1.Item1;
    }
  }

  private sealed class \u0023\u003DzOZhF6r12fDpdxPyyP9aETuM\u003D
  {
    public \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D \u0023\u003DzRRvwDu67s9Rm;
    public string \u0023\u003Dzz3d39DAyibKX;
    public float \u0023\u003DzyvmR52E\u003D;
    public FontWeight \u0023\u003Dz_xu4jb2w_ISy;
    public Color \u0023\u003Dz9GKNZpE\u003D;

    internal \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D \u0023\u003Dzt2VsFZgiJvx3sBds6JkN7sI\u003D(
      char _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzNDXEipsPX5MUibJptw\u003D\u003D(_param1, this.\u0023\u003Dzz3d39DAyibKX, this.\u0023\u003DzyvmR52E\u003D, this.\u0023\u003Dz_xu4jb2w_ISy, this.\u0023\u003Dz9GKNZpE\u003D);
    }
  }

  private sealed class \u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D
  {
    public \u0023\u003DzuDpRUa8VnAP4HjqWjzAnaJoXmBOJyAGGTX_2ALc\u003D \u0023\u003DzRRvwDu67s9Rm;
    public string \u0023\u003Dzz3d39DAyibKX;
    public float \u0023\u003DzyvmR52E\u003D;
    public FontWeight \u0023\u003Dz_xu4jb2w_ISy;
    public Func<char, \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D> \u0023\u003Dzon\u0024_RZacJIPJ;

    internal \u0023\u003DzV9O5tWduWosGLvu_87Zf5PVzyNyECV88SquxH0BDrCzw3R4A1g\u003D\u003D \u0023\u003DzFd_eHDbriD9KB6iy6g\u003D\u003D(
      char _param1)
    {
      return this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzNDXEipsPX5MUibJptw\u003D\u003D(_param1, this.\u0023\u003Dzz3d39DAyibKX, this.\u0023\u003DzyvmR52E\u003D, this.\u0023\u003Dz_xu4jb2w_ISy, Colors.White);
    }
  }
}
