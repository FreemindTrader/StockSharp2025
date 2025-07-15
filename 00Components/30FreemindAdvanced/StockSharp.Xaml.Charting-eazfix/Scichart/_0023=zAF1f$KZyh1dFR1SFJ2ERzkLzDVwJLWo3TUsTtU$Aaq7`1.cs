// Decompiled with JetBrains decompiler
// Type: #=zAF1f$KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU$Aaq7iLz8eHA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
internal sealed class \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D<\u0023\u003DzPhmbMJE\u003D>
{
  private readonly Action<IList<\u0023\u003DzPhmbMJE\u003D>> \u0023\u003DzJ8maPHYQh\u00246r;
  private List<\u0023\u003DzPhmbMJE\u003D> \u0023\u003DzYw05nwk\u003D = new List<\u0023\u003DzPhmbMJE\u003D>();
  private readonly object \u0023\u003DzFfdu1N5nM9V1by\u0024JCXnlPq0\u003D = new object();

  public \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzkLzDVwJLWo3TUsTtU\u0024Aaq7iLz8eHA\u003D\u003D(
    Action<IList<\u0023\u003DzPhmbMJE\u003D>> _param1)
  {
    this.\u0023\u003DzJ8maPHYQh\u00246r = _param1;
  }

  public object \u0023\u003Dzjatnj7TNvda7() => this.\u0023\u003DzFfdu1N5nM9V1by\u0024JCXnlPq0\u003D;

  public void Clear()
  {
    lock (this.\u0023\u003Dzjatnj7TNvda7())
      this.\u0023\u003DzYw05nwk\u003D.Clear();
  }

  public void \u0023\u003DzY9qzIPY\u003D()
  {
    if (this.\u0023\u003DzYw05nwk\u003D.Count == 0)
      return;
    List<\u0023\u003DzPhmbMJE\u003D> zYw05nwk;
    lock (this.\u0023\u003Dzjatnj7TNvda7())
    {
      zYw05nwk = this.\u0023\u003DzYw05nwk\u003D;
      this.\u0023\u003DzYw05nwk\u003D = new List<\u0023\u003DzPhmbMJE\u003D>();
    }
    if (zYw05nwk.Count <= 0)
      return;
    this.\u0023\u003DzJ8maPHYQh\u00246r((IList<\u0023\u003DzPhmbMJE\u003D>) zYw05nwk);
  }

  public void \u0023\u003Dznc8esWY\u003D(\u0023\u003DzPhmbMJE\u003D _param1)
  {
    lock (this.\u0023\u003Dzjatnj7TNvda7())
      this.\u0023\u003DzYw05nwk\u003D.Add(_param1);
  }
}
