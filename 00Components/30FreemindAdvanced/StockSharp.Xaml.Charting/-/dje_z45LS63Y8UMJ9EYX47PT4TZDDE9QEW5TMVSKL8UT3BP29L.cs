// Decompiled with JetBrains decompiler
// Type: -.dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
namespace StockSharp.Xaml.Charting;

[TemplatePart(Name = "PART_ChartModifierSurface", Type = typeof (dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd))]
[TemplatePart(Name = "PART_MainGrid", Type = typeof (Grid))]
internal abstract class dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd : 
  Control,
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv1ei5a44WdHi6c16UXGWhmY1mMHOZA\u003D\u003D,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzKrsZ\u0024JY5ZFLk = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440414), typeof (bool), typeof (dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd), new PropertyMetadata((object) false));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzwRBYwhc7zDyy = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440444), typeof (string), typeof (dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003Dz8nrTMdEZLwbciOTO7Vi3t9Y\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzZlWtNzRQ4OYQ = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351947), typeof (\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7), typeof (dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzbbUGr_yR96RPK_DtNktUUF8\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzjroGW0xeV8YH = DependencyProperty.Register(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539440596), typeof (double?), typeof (dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd \u0023\u003DzO_kDrhvLYpaX;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly object \u0023\u003DzxztcSMfDuTst = new object();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile bool \u0023\u003Dz5Q_\u0024YkuhIHbd;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile bool \u0023\u003DzvBK\u00248KQ\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzg8Ufa_EMXfJU;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private dje_zY8XSKEXRDD9NTMDB93NVVVVR9HXUB78D8XNLDF4MGWTDBTZ_ejd \u0023\u003DzoaInVTVF1PK_;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 \u0023\u003DzK_OtVrQnpn26;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly RoutedEventHandler \u0023\u003DzDBof02j7wNLE;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly RoutedEventHandler \u0023\u003DzCulyDmxVeDrERKl9OA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly SizeChangedEventHandler \u0023\u003DzY4g995NuThV1;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly DependencyPropertyChangedEventHandler \u0023\u003DzvhtXLpDgYpSt;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private volatile bool \u0023\u003Dzc0ALknKZKMZw;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private EventHandler<EventArgs> \u0023\u003DzcyGdlF8\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzHCK5HNRZ8B\u0024Asrf4ngfS7R9rAu5kGLnFo5b5v7I\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D \u0023\u003DzQ7ziF8hV1\u0024a0lbi4RKafaLI\u003D;

  protected dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351955), new object[1]
    {
      (object) ((object) this).GetType().Name
    });
    this.\u0023\u003Dzg8Ufa_EMXfJU = (\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D();
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

  ~dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd()
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
      return (double?) this.GetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzjroGW0xeV8YH);
    }
    set
    {
      this.SetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzjroGW0xeV8YH, (object) value);
    }
  }

  [SpecialName]
  public \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzu\u0024P3XgkcE7BC()
  {
    return this.\u0023\u003Dzg8Ufa_EMXfJU;
  }

  protected internal void \u0023\u003DzrEoWi5uPS0Yz(
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D _param1)
  {
    this.\u0023\u003Dzg8Ufa_EMXfJU = _param1;
  }

  protected bool \u0023\u003Dz5OzJ0EHhtC8P() => this.\u0023\u003DzvBK\u00248KQ\u003D;

  protected bool \u0023\u003DzNIujeQAKJfHjmKL2ONO8jgpF_OJU() => this.\u0023\u003Dz5Q_\u0024YkuhIHbd;

  [SpecialName]
  public object \u0023\u003Dzjatnj7TNvda7() => this.\u0023\u003DzxztcSMfDuTst;

  public string ChartTitle
  {
    get
    {
      return (string) this.GetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzwRBYwhc7zDyy);
    }
    set
    {
      this.SetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzwRBYwhc7zDyy, (object) value);
    }
  }

  public bool ClipModifierSurface
  {
    get
    {
      return (bool) this.GetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzKrsZ\u0024JY5ZFLk);
    }
    set
    {
      this.SetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzKrsZ\u0024JY5ZFLk, (object) value);
    }
  }

  public bool IsSuspended
  {
    get
    {
      return \u0023\u003DzuPRmIFUVJkGxyCE55JH19ZE5sEUdF5DXPLZ7U6Rxl0An.\u0023\u003DzY5RcByYV3P6y((\u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D) this) || !this.\u0023\u003Dzc0ALknKZKMZw;
    }
  }

  [SpecialName]
  public \u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D \u0023\u003DzBgWxEdRxHdEh()
  {
    return (\u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D) this.\u0023\u003DzO_kDrhvLYpaX;
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

  public \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 RenderSurface
  {
    get
    {
      return (\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7) this.GetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzZlWtNzRQ4OYQ);
    }
    set
    {
      this.SetValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzZlWtNzRQ4OYQ, (object) value);
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

  public \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote SuspendUpdates()
  {
    return (\u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote) new \u0023\u003DzuPRmIFUVJkGxyCE55JH19ZE5sEUdF5DXPLZ7U6Rxl0An((\u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D) this);
  }

  public void ResumeUpdates(
    \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote _param1)
  {
    if (!_param1.\u0023\u003DzuWdUDFWIQOsx())
      return;
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  public void DecrementSuspend()
  {
  }

  public override void OnApplyTemplate()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351999), Array.Empty<object>());
    base.OnApplyTemplate();
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D azbJh76xifd94Ojrda = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D) this.\u0023\u003Dzu\u0024P3XgkcE7BC();
    if (azbJh76xifd94Ojrda.\u0023\u003DzY9a0Q6M\u003D<\u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D>())
      azbJh76xifd94Ojrda.\u0023\u003DzwC3KXAMeFY2h<\u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D>();
    this.\u0023\u003DzO_kDrhvLYpaX = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zLJ8YF663AZWU54HUL92JCTXZ2DPLKDPLLE4N2XAJB7KUMPA_ejd>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539352017));
    this.\u0023\u003DzoaInVTVF1PK_ = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zY8XSKEXRDD9NTMDB93NVVVVR9HXUB78D8XNLDF4MGWTDBTZ_ejd>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539352049));
    ((\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D) this.\u0023\u003Dzu\u0024P3XgkcE7BC()).\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D>(this.\u0023\u003DzBgWxEdRxHdEh());
  }

  public virtual void \u0023\u003Dz5q8i9C4\u003D()
  {
    if (this.IsSuspended)
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539352037), Array.Empty<object>());
    else if (\u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D.\u0023\u003DzMNVV9_LtRFVB() || this.\u0023\u003DzQ6xddArfD502() == (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 0)
    {
      this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>().\u0023\u003Dz40vIrjqAtFMX(new Action(this.\u0023\u003DzOB4dXYeIFfiB), (DispatcherPriority) 9);
    }
    else
    {
      if (this.\u0023\u003DzK_OtVrQnpn26 == null)
        return;
      this.\u0023\u003DzK_OtVrQnpn26.\u0023\u003Dz5q8i9C4\u003D();
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
    if (!(_param0 is \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D ks0apzs3KdsLxgXg))
      return;
    ks0apzs3KdsLxgXg.\u0023\u003Dz5q8i9C4\u003D();
  }

  protected T \u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<T>(string _param1) where T : class
  {
    return this.GetTemplateChild(_param1) is T templateChild ? templateChild : throw new InvalidOperationException(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539343438), (object) _param1));
  }

  protected virtual void \u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D()
  {
    this.\u0023\u003Dz5Q_\u0024YkuhIHbd = false;
  }

  protected virtual void \u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D()
  {
    this.\u0023\u003Dzc0ALknKZKMZw = true;
    this.\u0023\u003Dz5Q_\u0024YkuhIHbd = true;
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  protected virtual void \u0023\u003Dz4NN4xmXb4DExeo6zN7elWhI\u003D()
  {
  }

  protected virtual void \u0023\u003DzUq0D2jBe2UY\u0024(DependencyPropertyChangedEventArgs _param1)
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351889), Array.Empty<object>());
  }

  protected virtual void \u0023\u003DzbJLRM0DSJETF\u0024zflkg\u003D\u003D(
    DependencyPropertyChangedEventArgs _param1)
  {
    this.\u0023\u003DzK_OtVrQnpn26 = _param1.NewValue as \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7;
  }

  protected virtual void \u0023\u003DzLIrH2H7exOOX(
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D _param1)
  {
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>((\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D) new \u0023\u003Dz_tZkxN0xSCAji6_DomQ8vhQ5FfpLq6glVuatd7w\u003D(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzjLVbM_c\u003D((DependencyObject) this)));
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>((\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D) new \u0023\u003DzE2B_RS0KvtqHnw_gRshK2R_QUutRGL5CsoWFaJMJJyTN1jjm1xDOFlg\u003D());
  }

  protected abstract void \u0023\u003DzOB4dXYeIFfiB();

  protected internal void \u0023\u003DzVjhvGWo3fUWc(Exception _param1)
  {
    string str = string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351888), (object) ((object) this).GetType().Name, (object) _param1.Message, (object) _param1.StackTrace);
    Console.WriteLine(str);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(str, Array.Empty<object>());
  }

  internal void \u0023\u003Dz\u0024YPacLjgy1DJ(object _param1, EventArgs _param2)
  {
    if (this.\u0023\u003DzQ6xddArfD502() == (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 3)
      return;
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  internal static Dispatcher \u0023\u003DzjLVbM_c\u003D(DependencyObject _param0)
  {
    return ((DispatcherObject) _param0).Dispatcher;
  }

  bool \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv1ei5a44WdHi6c16UXGWhmY1mMHOZA\u003D\u003D.\u0023\u003Dz59_koqr2EQdapDcFKycZuEkqSVjDl1YEst5SQzVFpw8OzdcTdx\u0024O8XAY037X()
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
    this.RenderSurface = (\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7) null;
    BindingOperations.ClearAllBindings((DependencyObject) this);
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003Dz7qOdpi4\u003D();

    internal void \u0023\u003DzbbUGr_yR96RPK_DtNktUUF8\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd) _param1).\u0023\u003DzbJLRM0DSJETF\u0024zflkg\u003D\u003D(_param2);
    }
  }
}
