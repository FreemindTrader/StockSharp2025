// Decompiled with JetBrains decompiler
// Type: #=zzSC94lsu$2WfTPlDSLyhlAeCwpSJWgRV4sBbHcbkgHna
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlAeCwpSJWgRV4sBbHcbkgHna : 
  IDisposable,
  IEnumerator,
  IEnumerator<IPoint>,
  IEnumerator<\u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D>
{
  
  private readonly IPointSeries \u0023\u003Dz6leEYQVpwDm8;
  
  private readonly int \u0023\u003DztUQ677I\u003D;
  
  private int \u0023\u003DzKx97DYo\u003D;
  
  private bool \u0023\u003Dz5GSATUT75KjL;
  
  private IPoint \u0023\u003DzCUQ2vA0\u003D;
  
  private \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D \u0023\u003DzJe3j2ifNwqKL;

  public \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlAeCwpSJWgRV4sBbHcbkgHna(
    IPointSeries _param1)
  {
    this.\u0023\u003Dz6leEYQVpwDm8 = _param1;
    this.\u0023\u003DztUQ677I\u003D = _param1.\u0023\u003DzlpVGw6E\u003D();
    this.Reset();
  }

  public bool \u0023\u003Dz9hmykWfoVpXr() => this.\u0023\u003DzKx97DYo\u003D < 0;

  public IPoint Current
  {
    get
    {
      if (!this.\u0023\u003Dz5GSATUT75KjL)
        this.\u0023\u003DzCUQ2vA0\u003D = (IPoint) this.\u0023\u003DzJe3j2ifNwqKL;
      return this.\u0023\u003DzCUQ2vA0\u003D;
    }
  }

  \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D IEnumerator<\u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D>.\u0023\u003DzMFfdIvezJakHDaL_gs9K0sZZE1qIj1bqxGy_AVuIY_vy_YA9BHbyOPzoKZ_Grdmfmw\u003D\u003D()
  {
    return this.\u0023\u003DzJe3j2ifNwqKL;
  }

  public \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D \u0023\u003DzfGt4uR3b7q6A()
  {
    return this.\u0023\u003DzJe3j2ifNwqKL;
  }

  object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D() => (object) this.Current;

  public virtual bool MoveNext()
  {
    if (++this.\u0023\u003DzKx97DYo\u003D >= this.\u0023\u003DztUQ677I\u003D)
      return false;
    this.\u0023\u003DzCUQ2vA0\u003D = this.\u0023\u003Dz6leEYQVpwDm8.\u0023\u003Dz\u0024CeUvME\u003D(this.\u0023\u003DzKx97DYo\u003D);
    this.\u0023\u003DzJe3j2ifNwqKL = new \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D(this.\u0023\u003DzCUQ2vA0\u003D.X, this.\u0023\u003DzCUQ2vA0\u003D.Y);
    this.\u0023\u003Dz5GSATUT75KjL = true;
    return true;
  }

  public virtual void Reset()
  {
    this.\u0023\u003DzKx97DYo\u003D = -1;
    this.\u0023\u003DzCUQ2vA0\u003D = (IPoint) null;
    this.\u0023\u003DzJe3j2ifNwqKL = new \u0023\u003DzPauio66DvxKtWOFEEHOV9eqtPYsuAYR8YjvqTxI\u003D();
  }

  public virtual void Dispose()
  {
  }

  private void \u0023\u003DzYJ3tqgw\u003D()
  {
    if (this.\u0023\u003DzKx97DYo\u003D < 0)
      throw new InvalidOperationException();
  }
}
