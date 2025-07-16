// Decompiled with JetBrains decompiler
// Type: #=z6$kyD91Y546oWffDBUVKqAfDPGUkdbttZg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

#nullable disable
internal abstract class \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D : 
  \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D,
  ISuspendable,
  \u0023\u003DzUib3SzczDtLU7txM4YiSeNZjP0NRThUE6PRgmDMkI3UwPa6FIQ\u003D\u003D,
  IInvalidatableElement
{
  private ISciChartSurface \u0023\u003Dzk8uzfa2nSBLr;
  private IServiceContainer _serviceContainer;
  private bool _isAttached;

  [SpecialName]
  public IServiceContainer Services()
  {
    return this._serviceContainer;
  }

  [SpecialName]
  public void Services(
    IServiceContainer _param1)
  {
    this._serviceContainer = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003DzWNQ9StWEg__B() => this._isAttached;

  private void \u0023\u003Dz6YtMscjD4FqR(bool _param1)
  {
    this._isAttached = _param1;
  }

  public virtual void \u0023\u003DzPA8CxqX98AVD701gF5MzwGc\u003D(
    ISciChartSurface _param1)
  {
    this.\u0023\u003Dzk8uzfa2nSBLr = _param1;
    this._serviceContainer = this.\u0023\u003Dzk8uzfa2nSBLr.Services();
    this.\u0023\u003Dz6YtMscjD4FqR(true);
  }

  public virtual void \u0023\u003Dzpcs_ok3YoH9BrujbKxTSzYg\u003D()
  {
    this.\u0023\u003Dzk8uzfa2nSBLr = (ISciChartSurface) null;
    this._serviceContainer = (IServiceContainer) null;
    this.\u0023\u003Dz6YtMscjD4FqR(false);
  }

  public bool IsSuspended
  {
    get
    {
      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this);
    }
  }

  public IUpdateSuspender SuspendUpdates()
  {
    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this);
  }

  public void ResumeUpdates(
    IUpdateSuspender _param1)
  {
  }

  public void DecrementSuspend()
  {
  }

  public void InvalidateElement()
  {
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(this.\u0023\u003Dz78o1hEFvydGISDJqAgxt9Hk\u003D));
  }

  private void \u0023\u003DzJhZ2ohI\u003D(Action _param1)
  {
    this._serviceContainer.GetService<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>().\u0023\u003Dz40vIrjqAtFMX(_param1, (DispatcherPriority) 2);
  }

  public void ZoomExtents()
  {
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(this.\u0023\u003DzdvsLwG9JnTHhDWI\u0024G3VVNhM\u003D));
  }

  public void \u0023\u003Dz5v2P2MZQ6Mg5pHstYQ\u003D\u003D(TimeSpan _param1)
  {
    \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D.SomeClass7654 lvtppRwsYcyoelU8 = new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D.SomeClass7654();
    lvtppRwsYcyoelU8._variableSome3535 = this;
    lvtppRwsYcyoelU8.\u0023\u003DzXOURnwA\u003D = _param1;
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(lvtppRwsYcyoelU8.\u0023\u003Dz5cvR57bCJ\u0024icEF_PguV3F8KMaVLt));
  }

  public void ZoomExtentsY()
  {
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(this.\u0023\u003DzfYn71Dt5n5ePpEUapCPq4H8\u003D));
  }

  public void \u0023\u003Dzlt5y\u0024abM\u0024EiJBWUsR3G_Wrc\u003D(TimeSpan _param1)
  {
    \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D.SomeClass6409 v4vdZv8GtEzAmB0rzFq = new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D.SomeClass6409();
    v4vdZv8GtEzAmB0rzFq._variableSome3535 = this;
    v4vdZv8GtEzAmB0rzFq.\u0023\u003DzXOURnwA\u003D = _param1;
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(v4vdZv8GtEzAmB0rzFq.\u0023\u003DzRQvEFcONmUBT2PTwERTtLDbqy8es));
  }

  public void \u0023\u003Dz8zwqAzdRsuc\u0024()
  {
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(this.\u0023\u003DzvB6dKADvktLSs3oZyccuQt0\u003D));
  }

  public void \u0023\u003Dz8NovIOacEzVlET_SOgsaL_w\u003D(TimeSpan _param1)
  {
    \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D.SomeClass7237 doDcwiev7trI4Ny0 = new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D.SomeClass7237();
    doDcwiev7trI4Ny0._variableSome3535 = this;
    doDcwiev7trI4Ny0.\u0023\u003DzXOURnwA\u003D = _param1;
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.\u0023\u003DzJhZ2ohI\u003D(new Action(doDcwiev7trI4Ny0.\u0023\u003DzxSNk7gqofrWCxdQwCG6opwzg6Kvk));
  }

  public virtual IRange \u0023\u003DzF9GccVo\u0024dKwo(
    IAxis _param1)
  {
    if (_param1.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always || _param1.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Once)
    {
      IRange abyLt9clZggmJsWhw = _param1.\u0023\u003DzFwoMKP9juTnt();
      if (abyLt9clZggmJsWhw != null && abyLt9clZggmJsWhw.IsDefined)
        return abyLt9clZggmJsWhw;
    }
    return _param1.VisibleRange;
  }

  public IRange \u0023\u003Dzj78Bq7gNYueNTATr1Q\u003D\u003D(
    IAxis _param1)
  {
    return !this.IsSuspended ? this.\u0023\u003DzagcOSHrk9Og4(_param1) : _param1.VisibleRange;
  }

  public IRange \u0023\u003DzhDjoRkNuf3z7_02BMQ\u003D\u003D(
    IAxis _param1,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param2)
  {
    return !this.IsSuspended ? this.\u0023\u003DzzyBAd1oR0Zv2(_param1, _param2) : _param1.VisibleRange;
  }

  protected abstract IRange \u0023\u003DzagcOSHrk9Og4(
    IAxis _param1);

  protected abstract IRange \u0023\u003DzzyBAd1oR0Zv2(
    IAxis _param1,
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param2);

  public virtual void \u0023\u003Dz_0Le6I5slA7z(
    IAxis _param1)
  {
  }

  public virtual void \u0023\u003DzY1JcdEJm3Ryc(
    ISciChartSurface _param1)
  {
  }

  public void \u0023\u003Dzo1moqKMmSMi6(
    RangeMode _param1)
  {
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D qob9tosVgUIiwaqHr9Eoq = this.Services().GetService<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>();
    switch (_param1)
    {
      case (RangeMode) 0:
        qob9tosVgUIiwaqHr9Eoq.\u0023\u003DzosHqOAc\u003D<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B>(new \u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B((object) this));
        break;
      case (RangeMode) 1:
        qob9tosVgUIiwaqHr9Eoq.\u0023\u003DzosHqOAc\u003D<\u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D>(new \u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D((object) this));
        break;
      case (RangeMode) 2:
        qob9tosVgUIiwaqHr9Eoq.\u0023\u003DzosHqOAc\u003D<\u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D>(new \u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D((object) this, true));
        break;
    }
  }

  protected void \u0023\u003Dzmf\u0024vfR3OJQU9(DependencyPropertyChangedEventArgs _param1)
  {
    if (!this.\u0023\u003DzWNQ9StWEg__B())
      return;
    this.Services().GetService<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzosHqOAc\u003D<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B>(new \u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B((object) this));
  }

  private void \u0023\u003Dz78o1hEFvydGISDJqAgxt9Hk\u003D()
  {
    this.\u0023\u003Dzk8uzfa2nSBLr.InvalidateElement();
  }

  private void \u0023\u003DzdvsLwG9JnTHhDWI\u0024G3VVNhM\u003D()
  {
    this.\u0023\u003Dzk8uzfa2nSBLr.ZoomExtents();
  }

  private void \u0023\u003DzfYn71Dt5n5ePpEUapCPq4H8\u003D()
  {
    this.\u0023\u003Dzk8uzfa2nSBLr.ZoomExtentsY();
  }

  private void \u0023\u003DzvB6dKADvktLSs3oZyccuQt0\u003D()
  {
    this.\u0023\u003Dzk8uzfa2nSBLr.\u0023\u003Dz8zwqAzdRsuc\u0024();
  }

  private sealed class SomeClass7654
  {
    public \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D _variableSome3535;
    public TimeSpan \u0023\u003DzXOURnwA\u003D;

    internal void \u0023\u003Dz5cvR57bCJ\u0024icEF_PguV3F8KMaVLt()
    {
      this._variableSome3535.\u0023\u003Dzk8uzfa2nSBLr.\u0023\u003Dz5v2P2MZQ6Mg5pHstYQ\u003D\u003D(this.\u0023\u003DzXOURnwA\u003D);
    }
  }

  private sealed class SomeClass6409
  {
    public \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D _variableSome3535;
    public TimeSpan \u0023\u003DzXOURnwA\u003D;

    internal void \u0023\u003DzRQvEFcONmUBT2PTwERTtLDbqy8es()
    {
      this._variableSome3535.\u0023\u003Dzk8uzfa2nSBLr.\u0023\u003Dzlt5y\u0024abM\u0024EiJBWUsR3G_Wrc\u003D(this.\u0023\u003DzXOURnwA\u003D);
    }
  }

  private sealed class SomeClass7237
  {
    public \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqAfDPGUkdbttZg\u003D\u003D _variableSome3535;
    public TimeSpan \u0023\u003DzXOURnwA\u003D;

    internal void \u0023\u003DzxSNk7gqofrWCxdQwCG6opwzg6Kvk()
    {
      this._variableSome3535.\u0023\u003Dzk8uzfa2nSBLr.\u0023\u003Dz8NovIOacEzVlET_SOgsaL_w\u003D(this.\u0023\u003DzXOURnwA\u003D);
    }
  }
}
