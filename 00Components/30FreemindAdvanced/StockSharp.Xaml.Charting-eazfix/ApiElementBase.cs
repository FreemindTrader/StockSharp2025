// Decompiled with JetBrains decompiler
// Type: #=zHHCBm9UpsKz28K2k$32Cv_vXeGtDJsjTomycKYo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

#nullable disable
internal abstract class ApiElementBase : 
  ContentControl,
  INotifyPropertyChanged
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ISciChartSurface \u0023\u003Dzos6SMwAMXZ33;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzbtEp1C7aVw3qdJ3knA\u003D\u003D;

  public event PropertyChangedEventHandler PropertyChanged;

  public virtual ISciChartSurface ParentSurface
  {
    get => this.\u0023\u003Dzos6SMwAMXZ33;
    set
    {
      this.\u0023\u003Dzos6SMwAMXZ33 = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (ParentSurface));
    }
  }

  public IEnumerable<IAxis> XAxes
  {
    get
    {
      return this.ParentSurface == null || this.ParentSurface.get_XAxes() == null ? Enumerable.Empty<IAxis>() : (IEnumerable<IAxis>) this.ParentSurface.get_XAxes();
    }
  }

  public IEnumerable<IAxis> YAxes
  {
    get
    {
      return this.ParentSurface == null || this.ParentSurface.get_YAxes() == null ? Enumerable.Empty<IAxis>() : (IEnumerable<IAxis>) this.ParentSurface.get_YAxes();
    }
  }

  public virtual IAxis YAxis
  {
    get
    {
      ISciChartSurface parentSurface = this.ParentSurface;
      IAxis yaxis = (IAxis) null;
      if (parentSurface != null)
      {
        yaxis = parentSurface.YAxis;
        if (yaxis == null && !parentSurface.get_YAxes().\u0023\u003DzCCMM80zDpO6N<IAxis>())
          yaxis = parentSurface.get_YAxes().FirstOrDefault<IAxis>(ApiElementBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D ?? (ApiElementBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D = new Func<IAxis, bool>(ApiElementBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz40hF903Z7W1MLFKRgtpttIg\u003D)));
      }
      return yaxis;
    }
  }

  public virtual IAxis XAxis
  {
    get
    {
      ISciChartSurface parentSurface = this.ParentSurface;
      IAxis xaxis = (IAxis) null;
      if (parentSurface != null)
      {
        xaxis = parentSurface.XAxis;
        if (xaxis == null && !parentSurface.get_XAxes().\u0023\u003DzCCMM80zDpO6N<IAxis>())
          xaxis = parentSurface.get_XAxes().FirstOrDefault<IAxis>(ApiElementBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D ?? (ApiElementBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D = new Func<IAxis, bool>(ApiElementBase.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzQY8WNqpkffjul30yEBWakgc\u003D)));
      }
      return xaxis;
    }
  }

  public virtual \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D Services
  {
    get => this.\u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D;
    set => this.\u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D = value;
  }

  public \u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D ModifierSurface
  {
    get
    {
      return this.ParentSurface == null ? (\u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D) null : this.ParentSurface.\u0023\u003DzBgWxEdRxHdEh();
    }
  }

  public virtual bool IsAttached
  {
    get => this.\u0023\u003DzbtEp1C7aVw3qdJ3knA\u003D\u003D;
    set => this.\u0023\u003DzbtEp1C7aVw3qdJ3knA\u003D\u003D = value;
  }

  protected \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D \u0023\u003Dzwc4Gzka23TGB()
  {
    return this.ParentSurface == null ? (\u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D) null : this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB();
  }

  public abstract void OnAttached();

  public abstract void OnDetached();

  public IAxis \u0023\u003Dz4uoxB8oLWxeL(
    string _param1)
  {
    return this.ParentSurface == null || this.ParentSurface.get_YAxes() == null ? (IAxis) null : this.ParentSurface.get_YAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(_param1, false);
  }

  public IAxis \u0023\u003DzI0EiGDjWkH8S(
    string _param1)
  {
    return this.ParentSurface == null || this.ParentSurface.get_XAxes() == null ? (IAxis) null : this.ParentSurface.get_XAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(_param1, false);
  }

  protected virtual void \u0023\u003Dzmf\u0024vfR3OJQU9()
  {
    if (this.Services == null)
      return;
    this.Services.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzosHqOAc\u003D<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B>(new \u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B((object) this));
  }

  protected T \u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<T>(string _param1) where T : class
  {
    return this.GetTemplateChild(_param1) is T templateChild ? templateChild : throw new InvalidOperationException($"Unable to Apply the Control Template. {_param1} is missing or of the wrong type");
  }

  protected void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly ApiElementBase.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ApiElementBase.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IAxis, bool> \u0023\u003DzcFhfCcBLh7a_pKoCqw\u003D\u003D;
    public static Func<IAxis, bool> \u0023\u003Dz74JLo6CIZR_cYr0qvA\u003D\u003D;

    internal bool \u0023\u003Dz40hF903Z7W1MLFKRgtpttIg\u003D(
      IAxis _param1)
    {
      return _param1.get_IsPrimaryAxis();
    }

    internal bool \u0023\u003DzQY8WNqpkffjul30yEBWakgc\u003D(
      IAxis _param1)
    {
      return _param1.get_IsPrimaryAxis();
    }
  }
}
