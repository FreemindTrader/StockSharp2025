// Decompiled with JetBrains decompiler
// Type: #=z8B1nlAnvAhBdiQFqFRFmPTilr$AzSXiASpzkU3Q=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D : 
  IDisposable,
  IEnumerator<\u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5REq_FH1BSAP6ZMnJks\u003D>,
  IEnumerator
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzCdchccOd7WzD9XUBlw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzaH_ochogoqeB;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzD63fu__l9liL \u0023\u003DzUhBHbDcVs9O8 = new \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzD63fu__l9liL();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzprlB3QU\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dz\u0024h_yNEs\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzNkjnJzE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int \u0023\u003DzKx97DYo\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzbXeZ30JZJQXl;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Point \u0023\u003DzJEgbAKn4ZYm6;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5REq_FH1BSAP6ZMnJks\u003D \u0023\u003DzCUQ2vA0\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003Dz\u0024fZhibJjCIKj;

  internal \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D(
    Point _param1,
    Point _param2,
    Size _param3,
    \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024 _param4)
  {
    this.Reset(_param1, _param2, _param3, _param4);
  }

  internal \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D()
  {
    this.\u0023\u003DzKx97DYo\u003D = -1;
  }

  public \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5REq_FH1BSAP6ZMnJks\u003D Current
  {
    get => this.\u0023\u003DzCUQ2vA0\u003D;
  }

  public void Dispose()
  {
  }

  object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
  {
    return (object) this.\u0023\u003DzCUQ2vA0\u003D;
  }

  public bool MoveNext()
  {
    if (this.\u0023\u003DzCdchccOd7WzD9XUBlw\u003D\u003D)
    {
      if (!this.\u0023\u003DzUhBHbDcVs9O8.MoveNext())
        return false;
      \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzCW27bQGKuCyh current = this.\u0023\u003DzUhBHbDcVs9O8.Current;
      this.\u0023\u003DzCUQ2vA0\u003D = new \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5REq_FH1BSAP6ZMnJks\u003D(this.\u0023\u003DzbXeZ30JZJQXl.X + current.\u0023\u003DzCghbVqE\u003D * this.\u0023\u003Dz\u0024fZhibJjCIKj * this.\u0023\u003DzprlB3QU\u003D, this.\u0023\u003DzbXeZ30JZJQXl.Y + current.\u0023\u003DzCghbVqE\u003D * this.\u0023\u003Dz\u0024fZhibJjCIKj * this.\u0023\u003Dz\u0024h_yNEs\u003D, this.\u0023\u003DzbXeZ30JZJQXl.X + current.\u0023\u003DzBNsE20w\u003D * this.\u0023\u003Dz\u0024fZhibJjCIKj * this.\u0023\u003DzprlB3QU\u003D, this.\u0023\u003DzbXeZ30JZJQXl.Y + current.\u0023\u003DzBNsE20w\u003D * this.\u0023\u003Dz\u0024fZhibJjCIKj * this.\u0023\u003Dz\u0024h_yNEs\u003D);
      return true;
    }
    if (this.\u0023\u003DzKx97DYo\u003D >= 0)
      return false;
    ++this.\u0023\u003DzKx97DYo\u003D;
    this.\u0023\u003DzCUQ2vA0\u003D = new \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5REq_FH1BSAP6ZMnJks\u003D(this.\u0023\u003DzbXeZ30JZJQXl.X, this.\u0023\u003DzbXeZ30JZJQXl.Y, this.\u0023\u003DzJEgbAKn4ZYm6.X, this.\u0023\u003DzJEgbAKn4ZYm6.Y);
    return true;
  }

  public void Reset() => throw new NotSupportedException();

  internal void Reset(
    Point _param1,
    Point _param2,
    Size _param3,
    \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024 _param4)
  {
    this.\u0023\u003DzCdchccOd7WzD9XUBlw\u003D\u003D = _param4.\u0023\u003DzBZpmQRzEu_oDG0nIEA\u003D\u003D();
    this.\u0023\u003DzbXeZ30JZJQXl = _param1;
    this.\u0023\u003DzJEgbAKn4ZYm6 = _param2;
    Rect rect = new Rect(0.0, 0.0, _param3.Width, _param3.Height);
    if (this.\u0023\u003DzCdchccOd7WzD9XUBlw\u003D\u003D)
    {
      double x1 = _param1.X;
      double y1 = _param1.Y;
      double x2 = _param2.X;
      double y2 = _param2.Y;
      this.\u0023\u003DzaH_ochogoqeB = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(rect, ref x1, ref y1, ref x2, ref y2);
      this.\u0023\u003DzprlB3QU\u003D = x2 - x1;
      this.\u0023\u003Dz\u0024h_yNEs\u003D = y2 - y1;
      this.\u0023\u003DzNkjnJzE\u003D = Math.Sqrt(this.\u0023\u003DzprlB3QU\u003D * this.\u0023\u003DzprlB3QU\u003D + this.\u0023\u003Dz\u0024h_yNEs\u003D * this.\u0023\u003Dz\u0024h_yNEs\u003D);
      this.\u0023\u003Dz\u0024fZhibJjCIKj = 1.0 / this.\u0023\u003DzNkjnJzE\u003D;
      this.\u0023\u003DzUhBHbDcVs9O8.Reset(this.\u0023\u003DzNkjnJzE\u003D, _param4);
    }
    else
      this.\u0023\u003DzaH_ochogoqeB = true;
    this.\u0023\u003DzKx97DYo\u003D = -1;
  }

  internal struct \u0023\u003DzCW27bQGKuCyh
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly double \u0023\u003DzCghbVqE\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal readonly double \u0023\u003DzBNsE20w\u003D;

    internal \u0023\u003DzCW27bQGKuCyh(double _param1, double _param2)
    {
      this.\u0023\u003DzCghbVqE\u003D = _param1;
      this.\u0023\u003DzBNsE20w\u003D = _param2;
    }
  }

  private sealed class \u0023\u003DzD63fu__l9liL : 
    IDisposable,
    IEnumerator<\u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzCW27bQGKuCyh>,
    IEnumerator
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public double \u0023\u003DzEZ6iGHc\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024 \u0023\u003DzH8qyrxb_WyCX;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzCW27bQGKuCyh \u0023\u003DzCUQ2vA0\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003DzHJ09GLU\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzlHUzJ6AB\u0024yFV;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzV9aYAdc\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003Dz14KaPQET0IYM;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003Dzm7hBd9jYl4Gv;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool \u0023\u003DzukvY3XQ\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003DzcL29nSWtuHpMA\u0024ZFxA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private double \u0023\u003Dz59\u0024YMlUtK_c\u0024PRIHdA\u003D\u003D;

    public \u0023\u003DzD63fu__l9liL() => this.\u0023\u003DzHJ09GLU\u003D = 0;

    public \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzCW27bQGKuCyh Current
    {
      get => this.\u0023\u003DzCUQ2vA0\u003D;
    }

    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003DzCUQ2vA0\u003D;
    }

    public bool MoveNext()
    {
      switch (this.\u0023\u003DzHJ09GLU\u003D)
      {
        case 0:
          this.\u0023\u003DzHJ09GLU\u003D = -1;
          this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D = 0.0;
          this.\u0023\u003DzlHUzJ6AB\u0024yFV = this.\u0023\u003DzEZ6iGHc\u003D;
          goto label_6;
        case 1:
          this.\u0023\u003DzHJ09GLU\u003D = -1;
          break;
        case 2:
          this.\u0023\u003DzHJ09GLU\u003D = -1;
          goto default;
        default:
label_11:
          return false;
      }
label_4:
      \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024 zH8qyrxbWyCx1 = this.\u0023\u003DzH8qyrxb_WyCX;
      zH8qyrxbWyCx1.\u0023\u003Dz7jjovJyOcjl0y23idA\u003D\u003D(zH8qyrxbWyCx1.\u0023\u003Dz94d6KNkFELrvsSVOyQ\u003D\u003D() + 1);
      this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003DzHX4QluI_kG8NGHqDtYeOSVM\u003D(0.0);
      if (this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003Dz94d6KNkFELrvsSVOyQ\u003D\u003D() >= this.\u0023\u003DzH8qyrxb_WyCX.get_StrokeDashArray().Length)
        this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003Dz7jjovJyOcjl0y23idA\u003D\u003D(0);
label_6:
      this.\u0023\u003DzV9aYAdc\u003D = this.\u0023\u003DzH8qyrxb_WyCX.get_StrokeDashArray()[this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003Dz94d6KNkFELrvsSVOyQ\u003D\u003D()];
      this.\u0023\u003Dz14KaPQET0IYM = this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003DzQrg7PLXQAunq_KZlOvhA_wM\u003D();
      this.\u0023\u003Dzm7hBd9jYl4Gv = this.\u0023\u003DzV9aYAdc\u003D - this.\u0023\u003Dz14KaPQET0IYM;
      this.\u0023\u003DzukvY3XQ\u003D = (this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003Dz94d6KNkFELrvsSVOyQ\u003D\u003D() & 1) == 0;
      if (this.\u0023\u003DzlHUzJ6AB\u0024yFV >= this.\u0023\u003Dzm7hBd9jYl4Gv)
      {
        this.\u0023\u003DzcL29nSWtuHpMA\u0024ZFxA\u003D\u003D = this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D;
        this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D += this.\u0023\u003Dzm7hBd9jYl4Gv;
        this.\u0023\u003DzlHUzJ6AB\u0024yFV -= this.\u0023\u003Dzm7hBd9jYl4Gv;
        if (this.\u0023\u003DzukvY3XQ\u003D)
        {
          this.\u0023\u003DzCUQ2vA0\u003D = new \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzCW27bQGKuCyh(this.\u0023\u003DzcL29nSWtuHpMA\u0024ZFxA\u003D\u003D, this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D);
          this.\u0023\u003DzHJ09GLU\u003D = 1;
          return true;
        }
        goto label_4;
      }
      this.\u0023\u003Dz59\u0024YMlUtK_c\u0024PRIHdA\u003D\u003D = this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D;
      this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D += this.\u0023\u003DzlHUzJ6AB\u0024yFV;
      \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024 zH8qyrxbWyCx2 = this.\u0023\u003DzH8qyrxb_WyCX;
      zH8qyrxbWyCx2.\u0023\u003DzHX4QluI_kG8NGHqDtYeOSVM\u003D(zH8qyrxbWyCx2.\u0023\u003DzQrg7PLXQAunq_KZlOvhA_wM\u003D() + this.\u0023\u003DzlHUzJ6AB\u0024yFV);
      if (this.\u0023\u003DzukvY3XQ\u003D)
      {
        this.\u0023\u003DzCUQ2vA0\u003D = new \u0023\u003Dz8B1nlAnvAhBdiQFqFRFmPTilr\u0024AzSXiASpzkU3Q\u003D.\u0023\u003DzCW27bQGKuCyh(this.\u0023\u003Dz59\u0024YMlUtK_c\u0024PRIHdA\u003D\u003D, this.\u0023\u003DzMPEEu1efpuEqcYHAIw\u003D\u003D);
        this.\u0023\u003DzHJ09GLU\u003D = 2;
        return true;
      }
      goto label_11;
    }

    public void Reset(
      double _param1,
      \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5\u0024e1A7bvEMO3Uo\u0024\u0024JpKeVOZ\u0024 _param2)
    {
      this.\u0023\u003DzH8qyrxb_WyCX = _param2;
      this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003Dz7jjovJyOcjl0y23idA\u003D\u003D(0);
      this.\u0023\u003DzH8qyrxb_WyCX.\u0023\u003DzHX4QluI_kG8NGHqDtYeOSVM\u003D(0.0);
      this.\u0023\u003DzEZ6iGHc\u003D = _param1;
      this.\u0023\u003DzHJ09GLU\u003D = 0;
    }

    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    void IDisposable.Dispose()
    {
    }
  }
}
