// Decompiled with JetBrains decompiler
// Type: #=z3arZou$KE51WuqbncgcGPkZqxYZZKmiH00SK0z8=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Threading;

#nullable disable
internal sealed class \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D : IDisposable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D \u0023\u003DzVBVazrI\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Action \u0023\u003DzNQY2Q7mG9LR5;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private DispatcherTimer \u0023\u003DzO6iOtf4\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile bool \u0023\u003DzmHM_et\u0024F5OBc;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile bool \u0023\u003DzvBK\u00248KQ\u003D;

  internal \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPkZqxYZZKmiH00SK0z8\u003D(
    double? _param1,
    \u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D _param2,
    Action _param3)
  {
    this.\u0023\u003DzVBVazrI\u003D = _param2;
    this.\u0023\u003DzNQY2Q7mG9LR5 = _param3;
    if (_param1.HasValue)
    {
      this.\u0023\u003DzO6iOtf4\u003D = new DispatcherTimer();
      this.\u0023\u003DzO6iOtf4\u003D.Interval = TimeSpan.FromMilliseconds(1000.0 / \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D(_param1.Value, 0.0, 100.0));
      this.\u0023\u003DzO6iOtf4\u003D.Tick += new EventHandler(this.\u0023\u003Dz0rIRzgIYIXra);
      this.\u0023\u003DzO6iOtf4\u003D.Start();
    }
    else
    {
      CompositionTarget.Rendering -= new EventHandler(this.\u0023\u003DzzYypnyJA76yR);
      CompositionTarget.Rendering += new EventHandler(this.\u0023\u003DzzYypnyJA76yR);
    }
  }

  private void \u0023\u003Dz0rIRzgIYIXra(object _param1, EventArgs _param2)
  {
    if (this.\u0023\u003DzmHM_et\u0024F5OBc)
      return;
    this.\u0023\u003DzmHM_et\u0024F5OBc = true;
    \u0023\u003DzyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng\u003D\u003D dztiM3x3BrpxyflUng = new \u0023\u003DzyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng\u003D\u003D(new Action(this.\u0023\u003Dzpbvl85E\u003D));
  }

  private void \u0023\u003Dzpbvl85E\u003D()
  {
    try
    {
      this.\u0023\u003DzNQY2Q7mG9LR5();
    }
    finally
    {
      this.\u0023\u003DzmHM_et\u0024F5OBc = false;
    }
  }

  private void \u0023\u003DzzYypnyJA76yR(object _param1, EventArgs _param2)
  {
    if (this.\u0023\u003DzmHM_et\u0024F5OBc)
      return;
    this.\u0023\u003DzmHM_et\u0024F5OBc = true;
    this.\u0023\u003Dzpbvl85E\u003D();
  }

  public void Dispose()
  {
    lock (this)
    {
      if (this.\u0023\u003DzO6iOtf4\u003D != null)
      {
        this.\u0023\u003DzO6iOtf4\u003D.Stop();
        this.\u0023\u003DzO6iOtf4\u003D.Tick -= new EventHandler(this.\u0023\u003Dz0rIRzgIYIXra);
        this.\u0023\u003DzO6iOtf4\u003D = (DispatcherTimer) null;
      }
      else if (!this.\u0023\u003DzvBK\u00248KQ\u003D)
        this.\u0023\u003DzVBVazrI\u003D.\u0023\u003Dz40vIrjqAtFMX(new Action(this.\u0023\u003DzsrH3equ4gk75udMrVQ\u003D\u003D), (DispatcherPriority) 9);
      this.\u0023\u003DzvBK\u00248KQ\u003D = true;
    }
  }

  private void \u0023\u003DzsrH3equ4gk75udMrVQ\u003D\u003D()
  {
    CompositionTarget.Rendering -= new EventHandler(this.\u0023\u003DzzYypnyJA76yR);
  }
}
