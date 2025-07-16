// Decompiled with JetBrains decompiler
// Type: -.SciChartSurface
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

#nullable disable
namespace SciChart.Charting;

[TemplatePart(Name = "PART_ChartModifierSurface", Type = typeof (ChartModifierSurface))]
[TemplatePart(Name = "PART_MainGrid", Type = typeof (Grid))]
internal abstract class SciChartSurfaceBase : 
  Control,
  ISuspendable,
  IUltrachartSurfaceBase,
  IInvalidatableElement
{
  
  public static readonly DependencyProperty \u0023\u003DzKrsZ\u0024JY5ZFLk = DependencyProperty.Register(nameof (ClipModifierSurface), typeof (bool), typeof (SciChartSurface), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzwRBYwhc7zDyy = DependencyProperty.Register(nameof (ChartTitle), typeof (string), typeof (SciChartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(SciChartSurface.\u0023\u003Dz8nrTMdEZLwbciOTO7Vi3t9Y\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzZlWtNzRQ4OYQ = DependencyProperty.Register(nameof (RenderSurface), typeof (IRenderSurface), typeof (SciChartSurface), new PropertyMetadata((object) null, new PropertyChangedCallback(SciChartSurface.SomeClass34343383.SomeMethond0343.\u0023\u003DzbbUGr_yR96RPK_DtNktUUF8\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzjroGW0xeV8YH = DependencyProperty.Register(nameof (MaxFrameRate), typeof (double?), typeof (SciChartSurface), new PropertyMetadata((PropertyChangedCallback) null));
  
  private ChartModifierSurface \u0023\u003DzO_kDrhvLYpaX;
  
  private readonly object myLock = new object();
  
  private volatile bool \u0023\u003Dz5Q_\u0024YkuhIHbd;
  
  private volatile bool \u0023\u003DzvBK\u00248KQ\u003D;
  
  private IServiceContainer \u0023\u003Dzg8Ufa_EMXfJU;
  
  private MainGrid \u0023\u003DzoaInVTVF1PK_;
  
  private IRenderSurface \u0023\u003DzK_OtVrQnpn26;
  
  private readonly RoutedEventHandler \u0023\u003DzDBof02j7wNLE;
  
  private readonly RoutedEventHandler \u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;
  
  private readonly SizeChangedEventHandler \u0023\u003DzY4g995NuThV1;
  
  private readonly DependencyPropertyChangedEventHandler \u0023\u003DzvhtXLpDgYpSt;
  
  private volatile bool \u0023\u003Dzc0ALknKZKMZw;
  
  private EventHandler<EventArgs> \u0023\u003DzcyGdlF8\u003D;
  
  private bool \u0023\u003DzHCK5HNRZ8B\u0024Asrf4ngfS7R9rAu5kGLnFo5b5v7I\u003D;
  
  private \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D \u0023\u003DzQ7ziF8hV1\u0024a0lbi4RKafaLI\u003D;

  protected SciChartSurfaceBase()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Instantiating {0}", new object[1]
    {
      (object) ((object) this).GetType().Name
    });
    this.\u0023\u003Dzg8Ufa_EMXfJU = (IServiceContainer) new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D();
    this.\u0023\u003DzLIrH2H7exOOX(this.\u0023\u003Dzg8Ufa_EMXfJU);
    this.\u0023\u003DzDBof02j7wNLE = new RoutedEventHandler(this.\u0023\u003Dzu0nSxY0YBkzHs9q32EBLwew\u003D);
    this.\u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D = new RoutedEventHandler(this.\u0023\u003DzrzYTHp38TGfs9tC\u0024dk8sDH8\u003D);
    this.\u0023\u003DzY4g995NuThV1 = new SizeChangedEventHandler(this.\u0023\u003DzofFqpEB0_tdiiRo_d6wqrAg\u003D);
    this.\u0023\u003DzvhtXLpDgYpSt = new DependencyPropertyChangedEventHandler(this.\u0023\u003Dz68IlIZhApGfytSosu43Dk_E\u003D);
    this.SizeChanged += this.\u0023\u003DzY4g995NuThV1;
    this.Loaded += this.\u0023\u003DzDBof02j7wNLE;
    this.Unloaded += this.\u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;
    this.DataContextChanged += this.\u0023\u003DzvhtXLpDgYpSt;
    this.\u0023\u003Dz8zI9y6yvy4ZW6q\u0024mRPx1\u0024Lw0dpT2lbFRkg\u003D\u003D(false);
    this.\u0023\u003DzB9Fp01Mv4lWu((\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 1);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzKPHSi1vgK\u0024Fx(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzcyGdlF8\u003D;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzcyGdlF8\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzrRRdxqQwy\u0024OJ(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzcyGdlF8\u003D;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzcyGdlF8\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  ~SciChartSurface()
  {
    try
    {
      this.\u0023\u003DzTuM3X1E\u003D(false);
    }
    finally
    {
      // ISSUE: explicit finalizer call
      // ISSUE: explicit non-virtual call
      __nonvirtual (((object) this).Finalize());
    }
  }

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003DzBR\u0024QbsqohI1Fe2cEP8ix4DYKHfMwrzbUAA\u003D\u003D()
  {
    return this.\u0023\u003DzHCK5HNRZ8B\u0024Asrf4ngfS7R9rAu5kGLnFo5b5v7I\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz8zI9y6yvy4ZW6q\u0024mRPx1\u0024Lw0dpT2lbFRkg\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzHCK5HNRZ8B\u0024Asrf4ngfS7R9rAu5kGLnFo5b5v7I\u003D = _param1;
  }

  public double? MaxFrameRate
  {
    get
    {
      return (double?) this.GetValue(SciChartSurface.\u0023\u003DzjroGW0xeV8YH);
    }
    set
    {
      this.SetValue(SciChartSurface.\u0023\u003DzjroGW0xeV8YH, (object) value);
    }
  }

  [SpecialName]
  public IServiceContainer Services()
  {
    return this.\u0023\u003Dzg8Ufa_EMXfJU;
  }

  protected internal void Services(
    IServiceContainer _param1)
  {
    this.\u0023\u003Dzg8Ufa_EMXfJU = _param1;
  }

  protected bool IsDisposed() => this.\u0023\u003DzvBK\u00248KQ\u003D;

  protected bool \u0023\u003DzNIujeQAKJfHjmKL2ONO8jgpF_OJU() => this.\u0023\u003Dz5Q_\u0024YkuhIHbd;

  [SpecialName]
  public object \u0023\u003Dzjatnj7TNvda7() => this.myLock;

  public string ChartTitle
  {
    get
    {
      return (string) this.GetValue(SciChartSurface.\u0023\u003DzwRBYwhc7zDyy);
    }
    set
    {
      this.SetValue(SciChartSurface.\u0023\u003DzwRBYwhc7zDyy, (object) value);
    }
  }

  public bool ClipModifierSurface
  {
    get
    {
      return (bool) this.GetValue(SciChartSurface.\u0023\u003DzKrsZ\u0024JY5ZFLk);
    }
    set
    {
      this.SetValue(SciChartSurface.\u0023\u003DzKrsZ\u0024JY5ZFLk, (object) value);
    }
  }

  public bool IsSuspended
  {
    get
    {
      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this) || !this.\u0023\u003Dzc0ALknKZKMZw;
    }
  }

  [SpecialName]
  public IChartModifierSurface \u0023\u003DzBgWxEdRxHdEh()
  {
    return (IChartModifierSurface) this.\u0023\u003DzO_kDrhvLYpaX;
  }

  [CompilerGenerated]
  [SpecialName]
  public \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D \u0023\u003DzQ6xddArfD502()
  {
    return this.\u0023\u003DzQ7ziF8hV1\u0024a0lbi4RKafaLI\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzB9Fp01Mv4lWu(
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D _param1)
  {
    this.\u0023\u003DzQ7ziF8hV1\u0024a0lbi4RKafaLI\u003D = _param1;
  }

  public IRenderSurface RenderSurface
  {
    get
    {
      return (IRenderSurface) this.GetValue(SciChartSurface.\u0023\u003DzZlWtNzRQ4OYQ);
    }
    set
    {
      this.SetValue(SciChartSurface.\u0023\u003DzZlWtNzRQ4OYQ, (object) value);
    }
  }

  [SpecialName]
  public \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D \u0023\u003Dzwc4Gzka23TGB()
  {
    return (\u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D) this.\u0023\u003DzoaInVTVF1PK_;
  }

  public virtual void \u0023\u003DznV0cpNo\u003D()
  {
    this.\u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D();
  }

  public IUpdateSuspender SuspendUpdates()
  {
    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this);
  }

  public void ResumeUpdates(
    IUpdateSuspender _param1)
  {
    if (!_param1.ResumeTargetOnDispose)
      return;
    this.InvalidateElement();
  }

  public void DecrementSuspend()
  {
  }

  public override void OnApplyTemplate()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(nameof (OnApplyTemplate), Array.Empty<object>());
    base.OnApplyTemplate();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D azbJh76xifd94Ojrda = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D) this.Services();
    if (azbJh76xifd94Ojrda.\u0023\u003DzY9a0Q6M\u003D<IChartModifierSurface>())
      azbJh76xifd94Ojrda.\u0023\u003DzwC3KXAMeFY2h<IChartModifierSurface>();
    this.\u0023\u003DzO_kDrhvLYpaX = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<ChartModifierSurface>("PART_ChartModifierSurface");
    this.\u0023\u003DzoaInVTVF1PK_ = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<MainGrid>("PART_MainGrid");
    ((\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D) this.Services()).\u0023\u003Dz7wSH25w\u003D<IChartModifierSurface>(this.\u0023\u003DzBgWxEdRxHdEh());
  }

  public virtual void InvalidateElement()
  {
    if (this.IsSuspended)
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("UltrachartSurface.IsSuspended=true. Ignoring InvalidateElement() call", Array.Empty<object>());
    else if (\u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D.\u0023\u003DzMNVV9_LtRFVB() || this.\u0023\u003DzQ6xddArfD502() == (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 0)
    {
      this.Services().GetService<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>().\u0023\u003Dz40vIrjqAtFMX(new Action(this.\u0023\u003DzOB4dXYeIFfiB), (DispatcherPriority) 9);
    }
    else
    {
      if (this.\u0023\u003DzK_OtVrQnpn26 == null)
        return;
      this.\u0023\u003DzK_OtVrQnpn26.InvalidateElement();
    }
  }

  public virtual void \u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D()
  {
    EventHandler<EventArgs> zcyGdlF8 = this.\u0023\u003DzcyGdlF8\u003D;
    if (zcyGdlF8 == null)
      return;
    zcyGdlF8((object) this, EventArgs.Empty);
  }

  public void \u0023\u003DzqFIyyIbnwGLq(Cursor _param1)
  {
    this.SetCurrentValue(FrameworkElement.CursorProperty, (object) _param1);
  }

  public void Dispose()
  {
    this.\u0023\u003DzTuM3X1E\u003D(true);
    GC.SuppressFinalize((object) this);
  }

  protected virtual void \u0023\u003DzTuM3X1E\u003D(bool _param1)
  {
    if (this.\u0023\u003DzvBK\u00248KQ\u003D)
      return;
    ((DispatcherObject) this).Dispatcher.BeginInvoke((Delegate) new Action(this.\u0023\u003Dz77iVqRbRA\u0024bf5M2K2g\u003D\u003D), Array.Empty<object>());
    this.\u0023\u003DzvBK\u00248KQ\u003D = true;
    ((DispatcherObject) this).Dispatcher.BeginInvoke((Delegate) new Action(this.\u0023\u003DzqWRedN_h0w8urqA2fw\u003D\u003D), Array.Empty<object>());
  }

  protected static void \u0023\u003Dz8nrTMdEZLwbciOTO7Vi3t9Y\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is IInvalidatableElement ks0apzs3KdsLxgXg))
      return;
    ks0apzs3KdsLxgXg.InvalidateElement();
  }

  protected T \u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<T>(string _param1) where T : class
  {
    return this.GetTemplateChild(_param1) is T templateChild ? templateChild : throw new InvalidOperationException($"Unable to Apply the Control Template. {_param1} is missing or of the wrong type");
  }

  protected virtual void \u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D()
  {
    this.\u0023\u003Dz5Q_\u0024YkuhIHbd = false;
  }

  protected virtual void \u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D()
  {
    this.\u0023\u003Dzc0ALknKZKMZw = true;
    this.\u0023\u003Dz5Q_\u0024YkuhIHbd = true;
    this.InvalidateElement();
  }

  protected virtual void \u0023\u003Dz4NN4xmXb4DExeo6zN7elWhI\u003D()
  {
  }

  protected virtual void \u0023\u003DzUq0D2jBe2UY\u0024(DependencyPropertyChangedEventArgs _param1)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("OnDataContextChanged", Array.Empty<object>());
  }

  protected virtual void \u0023\u003DzbJLRM0DSJETF\u0024zflkg\u003D\u003D(
    DependencyPropertyChangedEventArgs _param1)
  {
    this.\u0023\u003DzK_OtVrQnpn26 = _param1.NewValue as IRenderSurface;
  }

  protected virtual void \u0023\u003DzLIrH2H7exOOX(
    IServiceContainer _param1)
  {
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>((\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D) new \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D(SciChartSurface.\u0023\u003DzjLVbM_c\u003D((DependencyObject) this)));
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>((\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D) new \u0023\u003DzE2B_RS0KvtqHnw_gRshK2R_QUutRGL5CsoWFaJMJJyTN1jjm1xDOFlg\u003D());
  }

  protected abstract void \u0023\u003DzOB4dXYeIFfiB();

  protected internal void \u0023\u003DzVjhvGWo3fUWc(Exception _param1)
  {
    string str = $" {((object) this).GetType().Name} didn't render, because an exception was thrown:\n  Message: {_param1.Message}\n  Stack Trace: {_param1.StackTrace}";
    Console.WriteLine(str);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(str, Array.Empty<object>());
  }

  internal void \u0023\u003Dz\u0024YPacLjgy1DJ(object _param1, EventArgs _param2)
  {
    if (this.\u0023\u003DzQ6xddArfD502() == (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 3)
      return;
    this.InvalidateElement();
  }

  internal static Dispatcher \u0023\u003DzjLVbM_c\u003D(DependencyObject _param0)
  {
    return ((DispatcherObject) _param0).Dispatcher;
  }

  bool IUltrachartSurfaceBase.\u0023\u003Dz59_koqr2EQdapDcFKycZuEkqSVjDl1YEst5SQzVFpw8OzdcTdx\u0024O8XAY037X()
  {
    return this.IsVisible;
  }

  private void \u0023\u003Dzu0nSxY0YBkzHs9q32EBLwew\u003D(object _param1, RoutedEventArgs _param2)
  {
    this.\u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D();
  }

  private void \u0023\u003DzrzYTHp38TGfs9tC\u0024dk8sDH8\u003D(
    object _param1,
    RoutedEventArgs _param2)
  {
    this.\u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D();
  }

  private void \u0023\u003DzofFqpEB0_tdiiRo_d6wqrAg\u003D(
    object _param1,
    SizeChangedEventArgs _param2)
  {
    this.\u0023\u003Dz4NN4xmXb4DExeo6zN7elWhI\u003D();
  }

  private void \u0023\u003Dz68IlIZhApGfytSosu43Dk_E\u003D(
    object _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003DzUq0D2jBe2UY\u0024(_param2);
  }

  private void \u0023\u003Dz77iVqRbRA\u0024bf5M2K2g\u003D\u003D()
  {
    this.DataContextChanged -= this.\u0023\u003DzvhtXLpDgYpSt;
    this.SizeChanged -= this.\u0023\u003DzY4g995NuThV1;
    this.Loaded -= this.\u0023\u003DzDBof02j7wNLE;
    this.Unloaded -= this.\u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;
    this.\u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D();
    if (this.\u0023\u003DzoaInVTVF1PK_ == null)
      return;
    this.\u0023\u003DzoaInVTVF1PK_.\u0023\u003Dz2_LGffrmWHwH();
  }

  private void \u0023\u003DzqWRedN_h0w8urqA2fw\u003D\u003D()
  {
    this.RenderSurface = (IRenderSurface) null;
    BindingOperations.ClearAllBindings((DependencyObject) this);
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly SciChartSurface.SomeClass34343383 SomeMethond0343 = new SciChartSurface.SomeClass34343383();

    internal void \u0023\u003DzbbUGr_yR96RPK_DtNktUUF8\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((SciChartSurface) _param1).\u0023\u003DzbJLRM0DSJETF\u0024zflkg\u003D\u003D(_param2);
    }
  }
}
