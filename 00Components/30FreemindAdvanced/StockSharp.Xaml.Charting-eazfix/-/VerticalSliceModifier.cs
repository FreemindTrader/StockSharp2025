// Decompiled with JetBrains decompiler
// Type: -.VerticalSliceModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable enable
namespace StockSharp.Charting;

public sealed class VerticalSliceModifier : 
  VerticalSliceModifierBase
{
  
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003DzNRygy3vTBpTh = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (VerticalSliceModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzYUs_mQPEOnkt = DependencyProperty.Register(nameof (VerticalLines), typeof (\u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy), typeof (VerticalSliceModifier), new PropertyMetadata((object) null, new PropertyChangedCallback(VerticalSliceModifier.\u0023\u003DzFO8zq6HsiQjTOG\u0024B6g\u003D\u003D)));
  
  private Dictionary<BaseRenderableSeries, \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<TemplatableControl>> \u0023\u003DzIG74tcJ5rDynZ21mNm2Rm5I\u003D = new Dictionary<BaseRenderableSeries, \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<TemplatableControl>>();

  public VerticalSliceModifier()
  {
    this.DefaultStyleKey = (object) typeof (VerticalSliceModifier);
    this.SetCurrentValue(InspectSeriesModifierBase.\u0023\u003DzE7h5hUE7Vu4g, (object) new \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D());
    this.SetCurrentValue(VerticalSliceModifier.\u0023\u003DzYUs_mQPEOnkt, (object) new \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy());
  }

  public static bool GetIncludeSeries(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(VerticalSliceModifier.\u0023\u003DzNRygy3vTBpTh);
  }

  public static void SetIncludeSeries(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(VerticalSliceModifier.\u0023\u003DzNRygy3vTBpTh, (object) _param1);
  }

  public \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy VerticalLines
  {
    get
    {
      return (\u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy) this.GetValue(VerticalSliceModifier.\u0023\u003DzYUs_mQPEOnkt);
    }
    set
    {
      this.SetValue(VerticalSliceModifier.\u0023\u003DzYUs_mQPEOnkt, (object) value);
    }
  }

  protected override IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzzhlDItrRFv\u0024\u0024(
    Point _param1)
  {
    return (IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) new VerticalSliceModifier.\u0023\u003DzZWngLGgklhPywGpG5ND4_Zk\u003D(-2)
    {
      _variableSome3535 = this
    };
  }

  protected override FrameworkElement \u0023\u003DzoHJDgDlSejs6FIKEDvqYw6U\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    VerticalSliceModifier.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D k0hz6MwLrPm7JfgVw01g = new VerticalSliceModifier.\u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D()
    {
      \u0023\u003DzxhPyxntvLsMx = _param1
    };
    k0hz6MwLrPm7JfgVw01g._IRenderableSeries334 = k0hz6MwLrPm7JfgVw01g.\u0023\u003DzxhPyxntvLsMx.RenderableSeries as BaseRenderableSeries;
    if (k0hz6MwLrPm7JfgVw01g._IRenderableSeries334 == null)
      return (FrameworkElement) null;
    \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<TemplatableControl> xaNmvCep2Le5Guown;
    if (!this.\u0023\u003DzIG74tcJ5rDynZ21mNm2Rm5I\u003D.TryGetValue(k0hz6MwLrPm7JfgVw01g._IRenderableSeries334, out xaNmvCep2Le5Guown))
    {
      xaNmvCep2Le5Guown = new \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<TemplatableControl>();
      this.\u0023\u003DzIG74tcJ5rDynZ21mNm2Rm5I\u003D.Add(k0hz6MwLrPm7JfgVw01g._IRenderableSeries334, xaNmvCep2Le5Guown);
    }
    return (FrameworkElement) xaNmvCep2Le5Guown.\u0023\u003Dza7jLYgw\u003D(new Func<TemplatableControl>(k0hz6MwLrPm7JfgVw01g.\u0023\u003DzfwAthCM9gFRqdQgGSqHIvpSD28CW));
  }

  protected override void \u0023\u003DzpWd3bhexNgJne_G3pk5QQoE\u003D(FrameworkElement _param1)
  {
    base.\u0023\u003DzpWd3bhexNgJne_G3pk5QQoE\u003D(_param1);
    if (!(_param1.DataContext is \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D dataContext))
      return;
    this.\u0023\u003DzIG74tcJ5rDynZ21mNm2Rm5I\u003D[(BaseRenderableSeries) dataContext.RenderableSeries].\u0023\u003DzhggR\u00247o\u003D((TemplatableControl) _param1);
  }

  protected override void \u0023\u003DzAX8eceNZBdIhNPOcDA\u003D\u003D()
  {
  }

  protected override bool \u0023\u003Dzt9d2ExuvJfVV(Point _param1) => true;

  protected override void \u0023\u003Dz_HFvQ2jjCDBP(
    IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param1,
    ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param2)
  {
    _param1.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(new Action<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(new VerticalSliceModifier.SomeInternalSealedClass98364()
    {
      \u0023\u003Dz1H5OM_lRQN1_Ej_iLQ\u003D\u003D = _param2
    }.\u0023\u003Dzk4y25HcID\u0024p8FcTs_CL_Zpw\u003D));
  }

  protected override void OnIsEnabledChanged()
  {
    base.OnIsEnabledChanged();
    if (this.IsEnabled)
      this.OnAttached();
    else
      this.OnDetached();
  }

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003Dzw6RjUc4ly661();
  }

  private void \u0023\u003Dzw6RjUc4ly661()
  {
    ISciChartSurface parentSurface = this.ParentSurface;
    if (parentSurface == null)
      return;
    parentSurface.\u0023\u003Dz6gcEAkTU68jnRIWXQQ\u003D\u003D(new EventHandler(this.\u0023\u003DzdxKzSkmKIrvuxV2EQHfUZXc\u003D));
    parentSurface.\u0023\u003DzVms\u0024rml2A7zL1pwmGg\u003D\u003D(new EventHandler(this.\u0023\u003DzdxKzSkmKIrvuxV2EQHfUZXc\u003D));
    this.VerticalLines.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz5jhxd4Qlmgra);
    this.VerticalLines.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz5jhxd4Qlmgra);
    foreach (VerticalLineAnnotation verticalLine in (Collection<VerticalLineAnnotation>) this.VerticalLines)
      this.ParentSurface.get_Annotations().Add((IAnnotation) verticalLine);
    this.\u0023\u003DzdxKzSkmKIrvuxV2EQHfUZXc\u003D((object) null, EventArgs.Empty);
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003Dzhtm7Y005G9mj();
  }

  private void \u0023\u003Dzhtm7Y005G9mj()
  {
    if (this.VerticalLines == null)
      return;
    this.VerticalLines.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003Dz5jhxd4Qlmgra);
    ISciChartSurface parentSurface = this.ParentSurface;
    if (parentSurface != null)
    {
      parentSurface.\u0023\u003Dz6gcEAkTU68jnRIWXQQ\u003D\u003D(new EventHandler(this.\u0023\u003DzdxKzSkmKIrvuxV2EQHfUZXc\u003D));
      if (parentSurface.get_Annotations() != null)
        parentSurface.get_Annotations().CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzxYNK3fP2Smwj);
    }
    foreach (VerticalLineAnnotation verticalLineAnnotation in this.VerticalLines.ToArray<VerticalLineAnnotation>())
      this.\u0023\u003Dzmos0jX4auQ\u0024T(verticalLineAnnotation);
  }

  private void \u0023\u003Dz5jhxd4Qlmgra(object _param1, NotifyCollectionChangedEventArgs _param2)
  {
    IList newItems = _param2.NewItems;
    IList oldItems = _param2.OldItems;
    if (oldItems != null)
      oldItems.OfType<VerticalLineAnnotation>().\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(this.\u0023\u003Dzmos0jX4auQ\u0024T));
    if (newItems != null)
      newItems.OfType<VerticalLineAnnotation>().\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(this.\u0023\u003DzP42\u0024FaFVbvo7));
    if (_param2.Action == NotifyCollectionChangedAction.Reset)
      this.VerticalLines.\u0023\u003DzqSDPyRh2Gp9P.\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(this.\u0023\u003Dzmos0jX4auQ\u0024T));
    this.\u0023\u003Dz_wtru8oSZoY9(this.\u0023\u003DzeAqKwx8\u003D);
  }

  private void \u0023\u003DzP42\u0024FaFVbvo7(VerticalLineAnnotation _param1)
  {
    this.\u0023\u003Dzmos0jX4auQ\u0024T(_param1);
    _param1.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
    if (this.ParentSurface == null || this.ParentSurface.get_Annotations() == null)
      return;
    this.ParentSurface.get_Annotations().Add((IAnnotation) _param1);
  }

  private void \u0023\u003Dzmos0jX4auQ\u0024T(VerticalLineAnnotation _param1)
  {
    _param1.IsHiddenChanged -= new EventHandler(this.\u0023\u003DzV_0VpU6ZLrfT);
    _param1.PropertyChanged -= new PropertyChangedEventHandler(this.OnPropertyChanged);
    if (this.ParentSurface == null || this.ParentSurface.get_Annotations() == null)
      return;
    this.ParentSurface.get_Annotations().Remove((IAnnotation) _param1);
  }

  private void OnPropertyChanged(object _param1, PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == "PositionChanged"))
      return;
    this.\u0023\u003Dz_wtru8oSZoY9(new Point());
  }

  private void \u0023\u003DzV_0VpU6ZLrfT(object _param1, EventArgs _param2)
  {
    this.\u0023\u003Dz_wtru8oSZoY9(new Point());
  }

  private void \u0023\u003DzdxKzSkmKIrvuxV2EQHfUZXc\u003D(object _param1, EventArgs _param2)
  {
    if (this.ParentSurface == null || this.ParentSurface.get_Annotations() == null)
      return;
    this.ParentSurface.get_Annotations().CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzxYNK3fP2Smwj);
    this.ParentSurface.get_Annotations().CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003DzxYNK3fP2Smwj);
    this.\u0023\u003DzxYNK3fP2Smwj((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    this.VerticalLines.\u0023\u003DzqSDPyRh2Gp9P.\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(((Collection<VerticalLineAnnotation>) this.VerticalLines).Add));
  }

  private void \u0023\u003DzxYNK3fP2Smwj(object _param1, NotifyCollectionChangedEventArgs _param2)
  {
    VerticalSliceModifier.Struct0 vqd1Qhu2nAw1nzwT0 = new VerticalSliceModifier.Struct0();
    if (this.ParentSurface == null || this.ParentSurface.get_Annotations() == null)
      return;
    vqd1Qhu2nAw1nzwT0.\u0023\u003DzcY2SqgU\u003D = this.VerticalLines;
    IList oldItems = _param2.OldItems;
    if (oldItems != null)
      oldItems.OfType<VerticalLineAnnotation>().\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(vqd1Qhu2nAw1nzwT0.\u0023\u003DzcH88MchroZVRhx9qkQ\u003D\u003D));
    if (_param2.Action != NotifyCollectionChangedAction.Reset)
      return;
    vqd1Qhu2nAw1nzwT0.\u0023\u003DzcY2SqgU\u003D.Clear();
    this.ParentSurface.InvalidateElement();
  }

  private static void \u0023\u003DzFO8zq6HsiQjTOG\u0024B6g\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    VerticalSliceModifier fljuaR85WhV6Rm45VEjd = (VerticalSliceModifier) _param0;
    \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy oldValue = _param1.OldValue as \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy;
    \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy newValue = _param1.NewValue as \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy;
    if (oldValue != null)
    {
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(fljuaR85WhV6Rm45VEjd.\u0023\u003Dz5jhxd4Qlmgra);
      oldValue.\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(fljuaR85WhV6Rm45VEjd.\u0023\u003Dzmos0jX4auQ\u0024T));
    }
    if (newValue == null)
      return;
    newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(fljuaR85WhV6Rm45VEjd.\u0023\u003Dz5jhxd4Qlmgra);
    newValue.\u0023\u003Dz30RSSSygABj_<VerticalLineAnnotation>(new Action<VerticalLineAnnotation>(fljuaR85WhV6Rm45VEjd.\u0023\u003DzP42\u0024FaFVbvo7));
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
  }

  [DebuggerHidden]
  private IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003DzhtAjDMSkTbY7(
    Point _param1)
  {
    return base.\u0023\u003DzzhlDItrRFv\u0024\u0024(_param1);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly VerticalSliceModifier.SomeClass34343383 SomeMethond0343 = new VerticalSliceModifier.SomeClass34343383();
    public static Func<VerticalLineAnnotation, bool> \u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D;

    public bool \u0023\u003DzjmBi8jZjC\u0024j95EOhgQaK4bU\u003D(VerticalLineAnnotation _param1)
    {
      return !_param1.IsHidden && _param1.IsAttached && _param1.XAxis != null && _param1.XAxis.IsHorizontalAxis;
    }
  }

  private sealed class \u0023\u003DzHF4K0hz6MwLRPm7JFGVw01g\u003D
  {
    public BaseRenderableSeries _IRenderableSeries334;
    public \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003DzxhPyxntvLsMx;

    public TemplatableControl \u0023\u003DzfwAthCM9gFRqdQgGSqHIvpSD28CW()
    {
      return (TemplatableControl) PointMarker.\u0023\u003DzBv1vB\u0024LEKSF4(this._IRenderableSeries334.RolloverMarkerTemplate, (object) this.\u0023\u003DzxhPyxntvLsMx);
    }
  }

  private new sealed class Struct0
  {
    public \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy \u0023\u003DzcY2SqgU\u003D;

    public void \u0023\u003DzcH88MchroZVRhx9qkQ\u003D\u003D(VerticalLineAnnotation _param1)
    {
      this.\u0023\u003DzcY2SqgU\u003D.Remove(_param1);
    }
  }

  private sealed class SomeInternalSealedClass98364
  {
    public ObservableCollection<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003Dz1H5OM_lRQN1_Ej_iLQ\u003D\u003D;

    public void \u0023\u003Dzk4y25HcID\u0024p8FcTs_CL_Zpw\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      if (!_param1.RenderableSeries.\u0023\u003DzVxrZQ3k9ZBGJ((\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 3))
        return;
      this.\u0023\u003Dz1H5OM_lRQN1_Ej_iLQ\u003D\u003D.Add(_param1);
    }
  }

  private sealed class \u0023\u003DzZWngLGgklhPywGpG5ND4_Zk\u003D : 
    IDisposable,
    IEnumerable,
    IEnumerator,
    IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>,
    IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    public VerticalSliceModifier _variableSome3535;
    
    private IEnumerator<VerticalLineAnnotation> \u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D;
    
    private IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D;

    [DebuggerHidden]
    public \u0023\u003DzZWngLGgklhPywGpG5ND4_Zk\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
      int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
      switch (z4fzyEz1SsHya)
      {
        case -4:
        case -3:
        case 1:
          try
          {
            if (z4fzyEz1SsHya != -4 && z4fzyEz1SsHya != 1)
              break;
            try
            {
            }
            finally
            {
              this.\u0023\u003DzhVCln\u0024A7RxgYzcG71w\u003D\u003D();
            }
            break;
          }
          finally
          {
            this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
          }
      }
    }

    bool IEnumerator.MoveNext()
    {
      // ISSUE: fault handler
      try
      {
        int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
        VerticalSliceModifier zRrvwDu67s9Rm = this._variableSome3535;
        switch (z4fzyEz1SsHya)
        {
          case 0:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
            this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D = zRrvwDu67s9Rm.VerticalLines.Where<VerticalLineAnnotation>(VerticalSliceModifier.SomeClass34343383.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D ?? (VerticalSliceModifier.SomeClass34343383.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D = new Func<VerticalLineAnnotation, bool>(VerticalSliceModifier.SomeClass34343383.SomeMethond0343.\u0023\u003DzjmBi8jZjC\u0024j95EOhgQaK4bU\u003D))).GetEnumerator();
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            goto label_9;
          case 1:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -4;
            break;
          default:
            return false;
        }
label_7:
        if (this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D.MoveNext())
        {
          this.\u0023\u003Dzaev1bhaFFIDX = this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D.Current;
          this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
          return true;
        }
        this.\u0023\u003DzhVCln\u0024A7RxgYzcG71w\u003D\u003D();
        this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D = (IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) null;
label_9:
        while (this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.MoveNext())
        {
          VerticalLineAnnotation current = this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.Current;
          if (current.X1 != null && current.XAxis != null)
          {
            double num = current.XAxis.\u0023\u003DzhL6gsJw\u003D(current.X1);
            this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D = zRrvwDu67s9Rm.\u0023\u003DzhtAjDMSkTbY7(new Point(num, 0.0)).GetEnumerator();
            this.\u0023\u003Dz4fzyEZ1SsHYa = -4;
            goto label_7;
          }
        }
        this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
        this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D = (IEnumerator<VerticalLineAnnotation>) null;
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

    private void \u0023\u003DzhVCln\u0024A7RxgYzcG71w\u003D\u003D()
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
      if (this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D == null)
        return;
      this.\u0023\u003Dzc3rfTwtoPKq7U6lxKg\u003D\u003D.Dispose();
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
      VerticalSliceModifier.\u0023\u003DzZWngLGgklhPywGpG5ND4_Zk\u003D lggklhPywGpG5Nd4Zk;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        lggklhPywGpG5Nd4Zk = this;
      }
      else
      {
        lggklhPywGpG5Nd4Zk = new VerticalSliceModifier.\u0023\u003DzZWngLGgklhPywGpG5ND4_Zk\u003D(0);
        lggklhPywGpG5Nd4Zk._variableSome3535 = this._variableSome3535;
      }
      return (IEnumerator<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) lggklhPywGpG5Nd4Zk;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003DzdCr_FD\u0024knmYtmaB5oybNnJEENtOXJw7Q1xWzDzSUVGdOwwA2iYK9I4g\u003D();
    }
  }
}
