// Decompiled with JetBrains decompiler
// Type: -.InspectSeriesModifierBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable enable
namespace SciChart.Charting;

internal abstract class InspectSeriesModifierBase : 
  ChartModifierBase
{
  
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003DzG8iU0LHRhO7j = DependencyProperty.Register(nameof (UseInterpolation), typeof (bool), typeof (InspectSeriesModifierBase), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzgKwtLGQKgy\u0024Y = DependencyProperty.Register(nameof (SourceMode), typeof (\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D), typeof (InspectSeriesModifierBase), new PropertyMetadata((object) \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllVisibleSeries));
  
  public static readonly DependencyProperty \u0023\u003DzE7h5hUE7Vu4g = DependencyProperty.Register(nameof (SeriesData), typeof (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D), typeof (InspectSeriesModifierBase), new PropertyMetadata((PropertyChangedCallback) null));
  
  protected Point \u0023\u003DzeAqKwx8\u003D = new Point(double.NaN, double.NaN);
  
  private bool \u0023\u003DzKpmBZbLVz8O6;

  protected InspectSeriesModifierBase()
  {
    this.SetCurrentValue(InspectSeriesModifierBase.\u0023\u003DzgKwtLGQKgy\u0024Y, (object) \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllVisibleSeries);
    this.SetCurrentValue(ChartModifierBase.ExecuteOnProperty, (object) ExecuteOn.MouseMove);
  }

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D SeriesData
  {
    get
    {
      return (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D) this.GetValue(InspectSeriesModifierBase.\u0023\u003DzE7h5hUE7Vu4g);
    }
    set
    {
      this.SetValue(InspectSeriesModifierBase.\u0023\u003DzE7h5hUE7Vu4g, (object) value);
    }
  }

  public bool UseInterpolation
  {
    get
    {
      return (bool) this.GetValue(InspectSeriesModifierBase.\u0023\u003DzG8iU0LHRhO7j);
    }
    set
    {
      this.SetValue(InspectSeriesModifierBase.\u0023\u003DzG8iU0LHRhO7j, (object) value);
    }
  }

  public \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D SourceMode
  {
    get
    {
      return (\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D) this.GetValue(InspectSeriesModifierBase.\u0023\u003DzgKwtLGQKgy\u0024Y);
    }
    set
    {
      this.SetValue(InspectSeriesModifierBase.\u0023\u003DzgKwtLGQKgy\u0024Y, (object) value);
    }
  }

  public override void \u0023\u003DzY1JcdEJm3Ryc(
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt _param1)
  {
    base.\u0023\u003DzY1JcdEJm3Ryc(_param1);
    if (!this.IsEnabled)
      return;
    this.\u0023\u003DzebZge1miA2O0(this.\u0023\u003DzeAqKwx8\u003D);
  }

  protected override void \u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D()
  {
    this.\u0023\u003DzeAqKwx8\u003D.X = this.\u0023\u003DzeAqKwx8\u003D.Y = double.NaN;
    this.\u0023\u003DzleRWWIS9Sb_X();
  }

  protected abstract void \u0023\u003DzleRWWIS9Sb_X();

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
    base.OnModifierMouseMove(_param1);
    this.\u0023\u003DzebZge1miA2O0(_param1);
    _param1.Handled(false);
  }

  protected void \u0023\u003DzebZge1miA2O0(
    ModifierMouseArgs _param1)
  {
    int num = this.\u0023\u003DziEIgi1dpKb6j(_param1) ? 1 : 0;
    bool flag = false;
    if (num != 0)
    {
      this.\u0023\u003DzeAqKwx8\u003D = this.GetPointRelativeTo(_param1.MousePoint(), (IHitTestable) this.ModifierSurface);
      this.\u0023\u003DzKpmBZbLVz8O6 = _param1.IsMaster();
      flag = this.\u0023\u003DzebZge1miA2O0(this.\u0023\u003DzeAqKwx8\u003D);
    }
    _param1.Handled(flag);
  }

  private bool \u0023\u003DziEIgi1dpKb6j(
    ModifierMouseArgs _param1)
  {
    return this.ModifierSurface != null && this.IsEnabled && this.MatchesExecuteOn(_param1.MouseButtons(), this.ExecuteOn);
  }

  private bool \u0023\u003DzebZge1miA2O0(Point _param1)
  {
    int num = this.\u0023\u003Dzt9d2ExuvJfVV(_param1) ? 1 : 0;
    if (num != 0)
    {
      if (this.\u0023\u003DzKpmBZbLVz8O6)
      {
        this.\u0023\u003Dz_wtru8oSZoY9(_param1);
        return num != 0;
      }
      this.\u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(_param1);
      return num != 0;
    }
    this.\u0023\u003DzleRWWIS9Sb_X();
    return num != 0;
  }

  protected virtual bool \u0023\u003Dzt9d2ExuvJfVV(Point _param1)
  {
    bool flag = _param1.X.IsDefined() && _param1.Y.IsDefined() && _param1.X >= 0.0 && _param1.X <= this.ModifierSurface.ActualWidth;
    if (this.\u0023\u003DzKpmBZbLVz8O6)
      flag = ((flag ? 1 : 0) & (_param1.Y < 0.0 ? 0 : (_param1.Y <= this.ModifierSurface.ActualHeight ? 1 : 0))) != 0;
    return flag;
  }

  protected abstract void \u0023\u003Dz_wtru8oSZoY9(Point _param1);

  protected abstract void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1);

  protected \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzU0tYbfdnROi1(
    IAxis _param1,
    Point _param2)
  {
    \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D uaaCmXpj7JdmPvp0w = _param1.\u0023\u003DzjuB\u0024Pa8\u003D(_param2);
    if (uaaCmXpj7JdmPvp0w != null)
      uaaCmXpj7JdmPvp0w.IsMasterChartAxis = this.\u0023\u003DzKpmBZbLVz8O6;
    return uaaCmXpj7JdmPvp0w;
  }

  protected virtual IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzzhlDItrRFv\u0024\u0024(
    Point _param1)
  {
    return this.\u0023\u003DzzhlDItrRFv\u0024\u0024(new Func<IRenderableSeries, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D>(new InspectSeriesModifierBase.SomeClass343()
    {
      \u0023\u003Dz_hWqBbI\u003D = _param1,
      _variableSome3535 = this
    }.\u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D));
  }

  protected virtual IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzzhlDItrRFv\u0024\u0024(
    Point _param1,
    double _param2)
  {
    return this.\u0023\u003DzzhlDItrRFv\u0024\u0024(new Func<IRenderableSeries, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D>(new InspectSeriesModifierBase.\u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D()
    {
      \u0023\u003Dz_hWqBbI\u003D = _param1,
      \u0023\u003DzADxu\u00246OzU_cF = _param2,
      _variableSome3535 = this
    }.\u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D));
  }

  protected IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzzhlDItrRFv\u0024\u0024(
    Func<IRenderableSeries, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D> _param1)
  {
    return (IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) new InspectSeriesModifierBase.\u0023\u003Dz89RgnopV4nZgv5NePKVh5qU\u003D(-2)
    {
      _variableSome3535 = this,
      \u0023\u003Dz3Z3OTONWhHvl8dsqDA\u003D\u003D = _param1
    };
  }

  protected virtual bool \u0023\u003DzD5SquRN7M_9c(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return !_param1.\u0023\u003DzMeGSfVE\u003D() && _param1.\u0023\u003Dzmh1LiTa467ce();
  }

  protected virtual bool \u0023\u003DzaBvGZQmHUOsn(
    IRenderableSeries _param1)
  {
    return _param1 != null && this.\u0023\u003DzQciw5LQGN0mc(_param1) && _param1.get_DataSeries() != null;
  }

  private bool \u0023\u003DzQciw5LQGN0mc(
    IRenderableSeries _param1)
  {
    if (this.SourceMode == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllSeries || _param1.IsVisible && this.SourceMode == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllVisibleSeries || _param1.get_IsSelected() && this.SourceMode == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.SelectedSeries)
      return true;
    return !_param1.get_IsSelected() && this.SourceMode == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.UnselectedSeries;
  }

  private sealed class \u0023\u003Dz89RgnopV4nZgv5NePKVh5qU\u003D : 
    IDisposable,
    IEnumerable,
    IEnumerator,
    IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>,
    IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    public InspectSeriesModifierBase _variableSome3535;
    
    private Func<IRenderableSeries, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D> \u0023\u003DzMYNk\u0024KRnKslk;
    
    public Func<IRenderableSeries, \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D> \u0023\u003Dz3Z3OTONWhHvl8dsqDA\u003D\u003D;
    
    private IEnumerator<IRenderableSeries> \u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D;

    [DebuggerHidden]
    public \u0023\u003Dz89RgnopV4nZgv5NePKVh5qU\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case -3:
        case 1:
          try
          {
          }
          finally
          {
            this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
          }
          break;
      }
    }

    bool IEnumerator.MoveNext()
    {
      // ISSUE: fault handler
      try
      {
        int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
        InspectSeriesModifierBase zRrvwDu67s9Rm = this._variableSome3535;
        switch (z4fzyEz1SsHya)
        {
          case 0:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
            if (zRrvwDu67s9Rm.ParentSurface != null && !zRrvwDu67s9Rm.ParentSurface.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>())
            {
              this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D = zRrvwDu67s9Rm.ParentSurface.get_RenderableSeries().GetEnumerator();
              this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
              break;
            }
            goto label_10;
          case 1:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            break;
          default:
            return false;
        }
        while (this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.MoveNext())
        {
          IRenderableSeries current = this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.Current;
          if (zRrvwDu67s9Rm.\u0023\u003DzaBvGZQmHUOsn(current))
          {
            \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = this.\u0023\u003DzMYNk\u0024KRnKslk(current);
            if (zRrvwDu67s9Rm.\u0023\u003DzD5SquRN7M_9c(zldchDrVsrVyHh6WyiGy))
            {
              this.\u0023\u003Dzaev1bhaFFIDX = current.\u0023\u003DzZZbJdAS6fDJ\u0024(zldchDrVsrVyHh6WyiGy);
              this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
              return true;
            }
          }
        }
        this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
        this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D = (IEnumerator<IRenderableSeries>) null;
label_10:
        return false;
      }
      __fault
      {
        this.Dispose();
      }
    }

    private void \u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D()
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
      if (this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D == null)
        return;
      this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.Dispose();
    }

    [DebuggerHidden]
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>.\u0023\u003Dz4\u0024vcRAdnkf8XAMQ6U6I6aJAVdQshEFF3YrEuKf9hCSePAnpiCKyv8pQ\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<
    #nullable disable
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>.\u0023\u003DzdCr_FD\u0024knmYtmaB5oybNnJEENtOXJw7Q1xWzDzSUVGdOwwA2iYK9I4g\u003D()
    {
      InspectSeriesModifierBase.\u0023\u003Dz89RgnopV4nZgv5NePKVh5qU\u003D v4nZgv5NePkVh5qU;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        v4nZgv5NePkVh5qU = this;
      }
      else
      {
        v4nZgv5NePkVh5qU = new InspectSeriesModifierBase.\u0023\u003Dz89RgnopV4nZgv5NePKVh5qU\u003D(0);
        v4nZgv5NePkVh5qU._variableSome3535 = this._variableSome3535;
      }
      v4nZgv5NePkVh5qU.\u0023\u003DzMYNk\u0024KRnKslk = this.\u0023\u003Dz3Z3OTONWhHvl8dsqDA\u003D\u003D;
      return (IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) v4nZgv5NePkVh5qU;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003DzdCr_FD\u0024knmYtmaB5oybNnJEENtOXJw7Q1xWzDzSUVGdOwwA2iYK9I4g\u003D();
    }
  }

  private sealed class SomeClass343
  {
    public Point \u0023\u003Dz_hWqBbI\u003D;
    public 
    #nullable disable
    InspectSeriesModifierBase _variableSome3535;

    internal \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D(
      IRenderableSeries _param1)
    {
      return _param1.\u0023\u003DzjuB\u0024Pa8\u003D(this.\u0023\u003Dz_hWqBbI\u003D, this._variableSome3535.UseInterpolation);
    }
  }

  private sealed class \u0023\u003DzNCeF9LrrNtIjst\u0024ouVg4RRo\u003D
  {
    public Point \u0023\u003Dz_hWqBbI\u003D;
    public double \u0023\u003DzADxu\u00246OzU_cF;
    public InspectSeriesModifierBase _variableSome3535;

    internal \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzvJtlk3txQNN91rBG8dcpcj0\u003D(
      IRenderableSeries _param1)
    {
      return _param1.\u0023\u003DzjuB\u0024Pa8\u003D(this.\u0023\u003Dz_hWqBbI\u003D, this.\u0023\u003DzADxu\u00246OzU_cF, this._variableSome3535.UseInterpolation);
    }
  }
}
